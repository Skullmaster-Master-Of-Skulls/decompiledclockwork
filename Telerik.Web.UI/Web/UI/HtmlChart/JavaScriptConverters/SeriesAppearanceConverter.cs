using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x020003D6 RID: 982
	public class SeriesAppearanceConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x00077DEC File Offset: 0x00075FEC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			SeriesAppearance seriesAppearance = (SeriesAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "overlay", seriesAppearance.Overlay, null);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00077E14 File Offset: 0x00076014
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SeriesAppearance)
				};
			}
		}
	}
}
