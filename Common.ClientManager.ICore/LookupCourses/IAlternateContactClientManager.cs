using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x02000039 RID: 57
	public interface IAlternateContactClientManager : IWebService
	{
		// Token: 0x06000195 RID: 405
		int CreateAlternateContact(AlternateContactDTO AltContact);

		// Token: 0x06000196 RID: 406
		AlternateContactDTO LoadAlternateContactById(int AlternateContactId);

		// Token: 0x06000197 RID: 407
		AlternateContactDTO LoadAlternateContactByEmployeeId(string EmployeeId);

		// Token: 0x06000198 RID: 408
		IList<AlternateContactDTO> LoadAlternateContactsByCourse(int LuCourseId);

		// Token: 0x06000199 RID: 409
		IList<AlternateContactDTO> LoadAlternateContactsBySearchString(string SearchString);

		// Token: 0x0600019A RID: 410
		void UpdateAlternateContact(AlternateContactDTO AltContact);

		// Token: 0x0600019B RID: 411
		void DeleteAlternateContact(int AlternateContactId);

		// Token: 0x0600019C RID: 412
		AlternateContactDTO LoadAlternateContactByUsername(string Username);

		// Token: 0x0600019D RID: 413
		void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x0600019E RID: 414
		void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId);

		// Token: 0x0600019F RID: 415
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId);
	}
}
