using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000038 RID: 56
	[ServiceContract(Name = "CustomFormListItemService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICustomFormListItem : IService
	{
		// Token: 0x060001C9 RID: 457
		[OperationContract(Name = "LoadListItemsByGroupIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadListItemsByGroupIdResp> LoadListItemsByGroupIdAsync(LoadListItemsByGroupIdReq Request);

		// Token: 0x060001CA RID: 458
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadListItemsByGroupIdResp LoadListItemsByGroupId(LoadListItemsByGroupIdReq Request);

		// Token: 0x060001CB RID: 459
		[OperationContract(Name = "LoadListItemByListItemIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadListItemByListItemIdResp> LoadListItemByListItemIdAsync(LoadListItemByListItemIdReq Request);

		// Token: 0x060001CC RID: 460
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadListItemByListItemIdResp LoadListItemByListItemId(LoadListItemByListItemIdReq Request);

		// Token: 0x060001CD RID: 461
		[OperationContract(Name = "CreateCustomListGroupAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateCustomListGroupResp> CreateCustomListGroupAsync(CreateCustomListGroupReq Request);

		// Token: 0x060001CE RID: 462
		[OperationContract(Name = "CreateCustomListItemAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateCustomListItemResp> CreateCustomListItemAsync(CreateCustomListItemReq Request);

		// Token: 0x060001CF RID: 463
		[OperationContract(Name = "UpdateCustomListItemAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateCustomListItemResp> UpdateCustomListItemAsync(UpdateCustomListItemReq Request);

		// Token: 0x060001D0 RID: 464
		[OperationContract(Name = "UpdateCustomListItemGroupAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateCustomListItemGroupResp> UpdateCustomListItemGroupAsync(UpdateCustomListItemGroupReq Request);

		// Token: 0x060001D1 RID: 465
		[OperationContract(Name = "EnableOrDisableCustomListItemAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<EnableOrDisableCustomListItemResp> EnableOrDisableCustomListItemAsync(EnableOrDisableCustomListItemReq Request);

		// Token: 0x060001D2 RID: 466
		[OperationContract(Name = "EnableOrDisableCustomListItemGroupAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<EnableOrDisableCustomListItemGroupResp> EnableOrDisableCustomListItemGroupAsync(EnableOrDisableCustomListItemGroupReq Request);
	}
}
