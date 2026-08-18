using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000081 RID: 129
	internal class AccommodationBatchLetterEmailsClientBaseProxy : ClientBase<IAccommodationBatchLetterEmails>, IAccommodationBatchLetterEmails, IService
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0000EF40 File Offset: 0x0000D140
		public AccommodationBatchLetterEmailsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000EF4B File Offset: 0x0000D14B
		public AccommodationBatchLetterEmailsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000EF58 File Offset: 0x0000D158
		public GetBatchLetterSentDatesResp GetBatchLetterSentDates(GetBatchLetterSentDatesReq Request)
		{
			return base.Channel.GetBatchLetterSentDates(Request);
		}
	}
}
