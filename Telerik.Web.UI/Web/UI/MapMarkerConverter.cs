using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020005A2 RID: 1442
	public class MapMarkerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033C3 RID: 13251 RVA: 0x000AC1AC File Offset: 0x000AA3AC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			MapMarker mapMarker = obj as MapMarker;
			ExplicitJavaScriptConverter.AddProperty(state, "location", mapMarker.LocationSettings.ToArray(), null);
			ExplicitJavaScriptConverter.AddProperty(state, "shape", StringHelpers.ToCamelCase(mapMarker.Shape.ToString()), "pinTarget");
			ExplicitJavaScriptConverter.AddProperty(state, "title", mapMarker.Title, "pinTarget");
			ExplicitJavaScriptConverter.AddProperty(state, "tooltip", mapMarker.TooltipSettings, null);
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000AC220 File Offset: 0x000AA420
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MapMarker)
				};
			}
		}
	}
}
