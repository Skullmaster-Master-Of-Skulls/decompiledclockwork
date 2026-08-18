using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x02000054 RID: 84
	internal class SeriesHighlightConverter : BorderAppearanceConverter
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00006F7C File Offset: 0x0000517C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			SeriesHighlightAppearance seriesHighlightAppearance = (SeriesHighlightAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "visible", seriesHighlightAppearance.Visible, null);
			ExplicitJavaScriptConverter.AddProperty(state, "visual", seriesHighlightAppearance.Visual, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "toggle", seriesHighlightAppearance.Toggle, string.Empty);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00006FD4 File Offset: 0x000051D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SeriesHighlightAppearance)
				};
			}
		}
	}
}
