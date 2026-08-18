using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001876 RID: 6262
	public class RadFilterGridBindableTypeExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2D4 RID: 62164 RVA: 0x00374E10 File Offset: 0x00373010
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			dataType = RadFilterTypeHelper.GetNonNullableType(dataType);
			if (dataType == typeof(string))
			{
				string arg = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("iif(it==null,\"\",it).ToString(){1}", fieldName, arg);
			}
			if (dataType == typeof(char))
			{
				string arg2 = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("Char(iif(it==null,'''',it)).ToString(){1}", fieldName, arg2);
			}
			return string.Format("it", fieldName);
		}

		// Token: 0x0600F2D5 RID: 62165 RVA: 0x00374E8E File Offset: 0x0037308E
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			return RadFilterDLinqExpressionFormatter.PrepareFieldValueFormat(values, forType, isCaseSensitive);
		}
	}
}
