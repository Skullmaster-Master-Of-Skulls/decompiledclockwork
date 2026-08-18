using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000065 RID: 101
	public class CrossReferenceActiveCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00017D18 File Offset: 0x00015F18
		public CrossReferenceActiveCourses()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00017D33 File Offset: 0x00015F33
		public CrossReferenceActiveCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017D51 File Offset: 0x00015F51
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00017D59 File Offset: 0x00015F59
		public OperationContext OpContext { get; set; }

		// Token: 0x06000435 RID: 1077 RVA: 0x00017D64 File Offset: 0x00015F64
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null && primaryDataTable.Columns.Count > 0;
			if (flag)
			{
				bool flag2 = !primaryDataTable.Columns.Contains("student_no");
				if (flag2)
				{
					throw new Exception("Missing student_no column for CrossReferenceActiveCourses");
				}
				SessionManager sessionManager = new SessionManager(this.OpContext);
				Session currentSession = sessionManager.GetCurrentSession();
				PeopleManager peopleManager = new PeopleManager(this.OpContext);
				CourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
				int num = 500;
				int num2 = 0;
				List<List<string>> list = new List<List<string>>();
				List<string> list2 = new List<string>();
				list.Add(list2);
				foreach (object obj in primaryDataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow["student_no"].ToString().Trim().ToUpper();
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						num2++;
						bool flag4 = num2 >= num;
						if (flag4)
						{
							num2 = 0;
							list2 = new List<string>();
							list.Add(list2);
						}
						list2.Add(text);
					}
				}
				List<CourseRegistration> list3 = new List<CourseRegistration>();
				foreach (List<string> studentNumbers in list)
				{
					IList<int> personIds = peopleManager.LoadPersonIdsByStudentNumbers(studentNumbers);
					IList<CourseRegistration> source = courseRegistrationManager.LoadStudentsCoursesBatch(currentSession.StartDate, currentSession.EndDate, personIds, false);
					list3.AddRange(source.ToList<CourseRegistration>());
				}
				bool flag5 = !primaryDataTable.Columns.Contains("LuCourseId");
				if (flag5)
				{
					primaryDataTable.Columns.Add("LuCourseId", typeof(int));
				}
				bool flag6 = !primaryDataTable.Columns.Contains("Course");
				if (flag6)
				{
					primaryDataTable.Columns.Add("Course");
				}
				bool flag7 = !primaryDataTable.Columns.Contains("PrimaryInstructor");
				if (flag7)
				{
					primaryDataTable.Columns.Add("PrimaryInstructor");
				}
				bool flag8 = !primaryDataTable.Columns.Contains("PrimaryInstructorPhone");
				if (flag8)
				{
					primaryDataTable.Columns.Add("PrimaryInstructorPhone");
				}
				bool flag9 = !primaryDataTable.Columns.Contains("PrimaryInstructorEmail");
				if (flag9)
				{
					primaryDataTable.Columns.Add("PrimaryInstructorEmail");
				}
				DataTable dataTable = primaryDataTable.Clone();
				DataView dataView = new DataView(primaryDataTable);
				dataView.Sort = "student_no";
				int j;
				for (int i = 0; i < dataView.Count; i = j)
				{
					DataRow row = dataView[i].Row;
					string snum0 = row["student_no"].ToString().Trim();
					for (j = i + 1; j < dataView.Count; j++)
					{
						DataRow row2 = dataView[j].Row;
						string a = row2["student_no"].ToString().Trim();
						bool flag10 = a != snum0;
						if (flag10)
						{
							break;
						}
					}
					List<CourseRegistration> list4 = list3.FindAll((CourseRegistration g) => g.Student.Student_no.Equals(snum0, StringComparison.OrdinalIgnoreCase));
					bool flag11 = list4.Count > 0;
					if (flag11)
					{
						foreach (CourseRegistration courseRegistration in list4)
						{
							string courseDescription = courseRegistration.Course.GetCourseDescription();
							for (int k = i; k < j; k++)
							{
								DataRow row3 = dataView[k].Row;
								row3["LuCourseId"] = courseRegistration.Course.LuCourseId;
								row3["course"] = courseDescription;
								dataTable.ImportRow(row3);
							}
						}
					}
					else
					{
						dataTable.ImportRow(row);
					}
				}
				result.Data.Table = dataTable;
			}
		}

		// Token: 0x040000C1 RID: 193
		private ReportDAO dao;
	}
}
