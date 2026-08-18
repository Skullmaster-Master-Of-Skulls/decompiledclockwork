using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000074 RID: 116
	internal class CustomFormListItemClientBaseProxy : ClientBase<ICustomFormListItem>, ICustomFormListItem, IService
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000DDBF File Offset: 0x0000BFBF
		public CustomFormListItemClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		public CustomFormListItemClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		[DebuggerStepThrough]
		public Task<LoadListItemsByGroupIdResp> LoadListItemsByGroupIdAsync(LoadListItemsByGroupIdReq Request)
		{
			CustomFormListItemClientBaseProxy.<LoadListItemsByGroupIdAsync>d__2 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemClientBaseProxy.<LoadListItemsByGroupIdAsync>d__2();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemsByGroupIdResp>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.Request = Request;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<LoadListItemsByGroupIdAsync>d__2>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000DE24 File Offset: 0x0000C024
		public LoadListItemsByGroupIdResp LoadListItemsByGroupId(LoadListItemsByGroupIdReq Request)
		{
			return base.Channel.LoadListItemsByGroupId(Request);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000DE44 File Offset: 0x0000C044
		[DebuggerStepThrough]
		public Task<LoadListItemByListItemIdResp> LoadListItemByListItemIdAsync(LoadListItemByListItemIdReq Request)
		{
			CustomFormListItemClientBaseProxy.<LoadListItemByListItemIdAsync>d__4 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemClientBaseProxy.<LoadListItemByListItemIdAsync>d__4();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemByListItemIdResp>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.Request = Request;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<LoadListItemByListItemIdAsync>d__4>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000DE90 File Offset: 0x0000C090
		public LoadListItemByListItemIdResp LoadListItemByListItemId(LoadListItemByListItemIdReq Request)
		{
			return base.Channel.LoadListItemByListItemId(Request);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		[DebuggerStepThrough]
		public Task<CreateCustomListGroupResp> CreateCustomListGroupAsync(CreateCustomListGroupReq Request)
		{
			CustomFormListItemClientBaseProxy.<CreateCustomListGroupAsync>d__6 <CreateCustomListGroupAsync>d__ = new CustomFormListItemClientBaseProxy.<CreateCustomListGroupAsync>d__6();
			<CreateCustomListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListGroupResp>.Create();
			<CreateCustomListGroupAsync>d__.<>4__this = this;
			<CreateCustomListGroupAsync>d__.Request = Request;
			<CreateCustomListGroupAsync>d__.<>1__state = -1;
			<CreateCustomListGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<CreateCustomListGroupAsync>d__6>(ref <CreateCustomListGroupAsync>d__);
			return <CreateCustomListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		[DebuggerStepThrough]
		public Task<CreateCustomListItemResp> CreateCustomListItemAsync(CreateCustomListItemReq Request)
		{
			CustomFormListItemClientBaseProxy.<CreateCustomListItemAsync>d__7 <CreateCustomListItemAsync>d__ = new CustomFormListItemClientBaseProxy.<CreateCustomListItemAsync>d__7();
			<CreateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListItemResp>.Create();
			<CreateCustomListItemAsync>d__.<>4__this = this;
			<CreateCustomListItemAsync>d__.Request = Request;
			<CreateCustomListItemAsync>d__.<>1__state = -1;
			<CreateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<CreateCustomListItemAsync>d__7>(ref <CreateCustomListItemAsync>d__);
			return <CreateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000DF48 File Offset: 0x0000C148
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemResp> UpdateCustomListItemAsync(UpdateCustomListItemReq Request)
		{
			CustomFormListItemClientBaseProxy.<UpdateCustomListItemAsync>d__8 <UpdateCustomListItemAsync>d__ = new CustomFormListItemClientBaseProxy.<UpdateCustomListItemAsync>d__8();
			<UpdateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemResp>.Create();
			<UpdateCustomListItemAsync>d__.<>4__this = this;
			<UpdateCustomListItemAsync>d__.Request = Request;
			<UpdateCustomListItemAsync>d__.<>1__state = -1;
			<UpdateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<UpdateCustomListItemAsync>d__8>(ref <UpdateCustomListItemAsync>d__);
			return <UpdateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000DF94 File Offset: 0x0000C194
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemGroupResp> UpdateCustomListItemGroupAsync(UpdateCustomListItemGroupReq Request)
		{
			CustomFormListItemClientBaseProxy.<UpdateCustomListItemGroupAsync>d__9 <UpdateCustomListItemGroupAsync>d__ = new CustomFormListItemClientBaseProxy.<UpdateCustomListItemGroupAsync>d__9();
			<UpdateCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemGroupResp>.Create();
			<UpdateCustomListItemGroupAsync>d__.<>4__this = this;
			<UpdateCustomListItemGroupAsync>d__.Request = Request;
			<UpdateCustomListItemGroupAsync>d__.<>1__state = -1;
			<UpdateCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<UpdateCustomListItemGroupAsync>d__9>(ref <UpdateCustomListItemGroupAsync>d__);
			return <UpdateCustomListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemResp> EnableOrDisableCustomListItemAsync(EnableOrDisableCustomListItemReq Request)
		{
			CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemAsync>d__10 <EnableOrDisableCustomListItemAsync>d__ = new CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemAsync>d__10();
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemResp>.Create();
			<EnableOrDisableCustomListItemAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemAsync>d__10>(ref <EnableOrDisableCustomListItemAsync>d__);
			return <EnableOrDisableCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000E02C File Offset: 0x0000C22C
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemGroupResp> EnableOrDisableCustomListItemGroupAsync(EnableOrDisableCustomListItemGroupReq Request)
		{
			CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemGroupAsync>d__11 <EnableOrDisableCustomListItemGroupAsync>d__ = new CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemGroupAsync>d__11();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemGroupResp>.Create();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemGroupAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientBaseProxy.<EnableOrDisableCustomListItemGroupAsync>d__11>(ref <EnableOrDisableCustomListItemGroupAsync>d__);
			return <EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
