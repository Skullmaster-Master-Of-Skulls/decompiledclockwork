using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A30 RID: 6704
	internal class MultiDayViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010461 RID: 66657 RVA: 0x003A2E04 File Offset: 0x003A1004
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			MultiDayViewSettings multiDayViewSettings = obj as MultiDayViewSettings;
			if (multiDayViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WeekViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayStartTime", (int)multiDayViewSettings.DayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayEndTime", (int)multiDayViewSettings.DayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(18.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayStartTime", (int)multiDayViewSettings.WorkDayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayEndTime", (int)multiDayViewSettings.WorkDayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(17.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", multiDayViewSettings.HeaderDateFormat, "d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "columnHeaderDateFormat", multiDayViewSettings.ColumnHeaderDateFormat, "ddd, d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", multiDayViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", multiDayViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "numberOfDays", multiDayViewSettings.NumberOfDays, 5);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hiddenAptsIndicator", multiDayViewSettings.ShowHiddenAppointmentsIndicator, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enableExactTimeRendering", multiDayViewSettings.EnableExactTimeRenderingResolved, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showAllDayInsertArea", multiDayViewSettings.ShowAllDayInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showInsertArea", multiDayViewSettings.ShowInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", multiDayViewSettings.ReadOnly, false);
			return dictionary;
		}

		// Token: 0x17004EDD RID: 20189
		// (get) Token: 0x06010462 RID: 66658 RVA: 0x003A3028 File Offset: 0x003A1228
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MultiDayViewSettings)
				};
			}
		}
	}
}
