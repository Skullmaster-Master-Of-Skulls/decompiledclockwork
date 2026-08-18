using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2D RID: 6957
	public static class Utils
	{
		// Token: 0x06010D52 RID: 68946 RVA: 0x003BC0B0 File Offset: 0x003BA2B0
		public static string ConvertColor(Color color)
		{
			return string.Format("#{0}", color.ToArgb().ToString("X").Remove(0, 2));
		}

		// Token: 0x06010D53 RID: 68947 RVA: 0x003BC0E4 File Offset: 0x003BA2E4
		public static double ConvertUnitsToPoints(Unit unit)
		{
			double result = 0.0;
			if (unit.Value != 0.0)
			{
				switch (unit.Type)
				{
				case UnitType.Pixel:
					result = unit.Value * 0.75;
					break;
				case UnitType.Point:
					result = unit.Value;
					break;
				case UnitType.Pica:
					result = unit.Value * 12.0;
					break;
				case UnitType.Inch:
					result = unit.Value * 72.0;
					break;
				case UnitType.Mm:
					result = unit.Value * 2.83464566929;
					break;
				case UnitType.Cm:
					result = unit.Value * 28.3464566929;
					break;
				case UnitType.Percentage:
				case UnitType.Em:
				case UnitType.Ex:
					throw new NotSupportedException("Relative units (Ex, Em, Percentage) are not supported. Please use absolute units instead.");
				}
			}
			return result;
		}

		// Token: 0x06010D54 RID: 68948 RVA: 0x003BC1C0 File Offset: 0x003BA3C0
		public static HorizontalAlignmentType ConvertHorizontalAlign(HorizontalAlign horizontalAlign)
		{
			HorizontalAlignmentType result;
			switch (horizontalAlign)
			{
			case HorizontalAlign.Left:
				result = HorizontalAlignmentType.Left;
				break;
			case HorizontalAlign.Center:
				result = HorizontalAlignmentType.Center;
				break;
			case HorizontalAlign.Right:
				result = HorizontalAlignmentType.Right;
				break;
			case HorizontalAlign.Justify:
				result = HorizontalAlignmentType.Fill;
				break;
			default:
				result = HorizontalAlignmentType.Automatic;
				break;
			}
			return result;
		}
	}
}
