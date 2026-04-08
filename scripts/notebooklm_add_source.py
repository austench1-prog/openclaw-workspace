#!/usr/bin/env python3
"""
NotebookLM Source Adder
Uses existing Chrome profile to add sources to MATS_v1_Compliance notebook
"""

import time
from playwright.sync_api import sync_playwright

CHROME_PROFILE = "/Users/austinai/Library/Application Support/Google/Chrome"
NOTEBOOKLM_URL = "https://notebooklm.google.com"

SOURCE_TEXT = """# Prop Firm 考试账户规则对比
最后更新：2026-04-08 | 仅限考试账户（Evaluation）

重要说明：以下数据来自官网及第三方评测，规则可能随时变动。使用前请对照账户实际合同核实。

## Apex Trader Funding
Drawdown类型：Intraday Trailing（最严格）
Drawdown跟随未实现盈亏实时移动，浮盈越高，底线越高。

$50K账户：利润目标 $3,000 / Trailing Drawdown $2,500 / 最低7天 / 无一致性规则

关键规则：
- 无 Daily Loss Limit
- 无一致性规则（考试期）
- 允许新闻交易
- Tradovate账户：Drawdown一直追踪，不停止
- Rithmic账户：Drawdown到达利润目标后停止追踪
- 陷阱：浮盈 $2000，drawdown线上移 $2000，平仓后线不再下移
- 隔夜持仓：禁止，必须在16:59 ET前平仓

## MyFundedFutures（MFF）
Drawdown类型：EOD Trailing（日终才结算，不跟日内浮盈）

$50K账户：利润目标 $3,000 / Max Drawdown $2,000 / 最低5天 / 50%一致性规则
一致性规则：单日盈利不超过总盈利目标的50%（基数为目标金额$3000，非当前盈利）
EOD Drawdown特点：底线追到起始余额+$100后停止（变为Static）
新闻交易：允许

## TradeDay
Drawdown类型：Static Max（底线永不移动）
关键规则：
- 30%一致性规则
- 最低5个交易天数
- 禁止过夜持仓
- 禁止在Tier 1数据发布前后持仓（非农、CPI等）
- 最高95%利润分成

## 系统级通用风控协议（草案）
- 统一强平时间：美东时间 16:00 强制平仓
- 只参与完整交易日，半天假/节假日不参与
- 所有开仓必须附带止损单

## 当前账户状态（2026-04-08）
- Apex APEX-165583-123：$50K / 净值$49,932 / DD上限$2,500 / Active测试账户
- MFF MFFUEVRPD122274040：$50K / 净值$48,084 / DD剩$12.94 / 停用等待新账户
"""

SOURCE_TITLE = "Prop_Firm_Rules_Internal_v1"

def main():
    with sync_playwright() as p:
        print("启动 Chrome（使用已登入的 profile）...")
        browser = p.chromium.launch_persistent_context(
            user_data_dir=CHROME_PROFILE,
            headless=False,
            channel="chrome",
            args=["--no-first-run", "--no-default-browser-check"]
        )
        
        page = browser.new_page()
        print(f"打开 NotebookLM...")
        page.goto(NOTEBOOKLM_URL)
        page.wait_for_load_state("networkidle", timeout=30000)
        time.sleep(3)
        
        print(f"当前页面：{page.url}")
        print(f"页面标题：{page.title()}")
        
        # 截图看当前状态
        page.screenshot(path="/Users/austinai/.openclaw/workspace/scripts/notebooklm_state.png")
        print("截图已保存：notebooklm_state.png")
        
        # 找 MATS_v1_Compliance notebook
        print("寻找 MATS_v1_Compliance notebook...")
        time.sleep(2)
        
        # 尝试点击已有的 notebook
        try:
            notebook = page.locator("text=MATS_v1_Compliance").first
            notebook.wait_for(timeout=10000)
            notebook.click()
            print("找到并点击了 MATS_v1_Compliance")
            time.sleep(3)
            page.screenshot(path="/Users/austinai/.openclaw/workspace/scripts/notebooklm_notebook.png")
            print("截图已保存：notebooklm_notebook.png")
        except Exception as e:
            print(f"找不到 notebook: {e}")
            page.screenshot(path="/Users/austinai/.openclaw/workspace/scripts/notebooklm_error.png")
            print("错误截图已保存：notebooklm_error.png")
        
        print("\n完成。浏览器保持打开，总裁可以查看。")
        print("按 Ctrl+C 退出脚本（浏览器会继续运行）")
        
        # 保持运行
        input("按 Enter 键退出...")
        browser.close()

if __name__ == "__main__":
    main()
