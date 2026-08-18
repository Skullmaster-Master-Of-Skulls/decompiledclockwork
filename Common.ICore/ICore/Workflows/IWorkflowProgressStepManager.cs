using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.ICore.Workflows
{
	// Token: 0x0200000F RID: 15
	public interface IWorkflowProgressStepManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000060 RID: 96
		Task<ProgressStep> LoadProgressStepByIdAsync(Guid progressStepId);
	}
}
