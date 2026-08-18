using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.ICore.Workflows;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.Core.Workflows
{
	// Token: 0x02000025 RID: 37
	public class WorkflowProgressStepManager : IWorkflowProgressStepManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00006C1F File Offset: 0x00004E1F
		public WorkflowProgressStepManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006C31 File Offset: 0x00004E31
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006C39 File Offset: 0x00004E39
		public OperationContext OpContext { get; set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00006C44 File Offset: 0x00004E44
		[DebuggerStepThrough]
		public Task<ProgressStep> LoadProgressStepByIdAsync(Guid progressStepId)
		{
			WorkflowProgressStepManager.<LoadProgressStepByIdAsync>d__5 <LoadProgressStepByIdAsync>d__ = new WorkflowProgressStepManager.<LoadProgressStepByIdAsync>d__5();
			<LoadProgressStepByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProgressStep>.Create();
			<LoadProgressStepByIdAsync>d__.<>4__this = this;
			<LoadProgressStepByIdAsync>d__.progressStepId = progressStepId;
			<LoadProgressStepByIdAsync>d__.<>1__state = -1;
			<LoadProgressStepByIdAsync>d__.<>t__builder.Start<WorkflowProgressStepManager.<LoadProgressStepByIdAsync>d__5>(ref <LoadProgressStepByIdAsync>d__);
			return <LoadProgressStepByIdAsync>d__.<>t__builder.Task;
		}
	}
}
