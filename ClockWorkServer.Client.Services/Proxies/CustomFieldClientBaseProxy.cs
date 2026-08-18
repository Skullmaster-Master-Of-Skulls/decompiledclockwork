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
	// Token: 0x02000070 RID: 112
	internal class CustomFieldClientBaseProxy : ClientBase<ICustomField>, ICustomField, IService
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000D65B File Offset: 0x0000B85B
		public CustomFieldClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000D666 File Offset: 0x0000B866
		public CustomFieldClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000D674 File Offset: 0x0000B874
		[DebuggerStepThrough]
		public Task<CreateDataInstanceResp> CreateDataInstanceAsync(CreateDataInstanceReq Request)
		{
			CustomFieldClientBaseProxy.<CreateDataInstanceAsync>d__2 <CreateDataInstanceAsync>d__ = new CustomFieldClientBaseProxy.<CreateDataInstanceAsync>d__2();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateDataInstanceResp>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.Request = Request;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientBaseProxy.<CreateDataInstanceAsync>d__2>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		[DebuggerStepThrough]
		public Task<DeleteDataInstanceResp> DeleteDataInstanceAsync(DeleteDataInstanceReq Request)
		{
			CustomFieldClientBaseProxy.<DeleteDataInstanceAsync>d__3 <DeleteDataInstanceAsync>d__ = new CustomFieldClientBaseProxy.<DeleteDataInstanceAsync>d__3();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteDataInstanceResp>.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.Request = Request;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientBaseProxy.<DeleteDataInstanceAsync>d__3>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000D70C File Offset: 0x0000B90C
		[DebuggerStepThrough]
		public Task<UpdateDataInstanceResp> UpdateDataInstanceAsync(UpdateDataInstanceReq Request)
		{
			CustomFieldClientBaseProxy.<UpdateDataInstanceAsync>d__4 <UpdateDataInstanceAsync>d__ = new CustomFieldClientBaseProxy.<UpdateDataInstanceAsync>d__4();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateDataInstanceResp>.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.Request = Request;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientBaseProxy.<UpdateDataInstanceAsync>d__4>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
