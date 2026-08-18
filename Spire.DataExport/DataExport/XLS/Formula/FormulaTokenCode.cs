using System;

namespace Spire.DataExport.XLS.Formula
{
	// Token: 0x0200015A RID: 346
	internal enum FormulaTokenCode : byte
	{
		// Token: 0x04000654 RID: 1620
		Empty,
		// Token: 0x04000655 RID: 1621
		Uplus = 18,
		// Token: 0x04000656 RID: 1622
		Uminus,
		// Token: 0x04000657 RID: 1623
		Percent,
		// Token: 0x04000658 RID: 1624
		Parentheses,
		// Token: 0x04000659 RID: 1625
		Add = 3,
		// Token: 0x0400065A RID: 1626
		Sub,
		// Token: 0x0400065B RID: 1627
		Mul,
		// Token: 0x0400065C RID: 1628
		Div,
		// Token: 0x0400065D RID: 1629
		Power,
		// Token: 0x0400065E RID: 1630
		Concat,
		// Token: 0x0400065F RID: 1631
		Lt,
		// Token: 0x04000660 RID: 1632
		Le,
		// Token: 0x04000661 RID: 1633
		Eq,
		// Token: 0x04000662 RID: 1634
		Ge,
		// Token: 0x04000663 RID: 1635
		Gt,
		// Token: 0x04000664 RID: 1636
		Ne,
		// Token: 0x04000665 RID: 1637
		Isect,
		// Token: 0x04000666 RID: 1638
		List,
		// Token: 0x04000667 RID: 1639
		Range,
		// Token: 0x04000668 RID: 1640
		Func1 = 33,
		// Token: 0x04000669 RID: 1641
		Func2 = 65,
		// Token: 0x0400066A RID: 1642
		Func3 = 97,
		// Token: 0x0400066B RID: 1643
		FuncVar1 = 34,
		// Token: 0x0400066C RID: 1644
		FuncVar2 = 66,
		// Token: 0x0400066D RID: 1645
		FuncVar3 = 98,
		// Token: 0x0400066E RID: 1646
		MissArg = 22,
		// Token: 0x0400066F RID: 1647
		Str,
		// Token: 0x04000670 RID: 1648
		Err = 28,
		// Token: 0x04000671 RID: 1649
		Bool,
		// Token: 0x04000672 RID: 1650
		Int,
		// Token: 0x04000673 RID: 1651
		Num,
		// Token: 0x04000674 RID: 1652
		Array1,
		// Token: 0x04000675 RID: 1653
		Array2 = 64,
		// Token: 0x04000676 RID: 1654
		Array3 = 96,
		// Token: 0x04000677 RID: 1655
		Ref1 = 36,
		// Token: 0x04000678 RID: 1656
		Ref2 = 68,
		// Token: 0x04000679 RID: 1657
		Ref3 = 100,
		// Token: 0x0400067A RID: 1658
		RefErr1 = 42,
		// Token: 0x0400067B RID: 1659
		RefErr2 = 74,
		// Token: 0x0400067C RID: 1660
		RefErr3 = 106,
		// Token: 0x0400067D RID: 1661
		Area1 = 37,
		// Token: 0x0400067E RID: 1662
		Area2 = 69,
		// Token: 0x0400067F RID: 1663
		Area3 = 101,
		// Token: 0x04000680 RID: 1664
		Name1 = 35,
		// Token: 0x04000681 RID: 1665
		Name2 = 67,
		// Token: 0x04000682 RID: 1666
		Name3 = 99,
		// Token: 0x04000683 RID: 1667
		NameX1 = 57,
		// Token: 0x04000684 RID: 1668
		NameX2 = 89,
		// Token: 0x04000685 RID: 1669
		NameX3 = 121,
		// Token: 0x04000686 RID: 1670
		Ref3d1 = 58,
		// Token: 0x04000687 RID: 1671
		Ref3d2 = 90,
		// Token: 0x04000688 RID: 1672
		Ref3d3 = 122,
		// Token: 0x04000689 RID: 1673
		Area3d1 = 59,
		// Token: 0x0400068A RID: 1674
		Area3d2 = 91,
		// Token: 0x0400068B RID: 1675
		Area3d3 = 123,
		// Token: 0x0400068C RID: 1676
		RefErr3d1 = 60,
		// Token: 0x0400068D RID: 1677
		RefErr3d2 = 92,
		// Token: 0x0400068E RID: 1678
		RefErr3d3 = 124,
		// Token: 0x0400068F RID: 1679
		AreaErr3d1 = 61,
		// Token: 0x04000690 RID: 1680
		AreaErr3d2 = 93,
		// Token: 0x04000691 RID: 1681
		AreaErr3d3 = 125,
		// Token: 0x04000692 RID: 1682
		Exp = 1,
		// Token: 0x04000693 RID: 1683
		Tbl,
		// Token: 0x04000694 RID: 1684
		Extended = 24,
		// Token: 0x04000695 RID: 1685
		Attr,
		// Token: 0x04000696 RID: 1686
		Sheet,
		// Token: 0x04000697 RID: 1687
		EndSheet
	}
}
