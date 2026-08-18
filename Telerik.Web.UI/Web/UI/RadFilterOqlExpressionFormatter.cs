using System;
using System.Collections;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02001874 RID: 6260
	public class RadFilterOqlExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2CD RID: 62157 RVA: 0x00374C56 File Offset: 0x00372E56
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			return string.Format("{0}", fieldName);
		}

		// Token: 0x0600F2CE RID: 62158 RVA: 0x00374C64 File Offset: 0x00372E64
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			ArrayList arrayList = new ArrayList();
			forType = RadFilterTypeHelper.GetNonNullableType(forType);
			for (int i = 0; i < values.Count; i++)
			{
				if (forType == typeof(TimeSpan))
				{
					DateTime value = new DateTime(((TimeSpan)values[i]).Ticks);
					arrayList.Add(this.FormatDateTime(value));
				}
				else if (forType == typeof(DateTime))
				{
					arrayList.Add(this.FormatDateTime((DateTime)values[i]));
				}
				else if (forType == typeof(string))
				{
					arrayList.Add(string.Format("\"{0}\"", values[i]));
				}
				else
				{
					arrayList.Add(string.Format("{0}", values[i]));
				}
			}
			return arrayList;
		}

		// Token: 0x0600F2CF RID: 62159 RVA: 0x00374D45 File Offset: 0x00372F45
		public string FormatDateTime(DateTime value)
		{
			return string.Format("timestamp '{0}'", value.ToString("yyyy-MM-dd H:mm:ss", DateTimeFormatInfo.InvariantInfo));
		}
	}
}
