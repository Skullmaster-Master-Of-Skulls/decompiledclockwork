using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.SeriesItemCollections;

namespace Telerik.Web.UI
{
	// Token: 0x020004F7 RID: 1271
	public class BoxPlotSeriesItem : SeriesItemBase
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x00094B7E File Offset: 0x00092D7E
		public BoxPlotSeriesItem()
		{
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00094B86 File Offset: 0x00092D86
		public BoxPlotSeriesItem(decimal? lower, decimal? upper, decimal? q1, decimal? median, decimal? q3, decimal? mean)
		{
			this.Upper = upper;
			this.Lower = lower;
			this.Q1 = q1;
			this.Median = median;
			this.Q3 = q3;
			this.Mean = mean;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x00094BBB File Offset: 0x00092DBB
		public BoxPlotSeriesItem(decimal? lower, decimal? upper, decimal? q1, decimal? median, decimal? q3, decimal? mean, Color backgroundColor) : this(lower, upper, q1, median, q3, mean)
		{
			base.BackgroundColor = backgroundColor;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x00094BD4 File Offset: 0x00092DD4
		public BoxPlotSeriesItem(decimal? lower, decimal? upper, decimal? q1, decimal? median, decimal? q3, decimal? mean, decimal?[] outliers) : this(lower, upper, q1, median, q3, mean)
		{
			this.Outliers.AddRange(outliers);
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x00094BF2 File Offset: 0x00092DF2
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x00094C09 File Offset: 0x00092E09
		[DefaultValue(null)]
		public decimal? Lower
		{
			get
			{
				return (decimal?)base.ViewState["Lower"];
			}
			set
			{
				base.ViewState["Lower"] = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x00094C21 File Offset: 0x00092E21
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x00094C38 File Offset: 0x00092E38
		[DefaultValue(null)]
		public decimal? Upper
		{
			get
			{
				return (decimal?)base.ViewState["Upper"];
			}
			set
			{
				base.ViewState["Upper"] = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x00094C50 File Offset: 0x00092E50
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x00094C67 File Offset: 0x00092E67
		[DefaultValue(null)]
		public decimal? Q1
		{
			get
			{
				return (decimal?)base.ViewState["Q1"];
			}
			set
			{
				base.ViewState["Q1"] = value;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x00094C7F File Offset: 0x00092E7F
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x00094C96 File Offset: 0x00092E96
		[DefaultValue(null)]
		public decimal? Median
		{
			get
			{
				return (decimal?)base.ViewState["Median"];
			}
			set
			{
				base.ViewState["Median"] = value;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x00094CAE File Offset: 0x00092EAE
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x00094CC5 File Offset: 0x00092EC5
		[DefaultValue(null)]
		public decimal? Q3
		{
			get
			{
				return (decimal?)base.ViewState["Q3"];
			}
			set
			{
				base.ViewState["Q3"] = value;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00094CDD File Offset: 0x00092EDD
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x00094CF4 File Offset: 0x00092EF4
		[DefaultValue(null)]
		public decimal? Mean
		{
			get
			{
				return (decimal?)base.ViewState["Mean"];
			}
			set
			{
				base.ViewState["Mean"] = value;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00094D0C File Offset: 0x00092F0C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OutliersCollection Outliers
		{
			get
			{
				if (this._outliers == null)
				{
					this._outliers = new OutliersCollection();
				}
				return this._outliers;
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00094D28 File Offset: 0x00092F28
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			this.SerializeItemSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00094D5C File Offset: 0x00092F5C
		private void SerializeItemSpecificProperties(StringBuilder sb)
		{
			sb.AppendFormat("lower:{0},", HtmlChartHelper.ToStringInvariant(this.Lower));
			sb.AppendFormat("upper:{0},", HtmlChartHelper.ToStringInvariant(this.Upper));
			sb.AppendFormat("q1:{0},", HtmlChartHelper.ToStringInvariant(this.Q1));
			sb.AppendFormat("median:{0},", HtmlChartHelper.ToStringInvariant(this.Median));
			sb.AppendFormat("q3:{0},", HtmlChartHelper.ToStringInvariant(this.Q3));
			sb.AppendFormat("mean:{0},", HtmlChartHelper.ToStringInvariant(this.Mean));
			if (this.Outliers.Count > 0)
			{
				sb.AppendFormat("{0},", this.Outliers.Serialize());
			}
		}

		// Token: 0x04000C33 RID: 3123
		private OutliersCollection _outliers;
	}
}
