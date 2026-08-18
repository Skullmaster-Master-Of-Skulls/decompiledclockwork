using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003D4 RID: 980
	internal class ZoomConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002403 RID: 9219 RVA: 0x00077CB0 File Offset: 0x00075EB0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Zoom zoom = obj as Zoom;
			if (zoom != null)
			{
				if (zoom.MouseWheel.Enabled)
				{
					ExplicitJavaScriptConverter.AddProperty(state, "mousewheel", zoom.MouseWheel, null);
				}
				else
				{
					ExplicitJavaScriptConverter.AddProperty(state, "mousewheel", false, true);
				}
				if (zoom.Selection.Enabled)
				{
					ExplicitJavaScriptConverter.AddProperty(state, "selection", zoom.Selection, null);
					return;
				}
				ExplicitJavaScriptConverter.AddProperty(state, "selection", false, true);
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00077D38 File Offset: 0x00075F38
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Zoom),
					typeof(MouseWheelZoom),
					typeof(SelectionZoom)
				};
			}
		}
	}
}
