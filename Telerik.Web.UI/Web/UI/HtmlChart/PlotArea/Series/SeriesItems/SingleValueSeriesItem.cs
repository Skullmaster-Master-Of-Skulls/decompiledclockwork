using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems
{
	// Token: 0x020003ED RID: 1005
	public abstract class SingleValueSeriesItem : SeriesItemBase
	{
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x0007B225 File Offset: 0x00079425
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x0007B23C File Offset: 0x0007943C
		[DefaultValue(null)]
		public decimal? Y
		{
			get
			{
				return (decimal?)base.ViewState["Y"];
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0007B254 File Offset: 0x00079454
		protected internal SingleValueSeriesItem()
		{
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0007B25C File Offset: 0x0007945C
		protected internal SingleValueSeriesItem(decimal? y)
		{
			this.Y = y;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0007B26B File Offset: 0x0007946B
		protected internal SingleValueSeriesItem(decimal? y, Color backgroundColor) : this(y)
		{
			base.BackgroundColor = backgroundColor;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0007B27C File Offset: 0x0007947C
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			stringBuilder.AppendFormat("value:{0},", HtmlChartHelper.ToStringInvariant(this.Y));
			return stringBuilder.ToString();
		}
	}
}
