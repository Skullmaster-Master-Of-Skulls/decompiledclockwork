using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A31 RID: 6705
	internal class MonthViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010464 RID: 66660 RVA: 0x003A3054 File Offset: 0x003A1254
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			MonthViewSettings monthViewSettings = obj as MonthViewSettings;
			if (monthViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only MonthViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", monthViewSettings.HeaderDateFormat, "MMM, yyyy");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayHeaderDateFormat", monthViewSettings.DayHeaderDateFormat, "dd");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "firstDayHeaderDateFormat", monthViewSettings.FirstDayHeaderDateFormat, "d MMM");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", monthViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", monthViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "visibleAppointmentsPerDay", monthViewSettings.VisibleAppointmentsPerDay, 2);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", monthViewSettings.ReadOnly, false);
			return dictionary;
		}

		// Token: 0x17004EDE RID: 20190
		// (get) Token: 0x06010465 RID: 66661 RVA: 0x003A312C File Offset: 0x003A132C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MonthViewSettings)
				};
			}
		}
	}
}
