using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.DAO.LookupCourses.Management
{
	// Token: 0x0200005D RID: 93
	public interface ILookupInstructorManagementDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600021F RID: 543
		IList<LookupInstructorForManagement> LoadAllLookupInstructorsForManagement();

		// Token: 0x06000220 RID: 544
		void DeleteInstructor(int instructorId);

		// Token: 0x06000221 RID: 545
		void SwapInstructors(int instructorSourceId, int instructorDestId);
	}
}
