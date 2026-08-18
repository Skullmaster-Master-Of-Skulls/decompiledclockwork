using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BF RID: 6079
	public class ShadowStyleChart : ShadowStyle
	{
		// Token: 0x0600EC95 RID: 60565 RVA: 0x0035F042 File Offset: 0x0035D242
		public ShadowStyleChart(Chart parent)
		{
			this.chart = parent;
		}

		// Token: 0x17004797 RID: 18327
		// (get) Token: 0x0600EC96 RID: 60566 RVA: 0x0035F051 File Offset: 0x0035D251
		// (set) Token: 0x0600EC97 RID: 60567 RVA: 0x0035F059 File Offset: 0x0035D259
		[NotifyParentProperty(true)]
		[DefaultValue(0f)]
		[SkinnableProperty]
		public override float Blur
		{
			get
			{
				return base.Blur;
			}
			set
			{
				base.Blur = value;
				this.SetShadowBlur(value);
			}
		}

		// Token: 0x17004798 RID: 18328
		// (get) Token: 0x0600EC98 RID: 60568 RVA: 0x0035F069 File Offset: 0x0035D269
		// (set) Token: 0x0600EC99 RID: 60569 RVA: 0x0035F071 File Offset: 0x0035D271
		[DefaultValue(typeof(Color), "0, 0, 0")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		public override Color Color
		{
			get
			{
				return base.Color;
			}
			set
			{
				base.Color = value;
				this.SetShadowColor(value);
			}
		}

		// Token: 0x17004799 RID: 18329
		// (get) Token: 0x0600EC9A RID: 60570 RVA: 0x0035F081 File Offset: 0x0035D281
		// (set) Token: 0x0600EC9B RID: 60571 RVA: 0x0035F089 File Offset: 0x0035D289
		[NotifyParentProperty(true)]
		[DefaultValue(0f)]
		[SkinnableProperty]
		public override float Distance
		{
			get
			{
				return base.Distance;
			}
			set
			{
				base.Distance = value;
				this.SetShadowDistance(value);
			}
		}

		// Token: 0x1700479A RID: 18330
		// (get) Token: 0x0600EC9C RID: 60572 RVA: 0x0035F099 File Offset: 0x0035D299
		// (set) Token: 0x0600EC9D RID: 60573 RVA: 0x0035F0A1 File Offset: 0x0035D2A1
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(ShadowPosition), "BottomRight")]
		public override ShadowPosition Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				base.Position = value;
				this.SetShadowPosition(value);
			}
		}

		// Token: 0x0600EC9E RID: 60574 RVA: 0x0035F0B4 File Offset: 0x0035D2B4
		internal void SetShadowBlur(float blur)
		{
			if (this.chart != null && blur >= 0f)
			{
				this.chart.ChartTitle.Appearance.Shadow.Blur = blur;
				foreach (ChartSeries chartSeries in this.chart.Series)
				{
					chartSeries.Appearance.Shadow.Blur = blur;
					chartSeries.Appearance.PointMark.Shadow.Blur = blur;
					chartSeries.Appearance.LabelAppearance.Shadow.Blur = blur;
				}
				this.chart.Legend.Appearance.Shadow.Blur = blur;
				this.chart.PlotArea.Appearance.Shadow.Blur = blur;
			}
		}

		// Token: 0x0600EC9F RID: 60575 RVA: 0x0035F1A4 File Offset: 0x0035D3A4
		internal void SetShadowPosition(ShadowPosition position)
		{
			if (this.chart != null)
			{
				this.chart.ChartTitle.Appearance.Shadow.Position = position;
				foreach (ChartSeries chartSeries in this.chart.Series)
				{
					chartSeries.Appearance.Shadow.Position = position;
					chartSeries.Appearance.PointMark.Shadow.Position = position;
					chartSeries.Appearance.LabelAppearance.Shadow.Position = position;
				}
				this.chart.Legend.Appearance.Shadow.Position = position;
				this.chart.PlotArea.Appearance.Shadow.Position = position;
			}
		}

		// Token: 0x0600ECA0 RID: 60576 RVA: 0x0035F288 File Offset: 0x0035D488
		internal void SetShadowDistance(float distance)
		{
			if (this.chart != null)
			{
				this.chart.ChartTitle.Appearance.Shadow.Distance = distance;
				foreach (ChartSeries chartSeries in this.chart.Series)
				{
					chartSeries.Appearance.Shadow.Distance = distance;
					chartSeries.Appearance.PointMark.Shadow.Distance = distance;
					chartSeries.Appearance.LabelAppearance.Shadow.Distance = distance;
				}
				this.chart.Legend.Appearance.Shadow.Distance = distance;
				this.chart.PlotArea.Appearance.Shadow.Distance = distance;
			}
		}

		// Token: 0x0600ECA1 RID: 60577 RVA: 0x0035F36C File Offset: 0x0035D56C
		internal void SetShadowColor(Color color)
		{
			if (this.chart != null)
			{
				this.chart.ChartTitle.Appearance.Shadow.Color = color;
				foreach (ChartSeries chartSeries in this.chart.Series)
				{
					chartSeries.Appearance.Shadow.Color = color;
					chartSeries.Appearance.PointMark.Shadow.Color = color;
					chartSeries.Appearance.LabelAppearance.Shadow.Color = color;
				}
				this.chart.Legend.Appearance.Shadow.Color = color;
				this.chart.PlotArea.Appearance.Shadow.Color = color;
			}
		}

		// Token: 0x04004432 RID: 17458
		internal Chart chart;
	}
}
