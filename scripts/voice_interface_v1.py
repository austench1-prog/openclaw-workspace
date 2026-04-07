#!/usr/bin/env python3
# Voice Interface V1 - Dragon Voice Core
# Source: Dragon | Version: 1.0 | Date: 2026-04-07
# Task: Voice-Core-V1
# Target: < 3 second response time
# Voice: Calm, rational, concise male voice (system engineer / assistant)

import os
import json
import time
import tempfile
import requests
from datetime import datetime

# Config
OPENAI_API_KEY = os.environ.get("OPENAI_API_KEY", "")
ELEVENLABS_API_KEY = os.environ.get("ELEVENLABS_API_KEY", "")

# TTS provider: "openai" or "elevenlabs"
TTS_PROVIDER = "openai"

# OpenAI TTS voice (calm, neutral male)
OPENAI_VOICE = "onyx"  # onyx = calm, authoritative male

# ElevenLabs voice ID (configure after account setup)
ELEVENLABS_VOICE_ID = ""


def transcribe_audio(audio_file_path: str) -> dict:
    """
    Transcribe audio using OpenAI Whisper.
    Returns: {text, confidence, duration}
    """
    if not OPENAI_API_KEY:
        return {"error": "OpenAI API key not configured", "text": ""}

    t_start = time.time()

    with open(audio_file_path, 'rb') as f:
        response = requests.post(
            "https://api.openai.com/v1/audio/transcriptions",
            headers={"Authorization": f"Bearer {OPENAI_API_KEY}"},
            files={"file": f},
            data={
                "model": "whisper-1",
                "language": "zh",
                "response_format": "verbose_json"
            },
            timeout=10
        )

    duration = time.time() - t_start

    if response.status_code == 200:
        data = response.json()
        return {
            "text": data.get("text", ""),
            "duration_ms": round(duration * 1000),
            "language": data.get("language", ""),
        }
    else:
        return {"error": response.text, "text": ""}


def parse_intent(text: str) -> dict:
    """
    Parse trading intent from transcribed text.
    Returns: {intent, action, instrument, quantity, sl, tp, confirmation_required}
    """
    text_upper = text.upper()

    # Simple keyword matching for trading commands
    intent = {
        "raw_text": text,
        "intent": "unknown",
        "action": None,
        "instrument": None,
        "quantity": 1,
        "sl_points": None,
        "tp_points": None,
        "confirmation_required": False
    }

    # Detect instrument
    for inst in ["NQ", "MNQ", "ES", "MES", "SPX", "GC", "MGC"]:
        if inst in text_upper:
            intent["instrument"] = inst
            break

    # Detect action
    if any(w in text_upper for w in ["买", "做多", "BUY", "LONG", "多"]):
        intent["action"] = "BUY"
        intent["intent"] = "trade"
    elif any(w in text_upper for w in ["卖", "做空", "SELL", "SHORT", "空"]):
        intent["action"] = "SELL"
        intent["intent"] = "trade"
    elif any(w in text_upper for w in ["平仓", "CLOSE", "FLATTEN", "出场"]):
        intent["action"] = "CLOSE"
        intent["intent"] = "close"
    elif any(w in text_upper for w in ["账户", "余额", "状态", "报告"]):
        intent["intent"] = "status"
    elif any(w in text_upper for w in ["止损", "SL", "STOP"]):
        intent["intent"] = "modify_sl"
        intent["confirmation_required"] = True

    # High-risk actions require confirmation
    if intent["intent"] in ["trade", "close", "modify_sl"]:
        intent["confirmation_required"] = True

    return intent


