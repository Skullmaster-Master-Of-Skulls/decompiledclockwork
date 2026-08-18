using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003C RID: 60
	public interface ILookupInstructorManagementClientManager : IWebService
	{
		// Token: 0x060001C1 RID: 449
		LookInstructorForManagementListDTO LoadLookupInstructorsForManagement(int startIndex, int count);

		// Token: 0x060001C2 RID: 450
		void DeleteInstructor(int instructorId);

		// Token: 0x060001C3 RID: 451
		void MergeInstructors(int instructor1Id, int instructor2Id);
	}
}
