using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ClientManager.Core.StudentFiles
{
	// Token: 0x02000016 RID: 22
	public class StudentFilesQueueClientManager : IStudentFilesQueueClientManager, IWebService
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004B8A File Offset: 0x00002D8A
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004B8A File Offset: 0x00002D8A
		public OperationContext OpContext
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004B94 File Offset: 0x00002D94
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItemDTO>> LoadStudentFilesQueueFileItemsByStudentAsync(int pid)
		{
			StudentFilesQueueClientManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__3 <LoadStudentFilesQueueFileItemsByStudentAsync>d__ = new StudentFilesQueueClientManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__3();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItemDTO>>.Create();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.pid = pid;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Start<StudentFilesQueueClientManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__3>(ref <LoadStudentFilesQueueFileItemsByStudentAsync>d__);
			return <LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004BE0 File Offset: 0x00002DE0
		[DebuggerStepThrough]
		public Task<StudentFilesQueueItemsDTO> LoadStudentFilesQueueItemsAsync(StudentFilesQueueLoadParametersDTO loadParameters)
		{
			StudentFilesQueueClientManager.<LoadStudentFilesQueueItemsAsync>d__4 <LoadStudentFilesQueueItemsAsync>d__ = new StudentFilesQueueClientManager.<LoadStudentFilesQueueItemsAsync>d__4();
			<LoadStudentFilesQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StudentFilesQueueItemsDTO>.Create();
			<LoadStudentFilesQueueItemsAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueItemsAsync>d__.loadParameters = loadParameters;
			<LoadStudentFilesQueueItemsAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueItemsAsync>d__.<>t__builder.Start<StudentFilesQueueClientManager.<LoadStudentFilesQueueItemsAsync>d__4>(ref <LoadStudentFilesQueueItemsAsync>d__);
			return <LoadStudentFilesQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004C2C File Offset: 0x00002E2C
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItemDTO>> UpdateStudentFilesQueueStudentItemAsync(int pid, IList<StudentFilesQueueFileItemDTO> allUpdatedFileItemsForStudent)
		{
			StudentFilesQueueClientManager.<UpdateStudentFilesQueueStudentItemAsync>d__5 <UpdateStudentFilesQueueStudentItemAsync>d__ = new StudentFilesQueueClientManager.<UpdateStudentFilesQueueStudentItemAsync>d__5();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItemDTO>>.Create();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>4__this = this;
			<UpdateStudentFilesQueueStudentItemAsync>d__.pid = pid;
			<UpdateStudentFilesQueueStudentItemAsync>d__.allUpdatedFileItemsForStudent = allUpdatedFileItemsForStudent;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>1__state = -1;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Start<StudentFilesQueueClientManager.<UpdateStudentFilesQueueStudentItemAsync>d__5>(ref <UpdateStudentFilesQueueStudentItemAsync>d__);
			return <UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Task;
		}
	}
}
