using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DataFileIO.cs;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x02000110 RID: 272
	public class DataSyncInfoManager : IDataSyncInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0004AAA0 File Offset: 0x00048CA0
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0004AAD8 File Offset: 0x00048CD8
		private DynamicDataManager dynamicDataManager
		{
			get
			{
				bool flag = this._dynamicDataManager == null;
				if (flag)
				{
					this._dynamicDataManager = new DynamicDataManager(this.OpContext);
				}
				return this._dynamicDataManager;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0004AB10 File Offset: 0x00048D10
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0004AB18 File Offset: 0x00048D18
		internal IDataSyncInfoDAO DataSyncInfoDao { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0004AB24 File Offset: 0x00048D24
		private IOldUserSettingManager oldUserSettingManager
		{
			get
			{
				bool flag = this.osm == null;
				if (flag)
				{
					this.osm = new OldUserSettingManager(this.OpContext);
				}
				return this.osm;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004AB5A File Offset: 0x00048D5A
		public DataSyncInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.DataSyncInfoDao = new DataSyncInfoDAO(opContext);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0004AB79 File Offset: 0x00048D79
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0004AB81 File Offset: 0x00048D81
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B37 RID: 2871 RVA: 0x0004AB8C File Offset: 0x00048D8C
		public DataSyncInfo LoadDataSyncInfo()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			return new DataSyncInfo
			{
				BatchDataSyncReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_DataSync_BatchImportReportId),
				MoveDataIntoClockWorkReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_DataSync_MoveDataIntoClockWorkReportid),
				ImportStudentDataReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_ReportNumberToRunForImportingStudentsFromExternalDatabase),
				ImportStudentCoursesReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_ReportNumberToRunForImportingStudentCourses),
				PreviewStudentDataReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_ReportNumberToRunForPreviewingStudentsFromExternalDatabase),
				GroupsReportId = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_ReportNumberToRunForGettingGroupMembershipsForStudentsFromExternalDatabase)
			};
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0004AC2C File Offset: 0x00048E2C
		public IList<DataSyncInfoActionResult> DataSyncInfo(DataSyncInfoSettings Settings, string MapXml, DataTable ExternalDataTable)
		{
			IList<DataSyncInfoMapItem> map = this.ParseMap(MapXml);
			IList<DataSyncExternalData> list = this.ParseExternalDataFromTable(map, ExternalDataTable);
			return this.DataSyncInfo(Settings, map, ref list);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0004AC5C File Offset: 0x00048E5C
		public IList<DataSyncInfoActionResult> DataSyncInfo(DataSyncInfoSettings Settings, IList<DataSyncInfoMapItem> Map, ref IList<DataSyncExternalData> ExternalDataItems)
		{
			int num = this.LookupClockWorkPersonId(ref ExternalDataItems);
			bool flag = num > 0;
			IList<DataSyncInfoActionResult> result;
			if (flag)
			{
				this.LookupExistingClockWorkData(num, Settings, ref ExternalDataItems);
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				IDynamicDataManager dynamicDataManager2 = dynamicDataManager;
				DynamicDataContext dynamicDataContext = new DynamicDataContext();
				dynamicDataContext.PrimaryId = num;
				IList<IDynamicDataSerializableItem> source = dynamicDataManager2.LoadDynamicDataItemsByControlIds(dynamicDataContext, (from h in ExternalDataItems
				where h.MapItem != null && h.MapItem.ClockWorkControlId > 0
				select h).ToList<DataSyncExternalData>().ConvertAll<int>((DataSyncExternalData g) => g.MapItem.ClockWorkControlId), eDynamicFormType.PerStudent);
				foreach (DataSyncExternalData dataSyncExternalData in ExternalDataItems)
				{
					bool flag2 = dataSyncExternalData.MapItem != null && dataSyncExternalData.MapItem.ClockWorkControlId > 0;
					if (flag2)
					{
						int cid = dataSyncExternalData.MapItem.ClockWorkControlId;
						List<IDynamicDataSerializableItem> list = (from g in source
						where g.Field.ControlId == cid
						select g).ToList<IDynamicDataSerializableItem>();
					}
				}
				IList<DataSyncInfoAction> actions = this.FigureOutActions(num, Settings, Map, ExternalDataItems);
				IList<DataSyncInfoActionResult> list2 = this.ExecuteActions(num, Settings, Map, actions);
				result = list2;
			}
			else
			{
				result = new List<DataSyncInfoActionResult>
				{
					new DataSyncInfoActionResult
					{
						Action = null,
						ResultType = eDataSyncActionResultType.FailBecauseStudentNoNotFound
					}
				};
			}
			return result;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		private IList<DataSyncInfoActionResult> ExecuteActions(int pid, DataSyncInfoSettings Settings, IList<DataSyncInfoMapItem> Map, IList<DataSyncInfoAction> Actions)
		{
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = pid
			};
			List<DataSyncInfoActionResult> list = new List<DataSyncInfoActionResult>();
			foreach (DataSyncInfoAction dataSyncInfoAction in Actions)
			{
				DataSyncInfoActionResult dataSyncInfoActionResult = new DataSyncInfoActionResult
				{
					Action = dataSyncInfoAction,
					PreviousValue = dataSyncInfoAction.ExternalData.MatchingClockWorkData.Value
				};
				List<DynamicData> data = new List<DynamicData>
				{
					dataSyncInfoAction.ExternalData.MatchingClockWorkData
				};
				eDataSyncInfoActionType actionType = dataSyncInfoAction.ActionType;
				eDataSyncInfoActionType eDataSyncInfoActionType = actionType;
				if (eDataSyncInfoActionType != eDataSyncInfoActionType.ClearClockWorkField)
				{
					if (eDataSyncInfoActionType != eDataSyncInfoActionType.UpdateClockWorkField)
					{
						dataSyncInfoActionResult.WasSuccessful = false;
						dataSyncInfoActionResult.ResultType = eDataSyncActionResultType.Unknown;
						dataSyncInfoActionResult.ErrorMessage = dataSyncInfoAction.ValueToWrite.ToString();
					}
					else
					{
						dataSyncInfoAction.ExternalData.MatchingClockWorkData.Value = dataSyncInfoAction.ValueToWrite;
						try
						{
							this.dynamicDataManager.SaveData(context, data, Settings.DynamicFormType);
							dataSyncInfoActionResult.ResultType = eDataSyncActionResultType.ClockWorkDataUpdatedSuccess;
							dataSyncInfoActionResult.WasSuccessful = true;
						}
						catch (Exception ex)
						{
							dataSyncInfoActionResult.ResultType = eDataSyncActionResultType.ClockWorkDataUpdatedFail;
							dataSyncInfoActionResult.ErrorMessage = ex.ToString();
							dataSyncInfoActionResult.WasSuccessful = false;
						}
					}
				}
				else
				{
					dataSyncInfoAction.ExternalData.MatchingClockWorkData.Value = dataSyncInfoAction.ValueToWrite;
					try
					{
						this.dynamicDataManager.SaveData(context, data, Settings.DynamicFormType);
						dataSyncInfoActionResult.ResultType = eDataSyncActionResultType.ClockWorkDataClearedSuccess;
						dataSyncInfoActionResult.WasSuccessful = true;
					}
					catch (Exception ex2)
					{
						dataSyncInfoActionResult.ResultType = eDataSyncActionResultType.ClockWorkDataClearedFail;
						dataSyncInfoActionResult.ErrorMessage = ex2.ToString();
						dataSyncInfoActionResult.WasSuccessful = false;
					}
				}
				list.Add(dataSyncInfoActionResult);
			}
			return list;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0004AFE4 File Offset: 0x000491E4
		private IList<DataSyncInfoAction> FigureOutActions(int pid, DataSyncInfoSettings Settings, IList<DataSyncInfoMapItem> Map, IList<DataSyncExternalData> ExternalDataItems)
		{
			Dictionary<int, List<DynamicListItem>> dictionary = new Dictionary<int, List<DynamicListItem>>();
			List<DataSyncInfoAction> list = new List<DataSyncInfoAction>();
			List<DataSyncExternalData> list2 = ExternalDataItems.ToList<DataSyncExternalData>();
			list2.Sort(delegate(DataSyncExternalData e1, DataSyncExternalData e2)
			{
				bool flag14 = e1.MapItem.ClockWorkControlId == e2.MapItem.ClockWorkControlId;
				int result;
				if (flag14)
				{
					result = e1.MapItem.ClockWorkSecondaryId.CompareTo(e2.MapItem.ClockWorkSecondaryId);
				}
				else
				{
					result = e1.MapItem.ClockWorkControlId.CompareTo(e2.MapItem.ClockWorkControlId);
				}
				return result;
			});
			int i = 0;
			List<DataSyncExternalDataGroup> list3 = new List<DataSyncExternalDataGroup>();
			while (i < list2.Count)
			{
				int clockWorkControlId = list2[0].MapItem.ClockWorkControlId;
				int j;
				for (j = i + 1; j < list2.Count; j++)
				{
					int clockWorkControlId2 = list2[j].MapItem.ClockWorkControlId;
					bool flag = clockWorkControlId2 != clockWorkControlId;
					if (flag)
					{
						break;
					}
				}
				List<DataSyncExternalData> list4 = new List<DataSyncExternalData>();
				for (int k = i; k < j; k++)
				{
					list4.Add(list2[k]);
				}
				list3.Add(new DataSyncExternalDataGroup
				{
					ClockWorkControlId = clockWorkControlId,
					Items = list4
				});
				i = j;
			}
			foreach (DataSyncExternalDataGroup dataSyncExternalDataGroup in list3)
			{
				bool flag2 = dataSyncExternalDataGroup.Items.Count == 1;
				if (flag2)
				{
					DataSyncExternalData dataSyncExternalData = dataSyncExternalDataGroup.Items[0];
					bool flag3 = dataSyncExternalData.MatchingClockWorkData == null;
					if (flag3)
					{
						list.Add(new DataSyncInfoAction
						{
							ExternalData = dataSyncExternalData,
							ClockWorkDataType = Settings.DynamicFormType,
							ActionType = eDataSyncInfoActionType.NoAction,
							ValueToWrite = "Can't find controlid in ClockWork dynamiccontrols"
						});
					}
					else
					{
						object valueToWrite = this.GetValueToWrite(Settings, ref dictionary, dataSyncExternalData);
						bool flag4 = valueToWrite == null;
						bool flag5 = dataSyncExternalData.MatchingClockWorkData == null || dataSyncExternalData.MatchingClockWorkData.Value == null || (dataSyncExternalData.MatchingClockWorkData.Value is string && ((string)dataSyncExternalData.MatchingClockWorkData.Value).Trim().Length < 1);
						bool flag6 = flag4 && flag5;
						if (!flag6)
						{
							bool flag7 = flag4;
							if (flag7)
							{
								bool overwriteClockWorkValuesWithExternalEmptyValue = Settings.OverwriteClockWorkValuesWithExternalEmptyValue;
								if (overwriteClockWorkValuesWithExternalEmptyValue)
								{
									list.Add(new DataSyncInfoAction
									{
										ActionType = eDataSyncInfoActionType.ClearClockWorkField,
										ClockWorkDataType = Settings.DynamicFormType,
										ExternalData = dataSyncExternalData,
										ValueToWrite = null
									});
								}
							}
							else
							{
								bool flag8 = flag5;
								if (flag8)
								{
									list.Add(new DataSyncInfoAction
									{
										ActionType = eDataSyncInfoActionType.UpdateClockWorkField,
										ClockWorkDataType = Settings.DynamicFormType,
										ExternalData = dataSyncExternalData,
										ValueToWrite = valueToWrite
									});
								}
								else
								{
									bool flag9 = dataSyncExternalData.MatchingClockWorkData == null;
									if (flag9)
									{
										dataSyncExternalData.MatchingClockWorkData = new DynamicData();
									}
									bool flag10 = false;
									bool flag11 = dataSyncExternalData.MatchingClockWorkData.ValueId > 0;
									if (flag11)
									{
										flag10 = this.ObjectsAreEqual(valueToWrite, dataSyncExternalData.MatchingClockWorkData.ValueId);
									}
									bool flag12 = !flag10;
									if (flag12)
									{
										flag10 = this.ObjectsAreEqual(valueToWrite, dataSyncExternalData.MatchingClockWorkData.Value);
									}
									bool flag13 = !flag10;
									if (flag13)
									{
										list.Add(new DataSyncInfoAction
										{
											ActionType = eDataSyncInfoActionType.UpdateClockWorkField,
											ClockWorkDataType = Settings.DynamicFormType,
											ExternalData = dataSyncExternalData,
											ValueToWrite = valueToWrite
										});
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0004B388 File Offset: 0x00049588
		private bool ObjectsAreEqual(object o1, object o2)
		{
			bool flag = o1 == null && o2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = o1 == null || o2 == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = o1.GetType() == o2.GetType();
					if (flag3)
					{
						bool flag4 = o1 is DateTime;
						if (flag4)
						{
							DateTime dateTime = (DateTime)o1;
							DateTime dateTime2 = (DateTime)o2;
							result = dateTime.Date.Equals(dateTime2.Date);
						}
						else
						{
							bool flag5 = o1 is int;
							if (flag5)
							{
								result = ((int)o1 == (int)o2);
							}
							else
							{
								bool flag6 = o1 is byte[];
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = o1 is string;
									if (flag7)
									{
										result = ((string)o1).Equals((string)o2, StringComparison.OrdinalIgnoreCase);
									}
									else
									{
										result = o1.ToString().Equals(o2.ToString());
									}
								}
							}
						}
					}
					else
					{
						bool flag8 = o1 is int && o2 is bool;
						if (flag8)
						{
							int num = (int)o1;
							int num2 = ((bool)o2) ? 1 : 0;
							result = (num == num2);
						}
						else
						{
							bool flag9 = o1 is bool && o2 is int;
							if (flag9)
							{
								int num3 = (int)o2;
								int num4 = ((bool)o1) ? 1 : 0;
								result = (num3 == num4);
							}
							else
							{
								result = false;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0004B508 File Offset: 0x00049708
		private object GetValueToWrite(DataSyncInfoSettings settings, ref Dictionary<int, List<DynamicListItem>> dynamicListItemsCache, DataSyncExternalData extDataItem)
		{
			bool flag = string.IsNullOrEmpty(extDataItem.FieldValue);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = extDataItem.FieldValue.Trim().Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					DynamicField field = extDataItem.MatchingClockWorkData.Field;
					bool flag3 = field == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						string val = extDataItem.FieldValue;
						eControlCode controlCode = field.ControlCode;
						eControlCode eControlCode = controlCode;
						if (eControlCode <= eControlCode.RadioGroup)
						{
							switch (eControlCode)
							{
							case eControlCode.TextBox:
								return val.ToString().Trim();
							case eControlCode.CheckBox:
							case eControlCode.RadioButton:
								goto IL_D2;
							case eControlCode.DropList:
								break;
							case eControlCode.Label:
							case eControlCode.Time:
							case eControlCode.HorizontalRule:
							case eControlCode.BlankSpace:
								goto IL_208;
							case eControlCode.Date:
								goto IL_118;
							case eControlCode.ListView:
								return null;
							default:
								if (eControlCode != eControlCode.RadioGroup)
								{
									goto IL_208;
								}
								break;
							}
							bool flag4 = field.Setting4 == 0;
							if (!flag4)
							{
								return val.ToString().Trim();
							}
							int setting = field.Setting1;
							bool flag5 = dynamicListItemsCache.ContainsKey(setting);
							List<DynamicListItem> list;
							if (flag5)
							{
								list = dynamicListItemsCache[setting];
							}
							else
							{
								list = this.dynamicFieldManager.LoadListItems(setting);
								dynamicListItemsCache.Add(setting, list);
							}
							DynamicListItem dynamicListItem = list.Find((DynamicListItem g) => g.LookupText.Equals(val, StringComparison.OrdinalIgnoreCase) || g.LookupValue.Equals(val, StringComparison.OrdinalIgnoreCase));
							bool flag6 = dynamicListItem != null;
							if (flag6)
							{
								return dynamicListItem.LookupListId;
							}
							bool automaticallyCreateLookupListItemsIfTheyDontExistInClockWork = settings.AutomaticallyCreateLookupListItemsIfTheyDontExistInClockWork;
							if (automaticallyCreateLookupListItemsIfTheyDontExistInClockWork)
							{
							}
							goto IL_216;
						}
						else if (eControlCode != eControlCode.AccommodationCheckBox)
						{
							if (eControlCode != eControlCode.AccommodationDatePicker)
							{
								goto IL_208;
							}
							goto IL_118;
						}
						IL_D2:
						string value = val.ToString().Trim().ToLower();
						bool flag7 = "trueyes1".IndexOf(value) >= 0;
						bool flag8 = flag7;
						if (flag8)
						{
							return 1;
						}
						return null;
						IL_118:
						DateTime dateTime;
						bool flag9 = DateTime.TryParse(val.ToString(), out dateTime);
						if (flag9)
						{
							return dateTime;
						}
						goto IL_216;
						IL_208:
						return val.ToString();
						IL_216:
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0004B730 File Offset: 0x00049930
		private int LookupClockWorkPersonId(ref IList<DataSyncExternalData> externalDataItems)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int firstPid = 0;
			foreach (DataSyncExternalData dataSyncExternalData in externalDataItems)
			{
				string text = dataSyncExternalData.Student_no.Trim().ToUpper();
				bool flag = dictionary.ContainsKey(text);
				if (flag)
				{
					dataSyncExternalData.ClockWorkPersonId = dictionary[text];
				}
				else
				{
					PersonBase personBase = peopleManager.LoadPersonByStudentNumber(text);
					int num = (personBase == null) ? 0 : personBase.PersonId;
					dictionary.Add(text, num);
					dataSyncExternalData.ClockWorkPersonId = num;
					bool flag2 = firstPid < 1;
					if (flag2)
					{
						firstPid = num;
					}
				}
			}
			bool flag3 = firstPid > 0;
			if (flag3)
			{
				List<DataSyncExternalData> list = externalDataItems.ToList<DataSyncExternalData>().FindAll((DataSyncExternalData f) => f.ClockWorkPersonId != firstPid);
				bool flag4 = list != null;
				if (flag4)
				{
					foreach (DataSyncExternalData item in list)
					{
						externalDataItems.Remove(item);
					}
				}
			}
			return firstPid;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0004B8A4 File Offset: 0x00049AA4
		private void LookupExistingClockWorkData(int pid, DataSyncInfoSettings Settings, ref IList<DataSyncExternalData> externalDataItems)
		{
			bool flag = externalDataItems.Count < 1;
			if (!flag)
			{
				List<int> list = new List<int>();
				foreach (DataSyncExternalData dataSyncExternalData in externalDataItems)
				{
					int clockWorkControlId = dataSyncExternalData.MapItem.ClockWorkControlId;
					bool flag2 = !list.Contains(clockWorkControlId);
					if (flag2)
					{
						list.Add(clockWorkControlId);
					}
				}
				List<DynamicData> list2 = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
				{
					PrimaryId = pid
				}, list, Settings.DynamicFormType);
				using (IEnumerator<DataSyncExternalData> enumerator2 = externalDataItems.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DataSyncExternalData externalDataItem = enumerator2.Current;
						DynamicData dynamicData = list2.Find((DynamicData f) => f.Field.ControlId.Equals(externalDataItem.MapItem.ClockWorkControlId));
						bool flag3 = dynamicData != null;
						if (flag3)
						{
							externalDataItem.MatchingClockWorkData = dynamicData;
						}
						else
						{
							DynamicField field = this.dynamicFieldManager.LoadFieldByControlId(externalDataItem.MapItem.ClockWorkControlId);
							externalDataItem.MatchingClockWorkData = new DynamicData
							{
								Field = field
							};
						}
					}
				}
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0004BA08 File Offset: 0x00049C08
		private IList<DataSyncInfoMapItem> ParseMap(string mapXml)
		{
			bool flag = string.IsNullOrEmpty(mapXml);
			IList<DataSyncInfoMapItem> result;
			if (flag)
			{
				result = new List<DataSyncInfoMapItem>();
			}
			else
			{
				bool flag2 = !mapXml.StartsWith("<?xml ", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = this.ParseMapLegacy(mapXml);
				}
				else
				{
					XDocument xdocument = XDocument.Parse(mapXml);
					IEnumerable<DataSyncInfoMapItem> source = from mapItem in xdocument.Descendants("mapitem")
					select new DataSyncInfoMapItem
					{
						ExternalFieldName = this.ParseStringFromXmlElement(mapItem.Element("ext")),
						ClockWorkControlId = this.ParseIntFromXmlElement(mapItem.Element("cid")),
						ClockWorkSecondaryId = this.ParseIntFromXmlElement(mapItem.Element("cid2"))
					};
					result = source.ToList<DataSyncInfoMapItem>();
				}
			}
			return result;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0004BA7C File Offset: 0x00049C7C
		private string ParseStringFromXmlElement(XElement x)
		{
			bool flag = x == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (x.Value ?? "");
			}
			return result;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0004BAB0 File Offset: 0x00049CB0
		private int ParseIntFromXmlElement(XElement x)
		{
			bool flag = x == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				string text = x.Value ?? "";
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(text, out num);
					if (flag3)
					{
						result = 0;
					}
					else
					{
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0004BB04 File Offset: 0x00049D04
		private IList<DataSyncInfoMapItem> ParseMapLegacy(string mapDefinition)
		{
			List<DataSyncInfoMapItem> list = new List<DataSyncInfoMapItem>();
			byte[] bytes = Encoding.ASCII.GetBytes(mapDefinition);
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (TextReader textReader = new StreamReader(memoryStream, Encoding.Default))
				{
					string text;
					while ((text = textReader.ReadLine()) != null)
					{
						bool flag = text.Trim().Length > 0;
						if (flag)
						{
							int num = text.IndexOf('=');
							bool flag2 = num > 0;
							if (flag2)
							{
								string s = text.Substring(0, num);
								string text2 = text.Substring(num + 1);
								int clockWorkControlId;
								bool flag3 = int.TryParse(s, out clockWorkControlId);
								if (flag3)
								{
									string[] array = text2.Split(new char[]
									{
										','
									});
									int num2 = 0;
									foreach (string text3 in array)
									{
										bool flag4 = text3.Trim().Length > 0;
										if (flag4)
										{
											list.Add(new DataSyncInfoMapItem
											{
												ClockWorkControlId = clockWorkControlId,
												ClockWorkSecondaryId = num2++,
												ExternalFieldName = text3.Trim()
											});
										}
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0004BC90 File Offset: 0x00049E90
		private IList<DataSyncExternalData> ParseExternalDataFromTable(IList<DataSyncInfoMapItem> map, DataTable table)
		{
			List<DataSyncExternalData> list = new List<DataSyncExternalData>();
			bool flag = table.Rows.Count < 1;
			IList<DataSyncExternalData> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string student_no = table.Rows[0]["student_no"].ToString();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					foreach (DataSyncInfoMapItem dataSyncInfoMapItem in map)
					{
						bool flag2 = table.Columns.Contains(dataSyncInfoMapItem.ExternalFieldName);
						if (flag2)
						{
							string fieldValue = dataRow[dataSyncInfoMapItem.ExternalFieldName].ToString().Trim();
							list.Add(new DataSyncExternalData
							{
								Student_no = student_no,
								FieldName = dataSyncInfoMapItem.ExternalFieldName,
								FieldValue = fieldValue,
								MatchingClockWorkData = null,
								MapItem = dataSyncInfoMapItem
							});
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0004BDE4 File Offset: 0x00049FE4
		public DataTable LoadOnlineIntakeFormData(int ScreenNum, string StudentNumber, out PersonBase StudentInfo)
		{
			DataTable dataTable = new DataTable("q");
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("student_no");
			dataTable.Columns.Add("firstname");
			dataTable.Columns.Add("middlename");
			dataTable.Columns.Add("lastname");
			List<DynamicField> list = this.dynamicFieldManager.LoadFields(ScreenNum, false);
			foreach (DynamicField dynamicField in list)
			{
				string text = dynamicField.ControlCaption ?? "";
				int num = text.IndexOf("~~");
				bool flag = num > 0;
				if (flag)
				{
					text = text.Substring(0, num);
				}
				bool flag2 = !dataTable.Columns.Contains(text);
				if (flag2)
				{
					dataTable.Columns.Add(text, (dynamicField.ControlCode == eControlCode.File || dynamicField.ControlCode == eControlCode.Picture) ? typeof(byte[]) : typeof(string));
				}
			}
			IList<DynamicData> list2 = this.DataSyncInfoDao.LoadOnlineIntakeFormData(ScreenNum, StudentNumber, out StudentInfo);
			bool flag3 = list2 == null || list2.Count < 1;
			DataTable result;
			if (flag3)
			{
				result = dataTable;
			}
			else
			{
				DataRow dataRow = dataTable.NewRow();
				bool flag4 = StudentInfo != null;
				if (flag4)
				{
					dataRow["firstname"] = StudentInfo.FirstName;
					dataRow["middlename"] = StudentInfo.MiddleName;
					dataRow["lastname"] = StudentInfo.LastName;
					dataRow["student_no"] = StudentInfo.Student_no;
					dataRow["personid"] = StudentInfo.PersonId;
				}
				foreach (DynamicData dynamicData in list2)
				{
					string text2 = dynamicData.Field.ControlCaption ?? "";
					int num2 = text2.IndexOf("~~");
					bool flag5 = num2 > 0;
					if (flag5)
					{
						text2 = text2.Substring(0, num2);
					}
					bool flag6 = !dataTable.Columns.Contains(text2);
					if (flag6)
					{
						bool flag7 = dynamicData.Field.ControlCode == eControlCode.File || dynamicData.Field.ControlCode == eControlCode.Picture;
						if (flag7)
						{
							dataTable.Columns.Add(text2, typeof(byte[]));
						}
						else
						{
							dataTable.Columns.Add(text2);
						}
					}
					bool flag8 = dataTable.Columns[text2].DataType == typeof(byte[]);
					if (flag8)
					{
						bool flag9 = dynamicData.Value != null;
						if (flag9)
						{
							bool flag10 = dynamicData.Value is byte[];
							if (flag10)
							{
								dataRow[text2] = (byte[])dynamicData.Value;
							}
						}
					}
					else
					{
						string value = (dynamicData.Value == null) ? "" : dynamicData.Value.ToString();
						dataRow[text2] = value;
					}
				}
				dataTable.Rows.Add(dataRow);
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0004C184 File Offset: 0x0004A384
		public DataTable LoadOnlineIntakeFormDataAndMergeWithExternalData(DataTable existingStudentDataToMergeWithResults, int ScreenNum, string StudentNumber, out PersonBase StudentInfo)
		{
			DataTable dataTable = this.LoadOnlineIntakeFormData(ScreenNum, StudentNumber, out StudentInfo);
			bool flag = dataTable == null || existingStudentDataToMergeWithResults == null || existingStudentDataToMergeWithResults.Rows.Count < 1;
			DataTable result;
			if (flag)
			{
				result = existingStudentDataToMergeWithResults;
			}
			else
			{
				result = DataTableUtility.JoinTables2<string>(existingStudentDataToMergeWithResults, dataTable, "student_no");
			}
			return result;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0004C1CC File Offset: 0x0004A3CC
		public DataTable LoadOnlineIntakeFormDataAndMergeWithExternalData(DataTable existingStudentDataToMergeWithResults, int ScreenNum, string StudentNumber)
		{
			PersonBase personBase;
			return this.LoadOnlineIntakeFormDataAndMergeWithExternalData(existingStudentDataToMergeWithResults, ScreenNum, StudentNumber, out personBase);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0004C1EC File Offset: 0x0004A3EC
		public void DataSyncIntakeData(string student_no)
		{
			string text = (student_no ?? "").Trim().ToUpper();
			bool flag = text.Length < 1;
			if (!flag)
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				PersonBase personBase = peopleManager.LoadPersonByStudentNumber(text);
				bool flag2 = personBase == null || personBase.PersonId < 1;
				if (flag2)
				{
					CWLogger.Logger.Warn("Common.Core.DataSync.DataSyncInfoManager:DataSyncIntakeData:FailedToFindStudentInClockWork:snum={0}", text);
				}
				else
				{
					bool flag3 = personBase.CoreGroup != eCoreGroup.Students;
					if (flag3)
					{
						CWLogger.Logger.Warn("Common.Core.DataSync.DataSyncInfoManager:DataSyncIntakeData:FoundPersonInClockWorkButNotAStudent:snum={0}:coregroup={1}", text, personBase.CoreGroup.ToString());
					}
					else
					{
						int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.INTAKE_FormNum);
						int personId = personBase.PersonId;
						this.DataSyncInfoDao.DataSyncIntakeData(personId, text, settingValue, true);
					}
				}
			}
		}

		// Token: 0x040001F2 RID: 498
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x040001F3 RID: 499
		private DynamicDataManager _dynamicDataManager;

		// Token: 0x040001F5 RID: 501
		private IOldUserSettingManager osm;
	}
}
