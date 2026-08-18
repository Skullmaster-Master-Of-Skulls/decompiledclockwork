using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000092 RID: 146
	public class ActiveStudentsWithCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0000672B File Offset: 0x0000492B
		public ActiveStudentsWithCourses()
		{
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001E610 File Offset: 0x0001C810
		public ActiveStudentsWithCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001E624 File Offset: 0x0001C824
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			ActiveStudentsWithAccommodations.ExtractStartDateAndEndDateFromParameters(Function, out dateTime, out dateTime2);
			bool flag = dateTime == null || dateTime2 == null;
			if (flag)
			{
				throw new Exception("Invalid or missing startdate/enddate");
			}
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "startdate",
				Value = dateTime.Value
			});
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "enddate",
				Value = dateTime2.Value
			});
			bool flag2 = false;
			bool flag3 = Function.FunctionParameters != null;
			if (flag3)
			{
				ReportParameter reportParameter = Function.FunctionParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("includedroppedcourses", StringComparison.OrdinalIgnoreCase));
				bool flag4 = reportParameter != null && reportParameter.Value != null;
				if (flag4)
				{
					bool flag5 = reportParameter.Value is bool;
					if (flag5)
					{
						flag2 = (bool)reportParameter.Value;
					}
					else
					{
						bool flag6 = reportParameter.Value is int;
						if (flag6)
						{
							flag2 = ((int)reportParameter.Value == 1);
						}
						else
						{
							string value = reportParameter.Value.ToString();
							bool flag8;
							bool flag7 = bool.TryParse(value, out flag8);
							if (flag7)
							{
								flag2 = flag8;
							}
						}
					}
				}
			}
			OperationContext operationContext;
			if ((operationContext = this.OpContext) == null)
			{
				(operationContext = new OperationContext()).WhoAmI = 1;
			}
			OperationContext operationContext2 = operationContext;
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(operationContext2);
			IList<CourseRegistration> list = courseRegistrationManager.LoadActiveStudentsWithCourses(dateTime.Value, dateTime2.Value, flag2);
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("MiddleName");
			dataTable.Columns.Add("StudentNumber");
			bool flag9 = flag2;
			if (flag9)
			{
				dataTable.Columns.Add("CourseRegistrationStatus");
			}
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(operationContext2);
			int whoAmI = operationContext2.WhoAmI;
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_CoursesUseDuration);
			bool settingValue_Bool2 = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_CoursesUseTimeOfDay);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(whoAmI, eSettingCode.SETTING_CoursesDurationOverrideName, false);
			string settingValue_String2 = oldUserSettingManager.GetSettingValue_String(whoAmI, eSettingCode.SETTING_CoursesTermOverrideName, false);
			string settingValue_String3 = oldUserSettingManager.GetSettingValue_String(whoAmI, eSettingCode.SETTING_CoursesTimeOfDayOverrideName, false);
			string columnName = string.IsNullOrEmpty(settingValue_String) ? "Duration" : settingValue_String;
			string columnName2 = string.IsNullOrEmpty(settingValue_String2) ? "Term" : settingValue_String2;
			string columnName3 = string.IsNullOrEmpty(settingValue_String3) ? "ClassType" : settingValue_String3;
			dataTable.Columns.Add("lucourseid", typeof(int));
			bool flag10 = settingValue_Bool;
			if (flag10)
			{
				dataTable.Columns.Add(columnName);
			}
			dataTable.Columns.Add(columnName2);
			dataTable.Columns.Add("Subject");
			dataTable.Columns.Add("CourseCode");
			dataTable.Columns.Add("Section");
			bool flag11 = settingValue_Bool2;
			if (flag11)
			{
				dataTable.Columns.Add(columnName3);
			}
			dataTable.Columns.Add("Instructor");
			list.ToList<CourseRegistration>().Sort((CourseRegistration g1, CourseRegistration g2) => g1.Student.GetStudentName().CompareTo(g2.Student.GetStudentName()));
			foreach (CourseRegistration courseRegistration in list)
			{
				DataRow dataRow = dataTable.NewRow();
				PersonBase student = courseRegistration.Student;
				dataRow["personid"] = student.PersonId;
				dataRow["LastName"] = (student.LastName ?? "");
				dataRow["FirstName"] = (student.FirstName ?? "");
				dataRow["MiddleName"] = (student.MiddleName ?? "");
				dataRow["StudentNumber"] = (student.Student_no ?? "");
				dataRow["lucourseid"] = courseRegistration.Course.LuCourseId;
				bool flag12 = settingValue_Bool;
				if (flag12)
				{
					dataRow[columnName] = (courseRegistration.Course.Duration ?? "");
				}
				bool flag13 = settingValue_Bool2;
				if (flag13)
				{
					dataRow[columnName3] = (courseRegistration.Course.TimeOfDay ?? "");
				}
				dataRow[columnName2] = (courseRegistration.Course.Term ?? "");
				dataRow["Subject"] = (courseRegistration.Course.Subject.SubjectCode ?? "");
				dataRow["CourseCode"] = (courseRegistration.Course.Course ?? "");
				dataRow["Section"] = (courseRegistration.Course.Section ?? "");
				dataRow["Instructor"] = courseRegistration.Course.GetPrimaryInstructor();
				bool flag14 = flag2;
				if (flag14)
				{
					dataRow["CourseRegistrationStatus"] = ((courseRegistration.RegistrationStatus == eRegistrationStatus.Dropped) ? "Dropped" : "Registered");
				}
				dataTable.Rows.Add(dataRow);
			}
			Result.Data.Table = dataTable;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001EC04 File Offset: 0x0001CE04
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		public OperationContext OpContext { get; set; }
	}
}
