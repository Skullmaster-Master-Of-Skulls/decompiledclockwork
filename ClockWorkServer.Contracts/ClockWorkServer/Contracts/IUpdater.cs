using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000D2 RID: 210
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraSizeService]
	public interface IUpdater : IService, IConnectivity
	{
		// Token: 0x060005B9 RID: 1465
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateResponse GetUpdate(UpdateRequest updateReq);

		// Token: 0x060005BA RID: 1466
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AvailableUpdateResp GetAvailableUpdates(AvailableUpdateReq availableUpdateReq);

		// Token: 0x060005BB RID: 1467
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ApplyUpdateResp ApplyUpdate(ApplyUpdateReq applyUpdateReq);

		// Token: 0x060005BC RID: 1468
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetOnScheduleUpdatesResp GetOnScheduleUpdates(GetOnScheduleUpdatesReq getOnScheduleUpdatesReq);

		// Token: 0x060005BD RID: 1469
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelOnScheduleUpdateResp CancelOnScheduleUpdates(CancelOnScheduleUpdatesReq cancelOnScheduleUpdatesReq);

		// Token: 0x060005BE RID: 1470
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadUpdateFilesResp UploadUpdateFiles(UploadUpdateFilesReq Request);

		// Token: 0x060005BF RID: 1471
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ForceUpdatingServiceToRunResp ForceUpdatingServiceToRun(ForceUpdatingServiceToRunReq Request);
	}
}
