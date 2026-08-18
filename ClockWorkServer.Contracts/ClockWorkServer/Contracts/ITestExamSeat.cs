using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200001E RID: 30
	[ServiceContract(Name = "TestExamSeatService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITestExamSeat : IService
	{
		// Token: 0x0600011B RID: 283
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedSeatsResp LoadAllowedSeats(LoadAllowedSeatsReq Request);

		// Token: 0x0600011C RID: 284
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSeatByIdResp LoadSeatById(LoadSeatByIdReq Request);

		// Token: 0x0600011D RID: 285
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRoomsWithAvailabilityResp LoadRoomsWithAvailability(LoadRoomsWithAvailabilityReq Request);
	}
}
