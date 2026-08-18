using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegEmail;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.Common.ICore.StudentAccommodationRequests
{
	// Token: 0x02000030 RID: 48
	public interface ISelfRegManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600014F RID: 335
		void ProcessSelfRegRequest(int pid, eSelfRegCoursesAccommodationsStatus accChange, IList<SelfRegCourseInfo> selectedLucids, List<SelfRegCheckedAccommodation> checkedAccommodations, IList<AccommodationData> hidingAccommodations, string noteFromStudent, string baseUrl, string pidEncodedForUrl, string ipAddressForLogging);

		// Token: 0x06000150 RID: 336
		AllowedStudentCourseRegistrationsForCustomEmailLogic GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(int studentPersonId);

		// Token: 0x06000151 RID: 337
		AllowedStudentCourseRegistrationsForCustomEmailLogic GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(string studentPersonIdHash, string plainTextStudentPersonIdHash);

		// Token: 0x06000152 RID: 338
		SelfRegEmailLogicRule FindLogicRuleThatApplies(int studentPersonId, int luCourseId);
	}
}
