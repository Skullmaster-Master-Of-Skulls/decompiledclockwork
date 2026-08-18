using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x0200006B RID: 107
	public interface IAlternateContactManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002E7 RID: 743
		int CreateAlternateContact(AlternateContact AltContact);

		// Token: 0x060002E8 RID: 744
		AlternateContact LoadAlternateContactById(int AlternateContactId);

		// Token: 0x060002E9 RID: 745
		AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId);

		// Token: 0x060002EA RID: 746
		IList<AlternateContact> LoadAlternateContactsByCourse(int LuCourseId);

		// Token: 0x060002EB RID: 747
		IList<AlternateContact> LoadAlternateContactsBySearchString(string SearchString);

		// Token: 0x060002EC RID: 748
		void UpdateAlternateContact(AlternateContact AltContact);

		// Token: 0x060002ED RID: 749
		void DeleteAlternateContact(int AlternateContactId);

		// Token: 0x060002EE RID: 750
		AlternateContact LoadAlternateContactByUsername(string Username);

		// Token: 0x060002EF RID: 751
		void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x060002F0 RID: 752
		void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x060002F1 RID: 753
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId);

		// Token: 0x060002F2 RID: 754
		AlternateContact LoadAlternateContactByEmail(string Email);
	}
}
