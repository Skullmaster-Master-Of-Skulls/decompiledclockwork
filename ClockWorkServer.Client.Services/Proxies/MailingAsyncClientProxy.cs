using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E1 RID: 225
	public class MailingAsyncClientProxy : WCFTokenBasedAsyncClientProxy<IMailingAsync>, IMailingAsync, IMailing, IService
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public MailingAsyncClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00016AF3 File Offset: 0x00014CF3
		public MailingAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00016B00 File Offset: 0x00014D00
		public IAsyncResult BeginSendEmails(SendEmailsReq req, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginSendEmails(req, callback, asyncState));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00016B48 File Offset: 0x00014D48
		public SendEmailsResp EndSendEmails(IAsyncResult result)
		{
			return this.WrapServiceMethod<SendEmailsResp>(() => this.Proxy.EndSendEmails(result));
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00016B80 File Offset: 0x00014D80
		public SendEmailsResp SendEmails(SendEmailsReq request)
		{
			return this.WrapServiceMethod<SendEmailsResp>(() => this.Proxy.SendEmails(request));
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			return this.WrapServiceMethod<GetDefaultFromAddressResp>(() => this.Proxy.GetDefaultFromAddress(Request));
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			return this.WrapServiceMethod<SendEmailWithOverrideSettingsResp>(() => this.Proxy.SendEmailWithOverrideSettings(Request));
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00016C28 File Offset: 0x00014E28
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			return this.WrapServiceMethod<SendEmailsReturnResultResp>(() => this.Proxy.SendEmailsReturnResult(Request));
		}
	}
}
