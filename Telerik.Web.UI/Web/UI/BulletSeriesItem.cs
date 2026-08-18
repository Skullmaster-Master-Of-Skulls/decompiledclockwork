using System;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020003C8 RID: 968
	public class BulletSeriesItem : SeriesItemBase
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x0007651E File Offset: 0x0007471E
		public BulletSeriesItem()
		{
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00076526 File Offset: 0x00074726
		public BulletSeriesItem(decimal? current) : this()
		{
			this.Current = current;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00076535 File Offset: 0x00074735
		public BulletSeriesItem(decimal? current, decimal? target) : this(current)
		{
			this.Target = target;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00076545 File Offset: 0x00074745
		public BulletSeriesItem(decimal? current, decimal? target, Color bgColor) : this(current, target)
		{
			base.BackgroundColor = bgColor;
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x00076556 File Offset: 0x00074756
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x0007656D File Offset: 0x0007476D
		public decimal? Current
		{
			get
			{
				return (decimal?)base.ViewState["Current"];
			}
			set
			{
				base.ViewState["Current"] = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x00076585 File Offset: 0x00074785
		// (set) Token: 0x0600236F RID: 9071 RVA: 0x0007659C File Offset: 0x0007479C
		public decimal? Target
		{
			get
			{
				return (decimal?)base.ViewState["Target"];
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}
	}
}
