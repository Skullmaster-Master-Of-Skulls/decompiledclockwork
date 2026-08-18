using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x0200005A RID: 90
	public interface ILookupTimetableItemDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000217 RID: 535
		LookupTimetableItem LoadLookupTimetableItem(int TimetableId);

		// Token: 0x06000218 RID: 536
		void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItem> items);

		// Token: 0x06000219 RID: 537
		IList<LookupCourse> LoadLookupTimetableItemsByStudent(int StudentPid, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600021A RID: 538
		Task<IList<LookupCourse>> LoadLookupTimetableItemsByStudentAsync(int StudentPid, DateTime StartDateTime, DateTime EndDateTime);
	}
}
