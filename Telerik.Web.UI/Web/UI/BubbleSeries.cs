using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000B8D RID: 2957
	public class BubbleSeries : ScatterAndBubbleSeriesBase
	{
		// Token: 0x06006F97 RID: 28567 RVA: 0x001A0EA0 File Offset: 0x0019F0A0
		public BubbleSeries()
		{
			this.sType = SeriesType.Bubble;
			base.LabelsAppearance.Visible = new bool?(false);
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x06006F98 RID: 28568 RVA: 0x001A0EC0 File Offset: 0x0019F0C0
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldX != string.Empty || base.DataFieldY != string.Empty || this.DataFieldSize != string.Empty || this.DataFieldTooltip != string.Empty);
			}
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x001A0F2E File Offset: 0x0019F12E
		// (set) Token: 0x06006F9A RID: 28570 RVA: 0x001A0F4F File Offset: 0x0019F14F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(MissingValuesBehavior.Gap)]
		[Bindable(false)]
		public override MissingValuesBehavior MissingValues
		{
			get
			{
				return (MissingValuesBehavior)(base.ViewState["MissingValues"] ?? MissingValuesBehavior.Gap);
			}
			set
			{
				base.ViewState["MissingValues"] = value;
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x06006F9B RID: 28571 RVA: 0x001A0F67 File Offset: 0x0019F167
		// (set) Token: 0x06006F9C RID: 28572 RVA: 0x001A0F87 File Offset: 0x0019F187
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public new string DataFieldSize
		{
			get
			{
				return (string)(base.ViewState["DataFieldSize"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldSize"] = value;
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06006F9D RID: 28573 RVA: 0x001A0F9A File Offset: 0x0019F19A
		// (set) Token: 0x06006F9E RID: 28574 RVA: 0x001A0FBA File Offset: 0x0019F1BA
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Browsable(true)]
		public new string DataFieldTooltip
		{
			get
			{
				return (string)(base.ViewState["DataFieldTooltip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldTooltip"] = value;
			}
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06006F9F RID: 28575 RVA: 0x001A0FCD File Offset: 0x0019F1CD
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

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x001A0FE8 File Offset: 0x0019F1E8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BubbleSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._bubbleSeriesItems == null)
				{
					this._bubbleSeriesItems = new BubbleSeriesItemCollection();
				}
				return this._bubbleSeriesItems;
			}
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x001A1004 File Offset: 0x0019F204
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x001A1030 File Offset: 0x0019F230
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x001A105E File Offset: 0x0019F25E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x001A1074 File Offset: 0x0019F274
		internal override void SerializeDataboundFields(StringBuilder sb)
		{
			base.SerializeDataboundFields(sb);
			sb.Append(",");
			sb.Append("sizeField: '").Append(this.DataFieldSize).Append("'");
			if (!string.IsNullOrEmpty(this.DataFieldTooltip))
			{
				sb.Append(",categoryField: '").Append(this.DataFieldTooltip).Append("'");
			}
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x001A10E4 File Offset: 0x0019F2E4
		internal override string Serialize()
		{
			string text = base.Serialize();
			if (!this.BorderAppearance.IsDefault)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("border:").Append(value);
				text = stringBuilder.ToString();
			}
			return string.Format("{{{0}}}", text);
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x001A1144 File Offset: 0x0019F344
		protected override void SerializeItem(StringBuilder sb, SeriesItem item)
		{
			base.SerializeItem(sb, item);
			sb.Append(",");
			sb.Append((item.SizeValue != null) ? base.GetSerializedField(item.SizeValue.ToString()) : "null");
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x001A11A0 File Offset: 0x0019F3A0
		internal override void AddSerializedItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				this.AddSeriesItems(sb);
				return;
			}
			if (base.Items.Count == 0)
			{
				return;
			}
			sb.Append(", data: [");
			foreach (object obj in base.Items)
			{
				SeriesItem seriesItem = (SeriesItem)obj;
				sb.Append("{");
				sb.Append("x: ");
				sb.Append((seriesItem.XValue != null) ? base.GetSerializedField(seriesItem.XValue.ToString()) : "null");
				sb.Append(", ");
				sb.Append("y: ");
				sb.Append((seriesItem.YValue != null) ? base.GetSerializedField(seriesItem.YValue.ToString()) : "null");
				sb.Append(", ");
				sb.Append("size: ");
				sb.Append((seriesItem.SizeValue != null) ? base.GetSerializedField(seriesItem.SizeValue.ToString()) : "null");
				if (!string.IsNullOrEmpty(seriesItem.TooltipValue))
				{
					sb.Append(", ");
					sb.Append("category: ");
					sb.Append("'" + seriesItem.TooltipValue + "'");
				}
				sb.Append("},");
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x001A1394 File Offset: 0x0019F594
		protected override void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				BubbleSeriesItem item = (BubbleSeriesItem)obj;
				sb.Append("{");
				this.SerializeBubbleItem(sb, item);
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06006FA9 RID: 28585 RVA: 0x001A1428 File Offset: 0x0019F628
		private void SerializeBubbleItem(StringBuilder sb, BubbleSeriesItem item)
		{
			if (item.X != null)
			{
				sb.Append("x: ").Append(base.GetSerializedField(item.X.ToString())).Append(",");
			}
			if (item.Y != null)
			{
				sb.Append("y: ").Append(base.GetSerializedField(item.Y.ToString())).Append(",");
			}
			if (item.Size != null)
			{
				sb.Append("size: ").Append(base.GetSerializedField(item.Size.ToString())).Append(",");
			}
			if (item.Tooltip != string.Empty)
			{
				sb.Append("category: '").Append(item.Tooltip).Append("',");
			}
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append("color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("',");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x001A1718 File Offset: 0x0019F918
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				BubbleSeriesItem item = (BubbleSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06006FAB RID: 28587 RVA: 0x001A1735 File Offset: 0x0019F935
		internal override SeriesItemBase GetSeriesItem()
		{
			return new BubbleSeriesItem();
		}

		// Token: 0x06006FAC RID: 28588 RVA: 0x001A173C File Offset: 0x0019F93C
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x001A174C File Offset: 0x0019F94C
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			BubbleSeriesItem bubbleSeriesItem = seriesItem as BubbleSeriesItem;
			if (bubbleSeriesItem != null)
			{
				this.SeriesItems.Add(bubbleSeriesItem);
			}
		}

		// Token: 0x04001E0B RID: 7691
		private SeriesBorderAppearance _borderAppearance;

		// Token: 0x04001E0C RID: 7692
		private BubbleSeriesItemCollection _bubbleSeriesItems;
	}
}
