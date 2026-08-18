using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.ClockWorkDatabase;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DataFileIO.cs.Base;
using TechnoPro.Common.DataFileIO.cs.CharDelimited;
using TechnoPro.Common.DataFileIO.cs.Csv;
using TechnoPro.Common.DataFileIO.cs.TabDelimited;
using TechnoPro.Common.ICore.ClockWorkDatabase;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;
using TechnoPro.Common.Public.Entities.DataSync.Notetaking;
using TechnoPro.Common.Public.Entities.DataSync.Student;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Xml;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x02000111 RID: 273
	public class DataSyncManager : IDataSyncManager, IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0004C330 File Offset: 0x0004A530
		private DataSyncInfoManager dataSyncInfoManager
		{
			get
			{
				bool flag = this.dsm == null;
				if (flag)
				{
					this.dsm = new DataSyncInfoManager(this.OpContext);
				}
				return this.dsm;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0004C368 File Offset: 0x0004A568
		private ReportManager reportManager
		{
			get
			{
				bool flag = this.rm == null;
				if (flag)
				{
					this.rm = new ReportManager(this.OpContext);
				}
				return this.rm;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0004C3A0 File Offset: 0x0004A5A0
		private PeopleManager peopleManager
		{
			get
			{
				bool flag = this.pm == null;
				if (flag)
				{
					this.pm = new PeopleManager(this.OpContext);
				}
				return this.pm;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0004C3D6 File Offset: 0x0004A5D6
		public DataSyncManager(DataSyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DataSyncDAO(this.OpContext);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0004C3F9 File Offset: 0x0004A5F9
		public DataSyncManager(OperationContext opContext)
		{
			this.OpContext = opContext.ConvertTo<DataSyncOperationContext>();
			this.dao = new DataSyncDAO(this.OpContext);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0004C424 File Offset: 0x0004A624
		private static void CreateEmptyTableFromObject(DataTable t, Type itemType)
		{
			PropertyInfo[] properties = itemType.GetProperties();
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				Type propertyType = propertyInfo.PropertyType;
				bool flag = propertyType == typeof(string);
				if (flag)
				{
					t.Columns.Add(name);
				}
				else
				{
					bool isGenericType = propertyType.IsGenericType;
					if (isGenericType)
					{
						list.Add(propertyInfo);
					}
				}
			}
			foreach (PropertyInfo propertyInfo2 in list)
			{
				string name2 = propertyInfo2.Name;
				Type propertyType2 = propertyInfo2.PropertyType;
				Type itemType2 = propertyType2.GetGenericArguments()[0];
				DataSyncManager.CreateEmptyTableFromObject(t, itemType2);
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0004C50C File Offset: 0x0004A70C
		private static void ConvertObjectToDataRows(DataTable t, object item, Type itemType, object[] row)
		{
			PropertyInfo[] properties = itemType.GetProperties();
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				Type propertyType = propertyInfo.PropertyType;
				bool flag = propertyType == typeof(string);
				if (flag)
				{
					int num = t.Columns.IndexOf(name);
					bool flag2 = num >= 0;
					if (flag2)
					{
						row[num] = propertyInfo.GetValue(item, null);
					}
				}
				else
				{
					bool isGenericType = propertyType.IsGenericType;
					if (isGenericType)
					{
						list.Add(propertyInfo);
					}
				}
			}
			bool flag3 = list.Count > 0;
			if (flag3)
			{
				foreach (PropertyInfo propertyInfo2 in list)
				{
					string name2 = propertyInfo2.Name;
					Type propertyType2 = propertyInfo2.PropertyType;
					Type itemType2 = propertyType2.GetGenericArguments()[0];
					IList list2 = (IList)propertyInfo2.GetValue(item, null);
					foreach (object item2 in list2)
					{
						object[] array2 = new object[row.Length];
						row.CopyTo(array2, 0);
						DataSyncManager.ConvertObjectToDataRows(t, item2, itemType2, array2);
					}
				}
			}
			else
			{
				t.Rows.Add(row);
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0004C6A8 File Offset: 0x0004A8A8
		private string[] GetHeaderRow(BaseStream stream, bool FirstRowHasHeaders, string ColumnNameForStudentNumberInCsvFile, out int csvIndexForStudentNumber, out bool noDataPresent)
		{
			bool flag = !string.IsNullOrEmpty(ColumnNameForStudentNumberInCsvFile) && ColumnNameForStudentNumberInCsvFile.Trim().Length > 0;
			csvIndexForStudentNumber = -1;
			string[] result;
			if (FirstRowHasHeaders)
			{
				string[] nextRow = stream.GetNextRow();
				bool flag2 = nextRow == null || nextRow.Length < 1;
				if (flag2)
				{
					noDataPresent = true;
					result = null;
				}
				else
				{
					bool flag3 = nextRow != null && flag;
					if (flag3)
					{
						for (int i = 0; i < nextRow.Length; i++)
						{
							string text = nextRow[i];
							bool flag4 = text != null && text.Equals(ColumnNameForStudentNumberInCsvFile, StringComparison.OrdinalIgnoreCase);
							if (flag4)
							{
								csvIndexForStudentNumber = i;
								break;
							}
						}
					}
					noDataPresent = false;
					result = nextRow;
				}
			}
			else
			{
				bool flag5 = flag;
				if (flag5)
				{
					int num;
					bool flag6 = int.TryParse(ColumnNameForStudentNumberInCsvFile, out num);
					if (flag6)
					{
						csvIndexForStudentNumber = num;
					}
				}
				noDataPresent = false;
				result = null;
			}
			return result;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0004C77C File Offset: 0x0004A97C
		private string GetStudentNumberForNotetakerUserName(string UserName)
		{
			ISettingManager settingManager = new SettingManager(this.OpContext);
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.NOTETAKINGB_UsernameIsActuallyStudentNumber);
			bool flag = settingValue;
			string text;
			if (flag)
			{
				text = UserName;
				CWLogger.Logger.Trace("DataSyncManager:GetStudentNumberForNotetakerUserName:Setting.NOTETAKINGB_UsernameIsActuallyStudentNumber is true; this means the username will be used as the student number");
			}
			else
			{
				int settingValue2 = settingManager.GetSettingValue<int>(Setting.NOTETAKINGB_ReportIdToRetreiveNotetakerStudentNumberFromUsername);
				bool flag2 = settingValue2 < 1;
				if (flag2)
				{
					CWLogger.Logger.Warn("DataSyncManager:GetNotetakerPreviewData:MissingRidForGettingStudentNumberFromUsername");
					text = UserName;
				}
				else
				{
					IReportManager reportManager = new ReportManager(this.OpContext);
					ReportParameter[] parameters = new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "username",
							Value = UserName
						},
						new ReportParameter
						{
							Name = "student_no",
							Value = UserName
						}
					};
					RunReportResult runReportResult = reportManager.ExecuteReport2(settingValue2, parameters);
					bool flag3 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully;
					if (flag3)
					{
						CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewData:FailedToLoadStudentNumberFromUsername:UserName={0}:rid={1}:laststatusstep={2}", UserName ?? "", settingValue2.ToString(), (runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : runReportResult.ReportStatus.LastStatusStep.ToString());
						return null;
					}
					DataTable dataTable = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
					bool flag4 = dataTable == null || dataTable.Rows.Count < 1 || !dataTable.Columns.Contains("student_no");
					if (flag4)
					{
						CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewData:rid={0}:datatable={1}", settingValue2.ToString(), (dataTable == null) ? "NULL" : dataTable.Rows.Count.ToString());
						return null;
					}
					text = dataTable.Rows[0]["student_no"].ToString().Trim();
				}
			}
			bool flag5 = string.IsNullOrEmpty(text);
			string result;
			if (flag5)
			{
				CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewData:SnumIsEmpty");
				result = null;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0004C9A2 File Offset: 0x0004ABA2
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0004C9AA File Offset: 0x0004ABAA
		public DataSyncOperationContext OpContext { get; set; }

		// Token: 0x06000B55 RID: 2901 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
		public RunReportResult RunMoveDataIntoClockWork()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IList<OldUserSetting> source = oldUserSettingManager.LoadEveryoneSettings();
			OldUserSetting oldUserSetting = source.FirstOrDefault((OldUserSetting g) => g.SettingCode == eSettingCode.SETTING_DataSync_MoveDataIntoClockWorkReportid);
			int num = (oldUserSetting == null) ? 0 : oldUserSetting.IntVal;
			bool flag = num < 1;
			RunReportResult result;
			if (flag)
			{
				result = new RunReportResult
				{
					ReportStatus = new RunStatus
					{
						LastStatusStep = eRunStatusStep.FailedUnableToStart,
						ErrorMessage = "Missing Move data into ClockWork report id"
					}
				};
			}
			else
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				result = reportManager.ExecuteReport2(num, Array.Empty<ReportParameter>());
			}
			return result;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0004CA5C File Offset: 0x0004AC5C
		public DataSyncResult RunBatchDataSync()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IList<OldUserSetting> source = oldUserSettingManager.LoadEveryoneSettings();
			OldUserSetting oldUserSetting = source.FirstOrDefault((OldUserSetting g) => g.SettingCode == eSettingCode.SETTING_DataSync_BatchImportReportId);
			int num = (oldUserSetting != null) ? oldUserSetting.IntVal : 0;
			bool flag = num < 1;
			DataSyncResult result;
			if (flag)
			{
				result = new DataSyncResult
				{
					Status = eDataSyncStatus.Failed,
					SyncError = new DataSyncError
					{
						ErrorMessage = "Can't find batch data sync report id in everyone settings."
					}
				};
			}
			else
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				RunReportResult runReportResult = reportManager.ExecuteReport2(num, Array.Empty<ReportParameter>());
				eRunStatusStep eRunStatusStep = (runReportResult == null || runReportResult.ReportStatus == null) ? eRunStatusStep.Failed : runReportResult.ReportStatus.LastStatusStep;
				bool flag2 = eRunStatusStep != eRunStatusStep.CompletedSuccessfully;
				if (flag2)
				{
					result = new DataSyncResult
					{
						Status = eDataSyncStatus.Failed,
						SyncError = new DataSyncError
						{
							ErrorMessage = string.Concat(new string[]
							{
								"Report failed:rid=",
								num.ToString(),
								":status=",
								eRunStatusStep.ToString(),
								":err=",
								(runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.ErrorMessage == null) ? "" : runReportResult.ReportStatus.ErrorMessage
							})
						}
					};
				}
				else
				{
					result = new DataSyncResult
					{
						Status = eDataSyncStatus.CompletedSuccessfully
					};
				}
			}
			return result;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0004CBE4 File Offset: 0x0004ADE4
		public DataSyncResult RunCourseDataSyncByStudentNumber(string Student_no)
		{
			DataSyncInfoManager dataSyncInfoManager = this.dataSyncInfoManager;
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			ReportParameter[] parameters = new ReportParameter[]
			{
				new ReportParameter
				{
					Name = "student_no",
					Value = Student_no
				},
				new ReportParameter
				{
					Name = "studentno",
					Value = Student_no
				}
			};
			ReportManager reportManager = this.reportManager;
			RunReportResult runReportResult = reportManager.ExecuteReport2(dataSyncInfo.ImportStudentCoursesReportId, parameters);
			bool flag = runReportResult != null && runReportResult.ReportStatus != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
			DataSyncResult result;
			if (flag)
			{
				result = new DataSyncResult
				{
					Status = eDataSyncStatus.CompletedSuccessfully
				};
			}
			else
			{
				result = new DataSyncResult
				{
					Status = eDataSyncStatus.Failed,
					SyncError = new DataSyncError
					{
						ErrorMessage = ((runReportResult == null || runReportResult.ReportStatus == null) ? "No data returned" : (runReportResult.ReportStatus.ErrorMessage ?? ""))
					}
				};
			}
			return result;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0004CCDC File Offset: 0x0004AEDC
		public DataSyncResult RunCourseDataSyncById(int pid)
		{
			PeopleManager peopleManager = this.peopleManager;
			PersonBase personBase = peopleManager.LoadPerson(pid);
			bool flag = personBase == null;
			if (flag)
			{
				throw new Exception("Can't load student with personid=" + pid.ToString());
			}
			return this.RunCourseDataSyncByStudentNumber(personBase.Student_no);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0004CD28 File Offset: 0x0004AF28
		public DataSyncResult RunFullDataSyncForExistingStudent(string Student_no, bool DontSyncData, bool DontSyncCourses = false)
		{
			DataSyncInfoManager dataSyncInfoManager = this.dataSyncInfoManager;
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			ReportParameter[] parameters = new ReportParameter[]
			{
				new ReportParameter
				{
					Name = "student_no",
					Value = Student_no
				},
				new ReportParameter
				{
					Name = "studentno",
					Value = Student_no
				}
			};
			ReportManager reportManager = this.reportManager;
			bool flag;
			RunReportResult runReportResult;
			if (DontSyncData)
			{
				flag = true;
				runReportResult = new RunReportResult
				{
					ReportStatus = new RunStatus
					{
						LastStatusStep = eRunStatusStep.CompletedSuccessfully
					}
				};
				CWLogger.Logger.Trace("DataSyncManager:RunFullDataSyncForExistingStudent:SkippingSyncingData");
			}
			else
			{
				flag = false;
				runReportResult = reportManager.ExecuteReport2(dataSyncInfo.ImportStudentDataReportId, parameters);
				CWLogger.Logger.Trace("DataSyncManager:RunFullDataSyncForExistingStudent:SyncedData:result={0}", (runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : runReportResult.ReportStatus.LastStatusStep.ToString());
			}
			bool flag2 = flag || (runReportResult != null && runReportResult.ReportStatus != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully);
			DataSyncResult result;
			if (flag2)
			{
				bool flag3 = flag || (runReportResult.PrimaryData != null && runReportResult.PrimaryData.Table != null);
				if (flag3)
				{
					RunReportResult runReportResult2;
					if (DontSyncCourses)
					{
						runReportResult2 = new RunReportResult
						{
							ReportStatus = new RunStatus
							{
								LastStatusStep = eRunStatusStep.CompletedSuccessfully
							}
						};
						CWLogger.Logger.Trace("DataSyncManager:RunFullDataSyncForExistingStudent:SkippingSyncingCourses");
					}
					else
					{
						runReportResult2 = reportManager.ExecuteReport2(dataSyncInfo.ImportStudentCoursesReportId, parameters);
						CWLogger.Logger.Trace("DataSyncManager:RunFullDataSyncForExistingStudent:SyncedCourses:result={0}", (runReportResult2 == null || runReportResult2.ReportStatus == null) ? "NULL" : runReportResult2.ReportStatus.LastStatusStep.ToString());
					}
					bool flag4 = runReportResult2 != null && runReportResult2.ReportStatus != null && runReportResult2.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
					if (flag4)
					{
						return new DataSyncResult
						{
							Status = eDataSyncStatus.CompletedSuccessfully
						};
					}
				}
				result = new DataSyncResult
				{
					Status = eDataSyncStatus.Failed,
					SyncError = new DataSyncError
					{
						ErrorMessage = ((runReportResult == null || runReportResult.ReportStatus == null) ? "No data returned" : (runReportResult.ReportStatus.ErrorMessage ?? ""))
					}
				};
			}
			else
			{
				result = new DataSyncResult
				{
					Status = eDataSyncStatus.Failed,
					SyncError = new DataSyncError
					{
						ErrorMessage = ((runReportResult == null || runReportResult.ReportStatus == null) ? "Unknown error (res=null)" : (runReportResult.ReportStatus.ErrorMessage ?? ""))
					}
				};
			}
			return result;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0004CFD0 File Offset: 0x0004B1D0
		public DataSyncPreviewResult PreviewDataSyncData(string Student_no)
		{
			DataSyncInfoManager dataSyncInfoManager = this.dataSyncInfoManager;
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			ReportParameter[] parameters = new ReportParameter[]
			{
				new ReportParameter
				{
					Name = "student_no",
					Value = Student_no
				},
				new ReportParameter
				{
					Name = "studentno",
					Value = Student_no
				}
			};
			ReportManager reportManager = this.reportManager;
			RunReportResult runReportResult = reportManager.ExecuteReport2(dataSyncInfo.PreviewStudentDataReportId, parameters);
			bool flag = runReportResult != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
			DataSyncPreviewResult result;
			if (flag)
			{
				bool flag2 = runReportResult.PrimaryData != null && runReportResult.PrimaryData.Table != null;
				if (flag2)
				{
					List<DataSyncExternalData> list = new List<DataSyncExternalData>();
					bool flag3 = runReportResult.PrimaryData.Table != null && runReportResult.PrimaryData.Table.Rows.Count > 0;
					if (flag3)
					{
						DataTable table = runReportResult.PrimaryData.Table;
						DataRow dataRow = runReportResult.PrimaryData.Table.Rows[0];
						for (int i = 0; i < table.Columns.Count; i++)
						{
							list.Add(new DataSyncExternalData
							{
								FieldName = table.Columns[i].ColumnName,
								FieldValue = ((dataRow[i] == DBNull.Value) ? "" : dataRow[i].ToString())
							});
						}
					}
					result = new DataSyncPreviewResult
					{
						Status = eDataSyncStatus.CompletedSuccessfully,
						Data = list
					};
				}
				else
				{
					result = new DataSyncPreviewResult
					{
						Status = eDataSyncStatus.Failed,
						SyncError = new DataSyncError
						{
							ErrorMessage = ((runReportResult == null || runReportResult.ReportStatus == null) ? "No data returned" : (runReportResult.ReportStatus.ErrorMessage ?? ""))
						}
					};
				}
			}
			else
			{
				result = new DataSyncPreviewResult
				{
					Status = eDataSyncStatus.Failed,
					SyncError = new DataSyncError
					{
						ErrorMessage = ((runReportResult == null || runReportResult.ReportStatus == null) ? "Unknown error (res=null)" : (runReportResult.ReportStatus.ErrorMessage ?? ""))
					}
				};
			}
			return result;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0004D21C File Offset: 0x0004B41C
		public DataTable LoadCustomDataByEncryptedLookupField(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string LookupFieldPlainText, string ExternalColumnNameForEncryptedLookupField, params string[] ExternalColumnsToReturnNullForAll)
		{
			IList<ExternalInternalColumnMapping> list = this.dao.LoadCustomDataMappings(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			ExternalInternalColumnMapping externalInternalColumnMapping = list.FirstOrDefault((ExternalInternalColumnMapping g) => g.ExternalColumnName.Equals(ExternalColumnNameForEncryptedLookupField, StringComparison.OrdinalIgnoreCase));
			bool flag = externalInternalColumnMapping == null;
			DataTable result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncmanager:LoadCustomDataByEncryptedLookupField:CantFindExternalColumnnameForLookupFieldInMapping:ExternalColumnNameForLookupField={0}:ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn={1}", ExternalColumnNameForEncryptedLookupField ?? "NULL", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn ?? "NULL");
				result = null;
			}
			else
			{
				List<ExternalInternalColumnMapping> mapping_fieldsToReturn = (ExternalColumnsToReturnNullForAll == null) ? (from g in list
				where !g.ExternalColumnName.Equals(ExternalColumnNameForEncryptedLookupField, StringComparison.OrdinalIgnoreCase)
				select g).ToList<ExternalInternalColumnMapping>() : (from g in list
				where ExternalColumnsToReturnNullForAll.Any((string h) => h.Equals(g.ExternalColumnName, StringComparison.OrdinalIgnoreCase))
				select g).ToList<ExternalInternalColumnMapping>();
				result = this.dao.LoadCustomDataByEncryptedLookupField(LookupFieldPlainText, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, externalInternalColumnMapping.ClockWorkColumnName, list, mapping_fieldsToReturn);
			}
			return result;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public DataTable LoadCustomData(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string StudentNumber, string ExternalColumnNameForStudentNumber)
		{
			IList<ExternalInternalColumnMapping> list = this.dao.LoadCustomDataMappings(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			ExternalInternalColumnMapping externalInternalColumnMapping = list.FirstOrDefault((ExternalInternalColumnMapping g) => g.ExternalColumnName.Equals(ExternalColumnNameForStudentNumber, StringComparison.OrdinalIgnoreCase));
			bool flag = externalInternalColumnMapping == null;
			DataTable result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncmanager:LoadCustomData:CantFindExternalColumnnameForStudentNumberInMapping:ExternalColumnNameForStudentNumber={0}:ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn={1}", ExternalColumnNameForStudentNumber ?? "NULL", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn ?? "NULL");
				result = null;
			}
			else
			{
				result = this.dao.LoadCustomData(StudentNumber, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, externalInternalColumnMapping.ClockWorkColumnName, list);
			}
			return result;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0004D378 File Offset: 0x0004B578
		private SPProvider GetNotetakerInfoFromData(string username, string snum, IList<DataSyncExternalData> externalData)
		{
			SPProvider spprovider = new SPProvider
			{
				Person = new PersonBase
				{
					Student_no = this.GetDataSyncExternalDataValueByName(externalData, new string[]
					{
						"student_no"
					}),
					FirstName = this.GetDataSyncExternalDataValueByName(externalData, new string[]
					{
						"firstname"
					}),
					MiddleName = this.GetDataSyncExternalDataValueByName(externalData, new string[]
					{
						"middlename"
					}),
					LastName = this.GetDataSyncExternalDataValueByName(externalData, new string[]
					{
						"lastname"
					})
				},
				Address1 = this.GetDataSyncExternalDataValueByName(externalData, new string[]
				{
					"address"
				}),
				Address2 = this.GetDataSyncExternalDataValueByName(externalData, new string[]
				{
					"paddress",
					"address2",
					"permaddress",
					"permanentaddress"
				}),
				Phone1 = this.GetDataSyncExternalDataValueByName(externalData, new string[]
				{
					"phone1",
					"phone",
					"home phone",
					"homephone"
				}),
				Phone2 = this.GetDataSyncExternalDataValueByName(externalData, new string[]
				{
					"phone2",
					"cellphone",
					"cell phone"
				}),
				Email = this.GetDataSyncExternalDataValueByName(externalData, new string[]
				{
					"email",
					"email address",
					"emailaddress"
				}),
				UserName = username
			};
			bool flag = spprovider.Address1.Length > 0;
			if (flag)
			{
				spprovider.Address1 = this.FormatAddress(spprovider.Address1);
			}
			bool flag2 = spprovider.Address2.Length > 0;
			if (flag2)
			{
				spprovider.Address2 = this.FormatAddress(spprovider.Address2);
			}
			return spprovider;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0004D544 File Offset: 0x0004B744
		private string FormatAddress(string s)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string text in array)
			{
				string text2 = text.Trim();
				bool flag = text2.Length > 0;
				if (flag)
				{
					bool flag2 = num++ > 0;
					if (flag2)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0004D5D4 File Offset: 0x0004B7D4
		private string GetDataSyncExternalDataValueByName(IList<DataSyncExternalData> externalData, params string[] names)
		{
			DataSyncExternalData dataSyncExternalData = null;
			string[] array = names ?? new string[0];
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i];
				dataSyncExternalData = externalData.FirstOrDefault((DataSyncExternalData g) => g.FieldName != null && g.FieldName.Equals(name, StringComparison.OrdinalIgnoreCase));
				bool flag = dataSyncExternalData != null;
				if (flag)
				{
					break;
				}
			}
			bool flag2 = dataSyncExternalData == null;
			if (flag2)
			{
				CWLogger.Logger.Warn("DataSyncManager:GetDataSyncExternalDataValueByName:External data not found:externalData.Count={0}:names={1}", (externalData == null) ? "NULL" : externalData.Count.ToString(), (names == null) ? "NULL" : string.Join(",", names));
			}
			bool flag3 = dataSyncExternalData == null || dataSyncExternalData.FieldValue == null;
			string result;
			if (flag3)
			{
				result = "";
			}
			else
			{
				result = dataSyncExternalData.FieldValue.Trim();
			}
			return result;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
		public NotetakerWithExternalCourses GetNotetakerPreviewData(string UserName)
		{
			string studentNumberForNotetakerUserName = this.GetStudentNumberForNotetakerUserName(UserName);
			return this.GetNotetakerPreviewDataByStudentNumber(UserName, studentNumberForNotetakerUserName);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		public IList<DataSyncExternalCourse> GetNotetakerPreviewExternalCoursesByUserName(string UserName)
		{
			string studentNumberForNotetakerUserName = this.GetStudentNumberForNotetakerUserName(UserName);
			bool flag = string.IsNullOrEmpty(studentNumberForNotetakerUserName);
			IList<DataSyncExternalCourse> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.GetNotetakerPreviewExternalCoursesByStudentNumber(studentNumberForNotetakerUserName);
			}
			return result;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		public IList<DataSyncExternalCourse> GetNotetakerPreviewExternalCoursesByStudentNumber(string StudentNumber)
		{
			bool flag = StudentNumber == null || StudentNumber.Trim().Length < 1;
			IList<DataSyncExternalCourse> result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:Student number is null or empty");
				result = null;
			}
			else
			{
				ISettingManager settingManager = new SettingManager(this.OpContext);
				int num = settingManager.GetSettingValue<int>(Setting.NOTETAKINGB_ReportIdToPreviewNotetakerRegisteredCourses);
				bool flag2 = num < 1;
				if (flag2)
				{
					IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
					DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
					bool flag3 = dataSyncInfo != null;
					if (flag3)
					{
						num = dataSyncInfo.ImportStudentCoursesReportId;
					}
				}
				else
				{
					CWLogger.Logger.Warn("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:Warning: using old setting for notetaker report to preview courses: rid={0}", num.ToString());
				}
				bool flag4 = num < 1;
				if (flag4)
				{
					CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:NoRidCoursesFound");
					result = null;
				}
				else
				{
					ReportParameter[] parameters = new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "studentno",
							Value = StudentNumber
						},
						new ReportParameter
						{
							Name = "student_no",
							Value = StudentNumber
						}
					};
					RunReportResult runReportResult = this.reportManager.ExecuteReport2(num, new List<eFunctionType>
					{
						eFunctionType.Data_Sync_Courses_2,
						eFunctionType.Import_Students_Courses
					}, parameters);
					bool flag5 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully || runReportResult.PrimaryData == null || runReportResult.PrimaryData.Table == null || runReportResult.PrimaryData.Table.Rows.Count < 1;
					if (flag5)
					{
						CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:NoCoursesReturned:snum={0}:status={1}:emsg={2}:primaryTableRowCount={3}", new object[]
						{
							StudentNumber ?? "NULL",
							(runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : runReportResult.ReportStatus.LastStatusStep.ToString(),
							(runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : (runReportResult.ReportStatus.ErrorMessage ?? ""),
							(runReportResult == null || runReportResult.PrimaryData == null || runReportResult.PrimaryData.Table == null) ? "NULL" : runReportResult.PrimaryData.Table.Rows.Count.ToString()
						});
						result = new List<DataSyncExternalCourse>();
					}
					else
					{
						DataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
						List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = dataSyncCourseManager.GetRowPartsFromDataTable(runReportResult.PrimaryData.Table);
						bool flag6 = rowPartsFromDataTable == null || rowPartsFromDataTable.Count < 1;
						if (flag6)
						{
							CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:NoRowPartsAvailable");
							result = new List<DataSyncExternalCourse>();
						}
						else
						{
							IDataSyncCourseManager dataSyncCourseManager2 = new DataSyncCourseManager(this.OpContext);
							List<DataSyncExternalCourse> list = dataSyncCourseManager2.ParseExternalCourseRowParts(rowPartsFromDataTable);
							CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewExternalCoursesByStudentNumber:SuccessfullyRetrievedNotetakerData:snum={0}:extCoursesCt={1}", StudentNumber ?? "NULL", (list == null) ? "NULL" : list.Count.ToString());
							result = list;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		public StudentDataSyncPreviewData GetStudentPreviewDataByStudentNumberOrUsername(string UserName, string StudentNumber)
		{
			string text = (StudentNumber ?? "").Trim();
			string text2 = (UserName ?? "").Trim();
			bool flag = text.Length < 1 && text2.Length < 1;
			StudentDataSyncPreviewData result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncManager:GetStudentPreviewDataByStudentNumberOrUsername:Username AND student number are null or empty");
				result = null;
			}
			else
			{
				StudentDataSyncPreviewData studentDataSyncPreviewData = (text.Length > 0) ? this.GetStudentPreviewDataByStudentNumber(text2, text) : null;
				bool flag2 = studentDataSyncPreviewData != null;
				if (flag2)
				{
					result = studentDataSyncPreviewData;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(UserName);
					if (flag3)
					{
						CWLogger.Logger.Warn("DataSyncManager:GetStudentPreviewDataByStudentNumberOrUsername:Couldn't get data using student number, and username is empty:snum={0}:username={1}", text, text2);
						result = null;
					}
					else
					{
						text = ((text2.Length > 0) ? (this.GetStudentNumberForNotetakerUserName(UserName) ?? "").Trim() : null);
						bool flag4 = !string.IsNullOrEmpty(text);
						if (flag4)
						{
							result = this.GetStudentPreviewDataByStudentNumber(UserName, text);
						}
						else
						{
							CWLogger.Logger.Warn("Common.Core.DataSync.DataSyncManager:GetStudentPreviewDataByStudentNumberOrUsername:FailedToRetrieveStudentNumberFromUsername:username={0}:snum={1}:originalSnum={2}", text2, text, StudentNumber ?? "NULL");
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0004DAFC File Offset: 0x0004BCFC
		private StudentDataSyncPreviewData GetStudentPreviewDataByStudentNumber(string UserName, string StudentNumber)
		{
			string text = (StudentNumber ?? "").Trim();
			bool flag = text.Length < 1;
			StudentDataSyncPreviewData result;
			if (flag)
			{
				CWLogger.Logger.Warn("Common.Core.DataSync.DataSyncManager:GetStudentPreviewDataByStudentNumber:StudentNumber is empty:username={0}", UserName ?? "NULL");
				result = null;
			}
			else
			{
				DataSyncPreviewResult dataSyncPreviewResult = this.PreviewDataSyncData(text);
				bool flag2 = dataSyncPreviewResult == null || dataSyncPreviewResult.Status != eDataSyncStatus.CompletedSuccessfully || dataSyncPreviewResult.Data == null;
				if (flag2)
				{
					CWLogger.Logger.Trace("DataSyncManager:GetStudentPreviewDataByStudentNumberOrUsername:PreviewDataSyncFailed:snum={0}:username={1}:status={2}:err={3}", new object[]
					{
						text,
						UserName ?? "NULL",
						(dataSyncPreviewResult == null) ? "NULL" : dataSyncPreviewResult.Status.ToString(),
						(dataSyncPreviewResult == null || dataSyncPreviewResult.SyncError == null) ? "NULL" : (dataSyncPreviewResult.SyncError.ErrorMessage ?? "NULL2")
					});
					result = null;
				}
				else
				{
					IList<DataSyncExternalData> data = dataSyncPreviewResult.Data;
					result = this.GetStudentDataSyncPreviewDataFromData(UserName, text, data);
				}
			}
			return result;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0004DC00 File Offset: 0x0004BE00
		private string GetExternalDataValue(IList<DataSyncExternalData> data, string fieldName)
		{
			DataSyncExternalData dataSyncExternalData = data.FirstOrDefault((DataSyncExternalData g) => g.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
			return (dataSyncExternalData == null) ? "" : (dataSyncExternalData.FieldValue ?? "");
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0004DC4C File Offset: 0x0004BE4C
		private StudentDataSyncPreviewData GetStudentDataSyncPreviewDataFromData(string UserName, string StudentNumber, IList<DataSyncExternalData> data)
		{
			StudentDataSyncPreviewData studentDataSyncPreviewData = new StudentDataSyncPreviewData
			{
				FirstName = this.GetExternalDataValue(data, "firstname"),
				MiddleName = this.GetExternalDataValue(data, "middlename"),
				LastName = this.GetExternalDataValue(data, "lastname"),
				StudentNumber = StudentNumber,
				Username = this.GetExternalDataValue(data, "username"),
				Email = this.GetExternalDataValue(data, "email"),
				ExternalDataItems = data
			};
			bool flag = studentDataSyncPreviewData.Username.Length < 1;
			if (flag)
			{
				studentDataSyncPreviewData.Username = (UserName ?? "");
			}
			return studentDataSyncPreviewData;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0004DCF8 File Offset: 0x0004BEF8
		public NotetakerWithExternalCourses GetNotetakerPreviewDataByStudentNumber(string UserName, string StudentNumber)
		{
			bool flag = StudentNumber == null || StudentNumber.Trim().Length < 1;
			NotetakerWithExternalCourses result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncManager:GetNotetakerPreviewDataByStudentNumber:Student number is null or empty");
				result = null;
			}
			else
			{
				DataSyncPreviewResult dataSyncPreviewResult = this.PreviewDataSyncData(StudentNumber);
				bool flag2 = dataSyncPreviewResult == null || dataSyncPreviewResult.Status != eDataSyncStatus.CompletedSuccessfully || dataSyncPreviewResult.Data == null;
				if (flag2)
				{
					CWLogger.Logger.Trace("DataSyncManager:GetNotetakerPreviewDataByStudentNumber:PreviewDataSync Failed:snum={0}:res2.status={1}:res2.msg={2}", StudentNumber ?? "NULL", (dataSyncPreviewResult == null) ? "NULL" : dataSyncPreviewResult.Status.ToString(), (dataSyncPreviewResult == null || dataSyncPreviewResult.SyncError == null) ? "NULL" : (dataSyncPreviewResult.SyncError.ErrorMessage ?? ""));
					result = null;
				}
				else
				{
					IList<DataSyncExternalData> data = dataSyncPreviewResult.Data;
					SPProvider notetakerInfoFromData = this.GetNotetakerInfoFromData(UserName, StudentNumber, data);
					IList<DataSyncExternalCourse> notetakerPreviewExternalCoursesByStudentNumber = this.GetNotetakerPreviewExternalCoursesByStudentNumber(StudentNumber);
					result = new NotetakerWithExternalCourses
					{
						ExternalCourses = notetakerPreviewExternalCoursesByStudentNumber,
						Notetaker = notetakerInfoFromData
					};
				}
			}
			return result;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0004DDF8 File Offset: 0x0004BFF8
		public void CopyCsvDataToCustomTable(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			this.dao.DeleteAllCustomData(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			IList<string> databaseCustomColumnNames = this.dao.GetDatabaseCustomColumnNames(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			using (TextReader textReader = new StreamReader(FileName, Encoding.Default))
			{
				CsvStream stream = new CsvStream(textReader);
				this.ParseTextStream(stream, databaseCustomColumnNames, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0004DE64 File Offset: 0x0004C064
		public void CopyTabDelimitedDataToCustomTable(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			this.dao.DeleteAllCustomData(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			IList<string> databaseCustomColumnNames = this.dao.GetDatabaseCustomColumnNames(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			using (TextReader textReader = new StreamReader(FileName, Encoding.Default))
			{
				TabStream stream = new TabStream(textReader);
				this.ParseTextStream(stream, databaseCustomColumnNames, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0004DED0 File Offset: 0x0004C0D0
		private void ParseTextStream(BaseStream stream, IList<string> colNames, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			int num;
			bool flag;
			string[] array = this.GetHeaderRow(stream, FirstRowHasHeaders, ColumnNameForStudentNumberInCsvFile, out num, out flag);
			bool flag2 = flag;
			if (!flag2)
			{
				bool flag3 = num < 0 && CsvColumnNamesIfNotFirstRowHasHeaders != null;
				if (flag3)
				{
					for (int i = 0; i < CsvColumnNamesIfNotFirstRowHasHeaders.Length; i++)
					{
						bool flag4 = CsvColumnNamesIfNotFirstRowHasHeaders[i].Equals(ColumnNameForStudentNumberInCsvFile, StringComparison.OrdinalIgnoreCase);
						if (flag4)
						{
							num = i;
							break;
						}
					}
				}
				bool flag5 = (array == null || array.Length < 1) && CsvColumnNamesIfNotFirstRowHasHeaders != null && CsvColumnNamesIfNotFirstRowHasHeaders.Length != 0;
				if (flag5)
				{
					array = CsvColumnNamesIfNotFirstRowHasHeaders;
				}
				bool flag6 = array == null || array.Length < 1;
				if (flag6)
				{
					throw new Exception("CopyCsvDataToCustomTable:Can'tCompleteDueToMissingCsvColumnNames");
				}
				List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
				for (int j = 0; j < array.Length; j++)
				{
					bool flag7 = j >= colNames.Count;
					if (flag7)
					{
						CWLogger.Logger.Warn("DataSyncManager:CopyCsvDataToCustomData:Out of custom_data columns to store data:i={0}:headerRow[i]={1}", j.ToString(), array[j] ?? "");
						break;
					}
					string value = array[j];
					string value2 = colNames[j];
					bool flag8 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2);
					if (flag8)
					{
						list.Add(new ExternalInternalColumnMapping
						{
							ClockWorkTableName = "CUSTOM_" + ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn,
							ExternalColumnName = array[j],
							ClockWorkColumnName = colNames[j],
							IsClockWorkDataEncrypted = (j != num)
						});
					}
				}
				this.dao.WriteCustomDataMappings(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, list);
				string[] nextRow = stream.GetNextRow();
				while (nextRow != null && nextRow.Length != 0)
				{
					this.dao.WriteCustomDataRow(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, colNames, nextRow, num, Array.Empty<int>());
					nextRow = stream.GetNextRow();
				}
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		public void CopyCharacterDelimitedDataToCustomTable(char Delimiter, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInFile, bool FirstRowHasHeaders, params string[] FileColumnNamesIfNotFirstRowHasHeaders)
		{
			this.dao.DeleteAllCustomData(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			IList<string> databaseCustomColumnNames = this.dao.GetDatabaseCustomColumnNames(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			using (TextReader textReader = new StreamReader(FileName, Encoding.Default))
			{
				CharStream stream = new CharStream(Delimiter, textReader);
				this.ParseTextStream(stream, databaseCustomColumnNames, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, FileName, ColumnNameForStudentNumberInFile, FirstRowHasHeaders, FileColumnNamesIfNotFirstRowHasHeaders);
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0004E110 File Offset: 0x0004C310
		public void CopyXmlDataToCustomData<T>(string fileName, string[] headerRow, Func<T, string[][]> convertForStorage, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string colNameWithStudentNumber)
		{
			this.dao.DeleteAllCustomData(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			IList<string> colNames = this.dao.GetDatabaseCustomColumnNames(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			int colIndexForStudentNumber = Array.IndexOf<string>(headerRow, colNameWithStudentNumber);
			bool flag = colIndexForStudentNumber < 0;
			if (flag)
			{
				throw new Exception(string.Format("Can't find student number ['{0}'] in columns [{1}]", colNameWithStudentNumber ?? "NULL", string.Join(", ", headerRow)));
			}
			List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
			for (int i = 0; i < headerRow.Length; i++)
			{
				bool flag2 = i >= colNames.Count;
				if (flag2)
				{
					CWLogger.Logger.Warn("DataSyncManager:CopyXmlSimpleDataToCustomData:Out of custom_data columns to store data:i={0}:headerRow[i]={1}", i.ToString(), headerRow[i] ?? "");
					break;
				}
				string value = headerRow[i];
				string value2 = colNames[i];
				bool flag3 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2);
				if (flag3)
				{
					list.Add(new ExternalInternalColumnMapping
					{
						ClockWorkTableName = "CUSTOM_" + ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn,
						ExternalColumnName = headerRow[i],
						ClockWorkColumnName = colNames[i],
						IsClockWorkDataEncrypted = (i != colIndexForStudentNumber)
					});
				}
			}
			this.dao.WriteCustomDataMappings(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, list);
			XmlStreamConverter.ConvertXmlData<T>(fileName, delegate(T g)
			{
				string[][] array = convertForStorage(g);
				bool flag4 = array != null;
				if (flag4)
				{
					foreach (string[] row in array)
					{
						this.dao.WriteCustomDataRow(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, colNames, row, colIndexForStudentNumber, Array.Empty<int>());
					}
				}
				return true;
			});
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0004E2B0 File Offset: 0x0004C4B0
		public DataTable ConvertObjectToDataRows<T>() where T : class
		{
			return this.ConvertObjectToDataRows<T>(default(T));
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		public DataTable ConvertObjectToDataRows<T>(T item) where T : class
		{
			Type typeFromHandle = typeof(T);
			DataTable dataTable = new DataTable("t");
			DataSyncManager.CreateEmptyTableFromObject(dataTable, typeFromHandle);
			bool flag = item == null;
			DataTable result;
			if (flag)
			{
				result = dataTable;
			}
			else
			{
				object[] row = new object[dataTable.Columns.Count];
				DataSyncManager.ConvertObjectToDataRows(dataTable, item, typeFromHandle, row);
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0004E33C File Offset: 0x0004C53C
		public DataTable LoadCustomDataWithCustomSql(string Sql, string StudentNumber)
		{
			string text = Sql;
			Regex regex = new Regex("\\bCUSTOM_\\w*\\b \\b\\w*\\b");
			MatchCollection source = regex.Matches(Sql);
			List<DataSyncManager.CustomTableInfo> list = (from Match m in source
			select new DataSyncManager.CustomTableInfo(m.Value)).ToList<DataSyncManager.CustomTableInfo>();
			IList<ExternalInternalColumnMapping> source2 = this.dao.LoadCustomDataMappingsForMultipleTables((from g in list
			select g.GetTableNameWithoutCustom()).ToArray<string>());
			using (List<DataSyncManager.CustomTableInfo>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataSyncManager.CustomTableInfo ti = enumerator.Current;
					List<ExternalInternalColumnMapping> source3 = (from g in source2
					where g.ClockWorkTableName.Equals(ti.GetTableNameWithoutCustom(), StringComparison.OrdinalIgnoreCase)
					select g).ToList<ExternalInternalColumnMapping>();
					string newValue = string.Join(",", (from g in source3
					select string.Concat(new string[]
					{
						ti.Alias,
						".[",
						g.ClockWorkColumnName,
						"] AS [",
						g.ExternalColumnName,
						"]"
					})).ToArray<string>());
					Regex regex2 = new Regex(string.Format("{0}\\.\\[(.*?)\\]|{0}\\.\\b\\w*", ti.Alias));
					MatchCollection matchCollection = regex2.Matches(text);
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						string value = match.Value;
						int num = value.IndexOf(".");
						bool flag = num <= 0;
						if (!flag)
						{
							string extName = value.Substring(num + 1).Trim();
							bool flag2 = extName.StartsWith("[");
							if (flag2)
							{
								extName = extName.Substring(1);
							}
							bool flag3 = extName.EndsWith("]");
							if (flag3)
							{
								extName = extName.Substring(0, extName.Length - 1);
							}
							ExternalInternalColumnMapping externalInternalColumnMapping = source3.FirstOrDefault((ExternalInternalColumnMapping g) => g.ExternalColumnName.Equals(extName, StringComparison.OrdinalIgnoreCase));
							bool flag4 = externalInternalColumnMapping == null;
							if (flag4)
							{
								text = text.Replace(value, "NOT FOUND:" + value);
							}
							else
							{
								text = text.Replace(value, ti.Alias + "." + externalInternalColumnMapping.ClockWorkColumnName);
							}
						}
					}
					text = text.Replace(ti.Alias + ".*", newValue);
				}
			}
			return this.dao.LoadCustomData(text, StudentNumber);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0004E624 File Offset: 0x0004C824
		public string[] LoadCustomTableNames()
		{
			IClockWorkDatabaseManager clockWorkDatabaseManager = new ClockWorkDatabaseManager(this.OpContext);
			string[] source = clockWorkDatabaseManager.LoadAllTableNames();
			return (from g in source
			where g.StartsWith("custom_", StringComparison.OrdinalIgnoreCase)
			select g).ToArray<string>();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0004E674 File Offset: 0x0004C874
		public string[] LoadCustomExternalColumnNames(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn)
		{
			IList<ExternalInternalColumnMapping> source = this.dao.LoadCustomDataMappings(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			return (from g in source
			select g.ExternalColumnName ?? "" into h
			where h.Length > 0
			select h).ToArray<string>();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
		public void RunBatchDataSyncForOldCourses(DataTable studentsTable, DataSyncBatchParameters batchSyncParameters)
		{
			DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions = new DataSyncManager.DataSyncStudentOptions
			{
				OpContext = this.OpContext,
				ReportManager = new ReportManager(this.OpContext),
				LastDataSyncControlId = batchSyncParameters.LastDataSyncControlId,
				AllowedTimeToRun = batchSyncParameters.AllowedTimeToRun,
				UseSingleThread = batchSyncParameters.UseSingleThread,
				ReportIdCoursesData = batchSyncParameters.OverrideImportStudentCoursesReportId,
				ReportIdStudentData = 0
			};
			DataSyncManager.RunBatchDataSync(studentsTable, dataSyncStudentOptions, new Func<DataSyncManager.DataSyncStudentOptions, string, bool>(DataSyncManager.DataSyncStudentOldCourses));
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0004E768 File Offset: 0x0004C968
		private static bool DataSyncStudentOldCourses(DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions, string student_no)
		{
			bool flag = string.IsNullOrEmpty(student_no);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ReportParameter reportParameter = new ReportParameter
				{
					Name = "studentno",
					Value = student_no
				};
				ReportParameter reportParameter2 = new ReportParameter
				{
					Name = "student_no",
					Value = student_no
				};
				bool? flag2 = null;
				bool flag3 = dataSyncStudentOptions.ReportIdCoursesData < 1;
				if (flag3)
				{
					CWLogger.Logger.Warn("DataSyncManager:DataSyncStudentOldCourses:reportIdCoursesData={0}", dataSyncStudentOptions.ReportIdCoursesData.ToString());
					result = false;
				}
				else
				{
					try
					{
						RunReportResult runReportResult = dataSyncStudentOptions.ReportManager.ExecuteReport2(dataSyncStudentOptions.ReportIdCoursesData, new ReportParameter[]
						{
							reportParameter,
							reportParameter2
						});
						bool flag4 = ((runReportResult != null) ? runReportResult.ReportStatus : null) != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
						result = flag4;
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("BatchDataSyncStudentOldCourses:Courses:snum={0}:error={1}", student_no, ex.ToString());
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004E878 File Offset: 0x0004CA78
		private static void RunBatchDataSync(DataTable studentsTable, DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions, Func<DataSyncManager.DataSyncStudentOptions, string, bool> DoIndividualDataSync)
		{
			DataSyncOperationContext dataSyncOperationContext = dataSyncStudentOptions.OpContext ?? new DataSyncOperationContext();
			bool flag = !dataSyncStudentOptions.UseSingleThread;
			string text = null;
			int successfulStudentCount = 0;
			try
			{
				List<string> list = (from DataRow dr in studentsTable.Rows
				select dr["student_no"].ToString().Trim().ToUpper()).ToList<string>();
				bool flag2 = dataSyncStudentOptions.LastDataSyncControlId > 0;
				if (flag2)
				{
					IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(dataSyncOperationContext);
					DataTable dataTable = dynamicDataForReportsManager.CrossReferencePerStudentData(studentsTable, new List<int>
					{
						dataSyncStudentOptions.LastDataSyncControlId
					});
					bool flag3 = dataTable != null;
					if (flag3)
					{
						DataView source = new DataView
						{
							Table = dataTable,
							Sort = dataTable.Columns[dataTable.Columns.Count - 1].ColumnName
						};
						list = (from DataRowView drv in source
						select drv.Row["student_no"].ToString().Trim().ToUpper()).ToList<string>();
					}
				}
				IDataSyncDAO dataSyncDAO = new DataSyncDAO(dataSyncOperationContext);
				dataSyncOperationContext.BatchDataSyncLogId = dataSyncDAO.GetNewBatchDataSyncLogId(list.Count);
				DateTime now = DateTime.Now;
				bool checkAllowedTimeToRun = dataSyncStudentOptions.AllowedTimeToRun.TotalMinutes > 0.0;
				dataSyncStudentOptions.ReportStudentData = dataSyncStudentOptions.ReportManager.LoadReport(dataSyncStudentOptions.ReportIdStudentData);
				dataSyncStudentOptions.ReportCoursesData = dataSyncStudentOptions.ReportManager.LoadReport(dataSyncStudentOptions.ReportIdCoursesData);
				successfulStudentCount = DataSyncManager.DoDataSyncWithoutParallelProcessing(now, checkAllowedTimeToRun, dataSyncStudentOptions.AllowedTimeToRun, list, dataSyncStudentOptions, DoIndividualDataSync);
			}
			catch (Exception ex)
			{
				text = ex.ToString();
				CWLogger.Logger.Error("DataSyncManager:RunBatchDataSync:Failed:context={0}:err={1}", dataSyncOperationContext.BatchDataSyncLogId.ToString(), text);
			}
			bool flag4 = dataSyncOperationContext.BatchDataSyncLogId > 0;
			if (flag4)
			{
				DataSyncManager.UpdateBatchSync(dataSyncOperationContext, dataSyncOperationContext.BatchDataSyncLogId, successfulStudentCount, text);
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0004EA78 File Offset: 0x0004CC78
		public void RunBatchDataSync(DataTable studentsTable, DataSyncBatchParameters batchSyncParameters)
		{
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions = new DataSyncManager.DataSyncStudentOptions
			{
				OpContext = this.OpContext,
				ReportManager = new ReportManager(this.OpContext),
				LastDataSyncControlId = batchSyncParameters.LastDataSyncControlId,
				AllowedTimeToRun = batchSyncParameters.AllowedTimeToRun,
				UseSingleThread = batchSyncParameters.UseSingleThread,
				ReportIdCoursesData = ((batchSyncParameters.OverrideImportStudentCoursesReportId < 1) ? ((dataSyncInfo != null) ? dataSyncInfo.ImportStudentCoursesReportId : 0) : batchSyncParameters.OverrideImportStudentCoursesReportId),
				ReportIdStudentData = ((batchSyncParameters.OverrideImportStudentDataReportId < 1) ? ((dataSyncInfo != null) ? dataSyncInfo.ImportStudentDataReportId : 0) : batchSyncParameters.OverrideImportStudentDataReportId)
			};
			DataSyncManager.RunBatchDataSync(studentsTable, dataSyncStudentOptions, new Func<DataSyncManager.DataSyncStudentOptions, string, bool>(DataSyncManager.DataSyncStudent));
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0004EB44 File Offset: 0x0004CD44
		private static void UpdateBatchSync(DataSyncOperationContext OpContext, int batchDataSyncLogId, int successfulStudentCount, string errorMessage)
		{
			IDataSyncDAO dataSyncDAO = new DataSyncDAO(OpContext);
			dataSyncDAO.UpdateBatchSync(batchDataSyncLogId, successfulStudentCount, errorMessage);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004EB64 File Offset: 0x0004CD64
		private static int DoDataSyncWithParallelProcessing(DateTime startTime, bool checkAllowedTimeToRun, TimeSpan allowedTimeToRun, IList<string> snums, DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions, Func<DataSyncManager.DataSyncStudentOptions, string, bool> DoIndividualDataSync)
		{
			object syncObj = new object();
			int count = 0;
			Parallel.ForEach<string>(snums, new ParallelOptions
			{
				MaxDegreeOfParallelism = 4
			}, delegate(string snum)
			{
				bool flag = true;
				bool checkAllowedTimeToRun2 = checkAllowedTimeToRun;
				if (checkAllowedTimeToRun2)
				{
					TimeSpan t = DateTime.Now - startTime;
					bool flag2 = t >= allowedTimeToRun;
					if (flag2)
					{
						flag = false;
					}
				}
				bool flag3 = !flag;
				if (!flag3)
				{
					bool flag4 = !DoIndividualDataSync(dataSyncStudentOptions, snum);
					if (!flag4)
					{
						object syncObj = syncObj;
						lock (syncObj)
						{
							count++;
						}
					}
				}
			});
			return count;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004EBDC File Offset: 0x0004CDDC
		private static int DoDataSyncWithoutParallelProcessing(DateTime startTime, bool checkAllowedTimeToRun, TimeSpan allowedTimeToRun, IList<string> snums, DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions, Func<DataSyncManager.DataSyncStudentOptions, string, bool> DoIndividualDataSync)
		{
			int num = 0;
			foreach (string arg in snums)
			{
				if (checkAllowedTimeToRun)
				{
					TimeSpan t = DateTime.Now - startTime;
					bool flag = t >= allowedTimeToRun;
					if (flag)
					{
						break;
					}
				}
				bool flag2 = DoIndividualDataSync(dataSyncStudentOptions, arg);
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004EC64 File Offset: 0x0004CE64
		private static bool DataSyncStudent(DataSyncManager.DataSyncStudentOptions dataSyncStudentOptions, string snum)
		{
			DataSyncOperationContext opContext = dataSyncStudentOptions.OpContext;
			bool flag = string.IsNullOrEmpty(snum);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ReportParameter reportParameter = new ReportParameter
				{
					Name = "studentno",
					Value = snum
				};
				ReportParameter reportParameter2 = new ReportParameter
				{
					Name = "student_no",
					Value = snum
				};
				ReportParameter reportParameter3 = new ReportParameter
				{
					Name = "BatchDataSyncLogId",
					Value = (((opContext != null) ? opContext.BatchDataSyncLogId.ToString() : null) ?? "0")
				};
				bool? flag2 = null;
				bool flag3 = dataSyncStudentOptions.ReportIdStudentData > 0;
				if (flag3)
				{
					try
					{
						RunReportResult runReportResult = (dataSyncStudentOptions.ReportStudentData == null) ? dataSyncStudentOptions.ReportManager.ExecuteReport2(dataSyncStudentOptions.ReportIdStudentData, new ReportParameter[]
						{
							reportParameter,
							reportParameter2,
							reportParameter3
						}) : dataSyncStudentOptions.ReportManager.ExecuteReport2(dataSyncStudentOptions.ReportStudentData, null, null, new ReportParameter[]
						{
							reportParameter,
							reportParameter2,
							reportParameter3
						});
						flag2 = new bool?(((runReportResult != null) ? runReportResult.ReportStatus : null) != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("BatchDataSync:StudentData:snum={0}:error={1}", snum, ex.ToString());
					}
				}
				bool flag4 = dataSyncStudentOptions.ReportIdCoursesData < 1;
				if (flag4)
				{
					result = (flag2 != null && flag2.Value);
				}
				else
				{
					try
					{
						RunReportResult runReportResult2 = (dataSyncStudentOptions.ReportCoursesData == null) ? dataSyncStudentOptions.ReportManager.ExecuteReport2(dataSyncStudentOptions.ReportIdCoursesData, new ReportParameter[]
						{
							reportParameter,
							reportParameter2,
							reportParameter3
						}) : dataSyncStudentOptions.ReportManager.ExecuteReport2(dataSyncStudentOptions.ReportCoursesData, null, null, new ReportParameter[]
						{
							reportParameter,
							reportParameter2,
							reportParameter3
						});
						bool flag5 = ((runReportResult2 != null) ? runReportResult2.ReportStatus : null) != null && runReportResult2.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
						result = (flag5 && (flag2 == null || flag2.Value));
					}
					catch (Exception ex2)
					{
						CWLogger.Logger.Error("BatchDataSync:Courses:snum={0}:error={1}", snum, ex2.ToString());
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x040001F7 RID: 503
		private IDataSyncDAO dao;

		// Token: 0x040001F8 RID: 504
		private DataSyncInfoManager dsm;

		// Token: 0x040001F9 RID: 505
		private ReportManager rm;

		// Token: 0x040001FA RID: 506
		private PeopleManager pm;

		// Token: 0x0200033D RID: 829
		internal class CustomTableInfo
		{
			// Token: 0x060016DC RID: 5852 RVA: 0x0000672B File Offset: 0x0000492B
			public CustomTableInfo()
			{
			}

			// Token: 0x060016DD RID: 5853 RVA: 0x00089AE4 File Offset: 0x00087CE4
			public CustomTableInfo(string s)
			{
				int num = s.LastIndexOf(' ');
				this.TableName = s.Substring(0, num).Trim();
				this.Alias = s.Substring(num + 1).Trim();
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x060016DE RID: 5854 RVA: 0x00089B2B File Offset: 0x00087D2B
			// (set) Token: 0x060016DF RID: 5855 RVA: 0x00089B33 File Offset: 0x00087D33
			public string TableName { get; set; }

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00089B3C File Offset: 0x00087D3C
			// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00089B44 File Offset: 0x00087D44
			public string Alias { get; set; }

			// Token: 0x060016E2 RID: 5858 RVA: 0x00089B50 File Offset: 0x00087D50
			public string GetTableNameWithoutCustom()
			{
				bool flag = this.TableName == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = !this.TableName.StartsWith("custom_", StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						result = this.TableName;
					}
					else
					{
						result = this.TableName.Substring(7);
					}
				}
				return result;
			}
		}

		// Token: 0x0200033E RID: 830
		internal class DataSyncStudentOptions
		{
			// Token: 0x1700028F RID: 655
			// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00089BA4 File Offset: 0x00087DA4
			// (set) Token: 0x060016E4 RID: 5860 RVA: 0x00089BAC File Offset: 0x00087DAC
			public DataSyncOperationContext OpContext { get; set; }

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00089BB5 File Offset: 0x00087DB5
			// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00089BBD File Offset: 0x00087DBD
			public IReportManager ReportManager { get; set; }

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00089BC6 File Offset: 0x00087DC6
			// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00089BCE File Offset: 0x00087DCE
			public int ReportIdStudentData { get; set; }

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00089BD7 File Offset: 0x00087DD7
			// (set) Token: 0x060016EA RID: 5866 RVA: 0x00089BDF File Offset: 0x00087DDF
			public int ReportIdCoursesData { get; set; }

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x060016EB RID: 5867 RVA: 0x00089BE8 File Offset: 0x00087DE8
			// (set) Token: 0x060016EC RID: 5868 RVA: 0x00089BF0 File Offset: 0x00087DF0
			public bool UseSingleThread { get; set; }

			// Token: 0x17000294 RID: 660
			// (get) Token: 0x060016ED RID: 5869 RVA: 0x00089BF9 File Offset: 0x00087DF9
			// (set) Token: 0x060016EE RID: 5870 RVA: 0x00089C01 File Offset: 0x00087E01
			public int LastDataSyncControlId { get; set; }

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x060016EF RID: 5871 RVA: 0x00089C0A File Offset: 0x00087E0A
			// (set) Token: 0x060016F0 RID: 5872 RVA: 0x00089C12 File Offset: 0x00087E12
			public TimeSpan AllowedTimeToRun { get; set; }

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00089C1B File Offset: 0x00087E1B
			// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00089C23 File Offset: 0x00087E23
			public Report ReportStudentData { get; set; }

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00089C2C File Offset: 0x00087E2C
			// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00089C34 File Offset: 0x00087E34
			public Report ReportCoursesData { get; set; }
		}
	}
}
