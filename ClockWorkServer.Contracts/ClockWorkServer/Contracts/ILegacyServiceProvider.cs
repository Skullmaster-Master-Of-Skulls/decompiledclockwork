using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000061 RID: 97
	[ServiceContract(Name = "LegacyServiceProviderService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyServiceProvider : IService
	{
		// Token: 0x060002D7 RID: 727
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestDetailNotesAndSpecialInstructionsResp LoadRequestDetailNotesAndSpecialInstructions(LoadRequestDetailNotesAndSpecialInstructionsReq Request);

		// Token: 0x060002D8 RID: 728
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRequestDetailNotesAndSpecialInstructionsResp UpdateRequestDetailNotesAndSpecialInstructions(UpdateRequestDetailNotesAndSpecialInstructionsReq Request);

		// Token: 0x060002D9 RID: 729
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateServiceProviderRequestResp UpdateServiceProviderRequest(UpdateServiceProviderRequestReq Request);

		// Token: 0x060002DA RID: 730
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateServiceProviderRequestNotesResp UpdateServiceProviderRequestNotes(UpdateServiceProviderRequestNotesReq Request);

		// Token: 0x060002DB RID: 731
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateServiceProviderResp UpdateServiceProvider(UpdateServiceProviderReq Request);

		// Token: 0x060002DC RID: 732
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateServiceProviderResp CreateServiceProvider(CreateServiceProviderReq Request);

		// Token: 0x060002DD RID: 733
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderResp LoadProvider(LoadProviderReq Request);

		// Token: 0x060002DE RID: 734
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderIdByStudentNumberResp LoadProviderIdByStudentNumber(LoadProviderIdByStudentNumberReq Request);
	}
}
