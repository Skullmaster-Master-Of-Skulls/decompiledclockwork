using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI
{
	// Token: 0x020003F1 RID: 1009
	public class WaterfallSeries : SeriesBase, ISpacedSeries
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x0007B4A0 File Offset: 0x000796A0
		public WaterfallSeries()
		{
			this.sType = SeriesType.Waterfall;
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x0007B4B0 File Offset: 0x000796B0
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldY != string.Empty || this.DataSummaryField != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x0007B50C File Offset: 0x0007970C
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x0007B52C File Offset: 0x0007972C
		[DefaultValue("")]
		public string DataSummaryField
		{
			get
			{
				return (string)(base.ViewState["DataSummaryField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataSummaryField"] = value;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x0007B53F File Offset: 0x0007973F
		[Category("Appearance")]
		[Description("Series labels visual settings")]
		[DefaultValue("LabelsAppearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BarColumnSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new BarColumnSeriesLabelsAppearance("bla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x0007B565 File Offset: 0x00079765
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

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x0007B580 File Offset: 0x00079780
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WaterfallSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._waterfallSeriesItems == null)
				{
					this._waterfallSeriesItems = new WaterfallSeriesItemCollection();
				}
				return this._waterfallSeriesItems;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x0007B59B File Offset: 0x0007979B
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x0007B5B7 File Offset: 0x000797B7
		[DefaultValue(null)]
		public virtual double? Gap
		{
			get
			{
				return (double?)(base.ViewState["Gap"] ?? null);
			}
			set
			{
				base.ViewState["Gap"] = value;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0007B5CF File Offset: 0x000797CF
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x0007B5DB File Offset: 0x000797DB
		[Browsable(false)]
		[DefaultValue(null)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual double? Spacing
		{
			get
			{
				throw new Exception("The Spacing property is not supported by Waterfall series types.");
			}
			set
			{
				throw new Exception("The Spacing property is not supported by Waterfall series types.");
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0007B5E8 File Offset: 0x000797E8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0007B614 File Offset: 0x00079814
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0007B642 File Offset: 0x00079842
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0007B658 File Offset: 0x00079858
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.Append(base.Serialize());
			if (!this.IsDataBound)
			{
				this.AddSerializedItems(stringBuilder);
			}
			string text = this.LabelsAppearance.Serialize();
			if (text != string.Empty)
			{
				stringBuilder.Append(",").Append(text);
			}
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("border:").Append(value);
			}
			HtmlChartHelper.AddComma(stringBuilder);
			if (this.Gap != null)
			{
				stringBuilder.AppendFormat("gap:{0}", HtmlChartHelper.ToStringInvariant(this.Gap));
				HtmlChartHelper.AddComma(stringBuilder);
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0007B734 File Offset: 0x00079934
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0007B748 File Offset: 0x00079948
		protected void AddSerializedItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				this.AddSeriesItems(sb);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0007B760 File Offset: 0x00079960
		protected void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				WaterfallSeriesItem waterfallSeriesItem = (WaterfallSeriesItem)obj;
				sb.Append("{");
				if (waterfallSeriesItem.Summary == SummaryType.Default)
				{
					sb.Append("value: ").Append((waterfallSeriesItem.Y != null) ? base.GetSerializedField(waterfallSeriesItem.Y.ToString()) : "null");
				}
				else
				{
					sb.AppendFormat("summary:'{0}'", HtmlChartHelper.StringToLowerCamelCase(waterfallSeriesItem.Summary.ToString()));
				}
				if (waterfallSeriesItem.BackgroundColor.A != 0)
				{
					sb.Append(",");
					string value = HtmlChartHelper.ColorToHex(waterfallSeriesItem.BackgroundColor);
					base.SerializeNonEmptyProperty(sb, "color", value);
				}
				sb.Append("},");
			}
			if (sb.Length - 1 >= 0 && sb[sb.Length - 1] == ',')
			{
				sb.Remove(sb.Length - 1, 1);
			}
			sb.Append("]");
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0007BA5C File Offset: 0x00079C5C
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				WaterfallSeriesItem item = (WaterfallSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0007BA79 File Offset: 0x00079C79
		internal override SeriesItemBase GetSeriesItem()
		{
			return new WaterfallSeriesItem();
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0007BA80 File Offset: 0x00079C80
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0007BA90 File Offset: 0x00079C90
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			WaterfallSeriesItem waterfallSeriesItem = seriesItem as WaterfallSeriesItem;
			if (waterfallSeriesItem != null)
			{
				this.SeriesItems.Add(waterfallSeriesItem);
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0007BAB3 File Offset: 0x00079CB3
		internal override void SerializeDataboundFields(StringBuilder sb)
		{
			base.SerializeDataboundFields(sb);
			sb.Append(",summaryField: '").Append(this.DataSummaryField).Append("'");
		}

		// Token: 0x04000974 RID: 2420
		private BarColumnSeriesLabelsAppearance _labelsAppearance;

		// Token: 0x04000975 RID: 2421
		private SeriesBorderAppearance _borderAppearance;

		// Token: 0x04000976 RID: 2422
		private WaterfallSeriesItemCollection _waterfallSeriesItems;
	}
}
