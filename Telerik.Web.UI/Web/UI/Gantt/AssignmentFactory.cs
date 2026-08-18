using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200031E RID: 798
	public class AssignmentFactory : IAssignmentFactory
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x00056B44 File Offset: 0x00054D44
		public IAssignment CreateAssignment()
		{
			return new Assignment();
		}
	}
}
