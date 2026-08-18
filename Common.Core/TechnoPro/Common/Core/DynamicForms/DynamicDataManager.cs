using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FB RID: 251
	public class DynamicDataManager : IDynamicDataManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0003FB44 File Offset: 0x0003DD44
		private IOldUserSettingManager oldUserSettingManager
		{
			get
			{
				bool flag = this._oldUserSettingManager == null;
				if (flag)
				{
					this._oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				}
				return this._oldUserSettingManager;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0003FB7C File Offset: 0x0003DD7C
		private DynamicFieldManager dynamicFieldManager
		{
			get
			{
				bool flag = this._dynamicFieldManager == null;
				if (flag)
				{
					this._dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				}
				return this._dynamicFieldManager;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		public OperationContext OpContext { get; set; }

		// Token: 0x060009E7 RID: 2535 RVA: 0x0003FBC5 File Offset: 0x0003DDC5
		public DynamicDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DynamicDataDAO(opContext);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003FBE4 File Offset: 0x0003DDE4
		private IList<IDynamicDataSerializableItem> ConvertDynamicDataStorageItemsToDataItems(IList<DynamicDataStorageItem> items)
		{
			bool flag = items == null;
			IList<IDynamicDataSerializableItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<IDynamicDataSerializableItem> list = new List<IDynamicDataSerializableItem>();
				foreach (DynamicDataStorageItem dynamicDataStorageItem in items)
				{
					DynamicField field = dynamicDataStorageItem.Field;
					DynamicControlAttribute attribute = field.ControlCode.GetAttribute();
					bool flag2 = attribute != null;
					if (flag2)
					{
						string dynamicDataItemClass = attribute.DynamicDataItemClass;
						bool flag3 = !string.IsNullOrEmpty(dynamicDataItemClass);
						if (flag3)
						{
							Type type = Type.GetType(dynamicDataItemClass);
							IDynamicDataSerializableItem dynamicDataSerializableItem = (IDynamicDataSerializableItem)Activator.CreateInstance(type);
							dynamicDataSerializableItem.ReadFromStorage(dynamicDataStorageItem);
							dynamicDataSerializableItem.Field = field;
							list.Add(dynamicDataSerializableItem);
						}
						else
						{
							CWLogger.Logger.Error("Common.Core.DynamicForms.DynamicDataManager:Class is null: item.Field.ControlId={0}", dynamicDataStorageItem.Field.ControlId.ToString());
						}
					}
					else
					{
						CWLogger.Logger.Error("Common.Core.DynamicForms.DynamicDataManager:Attr is null: item.Field.ControlId={0}", dynamicDataStorageItem.Field.ControlId.ToString());
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003FD0C File Offset: 0x0003DF0C
		public static List<string[]> DecodeDocumentsList(string list)
		{
			List<string[]> list2 = new List<string[]>();
			bool flag = string.IsNullOrEmpty(list);
			List<string[]> result;
			if (flag)
			{
				result = list2;
			}
			else
			{
				string[] array = list.Split(new char[]
				{
					'\t'
				});
				string[] array2 = new string[0];
				foreach (string text in array)
				{
					string[] array4 = text.Split(new char[1]);
					array2 = new string[array4.Length];
					for (int j = 0; j < array4.Length; j++)
					{
						array2[j] = array4[j];
					}
					list2.Add(array2);
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0003FDB8 File Offset: 0x0003DFB8
		public static string EncodeDocumentsList(List<string[]> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string[] array in items)
			{
				bool flag = array != null && array.Length != 0;
				if (flag)
				{
					string value = string.Join('\0'.ToString(), array);
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append('\t');
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0003FE5C File Offset: 0x0003E05C
		public List<DynamicData> LoadData(DynamicDataContext Context, DynamicForm Form)
		{
			return this.dao.LoadData(Context, Form.ScreenNum, Form.FormType);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003FE88 File Offset: 0x0003E088
		public List<DynamicData> LoadData(DynamicDataContext Context, int screenNum, eDynamicFormType formType)
		{
			return this.dao.LoadData(Context, screenNum, formType);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003FEA8 File Offset: 0x0003E0A8
		[DebuggerStepThrough]
		public Task<List<DynamicData>> LoadDataAsync(DynamicDataContext Context, int screenNum, eDynamicFormType formType)
		{
			DynamicDataManager.<LoadDataAsync>d__17 <LoadDataAsync>d__ = new DynamicDataManager.<LoadDataAsync>d__17();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicData>>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.Context = Context;
			<LoadDataAsync>d__.screenNum = screenNum;
			<LoadDataAsync>d__.formType = formType;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadDataAsync>d__17>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003FF04 File Offset: 0x0003E104
		public List<DynamicDataSet> LoadPerStudentDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds)
		{
			return this.dao.LoadPerStudentDataForMultipleStudents(PersonIds, ControlIds);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0003FF24 File Offset: 0x0003E124
		public IList<int> FindPerAppointmentExistingDataForAnyAppointment(int pid, IList<int> controlIds)
		{
			return this.dao.FindPerAppointmentExistingDataForAnyAppointment(pid, controlIds);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003FF44 File Offset: 0x0003E144
		public List<DynamicData> LoadDataByFields(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType)
		{
			return this.dao.LoadDataByFields(Context, ControlIds, DataType);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003FF64 File Offset: 0x0003E164
		[DebuggerStepThrough]
		public Task<List<DynamicData>> LoadDataByFieldsAsync(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType)
		{
			DynamicDataManager.<LoadDataByFieldsAsync>d__21 <LoadDataByFieldsAsync>d__ = new DynamicDataManager.<LoadDataByFieldsAsync>d__21();
			<LoadDataByFieldsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicData>>.Create();
			<LoadDataByFieldsAsync>d__.<>4__this = this;
			<LoadDataByFieldsAsync>d__.Context = Context;
			<LoadDataByFieldsAsync>d__.ControlIds = ControlIds;
			<LoadDataByFieldsAsync>d__.DataType = DataType;
			<LoadDataByFieldsAsync>d__.<>1__state = -1;
			<LoadDataByFieldsAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadDataByFieldsAsync>d__21>(ref <LoadDataByFieldsAsync>d__);
			return <LoadDataByFieldsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003FFC0 File Offset: 0x0003E1C0
		public DynamicData LoadEmail(int PersonId)
		{
			DynamicField emailField = this.dynamicFieldManager.GetEmailField();
			bool flag = emailField != null;
			DynamicData result;
			if (flag)
			{
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = PersonId
				};
				List<DynamicField> list = new List<DynamicField>();
				list.Add(emailField);
				List<DynamicData> list2 = this.dao.LoadDataByFields(context, list.ConvertAll<int>((DynamicField f) => f.ControlId), eDynamicFormType.PerStudent);
				result = ((list2 == null || list2.Count < 1) ? null : list2[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00040058 File Offset: 0x0003E258
		public int StoreFileInDocuments(string Title, string Notes, BinaryFile File, int StudentPersonId, int fileTypeCode = 1000)
		{
			return this.StoreFileInDocuments(Title, Notes, File, StudentPersonId, 0, fileTypeCode);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00040078 File Offset: 0x0003E278
		public int StoreFileInDocuments(string Title, string Notes, BinaryFile File, int StudentPersonId, int cid, int fileTypeCode = 1000)
		{
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			int num = (cid > 0) ? cid : oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DocumentsControlId);
			bool flag = num > 0;
			int result;
			if (flag)
			{
				List<int> controlIds = new List<int>
				{
					num
				};
				List<DynamicField> list = this.dynamicFieldManager.LoadFieldsByControlIds(controlIds);
				DynamicField field = list[0];
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = StudentPersonId
				};
				List<int> controlIds2 = new List<int>
				{
					num
				};
				List<DynamicData> list2 = this.LoadDataByFields(context, controlIds2, eDynamicFormType.PerStudent);
				string text = (list2 != null && list2.Count > 0 && list2[0].Value != null && list2[0].Value is string) ? ((string)list2[0].Value) : "";
				List<string[]> list3 = DynamicDataManager.DecodeDocumentsList(text);
				int num2 = this.UploadDocumentToDatabase(File, fileTypeCode);
				list3.Add(new string[]
				{
					Title,
					Notes ?? "",
					DateTime.Now.ToString("yyyy-MM-dd"),
					Path.GetFileName(File.FileName) + ":" + num2.ToString()
				});
				text = DynamicDataManager.EncodeDocumentsList(list3);
				List<DynamicData> list4 = new List<DynamicData>();
				DynamicData item = new DynamicData
				{
					Field = field,
					Value = text
				};
				list4.Add(item);
				this.SaveData(context, list4, eDynamicFormType.PerStudent);
				result = num2;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00040214 File Offset: 0x0003E414
		public int UploadDocumentToDatabase(BinaryFile File, int fileTypeCode = 1000)
		{
			return this.dao.UploadDocumentToDatabase(File, fileTypeCode);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00040234 File Offset: 0x0003E434
		[DebuggerStepThrough]
		public Task<int> UploadDocumentToDatabaseAsync(BinaryFile File, int fileTypeCode = 1000)
		{
			DynamicDataManager.<UploadDocumentToDatabaseAsync>d__26 <UploadDocumentToDatabaseAsync>d__ = new DynamicDataManager.<UploadDocumentToDatabaseAsync>d__26();
			<UploadDocumentToDatabaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadDocumentToDatabaseAsync>d__.<>4__this = this;
			<UploadDocumentToDatabaseAsync>d__.File = File;
			<UploadDocumentToDatabaseAsync>d__.fileTypeCode = fileTypeCode;
			<UploadDocumentToDatabaseAsync>d__.<>1__state = -1;
			<UploadDocumentToDatabaseAsync>d__.<>t__builder.Start<DynamicDataManager.<UploadDocumentToDatabaseAsync>d__26>(ref <UploadDocumentToDatabaseAsync>d__);
			return <UploadDocumentToDatabaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00040286 File Offset: 0x0003E486
		public void SaveData(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType)
		{
			this.dao.SaveData(context, data, DataType);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00040298 File Offset: 0x0003E498
		[DebuggerStepThrough]
		public Task SaveDataAsync(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType)
		{
			DynamicDataManager.<SaveDataAsync>d__28 <SaveDataAsync>d__ = new DynamicDataManager.<SaveDataAsync>d__28();
			<SaveDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDataAsync>d__.<>4__this = this;
			<SaveDataAsync>d__.context = context;
			<SaveDataAsync>d__.data = data;
			<SaveDataAsync>d__.DataType = DataType;
			<SaveDataAsync>d__.<>1__state = -1;
			<SaveDataAsync>d__.<>t__builder.Start<DynamicDataManager.<SaveDataAsync>d__28>(ref <SaveDataAsync>d__);
			return <SaveDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000402F4 File Offset: 0x0003E4F4
		public IList<PersonBase> LoadStudentByDataItem(eDynamicFormType FormType, DynamicField Field, object Value)
		{
			return this.dao.LoadStudentByDataItem(FormType, Field, Value);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00040314 File Offset: 0x0003E514
		public IList<PersonBase> LoadUniqueStudentsWithPerStudentDataEnteredByForm(int ScreenNum)
		{
			return this.dao.LoadUniqueStudentsWithPerStudentDataEnteredByForm(ScreenNum);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00040334 File Offset: 0x0003E534
		public int CopyDataFromPerStudentToPerDateForm(int ScreenNumPerStudentData, int ScreenNumPerDateData)
		{
			IDynamicPerDateDataDAO dynamicPerDateDataDAO = new DynamicPerDateDataDAO(this.OpContext);
			List<DynamicField> list = this.dynamicFieldManager.LoadFields(new DynamicForm
			{
				ScreenNum = ScreenNumPerStudentData
			});
			IList<PersonBase> list2 = this.LoadUniqueStudentsWithPerStudentDataEnteredByForm(ScreenNumPerStudentData);
			PersonBase whoEntered = new PersonBase
			{
				PersonId = this.OpContext.WhoAmI
			};
			int num = 0;
			foreach (PersonBase personBase in list2)
			{
				int num2 = dynamicPerDateDataDAO.CreatePerDateEntry(new PerDateEntry
				{
					Student = personBase,
					DateEntered = DateTime.Now,
					ScreenNum = ScreenNumPerDateData,
					WhoEntered = whoEntered,
					Description = "Automatic entry"
				});
				bool flag = num2 > 0;
				if (flag)
				{
					this.dao.CopyAllFormDataFromPerStudentToPerDate(personBase.PersonId, ScreenNumPerDateData, num2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00040438 File Offset: 0x0003E638
		public void MergeAllData(int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeAllData(PersonIdNew, PersonIdOld);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0004044C File Offset: 0x0003E64C
		public IList<DynamicDataSet> LoadInstructorFormDataForMultipleExams(IList<int> examIds, IList<int> controlIds)
		{
			return this.dao.LoadInstructorFormDataForMultipleExams(examIds, controlIds);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0004046C File Offset: 0x0003E66C
		public void SaveDataBase(DynamicDataContext context, List<DynamicDataBase> data, eDynamicFormType DataType)
		{
			List<int> controlIds = data.ConvertAll<int>((DynamicDataBase f) => f.ControlId);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			List<DynamicField> fields = dynamicFieldManager.LoadFieldsByControlIds(controlIds);
			List<DynamicData> list = new List<DynamicData>(data.Count);
			list.AddRange(from d in data
			select new DynamicData
			{
				Id = d.Id,
				DataId = d.DataId,
				Field = fields.Find((DynamicField f) => f.ControlId == d.ControlId),
				Value = d.Value,
				ValueId = d.ValueId
			});
			this.dao.SaveData(context, list, DataType);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000404F4 File Offset: 0x0003E6F4
		public bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext context, int ScreenNum, eDynamicFormType FormType)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			List<DynamicField> list = dynamicFieldManager.LoadFields(new DynamicForm
			{
				ScreenNum = ScreenNum
			});
			List<int> cids = list.ConvertAll<int>((DynamicField f) => f.ControlId);
			return this.DoesAtLeastOneSavedDataItemExistByControlIds(context, cids, FormType);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00040558 File Offset: 0x0003E758
		public bool DoesAtLeastOneSavedDataItemExistByControlIds(DynamicDataContext context, IList<int> cids, eDynamicFormType FormType)
		{
			return this.dao.DoesAtLeastOneSavedDataItemExist(context, cids, FormType);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00040578 File Offset: 0x0003E778
		public IList<DynamicDataSet> LoadData(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType)
		{
			IList<DynamicDataSet> dataSets = this.dao.LoadData(PrimaryId, SecondaryIds, ScreenNums, ScreensType);
			return this.RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettings(dataSets);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000405A4 File Offset: 0x0003E7A4
		[DebuggerStepThrough]
		public Task<IList<DynamicDataSet>> LoadDataAsync(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType)
		{
			DynamicDataManager.<LoadDataAsync>d__38 <LoadDataAsync>d__ = new DynamicDataManager.<LoadDataAsync>d__38();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicDataSet>>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.PrimaryId = PrimaryId;
			<LoadDataAsync>d__.SecondaryIds = SecondaryIds;
			<LoadDataAsync>d__.ScreenNums = ScreenNums;
			<LoadDataAsync>d__.ScreensType = ScreensType;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadDataAsync>d__38>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00040608 File Offset: 0x0003E808
		private IList<DynamicDataSet> RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettings(IList<DynamicDataSet> dataSets)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> hiddenCids = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_HiddenCids).ToList<int>();
			List<int> list = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_ScreenNumsToHide).ToList<int>();
			bool flag = list.Count > 0;
			if (flag)
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				List<int> collection = dynamicFieldManager.LoadControlIdsOnForms(false, list.ToArray()).ToList<int>();
				hiddenCids.AddRange(collection);
			}
			bool flag2 = hiddenCids.Count > 0;
			if (flag2)
			{
				List<int> overrideHiddenCids = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OverrideVisibleCids).ToList<int>();
				hiddenCids = (from g in hiddenCids
				where !overrideHiddenCids.Contains(g)
				select g).ToList<int>();
				bool flag3 = hiddenCids.Count > 0;
				if (flag3)
				{
					Func<DynamicData, bool> <>9__2;
					dataSets = dataSets.Select(delegate(DynamicDataSet g)
					{
						IEnumerable<DynamicData> data = g.Data;
						Func<DynamicData, bool> predicate;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = ((DynamicData h) => h.Field == null || !hiddenCids.Contains(h.Field.ControlId)));
						}
						g.Data = data.Where(predicate).ToList<DynamicData>();
						return g;
					}).ToList<DynamicDataSet>();
				}
			}
			return dataSets;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0004073C File Offset: 0x0003E93C
		[DebuggerStepThrough]
		private Task<IList<DynamicDataSet>> RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync(IList<DynamicDataSet> dataSets)
		{
			DynamicDataManager.<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__40 <RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__ = new DynamicDataManager.<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__40();
			<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicDataSet>>.Create();
			<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.<>4__this = this;
			<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.dataSets = dataSets;
			<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.<>1__state = -1;
			<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.<>t__builder.Start<DynamicDataManager.<RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__40>(ref <RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__);
			return <RemoveDataWithControlIdsOnHiddenCidsOrHiddenFormsSettingsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00040787 File Offset: 0x0003E987
		public void DeleteDataItem(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown)
		{
			this.dao.DeleteDataItem(context, ControlId, eControlCode, DataType, location);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000407A0 File Offset: 0x0003E9A0
		[DebuggerStepThrough]
		public Task<int> StoreFileInDocumentsAsync(string Title, string Notes, BinaryFile File, DynamicDataContext context, eDynamicFormType DataType, int cid, int fileTypeCode = 1000)
		{
			DynamicDataManager.<StoreFileInDocumentsAsync>d__42 <StoreFileInDocumentsAsync>d__ = new DynamicDataManager.<StoreFileInDocumentsAsync>d__42();
			<StoreFileInDocumentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<StoreFileInDocumentsAsync>d__.<>4__this = this;
			<StoreFileInDocumentsAsync>d__.Title = Title;
			<StoreFileInDocumentsAsync>d__.Notes = Notes;
			<StoreFileInDocumentsAsync>d__.File = File;
			<StoreFileInDocumentsAsync>d__.context = context;
			<StoreFileInDocumentsAsync>d__.DataType = DataType;
			<StoreFileInDocumentsAsync>d__.cid = cid;
			<StoreFileInDocumentsAsync>d__.fileTypeCode = fileTypeCode;
			<StoreFileInDocumentsAsync>d__.<>1__state = -1;
			<StoreFileInDocumentsAsync>d__.<>t__builder.Start<DynamicDataManager.<StoreFileInDocumentsAsync>d__42>(ref <StoreFileInDocumentsAsync>d__);
			return <StoreFileInDocumentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0004081C File Offset: 0x0003EA1C
		public int StoreFileInDocuments(string Title, string Notes, BinaryFile File, DynamicDataContext context, eDynamicFormType DataType, int cid, int fileTypeCode = 1000)
		{
			bool flag = cid < 1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				List<int> controlIds = new List<int>
				{
					cid
				};
				List<DynamicField> list = this.dynamicFieldManager.LoadFieldsByControlIds(controlIds);
				DynamicField dynamicField = list[0];
				int setting = dynamicField.Setting1;
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				List<DynamicListItem> list2 = (setting > 0) ? dynamicFieldManager.LoadListItems(setting) : null;
				List<int> controlIds2 = new List<int>
				{
					cid
				};
				List<DynamicData> list3 = this.LoadDataByFields(context, controlIds2, DataType);
				string text = (list3 != null && list3.Count > 0 && list3[0].Value != null && list3[0].Value is string) ? ((string)list3[0].Value) : "";
				List<string[]> list4 = DynamicDataManager.DecodeDocumentsList(text);
				int num = this.UploadDocumentToDatabase(File, fileTypeCode);
				int num2 = (list2 == null || list2.Count < 1) ? 4 : (list2.Count + 2);
				string[] array = new string[num2];
				array[0] = Title;
				array[1] = (Notes ?? "");
				for (int i = 2; i < num2 - 2; i++)
				{
					array[i] = "";
				}
				array[num2 - 2] = DateTime.Now.ToString("yyyy-MM-dd");
				array[num2 - 1] = Path.GetFileName(File.FileName) + ":" + num.ToString();
				list4.Add(array);
				text = DynamicDataManager.EncodeDocumentsList(list4);
				List<DynamicData> list5 = new List<DynamicData>();
				DynamicData item = new DynamicData
				{
					Field = dynamicField,
					Value = text
				};
				list5.Add(item);
				this.SaveData(context, list5, DataType);
				result = num;
			}
			return result;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000409F0 File Offset: 0x0003EBF0
		public void AddRowToDynamicTableControl(DynamicDataContext context, eDynamicFormType DataType, int cid, params string[] columnValues)
		{
			bool flag = cid < 1;
			if (!flag)
			{
				List<int> list = new List<int>();
				list.Add(cid);
				List<DynamicField> list2 = this.dynamicFieldManager.LoadFieldsByControlIds(list);
				DynamicField dynamicField = list2[0];
				int setting = dynamicField.Setting1;
				List<DynamicListItem> list3 = this.dynamicFieldManager.LoadListItems(setting);
				int num = list3.Count + 1;
				List<int> controlIds = new List<int>
				{
					cid
				};
				List<DynamicData> list4 = this.LoadDataByFields(context, controlIds, DataType);
				string text = (list4 != null && list4.Count > 0 && list4[0].Value != null && list4[0].Value is string) ? ((string)list4[0].Value) : "";
				List<string[]> list5 = DynamicDataManager.DecodeDocumentsList(text);
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ((i < columnValues.Length) ? columnValues[i] : DateTime.Now.ToString("yyyy-MM-dd"));
					bool flag2 = i < columnValues.Length;
					if (flag2)
					{
						array[i] = columnValues[i];
					}
					else
					{
						bool flag3 = i == num - 1;
						if (flag3)
						{
							array[i] = DateTime.Now.ToString("yyyy-MM-dd");
						}
					}
				}
				list5.Add(array);
				text = DynamicDataManager.EncodeDocumentsList(list5);
				List<DynamicData> list6 = new List<DynamicData>();
				DynamicData item = new DynamicData
				{
					Field = dynamicField,
					Value = text
				};
				list6.Add(item);
				this.SaveData(context, list6, DataType);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00040B90 File Offset: 0x0003ED90
		public IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate)
		{
			return this.dao.UpdateIconForPerAppointmentDataChange(ScreenNum, IconId, StudentPersonId, ControlIdToActivate);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00040BB4 File Offset: 0x0003EDB4
		private DataTable GetTableWithBasicPeopleInfo(IList<int> pids)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			IList<PersonBase> list = peopleManager.LoadPersonsByIds(pids);
			DataTable dataTable = new DataTable("people");
			dataTable.Columns.Add("personid", typeof(int));
			foreach (string columnName in new string[]
			{
				"firstname",
				"middlename",
				"lastname",
				"student_no"
			})
			{
				dataTable.Columns.Add(columnName);
			}
			foreach (PersonBase personBase in list)
			{
				dataTable.Rows.Add(new object[]
				{
					personBase.PersonId,
					personBase.FirstName ?? "",
					personBase.MiddleName ?? "",
					personBase.LastName ?? "",
					personBase.Student_no ?? ""
				});
			}
			return dataTable;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00040CFC File Offset: 0x0003EEFC
		public DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			DataTable tableWithBasicPeopleInfo = this.GetTableWithBasicPeopleInfo(PersonIds);
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
			return dynamicDataForReportsManager.CrossReferencePerStudentData(tableWithBasicPeopleInfo, ControlIds);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00040D2C File Offset: 0x0003EF2C
		public DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			DataTable tableWithBasicPeopleInfo = this.GetTableWithBasicPeopleInfo(PersonIds);
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
			return dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOnly(tableWithBasicPeopleInfo, ControlIds);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00040D5C File Offset: 0x0003EF5C
		public IList<IDynamicDataSerializableItem> LoadDynamicDataItemsByForm(DynamicDataContext Context, int FormNum, eDynamicFormType FormType)
		{
			IList<DynamicDataStorageItem> items = this.dao.LoadDynamicDataStorageItemsByForm(Context, FormNum, FormType);
			return this.ConvertDynamicDataStorageItemsToDataItems(items);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00040D84 File Offset: 0x0003EF84
		public IList<IDynamicDataSerializableItem> LoadDynamicDataItemsByControlIds(DynamicDataContext Context, IList<int> ControlIds, eDynamicFormType FormType)
		{
			IList<DynamicDataStorageItem> items = this.dao.LoadDynamicDataItemsByControlIds(Context, ControlIds, FormType);
			return this.ConvertDynamicDataStorageItemsToDataItems(items);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00040DAC File Offset: 0x0003EFAC
		public void SaveDynamicDataItems(DynamicDataContext Context, IList<IDynamicDataSerializableItem> Items, eDynamicFormType FormType)
		{
			IEnumerable<IDynamicDataSerializableItem> enumerable = from g in Items
			where g is DynamicDataItemFileList
			select g;
			foreach (IDynamicDataSerializableItem dynamicDataSerializableItem in enumerable)
			{
				DynamicDataItemFileList dynamicDataItemFileList = (DynamicDataItemFileList)dynamicDataSerializableItem;
			}
			List<DynamicDataStorageItem> storageItems = Items.ToList<IDynamicDataSerializableItem>().ConvertAll<DynamicDataStorageItem>((IDynamicDataSerializableItem g) => g.WriteToStorage());
			this.dao.SaveDynamicDataStorageItems(Context, storageItems, FormType);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00040E58 File Offset: 0x0003F058
		public BinaryFile LoadFileFromDocuments(int StudentPersonId, int FileId)
		{
			return this.dao.LoadFileFromDocuments(StudentPersonId, FileId);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00040E78 File Offset: 0x0003F078
		public IList<BasicPerson> LoadAssignedAdvisors(eDynamicFormType formType, int studentPersonId, int[] cids)
		{
			if (formType != eDynamicFormType.PerStudent)
			{
				throw new Exception(string.Format("DynamicDataManager:LoadAssignedAdvisors:FormType not supported:formType={0}", formType));
			}
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			return dynamicDataDAO.LoadAssignedAdvisorsFromPerStudentForm(studentPersonId, cids);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00040EC0 File Offset: 0x0003F0C0
		[DebuggerStepThrough]
		public Task<BinaryFile> LoadFileFromDocumentsAsync(int StudentPersonId, int FileId)
		{
			DynamicDataManager.<LoadFileFromDocumentsAsync>d__54 <LoadFileFromDocumentsAsync>d__ = new DynamicDataManager.<LoadFileFromDocumentsAsync>d__54();
			<LoadFileFromDocumentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<LoadFileFromDocumentsAsync>d__.<>4__this = this;
			<LoadFileFromDocumentsAsync>d__.StudentPersonId = StudentPersonId;
			<LoadFileFromDocumentsAsync>d__.FileId = FileId;
			<LoadFileFromDocumentsAsync>d__.<>1__state = -1;
			<LoadFileFromDocumentsAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadFileFromDocumentsAsync>d__54>(ref <LoadFileFromDocumentsAsync>d__);
			return <LoadFileFromDocumentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00040F14 File Offset: 0x0003F114
		[DebuggerStepThrough]
		public Task<BinaryFile> LoadFileFromImageInfoAsync(int DataId, int ControlId, string databaseTablePostFix)
		{
			DynamicDataManager.<LoadFileFromImageInfoAsync>d__55 <LoadFileFromImageInfoAsync>d__ = new DynamicDataManager.<LoadFileFromImageInfoAsync>d__55();
			<LoadFileFromImageInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<LoadFileFromImageInfoAsync>d__.<>4__this = this;
			<LoadFileFromImageInfoAsync>d__.DataId = DataId;
			<LoadFileFromImageInfoAsync>d__.ControlId = ControlId;
			<LoadFileFromImageInfoAsync>d__.databaseTablePostFix = databaseTablePostFix;
			<LoadFileFromImageInfoAsync>d__.<>1__state = -1;
			<LoadFileFromImageInfoAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadFileFromImageInfoAsync>d__55>(ref <LoadFileFromImageInfoAsync>d__);
			return <LoadFileFromImageInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00040F70 File Offset: 0x0003F170
		public BinaryFile LoadFileFromImageInfo(int DataId, int ControlId, string databaseTablePostFix)
		{
			bool flag = !string.IsNullOrWhiteSpace(databaseTablePostFix);
			BinaryFile result;
			if (flag)
			{
				result = this.dao.LoadFileFromImageInfo("imageinfo" + databaseTablePostFix, DataId);
			}
			else
			{
				IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
				IList<int> list = (ControlId > 0) ? dynamicFormManager.FindScreensAControlExistsOn(ControlId) : null;
				bool flag2 = list == null || list.Count < 1;
				if (flag2)
				{
					CWLogger.Logger.Warn("DynamicDataManager:LoadFileFromImageInfo:Can't find any forms for controlid={0}", ControlId.ToString());
					result = null;
				}
				else
				{
					int num = list[0];
					string key = string.Format("FormWithTablePostFix_{0}", num);
					ICacheStorageManager cacheManager = CacheStorageManager.GetCacheManager(null);
					string text = cacheManager[key] as string;
					bool flag3 = string.IsNullOrWhiteSpace(text);
					if (flag3)
					{
						DynamicForm dynamicForm = dynamicFormManager.LoadDynamicFormById(list[0]);
						string text2;
						if (dynamicForm == null)
						{
							text2 = null;
						}
						else
						{
							DynamicFormTypeAttribute attribute = dynamicForm.FormType.GetAttribute<DynamicFormTypeAttribute>();
							text2 = ((attribute != null) ? attribute.TablePostFix : null);
						}
						text = text2;
						cacheManager.Insert(key, text, TimeSpan.FromHours(9.0));
					}
					bool flag4 = string.IsNullOrWhiteSpace(text);
					if (flag4)
					{
						CWLogger.Logger.Warn("DynamicDataManager:LoadFileFromImageInfo:Can't find any forms for controlid={0}", ControlId.ToString());
						result = null;
					}
					else
					{
						string imageInfoTableName = "imageinfo" + text;
						result = this.dao.LoadFileFromImageInfo(imageInfoTableName, DataId);
					}
				}
			}
			return result;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000410D0 File Offset: 0x0003F2D0
		public IList<Pair<PersonBase, PersonBase>> ChangeAssignedAdvisorBatch(int ControlId, int OldAssignedAdvisorPersonId, int NewAssignedAdvisorPersonId)
		{
			bool flag = ControlId < 1;
			if (flag)
			{
				throw new InvalidParameterException("DynamicDataManager:ChangeAssignedAdvisorBatch:ControlId parameter is invalid:cid=" + ControlId.ToString());
			}
			bool flag2 = OldAssignedAdvisorPersonId < 1;
			if (flag2)
			{
				throw new InvalidParameterException("DynamicDataManager:ChangeAssignedAdvisorBatch:OldAssignedAdvisorPersonId parameter is invalid:OldAssignedAdvisorPersonId=" + OldAssignedAdvisorPersonId.ToString());
			}
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			return dynamicDataDAO.SwapAssignedAdvisors(ControlId, OldAssignedAdvisorPersonId, NewAssignedAdvisorPersonId);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00041138 File Offset: 0x0003F338
		public int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid)
		{
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			return dynamicDataDAO.GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(cid, pid);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00041160 File Offset: 0x0003F360
		public IDictionary<int, DateTime?> LoadDateTimeDynamicPerStudentDataForStudents(int[] studentPersonIds, int cid)
		{
			int[] pids = studentPersonIds.Distinct<int>().ToArray<int>();
			int num = pids.Length;
			bool flag = num < 1;
			IDictionary<int, DateTime?> result;
			if (flag)
			{
				result = new Dictionary<int, DateTime?>();
			}
			else
			{
				IList<Chunk> source = num.BreakdownItemsIntoChunks(1000);
				ConcurrentDictionary<int, DateTime?> items = new ConcurrentDictionary<int, DateTime?>();
				Parallel.ForEach<Chunk>(source, delegate(Chunk chunk)
				{
					IDictionary<int, DateTime?> dictionary = this.dao.LoadDateTimeDynamicPerStudentDataForStudents(pids.GetRange(chunk.Start, chunk.End - chunk.Start + 1), cid);
					foreach (KeyValuePair<int, DateTime?> keyValuePair in dictionary)
					{
						int key = keyValuePair.Key;
						bool flag2 = items.ContainsKey(key);
						if (flag2)
						{
							items[key] = keyValuePair.Value;
						}
					}
				});
				result = items;
			}
			return result;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000411E4 File Offset: 0x0003F3E4
		[DebuggerStepThrough]
		public Task<IDictionary<int, DateTime?>> LoadDateTimeDynamicPerStudentDataForStudentsAsync(int[] studentPersonIds, int cid)
		{
			DynamicDataManager.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__60 <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__ = new DynamicDataManager.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__60();
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDictionary<int, DateTime?>>.Create();
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>4__this = this;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.studentPersonIds = studentPersonIds;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.cid = cid;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>1__state = -1;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder.Start<DynamicDataManager.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__60>(ref <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__);
			return <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001B7 RID: 439
		private IOldUserSettingManager _oldUserSettingManager;

		// Token: 0x040001B8 RID: 440
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x040001B9 RID: 441
		private IDynamicDataDAO dao;
	}
}
