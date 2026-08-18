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
	// Token: 0x02000072 RID: 114
	internal class CustomFormClientBaseProxy : ClientBase<ICustomForm>, ICustomForm, IService
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0000D923 File Offset: 0x0000BB23
		public CustomFormClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000D92E File Offset: 0x0000BB2E
		public CustomFormClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000D93C File Offset: 0x0000BB3C
		[DebuggerStepThrough]
		public Task<LoadFormByIdResp> LoadFormByIdAsync(LoadFormByIdReq Request)
		{
			CustomFormClientBaseProxy.<LoadFormByIdAsync>d__2 <LoadFormByIdAsync>d__ = new CustomFormClientBaseProxy.<LoadFormByIdAsync>d__2();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadFormByIdResp>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.Request = Request;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormClientBaseProxy.<LoadFormByIdAsync>d__2>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000D988 File Offset: 0x0000BB88
		public LoadFormByIdResp LoadFormById(LoadFormByIdReq Request)
		{
			return base.Channel.LoadFormById(Request);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		[DebuggerStepThrough]
		public Task<CreateCustomFormResp> CreateCustomFormAsync(CreateCustomFormReq Request)
		{
			CustomFormClientBaseProxy.<CreateCustomFormAsync>d__4 <CreateCustomFormAsync>d__ = new CustomFormClientBaseProxy.<CreateCustomFormAsync>d__4();
			<CreateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomFormResp>.Create();
			<CreateCustomFormAsync>d__.<>4__this = this;
			<CreateCustomFormAsync>d__.Request = Request;
			<CreateCustomFormAsync>d__.<>1__state = -1;
			<CreateCustomFormAsync>d__.<>t__builder.Start<CustomFormClientBaseProxy.<CreateCustomFormAsync>d__4>(ref <CreateCustomFormAsync>d__);
			return <CreateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		[DebuggerStepThrough]
		public Task<DeleteCustomFormResp> DeleteCustomFormAsync(DeleteCustomFormReq Request)
		{
			CustomFormClientBaseProxy.<DeleteCustomFormAsync>d__5 <DeleteCustomFormAsync>d__ = new CustomFormClientBaseProxy.<DeleteCustomFormAsync>d__5();
			<DeleteCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteCustomFormResp>.Create();
			<DeleteCustomFormAsync>d__.<>4__this = this;
			<DeleteCustomFormAsync>d__.Request = Request;
			<DeleteCustomFormAsync>d__.<>1__state = -1;
			<DeleteCustomFormAsync>d__.<>t__builder.Start<CustomFormClientBaseProxy.<DeleteCustomFormAsync>d__5>(ref <DeleteCustomFormAsync>d__);
			return <DeleteCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000DA40 File Offset: 0x0000BC40
		[DebuggerStepThrough]
		public Task<UpdateCustomFormResp> UpdateCustomFormAsync(UpdateCustomFormReq Request)
		{
			CustomFormClientBaseProxy.<UpdateCustomFormAsync>d__6 <UpdateCustomFormAsync>d__ = new CustomFormClientBaseProxy.<UpdateCustomFormAsync>d__6();
			<UpdateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomFormResp>.Create();
			<UpdateCustomFormAsync>d__.<>4__this = this;
			<UpdateCustomFormAsync>d__.Request = Request;
			<UpdateCustomFormAsync>d__.<>1__state = -1;
			<UpdateCustomFormAsync>d__.<>t__builder.Start<CustomFormClientBaseProxy.<UpdateCustomFormAsync>d__6>(ref <UpdateCustomFormAsync>d__);
			return <UpdateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		[DebuggerStepThrough]
		public Task<LoadAllCustomFormsResp> LoadAllCustomFormsAsync(LoadAllCustomFormsReq Request)
		{
			CustomFormClientBaseProxy.<LoadAllCustomFormsAsync>d__7 <LoadAllCustomFormsAsync>d__ = new CustomFormClientBaseProxy.<LoadAllCustomFormsAsync>d__7();
			<LoadAllCustomFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllCustomFormsResp>.Create();
			<LoadAllCustomFormsAsync>d__.<>4__this = this;
			<LoadAllCustomFormsAsync>d__.Request = Request;
			<LoadAllCustomFormsAsync>d__.<>1__state = -1;
			<LoadAllCustomFormsAsync>d__.<>t__builder.Start<CustomFormClientBaseProxy.<LoadAllCustomFormsAsync>d__7>(ref <LoadAllCustomFormsAsync>d__);
			return <LoadAllCustomFormsAsync>d__.<>t__builder.Task;
		}
	}
}
