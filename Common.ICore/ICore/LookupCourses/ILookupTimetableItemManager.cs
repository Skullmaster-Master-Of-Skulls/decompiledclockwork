using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x0200006F RID: 111
	public interface ILookupTimetableItemManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000329 RID: 809
		LookupTimetableItem LoadLookupTimetableItem(int TimetableId);

		// Token: 0x0600032A RID: 810
		void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItem> items);

		// Token: 0x0600032B RID: 811
		IList<LookupCourse> LoadLookupTimetableItemsByStudent(int StudentPid, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600032C RID: 812
		Task<IList<LookupCourse>> LoadLookupTimetableItemsByStudentAsync(int StudentPid, DateTime StartDateTime, DateTime EndDateTime);
	}
}
