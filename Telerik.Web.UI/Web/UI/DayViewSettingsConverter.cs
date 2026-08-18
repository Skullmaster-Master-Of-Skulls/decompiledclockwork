using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A32 RID: 6706
	internal class DayViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010467 RID: 66663 RVA: 0x003A3158 File Offset: 0x003A1358
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DayViewSettings dayViewSettings = obj as DayViewSettings;
			if (dayViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only DayViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayStartTime", (int)dayViewSettings.DayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dayEndTime", (int)dayViewSettings.DayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(18.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayStartTime", (int)dayViewSettings.WorkDayStartTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(8.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "workDayEndTime", (int)dayViewSettings.WorkDayEndTimeResolved.TotalMilliseconds, (int)TimeSpan.FromHours(17.0).TotalMinutes);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", dayViewSettings.HeaderDateFormat, "D");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", dayViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", dayViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hiddenAptsIndicator", dayViewSettings.ShowHiddenAppointmentsIndicator, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enableExactTimeRendering", dayViewSettings.EnableExactTimeRenderingResolved, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showAllDayInsertArea", dayViewSettings.ShowAllDayInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showInsertArea", dayViewSettings.ShowInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", dayViewSettings.ReadOnly, false);
			return dictionary;
		}

		// Token: 0x17004EDF RID: 20191
		// (get) Token: 0x06010468 RID: 66664 RVA: 0x003A334C File Offset: 0x003A154C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DayViewSettings)
				};
			}
		}
	}
}
