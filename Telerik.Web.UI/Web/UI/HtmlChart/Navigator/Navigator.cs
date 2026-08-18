using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.Navigator
{
	// Token: 0x020004EE RID: 1262
	public class Navigator : ObjectWithState
	{
		// Token: 0x06002D02 RID: 11522 RVA: 0x00093F0E File Offset: 0x0009210E
		public Navigator(StateBag OwnerStateBag) : base("chn", OwnerStateBag)
		{
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00093F1C File Offset: 0x0009211C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[Description("Navigator's series settings")]
		public SeriesCollection Series
		{
			get
			{
				if (this._series == null)
				{
					this._series = new SeriesCollection();
				}
				return this._series;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x00093F37 File Offset: 0x00092137
		[Category("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Navigator's selection settings")]
		public RangeSelector RangeSelector
		{
			get
			{
				if (this._rangeSelector == null)
				{
					this._rangeSelector = new RangeSelector(base.OwnerViewState);
				}
				return this._rangeSelector;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x00093F58 File Offset: 0x00092158
		[Description("Navigator's selection hint settings")]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SelectionHint SelectionHint
		{
			get
			{
				if (this._selectionHint == null)
				{
					this._selectionHint = new SelectionHint(base.OwnerViewState);
				}
				return this._selectionHint;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x00093F7C File Offset: 0x0009217C
		[Category("Data")]
		[Description("Navigator X axis settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartXAxis XAxis
		{
			get
			{
				if (this._xAxis == null)
				{
					this._xAxis = new ChartXAxis
					{
						PlotType = PlotType.Categorial
					};
				}
				return this._xAxis;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00093FAB File Offset: 0x000921AB
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x00093FCC File Offset: 0x000921CC
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x00093FE4 File Offset: 0x000921E4
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			if (this.Series.Count > 0)
			{
				stringBuilder.AppendFormat("series: {0},", this.Series.Serialize());
			}
			if (this.RangeSelector.From != null || this.RangeSelector.To != null)
			{
				stringBuilder.AppendFormat("{0},", this.RangeSelector.Serialize());
			}
			stringBuilder.AppendFormat("{0},", this.SelectionHint.Serialize());
			stringBuilder.AppendFormat("{0},", this.XAxis.Serialize());
			if (!this.Visible)
			{
				stringBuilder.Append("visible:false,");
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000C2B RID: 3115
		private SeriesCollection _series;

		// Token: 0x04000C2C RID: 3116
		private RangeSelector _rangeSelector;

		// Token: 0x04000C2D RID: 3117
		private SelectionHint _selectionHint;

		// Token: 0x04000C2E RID: 3118
		private ChartXAxis _xAxis;
	}
}
