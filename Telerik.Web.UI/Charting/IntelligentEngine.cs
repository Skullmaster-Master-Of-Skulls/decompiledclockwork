using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200170B RID: 5899
	internal class IntelligentEngine
	{
		// Token: 0x0600E55D RID: 58717 RVA: 0x0032F144 File Offset: 0x0032D344
		internal static List<SeriesItemLabel> Distribute(List<SeriesItemLabel> labels, RectangleF plotRect, bool verticalOnly)
		{
			int num = 0;
			if (labels.Count > 0)
			{
				while (num++ < labels.Count)
				{
					for (int i = 0; i < labels.Count; i++)
					{
						RectangleF[] array = new RectangleF[labels.Count - 1];
						int num2 = 0;
						for (int j = 0; j < labels.Count; j++)
						{
							if (j != i)
							{
								array[num2++] = labels[j].seriesItemLabelRectangle;
							}
						}
						IntelligentEngine.Distribute(array, labels[i], verticalOnly);
						if (!IntelligentEngine.IsLocateInVisibleArea(labels[i], plotRect))
						{
							labels.RemoveAt(i);
							i--;
						}
					}
				}
			}
			return labels;
		}

		// Token: 0x0600E55E RID: 58718 RVA: 0x0032F1F8 File Offset: 0x0032D3F8
		private static void Distribute(RectangleF[] rects, SeriesItemLabel moveLabel, bool verticalOnly)
		{
			PointF? pointF = null;
			RectangleF? rectangleF = null;
			int num = 0;
			long num2 = (long)(rects.Length * rects.Length);
			while (IntelligentEngine.HitTest(rects, moveLabel.seriesItemLabelRectangle, ref pointF, ref rectangleF))
			{
				if (pointF != null && rectangleF != null)
				{
					IntelligentEngine.MoveData moveData = verticalOnly ? IntelligentEngine.GetMoveDataVerticalyOnly(rectangleF.Value, moveLabel.seriesItemLabelRectangle, pointF.Value) : IntelligentEngine.GetMoveData(rectangleF.Value, moveLabel.seriesItemLabelRectangle, pointF.Value);
					PointF point = new PointF(moveLabel.seriesItemLabelRectangle.Left, moveLabel.seriesItemLabelRectangle.Top);
					switch (moveData.Direction)
					{
					case IntelligentEngine.Direction.ToLeft:
						point.X -= moveData.Distance;
						break;
					case IntelligentEngine.Direction.ToBottom:
						point.Y += moveData.Distance;
						break;
					case IntelligentEngine.Direction.ToRight:
						point.X += moveData.Distance;
						break;
					case IntelligentEngine.Direction.ToTop:
						point.Y -= moveData.Distance;
						break;
					}
					IntelligentEngine.MoveTo(ref moveLabel.seriesItemLabelRectangle, point);
					if ((long)num++ > num2)
					{
						return;
					}
				}
			}
		}

		// Token: 0x0600E55F RID: 58719 RVA: 0x0032F33C File Offset: 0x0032D53C
		internal static bool IsLocateInVisibleArea(SeriesItemLabel label, RectangleF area)
		{
			label.Appearance.Position.X = label.seriesItemLabelRectangle.X;
			label.Appearance.Position.Y = label.seriesItemLabelRectangle.Y;
			Position position = RenderEngine.LocalToGlobal(label);
			RectangleF rect = new RectangleF(position.X, position.Y, label.Appearance.Dimensions.Width.PixelValue, label.Appearance.Dimensions.Height.PixelValue);
			return area.IntersectsWith(rect);
		}

		// Token: 0x0600E560 RID: 58720 RVA: 0x0032F3CC File Offset: 0x0032D5CC
		private static bool HitTest(RectangleF[] rects, RectangleF rect, ref PointF? cPoint, ref RectangleF? cRect)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddRectangle(rect);
			foreach (RectangleF value in rects)
			{
				foreach (PointF pointF in graphicsPath.PathPoints)
				{
					if (value.Contains(pointF))
					{
						cPoint = new PointF?(pointF);
						cRect = new RectangleF?(value);
						return true;
					}
				}
			}
			cPoint = null;
			cRect = null;
			return false;
		}

		// Token: 0x0600E561 RID: 58721 RVA: 0x0032F46C File Offset: 0x0032D66C
		private static void MoveTo(ref RectangleF rect, PointF point)
		{
			rect = new RectangleF(point, rect.Size);
		}

		// Token: 0x0600E562 RID: 58722 RVA: 0x0032F480 File Offset: 0x0032D680
		private static float GetDistance(PointF point1, PointF point2)
		{
			return (float)Math.Sqrt(Math.Pow((double)(point2.X - point1.X), 2.0) + Math.Pow((double)(point2.Y - point1.Y), 2.0));
		}

		// Token: 0x0600E563 RID: 58723 RVA: 0x0032F4D0 File Offset: 0x0032D6D0
		private static IntelligentEngine.MoveData GetMoveData(RectangleF rect, RectangleF rect2, PointF ipoint)
		{
			PointF point = ipoint;
			PointF point2 = new PointF(point.X, rect.Top);
			PointF point3 = new PointF(point.X, rect.Bottom);
			PointF point4 = new PointF(rect.Left, point.Y);
			PointF point5 = new PointF(rect.Right, point.Y);
			float[] array = new float[]
			{
				IntelligentEngine.GetDistance(point, point4),
				IntelligentEngine.GetDistance(point, point3),
				IntelligentEngine.GetDistance(point, point5),
				IntelligentEngine.GetDistance(point, point2)
			};
			int valueIndex = IntelligentEngine.GetValueIndex(array);
			IntelligentEngine.MoveData result = default(IntelligentEngine.MoveData);
			result.Direction = (IntelligentEngine.Direction)valueIndex;
			switch (result.Direction)
			{
			case IntelligentEngine.Direction.ToLeft:
			case IntelligentEngine.Direction.ToTop:
				result.Distance = (IntelligentEngine.IsVertical(result.Direction) ? rect2.Height : rect2.Width) + array[valueIndex];
				break;
			case IntelligentEngine.Direction.ToBottom:
			case IntelligentEngine.Direction.ToRight:
				result.Distance = array[valueIndex];
				break;
			}
			result.Distance += 1f;
			return result;
		}

		// Token: 0x0600E564 RID: 58724 RVA: 0x0032F5F4 File Offset: 0x0032D7F4
		private static IntelligentEngine.MoveData GetMoveDataVerticalyOnly(RectangleF rect, RectangleF rect2, PointF ipoint)
		{
			PointF point = ipoint;
			PointF point2 = new PointF(point.X, rect.Top);
			PointF point3 = new PointF(point.X, rect.Bottom);
			float[] array = new float[]
			{
				IntelligentEngine.GetDistance(point, point3),
				IntelligentEngine.GetDistance(point, point2)
			};
			int valueIndex = IntelligentEngine.GetValueIndex(array);
			int direction = (valueIndex == 0) ? 1 : 3;
			IntelligentEngine.MoveData result = default(IntelligentEngine.MoveData);
			result.Direction = (IntelligentEngine.Direction)direction;
			switch (result.Direction)
			{
			case IntelligentEngine.Direction.ToBottom:
				result.Distance = array[valueIndex];
				break;
			case IntelligentEngine.Direction.ToTop:
				result.Distance = (IntelligentEngine.IsVertical(result.Direction) ? rect2.Height : rect2.Width) + array[valueIndex];
				break;
			}
			result.Distance += 1f;
			return result;
		}

		// Token: 0x0600E565 RID: 58725 RVA: 0x0032F6D8 File Offset: 0x0032D8D8
		private static int GetValueIndex(float[] dims)
		{
			int num = 0;
			for (int i = 1; i < dims.Length; i++)
			{
				if (dims[i] <= dims[num])
				{
					num = i;
				}
			}
			if (num == 3 && dims[num] == 0f)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x0600E566 RID: 58726 RVA: 0x0032F710 File Offset: 0x0032D910
		private static bool IsVertical(IntelligentEngine.Direction direction)
		{
			return direction == IntelligentEngine.Direction.ToTop || direction == IntelligentEngine.Direction.ToBottom;
		}

		// Token: 0x0200170C RID: 5900
		private enum Direction
		{
			// Token: 0x04004207 RID: 16903
			ToLeft,
			// Token: 0x04004208 RID: 16904
			ToBottom,
			// Token: 0x04004209 RID: 16905
			ToRight,
			// Token: 0x0400420A RID: 16906
			ToTop
		}

		// Token: 0x0200170D RID: 5901
		private struct MoveData
		{
			// Token: 0x170045E5 RID: 17893
			// (get) Token: 0x0600E568 RID: 58728 RVA: 0x0032F724 File Offset: 0x0032D924
			// (set) Token: 0x0600E569 RID: 58729 RVA: 0x0032F72C File Offset: 0x0032D92C
			internal float Distance
			{
				get
				{
					return this.moveDataDistance;
				}
				set
				{
					this.moveDataDistance = value;
				}
			}

			// Token: 0x170045E6 RID: 17894
			// (get) Token: 0x0600E56A RID: 58730 RVA: 0x0032F735 File Offset: 0x0032D935
			// (set) Token: 0x0600E56B RID: 58731 RVA: 0x0032F73D File Offset: 0x0032D93D
			internal IntelligentEngine.Direction Direction
			{
				get
				{
					return this.moveDataDirection;
				}
				set
				{
					this.moveDataDirection = value;
				}
			}

			// Token: 0x0400420B RID: 16907
			private float moveDataDistance;

			// Token: 0x0400420C RID: 16908
			private IntelligentEngine.Direction moveDataDirection;
		}
	}
}
