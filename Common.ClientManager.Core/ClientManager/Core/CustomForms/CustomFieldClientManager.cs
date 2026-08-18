using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.Core.CustomForms
{
	// Token: 0x02000070 RID: 112
	public class CustomFieldClientManager : ICustomFieldClientManager, IWebService
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00012540 File Offset: 0x00010740
		[DebuggerStepThrough]
		public Task<Guid> CreateDataInstanceAsync(CustomDataInstanceDTO dataInstance)
		{
			CustomFieldClientManager.<CreateDataInstanceAsync>d__0 <CreateDataInstanceAsync>d__ = new CustomFieldClientManager.<CreateDataInstanceAsync>d__0();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.dataInstance = dataInstance;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientManager.<CreateDataInstanceAsync>d__0>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001258C File Offset: 0x0001078C
		[DebuggerStepThrough]
		public Task DeleteDataInstanceAsync(Guid dataInstanceId)
		{
			CustomFieldClientManager.<DeleteDataInstanceAsync>d__1 <DeleteDataInstanceAsync>d__ = new CustomFieldClientManager.<DeleteDataInstanceAsync>d__1();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.dataInstanceId = dataInstanceId;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientManager.<DeleteDataInstanceAsync>d__1>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000125D8 File Offset: 0x000107D8
		[DebuggerStepThrough]
		public Task UpdateDataInstanceAsync(CustomDataInstanceDTO dataInstance)
		{
			CustomFieldClientManager.<UpdateDataInstanceAsync>d__2 <UpdateDataInstanceAsync>d__ = new CustomFieldClientManager.<UpdateDataInstanceAsync>d__2();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.dataInstance = dataInstance;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldClientManager.<UpdateDataInstanceAsync>d__2>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
