using System;
using System.Collections;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016DE RID: 5854
	public abstract class LayoutElement : RenderedObject
	{
		// Token: 0x0600E2FE RID: 58110 RVA: 0x00324B58 File Offset: 0x00322D58
		public LayoutElement(IContainer container) : this(null, container)
		{
		}

		// Token: 0x0600E2FF RID: 58111 RVA: 0x00324B62 File Offset: 0x00322D62
		public LayoutElement(LayoutStyle appearance, IContainer container) : base(container)
		{
			this.appearance = (appearance ?? new LayoutStyle(this));
		}

		// Token: 0x0600E300 RID: 58112 RVA: 0x00324B7C File Offset: 0x00322D7C
		private static float GetOffset(object oelement, LayoutElement.OffsetCalculationDelegate calcMethod)
		{
			float result = 0f;
			IOrdering ordering = oelement as IOrdering;
			if (ordering != null)
			{
				IContainer container = ordering.Container;
				int order = container.GetOrder(ordering);
				if (order > 0)
				{
					IOrdering ordering2 = container.OrderList[order - 1];
					if (ordering2 != null)
					{
						bool flag = (bool)Style.GetStyleProperty(ordering2, StyleProperties.Visible);
						if (flag)
						{
							Position position = (Position)Style.GetStyleProperty(ordering, StyleProperties.Position);
							Position position2 = (Position)Style.GetStyleProperty(ordering2, StyleProperties.Position);
							if (position2 != null && position != null && position2.AlignedPosition == position.AlignedPosition)
							{
								result = calcMethod(ordering2, container, position2);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600E301 RID: 58113 RVA: 0x00324C55 File Offset: 0x00322E55
		protected float GetOffsetLeft(object oelement)
		{
			return LayoutElement.GetOffset(oelement, delegate(IOrdering prevElem, IContainer container, Position prevElemPosition)
			{
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(prevElem, StyleProperties.Dimensions);
				return prevElemPosition.X + dimensions.Width.PixelValue + dimensions.Margins.Right.PixelValue;
			});
		}

		// Token: 0x0600E302 RID: 58114 RVA: 0x00324CB9 File Offset: 0x00322EB9
		protected float GetOffsetTop(object element)
		{
			return LayoutElement.GetOffset(element, delegate(IOrdering prevElem, IContainer container, Position prevElemPosition)
			{
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(prevElem, StyleProperties.Dimensions);
				return prevElemPosition.Y + dimensions.Height.PixelValue + dimensions.Margins.Bottom.PixelValue;
			});
		}

		// Token: 0x0600E303 RID: 58115 RVA: 0x00324D2A File Offset: 0x00322F2A
		protected float GetOffsetRight(object element)
		{
			return LayoutElement.GetOffset(element, delegate(IOrdering prevElem, IContainer container, Position prevElemPosition)
			{
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(prevElem, StyleProperties.Dimensions);
				Dimensions dimensions2 = (Dimensions)Style.GetStyleProperty(container, StyleProperties.Dimensions);
				return dimensions2.Width.PixelValue - (prevElemPosition.X - dimensions.Margins.Left.PixelValue);
			});
		}

		// Token: 0x0600E304 RID: 58116 RVA: 0x00324D9A File Offset: 0x00322F9A
		protected float GetOffsetBottom(object element)
		{
			return LayoutElement.GetOffset(element, delegate(IOrdering prevElem, IContainer container, Position prevElemPosition)
			{
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(prevElem, StyleProperties.Dimensions);
				Dimensions dimensions2 = (Dimensions)Style.GetStyleProperty(container, StyleProperties.Dimensions);
				return dimensions2.Height.PixelValue - (prevElemPosition.Y - dimensions.Margins.Top.PixelValue);
			});
		}

		// Token: 0x0600E305 RID: 58117 RVA: 0x00324DC0 File Offset: 0x00322FC0
		internal void CalculatePosition(ISizesAndPaddings containerDimensions)
		{
			if (!this.appearance.Visible)
			{
				return;
			}
			Position position = this.appearance.Position;
			if (!position.requireCalculation)
			{
				return;
			}
			Dimensions dimensions = this.appearance.Dimensions;
			position.ResetGlobal();
			if (!position.Auto)
			{
				return;
			}
			AlignedPositions alignedPosition = position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Center)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.TopLeft:
				{
					float num = this.GetOffsetLeft(this);
					position.X = ((num == 0f) ? containerDimensions.Paddings.Left.PixelValue : num) + dimensions.Margins.Left.PixelValue;
					position.Y = containerDimensions.Paddings.Top.PixelValue + dimensions.Margins.Top.PixelValue;
					return;
				}
				case AlignedPositions.Top:
				{
					float num2 = this.GetOffsetTop(this);
					position.X = (containerDimensions.Width.PixelValue - dimensions.Width.PixelValue) / 2f;
					position.Y = ((num2 == 0f) ? containerDimensions.Paddings.Top.PixelValue : num2) + dimensions.Margins.Top.PixelValue;
					return;
				}
				case (AlignedPositions)3:
					break;
				case AlignedPositions.TopRight:
				{
					float num = this.GetOffsetRight(this);
					position.X = containerDimensions.Width.PixelValue - ((num == 0f) ? containerDimensions.Paddings.Right.PixelValue : num) - dimensions.Margins.Right.PixelValue - dimensions.Width.PixelValue;
					position.Y = containerDimensions.Paddings.Top.PixelValue + dimensions.Margins.Top.PixelValue;
					break;
				}
				default:
					if (alignedPosition == AlignedPositions.Left)
					{
						float num = this.GetOffsetLeft(this);
						position.X = ((num == 0f) ? containerDimensions.Paddings.Left.PixelValue : num) + dimensions.Margins.Left.PixelValue;
						position.Y = (containerDimensions.Height.PixelValue - dimensions.Height.PixelValue) / 2f;
						return;
					}
					if (alignedPosition != AlignedPositions.Center)
					{
						return;
					}
					position.X = (containerDimensions.Width.PixelValue - dimensions.Width.PixelValue) / 2f;
					position.Y = (containerDimensions.Height.PixelValue - dimensions.Height.PixelValue) / 2f;
					return;
				}
				return;
			}
			if (alignedPosition <= AlignedPositions.BottomLeft)
			{
				if (alignedPosition == AlignedPositions.Right)
				{
					float num = this.GetOffsetRight(this);
					position.X = containerDimensions.Width.PixelValue - ((num == 0f) ? containerDimensions.Paddings.Right.PixelValue : num) - dimensions.Margins.Right.PixelValue - dimensions.Width.PixelValue;
					position.Y = (containerDimensions.Height.PixelValue - dimensions.Height.PixelValue) / 2f;
					return;
				}
				if (alignedPosition != AlignedPositions.BottomLeft)
				{
					return;
				}
				position.X = containerDimensions.Paddings.Left.PixelValue + dimensions.Margins.Left.PixelValue;
				position.Y = containerDimensions.Height.PixelValue - containerDimensions.Paddings.Bottom.PixelValue - dimensions.Margins.Bottom.PixelValue - dimensions.Height.PixelValue;
				return;
			}
			else
			{
				if (alignedPosition == AlignedPositions.Bottom)
				{
					float num2 = this.GetOffsetBottom(this);
					position.X = (containerDimensions.Width.PixelValue - dimensions.Width.PixelValue) / 2f;
					position.Y = containerDimensions.Height.PixelValue - ((num2 == 0f) ? containerDimensions.Paddings.Bottom.PixelValue : num2) - dimensions.Margins.Bottom.PixelValue - dimensions.Height.PixelValue;
					return;
				}
				if (alignedPosition != AlignedPositions.BottomRight)
				{
					return;
				}
				position.X = containerDimensions.Width.PixelValue - containerDimensions.Paddings.Right.PixelValue - dimensions.Margins.Right.PixelValue - dimensions.Width.PixelValue;
				position.Y = containerDimensions.Height.PixelValue - containerDimensions.Paddings.Bottom.PixelValue - dimensions.Margins.Bottom.PixelValue - dimensions.Height.PixelValue;
				return;
			}
		}

		// Token: 0x0600E306 RID: 58118 RVA: 0x0032522C File Offset: 0x0032342C
		internal virtual void CalculatePosition(RenderEngine renderEngine)
		{
			if (!this.appearance.Visible)
			{
				return;
			}
			LayoutElement layoutElement = this.objectContainer as LayoutElement;
			if (layoutElement == null)
			{
				throw new Exception("Wrong container set for this element.");
			}
			ISizesAndPaddings dimensions = layoutElement.appearance.Dimensions;
			this.CalculatePosition(dimensions);
		}

		// Token: 0x0600E307 RID: 58119 RVA: 0x00325274 File Offset: 0x00323474
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.appearance).TrackViewState();
		}

		// Token: 0x0600E308 RID: 58120 RVA: 0x00325288 File Offset: 0x00323488
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.appearance).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600E309 RID: 58121 RVA: 0x003252B8 File Offset: 0x003234B8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.appearance).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E30A RID: 58122 RVA: 0x003252F0 File Offset: 0x003234F0
		protected override void Dispose(bool disposing)
		{
			if (this.appearance != null)
			{
				this.appearance.Dispose();
				this.appearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400417D RID: 16765
		internal LayoutStyle appearance;

		// Token: 0x020016DF RID: 5855
		// (Invoke) Token: 0x0600E310 RID: 58128
		private delegate float OffsetCalculationDelegate(IOrdering prevElem, IContainer container, Position prevElemPosition);
	}
}
