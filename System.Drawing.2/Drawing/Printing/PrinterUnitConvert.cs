using System;

namespace System.Drawing.Printing
{
	// Token: 0x02000066 RID: 102
	public sealed class PrinterUnitConvert
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00003800 File Offset: 0x00001A00
		private PrinterUnitConvert()
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00020668 File Offset: 0x0001E868
		public static double Convert(double value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			double num = PrinterUnitConvert.UnitsPerDisplay(fromUnit);
			double num2 = PrinterUnitConvert.UnitsPerDisplay(toUnit);
			return value * num2 / num;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00020688 File Offset: 0x0001E888
		public static int Convert(int value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return (int)Math.Round(PrinterUnitConvert.Convert((double)value, fromUnit, toUnit));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00020699 File Offset: 0x0001E899
		public static Point Convert(Point value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Point(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit));
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000206BC File Offset: 0x0001E8BC
		public static Size Convert(Size value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Size(PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000206DF File Offset: 0x0001E8DF
		public static Rectangle Convert(Rectangle value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Rectangle(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00020720 File Offset: 0x0001E920
		public static Margins Convert(Margins value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Margins
			{
				DoubleLeft = PrinterUnitConvert.Convert(value.DoubleLeft, fromUnit, toUnit),
				DoubleRight = PrinterUnitConvert.Convert(value.DoubleRight, fromUnit, toUnit),
				DoubleTop = PrinterUnitConvert.Convert(value.DoubleTop, fromUnit, toUnit),
				DoubleBottom = PrinterUnitConvert.Convert(value.DoubleBottom, fromUnit, toUnit)
			};
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020780 File Offset: 0x0001E980
		private static double UnitsPerDisplay(PrinterUnit unit)
		{
			double result;
			switch (unit)
			{
			case PrinterUnit.Display:
				result = 1.0;
				break;
			case PrinterUnit.ThousandthsOfAnInch:
				result = 10.0;
				break;
			case PrinterUnit.HundredthsOfAMillimeter:
				result = 25.4;
				break;
			case PrinterUnit.TenthsOfAMillimeter:
				result = 2.54;
				break;
			default:
				result = 1.0;
				break;
			}
			return result;
		}
	}
}
