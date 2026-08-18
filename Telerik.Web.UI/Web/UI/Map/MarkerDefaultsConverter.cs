using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A7 RID: 1447
	public class MarkerDefaultsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033DF RID: 13279 RVA: 0x000AC544 File Offset: 0x000AA744
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			MarkerDefaults markerDefaults = obj as MarkerDefaults;
			ExplicitJavaScriptConverter.AddProperty(state, "shape", markerDefaults.Shape, "pinTarget");
			ExplicitJavaScriptConverter.AddProperty(state, "tooltip", markerDefaults.TooltipSettings, null);
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000AC580 File Offset: 0x000AA780
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MarkerDefaults)
				};
			}
		}
	}
}
