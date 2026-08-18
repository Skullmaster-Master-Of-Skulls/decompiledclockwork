using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series
{
	// Token: 0x02000B8E RID: 2958
	public class PieSeriesBase : SeriesBase
	{
		// Token: 0x06006FAE RID: 28590 RVA: 0x001A176F File Offset: 0x0019F96F
		public PieSeriesBase()
		{
			this.sType = SeriesType.Pie;
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x001A1780 File Offset: 0x0019F980
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldY != string.Empty || this.NameField != string.Empty || this.ExplodeField != string.Empty || this.DataVisibleInLegendField != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06006FB0 RID: 28592 RVA: 0x001A1800 File Offset: 0x0019FA00
		// (set) Token: 0x06006FB1 RID: 28593 RVA: 0x001A1820 File Offset: 0x0019FA20
		[DefaultValue("")]
		public string ExplodeField
		{
			get
			{
				return (string)(base.ViewState["ExplodeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExplodeField"] = value;
			}
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x001A1833 File Offset: 0x0019FA33
		// (set) Token: 0x06006FB3 RID: 28595 RVA: 0x001A1853 File Offset: 0x0019FA53
		[DefaultValue("")]
		public string NameField
		{
			get
			{
				return (string)(base.ViewState["NameField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["NameField"] = value;
			}
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x001A1866 File Offset: 0x0019FA66
		// (set) Token: 0x06006FB5 RID: 28597 RVA: 0x001A1886 File Offset: 0x0019FA86
		[DefaultValue("")]
		public string DataVisibleInLegendField
		{
			get
			{
				return (string)(base.ViewState["DataVisibleInLegendField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataVisibleInLegendField"] = value;
			}
		}

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x001A1899 File Offset: 0x0019FA99
		// (set) Token: 0x06006FB7 RID: 28599 RVA: 0x001A18BB File Offset: 0x0019FABB
		public int StartAngle
		{
			get
			{
				return (int)(base.ViewState["StartAngle"] ?? 90);
			}
			set
			{
				base.ViewState["StartAngle"] = value;
			}
		}

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x06006FB8 RID: 28600 RVA: 0x001A18D3 File Offset: 0x0019FAD3
		// (set) Token: 0x06006FB9 RID: 28601 RVA: 0x001A18DB File Offset: 0x0019FADB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string AxisName
		{
			get
			{
				return base.AxisName;
			}
			set
			{
				base.AxisName = value;
			}
		}

		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06006FBA RID: 28602 RVA: 0x001A18E4 File Offset: 0x0019FAE4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PieSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._pieSeriesItems == null)
				{
					this._pieSeriesItems = new PieSeriesItemCollection();
				}
				return this._pieSeriesItems;
			}
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x06006FBB RID: 28603 RVA: 0x001A18FF File Offset: 0x0019FAFF
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

		// Token: 0x06006FBC RID: 28604 RVA: 0x001A191C File Offset: 0x0019FB1C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x001A1948 File Offset: 0x0019FB48
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x001A1976 File Offset: 0x0019FB76
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x001A198C File Offset: 0x0019FB8C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{").Append(base.Serialize());
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Append(",");
			}
			this.SerializeCommonProperties(stringBuilder);
			if (this.StartAngle != 90)
			{
				stringBuilder.Append(", startAngle: ").Append(this.StartAngle);
			}
			if (!this.IsDataBound)
			{
				this.AddSerializedItems(stringBuilder);
			}
			else
			{
				base.SerializeNonEmptyProperty(stringBuilder, ",explodeField", this.ExplodeField);
				base.SerializeNonEmptyProperty(stringBuilder, ",colorField", base.ColorField);
				base.SerializeNonEmptyProperty(stringBuilder, ",categoryField", this.NameField);
				base.SerializeNonEmptyProperty(stringBuilder, ",visibleInLegendField", this.DataVisibleInLegendField);
			}
			this.SerializeLabels(stringBuilder);
			this.SerializeBorders(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x001A1A65 File Offset: 0x0019FC65
		protected virtual void SerializeCommonProperties(StringBuilder sb)
		{
			sb.Append("type: '").Append(base.Type.ToString().ToLower()).Append("'");
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x001A1A97 File Offset: 0x0019FC97
		internal virtual void SerializeLabels(StringBuilder sb)
		{
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x001A1A9C File Offset: 0x0019FC9C
		internal virtual void SerializeBorders(StringBuilder sb)
		{
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(sb);
				sb.Append("border:").Append(value);
			}
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x001A1ADC File Offset: 0x0019FCDC
		protected void AddSerializedItems(StringBuilder sb)
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
				SeriesItem item = (SeriesItem)obj;
				this.AddSerializedItem(sb, item);
				sb.Append(",");
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x001A1B90 File Offset: 0x0019FD90
		protected void AddSerializedItem(StringBuilder sb, SeriesItem item)
		{
			sb.Append("{");
			sb.Append("value: ").Append((item.YValue != null) ? base.GetSerializedField(item.YValue.ToString()) : "null").Append(",");
			if (item.Name != string.Empty)
			{
				sb.Append("category: '").Append(item.Name).Append("',");
			}
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append("color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("',");
			}
			sb.Append("explode: ").Append(item.Exploded.ToString().ToLower());
			sb.Append("}");
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x001A1C90 File Offset: 0x0019FE90
		protected void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				PieSeriesItem item = (PieSeriesItem)obj;
				sb.Append("{");
				this.SerializePieItem(sb, item);
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x001A1D24 File Offset: 0x0019FF24
		protected void SerializePieItem(StringBuilder sb, PieSeriesItem item)
		{
			sb.Append("value: ").Append((item.Y != null) ? base.GetSerializedField(item.Y.ToString()) : "null").Append(",");
			if (item.Name != string.Empty)
			{
				sb.Append("category: '").Append(item.Name).Append("',");
			}
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append("color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("',");
			}
			if (item.Exploded)
			{
				sb.Append("explode: ").Append(item.Exploded.ToString().ToLower()).Append(",");
			}
			if (!item.Visible)
			{
				sb.Append("visible: false").Append(",");
			}
			if (!item.VisibleInLegend)
			{
				sb.Append("visibleInLegend: false");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x001A1E57 File Offset: 0x001A0057
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x001A1FFC File Offset: 0x001A01FC
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				PieSeriesItem item = (PieSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x001A2019 File Offset: 0x001A0219
		internal override SeriesItemBase GetSeriesItem()
		{
			return new PieSeriesItem();
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x001A2020 File Offset: 0x001A0220
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x001A2030 File Offset: 0x001A0230
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			PieSeriesItem pieSeriesItem = seriesItem as PieSeriesItem;
			if (pieSeriesItem != null)
			{
				this.SeriesItems.Add(pieSeriesItem);
			}
		}

		// Token: 0x04001E0D RID: 7693
		private PieSeriesItemCollection _pieSeriesItems;

		// Token: 0x04001E0E RID: 7694
		private SeriesBorderAppearance _borderAppearance;
	}
}
