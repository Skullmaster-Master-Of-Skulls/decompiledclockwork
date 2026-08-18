using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.SeriesItems
{
	// Token: 0x020003BF RID: 959
	public class CategorySeriesItemConverter : SeriesItemBaseConverter
	{
		// Token: 0x0600233B RID: 9019 RVA: 0x00075FDC File Offset: 0x000741DC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			CategorySeriesItem categorySeriesItem = (CategorySeriesItem)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "value", HtmlChartHelper.ToStringInvariant(categorySeriesItem.Y), null);
			base.PopulateProperties(state, obj);
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00076010 File Offset: 0x00074210
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(CategorySeriesItem)
				};
			}
		}
	}
}
