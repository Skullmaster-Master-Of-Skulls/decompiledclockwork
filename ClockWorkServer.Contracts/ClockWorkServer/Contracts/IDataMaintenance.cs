using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200003F RID: 63
	[ServiceContract(Name = "DataMaintenanceService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDataMaintenance : IService
	{
		// Token: 0x060001F4 RID: 500
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAssignmentsForStaffDropListResp LoadAssignmentsForStaffDropList(LoadAssignmentsForStaffDropListReq Request);

		// Token: 0x060001F5 RID: 501
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ReassignStaffDropListResp ReassignStaffDropList(ReassignStaffDropListReq Request);
	}
}
