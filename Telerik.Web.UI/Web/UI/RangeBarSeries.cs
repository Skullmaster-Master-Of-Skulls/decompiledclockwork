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
	// Token: 0x020003F3 RID: 1011
	public class RangeBarSeries : SeriesBase, ISpacedSeries
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x0007BAED File Offset: 0x00079CED
		public RangeBarSeries()
		{
			this.sType = SeriesType.RangeBar;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0007BAFD File Offset: 0x00079CFD
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x0007BB09 File Offset: 0x00079D09
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		[Browsable(false)]
		public new string DataFieldY
		{
			get
			{
				throw new Exception("The DataFieldY property is not supported by Polar series types. Use the DataRadiusField property instead.");
			}
			set
			{
				throw new Exception("The DataFieldY property is not supported by Polar series types. Use the DataRadiusField property instead.");
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x0007BB15 File Offset: 0x00079D15
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x0007BB21 File Offset: 0x00079D21
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string DataFieldX
		{
			get
			{
				throw new Exception("The DataFieldX property is not supported by Polar series types. Use the DataAngleField property instead.");
			}
			set
			{
				throw new Exception("The DataFieldX property is not supported by Polar series types. Use the DataAngleField property instead.");
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x0007BB2D File Offset: 0x00079D2D
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x0007BB4D File Offset: 0x00079D4D
		[DefaultValue("")]
		public string DataFromField
		{
			get
			{
				return (string)(base.ViewState["DataFromField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFromField"] = value;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0007BB60 File Offset: 0x00079D60
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x0007BB80 File Offset: 0x00079D80
		[DefaultValue("")]
		public string DataToField
		{
			get
			{
				return (string)(base.ViewState["DataToField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataToField"] = value;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x0007BB93 File Offset: 0x00079D93
		[Description("Series labels visual settings")]
		[DefaultValue("LabelsAppearance")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RangeSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new RangeSeriesLabelsAppearance("rsla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0007BBB9 File Offset: 0x00079DB9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RangeSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._rangeSeriesItems == null)
				{
					this._rangeSeriesItems = new RangeSeriesItemCollection();
				}
				return this._rangeSeriesItems;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0007BBD4 File Offset: 0x00079DD4
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x0007BBF0 File Offset: 0x00079DF0
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

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0007BC08 File Offset: 0x00079E08
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x0007BC24 File Offset: 0x00079E24
		[DefaultValue(null)]
		public virtual double? Spacing
		{
			get
			{
				return (double?)(base.ViewState["Spacing"] ?? null);
			}
			set
			{
				base.ViewState["Spacing"] = value;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0007BC3C File Offset: 0x00079E3C
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

		// Token: 0x06002532 RID: 9522 RVA: 0x0007BC58 File Offset: 0x00079E58
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0007BC84 File Offset: 0x00079E84
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0007BCB2 File Offset: 0x00079EB2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0007BCC8 File Offset: 0x00079EC8
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
			if (this.Spacing != null)
			{
				stringBuilder.AppendFormat("spacing:{0}", HtmlChartHelper.ToStringInvariant(this.Spacing));
				HtmlChartHelper.AddComma(stringBuilder);
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0007BDD2 File Offset: 0x00079FD2
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0007BDE6 File Offset: 0x00079FE6
		protected void AddSerializedItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				this.AddSeriesItems(sb);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0007BE00 File Offset: 0x0007A000
		protected void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				RangeSeriesItem rangeSeriesItem = (RangeSeriesItem)obj;
				if (rangeSeriesItem.From != null)
				{
					sb.Append("{");
					sb.Append("from: ").Append(base.GetSerializedField(rangeSeriesItem.From.ToString()));
					if (rangeSeriesItem.To != null)
					{
						sb.Append(",");
						sb.Append("to: ").Append(base.GetSerializedField(rangeSeriesItem.To.ToString()));
					}
					if (rangeSeriesItem.BackgroundColor.A != 0)
					{
						sb.Append(",");
						string value = HtmlChartHelper.ColorToHex(rangeSeriesItem.BackgroundColor);
						base.SerializeNonEmptyProperty(sb, "color", value);
					}
					HtmlChartHelper.RemoveEndingComma(sb);
					sb.Append("},");
				}
			}
			if (sb.Length - 1 >= 0 && sb[sb.Length - 1] == ',')
			{
				sb.Remove(sb.Length - 1, 1);
			}
			sb.Append("]");
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x0007BF80 File Offset: 0x0007A180
		internal override bool IsDataBound
		{
			get
			{
				return this.SeriesItems.Count == 0 && (this.DataFromField != string.Empty || this.DataToField != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0007BFD4 File Offset: 0x0007A1D4
		internal override void SerializeDataboundFields(StringBuilder sb)
		{
			sb.Append("fromField: '").Append(this.DataFromField).Append("'");
			sb.Append(",");
			sb.Append("toField: '").Append(this.DataToField).Append("'");
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0007C1D0 File Offset: 0x0007A3D0
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				RangeSeriesItem item = (RangeSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x0007C1ED File Offset: 0x0007A3ED
		internal override SeriesItemBase GetSeriesItem()
		{
			return new RangeSeriesItem();
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0007C1F4 File Offset: 0x0007A3F4
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0007C204 File Offset: 0x0007A404
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			RangeSeriesItem rangeSeriesItem = seriesItem as RangeSeriesItem;
			if (rangeSeriesItem != null)
			{
				this.SeriesItems.Add(rangeSeriesItem);
			}
		}

		// Token: 0x04000977 RID: 2423
		private RangeSeriesLabelsAppearance _labelsAppearance;

		// Token: 0x04000978 RID: 2424
		private RangeSeriesItemCollection _rangeSeriesItems;

		// Token: 0x04000979 RID: 2425
		private SeriesBorderAppearance _borderAppearance;
	}
}
