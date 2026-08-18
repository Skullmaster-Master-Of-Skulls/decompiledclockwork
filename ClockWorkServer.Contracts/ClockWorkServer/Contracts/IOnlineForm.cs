using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200006D RID: 109
	[ServiceContract(Name = "OnlineFormService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IOnlineForm : IService
	{
		// Token: 0x0600033C RID: 828
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllOnlineFormsResp GetAllOnlineForms(GetAllOnlineFormsReq request);

		// Token: 0x0600033D RID: 829
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetOnlineFormResp GetOnlineForm(GetOnlineFormReq request);

		// Token: 0x0600033E RID: 830
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateOnlineForm(UpdateOnlineFormReq request);

		// Token: 0x0600033F RID: 831
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateNewOnlineFormResp CreateNewOnlineForm(CreateNewOnlineFormReq request);

		// Token: 0x06000340 RID: 832
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteOnlineForm(DeleteOnlineFormReq Request);

		// Token: 0x06000341 RID: 833
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DisableOnlineForm(DisableOnlineFormReq Request);

		// Token: 0x06000342 RID: 834
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void EnableOnlineForm(EnableOnlineFormReq Request);

		// Token: 0x06000343 RID: 835
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveOnlineFormsResp GetActiveOnlineForms(GetActiveOnlineFormsReq request);
	}
}
