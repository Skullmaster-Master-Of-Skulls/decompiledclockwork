using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000043 RID: 67
	public class RadDrawerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600023A RID: 570 RVA: 0x000060B0 File Offset: 0x000042B0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadDrawer radDrawer = obj as RadDrawer;
			ExplicitJavaScriptConverter.AddProperty(state, "navigatable", radDrawer.Navigatable, false);
			ExplicitJavaScriptConverter.AddProperty(state, "position", radDrawer.Position.ToString().ToLower(), "left");
			ExplicitJavaScriptConverter.AddProperty(state, "mode", radDrawer.Mode.ToString().ToLower(), "overlay");
			ExplicitJavaScriptConverter.AddProperty(state, "showBorders", radDrawer.ShowBorders, false);
			if (radDrawer.ItemsTemplate.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", radDrawer.ItemsTemplate.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", radDrawer.ItemsTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "minHeight", radDrawer.MinHeight, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "mini", radDrawer.Mini, false);
			ExplicitJavaScriptConverter.AddProperty(state, "mini", radDrawer.MiniSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "swipeToOpen", radDrawer.SwipeToOpen, true);
			ExplicitJavaScriptConverter.AddProperty(state, "width", radDrawer.DrawerWidth, 0.0);
			base.AddScript(state, "hide", radDrawer.ClientEvents.OnHide);
			base.AddScript(state, "show", radDrawer.ClientEvents.OnShow);
			base.AddScript(state, "itemClick", radDrawer.ClientEvents.OnItemClick);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000626C File Offset: 0x0000446C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadDrawer)
				};
			}
		}
	}
}
