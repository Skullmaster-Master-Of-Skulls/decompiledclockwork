using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE7 RID: 2791
	internal class StyleHandler
	{
		// Token: 0x060068E2 RID: 26850 RVA: 0x001895C4 File Offset: 0x001877C4
		internal static int GetXFIndex(Cell cell)
		{
			int fontIndex = StyleHandler.GetFontIndex(cell);
			int formatIndex = StyleHandler.GetFormatIndex(cell);
			XF xf = StyleHandler.GetXF(cell, (ushort)fontIndex, (ushort)formatIndex);
			return cell.Worksheet.Workbook.AddXFRecordToList(xf);
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x00189600 File Offset: 0x00187800
		private static XF GetXF(Cell cell, ushort fontIndex, ushort formatIndex)
		{
			XF xf = new XF(fontIndex, formatIndex, true, 0);
			if (cell.BackgroundColor == Color.Transparent)
			{
				xf.SetTransparent();
			}
			else
			{
				ushort biffColor = StyleHandler.GetBiffColor(cell.BackgroundColor, false);
				if (biffColor != 64)
				{
					xf.CellColor = biffColor;
				}
			}
			xf.HorizontalAlignment = StyleHandler.GetBiffHorizontalAlignment(cell.HorizontalAlignment);
			xf.VerticalAlignment = StyleHandler.GetBiffVerticalAlignment(cell.VerticalAlignment);
			xf.WrapText = cell.TextWrap;
			xf.RTL = cell.RTL;
			xf.RotationAngle = cell.RotationAngle;
			xf.TopBorderColor = StyleHandler.GetBiffColor(cell.TopBorderColor, false);
			xf.BottomBorderColor = StyleHandler.GetBiffColor(cell.BottomBorderColor, false);
			xf.LeftBorderColor = StyleHandler.GetBiffColor(cell.LeftBorderColor, false);
			xf.RightBorderColor = StyleHandler.GetBiffColor(cell.RightBorderColor, false);
			xf.TopBorder = cell.TopBorderStyle;
			xf.BottomBorder = cell.BottomBorderStyle;
			xf.LeftBorder = cell.LeftBorderStyle;
			xf.RightBorder = cell.RightBorderStyle;
			return xf;
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x0018970C File Offset: 0x0018790C
		private static int GetFontIndex(Cell cell)
		{
			Font font = new Font();
			if (!cell.Color.IsEmpty)
			{
				font.FontColor = StyleHandler.GetBiffColor(cell.Color, true);
			}
			if (!string.IsNullOrEmpty(cell.FontName))
			{
				font.FontName = cell.FontName;
				font.FontSize = (ushort)(cell.FontSizeInPoints * 20f);
				if (cell.FontBold)
				{
					font.FontBold = BiffCell.FontBoldness.Bold;
				}
				if (cell.FontItalic)
				{
					font.FontAttributes = BiffCell.FontAttributes.Italic;
				}
				if (cell.FontUnderline)
				{
					font.FontUnderline = BiffCell.FontUnderlines.Single;
				}
				if (cell.FontStrikeout)
				{
					font.FontAttributes = BiffCell.FontAttributes.Strikeout;
				}
			}
			return cell.Worksheet.Workbook.AddFontRecordToList(font);
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x001897C0 File Offset: 0x001879C0
		private static int GetFormatIndex(Cell cell)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(cell.Format))
			{
				Format format = new Format(cell.Format);
				result = cell.Worksheet.Workbook.AddFormatRecordToList(format);
			}
			return result;
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x001897FC File Offset: 0x001879FC
		private static BiffCell.HorizontalAlignments GetBiffHorizontalAlignment(HorizontalAlignment horizontalAlignment)
		{
			BiffCell.HorizontalAlignments result;
			switch (horizontalAlignment)
			{
			case HorizontalAlignment.Center:
				result = BiffCell.HorizontalAlignments.Middle;
				break;
			case HorizontalAlignment.CenterAcrossSel:
				result = BiffCell.HorizontalAlignments.CenterAcrossSel;
				break;
			case HorizontalAlignment.Distributed:
				result = BiffCell.HorizontalAlignments.General;
				break;
			case HorizontalAlignment.Fill:
				result = BiffCell.HorizontalAlignments.Fill;
				break;
			case HorizontalAlignment.General:
				result = BiffCell.HorizontalAlignments.General;
				break;
			case HorizontalAlignment.Justify:
				result = BiffCell.HorizontalAlignments.Justify;
				break;
			case HorizontalAlignment.Left:
				result = BiffCell.HorizontalAlignments.Left;
				break;
			case HorizontalAlignment.Right:
				result = BiffCell.HorizontalAlignments.Right;
				break;
			default:
				result = BiffCell.HorizontalAlignments.General;
				break;
			}
			return result;
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x00189858 File Offset: 0x00187A58
		private static BiffCell.VerticalAlignments GetBiffVerticalAlignment(VerticalAlignment verticalAlignment)
		{
			BiffCell.VerticalAlignments result;
			switch (verticalAlignment)
			{
			case VerticalAlignment.Top:
				result = BiffCell.VerticalAlignments.Top;
				break;
			case VerticalAlignment.Center:
				result = BiffCell.VerticalAlignments.Center;
				break;
			case VerticalAlignment.Bottom:
				result = BiffCell.VerticalAlignments.Bottom;
				break;
			case VerticalAlignment.Justify:
				result = BiffCell.VerticalAlignments.Justify;
				break;
			case VerticalAlignment.Distributed:
				result = BiffCell.VerticalAlignments.Bottom;
				break;
			default:
				result = BiffCell.VerticalAlignments.Bottom;
				break;
			}
			return result;
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x001898A0 File Offset: 0x00187AA0
		private static ushort GetBiffColor(Color color, bool isFont)
		{
			ushort result;
			if (isFont)
			{
				result = 8;
			}
			else
			{
				result = 64;
			}
			if (!color.IsEmpty)
			{
				bool flag = color.R == color.G && color.G == color.B;
				int num = int.MaxValue;
				for (int i = 0; i < BiffCell.PaletteColorRGB.Count; i++)
				{
					RGB rgb = (RGB)BiffCell.PaletteColorRGB[i];
					if (!flag || rgb.IsChromatic)
					{
						int num2 = rgb.Distance(color);
						if (num2 == 0)
						{
							result = (ushort)(i + 8);
							break;
						}
						if (num2 < num)
						{
							num = num2;
							result = (ushort)(i + 8);
						}
					}
				}
			}
			return result;
		}
	}
}
