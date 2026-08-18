using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000074 RID: 116
	internal static class TokenTypeIds
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00007A6B File Offset: 0x00005C6B
		internal static bool IsEntityOrUserStringToken(uint vToken)
		{
			return (vToken & 2130706432U) <= 1879048192U;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00007A7E File Offset: 0x00005C7E
		internal static bool IsEntityToken(uint vToken)
		{
			return (vToken & 2130706432U) < 1879048192U;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00007A8E File Offset: 0x00005C8E
		internal static bool IsValidRowId(uint rowId)
		{
			return (rowId & 4278190080U) == 0U;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007A9A File Offset: 0x00005C9A
		internal static bool IsValidRowId(int rowId)
		{
			return ((long)rowId & (long)((ulong)-16777216)) == 0L;
		}

		// Token: 0x04000438 RID: 1080
		internal const uint Module = 0U;

		// Token: 0x04000439 RID: 1081
		internal const uint TypeRef = 16777216U;

		// Token: 0x0400043A RID: 1082
		internal const uint TypeDef = 33554432U;

		// Token: 0x0400043B RID: 1083
		internal const uint FieldDef = 67108864U;

		// Token: 0x0400043C RID: 1084
		internal const uint MethodDef = 100663296U;

		// Token: 0x0400043D RID: 1085
		internal const uint ParamDef = 134217728U;

		// Token: 0x0400043E RID: 1086
		internal const uint InterfaceImpl = 150994944U;

		// Token: 0x0400043F RID: 1087
		internal const uint MemberRef = 167772160U;

		// Token: 0x04000440 RID: 1088
		internal const uint Constant = 184549376U;

		// Token: 0x04000441 RID: 1089
		internal const uint CustomAttribute = 201326592U;

		// Token: 0x04000442 RID: 1090
		internal const uint DeclSecurity = 234881024U;

		// Token: 0x04000443 RID: 1091
		internal const uint Signature = 285212672U;

		// Token: 0x04000444 RID: 1092
		internal const uint EventMap = 301989888U;

		// Token: 0x04000445 RID: 1093
		internal const uint Event = 335544320U;

		// Token: 0x04000446 RID: 1094
		internal const uint PropertyMap = 352321536U;

		// Token: 0x04000447 RID: 1095
		internal const uint Property = 385875968U;

		// Token: 0x04000448 RID: 1096
		internal const uint MethodSemantics = 402653184U;

		// Token: 0x04000449 RID: 1097
		internal const uint MethodImpl = 419430400U;

		// Token: 0x0400044A RID: 1098
		internal const uint ModuleRef = 436207616U;

		// Token: 0x0400044B RID: 1099
		internal const uint TypeSpec = 452984832U;

		// Token: 0x0400044C RID: 1100
		internal const uint Assembly = 536870912U;

		// Token: 0x0400044D RID: 1101
		internal const uint AssemblyRef = 587202560U;

		// Token: 0x0400044E RID: 1102
		internal const uint File = 637534208U;

		// Token: 0x0400044F RID: 1103
		internal const uint ExportedType = 654311424U;

		// Token: 0x04000450 RID: 1104
		internal const uint ManifestResource = 671088640U;

		// Token: 0x04000451 RID: 1105
		internal const uint NestedClass = 687865856U;

		// Token: 0x04000452 RID: 1106
		internal const uint GenericParam = 704643072U;

		// Token: 0x04000453 RID: 1107
		internal const uint MethodSpec = 721420288U;

		// Token: 0x04000454 RID: 1108
		internal const uint GenericParamConstraint = 738197504U;

		// Token: 0x04000455 RID: 1109
		internal const uint Document = 805306368U;

		// Token: 0x04000456 RID: 1110
		internal const uint MethodDebugInformation = 822083584U;

		// Token: 0x04000457 RID: 1111
		internal const uint LocalScope = 838860800U;

		// Token: 0x04000458 RID: 1112
		internal const uint LocalVariable = 855638016U;

		// Token: 0x04000459 RID: 1113
		internal const uint LocalConstant = 872415232U;

		// Token: 0x0400045A RID: 1114
		internal const uint ImportScope = 889192448U;

		// Token: 0x0400045B RID: 1115
		internal const uint AsyncMethod = 905969664U;

		// Token: 0x0400045C RID: 1116
		internal const uint CustomDebugInformation = 922746880U;

		// Token: 0x0400045D RID: 1117
		internal const uint UserString = 1879048192U;

		// Token: 0x0400045E RID: 1118
		internal const int RowIdBitCount = 24;

		// Token: 0x0400045F RID: 1119
		internal const uint RIDMask = 16777215U;

		// Token: 0x04000460 RID: 1120
		internal const uint TypeMask = 2130706432U;

		// Token: 0x04000461 RID: 1121
		internal const uint VirtualBit = 2147483648U;
	}
}
