using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001873 RID: 6259
	public class RadFilterEntitySqlExpressionFormatter : IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2CA RID: 62154 RVA: 0x00374B23 File Offset: 0x00372D23
		public string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive)
		{
			return string.Format("it.{0}", fieldName);
		}

		// Token: 0x0600F2CB RID: 62155 RVA: 0x00374B30 File Offset: 0x00372D30
		public ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive)
		{
			ArrayList arrayList = new ArrayList();
			forType = RadFilterTypeHelper.GetNonNullableType(forType);
			for (int i = 0; i < values.Count; i++)
			{
				string format;
				if (forType == typeof(TimeSpan))
				{
					format = "TIME'{0}')";
				}
				else if (forType == typeof(DateTime))
				{
					format = "DATETIME'{0}'";
				}
				else if (forType == typeof(Guid))
				{
					format = "GUID('{0}')";
				}
				else if (forType == typeof(string) || forType == typeof(char))
				{
					format = "\"{0}\"";
				}
				else if (forType == typeof(decimal))
				{
					format = "{0}m";
				}
				else
				{
					format = "{0}";
				}
				if (forType == typeof(DateTime))
				{
					arrayList.Add(string.Format(format, ((DateTime)values[i]).ToString("yyyy-MM-dd HH:mm")));
				}
				else
				{
					arrayList.Add(string.Format(format, values[i]));
				}
			}
			return arrayList;
		}
	}
}
