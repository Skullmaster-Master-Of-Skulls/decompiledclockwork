using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000035 RID: 53
	[ServiceContract(Name = "ClockWorkServerJobService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClockWorkServerJob : IService
	{
		// Token: 0x060001A8 RID: 424
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkServerJobsResp GetClockWorkServerJobs(GetClockWorkServerJobsReq request);

		// Token: 0x060001A9 RID: 425
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkServerJobByIdResp GetClockWorkServerJobById(GetClockWorkServerJobByIdReq request);

		// Token: 0x060001AA RID: 426
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateClockWorkServerJobResp CreateClockWorkServerJob(CreateClockWorkServerJobReq request);

		// Token: 0x060001AB RID: 427
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateClockWorkServerJobResp UpdateClockWorkServerJob(UpdateClockWorkServerJobReq request);

		// Token: 0x060001AC RID: 428
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveClockWorkServerJobResp RemoveClockWorkServerJob(RemoveClockWorkServerJobReq request);

		// Token: 0x060001AD RID: 429
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkServerExecutingLogsByJobResp GetClockWorkServerExecutingLogsByJob(GetClockWorkServerExecutingLogsByJobReq request);

		// Token: 0x060001AE RID: 430
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkServerExecutingLogsResp GetClockWorkServerExecutingLogs(GetClockWorkServerExecutingLogsReq request);

		// Token: 0x060001AF RID: 431
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkServerJobTypesResp GetClockWorkServerJobTypes(GetClockWorkServerJobTypesReq request);

		// Token: 0x060001B0 RID: 432
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RunClockWorkServerJobNowResp RunClockWorkServerJobNow(RunClockWorkServerJobNowReq request);

		// Token: 0x060001B1 RID: 433
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		EnableClockWorkServerJobResp EnableClockWorkServerJob(EnableClockWorkServerJobReq request);

		// Token: 0x060001B2 RID: 434
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DisableClockWorkServerJobResp DisableClockWorkServerJob(DisableClockWorkServerJobReq request);
	}
}
