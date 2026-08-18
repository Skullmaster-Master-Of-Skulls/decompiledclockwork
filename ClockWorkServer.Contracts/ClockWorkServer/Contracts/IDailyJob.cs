using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000032 RID: 50
	[ServiceContract(Name = "DailyJobService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDailyJob : IService
	{
		// Token: 0x0600019D RID: 413
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RunDailyJobResp RunDailyJob(RunDailyJobReq Request);

		// Token: 0x0600019E RID: 414
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateDailyJobTaskResp CreateDailyJobTask(CreateDailyJobTaskReq Request);

		// Token: 0x0600019F RID: 415
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateDailyJobTask(UpdateDailyJobTaskReq Request);

		// Token: 0x060001A0 RID: 416
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ChangeTaskActiveStatus(ChangeTaskActiveStatusReq Request);

		// Token: 0x060001A1 RID: 417
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDailyJobTasksByGroupResp LoadDailyJobTasksByGroup(LoadDailyJobTasksByGroupReq Request);

		// Token: 0x060001A2 RID: 418
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDailyJobTaskByIdResp LoadDailyJobTaskById(LoadDailyJobTaskByIdReq Request);

		// Token: 0x060001A3 RID: 419
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteDailyJobTask(DeleteDailyJobTaskReq Request);
	}
}
