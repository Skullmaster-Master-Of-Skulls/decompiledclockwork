using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178B RID: 6027
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FillStyleSeries : FillStyle
	{
		// Token: 0x0600EB1A RID: 60186 RVA: 0x00358F10 File Offset: 0x00357110
		public FillStyleSeries()
		{
		}

		// Token: 0x0600EB1B RID: 60187 RVA: 0x00358F18 File Offset: 0x00357118
		public FillStyleSeries(ChartSeries series) : base(series)
		{
			this.fillStyleContainerObject = series;
		}

		// Token: 0x17004735 RID: 18229
		// (get) Token: 0x0600EB1C RID: 60188 RVA: 0x00358F28 File Offset: 0x00357128
		// (set) Token: 0x0600EB1D RID: 60189 RVA: 0x00358FD8 File Offset: 0x003571D8
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Main color")]
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public override Color MainColor
		{
			get
			{
				if (base.ViewState["MainColor"] != null)
				{
					return (Color)base.ViewState["MainColor"];
				}
				ChartSeries chartSeries = this.fillStyleContainerObject as ChartSeries;
				if (chartSeries != null && chartSeries.Chart != null && !chartSeries.Chart.DesignTime && !string.IsNullOrEmpty(chartSeries.Chart.SeriesPaletteWrapper))
				{
					Palette palette = PalettesCollection.GetPalette(chartSeries.Chart.SeriesPaletteWrapper);
					if (palette == null)
					{
						palette = PalettesCollection.GetPalette(chartSeries.Chart.SeriesPaletteWrapper, chartSeries.Chart);
					}
					if (palette != null)
					{
						return palette.GetPaletteItem(chartSeries.Index).MainColor;
					}
				}
				return Color.Empty;
			}
			set
			{
				base.MainColor = value;
			}
		}

		// Token: 0x17004736 RID: 18230
		// (get) Token: 0x0600EB1E RID: 60190 RVA: 0x00358FE4 File Offset: 0x003571E4
		// (set) Token: 0x0600EB1F RID: 60191 RVA: 0x00359094 File Offset: 0x00357294
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "")]
		[SkinnableProperty]
		[Description("Second color")]
		[TypeConverter(typeof(ColorConverter))]
		public override Color SecondColor
		{
			get
			{
				if (base.ViewState["SecondColor"] != null)
				{
					return (Color)base.ViewState["SecondColor"];
				}
				ChartSeries chartSeries = this.fillStyleContainerObject as ChartSeries;
				if (chartSeries != null && chartSeries.Chart != null && !chartSeries.Chart.DesignTime && !string.IsNullOrEmpty(chartSeries.Chart.SeriesPaletteWrapper))
				{
					Palette palette = PalettesCollection.GetPalette(chartSeries.Chart.SeriesPaletteWrapper);
					if (palette == null)
					{
						palette = PalettesCollection.GetPalette(chartSeries.Chart.SeriesPaletteWrapper, chartSeries.Chart);
					}
					if (palette != null)
					{
						return palette.GetPaletteItem(chartSeries.Index).SecondColor;
					}
				}
				return Color.Empty;
			}
			set
			{
				base.SecondColor = value;
			}
		}

		// Token: 0x0600EB20 RID: 60192 RVA: 0x0035909D File Offset: 0x0035729D
		internal override void Reset()
		{
			base.Reset();
			this.MainColor = Color.Empty;
			this.SecondColor = Color.Empty;
		}

		// Token: 0x040043F0 RID: 17392
		public static readonly FillStyleSeries Empty = new FillStyleSeries();
	}
}
