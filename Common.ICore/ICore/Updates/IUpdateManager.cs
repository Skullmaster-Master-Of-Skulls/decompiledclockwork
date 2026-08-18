using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.ICore.Updates
{
	// Token: 0x0200001D RID: 29
	public interface IUpdateManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000B3 RID: 179
		IList<UpdateFileInfo> GetAvailableUpdates();

		// Token: 0x060000B4 RID: 180
		void ApplyUpdates(IList<UpdateFileInfo> updates);

		// Token: 0x060000B5 RID: 181
		IList<UpdateFileInfo> GetOnScheduleUpdates();

		// Token: 0x060000B6 RID: 182
		void CancelOnScheduleUpdates(IList<UpdateFileInfo> updates);

		// Token: 0x060000B7 RID: 183
		IList<UploadUpdateFileResult> UploadUpdateFiles(IList<FileSystemStructure> updFiles);

		// Token: 0x060000B8 RID: 184
		void MoveFilesToUpdatingFolder(string source, IList<CloudBlobInfo> files);

		// Token: 0x060000B9 RID: 185
		void CopyFilesToUpdatingFolder(string source, string destination, IList<CloudBlobInfo> files);

		// Token: 0x060000BA RID: 186
		void MarkUpdateAsPending(string serverFilename, bool isPublicFolder);

		// Token: 0x060000BB RID: 187
		void ForceUpdatingServiceToRun();
	}
}
