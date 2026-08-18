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
	// Token: 0x02000073 RID: 115
	public class CustomFormListItemReusableClientProxy : WCFTokenBasedReusableClientProxy<ICustomFormListItem>, ICustomFormListItem, IService
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0000DAD7 File Offset: 0x0000BCD7
		public CustomFormListItemReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000DAE2 File Offset: 0x0000BCE2
		public CustomFormListItemReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		[DebuggerStepThrough]
		public Task<LoadListItemsByGroupIdResp> LoadListItemsByGroupIdAsync(LoadListItemsByGroupIdReq Request)
		{
			CustomFormListItemReusableClientProxy.<LoadListItemsByGroupIdAsync>d__2 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemReusableClientProxy.<LoadListItemsByGroupIdAsync>d__2();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemsByGroupIdResp>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.Request = Request;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<LoadListItemsByGroupIdAsync>d__2>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		public LoadListItemsByGroupIdResp LoadListItemsByGroupId(LoadListItemsByGroupIdReq Request)
		{
			return this.WrapServiceMethod<LoadListItemsByGroupIdResp>(() => this.Proxy.LoadListItemsByGroupId(Request));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000DB74 File Offset: 0x0000BD74
		[DebuggerStepThrough]
		public Task<LoadListItemByListItemIdResp> LoadListItemByListItemIdAsync(LoadListItemByListItemIdReq Request)
		{
			CustomFormListItemReusableClientProxy.<LoadListItemByListItemIdAsync>d__4 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemReusableClientProxy.<LoadListItemByListItemIdAsync>d__4();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemByListItemIdResp>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.Request = Request;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<LoadListItemByListItemIdAsync>d__4>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public LoadListItemByListItemIdResp LoadListItemByListItemId(LoadListItemByListItemIdReq Request)
		{
			return this.WrapServiceMethod<LoadListItemByListItemIdResp>(() => this.Proxy.LoadListItemByListItemId(Request));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		[DebuggerStepThrough]
		public Task<CreateCustomListGroupResp> CreateCustomListGroupAsync(CreateCustomListGroupReq Request)
		{
			CustomFormListItemReusableClientProxy.<CreateCustomListGroupAsync>d__6 <CreateCustomListGroupAsync>d__ = new CustomFormListItemReusableClientProxy.<CreateCustomListGroupAsync>d__6();
			<CreateCustomListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListGroupResp>.Create();
			<CreateCustomListGroupAsync>d__.<>4__this = this;
			<CreateCustomListGroupAsync>d__.Request = Request;
			<CreateCustomListGroupAsync>d__.<>1__state = -1;
			<CreateCustomListGroupAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<CreateCustomListGroupAsync>d__6>(ref <CreateCustomListGroupAsync>d__);
			return <CreateCustomListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000DC44 File Offset: 0x0000BE44
		[DebuggerStepThrough]
		public Task<CreateCustomListItemResp> CreateCustomListItemAsync(CreateCustomListItemReq Request)
		{
			CustomFormListItemReusableClientProxy.<CreateCustomListItemAsync>d__7 <CreateCustomListItemAsync>d__ = new CustomFormListItemReusableClientProxy.<CreateCustomListItemAsync>d__7();
			<CreateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListItemResp>.Create();
			<CreateCustomListItemAsync>d__.<>4__this = this;
			<CreateCustomListItemAsync>d__.Request = Request;
			<CreateCustomListItemAsync>d__.<>1__state = -1;
			<CreateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<CreateCustomListItemAsync>d__7>(ref <CreateCustomListItemAsync>d__);
			return <CreateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000DC90 File Offset: 0x0000BE90
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemResp> UpdateCustomListItemAsync(UpdateCustomListItemReq Request)
		{
			CustomFormListItemReusableClientProxy.<UpdateCustomListItemAsync>d__8 <UpdateCustomListItemAsync>d__ = new CustomFormListItemReusableClientProxy.<UpdateCustomListItemAsync>d__8();
			<UpdateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemResp>.Create();
			<UpdateCustomListItemAsync>d__.<>4__this = this;
			<UpdateCustomListItemAsync>d__.Request = Request;
			<UpdateCustomListItemAsync>d__.<>1__state = -1;
			<UpdateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<UpdateCustomListItemAsync>d__8>(ref <UpdateCustomListItemAsync>d__);
			return <UpdateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemGroupResp> UpdateCustomListItemGroupAsync(UpdateCustomListItemGroupReq Request)
		{
			CustomFormListItemReusableClientProxy.<UpdateCustomListItemGroupAsync>d__9 <UpdateCustomListItemGroupAsync>d__ = new CustomFormListItemReusableClientProxy.<UpdateCustomListItemGroupAsync>d__9();
			<UpdateCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemGroupResp>.Create();
			<UpdateCustomListItemGroupAsync>d__.<>4__this = this;
			<UpdateCustomListItemGroupAsync>d__.Request = Request;
			<UpdateCustomListItemGroupAsync>d__.<>1__state = -1;
			<UpdateCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<UpdateCustomListItemGroupAsync>d__9>(ref <UpdateCustomListItemGroupAsync>d__);
			return <UpdateCustomListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000DD28 File Offset: 0x0000BF28
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemResp> EnableOrDisableCustomListItemAsync(EnableOrDisableCustomListItemReq Request)
		{
			CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemAsync>d__10 <EnableOrDisableCustomListItemAsync>d__ = new CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemAsync>d__10();
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemResp>.Create();
			<EnableOrDisableCustomListItemAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemAsync>d__10>(ref <EnableOrDisableCustomListItemAsync>d__);
			return <EnableOrDisableCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000DD74 File Offset: 0x0000BF74
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemGroupResp> EnableOrDisableCustomListItemGroupAsync(EnableOrDisableCustomListItemGroupReq Request)
		{
			CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemGroupAsync>d__11 <EnableOrDisableCustomListItemGroupAsync>d__ = new CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemGroupAsync>d__11();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemGroupResp>.Create();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemGroupAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemReusableClientProxy.<EnableOrDisableCustomListItemGroupAsync>d__11>(ref <EnableOrDisableCustomListItemGroupAsync>d__);
			return <EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
