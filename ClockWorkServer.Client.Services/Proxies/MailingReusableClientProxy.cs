using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E0 RID: 224
	public class MailingReusableClientProxy : WCFTokenBasedReusableClientProxy<IMailing>, IMailing, IService
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x000169F1 File Offset: 0x00014BF1
		public MailingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000169FC File Offset: 0x00014BFC
		public MailingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00016A08 File Offset: 0x00014C08
		public SendEmailsResp SendEmails(SendEmailsReq request)
		{
			return this.WrapServiceMethod<SendEmailsResp>(() => this.Proxy.SendEmails(request));
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00016A40 File Offset: 0x00014C40
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			return this.WrapServiceMethod<GetDefaultFromAddressResp>(() => this.Proxy.GetDefaultFromAddress(Request));
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00016A78 File Offset: 0x00014C78
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			return this.WrapServiceMethod<SendEmailWithOverrideSettingsResp>(() => this.Proxy.SendEmailWithOverrideSettings(Request));
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			return this.WrapServiceMethod<SendEmailsReturnResultResp>(() => this.Proxy.SendEmailsReturnResult(Request));
		}
	}
}
