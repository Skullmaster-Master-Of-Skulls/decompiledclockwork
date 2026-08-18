using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CE RID: 1230
	internal class LayoutUtils
	{
		// Token: 0x060050AB RID: 20651 RVA: 0x0014F9D0 File Offset: 0x0014DBD0
		public static Size OldGetLargestStringSizeInCollection(Font font, ICollection objects)
		{
			Size empty = Size.Empty;
			if (objects != null)
			{
				foreach (object obj in objects)
				{
					Size size = TextRenderer.MeasureText(obj.ToString(), font, new Size(32767, 32767), TextFormatFlags.SingleLine);
					empty.Width = Math.Max(empty.Width, size.Width);
					empty.Height = Math.Max(empty.Height, size.Height);
				}
			}
			return empty;
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0014FA7C File Offset: 0x0014DC7C
		public static int ContentAlignmentToIndex(ContentAlignment alignment)
		{
			int num = (int)LayoutUtils.xContentAlignmentToIndex((int)(alignment & (ContentAlignment)15));
			int num2 = (int)LayoutUtils.xContentAlignmentToIndex((int)(alignment >> 4 & (ContentAlignment)15));
			int num3 = (int)LayoutUtils.xContentAlignmentToIndex((int)(alignment >> 8 & (ContentAlignment)15));
			int num4 = ((num2 != 0) ? 4 : 0) | ((num3 != 0) ? 8 : 0) | num | num2 | num3;
			return num4 - 1;
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x0014FAC8 File Offset: 0x0014DCC8
		private static byte xContentAlignmentToIndex(int threeBitFlag)
		{
			return (threeBitFlag == 4) ? 3 : ((byte)threeBitFlag);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0014FAE0 File Offset: 0x0014DCE0
		public static Size ConvertZeroToUnbounded(Size size)
		{
			if (size.Width == 0)
			{
				size.Width = int.MaxValue;
			}
			if (size.Height == 0)
			{
				size.Height = int.MaxValue;
			}
			return size;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0014FB10 File Offset: 0x0014DD10
		public static Padding ClampNegativePaddingToZero(Padding padding)
		{
			if (padding.All < 0)
			{
				padding.Left = Math.Max(0, padding.Left);
				padding.Top = Math.Max(0, padding.Top);
				padding.Right = Math.Max(0, padding.Right);
				padding.Bottom = Math.Max(0, padding.Bottom);
			}
			return padding;
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0014FB78 File Offset: 0x0014DD78
		private static AnchorStyles GetOppositeAnchor(AnchorStyles anchor)
		{
			AnchorStyles anchorStyles = AnchorStyles.None;
			if (anchor == AnchorStyles.None)
			{
				return anchorStyles;
			}
			for (int i = 1; i <= 8; i <<= 1)
			{
				switch (anchor & (AnchorStyles)i)
				{
				case AnchorStyles.Top:
					anchorStyles |= AnchorStyles.Bottom;
					break;
				case AnchorStyles.Bottom:
					anchorStyles |= AnchorStyles.Top;
					break;
				case AnchorStyles.Left:
					anchorStyles |= AnchorStyles.Right;
					break;
				case AnchorStyles.Right:
					anchorStyles |= AnchorStyles.Left;
					break;
				}
			}
			return anchorStyles;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0014FBDF File Offset: 0x0014DDDF
		public static TextImageRelation GetOppositeTextImageRelation(TextImageRelation relation)
		{
			return (TextImageRelation)LayoutUtils.GetOppositeAnchor((AnchorStyles)relation);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0014FBE7 File Offset: 0x0014DDE7
		public static Size UnionSizes(Size a, Size b)
		{
			return new Size(Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0014FC14 File Offset: 0x0014DE14
		public static Size IntersectSizes(Size a, Size b)
		{
			return new Size(Math.Min(a.Width, b.Width), Math.Min(a.Height, b.Height));
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0014FC44 File Offset: 0x0014DE44
		public static bool IsIntersectHorizontally(Rectangle rect1, Rectangle rect2)
		{
			return rect1.IntersectsWith(rect2) && ((rect1.X <= rect2.X && rect1.X + rect1.Width >= rect2.X + rect2.Width) || (rect2.X <= rect1.X && rect2.X + rect2.Width >= rect1.X + rect1.Width));
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0014FCC4 File Offset: 0x0014DEC4
		public static bool IsIntersectVertically(Rectangle rect1, Rectangle rect2)
		{
			return rect1.IntersectsWith(rect2) && ((rect1.Y <= rect2.Y && rect1.Y + rect1.Width >= rect2.Y + rect2.Width) || (rect2.Y <= rect1.Y && rect2.Y + rect2.Width >= rect1.Y + rect1.Width));
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0014FD44 File Offset: 0x0014DF44
		internal static AnchorStyles GetUnifiedAnchor(IArrangedElement element)
		{
			DockStyle dock = DefaultLayout.GetDock(element);
			if (dock != DockStyle.None)
			{
				return LayoutUtils.dockingToAnchor[(int)dock];
			}
			return DefaultLayout.GetAnchor(element);
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0014FD69 File Offset: 0x0014DF69
		public static Rectangle AlignAndStretch(Size fitThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			return LayoutUtils.Align(LayoutUtils.Stretch(fitThis, withinThis.Size, anchorStyles), withinThis, anchorStyles);
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0014FD80 File Offset: 0x0014DF80
		public static Rectangle Align(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, anchorStyles), anchorStyles);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0014FD91 File Offset: 0x0014DF91
		public static Rectangle Align(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, align), align);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0014FDA4 File Offset: 0x0014DFA4
		public static Rectangle HAlign(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			if ((anchorStyles & AnchorStyles.Right) != AnchorStyles.None)
			{
				withinThis.X += withinThis.Width - alignThis.Width;
			}
			else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.None)
			{
				withinThis.X += (withinThis.Width - alignThis.Width) / 2;
			}
			withinThis.Width = alignThis.Width;
			return withinThis;
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0014FE0C File Offset: 0x0014E00C
		private static Rectangle HAlign(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			if ((align & (ContentAlignment)1092) != (ContentAlignment)0)
			{
				withinThis.X += withinThis.Width - alignThis.Width;
			}
			else if ((align & (ContentAlignment)546) != (ContentAlignment)0)
			{
				withinThis.X += (withinThis.Width - alignThis.Width) / 2;
			}
			withinThis.Width = alignThis.Width;
			return withinThis;
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0014FE78 File Offset: 0x0014E078
		public static Rectangle VAlign(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			if ((anchorStyles & AnchorStyles.Bottom) != AnchorStyles.None)
			{
				withinThis.Y += withinThis.Height - alignThis.Height;
			}
			else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == AnchorStyles.None)
			{
				withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
			}
			withinThis.Height = alignThis.Height;
			return withinThis;
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0014FEE0 File Offset: 0x0014E0E0
		public static Rectangle VAlign(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			if ((align & (ContentAlignment)1792) != (ContentAlignment)0)
			{
				withinThis.Y += withinThis.Height - alignThis.Height;
			}
			else if ((align & (ContentAlignment)112) != (ContentAlignment)0)
			{
				withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
			}
			withinThis.Height = alignThis.Height;
			return withinThis;
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0014FF4C File Offset: 0x0014E14C
		public static Size Stretch(Size stretchThis, Size withinThis, AnchorStyles anchorStyles)
		{
			Size result = new Size(((anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right)) ? withinThis.Width : stretchThis.Width, ((anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom)) ? withinThis.Height : stretchThis.Height);
			if (result.Width > withinThis.Width)
			{
				result.Width = withinThis.Width;
			}
			if (result.Height > withinThis.Height)
			{
				result.Height = withinThis.Height;
			}
			return result;
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0014FFCC File Offset: 0x0014E1CC
		public static Rectangle InflateRect(Rectangle rect, Padding padding)
		{
			rect.X -= padding.Left;
			rect.Y -= padding.Top;
			rect.Width += padding.Horizontal;
			rect.Height += padding.Vertical;
			return rect;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00150030 File Offset: 0x0014E230
		public static Rectangle DeflateRect(Rectangle rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x00150092 File Offset: 0x0014E292
		public static Size AddAlignedRegion(Size textSize, Size imageSize, TextImageRelation relation)
		{
			return LayoutUtils.AddAlignedRegionCore(textSize, imageSize, LayoutUtils.IsVerticalRelation(relation));
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x001500A4 File Offset: 0x0014E2A4
		public static Size AddAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
		{
			if (vertical)
			{
				currentSize.Width = Math.Max(currentSize.Width, contentSize.Width);
				currentSize.Height += contentSize.Height;
			}
			else
			{
				currentSize.Width += contentSize.Width;
				currentSize.Height = Math.Max(currentSize.Height, contentSize.Height);
			}
			return currentSize;
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00150118 File Offset: 0x0014E318
		public static Padding FlipPadding(Padding padding)
		{
			if (padding.All != -1)
			{
				return padding;
			}
			int num = padding.Top;
			padding.Top = padding.Left;
			padding.Left = num;
			num = padding.Bottom;
			padding.Bottom = padding.Right;
			padding.Right = num;
			return padding;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00150170 File Offset: 0x0014E370
		public static Point FlipPoint(Point point)
		{
			int x = point.X;
			point.X = point.Y;
			point.Y = x;
			return point;
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0015019C File Offset: 0x0014E39C
		public static Rectangle FlipRectangle(Rectangle rect)
		{
			rect.Location = LayoutUtils.FlipPoint(rect.Location);
			rect.Size = LayoutUtils.FlipSize(rect.Size);
			return rect;
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x001501C5 File Offset: 0x0014E3C5
		public static Rectangle FlipRectangleIf(bool condition, Rectangle rect)
		{
			if (!condition)
			{
				return rect;
			}
			return LayoutUtils.FlipRectangle(rect);
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x001501D4 File Offset: 0x0014E3D4
		public static Size FlipSize(Size size)
		{
			int width = size.Width;
			size.Width = size.Height;
			size.Height = width;
			return size;
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00150200 File Offset: 0x0014E400
		public static Size FlipSizeIf(bool condition, Size size)
		{
			if (!condition)
			{
				return size;
			}
			return LayoutUtils.FlipSize(size);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0015020D File Offset: 0x0014E40D
		public static bool IsHorizontalAlignment(ContentAlignment align)
		{
			return !LayoutUtils.IsVerticalAlignment(align);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00150218 File Offset: 0x0014E418
		public static bool IsHorizontalRelation(TextImageRelation relation)
		{
			return (relation & (TextImageRelation)12) > TextImageRelation.Overlay;
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00150221 File Offset: 0x0014E421
		public static bool IsVerticalAlignment(ContentAlignment align)
		{
			return (align & (ContentAlignment)514) > (ContentAlignment)0;
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0015022D File Offset: 0x0014E42D
		public static bool IsVerticalRelation(TextImageRelation relation)
		{
			return (relation & (TextImageRelation)3) > TextImageRelation.Overlay;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00150235 File Offset: 0x0014E435
		public static bool IsZeroWidthOrHeight(Rectangle rectangle)
		{
			return rectangle.Width == 0 || rectangle.Height == 0;
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x0015024C File Offset: 0x0014E44C
		public static bool IsZeroWidthOrHeight(Size size)
		{
			return size.Width == 0 || size.Height == 0;
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00150263 File Offset: 0x0014E463
		public static bool AreWidthAndHeightLarger(Size size1, Size size2)
		{
			return size1.Width >= size2.Width && size1.Height >= size2.Height;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0015028C File Offset: 0x0014E48C
		public static void SplitRegion(Rectangle bounds, Size specifiedContent, AnchorStyles region1Align, out Rectangle region1, out Rectangle region2)
		{
			region1 = (region2 = bounds);
			switch (region1Align)
			{
			case AnchorStyles.Top:
				region1.Height = specifiedContent.Height;
				region2.Y += specifiedContent.Height;
				region2.Height -= specifiedContent.Height;
				return;
			case AnchorStyles.Bottom:
				region1.Y += bounds.Height - specifiedContent.Height;
				region1.Height = specifiedContent.Height;
				region2.Height -= specifiedContent.Height;
				break;
			case AnchorStyles.Top | AnchorStyles.Bottom:
				break;
			case AnchorStyles.Left:
				region1.Width = specifiedContent.Width;
				region2.X += specifiedContent.Width;
				region2.Width -= specifiedContent.Width;
				return;
			default:
				if (region1Align != AnchorStyles.Right)
				{
					return;
				}
				region1.X += bounds.Width - specifiedContent.Width;
				region1.Width = specifiedContent.Width;
				region2.Width -= specifiedContent.Width;
				return;
			}
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x001503B4 File Offset: 0x0014E5B4
		public static void ExpandRegionsToFillBounds(Rectangle bounds, AnchorStyles region1Align, ref Rectangle region1, ref Rectangle region2)
		{
			switch (region1Align)
			{
			case AnchorStyles.Top:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Bottom);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Top);
				return;
			case AnchorStyles.Bottom:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Top);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Bottom);
				break;
			case AnchorStyles.Top | AnchorStyles.Bottom:
				break;
			case AnchorStyles.Left:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Right);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Left);
				return;
			default:
				if (region1Align != AnchorStyles.Right)
				{
					return;
				}
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Left);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Right);
				return;
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00150479 File Offset: 0x0014E679
		public static Size SubAlignedRegion(Size currentSize, Size contentSize, TextImageRelation relation)
		{
			return LayoutUtils.SubAlignedRegionCore(currentSize, contentSize, LayoutUtils.IsVerticalRelation(relation));
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00150488 File Offset: 0x0014E688
		public static Size SubAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
		{
			if (vertical)
			{
				currentSize.Height -= contentSize.Height;
			}
			else
			{
				currentSize.Width -= contentSize.Width;
			}
			return currentSize;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x001504BC File Offset: 0x0014E6BC
		private static Rectangle SubstituteSpecifiedBounds(Rectangle originalBounds, Rectangle substitutionBounds, AnchorStyles specified)
		{
			int left = ((specified & AnchorStyles.Left) != AnchorStyles.None) ? substitutionBounds.Left : originalBounds.Left;
			int top = ((specified & AnchorStyles.Top) != AnchorStyles.None) ? substitutionBounds.Top : originalBounds.Top;
			int right = ((specified & AnchorStyles.Right) != AnchorStyles.None) ? substitutionBounds.Right : originalBounds.Right;
			int bottom = ((specified & AnchorStyles.Bottom) != AnchorStyles.None) ? substitutionBounds.Bottom : originalBounds.Bottom;
			return Rectangle.FromLTRB(left, top, right, bottom);
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0015052A File Offset: 0x0014E72A
		public static Rectangle RTLTranslate(Rectangle bounds, Rectangle withinBounds)
		{
			bounds.X = withinBounds.Width - bounds.Right;
			return bounds;
		}

		// Token: 0x0400349D RID: 13469
		public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);

		// Token: 0x0400349E RID: 13470
		public static readonly Size InvalidSize = new Size(int.MinValue, int.MinValue);

		// Token: 0x0400349F RID: 13471
		public static readonly Rectangle MaxRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

		// Token: 0x040034A0 RID: 13472
		public const ContentAlignment AnyTop = (ContentAlignment)7;

		// Token: 0x040034A1 RID: 13473
		public const ContentAlignment AnyBottom = (ContentAlignment)1792;

		// Token: 0x040034A2 RID: 13474
		public const ContentAlignment AnyLeft = (ContentAlignment)273;

		// Token: 0x040034A3 RID: 13475
		public const ContentAlignment AnyRight = (ContentAlignment)1092;

		// Token: 0x040034A4 RID: 13476
		public const ContentAlignment AnyCenter = (ContentAlignment)546;

		// Token: 0x040034A5 RID: 13477
		public const ContentAlignment AnyMiddle = (ContentAlignment)112;

		// Token: 0x040034A6 RID: 13478
		public const AnchorStyles HorizontalAnchorStyles = AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x040034A7 RID: 13479
		public const AnchorStyles VerticalAnchorStyles = AnchorStyles.Top | AnchorStyles.Bottom;

		// Token: 0x040034A8 RID: 13480
		private static readonly AnchorStyles[] dockingToAnchor = new AnchorStyles[]
		{
			AnchorStyles.Top | AnchorStyles.Left,
			AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
			AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
		};

		// Token: 0x040034A9 RID: 13481
		public static readonly string TestString = "j^";

		// Token: 0x02000866 RID: 2150
		public sealed class MeasureTextCache
		{
			// Token: 0x0600710B RID: 28939 RVA: 0x0019EC0C File Offset: 0x0019CE0C
			public void InvalidateCache()
			{
				this.unconstrainedPreferredSize = LayoutUtils.InvalidSize;
				this.sizeCacheList = null;
			}

			// Token: 0x0600710C RID: 28940 RVA: 0x0019EC20 File Offset: 0x0019CE20
			public Size GetTextSize(string text, Font font, Size proposedConstraints, TextFormatFlags flags)
			{
				if (!this.TextRequiresWordBreak(text, font, proposedConstraints, flags))
				{
					return this.unconstrainedPreferredSize;
				}
				if (this.sizeCacheList == null)
				{
					this.sizeCacheList = new LayoutUtils.MeasureTextCache.PreferredSizeCache[6];
				}
				foreach (LayoutUtils.MeasureTextCache.PreferredSizeCache preferredSizeCache in this.sizeCacheList)
				{
					if (preferredSizeCache.ConstrainingSize == proposedConstraints)
					{
						return preferredSizeCache.PreferredSize;
					}
					Size size = preferredSizeCache.ConstrainingSize;
					if (size.Width == proposedConstraints.Width)
					{
						size = preferredSizeCache.PreferredSize;
						if (size.Height <= proposedConstraints.Height)
						{
							return preferredSizeCache.PreferredSize;
						}
					}
				}
				Size size2 = TextRenderer.MeasureText(text, font, proposedConstraints, flags);
				this.nextCacheEntry = (this.nextCacheEntry + 1) % 6;
				this.sizeCacheList[this.nextCacheEntry] = new LayoutUtils.MeasureTextCache.PreferredSizeCache(proposedConstraints, size2);
				return size2;
			}

			// Token: 0x0600710D RID: 28941 RVA: 0x0019ECF2 File Offset: 0x0019CEF2
			private Size GetUnconstrainedSize(string text, Font font, TextFormatFlags flags)
			{
				if (this.unconstrainedPreferredSize == LayoutUtils.InvalidSize)
				{
					flags &= ~TextFormatFlags.WordBreak;
					this.unconstrainedPreferredSize = TextRenderer.MeasureText(text, font, LayoutUtils.MaxSize, flags);
				}
				return this.unconstrainedPreferredSize;
			}

			// Token: 0x0600710E RID: 28942 RVA: 0x0019ED28 File Offset: 0x0019CF28
			public bool TextRequiresWordBreak(string text, Font font, Size size, TextFormatFlags flags)
			{
				return this.GetUnconstrainedSize(text, font, flags).Width > size.Width;
			}

			// Token: 0x040043FB RID: 17403
			private Size unconstrainedPreferredSize = LayoutUtils.InvalidSize;

			// Token: 0x040043FC RID: 17404
			private const int MaxCacheSize = 6;

			// Token: 0x040043FD RID: 17405
			private int nextCacheEntry = -1;

			// Token: 0x040043FE RID: 17406
			private LayoutUtils.MeasureTextCache.PreferredSizeCache[] sizeCacheList;

			// Token: 0x0200097D RID: 2429
			private struct PreferredSizeCache
			{
				// Token: 0x06007587 RID: 30087 RVA: 0x001A9800 File Offset: 0x001A7A00
				public PreferredSizeCache(Size constrainingSize, Size preferredSize)
				{
					this.ConstrainingSize = constrainingSize;
					this.PreferredSize = preferredSize;
				}

				// Token: 0x040047D1 RID: 18385
				public Size ConstrainingSize;

				// Token: 0x040047D2 RID: 18386
				public Size PreferredSize;
			}
		}
	}
}
