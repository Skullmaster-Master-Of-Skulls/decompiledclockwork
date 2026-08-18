using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Core.SpireDoc;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000039 RID: 57
	public class AccommodationBatchLetterEmailsServiceManager : IAccommodationBatchLetterEmails, IService
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public GetBatchLetterSentDatesResp GetBatchLetterSentDates(GetBatchLetterSentDatesReq Request)
		{
			IAccommodationBatchLetterEmailsManager accommodationBatchLetterEmailsManager = new AccommodationBatchLetterEmailsManager(Request.GetOperationContext());
			IDictionary<int, DateTime?> batchLetterSentDates = accommodationBatchLetterEmailsManager.GetBatchLetterSentDates(Request.PersonId, Request.LuCourseIds);
			return new GetBatchLetterSentDatesResp
			{
				BatchLetterSentDates = batchLetterSentDates
			};
		}
	}
}
