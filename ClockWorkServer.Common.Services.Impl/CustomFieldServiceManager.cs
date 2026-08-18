using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000032 RID: 50
	public class CustomFieldServiceManager : ICustomField, IService
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00009F84 File Offset: 0x00008184
		[DebuggerStepThrough]
		public Task<CreateDataInstanceResp> CreateDataInstanceAsync(CreateDataInstanceReq Request)
		{
			CustomFieldServiceManager.<CreateDataInstanceAsync>d__0 <CreateDataInstanceAsync>d__ = new CustomFieldServiceManager.<CreateDataInstanceAsync>d__0();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateDataInstanceResp>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.Request = Request;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldServiceManager.<CreateDataInstanceAsync>d__0>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009FD0 File Offset: 0x000081D0
		[DebuggerStepThrough]
		public Task<DeleteDataInstanceResp> DeleteDataInstanceAsync(DeleteDataInstanceReq Request)
		{
			CustomFieldServiceManager.<DeleteDataInstanceAsync>d__1 <DeleteDataInstanceAsync>d__ = new CustomFieldServiceManager.<DeleteDataInstanceAsync>d__1();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteDataInstanceResp>.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.Request = Request;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldServiceManager.<DeleteDataInstanceAsync>d__1>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A01C File Offset: 0x0000821C
		[DebuggerStepThrough]
		public Task<UpdateDataInstanceResp> UpdateDataInstanceAsync(UpdateDataInstanceReq Request)
		{
			CustomFieldServiceManager.<UpdateDataInstanceAsync>d__2 <UpdateDataInstanceAsync>d__ = new CustomFieldServiceManager.<UpdateDataInstanceAsync>d__2();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateDataInstanceResp>.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.Request = Request;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldServiceManager.<UpdateDataInstanceAsync>d__2>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
