using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000087 RID: 135
	public class LoadAppointments : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001BD09 File Offset: 0x00019F09
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0001BD11 File Offset: 0x00019F11
		public OperationContext OpContext { get; set; }

		// Token: 0x060004EF RID: 1263 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadAppointments()
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001BD1A File Offset: 0x00019F1A
		public LoadAppointments(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001BD2C File Offset: 0x00019F2C
		private DateTime? GetDateTimeFromObject(object obj)
		{
			bool flag = obj == null;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = obj is DateTime;
				if (flag2)
				{
					result = new DateTime?((DateTime)obj);
				}
				else
				{
					string text = obj.ToString();
					DateTime value;
					bool flag3 = text.Length < 1 || !DateTime.TryParse(text, out value);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new DateTime?(value);
					}
				}
			}
			return result;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001BDAC File Offset: 0x00019FAC
		private bool? GetBoolFromObject(object obj)
		{
			bool flag = obj == null;
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = obj is bool;
				if (flag2)
				{
					result = new bool?((bool)obj);
				}
				else
				{
					string text = obj.ToString();
					bool value;
					bool flag3 = text.Length < 1 || !bool.TryParse(text, out value);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new bool?(value);
					}
				}
			}
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001BE2C File Offset: 0x0001A02C
		private IList<int> GetListFromParameters(IList<ReportParameter> parameters, params string[] possibleNames)
		{
			for (int i = 0; i < possibleNames.Length; i++)
			{
				string name = possibleNames[i];
				ReportParameter reportParameter = parameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
				bool flag = reportParameter != null;
				if (flag)
				{
					return this.GetListFromObject(reportParameter.Value);
				}
			}
			return null;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001BE90 File Offset: 0x0001A090
		private IList<int> GetListFromObject(object obj)
		{
			bool flag = obj == null;
			IList<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = obj is IList<int>;
				if (flag2)
				{
					result = (IList<int>)obj;
				}
				else
				{
					bool flag3 = obj is IList<string>;
					if (flag3)
					{
						result = (from h in ((IList<string>)obj).Select(delegate(string g)
						{
							int result2;
							int.TryParse(g ?? "", out result2);
							return result2;
						})
						where h > 0
						select h).ToList<int>();
					}
					else
					{
						result = (from h in obj.ToString().Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
						{
							int result2;
							int.TryParse(g ?? "", out result2);
							return result2;
						})
						where h > 0
						select h).ToList<int>();
					}
				}
			}
			return result;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001BF94 File Offset: 0x0001A194
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			ReportFunctionLoadAppointmentsParameters parameters = defaultFunctionParameter.GetReportFunctionLoadAppointmentsParametersFromXml();
			IList<ReportParameter> list = CurrentReportResult.CurrentReportParameters;
			bool flag = list == null;
			if (flag)
			{
				list = new List<ReportParameter>();
			}
			ReportParameter reportParameter = list.FirstOrDefault((ReportParameter g) => g.Name.Equals("startdate", StringComparison.OrdinalIgnoreCase));
			DateTime? dateTimeFromObject = this.GetDateTimeFromObject((reportParameter == null) ? null : reportParameter.Value);
			ReportParameter reportParameter2 = list.FirstOrDefault((ReportParameter g) => g.Name.Equals("enddate", StringComparison.OrdinalIgnoreCase));
			DateTime? dateTimeFromObject2 = this.GetDateTimeFromObject((reportParameter2 == null) ? null : reportParameter2.Value);
			ReportParameter reportParameter3 = list.FirstOrDefault((ReportParameter g) => g.Name.Equals("includecancelled", StringComparison.OrdinalIgnoreCase));
			bool? flag2 = (reportParameter3 == null) ? null : this.GetBoolFromObject(reportParameter3.Value ?? false);
			IList<int> listFromParameters = this.GetListFromParameters(list, new string[]
			{
				"users",
				"students",
				"staff",
				"pids",
				"personids"
			});
			IList<int> listFromParameters2 = this.GetListFromParameters(list, new string[]
			{
				"groups",
				"gids"
			});
			ReportParameter reportParameter4 = list.FirstOrDefault((ReportParameter g) => g.Name.Equals("type", StringComparison.OrdinalIgnoreCase));
			bool flag3 = reportParameter4 != null && reportParameter4.Value != null;
			if (flag3)
			{
				bool flag4 = reportParameter4.Value is int;
				if (flag4)
				{
					int num = (int)reportParameter4.Value;
					bool flag5 = Enum.IsDefined(typeof(eLoadAppointmentsType), num);
					if (flag5)
					{
						parameters.LoadAppointmentsdMethod = (eLoadAppointmentsType)num;
					}
				}
				else
				{
					string value = reportParameter4.Value.ToString();
					bool flag6 = Enum.IsDefined(typeof(eLoadAppointmentsType), value);
					if (flag6)
					{
						parameters.LoadAppointmentsdMethod = (eLoadAppointmentsType)Enum.Parse(typeof(eLoadAppointmentsType), value);
					}
				}
			}
			bool flag7 = dateTimeFromObject != null;
			if (flag7)
			{
				parameters.StartDate = new DateTime?(dateTimeFromObject.Value);
			}
			bool flag8 = dateTimeFromObject2 != null;
			if (flag8)
			{
				parameters.EndDate = new DateTime?(dateTimeFromObject2.Value);
			}
			bool flag9 = flag2 != null;
			if (flag9)
			{
				parameters.IncludeCancelled = flag2.Value;
			}
			bool flag10 = listFromParameters != null;
			if (flag10)
			{
				parameters.PersonIds = listFromParameters;
			}
			bool flag11 = listFromParameters2 != null;
			if (flag11)
			{
				parameters.GroupIds = listFromParameters2;
			}
			List<int> pids2 = (parameters.PersonIds == null) ? null : parameters.PersonIds.ToList<int>();
			List<int> list2;
			if (parameters.GroupIds != null)
			{
				list2 = (from g in parameters.GroupIds
				where g > 0
				select g).ToList<int>();
			}
			else
			{
				list2 = new List<int>();
			}
			List<int> list3 = list2;
			bool flag12 = list3.Count > 0;
			if (flag12)
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				List<int> list4 = (from g in peopleManager.LoadGroupMembers(list3.ToArray())
				select g.PersonId).ToList<int>();
				bool flag13 = list4.Count > 0;
				if (flag13)
				{
					bool flag14 = pids2 == null;
					if (flag14)
					{
						pids2 = new List<int>();
					}
					pids2.AddRange(from g in list4
					where !pids2.Contains(g)
					select g);
				}
			}
			eLoadAppointmentsType loadAppointmentsdMethod = parameters.LoadAppointmentsdMethod;
			eLoadAppointmentsType eLoadAppointmentsType = loadAppointmentsdMethod;
			if (eLoadAppointmentsType != eLoadAppointmentsType.TestsAndExams)
			{
				if (eLoadAppointmentsType != eLoadAppointmentsType.WorkshopsAndEvents)
				{
					IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
					List<Appointment> apps = appointmentManager.LoadAppointments(pids2, (parameters.AppTypeIds == null) ? null : parameters.AppTypeIds.ToList<int>(), !parameters.IncludeCancelled, false, false, (parameters.StartDate != null) ? parameters.StartDate.Value : DateTime.MinValue, (parameters.EndDate != null) ? parameters.EndDate.Value : DateTime.MinValue);
					Result.Data.Table = this.ConvertAppointmentsListToTable(apps);
				}
			}
			else
			{
				ITestBookingManager testBookingManager = new TestBookingManager(this.OpContext);
				IList<Test> list5 = testBookingManager.LoadTests((parameters.StartDate != null) ? parameters.StartDate.Value : DateTime.Now.AddYears(-100), (parameters.EndDate != null) ? parameters.EndDate.Value : DateTime.Now.AddYears(100), !parameters.IncludeCancelled);
				Func<Attendee, bool> <>9__8;
				list5 = list5.Where(delegate(Test g)
				{
					bool result;
					if (parameters.IncludeCancelled || !g.IsCancelled)
					{
						IEnumerable<Attendee> attendees = g.Attendees;
						Func<Attendee, bool> predicate;
						if ((predicate = <>9__8) == null)
						{
							predicate = (<>9__8 = ((Attendee h) => pids2.Contains(h.Person.PersonId)));
						}
						result = (attendees.FirstOrDefault(predicate) != null);
					}
					else
					{
						result = false;
					}
					return result;
				}).ToList<Test>();
				Result.Data.Table = this.ConvertTestsAndExamsListToTable(list5);
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001C540 File Offset: 0x0001A740
		private DataTable ConvertTestsAndExamsListToTable(IList<Test> tests)
		{
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("appointmentid", typeof(int));
			dataTable.Columns.Add("startdate", typeof(DateTime));
			dataTable.Columns.Add("enddate", typeof(DateTime));
			dataTable.Columns.Add("Title");
			dataTable.Columns.Add("Subtitle");
			dataTable.Columns.Add("Course");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("Room");
			dataTable.Columns.Add("IsCancelled", typeof(bool));
			dataTable.Columns.Add("IsNoshow", typeof(bool));
			dataTable.Columns.Add("Students");
			dataTable.Columns.Add("Staff");
			dataTable.Columns.Add("Memo");
			foreach (Test test in tests)
			{
				List<Attendee> studentAttendees = test.GetStudentAttendees();
				List<Attendee> staffAttendees = test.GetStaffAttendees();
				LookupCourseBase course = test.GetCourse();
				DataRowCollection rows = dataTable.Rows;
				object[] array = new object[13];
				array[0] = test.AppointmentId;
				array[1] = test.StartDateTime;
				array[2] = test.EndDateTime;
				array[3] = ((test.AppType == null) ? "" : (test.AppType.Description ?? ""));
				array[4] = (test.SubTitle ?? "");
				array[5] = ((course == null) ? "" : course.GetCourseDescription());
				array[6] = (test.Location ?? "");
				array[7] = ((test.Room == null) ? "" : (test.Room.RoomDescription ?? ""));
				array[8] = test.IsCancelled;
				int num = 9;
				bool flag;
				if (studentAttendees != null)
				{
					flag = (studentAttendees.FirstOrDefault((Attendee g) => g.IsNoShow) != null);
				}
				else
				{
					flag = false;
				}
				array[num] = flag;
				int num2 = 10;
				object obj;
				if (studentAttendees != null)
				{
					obj = string.Join(Environment.NewLine, (from g in studentAttendees
					select g.Person.GetStudentName()).ToArray<string>());
				}
				else
				{
					obj = "";
				}
				array[num2] = obj;
				int num3 = 11;
				object obj2;
				if (staffAttendees != null)
				{
					obj2 = string.Join(Environment.NewLine, (from g in staffAttendees
					select g.Person.GetName()).ToArray<string>());
				}
				else
				{
					obj2 = "";
				}
				array[num3] = obj2;
				array[12] = ((test.Memo == null) ? "" : test.Memo.ConvertRtfToPlainText());
				rows.Add(array);
			}
			return dataTable;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001C87C File Offset: 0x0001AA7C
		private DataTable ConvertAppointmentsListToTable(IList<Appointment> apps)
		{
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("appointmentid", typeof(int));
			dataTable.Columns.Add("startdate", typeof(DateTime));
			dataTable.Columns.Add("enddate", typeof(DateTime));
			dataTable.Columns.Add("Title");
			dataTable.Columns.Add("Subtitle");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("Room");
			dataTable.Columns.Add("IsCancelled", typeof(bool));
			dataTable.Columns.Add("IsNoshow", typeof(bool));
			dataTable.Columns.Add("Students");
			dataTable.Columns.Add("Staff");
			dataTable.Columns.Add("Memo");
			foreach (Appointment appointment in apps)
			{
				List<Attendee> studentAttendees = appointment.GetStudentAttendees();
				List<Attendee> staffAttendees = appointment.GetStaffAttendees();
				DataRowCollection rows = dataTable.Rows;
				object[] array = new object[12];
				array[0] = appointment.AppointmentId;
				array[1] = appointment.StartDateTime;
				array[2] = appointment.EndDateTime;
				array[3] = ((appointment.AppType == null) ? "" : (appointment.AppType.Description ?? ""));
				array[4] = (appointment.SubTitle ?? "");
				array[5] = (appointment.Location ?? "");
				array[6] = ((appointment.Room == null) ? "" : (appointment.Room.RoomDescription ?? ""));
				array[7] = appointment.IsCancelled;
				int num = 8;
				bool flag;
				if (studentAttendees != null)
				{
					flag = (studentAttendees.FirstOrDefault((Attendee g) => g.IsNoShow) != null);
				}
				else
				{
					flag = false;
				}
				array[num] = flag;
				int num2 = 9;
				object obj;
				if (studentAttendees != null)
				{
					obj = string.Join(Environment.NewLine, (from g in studentAttendees
					select g.Person.GetStudentName()).ToArray<string>());
				}
				else
				{
					obj = "";
				}
				array[num2] = obj;
				int num3 = 10;
				object obj2;
				if (staffAttendees != null)
				{
					obj2 = string.Join(Environment.NewLine, (from g in staffAttendees
					select g.Person.GetName()).ToArray<string>());
				}
				else
				{
					obj2 = "";
				}
				array[num3] = obj2;
				array[11] = ((appointment.Memo == null) ? "" : appointment.Memo.ConvertRtfToPlainText());
				rows.Add(array);
			}
			return dataTable;
		}
	}
}
