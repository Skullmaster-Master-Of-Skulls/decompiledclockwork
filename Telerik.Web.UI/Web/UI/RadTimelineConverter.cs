using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000934 RID: 2356
	public class RadTimelineConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005984 RID: 22916 RVA: 0x001106F4 File Offset: 0x0010E8F4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadTimeline radTimeline = obj as RadTimeline;
			string text = "javascript:";
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radTimeline.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "navigatable", radTimeline.Navigatable, false);
			ExplicitJavaScriptConverter.AddProperty(state, "alternatingMode", radTimeline.AlternatingMode, false);
			ExplicitJavaScriptConverter.AddProperty(state, "orientation", radTimeline.Orientation, RadTimelineOrientation.Vertical);
			ExplicitJavaScriptConverter.AddProperty(state, "collapsibleEvents", radTimeline.CollapsibleEvents, false);
			ExplicitJavaScriptConverter.AddProperty(state, "dataActionsField", radTimeline.DataActionsField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataDescriptionField", radTimeline.DataDescriptionField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataDateField", radTimeline.DataDateField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataImagesField", radTimeline.DataImagesField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataSubtitleField", radTimeline.DataSubtitleField, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dataTitleField", radTimeline.DataTitleField, "");
			if (radTimeline.EventTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "eventTemplate", radTimeline.EventTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "eventTemplate", radTimeline.EventTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "dateFormat", radTimeline.DateFormat, "MMM d, yyyy");
			ExplicitJavaScriptConverter.AddProperty(state, "eventHeight", radTimeline.EventHeight, 600.0);
			ExplicitJavaScriptConverter.AddProperty(state, "eventWidth", radTimeline.EventWidth, 400.0);
			ExplicitJavaScriptConverter.AddProperty(state, "showDateLabels", radTimeline.ShowDateLabels, true);
			base.AddScript(state, "change", radTimeline.ClientEvents.OnChange);
			base.AddScript(state, "dataBound", radTimeline.ClientEvents.OnDataBound);
			base.AddScript(state, "expand", radTimeline.ClientEvents.OnExpand);
			base.AddScript(state, "collapse", radTimeline.ClientEvents.OnCollapse);
			base.AddScript(state, "actionClick", radTimeline.ClientEvents.OnActionClick);
			base.AddScript(state, "navigate", radTimeline.ClientEvents.OnNavigate);
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x0011096C File Offset: 0x0010EB6C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadTimeline)
				};
			}
		}
	}
}
