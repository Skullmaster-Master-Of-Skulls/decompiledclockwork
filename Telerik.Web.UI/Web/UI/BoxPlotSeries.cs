using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.SeriesItemCollections;

namespace Telerik.Web.UI
{
	// Token: 0x020003CC RID: 972
	public class BoxPlotSeries : SeriesBase
	{
		// Token: 0x060023B6 RID: 9142 RVA: 0x00077041 File Offset: 0x00075241
		public BoxPlotSeries()
		{
			this.sType = SeriesType.BoxPlot;
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00077054 File Offset: 0x00075254
		internal override bool IsDataBound
		{
			get
			{
				return this.SeriesItems.Count == 0 && !string.IsNullOrEmpty(this.DataLowerField) && !string.IsNullOrEmpty(this.DataUpperField) && !string.IsNullOrEmpty(this.DataQ1Field) && !string.IsNullOrEmpty(this.DataMedianField) && !string.IsNullOrEmpty(this.DataQ3Field);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000770B2 File Offset: 0x000752B2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BoxPlotSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._boxPlotSeriesItems == null)
				{
					this._boxPlotSeriesItems = new BoxPlotSeriesItemCollection();
				}
				return this._boxPlotSeriesItems;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000770CD File Offset: 0x000752CD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public OutliersAppearance OutliersAppearance
		{
			get
			{
				if (this._outliersAppearance == null)
				{
					this._outliersAppearance = new OutliersAppearance("outliersAppearance", base.ViewState);
				}
				return this._outliersAppearance;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000770F3 File Offset: 0x000752F3
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ExtremesAppearance ExtremesAppearance
		{
			get
			{
				if (this._extremesAppearance == null)
				{
					this._extremesAppearance = new ExtremesAppearance("extremesAppearance", base.ViewState);
				}
				return this._extremesAppearance;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x00077119 File Offset: 0x00075319
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SeriesBorderAppearance BorderAppearance
		{
			get
			{
				if (this._borderAppearance == null)
				{
					this._borderAppearance = new SeriesBorderAppearance();
				}
				return this._borderAppearance;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x00077134 File Offset: 0x00075334
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x00077154 File Offset: 0x00075354
		[DefaultValue("")]
		public string DataLowerField
		{
			get
			{
				return (string)(base.ViewState["DataLowerField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataLowerField"] = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00077167 File Offset: 0x00075367
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x00077187 File Offset: 0x00075387
		[DefaultValue("")]
		public string DataUpperField
		{
			get
			{
				return (string)(base.ViewState["DataUpperField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataUpperField"] = value;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0007719A File Offset: 0x0007539A
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x000771BA File Offset: 0x000753BA
		[DefaultValue("")]
		public string DataQ1Field
		{
			get
			{
				return (string)(base.ViewState["DataQ1Field"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataQ1Field"] = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000771CD File Offset: 0x000753CD
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x000771ED File Offset: 0x000753ED
		[DefaultValue("")]
		public string DataMedianField
		{
			get
			{
				return (string)(base.ViewState["DataMedianField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMedianField"] = value;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x00077200 File Offset: 0x00075400
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x00077220 File Offset: 0x00075420
		[DefaultValue("")]
		public string DataQ3Field
		{
			get
			{
				return (string)(base.ViewState["DataQ3Field"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataQ3Field"] = value;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x00077233 File Offset: 0x00075433
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x00077253 File Offset: 0x00075453
		[DefaultValue("")]
		public string DataMeanField
		{
			get
			{
				return (string)(base.ViewState["DataMeanField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMeanField"] = value;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x00077266 File Offset: 0x00075466
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x00077286 File Offset: 0x00075486
		[DefaultValue("")]
		public string DataOutliersField
		{
			get
			{
				return (string)(base.ViewState["DataOutliersField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataOutliersField"] = value;
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0007729C File Offset: 0x0007549C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000772C8 File Offset: 0x000754C8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000772F6 File Offset: 0x000754F6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0007730C File Offset: 0x0007550C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("{0},", base.Serialize());
			if (!this.IsDataBound)
			{
				this.SerializeSeriesItems(stringBuilder);
			}
			else
			{
				this.SerializeDataboundProperties(stringBuilder);
			}
			this.SerializeSeriesSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00077378 File Offset: 0x00075578
		private void SerializeSeriesItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				sb.Append("data:[");
				foreach (object obj in this.SeriesItems)
				{
					BoxPlotSeriesItem boxPlotSeriesItem = (BoxPlotSeriesItem)obj;
					sb.Append("{");
					sb.Append(boxPlotSeriesItem.Serialize());
					sb.Append("},");
				}
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append("],");
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0007741C File Offset: 0x0007561C
		private void SerializeDataboundProperties(StringBuilder sb)
		{
			sb.AppendFormat("lowerField:'{0}',upperField:'{1}',q1Field:'{2}',medianField:'{3}',q3Field:'{4}',meanField:'{5}',outliersField:'{6}',", new object[]
			{
				this.DataLowerField,
				this.DataUpperField,
				this.DataQ1Field,
				this.DataMedianField,
				this.DataQ3Field,
				this.DataMeanField,
				this.DataOutliersField
			});
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0007747C File Offset: 0x0007567C
		internal override void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
			sb.AppendFormat("outliers:{0},", this.OutliersAppearance.Serialize());
			sb.AppendFormat("extremes:{0},", this.ExtremesAppearance.Serialize());
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(sb);
				sb.Append("border:").Append(value);
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00077688 File Offset: 0x00075888
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				BoxPlotSeriesItem item = (BoxPlotSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000776A5 File Offset: 0x000758A5
		internal override SeriesItemBase GetSeriesItem()
		{
			return new BoxPlotSeriesItem();
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000776AC File Offset: 0x000758AC
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000776BC File Offset: 0x000758BC
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			BoxPlotSeriesItem boxPlotSeriesItem = seriesItem as BoxPlotSeriesItem;
			if (boxPlotSeriesItem != null)
			{
				this.SeriesItems.Add(boxPlotSeriesItem);
			}
		}

		// Token: 0x04000950 RID: 2384
		private BoxPlotSeriesItemCollection _boxPlotSeriesItems;

		// Token: 0x04000951 RID: 2385
		private OutliersAppearance _outliersAppearance;

		// Token: 0x04000952 RID: 2386
		private ExtremesAppearance _extremesAppearance;

		// Token: 0x04000953 RID: 2387
		private SeriesBorderAppearance _borderAppearance;
	}
}
