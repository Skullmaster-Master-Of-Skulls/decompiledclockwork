using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Workflows;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.DAO.Impl.Workflows
{
	// Token: 0x0200001D RID: 29
	public class WorkflowProgressStepDAO : IWorkflowProgressStepDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00005A0F File Offset: 0x00003C0F
		public WorkflowProgressStepDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005A21 File Offset: 0x00003C21
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00005A29 File Offset: 0x00003C29
		public OperationContext OpContext { get; set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00005A34 File Offset: 0x00003C34
		public static ProgressStep GetProgressStepFromRecord(IDataRecord record)
		{
			bool flag = record == null || record["ProgressStepId"] is DBNull;
			ProgressStep result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eWorkflowType workflowType;
				Enum.TryParse<eWorkflowType>((record["WorkflowGroupCode"] is DBNull) ? string.Empty : ((string)record["WorkflowGroupCode"]), out workflowType);
				result = new ProgressStep
				{
					ProgressStepId = (Guid)record["ProgressStepId"],
					WorkflowType = workflowType,
					Title = ((record["ProgressTitle"] is DBNull) ? string.Empty : ((string)record["ProgressTitle"])),
					Description = ((record["ProgressDescription"] is DBNull) ? string.Empty : ((string)record["ProgressDescription"])),
					ProgressStepNumber = ((record["ProgressStepNumber"] is DBNull) ? 0 : ((int)record["ProgressStepNumber"])),
					ProgressStepTotalCount = ((record["ProgressStepTotalCount"] is DBNull) ? 0 : ((int)record["ProgressStepTotalCount"]))
				};
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005B7C File Offset: 0x00003D7C
		[DebuggerStepThrough]
		public Task<ProgressStep> LoadProgressStepByIdAsync(Guid progressStepId)
		{
			WorkflowProgressStepDAO.<LoadProgressStepByIdAsync>d__6 <LoadProgressStepByIdAsync>d__ = new WorkflowProgressStepDAO.<LoadProgressStepByIdAsync>d__6();
			<LoadProgressStepByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProgressStep>.Create();
			<LoadProgressStepByIdAsync>d__.<>4__this = this;
			<LoadProgressStepByIdAsync>d__.progressStepId = progressStepId;
			<LoadProgressStepByIdAsync>d__.<>1__state = -1;
			<LoadProgressStepByIdAsync>d__.<>t__builder.Start<WorkflowProgressStepDAO.<LoadProgressStepByIdAsync>d__6>(ref <LoadProgressStepByIdAsync>d__);
			return <LoadProgressStepByIdAsync>d__.<>t__builder.Task;
		}
	}
}
