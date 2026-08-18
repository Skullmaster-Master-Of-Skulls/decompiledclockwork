using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200175D RID: 5981
	internal class RenderEngine : IDisposable
	{
		// Token: 0x0600E92C RID: 59692 RVA: 0x00345BD2 File Offset: 0x00343DD2
		public RenderEngine(Chart chart, float width, float height) : this(chart, width, height, true)
		{
		}

		// Token: 0x0600E92D RID: 59693 RVA: 0x00345BDE File Offset: 0x00343DDE
		public RenderEngine(Chart chart, float width, float height, float dpi) : this(chart, width, height, false)
		{
			this.bitmapResolution = dpi;
			this.InitGraphics((int)Math.Round((double)width), (int)Math.Round((double)height));
		}

		// Token: 0x0600E92E RID: 59694 RVA: 0x00345C0C File Offset: 0x00343E0C
		public RenderEngine(Chart chart, float width, float height, bool initGraphics)
		{
			this.chart = chart;
			this.chart.Appearance.Dimensions.SetDimensions(width, height);
			if (initGraphics)
			{
				this.InitGraphics((int)Math.Round((double)width), (int)Math.Round((double)height));
			}
		}

		// Token: 0x0600E92F RID: 59695 RVA: 0x00345C60 File Offset: 0x00343E60
		~RenderEngine()
		{
			this.Dispose(false);
		}

		// Token: 0x0600E930 RID: 59696 RVA: 0x00345C90 File Offset: 0x00343E90
		internal static GraphicsPath ScaleTo(GraphicsPath path, float width, float height)
		{
			using (Matrix matrix = new Matrix())
			{
				if (path == null)
				{
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddRectangle(new RectangleF(0f, 0f, width, height));
					return graphicsPath;
				}
				RectangleF bounds = path.GetBounds();
				float width2 = bounds.Width;
				float height2 = bounds.Height;
				matrix.Scale(width / width2, height / height2);
				path.Transform(matrix);
			}
			return path;
		}

		// Token: 0x0600E931 RID: 59697 RVA: 0x00345D14 File Offset: 0x00343F14
		internal static GraphicsPath MoveTo(GraphicsPath path, float x, float y)
		{
			GraphicsPath graphicsPath = (GraphicsPath)path.Clone();
			using (Matrix matrix = new Matrix())
			{
				matrix.Translate(x, y, MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			return graphicsPath;
		}

		// Token: 0x0600E932 RID: 59698 RVA: 0x00345D60 File Offset: 0x00343F60
		internal static Position LocalToGlobal(IOrdering element)
		{
			List<IOrdering> list = new List<IOrdering>();
			list.Add(element);
			Position position = new Position(0f, 0f);
			int num = list.Count;
			for (int i = 0; i < num; i++)
			{
				IOrdering ordering = list[i];
				Position position2 = Style.GetStyleProperty(ordering, StyleProperties.Position) as Position;
				if (position2 != null)
				{
					if (position2.IsSetGlobal)
					{
						position.X += position2.GlobalX;
						position.Y += position2.GlobalY;
						break;
					}
					position.X += position2.X;
					position.Y += position2.Y;
				}
				IOrdering ordering2 = ordering.Container as IOrdering;
				if (ordering2 != null)
				{
					list.Add(ordering2);
					num++;
				}
			}
			Position position3 = Style.GetStyleProperty(element, StyleProperties.Position) as Position;
			position3.GlobalX = position.X;
			position3.GlobalY = position.Y;
			return position;
		}

		// Token: 0x0600E933 RID: 59699 RVA: 0x00345E64 File Offset: 0x00344064
		private static Pen GetPen(StyleBorder border, PenAlignment aligment)
		{
			if (border.Width > 0f && border.Visible && border.Color != Color.Empty)
			{
				aligment = (((int)border.Width == 1) ? PenAlignment.Outset : aligment);
				Pen pen = new Pen(border.Color, border.Width);
				pen.LineJoin = LineJoin.Round;
				pen.DashStyle = border.PenStyle;
				pen.Alignment = aligment;
				LineStyle lineStyle = border as LineStyle;
				if (lineStyle != null)
				{
					pen.EndCap = lineStyle.EndCap;
					pen.StartCap = lineStyle.StartCap;
				}
				return pen;
			}
			return new Pen(Color.Empty, 0f);
		}

		// Token: 0x0600E934 RID: 59700 RVA: 0x00345F07 File Offset: 0x00344107
		private static Pen GetPen(StyleBorder border)
		{
			return RenderEngine.GetPen(border, PenAlignment.Outset);
		}

		// Token: 0x0600E935 RID: 59701 RVA: 0x00345F10 File Offset: 0x00344110
		private static Pen GetPen(LineStyle border, Color color, float width)
		{
			if (width > 0f && border.Visible && border.Color != Color.Empty)
			{
				return new Pen(color, width)
				{
					DashStyle = border.PenStyle,
					EndCap = border.EndCap,
					StartCap = border.StartCap
				};
			}
			return new Pen(Color.Empty, 0f);
		}

		// Token: 0x0600E936 RID: 59702 RVA: 0x00345F7C File Offset: 0x0034417C
		private Brush GetBrush(FillStyle fill, RectangleF rect)
		{
			switch (fill.FillType)
			{
			case FillType.Solid:
				return new SolidBrush(fill.MainColor);
			case FillType.Gradient:
			case FillType.ComplexGradient:
				break;
			case FillType.Hatch:
				return new HatchBrush(fill.FillSettings.HatchStyle, fill.MainColor, fill.SecondColor);
			case FillType.Image:
				try
				{
					using (Image image = fill.FillSettings.GetImage(this.chart))
					{
						Brush brush = new TextureBrush(image);
						TextureBrush textureBrush = brush as TextureBrush;
						switch (fill.FillSettings.ImageDrawMode)
						{
						case ImageDrawMode.Align:
							return this.GetAlignedImageBrush(fill, (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, image);
						case ImageDrawMode.Stretch:
							return this.GetStretchedImageBrush((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, image);
						case ImageDrawMode.Flip:
							switch (fill.FillSettings.ImageFlip)
							{
							case ImageTileModes.Flip:
								textureBrush.WrapMode = WrapMode.Tile;
								break;
							case ImageTileModes.FlipX:
								textureBrush.WrapMode = WrapMode.TileFlipX;
								break;
							case ImageTileModes.FlipY:
								textureBrush.WrapMode = WrapMode.TileFlipY;
								break;
							case ImageTileModes.FlipXY:
								textureBrush.WrapMode = WrapMode.TileFlipXY;
								break;
							}
							textureBrush.TranslateTransform(rect.X, rect.Y);
							return brush;
						}
					}
					goto IL_173;
				}
				catch
				{
					return new SolidBrush(fill.MainColor);
				}
				break;
			default:
				goto IL_173;
			}
			return RenderEngine.GetGradientBrush(rect, fill);
			IL_173:
			return new SolidBrush(fill.MainColor);
		}

		// Token: 0x0600E937 RID: 59703 RVA: 0x00346140 File Offset: 0x00344340
		private static Brush GetGradientBrush(RectangleF rect, FillStyle fill)
		{
			float width = rect.Width;
			float height = rect.Height;
			float x = rect.X;
			float y = rect.Y;
			RectangleF rectangleF = new RectangleF(rect.X - 1f, rect.Y - 1f, width + 2f, height + 2f);
			LinearGradientBrush linearGradientBrush = null;
			if (width * height == 0f)
			{
				return new SolidBrush(fill.MainColor);
			}
			switch (fill.FillSettings.GradientMode)
			{
			case GradientFillStyle.Horizontal:
				if (fill.FillType == FillType.ComplexGradient)
				{
					linearGradientBrush = fill.FillSettings.ComplexGradient.GetBrush(rectangleF, fill.FillSettings.GradientAngle);
				}
				else
				{
					linearGradientBrush = new LinearGradientBrush(rectangleF, fill.MainColor, fill.SecondColor, fill.FillSettings.GradientAngle);
				}
				break;
			case GradientFillStyle.Vertical:
				if (fill.FillType == FillType.ComplexGradient)
				{
					linearGradientBrush = fill.FillSettings.ComplexGradient.GetBrush(rectangleF, fill.FillSettings.GradientAngle + 90f);
				}
				else
				{
					linearGradientBrush = new LinearGradientBrush(rectangleF, fill.MainColor, fill.SecondColor, fill.FillSettings.GradientAngle + 90f);
				}
				break;
			case GradientFillStyle.ForwardDiagonal:
				if (fill.FillType == FillType.ComplexGradient)
				{
					linearGradientBrush = fill.FillSettings.ComplexGradient.GetBrush(rectangleF, fill.FillSettings.GradientAngle + RenderEngine.getDiagonalAngle(rectangleF));
				}
				else
				{
					linearGradientBrush = new LinearGradientBrush(rectangleF, fill.MainColor, fill.SecondColor, fill.FillSettings.GradientAngle + RenderEngine.getDiagonalAngle(rectangleF));
				}
				break;
			case GradientFillStyle.BackwardDiagonal:
				if (fill.FillType == FillType.ComplexGradient)
				{
					linearGradientBrush = fill.FillSettings.ComplexGradient.GetBrush(rectangleF, fill.FillSettings.GradientAngle + (180f - RenderEngine.getDiagonalAngle(rectangleF)));
				}
				else
				{
					linearGradientBrush = new LinearGradientBrush(rectangleF, fill.MainColor, fill.SecondColor, fill.FillSettings.GradientAngle + (180f - RenderEngine.getDiagonalAngle(rectangleF)));
				}
				break;
			case GradientFillStyle.Center:
			{
				GraphicsPath graphicsPath = new GraphicsPath();
				Rectangle rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);
				graphicsPath.AddRectangle(rectangle);
				PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
				if (fill.FillType == FillType.ComplexGradient)
				{
					pathGradientBrush.InterpolationColors = fill.FillSettings.ComplexGradient.GetBrush(rectangle, 0f).InterpolationColors;
				}
				else
				{
					pathGradientBrush.CenterColor = fill.MainColor;
					pathGradientBrush.SurroundColors = new Color[]
					{
						fill.SecondColor
					};
				}
				return pathGradientBrush;
			}
			case GradientFillStyle.Circle:
			{
				GraphicsPath graphicsPath2 = new GraphicsPath();
				int num = (int)(width * 1.5f);
				int num2 = (int)(height * 1.5f);
				int x2 = (int)(x - ((float)num - width) / 2f);
				int y2 = (int)(y - ((float)num2 - height) / 2f);
				if ((float)(num * num2) == 0f)
				{
					return new SolidBrush(fill.MainColor);
				}
				Rectangle rectangle2 = new Rectangle(new Point(x2, y2), new Size(num, num2));
				graphicsPath2.AddEllipse(rectangle2);
				PathGradientBrush pathGradientBrush2 = new PathGradientBrush(graphicsPath2);
				if (fill.FillType == FillType.ComplexGradient)
				{
					pathGradientBrush2.InterpolationColors = fill.FillSettings.ComplexGradient.GetBrush(rectangle2, 0f).InterpolationColors;
				}
				else
				{
					pathGradientBrush2.CenterColor = fill.MainColor;
					pathGradientBrush2.SurroundColors = new Color[]
					{
						fill.SecondColor
					};
				}
				return pathGradientBrush2;
			}
			}
			if (linearGradientBrush != null)
			{
				linearGradientBrush.GammaCorrection = fill.GammaCorrection;
			}
			return linearGradientBrush;
		}

		// Token: 0x0600E938 RID: 59704 RVA: 0x003464EE File Offset: 0x003446EE
		private static float getDiagonalAngle(RectangleF rectS)
		{
			return (float)(Math.Atan2((double)rectS.Width, (double)rectS.Height) * 180.0 / 3.141592653589793);
		}

		// Token: 0x0600E939 RID: 59705 RVA: 0x0034651C File Offset: 0x0034471C
		private static void AdjustRect(ref RectangleF rect)
		{
			if (rect.Width < 0f)
			{
				rect.X += rect.Width;
				rect.Width = -rect.Width;
			}
			if (rect.Height < 0f)
			{
				rect.Y += rect.Height;
				rect.Height = -rect.Height;
			}
		}

		// Token: 0x0600E93A RID: 59706 RVA: 0x00346583 File Offset: 0x00344783
		private static int AdjustRoundSize(int roundSize, CornerType widthCorner, CornerType heightCorner, int width, int height)
		{
			if (widthCorner == CornerType.Round)
			{
				if (roundSize * 2 > width)
				{
					roundSize = width / 2;
				}
			}
			else if (roundSize > width)
			{
				roundSize = width;
			}
			if (heightCorner == CornerType.Round)
			{
				if (roundSize * 2 > height)
				{
					roundSize = height / 2;
				}
			}
			else if (roundSize > height)
			{
				roundSize = height;
			}
			return roundSize;
		}

		// Token: 0x0600E93B RID: 59707 RVA: 0x003465BC File Offset: 0x003447BC
		private static GraphicsPath GetRoundArea(Corners corners, float X, float Y, float width, float height)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)Math.Round((double)(X + width));
			X = (float)Math.Round((double)X);
			width = num - X;
			num = (float)Math.Round((double)(Y + height));
			Y = (float)Math.Round((double)Y);
			height = num - Y;
			if (corners.TopLeft != CornerType.Round && corners.TopRight != CornerType.Round && corners.BottomRight != CornerType.Round && corners.BottomLeft != CornerType.Round)
			{
				graphicsPath.AddRectangle(new RectangleF(X, Y, width, height));
				return graphicsPath;
			}
			int roundSize = corners.RoundSize;
			float num2 = X + width;
			float num3 = Y + height;
			if (corners.TopLeft == CornerType.Round)
			{
				int num4 = RenderEngine.AdjustRoundSize(roundSize, corners.TopRight, corners.BottomLeft, (int)width, (int)height);
				if (num4 > 0)
				{
					graphicsPath.AddArc(X, Y, (float)(2 * num4), (float)(2 * num4), 180f, 90f);
				}
				else
				{
					graphicsPath.AddLine(X, Y, X, Y);
				}
			}
			else
			{
				graphicsPath.AddLine(X, Y, X, Y);
			}
			if (corners.TopRight == CornerType.Round)
			{
				int num4 = RenderEngine.AdjustRoundSize(roundSize, corners.TopLeft, corners.BottomRight, (int)width, (int)height);
				if (num4 > 0)
				{
					graphicsPath.AddArc(num2 - (float)(2 * num4), Y, (float)(2 * num4), (float)(2 * num4), 270f, 90f);
				}
				else
				{
					graphicsPath.AddLine(num2, Y, num2, Y);
				}
			}
			else
			{
				graphicsPath.AddLine(num2, Y, num2, Y);
			}
			if (corners.BottomRight == CornerType.Round)
			{
				int num4 = RenderEngine.AdjustRoundSize(roundSize, corners.BottomLeft, corners.TopRight, (int)width, (int)height);
				if (num4 > 0)
				{
					graphicsPath.AddArc(num2 - (float)(2 * num4), num3 - (float)(2 * num4), (float)(2 * num4), (float)(2 * num4), 0f, 90f);
				}
				else
				{
					graphicsPath.AddLine(num2, num3, num2, num3);
				}
			}
			else
			{
				graphicsPath.AddLine(num2, num3, num2, num3);
			}
			if (corners.BottomLeft == CornerType.Round)
			{
				int num4 = RenderEngine.AdjustRoundSize(roundSize, corners.BottomRight, corners.TopLeft, (int)width, (int)height);
				if (num4 > 0)
				{
					graphicsPath.AddArc(X, num3 - (float)(2 * num4), (float)(2 * num4), (float)(2 * num4), 90f, 90f);
				}
				else
				{
					graphicsPath.AddLine(X, num3, X, num3);
				}
			}
			else
			{
				graphicsPath.AddLine(X, num3, X, num3);
			}
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600E93C RID: 59708 RVA: 0x003467D8 File Offset: 0x003449D8
		private GraphicsPath GetRoundRectangle(Corners corners, RectangleF rect, ChartSeries series)
		{
			if (corners.Equals(new Corners()))
			{
				corners.CopyFrom(series.Appearance.Corners);
			}
			return RenderEngine.GetRoundArea(corners, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x0600E93D RID: 59709 RVA: 0x00346828 File Offset: 0x00344A28
		internal static SizeF GetMaxSize(List<SizeF> sizes)
		{
			SizeF result = new SizeF(0f, 0f);
			foreach (SizeF sizeF in sizes)
			{
				if (sizeF.Width > result.Width)
				{
					result.Width = sizeF.Width;
				}
				if (sizeF.Height > result.Height)
				{
					result.Height = sizeF.Height;
				}
			}
			return result;
		}

		// Token: 0x0600E93E RID: 59710 RVA: 0x003468C0 File Offset: 0x00344AC0
		private static string AddString(ChartGraphics graphics, string result, string str, string space, float width, Font font)
		{
			StringBuilder stringBuilder = new StringBuilder(result);
			stringBuilder.Append(space);
			stringBuilder.Append(str);
			string text = stringBuilder.ToString();
			stringBuilder = new StringBuilder(result);
			if (graphics.MeasureString(text, font).Width > width)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(str);
				result = stringBuilder.ToString();
			}
			else
			{
				stringBuilder.Append(space);
				stringBuilder.Append(str);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600E93F RID: 59711 RVA: 0x00346940 File Offset: 0x00344B40
		internal static string PrepareForVerticalOverflow(ChartGraphics graphics, string text, Font font, float width)
		{
			string[] array = text.Split(new char[]
			{
				' '
			});
			string text2 = "";
			string space = "";
			foreach (string text3 in array)
			{
				if (graphics.MeasureString(text3, font).Width > width)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						text2 += "\n";
						space = "";
					}
					char[] array3 = text3.ToCharArray();
					foreach (char c in array3)
					{
						text2 = RenderEngine.AddString(graphics, text2, c.ToString(), space, width, font);
						space = "";
					}
					space = " ";
				}
				else
				{
					text2 = RenderEngine.AddString(graphics, text2, text3, space, width, font);
					space = " ";
				}
			}
			return text2;
		}

		// Token: 0x0600E940 RID: 59712 RVA: 0x00346A1C File Offset: 0x00344C1C
		internal Region GetRenderRegion(ChartYAxisType yAxisType)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			switch (yAxisType)
			{
			case ChartYAxisType.Primary:
				if (plotArea.PlotRegionYAxisPrimary != null)
				{
					return plotArea.PlotRegionYAxisPrimary;
				}
				break;
			case ChartYAxisType.Secondary:
				if (plotArea.PlotRegionYAxisSecondary != null)
				{
					return plotArea.PlotRegionYAxisSecondary;
				}
				break;
			}
			if (plotArea.PlotRegionCommon != null)
			{
				return plotArea.PlotRegionCommon;
			}
			return this.GetRenderRegion(plotArea);
		}

		// Token: 0x0600E941 RID: 59713 RVA: 0x00346A7C File Offset: 0x00344C7C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal Region GetRenderRegion(object element)
		{
			ChartPlotArea chartPlotArea = element as ChartPlotArea;
			if (chartPlotArea != null && chartPlotArea.PlotRegionCommon != null)
			{
				return chartPlotArea.PlotRegionCommon;
			}
			List<GraphicsPath> list = new List<GraphicsPath>();
			List<object> list2 = new List<object>();
			list2.Add(element);
			int i = 0;
			while (i < list2.Count)
			{
				object obj = list2[i] as IOrdering;
				bool flag = obj != null;
				if (flag)
				{
					goto IL_64;
				}
				obj = (list2[i] as Chart);
				if (obj != null)
				{
					goto IL_64;
				}
				IL_187:
				i++;
				continue;
				IL_64:
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(obj, StyleProperties.Dimensions);
				string text = (string)Style.GetStyleProperty(obj, StyleProperties.Figure);
				Position position;
				if (flag && !(obj is Chart))
				{
					position = RenderEngine.LocalToGlobal((IOrdering)obj);
				}
				else
				{
					position = new Position(0f, 0f);
				}
				GraphicsPath graphicsPath;
				if (string.Compare(text, "Rectangle", true) == 0)
				{
					graphicsPath = RenderEngine.GetRoundArea((Corners)Style.GetStyleProperty(obj, StyleProperties.Corners), position.X, position.Y, dimensions.Width.PixelValue, dimensions.Height.PixelValue);
				}
				else
				{
					graphicsPath = FiguresCollection.GetPath(text);
					if (graphicsPath == null)
					{
						graphicsPath = FiguresCollection.GetPath(text, this.chart);
					}
					graphicsPath = RenderEngine.ScaleTo(graphicsPath, dimensions.Width.PixelValue, dimensions.Height.PixelValue);
					graphicsPath = RenderEngine.MoveTo(graphicsPath, position.X, position.Y);
				}
				if (flag)
				{
					IOrdering ordering = obj as IOrdering;
					this.GetRotationAngle(ordering, ref graphicsPath);
					if (ordering.Container != null)
					{
						list2.Add(ordering.Container);
					}
				}
				list.Add(graphicsPath);
				goto IL_187;
			}
			Region region = new Region();
			if (list.Count > 0)
			{
				region = new Region(list[list.Count - 1]);
				if (list.Count > 1)
				{
					for (int j = list.Count - 2; j >= 0; j--)
					{
						region.Intersect(list[j]);
					}
				}
			}
			return region;
		}

		// Token: 0x0600E942 RID: 59714 RVA: 0x00346C78 File Offset: 0x00344E78
		private void Render(IContainer element)
		{
			IOrderingCollection orderingCollection = new IOrderingCollection();
			orderingCollection.AddVisibleRange(element.OrderList, -1);
			for (int i = 0; i < orderingCollection.Count; i++)
			{
				IOrdering ordering = orderingCollection[i];
				ChartDataTable chartDataTable = ordering as ChartDataTable;
				if ((ordering != null && Style.IsVisible(ordering)) || ordering is ChartYAxis)
				{
					if (chartDataTable == null && !(ordering is ChartPlotArea))
					{
						this.RenderElement(ordering, true, true);
					}
					IContainer container = ordering as IContainer;
					if (container != null)
					{
						ChartPlotArea chartPlotArea = container as ChartPlotArea;
						if (chartPlotArea != null)
						{
							chartPlotArea.OnRender();
							this.RenderPlotAreaElements(true, true);
						}
						orderingCollection.AddVisibleRange(container.OrderList, i);
					}
					else
					{
						TextBlock textBlock = ordering as TextBlock;
						if (textBlock != null)
						{
							this.RenderTextBlock(textBlock);
						}
						else
						{
							ChartDataTable chartDataTable2 = chartDataTable;
							if (chartDataTable2 != null)
							{
								this.RenderChartDataTable(chartDataTable2);
							}
						}
					}
				}
			}
			Chart chart = element as Chart;
			if (chart != null && chart.DesignTime)
			{
				chart.Legend.ClearBoundItems(true);
			}
		}

		// Token: 0x0600E943 RID: 59715 RVA: 0x00346D64 File Offset: 0x00344F64
		private void RenderPlotAreaElements(bool withGrid, bool withTicks)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			this.RenderElement(plotArea, true, false);
			if (!plotArea.EmptySeriesMessage.IsVisible())
			{
				this.chart.PlotArea.AlignAxisByZeros();
				this.chart.PlotArea.PopularValues = PopularCollection.GetPopularValues(this.chart);
				this.RenderMarkedZones();
				if (withGrid)
				{
					this.DrawGrids();
				}
				if (withTicks)
				{
					this.DrawTicks();
				}
				this.RenderMarkedZonesLabel();
				int num = -1;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				plotArea.SeriesLabels.Clear();
				this.DrawScaleBreaks(plotArea.YAxis2);
				this.DrawScaleBreaks(plotArea.YAxis);
				foreach (ChartSeries chartSeries in this.seriesList)
				{
					this.barWidth = plotArea.GetBarWidth(chartSeries);
					this.barWidthRatio = (float)(this.chart.Appearance.BarWidthPercent / 100m);
					num++;
					if ((chartSeries.Type != ChartSeriesType.StackedArea || !flag) && (chartSeries.Type != ChartSeriesType.StackedArea100 || !flag3) && (chartSeries.Type != ChartSeriesType.StackedLine || !flag7) && (chartSeries.Type != ChartSeriesType.StackedSpline || !flag8) && (chartSeries.Type != ChartSeriesType.StackedBar100 || !flag5) && (chartSeries.Type != ChartSeriesType.StackedBar || !flag6) && (chartSeries.Type != ChartSeriesType.StackedSplineArea || !flag2) && (chartSeries.Type != ChartSeriesType.StackedSplineArea100 || !flag4))
					{
						flag |= (chartSeries.Type == ChartSeriesType.StackedArea);
						flag2 |= (chartSeries.Type == ChartSeriesType.StackedSplineArea);
						flag4 |= (chartSeries.Type == ChartSeriesType.StackedSplineArea100);
						flag3 |= (chartSeries.Type == ChartSeriesType.StackedArea100);
						flag6 |= (chartSeries.Type == ChartSeriesType.StackedBar);
						flag5 |= (chartSeries.Type == ChartSeriesType.StackedBar100);
						flag7 |= (chartSeries.Type == ChartSeriesType.StackedLine);
						flag8 |= (chartSeries.Type == ChartSeriesType.StackedSpline);
						this.Render(chartSeries, num);
					}
				}
				this.chart.chartSeriesCollection = this.originalSeries;
				this.SeriesLabelsDraw();
				if (!this.chart.ScaleEnabled || this.chart.DesignTime)
				{
					this.RenderAxis(plotArea.XAxis);
					this.RenderAxis(plotArea.YAxis);
					this.RenderAxis(plotArea.YAxis2);
				}
				plotArea.ResetRegions();
			}
			if (!this.chart.ScaleEnabled || this.chart.DesignTime)
			{
				this.RenderElement(plotArea, false, true);
			}
		}

		// Token: 0x0600E944 RID: 59716 RVA: 0x00347030 File Offset: 0x00345230
		private void DrawScaleBreaks(ChartYAxis chartYAxis)
		{
			if (!chartYAxis.IsVisible() || !chartYAxis.ScaleBreaks.Enabled)
			{
				return;
			}
			using (Pen pen = RenderEngine.GetPen(chartYAxis.ScaleBreaks.Line))
			{
				pen.Alignment = PenAlignment.Center;
				foreach (AxisSegment axisSegment in chartYAxis.Segments)
				{
					if (axisSegment.axisSegmentPaths[0] != null)
					{
						GraphicsPath path = RenderEngine.MoveTo(axisSegment.axisSegmentPaths[0], 0f, -chartYAxis.ScaleBreaks.Line.Width / 2f);
						this.graphics.DrawPath(pen, path);
					}
					if (axisSegment.axisSegmentPaths[1] != null)
					{
						GraphicsPath path2 = RenderEngine.MoveTo(axisSegment.axisSegmentPaths[1], 0f, chartYAxis.ScaleBreaks.Line.Width / 2f);
						this.graphics.DrawPath(pen, path2);
					}
				}
			}
		}

		// Token: 0x0600E945 RID: 59717 RVA: 0x00347148 File Offset: 0x00345348
		private void RenderMarkedZonesLabel()
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			foreach (ChartMarkedZone chartMarkedZone in plotArea.MarkedZones)
			{
				ChartYAxis chartYAxis = (chartMarkedZone.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
				if (Style.IsVisible(chartMarkedZone) && !this.chart.OnlyPieSeries() && chartYAxis.IsVisible() && chartMarkedZone.Label.IsVisible())
				{
					this.RenderElement(chartMarkedZone.Label);
					chartMarkedZone.Label.TextBlock.Container = chartMarkedZone.Label;
					this.Render(chartMarkedZone.Label);
				}
			}
		}

		// Token: 0x0600E946 RID: 59718 RVA: 0x0034720C File Offset: 0x0034540C
		private void RenderMarkedZones()
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			this.graphics.SetClip(this.GetRenderRegion(plotArea), CombineMode.Replace);
			foreach (ChartMarkedZone chartMarkedZone in plotArea.MarkedZones)
			{
				this.RenderMarkedZone(chartMarkedZone, plotArea.XAxis, (chartMarkedZone.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2);
			}
			this.graphics.ResetClip();
		}

		// Token: 0x0600E947 RID: 59719 RVA: 0x003472A0 File Offset: 0x003454A0
		private void RenderAxisItems(ChartAxis chartAxis)
		{
			if (chartAxis is ChartXAxis && this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical && chartAxis.Parent.DataTable.IsVisible && chartAxis.Parent.DataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative)
			{
				return;
			}
			int count = chartAxis.Items.Count;
			ChartAxisItem chartAxisItem = new ChartAxisItem();
			TextPropertiesAxisItem textPropertiesAxisItem = new TextPropertiesAxisItem();
			ChartAxisItem chartAxisItem2 = new ChartAxisItem();
			for (int i = 0; i < count; i++)
			{
				ChartAxisItem chartAxisItem3 = chartAxis.Items[i];
				if (chartAxis.CheckAxisItemVisibility(chartAxisItem3))
				{
					chartAxisItem2.appearance = (StyleLabel)chartAxisItem3.Appearance.Clone();
					chartAxisItem2.Appearance.styleContainerObject = null;
					chartAxisItem2.Container = chartAxisItem3.Container;
					chartAxisItem2.ActiveRegion = chartAxisItem3.ActiveRegion;
					chartAxisItem2.Marker.appearance = (StyleMarker)chartAxisItem3.Marker.Appearance.Clone();
					chartAxisItem2.Parent = chartAxisItem3.Parent;
					chartAxisItem2.PlacementDirection = chartAxisItem3.PlacementDirection;
					chartAxisItem2.Value = chartAxisItem3.Value;
					chartAxisItem2.TextBlock.Text = chartAxisItem3.TextBlock.Text;
					chartAxisItem2.TextBlock.textBlockWrappedText = chartAxisItem3.TextBlock.textBlockWrappedText;
					chartAxisItem2.TextBlock.textBlockWrapContext = chartAxisItem3.TextBlock.textBlockWrapContext;
					chartAxisItem2.TextBlock.appearance = (LayoutStyle)chartAxisItem3.TextBlock.Appearance.Clone();
					if (chartAxisItem3.Appearance.Border.Color.Equals(chartAxisItem.Appearance.Border.Color))
					{
						chartAxisItem2.Appearance.Border.Color = chartAxis.Appearance.LabelAppearance.Border.Color;
					}
					if (chartAxisItem3.Appearance.Border.Width == chartAxisItem.Appearance.Border.Width)
					{
						chartAxisItem2.Appearance.Border.Width = chartAxis.Appearance.LabelAppearance.Border.Width;
					}
					if (chartAxisItem3.Appearance.Border.Visible == chartAxisItem.Appearance.Border.Visible)
					{
						chartAxisItem2.Appearance.Border.Visible = chartAxis.Appearance.LabelAppearance.Border.Visible;
					}
					if (chartAxisItem3.Appearance.Border.PenStyle == chartAxisItem.Appearance.Border.PenStyle)
					{
						chartAxisItem2.Appearance.Border.PenStyle = chartAxis.Appearance.LabelAppearance.Border.PenStyle;
					}
					if (chartAxisItem3.Appearance.Position.AlignedPosition == chartAxisItem.Appearance.Position.AlignedPosition)
					{
						chartAxisItem2.Appearance.Position.AlignedPosition = chartAxis.Appearance.LabelAppearance.Position.AlignedPosition;
					}
					if (chartAxisItem3.Appearance.Corners.Equals(chartAxisItem.Appearance.Corners))
					{
						chartAxisItem2.Appearance.Corners.CopyFrom(chartAxis.Appearance.LabelAppearance.Corners);
					}
					else
					{
						chartAxisItem2.Appearance.Corners.CopyFrom(chartAxisItem3.Appearance.Corners);
					}
					if (chartAxisItem3.Appearance.Figure.Equals(chartAxisItem.Appearance.Figure))
					{
						chartAxisItem2.Appearance.Figure = chartAxis.Appearance.LabelAppearance.Figure;
					}
					if (chartAxisItem3.Appearance.FillStyle.Equals(chartAxisItem.Appearance.FillStyle))
					{
						chartAxisItem2.Appearance.styleLabelFillStyle = (FillStyle)chartAxis.Appearance.LabelAppearance.FillStyle.Clone();
					}
					if (chartAxisItem3.Appearance.RotationAngle == chartAxisItem.Appearance.RotationAngle)
					{
						chartAxisItem2.Appearance.RotationAngle = chartAxis.Appearance.LabelAppearance.RotationAngle;
					}
					if (chartAxisItem3.Appearance.Shadow.Equals(chartAxisItem.Appearance.Shadow))
					{
						chartAxisItem2.Appearance.styleShadow = (ShadowStyle)chartAxis.Appearance.LabelAppearance.Shadow.Clone();
					}
					if (chartAxisItem3.Appearance.Dimensions.AutoSize == chartAxisItem.Appearance.Dimensions.AutoSize)
					{
						chartAxisItem2.Appearance.Dimensions.AutoSize = chartAxis.Appearance.LabelAppearance.Dimensions.AutoSize;
						if (chartAxisItem3.Appearance.Dimensions.Width == chartAxisItem.Appearance.Dimensions.Width)
						{
							chartAxisItem2.Appearance.Dimensions.Width = chartAxis.Appearance.LabelAppearance.Dimensions.Width;
						}
						if (chartAxisItem3.Appearance.Dimensions.Height == chartAxisItem.Appearance.Dimensions.Height)
						{
							chartAxisItem2.Appearance.Dimensions.Height = chartAxis.Appearance.LabelAppearance.Dimensions.Height;
						}
					}
					if (chartAxisItem3.TextBlock.Appearance.TextProperties.Color.Equals(textPropertiesAxisItem.Color))
					{
						chartAxisItem2.TextBlock.Appearance.TextProperties.Color = chartAxis.Appearance.TextAppearance.TextProperties.Color;
					}
					if (chartAxisItem3.TextBlock.Appearance.Border.Color.Equals(chartAxisItem.TextBlock.Appearance.Border.Color))
					{
						chartAxisItem2.TextBlock.Appearance.Border.Color = chartAxis.Appearance.TextAppearance.Border.Color;
					}
					if (chartAxisItem3.TextBlock.Appearance.Border.Width == chartAxisItem.TextBlock.Appearance.Border.Width)
					{
						chartAxisItem2.TextBlock.Appearance.Border.Width = chartAxis.Appearance.TextAppearance.Border.Width;
					}
					if (chartAxisItem3.TextBlock.Appearance.Border.Visible == chartAxisItem.TextBlock.Appearance.Border.Visible)
					{
						chartAxisItem2.TextBlock.Appearance.Border.Visible = chartAxis.Appearance.TextAppearance.Border.Visible;
					}
					if (chartAxisItem3.TextBlock.Appearance.Border.PenStyle == chartAxisItem.TextBlock.Appearance.Border.PenStyle)
					{
						chartAxisItem2.TextBlock.Appearance.Border.PenStyle = chartAxis.Appearance.TextAppearance.Border.PenStyle;
					}
					if (chartAxisItem3.TextBlock.Appearance.Corners.Equals(chartAxisItem.TextBlock.Appearance.Corners))
					{
						chartAxisItem2.TextBlock.Appearance.Corners = (Corners)chartAxis.Appearance.TextAppearance.Corners.Clone();
					}
					if (chartAxisItem3.TextBlock.Appearance.FillStyle.Equals(chartAxisItem.TextBlock.Appearance.FillStyle))
					{
						chartAxisItem2.TextBlock.Appearance.styleTextBlockFillStyle = (FillStyle)chartAxis.Appearance.TextAppearance.FillStyle.Clone();
					}
					if (chartAxisItem3.TextBlock.Appearance.Position.Equals(chartAxisItem.TextBlock.Appearance.Position))
					{
						chartAxisItem2.TextBlock.Appearance.position = (Position)chartAxis.Appearance.TextAppearance.Position.Clone();
					}
					if (chartAxisItem3.TextBlock.Appearance.Visible == chartAxisItem.TextBlock.Appearance.Visible)
					{
						chartAxisItem2.TextBlock.Appearance.Visible = chartAxis.Appearance.TextAppearance.Visible;
					}
					if (chartAxisItem3.TextBlock.Appearance.Shadow.Blur == chartAxisItem.TextBlock.Appearance.Shadow.Blur)
					{
						chartAxisItem2.TextBlock.Appearance.Shadow.Blur = chartAxis.Appearance.TextAppearance.Shadow.Blur;
					}
					if (chartAxisItem3.TextBlock.Appearance.Shadow.Color.Equals(chartAxisItem2.TextBlock.Appearance.Shadow.Color))
					{
						chartAxisItem2.TextBlock.Appearance.Shadow.Color = chartAxis.Appearance.TextAppearance.Shadow.Color;
					}
					if (chartAxisItem3.TextBlock.Appearance.Shadow.Distance == chartAxisItem.TextBlock.Appearance.Shadow.Distance)
					{
						chartAxisItem2.TextBlock.Appearance.Shadow.Distance = chartAxis.Appearance.TextAppearance.Shadow.Distance;
					}
					if (chartAxisItem3.TextBlock.Appearance.Shadow.Position == chartAxisItem.TextBlock.Appearance.Shadow.Position)
					{
						chartAxisItem2.TextBlock.Appearance.Shadow.Position = chartAxis.Appearance.TextAppearance.Shadow.Position;
					}
					if (i != 0 && chartAxisItem3.chartAxisItemType == ChartAxisItemType.SegmentStart)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							chartAxisItem2.Appearance.Position.Y -= chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 4f;
						}
						else
						{
							chartAxisItem2.Appearance.Position.X += chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f;
						}
					}
					if (i != count - 1 && chartAxisItem3.chartAxisItemType == ChartAxisItemType.SegmentEnd)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							chartAxisItem2.Appearance.Position.Y += chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 4f;
						}
						else
						{
							chartAxisItem2.Appearance.Position.X -= chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f;
						}
					}
					chartAxis.CorrectAxisItemPosition(chartAxisItem2.Appearance.Position);
					this.RenderElement(chartAxisItem2);
					this.Render(chartAxisItem2);
					if (chartAxisItem3.Appearance.Position.AlignedPosition == chartAxis.Appearance.LabelAppearance.Position.AlignedPosition)
					{
						chartAxisItem3.Appearance.Position.AlignedPosition = chartAxisItem.Appearance.Position.AlignedPosition;
					}
				}
			}
		}

		// Token: 0x0600E948 RID: 59720 RVA: 0x00347DBC File Offset: 0x00345FBC
		private void RenderAxis(ChartAxis chartAxis)
		{
			if (chartAxis == null || !chartAxis.Parent.Visible || !chartAxis.IsVisible() || chartAxis.Parent.EmptySeriesMessage.IsVisible())
			{
				return;
			}
			this.RenderAxisItems(chartAxis);
			this.RenderAxisLabel(chartAxis.AxisLabel);
			if (chartAxis.AxisType == ChartAxisType.XAxis)
			{
				ChartPlotArea parent = chartAxis.Parent;
				if (!parent.XAxis.IsVisible())
				{
					return;
				}
				using (Pen pen = RenderEngine.GetPen(parent.XAxis.Appearance))
				{
					this.graphics.DrawLine(pen, parent.XAxis.StartPoint, parent.XAxis.EndPoint);
					return;
				}
			}
			this.RenderYAxis((ChartYAxis)chartAxis);
		}

		// Token: 0x0600E949 RID: 59721 RVA: 0x00347E80 File Offset: 0x00346080
		private void RenderYAxis(ChartYAxis yAxis)
		{
			if (yAxis.IsVisible())
			{
				using (Pen pen = RenderEngine.GetPen(yAxis.Appearance))
				{
					if (yAxis.Segments.Count > 1 && yAxis.ScaleBreaks.Enabled)
					{
						using (IEnumerator<AxisSegment> enumerator = yAxis.Segments.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AxisSegment axisSegment = enumerator.Current;
								this.graphics.DrawLine(pen, axisSegment.StartPoint, axisSegment.EndPoint);
							}
							goto IL_8B;
						}
					}
					this.graphics.DrawLine(pen, yAxis.StartPoint, yAxis.EndPoint);
					IL_8B:;
				}
			}
		}

		// Token: 0x0600E94A RID: 59722 RVA: 0x00347F40 File Offset: 0x00346140
		private void RenderAxisLabel(ChartLabel axisLabel)
		{
			if (axisLabel.IsVisible() && axisLabel.Visible)
			{
				this.RenderElement(axisLabel);
				this.Render(axisLabel);
				axisLabel.OnRender();
			}
		}

		// Token: 0x0600E94B RID: 59723 RVA: 0x00347F68 File Offset: 0x00346168
		private void RenderMarkedZone(ChartMarkedZone zone, ChartXAxis chartXAxis, ChartYAxis chartYAxis)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!Style.IsVisible(zone) || this.chart.OnlyPieSeries() || !chartYAxis.IsVisible())
			{
				return;
			}
			MarkedZoneType zoneType = zone.GetZoneType();
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			switch (this.chart.SeriesOrientation)
			{
			case ChartSeriesOrientation.Vertical:
				switch (zoneType)
				{
				case MarkedZoneType.Horizontal:
					num = chartYAxis.PlotRect.Left;
					num2 = chartYAxis.GetCoordinate(zone.ValueStartY);
					num3 = chartYAxis.PlotRect.Right;
					num4 = chartYAxis.GetCoordinate(zone.ValueEndY);
					break;
				case MarkedZoneType.Vertical:
					num = chartXAxis.GetCoordinate(zone.ValueStartX);
					num2 = chartXAxis.PlotRect.Top;
					num3 = chartXAxis.GetCoordinate(zone.ValueEndX);
					num4 = chartXAxis.PlotRect.Bottom;
					break;
				case MarkedZoneType.Rectangular:
					num = chartXAxis.GetCoordinate(zone.ValueStartX);
					num2 = chartYAxis.GetCoordinate(zone.ValueStartY);
					num3 = chartXAxis.GetCoordinate(zone.ValueEndX);
					num4 = chartYAxis.GetCoordinate(zone.ValueEndY);
					break;
				}
				break;
			case ChartSeriesOrientation.Horizontal:
				switch (zoneType)
				{
				case MarkedZoneType.Horizontal:
					num = chartYAxis.GetCoordinate(zone.ValueStartY);
					num2 = chartXAxis.PlotRect.Top;
					num3 = chartYAxis.GetCoordinate(zone.ValueEndY);
					num4 = chartXAxis.PlotRect.Bottom;
					break;
				case MarkedZoneType.Vertical:
					num = chartXAxis.PlotRect.Left;
					num2 = chartXAxis.GetCoordinate(zone.ValueStartX);
					num3 = chartYAxis.PlotRect.Right;
					num4 = chartXAxis.GetCoordinate(zone.ValueEndX);
					break;
				case MarkedZoneType.Rectangular:
					num = chartYAxis.GetCoordinate(zone.ValueStartY);
					num2 = chartXAxis.GetCoordinate(zone.ValueStartX);
					num3 = chartYAxis.GetCoordinate(zone.ValueEndY);
					num4 = chartXAxis.GetCoordinate(zone.ValueEndX);
					break;
				}
				break;
			}
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				if ((num == num3 || num2 == num4) && (zone.ValueStartX != 0.0 || zone.ValueEndX != 0.0 || zone.ValueStartY != 0.0 || zone.ValueEndY != 0.0))
				{
					graphicsPath.AddLine(num, num2, num3, num4);
				}
				else
				{
					RectangleF rect = new RectangleF(Math.Min(num, num3), Math.Min(num2, num4), Math.Abs(num3 - num), Math.Abs(num4 - num2));
					graphicsPath.AddRectangle(rect);
					ShadowStyle shadowStyle = (ShadowStyle)Style.GetStyleProperty(zone, StyleProperties.Shadow);
					if (shadowStyle.Distance > 0f)
					{
						ShadowManager.DrawPolygonShadow(graphicsPath, this.graphics, Convert.ToInt32(this.chart.Appearance.Dimensions.Width.PixelValue), Convert.ToInt32(this.chart.Appearance.Dimensions.Height.PixelValue), Convert.ToInt32(shadowStyle.Distance), shadowStyle.Color, shadowStyle.Blur, shadowStyle.Position);
					}
					this.graphics.FillPath(this.GetBrush((FillStyle)Style.GetStyleProperty(zone, StyleProperties.FillStyle), rect), graphicsPath);
				}
				this.graphics.DrawPath(RenderEngine.GetPen((StyleBorder)Style.GetStyleProperty(zone, StyleProperties.Border)), graphicsPath);
				RectangleF bounds = graphicsPath.GetBounds();
				if (bounds.Width > 0f || bounds.Height > 0f)
				{
					ExtendedLabel extendedLabel = new ExtendedLabel();
					extendedLabel.Container = this.chart.PlotArea;
					extendedLabel.Appearance.Position.X = bounds.Location.X - chartXAxis.PlotRect.X;
					extendedLabel.Appearance.Position.Y = bounds.Location.Y - chartXAxis.PlotRect.Y;
					extendedLabel.Appearance.Dimensions.SetDimensions(bounds.Width, bounds.Height);
					zone.Label.Container = extendedLabel;
					if (zone.Label.Appearance.Position.Auto)
					{
						zone.Label.Appearance.Position.X = (zone.Label.Appearance.Position.Y = 0f);
					}
					if (zone.Label.TextBlock.textBlockWrapContext == null)
					{
						zone.Label.TextBlock.textBlockWrapContext = new WrapContext(extendedLabel.Appearance.Dimensions, WrapType.FixedWidth);
					}
					zone.Label.CalculatePosition(this);
					this.CalculateElementsForRender(zone.Label);
				}
			}
		}

		// Token: 0x0600E94C RID: 59724 RVA: 0x0034846C File Offset: 0x0034666C
		internal static void ChangePlaces(ref PointF point)
		{
			float x = point.X;
			point.X = point.Y;
			point.Y = x;
		}

		// Token: 0x0600E94D RID: 59725 RVA: 0x00348494 File Offset: 0x00346694
		private void DrawGrids(PointF[] gridPoints, Pen pen)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (gridPoints != null)
			{
				int num = gridPoints.Length;
				int num2 = num * 2;
				byte[] array = new byte[num2];
				PointF[] array2 = new PointF[num2];
				int num3 = 0;
				float pixelValue = plotArea.Appearance.Dimensions.Width.PixelValue;
				float pixelValue2 = plotArea.Appearance.Dimensions.Height.PixelValue;
				float x = plotArea.Appearance.Position.X;
				float y = plotArea.Appearance.Position.Y;
				if (pixelValue2 > 0f && pixelValue > 0f)
				{
					for (int i = 0; i < num; i++)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							array2[num3++] = new PointF(x, gridPoints[i].Y);
							array2[num3] = new PointF(x + pixelValue, gridPoints[i].Y);
							array[num3++] = 1;
						}
						else
						{
							array2[num3++] = new PointF(gridPoints[i].X, y);
							array2[num3] = new PointF(gridPoints[i].X, y + pixelValue2);
							array[num3++] = 1;
						}
					}
					for (int j = 0; j < num2; j++)
					{
						if (array[j] == 0 && j + 1 < num2)
						{
							this.graphics.DrawLine(pen, array2[j], array2[j + 1]);
						}
					}
				}
			}
		}

		// Token: 0x0600E94E RID: 59726 RVA: 0x00348650 File Offset: 0x00346850
		private void DrawTicks(PointF[] tickPoints, int tickLength, Pen pen)
		{
			if (tickPoints != null)
			{
				List<PointF> list = new List<PointF>();
				for (int i = tickPoints.Length - 1; i >= 0; i--)
				{
					if (tickPoints[i].X > 0f && tickPoints[i].Y > 0f)
					{
						list.Add(tickPoints[i]);
					}
				}
				tickPoints = list.ToArray();
				int num = tickPoints.Length;
				if (num == 0)
				{
					return;
				}
				ChartPlotArea plotArea = this.chart.PlotArea;
				byte[] array = new byte[num * 2];
				PointF[] array2 = new PointF[num * 2];
				int num2 = 0;
				for (int j = 0; j < num; j++)
				{
					if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						array2[num2++] = new PointF(tickPoints[j].X - (float)tickLength, tickPoints[j].Y);
						array2[num2] = tickPoints[j];
						array[num2++] = 1;
					}
					else
					{
						array2[num2++] = new PointF(tickPoints[j].X, tickPoints[j].Y + (float)tickLength);
						array2[num2] = tickPoints[j];
						array[num2++] = 1;
					}
				}
				using (GraphicsPath graphicsPath = new GraphicsPath(array2, array))
				{
					this.graphics.DrawPath(pen, graphicsPath);
					pen.Dispose();
				}
			}
		}

		// Token: 0x0600E94F RID: 59727 RVA: 0x003487F8 File Offset: 0x003469F8
		private void DrawTicks()
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!Style.IsVisible(plotArea))
			{
				return;
			}
			this.DrawTicks(plotArea.YAxis);
			this.DrawTicks(plotArea.YAxis2);
			this.DrawTicks(plotArea.XAxis);
		}

		// Token: 0x0600E950 RID: 59728 RVA: 0x00348840 File Offset: 0x00346A40
		private void DrawTicks(ChartXAxis axis)
		{
			if (axis.TickPoints != null && axis.TickPoints.Length > 0 && axis.IsVisible() && axis.Appearance.MajorTick.IsVisible() && axis.IsMajorTickVisible)
			{
				using (Pen pen = RenderEngine.GetPen(axis.Appearance.MajorTick))
				{
					using (GraphicsPath graphicsPath = new GraphicsPath(axis.TickPoints, axis.TickPointsTypes))
					{
						this.graphics.DrawPath(pen, graphicsPath);
					}
				}
			}
		}

		// Token: 0x0600E951 RID: 59729 RVA: 0x003488E8 File Offset: 0x00346AE8
		private void DrawTicks(ChartYAxis axis)
		{
			if (axis.IsVisible())
			{
				if (axis.Appearance.MajorTick.IsVisible() && axis.IsMajorTickVisible)
				{
					this.DrawTicks(axis.MajorPoints, axis.Appearance.MajorTick.Length, RenderEngine.GetPen(axis.Appearance.MajorTick));
				}
				if (axis.Appearance.MinorTick.IsVisible() && axis.IsMinorTickVisible)
				{
					this.DrawTicks(axis.MinorPoints, axis.Appearance.MinorTick.Length, RenderEngine.GetPen(axis.Appearance.MinorTick));
				}
			}
		}

		// Token: 0x0600E952 RID: 59730 RVA: 0x0034898C File Offset: 0x00346B8C
		private void DrawGrids()
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!Style.IsVisible(plotArea))
			{
				return;
			}
			this.graphics.SetClip(this.GetRenderRegion(plotArea), CombineMode.Replace);
			this.DrawGrids(plotArea.YAxis);
			this.DrawGrids(plotArea.YAxis2);
			this.DrawGrids(plotArea.XAxis);
			this.ResetClip();
		}

		// Token: 0x0600E953 RID: 59731 RVA: 0x003489EC File Offset: 0x00346BEC
		private void DrawGrids(ChartYAxis axis)
		{
			bool axisVisible = axis.IsVisible();
			bool flag = axis.Appearance.MajorGridLines.ShouldRender(axisVisible);
			bool flag2 = axis.Appearance.MinorGridLines.ShouldRender(axisVisible);
			if (flag)
			{
				this.DrawGrids(axis.MajorPoints, RenderEngine.GetPen(axis.Appearance.MajorGridLines));
			}
			if (flag2)
			{
				this.DrawGrids(axis.MinorPoints, RenderEngine.GetPen(axis.Appearance.MinorGridLines));
			}
		}

		// Token: 0x0600E954 RID: 59732 RVA: 0x00348A64 File Offset: 0x00346C64
		private void DrawGrids(ChartXAxis axis)
		{
			if (axis.GridPoints != null && axis.GridPoints.Length > 0 && axis.Appearance.MajorGridLines.IsVisible())
			{
				using (GraphicsPath graphicsPath = new GraphicsPath(axis.GridPoints, axis.GridPointsTypes))
				{
					this.graphics.DrawPath(RenderEngine.GetPen(axis.Appearance.MajorGridLines), graphicsPath);
				}
			}
		}

		// Token: 0x0600E955 RID: 59733 RVA: 0x00348AE0 File Offset: 0x00346CE0
		private void RenderTextBlock(TextBlock textBlock)
		{
			if (!textBlock.IsVisible)
			{
				return;
			}
			if (textBlock.Container != null)
			{
				this.graphics.SetClip(this.GetRenderRegion(textBlock.Container), CombineMode.Replace);
			}
			GraphicsPath graphicsPath = null;
			this.GetRotationAngle(textBlock, ref graphicsPath);
			Position position = RenderEngine.LocalToGlobal(textBlock);
			RectangleF layoutRectangle = new RectangleF((float)Math.Round((double)(position.X + textBlock.Appearance.Dimensions.Paddings.Left.PixelValue)), (float)Math.Round((double)(position.Y + textBlock.Appearance.Dimensions.Paddings.Top.PixelValue)), textBlock.Appearance.Dimensions.Width.PixelValue, textBlock.Appearance.Dimensions.Height.PixelValue);
			this.graphics.DrawString(textBlock.VisibleText, textBlock.Appearance.TextProperties.Font, new SolidBrush(textBlock.Appearance.TextProperties.Color), layoutRectangle, textBlock.Appearance.StringFormat);
			this.graphics.ResetTransform();
			textBlock.textBlockWrappedText = textBlock.Text;
			this.ResetClip();
		}

		// Token: 0x0600E956 RID: 59734 RVA: 0x00348C09 File Offset: 0x00346E09
		private void RenderElement(IOrdering element)
		{
			this.RenderElement(element, true, true);
		}

		// Token: 0x0600E957 RID: 59735 RVA: 0x00348C14 File Offset: 0x00346E14
		private void RenderElement(IOrdering element, ChartSeriesItem item)
		{
			this.RenderElement(element, true, true);
			IActiveRegion activeRegion = element as IActiveRegion;
			if (activeRegion != null && item != null)
			{
				if (item.ActiveRegion.Region == null)
				{
					item.ActiveRegion.Region = activeRegion.ActiveRegion.Region;
					return;
				}
				item.ActiveRegion.activeRegionList.Add(activeRegion.ActiveRegion.Region);
			}
		}

		// Token: 0x0600E958 RID: 59736 RVA: 0x00348C78 File Offset: 0x00346E78
		private void RenderElement(IOrdering element, bool withFill, bool withBorder)
		{
			Dimensions dimensions = (Dimensions)Style.GetStyleProperty(element, StyleProperties.Dimensions);
			if (dimensions == null || dimensions.IsZero())
			{
				return;
			}
			string text = (string)Style.GetStyleProperty(element, StyleProperties.Figure);
			Position position = RenderEngine.LocalToGlobal(element);
			Corners corners = (Corners)Style.GetStyleProperty(element, StyleProperties.Corners);
			bool flag = string.Compare(text, "Rectangle", true) == 0;
			GraphicsPath graphicsPath;
			if (flag)
			{
				graphicsPath = RenderEngine.GetRoundArea(corners, position.X, position.Y, dimensions.Width.PixelValue, dimensions.Height.PixelValue);
				if (graphicsPath.PointCount == 4)
				{
					float? num = Style.GetStyleProperty(element, StyleProperties.RotationAngle) as float?;
					if (num != null && num.Value % 90f == 0f)
					{
						this.graphics.SmoothingMode = SmoothingMode.Default;
					}
				}
			}
			else
			{
				graphicsPath = FiguresCollection.GetPath(text);
				if (graphicsPath == null)
				{
					graphicsPath = FiguresCollection.GetPath(text, this.chart);
				}
				graphicsPath = RenderEngine.ScaleTo(graphicsPath, dimensions.Width.PixelValue, dimensions.Height.PixelValue);
				graphicsPath = RenderEngine.MoveTo(graphicsPath, position.X, position.Y);
			}
			if (element.Container != null)
			{
				this.graphics.SetClip(this.GetRenderRegion(element.Container), CombineMode.Replace);
			}
			bool flag2 = false;
			ShadowStyle shadowStyle = (ShadowStyle)Style.GetStyleProperty(element, StyleProperties.Shadow);
			if (element is ChartPlotArea && shadowStyle.Distance <= 0f)
			{
				this.graphics.SetClip(this.GetRenderRegion(element), CombineMode.Intersect);
				flag2 = true;
			}
			float rotationAngle = this.GetRotationAngle(element, ref graphicsPath);
			IActiveRegion activeRegion = element as IActiveRegion;
			if (activeRegion != null)
			{
				activeRegion.ActiveRegion.Region = graphicsPath;
			}
			if (shadowStyle.Distance > 0f && withFill)
			{
				ShadowManager.DrawPolygonShadow(graphicsPath, this.graphics, Convert.ToInt32(this.chart.Appearance.Dimensions.Width.PixelValue), Convert.ToInt32(this.chart.Appearance.Dimensions.Height.PixelValue), Convert.ToInt32(shadowStyle.Distance), shadowStyle.Color, shadowStyle.Blur, shadowStyle.Position);
			}
			if (withFill)
			{
				FillStyle fillStyle = (FillStyle)Style.GetStyleProperty(element, StyleProperties.FillStyle);
				FillStyle fillStyle2 = (FillStyle)fillStyle.Clone();
				fillStyle2.FillSettings.GradientAngle += rotationAngle;
				this.graphics.FillPath(this.GetBrush(fillStyle2, graphicsPath.GetBounds()), graphicsPath);
			}
			if (withBorder)
			{
				if (flag2)
				{
					this.ResetClip();
				}
				StyleBorder styleBorder = (StyleBorder)Style.GetStyleProperty(element, StyleProperties.Border);
				if (styleBorder.IsVisible())
				{
					this.graphics.DrawPath(RenderEngine.GetPen(styleBorder), graphicsPath);
				}
			}
			if (element is TextBlock)
			{
				this.graphics.ResetTransform();
			}
			this.graphics.SmoothingMode = this.chart.GetImageQuality();
			this.ResetClip();
		}

		// Token: 0x0600E959 RID: 59737 RVA: 0x00348F48 File Offset: 0x00347148
		private void RenderChart()
		{
			if (this.chart.Appearance.Dimensions.IsZero())
			{
				return;
			}
			int num = (int)(this.chart.Appearance.Border.Width / 2f);
			int num2 = (int)(this.chart.Appearance.Border.Width / 2f);
			int num3 = (int)(this.chart.Appearance.Dimensions.Width.PixelValue - this.chart.Appearance.Border.Width);
			int num4 = (int)(this.chart.Appearance.Dimensions.Height.PixelValue - this.chart.Appearance.Border.Width);
			if (this.chart.Appearance.Corners.IsRectangle)
			{
				RectangleF bounds = new RectangleF((float)num, (float)num2, (float)num3, (float)num4);
				this.graphics.FillRectangle(this.GetBrush(this.chart.Appearance.FillStyle, bounds), bounds);
				if (this.chart.Appearance.Border.IsVisible())
				{
					this.graphics.DrawRectangle(RenderEngine.GetPen(this.chart.Appearance.Border, PenAlignment.Outset), num, num2, num3, num4);
					return;
				}
			}
			else
			{
				GraphicsPath roundArea = RenderEngine.GetRoundArea(this.chart.Appearance.Corners, (float)num, (float)num2, (float)num3, (float)num4);
				RectangleF bounds = roundArea.GetBounds();
				this.graphics.FillPath(this.GetBrush(this.chart.Appearance.FillStyle, bounds), roundArea);
				if (this.chart.Appearance.Border.IsVisible())
				{
					this.graphics.DrawPath(RenderEngine.GetPen(this.chart.Appearance.Border, PenAlignment.Outset), roundArea);
				}
				roundArea.Dispose();
			}
		}

		// Token: 0x0600E95A RID: 59738 RVA: 0x00349124 File Offset: 0x00347324
		private void RenderChartDataTableBorder(ChartDataTable dataTable)
		{
			float pixelValue = dataTable.Appearance.Dimensions.Width.PixelValue;
			float pixelValue2 = dataTable.Appearance.Dimensions.Height.PixelValue;
			PointF[] array = new PointF[5];
			GraphicsPath graphicsPath = new GraphicsPath();
			using (Pen pen = RenderEngine.GetPen(dataTable.Appearance.Border, PenAlignment.Center))
			{
				float num = dataTable.Appearance.Position.X;
				float num2 = dataTable.Appearance.Position.Y;
				if (dataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative)
				{
					float num3 = dataTable.PlotArea.Appearance.Position.Y + dataTable.PlotArea.Appearance.Dimensions.Height.PixelValue;
					this.graphics.SetClip(new RectangleF(0f, (float)Math.Round((double)num3), this.chart.Appearance.Dimensions.Width.PixelValue, this.chart.Appearance.Dimensions.Height.PixelValue - num3));
				}
				array[0] = new PointF((float)Math.Round((double)(num + dataTable.SizesW[0])), (float)Math.Round((double)num2));
				array[4] = new PointF((float)Math.Round((double)num), (float)Math.Round((double)(num2 + dataTable.SizesH[0])));
				num += pixelValue;
				array[1] = new PointF((float)Math.Round((double)num), (float)Math.Round((double)num2));
				num2 += pixelValue2;
				array[2] = new PointF((float)Math.Round((double)num), (float)Math.Round((double)num2));
				array[3] = new PointF((float)Math.Round((double)dataTable.Appearance.Position.X), (float)Math.Round((double)num2));
				graphicsPath.AddLine(array[0], array[1]);
				graphicsPath.AddLine(array[1], array[2]);
				graphicsPath.AddLine(array[2], array[3]);
				graphicsPath.AddLine(array[3], array[4]);
				graphicsPath.AddLine(array[4], new PointF(array[0].X, array[4].Y));
				if (!dataTable.Appearance.DrawVerticalLines)
				{
					graphicsPath.AddLine(new PointF(array[0].X, array[3].Y), array[0]);
				}
				else
				{
					graphicsPath.AddLine(new PointF(array[0].X, array[4].Y), array[0]);
				}
				if (!dataTable.Appearance.DrawHorizontalLines && !dataTable.Appearance.DrawVerticalLines)
				{
					graphicsPath.AddLine(new PointF(array[0].X, array[4].Y), array[0]);
					graphicsPath.AddLine(new PointF(array[0].X, array[4].Y), new PointF(array[0].X, array[3].Y));
				}
				graphicsPath.CloseFigure();
				this.graphics.SetClip(this.GetRenderRegion(this.chart), CombineMode.Intersect);
				ShadowManager.DrawPolygonShadow(graphicsPath, this.graphics, (int)this.chart.Appearance.Dimensions.Width.PixelValue, (int)this.chart.Appearance.Dimensions.Height.PixelValue, (int)dataTable.Appearance.Shadow.Distance, dataTable.Appearance.Shadow.Color, dataTable.Appearance.Shadow.Blur, dataTable.Appearance.Shadow.Position);
				this.graphics.FillPath(this.GetBrush(dataTable.Appearance.FillStyle, graphicsPath.GetBounds()), graphicsPath);
				this.graphics.DrawPath(pen, graphicsPath);
				this.graphics.ResetClip();
			}
		}

		// Token: 0x0600E95B RID: 59739 RVA: 0x003495B8 File Offset: 0x003477B8
		private void RenderChartDataTable(ChartDataTable dataTable)
		{
			if (!dataTable.IsVisible || !dataTable.PlotArea.Visible || dataTable.PlotArea.EmptySeriesMessage.IsVisible())
			{
				return;
			}
			this.RenderChartDataTableBorder(dataTable);
			StyleBorder styleBorder = (StyleBorder)dataTable.Appearance.Border.Clone();
			styleBorder.Visible = true;
			int num = dataTable.Data.Length;
			float width = dataTable.Appearance.Border.Width;
			float pixelValue = dataTable.Appearance.Dimensions.Paddings.Top.PixelValue;
			float pixelValue2 = dataTable.Appearance.Dimensions.Paddings.Bottom.PixelValue;
			float pixelValue3 = dataTable.Appearance.Dimensions.Paddings.Left.PixelValue;
			float pixelValue4 = dataTable.Appearance.Dimensions.Paddings.Right.PixelValue;
			ChartSeriesCollection chartSeriesCollection = dataTable.PlotArea.SeriesCollection();
			if (num > 0)
			{
				PointF pointF = new PointF(dataTable.Appearance.Position.X, dataTable.Appearance.Position.Y);
				Pen pen = RenderEngine.GetPen(styleBorder);
				for (int i = 0; i < num; i++)
				{
					pointF.X = dataTable.Appearance.Position.X;
					pointF.Y += ((i > 1 && width > 1f) ? width : 0f);
					if (dataTable.Appearance.DrawHorizontalLines && i > 0)
					{
						this.graphics.SetClip(this.GetRenderRegion(this.chart), CombineMode.Replace);
						if (i == 1)
						{
							this.graphics.DrawLine(pen, new PointF((float)Math.Round((double)(pointF.X + dataTable.SizesW[0])), (float)Math.Round((double)pointF.Y)), new PointF((float)Math.Round((double)(dataTable.Appearance.Dimensions.Width.PixelValue + pointF.X)), (float)Math.Round((double)pointF.Y)));
						}
						else
						{
							this.graphics.DrawLine(pen, new PointF((float)Math.Round((double)pointF.X), (float)Math.Round((double)pointF.Y)), new PointF((float)Math.Round((double)(dataTable.Appearance.Dimensions.Width.PixelValue + pointF.X)), (float)Math.Round((double)pointF.Y)));
						}
						this.ResetClip();
					}
					int num2 = dataTable.Data[i].Length;
					for (int j = 0; j < num2; j++)
					{
						if (dataTable.Appearance.DrawVerticalLines && j > 0)
						{
							this.graphics.SetClip(this.GetRenderRegion(this.chart), CombineMode.Replace);
							if (j == 1)
							{
								this.graphics.DrawLine(pen, new PointF((float)Math.Round((double)pointF.X), (float)Math.Round((double)(dataTable.Appearance.Position.Y + dataTable.SizesH[0]))), new PointF((float)Math.Round((double)pointF.X), (float)Math.Round((double)(dataTable.Appearance.Position.Y + dataTable.Appearance.Dimensions.Height.PixelValue))));
							}
							else
							{
								this.graphics.DrawLine(pen, new PointF((float)Math.Round((double)pointF.X), (float)Math.Round((double)dataTable.Appearance.Position.Y)), new PointF((float)Math.Round((double)pointF.X), (float)Math.Round((double)(dataTable.Appearance.Position.Y + dataTable.Appearance.Dimensions.Height.PixelValue))));
							}
							this.ResetClip();
						}
						if (!string.IsNullOrEmpty(dataTable.Data[i][j]))
						{
							PointF point = default(PointF);
							SizeF sizeF = this.graphics.MeasureString(dataTable.Data[i][j], dataTable.Appearance.TextProperties.Font);
							float num3 = sizeF.Width;
							if (j == 0 && dataTable.SeriesMarkers.Count > 0)
							{
								num3 += dataTable.SeriesMarkers[i - 1].Appearance.Dimensions.Width.PixelValue;
							}
							point.X = pointF.X + pixelValue3;
							if (i <= 0 || j != 0)
							{
								switch (dataTable.Appearance.TextHorizontalAlign)
								{
								case ContentHorizontalAlign.Center:
									point.X = pointF.X + (dataTable.SizesW[j] - num3) / 2f;
									break;
								case ContentHorizontalAlign.Right:
									point.X = pointF.X + dataTable.SizesW[j] - pixelValue4 - num3;
									break;
								}
							}
							switch (dataTable.Appearance.TextVerticalAlign)
							{
							case ContentVerticalAlign.Top:
								point.Y = pointF.Y + pixelValue;
								break;
							case ContentVerticalAlign.Middle:
								point.Y = pointF.Y + (dataTable.SizesH[i] - sizeF.Height) / 2f;
								break;
							case ContentVerticalAlign.Bottom:
								point.Y = pointF.Y + dataTable.SizesH[i] - sizeF.Height - pixelValue2;
								break;
							}
							point.Y += dataTable.Appearance.Border.Width / 2f;
							using (Brush brush = new SolidBrush(dataTable.Appearance.TextProperties.Color))
							{
								if (j == 0 && dataTable.SeriesMarkers.Count > 0)
								{
									ChartMarker chartMarker = dataTable.SeriesMarkers[i - 1];
									chartMarker.Appearance.Position.X = point.X + 2f;
									chartMarker.Appearance.Position.Y = point.Y + (sizeF.Height - chartMarker.Appearance.Dimensions.Width.PixelValue) / 2f;
									point.X += chartMarker.Appearance.Dimensions.Width.PixelValue;
									ChartSeries chartSeries = chartSeriesCollection[i - 1];
									using (Pen pen2 = this.GetPen(chartSeries, i - 1, null))
									{
										if (chartSeries.IsLine)
										{
											chartMarker.Appearance.FillStyle.MainColor = pen2.Color;
											chartMarker.Appearance.FillStyle.FillType = FillType.Solid;
										}
										else
										{
											chartMarker.Appearance.styleMarkerFillStyle = this.GetFillStyle(chartSeries, i - 1, null, 0);
											chartMarker.Appearance.Border.Color = pen2.Color;
											chartMarker.Appearance.Border.Width = pen2.Width;
										}
									}
									chartMarker.Container = this.chart;
									this.RenderElement(chartMarker);
									point.X += 3f;
								}
								this.graphics.SetClip(this.GetRenderRegion(this.chart), CombineMode.Replace);
								this.graphics.DrawString(dataTable.Data[i][j], dataTable.Appearance.TextProperties.Font, brush, point);
								this.ResetClip();
							}
						}
						pointF.X += dataTable.SizesW[j];
					}
					pointF.Y += dataTable.SizesH[i];
				}
				pointF.X = (float)Math.Round((double)(width / 2f)) + dataTable.Appearance.Position.X;
			}
			dataTable.OnRender();
		}

		// Token: 0x0600E95C RID: 59740 RVA: 0x00349DB8 File Offset: 0x00347FB8
		private Brush GetAlignedImageBrush(FillStyle fs, int X, int Y, int width, int height, Image img)
		{
			Brush result;
			using (Image image = new Bitmap((int)this.chart.Appearance.Dimensions.Width.PixelValue, (int)this.chart.Appearance.Dimensions.Height.PixelValue))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					PointF pointF = new PointF((float)X, (float)Y);
					switch (fs.FillSettings.ImageAlign)
					{
					case ImageAlignModes.Top:
					case ImageAlignModes.Bottom:
					case ImageAlignModes.Center:
						pointF.X += (float)((width - img.Width) / 2);
						break;
					case ImageAlignModes.Right:
					case ImageAlignModes.TopRight:
					case ImageAlignModes.BottomRight:
						pointF.X += (float)(width - img.Width);
						break;
					}
					switch (fs.FillSettings.ImageAlign)
					{
					case ImageAlignModes.Bottom:
					case ImageAlignModes.BottomRight:
					case ImageAlignModes.BottomLeft:
						pointF.Y += (float)(height - img.Height);
						break;
					case ImageAlignModes.Right:
					case ImageAlignModes.Left:
					case ImageAlignModes.Center:
						pointF.Y += (float)((height - img.Height) / 2);
						break;
					}
					graphics.DrawImage(img, pointF.X, pointF.Y);
					result = new TextureBrush(image);
				}
			}
			return result;
		}

		// Token: 0x0600E95D RID: 59741 RVA: 0x00349F58 File Offset: 0x00348158
		private Brush GetStretchedImageBrush(int X, int Y, int width, int height, Image img)
		{
			Brush result;
			using (Image image = new Bitmap(img, width, height))
			{
				using (Image image2 = new Bitmap((int)this.chart.Appearance.Dimensions.Width.PixelValue, (int)this.chart.Appearance.Dimensions.Height.PixelValue))
				{
					using (Graphics graphics = Graphics.FromImage(image2))
					{
						graphics.DrawImage(image, X, Y);
						result = new TextureBrush(image2);
					}
				}
			}
			return result;
		}

		// Token: 0x0600E95E RID: 59742 RVA: 0x0034A00C File Offset: 0x0034820C
		private static PointF GetBasePoint(RectangleF rect, AlignedPositions pos)
		{
			PointF result;
			if (pos <= AlignedPositions.Center)
			{
				switch (pos)
				{
				case AlignedPositions.None:
				case (AlignedPositions)3:
					break;
				case AlignedPositions.TopLeft:
					result = new PointF(rect.X, rect.Y);
					return result;
				case AlignedPositions.Top:
					result = new PointF(rect.X + rect.Width / 2f, rect.Y);
					return result;
				case AlignedPositions.TopRight:
					result = new PointF(rect.X + rect.Width, rect.Y);
					return result;
				default:
					if (pos == AlignedPositions.Left)
					{
						result = new PointF(rect.X, rect.Y + rect.Height / 2f);
						return result;
					}
					if (pos != AlignedPositions.Center)
					{
					}
					break;
				}
			}
			else if (pos <= AlignedPositions.BottomLeft)
			{
				if (pos == AlignedPositions.Right)
				{
					result = new PointF(rect.X + rect.Width, rect.Y + rect.Height / 2f);
					return result;
				}
				if (pos == AlignedPositions.BottomLeft)
				{
					result = new PointF(rect.X, rect.Y + rect.Height);
					return result;
				}
			}
			else
			{
				if (pos == AlignedPositions.Bottom)
				{
					result = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height);
					return result;
				}
				if (pos == AlignedPositions.BottomRight)
				{
					result = new PointF(rect.X + rect.Width, rect.Y + rect.Height);
					return result;
				}
			}
			result = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
			return result;
		}

		// Token: 0x0600E95F RID: 59743 RVA: 0x0034A1E5 File Offset: 0x003483E5
		private static void GetParentList(IOrdering element, ref List<IOrdering> list)
		{
			if (element.Container is IOrdering)
			{
				list.Add((IOrdering)element.Container);
				RenderEngine.GetParentList((IOrdering)element.Container, ref list);
			}
		}

		// Token: 0x0600E960 RID: 59744 RVA: 0x0034A218 File Offset: 0x00348418
		private float GetRotationAngle(IOrdering elem, ref GraphicsPath drawPath)
		{
			float num = 0f;
			List<IOrdering> list = new List<IOrdering>();
			list.Add(elem);
			RenderEngine.GetParentList(elem, ref list);
			bool flag = drawPath != null;
			if (!flag)
			{
				Position position = RenderEngine.LocalToGlobal(elem);
				Dimensions dimensions = Style.GetStyleProperty(elem, StyleProperties.Dimensions) as Dimensions;
				drawPath = new GraphicsPath();
				if (dimensions != null)
				{
					drawPath.AddRectangle(new Rectangle((int)(position.X + dimensions.Paddings.Left.PixelValue), (int)(position.Y + dimensions.Paddings.Top.PixelValue), (int)dimensions.Width.PixelValue, (int)dimensions.Height.PixelValue));
				}
			}
			foreach (IOrdering element in list)
			{
				float? num2 = Style.GetStyleProperty(element, StyleProperties.RotationAngle) as float?;
				if (num2 != null)
				{
					float value = num2.Value;
					num += value;
					if (value != 0f)
					{
						Dimensions dimensions = Style.GetStyleProperty(element, StyleProperties.Dimensions) as Dimensions;
						Position position2 = Style.GetStyleProperty(element, StyleProperties.Position) as Position;
						if (position2 == null)
						{
							position2 = new Position(AlignedPositions.None, 0f, 0f);
						}
						if (dimensions != null)
						{
							Position position = RenderEngine.LocalToGlobal(element);
							PointF point = new PointF(position.X + dimensions.Width.PixelValue / 2f, position.Y + dimensions.Height.PixelValue / 2f);
							GraphicsPath graphicsPath = new GraphicsPath();
							graphicsPath.AddRectangle(new RectangleF(position.X, position.Y, dimensions.Width.PixelValue, dimensions.Height.PixelValue));
							Matrix matrix = new Matrix();
							matrix.RotateAt(value, point, MatrixOrder.Append);
							RectangleF bounds = graphicsPath.GetBounds();
							PointF basePoint = RenderEngine.GetBasePoint(bounds, position2.AlignedPosition);
							graphicsPath.Transform(matrix);
							RectangleF bounds2 = graphicsPath.GetBounds();
							PointF basePoint2 = RenderEngine.GetBasePoint(bounds2, position2.AlignedPosition);
							float num3 = basePoint.X - basePoint2.X;
							float num4 = basePoint.Y - basePoint2.Y;
							matrix.Translate(num3, num4, MatrixOrder.Append);
							drawPath.Transform(matrix);
							if (!flag)
							{
								float offsetX = this.graphics.Transform.OffsetX;
								float offsetY = this.graphics.Transform.OffsetY;
								this.graphics.DropTranslateTransformDefault();
								this.graphics.TranslateTransform(-point.X, -point.Y, MatrixOrder.Append);
								this.graphics.RotateTransform(value, MatrixOrder.Append);
								this.graphics.TranslateTransform(num3 + offsetX, num4 + offsetY, MatrixOrder.Append);
								this.graphics.TranslateTransform(point.X, point.Y, MatrixOrder.Append);
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600E961 RID: 59745 RVA: 0x0034A51C File Offset: 0x0034871C
		internal void InitializeChartElements()
		{
			if (this.chart.DesignTime)
			{
				this.chart.InitDesignTime();
			}
			this.originalSeries = this.chart.Series;
			this.seriesList = new ChartSeriesCollection(this.originalSeries.Parent);
			foreach (ChartSeries chartSeries in this.originalSeries)
			{
				ChartSeries chartSeries2 = chartSeries.CloneSeries();
				if (chartSeries2.IsXDependentSeriesType)
				{
					chartSeries2.PrepareSeriesByXValues();
					this.seriesList.Add(chartSeries2);
				}
				else
				{
					this.seriesList.Add(chartSeries);
				}
			}
			this.chart.chartSeriesCollection = this.seriesList;
			this.SetOrderingMode();
			this.chart.Series.DefineItemsLabelText();
			this.chart.Legend.BindSeriesToLegend(this);
			this.chart.PlotArea.XAxis.SaveLabelPosition();
			this.chart.PlotArea.YAxis.SaveLabelPosition();
			this.chart.PlotArea.YAxis2.SaveLabelPosition();
			if (this.chart.AutoLayoutWrapper)
			{
				this.chart.PrepareForAutoLayout();
			}
			this.chart.PlotArea.InitializeAxes();
		}

		// Token: 0x0600E962 RID: 59746 RVA: 0x0034A670 File Offset: 0x00348870
		internal void CalculateElementsForRender()
		{
			if (this.chart.AutoLayoutWrapper)
			{
				this.CalculateElementsForRender(this.chart);
				return;
			}
			this.CalculateElementsForRender(this.chart);
		}

		// Token: 0x0600E963 RID: 59747 RVA: 0x0034A698 File Offset: 0x00348898
		internal void ScalePlotArea(float xScale, float yScale)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				float num = xScale;
				xScale = yScale;
				yScale = num;
			}
			if (this.chart.AutoLayoutWrapper)
			{
				plotArea.Appearance.RestoreAutoLayoutMargins();
			}
			plotArea.Appearance.Dimensions.AutoSize = false;
			plotArea.Appearance.Dimensions.Width = Unit.Pixel(plotArea.Appearance.Dimensions.Width.PixelValue * xScale);
			plotArea.Appearance.Dimensions.Height = Unit.Pixel(plotArea.Appearance.Dimensions.Height.PixelValue * yScale);
			plotArea.CalculatePosition(this);
			this.CalculateElementsForRender(plotArea);
		}

		// Token: 0x0600E964 RID: 59748 RVA: 0x0034A758 File Offset: 0x00348958
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal void CalculateElementsForRender(IContainer element)
		{
			IOrderingCollection orderingCollection = new IOrderingCollection();
			orderingCollection.AddVisibleRange(element.OrderList, -1);
			for (int i = 0; i < orderingCollection.Count; i++)
			{
				IOrdering ordering = orderingCollection[i];
				bool flag = Style.IsVisible(ordering);
				if (flag)
				{
					Style.SetPixelValues(ordering, ordering.Container);
				}
				if ((flag || ordering is ChartAxis || ordering is EmptySeriesMessage) && !(ordering is Chart) && !(ordering is BindableLegendItem))
				{
					ChartAxis chartAxis = ordering as ChartAxis;
					if (chartAxis != null)
					{
						this.getAxisItemBoundOnly = false;
						chartAxis.CalculateLayout(this);
						this.CalculateElementsForRender(chartAxis.AxisLabel);
						if (!chartAxis.Parent.Parent.OnlyPieSeries() && (chartAxis.Appearance.MajorGridLines.Visible || chartAxis.Appearance.MinorGridLines.Visible || chartAxis.IsMajorTickVisible || chartAxis.IsMinorTickVisible))
						{
							chartAxis.CalculateGridsAndTicks();
						}
					}
					else
					{
						LayoutElement layoutElement = ordering as LayoutElement;
						if (layoutElement != null)
						{
							layoutElement.CalculatePosition(this);
						}
						IContainer container = ordering as IContainer;
						if (container != null)
						{
							orderingCollection.AddVisibleRange(container.OrderList, i);
						}
					}
				}
			}
		}

		// Token: 0x0600E965 RID: 59749 RVA: 0x0034A883 File Offset: 0x00348A83
		private LayoutZone GetLabelZone(ChartBaseLabel label, bool visible)
		{
			if (visible)
			{
				Style.SetPixelValues(label, label.Container);
				label.CalculatePosition(this);
				return LayoutZone.FromStyle(this.chart.Appearance.Dimensions, label);
			}
			return new LayoutZone();
		}

		// Token: 0x0600E966 RID: 59750 RVA: 0x0034A8B8 File Offset: 0x00348AB8
		internal void CalculateElementsForRender(Chart chart)
		{
			bool flag = chart.ChartTitle.IsVisible();
			bool flag2 = chart.Legend.IsVisible() && chart.Legend.Appearance.Location != LabelLocation.InsidePlotArea;
			bool flag3 = chart.PlotArea.DataTable.IsVisible && chart.PlotArea.DataTable.Appearance.RenderType != TableRenderType.PlotAreaRelative;
			bool flag4 = flag && chart.ShouldApplyTextWrapping(chart.ChartTitle.TextBlock.Appearance.AutoTextWrap) && (chart.ChartTitle.Appearance.Position.AlignedPosition == AlignedPositions.Top || chart.ChartTitle.Appearance.Position.AlignedPosition == AlignedPositions.Bottom);
			string text = "";
			if (flag4)
			{
				text = chart.ChartTitle.TextBlock.Text;
				chart.ChartTitle.TextBlock.Text = "";
				chart.ChartTitle.Measure(this);
			}
			LayoutZone labelZone = this.GetLabelZone(chart.ChartTitle, flag);
			LayoutZone labelZone2 = this.GetLabelZone(chart.Legend, flag2);
			LayoutZone layoutZone = new LayoutZone();
			if (flag3)
			{
				Style.SetPixelValues(chart.PlotArea.DataTable, chart);
				chart.PlotArea.DataTable.Measure(this);
				layoutZone = LayoutZone.FromStyle(chart.Appearance.Dimensions, chart.PlotArea.DataTable);
				chart.PlotArea.DataTable.dataTableShouldCalculate = false;
			}
			LayoutZone.DistributeZones(ref labelZone, ref labelZone2, ref layoutZone);
			if (flag4)
			{
				chart.ChartTitle.TextBlock.Text = text;
				chart.ChartTitle.TextBlock.textBlockWrapContext = new WrapContext(labelZone.Width - chart.ChartTitle.Appearance.Dimensions.Margins.Left.PixelValue - chart.ChartTitle.Appearance.Dimensions.Margins.Right.PixelValue, 1f, WrapType.FixedWidth);
				chart.ChartTitle.Measure(this);
				labelZone = this.GetLabelZone(chart.ChartTitle, flag);
				labelZone2 = this.GetLabelZone(chart.Legend, flag2);
				layoutZone = new LayoutZone();
				if (flag3)
				{
					Style.SetPixelValues(chart.PlotArea.DataTable, chart);
					chart.PlotArea.DataTable.Measure(this);
					layoutZone = LayoutZone.FromStyle(chart.Appearance.Dimensions, chart.PlotArea.DataTable);
					chart.PlotArea.DataTable.dataTableShouldCalculate = false;
				}
				LayoutZone.DistributeZones(ref labelZone, ref labelZone2, ref layoutZone);
			}
			if (flag)
			{
				labelZone.CalculatePosition(chart.ChartTitle, chart.ChartTitle.Appearance.Dimensions, chart.ChartTitle.Appearance.Position);
				this.CalculateElementsForRender(chart.ChartTitle);
			}
			if (flag2)
			{
				labelZone2.CalculatePosition(chart.Legend, chart.Legend.Appearance.Dimensions, chart.Legend.Appearance.Position);
				this.CalculateElementsForRender(chart.Legend);
			}
			if (flag3)
			{
				layoutZone.CalculatePosition(chart.PlotArea.DataTable, chart.PlotArea.DataTable.Appearance.Dimensions, chart.PlotArea.DataTable.Appearance.Position);
			}
			Style.SetPixelValues(chart.PlotArea, chart.PlotArea.Container);
			chart.PlotArea.CalculatePosition(this);
			LayoutZone layoutZone2 = LayoutZone.CreateFromAvailableSpace((DimensionsChart)chart.Appearance.Dimensions, chart.PlotArea, new LayoutZone[]
			{
				labelZone,
				labelZone2,
				layoutZone
			});
			if (chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				chart.PlotArea.XAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Width, 1f, WrapType.FixedWidth);
				chart.PlotArea.YAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Height, 1f, WrapType.FixedWidth);
				chart.PlotArea.YAxis2.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Height, 1f, WrapType.FixedWidth);
			}
			else
			{
				chart.PlotArea.XAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Height, 1f, WrapType.FixedWidth);
				chart.PlotArea.YAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Width, 1f, WrapType.FixedWidth);
				chart.PlotArea.YAxis2.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(layoutZone2.Width, 1f, WrapType.FixedWidth);
			}
			this.CalculateElementsForRender(chart.PlotArea);
			float num = 0f;
			float val = 0f;
			bool flag5 = chart.PlotArea.DataTable.IsVisible && chart.PlotArea.DataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative;
			if (flag5 && chart.PlotArea.DataTable.Data.Length > 0)
			{
				num = chart.PlotArea.DataTable.Appearance.Dimensions.Height.PixelValue + chart.PlotArea.DataTable.Appearance.Dimensions.Margins.Top.PixelValue + chart.PlotArea.DataTable.Appearance.Dimensions.Margins.Bottom.PixelValue + chart.PlotArea.DataTable.Appearance.Border.Width;
				val = chart.PlotArea.DataTable.SizesW[0] + chart.PlotArea.DataTable.Appearance.Border.Width + chart.PlotArea.DataTable.Appearance.Dimensions.Margins.Left.PixelValue;
			}
			float num2;
			float num3;
			float num4;
			float num5;
			if (chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				float val2 = 0f;
				float val3 = 0f;
				if (chart.PlotArea.YAxis.Items.Count > 0)
				{
					val2 = chart.PlotArea.YAxis.Items.Last.GetHeight(true, false) - chart.PlotArea.YAxis.Items.Last.GetHeight() / 2f;
				}
				if (chart.PlotArea.YAxis2.Items.Count > 0)
				{
					val3 = chart.PlotArea.YAxis2.Items.Last.GetHeight(true, false) - chart.PlotArea.YAxis2.Items.Last.GetHeight() / 2f;
				}
				num2 = Math.Max(val2, val3);
				num3 = Math.Max(chart.PlotArea.YAxis.GetWidth() + (float)chart.PlotArea.YAxis.TicksLength, val);
				num4 = chart.PlotArea.XAxis.GetHeight() + (float)chart.PlotArea.XAxis.TicksLength + num;
				num5 = chart.PlotArea.YAxis2.GetWidth() + (float)chart.PlotArea.YAxis2.TicksLength;
			}
			else
			{
				num3 = Math.Max(chart.PlotArea.XAxis.GetWidth() + (float)chart.PlotArea.XAxis.TicksLength, val);
				num4 = chart.PlotArea.YAxis.GetHeight() + (float)chart.PlotArea.YAxis.TicksLength + num;
				num2 = chart.PlotArea.YAxis2.GetHeight();
				float val4 = 0f;
				float val5 = 0f;
				if (chart.PlotArea.YAxis.Items.Count > 0)
				{
					val4 = chart.PlotArea.YAxis.Items.Last.GetWidth(false, true) - chart.PlotArea.YAxis.Items.Last.GetWidth() / 2f;
				}
				if (chart.PlotArea.YAxis2.Items.Count > 0)
				{
					val5 = chart.PlotArea.YAxis2.Items.Last.GetWidth(false, true) - chart.PlotArea.YAxis2.Items.Last.GetWidth() / 2f;
				}
				num5 = Math.Max(val4, val5);
			}
			if (flag5)
			{
				chart.PlotArea.Appearance.Dimensions.Margins.Top = DefaultValues.AUTO_MARGIN_PLOTAREA_TOP.Clone();
				chart.PlotArea.Appearance.Dimensions.Margins.Right = DefaultValues.AUTO_MARGIN_PLOTAREA_RIGHT.Clone();
				chart.PlotArea.Appearance.Dimensions.Margins.Bottom = DefaultValues.AUTO_MARGIN_PLOTAREA_BOTTOM.Clone();
				chart.PlotArea.Appearance.Dimensions.Margins.Left = DefaultValues.AUTO_MARGIN_PLOTAREA_LEFT.Clone();
				Style.SetPixelValues(chart.PlotArea, chart.PlotArea.Container);
			}
			float n = Math.Max(chart.PlotArea.Appearance.Dimensions.Margins.Top.PixelValue, layoutZone2.Y + num2);
			float n2 = Math.Max(chart.PlotArea.Appearance.Dimensions.Margins.Right.PixelValue, chart.Appearance.Dimensions.Width.PixelValue - layoutZone2.X - layoutZone2.Width + num5);
			float n3 = Math.Max(chart.PlotArea.Appearance.Dimensions.Margins.Bottom.PixelValue, chart.Appearance.Dimensions.Height.PixelValue - layoutZone2.Y - layoutZone2.Height + num4);
			float n4 = Math.Max(chart.PlotArea.Appearance.Dimensions.Margins.Left.PixelValue, layoutZone2.X + num3);
			chart.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(Unit.Pixel(n), Unit.Pixel(n2), Unit.Pixel(n3), Unit.Pixel(n4));
			bool autoSize = chart.PlotArea.Appearance.Dimensions.AutoSize;
			if (flag5)
			{
				chart.PlotArea.DataTable.dataTableShouldCalculate = false;
				chart.PlotArea.CalculatePosition(this);
				chart.PlotArea.Appearance.Dimensions.AutoSize = false;
				chart.PlotArea.DataTable.dataTableShouldCalculate = true;
				chart.PlotArea.CalculatePosition(this);
			}
			else
			{
				chart.PlotArea.CalculatePosition(this);
			}
			if (chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				chart.PlotArea.YAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(chart.PlotArea.Appearance.Dimensions.Height.PixelValue, 1f, WrapType.FixedWidth);
				chart.PlotArea.YAxis2.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(chart.PlotArea.Appearance.Dimensions.Height.PixelValue, 1f, WrapType.FixedWidth);
				float height = chart.PlotArea.XAxis.GetHeight();
				this.CalculateElementsForRender(chart.PlotArea);
				chart.PlotArea.Appearance.Dimensions.Margins.Bottom = new Unit(chart.PlotArea.Appearance.Dimensions.Margins.Bottom.PixelValue + (chart.PlotArea.XAxis.GetHeight() - height));
				chart.PlotArea.YAxis.shouldOptimizeMaxLength = true;
				chart.PlotArea.YAxis2.shouldOptimizeMaxLength = true;
			}
			else
			{
				chart.PlotArea.XAxis.AxisLabel.TextBlock.textBlockWrapContext = new WrapContext(chart.PlotArea.Appearance.Dimensions.Height.PixelValue, 1f, WrapType.FixedWidth);
				float height2 = chart.PlotArea.YAxis.GetHeight();
				float height3 = chart.PlotArea.YAxis2.GetHeight();
				this.CalculateElementsForRender(chart.PlotArea);
				chart.PlotArea.Appearance.Dimensions.Margins.Bottom = new Unit(chart.PlotArea.Appearance.Dimensions.Margins.Bottom.PixelValue + (chart.PlotArea.YAxis.GetHeight() - height2));
				chart.PlotArea.Appearance.Dimensions.Margins.Top = new Unit(chart.PlotArea.Appearance.Dimensions.Margins.Top.PixelValue + (chart.PlotArea.YAxis2.GetHeight() - height3));
				chart.PlotArea.XAxis.shouldOptimizeMaxLength = true;
			}
			chart.PlotArea.CalculatePosition(this);
			this.CalculateElementsForRender(chart.PlotArea);
			chart.PlotArea.YAxis.shouldOptimizeMaxLength = false;
			chart.PlotArea.YAxis2.shouldOptimizeMaxLength = false;
			chart.PlotArea.XAxis.shouldOptimizeMaxLength = false;
			chart.PlotArea.DataTable.dataTableShouldCalculate = true;
			chart.PlotArea.Appearance.Dimensions.AutoSize = autoSize;
		}

		// Token: 0x0600E967 RID: 59751 RVA: 0x0034B67C File Offset: 0x0034987C
		private static Metafile CreateMetafile(int width, int height)
		{
			Metafile result;
			using (Bitmap bitmap = new Bitmap(2, 2))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					IntPtr hdc = graphics.GetHdc();
					result = new Metafile(hdc, new Rectangle(0, 0, width, height), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
					graphics.ReleaseHdc();
				}
			}
			return result;
		}

		// Token: 0x0600E968 RID: 59752 RVA: 0x0034B6EC File Offset: 0x003498EC
		internal bool InitGraphics(int width, int height)
		{
			if (this.image != null)
			{
				this.image.Dispose();
			}
			if (this.graphics != null)
			{
				this.graphics.Dispose();
			}
			if (this.chart.ImageFormat == ImageFormat.Emf)
			{
				this.image = RenderEngine.CreateMetafile(width, height);
			}
			else
			{
				try
				{
					this.image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
					if (this.bitmapResolution > 0f)
					{
						(this.image as Bitmap).SetResolution(this.bitmapResolution, this.bitmapResolution);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					if (File.Exists(this.chart.TempImagePath))
					{
						File.Delete(this.chart.TempImagePath);
					}
				}
			}
			this.graphics = new ChartGraphics(Graphics.FromImage(this.image));
			this.graphics.CompositingMode = CompositingMode.SourceOver;
			this.graphics.SmoothingMode = this.chart.GetImageQuality();
			this.graphics.TextRenderingHint = this.chart.GetTextQuality();
			return true;
		}

		// Token: 0x0600E969 RID: 59753 RVA: 0x0034B814 File Offset: 0x00349A14
		internal Image Render()
		{
			return this.Render(false);
		}

		// Token: 0x0600E96A RID: 59754 RVA: 0x0034B81D File Offset: 0x00349A1D
		internal Image Render(bool shouldClone)
		{
			this.RenderChart();
			if (!this.ErrorMessageRendered)
			{
				this.Render(this.chart);
			}
			return this.RenderFinalImage(shouldClone);
		}

		// Token: 0x0600E96B RID: 59755 RVA: 0x0034B840 File Offset: 0x00349A40
		internal Image RenderPlotArea(bool shouldClone)
		{
			if (!this.ErrorMessageRendered)
			{
				this.RenderPlotAreaElements(true, false);
			}
			return this.RenderFinalImage(shouldClone);
		}

		// Token: 0x0600E96C RID: 59756 RVA: 0x0034B85C File Offset: 0x00349A5C
		internal Image RenderChartArea(bool shouldClone, bool withBackground, bool withTitle, bool withLegend, bool withPlotAreaBorder, bool withXAxis, bool withYAxis, bool withYAxis2)
		{
			if (withBackground)
			{
				this.RenderChart();
			}
			if (!this.ErrorMessageRendered)
			{
				if (withTitle)
				{
					this.RenderElement(this.chart.ChartTitle);
					this.Render(this.chart.ChartTitle);
				}
				if (withLegend)
				{
					this.chart.Legend.CalculatePosition(this);
					this.RenderElement(this.chart.Legend);
					this.Render(this.chart.Legend);
				}
				if (withPlotAreaBorder)
				{
					this.RenderElement(this.chart.PlotArea, false, true);
				}
				if (withXAxis)
				{
					this.RenderAxis(this.chart.PlotArea.XAxis);
					this.DrawTicks(this.chart.PlotArea.XAxis);
				}
				if (withYAxis)
				{
					this.RenderAxis(this.chart.PlotArea.YAxis);
					this.DrawTicks(this.chart.PlotArea.YAxis);
				}
				if (withYAxis2)
				{
					this.RenderAxis(this.chart.PlotArea.YAxis2);
					this.DrawTicks(this.chart.PlotArea.YAxis2);
				}
			}
			return this.RenderFinalImage(shouldClone);
		}

		// Token: 0x0600E96D RID: 59757 RVA: 0x0034B988 File Offset: 0x00349B88
		internal Image RenderAxis(bool shouldClone, ChartAxisType axisType)
		{
			if (!this.ErrorMessageRendered)
			{
				switch (axisType)
				{
				case ChartAxisType.XAxis:
					this.RenderAxis(this.chart.PlotArea.XAxis);
					this.DrawTicks(this.chart.PlotArea.XAxis);
					break;
				case ChartAxisType.YAxis:
					this.RenderAxis(this.chart.PlotArea.YAxis);
					this.DrawTicks(this.chart.PlotArea.YAxis);
					break;
				case ChartAxisType.YAxis2:
					this.RenderAxis(this.chart.PlotArea.YAxis2);
					this.DrawTicks(this.chart.PlotArea.YAxis2);
					break;
				}
			}
			return this.RenderFinalImage(shouldClone);
		}

		// Token: 0x170046E6 RID: 18150
		// (get) Token: 0x0600E96E RID: 59758 RVA: 0x0034BA48 File Offset: 0x00349C48
		private bool ErrorMessageRendered
		{
			get
			{
				string text = this.chart.Series.CheckForErrors();
				if (!string.IsNullOrEmpty(text))
				{
					throw new ChartException(text);
				}
				return false;
			}
		}

		// Token: 0x0600E96F RID: 59759 RVA: 0x0034BA78 File Offset: 0x00349C78
		private Image RenderFinalImage(bool shouldClone)
		{
			this.chart.FinalizeDesignTime();
			this.chart.Series.ClearAutoGeneratedItemsLabelText();
			this.chart.RestoreAutoLayoutChanges();
			if (this.graphics != null)
			{
				this.graphics.Dispose();
				this.graphics = null;
			}
			if (shouldClone)
			{
				return (Image)this.image.Clone();
			}
			return this.image;
		}

		// Token: 0x0600E970 RID: 59760 RVA: 0x0034BADF File Offset: 0x00349CDF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E971 RID: 59761 RVA: 0x0034BAF0 File Offset: 0x00349CF0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.graphics != null)
				{
					this.graphics.Dispose();
					this.graphics = null;
				}
				if (this.seriesList != null)
				{
					this.seriesList = null;
				}
				if (this.originalSeries != null)
				{
					this.originalSeries = null;
				}
				if (this.image != null)
				{
					this.image.Dispose();
					this.image = null;
				}
			}
			if (File.Exists(this.chart.TempImagePath))
			{
				File.Delete(this.chart.TempImagePath);
			}
		}

		// Token: 0x0600E972 RID: 59762 RVA: 0x0034BB74 File Offset: 0x00349D74
		public void Render(ChartSeries series, int index)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!Style.IsVisible(plotArea))
			{
				return;
			}
			bool flag = series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f;
			series.Items.ResetActiveRegions();
			switch (series.Type)
			{
			case ChartSeriesType.Bar:
				switch (plotArea.XAxis.OrderingMode)
				{
				case BarOrderingMode.Strict:
					if (flag)
					{
						PopularCollection popularCollection = plotArea.PopularValues.CopyPopList();
						this.renderEngineDrawOnlyShadow = true;
						this.RenderBarSeries(series, index, BarOrderingMode.Strict);
						plotArea.PopularValues = popularCollection.CopyPopList();
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderBarSeries(series, index, BarOrderingMode.Strict);
					return;
				case BarOrderingMode.Classic:
					if (flag)
					{
						this.renderEngineDrawOnlyShadow = true;
						this.RenderBarSeries(series, index, BarOrderingMode.Classic);
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderBarSeries(series, index, BarOrderingMode.Classic);
					return;
				default:
					return;
				}
				break;
			case ChartSeriesType.StackedBar:
			case ChartSeriesType.StackedBar100:
				switch (plotArea.XAxis.OrderingMode)
				{
				case BarOrderingMode.Strict:
					if (flag)
					{
						PopularCollection popularCollection2 = plotArea.PopularValues.CopyPopList();
						this.renderEngineDrawOnlyShadow = true;
						this.RenderStackedBarSeries(series.Type, BarOrderingMode.Strict);
						plotArea.PopularValues = popularCollection2.CopyPopList();
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderStackedBarSeries(series.Type, BarOrderingMode.Strict);
					return;
				case BarOrderingMode.Classic:
					if (flag)
					{
						this.renderEngineDrawOnlyShadow = true;
						this.RenderStackedBarSeries(series.Type, BarOrderingMode.Classic);
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderStackedBarSeries(series.Type, BarOrderingMode.Classic);
					return;
				default:
					return;
				}
				break;
			case ChartSeriesType.Line:
			case ChartSeriesType.Bezier:
			case ChartSeriesType.Spline:
				this.RenderLineSeries(series, index);
				return;
			case ChartSeriesType.Area:
			case ChartSeriesType.SplineArea:
				this.RenderAreaSeries(series, index);
				return;
			case ChartSeriesType.StackedArea:
			case ChartSeriesType.StackedArea100:
			case ChartSeriesType.StackedSplineArea:
			case ChartSeriesType.StackedSplineArea100:
			case ChartSeriesType.StackedLine:
			case ChartSeriesType.StackedSpline:
				this.RenderStackedAreaSeries(series.Type);
				return;
			case ChartSeriesType.Pie:
				this.RenderPieSeries(series, index);
				return;
			case ChartSeriesType.Gantt:
				if (flag)
				{
					this.renderEngineDrawOnlyShadow = true;
					this.RenderGanttSeries(series, index);
				}
				this.renderEngineDrawOnlyShadow = false;
				this.RenderGanttSeries(series, index);
				return;
			case ChartSeriesType.Bubble:
				this.RenderBubbleSeries(series, index);
				return;
			case ChartSeriesType.Point:
				this.RenderPointSeries(series, index);
				return;
			case ChartSeriesType.CandleStick:
				switch (plotArea.XAxis.OrderingMode)
				{
				case BarOrderingMode.Strict:
					if (flag)
					{
						PopularCollection popularCollection3 = plotArea.PopularValues.CopyPopList();
						this.renderEngineDrawOnlyShadow = true;
						this.RenderCandlestickSeries(series, index, BarOrderingMode.Strict);
						plotArea.PopularValues = popularCollection3.CopyPopList();
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderCandlestickSeries(series, index, BarOrderingMode.Strict);
					return;
				case BarOrderingMode.Classic:
					if (flag)
					{
						this.renderEngineDrawOnlyShadow = true;
						this.RenderCandlestickSeries(series, index, BarOrderingMode.Classic);
					}
					this.renderEngineDrawOnlyShadow = false;
					this.RenderCandlestickSeries(series, index, BarOrderingMode.Classic);
					break;
				default:
					return;
				}
				break;
			case (ChartSeriesType)19:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600E973 RID: 59763 RVA: 0x0034BE1C File Offset: 0x0034A01C
		private void RenderBar(ChartSeries series, int index, ChartSeriesItem item, int itemIndex, RectangleF barRect)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!this.renderEngineDrawOnlyShadow)
			{
				using (Pen pen = item.Empty ? this.GetEmptyPen(series, index, item) : this.GetPen(series, index, item))
				{
					GraphicsPath graphicsPath;
					if (pen.Width > 1f)
					{
						float num = pen.Width / 2f;
						RectangleF rect;
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							rect = new RectangleF(barRect.X + num - 0.0001f, barRect.Y + num, barRect.Width - num * 2f + 1f, barRect.Height - num * 2f);
						}
						else
						{
							rect = new RectangleF(barRect.X + num, barRect.Y + num - 0.0001f, barRect.Width - num * 2f, barRect.Height - num * 2f + 1f);
						}
						graphicsPath = this.GetRoundRectangle(item.Appearance.Corners, rect, series);
						item.ActiveRegion.Region = this.GetRoundRectangle(item.Appearance.Corners, barRect, series);
					}
					else
					{
						graphicsPath = (item.ActiveRegion.Region = this.GetRoundRectangle(item.Appearance.Corners, barRect, series));
					}
					using (Brush brush = item.Empty ? this.GetBrush(series.Appearance.EmptyValue.FillStyle, barRect) : this.GetBrush(series, index, item, itemIndex, barRect))
					{
						if (series.Appearance.ShowLabels && item.Label.Appearance.Visible && !item.Empty)
						{
							if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal && barRect.Width == 0f)
							{
								barRect.Width = 1f;
							}
							item.AddLabel(series.GetItemLabel(item), barRect, this);
						}
						ChartAxisVisibleValues chartAxisVisibleValues = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis.VisibleValues : plotArea.YAxis2.VisibleValues;
						bool flag = true;
						switch (chartAxisVisibleValues)
						{
						case ChartAxisVisibleValues.Positive:
							flag = (item.YValue >= 0.0);
							if (series.Type == ChartSeriesType.Gantt)
							{
								flag |= (item.YValue2 >= 0.0);
							}
							break;
						case ChartAxisVisibleValues.Negative:
							flag = (item.YValue <= 0.0);
							if (series.Type == ChartSeriesType.Gantt)
							{
								flag |= (item.YValue2 <= 0.0);
							}
							break;
						}
						if (flag)
						{
							if (graphicsPath.PointCount == 4)
							{
								this.graphics.SmoothingMode = SmoothingMode.Default;
							}
							this.graphics.FillPath(brush, graphicsPath);
							if (pen.Color != Color.Empty)
							{
								this.graphics.DrawPath(pen, graphicsPath);
							}
							this.graphics.SmoothingMode = this.chart.GetImageQuality();
						}
					}
				}
			}
		}

		// Token: 0x0600E974 RID: 59764 RVA: 0x0034C154 File Offset: 0x0034A354
		private void RenderBarShadow(ChartSeries series, ChartSeriesItem item, RectangleF barRect)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f && this.renderEngineDrawOnlyShadow && !item.Empty)
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					using (GraphicsPath roundRectangle = this.GetRoundRectangle(item.Appearance.Corners, barRect, series))
					{
						if (roundRectangle.PointCount > 0)
						{
							bool flag = true;
							switch (RenderEngine.AxisVisibleValues(series, plotArea))
							{
							case ChartAxisVisibleValues.Positive:
								flag = (item.YValue >= 0.0);
								break;
							case ChartAxisVisibleValues.Negative:
								flag = (item.YValue <= 0.0);
								break;
							}
							if (flag)
							{
								graphicsPath.AddPath(roundRectangle, false);
								ShadowManager.DrawPolygonShadow(series, graphicsPath, this.graphics, (int)series.Chart.Appearance.Dimensions.Width.PixelValue, (int)series.Chart.Appearance.Dimensions.Height.PixelValue);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600E975 RID: 59765 RVA: 0x0034C2AC File Offset: 0x0034A4AC
		private void RenderBarSeries(ChartSeries series, int index, BarOrderingMode mode)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!series.Visible || series.Items.Count == 0)
			{
				return;
			}
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			float zeroCoordinate = chartYAxis.GetZeroCoordinate();
			float num = plotArea.XAxis.GetPixelStep();
			(float)(this.chart.Appearance.BarOverlapPercent / 100m);
			float num2 = this.barWidth;
			float num3 = 0f;
			float num4 = 0f;
			if (mode == BarOrderingMode.Classic)
			{
				num3 = plotArea.GetBarStart(series);
			}
			else
			{
				num4 = plotArea.GetBarStart(series, true);
			}
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			Region renderRegion = this.GetRenderRegion(series.YAxisType);
			this.graphics.SetClip(renderRegion, CombineMode.Replace);
			int num5 = 0;
			foreach (ChartSeriesItem chartSeriesItem in series.Items)
			{
				double yvalue = chartSeriesItem.YValue;
				double xvalue = chartSeriesItem.XValue;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num5);
				}
				float num6 = chartYAxis.GetCoordinate(chartSeriesItem.YValue);
				float num7 = Math.Abs(zeroCoordinate - num6);
				if (chartSeriesItem.YValue < 0.0)
				{
					num6 = (float)((int)zeroCoordinate);
				}
				if (mode == BarOrderingMode.Strict)
				{
					int popularityIndex = plotArea.PopularValues.GetPopularityIndex(chartSeriesItem.GetXValue());
					num3 = plotArea.PopularValues[popularityIndex].X + num4 - series.GetBarWidthRatio() * Math.Abs(num) / 2f;
				}
				RectangleF barRect;
				if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					barRect = new RectangleF((float)Math.Round((double)num3), (float)Math.Round((double)num6), (float)Math.Round((double)num2, MidpointRounding.AwayFromZero), (float)Math.Round((double)num7));
				}
				else
				{
					num6 -= num7;
					barRect = new RectangleF((float)Math.Round((double)num6), (float)Math.Round((double)num3), (float)Math.Round((double)num7), (float)Math.Round((double)num2, MidpointRounding.AwayFromZero));
				}
				if (chartSeriesItem.Visible)
				{
					this.RenderBarShadow(series, chartSeriesItem, barRect);
					this.RenderBar(series, index, chartSeriesItem, num5, barRect);
					if (chartSeriesItem.Empty)
					{
						this.RenderEmptyPoint(series, chartSeriesItem, num5, num3 + num2 / 2f);
						this.graphics.SetClip(renderRegion, CombineMode.Replace);
					}
					chartSeriesItem.OnRender();
				}
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.XValue = xvalue;
				num5++;
				if (mode == BarOrderingMode.Classic)
				{
					num3 += num;
				}
			}
			this.ResetClip();
		}

		// Token: 0x0600E976 RID: 59766 RVA: 0x0034C564 File Offset: 0x0034A764
		private void RenderStackedBarSeries(ChartSeriesType seriesType, BarOrderingMode mode)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			ChartSeriesCollection chartSeriesCollection = plotArea.SeriesCollection();
			ChartSeriesCollection seriesCollection = chartSeriesCollection.GetSeriesCollection(seriesType);
			if (seriesCollection.Count == 0)
			{
				return;
			}
			float barOverlapRatio = (float)(this.chart.Appearance.BarOverlapPercent / 100m);
			float zeroCoordinate = plotArea.YAxis.GetZeroCoordinate();
			float zeroCoordinate2 = plotArea.YAxis2.GetZeroCoordinate();
			int maxItemsCount = seriesCollection.GetMaxItemsCount(seriesType);
			float num = this.barWidth;
			float num2 = 0f;
			float num3 = plotArea.XAxis.GetPixelStep();
			Dictionary<double, double> dictionary = new Dictionary<double, double>();
			bool flag = false;
			if (mode == BarOrderingMode.Classic)
			{
				num2 = plotArea.GetBarStart(seriesCollection[0]);
			}
			else
			{
				num = this.barWidthRatio * num3;
				if (seriesType == ChartSeriesType.StackedBar100)
				{
					flag = true;
					if (seriesCollection.Count > 0 && seriesCollection.IsXDepended)
					{
						dictionary = this.chart.Series.GetSumsForStacked(seriesType);
					}
				}
			}
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num3 = -num3;
			}
			for (int i = 0; i < maxItemsCount; i++)
			{
				double num4 = 1.0;
				int num5 = 1;
				if (seriesType == ChartSeriesType.StackedBar100)
				{
					num4 = 0.0;
					num5 = 100;
					if (!flag)
					{
						foreach (ChartSeries chartSeries in seriesCollection)
						{
							if (i < chartSeries.Items.Count)
							{
								num4 += (chartSeries[i].Empty ? 0.0 : Math.Abs(chartSeries[i].YValue));
							}
						}
					}
				}
				double num6 = 0.0;
				double num7 = 0.0;
				foreach (ChartSeries chartSeries2 in seriesCollection)
				{
					ChartYAxis chartYAxis = (chartSeries2.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
					float val = (chartSeries2.YAxisType == ChartYAxisType.Primary) ? zeroCoordinate : zeroCoordinate2;
					Region renderRegion = this.GetRenderRegion(ChartYAxisType.Primary);
					if (chartSeries2.Type == seriesType && i < chartSeries2.Items.Count)
					{
						ChartSeriesItem chartSeriesItem = chartSeries2[i];
						float num8 = num;
						double num9 = 0.0;
						double key = double.IsNaN(chartSeriesItem.XValue) ? 0.0 : chartSeriesItem.XValue;
						if (flag && dictionary.ContainsKey(key))
						{
							num4 = dictionary[key];
						}
						if (!chartSeriesItem.Empty && num4 != 0.0)
						{
							num9 = chartSeriesItem.YValue / num4;
						}
						chartSeriesItem.RelativeValue = num9;
						double num10;
						if (num9 >= 0.0)
						{
							num10 = num6;
						}
						else
						{
							num10 = num7 + num9;
						}
						RectangleF barRect;
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							double num11;
							double num12;
							if (num9 >= 0.0)
							{
								if (mode == BarOrderingMode.Strict)
								{
									num6 = this.StrictParametersModifyForStackedBarsPositive(chartSeriesItem, num9, num, barOverlapRatio, ref num2, ref num8, ref num10, ref num6);
								}
								else
								{
									num6 += num9;
									num8 = this.barWidth;
								}
								num11 = (double)Math.Min(chartYAxis.GetCoordinate(num6 * (double)num5), val);
								num12 = (double)(chartYAxis.GetCoordinate(num10 * (double)num5) - chartYAxis.GetCoordinate(num6 * (double)num5));
							}
							else
							{
								if (mode == BarOrderingMode.Strict)
								{
									this.StrictParametersModifyForStackedBarsNegative(chartSeriesItem, num9, num, barOverlapRatio, ref num2, ref num8, ref num10, ref num7);
								}
								else
								{
									num8 = this.barWidth;
								}
								num11 = (double)Math.Max(chartYAxis.GetCoordinate(num7 * (double)num5), val);
								num12 = (double)(chartYAxis.GetCoordinate(num10 * (double)num5) - chartYAxis.GetCoordinate(num7 * (double)num5));
								if (mode != BarOrderingMode.Strict)
								{
									num7 += num9;
								}
							}
							barRect = new RectangleF((float)Math.Round((double)num2), (float)num11, (float)Math.Round((double)num8, MidpointRounding.AwayFromZero), (float)num12);
						}
						else
						{
							double num13 = 0.0;
							double num11;
							double num12;
							if (num9 >= 0.0)
							{
								if (mode == BarOrderingMode.Strict)
								{
									num13 = this.StrictParametersModifyForStackedBarsPositive(chartSeriesItem, num9, num, barOverlapRatio, ref num2, ref num8, ref num10, ref num6);
								}
								else
								{
									num8 = this.barWidth;
								}
								num11 = (double)Math.Max(chartYAxis.GetCoordinate(num6 * (double)num5), val);
								if (mode != BarOrderingMode.Strict)
								{
									num6 += num9;
								}
								else
								{
									num6 = num13;
								}
								num12 = (double)(chartYAxis.GetCoordinate(num6 * (double)num5) - chartYAxis.GetCoordinate(num10 * (double)num5));
							}
							else
							{
								if (mode == BarOrderingMode.Strict)
								{
									num13 = this.StrictParametersModifyForStackedBarsNegative(chartSeriesItem, num9, num, barOverlapRatio, ref num2, ref num8, ref num10, ref num7);
								}
								else
								{
									num8 = this.barWidth;
								}
								num12 = (double)(chartYAxis.GetCoordinate(num7 * (double)num5) - chartYAxis.GetCoordinate(num10 * (double)num5));
								if (mode != BarOrderingMode.Strict)
								{
									num7 += num9;
								}
								else
								{
									num7 = num13;
								}
								num11 = (double)Math.Min(chartYAxis.GetCoordinate(num7 * (double)num5), val);
							}
							barRect = new RectangleF((float)num11, (float)Math.Round((double)num2), (float)num12, (float)Math.Round((double)num8, MidpointRounding.AwayFromZero));
						}
						this.graphics.SetClip(renderRegion, CombineMode.Replace);
						if (chartSeries2.Visible)
						{
							this.RenderBarShadow(chartSeries2, chartSeriesItem, barRect);
							int index = chartSeriesCollection.IndexOf(chartSeries2);
							this.RenderBar(chartSeries2, index, chartSeriesItem, i, barRect);
						}
						this.ResetClip();
					}
				}
				num2 += num3;
			}
			if (mode == BarOrderingMode.Strict)
			{
				foreach (Popular popular in plotArea.PopularValues)
				{
					popular.YNegative = (popular.YPositive = 0.0);
				}
			}
		}

		// Token: 0x0600E977 RID: 59767 RVA: 0x0034CB38 File Offset: 0x0034AD38
		private void StrictParametersModifyForStackedBars(ChartSeriesItem item, float barOverlapRatio, ref int ind, ref float barX, ref float barWidthPop)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			PopularCollection popularValues = plotArea.PopularValues;
			barWidthPop = plotArea.GetBarWidth();
			ind = popularValues.GetPopularityIndex(item.GetXValue());
			barX = popularValues[ind].X;
		}

		// Token: 0x0600E978 RID: 59768 RVA: 0x0034CB80 File Offset: 0x0034AD80
		private double StrictParametersModifyForStackedBarsPositive(ChartSeriesItem item, double val, float barWidthLocal, float barOverlapRatio, ref float barX, ref float barWidthPop, ref double minV, ref double tvalp)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			PopularCollection popularValues = plotArea.PopularValues;
			int index = -1;
			this.StrictParametersModifyForStackedBars(item, barOverlapRatio, ref index, ref barX, ref barWidthPop);
			minV = popularValues[index].YPositive;
			tvalp = popularValues[index].YPositive;
			popularValues[index].YPositive += val;
			barX -= barWidthLocal / 2f;
			return popularValues[index].YPositive;
		}

		// Token: 0x0600E979 RID: 59769 RVA: 0x0034CBFC File Offset: 0x0034ADFC
		private double StrictParametersModifyForStackedBarsNegative(ChartSeriesItem item, double val, float barWidthLocal, float barOverlapRatio, ref float barX, ref float barWidthPop, ref double minV, ref double tvaln)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			PopularCollection popularValues = plotArea.PopularValues;
			int index = -1;
			this.StrictParametersModifyForStackedBars(item, barOverlapRatio, ref index, ref barX, ref barWidthPop);
			minV = popularValues[index].YNegative + val;
			tvaln = popularValues[index].YNegative;
			popularValues[index].YNegative = minV;
			barX -= barWidthLocal / 2f;
			return popularValues[index].YNegative;
		}

		// Token: 0x0600E97A RID: 59770 RVA: 0x0034CC78 File Offset: 0x0034AE78
		private static PointF[] GetPointsArrayForArea(PointF[] areaPoints)
		{
			PointF[] array = new PointF[areaPoints.Length - 2];
			int num = areaPoints.Length - 1;
			for (int i = 1; i < num; i++)
			{
				array[i - 1] = new PointF(areaPoints[i].X, areaPoints[i].Y);
			}
			return array;
		}

		// Token: 0x0600E97B RID: 59771 RVA: 0x0034CCD0 File Offset: 0x0034AED0
		private static PointF[] GetPointaArrayForAreaPointMarks(PointF[] areaPoints, int maxItemsCount)
		{
			PointF[] array = new PointF[maxItemsCount];
			for (int i = 0; i < maxItemsCount; i++)
			{
				array[i] = new PointF(areaPoints[i].X, areaPoints[i].Y);
			}
			return array;
		}

		// Token: 0x0600E97C RID: 59772 RVA: 0x0034CD1C File Offset: 0x0034AF1C
		private static GraphicsPath[] GetAreaPath(ChartSeries series, int seriesIndex, int maxItemsCount, PointF[] points)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			GraphicsPath graphicsPath2 = new GraphicsPath();
			int count = series.Items.Count;
			int num = points.Length;
			graphicsPath.StartFigure();
			graphicsPath2.StartFigure();
			if (num > 0 && series.IsHasEmptyValues)
			{
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < count - 1; i++)
				{
					ChartSeriesItem chartSeriesItem = series[i];
					ChartSeriesItem chartSeriesItem2 = series[i + 1];
					if (!chartSeriesItem.Empty && !chartSeriesItem2.Empty)
					{
						if (i == 0)
						{
							if (seriesIndex == 0)
							{
								graphicsPath.AddLine(points[0], points[1]);
							}
							if (series.IsStackedArea && seriesIndex > 0)
							{
								graphicsPath.AddLine(points[num - 1], points[0]);
								if (series.IsStackedSplineArea)
								{
									graphicsPath.AddCurve(points, 0, 1, 0.5f);
								}
							}
							else if (series.IsArea)
							{
								graphicsPath.AddLine(points[i + 1], points[i + 2]);
							}
							else
							{
								graphicsPath.AddCurve(points, i + 1, 1, 0.5f);
							}
						}
						else if (series.IsStackedArea)
						{
							if (series.IsStackedNormalArea)
							{
								graphicsPath.AddLine(points[i], points[i + 1]);
							}
							else if (seriesIndex == 0)
							{
								graphicsPath.AddCurve(points, i + 1, 1, 0.5f);
							}
							else
							{
								graphicsPath.AddCurve(points, i, 1, 0.5f);
							}
						}
						else if (series.IsArea)
						{
							graphicsPath.AddLine(points[i + 1], points[i + 2]);
						}
						else
						{
							graphicsPath.AddCurve(points, i + 1, 1, 0.5f);
						}
					}
					else
					{
						if (!chartSeriesItem.Empty && chartSeriesItem2.Empty)
						{
							if (series.IsStackedArea && seriesIndex > 0)
							{
								num3 = i;
								graphicsPath.AddLine(points[i].X, points[i].Y, points[num - 1 - i].X, points[num - 1 - i].Y);
								graphicsPath2.AddLine(points[num - 1 - i].X, points[num - 1 - i].Y, points[i].X, points[i].Y);
							}
							else if (series.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								graphicsPath.AddLine(points[i + 1].X, points[i + 1].Y, points[i + 1].X, points[0].Y);
								graphicsPath2.AddLine(points[i + 1].X, points[0].Y, points[i + 1].X, points[i + 1].Y);
							}
							else
							{
								graphicsPath.AddLine(points[i + 1].X, points[i + 1].Y, points[0].X, points[i + 1].Y);
								graphicsPath2.AddLine(points[0].X, points[i + 1].Y, points[i + 1].X, points[i + 1].Y);
							}
							if (series.IsStackedArea && seriesIndex > 0)
							{
								for (int j = num - 1 - i; j < num - num2 - 1; j++)
								{
									if (series.IsStackedNormalArea)
									{
										graphicsPath.AddLine(points[j].X, points[j].Y, points[j + 1].X, points[j + 1].Y);
									}
									else
									{
										graphicsPath.AddCurve(points, j, 1, 0.5f);
									}
								}
							}
							graphicsPath.CloseFigure();
						}
						if (chartSeriesItem.Empty && !chartSeriesItem2.Empty)
						{
							num2 = i + 1;
							if (i == 0)
							{
								if (series.IsStackedArea && seriesIndex > 0)
								{
									graphicsPath.AddLine(points[num - 2].X, points[num - 2].Y, points[1].X, points[1].Y);
									graphicsPath2.AddLine(points[0].X, points[0].Y, points[num - 1].X, points[num - 1].Y);
								}
								else if (series.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
								{
									graphicsPath.AddLine(points[2].X, points[0].Y, points[2].X, points[2].Y);
								}
								else
								{
									graphicsPath.AddLine(points[0].X, points[2].Y, points[2].X, points[2].Y);
								}
								graphicsPath2.AddLine(points[0].X, points[0].Y, points[1].X, points[1].Y);
							}
							if (series.IsSplineArea)
							{
								if (seriesIndex == 0)
								{
									graphicsPath2.AddCurve(points, i + 1, 1, 0.5f);
								}
								else
								{
									graphicsPath2.AddCurve(points, i, 1, 0.5f);
								}
							}
							if (series.IsStackedArea && seriesIndex > 0)
							{
								graphicsPath.AddLine(points[i + 1].X, points[i + 1].Y, points[num - 2 - i].X, points[num - 2 - i].Y);
								graphicsPath2.AddLine(points[i + 1].X, points[i + 1].Y, points[num - 2 - i].X, points[num - 2 - i].Y);
							}
							else if (series.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								graphicsPath.AddLine(points[i + 2].X, points[0].Y, points[i + 2].X, points[i + 2].Y);
								graphicsPath2.AddLine(points[i + 2].X, points[i + 2].Y, points[i + 2].X, points[0].Y);
							}
							else
							{
								graphicsPath.AddLine(points[0].X, points[i + 2].Y, points[i + 2].X, points[i + 2].Y);
								graphicsPath2.AddLine(points[i + 2].X, points[i + 2].Y, points[0].X, points[i + 2].Y);
							}
							if (series.IsStackedArea && seriesIndex > 0)
							{
								for (int k = num - 2 - i; k < num - num3 - 1; k++)
								{
									if (series.IsStackedNormalArea)
									{
										graphicsPath2.AddLine(points[k].X, points[k].Y, points[k + 1].X, points[k + 1].Y);
									}
									else
									{
										graphicsPath2.AddCurve(points, k, 1, 0.5f);
									}
								}
							}
							graphicsPath2.CloseFigure();
						}
						else if (seriesIndex > 0 && series.IsStackedArea)
						{
							if (series.IsArea)
							{
								graphicsPath2.AddLine(points[i + 1], points[i + 2]);
							}
							else
							{
								graphicsPath2.AddCurve(points, i, 1, 0.5f);
							}
						}
						else if (series.IsArea)
						{
							graphicsPath2.AddLine(points[i + 1], points[i + 2]);
						}
						else
						{
							graphicsPath2.AddCurve(points, i + 1, 1, 0.5f);
						}
					}
				}
				if (series.IsStackedArea && seriesIndex > 0)
				{
					if (series[count - 1].Empty)
					{
						graphicsPath2.AddLine(points[count - 1].X, points[count - 1].Y, points[count].X, points[count].Y);
						for (int l = num - num3 - 1; l > count; l--)
						{
							if (series.IsStackedNormalArea)
							{
								graphicsPath2.AddLine(points[l].X, points[l].Y, points[l + 1].X, points[l + 1].Y);
							}
						}
					}
					else
					{
						graphicsPath.AddLine(points[count - 1].X, points[count - 1].Y, points[count].X, points[count].Y);
						for (int m = count; m < num - num2 - 1; m++)
						{
							if (series.IsStackedNormalArea)
							{
								graphicsPath.AddLine(points[m].X, points[m].Y, points[m + 1].X, points[m + 1].Y);
							}
							else
							{
								graphicsPath.AddCurve(points, m, 1, 0.5f);
							}
						}
					}
				}
				else
				{
					if (series[count - 1].Empty)
					{
						graphicsPath2.AddLine(points[count].X, points[count].Y, points[count].X, points[0].Y);
						graphicsPath2.CloseFigure();
					}
					if (series.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						graphicsPath.AddLine(points[count].X, points[count].Y, points[count].X, points[0].Y);
					}
					else
					{
						graphicsPath.AddLine(points[count].X, points[count].Y, points[0].X, points[count].Y);
					}
				}
			}
			else if (series.IsSplineArea)
			{
				if (seriesIndex == 0 || series.Type == ChartSeriesType.SplineArea)
				{
					PointF[] pointsArrayForArea = RenderEngine.GetPointsArrayForArea(points);
					graphicsPath.AddLine(points[0], points[1]);
					if (pointsArrayForArea.Length > 1)
					{
						graphicsPath.AddCurve(pointsArrayForArea);
					}
					graphicsPath.AddLine(points[maxItemsCount], points[maxItemsCount + 1]);
				}
				else
				{
					graphicsPath.AddLine(points[maxItemsCount * 2 - 1], points[0]);
					PointF[] array = new PointF[maxItemsCount];
					for (int n = 0; n < maxItemsCount; n++)
					{
						array[n] = points[n];
					}
					if (array.Length > 1)
					{
						graphicsPath.AddCurve(array);
					}
					graphicsPath.AddLine(points[maxItemsCount], points[maxItemsCount - 1]);
					PointF[] array2 = new PointF[maxItemsCount];
					for (int num4 = 0; num4 < maxItemsCount; num4++)
					{
						array2[num4] = points[maxItemsCount + num4];
					}
					if (array2.Length > 1)
					{
						graphicsPath.AddCurve(array2);
					}
				}
			}
			else
			{
				graphicsPath.AddLines(points);
			}
			graphicsPath.CloseFigure();
			return new GraphicsPath[]
			{
				graphicsPath,
				graphicsPath2
			};
		}

		// Token: 0x0600E97D RID: 59773 RVA: 0x0034D93C File Offset: 0x0034BB3C
		private GraphicsPath GetAreaItemActiveRegion(PointF firstPoint, PointF secondPoint, float prevValue1, float prevValue2, ChartSeriesOrientation serOrientation)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (serOrientation == ChartSeriesOrientation.Vertical)
			{
				graphicsPath.AddPolygon(new PointF[]
				{
					new PointF(firstPoint.X, prevValue1),
					firstPoint,
					secondPoint,
					new PointF(secondPoint.X, prevValue2)
				});
			}
			else
			{
				graphicsPath.AddPolygon(new PointF[]
				{
					new PointF(prevValue1, firstPoint.Y),
					firstPoint,
					secondPoint,
					new PointF(prevValue2, secondPoint.Y)
				});
			}
			return graphicsPath;
		}

		// Token: 0x0600E97E RID: 59774 RVA: 0x0034DA0C File Offset: 0x0034BC0C
		private void RenderAreaSeries(ChartSeries series, int index)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!series.Visible)
			{
				return;
			}
			Region renderRegion = this.GetRenderRegion(series.YAxisType);
			ChartXAxis xaxis = plotArea.XAxis;
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			float num = xaxis.GetPixelStep();
			float num2 = xaxis.GetStartCoordinate();
			float zeroCoordinate = chartYAxis.GetZeroCoordinate();
			int count = series.Items.Count;
			PointF[] array = new PointF[count + 2];
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			for (int i = 1; i <= count; i++)
			{
				int itemIndex = i - 1;
				ChartSeriesItem chartSeriesItem = series[itemIndex];
				double yvalue = chartSeriesItem.YValue;
				double xvalue = chartSeriesItem.XValue;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, itemIndex);
				}
				float coordinate = chartYAxis.GetCoordinate(chartSeriesItem.YValue);
				PointF pointF;
				if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					if (double.IsNaN(chartSeriesItem.XValue))
					{
						pointF = new PointF((float)Math.Round((double)num2), coordinate);
					}
					else
					{
						pointF = new PointF((float)Math.Round((double)xaxis.GetCoordinate(chartSeriesItem.XValue)), coordinate);
					}
				}
				else if (double.IsNaN(chartSeriesItem.XValue))
				{
					pointF = new PointF(coordinate, (float)Math.Round((double)num2));
				}
				else
				{
					pointF = new PointF(coordinate, (float)Math.Round((double)xaxis.GetCoordinate(chartSeriesItem.XValue)));
				}
				array[i] = pointF;
				if (series.Appearance.ShowLabels && chartSeriesItem.Label.Appearance.Visible && !chartSeriesItem.Empty)
				{
					RectangleF rect = new RectangleF(pointF.X - 1f, pointF.Y - 1f, 2f, 2f);
					chartSeriesItem.AddLabel(series.GetItemLabel(chartSeriesItem), rect, this);
				}
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.XValue = xvalue;
				num2 += num;
				if (i > 1)
				{
					chartSeriesItem.ActiveRegion.Region = this.GetAreaItemActiveRegion(array[i - 1], array[i], zeroCoordinate, zeroCoordinate, plotArea.Chart.SeriesOrientation);
				}
			}
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				array[0] = new PointF(array[1].X, zeroCoordinate);
				array[count + 1] = new PointF(array[count].X, zeroCoordinate);
			}
			else
			{
				array[0] = new PointF(zeroCoordinate, array[1].Y);
				array[count + 1] = new PointF(zeroCoordinate, array[count].Y);
			}
			this.graphics.SetClip(renderRegion, CombineMode.Replace);
			GraphicsPath[] areaPath = RenderEngine.GetAreaPath(series, index, count, array);
			using (GraphicsPath graphicsPath = areaPath[0])
			{
				using (GraphicsPath graphicsPath2 = areaPath[1])
				{
					if (series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f)
					{
						ShadowManager.DrawPolygonShadow(series, graphicsPath, this.graphics, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue);
					}
					this.DrawPolygon(series, index, array, graphicsPath, graphicsPath2);
				}
			}
			this.DrawPointMark(series, RenderEngine.GetPointsArrayForArea(array));
			this.ResetClip();
		}

		// Token: 0x0600E97F RID: 59775 RVA: 0x0034DDEC File Offset: 0x0034BFEC
		private void RenderStackedAreaSeries(ChartSeriesType seriesType)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			ChartSeriesCollection seriesCollection = plotArea.SeriesCollection().GetSeriesCollection(seriesType);
			int maxItemsCount = seriesCollection.GetMaxItemsCount(seriesType);
			int count = seriesCollection.Count;
			double[,] array = new double[count, maxItemsCount];
			double[] array2 = new double[maxItemsCount];
			float zeroCoordinate = plotArea.YAxis.GetZeroCoordinate();
			float zeroCoordinate2 = plotArea.YAxis2.GetZeroCoordinate();
			float num = plotArea.XAxis.GetPixelStep();
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			if (seriesType == ChartSeriesType.StackedArea100 || seriesType == ChartSeriesType.StackedSplineArea100)
			{
				foreach (ChartSeries chartSeries in seriesCollection)
				{
					int num2 = 0;
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						double yvalue = chartSeriesItem.YValue;
						if (chartSeriesItem.Empty)
						{
							chartSeriesItem.YValue = chartSeries.GetEmptyPointYValue(chartSeriesItem, num2);
						}
						array2[num2] += Math.Abs(chartSeriesItem.YValue);
						num2++;
						chartSeriesItem.YValue = yvalue;
					}
				}
			}
			int num3 = 0;
			foreach (ChartSeries chartSeries2 in seriesCollection)
			{
				int num4 = 0;
				foreach (ChartSeriesItem chartSeriesItem2 in chartSeries2.Items)
				{
					double num5 = 0.0;
					double yvalue2 = chartSeriesItem2.YValue;
					if (chartSeriesItem2.Empty)
					{
						chartSeriesItem2.YValue = chartSeries2.GetEmptyPointYValue(chartSeriesItem2, num4);
					}
					if (chartSeries2.IsStacked100)
					{
						if (array2[num4] != 0.0)
						{
							num5 = chartSeriesItem2.YValue / array2[num4];
						}
						chartSeriesItem2.RelativeValue = num5;
						if (num3 > 0)
						{
							array[num3, num4] = array[num3 - 1, num4] + num5;
						}
						else
						{
							array[num3, num4] = num5;
						}
					}
					else
					{
						num5 = chartSeriesItem2.YValue;
						if (num3 > 0)
						{
							array2[num4] += num5;
						}
						else
						{
							array2[num4] = num5;
						}
						array[num3, num4] = array2[num4];
					}
					chartSeriesItem2.YValue = yvalue2;
					num4++;
				}
				num3++;
			}
			num3 = 0;
			foreach (ChartSeries chartSeries3 in seriesCollection)
			{
				ChartYAxis chartYAxis = (chartSeries3.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
				int seriesIndex = plotArea.SeriesCollection().IndexOf(chartSeries3);
				float num6 = plotArea.XAxis.GetStartCoordinate();
				this.graphics.SetClip(this.GetRenderRegion(chartSeries3.YAxisType), CombineMode.Replace);
				int num7 = 1;
				if (chartSeries3.IsStacked100)
				{
					num7 = 100;
				}
				if (num3 == 0 || chartSeries3.IsStackedLine)
				{
					int num8 = chartSeries3.IsStackedLine ? maxItemsCount : (maxItemsCount + 2);
					int num9 = chartSeries3.IsStackedLine ? 0 : 1;
					PointF[] array3 = new PointF[num8];
					if (!chartSeries3.IsStackedLine)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							array3[0].X = (float)Math.Round((double)num6);
							array3[0].Y = ((chartSeries3.YAxisType == ChartYAxisType.Primary) ? zeroCoordinate : zeroCoordinate2);
						}
						else
						{
							array3[0].X = ((chartSeries3.YAxisType == ChartYAxisType.Primary) ? zeroCoordinate : zeroCoordinate2);
							array3[0].Y = (float)Math.Round((double)num6);
						}
					}
					for (int i = num9; i < maxItemsCount + num9; i++)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							array3[i].X = num6;
							array3[i].Y = chartYAxis.GetCoordinate(array[num3, i - num9] * (double)num7);
						}
						else
						{
							array3[i].X = chartYAxis.GetCoordinate(array[num3, i - num9] * (double)num7);
							array3[i].Y = num6;
						}
						num6 += num;
						if (chartSeries3.Appearance.ShowLabels && chartSeries3[i - num9].Label.Visible && !chartSeries3[i - num9].Empty && chartSeries3.Visible)
						{
							RectangleF rect = new RectangleF(array3[i].X - 1f, array3[i].Y - 1f, 3f, 3f);
							ChartSeriesItem chartSeriesItem3 = chartSeries3[i - num9];
							chartSeriesItem3.AddLabel(chartSeries3.GetItemLabel(chartSeriesItem3), rect, this);
						}
						if (i > 1 && !chartSeries3.IsStackedLine)
						{
							chartSeries3.Items[i - num9].ActiveRegion.Region = ((chartSeries3.YAxisType == ChartYAxisType.Primary) ? this.GetAreaItemActiveRegion(array3[i - 1], array3[i], zeroCoordinate, zeroCoordinate, plotArea.Chart.SeriesOrientation) : this.GetAreaItemActiveRegion(array3[i - 1], array3[i], zeroCoordinate2, zeroCoordinate2, plotArea.Chart.SeriesOrientation));
						}
					}
					if (!chartSeries3.IsStackedLine)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							array3[maxItemsCount + 1].X = (float)Math.Round((double)array3[maxItemsCount].X);
							array3[maxItemsCount + 1].Y = ((chartSeries3.YAxisType == ChartYAxisType.Primary) ? zeroCoordinate : zeroCoordinate2);
						}
						else
						{
							array3[maxItemsCount + 1].X = ((chartSeries3.YAxisType == ChartYAxisType.Primary) ? zeroCoordinate : zeroCoordinate2);
							array3[maxItemsCount + 1].Y = (float)Math.Round((double)array3[maxItemsCount].Y);
						}
					}
					if (chartSeries3.Visible)
					{
						if (!chartSeries3.IsStackedLine)
						{
							GraphicsPath[] areaPath = RenderEngine.GetAreaPath(chartSeries3, seriesIndex, maxItemsCount, array3);
							using (GraphicsPath graphicsPath = areaPath[0])
							{
								using (GraphicsPath graphicsPath2 = areaPath[1])
								{
									if (chartSeries3.Appearance.Shadow.Blur >= 0f && chartSeries3.Appearance.Shadow.Distance > 0f)
									{
										ShadowManager.DrawPolygonShadow(chartSeries3, graphicsPath, this.graphics, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue);
									}
									this.DrawPolygon(chartSeries3, seriesIndex, array3, graphicsPath, graphicsPath2);
								}
							}
							this.DrawPointMark(chartSeries3, RenderEngine.GetPointsArrayForArea(array3));
						}
						else
						{
							this.DrawLines(chartSeries3, num3, array3);
							this.DrawPointMark(chartSeries3, array3);
						}
					}
				}
				else
				{
					int num10 = 2 * maxItemsCount;
					PointF[] array4 = new PointF[num10];
					for (int j = 0; j < maxItemsCount; j++)
					{
						if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							array4[j].X = (float)Math.Round((double)num6);
							array4[j].Y = chartYAxis.GetCoordinate(array[num3, j] * (double)num7);
							array4[num10 - j - 1].X = (float)Math.Round((double)num6);
							array4[num10 - j - 1].Y = chartYAxis.GetCoordinate(array[num3 - 1, j] * (double)num7);
						}
						else
						{
							array4[j].X = chartYAxis.GetCoordinate(array[num3, j] * (double)num7);
							array4[j].Y = (float)Math.Round((double)num6);
							array4[num10 - j - 1].X = chartYAxis.GetCoordinate(array[num3 - 1, j] * (double)num7);
							array4[num10 - j - 1].Y = (float)Math.Round((double)num6);
						}
						num6 += num;
						if (chartSeries3.Appearance.ShowLabels && chartSeries3[j].Label.Visible && !chartSeries3[j].Empty && chartSeries3.Visible)
						{
							RectangleF rect2 = new RectangleF(array4[j].X - 1f, array4[j].Y - 1f, 3f, 3f);
							ChartSeriesItem chartSeriesItem4 = chartSeries3[j];
							chartSeriesItem4.AddLabel(chartSeries3.GetItemLabel(chartSeriesItem4), rect2, this);
						}
						if (j > 0)
						{
							float prevValue = (plotArea.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical) ? seriesCollection[num3 - 1].Items[j].ActiveRegion.Region.PathPoints[1].Y : seriesCollection[num3 - 1].Items[j].ActiveRegion.Region.PathPoints[1].X;
							float prevValue2 = (plotArea.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical) ? seriesCollection[num3 - 1].Items[j].ActiveRegion.Region.PathPoints[2].Y : seriesCollection[num3 - 1].Items[j].ActiveRegion.Region.PathPoints[2].X;
							chartSeries3.Items[j].ActiveRegion.Region = this.GetAreaItemActiveRegion(array4[j - 1], array4[j], prevValue, prevValue2, plotArea.Chart.SeriesOrientation);
						}
					}
					if (chartSeries3.Visible)
					{
						GraphicsPath[] areaPath2 = RenderEngine.GetAreaPath(chartSeries3, seriesIndex, maxItemsCount, array4);
						using (GraphicsPath graphicsPath3 = areaPath2[0])
						{
							using (GraphicsPath graphicsPath4 = areaPath2[1])
							{
								if (chartSeries3.Appearance.Shadow.Blur >= 0f && chartSeries3.Appearance.Shadow.Distance > 0f)
								{
									ShadowManager.DrawPolygonShadow(chartSeries3, graphicsPath3, this.graphics, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue);
								}
								this.DrawPolygon(chartSeries3, seriesIndex, array4, graphicsPath3, graphicsPath4);
							}
						}
						this.DrawPointMark(chartSeries3, RenderEngine.GetPointaArrayForAreaPointMarks(array4, maxItemsCount));
					}
				}
				num3++;
				this.ResetClip();
			}
		}

		// Token: 0x0600E980 RID: 59776 RVA: 0x0034EA14 File Offset: 0x0034CC14
		private void RenderLineSeries(ChartSeries series, int index)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!series.Visible)
			{
				return;
			}
			this.graphics.SetClip(this.GetRenderRegion(series.YAxisType), CombineMode.Replace);
			double num = (double)plotArea.XAxis.GetPixelStep();
			double num2 = (double)plotArea.XAxis.GetStartCoordinate();
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			int count = series.Items.Count;
			PointF[] array = new PointF[count];
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			for (int i = 0; i < count; i++)
			{
				ChartSeriesItem chartSeriesItem = series.Items[i];
				double yvalue = chartSeriesItem.YValue;
				double xvalue = chartSeriesItem.XValue;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, i);
				}
				PointF pointF;
				if (double.IsNaN(chartSeriesItem.XValue))
				{
					pointF = new PointF((float)num2, chartYAxis.GetCoordinate(chartSeriesItem.YValue));
				}
				else
				{
					pointF = new PointF(plotArea.XAxis.GetCoordinate(chartSeriesItem.XValue), chartYAxis.GetCoordinate(chartSeriesItem.YValue));
				}
				if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
				{
					RenderEngine.ChangePlaces(ref pointF);
				}
				array[i] = pointF;
				if (((series.Type == ChartSeriesType.Bezier && i % 3 == 0) || series.Type != ChartSeriesType.Bezier) && series.Appearance.ShowLabels && chartSeriesItem.Label.Appearance.Visible && !chartSeriesItem.Empty)
				{
					RectangleF rect = new RectangleF(pointF.X - 1f, pointF.Y - 1f, 2f, 2f);
					chartSeriesItem.AddLabel(series.GetItemLabel(chartSeriesItem), rect, this);
				}
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.XValue = xvalue;
				num2 += num;
			}
			if (series.Type == ChartSeriesType.Bezier)
			{
				this.DrawBezier(series, index, array);
			}
			else
			{
				this.DrawLines(series, index, array);
			}
			this.DrawPointMark(series, array);
			this.ResetClip();
		}

		// Token: 0x0600E981 RID: 59777 RVA: 0x0034EC3C File Offset: 0x0034CE3C
		private void RenderPieSeries(ChartSeries series, int seriesIndex)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (!series.Visible)
			{
				return;
			}
			ChartSeriesCollection seriesCollection = plotArea.SeriesCollection().GetSeriesCollection(ChartSeriesType.Pie);
			int count = seriesCollection.Count;
			if (count == 0)
			{
				return;
			}
			int num = seriesCollection.IndexOf(series);
			int count2 = series.Items.Count;
			RectangleF rectangleF = new RectangleF(0f, 0f, plotArea.Appearance.Dimensions.Width.PixelValue, plotArea.Appearance.Dimensions.Height.PixelValue);
			float num2 = Math.Min(rectangleF.Width / (float)count, rectangleF.Height);
			float num3 = 0f;
			bool flag = true;
			float num4 = 0f;
			float num5;
			foreach (ChartSeries chartSeries in seriesCollection)
			{
				num5 = (float)(chartSeries.Appearance.DiameterScale * (double)num2);
				num3 += num5;
				if (flag && chartSeries != series)
				{
					num4 += num5;
				}
				else
				{
					flag = false;
				}
			}
			num5 = (float)(series.Appearance.DiameterScale * (double)num2);
			float num6 = (rectangleF.Width - num3) / (float)(count + 1);
			float num7 = num5 / 2f;
			double num8 = series.Sum();
			float num9 = (float)series.Appearance.StartAngle;
			float num10 = (float)(360.0 / ((num8 > 0.0) ? num8 : 1.0));
			PointF pointF = new PointF(rectangleF.X + num4 + num7 + num6 * (float)(num + 1), rectangleF.Height / 2f);
			PointF pieCenter = default(PointF);
			int num11 = (int)((float)series.Appearance.ExplodePercent / 100f * num7);
			int num12 = 0;
			if (series.Appearance.CenterXOffset == 0 && count > 1 && num12 != 0)
			{
				series.Appearance.CenterXOffset = num12;
			}
			pointF.X += (float)series.Appearance.CenterXOffset;
			pointF.Y += (float)series.Appearance.CenterYOffset;
			string[] array = new string[count2];
			PointF[] array2 = new PointF[count2];
			double[] array3 = new double[count2];
			int count3 = series.Items.Count;
			for (int i = 0; i < count3; i++)
			{
				ChartSeriesItem chartSeriesItem = series.Items[i];
				StyleBorder border = series.Appearance.Border;
				bool flag2 = (border.Width >= 1f && border.Visible) || chartSeriesItem.YValue == 0.0;
				if (!chartSeriesItem.Empty)
				{
					double num13 = Math.Abs(chartSeriesItem.YValue);
					bool exploded = chartSeriesItem.Appearance.Exploded;
					float num14 = (float)((double)num10 * num13);
					double num15;
					for (num15 = (double)(num9 + num14 / 2f) * 3.141592653589793 / 180.0; num15 < 0.0; num15 += 6.283185307179586)
					{
					}
					while (num15 > 6.283185307179586)
					{
						num15 -= 6.283185307179586;
					}
					if (exploded)
					{
						pieCenter.X = (float)((int)((double)pointF.X + (double)num11 * Math.Cos(num15)));
						pieCenter.Y = (float)((int)((double)pointF.Y + (double)num11 * Math.Sin(num15)));
					}
					else
					{
						pieCenter = pointF;
					}
					this.graphics.SetClip(this.GetRenderRegion(series.YAxisType), CombineMode.Replace);
					pieCenter.X += (float)((int)this.graphics.ClipBounds.X);
					pieCenter.Y += (float)((int)this.graphics.ClipBounds.Y);
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddPie((int)Math.Round((double)(pieCenter.X - num7)), (int)Math.Round((double)(pieCenter.Y - num7)), (int)Math.Round((double)num5), (int)Math.Round((double)num5), num9, num14);
					chartSeriesItem.ActiveRegion.Region = graphicsPath;
					FillStyle fillStyle = this.GetFillStyle(series, seriesIndex, chartSeriesItem, i);
					Brush brush = this.GetBrush(fillStyle, graphicsPath.GetBounds());
					if (brush != null)
					{
						ShadowManager.DrawPolygonShadow(series, graphicsPath, this.graphics, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue);
						this.graphics.FillPath(brush, graphicsPath);
					}
					using (Pen pen = this.GetPen(series, plotArea.SeriesCollection().IndexOf(series), chartSeriesItem))
					{
						if (pen != null && pen.Width > 0f)
						{
							pen.Alignment = PenAlignment.Center;
							this.graphics.DrawPath(pen, graphicsPath);
						}
						else if (!flag2)
						{
							using (Pen pen2 = new Pen(brush, 1.7f))
							{
								this.graphics.DrawPath(pen2, graphicsPath);
							}
						}
					}
					if (brush != null)
					{
						brush.Dispose();
					}
					float num16 = (float)((double)(num9 + num14 / 2f) * 3.141592653589793 / 180.0);
					array2[i] = new PointF((float)((double)pieCenter.X + Math.Floor((double)num7 * Math.Cos((double)num16))), (float)((double)pieCenter.Y + Math.Floor((double)num7 * Math.Sin((double)num16))));
					array[i] = series.GetItemLabel(chartSeriesItem);
					array3[i] = num15;
					num9 += num14;
					if (num12 != 0)
					{
						series.Appearance.CenterXOffset = 0;
					}
					this.ResetClip();
				}
			}
			series.AddLabelsForPieSeries(array2, array, array3, pieCenter, num7, this);
		}

		// Token: 0x0600E982 RID: 59778 RVA: 0x0034F238 File Offset: 0x0034D438
		private void RenderEmptyPoint(ChartSeries series, ChartSeriesItem item, int itemIndex, float axisStart)
		{
			if (series.IsStacked)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			PointF pointF = default(PointF);
			pointF.Y = ((series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis.GetCoordinate(item.YValue) : plotArea.YAxis2.GetCoordinate(item.YValue));
			float num = plotArea.XAxis.GetPixelStep();
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			switch (series.Type)
			{
			case ChartSeriesType.Bar:
			case ChartSeriesType.Gantt:
			case ChartSeriesType.CandleStick:
				pointF.X = (float)((int)axisStart);
				goto IL_11D;
			case ChartSeriesType.Line:
			case ChartSeriesType.Area:
			case ChartSeriesType.Bezier:
			case ChartSeriesType.Spline:
			case ChartSeriesType.Bubble:
			case ChartSeriesType.Point:
			case ChartSeriesType.SplineArea:
				if (double.IsNaN(item.XValue))
				{
					pointF.X = plotArea.XAxis.GetStartCoordinate() + num * (float)itemIndex;
					goto IL_11D;
				}
				pointF.X = plotArea.XAxis.GetCoordinate(item.XValue);
				goto IL_11D;
			}
			pointF.X = 0f;
			IL_11D:
			FillStyle fillStyle = series.Appearance.EmptyValue.PointMark.FillStyle;
			float num3;
			float num2;
			if (fillStyle.MainColor.IsEmpty && fillStyle.SecondColor.IsEmpty)
			{
				num2 = (num3 = 0f);
			}
			else if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				num3 = series.Appearance.EmptyValue.PointMark.Dimensions.Height.PixelValue;
				num2 = series.Appearance.EmptyValue.PointMark.Dimensions.Width.PixelValue;
			}
			else
			{
				num3 = series.Appearance.EmptyValue.PointMark.Dimensions.Width.PixelValue;
				num2 = series.Appearance.EmptyValue.PointMark.Dimensions.Height.PixelValue;
				float x = pointF.X;
				pointF.X = pointF.Y;
				pointF.Y = x;
			}
			pointF.X -= plotArea.Appearance.Position.X + num3 / 2f;
			pointF.Y -= plotArea.Appearance.Position.Y + num2 / 2f;
			if (!this.renderEngineDrawOnlyShadow)
			{
				if (series.Appearance.ShowLabels && item.Label.Appearance.Visible)
				{
					RectangleF rect = new RectangleF(pointF.X, pointF.Y, num3, num2);
					rect.X += plotArea.Appearance.Position.X;
					rect.Y += plotArea.Appearance.Position.Y;
					item.AddLabel(series.GetItemLabel(item), rect, this);
				}
				this.RenderElement(new ChartMarker(this.chart)
				{
					Container = plotArea,
					appearance = (StyleMarkerEmptyValue)series.Appearance.EmptyValue.PointMark.Clone(),
					Appearance = 
					{
						Position = 
						{
							X = pointF.X,
							Y = pointF.Y
						}
					}
				});
			}
		}

		// Token: 0x0600E983 RID: 59779 RVA: 0x0034F5A8 File Offset: 0x0034D7A8
		private void RenderGanttSeries(ChartSeries series, int index)
		{
			if (!series.Visible)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			Region renderRegion = this.GetRenderRegion(series.YAxisType);
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			float zeroCoordinate = chartYAxis.GetZeroCoordinate();
			float num = plotArea.XAxis.GetPixelStep();
			float barStart = plotArea.GetBarStart(series);
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			float num2 = float.NaN;
			bool flag = true;
			int num3 = 0;
			foreach (ChartSeriesItem chartSeriesItem in series.Items)
			{
				double yvalue = chartSeriesItem.YValue;
				double xvalue = chartSeriesItem.XValue;
				double yvalue2 = chartSeriesItem.YValue2;
				double xvalue2 = chartSeriesItem.XValue2;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num3);
				}
				if (double.IsNaN(xvalue))
				{
					if (double.IsNaN((double)num2))
					{
						num2 = barStart;
					}
					else
					{
						num2 += num;
					}
					flag = false;
				}
				else
				{
					num2 = plotArea.XAxis.GetCoordinate(xvalue);
				}
				if (!double.IsNaN(xvalue2))
				{
					float coordinate = plotArea.XAxis.GetCoordinate(xvalue2);
					this.barWidth = Math.Abs(num2 - coordinate);
					num2 = Math.Min(num2, coordinate);
				}
				else
				{
					num2 -= (flag ? (this.barWidth / 2f) : 0f);
				}
				this.graphics.SetClip(renderRegion, CombineMode.Replace);
				float num4;
				if (double.IsNaN(yvalue))
				{
					num4 = this.graphics.ClipBounds.Y;
				}
				else
				{
					num4 = chartYAxis.GetCoordinate(chartSeriesItem.YValue);
				}
				if (!chartSeriesItem.Empty)
				{
					float num5;
					if (double.IsNaN(yvalue2))
					{
						num5 = zeroCoordinate;
					}
					else
					{
						num5 = chartYAxis.GetCoordinate(yvalue2);
					}
					float num6 = Math.Abs(num5 - num4);
					RectangleF barRect;
					if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						barRect = new RectangleF((float)Math.Round((double)num2), (num4 < num5) ? num4 : num5, (float)Math.Round((double)this.barWidth, MidpointRounding.AwayFromZero), num6);
					}
					else
					{
						barRect = new RectangleF((num4 < num5) ? num4 : num5, (float)Math.Round((double)num2), num6, (float)Math.Round((double)this.barWidth, MidpointRounding.AwayFromZero));
					}
					this.RenderBarShadow(series, chartSeriesItem, barRect);
					this.RenderBar(series, index, chartSeriesItem, num3, barRect);
				}
				else
				{
					this.RenderEmptyPoint(series, chartSeriesItem, num3, num2 + (float)index * this.barWidth + this.barWidth / 2f);
					this.graphics.SetClip(renderRegion, CombineMode.Replace);
				}
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.XValue = xvalue;
				chartSeriesItem.YValue2 = yvalue2;
				chartSeriesItem.XValue2 = xvalue2;
				num3++;
				this.ResetClip();
			}
		}

		// Token: 0x0600E984 RID: 59780 RVA: 0x0034F890 File Offset: 0x0034DA90
		private void RenderPointSeries(ChartSeries series, int index)
		{
			if (!series.Visible)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			float num = plotArea.XAxis.GetPixelStep();
			int num2 = 0;
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			float startCoordinate = plotArea.XAxis.GetStartCoordinate();
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			foreach (ChartSeriesItem chartSeriesItem in series.Items)
			{
				if (chartSeriesItem.Empty)
				{
					double yvalue = chartSeriesItem.YValue;
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num2);
					this.RenderEmptyPoint(series, chartSeriesItem, num2, 0f);
					chartSeriesItem.YValue = yvalue;
				}
				else
				{
					float num3;
					if (double.IsNaN(chartSeriesItem.XValue))
					{
						num3 = startCoordinate + num * (float)num2;
					}
					else
					{
						num3 = plotArea.XAxis.GetCoordinate(chartSeriesItem.XValue);
					}
					float coordinate = chartYAxis.GetCoordinate(chartSeriesItem.YValue);
					PointF point;
					if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						point = new PointF(num3, coordinate);
					}
					else
					{
						point = new PointF(coordinate, num3);
					}
					point.X -= plotArea.Appearance.Position.X + series.Appearance.PointMark.Dimensions.Width.PixelValue / 2f;
					point.Y -= plotArea.Appearance.Position.Y + series.Appearance.PointMark.Dimensions.Height.PixelValue / 2f;
					this.RenderPointLabelAndMarker(series, chartSeriesItem, index, num2, point);
				}
				num2++;
			}
		}

		// Token: 0x0600E985 RID: 59781 RVA: 0x0034FA78 File Offset: 0x0034DC78
		private void RenderPointLabelAndMarker(ChartSeries series, ChartSeriesItem item, int index, int itemIndex, PointF point)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (series.Appearance.ShowLabels && item.Label.Appearance.Visible)
			{
				string itemLabel = series.GetItemLabel(item);
				RectangleF rect = new RectangleF(point.X, point.Y, series.Appearance.PointMark.Dimensions.Width.PixelValue, series.Appearance.PointMark.Dimensions.Height.PixelValue);
				rect.X += plotArea.Appearance.Position.X;
				rect.Y += plotArea.Appearance.Position.Y;
				item.AddLabel(itemLabel, rect, this);
			}
			ChartMarker chartMarker = new ChartMarker(this.chart);
			chartMarker.Container = plotArea;
			StyleSeriesItem styleSeriesItem = new StyleSeriesItem();
			StyleBorder obj = new StyleSeriesBorder();
			if (!item.Appearance.Border.Equals(obj))
			{
				chartMarker.Appearance.styleBorder = (StyleBorder)item.Appearance.Border.Clone();
			}
			else
			{
				chartMarker.Appearance.styleBorder = (StyleBorder)series.Appearance.Border.Clone();
			}
			DimensionsPointMarker obj2 = new DimensionsPointMarker();
			if (!item.Appearance.PointDimentions.Equals(obj2))
			{
				chartMarker.Appearance.dimensions = (DimensionsPointMarker)item.Appearance.PointDimentions.Clone();
			}
			else
			{
				chartMarker.Appearance.dimensions = (DimensionsPointMarker)series.Appearance.PointDimentions.Clone();
			}
			if (item.Appearance.PointShape != styleSeriesItem.PointShape)
			{
				chartMarker.Appearance.Figure = item.Appearance.PointShape;
			}
			else
			{
				chartMarker.Appearance.Figure = series.Appearance.PointShape;
			}
			Corners obj3 = new Corners();
			if (!item.Appearance.Corners.Equals(obj3))
			{
				chartMarker.Appearance.styleMarkerCorners = (Corners)item.Appearance.Corners.Clone();
			}
			else
			{
				chartMarker.Appearance.styleMarkerCorners = (Corners)series.Appearance.Corners.Clone();
			}
			ShadowStyle obj4 = new ShadowStyle();
			if (!item.Appearance.Shadow.Equals(obj4))
			{
				chartMarker.Appearance.styleShadow = (ShadowStyle)item.Appearance.Shadow.Clone();
			}
			else
			{
				chartMarker.Appearance.styleShadow = (ShadowStyle)series.Appearance.Shadow.Clone();
			}
			if (item.Appearance.Visible != styleSeriesItem.Visible)
			{
				chartMarker.Appearance.Visible = item.Appearance.Visible;
			}
			else
			{
				chartMarker.Appearance.Visible = series.Appearance.Visible;
			}
			if (item.Appearance.PointRotationAngle != styleSeriesItem.PointRotationAngle)
			{
				chartMarker.Appearance.RotationAngle = item.Appearance.PointRotationAngle;
			}
			else
			{
				chartMarker.Appearance.RotationAngle = series.Appearance.PointRotationAngle;
			}
			chartMarker.Appearance.styleChart = item.Appearance.styleChart;
			chartMarker.Appearance.styleMarkerFillStyle = this.GetFillStyle(series, index, item, itemIndex);
			chartMarker.Appearance.Position.X = point.X;
			chartMarker.Appearance.Position.Y = point.Y;
			this.RenderElement(chartMarker, item);
		}

		// Token: 0x0600E986 RID: 59782 RVA: 0x0034FE08 File Offset: 0x0034E008
		private void RenderBubbleSeries(ChartSeries series, int index)
		{
			if (!series.Visible)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			Region renderRegion = this.GetRenderRegion(series.YAxisType);
			float num = plotArea.XAxis.GetPixelStep();
			float startCoordinate = plotArea.XAxis.GetStartCoordinate();
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			int num2 = 0;
			foreach (ChartSeriesItem chartSeriesItem in series.Items)
			{
				double yvalue = chartSeriesItem.YValue;
				double xvalue = chartSeriesItem.XValue;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num2);
				}
				PointF pointF;
				if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					if (chartSeriesItem.XValue.Equals(double.NaN))
					{
						pointF = new PointF(startCoordinate + num * (float)num2, chartYAxis.GetCoordinate(chartSeriesItem.YValue));
					}
					else
					{
						pointF = new PointF(plotArea.XAxis.GetCoordinate(chartSeriesItem.XValue), chartYAxis.GetCoordinate(chartSeriesItem.YValue));
					}
				}
				else if (chartSeriesItem.XValue.Equals(double.NaN))
				{
					pointF = new PointF(chartYAxis.GetCoordinate(chartSeriesItem.YValue), startCoordinate + num * (float)num2);
				}
				else
				{
					pointF = new PointF(chartYAxis.GetCoordinate(chartSeriesItem.YValue), plotArea.XAxis.GetCoordinate(chartSeriesItem.XValue));
				}
				int num3 = series.Appearance.BubbleSize;
				int num4 = series.Appearance.BubbleSize;
				if (!chartSeriesItem.Empty)
				{
					if (!chartSeriesItem.XValue2.Equals(double.NaN))
					{
						num3 = (int)(Math.Abs(chartSeriesItem.XValue2) * (double)plotArea.XAxis.PixelsPerValue);
					}
					if (!chartSeriesItem.YValue2.Equals(double.NaN))
					{
						num4 = (int)(Math.Abs(chartSeriesItem.YValue2) * (double)chartYAxis.PixelsPerValue);
					}
					RectangleF rect;
					if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						rect = new RectangleF(pointF.X - (float)(num3 / 2), pointF.Y - (float)(num4 / 2), (float)num3, (float)num4);
					}
					else
					{
						rect = new RectangleF(pointF.X - (float)(num4 / 2), pointF.Y - (float)(num3 / 2), (float)num4, (float)num3);
					}
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddEllipse(rect);
					chartSeriesItem.ActiveRegion.Region = graphicsPath;
					this.graphics.SetClip(renderRegion, CombineMode.Replace);
					if (rect.Width != 0f && rect.Height != 0f)
					{
						RenderEngine.AdjustRect(ref rect);
						if (series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f)
						{
							GraphicsPath graphicsPath2 = new GraphicsPath();
							graphicsPath2.AddEllipse(rect);
							ShadowManager.DrawPolygonShadow(series, graphicsPath2, this.graphics, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue);
						}
						using (Brush brush = this.GetBrush(series, index, chartSeriesItem, num2, rect))
						{
							if (brush != null)
							{
								this.graphics.FillEllipse(brush, rect);
							}
						}
					}
					using (Pen pen = this.GetPen(series, index, chartSeriesItem))
					{
						if (pen != null)
						{
							this.graphics.DrawEllipse(pen, rect);
						}
					}
					this.ResetClip();
					if (series.Appearance.ShowLabels && chartSeriesItem.Label.Appearance.Visible)
					{
						chartSeriesItem.AddLabel(series.GetItemLabel(chartSeriesItem), rect, this);
					}
				}
				else
				{
					this.RenderEmptyPoint(series, chartSeriesItem, num2, pointF.X + (float)(num3 / 2));
					this.graphics.SetClip(renderRegion, CombineMode.Replace);
				}
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.XValue = xvalue;
				num2++;
			}
		}

		// Token: 0x0600E987 RID: 59783 RVA: 0x0035029C File Offset: 0x0034E49C
		private void RenderCandlestickSeries(ChartSeries series, int index, BarOrderingMode mode)
		{
			if (!series.Visible)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			Region renderRegion = this.GetRenderRegion(series.YAxisType);
			ChartYAxis chartYAxis = (series.YAxisType == ChartYAxisType.Primary) ? plotArea.YAxis : plotArea.YAxis2;
			chartYAxis.GetZeroCoordinate();
			float num = plotArea.XAxis.GetPixelStep();
			(float)(this.chart.Appearance.BarOverlapPercent / 100m);
			float num2 = this.barWidth;
			float num3 = 0f;
			float num4 = 0f;
			if (mode == BarOrderingMode.Classic)
			{
				num3 = plotArea.GetBarStart(series);
			}
			else
			{
				num4 = plotArea.GetBarStart(series, true);
			}
			if (this.chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			int num5 = 0;
			foreach (ChartSeriesItem chartSeriesItem in series.Items)
			{
				double yvalue = chartSeriesItem.YValue;
				double yvalue2 = chartSeriesItem.YValue2;
				double yvalue3 = chartSeriesItem.YValue3;
				double yvalue4 = chartSeriesItem.YValue4;
				double xvalue = chartSeriesItem.XValue;
				double xvalue2 = chartSeriesItem.XValue2;
				if (chartSeriesItem.Empty)
				{
					chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num5, "YValue");
					chartSeriesItem.YValue2 = series.GetEmptyPointYValue(chartSeriesItem, num5, "YValue2");
					chartSeriesItem.YValue3 = series.GetEmptyPointYValue(chartSeriesItem, num5, "YValue3");
					chartSeriesItem.YValue4 = series.GetEmptyPointYValue(chartSeriesItem, num5, "YValue4");
					chartSeriesItem.XValue = series.GetEmptyPointYValue(chartSeriesItem, num5, "XValue");
					chartSeriesItem.XValue2 = series.GetEmptyPointYValue(chartSeriesItem, num5, "XValue2");
				}
				double yvalue5 = chartSeriesItem.YValue;
				double yvalue6 = chartSeriesItem.YValue2;
				double yvalue7 = chartSeriesItem.YValue3;
				double yvalue8 = chartSeriesItem.YValue4;
				if (mode == BarOrderingMode.Strict)
				{
					int popularityIndex = plotArea.PopularValues.GetPopularityIndex(chartSeriesItem.GetXValue());
					num3 = plotArea.PopularValues[popularityIndex].X + num4 - series.GetBarWidthRatio() * Math.Abs(num) / 2f;
				}
				RectangleF rectangleF;
				if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					float coordinate = chartYAxis.GetCoordinate(Math.Max(yvalue5, yvalue6));
					float num6 = chartYAxis.GetCoordinate(Math.Min(yvalue5, yvalue6)) - coordinate;
					num6 = ((num6 == 0f) ? 1f : num6);
					rectangleF = new RectangleF((float)Math.Round((double)num3), coordinate, (float)Math.Round((double)num2, MidpointRounding.AwayFromZero), num6);
				}
				else
				{
					float coordinate = chartYAxis.GetCoordinate(Math.Min(yvalue5, yvalue6));
					float num6 = chartYAxis.GetCoordinate(Math.Max(yvalue5, yvalue6)) - coordinate;
					num6 = ((num6 == 0f) ? 1f : num6);
					rectangleF = new RectangleF(coordinate, (float)Math.Round((double)num3), num6, (float)Math.Round((double)num2, MidpointRounding.AwayFromZero));
				}
				this.graphics.SetClip(renderRegion, CombineMode.Replace);
				this.RenderBarShadow(series, chartSeriesItem, rectangleF);
				if (!this.renderEngineDrawOnlyShadow)
				{
					using (Pen pen = chartSeriesItem.Empty ? this.GetEmptyPen(series, index, chartSeriesItem) : this.GetPen(series, index, chartSeriesItem))
					{
						if (pen.Width > 1f)
						{
							float num7 = pen.Width / 2f;
							rectangleF = new RectangleF(rectangleF.X + num7, rectangleF.Y + num7, rectangleF.Width - num7 * 2f, rectangleF.Height - num7 * 2f);
						}
						using (Brush brush = chartSeriesItem.Empty ? this.GetBrush(series.Appearance.EmptyValue.FillStyle, rectangleF) : this.GetBrush(series, index, chartSeriesItem, num5, rectangleF))
						{
							GraphicsPath roundRectangle = this.GetRoundRectangle(chartSeriesItem.Appearance.Corners, rectangleF, series);
							chartSeriesItem.ActiveRegion.Region = roundRectangle;
							if (this.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								float x = (float)Math.Round((double)(rectangleF.Left + rectangleF.Width / 2f));
								float coordinate2 = chartYAxis.GetCoordinate(yvalue7);
								float coordinate3 = chartYAxis.GetCoordinate(yvalue8);
								if (yvalue5 >= yvalue6)
								{
									this.graphics.FillPath(brush, roundRectangle);
								}
								this.graphics.DrawPath(pen, roundRectangle);
								this.graphics.DrawLine(pen, new PointF(x, coordinate2), new PointF(x, rectangleF.Top));
								this.graphics.DrawLine(pen, new PointF(x, coordinate3), new PointF(x, rectangleF.Bottom));
								if (series.Appearance.ShowLabels && chartSeriesItem.Label.Appearance.Visible && !chartSeriesItem.Empty)
								{
									chartSeriesItem.AddLabel(series.GetItemLabel(chartSeriesItem), new RectangleF(rectangleF.Left, coordinate2, rectangleF.Width, coordinate3 - coordinate2), this);
								}
							}
							else
							{
								float y = (float)Math.Round((double)(rectangleF.Top + rectangleF.Height / 2f));
								float coordinate4 = chartYAxis.GetCoordinate(yvalue8);
								float coordinate5 = chartYAxis.GetCoordinate(yvalue7);
								if (yvalue5 >= yvalue6)
								{
									this.graphics.FillPath(brush, roundRectangle);
								}
								this.graphics.DrawPath(pen, roundRectangle);
								this.graphics.DrawLine(pen, new PointF(coordinate4, y), new PointF(rectangleF.Left, y));
								this.graphics.DrawLine(pen, new PointF(coordinate5, y), new PointF(rectangleF.Right, y));
								if (series.Appearance.ShowLabels && chartSeriesItem.Label.Appearance.Visible && !chartSeriesItem.Empty)
								{
									chartSeriesItem.AddLabel(series.GetItemLabel(chartSeriesItem), new RectangleF(coordinate4, rectangleF.Top, coordinate5 - coordinate4, rectangleF.Height), this);
								}
							}
						}
					}
				}
				if (chartSeriesItem.Empty)
				{
					this.RenderEmptyPoint(series, chartSeriesItem, num5, rectangleF.Left + rectangleF.Width / 2f);
					this.graphics.SetClip(renderRegion, CombineMode.Replace);
				}
				this.ResetClip();
				chartSeriesItem.YValue = yvalue;
				chartSeriesItem.YValue2 = yvalue2;
				chartSeriesItem.YValue3 = yvalue3;
				chartSeriesItem.YValue4 = yvalue4;
				chartSeriesItem.XValue = xvalue;
				chartSeriesItem.XValue2 = xvalue2;
				num3 += num;
				num5++;
			}
		}

		// Token: 0x0600E988 RID: 59784 RVA: 0x00350954 File Offset: 0x0034EB54
		private void SeriesLabelsDraw()
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			RectangleF rectangleF = new RectangleF(plotArea.Appearance.Position.X - 2f, plotArea.Appearance.Position.Y - 2f, plotArea.Appearance.Dimensions.Width.PixelValue + 4f, plotArea.Appearance.Dimensions.Height.PixelValue + 4f);
			if (this.chart.IntelligentLabelsEnabled)
			{
				plotArea.CreateRectanglesInSeriesLabel();
				plotArea.SeriesLabels = IntelligentEngine.Distribute(plotArea.SeriesLabels, rectangleF, this.chart.Series.OnlyPieSeries());
			}
			int count = plotArea.SeriesLabels.Count;
			for (int i = 0; i < count; i++)
			{
				SeriesItemLabel seriesItemLabel = plotArea.SeriesLabels[i];
				this.RenderElement(seriesItemLabel);
				this.Render(seriesItemLabel);
				if (!seriesItemLabel.ConnectionPoint.IsEmpty)
				{
					Position position = RenderEngine.LocalToGlobal(seriesItemLabel);
					float num = seriesItemLabel.Appearance.Dimensions.Width.PixelValue / 2f;
					float num2 = seriesItemLabel.Appearance.Dimensions.Height.PixelValue / 2f;
					Region region = new Region(rectangleF);
					if (seriesItemLabel.ActiveRegion.Region != null)
					{
						region.Xor(seriesItemLabel.ActiveRegion.Region);
					}
					region.Intersect(rectangleF);
					this.graphics.SetClip(region, CombineMode.Replace);
					Point point;
					if (!seriesItemLabel.ConnectionMidPoint.IsEmpty)
					{
						Point pt = new Point((int)seriesItemLabel.ConnectionPoint.X, (int)seriesItemLabel.ConnectionPoint.Y);
						point = new Point((int)(seriesItemLabel.ConnectionMidPoint.X + num), (int)(seriesItemLabel.ConnectionMidPoint.Y + num2));
						this.graphics.DrawLine(RenderEngine.GetPen(seriesItemLabel.Appearance.LabelConnectorStyle, PenAlignment.Center), pt, point);
					}
					else
					{
						point = new Point((int)seriesItemLabel.ConnectionPoint.X, (int)seriesItemLabel.ConnectionPoint.Y);
					}
					Point pt2 = new Point((int)(position.X + num), (int)(position.Y + num2));
					this.graphics.DrawLine(RenderEngine.GetPen(seriesItemLabel.Appearance.LabelConnectorStyle, PenAlignment.Center), point, pt2);
				}
			}
			plotArea.SeriesLabels.Clear();
		}

		// Token: 0x0600E989 RID: 59785 RVA: 0x00350BD4 File Offset: 0x0034EDD4
		private void DrawPointMark(ChartSeries series, PointF[] points)
		{
			if (!series.Appearance.PointMark.Visible)
			{
				return;
			}
			ChartPlotArea plotArea = this.chart.PlotArea;
			int num = -1;
			foreach (PointF pointF in points)
			{
				num++;
				if (series.Type != ChartSeriesType.Bezier || num % 3 == 0)
				{
					ChartSeriesItem chartSeriesItem = series[num];
					double yvalue = chartSeriesItem.YValue;
					double xvalue = chartSeriesItem.XValue;
					if (chartSeriesItem.Empty)
					{
						chartSeriesItem.YValue = series.GetEmptyPointYValue(chartSeriesItem, num);
					}
					if (!chartSeriesItem.Empty)
					{
						ChartMarker chartMarker = new ChartMarker(this.chart);
						chartMarker.Container = plotArea;
						StyleMarkerSeriesPoint styleMarkerSeriesPoint = new StyleMarkerSeriesPoint();
						if (chartSeriesItem.PointAppearance.Figure != styleMarkerSeriesPoint.Figure)
						{
							chartMarker.Appearance.Figure = chartSeriesItem.PointAppearance.Figure;
						}
						else
						{
							chartMarker.Appearance.Figure = series.Appearance.PointMark.Figure;
						}
						if (!chartSeriesItem.PointAppearance.Border.Equals(styleMarkerSeriesPoint.Border))
						{
							chartMarker.Appearance.styleBorder = (StyleBorder)chartSeriesItem.PointAppearance.Border.Clone();
						}
						else
						{
							chartMarker.Appearance.styleBorder = (StyleBorder)series.Appearance.PointMark.Border.Clone();
						}
						if (!chartSeriesItem.PointAppearance.FillStyle.Equals(styleMarkerSeriesPoint.FillStyle))
						{
							chartMarker.Appearance.styleMarkerFillStyle = (FillStyle)chartSeriesItem.PointAppearance.FillStyle.Clone();
						}
						else
						{
							chartMarker.Appearance.styleMarkerFillStyle = (FillStyle)series.Appearance.PointMark.FillStyle.Clone();
						}
						if (!chartSeriesItem.PointAppearance.Dimensions.Equals(styleMarkerSeriesPoint.Dimensions))
						{
							chartMarker.Appearance.dimensions = (Dimensions)chartSeriesItem.PointAppearance.Dimensions.Clone();
						}
						else
						{
							chartMarker.Appearance.dimensions = (Dimensions)series.Appearance.PointMark.Dimensions.Clone();
						}
						if (chartSeriesItem.PointAppearance.RotationAngle != styleMarkerSeriesPoint.RotationAngle)
						{
							chartMarker.Appearance.RotationAngle = chartSeriesItem.PointAppearance.RotationAngle;
						}
						else
						{
							chartMarker.Appearance.RotationAngle = series.Appearance.PointMark.RotationAngle;
						}
						if (!chartSeriesItem.PointAppearance.Corners.Equals(styleMarkerSeriesPoint.Corners))
						{
							chartMarker.Appearance.Corners = (Corners)chartSeriesItem.PointAppearance.Corners.Clone();
						}
						else
						{
							chartMarker.Appearance.Corners = (Corners)series.Appearance.PointMark.Corners.Clone();
						}
						if (!chartSeriesItem.PointAppearance.Position.Equals(styleMarkerSeriesPoint.Position))
						{
							chartMarker.Appearance.position = (Position)chartSeriesItem.PointAppearance.Position.Clone();
						}
						else
						{
							chartMarker.Appearance.position = (Position)series.Appearance.PointMark.Position.Clone();
						}
						if (!chartSeriesItem.PointAppearance.Shadow.Equals(styleMarkerSeriesPoint.Shadow))
						{
							chartMarker.Appearance.styleShadow = (ShadowStyle)chartSeriesItem.PointAppearance.Shadow.Clone();
						}
						else
						{
							chartMarker.Appearance.styleShadow = (ShadowStyle)series.Appearance.PointMark.Shadow.Clone();
						}
						if (!chartSeriesItem.PointAppearance.Visible.Equals(styleMarkerSeriesPoint.Visible))
						{
							chartMarker.Appearance.Visible = chartSeriesItem.PointAppearance.Visible;
						}
						else
						{
							chartMarker.Appearance.Visible = series.Appearance.PointMark.Visible;
						}
						chartMarker.Appearance.Position.X = pointF.X - plotArea.Appearance.Position.X - chartMarker.Appearance.Dimensions.Width.PixelValue / 2f;
						chartMarker.Appearance.Position.Y = pointF.Y - plotArea.Appearance.Position.Y - chartMarker.Appearance.Dimensions.Height.PixelValue / 2f;
						this.graphics.SetClip(this.GetRenderRegion(series.YAxisType), CombineMode.Replace);
						this.RenderElement(chartMarker, chartSeriesItem);
						this.ResetClip();
					}
					else
					{
						this.RenderEmptyPoint(series, chartSeriesItem, num, pointF.X);
					}
					chartSeriesItem.YValue = yvalue;
					chartSeriesItem.XValue = xvalue;
				}
			}
		}

		// Token: 0x0600E98A RID: 59786 RVA: 0x003510A2 File Offset: 0x0034F2A2
		private static ChartAxisVisibleValues AxisVisibleValues(ChartSeries series, ChartPlotArea plotArea)
		{
			if (series.YAxisType != ChartYAxisType.Primary)
			{
				return plotArea.YAxis2.VisibleValues;
			}
			return plotArea.YAxis.VisibleValues;
		}

		// Token: 0x0600E98B RID: 59787 RVA: 0x003510C4 File Offset: 0x0034F2C4
		private void DrawLineShadow(Pen shadowPen, ChartSeries series, GraphicsPath path)
		{
			if (series.Appearance.LineSeriesAppearance.Width >= 1f && series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f)
			{
				ChartPlotArea plotArea = this.chart.PlotArea;
				ShadowManager.DrawLineShadow(this.graphics, shadowPen, path, (int)series.Appearance.LineSeriesAppearance.Width, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue, (int)series.Appearance.Shadow.Distance, series.Appearance.Shadow.Color, series.Appearance.Shadow.Blur, series.Appearance.Shadow.Position);
			}
		}

		// Token: 0x0600E98C RID: 59788 RVA: 0x003511C0 File Offset: 0x0034F3C0
		private static void RemoveZerosFromEndOfList(List<byte> list)
		{
			while (list.Count > 0 && list[list.Count - 1] == 0)
			{
				list.RemoveAt(list.Count - 1);
			}
		}

		// Token: 0x0600E98D RID: 59789 RVA: 0x0035120C File Offset: 0x0034F40C
		private void DrawLines(ChartSeries series, int index, PointF[] points)
		{
			int count = series.Items.Count;
			ChartPlotArea plotArea = this.chart.PlotArea;
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			using (Pen pen = this.GetPen(series, index, null))
			{
				using (Pen pen2 = RenderEngine.GetPen(series.Appearance.LineSeriesAppearance, series.Appearance.Shadow.Color, series.Appearance.Shadow.Distance))
				{
					using (Pen emptyPen = this.GetEmptyPen(series, index, null))
					{
						bool isHasEmptyValues = series.IsHasEmptyValues;
						bool flag = series.Appearance.Shadow.Distance > 0f;
						bool isLine = series.IsLine;
						using (GraphicsPath graphicsPath = new GraphicsPath())
						{
							using (GraphicsPath graphicsPath2 = new GraphicsPath())
							{
								if (points.Length > 0)
								{
									if (points.Length == 1)
									{
										if (series[0].Empty)
										{
											this.graphics.DrawLine(emptyPen, points[0], points[0]);
										}
										else
										{
											this.graphics.DrawLine(pen, points[0], points[0]);
										}
									}
									else
									{
										for (int i = 0; i < count - 1; i++)
										{
											ChartSeriesItem chartSeriesItem = series[i];
											ChartSeriesItem chartSeriesItem2 = series[i + 1];
											if ((!chartSeriesItem.Empty && !chartSeriesItem2.Empty) || series.IsStackedLine)
											{
												ChartSeriesType type = series.Type;
												if (type <= ChartSeriesType.Spline)
												{
													switch (type)
													{
													case ChartSeriesType.Line:
													case ChartSeriesType.Area:
														goto IL_198;
													default:
														if (type == ChartSeriesType.Spline)
														{
															goto IL_1E3;
														}
														break;
													}
												}
												else
												{
													if (type == ChartSeriesType.SplineArea)
													{
														goto IL_1E3;
													}
													switch (type)
													{
													case ChartSeriesType.StackedLine:
														goto IL_198;
													case ChartSeriesType.StackedSpline:
														goto IL_1E3;
													}
												}
												IL_22B:
												if (isHasEmptyValues && list2.Count > 0 && list2[list2.Count - 1] != 0)
												{
													list2.Add(0);
													goto IL_33A;
												}
												goto IL_33A;
												IL_198:
												graphicsPath.AddLine(points[i], points[i + 1]);
												if (list.Count == 0)
												{
													list.AddRange(new byte[]
													{
														0,
														1
													});
													goto IL_22B;
												}
												list.Add(1);
												goto IL_22B;
												IL_1E3:
												graphicsPath.AddCurve(points, i, 1, 0.5f);
												if (list.Count == 0)
												{
													list.AddRange(new byte[]
													{
														0,
														3,
														3,
														3
													});
													goto IL_22B;
												}
												list.AddRange(new byte[]
												{
													3,
													3,
													3
												});
												goto IL_22B;
											}
											else
											{
												ChartSeriesType type2 = series.Type;
												switch (type2)
												{
												case ChartSeriesType.Line:
												case ChartSeriesType.Area:
													graphicsPath2.AddLine(points[i], points[i + 1]);
													if (list2.Count == 0)
													{
														list2.AddRange(new byte[]
														{
															0,
															1
														});
													}
													else
													{
														list2.Add(1);
													}
													break;
												default:
													if (type2 == ChartSeriesType.Spline || type2 == ChartSeriesType.SplineArea)
													{
														graphicsPath2.AddCurve(points, i, 1, 0.5f);
														if (list2.Count == 0)
														{
															list2.AddRange(new byte[]
															{
																0,
																3,
																3,
																3
															});
														}
														else
														{
															list2.AddRange(new byte[]
															{
																3,
																3,
																3
															});
														}
													}
													break;
												}
												if (list.Count > 0 && list[list.Count - 1] != 0)
												{
													list.Add(0);
												}
											}
											IL_33A:;
										}
										if (isHasEmptyValues && list2.Count > 0)
										{
											RenderEngine.RemoveZerosFromEndOfList(list2);
										}
										if (list.Count > 0)
										{
											RenderEngine.RemoveZerosFromEndOfList(list);
										}
										if (isHasEmptyValues && !series.IsStackedLine)
										{
											this.graphics.DrawPath(emptyPen, new GraphicsPath(graphicsPath2.PathPoints, list2.ToArray()));
										}
										if (list.Count > 0)
										{
											if ((isLine || series.IsStackedLine) && flag)
											{
												this.DrawLineShadow(pen2, series, new GraphicsPath(graphicsPath.PathPoints, list.ToArray()));
											}
											this.graphics.DrawPath(pen, new GraphicsPath(graphicsPath.PathPoints, list.ToArray()));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600E98E RID: 59790 RVA: 0x003516C4 File Offset: 0x0034F8C4
		private void DrawBezier(ChartSeries series, int index, PointF[] points)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			if (series.Appearance.LineSeriesAppearance.Width >= 1f && series.Appearance.Shadow.Blur >= 0f && series.Appearance.Shadow.Distance > 0f)
			{
				ShadowManager.DrawLineShadow(this.graphics, RenderEngine.GetPen(series.Appearance.LineSeriesAppearance, series.Appearance.Shadow.Color, series.Appearance.Shadow.Distance), points, 1, (int)series.Appearance.LineSeriesAppearance.Width, (int)plotArea.Chart.Appearance.Dimensions.Width.PixelValue, (int)plotArea.Chart.Appearance.Dimensions.Height.PixelValue, (int)series.Appearance.Shadow.Distance, series.Appearance.Shadow.Color, series.Appearance.Shadow.Blur, series.Appearance.Shadow.Position);
			}
			using (Pen pen = this.GetPen(series, index, null))
			{
				if (points.Length > 0)
				{
					if (points.Length == 1)
					{
						this.graphics.DrawLine(pen, points[0], points[0]);
					}
					else
					{
						this.graphics.DrawBeziers(pen, points);
					}
				}
			}
		}

		// Token: 0x0600E98F RID: 59791 RVA: 0x00351858 File Offset: 0x0034FA58
		private void DrawLinesForAreas(ChartSeries series, int index, PointF[] points)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			PointF[] pointsArrayForArea = RenderEngine.GetPointsArrayForArea(points);
			using (Pen pen = this.GetPen(series, index, null))
			{
				this.graphics.DrawLine(pen, points[0], points[1]);
				this.graphics.DrawLine(pen, points[points.Length - 2], points[points.Length - 1]);
			}
			this.DrawLines(series, index, pointsArrayForArea);
		}

		// Token: 0x0600E990 RID: 59792 RVA: 0x003518F8 File Offset: 0x0034FAF8
		private void DrawPolygon(ChartSeries series, int seriesIndex, PointF[] seriesPoints, GraphicsPath grPath, GraphicsPath emptyPath)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			try
			{
				using (Brush brush = this.GetBrush(series, seriesIndex, null, 0, grPath.GetBounds()))
				{
					if (brush != null)
					{
						try
						{
							this.graphics.FillPath(brush, grPath);
						}
						catch
						{
							return;
						}
					}
					if (emptyPath != null)
					{
						using (Brush emptyBrush = this.GetEmptyBrush(series, emptyPath.GetBounds()))
						{
							if (emptyBrush != null)
							{
								try
								{
									this.graphics.FillPath(emptyBrush, emptyPath);
								}
								catch
								{
									return;
								}
							}
						}
					}
					using (Pen pen = this.GetPen(series, seriesIndex, null))
					{
						this.graphics.DrawPath(pen, grPath);
					}
					using (Pen emptyPen = this.GetEmptyPen(series, seriesIndex, null))
					{
						this.graphics.DrawPath(emptyPen, emptyPath);
					}
				}
			}
			finally
			{
				if (grPath != null)
				{
					((IDisposable)grPath).Dispose();
				}
			}
		}

		// Token: 0x0600E991 RID: 59793 RVA: 0x00351A30 File Offset: 0x0034FC30
		internal Brush GetBrush(ChartSeries series, int seriesIndex, ChartSeriesItem item, int itemIndex, RectangleF rect)
		{
			return this.GetBrush(this.GetFillStyle(series, seriesIndex, item, itemIndex), rect);
		}

		// Token: 0x0600E992 RID: 59794 RVA: 0x00351A45 File Offset: 0x0034FC45
		internal Brush GetEmptyBrush(ChartSeries series, RectangleF rect)
		{
			return this.GetBrush(series.Appearance.EmptyValue.FillStyle, rect);
		}

		// Token: 0x0600E993 RID: 59795 RVA: 0x00351A60 File Offset: 0x0034FC60
		internal FillStyle GetFillStyle(ChartSeries series, int seriesIndex, ChartSeriesItem item, int itemIndex)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			FillStyle empty = FillStyleSeries.Empty;
			FillStyle fillStyle = (FillStyleSeries)series.Appearance.FillStyle.Clone();
			FillStyle fillStyle2 = (item == null) ? empty : ((FillStyle)item.Appearance.FillStyle.Clone());
			int index = (series.Type != ChartSeriesType.Pie) ? seriesIndex : itemIndex;
			FillStyle fillStyle3;
			if (series.Type != ChartSeriesType.Pie)
			{
				if (fillStyle2.Equals(empty) || fillStyle2.Equals(fillStyle))
				{
					fillStyle3 = (FillStyleSeries)fillStyle.Clone();
					this.GetDefaultColors(fillStyle3, index);
				}
				else
				{
					fillStyle3 = (FillStyleSeries)fillStyle2.Clone();
				}
			}
			else
			{
				fillStyle3 = (FillStyleSeries)fillStyle2.Clone();
				if (item != null)
				{
					this.GetDefaultColors(fillStyle3, index);
				}
			}
			if (!string.IsNullOrEmpty(plotArea.Chart.SeriesPalette))
			{
				if (this.renderEngineCurrentPalette == null)
				{
					this.renderEngineCurrentPalette = PalettesCollection.GetPalette(plotArea.Chart.SeriesPalette);
					if (this.renderEngineCurrentPalette == null)
					{
						this.renderEngineCurrentPalette = PalettesCollection.GetPalette(plotArea.Chart.SeriesPalette, this.chart);
					}
				}
				PaletteItem paletteItem = this.renderEngineCurrentPalette.GetPaletteItem(index);
				if (fillStyle3 != null)
				{
					fillStyle3.MainColor = paletteItem.MainColor;
					fillStyle3.SecondColor = paletteItem.SecondColor;
					fillStyle3.FillSettings.ComplexGradient.LoadFrom(paletteItem.AdditionalColors);
				}
			}
			return fillStyle3;
		}

		// Token: 0x0600E994 RID: 59796 RVA: 0x00351BBC File Offset: 0x0034FDBC
		private void GetDefaultColors(FillStyle fillStyle, int index)
		{
			if (index >= 0)
			{
				if (fillStyle.MainColor.IsEmpty)
				{
					fillStyle.MainColor = DefaultValues.GetMainColor(index);
				}
				if (fillStyle.SecondColor.IsEmpty)
				{
					fillStyle.SecondColor = DefaultValues.GetSecondColor(index);
				}
			}
		}

		// Token: 0x0600E995 RID: 59797 RVA: 0x00351C08 File Offset: 0x0034FE08
		internal StyleBorder GetLineStyle(ChartSeries series, int seriesIndex, ChartSeriesItem item)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			StyleBorder styleBorder = new StyleSeriesBorder();
			StyleBorder styleBorder2 = (StyleBorder)series.Appearance.Border.Clone();
			StyleBorder styleBorder3 = (item == null) ? styleBorder : ((StyleBorder)item.Appearance.Border.Clone());
			StyleBorder styleBorder4;
			if (series.IsLine || series.IsStackedLine)
			{
				styleBorder4 = (LineStyle)series.Appearance.LineSeriesAppearance.Clone();
				if (styleBorder4.Color == Color.Empty)
				{
					styleBorder4.Color = DefaultValues.GetMainColor(seriesIndex);
				}
				if (!string.IsNullOrEmpty(plotArea.Appearance.SeriesPalette))
				{
					if (this.renderEngineCurrentPalette == null)
					{
						this.renderEngineCurrentPalette = PalettesCollection.GetPalette(plotArea.Appearance.SeriesPalette);
						if (this.renderEngineCurrentPalette == null)
						{
							this.renderEngineCurrentPalette = PalettesCollection.GetPalette(plotArea.Appearance.SeriesPalette, this.chart);
						}
					}
					styleBorder4.Color = this.renderEngineCurrentPalette.GetPaletteItem(seriesIndex).MainColor;
				}
			}
			else if (styleBorder3.Equals(styleBorder2) || styleBorder3.Equals(styleBorder))
			{
				styleBorder4 = styleBorder2;
			}
			else
			{
				styleBorder4 = styleBorder3;
			}
			return styleBorder4;
		}

		// Token: 0x0600E996 RID: 59798 RVA: 0x00351D2A File Offset: 0x0034FF2A
		internal Pen GetPen(ChartSeries series, int seriesIndex, ChartSeriesItem item)
		{
			return RenderEngine.GetPen(this.GetLineStyle(series, seriesIndex, item));
		}

		// Token: 0x0600E997 RID: 59799 RVA: 0x00351D3C File Offset: 0x0034FF3C
		internal Pen GetEmptyPen(ChartSeries series, int seriesIndex, ChartSeriesItem item)
		{
			ChartPlotArea plotArea = this.chart.PlotArea;
			LineStyle lineStyle = new StyleEmptyLineSeries();
			LineStyle line = series.Appearance.EmptyValue.Line;
			if (lineStyle.Equals(line))
			{
				StyleBorder lineStyle2 = this.GetLineStyle(series, seriesIndex, item);
				lineStyle2.PenStyle = lineStyle.PenStyle;
				return RenderEngine.GetPen(lineStyle2);
			}
			return RenderEngine.GetPen(line);
		}

		// Token: 0x0600E998 RID: 59800 RVA: 0x00351D98 File Offset: 0x0034FF98
		private void ResetClip()
		{
			this.graphics.ResetClip();
		}

		// Token: 0x0600E999 RID: 59801 RVA: 0x00351DA5 File Offset: 0x0034FFA5
		private void SetOrderingMode()
		{
			this.chart.PlotArea.XAxis.OrderingMode = this.CheckCategoricalOrderingMode();
		}

		// Token: 0x0600E99A RID: 59802 RVA: 0x00351DC4 File Offset: 0x0034FFC4
		private BarOrderingMode CheckCategoricalOrderingMode()
		{
			ChartSeriesCollection chartSeriesCollection = this.chart.PlotArea.SeriesCollection();
			if (chartSeriesCollection.HaveXValue())
			{
				return BarOrderingMode.Classic;
			}
			return BarOrderingMode.Strict;
		}

		// Token: 0x0400430B RID: 17163
		internal Image image;

		// Token: 0x0400430C RID: 17164
		internal ChartGraphics graphics;

		// Token: 0x0400430D RID: 17165
		internal readonly Chart chart;

		// Token: 0x0400430E RID: 17166
		private ChartSeriesCollection seriesList;

		// Token: 0x0400430F RID: 17167
		internal bool getAxisItemBoundOnly = true;

		// Token: 0x04004310 RID: 17168
		private float bitmapResolution;

		// Token: 0x04004311 RID: 17169
		private ChartSeriesCollection originalSeries;

		// Token: 0x04004312 RID: 17170
		private bool renderEngineDrawOnlyShadow;

		// Token: 0x04004313 RID: 17171
		private float barWidth;

		// Token: 0x04004314 RID: 17172
		private float barWidthRatio;

		// Token: 0x04004315 RID: 17173
		private Palette renderEngineCurrentPalette;
	}
}
