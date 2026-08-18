using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Web;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001722 RID: 5922
	internal abstract class MapAreaBuilderBase
	{
		// Token: 0x0600E5FD RID: 58877 RVA: 0x003313A0 File Offset: 0x0032F5A0
		protected string GetPath(IOrdering element, ArrayList list)
		{
			list.Add(element.GetOrder().ToString());
			IOrdering ordering = element.Container as IOrdering;
			if (ordering != null && !(ordering is Chart))
			{
				this.GetPath(ordering, list);
			}
			else
			{
				list.Reverse();
			}
			string text = string.Join(",", (string[])list.ToArray(typeof(string)));
			if (!string.IsNullOrEmpty(text))
			{
				return "false," + text;
			}
			return string.Empty;
		}

		// Token: 0x0600E5FE RID: 58878 RVA: 0x00331424 File Offset: 0x0032F624
		protected string GenerateImageMap(IContainer container)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IOrdering ordering in container.OrderList)
			{
				if (Style.IsVisible(ordering))
				{
					IContainer container2 = ordering as IContainer;
					if (container2 != null)
					{
						stringBuilder.Append(this.GenerateImageMap(container2));
					}
					ChartPlotArea chartPlotArea = ordering as ChartPlotArea;
					if (chartPlotArea != null)
					{
						ChartSeriesCollection chartSeriesCollection = chartPlotArea.SeriesCollection();
						int num = 0;
						int num2 = chartSeriesCollection.Count;
						int num3 = 1;
						if (chartSeriesCollection.GetSeriesCollection(new ChartSeriesType[]
						{
							ChartSeriesType.Area,
							ChartSeriesType.SplineArea,
							ChartSeriesType.StackedArea,
							ChartSeriesType.StackedArea100,
							ChartSeriesType.StackedSplineArea,
							ChartSeriesType.StackedSplineArea100
						}).Count > 0)
						{
							num = chartSeriesCollection.Count - 1;
							num2 = -1;
							num3 = -1;
						}
						for (int num4 = num; num4 != num2; num4 += num3)
						{
							ChartSeries chartSeries = chartSeriesCollection[num4];
							for (int i = 0; i < chartSeries.Items.Count; i++)
							{
								ChartSeriesItem chartSeriesItem = chartSeries.Items[i];
								ActiveRegion activeRegion = new ActiveRegion();
								if (chartSeriesItem.ActiveRegion.Attributes == activeRegion.Attributes && chartSeriesItem.ActiveRegion.Tooltip == activeRegion.Tooltip && chartSeriesItem.ActiveRegion.Url == activeRegion.Url && chartSeries.IsActiveRegionSet)
								{
									chartSeriesItem.ActiveRegion.Url = chartSeries.ActiveRegionUrl;
									chartSeriesItem.ActiveRegion.Tooltip = chartSeries.ActiveRegionToolTip;
									chartSeriesItem.ActiveRegion.Attributes = chartSeries.ActiveRegionAttributes;
								}
								string url = chartSeriesItem.ActiveRegion.Url;
								string text = "";
								if (!string.IsNullOrEmpty(url))
								{
									text = url;
								}
								else if (this.HasChartClickEvent() || chartSeriesItem.ActiveRegion.HasClickEvent())
								{
									text = string.Format("javascript:{0}", this.GetPostBackEventReference(string.Concat(new object[]
									{
										"true,",
										num4,
										",",
										i
									})));
								}
								if (chartSeriesItem.ActiveRegion.Region != null && (!string.IsNullOrEmpty(chartSeriesItem.ActiveRegion.Tooltip) || !string.IsNullOrEmpty(chartSeriesItem.ActiveRegion.Attributes) || !string.IsNullOrEmpty(text)))
								{
									foreach (GraphicsPath graphicsPath in chartSeriesItem.ActiveRegion.activeRegionList)
									{
										StringBuilder stringBuilder2 = new StringBuilder();
										stringBuilder2.Append("\n<area shape=\"");
										string figureName = MapAreaBuilderBase.GetFigureName(chartSeriesItem, chartSeriesItem.ActiveRegion.activeRegionList.IndexOf(graphicsPath));
										stringBuilder2.Append(MapAreaBuilderBase.GetShapeType(figureName));
										stringBuilder2.Append("\"");
										if (!string.IsNullOrEmpty(text))
										{
											stringBuilder2.Append(" href=\"");
											stringBuilder2.Append(chartSeries.FormatValues(text, chartSeriesItem));
											stringBuilder2.Append("\"");
										}
										stringBuilder2.Append(" coords=\"");
										stringBuilder2.Append(MapAreaBuilderBase.GetCoordinates(graphicsPath, figureName));
										stringBuilder2.Append("\" alt=\"");
										stringBuilder2.Append(HttpUtility.HtmlEncode(chartSeries.FormatValues(chartSeriesItem.ActiveRegion.Tooltip, chartSeriesItem)));
										stringBuilder2.Append("\"");
										stringBuilder2.Append(" title=\"");
										stringBuilder2.Append(HttpUtility.HtmlEncode(chartSeries.FormatValues(chartSeriesItem.ActiveRegion.Tooltip, chartSeriesItem)));
										stringBuilder2.Append("\" ");
										stringBuilder2.Append(chartSeries.FormatValues(chartSeriesItem.ActiveRegion.Attributes, chartSeriesItem));
										stringBuilder2.Append(" />");
										stringBuilder.Append(stringBuilder2);
									}
								}
								this.AddImageMap(chartSeriesItem.Label, stringBuilder, true);
							}
						}
						this.AddAxesItemsImageMap(chartPlotArea.XAxis, stringBuilder);
						this.AddAxesItemsImageMap(chartPlotArea.YAxis, stringBuilder);
						this.AddAxesItemsImageMap(chartPlotArea.YAxis2, stringBuilder);
						foreach (ChartMarkedZone chartMarkedZone in chartPlotArea.MarkedZones)
						{
							this.AddImageMap(chartMarkedZone.Label, stringBuilder, false);
						}
					}
					this.AddImageMap(ordering, stringBuilder, false);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600E5FF RID: 58879 RVA: 0x00331908 File Offset: 0x0032FB08
		private void AddAxesItemsImageMap(ChartAxis axis, StringBuilder html)
		{
			if (axis.IsVisible())
			{
				if (axis.AxisLabel.IsVisible())
				{
					this.AddImageMap(axis.AxisLabel, html, true);
				}
				foreach (ChartAxisItem elem in axis.Items)
				{
					this.AddImageMap(elem, html, true);
				}
			}
		}

		// Token: 0x0600E600 RID: 58880 RVA: 0x0033197C File Offset: 0x0032FB7C
		private void AddImageMap(IOrdering elem, StringBuilder html, bool makeTooltipOnly)
		{
			IActiveRegion activeRegion = elem as IActiveRegion;
			if (activeRegion != null)
			{
				string url = activeRegion.ActiveRegion.Url;
				string value = "";
				if (!string.IsNullOrEmpty(url))
				{
					value = url;
				}
				else if ((this.HasChartClickEvent() || activeRegion.ActiveRegion.HasClickEvent()) && !makeTooltipOnly)
				{
					value = string.Format("javascript:{0}", this.GetPostBackEventReference(this.GetPath(elem, new ArrayList())));
				}
				if (!string.IsNullOrEmpty(activeRegion.ActiveRegion.Tooltip) || !string.IsNullOrEmpty(activeRegion.ActiveRegion.Attributes) || !string.IsNullOrEmpty(value))
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("\n<area shape=\"");
					string figure = Style.GetStyleProperty(elem, StyleProperties.Figure).ToString();
					stringBuilder.Append(MapAreaBuilderBase.GetShapeType(figure));
					stringBuilder.Append("\"");
					if (!string.IsNullOrEmpty(value))
					{
						stringBuilder.Append(" href=\"");
						stringBuilder.Append(value);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(" coords=\"");
					stringBuilder.Append(MapAreaBuilderBase.GetCoordinates(activeRegion.ActiveRegion.Region, figure));
					stringBuilder.Append("\" alt=\"");
					stringBuilder.Append(HttpUtility.HtmlEncode(activeRegion.ActiveRegion.Tooltip));
					stringBuilder.Append("\"");
					stringBuilder.Append(" title=\"");
					stringBuilder.Append(HttpUtility.HtmlEncode(activeRegion.ActiveRegion.Tooltip));
					stringBuilder.Append("\" ");
					stringBuilder.Append(activeRegion.ActiveRegion.Attributes);
					stringBuilder.Append(" />");
					html.Append(stringBuilder);
				}
			}
		}

		// Token: 0x0600E601 RID: 58881 RVA: 0x00331B24 File Offset: 0x0032FD24
		private static string GetFigureName(ChartSeriesItem seriesItem, int regionIndex)
		{
			ChartSeries parent = seriesItem.Parent;
			string result = "";
			ChartSeriesType chartSeriesType = parent.Type;
			if (regionIndex > 0)
			{
				chartSeriesType = ChartSeriesType.Line;
			}
			switch (chartSeriesType)
			{
			case ChartSeriesType.Bar:
			case ChartSeriesType.StackedBar:
			case ChartSeriesType.StackedBar100:
			case ChartSeriesType.Gantt:
				result = "Rectangle";
				break;
			case ChartSeriesType.Line:
			case ChartSeriesType.Bezier:
			case ChartSeriesType.Spline:
			case ChartSeriesType.StackedLine:
			case ChartSeriesType.StackedSpline:
			{
				StyleMarkerSeriesPoint styleMarkerSeriesPoint = new StyleMarkerSeriesPoint();
				if (!string.Equals(seriesItem.PointAppearance.Figure, styleMarkerSeriesPoint.Figure))
				{
					result = seriesItem.PointAppearance.Figure;
				}
				else
				{
					result = parent.Appearance.PointMark.Figure;
				}
				break;
			}
			case ChartSeriesType.Area:
			case ChartSeriesType.StackedArea:
			case ChartSeriesType.StackedArea100:
			case ChartSeriesType.Pie:
			case ChartSeriesType.Bubble:
			case ChartSeriesType.SplineArea:
			case ChartSeriesType.StackedSplineArea:
			case ChartSeriesType.StackedSplineArea100:
				result = "poly";
				break;
			case ChartSeriesType.Point:
			{
				StyleSeriesItem styleSeriesItem = new StyleSeriesItem();
				if (!string.Equals(seriesItem.Appearance.PointShape, styleSeriesItem.PointShape))
				{
					result = seriesItem.Appearance.PointShape;
				}
				else
				{
					result = parent.Appearance.PointShape;
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x0600E602 RID: 58882 RVA: 0x00331C38 File Offset: 0x0032FE38
		private static string GetShapeType(string figure)
		{
			if (figure != null)
			{
				if (figure == "Rectangle")
				{
					return "rect";
				}
				if (figure == "Circle")
				{
					return "circle";
				}
			}
			return "poly";
		}

		// Token: 0x0600E603 RID: 58883 RVA: 0x00331C80 File Offset: 0x0032FE80
		private static string GetCoordinates(GraphicsPath path, string figure)
		{
			string result = string.Empty;
			if (path != null)
			{
				if (figure != null)
				{
					if (figure == "Rectangle")
					{
						RectangleF bounds = path.GetBounds();
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append((int)bounds.Left);
						stringBuilder.Append(",");
						stringBuilder.Append((int)bounds.Top);
						stringBuilder.Append(",");
						stringBuilder.Append((int)bounds.Right);
						stringBuilder.Append(",");
						stringBuilder.Append((int)bounds.Bottom);
						return stringBuilder.ToString();
					}
					if (figure == "Circle")
					{
						RectangleF bounds2 = path.GetBounds();
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append((int)(bounds2.Left + bounds2.Width / 2f));
						stringBuilder2.Append(",");
						stringBuilder2.Append((int)(bounds2.Top + bounds2.Height / 2f));
						stringBuilder2.Append(",");
						stringBuilder2.Append((int)(Math.Min(bounds2.Width, bounds2.Height) / 2f));
						return stringBuilder2.ToString();
					}
				}
				string[] array = new string[path.PathPoints.Length];
				int num = 0;
				foreach (PointF pointF in path.PathPoints)
				{
					StringBuilder stringBuilder3 = new StringBuilder();
					stringBuilder3.Append((int)pointF.X);
					stringBuilder3.Append(",");
					stringBuilder3.Append((int)pointF.Y);
					array[num++] = stringBuilder3.ToString();
				}
				result = string.Join(",", array);
			}
			return result;
		}

		// Token: 0x0600E604 RID: 58884
		protected abstract string GetPostBackEventReference(string arguments);

		// Token: 0x0600E605 RID: 58885
		protected abstract bool HasChartClickEvent();

		// Token: 0x0600E606 RID: 58886
		public abstract string GenerateImageMap();
	}
}
