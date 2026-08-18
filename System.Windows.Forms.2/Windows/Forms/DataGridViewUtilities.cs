using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000223 RID: 547
	internal class DataGridViewUtilities
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x000A9190 File Offset: 0x000A7390
		internal static ContentAlignment ComputeDrawingContentAlignmentForCellStyleAlignment(DataGridViewContentAlignment alignment)
		{
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					return ContentAlignment.TopLeft;
				case DataGridViewContentAlignment.TopCenter:
					return ContentAlignment.TopCenter;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					return ContentAlignment.TopRight;
				default:
					if (alignment == DataGridViewContentAlignment.MiddleLeft)
					{
						return ContentAlignment.MiddleLeft;
					}
					if (alignment == DataGridViewContentAlignment.MiddleCenter)
					{
						return ContentAlignment.MiddleCenter;
					}
					break;
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment == DataGridViewContentAlignment.MiddleRight)
				{
					return ContentAlignment.MiddleRight;
				}
				if (alignment == DataGridViewContentAlignment.BottomLeft)
				{
					return ContentAlignment.BottomLeft;
				}
			}
			else
			{
				if (alignment == DataGridViewContentAlignment.BottomCenter)
				{
					return ContentAlignment.BottomCenter;
				}
				if (alignment == DataGridViewContentAlignment.BottomRight)
				{
					return ContentAlignment.BottomRight;
				}
			}
			return ContentAlignment.MiddleCenter;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000A9214 File Offset: 0x000A7414
		internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(bool rightToLeft, DataGridViewContentAlignment alignment, DataGridViewTriState wrapMode)
		{
			TextFormatFlags textFormatFlags;
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					textFormatFlags = TextFormatFlags.Default;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Right;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Default;
					goto IL_CD;
				case DataGridViewContentAlignment.TopCenter:
					textFormatFlags = TextFormatFlags.HorizontalCenter;
					goto IL_CD;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					textFormatFlags = TextFormatFlags.Default;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				default:
					if (alignment != DataGridViewContentAlignment.MiddleLeft)
					{
						if (alignment == DataGridViewContentAlignment.MiddleCenter)
						{
							textFormatFlags = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
							goto IL_CD;
						}
					}
					else
					{
						textFormatFlags = TextFormatFlags.VerticalCenter;
						if (rightToLeft)
						{
							textFormatFlags |= TextFormatFlags.Right;
							goto IL_CD;
						}
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					break;
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment == DataGridViewContentAlignment.BottomLeft)
					{
						textFormatFlags = TextFormatFlags.Bottom;
						if (rightToLeft)
						{
							textFormatFlags |= TextFormatFlags.Right;
							goto IL_CD;
						}
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
				}
				else
				{
					textFormatFlags = TextFormatFlags.VerticalCenter;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				}
			}
			else
			{
				if (alignment == DataGridViewContentAlignment.BottomCenter)
				{
					textFormatFlags = (TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter);
					goto IL_CD;
				}
				if (alignment == DataGridViewContentAlignment.BottomRight)
				{
					textFormatFlags = TextFormatFlags.Bottom;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				}
			}
			textFormatFlags = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			IL_CD:
			if (wrapMode == DataGridViewTriState.False)
			{
				textFormatFlags |= TextFormatFlags.SingleLine;
			}
			else
			{
				textFormatFlags |= TextFormatFlags.WordBreak;
			}
			textFormatFlags |= TextFormatFlags.NoPrefix;
			textFormatFlags |= TextFormatFlags.PreserveGraphicsClipping;
			if (rightToLeft)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}
			return textFormatFlags;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000A931C File Offset: 0x000A751C
		internal static Size GetPreferredRowHeaderSize(Graphics graphics, string val, DataGridViewCellStyle cellStyle, int borderAndPaddingWidths, int borderAndPaddingHeights, bool showRowErrors, bool showGlyph, Size constraintSize, TextFormatFlags flags)
		{
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
			{
				int val2 = 1;
				int num = 1;
				int num2 = constraintSize.Width - borderAndPaddingWidths;
				if (!string.IsNullOrEmpty(val))
				{
					if (showGlyph && num2 >= 18)
					{
						val2 = 15;
						num2 -= 18;
					}
					if (showRowErrors && num2 >= 18)
					{
						val2 = 15;
						num2 -= 18;
					}
					if (num2 > 9)
					{
						num2 -= 9;
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							num = DataGridViewCell.MeasureTextHeight(graphics, val, cellStyle.Font, num2, flags);
						}
						else
						{
							num = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Height;
						}
						num += 2;
					}
				}
				else if ((showGlyph || showRowErrors) && num2 >= 18)
				{
					val2 = 15;
				}
				return new Size(0, Math.Max(val2, num) + borderAndPaddingHeights);
			}
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
			{
				int num3 = 0;
				int num4 = constraintSize.Height - borderAndPaddingHeights;
				if (!string.IsNullOrEmpty(val))
				{
					int num5 = num4 - 2;
					if (num5 > 0)
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							num3 = DataGridViewCell.MeasureTextWidth(graphics, val, cellStyle.Font, num5, flags);
						}
						else
						{
							num3 = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Width;
						}
						num3 += 9;
					}
				}
				if (num4 >= 15)
				{
					if (showGlyph)
					{
						num3 += 18;
					}
					if (showRowErrors)
					{
						num3 += 18;
					}
				}
				num3 = Math.Max(num3, 1);
				num3 += borderAndPaddingWidths;
				return new Size(num3, 0);
			}
			Size result;
			if (!string.IsNullOrEmpty(val))
			{
				if (cellStyle.WrapMode == DataGridViewTriState.True)
				{
					result = DataGridViewCell.MeasureTextPreferredSize(graphics, val, cellStyle.Font, 5f, flags);
				}
				else
				{
					result = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags);
				}
				result.Width += 9;
				result.Height += 2;
			}
			else
			{
				result = new Size(0, 1);
			}
			if (showGlyph)
			{
				result.Width += 18;
			}
			if (showRowErrors)
			{
				result.Width += 18;
			}
			if (showGlyph || showRowErrors)
			{
				result.Height = Math.Max(result.Height, 15);
			}
			result.Width += borderAndPaddingWidths;
			result.Height += borderAndPaddingHeights;
			return result;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000A953E File Offset: 0x000A773E
		internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, DataGridViewCellStyle cellStyle)
		{
			return DataGridViewUtilities.GetTextBounds(cellBounds, text, flags, cellStyle, cellStyle.Font);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000A9550 File Offset: 0x000A7750
		internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, DataGridViewCellStyle cellStyle, Font font)
		{
			if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.Default && TextRenderer.MeasureText(text, font, new Size(2147483647, 2147483647), flags).Width > cellBounds.Width)
			{
				flags |= TextFormatFlags.EndEllipsis;
			}
			Size size = new Size(cellBounds.Width, cellBounds.Height);
			Size size2 = TextRenderer.MeasureText(text, font, size, flags);
			if (size2.Width > size.Width)
			{
				size2.Width = size.Width;
			}
			if (size2.Height > size.Height)
			{
				size2.Height = size.Height;
			}
			if (size2 == size)
			{
				return cellBounds;
			}
			return new Rectangle(DataGridViewUtilities.GetTextLocation(cellBounds, size2, flags, cellStyle), size2);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000A960C File Offset: 0x000A780C
		internal static Point GetTextLocation(Rectangle cellBounds, Size sizeText, TextFormatFlags flags, DataGridViewCellStyle cellStyle)
		{
			Point result = new Point(0, 0);
			DataGridViewContentAlignment dataGridViewContentAlignment = cellStyle.Alignment;
			if ((flags & TextFormatFlags.RightToLeft) != TextFormatFlags.Default)
			{
				if (dataGridViewContentAlignment <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.TopLeft)
					{
						if (dataGridViewContentAlignment != DataGridViewContentAlignment.TopRight)
						{
							if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleLeft)
							{
								dataGridViewContentAlignment = DataGridViewContentAlignment.MiddleRight;
							}
						}
						else
						{
							dataGridViewContentAlignment = DataGridViewContentAlignment.TopLeft;
						}
					}
					else
					{
						dataGridViewContentAlignment = DataGridViewContentAlignment.TopRight;
					}
				}
				else if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomLeft)
					{
						if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
						{
							dataGridViewContentAlignment = DataGridViewContentAlignment.BottomLeft;
						}
					}
					else
					{
						dataGridViewContentAlignment = DataGridViewContentAlignment.BottomRight;
					}
				}
				else
				{
					dataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft;
				}
			}
			if (dataGridViewContentAlignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (dataGridViewContentAlignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					result.X = cellBounds.X;
					result.Y = cellBounds.Y;
					break;
				case DataGridViewContentAlignment.TopCenter:
					result.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
					result.Y = cellBounds.Y;
					break;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					result.X = cellBounds.Right - sizeText.Width;
					result.Y = cellBounds.Y;
					break;
				default:
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleLeft)
					{
						if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleCenter)
						{
							result.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
							result.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
						}
					}
					else
					{
						result.X = cellBounds.X;
						result.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
					}
					break;
				}
			}
			else if (dataGridViewContentAlignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomLeft)
					{
						result.X = cellBounds.X;
						result.Y = cellBounds.Bottom - sizeText.Height;
					}
				}
				else
				{
					result.X = cellBounds.Right - sizeText.Width;
					result.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
				}
			}
			else if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomCenter)
			{
				if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
				{
					result.X = cellBounds.Right - sizeText.Width;
					result.Y = cellBounds.Bottom - sizeText.Height;
				}
			}
			else
			{
				result.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
				result.Y = cellBounds.Bottom - sizeText.Height;
			}
			return result;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000A98A5 File Offset: 0x000A7AA5
		internal static bool ValidTextFormatFlags(TextFormatFlags flags)
		{
			return (flags & ~(TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis | TextFormatFlags.ExpandTabs | TextFormatFlags.ExternalLeading | TextFormatFlags.HidePrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.Internal | TextFormatFlags.ModifyString | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix | TextFormatFlags.NoFullWidthCharacterBreak | TextFormatFlags.PathEllipsis | TextFormatFlags.PrefixOnly | TextFormatFlags.Right | TextFormatFlags.RightToLeft | TextFormatFlags.SingleLine | TextFormatFlags.TextBoxControl | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.WordEllipsis | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform)) == TextFormatFlags.Default;
		}

		// Token: 0x04000E9D RID: 3741
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginWidth = 3;

		// Token: 0x04000E9E RID: 3742
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginHeight = 2;

		// Token: 0x04000E9F RID: 3743
		private const byte DATAGRIDVIEWROWHEADERCELL_contentMarginWidth = 3;

		// Token: 0x04000EA0 RID: 3744
		private const byte DATAGRIDVIEWROWHEADERCELL_contentMarginHeight = 3;

		// Token: 0x04000EA1 RID: 3745
		private const byte DATAGRIDVIEWROWHEADERCELL_iconsWidth = 12;

		// Token: 0x04000EA2 RID: 3746
		private const byte DATAGRIDVIEWROWHEADERCELL_iconsHeight = 11;

		// Token: 0x04000EA3 RID: 3747
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000EA4 RID: 3748
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000EA5 RID: 3749
		private const byte DATAGRIDVIEWROWHEADERCELL_verticalTextMargin = 1;
	}
}
