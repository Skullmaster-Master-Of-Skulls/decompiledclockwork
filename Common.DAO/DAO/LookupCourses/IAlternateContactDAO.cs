using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x02000056 RID: 86
	public interface IAlternateContactDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001D6 RID: 470
		int CreateAlternateContact(AlternateContact AltContact);

		// Token: 0x060001D7 RID: 471
		AlternateContact LoadAlternateContactById(int AlternateContactId);

		// Token: 0x060001D8 RID: 472
		IList<AlternateContact> LoadAlternateContactsByCourse(int LuCourseId);

		// Token: 0x060001D9 RID: 473
		IList<AlternateContact> LoadAlternateContactsBySearchString(string SearchString);

		// Token: 0x060001DA RID: 474
		void UpdateAlternateContact(AlternateContact AltContact);

		// Token: 0x060001DB RID: 475
		void DeleteAlternateContact(int AlternateContactId);

		// Token: 0x060001DC RID: 476
		AlternateContact LoadAlternateContactByUsername(string Username);

		// Token: 0x060001DD RID: 477
		void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x060001DE RID: 478
		void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x060001DF RID: 479
		AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId);

		// Token: 0x060001E0 RID: 480
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId);

		// Token: 0x060001E1 RID: 481
		AlternateContact LoadAlternateContactByEmail(string Email);
	}
}
