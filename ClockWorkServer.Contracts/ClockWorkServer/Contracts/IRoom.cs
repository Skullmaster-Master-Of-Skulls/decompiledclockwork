using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000079 RID: 121
	[ServiceContract(Name = "RoomService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IRoom : IService
	{
		// Token: 0x06000378 RID: 888
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllSeatsResp LoadAllSeats(LoadAllSeatsReq Request);
	}
}
