using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200005B RID: 91
	[ServiceContract(Name = "InventoryReservationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryReservation : IService
	{
		// Token: 0x060002C3 RID: 707
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationByIdResp GetReservationById(GetReservationByIdReq request);

		// Token: 0x060002C4 RID: 708
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsByProductResp GetReservationsByProduct(GetReservationsByProductReq request);

		// Token: 0x060002C5 RID: 709
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsByProductInDateRangeResp GetReservationsByProductInDateRange(GetReservationsByProductInDateRangeReq request);

		// Token: 0x060002C6 RID: 710
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsByWhoMadeItResp GetReservationsByWhoMadeIt(GetReservationsByWhoMadeItReq request);

		// Token: 0x060002C7 RID: 711
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsResp GetReservations(GetReservationsReq request);

		// Token: 0x060002C8 RID: 712
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsByWhoMadeItInDateRangeResp GetReservationsByWhoMadeItInDateRange(GetReservationsByWhoMadeItInDateRangeReq request);

		// Token: 0x060002C9 RID: 713
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetNextReservationAfterDateByProductResp GetNextReservationAfterDateByProduct(GetNextReservationAfterDateByProductReq request);

		// Token: 0x060002CA RID: 714
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MakeReservationResp MakeReservation(MakeReservationReq request);

		// Token: 0x060002CB RID: 715
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MarkReservationAsCompletedResp MarkReservationAsCompleted(MarkReservationAsCompletedReq request);

		// Token: 0x060002CC RID: 716
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelReservationResp CancelReservation(CancelReservationReq request);

		// Token: 0x060002CD RID: 717
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelReservationGroupResp CancelReservationGroup(CancelReservationGroupReq request);

		// Token: 0x060002CE RID: 718
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateReservationResp UpdateReservation(UpdateReservationReq request);

		// Token: 0x060002CF RID: 719
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateReservationGroupResp UpdateReservationGroup(UpdateReservationGroupReq request);

		// Token: 0x060002D0 RID: 720
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReservationsByReservationGroupIdResp GetReservationsByReservationGroupId(GetReservationsByReservationGroupIdReq request);
	}
}
