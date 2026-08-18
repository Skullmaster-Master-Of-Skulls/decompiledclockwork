using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001872 RID: 6258
	public class RadFilterDLinqExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2C6 RID: 62150 RVA: 0x003749B0 File Offset: 0x00372BB0
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			dataType = RadFilterTypeHelper.GetNonNullableType(dataType);
			if (dataType == typeof(string))
			{
				string arg = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("iif(it.{0}==null,\"\",it.{0}).ToString(){1}", fieldName, arg);
			}
			if (dataType == typeof(char))
			{
				string arg2 = isCaseSensitive ? "" : ".ToUpper()";
				return string.Format("Char(iif(it.{0}==null,'''',it.{0})).ToString(){1}", fieldName, arg2);
			}
			return string.Format("it.{0}", fieldName);
		}

		// Token: 0x0600F2C7 RID: 62151 RVA: 0x00374A30 File Offset: 0x00372C30
		internal static ArrayList PrepareFieldValueFormat(ArrayList values, Type forType, bool isCaseSensitive)
		{
			ArrayList arrayList = new ArrayList();
			forType = RadFilterTypeHelper.GetNonNullableType(forType);
			for (int i = 0; i < values.Count; i++)
			{
				string format;
				if (forType == typeof(TimeSpan))
				{
					format = "TimeSpan.Parse(\"{0}\")";
				}
				else if (forType == typeof(DateTime))
				{
					format = "DateTime.Parse(\"{0}\")";
				}
				else if (forType == typeof(Guid))
				{
					format = "Guid(\"{0}\").ToString()";
				}
				else if (forType == typeof(string) || forType == typeof(char))
				{
					format = (isCaseSensitive ? "\"{0}\"" : "\"{0}\".ToUpper()");
				}
				else
				{
					format = "{0}";
				}
				arrayList.Add(string.Format(format, (values[i] == null) ? "null" : values[i]));
			}
			return arrayList;
		}

		// Token: 0x0600F2C8 RID: 62152 RVA: 0x00374B11 File Offset: 0x00372D11
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			return RadFilterDLinqExpressionFormatter.PrepareFieldValueFormat(values, forType, isCaseSensitive);
		}
	}
}
