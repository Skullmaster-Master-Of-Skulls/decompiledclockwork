using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A33 RID: 6707
	internal class TimeLineViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601046A RID: 66666 RVA: 0x003A3378 File Offset: 0x003A1578
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimelineViewSettings timelineViewSettings = obj as TimelineViewSettings;
			if (timelineViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only TimelineViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "slotDuration", (long)timelineViewSettings.SlotDuration.TotalMilliseconds, (long)TimeSpan.FromDays(1.0).TotalMilliseconds);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "startTime", (int)timelineViewSettings.StartTime.TotalMilliseconds, 0);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "numberOfSlots", timelineViewSettings.NumberOfSlots, 3);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", timelineViewSettings.HeaderDateFormat, "d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", timelineViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", timelineViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "columnHeaderDateFormat", timelineViewSettings.ColumnHeaderDateFormat, "d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "timeLabelSpan", timelineViewSettings.TimeLabelSpan, 1);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showInsertArea", timelineViewSettings.ShowInsertArea, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enableExactTimeRendering", timelineViewSettings.EnableExactTimeRenderingResolved, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", timelineViewSettings.ReadOnly, false);
			return dictionary;
		}

		// Token: 0x17004EE0 RID: 20192
		// (get) Token: 0x0601046B RID: 66667 RVA: 0x003A34F0 File Offset: 0x003A16F0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TimelineViewSettings)
				};
			}
		}
	}
}
