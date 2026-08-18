using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Workflows;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.DAO.Impl.Workflows
{
	// Token: 0x0200001C RID: 28
	public class WorkflowDAO : IWorkflowDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000599F File Offset: 0x00003B9F
		public WorkflowDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000059B1 File Offset: 0x00003BB1
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000059B9 File Offset: 0x00003BB9
		public OperationContext OpContext { get; set; }

		// Token: 0x060000BB RID: 187 RVA: 0x000059C4 File Offset: 0x00003BC4
		[DebuggerStepThrough]
		public Task<Workflow> LoadWorkflowAsync(eWorkflowType workflowType)
		{
			WorkflowDAO.<LoadWorkflowAsync>d__5 <LoadWorkflowAsync>d__ = new WorkflowDAO.<LoadWorkflowAsync>d__5();
			<LoadWorkflowAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Workflow>.Create();
			<LoadWorkflowAsync>d__.<>4__this = this;
			<LoadWorkflowAsync>d__.workflowType = workflowType;
			<LoadWorkflowAsync>d__.<>1__state = -1;
			<LoadWorkflowAsync>d__.<>t__builder.Start<WorkflowDAO.<LoadWorkflowAsync>d__5>(ref <LoadWorkflowAsync>d__);
			return <LoadWorkflowAsync>d__.<>t__builder.Task;
		}
	}
}
