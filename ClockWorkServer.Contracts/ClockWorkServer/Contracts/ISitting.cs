using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200001A RID: 26
	[ServiceContract(Name = "SittingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ISitting : IService
	{
		// Token: 0x060000FC RID: 252
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateSitting(UpdateSittingReq request);

		// Token: 0x060000FD RID: 253
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateSittingResp CreateSitting(CreateSittingReq Request);

		// Token: 0x060000FE RID: 254
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSittingTestsResp LoadSittingTests(LoadSittingTestsReq request);

		// Token: 0x060000FF RID: 255
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSittingsResp LoadSittings(LoadSittingsReq request);

		// Token: 0x06000100 RID: 256
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSittingByIdResp LoadSittingById(LoadSittingByIdReq request);

		// Token: 0x06000101 RID: 257
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSittingEffectiveTimeRangeResp GetSittingEffectiveTimeRange(GetSittingEffectiveTimeRangeReq request);

		// Token: 0x06000102 RID: 258
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSittingsByDateRangeResp LoadSittingsByDateRange(LoadSittingsByDateRangeReq Request);

		// Token: 0x06000103 RID: 259
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearSittingOnAppointment(ClearSittingOnAppointmentReq Request);

		// Token: 0x06000104 RID: 260
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetSittingOnAppointment(SetSittingOnAppointmentReq Request);
	}
}
