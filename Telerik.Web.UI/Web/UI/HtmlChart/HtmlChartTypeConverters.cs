using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020004EC RID: 1260
	internal class HtmlChartTypeConverters : ExplicitJavaScriptConverter
	{
		// Token: 0x06002CFC RID: 11516 RVA: 0x00093CF8 File Offset: 0x00091EF8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			PlotBand plotBand = obj as PlotBand;
			if (plotBand.FromDate != null && plotBand.ToDate != null)
			{
				base.AddScript(state, "from", HtmlChartHelper.GetSerializedDate(new DateTime?(plotBand.FromDate.Value)));
				base.AddScript(state, "to", HtmlChartHelper.GetSerializedDate(new DateTime?(plotBand.ToDate.Value)));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "from", plotBand.From, "null");
				ExplicitJavaScriptConverter.AddProperty(state, "to", plotBand.To, "null");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "color", ColorTranslator.ToHtml(plotBand.Color), "transparent");
			double num = Math.Round((double)(byte.MaxValue - plotBand.Alpha) / 255.0, 1);
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", num, 1);
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00093E00 File Offset: 0x00092000
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(PlotBand)
				};
			}
		}
	}
}
