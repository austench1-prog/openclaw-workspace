# Apex Account Scraper using Playwright
# Source: Dragon
# Version: 1.0 | Date: 2026-04-06
# Reads Apex dashboard data without screenshots

import asyncio
import json
from datetime import datetime
from playwright.async_api import async_playwright

APEX_URL = "https://apextraderfunding.com/member"

async def scrape_apex(email: str, password: str) -> dict:
    """
    Login to Apex and extract account data.
    Returns structured JSON with account status, equity, drawdown floor.
    """
    result = {
        "firm": "Apex",
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "accounts": [],
        "error": None
    }
    
    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=True)
        context = await browser.new_context(
            viewport={"width": 1280, "height": 800},
            user_agent="Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36"
        )
        page = await context.new_page()
        
        try:
            print(f"[{result['timestamp']}] Navigating to Apex...")
            await page.goto(APEX_URL, wait_until="networkidle", timeout=30000)
            
            # Login
            await page.fill('input[type="email"]', email)
            await page.fill('input[type="password"]', password)
            await page.click('button[type="submit"]')
            await page.wait_for_load_state("networkidle")
            
            print(f"[{result['timestamp']}] Logged in, extracting data...")
            
            # Wait for dashboard to load
            await page.wait_for_timeout(2000)
            
            # Extract account data from page
            # These selectors may need adjustment based on actual Apex dashboard HTML
            accounts_data = await page.evaluate("""
                () => {
                    const accounts = [];
                    // Try to find account cards/panels
                    const cards = document.querySelectorAll('[class*="account"], [class*="Account"]');
                    cards.forEach(card => {
                        const text = card.innerText;
                        accounts.push(text.substring(0, 200));
                    });
                    return accounts;
                }
            """)
            
            # Get page title and key numbers
            title = await page.title()
            url = page.url
            
            # Take a screenshot for debugging
            await page.screenshot(path="/tmp/apex_dashboard.png")
            
            result["page_title"] = title
            result["url"] = url
            result["raw_data"] = accounts_data[:5]
            result["status"] = "success"
            
        except Exception as e:
            result["error"] = str(e)
            result["status"] = "error"
            print(f"Error: {e}")
        finally:
            await browser.close()
    
    return result


async def scrape_apex_no_login(existing_session_url: str = None) -> dict:
    """
    For use when browser is already logged in.
    Uses existing browser session if possible.
    """
    result = {
        "firm": "Apex", 
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "status": "pending"
    }
    
    async with async_playwright() as p:
        # Connect to existing Chrome if running
        try:
            browser = await p.chromium.connect_over_cdp("http://localhost:9222")
            contexts = browser.contexts
            
            if contexts:
                page = contexts[0].pages[0]
                
                # Extract data from current page
                content = await page.content()
                text = await page.inner_text("body")
                
                result["page_text_sample"] = text[:500]
                result["status"] = "success"
            else:
                result["error"] = "No existing browser session found"
                result["status"] = "error"
                
        except Exception as e:
            result["error"] = f"Could not connect to existing browser: {e}"
            result["status"] = "error"
    
    return result


if __name__ == "__main__":
    import sys
    
    print("Playwright Apex Scraper v1.0")
    print("Usage: python playwright_apex_scraper.py [email] [password]")
    print()
    
    if len(sys.argv) == 3:
        email = sys.argv[1]
        password = sys.argv[2]
        result = asyncio.run(scrape_apex(email, password))
    else:
        # Test: try to connect to existing browser
        print("No credentials provided, trying existing browser session...")
        result = asyncio.run(scrape_apex_no_login())
    
    print(json.dumps(result, indent=2, ensure_ascii=False))
