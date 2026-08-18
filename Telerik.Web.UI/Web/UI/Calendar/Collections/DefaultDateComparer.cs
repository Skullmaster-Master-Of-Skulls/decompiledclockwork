using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.Calendar.Collections
{
	// Token: 0x02000FE4 RID: 4068
	internal class DefaultDateComparer : IComparer, IComparer<RadCalendarDay>
	{
		// Token: 0x06009E53 RID: 40531 RVA: 0x002349D4 File Offset: 0x00232BD4
		public int Compare(RadCalendarDay x, RadCalendarDay y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return x.Date.CompareTo(y.Date);
		}

		// Token: 0x06009E54 RID: 40532 RVA: 0x00234A11 File Offset: 0x00232C11
		int IComparer.Compare(object x, object y)
		{
			return this.Compare((RadCalendarDay)x, (RadCalendarDay)y);
		}
	}
}
