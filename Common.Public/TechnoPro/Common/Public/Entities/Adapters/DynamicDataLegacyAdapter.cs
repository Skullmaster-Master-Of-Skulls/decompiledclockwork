using System;
using System.Data;
using System.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005BF RID: 1471
	public static class DynamicDataLegacyAdapter
	{
		// Token: 0x06002F73 RID: 12147 RVA: 0x00035DBC File Offset: 0x00033FBC
		public static LegacyDynamicDataRowDatas ConvertDataTableToLegacyDynamicDataRowDatas(this DataTable t)
		{
			bool flag = t == null;
			LegacyDynamicDataRowDatas result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Type left = t.Columns.Contains("controlvalue") ? t.Columns["controlvalue"].DataType : typeof(string);
				bool flag2 = left == typeof(int) || left == typeof(long);
				eLegacyDynamicDataType controlValueType;
				if (flag2)
				{
					controlValueType = eLegacyDynamicDataType.Int;
				}
				else
				{
					bool flag3 = left == typeof(DateTime);
					if (flag3)
					{
						controlValueType = eLegacyDynamicDataType.DateTime;
					}
					else
					{
						bool flag4 = left == typeof(byte[]);
						if (flag4)
						{
							controlValueType = eLegacyDynamicDataType.Binary;
						}
						else
						{
							controlValueType = eLegacyDynamicDataType.Unknown;
						}
					}
				}
				result = new LegacyDynamicDataRowDatas
				{
					ControlValueType = controlValueType,
					RowDatas = (from DataRow dr in t.Rows
					select dr.ConvertDataRowToLegacyDynamicDataRowData(controlValueType)).ToList<LegacyDynamicDataRowData>()
				};
			}
			return result;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00035ECC File Offset: 0x000340CC
		public static LegacyDynamicDataRowData ConvertDataRowToLegacyDynamicDataRowData(this DataRow dr, eLegacyDynamicDataType controlValueType)
		{
			eLegacyDynamicDataRowState rowState = dr.RowState.GetRowState();
			bool flag = rowState == eLegacyDynamicDataRowState.Deleted;
			if (flag)
			{
				dr.RejectChanges();
			}
			LegacyDynamicDataRowData legacyDynamicDataRowData = new LegacyDynamicDataRowData
			{
				RowState = rowState,
				ControlId = ((dr["controlid"] is DBNull) ? 0 : ((int)dr["controlid"]))
			};
			switch (controlValueType)
			{
			case eLegacyDynamicDataType.Int:
				legacyDynamicDataRowData.ControlValueInt = ((dr["controlvalue"] is DBNull) ? null : new int?((int)dr["controlvalue"]));
				break;
			case eLegacyDynamicDataType.Binary:
				legacyDynamicDataRowData.ControlValueBytes = ((dr["controlvalue"] is DBNull) ? null : ((byte[])dr["controlvalue"]));
				break;
			case eLegacyDynamicDataType.DateTime:
				legacyDynamicDataRowData.ControlValueDateTime = ((dr["controlvalue"] is DBNull) ? null : new DateTime?((DateTime)dr["controlvalue"]));
				break;
			}
			bool flag2 = rowState == eLegacyDynamicDataRowState.Deleted;
			if (flag2)
			{
				dr.Delete();
			}
			return legacyDynamicDataRowData;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x00036010 File Offset: 0x00034210
		public static eLegacyDynamicDataRowState GetRowState(this DataRowState drState)
		{
			eLegacyDynamicDataRowState[] source = (eLegacyDynamicDataRowState[])Enum.GetValues(typeof(eLegacyDynamicDataRowState));
			return source.FirstOrDefault(delegate(eLegacyDynamicDataRowState g)
			{
				LegacyDynamicDataRowStateAttribute attribute = g.GetAttribute<LegacyDynamicDataRowStateAttribute>();
				return attribute != null && attribute.DataRowState == drState;
			});
		}
	}
}
