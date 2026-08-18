using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x020003BA RID: 954
	internal class SeriesTooltipsConverter : BorderAppearanceConverter
	{
		// Token: 0x0600232C RID: 9004 RVA: 0x00075CEC File Offset: 0x00073EEC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			SeriesTooltipsAppearance seriesTooltipsAppearance = (SeriesTooltipsAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "visible", seriesTooltipsAppearance.Visible, null);
			if (seriesTooltipsAppearance.BackgroundColor != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "background", HtmlChartHelper.ColorToHex(seriesTooltipsAppearance.BackgroundColor));
			}
			if (seriesTooltipsAppearance.Color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "color", HtmlChartHelper.ColorToHex(seriesTooltipsAppearance.Color));
			}
			ExplicitJavaScriptConverter.AddProperty(state, "format", seriesTooltipsAppearance.DataFormatString, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "template", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(seriesTooltipsAppearance.ClientTemplate), string.Empty);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00075D98 File Offset: 0x00073F98
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SeriesTooltipsAppearance)
				};
			}
		}
	}
}
