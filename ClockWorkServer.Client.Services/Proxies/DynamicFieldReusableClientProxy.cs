using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008A RID: 138
	public class DynamicFieldReusableClientProxy : WCFTokenBasedReusableClientProxy<IDynamicField>, IDynamicField, IService
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public DynamicFieldReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000FFA6 File Offset: 0x0000E1A6
		public DynamicFieldReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		public CreateFieldResp CreateField(CreateFieldReq Request)
		{
			return this.WrapServiceMethod<CreateFieldResp>(() => this.Proxy.CreateField(Request));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public LoadFieldsByFormIdResp LoadFieldsByFormId(LoadFieldsByFormIdReq Request)
		{
			return this.WrapServiceMethod<LoadFieldsByFormIdResp>(() => this.Proxy.LoadFieldsByFormId(Request));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00010024 File Offset: 0x0000E224
		public LoadFieldByNameResp LoadFieldByName(LoadFieldByNameReq Request)
		{
			return this.WrapServiceMethod<LoadFieldByNameResp>(() => this.Proxy.LoadFieldByName(Request));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001005C File Offset: 0x0000E25C
		public LoadFieldsByFormResp LoadFieldsByForm(LoadFieldsByFormReq Request)
		{
			return this.WrapServiceMethod<LoadFieldsByFormResp>(() => this.Proxy.LoadFieldsByForm(Request));
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00010094 File Offset: 0x0000E294
		public LoadListItemsResp LoadListItems(LoadListItemsReq Request)
		{
			return this.WrapServiceMethod<LoadListItemsResp>(() => this.Proxy.LoadListItems(Request));
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000100CC File Offset: 0x0000E2CC
		public LoadFieldsAsTreeResp LoadFieldsAsTree(LoadFieldsAsTreeReq Request)
		{
			return this.WrapServiceMethod<LoadFieldsAsTreeResp>(() => this.Proxy.LoadFieldsAsTree(Request));
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00010104 File Offset: 0x0000E304
		public LoadFieldsByControlIdsResp LoadFieldsByControlIds(LoadFieldsByControlIdsReq Request)
		{
			return this.WrapServiceMethod<LoadFieldsByControlIdsResp>(() => this.Proxy.LoadFieldsByControlIds(Request));
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001013C File Offset: 0x0000E33C
		public LoadFormsWithControls2Resp LoadFormsWithControls2(LoadFormsWithControls2Req Request)
		{
			return this.WrapServiceMethod<LoadFormsWithControls2Resp>(() => this.Proxy.LoadFormsWithControls2(Request));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00010174 File Offset: 0x0000E374
		public GetEmailFieldResp GetEmailField(GetEmailFieldReq Request)
		{
			return this.WrapServiceMethod<GetEmailFieldResp>(() => this.Proxy.GetEmailField(Request));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000101AC File Offset: 0x0000E3AC
		public IsListItemSavedSomewhereResp IsListItemSavedSomewhere(IsListItemSavedSomewhereReq Request)
		{
			return this.WrapServiceMethod<IsListItemSavedSomewhereResp>(() => this.Proxy.IsListItemSavedSomewhere(Request));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public LoadAllLookupListsResp LoadAllLookupLists(LoadAllLookupListsReq Request)
		{
			return this.WrapServiceMethod<LoadAllLookupListsResp>(() => this.Proxy.LoadAllLookupLists(Request));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001021C File Offset: 0x0000E41C
		public GetFieldPossibleValuesResp GetFieldPossibleValues(GetFieldPossibleValuesReq Request)
		{
			return this.WrapServiceMethod<GetFieldPossibleValuesResp>(() => this.Proxy.GetFieldPossibleValues(Request));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00010254 File Offset: 0x0000E454
		public CreateListResp CreateList(CreateListReq Request)
		{
			return this.WrapServiceMethod<CreateListResp>(() => this.Proxy.CreateList(Request));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001028C File Offset: 0x0000E48C
		public CreateFieldsResp CreateFields(CreateFieldsReq Request)
		{
			return this.WrapServiceMethod<CreateFieldsResp>(() => this.Proxy.CreateFields(Request));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public LoadControlIdsOnFormsResp LoadControlIdsOnForms(LoadControlIdsOnFormsReq Request)
		{
			return this.WrapServiceMethod<LoadControlIdsOnFormsResp>(() => this.Proxy.LoadControlIdsOnForms(Request));
		}
	}
}
