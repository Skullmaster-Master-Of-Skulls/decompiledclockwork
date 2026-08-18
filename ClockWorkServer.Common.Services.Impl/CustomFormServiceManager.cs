using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.Core.CustomForms;
using TechnoPro.Common.Core.Mappers.CustomForms.Form;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000034 RID: 52
	public class CustomFormServiceManager : ICustomForm, IService
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000A374 File Offset: 0x00008574
		[DebuggerStepThrough]
		public Task<LoadFormByIdResp> LoadFormByIdAsync(LoadFormByIdReq Request)
		{
			CustomFormServiceManager.<LoadFormByIdAsync>d__0 <LoadFormByIdAsync>d__ = new CustomFormServiceManager.<LoadFormByIdAsync>d__0();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadFormByIdResp>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.Request = Request;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormServiceManager.<LoadFormByIdAsync>d__0>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public LoadFormByIdResp LoadFormById(LoadFormByIdReq Request)
		{
			ICustomFormManager customFormManager = new CustomFormManager(Request.GetOperationContext());
			CustomForm customForm = customFormManager.LoadFormById(Request.FormId);
			return new LoadFormByIdResp
			{
				Form = ((customForm != null) ? customForm.ToDTO() : null)
			};
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A404 File Offset: 0x00008604
		[DebuggerStepThrough]
		public Task<CreateCustomFormResp> CreateCustomFormAsync(CreateCustomFormReq Request)
		{
			CustomFormServiceManager.<CreateCustomFormAsync>d__2 <CreateCustomFormAsync>d__ = new CustomFormServiceManager.<CreateCustomFormAsync>d__2();
			<CreateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomFormResp>.Create();
			<CreateCustomFormAsync>d__.<>4__this = this;
			<CreateCustomFormAsync>d__.Request = Request;
			<CreateCustomFormAsync>d__.<>1__state = -1;
			<CreateCustomFormAsync>d__.<>t__builder.Start<CustomFormServiceManager.<CreateCustomFormAsync>d__2>(ref <CreateCustomFormAsync>d__);
			return <CreateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A450 File Offset: 0x00008650
		[DebuggerStepThrough]
		public Task<DeleteCustomFormResp> DeleteCustomFormAsync(DeleteCustomFormReq Request)
		{
			CustomFormServiceManager.<DeleteCustomFormAsync>d__3 <DeleteCustomFormAsync>d__ = new CustomFormServiceManager.<DeleteCustomFormAsync>d__3();
			<DeleteCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteCustomFormResp>.Create();
			<DeleteCustomFormAsync>d__.<>4__this = this;
			<DeleteCustomFormAsync>d__.Request = Request;
			<DeleteCustomFormAsync>d__.<>1__state = -1;
			<DeleteCustomFormAsync>d__.<>t__builder.Start<CustomFormServiceManager.<DeleteCustomFormAsync>d__3>(ref <DeleteCustomFormAsync>d__);
			return <DeleteCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A49C File Offset: 0x0000869C
		[DebuggerStepThrough]
		public Task<UpdateCustomFormResp> UpdateCustomFormAsync(UpdateCustomFormReq Request)
		{
			CustomFormServiceManager.<UpdateCustomFormAsync>d__4 <UpdateCustomFormAsync>d__ = new CustomFormServiceManager.<UpdateCustomFormAsync>d__4();
			<UpdateCustomFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomFormResp>.Create();
			<UpdateCustomFormAsync>d__.<>4__this = this;
			<UpdateCustomFormAsync>d__.Request = Request;
			<UpdateCustomFormAsync>d__.<>1__state = -1;
			<UpdateCustomFormAsync>d__.<>t__builder.Start<CustomFormServiceManager.<UpdateCustomFormAsync>d__4>(ref <UpdateCustomFormAsync>d__);
			return <UpdateCustomFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A4E8 File Offset: 0x000086E8
		[DebuggerStepThrough]
		public Task<LoadAllCustomFormsResp> LoadAllCustomFormsAsync(LoadAllCustomFormsReq Request)
		{
			CustomFormServiceManager.<LoadAllCustomFormsAsync>d__5 <LoadAllCustomFormsAsync>d__ = new CustomFormServiceManager.<LoadAllCustomFormsAsync>d__5();
			<LoadAllCustomFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllCustomFormsResp>.Create();
			<LoadAllCustomFormsAsync>d__.<>4__this = this;
			<LoadAllCustomFormsAsync>d__.Request = Request;
			<LoadAllCustomFormsAsync>d__.<>1__state = -1;
			<LoadAllCustomFormsAsync>d__.<>t__builder.Start<CustomFormServiceManager.<LoadAllCustomFormsAsync>d__5>(ref <LoadAllCustomFormsAsync>d__);
			return <LoadAllCustomFormsAsync>d__.<>t__builder.Task;
		}
	}
}
