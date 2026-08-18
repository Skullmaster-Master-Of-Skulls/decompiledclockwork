using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.DAO.Impl.CustomForms
{
	// Token: 0x020000FF RID: 255
	public class CustomFieldDAO : ICustomFieldDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0004B0C1 File Offset: 0x000492C1
		public CustomFieldDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0004B0D3 File Offset: 0x000492D3
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0004B0DB File Offset: 0x000492DB
		public OperationContext OpContext { get; set; }

		// Token: 0x06000749 RID: 1865 RVA: 0x0004B0E4 File Offset: 0x000492E4
		[DebuggerStepThrough]
		public Task<Guid> CreateDataInstanceAsync(CustomDataInstance dataInstance)
		{
			CustomFieldDAO.<CreateDataInstanceAsync>d__5 <CreateDataInstanceAsync>d__ = new CustomFieldDAO.<CreateDataInstanceAsync>d__5();
			<CreateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateDataInstanceAsync>d__.<>4__this = this;
			<CreateDataInstanceAsync>d__.dataInstance = dataInstance;
			<CreateDataInstanceAsync>d__.<>1__state = -1;
			<CreateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldDAO.<CreateDataInstanceAsync>d__5>(ref <CreateDataInstanceAsync>d__);
			return <CreateDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0004B130 File Offset: 0x00049330
		[DebuggerStepThrough]
		public Task DeleteDataInstanceAsync(Guid dataInstanceId)
		{
			CustomFieldDAO.<DeleteDataInstanceAsync>d__6 <DeleteDataInstanceAsync>d__ = new CustomFieldDAO.<DeleteDataInstanceAsync>d__6();
			<DeleteDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteDataInstanceAsync>d__.<>4__this = this;
			<DeleteDataInstanceAsync>d__.dataInstanceId = dataInstanceId;
			<DeleteDataInstanceAsync>d__.<>1__state = -1;
			<DeleteDataInstanceAsync>d__.<>t__builder.Start<CustomFieldDAO.<DeleteDataInstanceAsync>d__6>(ref <DeleteDataInstanceAsync>d__);
			return <DeleteDataInstanceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0004B17C File Offset: 0x0004937C
		[DebuggerStepThrough]
		public Task UpdateDataInstanceAsync(CustomDataInstance dataInstance)
		{
			CustomFieldDAO.<UpdateDataInstanceAsync>d__7 <UpdateDataInstanceAsync>d__ = new CustomFieldDAO.<UpdateDataInstanceAsync>d__7();
			<UpdateDataInstanceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateDataInstanceAsync>d__.<>4__this = this;
			<UpdateDataInstanceAsync>d__.dataInstance = dataInstance;
			<UpdateDataInstanceAsync>d__.<>1__state = -1;
			<UpdateDataInstanceAsync>d__.<>t__builder.Start<CustomFieldDAO.<UpdateDataInstanceAsync>d__7>(ref <UpdateDataInstanceAsync>d__);
			return <UpdateDataInstanceAsync>d__.<>t__builder.Task;
		}
	}
}
