using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000315 RID: 789
	public class AssignmentEventArgs : EventArgs, IAssignmentEvent
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x00056AF4 File Offset: 0x00054CF4
		public AssignmentEventArgs(IEnumerable<IAssignment> assignments)
		{
			this._assignments = assignments;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00056B03 File Offset: 0x00054D03
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x00056B0B File Offset: 0x00054D0B
		public bool Cancel { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00056B14 File Offset: 0x00054D14
		public IEnumerable<IAssignment> Assignments
		{
			get
			{
				return this._assignments;
			}
		}

		// Token: 0x040006C7 RID: 1735
		private readonly IEnumerable<IAssignment> _assignments;
	}
}
