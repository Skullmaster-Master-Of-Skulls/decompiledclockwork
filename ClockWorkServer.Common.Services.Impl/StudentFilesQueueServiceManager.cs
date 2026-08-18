using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Core.Mappers.StudentFiles;
using TechnoPro.Common.Core.StudentFiles;
using TechnoPro.Common.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000090 RID: 144
	public class StudentFilesQueueServiceManager : IStudentFilesQueue, IService
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00018058 File Offset: 0x00016258
		public LoadStudentFilesQueueFileItemsByStudentResp LoadStudentFilesQueueFileItemsByStudent(LoadStudentFilesQueueFileItemsByStudentReq Request)
		{
			IStudentFilesQueueManager studentFilesQueueManager = new StudentFilesQueueManager(Request.GetOperationContext());
			LoadStudentFilesQueueFileItemsByStudentResp loadStudentFilesQueueFileItemsByStudentResp = new LoadStudentFilesQueueFileItemsByStudentResp();
			IList<StudentFilesQueueFileItem> list = studentFilesQueueManager.LoadStudentFilesQueueFileItemsByStudent(Request.PersonId);
			IList<StudentFilesQueueFileItemDTO> items;
			if (list == null)
			{
				items = null;
			}
			else
			{
				items = (from g in list
				select g.ToDTO()).ToList<StudentFilesQueueFileItemDTO>();
			}
			loadStudentFilesQueueFileItemsByStudentResp.Items = items;
			return loadStudentFilesQueueFileItemsByStudentResp;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000180C0 File Offset: 0x000162C0
		public LoadStudentFilesQueueItemsResp LoadStudentFilesQueueItems(LoadStudentFilesQueueItemsReq Request)
		{
			IStudentFilesQueueManager studentFilesQueueManager = new StudentFilesQueueManager(Request.GetOperationContext());
			LoadStudentFilesQueueItemsResp loadStudentFilesQueueItemsResp = new LoadStudentFilesQueueItemsResp();
			IStudentFilesQueueManager studentFilesQueueManager2 = studentFilesQueueManager;
			StudentFilesQueueLoadParametersDTO loadParameters = Request.LoadParameters;
			StudentFilesQueueItems studentFilesQueueItems = studentFilesQueueManager2.LoadStudentFilesQueueItems((loadParameters != null) ? loadParameters.ToDomainObject() : null);
			loadStudentFilesQueueItemsResp.QueueItems = ((studentFilesQueueItems != null) ? studentFilesQueueItems.ToDTO() : null);
			return loadStudentFilesQueueItemsResp;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00018110 File Offset: 0x00016310
		public UpdateStudentFilesQueueStudentItemResp UpdateStudentFilesQueueStudentItem(UpdateStudentFilesQueueStudentItemReq Request)
		{
			IStudentFilesQueueManager studentFilesQueueManager = new StudentFilesQueueManager(Request.GetOperationContext());
			UpdateStudentFilesQueueStudentItemResp updateStudentFilesQueueStudentItemResp = new UpdateStudentFilesQueueStudentItemResp();
			IStudentFilesQueueManager studentFilesQueueManager2 = studentFilesQueueManager;
			int personId = Request.PersonId;
			IList<StudentFilesQueueFileItemDTO> allUpdatedFileItemsForStudent = Request.AllUpdatedFileItemsForStudent;
			IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent2;
			if (allUpdatedFileItemsForStudent == null)
			{
				allUpdatedFileItemsForStudent2 = null;
			}
			else
			{
				allUpdatedFileItemsForStudent2 = (from g in allUpdatedFileItemsForStudent
				select g.ToDomainObject()).ToList<StudentFilesQueueFileItem>();
			}
			IList<StudentFilesQueueFileItem> list = studentFilesQueueManager2.UpdateStudentFilesQueueStudentItem(personId, allUpdatedFileItemsForStudent2);
			IList<StudentFilesQueueFileItemDTO> reloadedFilesItems;
			if (list == null)
			{
				reloadedFilesItems = null;
			}
			else
			{
				reloadedFilesItems = (from g in list
				select g.ToDTO()).ToList<StudentFilesQueueFileItemDTO>();
			}
			updateStudentFilesQueueStudentItemResp.ReloadedFilesItems = reloadedFilesItems;
			return updateStudentFilesQueueStudentItemResp;
		}
	}
}
