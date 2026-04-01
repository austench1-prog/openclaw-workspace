# Camarilla TH/TL Zone
# ThinkorSwim ThinkScript
# 用途：在5分钟图上显示当日 Camarilla TH / TL 区间
# 公式：
#   P  = (昨H + 昨L + 昨C) / 3
#   B  = P + 今日开盘O
#   TH = B - 昨L
#   TL = B - 昨H

declare upper;

# ================================================================
# 模块1：获取昨日日线 H / L / C
# ================================================================
def prevH = high(period = AggregationPeriod.DAY)[1];
def prevL = low(period = AggregationPeriod.DAY)[1];
def prevC = close(period = AggregationPeriod.DAY)[1];

# ================================================================
# 模块2：获取今日 09:30 开盘价（RTH 第一根 bar 的 open）
# ================================================================
def isRTHOpen = SecondsFromTime(0930) == 0;

def todayO = if isRTHOpen then open
             else todayO[1];

# ================================================================
# 模块3：计算 P / B / TH / TL（每日固定，不变）
# ================================================================
def P  = (prevH + prevL + prevC) / 3;
def B  = P + todayO;
def TH = B - prevL;
def TL = B - prevH;

# ================================================================
# 模块4：绘图
# ================================================================

# TH 线（目标高点）
plot TargetHigh = TH;
TargetHigh.SetDefaultColor(Color.GREEN);
TargetHigh.SetLineWeight(2);
TargetHigh.SetStyle(Curve.FIRM);
TargetHigh.HideBubble();

# TL 线（目标低点）
plot TargetLow = TL;
TargetLow.SetDefaultColor(Color.RED);
TargetLow.SetLineWeight(2);
TargetLow.SetStyle(Curve.FIRM);
TargetLow.HideBubble();

# 中间区域填充（TH ~ TL 之间涂色）
DefineGlobalColor("ZoneFill", CreateColor(100, 149, 237));  # 蓝色

AddCloud(TargetHigh, TargetLow, GlobalColor("ZoneFill"), GlobalColor("ZoneFill"));

# 标签（显示在 Y 轴）
TargetHigh.SetPaintingStrategy(PaintingStrategy.HORIZONTAL);
TargetLow.SetPaintingStrategy(PaintingStrategy.HORIZONTAL);

# ================================================================
# 右下角信息框（Labels）
# ================================================================
AddLabel(yes,
    "昨H: " + AsPrice(prevH) +
    "  昨L: " + AsPrice(prevL) +
    "  昨C: " + AsPrice(prevC),
    Color.GRAY);

AddLabel(yes,
    "开盘O: " + AsPrice(todayO),
    Color.WHITE);

AddLabel(yes,
    "TH: " + AsPrice(TH),
    Color.GREEN);

AddLabel(yes,
    "TL: " + AsPrice(TL),
    Color.RED);

AddLabel(yes,
    "P: " + AsPrice(P) + "  B: " + AsPrice(B),
    Color.CYAN);

# ================================================================
# 告警（可选）
# ================================================================
Alert(close >= TH, "价格触及 TH: " + AsPrice(TH), Alert.BAR, Sound.Bell);
Alert(close <= TL, "价格触及 TL: " + AsPrice(TL), Alert.BAR, Sound.Bell);
