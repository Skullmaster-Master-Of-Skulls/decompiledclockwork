using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.ICore.Tutoring
{
	// Token: 0x0200001F RID: 31
	public interface IStudentTuteeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000C4 RID: 196
		IList<MyTutor> GetStudentMyTutors(int StudentPersonId, DateTime? StartDate, DateTime? EndDate);

		// Token: 0x060000C5 RID: 197
		void MarkStudentCantFindTutor(int PersonId, int searchLucid, string searchLuc, string searchString);

		// Token: 0x060000C6 RID: 198
		void MarkStudentCantFindAvailability(int PersonId, params int[] TutorPids);

		// Token: 0x060000C7 RID: 199
		eTuteeStatus GetTuteeStatus(int StudentPersonId);

		// Token: 0x060000C8 RID: 200
		bool IsConfidentialityAgreementSigningRequiredForStudent(int StudentPersonId);

		// Token: 0x060000C9 RID: 201
		void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId);
	}
}
