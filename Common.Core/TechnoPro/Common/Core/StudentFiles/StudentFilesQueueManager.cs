using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.DynamicForms.Legacy;
using TechnoPro.Common.DAO.Impl.DynamicForms.Legacy;
using TechnoPro.Common.DAO.Impl.StudentFiles;
using TechnoPro.Common.DAO.StudentFiles;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.StudentFiles
{
	// Token: 0x0200003B RID: 59
	public class StudentFilesQueueManager : IStudentFilesQueueManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000CA66 File Offset: 0x0000AC66
		public StudentFilesQueueManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000CA78 File Offset: 0x0000AC78
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public OperationContext OpContext { get; set; }

		// Token: 0x06000264 RID: 612 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		[DebuggerStepThrough]
		public Task<StudentFilesQueueItems> LoadStudentFilesQueueItemsAsync(StudentFilesQueueLoadParameters loadParameters)
		{
			StudentFilesQueueManager.<LoadStudentFilesQueueItemsAsync>d__5 <LoadStudentFilesQueueItemsAsync>d__ = new StudentFilesQueueManager.<LoadStudentFilesQueueItemsAsync>d__5();
			<LoadStudentFilesQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StudentFilesQueueItems>.Create();
			<LoadStudentFilesQueueItemsAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueItemsAsync>d__.loadParameters = loadParameters;
			<LoadStudentFilesQueueItemsAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueItemsAsync>d__.<>t__builder.Start<StudentFilesQueueManager.<LoadStudentFilesQueueItemsAsync>d__5>(ref <LoadStudentFilesQueueItemsAsync>d__);
			return <LoadStudentFilesQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public StudentFilesQueueItems LoadStudentFilesQueueItems(StudentFilesQueueLoadParameters loadParameters)
		{
			StudentFilesQueueItems studentFilesQueueItems = new StudentFilesQueueItems();
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.STUDENTFILES_FileUploadControlId);
			bool flag = settingValue < 1;
			StudentFilesQueueItems result;
			if (flag)
			{
				result = studentFilesQueueItems;
			}
			else
			{
				IStudentFilesQueueDAO studentFilesQueueDAO = new StudentFilesQueueDAO(this.OpContext);
				studentFilesQueueItems.LookupStatuses = studentFilesQueueDAO.GetStudentFileLookupStatuses(settingValue);
				IList<StudentFilesQueueStudentItem> list = studentFilesQueueDAO.LoadStudentFilesQueueStudentItems(settingValue, loadParameters.StartDate, !loadParameters.ExcludeItemsWithClosedStatuses);
				StudentFilesQueueItems studentFilesQueueItems2 = studentFilesQueueItems;
				IList<StudentFilesQueueStudentItem> studentItems;
				if (!loadParameters.ExcludeItemsWithClosedStatuses)
				{
					studentItems = list;
				}
				else
				{
					IList<StudentFilesQueueStudentItem> list2 = (from g in list
					where g.FileItems.Any((StudentFilesQueueFileItem m) => m.Status == null || m.Status.StatusType != eStudentFileStatusType.Closed)
					select g).ToList<StudentFilesQueueStudentItem>();
					studentItems = list2;
				}
				studentFilesQueueItems2.StudentItems = studentItems;
				result = studentFilesQueueItems;
			}
			return result;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CB94 File Offset: 0x0000AD94
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItem>> UpdateStudentFilesQueueStudentItemAsync(int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent)
		{
			StudentFilesQueueManager.<UpdateStudentFilesQueueStudentItemAsync>d__7 <UpdateStudentFilesQueueStudentItemAsync>d__ = new StudentFilesQueueManager.<UpdateStudentFilesQueueStudentItemAsync>d__7();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItem>>.Create();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>4__this = this;
			<UpdateStudentFilesQueueStudentItemAsync>d__.pid = pid;
			<UpdateStudentFilesQueueStudentItemAsync>d__.allUpdatedFileItemsForStudent = allUpdatedFileItemsForStudent;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>1__state = -1;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Start<StudentFilesQueueManager.<UpdateStudentFilesQueueStudentItemAsync>d__7>(ref <UpdateStudentFilesQueueStudentItemAsync>d__);
			return <UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public IList<StudentFilesQueueFileItem> UpdateStudentFilesQueueStudentItem(int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.STUDENTFILES_FileUploadControlId);
			bool flag = settingValue < 1;
			IList<StudentFilesQueueFileItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IStudentFilesQueueDAO studentFilesQueueDAO = new StudentFilesQueueDAO(this.OpContext);
				IList<StudentFilesQueueFileItem> list = studentFilesQueueDAO.UpdateStudentFilesQueueStudentItem(settingValue, pid, allUpdatedFileItemsForStudent);
				bool flag2;
				if (list == null)
				{
					flag2 = true;
				}
				else
				{
					flag2 = list.Any((StudentFilesQueueFileItem g) => g.Status == null || g.Status.StatusType != eStudentFileStatusType.Closed);
				}
				bool value = flag2;
				ILegacyDynamicFieldSaveLoadDAO legacyDynamicFieldSaveLoadDAO = new LegacyDynamicFieldSaveLoadDAO(this.OpContext);
				legacyDynamicFieldSaveLoadDAO.UpdateStudentFileUploadStatusMarkers(settingValue, new Dictionary<int, bool>
				{
					{
						pid,
						value
					}
				});
				result = list;
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CC94 File Offset: 0x0000AE94
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItem>> LoadStudentFilesQueueFileItemsByStudentAsync(int pid)
		{
			StudentFilesQueueManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__9 <LoadStudentFilesQueueFileItemsByStudentAsync>d__ = new StudentFilesQueueManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__9();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItem>>.Create();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.pid = pid;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Start<StudentFilesQueueManager.<LoadStudentFilesQueueFileItemsByStudentAsync>d__9>(ref <LoadStudentFilesQueueFileItemsByStudentAsync>d__);
			return <LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public IList<StudentFilesQueueFileItem> LoadStudentFilesQueueFileItemsByStudent(int pid)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.STUDENTFILES_FileUploadControlId);
			bool flag = settingValue < 1;
			IList<StudentFilesQueueFileItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IStudentFilesQueueDAO studentFilesQueueDAO = new StudentFilesQueueDAO(this.OpContext);
				result = studentFilesQueueDAO.LoadStudentFilesQueueFileItemsByStudent(settingValue, pid);
			}
			return result;
		}
	}
}
