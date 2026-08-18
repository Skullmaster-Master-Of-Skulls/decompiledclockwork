using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.DataMigration;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Internal;
using TechnoPro.Common.Public.Entities.DataMigration.Mapping;
using TechnoPro.Common.Public.Entities.DataMigration.Results;
using TechnoPro.Common.Public.Entities.DataMigration.TestsAndExams;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DataMigration
{
	// Token: 0x02000113 RID: 275
	public class DataMigrationManager : IDataMigrationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
		public DataMigrationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0004EED1 File Offset: 0x0004D0D1
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0004EED9 File Offset: 0x0004D0D9
		public OperationContext OpContext { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		private IDynamicFieldManager dynamicFieldManager
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0004EF1C File Offset: 0x0004D11C
		private IBaseAppointmentManager baseAppManager
		{
			get
			{
				bool flag = this._baseAppManager == null;
				if (flag)
				{
					this._baseAppManager = new BaseAppointmentManager(this.OpContext);
				}
				return this._baseAppManager;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004EF54 File Offset: 0x0004D154
		public List<MigrationMapperDataItemInternal> PreProcessMapperItems(IList<MigrationMapperDataItem> MapperItems)
		{
			List<MigrationMapperDataItem> list = MapperItems.ToList<MigrationMapperDataItem>();
			List<int> controlIds = list.ConvertAll<int>((MigrationMapperDataItem f) => f.ClockWorkCid);
			List<DynamicField> fields = this.dynamicFieldManager.LoadFieldsByControlIds(controlIds);
			return (from item in list
			let field = fields.Find((DynamicField f) => f.ControlId == item.ClockWorkCid)
			select new MigrationMapperDataItemInternal
			{
				ClockWorkField = field,
				DataNamesOrdered = item.DataNamesOrdered,
				ClockWorkCid = item.ClockWorkCid
			}).ToList<MigrationMapperDataItemInternal>();
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004EFE8 File Offset: 0x0004D1E8
		public MigrationStudentInternal PreProcessStudent(MigrationStudent Student)
		{
			string text = (Student.StudentNumber ?? "").Trim().ToUpper();
			PersonBase clockWorkStudent = (text.Length > 0) ? this.adminPeopleManager.LoadAnyNonDeletedAccountByStudentNumber(text) : null;
			MigrationStudentInternal migrationStudentInternal = new MigrationStudentInternal();
			IList<int> clockWorkGroupIds;
			if (Student.ClockWorkGroupIds != null && Student.ClockWorkGroupIds.Count >= 1)
			{
				clockWorkGroupIds = Student.ClockWorkGroupIds;
			}
			else
			{
				IList<int> list = new List<int>
				{
					1
				};
				clockWorkGroupIds = list;
			}
			migrationStudentInternal.ClockWorkGroupIds = clockWorkGroupIds;
			migrationStudentInternal.FirstName = (Student.FirstName ?? "");
			migrationStudentInternal.MiddleName = (Student.MiddleName ?? "");
			migrationStudentInternal.LastName = (Student.LastName ?? "");
			migrationStudentInternal.StudentNumber = text;
			migrationStudentInternal.ClockWorkStudent = clockWorkStudent;
			return migrationStudentInternal;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0004F0B8 File Offset: 0x0004D2B8
		private IDynamicDataManager dataManager
		{
			get
			{
				IDynamicDataManager result;
				if ((result = this._dataManager) == null)
				{
					result = (this._dataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
		private IPeopleManager peopleManager
		{
			get
			{
				IPeopleManager result;
				if ((result = this._peopleManager) == null)
				{
					result = (this._peopleManager = new PeopleManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0004F118 File Offset: 0x0004D318
		private IAdminPeopleManager adminPeopleManager
		{
			get
			{
				IAdminPeopleManager result;
				if ((result = this._adminPeopleManager) == null)
				{
					result = (this._adminPeopleManager = new AdminPeopleManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004F144 File Offset: 0x0004D344
		private IDictionary<string, int> GetRadioGroupAsPrimaryLookupControlList(int radioGroupCid)
		{
			string key = "radioGroupCid_" + radioGroupCid.ToString();
			bool flag = this.tempCache.ContainsKey(key);
			IDictionary<string, int> result;
			if (flag)
			{
				result = (IDictionary<string, int>)this.tempCache[key];
			}
			else
			{
				IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
				IList<int> list = dynamicFormManager.FindScreensAControlExistsOn(radioGroupCid);
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				Predicate<DynamicField> <>9__0;
				foreach (int screenNum in list)
				{
					List<DynamicField> list2 = this.dynamicFieldManager.LoadFields(screenNum, true);
					List<DynamicField> list3 = list2;
					Predicate<DynamicField> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((DynamicField g) => g.ControlId == radioGroupCid));
					}
					int num = list3.FindIndex(match);
					bool flag2 = num >= 0;
					if (flag2)
					{
						int i;
						for (i = num + 1; i < list2.Count; i++)
						{
							DynamicField dynamicField = list2[i];
							DynamicControlAttribute attribute = dynamicField.ControlCode.GetAttribute();
							bool flag3 = attribute == null || attribute.IsControlCollectionEnd || attribute.IsControlCollectionStart;
							if (flag3)
							{
								break;
							}
						}
						for (int j = num + 1; j < i; j++)
						{
							string key2 = list2[j].GetCaptionForDisplay().ToLower();
							bool flag4 = !dictionary.ContainsKey(key2);
							if (flag4)
							{
								dictionary.Add(key2, list2[j].ControlId);
							}
						}
					}
				}
				this.tempCache.Add(key, dictionary);
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004F328 File Offset: 0x0004D528
		private void WriteDynamicData(bool PreviewOnly, ref MigrationDataItemResult result, MigrationMapperDataItemInternal mapperItem, MigrationDataItem item, DynamicDataContext context, eDynamicFormType formType, bool clearExistingDataWhenMigrationDataIsEmpty)
		{
			bool flag = false;
			DynamicData item2 = null;
			eControlCode eControlCode = mapperItem.ClockWorkField.ControlCode;
			bool flag2 = eControlCode == eControlCode.DropList && mapperItem.ClockWorkField.Setting3 != 0;
			if (flag2)
			{
				eControlCode = eControlCode.TextBox;
			}
			bool flag3 = eControlCode == eControlCode.ListView;
			if (flag3)
			{
				eControlCode = eControlCode.TextBox;
			}
			bool flag4 = eControlCode == eControlCode.RadioGroup && mapperItem.ClockWorkField.Setting4 != 1;
			if (flag4)
			{
				eControlCode = eControlCode.DropList;
			}
			bool flag5 = false;
			bool flag6 = item == null;
			if (flag6)
			{
				result.ErrorMessage = "item is null";
				result.Status = eMigrationDataItemStatus.Failed;
				flag5 = true;
			}
			else
			{
				bool flag7 = item.DataValue == null || item.DataValue == DBNull.Value;
				if (flag7)
				{
					flag = true;
				}
				else
				{
					eControlCode eControlCode2 = eControlCode;
					eControlCode eControlCode3 = eControlCode2;
					if (eControlCode3 <= eControlCode.RadioGroup)
					{
						switch (eControlCode3)
						{
						case eControlCode.TextBox:
						{
							string text = item.DataValue.ToString().Trim();
							bool flag8 = text.Length < 1;
							if (flag8)
							{
								flag = true;
							}
							else
							{
								item2 = new DynamicData
								{
									Field = mapperItem.ClockWorkField,
									Value = text
								};
							}
							goto IL_6BD;
						}
						case eControlCode.CheckBox:
						{
							bool flag9 = item.DataValue is bool;
							bool flag10;
							if (flag9)
							{
								flag10 = (bool)item.DataValue;
							}
							else
							{
								bool flag11 = item.DataValue is bool?;
								if (flag11)
								{
									flag10 = ((bool?)item.DataValue != null && ((bool?)item.DataValue).Value);
								}
								else
								{
									string text2 = item.DataValue.ToString().Trim().ToLower();
									flag10 = (text2.Length > 0 && "trueyes1".IndexOf(text2) >= 0);
								}
							}
							bool flag12 = !flag10;
							if (flag12)
							{
								flag = true;
							}
							else
							{
								item2 = new DynamicData
								{
									Field = mapperItem.ClockWorkField,
									Value = true,
									ValueId = 1
								};
							}
							goto IL_6BD;
						}
						case eControlCode.DropList:
						{
							string strc = (item.DataValue == null) ? "" : item.DataValue.ToString().Trim();
							bool flag13 = strc.Length < 1;
							if (flag13)
							{
								flag = true;
							}
							else
							{
								List<DynamicListItem> list = this.dynamicFieldManager.LoadListItems(mapperItem.ClockWorkField.Setting1);
								DynamicListItem dynamicListItem = list.Find((DynamicListItem f) => f.LookupText.Equals(strc, StringComparison.OrdinalIgnoreCase));
								bool flag14 = dynamicListItem != null;
								if (flag14)
								{
									item2 = new DynamicData
									{
										Field = mapperItem.ClockWorkField,
										Value = dynamicListItem.LookupListId,
										ValueId = dynamicListItem.LookupListId
									};
								}
								else
								{
									result.ErrorMessage = strc;
									result.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.FailedToFindLookupListItem);
									flag5 = true;
								}
							}
							goto IL_6BD;
						}
						case eControlCode.RadioButton:
						case eControlCode.Label:
							break;
						case eControlCode.Date:
						{
							bool flag15 = item.DataValue == null;
							DateTime? dateTime;
							if (flag15)
							{
								dateTime = null;
							}
							else
							{
								bool flag16 = item.DataValue is DateTime?;
								if (flag16)
								{
									dateTime = (DateTime?)item.DataValue;
								}
								else
								{
									bool flag17 = item.DataValue is DateTime;
									if (flag17)
									{
										dateTime = new DateTime?((DateTime)item.DataValue);
									}
									else
									{
										string s = item.DataValue.ToString();
										DateTime value;
										bool flag18 = !DateTime.TryParse(s, out value);
										if (flag18)
										{
											dateTime = null;
										}
										else
										{
											dateTime = new DateTime?(value);
										}
									}
								}
							}
							bool flag19 = dateTime == null || dateTime.Value == DateTime.MinValue;
							if (flag19)
							{
								flag = true;
							}
							else
							{
								item2 = new DynamicData
								{
									Field = mapperItem.ClockWorkField,
									Value = dateTime.Value
								};
							}
							goto IL_6BD;
						}
						default:
							if (eControlCode3 == eControlCode.RadioGroup)
							{
								string text3 = (item.DataValue == null) ? "" : item.DataValue.ToString().Trim();
								bool flag20 = text3.Length < 1;
								if (flag20)
								{
									flag = true;
								}
								else
								{
									IDictionary<string, int> radioGroupAsPrimaryLookupControlList = this.GetRadioGroupAsPrimaryLookupControlList(mapperItem.ClockWorkField.ControlId);
									int num = (radioGroupAsPrimaryLookupControlList != null && radioGroupAsPrimaryLookupControlList.ContainsKey(text3.ToLower())) ? radioGroupAsPrimaryLookupControlList[text3.ToLower()] : 0;
									bool flag21 = num > 0;
									if (flag21)
									{
										item2 = new DynamicData
										{
											Field = mapperItem.ClockWorkField,
											Value = num,
											ValueId = num
										};
									}
									else
									{
										result.ErrorMessage = text3;
										result.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.FailedToFindLookupListItem);
										flag5 = true;
									}
								}
								goto IL_6BD;
							}
							break;
						}
					}
					else
					{
						if (eControlCode3 == eControlCode.StaffComboBox)
						{
							string staffVal = (item.DataValue == null) ? "" : item.DataValue.ToString().Trim();
							IList<PersonBase> source = this.LoadStaff();
							PersonBase personBase = source.FirstOrDefault((PersonBase g) => staffVal.Equals(g.Student_no ?? "", StringComparison.OrdinalIgnoreCase));
							bool flag22 = personBase == null;
							if (flag22)
							{
								personBase = source.FirstOrDefault((PersonBase g) => staffVal.Equals((g.FirstName ?? "") + " " + (g.LastName ?? ""), StringComparison.OrdinalIgnoreCase));
							}
							bool flag23 = personBase != null;
							if (flag23)
							{
								item2 = new DynamicData
								{
									Field = mapperItem.ClockWorkField,
									Value = personBase.PersonId,
									ValueId = personBase.PersonId
								};
							}
							else
							{
								result.ErrorMessage = staffVal;
								result.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.FailedToFindLookupListItem);
								flag5 = true;
							}
							goto IL_6BD;
						}
						if (eControlCode3 == eControlCode.File)
						{
							string text4 = item.DataValue.ToString().Trim();
							bool flag24 = text4.Length < 1;
							if (flag24)
							{
								flag = true;
							}
							else
							{
								try
								{
									item2 = new DynamicData
									{
										Field = mapperItem.ClockWorkField,
										Value = Convert.FromBase64String(text4)
									};
								}
								catch (Exception ex)
								{
									result.ErrorMessage = ex.ToString();
									result.Status = eMigrationDataItemStatus.FailedToParseBase64FileData;
									flag5 = true;
								}
							}
							goto IL_6BD;
						}
						if (eControlCode3 == eControlCode.RtfTextBox)
						{
							string text5 = item.DataValue.ToString().Trim();
							bool flag25 = text5.StartsWith("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
							bool flag26 = text5.Length < 1 || (flag25 && text5.ConvertRtfToPlainText().Length < 1);
							if (flag26)
							{
								flag = true;
							}
							else
							{
								bool flag27 = !flag25;
								if (flag27)
								{
									text5 = text5.ConvertPlainTextToRtf();
								}
								item2 = new DynamicData
								{
									Field = mapperItem.ClockWorkField,
									Value = text5
								};
							}
							goto IL_6BD;
						}
					}
					result.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.UnSupportedControlCode);
					flag5 = true;
					IL_6BD:;
				}
			}
			bool flag28 = !flag5;
			if (flag28)
			{
				bool flag29 = flag;
				if (flag29)
				{
					CWLogger.Logger.Debug("DataMigrationManager:WriteDynamicData:externalDataWasEmpty:appid={0}:studentpid={1}", context.SecondaryId.ToString(), context.PrimaryId.ToString());
					if (clearExistingDataWhenMigrationDataIsEmpty)
					{
						bool flag30 = !PreviewOnly;
						if (flag30)
						{
							this.dataManager.DeleteDataItem(context, mapperItem.ClockWorkField.ControlId, eControlCode, formType, eDynamicDataStorageLocation.Unknown);
							result.Status = eMigrationDataItemStatus.SuccessfulDeleteData;
						}
						else
						{
							result.Status = eMigrationDataItemStatus.SuccessfulDeleteData;
						}
					}
					else
					{
						result.Status = eMigrationDataItemStatus.SuccessfulAndNoData;
					}
				}
				else
				{
					CWLogger.Logger.Debug("DataMigrationManager:WriteDynamicData:NOT externalDataWasEmpty:appid={0}:studentpid={1}:previewOnly={2}", context.SecondaryId.ToString(), context.PrimaryId.ToString(), PreviewOnly.ToString());
					bool flag31 = !PreviewOnly;
					if (flag31)
					{
						this.dataManager.SaveData(context, new List<DynamicData>
						{
							item2
						}, formType);
					}
					result.Status = eMigrationDataItemStatus.Successful;
				}
			}
			else
			{
				CWLogger.Logger.Debug("DataMigrationManager:WriteDynamicData:shouldAbort:appid={0}:studentpid={1}", context.SecondaryId.ToString(), context.PrimaryId.ToString());
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0004FB48 File Offset: 0x0004DD48
		private IList<PersonBase> LoadStaff()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<PersonBase> list = (IList<PersonBase>)cacheStorageManager["migration_stafflist"];
			bool flag = list == null;
			if (flag)
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				list = peopleManager.LoadGroupMembers(2);
				cacheStorageManager.Insert("migration_stafflist", list, TimeSpan.FromMinutes(5.0));
			}
			return list;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0004FBB0 File Offset: 0x0004DDB0
		private List<MigrationFileInternal> PreProcessFiles(IList<MigrationFile> files0)
		{
			Dictionary<string, PersonBase> dictionary = new Dictionary<string, PersonBase>();
			List<MigrationFile> list = ((files0 != null) ? files0.ToList<MigrationFile>() : null) ?? new List<MigrationFile>();
			list.Sort((MigrationFile g1, MigrationFile g2) => (g1.StudentNumber ?? "").ToLower().CompareTo((g2.StudentNumber ?? "").ToLower()));
			int i = 0;
			List<MigrationFileInternal> list2 = new List<MigrationFileInternal>();
			while (i < list.Count)
			{
				MigrationFile migrationFile = list[i];
				string text = (migrationFile.StudentNumber ?? "").Trim().ToUpper();
				int j = i;
				List<string> list3 = new List<string>();
				while (j < list.Count)
				{
					MigrationFile migrationFile2 = list[j];
					string a = (migrationFile2.StudentNumber ?? "").Trim().ToUpper();
					bool flag = a != text;
					if (flag)
					{
						break;
					}
					list3.Add(migrationFile2.FilenameWithPath);
					j++;
				}
				bool flag2 = !string.IsNullOrEmpty(text);
				PersonBase personBase;
				if (flag2)
				{
					personBase = (dictionary.ContainsKey(text) ? dictionary[text] : null);
					bool flag3 = personBase == null;
					if (flag3)
					{
						personBase = this.adminPeopleManager.LoadAnyNonDeletedAccountByStudentNumber(text);
						bool flag4 = personBase != null;
						if (flag4)
						{
							dictionary.Add(text, personBase);
						}
					}
				}
				else
				{
					personBase = null;
				}
				List<string> list4 = new List<string>();
				List<MigrationFileInfo> list5 = new List<MigrationFileInfo>();
				foreach (string text2 in list3)
				{
					DataMigrationManager.<>c__DisplayClass26_0 CS$<>8__locals1 = new DataMigrationManager.<>c__DisplayClass26_0();
					CS$<>8__locals1.filenameWithoutPath = Path.GetFileName(text2);
					bool flag5 = !list4.Any((string g) => g.Equals(CS$<>8__locals1.filenameWithoutPath, StringComparison.OrdinalIgnoreCase));
					if (flag5)
					{
						list5.Add(new MigrationFileInfo
						{
							FileNameWithPath = text2,
							UniqueFilenameWithoutPath = CS$<>8__locals1.filenameWithoutPath
						});
					}
					else
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(CS$<>8__locals1.filenameWithoutPath);
						string extension = Path.GetExtension(CS$<>8__locals1.filenameWithoutPath);
						int num = 1;
						CS$<>8__locals1.possibleFilename = fileNameWithoutExtension + "_" + num.ToString() + extension;
						for (;;)
						{
							IEnumerable<string> source = list4;
							Func<string, bool> predicate;
							if ((predicate = CS$<>8__locals1.<>9__2) == null)
							{
								predicate = (CS$<>8__locals1.<>9__2 = ((string g) => g.Equals(CS$<>8__locals1.possibleFilename, StringComparison.OrdinalIgnoreCase)));
							}
							if (!source.Any(predicate))
							{
								break;
							}
							DataMigrationManager.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
							string str = fileNameWithoutExtension;
							string str2 = "_";
							int num2;
							num = (num2 = num + 1);
							CS$<>8__locals2.possibleFilename = str + str2 + num2.ToString() + extension;
						}
						list5.Add(new MigrationFileInfo
						{
							FileNameWithPath = text2,
							UniqueFilenameWithoutPath = CS$<>8__locals1.possibleFilename
						});
					}
				}
				list2.Add(new MigrationFileInternal
				{
					StudentNumber = text,
					PersonId = ((personBase != null) ? personBase.PersonId : 0),
					FilesForStudent = list5
				});
				i = j;
			}
			return list2;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004FEC4 File Offset: 0x0004E0C4
		private List<MigrationAppointmentInternal> PreProcessAppointments(IList<MigrationAppointment> appointments)
		{
			List<MigrationAppointmentInternal> list = new List<MigrationAppointmentInternal>();
			Dictionary<string, PersonBase> dictionary = new Dictionary<string, PersonBase>();
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<AppType> list2 = appointmentTypeManager.LoadAllAppTypes();
			Dictionary<string, AppType> dictionary2 = new Dictionary<string, AppType>();
			foreach (AppType appType in list2)
			{
				bool flag = !dictionary2.ContainsKey(appType.Description ?? "");
				if (flag)
				{
					dictionary2.Add(appType.Description ?? "", appType);
				}
			}
			foreach (MigrationAppointment migrationAppointment in appointments)
			{
				bool flag2 = !string.IsNullOrEmpty(migrationAppointment.StaffId);
				PersonBase personBase;
				if (flag2)
				{
					personBase = (dictionary.ContainsKey(migrationAppointment.StaffId) ? dictionary[migrationAppointment.StaffId] : null);
					bool flag3 = personBase == null;
					if (flag3)
					{
						personBase = this.adminPeopleManager.LoadAnyNonDeletedAccountByStudentNumber(migrationAppointment.StaffId);
						bool flag4 = personBase != null;
						if (flag4)
						{
							dictionary.Add(migrationAppointment.StaffId, personBase);
						}
					}
				}
				else
				{
					personBase = null;
				}
				bool flag5 = !string.IsNullOrEmpty(migrationAppointment.StudentId);
				PersonBase personBase2;
				if (flag5)
				{
					personBase2 = (dictionary.ContainsKey(migrationAppointment.StudentId) ? dictionary[migrationAppointment.StudentId] : null);
					bool flag6 = personBase2 == null;
					if (flag6)
					{
						personBase2 = this.adminPeopleManager.LoadAnyNonDeletedAccountByStudentNumber(migrationAppointment.StudentId);
						bool flag7 = personBase2 != null;
						if (flag7)
						{
							dictionary.Add(migrationAppointment.StudentId, personBase2);
						}
					}
				}
				else
				{
					personBase2 = null;
				}
				AppType appType2 = null;
				bool flag8 = !string.IsNullOrEmpty(migrationAppointment.Subject) && dictionary2.ContainsKey(migrationAppointment.Subject);
				if (flag8)
				{
					appType2 = dictionary2[migrationAppointment.Subject];
				}
				list.Add(new MigrationAppointmentInternal
				{
					DataItems = migrationAppointment.DataItems,
					StartDateTime = migrationAppointment.StartDateTime,
					EndDateTime = migrationAppointment.EndDateTime,
					Location = (migrationAppointment.Location ?? ""),
					Memo = (migrationAppointment.Memo ?? ""),
					StaffId = (migrationAppointment.StaffId ?? ""),
					StudentId = (migrationAppointment.StudentId ?? ""),
					Subject = (migrationAppointment.Subject ?? ""),
					IsCancelled = migrationAppointment.IsCancelled,
					IsPrivate = migrationAppointment.IsPrivate,
					IsNoShow = migrationAppointment.IsNoShow,
					IsTentative = migrationAppointment.IsTentative,
					ClockWorkStaff = personBase,
					ClockWorkStudent = personBase2,
					AppType = appType2
				});
			}
			return list;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000501F8 File Offset: 0x0004E3F8
		private ILookupCourseManager LookupCourseManager
		{
			get
			{
				ILookupCourseManager result;
				if ((result = this._lookupCourseManager) == null)
				{
					result = (this._lookupCourseManager = new LookupCourseManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00050224 File Offset: 0x0004E424
		private IDataSyncCourseManager DataSyncCourseManager
		{
			get
			{
				IDataSyncCourseManager result;
				if ((result = this._dataSyncCourseManager) == null)
				{
					result = (this._dataSyncCourseManager = new DataSyncCourseManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00050250 File Offset: 0x0004E450
		private int LookupCourse(MigrationCourse course)
		{
			DataSyncExternalCourse externalCourse = course.ExternalCourse;
			LookupCourse lookupCourse = this.DataSyncCourseManager.FindLookupCourse(externalCourse);
			bool flag = lookupCourse != null && lookupCourse.LuCourseId > 0;
			int result;
			if (flag)
			{
				result = lookupCourse.LuCourseId;
			}
			else
			{
				List<LookupInstructor> list = new List<LookupInstructor>();
				bool flag2 = externalCourse.Instructors != null;
				if (flag2)
				{
					foreach (DataSyncExternalCourseInstructor externalProf in from p in externalCourse.Instructors
					where !string.IsNullOrEmpty((p != null) ? p.Name : null)
					select p)
					{
						bool flag3;
						LookupInstructor lookupInstructor = this.DataSyncCourseManager.FindInstructorCreateIfNecessary(externalProf, out flag3);
						bool flag4 = lookupInstructor != null && lookupInstructor.InstructorId > 0;
						if (flag4)
						{
							list.Add(lookupInstructor);
						}
					}
				}
				bool flag5;
				LookupSubject lookupSubject = this.DataSyncCourseManager.FindSubjectCreateIfNecessary(externalCourse.Subject ?? "", "", out flag5);
				int subjectId = (lookupSubject != null) ? lookupSubject.SubjectId : 0;
				lookupCourse = this.LookupCourseManager.CreateLookupCourseFromExternalCourse(externalCourse, subjectId, list);
				result = ((lookupCourse != null) ? lookupCourse.LuCourseId : 0);
			}
			return result;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0005039C File Offset: 0x0004E59C
		public IList<MigrationCreateStudentResult> CreateStudents(bool PreviewOnly, IList<MigrationStudent> MigrationStudents)
		{
			List<MigrationCreateStudentResult> list = new List<MigrationCreateStudentResult>();
			foreach (MigrationStudent migrationStudent in MigrationStudents)
			{
				string text = (migrationStudent.StudentNumber ?? "").Trim().ToUpper();
				bool flag = text.Length > 0;
				if (flag)
				{
					MigrationStudentInternal migrationStudentInternal = this.PreProcessStudent(migrationStudent);
					bool flag2 = migrationStudentInternal.ClockWorkStudent != null && migrationStudentInternal.ClockWorkStudent.PersonId > 0;
					if (flag2)
					{
						list.Add(new MigrationCreateStudentResult
						{
							StudentNumber = text,
							Status = (eMigrationCreateStudentStatus.Successful | eMigrationCreateStudentStatus.StudentAlreadyExistsInClockWork)
						});
					}
					else
					{
						bool flag3 = !PreviewOnly;
						int num;
						if (flag3)
						{
							num = this.peopleManager.CreateUser(new PersonBase
							{
								Student_no = text,
								FirstName = migrationStudentInternal.FirstName,
								MiddleName = migrationStudentInternal.MiddleName,
								LastName = migrationStudentInternal.LastName
							}, migrationStudentInternal.ClockWorkGroupIds.ToList<int>());
						}
						else
						{
							num = 9999999;
						}
						bool flag4 = num > 0;
						if (flag4)
						{
							list.Add(new MigrationCreateStudentResult
							{
								StudentNumber = text,
								PersonId = num,
								Status = eMigrationCreateStudentStatus.Successful
							});
						}
						else
						{
							list.Add(new MigrationCreateStudentResult
							{
								StudentNumber = text,
								Status = (eMigrationCreateStudentStatus.Failed | eMigrationCreateStudentStatus.FailedToCreatePersonInClockWorkDatabase)
							});
						}
					}
				}
				else
				{
					list.Add(new MigrationCreateStudentResult
					{
						StudentNumber = text,
						Status = (eMigrationCreateStudentStatus.Failed | eMigrationCreateStudentStatus.MissingStudentNumber)
					});
				}
			}
			return list;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00050558 File Offset: 0x0004E758
		public DataTable GetMigrationDataFromTable(DataTable table, out IList<MigrationMapperDataItem> dataMapper, out IList<MigrationStudentWithData> studentsWithPerStudentData, string mappingsExternalNameEqualsCidCommaSeparated, string groupIdsCommaSeparatedColName = null)
		{
			dataMapper = new List<MigrationMapperDataItem>();
			string[] array = mappingsExternalNameEqualsCidCommaSeparated.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				string text2 = text.Substring(0, num).Trim();
				string s = text.Substring(num + 1).Trim();
				int clockWorkCid;
				int.TryParse(s, out clockWorkCid);
				MigrationMapperDataItem migrationMapperDataItem = new MigrationMapperDataItem();
				bool flag = text2.IndexOf(',') >= 0;
				if (flag)
				{
					List<string> dataNames = (from g in text2.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries)
					select g.Trim()).ToList<string>();
					migrationMapperDataItem.Add(dataNames, clockWorkCid);
				}
				else
				{
					migrationMapperDataItem.Add(text2, clockWorkCid);
				}
				dataMapper.Add(migrationMapperDataItem);
			}
			studentsWithPerStudentData = new List<MigrationStudentWithData>();
			foreach (object obj in table.Rows)
			{
				DataRow dr = (DataRow)obj;
				MigrationStudent migrationStudentFromDataRow = MigrationStudent.GetMigrationStudentFromDataRow(dr, groupIdsCommaSeparatedColName);
				bool flag2 = migrationStudentFromDataRow != null;
				if (flag2)
				{
					studentsWithPerStudentData.Add(MigrationStudentWithData.GetMigrationStudentWithDataFromDataRowUsingMapperItems(dr, migrationStudentFromDataRow, dataMapper));
				}
			}
			return table;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000506DC File Offset: 0x0004E8DC
		public IList<MigrationDataItemResult> MigrateStudentData(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithData> StudentsWithPerStudentData, bool clearExistingDataWhenMigrationDataIsEmpty)
		{
			List<MigrationMapperDataItemInternal> source = this.PreProcessMapperItems(DataMapper);
			foreach (MigrationStudentWithData migrationStudentWithData in StudentsWithPerStudentData)
			{
				MigrationStudentInternal student = this.PreProcessStudent(migrationStudentWithData.Student);
				migrationStudentWithData.Student = student;
			}
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			IDictionary<int, int[]> dictionary;
			if (!clearExistingDataWhenMigrationDataIsEmpty)
			{
				dictionary = dynamicDataDAO.LoadAllPersonIdsAndControlIdsWithDataForPerStudentData((from g in DataMapper
				select g.ClockWorkCid).Distinct<int>().ToArray<int>());
			}
			else
			{
				IDictionary<int, int[]> dictionary2 = new Dictionary<int, int[]>();
				dictionary = dictionary2;
			}
			IDictionary<int, int[]> dictionary3 = dictionary;
			CWLogger.Logger.Debug("MigrateStudentData:CompletedPreProcessing:existingPsDataStudentCount={0}", dictionary3.Count.ToString());
			List<MigrationDataItemResult> list = new List<MigrationDataItemResult>();
			int count = StudentsWithPerStudentData.Count;
			for (int i = 0; i < count; i++)
			{
				MigrationStudentWithData migrationStudentWithData2 = StudentsWithPerStudentData[i];
				MigrationStudentInternal migrationStudentInternal = (MigrationStudentInternal)migrationStudentWithData2.Student;
				bool flag = migrationStudentInternal.ClockWorkStudent == null || migrationStudentInternal.ClockWorkStudent.PersonId < 1;
				if (flag)
				{
					list.Add(new MigrationDataItemResult
					{
						StudentNumber = (migrationStudentWithData2.Student.StudentNumber ?? ""),
						Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingStudent)
					});
				}
				else
				{
					bool flag2 = migrationStudentWithData2.DataItems.Count > 0;
					if (flag2)
					{
						DynamicDataContext dynamicDataContext = new DynamicDataContext
						{
							PrimaryId = migrationStudentInternal.ClockWorkStudent.PersonId
						};
						using (IEnumerator<MigrationDataItem> enumerator2 = migrationStudentWithData2.DataItems.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								MigrationDataItem item = enumerator2.Current;
								MigrationDataItemResult migrationDataItemResult = new MigrationDataItemResult
								{
									DataItemName = item.DataName,
									DataItemValue = ((item.DataValue == null) ? "NULL" : (item.DataValue.GetType().ToString() + " - " + item.DataValue.ToString())),
									StudentNumber = migrationStudentWithData2.Student.StudentNumber
								};
								MigrationMapperDataItemInternal mapperItem = source.FirstOrDefault((MigrationMapperDataItemInternal f) => f.DataNamesOrdered != null && f.DataNamesOrdered.Count > 0 && f.DataNamesOrdered[0].Equals(item.DataName, StringComparison.OrdinalIgnoreCase));
								bool flag3 = mapperItem == null;
								if (flag3)
								{
									migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingMapper);
								}
								else
								{
									bool flag4 = mapperItem.ClockWorkField == null;
									if (flag4)
									{
										migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingClockWorkField);
									}
									else
									{
										bool flag5 = dictionary3.ContainsKey(dynamicDataContext.PrimaryId) && dictionary3[dynamicDataContext.PrimaryId].Any((int g) => g == mapperItem.ClockWorkField.ControlId);
										if (flag5)
										{
											migrationDataItemResult.Status = (eMigrationDataItemStatus.Successful | eMigrationDataItemStatus.SuccessfulSkippedBecauseDataAlreadyExistsInClockWork);
										}
										else
										{
											try
											{
												this.WriteDynamicData(PreviewOnly, ref migrationDataItemResult, mapperItem, item, dynamicDataContext, eDynamicFormType.PerStudent, clearExistingDataWhenMigrationDataIsEmpty);
											}
											catch (Exception ex)
											{
												migrationDataItemResult.Status = eMigrationDataItemStatus.Failed;
												migrationDataItemResult.ErrorMessage = "WriteDynamicData failed:mapperItem.cid=" + mapperItem.ClockWorkCid.ToString() + ":" + ex.ToString();
											}
										}
									}
								}
								list.Add(migrationDataItemResult);
							}
						}
					}
				}
				bool flag6 = i % 100 == 0;
				if (flag6)
				{
					CWLogger.Logger.Debug("MigrateStudentData:Processing(step={0})...:Item {1} of {2}", 100.ToString(), i.ToString(), count.ToString());
				}
			}
			return list;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00050AE0 File Offset: 0x0004ECE0
		private string FileListControlValueToString(IList<DataMigrationManager.FileListControlColumn> columns, IList<DataMigrationManager.FileListControlValueRow> rows)
		{
			bool flag = rows == null || rows.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				char c = '\t';
				char colDel = '\0';
				string text = string.Join(c.ToString(), rows.Select(delegate(DataMigrationManager.FileListControlValueRow row)
				{
					DataMigrationManager.FileListControlValueItem[] items = row.Items;
					string[] array = new string[columns.Count];
					for (int i = 0; i < columns.Count; i++)
					{
						bool flag2 = i >= items.Length;
						if (flag2)
						{
							break;
						}
						string[] array2 = array;
						int num = i;
						string text2;
						if (!columns[i].IsFilename)
						{
							DataMigrationManager.FileListControlValueItem fileListControlValueItem = items[i];
							text2 = (((fileListControlValueItem != null) ? fileListControlValueItem.Text : null) ?? "");
						}
						else
						{
							DataMigrationManager.FileListControlValueItem fileListControlValueItem2 = items[i];
							object arg = ((fileListControlValueItem2 != null) ? fileListControlValueItem2.Text : null) ?? "";
							object arg2 = ":";
							DataMigrationManager.FileListControlValueItem fileListControlValueItem3 = items[i];
							text2 = arg + arg2 + ((fileListControlValueItem3 != null) ? fileListControlValueItem3.FileId : 0);
						}
						array2[num] = text2;
					}
					return string.Join(colDel.ToString(), array.ToArray<string>());
				}).ToArray<string>());
				result = text;
			}
			return result;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00050B4C File Offset: 0x0004ED4C
		private void UpdateFileListControlValue(int pid, int cid, DataMigrationManager.FileListControlValue val)
		{
			DataMigrationManager.FileListControl fileListControl = this.GetFileListControl(cid);
			IList<DataMigrationManager.FileListControlColumn> columns = fileListControl.Columns;
			IList<DataMigrationManager.FileListControlValueRow> list = ((val != null) ? val.Rows : null) ?? new List<DataMigrationManager.FileListControlValueRow>();
			bool flag = list.Count < 1;
			if (!flag)
			{
				string value = this.FileListControlValueToString(columns, list);
				DynamicData dynamicData = new DynamicData
				{
					Field = fileListControl.Field,
					DataId = val.DataId,
					Value = value
				};
				this.dynamicDataManager.SaveData(new DynamicDataContext
				{
					PrimaryId = pid
				}, new DynamicData[]
				{
					dynamicData
				}.ToList<DynamicData>(), eDynamicFormType.PerStudent);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00050BEC File Offset: 0x0004EDEC
		private DataMigrationManager.FileListControl GetFileListControl(int cid)
		{
			bool flag = this._fileListControlCache == null;
			if (flag)
			{
				this._fileListControlCache = new Dictionary<int, DataMigrationManager.FileListControl>();
			}
			bool flag2 = !this._fileListControlCache.ContainsKey(cid);
			DataMigrationManager.FileListControl result;
			if (flag2)
			{
				DynamicField dynamicField = this.dynamicFieldManager.LoadFieldByControlId(cid);
				int setting = dynamicField.Setting1;
				List<DynamicListItem> source = (setting > 0) ? this.dynamicFieldManager.LoadListItems(setting) : new List<DynamicListItem>();
				List<DataMigrationManager.FileListControlColumn> list = (from g in source
				select new DataMigrationManager.FileListControlColumn
				{
					Title = g.LookupText
				}).ToList<DataMigrationManager.FileListControlColumn>();
				list.Add(new DataMigrationManager.FileListControlColumn
				{
					Title = "Date",
					IsDate = true
				});
				list.Add(new DataMigrationManager.FileListControlColumn
				{
					Title = "Filename",
					IsFilename = true
				});
				DataMigrationManager.FileListControl fileListControl = new DataMigrationManager.FileListControl
				{
					Field = dynamicField,
					Columns = list
				};
				this._fileListControlCache.Add(cid, fileListControl);
				result = fileListControl;
			}
			else
			{
				result = this._fileListControlCache[cid];
			}
			return result;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00050D04 File Offset: 0x0004EF04
		private IDynamicDataManager dynamicDataManager
		{
			get
			{
				IDynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00050D30 File Offset: 0x0004EF30
		private bool AreRowsTheSame(IList<DataMigrationManager.FileListControlColumn> columns, DataMigrationManager.FileListControlValueRow row1, DataMigrationManager.FileListControlValueRow row2)
		{
			DataMigrationManager.FileListControlValueItem[] array = ((row1 != null) ? row1.Items : null) ?? new DataMigrationManager.FileListControlValueItem[columns.Count];
			DataMigrationManager.FileListControlValueItem[] array2 = ((row2 != null) ? row2.Items : null) ?? new DataMigrationManager.FileListControlValueItem[columns.Count];
			for (int i = 0; i < columns.Count; i++)
			{
				string text;
				if (i >= array.Length)
				{
					text = "";
				}
				else
				{
					DataMigrationManager.FileListControlValueItem fileListControlValueItem = array[i];
					text = (((fileListControlValueItem != null) ? fileListControlValueItem.Text : null) ?? "").Trim();
				}
				string text2 = text;
				string text3;
				if (i >= array2.Length)
				{
					text3 = "";
				}
				else
				{
					DataMigrationManager.FileListControlValueItem fileListControlValueItem2 = array2[i];
					text3 = (((fileListControlValueItem2 != null) ? fileListControlValueItem2.Text : null) ?? "").Trim();
				}
				string value = text3;
				bool flag = !text2.Equals(value, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00050E08 File Offset: 0x0004F008
		private DataMigrationManager.FileListControlValue LoadExistingFileListControlValue(int pid, int cid)
		{
			DataMigrationManager.FileListControl fileListControl = this.GetFileListControl(cid);
			IList<DataMigrationManager.FileListControlColumn> columns = fileListControl.Columns;
			DataMigrationManager.FileListControlValue fileListControlValue = new DataMigrationManager.FileListControlValue
			{
				Columns = columns,
				Rows = new List<DataMigrationManager.FileListControlValueRow>()
			};
			List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
			{
				PrimaryId = pid
			}, new int[]
			{
				cid
			}.ToList<int>(), eDynamicFormType.PerStudent);
			bool flag = list == null || list.Count < 1;
			DataMigrationManager.FileListControlValue result;
			if (flag)
			{
				result = fileListControlValue;
			}
			else
			{
				fileListControlValue.DataId = list[0].DataId;
				string text = list[0].Value.ToString();
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					result = fileListControlValue;
				}
				else
				{
					char c = '\t';
					char c2 = '\0';
					List<string> list2 = (from g in text.Split(new char[]
					{
						c
					})
					select g.Trim() into h
					where h.Length > 0
					select h).ToList<string>();
					fileListControlValue.Rows = new List<DataMigrationManager.FileListControlValueRow>();
					for (int i = 0; i < list2.Count; i++)
					{
						string text2 = list2[i];
						List<string> list3 = (from g in text2.Split(new char[]
						{
							c2
						})
						select g.Trim()).ToList<string>();
						DataMigrationManager.FileListControlValueRow fileListControlValueRow = new DataMigrationManager.FileListControlValueRow
						{
							Items = new DataMigrationManager.FileListControlValueItem[fileListControlValue.Columns.Count]
						};
						for (int j = 0; j < columns.Count; j++)
						{
							bool flag3 = j >= list3.Count;
							if (flag3)
							{
								break;
							}
							bool isFilename = columns[j].IsFilename;
							if (isFilename)
							{
								int num = list3[j].LastIndexOf(':');
								bool flag4 = num > 0;
								int fileId;
								if (flag4)
								{
									string s = list3[j].Substring(num + 1).Trim();
									bool flag5 = !int.TryParse(s, out fileId);
									if (flag5)
									{
										fileId = 0;
									}
									list3[j] = list3[j].Substring(0, num).Trim();
								}
								else
								{
									fileId = 0;
								}
								fileListControlValueRow.Items[j] = new DataMigrationManager.FileListControlValueItem
								{
									Text = list3[j],
									FileId = fileId
								};
							}
							else
							{
								fileListControlValueRow.Items[j] = new DataMigrationManager.FileListControlValueItem
								{
									Text = list3[j]
								};
							}
						}
						fileListControlValue.Rows.Add(fileListControlValueRow);
					}
					result = fileListControlValue;
				}
			}
			return result;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public IList<MigrationDataItemResult> MigrateStudentPerDateData(bool PreviewOnly, int perDateScreenNum, string titleKeyName, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithPerDateData> StudentsWithPerDateData, bool clearExistingDataWhenMigrationDataIsEmpty)
		{
			IDynamicPerDateDataManager dynamicPerDateDataManager = new DynamicPerDateDataManager(this.OpContext);
			List<MigrationMapperDataItemInternal> list = this.PreProcessMapperItems(DataMapper);
			foreach (MigrationStudentWithPerDateData migrationStudentWithPerDateData in StudentsWithPerDateData)
			{
				MigrationStudentInternal student = this.PreProcessStudent(migrationStudentWithPerDateData.Student);
				migrationStudentWithPerDateData.Student = student;
			}
			List<MigrationStudentWithPerDateData> list2 = StudentsWithPerDateData.ToList<MigrationStudentWithPerDateData>();
			list2.Sort((MigrationStudentWithPerDateData m1, MigrationStudentWithPerDateData m2) => (m1.Student.StudentNumber + m1.DateKey).CompareTo(m2.Student.StudentNumber + m2.DateKey));
			List<MigrationDataItemResult> list3 = new List<MigrationDataItemResult>();
			int i = 0;
			int count = list2.Count;
			Func<MigrationDataItem, bool> <>9__1;
			while (i < count)
			{
				MigrationStudentWithPerDateData migrationStudentWithPerDateData2 = list2[i];
				MigrationStudentInternal migrationStudentInternal = (MigrationStudentInternal)migrationStudentWithPerDateData2.Student;
				bool flag = migrationStudentInternal.ClockWorkStudent == null || migrationStudentInternal.ClockWorkStudent.PersonId < 1;
				if (flag)
				{
					list3.Add(new MigrationDataItemResult
					{
						StudentNumber = (migrationStudentWithPerDateData2.Student.StudentNumber ?? ""),
						Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingStudent)
					});
					i++;
				}
				else
				{
					bool flag2 = migrationStudentWithPerDateData2.DataItems.Count > 0;
					if (flag2)
					{
						DynamicDataContext dynamicDataContext = new DynamicDataContext
						{
							PrimaryId = migrationStudentInternal.ClockWorkStudent.PersonId
						};
						int j;
						for (j = i; j < list2.Count; j++)
						{
							MigrationStudentWithPerDateData migrationStudentWithPerDateData3 = list2[j];
							MigrationStudentInternal migrationStudentInternal2 = (MigrationStudentInternal)migrationStudentWithPerDateData3.Student;
							bool flag3 = migrationStudentInternal2.ClockWorkStudent == null || migrationStudentInternal.ClockWorkStudent.PersonId != migrationStudentInternal2.ClockWorkStudent.PersonId;
							if (flag3)
							{
								break;
							}
							DateTime date = migrationStudentWithPerDateData3.DateKey.Date;
							PerDateEntry existingPerDateEntry = dynamicPerDateDataManager.GetExistingPerDateEntry(dynamicDataContext.PrimaryId, perDateScreenNum, new Session
							{
								StartDate = date,
								EndDate = date.AddDays(1.0).AddMinutes(-1.0)
							});
							bool flag4 = existingPerDateEntry == null;
							int num2;
							if (flag4)
							{
								IEnumerable<MigrationDataItem> dataItems = migrationStudentWithPerDateData3.DataItems;
								Func<MigrationDataItem, bool> predicate;
								if ((predicate = <>9__1) == null)
								{
									predicate = (<>9__1 = ((MigrationDataItem f) => f.DataName.Equals(titleKeyName ?? "", StringComparison.OrdinalIgnoreCase)));
								}
								MigrationDataItem migrationDataItem = dataItems.FirstOrDefault(predicate);
								string description = ((migrationDataItem != null) ? migrationDataItem.DataValue.ToString() : null) ?? string.Empty;
								string text = migrationStudentWithPerDateData3.WhoEnteredStudent_no ?? "";
								int num = migrationStudentWithPerDateData3.WhoEnterePersonId;
								bool flag5 = num < 1 && text.Length > 0;
								if (flag5)
								{
									PersonBase personBase = this.adminPeopleManager.LoadAnyNonDeletedAccountByStudentNumber(text);
									bool flag6 = personBase != null;
									if (flag6)
									{
										num = personBase.PersonId;
									}
								}
								bool flag7 = num < 1;
								if (flag7)
								{
									CWLogger.Logger.Debug("MigrateStudentPerDateData:MissingWhoEntered:whoEnteredStudent_no={0}:dateKey={1}:snum={2}", migrationStudentWithPerDateData3.WhoEnteredStudent_no ?? "NULL", date.ToString(), migrationStudentInternal2.StudentNumber ?? "");
								}
								bool flag8 = !PreviewOnly;
								if (flag8)
								{
									IDynamicPerDateDataManager dynamicPerDateDataManager2 = dynamicPerDateDataManager;
									PerDateEntry perDateEntry = new PerDateEntry();
									perDateEntry.DateEntered = date;
									perDateEntry.Description = description;
									perDateEntry.Student = new PersonBase
									{
										PersonId = dynamicDataContext.PrimaryId
									};
									perDateEntry.ScreenNum = perDateScreenNum;
									object whoEntered;
									if (num <= 0)
									{
										whoEntered = null;
									}
									else
									{
										(whoEntered = new PersonBase()).PersonId = num;
									}
									perDateEntry.WhoEntered = whoEntered;
									num2 = dynamicPerDateDataManager2.CreatePerDateEntry(perDateEntry);
								}
								else
								{
									num2 = -1;
								}
								CWLogger.Logger.Debug("MigrateStudentPerDateData:CreateNewExistingPerDateEntry:j={0}:perDateEntryId={1}:dateKey={2}:snum={3}", new object[]
								{
									j.ToString(),
									num2.ToString(),
									date.ToString(),
									migrationStudentInternal2.StudentNumber ?? ""
								});
							}
							else
							{
								num2 = existingPerDateEntry.AppointmentId;
								CWLogger.Logger.Debug("MigrateStudentPerDateData:FoundExistingPerDateEntry:j={0}:perDateEntryId={1}:dateKey={2}:snum={3}", new object[]
								{
									j.ToString(),
									num2.ToString(),
									date.ToString(),
									migrationStudentInternal2.StudentNumber ?? ""
								});
							}
							bool flag9 = num2 > 0;
							if (flag9)
							{
								dynamicDataContext.SecondaryId = num2;
								using (IEnumerator<MigrationDataItem> enumerator2 = migrationStudentWithPerDateData3.DataItems.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										MigrationDataItem dataItem = enumerator2.Current;
										bool flag10 = dataItem.DataName.Equals(titleKeyName, StringComparison.OrdinalIgnoreCase);
										if (!flag10)
										{
											MigrationDataItemResult migrationDataItemResult = new MigrationDataItemResult
											{
												DataItemName = dataItem.DataName,
												StudentNumber = migrationStudentWithPerDateData2.Student.StudentNumber
											};
											Func<string, bool> <>9__3;
											MigrationMapperDataItemInternal migrationMapperDataItemInternal = list.Find(delegate(MigrationMapperDataItemInternal f)
											{
												IEnumerable<string> dataNamesOrdered = f.DataNamesOrdered;
												Func<string, bool> predicate2;
												if ((predicate2 = <>9__3) == null)
												{
													predicate2 = (<>9__3 = ((string g) => g.Equals(dataItem.DataName, StringComparison.OrdinalIgnoreCase)));
												}
												return dataNamesOrdered.FirstOrDefault(predicate2) != null;
											});
											bool flag11 = migrationMapperDataItemInternal == null;
											if (flag11)
											{
												migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingMapper);
											}
											else
											{
												bool flag12 = migrationMapperDataItemInternal.ClockWorkField == null;
												if (flag12)
												{
													migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingClockWorkField);
												}
												else
												{
													this.WriteDynamicData(PreviewOnly, ref migrationDataItemResult, migrationMapperDataItemInternal, dataItem, dynamicDataContext, eDynamicFormType.PerDate, clearExistingDataWhenMigrationDataIsEmpty);
												}
											}
											bool flag13 = (migrationDataItemResult.Status & eMigrationDataItemStatus.Successful) <= eMigrationDataItemStatus.Unknown;
											if (flag13)
											{
												list3.Add(migrationDataItemResult);
											}
										}
									}
								}
							}
							else
							{
								list3.Add(new MigrationDataItemResult
								{
									Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.CantFindOrCreatePerDateEntryId),
									StudentNumber = migrationStudentInternal.StudentNumber
								});
							}
						}
						i = j;
					}
					else
					{
						i++;
					}
				}
				bool flag14 = i % 100 == 0;
				if (flag14)
				{
					CWLogger.Logger.Debug("MigrateStudentPerDateData:Processing(step={0})...:Item {1} of {2}", 100.ToString(), i.ToString(), count.ToString());
				}
			}
			return list3;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000516F4 File Offset: 0x0004F8F4
		public IList<MigrationExternalCourseResult> MigrateCourses(bool PreviewOnly, IList<MigrationStudentWithCourses> StudentsWithCourses)
		{
			foreach (MigrationStudentWithCourses migrationStudentWithCourses in StudentsWithCourses)
			{
				MigrationStudentInternal student = this.PreProcessStudent(migrationStudentWithCourses.Student);
				migrationStudentWithCourses.Student = student;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<MigrationTestExamItemResult> MigrateTestsAndExams(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapperForAccommodations, IList<MigrationTestExam> TestsAndExams, bool AvoidDuplicateAppointmentsEnabled = true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00051754 File Offset: 0x0004F954
		public void GetMigrationDataFromTable(DataTable table, out IList<MigrationMapperDataItem> dataMapper, out IList<MigrationAppointment> apps, string mappingsExternalNameEqualsCidCommaSeparated, string groupIdsCommaSeparatedColName = null)
		{
			dataMapper = new List<MigrationMapperDataItem>();
			string[] array = mappingsExternalNameEqualsCidCommaSeparated.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				MigrationMapperDataItem migrationMapperDataItem = new MigrationMapperDataItem();
				int clockWorkCid;
				int.TryParse(text.Substring(num + 1).Trim(), out clockWorkCid);
				migrationMapperDataItem.Add(text.Substring(0, num).Trim(), clockWorkCid);
				dataMapper.Add(migrationMapperDataItem);
			}
			apps = new List<MigrationAppointment>();
			foreach (object obj in table.Rows)
			{
				DataRow dr = (DataRow)obj;
				MigrationAppointment migrationAppointmentFromDataRow = MigrationAppointment.GetMigrationAppointmentFromDataRow(dr, dataMapper);
				bool flag = migrationAppointmentFromDataRow != null;
				if (flag)
				{
					apps.Add(migrationAppointmentFromDataRow);
				}
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00051858 File Offset: 0x0004FA58
		private bool AppsMatch(BaseExtendedAppointment app1, BaseExtendedAppointment app2)
		{
			bool flag = app1.StartDateTime != app2.StartDateTime;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IEnumerable<Attendee> source = from g in app1.Attendees
				where app2.Attendees.FirstOrDefault((Attendee h) => h.Person.PersonId == g.Person.PersonId) == null
				select g;
				bool flag2 = source.Any<Attendee>();
				if (flag2)
				{
					result = false;
				}
				else
				{
					IEnumerable<Attendee> source2 = from g in app2.Attendees
					where app1.Attendees.FirstOrDefault((Attendee h) => h.Person.PersonId == g.Person.PersonId) == null
					select g;
					bool flag3 = source2.Any<Attendee>();
					result = !flag3;
				}
			}
			return result;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00051904 File Offset: 0x0004FB04
		private bool IsAppointmentDateTimesOk(MigrationAppointmentInternal app)
		{
			return app.StartDateTime != DateTime.MinValue && app.EndDateTime != DateTime.MinValue && app.StartDateTime.Year > 1940 && app.EndDateTime.Year > 1940 && app.EndDateTime.Date >= app.StartDateTime.Date;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00051988 File Offset: 0x0004FB88
		public IList<MigrationFileItemResult> MigrateFiles(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationFile> migrationFiles)
		{
			List<MigrationMapperDataItemInternal> source = this.PreProcessMapperItems(DataMapper);
			MigrationMapperDataItemInternal migrationMapperDataItemInternal = source.FirstOrDefault<MigrationMapperDataItemInternal>();
			int num = (migrationMapperDataItemInternal != null) ? migrationMapperDataItemInternal.ClockWorkCid : 0;
			bool flag = num < 1;
			if (flag)
			{
				throw new Exception("Must map filename to file list control.");
			}
			List<MigrationFileInternal> list = this.PreProcessFiles(migrationFiles);
			List<MigrationFileItemResult> list2 = new List<MigrationFileItemResult>();
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			foreach (MigrationFileInternal migrationFileInternal in list)
			{
				bool flag2 = migrationFileInternal.PersonId < 1;
				if (flag2)
				{
					list2.Add(new MigrationFileItemResult
					{
						StudentNumber = migrationFileInternal.StudentNumber,
						PersonId = migrationFileInternal.PersonId,
						Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.MissingClockWorkStudent),
						ErrorMessage = "Student doesn't exist in ClockWork ;",
						FileListContents = "",
						ExternalFiles = migrationFileInternal.FilesForStudent
					});
				}
				else
				{
					bool flag3 = migrationFileInternal.FilesForStudent == null || migrationFileInternal.FilesForStudent.Count < 1;
					if (flag3)
					{
						list2.Add(new MigrationFileItemResult
						{
							StudentNumber = migrationFileInternal.StudentNumber,
							PersonId = migrationFileInternal.PersonId,
							Status = eMigrationAppointmentItemStatus.Ignored,
							ErrorMessage = "No files available",
							FileListContents = "",
							ExternalFiles = migrationFileInternal.FilesForStudent
						});
					}
					else
					{
						DataMigrationManager.FileListControlValue val = this.LoadExistingFileListControlValue(migrationFileInternal.PersonId, num);
						bool flag4 = val.Rows == null;
						if (flag4)
						{
							val.Rows = new List<DataMigrationManager.FileListControlValueRow>();
						}
						DataMigrationManager.FileListControlColumn fileListControlColumn = val.Columns.FirstOrDefault((DataMigrationManager.FileListControlColumn g) => g.IsDate);
						DataMigrationManager.FileListControlColumn fileListControlColumn2 = val.Columns.FirstOrDefault((DataMigrationManager.FileListControlColumn g) => g.IsFilename);
						int dateColInd = (fileListControlColumn != null) ? val.Columns.IndexOf(fileListControlColumn) : -1;
						int filenameColInd = (fileListControlColumn2 != null) ? val.Columns.IndexOf(fileListControlColumn2) : -1;
						DateTime now = DateTime.Now;
						List<DataMigrationManager.FileListControlValueRow> list3 = migrationFileInternal.FilesForStudent.Select(delegate(MigrationFileInfo g)
						{
							DataMigrationManager.FileListControlValueRow fileListControlValueRow = new DataMigrationManager.FileListControlValueRow
							{
								MigrationFileInfo = g,
								Items = new DataMigrationManager.FileListControlValueItem[val.Columns.Count]
							};
							fileListControlValueRow.Items[0] = new DataMigrationManager.FileListControlValueItem
							{
								Text = "Migrated file"
							};
							bool flag8 = dateColInd > 0;
							if (flag8)
							{
								fileListControlValueRow.Items[dateColInd] = new DataMigrationManager.FileListControlValueItem
								{
									Text = now.ToString("yyyy-MM-dd")
								};
							}
							bool flag9 = filenameColInd > 0;
							if (flag9)
							{
								fileListControlValueRow.Items[filenameColInd] = new DataMigrationManager.FileListControlValueItem
								{
									Text = g.UniqueFilenameWithoutPath,
									FileId = 0
								};
							}
							return fileListControlValueRow;
						}).ToList<DataMigrationManager.FileListControlValueRow>();
						List<DataMigrationManager.FileListControlValueRow> list4 = (from g in list3
						where !val.Rows.Any((DataMigrationManager.FileListControlValueRow h) => this.AreRowsTheSame(val.Columns, g, h))
						select g).ToList<DataMigrationManager.FileListControlValueRow>();
						bool flag5 = list4.Count < 1;
						if (flag5)
						{
							list2.Add(new MigrationFileItemResult
							{
								StudentNumber = migrationFileInternal.StudentNumber,
								PersonId = migrationFileInternal.PersonId,
								Status = eMigrationAppointmentItemStatus.Ignored,
								ErrorMessage = "All files already exist in ClockWork",
								FileListContents = this.FileListControlValueToString(val.Columns, list3),
								ExternalFiles = migrationFileInternal.FilesForStudent
							});
						}
						else
						{
							var enumerable = list4.Select(delegate(DataMigrationManager.FileListControlValueRow missingRow)
							{
								MigrationFileInfo migrationFileInfo = missingRow.MigrationFileInfo;
								byte[] array = null;
								string problem = null;
								try
								{
									array = File.ReadAllBytes(migrationFileInfo.FileNameWithPath);
									bool flag8 = array == null;
									if (flag8)
									{
										problem = "Filebytes is null: " + migrationFileInfo.FileNameWithPath;
									}
								}
								catch (Exception ex2)
								{
									problem = "Couldn't load file: " + migrationFileInfo.FileNameWithPath + ": " + ex2.ToString();
								}
								return new
								{
									Problem = problem,
									BinaryFile = new BinaryFile
									{
										FileName = migrationFileInfo.UniqueFilenameWithoutPath,
										ByteArray = array
									},
									Row = missingRow
								};
							});
							var list5 = (from g in enumerable
							where !string.IsNullOrEmpty(g.Problem)
							select g).ToList();
							bool flag6 = list5.Count > 0;
							if (flag6)
							{
								List<MigrationFileItemResult> list6 = list2;
								MigrationFileItemResult migrationFileItemResult = new MigrationFileItemResult();
								migrationFileItemResult.StudentNumber = migrationFileInternal.StudentNumber;
								migrationFileItemResult.PersonId = migrationFileInternal.PersonId;
								migrationFileItemResult.Status = eMigrationAppointmentItemStatus.Failed;
								migrationFileItemResult.ErrorMessage = string.Join("\r\n", from g in list5
								select g.Problem);
								migrationFileItemResult.FileListContents = this.FileListControlValueToString(val.Columns, list3);
								migrationFileItemResult.ExternalFiles = migrationFileInternal.FilesForStudent;
								list6.Add(migrationFileItemResult);
							}
							else
							{
								bool flag7 = !PreviewOnly;
								if (flag7)
								{
									foreach (var <>f__AnonymousType in enumerable)
									{
										<>f__AnonymousType.Row.Items[filenameColInd].FileId = this.dynamicDataManager.UploadDocumentToDatabase(<>f__AnonymousType.BinaryFile, 1000);
										val.Rows.Add(<>f__AnonymousType.Row);
									}
									try
									{
										this.UpdateFileListControlValue(migrationFileInternal.PersonId, num, val);
										list2.Add(new MigrationFileItemResult
										{
											StudentNumber = migrationFileInternal.StudentNumber,
											PersonId = migrationFileInternal.PersonId,
											Status = eMigrationAppointmentItemStatus.Successful,
											ErrorMessage = "",
											FileListContents = this.FileListControlValueToString(val.Columns, val.Rows),
											ExternalFiles = migrationFileInternal.FilesForStudent
										});
									}
									catch (Exception ex)
									{
										list2.Add(new MigrationFileItemResult
										{
											StudentNumber = migrationFileInternal.StudentNumber,
											PersonId = migrationFileInternal.PersonId,
											Status = eMigrationAppointmentItemStatus.Failed,
											ErrorMessage = "Failed to save file list contents: " + ex.ToString(),
											FileListContents = this.FileListControlValueToString(val.Columns, val.Rows),
											ExternalFiles = migrationFileInternal.FilesForStudent
										});
									}
								}
								else
								{
									list2.Add(new MigrationFileItemResult
									{
										StudentNumber = migrationFileInternal.StudentNumber,
										PersonId = migrationFileInternal.PersonId,
										Status = eMigrationAppointmentItemStatus.Successful,
										ErrorMessage = "In Preview Mode",
										FileListContents = this.FileListControlValueToString(val.Columns, val.Rows),
										ExternalFiles = migrationFileInternal.FilesForStudent
									});
								}
							}
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00052018 File Offset: 0x00050218
		public IList<MigrationAppointmentItemResult> MigrateAppointments(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationAppointment> Appointments, bool clearExistingDataWhenMigrationDataIsEmpty, bool AvoidDuplicatAppointmentsEnabled = true)
		{
			List<MigrationMapperDataItemInternal> list = this.PreProcessMapperItems(DataMapper);
			List<MigrationAppointmentInternal> list2 = this.PreProcessAppointments(Appointments);
			list2.Sort((MigrationAppointmentInternal g1, MigrationAppointmentInternal g2) => g1.StartDateTime.CompareTo(g2.StartDateTime));
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			List<MigrationAppointmentItemResult> list3 = new List<MigrationAppointmentItemResult>();
			List<MigrationAppointmentInternal> list4 = (from g in list2
			where this.IsAppointmentDateTimesOk(g)
			select g).ToList<MigrationAppointmentInternal>();
			List<MigrationAppointmentInternal> source = (from g in list2
			where !this.IsAppointmentDateTimesOk(g)
			select g).ToList<MigrationAppointmentInternal>();
			list3.AddRange(from g in source
			select new MigrationAppointmentItemResult
			{
				DataItemResults = new List<MigrationDataItemResult>(),
				ExternalAppointment = new MigrationAppointment
				{
					StartDateTime = g.StartDateTime,
					EndDateTime = g.EndDateTime
				},
				Status = eMigrationAppointmentItemStatus.InvalidDateTimes
			});
			int count = list4.Count;
			CWLogger.Logger.Debug("MigrateAppointments:CompletedPreProcessing:totCount={0}", count.ToString());
			int i = 0;
			while (i < count)
			{
				MigrationAppointmentInternal migrationAppointmentInternal = list4[i];
				DateTime date = migrationAppointmentInternal.StartDateTime.Date;
				int j;
				for (j = i + 1; j < list4.Count; j++)
				{
					MigrationAppointmentInternal migrationAppointmentInternal2 = list4[j];
					DateTime date2 = migrationAppointmentInternal2.StartDateTime.Date;
					bool flag = (date2 - date).TotalDays > 14.0;
					if (flag)
					{
						break;
					}
				}
				IList<BaseExtendedAppointment> source2 = new List<BaseExtendedAppointment>();
				if (AvoidDuplicatAppointmentsEnabled)
				{
					List<int> list5 = new List<int>();
					for (int k = i; k < j; k++)
					{
						MigrationAppointmentInternal migrationAppointmentInternal3 = list4[k];
						bool flag2 = migrationAppointmentInternal3.ClockWorkStudent != null && migrationAppointmentInternal3.ClockWorkStudent.PersonId > 0 && !list5.Contains(migrationAppointmentInternal3.ClockWorkStudent.PersonId);
						if (flag2)
						{
							list5.Add(migrationAppointmentInternal3.ClockWorkStudent.PersonId);
						}
						bool flag3 = migrationAppointmentInternal3.ClockWorkStaff != null && migrationAppointmentInternal3.ClockWorkStaff.PersonId > 0 && !list5.Contains(migrationAppointmentInternal3.ClockWorkStaff.PersonId);
						if (flag3)
						{
							list5.Add(migrationAppointmentInternal3.ClockWorkStaff.PersonId);
						}
					}
					bool flag4 = list5.Count > 0;
					if (flag4)
					{
						source2 = baseAppointmentManager.LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<BaseExtendedAppointment>(migrationAppointmentInternal.StartDateTime.Date, list4[j - 1].StartDateTime.Date, list5);
					}
				}
				for (int l = i; l < j; l++)
				{
					MigrationAppointmentInternal migrationAppointmentInternal4 = list4[l];
					bool flag5 = migrationAppointmentInternal4.ClockWorkStudent == null && migrationAppointmentInternal4.ClockWorkStaff == null;
					if (flag5)
					{
						list3.Add(new MigrationAppointmentItemResult
						{
							ExternalAppointment = migrationAppointmentInternal4,
							Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.MissingClockWorkStudent | eMigrationAppointmentItemStatus.MissingClockWorkStaff),
							ErrorMessage = string.Format("StudentExternalId={0}; StaffExternalId={1};", migrationAppointmentInternal4.StudentId ?? "NULL", migrationAppointmentInternal4.StaffId ?? "NULL")
						});
					}
					else
					{
						bool flag6 = migrationAppointmentInternal4.ClockWorkStaff == null && migrationAppointmentInternal4.StaffId != null && migrationAppointmentInternal4.StaffId.Trim().Length > 0;
						if (flag6)
						{
							list3.Add(new MigrationAppointmentItemResult
							{
								ExternalAppointment = migrationAppointmentInternal4,
								Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.MissingClockWorkStaff),
								ErrorMessage = string.Format("StudentExternalId={0}; StaffExternalId={1};", migrationAppointmentInternal4.StudentId ?? "NULL", migrationAppointmentInternal4.StaffId ?? "NULL")
							});
						}
						else
						{
							bool flag7 = migrationAppointmentInternal4.ClockWorkStudent == null && migrationAppointmentInternal4.StudentId != null && migrationAppointmentInternal4.StudentId.Trim().Length > 0;
							if (flag7)
							{
								list3.Add(new MigrationAppointmentItemResult
								{
									ExternalAppointment = migrationAppointmentInternal4,
									Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.MissingClockWorkStudent),
									ErrorMessage = string.Format("StudentExternalId={0}; StaffExternalId={1};", migrationAppointmentInternal4.StudentId ?? "NULL", migrationAppointmentInternal4.StaffId ?? "NULL")
								});
							}
							else
							{
								bool flag8 = migrationAppointmentInternal4.EndDateTime < migrationAppointmentInternal4.StartDateTime || migrationAppointmentInternal4.StartDateTime.Date != migrationAppointmentInternal4.EndDateTime.Date;
								if (flag8)
								{
									list3.Add(new MigrationAppointmentItemResult
									{
										ExternalAppointment = migrationAppointmentInternal4,
										Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.InvalidDateTimes)
									});
								}
								else
								{
									DataMigrationManager.<>c__DisplayClass59_0 CS$<>8__locals1 = new DataMigrationManager.<>c__DisplayClass59_0();
									CS$<>8__locals1.<>4__this = this;
									bool flag9 = migrationAppointmentInternal4.EndDateTime == migrationAppointmentInternal4.StartDateTime;
									if (flag9)
									{
										migrationAppointmentInternal4.EndDateTime = migrationAppointmentInternal4.StartDateTime.AddHours(1.0);
									}
									DataMigrationManager.<>c__DisplayClass59_0 CS$<>8__locals2 = CS$<>8__locals1;
									BaseExtendedAppointment baseExtendedAppointment = new BaseExtendedAppointment();
									baseExtendedAppointment.AppType = migrationAppointmentInternal4.AppType;
									baseExtendedAppointment.StartDateTime = migrationAppointmentInternal4.StartDateTime;
									baseExtendedAppointment.EndDateTime = migrationAppointmentInternal4.EndDateTime;
									baseExtendedAppointment.Location = migrationAppointmentInternal4.Location;
									baseExtendedAppointment.Memo = migrationAppointmentInternal4.Memo;
									baseExtendedAppointment.Attendees = new List<Attendee>();
									baseExtendedAppointment.IsCancelled = migrationAppointmentInternal4.IsCancelled;
									baseExtendedAppointment.IsPrivate = migrationAppointmentInternal4.IsPrivate;
									baseExtendedAppointment.SubTitle = migrationAppointmentInternal4.Subject;
									object showTimeAs;
									if (!migrationAppointmentInternal4.IsTentative)
									{
										showTimeAs = null;
									}
									else
									{
										(showTimeAs = new AppShowTimeAsType()).AppCode = -1;
									}
									baseExtendedAppointment.ShowTimeAs = showTimeAs;
									CS$<>8__locals2.cwApp = baseExtendedAppointment;
									bool flag10 = migrationAppointmentInternal4.ClockWorkStudent != null;
									if (flag10)
									{
										CS$<>8__locals1.cwApp.Attendees.Add(new Attendee
										{
											Person = migrationAppointmentInternal4.ClockWorkStudent,
											IsNoShow = migrationAppointmentInternal4.IsNoShow
										});
									}
									bool flag11 = migrationAppointmentInternal4.ClockWorkStaff != null;
									if (flag11)
									{
										CS$<>8__locals1.cwApp.Attendees.Add(new Attendee
										{
											Person = migrationAppointmentInternal4.ClockWorkStaff
										});
									}
									int num;
									if (AvoidDuplicatAppointmentsEnabled)
									{
										BaseExtendedAppointment baseExtendedAppointment2 = source2.FirstOrDefault((BaseExtendedAppointment g) => CS$<>8__locals1.<>4__this.AppsMatch(g, CS$<>8__locals1.cwApp));
										num = ((baseExtendedAppointment2 == null) ? 0 : baseExtendedAppointment2.AppointmentId);
									}
									else
									{
										num = 0;
									}
									bool flag12 = num > 0;
									MigrationAppointmentItemResult migrationAppointmentItemResult;
									if (flag12)
									{
										migrationAppointmentItemResult = new MigrationAppointmentItemResult
										{
											ExternalAppointment = migrationAppointmentInternal4,
											Status = (eMigrationAppointmentItemStatus.Successful | eMigrationAppointmentItemStatus.AppAlreadyExistsInClockWork),
											ErrorMessage = "Existing AppointmentId=" + num.ToString()
										};
									}
									else
									{
										num = ((!PreviewOnly) ? this.baseAppManager.CreateBaseExtendedAppointment(false, CS$<>8__locals1.cwApp) : 9999999);
										bool flag13 = num > 0;
										if (flag13)
										{
											migrationAppointmentItemResult = new MigrationAppointmentItemResult
											{
												ExternalAppointment = migrationAppointmentInternal4,
												Status = eMigrationAppointmentItemStatus.Successful
											};
										}
										else
										{
											migrationAppointmentItemResult = new MigrationAppointmentItemResult
											{
												ExternalAppointment = migrationAppointmentInternal4,
												Status = (eMigrationAppointmentItemStatus.Failed | eMigrationAppointmentItemStatus.UnableToCreateAppInClockWorkDatabase)
											};
										}
									}
									bool flag14 = num > 0;
									if (flag14)
									{
										bool flag15 = migrationAppointmentInternal4.ClockWorkStudent != null && migrationAppointmentInternal4.DataItems != null && migrationAppointmentInternal4.DataItems.Count > 0;
										if (flag15)
										{
											migrationAppointmentItemResult.DataItemResults = new List<MigrationDataItemResult>();
											DynamicDataContext context = new DynamicDataContext
											{
												PrimaryId = migrationAppointmentInternal4.ClockWorkStudent.PersonId,
												SecondaryId = num
											};
											using (IEnumerator<MigrationDataItem> enumerator = migrationAppointmentInternal4.DataItems.GetEnumerator())
											{
												while (enumerator.MoveNext())
												{
													MigrationDataItem item = enumerator.Current;
													MigrationDataItemResult migrationDataItemResult = new MigrationDataItemResult
													{
														DataItemName = item.DataName,
														DataItemValue = ((item.DataValue == null) ? "" : item.DataValue.ToString()),
														StudentNumber = migrationAppointmentInternal4.ClockWorkStudent.Student_no
													};
													Func<string, bool> <>9__6;
													MigrationMapperDataItemInternal migrationMapperDataItemInternal = list.Find(delegate(MigrationMapperDataItemInternal f)
													{
														IEnumerable<string> dataNamesOrdered = f.DataNamesOrdered;
														Func<string, bool> predicate;
														if ((predicate = <>9__6) == null)
														{
															predicate = (<>9__6 = ((string g) => g.Equals(item.DataName, StringComparison.OrdinalIgnoreCase)));
														}
														return dataNamesOrdered.FirstOrDefault(predicate) != null;
													});
													bool flag16 = migrationMapperDataItemInternal == null;
													if (flag16)
													{
														migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingMapper);
													}
													else
													{
														bool flag17 = migrationMapperDataItemInternal.ClockWorkField == null;
														if (flag17)
														{
															migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingClockWorkField);
														}
														else
														{
															this.WriteDynamicData(PreviewOnly, ref migrationDataItemResult, migrationMapperDataItemInternal, item, context, eDynamicFormType.PerAppointment, clearExistingDataWhenMigrationDataIsEmpty);
														}
													}
													migrationAppointmentItemResult.DataItemResults.Add(migrationDataItemResult);
												}
											}
										}
									}
									list3.Add(migrationAppointmentItemResult);
								}
							}
						}
					}
				}
				i = j;
				bool flag18 = i % 100 == 0;
				if (flag18)
				{
					CWLogger.Logger.Debug("MigrateAppointments:Processing(step={0})...:Item {1} of {2}", 100.ToString(), i.ToString(), count.ToString());
				}
			}
			return list3;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000528E0 File Offset: 0x00050AE0
		public IList<MigrationDataItemResult> MigrateAccommodations(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithData> StudentsWithAccommodationData, bool clearExistingDataWhenMigrationDataIsEmpty)
		{
			List<MigrationMapperDataItemInternal> list = this.PreProcessMapperItems(DataMapper);
			foreach (MigrationStudentWithData migrationStudentWithData in StudentsWithAccommodationData)
			{
				MigrationStudentInternal student = this.PreProcessStudent(migrationStudentWithData.Student);
				migrationStudentWithData.Student = student;
			}
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			IDictionary<int, int[]> dictionary;
			if (!clearExistingDataWhenMigrationDataIsEmpty)
			{
				dictionary = dynamicDataDAO.LoadAllPersonIdsAndControlIdsWithDataForTemplateOnlyAccommodations((from g in DataMapper
				select g.ClockWorkCid).Distinct<int>().ToArray<int>());
			}
			else
			{
				IDictionary<int, int[]> dictionary2 = new Dictionary<int, int[]>();
				dictionary = dictionary2;
			}
			IDictionary<int, int[]> dictionary3 = dictionary;
			CWLogger.Logger.Debug("MigrateStudentData:CompletedPreProcessing:existingDataAccommodationTemplateOnlyStudentCount={0}", dictionary3.Count.ToString());
			List<MigrationDataItemResult> list2 = new List<MigrationDataItemResult>();
			int count = StudentsWithAccommodationData.Count;
			for (int i = 0; i < count; i++)
			{
				MigrationStudentWithData migrationStudentWithData2 = StudentsWithAccommodationData[i];
				MigrationStudentInternal migrationStudentInternal = (MigrationStudentInternal)migrationStudentWithData2.Student;
				bool flag = migrationStudentInternal.ClockWorkStudent == null || migrationStudentInternal.ClockWorkStudent.PersonId < 1;
				if (flag)
				{
					list2.Add(new MigrationDataItemResult
					{
						StudentNumber = (migrationStudentWithData2.Student.StudentNumber ?? ""),
						Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingStudent)
					});
				}
				else
				{
					bool flag2 = migrationStudentWithData2.DataItems.Count > 0;
					if (flag2)
					{
						DynamicDataContext context = new DynamicDataContext
						{
							PrimaryId = migrationStudentInternal.ClockWorkStudent.PersonId,
							SecondaryId = 0
						};
						using (IEnumerator<MigrationDataItem> enumerator2 = migrationStudentWithData2.DataItems.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								MigrationDataItem item = enumerator2.Current;
								MigrationDataItemResult migrationDataItemResult = new MigrationDataItemResult
								{
									DataItemName = item.DataName,
									StudentNumber = migrationStudentWithData2.Student.StudentNumber
								};
								Func<string, bool> <>9__2;
								MigrationMapperDataItemInternal migrationMapperDataItemInternal = list.Find(delegate(MigrationMapperDataItemInternal f)
								{
									IEnumerable<string> dataNamesOrdered = f.DataNamesOrdered;
									Func<string, bool> predicate;
									if ((predicate = <>9__2) == null)
									{
										predicate = (<>9__2 = ((string g) => g.Equals(item.DataName, StringComparison.OrdinalIgnoreCase)));
									}
									return dataNamesOrdered.FirstOrDefault(predicate) != null;
								});
								bool flag3 = migrationMapperDataItemInternal == null;
								if (flag3)
								{
									migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingMapper);
								}
								else
								{
									bool flag4 = migrationMapperDataItemInternal.ClockWorkField == null;
									if (flag4)
									{
										migrationDataItemResult.Status = (eMigrationDataItemStatus.Failed | eMigrationDataItemStatus.MissingClockWorkField);
									}
									else
									{
										this.WriteDynamicData(PreviewOnly, ref migrationDataItemResult, migrationMapperDataItemInternal, item, context, eDynamicFormType.AccommodationTemplateOnly, clearExistingDataWhenMigrationDataIsEmpty);
									}
								}
								list2.Add(migrationDataItemResult);
							}
						}
					}
					else
					{
						list2.Add(new MigrationDataItemResult
						{
							StudentNumber = (migrationStudentWithData2.Student.StudentNumber ?? ""),
							Status = eMigrationDataItemStatus.SuccessfulAndNoData
						});
					}
				}
				bool flag5 = i % 100 == 0;
				if (flag5)
				{
					CWLogger.Logger.Debug("MigrateAccommodations:Processing(step={0})...:Item {1} of {2}", 100.ToString(), i.ToString(), count.ToString());
				}
			}
			return list2;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00052BF4 File Offset: 0x00050DF4
		public void ApplyDataMapping(DataTable table, IList<DataTableColumnMapping> dataMapping)
		{
			foreach (DataTableColumnMapping dataTableColumnMapping in dataMapping)
			{
				string text = dataTableColumnMapping.ColumnName ?? "";
				bool flag = !table.Columns.Contains(text);
				if (!flag)
				{
					foreach (object obj in table.Rows)
					{
						DataRow dr = (DataRow)obj;
						foreach (DataTableItemMapping itemMap in dataTableColumnMapping.ItemMappings)
						{
							this.MapItem(dr, text, itemMap);
						}
					}
				}
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00052CFC File Offset: 0x00050EFC
		private void MapItem(DataRow dr, string colName, DataTableItemMapping itemMap)
		{
			bool flag = itemMap is DataTableItemMappingString;
			if (flag)
			{
				DataTableItemMappingString dataTableItemMappingString = (DataTableItemMappingString)itemMap;
				string text = (dr[colName] is DBNull) ? "" : dr[colName].ToString().Trim();
				bool flag2 = text.Equals(dataTableItemMappingString.OldValue);
				if (flag2)
				{
					dr[colName] = (dataTableItemMappingString.NewValue ?? "");
				}
			}
		}

		// Token: 0x04000232 RID: 562
		private IDynamicFieldManager _dynamicFieldManager;

		// Token: 0x04000233 RID: 563
		private IBaseAppointmentManager _baseAppManager;

		// Token: 0x04000234 RID: 564
		private IDynamicDataManager _dataManager;

		// Token: 0x04000235 RID: 565
		private IPeopleManager _peopleManager;

		// Token: 0x04000236 RID: 566
		private IAdminPeopleManager _adminPeopleManager;

		// Token: 0x04000237 RID: 567
		private IDictionary<string, object> tempCache = new Dictionary<string, object>();

		// Token: 0x04000238 RID: 568
		private ILookupCourseManager _lookupCourseManager;

		// Token: 0x04000239 RID: 569
		private IDataSyncCourseManager _dataSyncCourseManager;

		// Token: 0x0400023A RID: 570
		private IDictionary<int, DataMigrationManager.FileListControl> _fileListControlCache;

		// Token: 0x0400023B RID: 571
		private IDynamicDataManager _dynamicDataManager;

		// Token: 0x02000349 RID: 841
		internal class FileListControl
		{
			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06001716 RID: 5910 RVA: 0x00089F24 File Offset: 0x00088124
			// (set) Token: 0x06001717 RID: 5911 RVA: 0x00089F2C File Offset: 0x0008812C
			public DynamicField Field { get; set; }

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06001718 RID: 5912 RVA: 0x00089F35 File Offset: 0x00088135
			// (set) Token: 0x06001719 RID: 5913 RVA: 0x00089F3D File Offset: 0x0008813D
			public IList<DataMigrationManager.FileListControlColumn> Columns { get; set; }
		}

		// Token: 0x0200034A RID: 842
		internal class FileListControlColumn
		{
			// Token: 0x1700029A RID: 666
			// (get) Token: 0x0600171B RID: 5915 RVA: 0x00089F46 File Offset: 0x00088146
			// (set) Token: 0x0600171C RID: 5916 RVA: 0x00089F4E File Offset: 0x0008814E
			public string Title { get; set; }

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x0600171D RID: 5917 RVA: 0x00089F57 File Offset: 0x00088157
			// (set) Token: 0x0600171E RID: 5918 RVA: 0x00089F5F File Offset: 0x0008815F
			public bool IsDate { get; set; }

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x0600171F RID: 5919 RVA: 0x00089F68 File Offset: 0x00088168
			// (set) Token: 0x06001720 RID: 5920 RVA: 0x00089F70 File Offset: 0x00088170
			public bool IsFilename { get; set; }
		}

		// Token: 0x0200034B RID: 843
		internal class FileListControlValue
		{
			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06001722 RID: 5922 RVA: 0x00089F79 File Offset: 0x00088179
			// (set) Token: 0x06001723 RID: 5923 RVA: 0x00089F81 File Offset: 0x00088181
			public IList<DataMigrationManager.FileListControlColumn> Columns { get; set; }

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06001724 RID: 5924 RVA: 0x00089F8A File Offset: 0x0008818A
			// (set) Token: 0x06001725 RID: 5925 RVA: 0x00089F92 File Offset: 0x00088192
			public int DataId { get; set; }

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06001726 RID: 5926 RVA: 0x00089F9B File Offset: 0x0008819B
			// (set) Token: 0x06001727 RID: 5927 RVA: 0x00089FA3 File Offset: 0x000881A3
			public IList<DataMigrationManager.FileListControlValueRow> Rows { get; set; }
		}

		// Token: 0x0200034C RID: 844
		internal class FileListControlValueRow
		{
			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06001729 RID: 5929 RVA: 0x00089FAC File Offset: 0x000881AC
			// (set) Token: 0x0600172A RID: 5930 RVA: 0x00089FB4 File Offset: 0x000881B4
			public DataMigrationManager.FileListControlValueItem[] Items { get; set; }

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x0600172B RID: 5931 RVA: 0x00089FBD File Offset: 0x000881BD
			// (set) Token: 0x0600172C RID: 5932 RVA: 0x00089FC5 File Offset: 0x000881C5
			public MigrationFileInfo MigrationFileInfo { get; set; }
		}

		// Token: 0x0200034D RID: 845
		internal class FileListControlValueItem
		{
			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x0600172E RID: 5934 RVA: 0x00089FCE File Offset: 0x000881CE
			// (set) Token: 0x0600172F RID: 5935 RVA: 0x00089FD6 File Offset: 0x000881D6
			public string Text { get; set; }

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06001730 RID: 5936 RVA: 0x00089FDF File Offset: 0x000881DF
			// (set) Token: 0x06001731 RID: 5937 RVA: 0x00089FE7 File Offset: 0x000881E7
			public int FileId { get; set; }
		}
	}
}
