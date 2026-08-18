using System;

namespace Telerik.Web.UI.PivotGrid.Core.Internal
{
	// Token: 0x020006E7 RID: 1767
	internal static class DoubleUtil
	{
		// Token: 0x06003EFB RID: 16123 RVA: 0x000C87F4 File Offset: 0x000C69F4
		public static bool AreClose(double value1, double value2)
		{
			if (value1 == value2)
			{
				return true;
			}
			double num = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.220446049250313E-16;
			double num2 = value1 - value2;
			return -num < num2 && num > num2;
		}

		// Token: 0x040010B6 RID: 4278
		internal const double DblEpsilon = 2.220446049250313E-16;
	}
}
