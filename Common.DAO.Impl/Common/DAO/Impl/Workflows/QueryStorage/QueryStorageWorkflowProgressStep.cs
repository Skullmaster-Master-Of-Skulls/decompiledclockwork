using System;

namespace TechnoPro.Common.DAO.Impl.Workflows.QueryStorage
{
	// Token: 0x0200001E RID: 30
	internal static class QueryStorageWorkflowProgressStep
	{
		// Token: 0x0400003B RID: 59
		internal const string QS_PROGRESS_STEP_BY_ID = "SELECT ProgressId,WorkflowGroupCode,ProgressTitle,ProgressDescription,ProgressStepNumber,ProgressStepTotalCount FROM WorkflowProgress WHERE ProgressId=@id";
	}
}
