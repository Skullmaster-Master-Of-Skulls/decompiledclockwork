using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200009F RID: 159
	[ServiceContract(Name = "MailMergingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMailMerging : IService
	{
		// Token: 0x06000477 RID: 1143
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LookupCodeValuesResp LookupCodeValues(LookupCodeValuesReq Request);

		// Token: 0x06000478 RID: 1144
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		OutputTextResp OutputText(OutputTextReq Request);

		// Token: 0x06000479 RID: 1145
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExtractCodesResp ExtractCodes(ExtractCodesReq Request);

		// Token: 0x0600047A RID: 1146
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeTextResp MailMergeText(MailMergeTextReq Request);

		// Token: 0x0600047B RID: 1147
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMailMergeCodeDefinitionsForDisplayResp GetMailMergeCodeDefinitionsForDisplay(GetMailMergeCodeDefinitionsForDisplayReq Request);

		// Token: 0x0600047C RID: 1148
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TestAllMailMergeCodesResp TestAllMailMergeCodes(TestAllMailMergeCodesReq Request);
	}
}
