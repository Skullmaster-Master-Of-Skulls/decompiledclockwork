using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020012CB RID: 4811
	internal class AppointmentComparer : IComparer<Appointment>
	{
		// Token: 0x0600CA41 RID: 51777 RVA: 0x002D223C File Offset: 0x002D043C
		public int Compare(Appointment first, Appointment second)
		{
			if (first == null || second == null)
			{
				throw new InvalidOperationException("Can't compare null object(s).");
			}
			if (first.Start < second.Start)
			{
				return -1;
			}
			if (first.Start > second.Start)
			{
				return 1;
			}
			if (first.End > second.End)
			{
				return -1;
			}
			if (first.End < second.End)
			{
				return 1;
			}
			return 0;
		}
	}
}
