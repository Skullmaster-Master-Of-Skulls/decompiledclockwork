using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000119 RID: 281
	internal static class HasCustomDebugInformationTag
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0001B938 File Offset: 0x00019B38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint taggedReference)
		{
			uint num = HasCustomDebugInformationTag.TagToTokenTypeArray[(int)(taggedReference & 31U)];
			uint num2 = taggedReference >> 5;
			if (num == 4294967295U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001B96C File Offset: 0x00019B6C
		internal static uint ConvertToTag(EntityHandle handle)
		{
			uint type = handle.Type;
			uint rowId = (uint)handle.RowId;
			uint num = type >> 24;
			switch (num)
			{
			case 0U:
				return rowId << 5 | 7U;
			case 1U:
				return rowId << 5 | 2U;
			case 2U:
				return rowId << 5 | 3U;
			case 3U:
			case 5U:
			case 7U:
			case 11U:
			case 12U:
			case 13U:
			case 15U:
			case 16U:
			case 18U:
			case 19U:
				break;
			case 4U:
				return rowId << 5 | 1U;
			case 6U:
				return rowId << 5 | 0U;
			case 8U:
				return rowId << 5 | 4U;
			case 9U:
				return rowId << 5 | 5U;
			case 10U:
				return rowId << 5 | 6U;
			case 14U:
				return rowId << 5 | 8U;
			case 17U:
				return rowId << 5 | 11U;
			case 20U:
				return rowId << 5 | 10U;
			default:
				switch (num)
				{
				case 23U:
					return rowId << 5 | 9U;
				case 26U:
					return rowId << 5 | 12U;
				case 27U:
					return rowId << 5 | 13U;
				case 32U:
					return rowId << 5 | 14U;
				case 35U:
					return rowId << 5 | 15U;
				case 38U:
					return rowId << 5 | 16U;
				case 39U:
					return rowId << 5 | 17U;
				case 40U:
					return rowId << 5 | 18U;
				case 42U:
					return rowId << 5 | 19U;
				case 43U:
					return rowId << 5 | 21U;
				case 44U:
					return rowId << 5 | 20U;
				case 48U:
					return rowId << 5 | 22U;
				case 50U:
					return rowId << 5 | 23U;
				case 51U:
					return rowId << 5 | 24U;
				case 52U:
					return rowId << 5 | 25U;
				case 53U:
					return rowId << 5 | 26U;
				}
				break;
			}
			return 0U;
		}

		// Token: 0x0400082F RID: 2095
		internal const int NumberOfBits = 5;

		// Token: 0x04000830 RID: 2096
		internal const int LargeRowSize = 2048;

		// Token: 0x04000831 RID: 2097
		internal const uint MethodDef = 0U;

		// Token: 0x04000832 RID: 2098
		internal const uint Field = 1U;

		// Token: 0x04000833 RID: 2099
		internal const uint TypeRef = 2U;

		// Token: 0x04000834 RID: 2100
		internal const uint TypeDef = 3U;

		// Token: 0x04000835 RID: 2101
		internal const uint Param = 4U;

		// Token: 0x04000836 RID: 2102
		internal const uint InterfaceImpl = 5U;

		// Token: 0x04000837 RID: 2103
		internal const uint MemberRef = 6U;

		// Token: 0x04000838 RID: 2104
		internal const uint Module = 7U;

		// Token: 0x04000839 RID: 2105
		internal const uint DeclSecurity = 8U;

		// Token: 0x0400083A RID: 2106
		internal const uint Property = 9U;

		// Token: 0x0400083B RID: 2107
		internal const uint Event = 10U;

		// Token: 0x0400083C RID: 2108
		internal const uint StandAloneSig = 11U;

		// Token: 0x0400083D RID: 2109
		internal const uint ModuleRef = 12U;

		// Token: 0x0400083E RID: 2110
		internal const uint TypeSpec = 13U;

		// Token: 0x0400083F RID: 2111
		internal const uint Assembly = 14U;

		// Token: 0x04000840 RID: 2112
		internal const uint AssemblyRef = 15U;

		// Token: 0x04000841 RID: 2113
		internal const uint File = 16U;

		// Token: 0x04000842 RID: 2114
		internal const uint ExportedType = 17U;

		// Token: 0x04000843 RID: 2115
		internal const uint ManifestResource = 18U;

		// Token: 0x04000844 RID: 2116
		internal const uint GenericParam = 19U;

		// Token: 0x04000845 RID: 2117
		internal const uint GenericParamConstraint = 20U;

		// Token: 0x04000846 RID: 2118
		internal const uint MethodSpec = 21U;

		// Token: 0x04000847 RID: 2119
		internal const uint Document = 22U;

		// Token: 0x04000848 RID: 2120
		internal const uint LocalScope = 23U;

		// Token: 0x04000849 RID: 2121
		internal const uint LocalVariable = 24U;

		// Token: 0x0400084A RID: 2122
		internal const uint LocalConstant = 25U;

		// Token: 0x0400084B RID: 2123
		internal const uint Import = 26U;

		// Token: 0x0400084C RID: 2124
		internal const uint TagMask = 31U;

		// Token: 0x0400084D RID: 2125
		internal const uint InvalidTokenType = 4294967295U;

		// Token: 0x0400084E RID: 2126
		internal static uint[] TagToTokenTypeArray = new uint[]
		{
			100663296U,
			67108864U,
			16777216U,
			33554432U,
			134217728U,
			150994944U,
			167772160U,
			0U,
			234881024U,
			385875968U,
			335544320U,
			285212672U,
			436207616U,
			452984832U,
			536870912U,
			587202560U,
			637534208U,
			654311424U,
			671088640U,
			704643072U,
			738197504U,
			721420288U,
			805306368U,
			838860800U,
			855638016U,
			872415232U,
			889192448U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x0400084F RID: 2127
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint | TableMask.Document | TableMask.LocalScope | TableMask.LocalVariable | TableMask.LocalConstant | TableMask.ImportScope;
	}
}
