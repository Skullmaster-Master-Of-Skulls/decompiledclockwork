using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.ICore.StudentFiles
{
	// Token: 0x0200002E RID: 46
	public interface IStudentFilesQueueManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000149 RID: 329
		Task<StudentFilesQueueItems> LoadStudentFilesQueueItemsAsync(StudentFilesQueueLoadParameters loadParameters);

		// Token: 0x0600014A RID: 330
		Task<IList<StudentFilesQueueFileItem>> UpdateStudentFilesQueueStudentItemAsync(int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent);

		// Token: 0x0600014B RID: 331
		Task<IList<StudentFilesQueueFileItem>> LoadStudentFilesQueueFileItemsByStudentAsync(int pid);

		// Token: 0x0600014C RID: 332
		StudentFilesQueueItems LoadStudentFilesQueueItems(StudentFilesQueueLoadParameters loadParameters);

		// Token: 0x0600014D RID: 333
		IList<StudentFilesQueueFileItem> UpdateStudentFilesQueueStudentItem(int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent);

		// Token: 0x0600014E RID: 334
		IList<StudentFilesQueueFileItem> LoadStudentFilesQueueFileItemsByStudent(int pid);
	}
}
