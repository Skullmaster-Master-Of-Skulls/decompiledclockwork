using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001877 RID: 6263
	public class RadFilterGridCalculatedColumnExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2D7 RID: 62167 RVA: 0x00374EA0 File Offset: 0x003730A0
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			dataType = RadFilterTypeHelper.GetNonNullableType(dataType);
			if (dataType == typeof(string))
			{
				string arg = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("{0}.ToString(){1}", fieldName, arg);
			}
			if (dataType == typeof(char))
			{
				string arg2 = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("Char(iif(it.{0}==null,'''',it.{0})).ToString(){1}", fieldName, arg2);
			}
			return string.Format("({1}?({0}))", fieldName, dataType.Name);
		}

		// Token: 0x0600F2D8 RID: 62168 RVA: 0x00374F24 File Offset: 0x00373124
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			return RadFilterDLinqExpressionFormatter.PrepareFieldValueFormat(values, forType, isCaseSensitive);
		}
	}
}
