using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200006F RID: 111
	public class CustomFieldReusableClientProxy : WCFTokenBasedReusableClientProxy<ICustomField>, ICustomField, IService
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D55F File Offset: 0x0000B75F
		public CustomFieldReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D56A File Offset: 0x0000B76A
		public CustomFieldReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000D578 File Offset: 0x0000B778
		[DebuggerStepThrough]
		public Task<CreateDataInstanceResp> CreateDataInstanceAsync(CreateDataInstanceReq Request)
		{
			CustomFieldReusableClientProxy.<CreateDataInstanceAsync>d__2 <CreateDataInstanceAsync>d__ = new CustomFieldReusableClientProxy.<CreateDataInstanceAsync>d__2();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateDataInstanceResp>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.Request = Request;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldReusableClientProxy.<CreateDataInstanceAsync>d__2>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		[DebuggerStepThrough]
		public Task<DeleteDataInstanceResp> DeleteDataInstanceAsync(DeleteDataInstanceReq Request)
		{
			CustomFieldReusableClientProxy.<DeleteDataInstanceAsync>d__3 <DeleteDataInstanceAsync>d__ = new CustomFieldReusableClientProxy.<DeleteDataInstanceAsync>d__3();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteDataInstanceResp>.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.Request = Request;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldReusableClientProxy.<DeleteDataInstanceAsync>d__3>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D610 File Offset: 0x0000B810
		[DebuggerStepThrough]
		public Task<UpdateDataInstanceResp> UpdateDataInstanceAsync(UpdateDataInstanceReq Request)
		{
			CustomFieldReusableClientProxy.<UpdateDataInstanceAsync>d__4 <UpdateDataInstanceAsync>d__ = new CustomFieldReusableClientProxy.<UpdateDataInstanceAsync>d__4();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateDataInstanceResp>.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.Request = Request;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldReusableClientProxy.<UpdateDataInstanceAsync>d__4>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
