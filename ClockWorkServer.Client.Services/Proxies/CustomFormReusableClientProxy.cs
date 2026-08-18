using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000071 RID: 113
	public class CustomFormReusableClientProxy : WCFTokenBasedReusableClientProxy<ICustomForm>, ICustomForm, IService
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0000D757 File Offset: 0x0000B957
		public CustomFormReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000D762 File Offset: 0x0000B962
		public CustomFormReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000D770 File Offset: 0x0000B970
		[DebuggerStepThrough]
		public Task<LoadFormByIdResp> LoadFormByIdAsync(LoadFormByIdReq Request)
		{
			CustomFormReusableClientProxy.<LoadFormByIdAsync>d__2 <LoadFormByIdAsync>d__ = new CustomFormReusableClientProxy.<LoadFormByIdAsync>d__2();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadFormByIdResp>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.Request = Request;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormReusableClientProxy.<LoadFormByIdAsync>d__2>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public LoadFormByIdResp LoadFormById(LoadFormByIdReq Request)
		{
			return this.WrapServiceMethod<LoadFormByIdResp>(() => this.Proxy.LoadFormById(Request));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		[DebuggerStepThrough]
		public Task<CreateCustomFormResp> CreateCustomFormAsync(CreateCustomFormReq Request)
		{
			CustomFormReusableClientProxy.<CreateCustomFormAsync>d__4 <CreateCustomFormAsync>d__ = new CustomFormReusableClientProxy.<CreateCustomFormAsync>d__4();
			<CreateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomFormResp>.Create();
			<CreateCustomFormAsync>d__.<>4__this = this;
			<CreateCustomFormAsync>d__.Request = Request;
			<CreateCustomFormAsync>d__.<>1__state = -1;
			<CreateCustomFormAsync>d__.<>t__builder.Start<CustomFormReusableClientProxy.<CreateCustomFormAsync>d__4>(ref <CreateCustomFormAsync>d__);
			return <CreateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000D840 File Offset: 0x0000BA40
		[DebuggerStepThrough]
		public Task<DeleteCustomFormResp> DeleteCustomFormAsync(DeleteCustomFormReq Request)
		{
			CustomFormReusableClientProxy.<DeleteCustomFormAsync>d__5 <DeleteCustomFormAsync>d__ = new CustomFormReusableClientProxy.<DeleteCustomFormAsync>d__5();
			<DeleteCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteCustomFormResp>.Create();
			<DeleteCustomFormAsync>d__.<>4__this = this;
			<DeleteCustomFormAsync>d__.Request = Request;
			<DeleteCustomFormAsync>d__.<>1__state = -1;
			<DeleteCustomFormAsync>d__.<>t__builder.Start<CustomFormReusableClientProxy.<DeleteCustomFormAsync>d__5>(ref <DeleteCustomFormAsync>d__);
			return <DeleteCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000D88C File Offset: 0x0000BA8C
		[DebuggerStepThrough]
		public Task<UpdateCustomFormResp> UpdateCustomFormAsync(UpdateCustomFormReq Request)
		{
			CustomFormReusableClientProxy.<UpdateCustomFormAsync>d__6 <UpdateCustomFormAsync>d__ = new CustomFormReusableClientProxy.<UpdateCustomFormAsync>d__6();
			<UpdateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomFormResp>.Create();
			<UpdateCustomFormAsync>d__.<>4__this = this;
			<UpdateCustomFormAsync>d__.Request = Request;
			<UpdateCustomFormAsync>d__.<>1__state = -1;
			<UpdateCustomFormAsync>d__.<>t__builder.Start<CustomFormReusableClientProxy.<UpdateCustomFormAsync>d__6>(ref <UpdateCustomFormAsync>d__);
			return <UpdateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		[DebuggerStepThrough]
		public Task<LoadAllCustomFormsResp> LoadAllCustomFormsAsync(LoadAllCustomFormsReq Request)
		{
			CustomFormReusableClientProxy.<LoadAllCustomFormsAsync>d__7 <LoadAllCustomFormsAsync>d__ = new CustomFormReusableClientProxy.<LoadAllCustomFormsAsync>d__7();
			<LoadAllCustomFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllCustomFormsResp>.Create();
			<LoadAllCustomFormsAsync>d__.<>4__this = this;
			<LoadAllCustomFormsAsync>d__.Request = Request;
			<LoadAllCustomFormsAsync>d__.<>1__state = -1;
			<LoadAllCustomFormsAsync>d__.<>t__builder.Start<CustomFormReusableClientProxy.<LoadAllCustomFormsAsync>d__7>(ref <LoadAllCustomFormsAsync>d__);
			return <LoadAllCustomFormsAsync>d__.<>t__builder.Task;
		}
	}
}
