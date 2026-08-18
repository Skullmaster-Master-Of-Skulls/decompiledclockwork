using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.ClientManager.ICore.Tutoring
{
	// Token: 0x0200000A RID: 10
	public interface IStudentTuteeClientManager : IWebService
	{
		// Token: 0x0600002D RID: 45
		eTuteeStatus GetTuteeStatus(int StudentPersonId);

		// Token: 0x0600002E RID: 46
		void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId);

		// Token: 0x0600002F RID: 47
		IList<MyTutorDTO> GetStudentMyTutors(int StudentPersonId, DateTime? StartDateTime, DateTime? EndDate);

		// Token: 0x06000030 RID: 48
		void MarkStudentCantFindTutor(int PersonId, int searchLucid, string searchLuc, string searchString);

		// Token: 0x06000031 RID: 49
		void MarkStudentCantFindAvailability(int PersonId, params int[] TutorPids);
	}
}
