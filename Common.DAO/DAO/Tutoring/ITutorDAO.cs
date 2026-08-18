using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.DAO.Tutoring
{
	// Token: 0x02000020 RID: 32
	public interface ITutorDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600006C RID: 108
		IList<Tutor> SearchForTutors(string courseSearchString, string SearchString, int TutorIsActiveCid);

		// Token: 0x0600006D RID: 109
		Tutor LoadTutorByPersonId(int PersonId);

		// Token: 0x0600006E RID: 110
		IList<TutorWithActiveStatus> LoadAllTutors(int ActiveCid);

		// Token: 0x0600006F RID: 111
		IList<TutorInfo> LoadTutorInfos(int[] tutorPersonIds, int tutorIsAuthorizedCid, int tutorConfidentialityAgreementSignedCid);
	}
}
