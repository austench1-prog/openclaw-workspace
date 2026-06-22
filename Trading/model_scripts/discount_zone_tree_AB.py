# -*- coding: utf-8 -*-
# A+B 完整笔级决策树 (结构, 先不填最终数字, 穷尽所有路径)
# 时间轴确定: A 先 -> B 后. A同构B. 区间各50点.
# A关闭(成功突破A高点)才进入B. A任何失败分支 = 终止, B不建.

print("="*74)
print("完整决策树: 原始交易(1楼) → 区域A(2楼) → 区域B(3楼)")
print("时间轴确定(A先B后), A同构B, 各50点. 下层失败=终止.")
print("="*74)

# 单个区域内部的结构 (A 和 B 相同) -- 返回该区域的所有"内部结局"
# 每个区域进场后:
#   进场1手(止损先放0) -> 分叉: 碰25% / 没碰25%
#   碰25%: 加4手(共5手) -> 反弹回50%? 
#            反弹成功 -> 减3手(必正) -> 剩2手冲最高TP -> 到TP / 保本出
#            反弹失败 -> 5手下行 -> 破第一STOP(5手损) / 继续破Original(5手+底仓爆)
#   没碰25%: 回50%加3手(共4手) -> 往上?
#            往上 -> 4手冲最高TP -> 到TP / 保本出
#            往下 -> 破第一STOP(4手损) / 继续破Original(4手+底仓爆)

def region_outcomes(name):
    # 返回 [(结局标签, 是否区域成功可进下一层, 备注)]
    return [
        # --- 碰25% 路径 ---
        (f"{name}碰25%-反弹-剩2手到最高TP",      True,  "大赢: 3笔×12.5(减仓) + 2手到顶TP"),
        (f"{name}碰25%-反弹-剩2手保本出",        True,  "3笔×12.5(减仓) + 2手保本"),
        (f"{name}碰25%-反弹失败-破第一STOP",     False, "5手全损, 底仓还在"),
        (f"{name}碰25%-反弹失败-破Original",     False, "5手+底仓 全爆 (最坏)"),
        # --- 没碰25% 路径 ---
        (f"{name}没碰25%-往上-4手到最高TP",       True,  "大赢: 4手到顶TP"),
        (f"{name}没碰25%-往上-4手保本出",         True,  "4手保本"),
        (f"{name}没碰25%-往下-破第一STOP",        False, "4手全损, 底仓还在"),
        (f"{name}没碰25%-往下-破Original",        False, "4手+底仓 全爆"),
    ]

A = region_outcomes("A")
B = region_outcomes("B")

print(f"\n单个区域内部结局数 = {len(A)} 个")
print("  其中 区域成功(可进下一层) =", sum(1 for o in A if o[1]), "个")
print("       区域失败(终止)      =", sum(1 for o in A if not o[1]), "个")

# ---- 1楼 原始交易: 它本身也有成/败, 但 A/B 是它内部的子结构 ----
# 按三层楼逻辑: 原始交易成功展开 = 通过内部A,B走完到 original TP
#   原始交易失败 = 还没形成A就破了 original stop = 叶[原始直接失败]
print("\n" + "="*74)
print("完整路径枚举")
print("="*74)

leaves = []

# 叶0: 原始交易直接失败 (内部还没形成有效A就破original stop)
leaves.append(("原始交易直接失败(未形成A)", "底仓损"))

# 进入A:
for a_label, a_ok, a_note in A:
    if not a_ok:
        # A失败 -> 终止, B不建
        leaves.append((f"原始✓ → {a_label}", f"[终止] {a_note}"))
    else:
        # A成功 -> 进入B
        for b_label, b_ok, b_note in B:
            leaves.append((f"原始✓ → {a_label} → {b_label}",
                           f"A:{a_note} || B:{b_note}"))

print(f"\n总叶子(可能性) = {len(leaves)} 种\n")
for i, (path, note) in enumerate(leaves, 1):
    print(f"[叶{i:2d}] {path}")
    print(f"        └ {note}")

# 计数验证
print("\n" + "="*74)
print("数量结构验证")
print("="*74)
a_fail = sum(1 for o in A if not o[1])   # A失败终止叶
a_ok   = sum(1 for o in A if o[1])       # A成功
b_total= len(B)                          # A成功后B的所有结局
print(f"  叶 = 1(原始直接失败) + {a_fail}(A失败终止) + {a_ok}(A成功)×{b_total}(B结局)")
print(f"      = 1 + {a_fail} + {a_ok*b_total} = {1 + a_fail + a_ok*b_total}")
print(f"\n  注: '逐级回落'(全局掉头依次碰 2nd→1st→Original stop) 体现在")
print(f"      B成功后再整体回落的子情形, 属于'B保本出/到TP后回撤'的延伸,")
print(f"      建模时作为 B 区域结束后的'移动止损链'单独处理 (见下).")
