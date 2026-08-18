using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.DAO.StudentFiles
{
	// Token: 0x02000028 RID: 40
	public interface IStudentFilesQueueDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000A6 RID: 166
		Task<IList<StudentFilesLookupStatus>> GetStudentFileLookupStatusesAsync(int cid);

		// Token: 0x060000A7 RID: 167
		Task<IList<StudentFilesQueueStudentItem>> LoadStudentFilesQueueStudentItemsAsync(int cid, DateTime startDate, bool loadClosedStudents);

		// Token: 0x060000A8 RID: 168
		Task<IList<StudentFilesQueueFileItem>> UpdateStudentFilesQueueStudentItemAsync(int cid, int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent);

		// Token: 0x060000A9 RID: 169
		Task<IList<StudentFilesQueueFileItem>> LoadStudentFilesQueueFileItemsByStudentAsync(int cid, int pid);

		// Token: 0x060000AA RID: 170
		IList<StudentFilesLookupStatus> GetStudentFileLookupStatuses(int cid);

		// Token: 0x060000AB RID: 171
		IList<StudentFilesQueueStudentItem> LoadStudentFilesQueueStudentItems(int cid, DateTime startDate, bool loadClosedStudents);

		// Token: 0x060000AC RID: 172
		IList<StudentFilesQueueFileItem> UpdateStudentFilesQueueStudentItem(int cid, int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent);

		// Token: 0x060000AD RID: 173
		IList<StudentFilesQueueFileItem> LoadStudentFilesQueueFileItemsByStudent(int cid, int pid);
	}
}
