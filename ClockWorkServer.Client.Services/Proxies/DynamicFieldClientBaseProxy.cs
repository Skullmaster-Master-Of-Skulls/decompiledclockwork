using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008B RID: 139
	internal class DynamicFieldClientBaseProxy : ClientBase<IDynamicField>, IDynamicField, IService
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x000102FC File Offset: 0x0000E4FC
		public DynamicFieldClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00010307 File Offset: 0x0000E507
		public DynamicFieldClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00010314 File Offset: 0x0000E514
		public LoadFieldsByControlIdsResp LoadFieldsByControlIds(LoadFieldsByControlIdsReq Request)
		{
			return base.Channel.LoadFieldsByControlIds(Request);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00010334 File Offset: 0x0000E534
		public CreateFieldResp CreateField(CreateFieldReq Request)
		{
			return base.Channel.CreateField(Request);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00010354 File Offset: 0x0000E554
		public LoadFieldsByFormIdResp LoadFieldsByFormId(LoadFieldsByFormIdReq Request)
		{
			return base.Channel.LoadFieldsByFormId(Request);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00010374 File Offset: 0x0000E574
		public LoadFieldByNameResp LoadFieldByName(LoadFieldByNameReq Request)
		{
			return base.Channel.LoadFieldByName(Request);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00010394 File Offset: 0x0000E594
		public LoadFieldsByFormResp LoadFieldsByForm(LoadFieldsByFormReq Request)
		{
			return base.Channel.LoadFieldsByForm(Request);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public LoadListItemsResp LoadListItems(LoadListItemsReq Request)
		{
			return base.Channel.LoadListItems(Request);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000103D4 File Offset: 0x0000E5D4
		public LoadFieldsAsTreeResp LoadFieldsAsTree(LoadFieldsAsTreeReq Request)
		{
			return base.Channel.LoadFieldsAsTree(Request);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public LoadFormsWithControls2Resp LoadFormsWithControls2(LoadFormsWithControls2Req Request)
		{
			return base.Channel.LoadFormsWithControls2(Request);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00010414 File Offset: 0x0000E614
		public GetEmailFieldResp GetEmailField(GetEmailFieldReq Request)
		{
			return base.Channel.GetEmailField(Request);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00010434 File Offset: 0x0000E634
		public IsListItemSavedSomewhereResp IsListItemSavedSomewhere(IsListItemSavedSomewhereReq Request)
		{
			return base.Channel.IsListItemSavedSomewhere(Request);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00010454 File Offset: 0x0000E654
		public LoadAllLookupListsResp LoadAllLookupLists(LoadAllLookupListsReq Request)
		{
			return base.Channel.LoadAllLookupLists(Request);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00010474 File Offset: 0x0000E674
		public GetFieldPossibleValuesResp GetFieldPossibleValues(GetFieldPossibleValuesReq Request)
		{
			return base.Channel.GetFieldPossibleValues(Request);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00010494 File Offset: 0x0000E694
		public CreateListResp CreateList(CreateListReq Request)
		{
			return base.Channel.CreateList(Request);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public CreateFieldsResp CreateFields(CreateFieldsReq Request)
		{
			return base.Channel.CreateFields(Request);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public LoadControlIdsOnFormsResp LoadControlIdsOnForms(LoadControlIdsOnFormsReq Request)
		{
			return base.Channel.LoadControlIdsOnForms(Request);
		}
	}
}
