using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.ClockWorkServer.Contracts.Faults.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000092 RID: 146
	[ServiceContract(Name = "TaskGroupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITaskGroup : IService
	{
		// Token: 0x060003F8 RID: 1016
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateNewTaskGroupResp CreateNewTaskGroup(CreateNewTaskGroupReq Request);

		// Token: 0x060003F9 RID: 1017
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteTaskGroup(DeleteTaskGroupReq Request);

		// Token: 0x060003FA RID: 1018
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateTaskGroup(UpdateTaskGroupReq Request);

		// Token: 0x060003FB RID: 1019
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupsResp LoadGroups(LoadGroupsReq Request);
	}
}
