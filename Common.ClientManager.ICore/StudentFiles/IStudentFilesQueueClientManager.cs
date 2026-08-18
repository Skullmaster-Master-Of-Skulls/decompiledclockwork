using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.StudentFiles
{
	// Token: 0x02000013 RID: 19
	public interface IStudentFilesQueueClientManager : IWebService
	{
		// Token: 0x0600007B RID: 123
		Task<StudentFilesQueueItemsDTO> LoadStudentFilesQueueItemsAsync(StudentFilesQueueLoadParametersDTO loadParameters);

		// Token: 0x0600007C RID: 124
		Task<IList<StudentFilesQueueFileItemDTO>> UpdateStudentFilesQueueStudentItemAsync(int pid, IList<StudentFilesQueueFileItemDTO> allUpdatedFileItemsForStudent);

		// Token: 0x0600007D RID: 125
		Task<IList<StudentFilesQueueFileItemDTO>> LoadStudentFilesQueueFileItemsByStudentAsync(int pid);
	}
}
