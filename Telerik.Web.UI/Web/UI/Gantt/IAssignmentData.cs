using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002E8 RID: 744
	public interface IAssignmentData : IAssignmentBase
	{
		// Token: 0x060019C0 RID: 6592
		void CopyFrom(IAssignment srcAssignment);

		// Token: 0x060019C1 RID: 6593
		void CopyTo(IAssignment destAssignment);
	}
}
