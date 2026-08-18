using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020004FA RID: 1274
	public class CandlestickSeries : SeriesBase
	{
		// Token: 0x06002D68 RID: 11624 RVA: 0x00094F2E File Offset: 0x0009312E
		public CandlestickSeries()
		{
			this.sType = SeriesType.Candlestick;
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x00094F40 File Offset: 0x00093140
		internal override bool IsDataBound
		{
			get
			{
				return this.SeriesItems.Count == 0 && (this.DataOpenField != string.Empty || this.DataCloseField != string.Empty || this.DataHighField != string.Empty || this.DataLowField != string.Empty || this.DataDownColorField != string.Empty);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x00094FB6 File Offset: 0x000931B6
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x00094FD6 File Offset: 0x000931D6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new string DataFieldY
		{
			get
			{
				return (string)(base.ViewState["DataFieldY"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldY"] = value;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x00094FE9 File Offset: 0x000931E9
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x00095009 File Offset: 0x00093209
		[DefaultValue("")]
		public string DataOpenField
		{
			get
			{
				return (string)(base.ViewState["DataOpenField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataOpenField"] = value;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0009501C File Offset: 0x0009321C
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x0009503C File Offset: 0x0009323C
		[DefaultValue("")]
		public string DataCloseField
		{
			get
			{
				return (string)(base.ViewState["DataCloseField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataCloseField"] = value;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0009504F File Offset: 0x0009324F
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x0009506F File Offset: 0x0009326F
		[DefaultValue("")]
		public string DataHighField
		{
			get
			{
				return (string)(base.ViewState["DataHighField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataHighField"] = value;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x00095082 File Offset: 0x00093282
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000950A2 File Offset: 0x000932A2
		[DefaultValue("")]
		public string DataLowField
		{
			get
			{
				return (string)(base.ViewState["DataLowField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataLowField"] = value;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000950B5 File Offset: 0x000932B5
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000950D5 File Offset: 0x000932D5
		[DefaultValue("")]
		public string DataDownColorField
		{
			get
			{
				return (string)(base.ViewState["DataDownColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataDownColorField"] = value;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000950E8 File Offset: 0x000932E8
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x0009510D File Offset: 0x0009330D
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

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x00095125 File Offset: 0x00093325
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CandlestickSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._candleStickSeriesItems == null)
				{
					this._candleStickSeriesItems = new CandlestickSeriesItemCollection();
				}
				return this._candleStickSeriesItems;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00095140 File Offset: 0x00093340
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
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

		// Token: 0x06002D7A RID: 11642 RVA: 0x0009515C File Offset: 0x0009335C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00095188 File Offset: 0x00093388
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000951B6 File Offset: 0x000933B6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000951CC File Offset: 0x000933CC
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{").Append(base.Serialize());
			if (this.DownColor != Color.Empty)
			{
				stringBuilder.Append(",downColor: '").Append(HtmlChartHelper.ColorToHex(this.DownColor)).Append("'");
			}
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("border:").Append(value);
			}
			if (!this.IsDataBound)
			{
				this.AddSerializedItems(stringBuilder);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00095279 File Offset: 0x00093479
		private void AddSerializedItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				this.AddSeriesItems(sb);
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00095290 File Offset: 0x00093490
		private void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				CandlestickSeriesItem item = (CandlestickSeriesItem)obj;
				sb.Append("{");
				this.SerializeCandleStickSeriesItem(sb, item);
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00095324 File Offset: 0x00093524
		private void SerializeCandleStickSeriesItem(StringBuilder sb, CandlestickSeriesItem item)
		{
			if (item.Open != null)
			{
				this.SerializeItemProperty(sb, "open: ", item.Open);
			}
			if (item.Close != null)
			{
				this.SerializeItemProperty(sb, "close: ", item.Close);
			}
			if (item.High != null)
			{
				this.SerializeItemProperty(sb, "high: ", item.High);
			}
			if (item.Low != null)
			{
				this.SerializeItemProperty(sb, "low: ", item.Low);
			}
			if (item.DownColor != Color.Empty)
			{
				sb.Append("downColor: '").Append(HtmlChartHelper.ColorToHex(item.DownColor)).Append("',");
			}
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append("color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("',");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00095430 File Offset: 0x00093630
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00095444 File Offset: 0x00093644
		internal override void SerializeDataboundFields(StringBuilder sb)
		{
			sb.Append("openField: '").Append(this.DataOpenField).Append("',");
			sb.Append("closeField: '").Append(this.DataCloseField).Append("',");
			sb.Append("highField: '").Append(this.DataHighField).Append("',");
			sb.Append("lowField: '").Append(this.DataLowField).Append("',");
			if (!string.IsNullOrEmpty(this.DataDownColorField))
			{
				sb.Append("downColorField: '").Append(this.DataDownColorField).Append("',");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0009550A File Offset: 0x0009370A
		public void SerializeItemProperty(StringBuilder sb, string property, decimal? value)
		{
			sb.Append(property).Append(base.GetSerializedField(value.ToString())).Append(",");
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000956D8 File Offset: 0x000938D8
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				CandlestickSeriesItem item = (CandlestickSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000956F5 File Offset: 0x000938F5
		internal override SeriesItemBase GetSeriesItem()
		{
			return new CandlestickSeriesItem();
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000956FC File Offset: 0x000938FC
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0009570C File Offset: 0x0009390C
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			CandlestickSeriesItem candlestickSeriesItem = seriesItem as CandlestickSeriesItem;
			if (candlestickSeriesItem != null)
			{
				this.SeriesItems.Add(candlestickSeriesItem);
			}
		}

		// Token: 0x04000C34 RID: 3124
		private CandlestickSeriesItemCollection _candleStickSeriesItems;

		// Token: 0x04000C35 RID: 3125
		private SeriesBorderAppearance _borderAppearance;
	}
}
