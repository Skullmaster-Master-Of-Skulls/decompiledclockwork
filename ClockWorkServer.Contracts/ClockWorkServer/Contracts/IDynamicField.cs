using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000045 RID: 69
	[ServiceContract(Name = "DynamicFieldService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDynamicField : IService
	{
		// Token: 0x06000224 RID: 548
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFieldsAsTreeResp LoadFieldsAsTree(LoadFieldsAsTreeReq Request);

		// Token: 0x06000225 RID: 549
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFieldsByControlIdsResp LoadFieldsByControlIds(LoadFieldsByControlIdsReq Request);

		// Token: 0x06000226 RID: 550
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFieldsByFormResp LoadFieldsByForm(LoadFieldsByFormReq Request);

		// Token: 0x06000227 RID: 551
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFieldsByFormIdResp LoadFieldsByFormId(LoadFieldsByFormIdReq Request);

		// Token: 0x06000228 RID: 552
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFieldByNameResp LoadFieldByName(LoadFieldByNameReq Request);

		// Token: 0x06000229 RID: 553
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateFieldResp CreateField(CreateFieldReq Request);

		// Token: 0x0600022A RID: 554
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadListItemsResp LoadListItems(LoadListItemsReq Request);

		// Token: 0x0600022B RID: 555
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormsWithControls2Resp LoadFormsWithControls2(LoadFormsWithControls2Req Request);

		// Token: 0x0600022C RID: 556
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetEmailFieldResp GetEmailField(GetEmailFieldReq Request);

		// Token: 0x0600022D RID: 557
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsListItemSavedSomewhereResp IsListItemSavedSomewhere(IsListItemSavedSomewhereReq Request);

		// Token: 0x0600022E RID: 558
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllLookupListsResp LoadAllLookupLists(LoadAllLookupListsReq Request);

		// Token: 0x0600022F RID: 559
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetFieldPossibleValuesResp GetFieldPossibleValues(GetFieldPossibleValuesReq Request);

		// Token: 0x06000230 RID: 560
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateListResp CreateList(CreateListReq Request);

		// Token: 0x06000231 RID: 561
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateFieldsResp CreateFields(CreateFieldsReq Request);

		// Token: 0x06000232 RID: 562
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadControlIdsOnFormsResp LoadControlIdsOnForms(LoadControlIdsOnFormsReq Request);
	}
}
