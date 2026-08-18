using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000074 RID: 116
	[ServiceContract(Name = "StaffCommonInfoService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStaffCommonInfo : IService
	{
		// Token: 0x06000366 RID: 870
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStaffStoredSignatureResp LoadStaffStoredSignature(LoadStaffStoredSignatureReq Request);

		// Token: 0x06000367 RID: 871
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveStaffStoredSignatureResp SaveStaffStoredSignature(SaveStaffStoredSignatureReq Request);

		// Token: 0x06000368 RID: 872
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStaffSignatureDataResp LoadStaffStoredSignatureData(LoadStaffSignatureDataReq Request);

		// Token: 0x06000369 RID: 873
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAssignedAdvisorSignatureDataResp LoadAssignedAdvisorSignatureData(LoadAssignedAdvisorSignatureDataReq Request);

		// Token: 0x0600036A RID: 874
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveAssignedAdvisorStoredSignatureWithImageBytesResp SaveAssignedAdvisorStoredSignatureWithImageBytes(SaveAssignedAdvisorStoredSignatureWithImageBytesReq Request);

		// Token: 0x0600036B RID: 875
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveAssignedAdvisorStoredSignatureResp SaveAssignedAdvisorStoredSignature(SaveAssignedAdvisorStoredSignatureReq Request);

		// Token: 0x0600036C RID: 876
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStaffWithCommonInfoByIdResp LoadStaffWithCommonInfoById(LoadStaffWithCommonInfoByIdReq Request);

		// Token: 0x0600036D RID: 877
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateCommonInfo(UpdateCommonInfoReq Request);
	}
}
