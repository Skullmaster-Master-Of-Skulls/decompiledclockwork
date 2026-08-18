using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DF RID: 223
	[ServiceContract(Name = "MailingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMailingAsync : IMailing, IService
	{
		// Token: 0x060008B6 RID: 2230
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginSendEmails(SendEmailsReq req, AsyncCallback callback, object asyncState);

		// Token: 0x060008B7 RID: 2231
		SendEmailsResp EndSendEmails(IAsyncResult result);

		// Token: 0x060008B8 RID: 2232
		void Close();
	}
}
