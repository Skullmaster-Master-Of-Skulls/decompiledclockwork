using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000826 RID: 2086
	internal class AgendaViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06004D31 RID: 19761 RVA: 0x000F2CB0 File Offset: 0x000F0EB0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AgendaViewSettings agendaViewSettings = obj as AgendaViewSettings;
			if (agendaViewSettings == null)
			{
				throw new InvalidOperationException("Can serialize only AgendaViewSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "groupBy", agendaViewSettings.GroupByResolved, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "readOnly", agendaViewSettings.ReadOnly, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "numberOfDays", agendaViewSettings.NumberOfDays, 7);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "headerDateFormat", agendaViewSettings.HeaderDateFormat, "d");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "resourceMarkerType", agendaViewSettings.ResourceMarkerType, null);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isVertical", agendaViewSettings.GroupingDirectionResolved == GroupingDirection.Vertical, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showDateHeaders", agendaViewSettings.ShowDateHeaders, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showResourceHeaders", agendaViewSettings.ShowResourceHeaders, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "resourceColumnWidth", agendaViewSettings.ResourceColumnWidth.Value, Unit.Empty.Value);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "dateColumnWidth", agendaViewSettings.DateColumnWidth.Value, Unit.Empty.Value);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "timeColumnWidth", agendaViewSettings.TimeColumnWidth.Value, Unit.Empty.Value);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "appointmentColumnWidth", agendaViewSettings.AppointmentColumnWidth.Value, Unit.Empty.Value);
			return dictionary;
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06004D32 RID: 19762 RVA: 0x000F2E74 File Offset: 0x000F1074
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AgendaViewSettings)
				};
			}
		}
	}
}
