using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001875 RID: 6261
	public class RadFilterLinqRowExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2D1 RID: 62161 RVA: 0x00374D6C File Offset: 0x00372F6C
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			dataType = RadFilterTypeHelper.GetNonNullableType(dataType);
			if (dataType == typeof(string) || dataType == typeof(char))
			{
				string arg = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("it[\"{0}\"].ToString(){1}", fieldName, arg);
			}
			if (dataType == typeof(Guid))
			{
				string format = "Guid(iif(it[\"{0}\"]==Convert.DBNull,null,it[\"{0}\"])).ToString()";
				return string.Format(format, fieldName, dataType.Name);
			}
			string format2 = "Convert.To{1}(iif(it[\"{0}\"]==Convert.DBNull,null,it[\"{0}\"]))";
			return string.Format(format2, fieldName, dataType.Name);
		}

		// Token: 0x0600F2D2 RID: 62162 RVA: 0x00374DFB File Offset: 0x00372FFB
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			return RadFilterDLinqExpressionFormatter.PrepareFieldValueFormat(values, forType, isCaseSensitive);
		}
	}
}
