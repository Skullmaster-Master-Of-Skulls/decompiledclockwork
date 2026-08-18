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
	// Token: 0x02000058 RID: 88
	[ServiceContract(Name = "InventoryLocationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryLocation : IService
	{
		// Token: 0x060002A2 RID: 674
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateLocationResp CreateLocation(CreateLocationReq request);

		// Token: 0x060002A3 RID: 675
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetLocationByIdResp GetLocationById(GetLocationByIdReq request);

		// Token: 0x060002A4 RID: 676
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllLocationsResp GetAllLocations(GetAllLocationsReq request);

		// Token: 0x060002A5 RID: 677
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetLocationsResp GetLocations(GetLocationsReq request);

		// Token: 0x060002A6 RID: 678
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LocationInUseResp LocationInUse(LocationInUseReq request);

		// Token: 0x060002A7 RID: 679
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteLocationResp DeleteLocation(DeleteLocationReq request);

		// Token: 0x060002A8 RID: 680
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateLocationResp UpdateLocation(UpdateLocationReq request);
	}
}
