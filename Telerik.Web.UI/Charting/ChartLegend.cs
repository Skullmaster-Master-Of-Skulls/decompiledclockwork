using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001721 RID: 5921
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Items")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ChartLegend : ExtendedLabel
	{
		// Token: 0x1700460B RID: 17931
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override LabelItem this[int itemIndex]
		{
			get
			{
				return this.extendedLabelItems[itemIndex];
			}
			set
			{
				this.extendedLabelItems[itemIndex] = value;
			}
		}

		// Token: 0x0600E5F6 RID: 58870 RVA: 0x00330F63 File Offset: 0x0032F163
		public ChartLegend() : this(null, null)
		{
		}

		// Token: 0x0600E5F7 RID: 58871 RVA: 0x00330F70 File Offset: 0x0032F170
		public ChartLegend(Chart parent, IContainer container) : base(parent, container, new StyleLabelLegend(), new TextBlockLegend(), null)
		{
			this.appearance.styleContainerObject = this;
			this.legendBindableItems = new ChartLabelsCollection();
			this.legendBindableItems.Parent = this;
			this.chartBaseLabelMarker.appearance = new StyleMarkerPositionNone();
			this.chartBaseLabelParent = parent;
			this.chartBaseLabelMarker.Appearance.styleChart = parent;
			this.appearance.styleChart = parent;
			((StyleLabelLegend)this.appearance).ItemMarkerAppearance.styleChart = parent;
			((StyleLabelLegend)this.appearance).ItemAppearance.styleChart = parent;
		}

		// Token: 0x1700460C RID: 17932
		// (get) Token: 0x0600E5F8 RID: 58872 RVA: 0x00331013 File Offset: 0x0032F213
		internal ChartLabelsCollection BoundItems
		{
			get
			{
				return this.legendBindableItems;
			}
		}

		// Token: 0x0600E5F9 RID: 58873 RVA: 0x0033101B File Offset: 0x0032F21B
		internal void ClearBoundItems(bool copyItems)
		{
			this.legendBindableItems.Clear();
			if (copyItems)
			{
				this.extendedLabelItems.CopyBindableItemsTo(this.legendBindableItems);
			}
			this.extendedLabelItems.ClearBindableItems();
		}

		// Token: 0x0600E5FA RID: 58874 RVA: 0x00331048 File Offset: 0x0032F248
		internal void AddBoundItem(RenderEngine engine, ChartSeries series, ChartSeriesItem item, ChartSeriesLegendDisplayMode mode, int seriesIndex, int itemIndex)
		{
			BindableLegendItem bindableLegendItem = new BindableLegendItem((StyleLabel)((StyleLabelLegend)this.appearance).ItemAppearance.Clone(), this);
			bindableLegendItem.TextBlock.appearance = (StyleTextBlock)((StyleLabelLegend)this.appearance).ItemTextAppearance.Clone();
			bindableLegendItem.TextBlock.Appearance.SetStringFormat();
			if (mode == ChartSeriesLegendDisplayMode.SeriesName)
			{
				bindableLegendItem.TextBlock.Text = (string.IsNullOrEmpty(series.LegendFormattedText) ? series.Name : series.LegendFormattedText);
				bindableLegendItem.Name = series.Name;
				bindableLegendItem.BindableLegendItemSource = series;
			}
			else if (mode == ChartSeriesLegendDisplayMode.ItemLabels)
			{
				bindableLegendItem.Name = item.Name;
				bindableLegendItem.TextBlock.Text = item.Name;
				bindableLegendItem.BindableLegendItemSource = item;
			}
			bindableLegendItem.Marker.appearance = (StyleMarker)((StyleLabelLegend)this.appearance).ItemMarkerAppearance.Clone();
			bindableLegendItem.Marker.Visible = true;
			bindableLegendItem.IsBound = true;
			using (Pen pen = engine.GetPen(series, seriesIndex, null))
			{
				if (series.IsLine)
				{
					bindableLegendItem.Marker.Appearance.FillStyle.MainColor = pen.Color;
					bindableLegendItem.Marker.Appearance.FillStyle.FillType = FillType.Solid;
				}
				else
				{
					bindableLegendItem.Marker.Appearance.styleMarkerFillStyle = engine.GetFillStyle(series, seriesIndex, item, itemIndex);
					bindableLegendItem.Marker.Appearance.Border.Width = pen.Width;
				}
				bindableLegendItem.Marker.Appearance.Border.Color = pen.Color;
			}
			this.extendedLabelItems.Add(bindableLegendItem);
		}

		// Token: 0x0600E5FB RID: 58875 RVA: 0x0033120C File Offset: 0x0032F40C
		internal void BindSeriesToLegend(RenderEngine engine)
		{
			if (this.Visible)
			{
				this.ClearBoundItems(false);
				Chart chart = this.chartBaseLabelParent as Chart;
				if (chart != null)
				{
					int count = chart.Series.Count;
					for (int i = 0; i < count; i++)
					{
						ChartSeries chartSeries = chart.Series[i];
						if (chartSeries.Appearance.LegendDisplayMode == ChartSeriesLegendDisplayMode.SeriesName)
						{
							this.AddBoundItem(engine, chartSeries, null, ChartSeriesLegendDisplayMode.SeriesName, i, 0);
						}
						else if (chartSeries.Appearance.LegendDisplayMode == ChartSeriesLegendDisplayMode.ItemLabels)
						{
							int count2 = chartSeries.Items.Count;
							for (int j = 0; j < count2; j++)
							{
								this.AddBoundItem(engine, chartSeries, chartSeries.Items[j], ChartSeriesLegendDisplayMode.ItemLabels, i, j);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600E5FC RID: 58876 RVA: 0x003312C4 File Offset: 0x0032F4C4
		public void AddCustomItemToLegend(string description, FillStyle fillStyle, string figure)
		{
			LabelItem labelItem = new LabelItem((StyleLabel)((StyleLabelLegend)this.appearance).ItemAppearance.Clone(), this);
			labelItem.TextBlock.appearance = (StyleTextBlock)((StyleLabelLegend)this.appearance).ItemTextAppearance.Clone();
			labelItem.TextBlock.Text = description;
			labelItem.Marker.appearance = (StyleMarker)((StyleLabelLegend)this.appearance).ItemMarkerAppearance.Clone();
			labelItem.Marker.Appearance.styleMarkerFillStyle = fillStyle;
			labelItem.Marker.Appearance.Figure = figure;
			labelItem.Marker.Appearance.styleChart = (Chart)base.Parent;
			labelItem.Marker.Visible = true;
			this.extendedLabelItems.Add(labelItem);
		}

		// Token: 0x0400422D RID: 16941
		private ChartLabelsCollection legendBindableItems;
	}
}
