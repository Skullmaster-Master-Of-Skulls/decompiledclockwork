using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.ICore.Workflows
{
	// Token: 0x0200000E RID: 14
	public interface IWorkflowManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600005F RID: 95
		Task<Workflow> LoadWorkflowAsync(eWorkflowType workflowType);
	}
}
