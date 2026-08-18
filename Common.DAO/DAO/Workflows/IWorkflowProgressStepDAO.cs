using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.DAO.Workflows
{
	// Token: 0x02000012 RID: 18
	public interface IWorkflowProgressStepDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000027 RID: 39
		Task<ProgressStep> LoadProgressStepByIdAsync(Guid progressStepId);
	}
}
