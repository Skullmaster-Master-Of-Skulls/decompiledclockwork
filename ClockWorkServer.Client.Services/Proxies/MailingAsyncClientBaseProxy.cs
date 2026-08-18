using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E3 RID: 227
	public class MailingAsyncClientBaseProxy : ClientBase<IMailingAsync>, IMailingAsync, IMailing, IService
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x00016CF6 File Offset: 0x00014EF6
		public MailingAsyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00016D01 File Offset: 0x00014F01
		public MailingAsyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00016D10 File Offset: 0x00014F10
		public IAsyncResult BeginSendEmails(SendEmailsReq req, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginSendEmails(req, callback, asyncState);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00016D30 File Offset: 0x00014F30
		public SendEmailsResp EndSendEmails(IAsyncResult result)
		{
			return base.Channel.EndSendEmails(result);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00016D50 File Offset: 0x00014F50
		public SendEmailsResp SendEmails(SendEmailsReq request)
		{
			return base.Channel.SendEmails(request);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00016D70 File Offset: 0x00014F70
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			return base.Channel.GetDefaultFromAddress(Request);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00016D90 File Offset: 0x00014F90
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			return base.Channel.SendEmailWithOverrideSettings(Request);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00016DB0 File Offset: 0x00014FB0
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			return base.Channel.SendEmailsReturnResult(Request);
		}
	}
}
