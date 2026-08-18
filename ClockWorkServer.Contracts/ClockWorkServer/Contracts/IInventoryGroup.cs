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
	// Token: 0x02000055 RID: 85
	[ServiceContract(Name = "InventoryGroupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryGroup : IService
	{
		// Token: 0x06000286 RID: 646
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateProductGroupResp CreateProductGroup(CreateProductGroupReq request);

		// Token: 0x06000287 RID: 647
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateProductGroupResp UpdateProductGroup(UpdateProductGroupReq request);

		// Token: 0x06000288 RID: 648
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteEmptyProductGroupResp DeleteEmptyProductGroup(DeleteEmptyProductGroupReq request);

		// Token: 0x06000289 RID: 649
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetGroupByIdResp GetGroupById(GetGroupByIdReq request);

		// Token: 0x0600028A RID: 650
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetGroupsResp GetGroups(GetGroupsReq request);
	}
}
