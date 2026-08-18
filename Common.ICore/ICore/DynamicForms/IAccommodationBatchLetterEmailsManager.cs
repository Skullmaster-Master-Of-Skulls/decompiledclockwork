using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000093 RID: 147
	public interface IAccommodationBatchLetterEmailsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600041B RID: 1051
		void MarkLetterSent(int PersonId, int LuCourseId, DateTime DateSent);

		// Token: 0x0600041C RID: 1052
		IList<PotentialLetterToSendOut> GetPotentialLettersToSendOut(DateTime Today);

		// Token: 0x0600041D RID: 1053
		IList<PotentialLetterToSendOutResult> SendLetters(int TemplateId, bool TestingMode, bool ReturnAttachmentFile);

		// Token: 0x0600041E RID: 1054
		IList<PotentialLetterToSendOutResult> SendLetters(int TemplateId, DateTime Today, bool TestingMode, bool ReturnAttachmentFile);

		// Token: 0x0600041F RID: 1055
		IDictionary<int, DateTime?> GetBatchLetterSentDates(int PersonId, IList<int> LuCourseIds);
	}
}
