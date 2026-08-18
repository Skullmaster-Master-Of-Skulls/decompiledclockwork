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
	// Token: 0x02000053 RID: 83
	[ServiceContract(Name = "InventoryCatalogService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryCatalog : IService
	{
		// Token: 0x0600027B RID: 635
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCatalogByIdResp GetCatalogById(GetCatalogByIdReq request);

		// Token: 0x0600027C RID: 636
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCatalogByNameResp GetCatalogByName(GetCatalogByNameReq request);

		// Token: 0x0600027D RID: 637
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCatalogsResp GetCatalogs(GetCatalogsReq request);

		// Token: 0x0600027E RID: 638
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExportToXMLResp ExportToXML(ExportToXMLReq request);

		// Token: 0x0600027F RID: 639
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTemplateCatalogByNameResp GetTemplateCatalogByName(GetTemplateCatalogByNameReq request);

		// Token: 0x06000280 RID: 640
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTemplateCatalogsResp GetTemplateCatalogs(GetTemplateCatalogsReq request);
	}
}
