using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x020003D3 RID: 979
	internal class HtmlChartClientEventsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002400 RID: 9216 RVA: 0x00077BA8 File Offset: 0x00075DA8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			HtmlChartClientEvents htmlChartClientEvents = obj as HtmlChartClientEvents;
			base.AddScript(state, "seriesClick", htmlChartClientEvents.OnSeriesClick);
			base.AddScript(state, "seriesHover", htmlChartClientEvents.OnSeriesHover);
			base.AddScript(state, "legendItemClick", htmlChartClientEvents.OnLegendItemClick);
			base.AddScript(state, "legendItemHover", htmlChartClientEvents.OnLegendItemHover);
			base.AddScript(state, "dragStart", htmlChartClientEvents.OnDragStart);
			base.AddScript(state, "drag", htmlChartClientEvents.OnDrag);
			base.AddScript(state, "dragEnd", htmlChartClientEvents.OnDragEnd);
			base.AddScript(state, "zoomStart", htmlChartClientEvents.OnZoomStart);
			base.AddScript(state, "zoom", htmlChartClientEvents.OnZoom);
			base.AddScript(state, "zoomEnd", htmlChartClientEvents.OnZoomEnd);
			base.AddScript(state, "kendoWidgetInitializing", htmlChartClientEvents.OnKendoWidgetInitializing);
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00077C84 File Offset: 0x00075E84
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(HtmlChartClientEvents)
				};
			}
		}
	}
}
