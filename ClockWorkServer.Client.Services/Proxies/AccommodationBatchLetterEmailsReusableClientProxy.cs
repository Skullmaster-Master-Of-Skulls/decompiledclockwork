using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000080 RID: 128
	public class AccommodationBatchLetterEmailsReusableClientProxy : WCFTokenBasedReusableClientProxy<IAccommodationBatchLetterEmails>, IAccommodationBatchLetterEmails, IService
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0000EEEE File Offset: 0x0000D0EE
		public AccommodationBatchLetterEmailsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000EEF9 File Offset: 0x0000D0F9
		public AccommodationBatchLetterEmailsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000EF08 File Offset: 0x0000D108
		public GetBatchLetterSentDatesResp GetBatchLetterSentDates(GetBatchLetterSentDatesReq Request)
		{
			return this.WrapServiceMethod<GetBatchLetterSentDatesResp>(() => this.Proxy.GetBatchLetterSentDates(Request));
		}
	}
}
