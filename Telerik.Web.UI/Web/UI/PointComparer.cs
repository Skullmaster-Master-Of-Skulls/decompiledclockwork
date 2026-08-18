using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x02000B74 RID: 2932
	internal class PointComparer : IComparer<Point>
	{
		// Token: 0x06006E96 RID: 28310 RVA: 0x0019B0E9 File Offset: 0x001992E9
		public int Compare(Point first, Point second)
		{
			if (first.X == second.X)
			{
				return first.Y - second.Y;
			}
			return first.X - second.X;
		}
	}
}
