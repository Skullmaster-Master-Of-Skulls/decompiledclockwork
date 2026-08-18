using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B6 RID: 1462
	public class TileConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600342C RID: 13356 RVA: 0x000AD170 File Offset: 0x000AB370
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Tile tile = obj as Tile;
			ExplicitJavaScriptConverter.AddProperty(state, "urlTemplate", tile.UrlTemplate, "");
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", tile.Attribution, "");
			ExplicitJavaScriptConverter.AddProperty(state, "subdomains", tile.Subdomains, null);
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", tile.Opacity, 1.0);
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000AD1E8 File Offset: 0x000AB3E8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Tile)
				};
			}
		}
	}
}
