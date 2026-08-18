using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001701 RID: 5889
	[ParseChildren(true)]
	[DefaultProperty("TextBlock")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class SeriesItemLabel : ChartBaseLabel
	{
		// Token: 0x0600E4E2 RID: 58594 RVA: 0x0032D520 File Offset: 0x0032B720
		public SeriesItemLabel() : this(null)
		{
		}

		// Token: 0x0600E4E3 RID: 58595 RVA: 0x0032D52C File Offset: 0x0032B72C
		public SeriesItemLabel(ChartSeries series) : base(null, null, new TextBlockSeriesItem(), new StyleSeriesItemLabel())
		{
			if (series != null)
			{
				this.appearance.Chart = series.Parent.Parent;
				this.appearance.styleContainerObject = series;
				this.chartBaseLabelTextBlock.Appearance.TextProperties.textPropertiesContainerObject = series;
			}
			this.seriesItemLabelConnectionMidPoint = (this.seriesItemLabelConnectionPoint = DefaultValues.LABEL_ITEM_CONNECTION_POINT);
			RenderedObject chartBaseLabelTextBlock = this.chartBaseLabelTextBlock;
			this.chartBaseLabelTextBlock.Parent = this;
			chartBaseLabelTextBlock.Container = this;
		}

		// Token: 0x170045CD RID: 17869
		// (get) Token: 0x0600E4E4 RID: 58596 RVA: 0x0032D5B9 File Offset: 0x0032B7B9
		// (set) Token: 0x0600E4E5 RID: 58597 RVA: 0x0032D5C1 File Offset: 0x0032B7C1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal PointF ConnectionPoint
		{
			get
			{
				return this.seriesItemLabelConnectionPoint;
			}
			set
			{
				this.seriesItemLabelConnectionPoint = value;
			}
		}

		// Token: 0x170045CE RID: 17870
		// (get) Token: 0x0600E4E6 RID: 58598 RVA: 0x0032D5CA File Offset: 0x0032B7CA
		// (set) Token: 0x0600E4E7 RID: 58599 RVA: 0x0032D5D2 File Offset: 0x0032B7D2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal PointF ConnectionMidPoint
		{
			get
			{
				return this.seriesItemLabelConnectionMidPoint;
			}
			set
			{
				this.seriesItemLabelConnectionMidPoint = value;
			}
		}

		// Token: 0x170045CF RID: 17871
		// (get) Token: 0x0600E4E8 RID: 58600 RVA: 0x0032D5DB File Offset: 0x0032B7DB
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public StyleSeriesItemLabel Appearance
		{
			get
			{
				return (StyleSeriesItemLabel)this.appearance;
			}
		}

		// Token: 0x170045D0 RID: 17872
		// (get) Token: 0x0600E4E9 RID: 58601 RVA: 0x0032D5E8 File Offset: 0x0032B7E8
		private Dimensions TextDimensions
		{
			get
			{
				return this.TextBlock.Appearance.Dimensions;
			}
		}

		// Token: 0x0600E4EA RID: 58602 RVA: 0x0032D5FC File Offset: 0x0032B7FC
		private int CheckPlotAreaIntersection(ChartPlotArea plotArea)
		{
			RectangleF realBounds = Style.GetRealBounds(this.TextDimensions, new float?(this.Appearance.RotationAngle));
			RectangleF realBounds2 = Style.GetRealBounds(this.Appearance.Dimensions, new float?(this.Appearance.RotationAngle));
			if (this.Appearance.Position.Y + this.TextBlock.Appearance.Position.Y + (realBounds2.Height - realBounds.Height) / 2f < plotArea.Appearance.Position.Y)
			{
				return 1;
			}
			if (this.Appearance.Position.X + this.TextBlock.Appearance.Position.X + realBounds.Width - this.TextBlock.Appearance.Dimensions.Paddings.Right.PixelValue > plotArea.Appearance.Position.X + plotArea.Appearance.Dimensions.Width.PixelValue)
			{
				return 2;
			}
			if (this.Appearance.Position.Y + this.TextBlock.Appearance.Position.Y + realBounds.Height - this.TextBlock.Appearance.Dimensions.Paddings.Bottom.PixelValue > plotArea.Appearance.Position.Y + plotArea.Appearance.Dimensions.Height.PixelValue)
			{
				return 3;
			}
			if (this.Appearance.Position.X + this.TextBlock.Appearance.Position.X + this.TextBlock.Appearance.Position.X + (realBounds2.Width - realBounds.Width) / 2f < plotArea.Appearance.Position.X)
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x0600E4EB RID: 58603 RVA: 0x0032D7F0 File Offset: 0x0032B9F0
		private void AdjustPositionByPlotArea(ChartPlotArea plotArea, int side)
		{
			RectangleF realBounds = Style.GetRealBounds(this.Appearance.Dimensions, new float?(this.Appearance.RotationAngle));
			RectangleF realBounds2 = Style.GetRealBounds(this.TextDimensions, new float?(this.Appearance.RotationAngle));
			switch (side)
			{
			case 1:
				this.Appearance.Position.Y = plotArea.Appearance.Position.Y - this.TextBlock.Appearance.Position.Y - (realBounds.Height - realBounds2.Height) / 2f;
				return;
			case 2:
				this.Appearance.Position.X = plotArea.Appearance.Position.X + plotArea.Appearance.Dimensions.Width.PixelValue - realBounds2.Width - this.TextBlock.Appearance.Position.X + this.TextBlock.Appearance.Dimensions.Paddings.Right.PixelValue;
				return;
			case 3:
				this.Appearance.Position.Y = plotArea.Appearance.Position.Y + plotArea.Appearance.Dimensions.Height.PixelValue - realBounds2.Height - this.TextBlock.Appearance.Position.Y + this.TextBlock.Appearance.Dimensions.Paddings.Bottom.PixelValue;
				return;
			case 4:
				this.Appearance.Position.X = plotArea.Appearance.Position.X - this.TextBlock.Appearance.Position.X - (realBounds.Width - realBounds2.Width) / 2f;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600E4EC RID: 58604 RVA: 0x0032D9D8 File Offset: 0x0032BBD8
		internal void SetOutsideCoordinates(RectangleF rect, bool isAuto)
		{
			int distance = this.Appearance.Distance;
			RectangleF realBounds = Style.GetRealBounds(this.Appearance.Dimensions, new float?(this.Appearance.RotationAngle));
			AlignedPositions alignedPosition = this.Appearance.Position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Center)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.TopLeft:
					if (!rect.IsEmpty)
					{
						this.Appearance.Position.X = rect.X;
					}
					this.Appearance.Position.X -= realBounds.Width + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Right.PixelValue);
					this.Appearance.Position.Y -= realBounds.Height + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Bottom.PixelValue);
					break;
				case AlignedPositions.Top:
					this.Appearance.Position.X -= realBounds.Width / 2f;
					this.Appearance.Position.Y -= realBounds.Height + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Bottom.PixelValue);
					break;
				case (AlignedPositions)3:
					break;
				case AlignedPositions.TopRight:
					if (!rect.IsEmpty)
					{
						this.Appearance.Position.X = rect.X + rect.Width;
					}
					this.Appearance.Position.X += (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Left.PixelValue);
					this.Appearance.Position.Y -= realBounds.Height + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Bottom.PixelValue);
					break;
				default:
					if (alignedPosition != AlignedPositions.Left)
					{
						if (alignedPosition == AlignedPositions.Center)
						{
							this.Appearance.Position.X -= realBounds.Width / 2f;
							this.Appearance.Position.Y += ((!rect.IsEmpty) ? (rect.Height / 2f) : 0f) - realBounds.Height / 2f;
						}
					}
					else
					{
						if (!rect.IsEmpty)
						{
							this.Appearance.Position.X = rect.X;
						}
						this.Appearance.Position.X -= realBounds.Width + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Right.PixelValue);
						this.Appearance.Position.Y += ((!rect.IsEmpty) ? (rect.Height / 2f) : 0f) - realBounds.Height / 2f;
					}
					break;
				}
			}
			else if (alignedPosition <= AlignedPositions.BottomLeft)
			{
				if (alignedPosition != AlignedPositions.Right)
				{
					if (alignedPosition == AlignedPositions.BottomLeft)
					{
						if (!rect.IsEmpty)
						{
							this.Appearance.Position.X = rect.X;
						}
						this.Appearance.Position.X -= realBounds.Width + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Right.PixelValue);
						this.Appearance.Position.Y += ((!rect.IsEmpty) ? rect.Height : 0f) + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Top.PixelValue);
					}
				}
				else
				{
					if (!rect.IsEmpty)
					{
						this.Appearance.Position.X = rect.X + rect.Width;
					}
					this.Appearance.Position.X += (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Left.PixelValue);
					this.Appearance.Position.Y += ((!rect.IsEmpty) ? (rect.Height / 2f) : 0f) - realBounds.Height / 2f;
				}
			}
			else if (alignedPosition != AlignedPositions.Bottom)
			{
				if (alignedPosition == AlignedPositions.BottomRight)
				{
					if (!rect.IsEmpty)
					{
						this.Appearance.Position.X = rect.X + rect.Width;
					}
					this.Appearance.Position.X += (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Left.PixelValue);
					this.Appearance.Position.Y += ((!rect.IsEmpty) ? rect.Height : 0f) + this.Appearance.Dimensions.Margins.Top.PixelValue;
				}
			}
			else
			{
				this.Appearance.Position.X -= realBounds.Width / 2f;
				this.Appearance.Position.Y += ((!rect.IsEmpty) ? rect.Height : 0f) + (isAuto ? ((float)distance) : this.Appearance.Dimensions.Margins.Top.PixelValue);
			}
			this.Appearance.Position.AlignedPosition = AlignedPositions.TopLeft;
		}

		// Token: 0x0600E4ED RID: 58605 RVA: 0x0032DFE4 File Offset: 0x0032C1E4
		internal void SetInsideCoordinates(RectangleF rect)
		{
			RectangleF realBounds = Style.GetRealBounds(this.Appearance.Dimensions, new float?(this.Appearance.RotationAngle));
			AlignedPositions alignedPosition = this.Appearance.Position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Center)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.TopLeft:
					this.Appearance.Position.X = rect.Left + this.Appearance.Dimensions.Margins.Left.PixelValue;
					this.Appearance.Position.Y = rect.Top + this.Appearance.Dimensions.Margins.Top.PixelValue;
					break;
				case AlignedPositions.Top:
					this.Appearance.Position.X = rect.Left + rect.Width / 2f - realBounds.Width / 2f;
					this.Appearance.Position.Y = rect.Top + this.Appearance.Dimensions.Margins.Top.PixelValue;
					break;
				case (AlignedPositions)3:
					break;
				case AlignedPositions.TopRight:
					this.Appearance.Position.X = rect.Right - realBounds.Width - this.Appearance.Dimensions.Margins.Right.PixelValue;
					this.Appearance.Position.Y = rect.Top + this.Appearance.Dimensions.Margins.Top.PixelValue;
					break;
				default:
					if (alignedPosition != AlignedPositions.Left)
					{
						if (alignedPosition == AlignedPositions.Center)
						{
							this.Appearance.Position.X = rect.Left + rect.Width / 2f - realBounds.Width / 2f;
							this.Appearance.Position.Y = rect.Top + rect.Height / 2f - realBounds.Height / 2f;
						}
					}
					else
					{
						this.Appearance.Position.X = rect.Left + this.Appearance.Dimensions.Margins.Left.PixelValue;
						this.Appearance.Position.Y = rect.Top + rect.Height / 2f - realBounds.Height / 2f;
					}
					break;
				}
			}
			else if (alignedPosition <= AlignedPositions.BottomLeft)
			{
				if (alignedPosition != AlignedPositions.Right)
				{
					if (alignedPosition == AlignedPositions.BottomLeft)
					{
						this.Appearance.Position.X = rect.Left + this.Appearance.Dimensions.Margins.Left.PixelValue;
						this.Appearance.Position.Y = rect.Bottom - realBounds.Height - this.Appearance.Dimensions.Margins.Bottom.PixelValue;
					}
				}
				else
				{
					this.Appearance.Position.X = rect.Right - realBounds.Width - this.Appearance.Dimensions.Margins.Right.PixelValue;
					this.Appearance.Position.Y = rect.Top + rect.Height / 2f - realBounds.Height / 2f;
				}
			}
			else if (alignedPosition != AlignedPositions.Bottom)
			{
				if (alignedPosition == AlignedPositions.BottomRight)
				{
					this.Appearance.Position.X = rect.Right - realBounds.Width - this.Appearance.Dimensions.Margins.Right.PixelValue;
					this.Appearance.Position.Y = rect.Bottom - realBounds.Height - this.Appearance.Dimensions.Margins.Bottom.PixelValue;
				}
			}
			else
			{
				this.Appearance.Position.X = rect.Left + rect.Width / 2f - realBounds.Width / 2f;
				this.Appearance.Position.Y = rect.Bottom - realBounds.Height - this.Appearance.Dimensions.Margins.Bottom.PixelValue;
			}
			this.Appearance.Position.AlignedPosition = AlignedPositions.TopLeft;
		}

		// Token: 0x0600E4EE RID: 58606 RVA: 0x0032E480 File Offset: 0x0032C680
		internal bool IsVisible(ChartSeries series)
		{
			return series.Appearance.LabelAppearance.Visible && this.Visible;
		}

		// Token: 0x0600E4EF RID: 58607 RVA: 0x0032E49C File Offset: 0x0032C69C
		internal void CalculateLayout(PointF locationPoint, PointF connectionPoint, bool showLabelConnectors, RenderEngine engine)
		{
			if (this.Appearance.Dimensions.IsZero())
			{
				SizeF sizeF = this.Measure(engine);
				this.Appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
			}
			this.Appearance.Position.X = locationPoint.X;
			this.Appearance.Position.Y = locationPoint.Y;
			if (showLabelConnectors)
			{
				this.ConnectionPoint = connectionPoint;
			}
		}

		// Token: 0x0600E4F0 RID: 58608 RVA: 0x0032E51C File Offset: 0x0032C71C
		internal void Adjust(ChartPlotArea plotArea)
		{
			int num = 0;
			int num2 = this.CheckPlotAreaIntersection(plotArea);
			while (num < 5 && num2 != 0)
			{
				this.AdjustPositionByPlotArea(plotArea, num2);
				num2 = this.CheckPlotAreaIntersection(plotArea);
				num++;
			}
			this.Appearance.Position.X -= plotArea.Appearance.Position.X;
			this.Appearance.Position.Y -= plotArea.Appearance.Position.Y;
		}

		// Token: 0x0600E4F1 RID: 58609 RVA: 0x0032E5A0 File Offset: 0x0032C7A0
		internal static PointF AdjustLabelConnectionPointForPie(double rotationAngle, PointF connectionPoint)
		{
			if (rotationAngle > 0.0 && rotationAngle < 1.5707963267948966)
			{
				return new PointF(connectionPoint.X + 2f, connectionPoint.Y + 1f);
			}
			if (rotationAngle == 1.5707963267948966)
			{
				return new PointF(connectionPoint.X, connectionPoint.Y + 2f);
			}
			return connectionPoint;
		}

		// Token: 0x0600E4F2 RID: 58610 RVA: 0x0032E60C File Offset: 0x0032C80C
		public override object Clone()
		{
			SeriesItemLabel seriesItemLabel = (SeriesItemLabel)base.Clone();
			seriesItemLabel.Visible = this.Visible;
			seriesItemLabel.appearance = (StyleSeriesItemLabel)this.Appearance.Clone();
			return seriesItemLabel;
		}

		// Token: 0x040041FC RID: 16892
		private PointF seriesItemLabelConnectionPoint;

		// Token: 0x040041FD RID: 16893
		private PointF seriesItemLabelConnectionMidPoint;

		// Token: 0x040041FE RID: 16894
		internal RectangleF seriesItemLabelRectangle;
	}
}
