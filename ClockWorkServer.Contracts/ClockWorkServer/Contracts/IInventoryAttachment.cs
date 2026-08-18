using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000052 RID: 82
	[ServiceContract(Name = "InventoryAttachmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraSizeService]
	public interface IInventoryAttachment : IService
	{
		// Token: 0x06000272 RID: 626
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAttachmentByIdResp GetAttachmentById(GetAttachmentByIdReq request);

		// Token: 0x06000273 RID: 627
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductAttachmentsResp GetProductAttachments(GetProductAttachmentsReq request);

		// Token: 0x06000274 RID: 628
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddAttachmentToProductResp AddAttachmentToProduct(AddAttachmentToProductReq request);

		// Token: 0x06000275 RID: 629
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddAttachmentsToProductResp AddAttachmentsToProduct(AddAttachmentsToProductReq request);

		// Token: 0x06000276 RID: 630
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveAttachmentFromProductResp RemoveAttachmentFromProduct(RemoveAttachmentFromProductReq request);

		// Token: 0x06000277 RID: 631
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveAttachmentsFromProductResp RemoveAttachmentsFromProduct(RemoveAttachmentsFromProductReq request);

		// Token: 0x06000278 RID: 632
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveAllAttachmentsFromProductResp RemoveAllAttachmentsFromProduct(RemoveAllAttachmentsFromProductReq request);

		// Token: 0x06000279 RID: 633
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetProductPictureResp GetProductPicture(GetProductPictureReq request);

		// Token: 0x0600027A RID: 634
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SetProductPictureResp SetProductPicture(SetProductPictureReq request);
	}
}
