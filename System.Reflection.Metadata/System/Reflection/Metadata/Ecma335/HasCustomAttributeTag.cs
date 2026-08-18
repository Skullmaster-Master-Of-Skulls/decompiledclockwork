using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C8 RID: 200
	internal static class HasCustomAttributeTag
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x00016520 File Offset: 0x00014720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint hasCustomAttribute)
		{
			uint num = HasCustomAttributeTag.TagToTokenTypeArray[(int)(hasCustomAttribute & 31U)];
			uint num2 = hasCustomAttribute >> 5;
			if (num == 4294967295U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00016554 File Offset: 0x00014754
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
				case 24U:
				case 25U:
					break;
				case 26U:
					return rowId << 5 | 12U;
				case 27U:
					return rowId << 5 | 13U;
				default:
					switch (num)
					{
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
					}
					break;
				}
				break;
			}
			return 0U;
		}

		// Token: 0x0400057B RID: 1403
		internal const int NumberOfBits = 5;

		// Token: 0x0400057C RID: 1404
		internal const int LargeRowSize = 2048;

		// Token: 0x0400057D RID: 1405
		internal const uint MethodDef = 0U;

		// Token: 0x0400057E RID: 1406
		internal const uint Field = 1U;

		// Token: 0x0400057F RID: 1407
		internal const uint TypeRef = 2U;

		// Token: 0x04000580 RID: 1408
		internal const uint TypeDef = 3U;

		// Token: 0x04000581 RID: 1409
		internal const uint Param = 4U;

		// Token: 0x04000582 RID: 1410
		internal const uint InterfaceImpl = 5U;

		// Token: 0x04000583 RID: 1411
		internal const uint MemberRef = 6U;

		// Token: 0x04000584 RID: 1412
		internal const uint Module = 7U;

		// Token: 0x04000585 RID: 1413
		internal const uint DeclSecurity = 8U;

		// Token: 0x04000586 RID: 1414
		internal const uint Property = 9U;

		// Token: 0x04000587 RID: 1415
		internal const uint Event = 10U;

		// Token: 0x04000588 RID: 1416
		internal const uint StandAloneSig = 11U;

		// Token: 0x04000589 RID: 1417
		internal const uint ModuleRef = 12U;

		// Token: 0x0400058A RID: 1418
		internal const uint TypeSpec = 13U;

		// Token: 0x0400058B RID: 1419
		internal const uint Assembly = 14U;

		// Token: 0x0400058C RID: 1420
		internal const uint AssemblyRef = 15U;

		// Token: 0x0400058D RID: 1421
		internal const uint File = 16U;

		// Token: 0x0400058E RID: 1422
		internal const uint ExportedType = 17U;

		// Token: 0x0400058F RID: 1423
		internal const uint ManifestResource = 18U;

		// Token: 0x04000590 RID: 1424
		internal const uint GenericParam = 19U;

		// Token: 0x04000591 RID: 1425
		internal const uint GenericParamConstraint = 20U;

		// Token: 0x04000592 RID: 1426
		internal const uint MethodSpec = 21U;

		// Token: 0x04000593 RID: 1427
		internal const uint TagMask = 31U;

		// Token: 0x04000594 RID: 1428
		internal const uint InvalidTokenType = 4294967295U;

		// Token: 0x04000595 RID: 1429
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
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04000596 RID: 1430
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint;
	}
}
