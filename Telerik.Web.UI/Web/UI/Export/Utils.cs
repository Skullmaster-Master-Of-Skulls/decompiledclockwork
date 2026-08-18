using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI.ExcelBiff;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace Telerik.Web.UI.Export
{
	// Token: 0x02000A55 RID: 2645
	public static class Utils
	{
		// Token: 0x0600667D RID: 26237 RVA: 0x0017FE11 File Offset: 0x0017E011
		internal static int GetColFromHashCode(long hash)
		{
			return (int)(hash >> 32);
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0017FE18 File Offset: 0x0017E018
		internal static int GetRowFromHashCode(long hash)
		{
			return (int)hash;
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0017FE1C File Offset: 0x0017E01C
		internal static long GetHashCode(int col, int row)
		{
			return ((long)col << 32) + (long)row;
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x0017FE28 File Offset: 0x0017E028
		public static bool IsValidExcelCellIndex(string index)
		{
			if (string.IsNullOrEmpty(index))
			{
				return false;
			}
			Regex regex = new Regex("^([^\\d\\s]{1,3})(\\d{1,5})$");
			return regex.IsMatch(index);
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x0017FE54 File Offset: 0x0017E054
		public static Point ConvertExcelCellIndexToPoint(string index)
		{
			Regex regex = new Regex("^([^\\d\\s]{1,3})(\\d{1,5})$");
			GroupCollection groups = regex.Match(index).Groups;
			if (groups.Count != 3)
			{
				throw new ParseException("Invalid Excel cell index!", 0);
			}
			int x = Utils.ConvertExcelColumnIndexToInt(groups[1].Value);
			int y = int.Parse(groups[2].Value);
			return new Point(x, y);
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x0017FEB9 File Offset: 0x0017E0B9
		internal static string AddSlashes(string source)
		{
			return source.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x0017FEDC File Offset: 0x0017E0DC
		public static int ConvertExcelColumnIndexToInt(string col)
		{
			int num = 0;
			col = col.ToUpperInvariant();
			Regex regex = new Regex("^([^\\d\\s]{1,3})$");
			if (!regex.IsMatch(col))
			{
				throw new ArgumentException("Invalid column index. Argument should be valid Excel-style column index!");
			}
			for (int i = 0; i < col.Length; i++)
			{
				char c = col[col.Length - i - 1];
				int num2 = (int)(c - 'A' + '\u0001');
				switch (i)
				{
				case 0:
					num += num2;
					break;
				case 1:
					num += num2 * 26;
					break;
				case 2:
					num += num2 * 26 * 26;
					break;
				}
			}
			if (num > 16384)
			{
				throw new IndexOutOfRangeException("Column number exceeds the allowed range! Value should be between A (1) and XFD (16384)");
			}
			return num;
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x0017FF84 File Offset: 0x0017E184
		public static string ConvertPointToExcelCellIndex(Point point)
		{
			string text = "";
			int num = point.X;
			int num2 = point.X;
			if (num2 > 16384)
			{
				throw new IndexOutOfRangeException("Column number exceeds the allowed range! Value should be between A (1) and XFD (16384)");
			}
			do
			{
				num = (num2 - 1) % 26;
				num2 = (num2 - 1) / 26;
				text += (char)(65 + num);
			}
			while (num2 > 0);
			return string.Format("{0}{1}", Utils.ReverseString(text), point.Y);
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x0017FFFC File Offset: 0x0017E1FC
		public static string ReverseString(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = str.Length - 1; i >= 0; i--)
			{
				stringBuilder.Append(str[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x00180038 File Offset: 0x0017E238
		internal static VerticalAlignment ConvertVerticalAlign(VerticalAlign verticalAlign)
		{
			switch (verticalAlign)
			{
			case VerticalAlign.Top:
				return VerticalAlignment.Top;
			case VerticalAlign.Middle:
				return VerticalAlignment.Center;
			case VerticalAlign.Bottom:
				return VerticalAlignment.Bottom;
			default:
				return VerticalAlignment.Center;
			}
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x00180064 File Offset: 0x0017E264
		internal static RadVerticalAlignment ConvertXlsxVerticalAlign(VerticalAlign verticalAlign)
		{
			switch (verticalAlign)
			{
			case VerticalAlign.Top:
				return 2;
			case VerticalAlign.Middle:
				return 1;
			case VerticalAlign.Bottom:
				return 0;
			default:
				return 1;
			}
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x00180090 File Offset: 0x0017E290
		internal static HorizontalAlignment ConvertHorizontalAlign(HorizontalAlign horizontalAlign)
		{
			switch (horizontalAlign)
			{
			case HorizontalAlign.Left:
				return HorizontalAlignment.Left;
			case HorizontalAlign.Center:
				return HorizontalAlignment.Center;
			case HorizontalAlign.Right:
				return HorizontalAlignment.Right;
			case HorizontalAlign.Justify:
				return HorizontalAlignment.Justify;
			default:
				return HorizontalAlignment.Left;
			}
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x001800C4 File Offset: 0x0017E2C4
		internal static RadHorizontalAlignment ConvertXlsxHorizontalAlign(HorizontalAlign horizontalAlign)
		{
			switch (horizontalAlign)
			{
			case HorizontalAlign.Left:
				return 1;
			case HorizontalAlign.Center:
				return 2;
			case HorizontalAlign.Right:
				return 3;
			case HorizontalAlign.Justify:
				return 4;
			default:
				return 1;
			}
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x001800F8 File Offset: 0x0017E2F8
		internal static CellBorderStyle ConvertXlsxBorderStyle(System.Web.UI.WebControls.BorderStyle borderStyle)
		{
			switch (borderStyle)
			{
			case System.Web.UI.WebControls.BorderStyle.None:
				return 0;
			case System.Web.UI.WebControls.BorderStyle.Dotted:
				return 2;
			case System.Web.UI.WebControls.BorderStyle.Dashed:
				return 5;
			case System.Web.UI.WebControls.BorderStyle.Solid:
				return 6;
			case System.Web.UI.WebControls.BorderStyle.Double:
				return 10;
			default:
				return 6;
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x00180134 File Offset: 0x0017E334
		internal static Telerik.Web.UI.ExcelBiff.BorderStyle ConvertBorderStyle(System.Web.UI.WebControls.BorderStyle borderStyle)
		{
			switch (borderStyle)
			{
			case System.Web.UI.WebControls.BorderStyle.None:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.None;
			case System.Web.UI.WebControls.BorderStyle.Dotted:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.Dotted;
			case System.Web.UI.WebControls.BorderStyle.Dashed:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.Dashed;
			case System.Web.UI.WebControls.BorderStyle.Solid:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.Thin;
			case System.Web.UI.WebControls.BorderStyle.Double:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.Double;
			default:
				return Telerik.Web.UI.ExcelBiff.BorderStyle.Thin;
			}
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x0018016C File Offset: 0x0017E36C
		internal static List<Point> GetCellRange(Point startPoint, Point endPoint)
		{
			List<Point> list = new List<Point>();
			if (startPoint == endPoint)
			{
				return list;
			}
			for (int i = startPoint.Y; i <= endPoint.Y; i++)
			{
				for (int j = startPoint.X; j <= endPoint.X; j++)
				{
					list.Add(new Point(j, i));
				}
			}
			list.Remove(new Point(startPoint.X, startPoint.Y));
			return list;
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x001801E2 File Offset: 0x0017E3E2
		internal static bool IsEmptyFontStyle(FontInfo fontInfo)
		{
			return Utils.AreFontStylesEqual(fontInfo, new Style().Font);
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001801F4 File Offset: 0x0017E3F4
		internal static bool AreFontStylesEqual(FontInfo first, FontInfo second)
		{
			return first.Bold == second.Bold && first.Italic == second.Italic && first.Name == second.Name && first.Names.SequenceEqual(second.Names) && first.Size == second.Size && first.Strikeout == second.Strikeout && first.Underline == second.Underline;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x00180274 File Offset: 0x0017E474
		internal static double GetPointsPerUnit(Unit unit)
		{
			switch (unit.Type)
			{
			case UnitType.Pixel:
				return unit.Value * 0.7200000286102295;
			case UnitType.Point:
				return unit.Value;
			case UnitType.Pica:
				return unit.Value * 12.0;
			case UnitType.Inch:
				return unit.Value * 72.0;
			case UnitType.Mm:
				return unit.Value * 28.346456693 / 10.0;
			case UnitType.Cm:
				return unit.Value * 28.346456693;
			default:
				return -1.0;
			}
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x00180320 File Offset: 0x0017E520
		internal static double GetInchesPerUnit(Unit unit)
		{
			switch (unit.Type)
			{
			case UnitType.Pixel:
				return unit.Value * 0.010416667;
			case UnitType.Point:
				return unit.Value * 0.0138888889;
			case UnitType.Pica:
				return unit.Value * 0.166666667;
			case UnitType.Inch:
				return unit.Value;
			case UnitType.Mm:
				return unit.Value * 0.0393700787;
			case UnitType.Cm:
				return unit.Value * 0.393700787;
			default:
				return -1.0;
			}
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x001803C4 File Offset: 0x0017E5C4
		internal static double GetPixelsPerUnit(Unit unit)
		{
			switch (unit.Type)
			{
			case UnitType.Pixel:
				return unit.Value;
			case UnitType.Point:
				return unit.Value * 1.3333300352096558;
			case UnitType.Pica:
				return unit.Value * 16.0;
			case UnitType.Inch:
				return unit.Value * 96.0;
			case UnitType.Mm:
				return unit.Value * 3.779527559;
			case UnitType.Cm:
				return unit.Value * 37.795275591;
			default:
				return -1.0;
			}
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x00180468 File Offset: 0x0017E668
		internal static double GetExcelCharactersPerUnit(Unit unit, System.Drawing.Font defaultFont)
		{
			Utils.converter = new ExcelConverter(defaultFont);
			switch (unit.Type)
			{
			case UnitType.Pixel:
				return Utils.converter.PixelsToCharacters(unit.Value);
			case UnitType.Point:
			case UnitType.Pica:
			case UnitType.Inch:
			case UnitType.Mm:
			case UnitType.Cm:
				return Utils.converter.PointsToCharacters(Utils.GetPointsPerUnit(unit));
			default:
				return 8.43;
			}
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x001804D8 File Offset: 0x0017E6D8
		internal static string SanitizeCellText(string cellText)
		{
			cellText = cellText.Trim().Replace("&nbsp;", " ").Replace("\r\n", "").Replace("<br/>", "\n");
			return cellText.TrimEnd(new char[0]);
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x00180528 File Offset: 0x0017E728
		internal static Guid ConvertToGuid(string pValue)
		{
			Guid result;
			try
			{
				result = new Guid(pValue.ToString());
			}
			catch (Exception ex)
			{
				if (!(ex is ArgumentNullException) && !(ex is FormatException) && !(ex is OverflowException))
				{
					throw;
				}
				result = Guid.Empty;
			}
			return result;
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x0018057C File Offset: 0x0017E77C
		internal static TimeSpan ConvertToTimeSpan(object tsValue)
		{
			TimeSpan zero = TimeSpan.Zero;
			TimeSpan.TryParse(tsValue.ToString(), out zero);
			return zero;
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x001805A0 File Offset: 0x0017E7A0
		internal static double FontSizeToPoints(FontSize fs)
		{
			double num = 10.0;
			switch (fs)
			{
			case FontSize.Smaller:
			case FontSize.XSmall:
				num -= 1.0;
				break;
			case FontSize.Larger:
				num += 1.0;
				break;
			case FontSize.XXSmall:
				num -= 2.0;
				break;
			case FontSize.Medium:
				num += 1.0;
				break;
			case FontSize.Large:
				num += 2.0;
				break;
			case FontSize.XLarge:
				num += 3.0;
				break;
			case FontSize.XXLarge:
				num += 4.0;
				break;
			}
			return num;
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x00180648 File Offset: 0x0017E848
		internal static string GetFileExtensionFromUrl(string imageUrl)
		{
			string result = "jpg";
			if (imageUrl.EndsWith(".jpg") || imageUrl.EndsWith(".jpeg") || imageUrl.EndsWith("jpe") || imageUrl.EndsWith(".png") || imageUrl.EndsWith(".bmp"))
			{
				int num = imageUrl.LastIndexOf('.') + 1;
				result = imageUrl.Substring(num, imageUrl.Length - num);
			}
			return result;
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x001806C0 File Offset: 0x0017E8C0
		internal static string GetFileExtensionFromByteArray(byte[] imageData)
		{
			byte[] array = new byte[8];
			string result = "jpg";
			Array.Copy(imageData, array, 8);
			if (array == new byte[]
			{
				137,
				80,
				78,
				71,
				13,
				10,
				26,
				10
			})
			{
				result = "png";
			}
			else if (array[0] == 255 && array[1] == 216 && array[2] == 255 && array[3] == 224)
			{
				result = "jpg";
			}
			else if (array[0] == 66 && array[1] == 77)
			{
				result = "bmp";
			}
			return result;
		}

		// Token: 0x040018E0 RID: 6368
		private const int NumbersCount = 26;

		// Token: 0x040018E1 RID: 6369
		private const string ExcelCellIndexRegex = "^([^\\d\\s]{1,3})(\\d{1,5})$";

		// Token: 0x040018E2 RID: 6370
		private const string ExcelColumnIndexRegex = "^([^\\d\\s]{1,3})$";

		// Token: 0x040018E3 RID: 6371
		internal const float PointToPixelConstant = 1.33333f;

		// Token: 0x040018E4 RID: 6372
		internal const float PixelToPointConstant = 0.72f;

		// Token: 0x040018E5 RID: 6373
		private const double pointsPerCm = 28.346456693;

		// Token: 0x040018E6 RID: 6374
		private static ExcelConverter converter;
	}
}
