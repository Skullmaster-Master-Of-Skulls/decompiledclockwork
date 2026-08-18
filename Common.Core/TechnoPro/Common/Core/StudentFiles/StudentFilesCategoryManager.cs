using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.DynamicForms.Legacy;
using TechnoPro.Common.DAO.Impl.DynamicForms.Legacy;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.StudentFiles
{
	// Token: 0x0200003A RID: 58
	public class StudentFilesCategoryManager : IStudentFilesCategoryManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public StudentFilesCategoryManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		public OperationContext OpContext { get; set; }

		// Token: 0x0600025A RID: 602 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		private StudentFileCategory[] GetStudentFileCategories()
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			string settingValue = webSettingManager.GetSettingValue<string>(Setting.STUDENTFILES_FilesToShow);
			StudentFileCategory[] array = settingValue.ConvertXmlToStudentFileCategories();
			StudentFileCategory[] result;
			if (array == null)
			{
				result = null;
			}
			else
			{
				result = (from g in array
				where !g.IsDisabled
				select g).ToArray<StudentFileCategory>();
			}
			return result;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C750 File Offset: 0x0000A950
		public StudentFileCategoryFileDescriptionsWithColData[] LoadStudentFileDescriptions(int studentPersonId)
		{
			StudentFileCategory[] studentFileCategories = this.GetStudentFileCategories();
			bool flag = studentFileCategories == null || studentFileCategories.Length < 1;
			StudentFileCategoryFileDescriptionsWithColData[] result;
			if (flag)
			{
				result = new StudentFileCategoryFileDescriptionsWithColData[0];
			}
			else
			{
				IDynamicFileStorageManager dfm = new DynamicFileStorageManager(this.OpContext);
				result = studentFileCategories.Select(delegate(StudentFileCategory fileCategory)
				{
					StudentFileCategoryField[] fields = fileCategory.Fields;
					List<StudentFileCategoryField> list;
					if (fields == null)
					{
						list = null;
					}
					else
					{
						list = (from g in fields
						where g.FieldType == eStudentFileCategoryFieldType.FileListControl && g.FormType == eStudentFileCategoryFormType.PerStudent
						select g).ToList<StudentFileCategoryField>();
					}
					List<StudentFileCategoryField> fields2 = list;
					StudentFileCategoryField[] fields3 = fileCategory.Fields;
					List<StudentFileCategoryField> list2;
					if (fields3 == null)
					{
						list2 = null;
					}
					else
					{
						list2 = (from g in fields3
						where g.FieldType == eStudentFileCategoryFieldType.SingleFileControl && g.FormType == eStudentFileCategoryFormType.PerStudent
						select g).ToList<StudentFileCategoryField>();
					}
					List<StudentFileCategoryField> fields4 = list2;
					IList<DynamicFileDescriptionWithColData> list3 = StudentFilesCategoryManager.LoadFileDescriptions(fields4, studentPersonId, new Func<int, int[], IList<DynamicFileDescriptionWithColData>>(dfm.LoadPerStudentSingleFileDescriptionsByStudentAndControls<DynamicFileDescriptionWithColData>));
					IList<DynamicFileDescriptionWithColData> list4 = StudentFilesCategoryManager.LoadFileDescriptions(fields2, studentPersonId, new Func<int, int[], IList<DynamicFileDescriptionWithColData>>(dfm.LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls));
					List<DynamicFileDescriptionWithColData> list5 = new List<DynamicFileDescriptionWithColData>();
					bool flag2 = list3 != null && list3.Count > 0;
					if (flag2)
					{
						list5.AddRange(list3);
					}
					bool flag3 = list4 != null && list4.Count > 0;
					if (flag3)
					{
						list5.AddRange(list4);
					}
					return new StudentFileCategoryFileDescriptionsWithColData
					{
						StudentFileCategoryTitle = fileCategory.Title,
						FileDescriptions = list5
					};
				}).ToArray<StudentFileCategoryFileDescriptionsWithColData>();
			}
			return result;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		private static IList<DynamicFileDescriptionWithColData> LoadFileDescriptions(List<StudentFileCategoryField> fields, int studentPersonId, Func<int, int[], IList<DynamicFileDescriptionWithColData>> loadFunc)
		{
			List<StudentFileCategoryField> fields2 = fields;
			int[] array;
			if (fields2 == null)
			{
				array = null;
			}
			else
			{
				array = (from g in fields2
				select g.ControlId into h
				where h > 0
				select h).Distinct<int>().ToArray<int>();
			}
			int[] array2 = array ?? new int[0];
			bool flag = array2.Length < 1;
			IList<DynamicFileDescriptionWithColData> result;
			if (flag)
			{
				result = new List<DynamicFileDescriptionWithColData>();
			}
			else
			{
				IList<DynamicFileDescriptionWithColData> source = loadFunc(studentPersonId, array2) ?? new List<DynamicFileDescriptionWithColData>();
				result = source.Where(delegate(DynamicFileDescriptionWithColData item)
				{
					List<StudentFileCategoryField> fields3 = fields;
					StudentFileCategoryField studentFileCategoryField = (fields3 != null) ? fields3.FirstOrDefault((StudentFileCategoryField g) => g.ControlId == item.ControlId) : null;
					string value = (studentFileCategoryField != null) ? studentFileCategoryField.FilenameFilter : null;
					return string.IsNullOrEmpty(value) || (item.Filename ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
				}).ToList<DynamicFileDescriptionWithColData>();
			}
			return result;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C880 File Offset: 0x0000AA80
		[DebuggerStepThrough]
		private static Task<IList<DynamicFileDescriptionWithColData>> LoadFileDescriptionsAsync(List<StudentFileCategoryField> fields, int studentPersonId, Func<int, int[], Task<IList<DynamicFileDescriptionWithColData>>> loadFunc)
		{
			StudentFilesCategoryManager.<LoadFileDescriptionsAsync>d__8 <LoadFileDescriptionsAsync>d__ = new StudentFilesCategoryManager.<LoadFileDescriptionsAsync>d__8();
			<LoadFileDescriptionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicFileDescriptionWithColData>>.Create();
			<LoadFileDescriptionsAsync>d__.fields = fields;
			<LoadFileDescriptionsAsync>d__.studentPersonId = studentPersonId;
			<LoadFileDescriptionsAsync>d__.loadFunc = loadFunc;
			<LoadFileDescriptionsAsync>d__.<>1__state = -1;
			<LoadFileDescriptionsAsync>d__.<>t__builder.Start<StudentFilesCategoryManager.<LoadFileDescriptionsAsync>d__8>(ref <LoadFileDescriptionsAsync>d__);
			return <LoadFileDescriptionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		[DebuggerStepThrough]
		public Task<StudentFileCategoryFileDescriptionsWithColData[]> LoadStudentFileDescriptionsAsync(int studentPersonId)
		{
			StudentFilesCategoryManager.<LoadStudentFileDescriptionsAsync>d__9 <LoadStudentFileDescriptionsAsync>d__ = new StudentFilesCategoryManager.<LoadStudentFileDescriptionsAsync>d__9();
			<LoadStudentFileDescriptionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StudentFileCategoryFileDescriptionsWithColData[]>.Create();
			<LoadStudentFileDescriptionsAsync>d__.<>4__this = this;
			<LoadStudentFileDescriptionsAsync>d__.studentPersonId = studentPersonId;
			<LoadStudentFileDescriptionsAsync>d__.<>1__state = -1;
			<LoadStudentFileDescriptionsAsync>d__.<>t__builder.Start<StudentFilesCategoryManager.<LoadStudentFileDescriptionsAsync>d__9>(ref <LoadStudentFileDescriptionsAsync>d__);
			return <LoadStudentFileDescriptionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C920 File Offset: 0x0000AB20
		[DebuggerStepThrough]
		public Task<int> UploadStudentFileAsync(string StudentComment, BinaryFile File)
		{
			StudentFilesCategoryManager.<UploadStudentFileAsync>d__10 <UploadStudentFileAsync>d__ = new StudentFilesCategoryManager.<UploadStudentFileAsync>d__10();
			<UploadStudentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadStudentFileAsync>d__.<>4__this = this;
			<UploadStudentFileAsync>d__.StudentComment = StudentComment;
			<UploadStudentFileAsync>d__.File = File;
			<UploadStudentFileAsync>d__.<>1__state = -1;
			<UploadStudentFileAsync>d__.<>t__builder.Start<StudentFilesCategoryManager.<UploadStudentFileAsync>d__10>(ref <UploadStudentFileAsync>d__);
			return <UploadStudentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C974 File Offset: 0x0000AB74
		public int UploadStudentFile(string StudentComment, BinaryFile File)
		{
			int whoAmI = this.OpContext.WhoAmI;
			IStudentManagementManager studentManagementManager = new StudentManagementManager(this.OpContext);
			string str = studentManagementManager.LoadStudentNumber(whoAmI);
			string path = (File.FileName ?? "").Trim();
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			string extension = Path.GetExtension(path);
			File.FileName = fileNameWithoutExtension + "_" + str + extension;
			DynamicDataContext dynamicDataContext = new DynamicDataContext
			{
				PrimaryId = whoAmI
			};
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.STUDENTFILES_FileUploadControlId);
			IDynamicFileStorageManager dynamicFileStorageManager = new DynamicFileStorageManager(this.OpContext);
			int result = dynamicFileStorageManager.AddFile(settingValue, dynamicDataContext, eDynamicFormType.PerStudent, "", StudentComment, File, 2000);
			ILegacyDynamicFieldSaveLoadDAO legacyDynamicFieldSaveLoadDAO = new LegacyDynamicFieldSaveLoadDAO(this.OpContext);
			legacyDynamicFieldSaveLoadDAO.UpdateStudentFileUploadStatusMarkers(settingValue, new Dictionary<int, bool>
			{
				{
					dynamicDataContext.PrimaryId,
					true
				}
			});
			return result;
		}
	}
}
