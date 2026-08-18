using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.DAO.Updates
{
	// Token: 0x0200001D RID: 29
	public interface IUpdateDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000058 RID: 88
		IList<UpdateFileInfo> GetAvailableUpdates(eUpdateFolderAccess updFolderAccess);

		// Token: 0x06000059 RID: 89
		void ApplyUpdate(IList<UpdateFileInfo> updates);

		// Token: 0x0600005A RID: 90
		IList<UpdateFileInfo> GetOnScheduleUpdates();

		// Token: 0x0600005B RID: 91
		void CancelOnScheduleUpdates(IList<UpdateFileInfo> updates);

		// Token: 0x0600005C RID: 92
		UpdateStatus GetExecutionStatus(string fileType, int addSize, bool isPublic);

		// Token: 0x0600005D RID: 93
		void SaveExecutionStatus(UpdateStatus updateStatus);

		// Token: 0x0600005E RID: 94
		IList<UpdateStatus> GetExecutionStatus();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		string UpdatesPrivatePath { get; set; }

		// Token: 0x06000061 RID: 97
		string GetLegacyPrivateFolderPath();
	}
}
