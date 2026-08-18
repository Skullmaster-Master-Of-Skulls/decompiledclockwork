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
	// Token: 0x02000095 RID: 149
	[ServiceContract(Name = "AutoTestBookingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAutoTestBooking : IService
	{
		// Token: 0x06000413 RID: 1043
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadBaseAutoTestBookingSettingsResp LoadBaseAutoTestBookingSettings(LoadBaseAutoTestBookingSettingsReq Request);

		// Token: 0x06000414 RID: 1044
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindPotentialBookingsExplicitResp FindPotentialBookingsExplicit(FindPotentialBookingsExplicitReq Request);

		// Token: 0x06000415 RID: 1045
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CalculateExtraTimeResp CalculateExtraTime(CalculateExtraTimeReq Request);

		// Token: 0x06000416 RID: 1046
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CalculateBreakTimeResp CalculateBreakTime(CalculateBreakTimeReq Request);

		// Token: 0x06000417 RID: 1047
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearAutoTestBookingCache(ClearAutoTestBookingCacheReq Request);

		// Token: 0x06000418 RID: 1048
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailableAssetsResp LoadAvailableAssets(LoadAvailableAssetsReq Request);

		// Token: 0x06000419 RID: 1049
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSpecialAccommodationsResp LoadSpecialAccommodations(LoadSpecialAccommodationsReq Request);

		// Token: 0x0600041A RID: 1050
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailableRoomsResp LoadAvailableRooms(LoadAvailableRoomsReq Request);

		// Token: 0x0600041B RID: 1051
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestRulesResp LoadTestRules(LoadTestRulesReq Request);

		// Token: 0x0600041C RID: 1052
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindPotentialBookings2Resp FindPotentialBookings(FindPotentialBookings2Req Request);

		// Token: 0x0600041D RID: 1053
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ApplySpecialAccommodations2Resp ApplySpecialAccommodations(ApplySpecialAccommodations2Req Request);

		// Token: 0x0600041E RID: 1054
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TryToFindBookingResp TryToFindBooking(TryToFindBookingReq Request);

		// Token: 0x0600041F RID: 1055
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AutoBookTestOrExamResp AutoBookTestOrExam(AutoBookTestOrExamReq Request);

		// Token: 0x06000420 RID: 1056
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AutoBookTestOrExamPreviewResp AutoBookTestOrExamPreview(AutoBookTestOrExamPreviewReq Request);

		// Token: 0x06000421 RID: 1057
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AutoRescheduleTestOrExamResp AutoRescheduleTestOrExam(AutoRescheduleTestOrExamReq Request);
	}
}
