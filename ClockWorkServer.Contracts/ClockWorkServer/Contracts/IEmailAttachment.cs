using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Email;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200004B RID: 75
	[ServiceContract(Name = "EmailAttachmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IEmailAttachment : IService
	{
		// Token: 0x06000251 RID: 593
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAttachmentResp LoadAttachment(LoadAttachmentReq Request);
	}
}
