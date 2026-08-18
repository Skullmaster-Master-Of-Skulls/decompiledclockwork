using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200002B RID: 43
	[ServiceContract(Name = "AvailabilityScheduleService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAvailabilitySchedule : IService
	{
		// Token: 0x0600017F RID: 383
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityItemsByContextAndDateRangeResp LoadAvailabilityItemsByContextAndDateRange(LoadAvailabilityItemsByContextAndDateRangeReq Request);

		// Token: 0x06000180 RID: 384
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityItemsByMultipleContextsAndDateRangeResp LoadAvailabilityItemsByMultipleContextsAndDateRange(LoadAvailabilityItemsByMultipleContextsAndDateRangeReq Request);

		// Token: 0x06000181 RID: 385
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityItemsByContextAndDatesResp LoadAvailabilityItemsByContextAndDates(LoadAvailabilityItemsByContextAndDatesReq Request);

		// Token: 0x06000182 RID: 386
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddAvailabilityTimesByContextAndDateResp AddAvailabilityTimesByContextAndDate(AddAvailabilityTimesByContextAndDateReq Request);

		// Token: 0x06000183 RID: 387
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddAvailabilityDatesAndTimesByContextResp AddAvailabilityDatesAndTimesByContext(AddAvailabilityDatesAndTimesByContextReq Request);

		// Token: 0x06000184 RID: 388
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteAvailabilityTimeByContextResp DeleteAvailabilityTimeByContext(DeleteAvailabilityTimeByContextReq Request);

		// Token: 0x06000185 RID: 389
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteAvailabilityDatesAndTimesByContextResp DeleteAvailabilityDatesAndTimesByContext(DeleteAvailabilityDatesAndTimesByContextReq Request);

		// Token: 0x06000186 RID: 390
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ClearAvailabilityForTheDayResp ClearAvailabilityForTheDay(ClearAvailabilityForTheDayReq Request);

		// Token: 0x06000187 RID: 391
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDaysWithAvailabilityResp LoadDaysWithAvailability(LoadDaysWithAvailabilityReq Request);

		// Token: 0x06000188 RID: 392
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAvailabilityGroupsResp LoadAllAvailabilityGroups(LoadAllAvailabilityGroupsReq Request);

		// Token: 0x06000189 RID: 393
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq Request);
	}
}
