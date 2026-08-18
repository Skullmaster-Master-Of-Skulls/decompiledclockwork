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
	// Token: 0x02000059 RID: 89
	[ServiceContract(Name = "InventoryProductService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryProduct : IService
	{
		// Token: 0x060002A9 RID: 681
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsMatchingResp GetProductsMatching(GetProductsMatchingReq request);

		// Token: 0x060002AA RID: 682
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductByIdResp GetProductById(GetProductByIdReq request);

		// Token: 0x060002AB RID: 683
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductBySerialNumberResp GetProductBySerialNumber(GetProductBySerialNumberReq request);

		// Token: 0x060002AC RID: 684
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductByBarCodeResp GetProductByBarCode(GetProductByBarCodeReq request);

		// Token: 0x060002AD RID: 685
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsByCatalogResp GetProductsByCatalog(GetProductsByCatalogReq request);

		// Token: 0x060002AE RID: 686
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsByRootCategoryResp GetProductsByRootCategory(GetProductsByRootCategoryReq request);

		// Token: 0x060002AF RID: 687
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsByCategoryResp GetProductsByCategory(GetProductsByCategoryReq request);

		// Token: 0x060002B0 RID: 688
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsByGroupResp GetProductsByGroup(GetProductsByGroupReq request);

		// Token: 0x060002B1 RID: 689
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsByLoanResp GetProductsByLoan(GetProductsByLoanReq request);

		// Token: 0x060002B2 RID: 690
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateProductResp UpdateProduct(UpdateProductReq request);

		// Token: 0x060002B3 RID: 691
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateProductResp CreateProduct(CreateProductReq request);

		// Token: 0x060002B4 RID: 692
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteProductResp DeleteProduct(DeleteProductReq request);

		// Token: 0x060002B5 RID: 693
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteProductsResp DeleteProducts(DeleteProductsReq request);

		// Token: 0x060002B6 RID: 694
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeProductsCategoryResp ChangeProductsCategory(ChangeProductsCategoryReq request);

		// Token: 0x060002B7 RID: 695
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AssignProductToGroupResp AssignProductToGroup(AssignProductToGroupReq request);

		// Token: 0x060002B8 RID: 696
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AssignProductsToGroupResp AssignProductsToGroup(AssignProductsToGroupReq request);

		// Token: 0x060002B9 RID: 697
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductSnapshotResp GetProductSnapshot(GetProductSnapshotReq request);

		// Token: 0x060002BA RID: 698
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductHistoryByIdResp GetProductHistoryById(GetProductHistoryByIdReq request);

		// Token: 0x060002BB RID: 699
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductHistoryByBarcodeResp GetProductHistoryByBarcode(GetProductHistoryByBarcodeReq request);

		// Token: 0x060002BC RID: 700
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductAvailabilityResp GetProductAvailability(GetProductAvailabilityReq request);

		// Token: 0x060002BD RID: 701
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductsInReservationGroupResp GetProductsInReservationGroup(GetProductsInReservationGroupReq request);

		// Token: 0x060002BE RID: 702
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ProductBarcodeAlreadyExistsResp ProductBarcodeAlreadyExists(ProductBarcodeAlreadyExistsReq request);
	}
}
