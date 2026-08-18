using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000512 RID: 1298
	public class CandlestickSeriesItem : SeriesItemBase
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x000983A4 File Offset: 0x000965A4
		public CandlestickSeriesItem()
		{
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000983AC File Offset: 0x000965AC
		public CandlestickSeriesItem(decimal? open, decimal? close, decimal? high, decimal? low)
		{
			this.Open = open;
			this.Close = close;
			this.High = high;
			this.Low = low;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000983D1 File Offset: 0x000965D1
		public CandlestickSeriesItem(decimal? open, decimal? close, decimal? high, decimal? low, Color backgroundColor) : this(open, close, high, low)
		{
			base.BackgroundColor = backgroundColor;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000983E6 File Offset: 0x000965E6
		public CandlestickSeriesItem(Color downColor, decimal? open, decimal? close, decimal? high, decimal? low) : this(open, close, high, low)
		{
			this.DownColor = downColor;
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000983FB File Offset: 0x000965FB
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x00098412 File Offset: 0x00096612
		[DefaultValue(null)]
		public decimal? Open
		{
			get
			{
				return (decimal?)base.ViewState["Open"];
			}
			set
			{
				base.ViewState["Open"] = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x0009842A File Offset: 0x0009662A
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x00098441 File Offset: 0x00096641
		[DefaultValue(null)]
		public decimal? Close
		{
			get
			{
				return (decimal?)base.ViewState["Close"];
			}
			set
			{
				base.ViewState["Close"] = value;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x00098459 File Offset: 0x00096659
		// (set) Token: 0x06002E74 RID: 11892 RVA: 0x00098470 File Offset: 0x00096670
		[DefaultValue(null)]
		public decimal? High
		{
			get
			{
				return (decimal?)base.ViewState["High"];
			}
			set
			{
				base.ViewState["High"] = value;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x00098488 File Offset: 0x00096688
		// (set) Token: 0x06002E76 RID: 11894 RVA: 0x0009849F File Offset: 0x0009669F
		[DefaultValue(null)]
		public decimal? Low
		{
			get
			{
				return (decimal?)base.ViewState["Low"];
			}
			set
			{
				base.ViewState["Low"] = value;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000984B7 File Offset: 0x000966B7
		// (set) Token: 0x06002E78 RID: 11896 RVA: 0x000984DC File Offset: 0x000966DC
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
		public Color DownColor
		{
			get
			{
				return (Color)(base.ViewState["DownColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["DownColor"] = value;
			}
		}
	}
}
