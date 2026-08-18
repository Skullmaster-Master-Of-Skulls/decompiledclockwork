using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E2 RID: 226
	public class MailingClientBaseProxy : ClientBase<IMailing>, IMailing, IService
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00016C60 File Offset: 0x00014E60
		public MailingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00016C6B File Offset: 0x00014E6B
		public MailingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00016C78 File Offset: 0x00014E78
		public SendEmailsResp SendEmails(SendEmailsReq request)
		{
			return base.Channel.SendEmails(request);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00016C98 File Offset: 0x00014E98
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			return base.Channel.GetDefaultFromAddress(Request);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00016CB8 File Offset: 0x00014EB8
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			return base.Channel.SendEmailWithOverrideSettings(Request);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00016CD8 File Offset: 0x00014ED8
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			return base.Channel.SendEmailsReturnResult(Request);
		}
	}
}
