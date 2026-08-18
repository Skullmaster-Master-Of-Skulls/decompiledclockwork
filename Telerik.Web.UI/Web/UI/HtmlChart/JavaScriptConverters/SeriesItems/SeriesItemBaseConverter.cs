using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.SeriesItems
{
	// Token: 0x020003B8 RID: 952
	public class SeriesItemBaseConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002326 RID: 8998 RVA: 0x00075C18 File Offset: 0x00073E18
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			SeriesItemBase seriesItemBase = (SeriesItemBase)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "color", HtmlChartHelper.ToSerializableColor(seriesItemBase.BackgroundColor), string.Empty);
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00075C48 File Offset: 0x00073E48
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SeriesItemBase)
				};
			}
		}
	}
}
