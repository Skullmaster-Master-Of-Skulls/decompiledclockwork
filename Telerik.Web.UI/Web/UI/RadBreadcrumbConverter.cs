using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000013 RID: 19
	public class RadBreadcrumbConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000039D4 File Offset: 0x00001BD4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadBreadcrumb radBreadcrumb = obj as RadBreadcrumb;
			string text = "javascript:";
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radBreadcrumb.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "bindToLocation", radBreadcrumb.BindToLocation, false);
			ExplicitJavaScriptConverter.AddProperty(state, "delimiterIcon", radBreadcrumb.DelimiterIcon, "arrow-chevron-right");
			ExplicitJavaScriptConverter.AddProperty(state, "editable", radBreadcrumb.Editable, false);
			if (radBreadcrumb.Items.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "items", radBreadcrumb.Items.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "gap", radBreadcrumb.Gap, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "messages", radBreadcrumb.MessagesSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "navigational", radBreadcrumb.Navigational, false);
			ExplicitJavaScriptConverter.AddProperty(state, "rootIcon", radBreadcrumb.RootIcon, "home");
			ExplicitJavaScriptConverter.AddProperty(state, "value", radBreadcrumb.Value, "");
			base.AddScript(state, "click", radBreadcrumb.ClientEvents.OnClick);
			base.AddScript(state, "change", radBreadcrumb.ClientEvents.OnChange);
			if (radBreadcrumb.ClientItemTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "itemTemplate", radBreadcrumb.ClientItemTemplate.Substring(text.Length).TrimStart(new char[0]));
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "itemTemplate", radBreadcrumb.ClientItemTemplate, "");
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003B70 File Offset: 0x00001D70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadBreadcrumb)
				};
			}
		}
	}
}
