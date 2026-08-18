using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020005AD RID: 1453
	public class RadMapConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033F2 RID: 13298 RVA: 0x000AC788 File Offset: 0x000AA988
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadMap radMap = obj as RadMap;
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radMap.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "center", radMap.CenterSettings.ToArray(), null);
			ExplicitJavaScriptConverter.AddProperty(state, "controls", radMap.ControlsSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "layerDefaults", radMap.LayerDefaultsSettings, null);
			if (radMap.LayersCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "layers", radMap.LayersCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "markerDefaults", radMap.MarkerDefaultsSettings, null);
			if (radMap.MarkersCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "markers", radMap.MarkersCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "minZoom", radMap.MinZoom, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "maxZoom", radMap.MaxZoom, 19.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minSize", radMap.MinSize, 256.0);
			ExplicitJavaScriptConverter.AddProperty(state, "pannable", radMap.Pannable, true);
			ExplicitJavaScriptConverter.AddProperty(state, "wraparound", radMap.Wraparound, true);
			ExplicitJavaScriptConverter.AddProperty(state, "zoom", radMap.Zoom, 3.0);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomable", radMap.Zoomable, true);
			base.AddScript(state, "beforeReset", radMap.ClientEvents.OnBeforeReset);
			base.AddScript(state, "click", radMap.ClientEvents.OnClick);
			base.AddScript(state, "markerActivate", radMap.ClientEvents.OnMarkerActivate);
			base.AddScript(state, "markerCreated", radMap.ClientEvents.OnMarkerCreated);
			base.AddScript(state, "markerClick", radMap.ClientEvents.OnMarkerClick);
			base.AddScript(state, "pan", radMap.ClientEvents.OnPan);
			base.AddScript(state, "panEnd", radMap.ClientEvents.OnPanEnd);
			base.AddScript(state, "reset", radMap.ClientEvents.OnReset);
			base.AddScript(state, "shapeClick", radMap.ClientEvents.OnShapeClick);
			base.AddScript(state, "shapeCreated", radMap.ClientEvents.OnShapeCreated);
			base.AddScript(state, "shapeFeatureCreated", radMap.ClientEvents.OnShapeFeatureCreated);
			base.AddScript(state, "shapeMouseEnter", radMap.ClientEvents.OnShapeMouseEnter);
			base.AddScript(state, "shapeMouseLeave", radMap.ClientEvents.OnShapeMouseLeave);
			base.AddScript(state, "zoomStart", radMap.ClientEvents.OnZoomStart);
			base.AddScript(state, "zoomEnd", radMap.ClientEvents.OnZoomEnd);
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000ACA84 File Offset: 0x000AAC84
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadMap)
				};
			}
		}
	}
}
