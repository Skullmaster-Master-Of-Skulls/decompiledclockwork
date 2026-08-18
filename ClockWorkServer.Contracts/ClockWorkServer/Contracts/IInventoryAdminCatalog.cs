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
	// Token: 0x02000051 RID: 81
	[ServiceContract(Name = "InventoryAdminCatalogService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryAdminCatalog : IService
	{
		// Token: 0x0600026C RID: 620
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetFullCatalogListResp GetFullCatalogList(GetFullCatalogListReq request);

		// Token: 0x0600026D RID: 621
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateCatalogResp CreateCatalog(CreateCatalogReq request);

		// Token: 0x0600026E RID: 622
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateCatalogResp UpdateCatalog(UpdateCatalogReq request);

		// Token: 0x0600026F RID: 623
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteEmptyCatalogResp DeleteEmptyCatalog(DeleteEmptyCatalogReq request);

		// Token: 0x06000270 RID: 624
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ImportFromXMLResp ImportFromXML(ImportFromXMLReq request);

		// Token: 0x06000271 RID: 625
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ImportFromTemplateResp ImportFromTemplate(ImportFromTemplateReq request);
	}
}
