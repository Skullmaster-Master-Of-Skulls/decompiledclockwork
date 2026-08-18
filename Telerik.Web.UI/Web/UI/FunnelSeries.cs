using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020004FB RID: 1275
	public class FunnelSeries : SeriesBase
	{
		// Token: 0x06002D88 RID: 11656 RVA: 0x0009572F File Offset: 0x0009392F
		public FunnelSeries()
		{
			this.sType = SeriesType.Funnel;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x0009573F File Offset: 0x0009393F
		internal override bool IsDataBound
		{
			get
			{
				return this.SeriesItems.Count == 0 && base.DataFieldY != string.Empty;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x00095760 File Offset: 0x00093960
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FunnelSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._funnelSeriesItems == null)
				{
					this._funnelSeriesItems = new FunnelSeriesItemCollection();
				}
				return this._funnelSeriesItems;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x0009577B File Offset: 0x0009397B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series labels visual settings")]
		[Category("Appearance")]
		public FunnelSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._funnelLabels == null)
				{
					this._funnelLabels = new FunnelSeriesLabelsAppearance("fla", base.ViewState);
				}
				return this._funnelLabels;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000957A1 File Offset: 0x000939A1
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x000957B8 File Offset: 0x000939B8
		[DefaultValue(null)]
		public decimal? SegmentSpacing
		{
			get
			{
				return (decimal?)base.ViewState["SegmentSpacing"];
			}
			set
			{
				base.ViewState["SegmentSpacing"] = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000957D0 File Offset: 0x000939D0
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x000957E8 File Offset: 0x000939E8
		[DefaultValue(null)]
		public decimal? NeckRatio
		{
			get
			{
				return (decimal?)base.ViewState["NeckRatio"];
			}
			set
			{
				if (value < 0.0m)
				{
					throw new Exception("You should specify a non-negative value for the NeckRatio property.");
				}
				base.ViewState["NeckRatio"] = value;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x0009583C File Offset: 0x00093A3C
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x0009585D File Offset: 0x00093A5D
		[DefaultValue(false)]
		public bool DynamicSlopeEnabled
		{
			get
			{
				return (bool)(base.ViewState["DynamicSlopeEnabled"] ?? false);
			}
			set
			{
				base.ViewState["DynamicSlopeEnabled"] = value;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x00095875 File Offset: 0x00093A75
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x00095896 File Offset: 0x00093A96
		[DefaultValue(true)]
		public bool DynamicHeightEnabled
		{
			get
			{
				return (bool)(base.ViewState["DynamicHeightEnabled"] ?? true);
			}
			set
			{
				base.ViewState["DynamicHeightEnabled"] = value;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000958AE File Offset: 0x00093AAE
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000958C5 File Offset: 0x00093AC5
		[DefaultValue("")]
		public string DataNameField
		{
			get
			{
				return (string)base.ViewState["DataNameField"];
			}
			set
			{
				base.ViewState["DataNameField"] = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000958D8 File Offset: 0x00093AD8
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000958EF File Offset: 0x00093AEF
		[DefaultValue("")]
		public string DataVisibleInLegendField
		{
			get
			{
				return (string)base.ViewState["DataVisibleInLegendField"];
			}
			set
			{
				base.ViewState["DataVisibleInLegendField"] = value;
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00095904 File Offset: 0x00093B04
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00095930 File Offset: 0x00093B30
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x0009595E File Offset: 0x00093B5E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x00095974 File Offset: 0x00093B74
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("{0},", base.Serialize());
			if (!this.IsDataBound)
			{
				this.SerializeSeriesItems(stringBuilder);
			}
			this.SerializeLabels(stringBuilder);
			this.SerializeSeriesSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000959DC File Offset: 0x00093BDC
		private void SerializeSeriesItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				sb.Append("data:[");
				foreach (object obj in this.SeriesItems)
				{
					FunnelSeriesItem funnelSeriesItem = (FunnelSeriesItem)obj;
					sb.Append("{");
					sb.Append(funnelSeriesItem.Serialize());
					sb.Append("},");
				}
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append("],");
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x00095A80 File Offset: 0x00093C80
		private void SerializeLabels(StringBuilder sb)
		{
			sb.AppendFormat("{0},", this.LabelsAppearance.Serialize());
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x00095A99 File Offset: 0x00093C99
		internal override void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
			this.SerializeSegmentSpacing(sb);
			this.SerializeDynamicSlope(sb);
			this.SerializeDynamicHeight(sb);
			this.SerializeNeckRatio(sb);
			this.SerializeDataNameField(sb);
			this.SerializeDataVisibleInLegendField(sb);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00095AC8 File Offset: 0x00093CC8
		private void SerializeSegmentSpacing(StringBuilder sb)
		{
			if (this.SegmentSpacing != null)
			{
				sb.AppendFormat("segmentSpacing:{0},", HtmlChartHelper.ToStringInvariant(this.SegmentSpacing));
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x00095AFC File Offset: 0x00093CFC
		private void SerializeDynamicSlope(StringBuilder sb)
		{
			if (this.DynamicSlopeEnabled)
			{
				sb.AppendFormat("dynamicSlope:{0},", HtmlChartHelper.SerializeBoolean(this.DynamicSlopeEnabled));
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00095B1D File Offset: 0x00093D1D
		private void SerializeDynamicHeight(StringBuilder sb)
		{
			if (!this.DynamicHeightEnabled)
			{
				sb.AppendFormat("dynamicHeight:{0},", HtmlChartHelper.SerializeBoolean(this.DynamicHeightEnabled));
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x00095B40 File Offset: 0x00093D40
		private void SerializeNeckRatio(StringBuilder sb)
		{
			if (this.NeckRatio != null)
			{
				sb.AppendFormat("neckRatio:{0},", HtmlChartHelper.ToStringInvariant(this.NeckRatio));
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x00095B74 File Offset: 0x00093D74
		private void SerializeDataNameField(StringBuilder sb)
		{
			if (!string.IsNullOrEmpty(this.DataNameField))
			{
				sb.AppendFormat("categoryField:'{0}',", this.DataNameField);
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00095B95 File Offset: 0x00093D95
		private void SerializeDataVisibleInLegendField(StringBuilder sb)
		{
			if (!string.IsNullOrEmpty(this.DataVisibleInLegendField))
			{
				sb.AppendFormat("visibleInLegendField:'{0}',", this.DataVisibleInLegendField);
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x00095D58 File Offset: 0x00093F58
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				FunnelSeriesItem item = (FunnelSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x00095D75 File Offset: 0x00093F75
		internal override SeriesItemBase GetSeriesItem()
		{
			return new FunnelSeriesItem();
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x00095D7C File Offset: 0x00093F7C
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00095D8C File Offset: 0x00093F8C
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			FunnelSeriesItem funnelSeriesItem = seriesItem as FunnelSeriesItem;
			if (funnelSeriesItem != null)
			{
				this.SeriesItems.Add(funnelSeriesItem);
			}
		}

		// Token: 0x04000C36 RID: 3126
		private FunnelSeriesItemCollection _funnelSeriesItems;

		// Token: 0x04000C37 RID: 3127
		private FunnelSeriesLabelsAppearance _funnelLabels;
	}
}
