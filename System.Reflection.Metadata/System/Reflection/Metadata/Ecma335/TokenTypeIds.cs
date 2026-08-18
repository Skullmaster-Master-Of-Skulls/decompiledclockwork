using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000DA RID: 218
	internal static class TokenTypeIds
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00017333 File Offset: 0x00015533
		internal static bool IsEntityOrUserStringToken(uint vToken)
		{
			return (vToken & 2130706432U) <= 1879048192U;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00017346 File Offset: 0x00015546
		internal static bool IsEntityToken(uint vToken)
		{
			return (vToken & 2130706432U) < 1879048192U;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00017356 File Offset: 0x00015556
		internal static bool IsValidRowId(uint rowId)
		{
			return (rowId & 4278190080U) == 0U;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00017362 File Offset: 0x00015562
		internal static bool IsValidRowId(int rowId)
		{
			return ((long)rowId & (long)((ulong)-16777216)) == 0L;
		}

		// Token: 0x04000650 RID: 1616
		internal const uint Module = 0U;

		// Token: 0x04000651 RID: 1617
		internal const uint TypeRef = 16777216U;

		// Token: 0x04000652 RID: 1618
		internal const uint TypeDef = 33554432U;

		// Token: 0x04000653 RID: 1619
		internal const uint FieldDef = 67108864U;

		// Token: 0x04000654 RID: 1620
		internal const uint MethodDef = 100663296U;

		// Token: 0x04000655 RID: 1621
		internal const uint ParamDef = 134217728U;

		// Token: 0x04000656 RID: 1622
		internal const uint InterfaceImpl = 150994944U;

		// Token: 0x04000657 RID: 1623
		internal const uint MemberRef = 167772160U;

		// Token: 0x04000658 RID: 1624
		internal const uint Constant = 184549376U;

		// Token: 0x04000659 RID: 1625
		internal const uint CustomAttribute = 201326592U;

		// Token: 0x0400065A RID: 1626
		internal const uint DeclSecurity = 234881024U;

		// Token: 0x0400065B RID: 1627
		internal const uint Signature = 285212672U;

		// Token: 0x0400065C RID: 1628
		internal const uint EventMap = 301989888U;

		// Token: 0x0400065D RID: 1629
		internal const uint Event = 335544320U;

		// Token: 0x0400065E RID: 1630
		internal const uint PropertyMap = 352321536U;

		// Token: 0x0400065F RID: 1631
		internal const uint Property = 385875968U;

		// Token: 0x04000660 RID: 1632
		internal const uint MethodSemantics = 402653184U;

		// Token: 0x04000661 RID: 1633
		internal const uint MethodImpl = 419430400U;

		// Token: 0x04000662 RID: 1634
		internal const uint ModuleRef = 436207616U;

		// Token: 0x04000663 RID: 1635
		internal const uint TypeSpec = 452984832U;

		// Token: 0x04000664 RID: 1636
		internal const uint Assembly = 536870912U;

		// Token: 0x04000665 RID: 1637
		internal const uint AssemblyRef = 587202560U;

		// Token: 0x04000666 RID: 1638
		internal const uint File = 637534208U;

		// Token: 0x04000667 RID: 1639
		internal const uint ExportedType = 654311424U;

		// Token: 0x04000668 RID: 1640
		internal const uint ManifestResource = 671088640U;

		// Token: 0x04000669 RID: 1641
		internal const uint NestedClass = 687865856U;

		// Token: 0x0400066A RID: 1642
		internal const uint GenericParam = 704643072U;

		// Token: 0x0400066B RID: 1643
		internal const uint MethodSpec = 721420288U;

		// Token: 0x0400066C RID: 1644
		internal const uint GenericParamConstraint = 738197504U;

		// Token: 0x0400066D RID: 1645
		internal const uint Document = 805306368U;

		// Token: 0x0400066E RID: 1646
		internal const uint MethodDebugInformation = 822083584U;

		// Token: 0x0400066F RID: 1647
		internal const uint LocalScope = 838860800U;

		// Token: 0x04000670 RID: 1648
		internal const uint LocalVariable = 855638016U;

		// Token: 0x04000671 RID: 1649
		internal const uint LocalConstant = 872415232U;

		// Token: 0x04000672 RID: 1650
		internal const uint ImportScope = 889192448U;

		// Token: 0x04000673 RID: 1651
		internal const uint AsyncMethod = 905969664U;

		// Token: 0x04000674 RID: 1652
		internal const uint CustomDebugInformation = 922746880U;

		// Token: 0x04000675 RID: 1653
		internal const uint UserString = 1879048192U;

		// Token: 0x04000676 RID: 1654
		internal const int RowIdBitCount = 24;

		// Token: 0x04000677 RID: 1655
		internal const uint RIDMask = 16777215U;

		// Token: 0x04000678 RID: 1656
		internal const uint TypeMask = 2130706432U;

		// Token: 0x04000679 RID: 1657
		internal const uint VirtualBit = 2147483648U;
	}
}