def synthesize_speech(text: str, output_path: str = None) -> str:
    """
    Convert text to speech.
    Returns path to audio file.
    """
    if output_path is None:
        output_path = tempfile.mktemp(suffix=".mp3")

    if TTS_PROVIDER == "openai" and OPENAI_API_KEY:
        response = requests.post(
            "https://api.openai.com/v1/audio/speech",
            headers={
                "Authorization": f"Bearer {OPENAI_API_KEY}",
                "Content-Type": "application/json"
            },
            json={
                "model": "tts-1",
                "input": text,
                "voice": OPENAI_VOICE,
                "speed": 1.1  # Slightly faster for efficiency
            },
            timeout=10
        )

        if response.status_code == 200:
            with open(output_path, 'wb') as f:
                f.write(response.content)
            return output_path

    elif TTS_PROVIDER == "elevenlabs" and ELEVENLABS_API_KEY and ELEVENLABS_VOICE_ID:
        response = requests.post(
            f"https://api.elevenlabs.io/v1/text-to-speech/{ELEVENLABS_VOICE_ID}",
            headers={
                "xi-api-key": ELEVENLABS_API_KEY,
                "Content-Type": "application/json"
            },
            json={
                "text": text,
                "model_id": "eleven_multilingual_v2",
                "voice_settings": {
                    "stability": 0.8,
                    "similarity_boost": 0.8
                }
            },
            timeout=10
        )

        if response.status_code == 200:
            with open(output_path, 'wb') as f:
                f.write(response.content)
            return output_path

    return None


def build_confirmation_text(intent: dict) -> str:
    """Build confirmation speech for high-risk actions."""
    action = intent.get("action")
    instrument = intent.get("instrument", "未知品种")
    qty = intent.get("quantity", 1)

    if action == "BUY":
        return f"收到，准备买入 {instrument} {qty}手，请确认。"
    elif action == "SELL":
        return f"收到，准备卖出 {instrument} {qty}手，请确认。"
    elif action == "CLOSE":
        return f"收到，准备平仓所有仓位，请确认。"
    elif intent.get("intent") == "modify_sl":
        return f"收到止损修改指令，请确认具体参数。"
    return "收到指令，请确认。"


def process_voice_command(audio_file_path: str) -> dict:
    """
    Main pipeline: audio → text → intent → response
    Target: < 3 seconds total
    """
    t_total_start = time.time()
    result = {"timestamp": datetime.now().isoformat()}

    # Step 1: Transcribe
    t1 = time.time()
    transcription = transcribe_audio(audio_file_path)
    result["transcription"] = transcription
    result["transcribe_ms"] = round((time.time() - t1) * 1000)

    if not transcription.get("text"):
        result["error"] = "Transcription failed or empty"
        return result

    # Step 2: Parse intent
    intent = parse_intent(transcription["text"])
    result["intent"] = intent

    # Step 3: Build response
    if intent["confirmation_required"] and intent["intent"] in ["trade", "close"]:
        response_text = build_confirmation_text(intent)
    elif intent["intent"] == "status":
        response_text = "正在读取账户状态，请稍候。"
    elif intent["intent"] == "unknown":
        # Low confidence → switch to text
        response_text = None
        result["fallback_to_text"] = True
        result["fallback_message"] = f"语音识别到：{transcription['text']}，请用文字确认指令。"
    else:
        response_text = f"收到：{transcription['text']}"

    # Step 4: Synthesize response
    if response_text:
        t3 = time.time()
        audio_path = synthesize_speech(response_text)
        result["response_text"] = response_text
        result["response_audio"] = audio_path
        result["tts_ms"] = round((time.time() - t3) * 1000)

    result["total_ms"] = round((time.time() - t_total_start) * 1000)
    result["within_3s"] = result["total_ms"] < 3000

    return result


if __name__ == "__main__":
    print("Voice Interface V1 - System Check")
    print(f"OpenAI API: {'✅ Configured' if OPENAI_API_KEY else '❌ Missing'}")
    print(f"ElevenLabs: {'✅ Configured' if ELEVENLABS_API_KEY else '⚠️ Not configured (optional)'}")
    print(f"TTS Provider: {TTS_PROVIDER}")
    print(f"Voice: {OPENAI_VOICE}")
    print("\nModule ready. Waiting for audio input.")
    print("To test: provide an audio file path as argument")
