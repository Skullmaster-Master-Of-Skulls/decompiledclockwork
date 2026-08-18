using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.Authentication
{
	// Token: 0x020000DB RID: 219
	public interface IClockWorkAuthenticationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006D5 RID: 1749
		PersonBase FindStudentByUserName(int Cid, string UserName);

		// Token: 0x060006D6 RID: 1750
		PersonBase LoadStudentByStudentNumber(string StudentNumber);

		// Token: 0x060006D7 RID: 1751
		LookupInstructor LoadInstructorByUsername(string username);

		// Token: 0x060006D8 RID: 1752
		LookupInstructor LoadInstructorByEmail(string email);

		// Token: 0x060006D9 RID: 1753
		LookupInstructor LoadInstructorByEmployeeId(string employeeId);

		// Token: 0x060006DA RID: 1754
		AlternateContact LoadAlternateContactById(int AlternateContactId);

		// Token: 0x060006DB RID: 1755
		AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId);

		// Token: 0x060006DC RID: 1756
		AlternateContact LoadAlternateContactByUsername(string Username);

		// Token: 0x060006DD RID: 1757
		ClockWorkUser LookupAuthenticatedUserInClockWork(AuthorizationContext Context, ExternalUserInfo externalUserInfo, bool verboseLogging);

		// Token: 0x060006DE RID: 1758
		AuthenticationAndAuthorizationResult AuthenticateAndAuthorizeUser(AuthenticationContext AuthenticationContext, AuthorizationContext AuthorizationContext, string UserName, string Password, AuthenticationArgs AuthenticationArgs, string BinPath, bool VerboseLogging = false);
	}
}
