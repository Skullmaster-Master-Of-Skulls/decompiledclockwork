using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.CustomForms
{
	// Token: 0x02000115 RID: 277
	public class CustomFieldManager : ICustomFieldManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00052EE2 File Offset: 0x000510E2
		public CustomFieldManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00052EF4 File Offset: 0x000510F4
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x00052EFC File Offset: 0x000510FC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000BAB RID: 2987 RVA: 0x00052F08 File Offset: 0x00051108
		[DebuggerStepThrough]
		public Task<Guid> CreateDataInstanceAsync(CustomDataInstance dataInstance)
		{
			CustomFieldManager.<CreateDataInstanceAsync>d__5 <CreateDataInstanceAsync>d__ = new CustomFieldManager.<CreateDataInstanceAsync>d__5();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.dataInstance = dataInstance;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldManager.<CreateDataInstanceAsync>d__5>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00052F54 File Offset: 0x00051154
		[DebuggerStepThrough]
		public Task DeleteDataInstanceAsync(Guid dataInstanceId)
		{
			CustomFieldManager.<DeleteDataInstanceAsync>d__6 <DeleteDataInstanceAsync>d__ = new CustomFieldManager.<DeleteDataInstanceAsync>d__6();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.dataInstanceId = dataInstanceId;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldManager.<DeleteDataInstanceAsync>d__6>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00052FA0 File Offset: 0x000511A0
		[DebuggerStepThrough]
		public Task UpdateDataInstanceAsync(CustomDataInstance dataInstance)
		{
			CustomFieldManager.<UpdateDataInstanceAsync>d__7 <UpdateDataInstanceAsync>d__ = new CustomFieldManager.<UpdateDataInstanceAsync>d__7();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.dataInstance = dataInstance;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldManager.<UpdateDataInstanceAsync>d__7>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
