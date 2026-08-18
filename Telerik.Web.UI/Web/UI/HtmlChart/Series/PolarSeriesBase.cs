using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.SeriesItemCollections;

namespace Telerik.Web.UI.HtmlChart.Series
{
	// Token: 0x020004FF RID: 1279
	public class PolarSeriesBase : ScatterAndBubbleSeriesBase
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x0009606F File Offset: 0x0009426F
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x0009607B File Offset: 0x0009427B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AxisName
		{
			get
			{
				throw new Exception("AxisName property is not valid for Radar and Polar series types.");
			}
			set
			{
				throw new Exception("AxisName property is not valid for Radar and Polar series types.");
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x00096087 File Offset: 0x00094287
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PolarSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._seriesItems == null)
				{
					this._seriesItems = new PolarSeriesItemCollection();
				}
				return this._seriesItems;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000960A4 File Offset: 0x000942A4
		[DefaultValue("MarkerssAppearance")]
		[Category("Appearance")]
		[Description("Series markers visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public override MarkersAppearance MarkersAppearance
		{
			get
			{
				if (this._markersAppearance == null)
				{
					this._markersAppearance = new MarkersAppearance("ma", base.ViewState)
					{
						Visible = new bool?(true)
					};
				}
				return this._markersAppearance;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000960E3 File Offset: 0x000942E3
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x000960EB File Offset: 0x000942EB
		[DefaultValue("")]
		public string DataRadiusField
		{
			get
			{
				return base.DataFieldY;
			}
			set
			{
				base.DataFieldY = value;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000960F4 File Offset: 0x000942F4
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x000960FC File Offset: 0x000942FC
		[DefaultValue("")]
		public string DataAngleField
		{
			get
			{
				return base.DataFieldX;
			}
			set
			{
				base.DataFieldX = value;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x00096105 File Offset: 0x00094305
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x00096111 File Offset: 0x00094311
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x0009611D File Offset: 0x0009431D
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x00096129 File Offset: 0x00094329
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x00096135 File Offset: 0x00094335
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (this.DataRadiusField != string.Empty || this.DataAngleField != string.Empty);
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00096174 File Offset: 0x00094374
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000961A0 File Offset: 0x000943A0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000961CE File Offset: 0x000943CE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000961E4 File Offset: 0x000943E4
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
				SeriesItem item = (SeriesItem)obj;
				sb.Append("[");
				this.SerializeItem(sb, item);
				sb.Append("],");
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000962A4 File Offset: 0x000944A4
		protected override void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				PolarSeriesItem item = (PolarSeriesItem)obj;
				sb.Append("{");
				this.SerializeItem(sb, item);
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x00096338 File Offset: 0x00094538
		private void SerializeItem(StringBuilder sb, PolarSeriesItem item)
		{
			sb.Append("x: ").Append((item.Angle != null) ? base.GetSerializedField(item.Angle.ToString()) : "null").Append(",");
			sb.Append("y: ").Append((item.Radius != null) ? base.GetSerializedField(item.Radius.ToString()) : "null");
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append(",color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("'");
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0009640B File Offset: 0x0009460B
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0009640D File Offset: 0x0009460D
		internal override SeriesItemBase GetSeriesItem()
		{
			return new PolarSeriesItem();
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00096414 File Offset: 0x00094614
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			PolarSeriesItem polarSeriesItem = seriesItem as PolarSeriesItem;
			if (polarSeriesItem != null)
			{
				this.SeriesItems.Add(polarSeriesItem);
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000965D8 File Offset: 0x000947D8
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				PolarSeriesItem item = (PolarSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000965F5 File Offset: 0x000947F5
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x04000C3B RID: 3131
		private PolarSeriesItemCollection _seriesItems;

		// Token: 0x04000C3C RID: 3132
		private MarkersAppearance _markersAppearance;
	}
}
