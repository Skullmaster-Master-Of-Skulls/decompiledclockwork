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
	// Token: 0x02000024 RID: 36
	public class WorkflowManager : IWorkflowManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public WorkflowManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006BC2 File Offset: 0x00004DC2
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006BCA File Offset: 0x00004DCA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000137 RID: 311 RVA: 0x00006BD4 File Offset: 0x00004DD4
		[DebuggerStepThrough]
		public Task<Workflow> LoadWorkflowAsync(eWorkflowType workflowType)
		{
			WorkflowManager.<LoadWorkflowAsync>d__5 <LoadWorkflowAsync>d__ = new WorkflowManager.<LoadWorkflowAsync>d__5();
			<LoadWorkflowAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Workflow>.Create();
			<LoadWorkflowAsync>d__.<>4__this = this;
			<LoadWorkflowAsync>d__.workflowType = workflowType;
			<LoadWorkflowAsync>d__.<>1__state = -1;
			<LoadWorkflowAsync>d__.<>t__builder.Start<WorkflowManager.<LoadWorkflowAsync>d__5>(ref <LoadWorkflowAsync>d__);
			return <LoadWorkflowAsync>d__.<>t__builder.Task;
		}
	}
}
