using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B9A RID: 2970
	public class HtmlChartPlotArea : ObjectWithState
	{
		// Token: 0x06007029 RID: 28713 RVA: 0x001A300B File Offset: 0x001A120B
		public HtmlChartPlotArea(StateBag OwnerStateBag) : base("pa", OwnerStateBag)
		{
			this._additionalYAxes = new AdditionalYAxes();
		}

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x0600702A RID: 28714 RVA: 0x001A3024 File Offset: 0x001A1224
		[Description("Chart' plot area visual settings")]
		[DefaultValue("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public PlotAreaAppearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new PlotAreaAppearance(base.OwnerViewState);
				}
				return this._appearance;
			}
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x0600702B RID: 28715 RVA: 0x001A3045 File Offset: 0x001A1245
		[Category("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Chart x axis")]
		public ChartXAxis XAxis
		{
			get
			{
				if (this._xAxis == null)
				{
					this._xAxis = new ChartXAxis();
				}
				return this._xAxis;
			}
		}

		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x0600702C RID: 28716 RVA: 0x001A3060 File Offset: 0x001A1260
		[Category("Data")]
		[Description("Chart y axis")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartYAxis YAxis
		{
			get
			{
				if (this._yAxis == null)
				{
					this._yAxis = new ChartYAxis();
				}
				return this._yAxis;
			}
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x0600702D RID: 28717 RVA: 0x001A307B File Offset: 0x001A127B
		[Description("Additional chart y axes")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		public AdditionalYAxes AdditionalYAxes
		{
			get
			{
				return this._additionalYAxes;
			}
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x0600702E RID: 28718 RVA: 0x001A3083 File Offset: 0x001A1283
		[Description("Chart plot area settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("ChartPlotArea")]
		[Category("Appearance")]
		public SeriesCollection Series
		{
			get
			{
				if (this._series == null)
				{
					this._series = new SeriesCollection();
					this._series.CollectionChanged += this.OnSeriesCollectionChanged;
				}
				return this._series;
			}
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x0600702F RID: 28719 RVA: 0x001A30B5 File Offset: 0x001A12B5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series' common tooltips visual settings")]
		[DefaultValue("CommonTooltipsAppearance")]
		[Category("Appearance")]
		public CommonTooltipsAppearance CommonTooltipsAppearance
		{
			get
			{
				if (this._commonTooltipsAppearance == null)
				{
					this._commonTooltipsAppearance = new CommonTooltipsAppearance("cta", base.OwnerViewState);
				}
				return this._commonTooltipsAppearance;
			}
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x001A30DC File Offset: 0x001A12DC
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			if (this.XAxis.PlotType != PlotType.Pie && this.XAxis.PlotType != PlotType.Funnel)
			{
				stringBuilder.Append(this.XAxis.Serialize());
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.AppendFormat("{0}:[", (this.XAxis.PlotType == PlotType.Numeric || this.XAxis.PlotType == PlotType.Polar) ? "yAxis" : "valueAxis");
				stringBuilder.Append(this.YAxis.Serialize());
				if (this.XAxis.PlotType != PlotType.Radar && this.XAxis.PlotType != PlotType.Polar)
				{
					foreach (object obj in this.AdditionalYAxes)
					{
						AxisY axisY = (AxisY)obj;
						stringBuilder.AppendFormat(",{0}", axisY.Serialize());
					}
				}
				stringBuilder.Append("]");
			}
			string text = this.Appearance.Serialize();
			if (text != string.Empty)
			{
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("appearance: ").Append(text);
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.AppendFormat("{0},", this.CommonTooltipsAppearance.Serialize());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x001A328C File Offset: 0x001A148C
		private void OnSeriesCollectionChanged(object sender, EventArgs e)
		{
			this.DefineAxes();
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x001A3294 File Offset: 0x001A1494
		private void DefineAxes()
		{
			if (this.Series.HasPolarSeries)
			{
				this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Polar);
				return;
			}
			if (this.Series.HasNumericSeries)
			{
				this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Numeric);
				return;
			}
			if (this.Series.HasPieSeries)
			{
				this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Pie);
				return;
			}
			if (this.Series.HasRadarSeries)
			{
				this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Radar);
				return;
			}
			if (this.Series.HasFunnelSeries)
			{
				this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Funnel);
				return;
			}
			this.XAxis.PlotType = (this.YAxis.PlotType = PlotType.Categorial);
		}

		// Token: 0x04001E1A RID: 7706
		private PlotAreaAppearance _appearance;

		// Token: 0x04001E1B RID: 7707
		private ChartXAxis _xAxis;

		// Token: 0x04001E1C RID: 7708
		private ChartYAxis _yAxis;

		// Token: 0x04001E1D RID: 7709
		private AdditionalYAxes _additionalYAxes;

		// Token: 0x04001E1E RID: 7710
		private SeriesCollection _series;

		// Token: 0x04001E1F RID: 7711
		private CommonTooltipsAppearance _commonTooltipsAppearance;
	}
}
