using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x0200007E RID: 126
	public interface IAccommodationBatchLetterEmailsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600031A RID: 794
		void MarkLetterSent(int PersonId, int LuCourseId, DateTime DateSent);

		// Token: 0x0600031B RID: 795
		IList<PotentialLetterToSendOut> GetPotentialLettersToSendOut(DateTime Today, int AccommodationExpiryDateCid);

		// Token: 0x0600031C RID: 796
		IDictionary<int, DateTime?> GetBatchLetterSentDates(int PersonId, IList<int> LuCourseIds);
	}
}
