using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000141 RID: 321
	public class TestExamBookingViewManager : ITestExamBookingViewManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0006A0A3 File Offset: 0x000682A3
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0006A0AB File Offset: 0x000682AB
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E32 RID: 3634 RVA: 0x0006A0B4 File Offset: 0x000682B4
		public TestExamBookingViewManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0006A0C8 File Offset: 0x000682C8
		private void AddExtendedInfoToTest<T>(T test) where T : TestBookingSmall
		{
			IList<string> list;
			this.AddExtendedInfoToTest<T>(test, out list);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0006A0E0 File Offset: 0x000682E0
		private void AddExtendedInfoToTest<T>(T test, out IList<string> extendedColumnNames) where T : TestBookingSmall
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_TestsExams_ExtendedInfoReportId);
			bool flag = settingValue_Int < 1;
			if (flag)
			{
				extendedColumnNames = new List<string>();
			}
			else
			{
				RunReportResult runReportResult = this.ExecuteExtendedBookingsReport(settingValue_Int, null, true, test.AppointmentId);
				bool flag2 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully || runReportResult.PrimaryData.Table == null;
				if (flag2)
				{
					extendedColumnNames = new List<string>();
				}
				else
				{
					DataTable table = runReportResult.PrimaryData.Table;
					Dictionary<int, string> colMappingsFromTable = this.GetColMappingsFromTable(table);
					extendedColumnNames = this.GetExtendedColumnNamesOrdered(colMappingsFromTable);
					bool flag3 = this.AddExtendedInfoToTest<T>(test, table, colMappingsFromTable);
					bool flag4 = flag3;
					if (!flag4)
					{
						IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
						BaseBasicAppointment baseBasicAppointment = baseAppointmentManager.LoadBaseBasicAppointmentById(test.AppointmentId);
						bool flag5 = baseBasicAppointment == null;
						if (!flag5)
						{
							this.AddExtendedInfoToTests<T>(new List<T>
							{
								test
							}, new BookingsManagementContext
							{
								LoadExtendedInfo = true,
								ReportId = 0
							}, new DateTime?(baseBasicAppointment.StartDateTime.Date), new DateTime?(baseBasicAppointment.StartDateTime.Date), false, out extendedColumnNames);
						}
					}
				}
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0006A238 File Offset: 0x00068438
		private RunReportResult ExecuteExtendedBookingsReport(int reportId, Range<DateTime> dateRange, bool allowCancelled, int overrideAppointmentId = 0)
		{
			ReportParameter[] parameters = new ReportParameter[]
			{
				new ReportParameter
				{
					Name = "StartDate",
					Value = ((dateRange != null) ? dateRange.Start : DBNull.Value)
				},
				new ReportParameter
				{
					Name = "EndDate",
					Value = ((dateRange != null) ? dateRange.End : null)
				},
				new ReportParameter
				{
					Name = "AllowCancelled",
					Value = allowCancelled
				},
				new ReportParameter
				{
					Name = "appid",
					Value = overrideAppointmentId
				}
			};
			IReportManager reportManager = new ReportManager(this.OpContext);
			return reportManager.ExecuteReport2(reportId, parameters);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0006A304 File Offset: 0x00068504
		private IList<string> GetExtendedColumnNamesOrdered(IDictionary<int, string> colMappings)
		{
			bool flag = colMappings.Count < 1;
			IList<string> result;
			if (flag)
			{
				result = new List<string>();
			}
			else
			{
				int num = colMappings.Keys.Max();
				string[] array = new string[num];
				array.Initialize();
				foreach (KeyValuePair<int, string> keyValuePair in colMappings)
				{
					array[keyValuePair.Key - 1] = keyValuePair.Value;
				}
				result = array.ToList<string>();
			}
			return result;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0006A39C File Offset: 0x0006859C
		private IList<T> AddExtendedInfoToTests<T>(IList<T> tests, BookingsManagementContext Context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled, out IList<string> extendedColumnNames) where T : TestBookingSmall
		{
			bool flag = !Context.LoadExtendedInfo;
			IList<T> result;
			if (flag)
			{
				extendedColumnNames = new List<string>();
				result = tests;
			}
			else
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_TestsExams_ExtendedInfoReportId);
				bool flag2 = settingValue_Int < 1;
				if (flag2)
				{
					extendedColumnNames = new List<string>();
					result = tests;
				}
				else
				{
					RunReportResult runReportResult = this.ExecuteExtendedBookingsReport(settingValue_Int, new Range<DateTime>((StartDate != null) ? StartDate.Value : new DateTime(1970, 1, 1), (EndDate != null) ? EndDate.Value : new DateTime(DateTime.Now.Year + 100, 1, 1)), !HideCancelled, 0);
					bool flag3 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully || runReportResult.PrimaryData.Table == null;
					if (flag3)
					{
						extendedColumnNames = new List<string>();
						result = tests;
					}
					else
					{
						DataTable table = runReportResult.PrimaryData.Table;
						Dictionary<int, string> colMappingsFromTable = this.GetColMappingsFromTable(table);
						bool flag4 = colMappingsFromTable.Count <= 0;
						if (flag4)
						{
							extendedColumnNames = new List<string>();
							result = tests;
						}
						else
						{
							extendedColumnNames = this.GetExtendedColumnNamesOrdered(colMappingsFromTable);
							bool flag5 = table.Rows.Count <= 0;
							if (flag5)
							{
								result = tests;
							}
							else
							{
								foreach (T item in tests)
								{
									this.AddExtendedInfoToTest<T>(item, table, colMappingsFromTable);
								}
								result = tests;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0006A550 File Offset: 0x00068750
		private Dictionary<int, string> GetColMappingsFromTable(DataTable table)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				bool flag = dataColumn.ColumnName.StartsWith("custom");
				if (flag)
				{
					int num = dataColumn.ColumnName.IndexOf("_");
					bool flag2 = num <= 0;
					if (!flag2)
					{
						int key;
						bool flag3 = int.TryParse(dataColumn.ColumnName.Substring(0, num).Substring(6), out key);
						if (flag3)
						{
							dictionary.Add(key, dataColumn.ColumnName);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0006A620 File Offset: 0x00068820
		private bool AddExtendedInfoToTest<T>(T item, DataTable table, Dictionary<int, string> colMappings) where T : TestBookingSmall
		{
			int appointmentId = item.AppointmentId;
			bool flag = appointmentId > 0;
			if (flag)
			{
				DataRow[] array = table.Select("appointmentid=" + appointmentId.ToString());
				bool flag2 = array.Length != 0;
				if (flag2)
				{
					DataRow dataRow = array[0];
					foreach (KeyValuePair<int, string> keyValuePair in colMappings)
					{
						switch (keyValuePair.Key)
						{
						case 1:
							item.Custom1 = dataRow[keyValuePair.Value].ToString();
							break;
						case 2:
							item.Custom2 = dataRow[keyValuePair.Value].ToString();
							break;
						case 3:
							item.Custom3 = dataRow[keyValuePair.Value].ToString();
							break;
						case 4:
							item.Custom4 = dataRow[keyValuePair.Value].ToString();
							break;
						case 5:
							item.Custom5 = dataRow[keyValuePair.Value].ToString();
							break;
						case 6:
							item.Custom6 = dataRow[keyValuePair.Value].ToString();
							break;
						case 7:
							item.Custom7 = dataRow[keyValuePair.Value].ToString();
							break;
						case 8:
							item.Custom8 = dataRow[keyValuePair.Value].ToString();
							break;
						case 9:
							item.Custom9 = dataRow[keyValuePair.Value].ToString();
							break;
						case 10:
							item.Custom10 = dataRow[keyValuePair.Value].ToString();
							break;
						case 11:
							item.Custom11 = dataRow[keyValuePair.Value].ToString();
							break;
						case 12:
							item.Custom12 = dataRow[keyValuePair.Value].ToString();
							break;
						case 13:
							item.Custom13 = dataRow[keyValuePair.Value].ToString();
							break;
						case 14:
							item.Custom14 = dataRow[keyValuePair.Value].ToString();
							break;
						case 15:
							item.Custom15 = dataRow[keyValuePair.Value].ToString();
							break;
						case 16:
							item.Custom16 = dataRow[keyValuePair.Value].ToString();
							break;
						case 17:
							item.Custom17 = dataRow[keyValuePair.Value].ToString();
							break;
						case 18:
							item.Custom18 = dataRow[keyValuePair.Value].ToString();
							break;
						case 19:
							item.Custom19 = dataRow[keyValuePair.Value].ToString();
							break;
						case 20:
							item.Custom20 = dataRow[keyValuePair.Value].ToString();
							break;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0006AA00 File Offset: 0x00068C00
		private IList<int> GetControlIdsFromString(string cidsString)
		{
			IEnumerable<string> enumerable = from g in cidsString.Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h;
			bool flag = cidsString.IndexOf('=') <= 0;
			IList<int> result;
			if (flag)
			{
				result = enumerable.Select(delegate(string g)
				{
					int result2;
					int.TryParse(g, out result2);
					return result2;
				}).ToList<int>();
			}
			else
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (string text in enumerable)
				{
					int num = text.IndexOf('=');
					bool flag2 = num <= 0;
					if (!flag2)
					{
						string key = text.Substring(0, num).ToLower();
						string s = text.Substring(num + 1);
						int value;
						int.TryParse(s, out value);
						bool flag3 = !dictionary.ContainsKey(key);
						if (flag3)
						{
							dictionary.Add(key, value);
						}
					}
				}
				List<string> list = dictionary.Keys.ToList<string>();
				list.Sort((string g1, string g2) => g1.CompareTo(g2));
				result = (from g in list
				select dictionary[g]).ToList<int>();
			}
			return result;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0006ABB8 File Offset: 0x00068DB8
		public IList<TestBookingFull> LoadTestsFull(BookingsManagementContext context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled, out IList<string> extendedColumnNames)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AssignedCounsellorCid);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			IList<TestBookingFull> tests = new List<TestBookingFull>();
			bool flag = context.ReportId > 0;
			if (flag)
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				RunReportResult runReportResult = reportManager.ExecuteReport2(context.ReportId, new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "StartDate",
						Value = ((StartDate != null) ? StartDate.Value : null)
					},
					new ReportParameter
					{
						Name = "EndDate",
						Value = ((EndDate != null) ? EndDate.Value : null)
					},
					new ReportParameter
					{
						Name = "HideCancelled",
						Value = HideCancelled
					},
					new ReportParameter
					{
						Name = "AssignedCounsellorCid",
						Value = settingValue_Int
					}
				});
				bool flag2 = runReportResult != null && runReportResult.PrimaryData != null && runReportResult.PrimaryData.Table != null;
				if (flag2)
				{
					try
					{
						IDataReader reader = runReportResult.PrimaryData.Table.CreateDataReader();
						tests = testExamBookingViewDAO.LoadTestsFull(reader);
						return this.AddExtendedInfoToTests<TestBookingFull>(tests, context, StartDate, EndDate, HideCancelled, out extendedColumnNames);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("TestExamBookingViewManager::LoadTestsFull: {0}", ex.ToString()), ex);
						extendedColumnNames = new List<string>();
						return new List<TestBookingFull>();
					}
				}
			}
			tests = testExamBookingViewDAO.LoadTestsFull(StartDate, EndDate, HideCancelled, settingValue_Int);
			return this.AddExtendedInfoToTests<TestBookingFull>(tests, context, StartDate, EndDate, HideCancelled, out extendedColumnNames);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0006AD94 File Offset: 0x00068F94
		public IList<TestBookingSmall> LoadTestsSmall(BookingsManagementContext context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled, out IList<string> extendedColumnNames)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AssignedCounsellorCid);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			IList<TestBookingSmall> tests = new List<TestBookingSmall>();
			bool flag = context.ReportId > 0;
			if (flag)
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				RunReportResult runReportResult = reportManager.ExecuteReport2(context.ReportId, new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "StartDate",
						Value = ((StartDate != null) ? StartDate.Value : null)
					},
					new ReportParameter
					{
						Name = "EndDate",
						Value = ((EndDate != null) ? EndDate.Value : null)
					},
					new ReportParameter
					{
						Name = "HideCancelled",
						Value = HideCancelled
					},
					new ReportParameter
					{
						Name = "AssignedCounsellorCid",
						Value = settingValue_Int
					}
				});
				bool flag2 = runReportResult != null && runReportResult.PrimaryData != null && runReportResult.PrimaryData.Table != null;
				if (flag2)
				{
					try
					{
						IDataReader reader = runReportResult.PrimaryData.Table.CreateDataReader();
						tests = testExamBookingViewDAO.LoadTestsSmall(reader);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("TestExamBookingViewManager::LoadTestsFull: {0}", ex.ToString()), ex);
						tests = new List<TestBookingSmall>();
					}
				}
			}
			tests = testExamBookingViewDAO.LoadTestsSmall(StartDate, EndDate, HideCancelled, settingValue_Int);
			return this.AddExtendedInfoToTests<TestBookingSmall>(tests, context, StartDate, EndDate, HideCancelled, out extendedColumnNames);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0006AF58 File Offset: 0x00069158
		public IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(ClassTestDefinitionsManagementContext context, DateTime? StartDate, DateTime? EndDate, out IList<string> extendedColumnNames)
		{
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_InstructorFormCidsToShowInMasterList, false);
			bool flag = settingValue_String == null || settingValue_String.Trim().Length < 1;
			IList<ClassTestDefinitionSmall> result;
			if (flag)
			{
				extendedColumnNames = new List<string>();
				result = testExamBookingViewDAO.LoadClassTestDefinitionsSmall(StartDate, EndDate);
			}
			else
			{
				IList<int> controlIdsFromString = this.GetControlIdsFromString(settingValue_String);
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				List<DynamicField> fields = dynamicFieldManager.LoadFieldsByControlIds(controlIdsFromString.ToList<int>());
				extendedColumnNames = (from cid in controlIdsFromString
				select fields.FirstOrDefault((DynamicField f) => f.ControlId == cid) into field
				select (field != null) ? (field.ControlCaption ?? string.Empty) : string.Empty).ToList<string>();
				result = testExamBookingViewDAO.LoadClassTestDefinitionsSmallWithExtendedInfo(StartDate, EndDate, controlIdsFromString.ToArray<int>());
			}
			return result;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0006B04C File Offset: 0x0006924C
		public IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(UnBookedStudentMmanagementContext context)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_OnlyShowLetterIssued);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			IList<UnbookedStudentsSmall> list = new List<UnbookedStudentsSmall>();
			bool flag = context.ReportId > 0;
			if (flag)
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				RunReportResult runReportResult = reportManager.ExecuteReport2(context.ReportId, new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "OnlyShowLetterIssued",
						Value = settingValue_Bool
					}
				});
				bool flag2 = runReportResult != null && runReportResult.PrimaryData != null && runReportResult.PrimaryData.Table != null;
				if (flag2)
				{
					try
					{
						IDataReader reader = runReportResult.PrimaryData.Table.CreateDataReader();
						return testExamBookingViewDAO.LoadUnbookedStudentsSmall(reader);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("TestExamBookingViewManager::LoadUnbookedStudentsSmall: {0}", ex.ToString()), ex);
						return new List<UnbookedStudentsSmall>();
					}
				}
			}
			return testExamBookingViewDAO.LoadUnbookedStudentsSmall(settingValue_Bool);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0006B178 File Offset: 0x00069378
		public TestBookingFull LoadTestFullByAppId(BookingsManagementContext context, int appId)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AssignedCounsellorCid);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			TestBookingFull testBookingFull = testExamBookingViewDAO.LoadTestFullByAppId(appId, settingValue_Int);
			bool flag = testBookingFull == null || !context.LoadExtendedInfo;
			TestBookingFull result;
			if (flag)
			{
				result = testBookingFull;
			}
			else
			{
				this.AddExtendedInfoToTest<TestBookingFull>(testBookingFull);
				result = testBookingFull;
			}
			return result;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0006B1E8 File Offset: 0x000693E8
		public TestBookingSmall LoadTestSmallByAppId(BookingsManagementContext context, int appId)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AssignedCounsellorCid);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			TestBookingSmall testBookingSmall = testExamBookingViewDAO.LoadTestSmallByAppId(appId, settingValue_Int);
			bool flag = testBookingSmall == null || !context.LoadExtendedInfo;
			TestBookingSmall result;
			if (flag)
			{
				result = testBookingSmall;
			}
			else
			{
				this.AddExtendedInfoToTest<TestBookingSmall>(testBookingSmall);
				result = testBookingSmall;
			}
			return result;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0006B258 File Offset: 0x00069458
		public ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamId(ClassTestDefinitionsManagementContext context, int examId)
		{
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_InstructorFormCidsToShowInMasterList, false);
			bool flag = settingValue_String == null || settingValue_String.Trim().Length < 1;
			ClassTestDefinitionSmall result;
			if (flag)
			{
				result = testExamBookingViewDAO.LoadClassTestDefinitionSmallByExamId(examId);
			}
			else
			{
				IList<int> controlIdsFromString = this.GetControlIdsFromString(settingValue_String);
				result = testExamBookingViewDAO.LoadClassTestDefinitionSmallByExamIdWithExtendedInfo(examId, controlIdsFromString.ToArray<int>());
			}
			return result;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0006B2D8 File Offset: 0x000694D8
		public IList<TestBookingFull> LoadTestsFullByExamId(BookingsManagementContext context, int ExamId)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(this.OpContext);
			IList<int> source = testBookingManager.LoadAppointmentIdsByExamId(ExamId);
			return this.LoadTestsFullByAppointmentIds(context, source.ToArray<int>());
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0006B30C File Offset: 0x0006950C
		public IList<TestBookingFull> LoadTestsFullByAppointmentIds(BookingsManagementContext context, params int[] appIds)
		{
			List<TestBookingFull> list = new List<TestBookingFull>();
			bool flag = appIds == null;
			IList<TestBookingFull> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				list.AddRange(from appId in appIds
				select this.LoadTestFullByAppId(context, appId) into test
				where test != null
				select test);
				result = list;
			}
			return result;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0006B384 File Offset: 0x00069584
		public IList<TestBookingSmall> LoadTestsSmallByExamId(BookingsManagementContext context, int ExamId)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(this.OpContext);
			IList<int> source = testBookingManager.LoadAppointmentIdsByExamId(ExamId);
			return this.LoadTestsSmallByAppointmentIds(context, source.ToArray<int>());
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0006B3B8 File Offset: 0x000695B8
		public IList<TestBookingSmall> LoadTestsSmallByAppointmentIds(BookingsManagementContext context, params int[] appIds)
		{
			List<TestBookingSmall> list = new List<TestBookingSmall>();
			bool flag = appIds == null;
			IList<TestBookingSmall> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				list.AddRange(from appId in appIds
				select this.LoadTestSmallByAppId(context, appId) into test
				where test != null
				select test);
				result = list;
			}
			return result;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0006B430 File Offset: 0x00069630
		public void SaveTestExamBookingLayoutToCentralizedSetting(eTestExamBookingGridViewType view, string layoutCompressed)
		{
			TestExamBookingGridViewTypeAttribute attribute = view.GetAttribute<TestExamBookingGridViewTypeAttribute>();
			bool flag = attribute == null || attribute.SettingToStoreDataIn == null;
			if (flag)
			{
				CWLogger.Logger.Warn("TestExamBookingViewManager:SaveTestExamBookingLayoutToCentralizedSetting:Can't save layout because view has no associated setting:view={0}", view.ToString());
			}
			else
			{
				eSettingCode value = attribute.SettingToStoreDataIn.Value;
				OldUserSetting item = new OldUserSetting
				{
					SettingCode = value,
					ModificationStatus = eDataItemModificationStatus.Modified,
					PersonOrGroupId = -1,
					StringVal = (layoutCompressed ?? "")
				};
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				oldUserSettingManager.SaveSettings(new List<OldUserSetting>
				{
					item
				});
				oldUserSettingManager.ClearCacheForUser(this.OpContext.WhoAmI);
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0006B4FC File Offset: 0x000696FC
		public void ClearTestExamBookingLayoutInCentralizedSetting(eTestExamBookingGridViewType view)
		{
			TestExamBookingGridViewTypeAttribute attribute = view.GetAttribute<TestExamBookingGridViewTypeAttribute>();
			bool flag = attribute == null || attribute.SettingToStoreDataIn == null;
			if (flag)
			{
				CWLogger.Logger.Warn("TestExamBookingViewManager:ClearTestExamBookingLayoutInCentralizedSetting:Can't clear layout because view has no associated setting:view={0}", view.ToString());
			}
			else
			{
				eSettingCode value = attribute.SettingToStoreDataIn.Value;
				OldUserSetting item = new OldUserSetting
				{
					SettingCode = value,
					ModificationStatus = eDataItemModificationStatus.Deleted,
					PersonOrGroupId = -1
				};
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				oldUserSettingManager.SaveSettings(new List<OldUserSetting>
				{
					item
				});
				oldUserSettingManager.ClearCacheForUser(this.OpContext.WhoAmI);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0006B5B4 File Offset: 0x000697B4
		public IList<UnbookedTestExamStudent> LoadUnbookedTestExamStudents()
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_OnlyShowLetterIssued, true);
			ITestExamBookingViewDAO testExamBookingViewDAO = new TestExamBookingViewDAO(this.OpContext);
			return testExamBookingViewDAO.LoadUnbookedTestExamStudents(settingValue_Bool, true);
		}
	}
}
