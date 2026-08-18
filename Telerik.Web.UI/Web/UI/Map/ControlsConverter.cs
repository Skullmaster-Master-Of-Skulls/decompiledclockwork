using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000597 RID: 1431
	public class ControlsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003360 RID: 13152 RVA: 0x000AB160 File Offset: 0x000A9360
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Controls controls = obj as Controls;
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", controls.Attribution, true);
			if (controls.Attribution)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "attribution", controls.AttributionSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "navigator", controls.Navigator, true);
			if (controls.Navigator)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "navigator", controls.NavigatorSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "zoom", controls.Zoom, true);
			if (controls.Zoom)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "zoom", controls.ZoomSettings, null);
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000AB218 File Offset: 0x000A9418
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Controls)
				};
			}
		}
	}
}
