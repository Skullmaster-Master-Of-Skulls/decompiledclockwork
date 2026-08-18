using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Workflows;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C92 RID: 3218
	public static class WorkflowAdapter
	{
		// Token: 0x0600430D RID: 17165 RVA: 0x0002440C File Offset: 0x0002260C
		public static bool IsProgressComplete(this ProgressStepDTO progressStep)
		{
			return progressStep != null && progressStep.ProgressStepNumber == progressStep.ProgressStepTotalCount - 1;
		}
	}
}
