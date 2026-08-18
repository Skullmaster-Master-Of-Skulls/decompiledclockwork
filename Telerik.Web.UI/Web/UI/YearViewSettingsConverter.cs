using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200081D RID: 2077
	internal class YearViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06004CB5 RID: 19637 RVA: 0x000F116C File Offset: 0x000EF36C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			YearViewSettings yearViewSettings = obj as YearViewSettings;
			if (yearViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only YearViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", yearViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", yearViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", yearViewSettings.ReadOnly, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", yearViewSettings.HeaderDateFormat, "yyyy");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayHeaderDateFormat", yearViewSettings.DayHeaderDateFormat, "dd");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "monthHeaderDateFormat", yearViewSettings.MonthHeaderDateFormat, "MMMM");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showMonthHeaders", yearViewSettings.ShowMonthHeaders, true);
			return dictionary;
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x000F1244 File Offset: 0x000EF444
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(YearViewSettings)
				};
			}
		}
	}
}
