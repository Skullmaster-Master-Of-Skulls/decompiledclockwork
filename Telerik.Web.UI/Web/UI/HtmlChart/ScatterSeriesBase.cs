using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B91 RID: 2961
	public class ScatterSeriesBase : ScatterAndBubbleSeriesBase
	{
		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x001A219F File Offset: 0x001A039F
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldX != string.Empty || base.DataFieldY != string.Empty);
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06006FD6 RID: 28630 RVA: 0x001A21DE File Offset: 0x001A03DE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ScatterSeriesItemCollection SeriesItems
		{
			get
			{
				if (this._scatterSeriesItems == null)
				{
					this._scatterSeriesItems = new ScatterSeriesItemCollection();
				}
				return this._scatterSeriesItems;
			}
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x001A21FC File Offset: 0x001A03FC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x001A2228 File Offset: 0x001A0428
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x001A2256 File Offset: 0x001A0456
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x001A226C File Offset: 0x001A046C
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

		// Token: 0x06006FDB RID: 28635 RVA: 0x001A232C File Offset: 0x001A052C
		protected override void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				ScatterSeriesItem item = (ScatterSeriesItem)obj;
				sb.Append("{");
				this.SerializeScatterItem(sb, item);
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x001A23C0 File Offset: 0x001A05C0
		private void SerializeScatterItem(StringBuilder sb, ScatterSeriesItem item)
		{
			sb.Append("x: ").Append((item.X != null) ? base.GetSerializedField(item.X.ToString()) : "null").Append(",");
			sb.Append("y: ").Append((item.Y != null) ? base.GetSerializedField(item.Y.ToString()) : "null");
			if (item.BackgroundColor != Color.Empty)
			{
				sb.Append(",color: '").Append(HtmlChartHelper.ColorToHex(item.BackgroundColor)).Append("'");
			}
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x001A2634 File Offset: 0x001A0834
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				ScatterSeriesItem item = (ScatterSeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x001A2651 File Offset: 0x001A0851
		internal override SeriesItemBase GetSeriesItem()
		{
			return new ScatterSeriesItem();
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x001A2658 File Offset: 0x001A0858
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x001A2668 File Offset: 0x001A0868
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			ScatterSeriesItem scatterSeriesItem = seriesItem as ScatterSeriesItem;
			if (scatterSeriesItem != null)
			{
				this.SeriesItems.Add(scatterSeriesItem);
			}
		}

		// Token: 0x04001E11 RID: 7697
		private ScatterSeriesItemCollection _scatterSeriesItems;
	}
}
