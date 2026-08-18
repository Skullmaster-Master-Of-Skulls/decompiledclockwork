using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A5 RID: 1445
	public class MarkerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033D3 RID: 13267 RVA: 0x000AC3B4 File Offset: 0x000AA5B4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Marker marker = obj as Marker;
			ExplicitJavaScriptConverter.AddProperty(state, "shape", marker.Shape, "pinTarget");
			ExplicitJavaScriptConverter.AddProperty(state, "tooltip", marker.TooltipSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", marker.Opacity, 1.0);
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000AC414 File Offset: 0x000AA614
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Marker)
				};
			}
		}
	}
}
