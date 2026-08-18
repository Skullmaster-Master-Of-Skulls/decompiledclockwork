using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.DAO.Workflows
{
	// Token: 0x02000011 RID: 17
	public interface IWorkflowDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000026 RID: 38
		Task<Workflow> LoadWorkflowAsync(eWorkflowType workflowType);
	}
}
