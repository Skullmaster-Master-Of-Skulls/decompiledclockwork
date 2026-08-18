using System;
using System.Collections.Generic;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Database;
using TechnoPro.Common.DAO.DynamicForms.Legacy;
using TechnoPro.Common.DAO.Impl.Database;
using TechnoPro.Common.DAO.Impl.DynamicForms.Legacy;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DynamicForms.Legacy
{
	// Token: 0x02000101 RID: 257
	public class LegacyDynamicDataSaveLoadManager : ILegacyDynamicDataSaveLoadManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0000672B File Offset: 0x0000492B
		public LegacyDynamicDataSaveLoadManager()
		{
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00043943 File Offset: 0x00041B43
		public LegacyDynamicDataSaveLoadManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00043955 File Offset: 0x00041B55
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0004395D File Offset: 0x00041B5D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A83 RID: 2691 RVA: 0x00043968 File Offset: 0x00041B68
		public IList<LegacySaveDataResult> SaveDataPS(LegacyDynamicDataRowDatas legacyData, string tableName, int screenNum, int studentPid, int whoModifiedPid, bool tablesStoreScreenNum)
		{
			ICacheStorageManager cacheStorageManager = null;
			int num = 0;
			bool flag = tableName.Equals("otherinfops", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["StudentFileUploads_FileListControlId"];
				num = ((obj != null && obj is int) ? ((int)obj) : 0);
			}
			List<LegacyDynamicDataRowSaveData> list = new List<LegacyDynamicDataRowSaveData>();
			Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
			foreach (LegacyDynamicDataRowData legacyDynamicDataRowData in legacyData.RowDatas)
			{
				bool flag2 = legacyDynamicDataRowData.RowState != eLegacyDynamicDataRowState.Added && legacyDynamicDataRowData.RowState != eLegacyDynamicDataRowState.Deleted && legacyDynamicDataRowData.RowState != eLegacyDynamicDataRowState.Modified;
				if (!flag2)
				{
					LegacyDynamicDataRowSaveData legacyDynamicDataRowSaveData = new LegacyDynamicDataRowSaveData(legacyDynamicDataRowData.RowState, legacyData.ControlValueType);
					if (tablesStoreScreenNum)
					{
						legacyDynamicDataRowSaveData.ScreenNum = screenNum;
					}
					legacyDynamicDataRowSaveData.PersonId = studentPid;
					legacyDynamicDataRowSaveData.ControlId = legacyDynamicDataRowData.ControlId;
					legacyDynamicDataRowSaveData.WhoAmI = whoModifiedPid;
					switch (legacyData.ControlValueType)
					{
					case eLegacyDynamicDataType.Int:
						legacyDynamicDataRowSaveData.ControlValue = ((legacyDynamicDataRowData.ControlValueInt != null) ? legacyDynamicDataRowData.ControlValueInt.Value : null);
						break;
					case eLegacyDynamicDataType.Binary:
					{
						legacyDynamicDataRowSaveData.ControlValue = legacyDynamicDataRowData.ControlValueBytes;
						bool flag3 = num > 0 && num == legacyDynamicDataRowSaveData.ControlId;
						if (flag3)
						{
							bool flag4 = legacyDynamicDataRowSaveData.RowState == eLegacyDynamicDataRowState.Deleted || legacyDynamicDataRowData.ControlValueBytes == null || legacyDynamicDataRowData.ControlValueBytes.Length < 1;
							bool value;
							if (flag4)
							{
								value = false;
							}
							else
							{
								string @string = Encoding.ASCII.GetString(legacyDynamicDataRowData.ControlValueBytes);
								List<string[]> list2 = DynamicDataManager.DecodeDocumentsList(@string ?? "");
								value = false;
								foreach (string[] array in list2)
								{
									bool flag5 = array.Length != 0 && array[0].IndexOf("[closed]", StringComparison.OrdinalIgnoreCase) >= 0;
									bool flag6 = !flag5;
									if (flag6)
									{
										value = true;
										break;
									}
								}
							}
							bool flag7 = dictionary.ContainsKey(studentPid);
							if (flag7)
							{
								dictionary.Remove(studentPid);
							}
							dictionary.Add(studentPid, value);
						}
						break;
					}
					case eLegacyDynamicDataType.DateTime:
						legacyDynamicDataRowSaveData.ControlValue = ((legacyDynamicDataRowData.ControlValueDateTime != null) ? legacyDynamicDataRowData.ControlValueDateTime.Value : null);
						break;
					}
					list.Add(legacyDynamicDataRowSaveData);
				}
			}
			bool flag8 = cacheStorageManager == null;
			if (flag8)
			{
				cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			}
			string key = "dynamicDataTableHasArchive_" + tableName;
			object obj2 = cacheStorageManager[key];
			bool flag9 = obj2 != null;
			bool flag10;
			if (flag9)
			{
				flag10 = (bool)obj2;
			}
			else
			{
				IDatabaseDAO databaseDAO = new DatabaseDAO(this.OpContext);
				flag10 = databaseDAO.DoesTableExist(tableName + "archive");
				cacheStorageManager.Insert(key, flag10);
			}
			ILegacyDynamicFieldSaveLoadDAO legacyDynamicFieldSaveLoadDAO = new LegacyDynamicFieldSaveLoadDAO(this.OpContext);
			IList<LegacySaveDataResult> result = legacyDynamicFieldSaveLoadDAO.SaveLegacyDataPerStudent(list, tableName, tablesStoreScreenNum, flag10);
			bool flag11 = dictionary.Count > 0;
			if (flag11)
			{
				legacyDynamicFieldSaveLoadDAO.UpdateStudentFileUploadStatusMarkers(num, dictionary);
			}
			return result;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00043D00 File Offset: 0x00041F00
		public Pair<eDynamicFormType, DynamicDataContext> GetFormTypeAndDynamicDataContextFromDataIdAndControlId(int dataId, int controlId)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<int> list = (controlId > 0) ? dynamicFormManager.FindScreensAControlExistsOn(controlId) : null;
			DynamicForm dynamicForm = (list == null || list.Count < 1) ? null : dynamicFormManager.LoadDynamicFormById(list[0]);
			bool flag = dynamicForm == null;
			Pair<eDynamicFormType, DynamicDataContext> result;
			if (flag)
			{
				CWLogger.Logger.Warn("LegacyDynamicDataSaveLoadManager:GetFormTypeAndPersonIdFromDataIdAndControlId:Can't find any forms for controlid={0}", controlId.ToString());
				result = null;
			}
			else
			{
				ILegacyDynamicFieldSaveLoadDAO legacyDynamicFieldSaveLoadDAO = new LegacyDynamicFieldSaveLoadDAO(this.OpContext);
				result = new Pair<eDynamicFormType, DynamicDataContext>(dynamicForm.FormType, legacyDynamicFieldSaveLoadDAO.LoadDataContext(dynamicForm.FormType, dataId, controlId));
			}
			return result;
		}
	}
}
