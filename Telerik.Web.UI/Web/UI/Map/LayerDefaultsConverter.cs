using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200059B RID: 1435
	public class LayerDefaultsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003379 RID: 13177 RVA: 0x000AB5FC File Offset: 0x000A97FC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			LayerDefaults layerDefaults = obj as LayerDefaults;
			ExplicitJavaScriptConverter.AddProperty(state, "marker", layerDefaults.MarkerSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "shape", layerDefaults.ShapeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "bubble", layerDefaults.BubbleSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "tileSize", layerDefaults.TileSize, 256.0);
			ExplicitJavaScriptConverter.AddProperty(state, "tile", layerDefaults.TileSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "bing", layerDefaults.BingSettings, null);
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000AB690 File Offset: 0x000A9890
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(LayerDefaults)
				};
			}
		}
	}
}
