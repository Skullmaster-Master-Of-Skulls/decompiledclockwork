using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200059F RID: 1439
	public class MapLayerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033B3 RID: 13235 RVA: 0x000ABD48 File Offset: 0x000A9F48
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			MapLayer mapLayer = obj as MapLayer;
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", mapLayer.Attribution, "");
			if (!mapLayer.ExtentSettings.IsDefault)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "extent", mapLayer.ExtentSettings.ToArray(), null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "key", mapLayer.Key, "");
			ExplicitJavaScriptConverter.AddProperty(state, "imagerySet", mapLayer.ImagerySet, "road");
			ExplicitJavaScriptConverter.AddProperty(state, "culture", mapLayer.Culture, "en-US");
			ExplicitJavaScriptConverter.AddProperty(state, "locationField", mapLayer.LocationField, "location");
			ExplicitJavaScriptConverter.AddProperty(state, "shape", mapLayer.Shape, "pinTarget");
			ExplicitJavaScriptConverter.AddProperty(state, "tileSize", mapLayer.TileSize, 256.0);
			ExplicitJavaScriptConverter.AddProperty(state, "titleField", mapLayer.TitleField, "title");
			ExplicitJavaScriptConverter.AddProperty(state, "tooltip", mapLayer.TooltipSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "maxSize", mapLayer.MaxSize, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minSize", mapLayer.MinSize, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", mapLayer.Opacity, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "subdomains", mapLayer.Subdomains, null);
			if (mapLayer.Symbol.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "symbol", mapLayer.Symbol.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "symbol", mapLayer.Symbol, "circle");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "type", StringHelpers.ToCamelCase(mapLayer.Type.ToString()), StringHelpers.ToCamelCase("".ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "style", mapLayer.StyleSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "urlTemplate", mapLayer.UrlTemplate, "");
			ExplicitJavaScriptConverter.AddProperty(state, "valueField", mapLayer.ValueField, "value");
			ExplicitJavaScriptConverter.AddProperty(state, "zIndex", mapLayer.ZIndex, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minZoom", mapLayer.MinZoom, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "maxZoom", mapLayer.MaxZoom, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "clientDataSourceID", mapLayer.ClientDataSourceID, "");
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000AC00C File Offset: 0x000AA20C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MapLayer)
				};
			}
		}
	}
}
