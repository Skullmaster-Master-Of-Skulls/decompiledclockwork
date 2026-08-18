using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.ICore.LookupCourses.Management
{
	// Token: 0x02000072 RID: 114
	public interface ILookupInstructorManagementManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600033E RID: 830
		LookInstructorForManagementList LoadLookupInstructorsForManagement(int startIndex, int count);

		// Token: 0x0600033F RID: 831
		void DeleteInstructor(int instructorId);

		// Token: 0x06000340 RID: 832
		void MergeInstructors(int instructor1Id, int instructor2Id);
	}
}
