using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A34 RID: 6708
	internal class WeekViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601046D RID: 66669 RVA: 0x003A351C File Offset: 0x003A171C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WeekViewSettings weekViewSettings = obj as WeekViewSettings;
			if (weekViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WeekViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayStartTime", (int)weekViewSettings.DayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayEndTime", (int)weekViewSettings.DayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(18.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayStartTime", (int)weekViewSettings.WorkDayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayEndTime", (int)weekViewSettings.WorkDayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(17.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", weekViewSettings.HeaderDateFormat, "d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "columnHeaderDateFormat", weekViewSettings.ColumnHeaderDateFormat, "ddd, d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", weekViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", weekViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hiddenAptsIndicator", weekViewSettings.ShowHiddenAppointmentsIndicator, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enableExactTimeRendering", weekViewSettings.EnableExactTimeRenderingResolved, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showAllDayInsertArea", weekViewSettings.ShowAllDayInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showInsertArea", weekViewSettings.ShowInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", weekViewSettings.ReadOnly, false);
			return dictionary;
		}

		// Token: 0x17004EE1 RID: 20193
		// (get) Token: 0x0601046E RID: 66670 RVA: 0x003A3724 File Offset: 0x003A1924
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WeekViewSettings)
				};
			}
		}
	}
}
