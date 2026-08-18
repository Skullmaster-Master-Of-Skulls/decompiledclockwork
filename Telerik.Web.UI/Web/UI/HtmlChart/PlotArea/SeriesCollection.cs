using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B94 RID: 2964
	[ParseChildren(typeof(SeriesBase))]
	public class SeriesCollection : StronglyTypedStateManagedCollection<SeriesBase>
	{
		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x001A2792 File Offset: 0x001A0992
		// (set) Token: 0x06006FED RID: 28653 RVA: 0x001A279A File Offset: 0x001A099A
		internal bool IsDataBound { get; set; }

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x001A27A3 File Offset: 0x001A09A3
		// (set) Token: 0x06006FEF RID: 28655 RVA: 0x001A27AB File Offset: 0x001A09AB
		internal bool HasFunnelSeries { get; set; }

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x001A27B4 File Offset: 0x001A09B4
		// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x001A27BC File Offset: 0x001A09BC
		internal bool HasPieSeries { get; set; }

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x001A27C5 File Offset: 0x001A09C5
		// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x001A27CD File Offset: 0x001A09CD
		internal bool HasNumericSeries { get; set; }

		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x001A27D6 File Offset: 0x001A09D6
		// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x001A27DE File Offset: 0x001A09DE
		internal bool HasRadarSeries { get; set; }

		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x001A27E7 File Offset: 0x001A09E7
		// (set) Token: 0x06006FF7 RID: 28663 RVA: 0x001A27EF File Offset: 0x001A09EF
		internal bool HasPolarSeries { get; set; }

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06006FF8 RID: 28664 RVA: 0x001A27F8 File Offset: 0x001A09F8
		// (remove) Token: 0x06006FF9 RID: 28665 RVA: 0x001A2830 File Offset: 0x001A0A30
		internal event EventHandler CollectionChanged;

		// Token: 0x06006FFA RID: 28666 RVA: 0x001A292C File Offset: 0x001A0B2C
		public new virtual void Add(SeriesBase series)
		{
			base.Add(series);
			if (!this.IsDataBound)
			{
				this.IsDataBound = series.IsDataBound;
			}
			this.RegisterSeries(() => this.HasPieSeries, delegate
			{
				this.HasPieSeries = (series is PieSeriesBase);
			});
			this.RegisterSeries(() => this.HasPolarSeries, delegate
			{
				this.HasPolarSeries = (series is PolarSeriesBase);
			});
			this.RegisterSeries(() => this.HasNumericSeries, delegate
			{
				this.HasNumericSeries = (series is ScatterAndBubbleSeriesBase);
			});
			this.RegisterSeries(() => this.HasFunnelSeries, delegate
			{
				this.HasFunnelSeries = (series is FunnelSeries);
			});
			this.RegisterSeries(() => this.HasRadarSeries, delegate
			{
				this.HasRadarSeries = series.GetType().IsDefined(typeof(RadarSeriesMarkerAttribute), true);
			});
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new EventArgs());
			}
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x001A2A24 File Offset: 0x001A0C24
		public new virtual void AddRange(IEnumerable<SeriesBase> seriesCollection)
		{
			foreach (SeriesBase series in seriesCollection)
			{
				this.Add(series);
			}
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x001A2A6C File Offset: 0x001A0C6C
		protected virtual void RegisterSeries(Func<bool> typeRegistered, Action registerType)
		{
			if (!typeRegistered())
			{
				registerType();
			}
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x001A2A7C File Offset: 0x001A0C7C
		protected override void SetDirtyObject(object o)
		{
			if (o is SeriesBase)
			{
				((StateManager)o).SetDirty();
			}
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x001A2A94 File Offset: 0x001A0C94
		public void UpdateIsDataBound()
		{
			foreach (object obj in base.List)
			{
				SeriesBase seriesBase = (SeriesBase)obj;
				if (seriesBase.IsDataBound)
				{
					this.IsDataBound = true;
					break;
				}
			}
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x001A2AF8 File Offset: 0x001A0CF8
		internal string Serialize()
		{
			if (base.List.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (object obj in base.List)
			{
				SeriesBase seriesBase = (SeriesBase)obj;
				string text = seriesBase.Serialize();
				if (seriesBase.IsDataBound)
				{
					this.IsDataBound = true;
				}
				if (text != string.Empty)
				{
					stringBuilder.Append(text).Append(",");
				}
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
