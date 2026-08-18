using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CC RID: 204
	[ServiceContract(Name = "MailingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraTimeService]
	public interface IMailing : IService
	{
		// Token: 0x06000599 RID: 1433
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SendEmailsResp SendEmails(SendEmailsReq request);

		// Token: 0x0600059A RID: 1434
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request);

		// Token: 0x0600059B RID: 1435
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request);

		// Token: 0x0600059C RID: 1436
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request);
	}
}
