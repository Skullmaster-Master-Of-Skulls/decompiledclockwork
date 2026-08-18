using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x0200004D RID: 77
	public class ClockWorkDataSyncCourses
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0004C1AC File Offset: 0x0004B1AC
		public static List<DataSyncCourseAction> DataSyncCourses(int pid, DataTable t)
		{
			List<DataSyncCourseAction> list = new List<DataSyncCourseAction>();
			if (t.Columns.Contains("instructor") && !t.Columns.Contains("instructorname"))
			{
				t.Columns["instructor"].ColumnName = "instructorname";
			}
			if (!t.Columns.Contains("monstartminutes") && t.Columns.Contains("dayofweek") && t.Columns.Contains("starttime") && t.Columns.Contains("endtime"))
			{
				string[] array = new string[]
				{
					"sun",
					"mon",
					"tue",
					"wed",
					"thu",
					"fri",
					"sat"
				};
				string[] array2 = new string[]
				{
					"sunstartminutes",
					"sunendminutes",
					"monstartminutes",
					"monendminutes",
					"tuestartminutes",
					"tueendminutes",
					"wedstartminutes",
					"wedendminutes",
					"thustartminutes",
					"thuendminutes",
					"fristartminutes",
					"friendminutes",
					"satstartminutes",
					"satendminutes"
				};
				string[] array3 = new string[]
				{
					"sunroom",
					"monroom",
					"tueroom",
					"wedroom",
					"thuroom",
					"friroom",
					"satroom"
				};
				foreach (string text in array2)
				{
					if (!t.Columns.Contains(text))
					{
						t.Columns.Add(text, typeof(int));
					}
				}
				foreach (string text in array3)
				{
					if (!t.Columns.Contains(text))
					{
						t.Columns.Add(text);
					}
				}
				foreach (object obj in t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = dataRow["dayofweek"].ToString().Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						string str = string.Format("{0}-01-01 ", DateTime.Now.Year.ToString());
						string s = str + dataRow["starttime"].ToString().Trim();
						string s2 = str + dataRow["endtime"].ToString().Trim();
						DateTime d;
						DateTime d2;
						if (DateTime.TryParse(s, out d) && DateTime.TryParse(s2, out d2))
						{
							string value = t.Columns.Contains("room") ? dataRow["room"].ToString().Trim() : "";
							int num = Convert.ToInt32((d - d.Date).TotalMinutes);
							int num2 = Convert.ToInt32((d2 - d2.Date).TotalMinutes);
							dataRow[text2 + "startminutes"] = num;
							dataRow[text2 + "endminutes"] = num2;
							dataRow[text2 + "room"] = value;
						}
					}
				}
			}
			string[] array5 = new string[]
			{
				"timeofday",
				"duration",
				"campus",
				"department",
				"location",
				"instructorname",
				"instructoremail",
				"instructorphone",
				"instructorusername",
				"sunstartminutes",
				"sunendminutes",
				"monstartminutes",
				"monendminutes",
				"tuestartminutes",
				"tueendminutes",
				"wedstartminutes",
				"wedendminutes",
				"thustartminutes",
				"thuendminutes",
				"fristartminutes",
				"friendminutes",
				"satstartminutes",
				"satendminutes",
				"sunroom",
				"monroom",
				"tueroom",
				"wedroom",
				"thuroom",
				"friroom",
				"satroom"
			};
			foreach (string text3 in array5)
			{
				if (!t.Columns.Contains(text3))
				{
					t.Columns.Add(text3);
				}
			}
			List<DataSyncExternalCourse> list2 = DataSyncExternalCourse.ParseExternalCourses(t);
			List<DataSyncTermScope> list3 = ClockWorkDataSyncCourses.ExtractTermScopes(list2);
			DataSyncTermScope termScope;
			foreach (DataSyncTermScope termScope2 in list3)
			{
				termScope = termScope2;
				List<DataSyncExternalCourse> list4 = list2.FindAll((DataSyncExternalCourse ec) => ec.IsInScope(termScope));
				List<DataSyncClockWorkCourse> list5 = ClockWorkDataSyncCourses.LoadClockWorkCourses(pid, termScope);
				foreach (DataSyncExternalCourse dataSyncExternalCourse in list4)
				{
					DataSyncClockWorkCourse dataSyncClockWorkCourse = ClockWorkDataSyncCourses.FindClockWorkCourseThatMatches(list5, dataSyncExternalCourse);
					if (dataSyncClockWorkCourse != null)
					{
						dataSyncExternalCourse.MatchingClockWorkCourse = dataSyncClockWorkCourse;
						dataSyncClockWorkCourse.MatchingExternalCourse = dataSyncExternalCourse;
					}
				}
				List<DataSyncExternalCourse> list6 = list4.FindAll((DataSyncExternalCourse ecc) => ecc.MatchingClockWorkCourse == null);
				foreach (DataSyncExternalCourse dataSyncExternalCourse2 in list6)
				{
					DataSyncClockWorkCourse dataSyncClockWorkCourse2 = ClockWorkDataSyncCourses.LoadClockWorkCourse_CreateIfMissing(list, dataSyncExternalCourse2);
					if (dataSyncClockWorkCourse2 != null && dataSyncClockWorkCourse2.LuCourseId > 0)
					{
						dataSyncExternalCourse2.MatchingClockWorkCourse = dataSyncClockWorkCourse2;
						list.Add(new DataSyncCourseAction
						{
							ActionType = DataSyncActionType.Course_RegisterWithStudent,
							ClockWorkCourse = dataSyncClockWorkCourse2,
							Pid = pid
						});
					}
				}
				List<DataSyncClockWorkCourse> list7 = list5.FindAll((DataSyncClockWorkCourse cc) => cc.MatchingExternalCourse == null && !cc.IsDropped);
				foreach (DataSyncClockWorkCourse clockWorkCourse in list7)
				{
					list.Add(new DataSyncCourseAction
					{
						ActionType = DataSyncActionType.Course_DropWithStudent,
						Pid = pid,
						ClockWorkCourse = clockWorkCourse
					});
				}
				List<DataSyncClockWorkCourse> list8 = list5.FindAll((DataSyncClockWorkCourse cc) => cc.MatchingExternalCourse != null && cc.IsDropped);
				foreach (DataSyncClockWorkCourse clockWorkCourse2 in list8)
				{
					list.Add(new DataSyncCourseAction
					{
						ActionType = DataSyncActionType.Course_UnDropWithStudent,
						Pid = pid,
						ClockWorkCourse = clockWorkCourse2
					});
				}
				foreach (DataSyncExternalCourse dataSyncExternalCourse in list4)
				{
					if (dataSyncExternalCourse.MatchingClockWorkCourse != null)
					{
						DataSyncClockWorkCourse matchingClockWorkCourse = dataSyncExternalCourse.MatchingClockWorkCourse;
						ClockWorkDataSyncCourses.SyncCourseDetails(dataSyncExternalCourse, matchingClockWorkCourse, list);
						ClockWorkDataSyncCourses.SyncCourseInstructors(dataSyncExternalCourse, matchingClockWorkCourse, list);
						ClockWorkDataSyncCourses.SyncTimetableItems(dataSyncExternalCourse, matchingClockWorkCourse, list);
					}
				}
			}
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			foreach (DataSyncCourseAction dataSyncCourseAction in list)
			{
				if (dataSyncCourseAction.ActionResult == DataSyncActionResult.Unknown)
				{
					DataSyncActionType actionType = dataSyncCourseAction.ActionType;
					switch (actionType)
					{
					case DataSyncActionType.Course_RegisterWithStudent:
					{
						string query = "INSERT INTO courses (personid,lucourseid,whoadded,registrationstatus,dateadded) VALUES (@pid,@lucid,-555,1,getdate())";
						instance.ExecuteNonQuery(query, new DbParameter[]
						{
							instance.GetParameter("@pid", DbType.Int32, dataSyncCourseAction.Pid),
							instance.GetParameter("@lucid", DbType.Int32, dataSyncCourseAction.ClockWorkCourse.LuCourseId)
						});
						dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
						break;
					}
					case DataSyncActionType.Course_DropWithStudent:
					{
						string query = "UPDATE courses SET registrationstatus=2 WHERE personid=@pid AND lucourseid=@lucid AND NOT registrationstatus=2";
						instance.ExecuteNonQuery(query, new DbParameter[]
						{
							instance.GetParameter("@pid", DbType.Int32, dataSyncCourseAction.Pid),
							instance.GetParameter("@lucid", DbType.Int32, dataSyncCourseAction.ClockWorkCourse.LuCourseId)
						});
						dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
						break;
					}
					default:
						switch (actionType)
						{
						case DataSyncActionType.Course_UnDropWithStudent:
						{
							string query = "UPDATE courses SET registrationstatus=1 WHERE personid=@pid AND lucourseid=@lucid AND registrationstatus=2";
							instance.ExecuteNonQuery(query, new DbParameter[]
							{
								instance.GetParameter("@pid", DbType.Int32, dataSyncCourseAction.Pid),
								instance.GetParameter("@lucid", DbType.Int32, dataSyncCourseAction.ClockWorkCourse.LuCourseId)
							});
							dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
							break;
						}
						case DataSyncActionType.Course_UpdateCourse:
						{
							string query = "UPDATE lucourses SET startdate=@startdate,enddate=@enddate,campus=@campus,department=@department,location=@location WHERE lucourseid=@lucid";
							instance.ExecuteNonQuery(query, new DbParameter[]
							{
								instance.GetParameter("@campus", DbType.String, dataSyncCourseAction.ExternalCourse.Campus),
								instance.GetParameter("@location", DbType.String, dataSyncCourseAction.ExternalCourse.Location),
								instance.GetParameter("@department", DbType.String, dataSyncCourseAction.ExternalCourse.Department),
								instance.GetParameter("@lucid", DbType.Int32, dataSyncCourseAction.ClockWorkCourse.LuCourseId),
								instance.GetParameter("@startdate", DbType.DateTime, dataSyncCourseAction.ExternalCourse.StartDate),
								instance.GetParameter("@enddate", DbType.DateTime, dataSyncCourseAction.ExternalCourse.EndDate)
							});
							dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
							break;
						}
						}
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0004CDF8 File Offset: 0x0004BDF8
		public static bool SyncCourseDetails(DataSyncExternalCourse extCourse, DataSyncClockWorkCourse cwCourse, List<DataSyncCourseAction> actions)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(extCourse.Campus) && extCourse.Campus.Trim().Length > 0 && !extCourse.Campus.Equals(cwCourse.Campus, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			if (!string.IsNullOrEmpty(extCourse.Department) && extCourse.Department.Trim().Length > 0 && !extCourse.Department.Equals(cwCourse.Department, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			if (!string.IsNullOrEmpty(extCourse.Location) && extCourse.Location.Trim().Length > 0 && !extCourse.Location.Equals(cwCourse.Location, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			if (extCourse.StartDate.Date != cwCourse.StartDate.Date || extCourse.EndDate.Date != cwCourse.EndDate.Date)
			{
				flag = true;
			}
			if (flag)
			{
				actions.Add(new DataSyncCourseAction
				{
					ActionType = DataSyncActionType.Course_UpdateCourse,
					ClockWorkCourse = cwCourse,
					ExternalCourse = extCourse
				});
			}
			return false;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0004D054 File Offset: 0x0004C054
		public static bool SyncCourseInstructors(DataSyncExternalCourse extCourse, DataSyncClockWorkCourse cwCourse, List<DataSyncCourseAction> actions)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			bool result;
			if (extCourse.Instructors.Count < 1)
			{
				result = false;
			}
			else
			{
				List<DataSyncInstructor> list = extCourse.Instructors.FindAll((DataSyncInstructor pr) => cwCourse.Instructors.Find((DataSyncInstructor pr2) => pr2.Equals(pr)) == null);
				List<DataSyncInstructor> list2 = cwCourse.Instructors.FindAll((DataSyncInstructor pr) => extCourse.Instructors.Find((DataSyncInstructor pr2) => pr2.Equals(pr)) == null);
				List<DataSyncInstructor> list3 = new List<DataSyncInstructor>();
				foreach (DataSyncInstructor dataSyncInstructor in cwCourse.Instructors)
				{
					if (!list2.Contains(dataSyncInstructor))
					{
						list3.Add(new DataSyncInstructor
						{
							Email = dataSyncInstructor.Email,
							FirstName = dataSyncInstructor.FirstName,
							Id = dataSyncInstructor.Id,
							IsPrimary = dataSyncInstructor.IsPrimary,
							LastName = dataSyncInstructor.LastName,
							Name = dataSyncInstructor.Name,
							Phone = dataSyncInstructor.Phone,
							Username = dataSyncInstructor.Username
						});
					}
				}
				foreach (DataSyncInstructor dataSyncInstructor in list)
				{
					int num = ClockWorkDataSyncCourses.LoadInstructor(dataSyncInstructor, actions);
					if (num > 0)
					{
						dataSyncInstructor.Id = num;
						list3.Add(dataSyncInstructor);
						DataSyncCourseAction dataSyncCourseAction = new DataSyncCourseAction();
						dataSyncCourseAction.ActionType = DataSyncActionType.Course_AddInstructor;
						dataSyncCourseAction.ClockWorkInstructor = dataSyncInstructor;
						actions.Add(dataSyncCourseAction);
						string query = "INSERT INTO lucourseinstructor (lucourseid,instructorid) SELECT @lucid,@iid WHERE NOT EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@iid)";
						instance.ExecuteQuery(query, new DbParameter[]
						{
							instance.GetParameter("@lucid", DbType.Int32, cwCourse.LuCourseId),
							instance.GetParameter("@iid", DbType.Int32, dataSyncInstructor.Id)
						});
						dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
					}
				}
				foreach (DataSyncInstructor dataSyncInstructor in list2)
				{
					DataSyncCourseAction dataSyncCourseAction = new DataSyncCourseAction();
					dataSyncCourseAction.ActionType = DataSyncActionType.Course_RemoveInstructor;
					dataSyncCourseAction.ClockWorkInstructor = dataSyncInstructor;
					actions.Add(dataSyncCourseAction);
					string query = "DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@iid; UPDATE lucourses SET instructorid=-1 WHERE lucourseid=@lucid AND instructorid=@iid";
					instance.ExecuteNonQuery(query, new DbParameter[]
					{
						instance.GetParameter("@lucid", DbType.Int32, cwCourse.LuCourseId),
						instance.GetParameter("@iid", DbType.Int32, dataSyncInstructor.Id)
					});
					dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
				}
				if (list3.Count > 0)
				{
					string query = "UPDATE lucourses SET instructorid=@iid WHERE lucourseid=@lucid AND instructorid<1";
					instance.ExecuteNonQuery(query, new DbParameter[]
					{
						instance.GetParameter("@lucid", DbType.Int32, cwCourse.LuCourseId),
						instance.GetParameter("@iid", DbType.Int32, list3[0].Id)
					});
					list3[0].IsPrimary = true;
				}
				DataSyncInstructor prof;
				foreach (DataSyncInstructor prof2 in list3)
				{
					prof = prof2;
					DataSyncInstructor dataSyncInstructor2 = extCourse.Instructors.Find((DataSyncInstructor pr) => pr.Equals(prof));
					if (dataSyncInstructor2 != null)
					{
						bool flag = false;
						if (!string.IsNullOrEmpty(dataSyncInstructor2.Email) && !dataSyncInstructor2.Email.Equals(prof.Email, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
						else if (!string.IsNullOrEmpty(dataSyncInstructor2.Phone) && !dataSyncInstructor2.Phone.Equals(prof.Phone, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
						if (flag)
						{
							string query = "UPDATE lucoursedata SET email=@email,phone=@phone WHERE lucoursedataid=@iid";
							instance.ExecuteNonQuery(query, new DbParameter[]
							{
								instance.GetParameter("@email", DbType.String, dataSyncInstructor2.Email),
								instance.GetParameter("@phone", DbType.String, dataSyncInstructor2.Phone),
								instance.GetParameter("@iid", DbType.Int32, prof.Id)
							});
							actions.Add(new DataSyncCourseAction
							{
								ActionType = DataSyncActionType.Course_UpdateInstructor,
								ClockWorkInstructor = prof,
								ActionResult = DataSyncActionResult.Success
							});
							prof.Email = dataSyncInstructor2.Email;
							prof.Phone = dataSyncInstructor2.Phone;
						}
						if (string.IsNullOrEmpty(prof.Username) && !string.IsNullOrEmpty(dataSyncInstructor2.Username))
						{
							string query = "UDPATE lucoursedata SET username=@username WHERE lucoursedataid=@iid";
							instance.ExecuteNonQuery(query, new DbParameter[]
							{
								instance.GetParameter("@username", DbType.String, dataSyncInstructor2.Username),
								instance.GetParameter("@iid", DbType.Int32, cwCourse.LuCourseId)
							});
							actions.Add(new DataSyncCourseAction
							{
								ActionType = DataSyncActionType.Course_UpdateInstructorUsername,
								ClockWorkInstructor = prof,
								ActionResult = DataSyncActionResult.Success
							});
							prof.Username = dataSyncInstructor2.Username;
						}
					}
				}
				cwCourse.Instructors = list3;
				result = true;
			}
			return result;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0004D860 File Offset: 0x0004C860
		public static bool SyncTimetableItems(DataSyncExternalCourse extCourse, DataSyncClockWorkCourse cwCourse, List<DataSyncCourseAction> actions)
		{
			List<DataSyncTimetableItem> list = extCourse.TimeTableItems.FindAll((DataSyncTimetableItem tt) => cwCourse.TimeTableItems.Find((DataSyncTimetableItem tt2) => tt2.DayOfWeek == tt.DayOfWeek && tt2.StartMinutes == tt.StartMinutes && tt2.EndMinutes == tt.EndMinutes && tt2.Room.Equals(tt.Room, StringComparison.OrdinalIgnoreCase)) == null);
			List<DataSyncTimetableItem> list2 = cwCourse.TimeTableItems.FindAll((DataSyncTimetableItem tt) => extCourse.TimeTableItems.Find((DataSyncTimetableItem tt2) => tt2.DayOfWeek == tt.DayOfWeek && tt2.StartMinutes == tt.StartMinutes && tt2.EndMinutes == tt.EndMinutes && tt2.Room.Equals(tt.Room, StringComparison.OrdinalIgnoreCase)) == null);
			if (list.Count > 0 || list2.Count > 0)
			{
				DatabaseLayer instance = DatabaseLayer.GetInstance();
				string query = "DELETE FROM timetable WHERE lucourseid=@lucid";
				instance.ExecuteNonQuery(query, new DbParameter[]
				{
					instance.GetParameter("@lucid", DbType.Int32, cwCourse.LuCourseId)
				});
				List<List<DataSyncTimetableItem>> rows = ClockWorkDataSyncCourses.FigureOutTimetableRows(extCourse.TimeTableItems);
				ClockWorkDataSyncCourses.InsertTimetableToDatabase(cwCourse.LuCourseId, rows, actions);
			}
			return true;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0004D954 File Offset: 0x0004C954
		public static DataSyncClockWorkCourse LoadClockWorkCourse_CreateIfMissing(List<DataSyncCourseAction> actions, DataSyncExternalCourse externalCourse)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			string query = "SELECT luc.lucourseid\r\n        ,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom\r\nFROM    lucourses luc \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\nWHERE   NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate )\r\n        AND luc.term=@term AND luc.duration=@duration AND luc.course=@course \r\n        AND luc.section=@section AND luc.timeofday=@timeofday\r\n        AND luc.subjectid IN (SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND ((altlookupstring=@subject OR lookupstring=@subject) OR (NOT @subjectcode='' AND lookupstring=@subjectcode)))";
			DataTable t = instance.ExecuteQuery(query, new DbParameter[]
			{
				instance.GetParameter("@startdate", DbType.DateTime, externalCourse.StartDate),
				instance.GetParameter("@enddate", DbType.DateTime, externalCourse.EndDate),
				instance.GetParameter("@term", DbType.String, externalCourse.Term),
				instance.GetParameter("@duration", DbType.String, externalCourse.Duration),
				instance.GetParameter("@subject", DbType.String, externalCourse.Subject),
				instance.GetParameter("@course", DbType.String, externalCourse.Course),
				instance.GetParameter("@section", DbType.String, externalCourse.Section),
				instance.GetParameter("@timeofday", DbType.String, externalCourse.TimeOfDay),
				instance.GetParameter("@subjectcode", DbType.String, (externalCourse.SubjectCode == null) ? "" : externalCourse.SubjectCode.Trim())
			});
			List<DataSyncClockWorkCourse> list = DataSyncClockWorkCourse.ParseClockWorkCourses(t);
			DataSyncClockWorkCourse dataSyncClockWorkCourse;
			if (list.Count > 0)
			{
				dataSyncClockWorkCourse = list[0];
			}
			else
			{
				dataSyncClockWorkCourse = null;
			}
			if (dataSyncClockWorkCourse == null)
			{
				dataSyncClockWorkCourse = ClockWorkDataSyncCourses.CreateClockWorkCourse(externalCourse, actions);
			}
			return dataSyncClockWorkCourse;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0004DAAC File Offset: 0x0004CAAC
		private static DataSyncClockWorkCourse CreateClockWorkCourse(DataSyncExternalCourse externalCourse, List<DataSyncCourseAction> actions)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			int num = ClockWorkDataSyncCourses.LoadSubjectId(externalCourse.Subject, externalCourse.SubjectCode);
			DataSyncClockWorkCourse result;
			if (num > 0)
			{
				int num2 = -1;
				foreach (DataSyncInstructor dataSyncInstructor in externalCourse.Instructors)
				{
					int num3 = ClockWorkDataSyncCourses.LoadInstructor(dataSyncInstructor, actions);
					dataSyncInstructor.Id = num3;
					if (num3 > 0)
					{
						num2 = num3;
					}
				}
				string query = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,section,timeofday,instructorid,campus,location,department) VALUES (@startdate,@enddate,@term,@duration,@subjectid,@course,@section,@timeofday,@instructorid,@campus,@location,@department);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)";
				DataTable dataTable = instance.ExecuteQuery(query, new DbParameter[]
				{
					instance.GetParameter("@startdate", DbType.DateTime, externalCourse.StartDate),
					instance.GetParameter("@enddate", DbType.DateTime, externalCourse.EndDate),
					instance.GetParameter("@term", DbType.String, externalCourse.Term),
					instance.GetParameter("@duration", DbType.String, externalCourse.Duration),
					instance.GetParameter("@subjectid", DbType.Int32, num),
					instance.GetParameter("@course", DbType.String, externalCourse.Course),
					instance.GetParameter("@section", DbType.String, externalCourse.Section),
					instance.GetParameter("@timeofday", DbType.String, externalCourse.TimeOfDay),
					instance.GetParameter("@instructorid", DbType.Int32, num2),
					instance.GetParameter("@campus", DbType.String, externalCourse.Campus),
					instance.GetParameter("@location", DbType.String, externalCourse.Location),
					instance.GetParameter("@department", DbType.String, externalCourse.Department)
				});
				int num4 = (int)dataTable.Rows[0][0];
				foreach (DataSyncInstructor dataSyncInstructor in externalCourse.Instructors)
				{
					if (dataSyncInstructor.Id > 0)
					{
						query = "INSERT INTO lucourseinstructor (lucourseid,instructorid) VALUES (@lucid,@id)";
						instance.ExecuteQuery(query, new DbParameter[]
						{
							instance.GetParameter("@lucid", DbType.Int32, num4),
							instance.GetParameter("@id", DbType.Int32, dataSyncInstructor.Id)
						});
					}
				}
				List<List<DataSyncTimetableItem>> rows = ClockWorkDataSyncCourses.FigureOutTimetableRows(externalCourse.TimeTableItems);
				ClockWorkDataSyncCourses.InsertTimetableToDatabase(num4, rows, actions);
				result = new DataSyncClockWorkCourse
				{
					LuCourseId = num4,
					StartDate = externalCourse.StartDate,
					EndDate = externalCourse.EndDate,
					Duration = externalCourse.Duration,
					Term = externalCourse.Term,
					Subject = externalCourse.Subject,
					SubjectId = num,
					Section = externalCourse.Section,
					TimeOfDay = externalCourse.TimeOfDay,
					Instructors = externalCourse.Instructors,
					TimeTableItems = externalCourse.TimeTableItems
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0004DE00 File Offset: 0x0004CE00
		private static void InsertTimetableToDatabase(int lucid, List<List<DataSyncTimetableItem>> rows, List<DataSyncCourseAction> actions)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			foreach (List<DataSyncTimetableItem> list in rows)
			{
				if (list.Count > 0)
				{
					string query = "INSERT INTO timetable (lucourseid,timetabletype,sunstartminutes,sunendminutes,monstartminutes,monendminutes\r\n,tuestartminutes,tueendminutes,wedstartminutes,wedendminutes,thustartminutes,thuendminutes,fristartminutes\r\n,friendminutes,satstartminutes,satendminutes,sunroom,monroom,tueroom,wedroom,thuroom,friroom,satroom)\r\nVALUES (@lucid,'C',@n0,@n1,@n2,@n3,@n4,@n5,@n6,@n7,@n8,@n9,@n10,@n11,@n12,@n13,@n14,@n15,@n16,@n17,@n18,@n19,@n20)";
					DbParameter[] array = new DbParameter[21];
					array[0] = instance.GetParameter("@lucid", DbType.Int32, lucid);
					for (int i = 0; i < 21; i++)
					{
						if (i > 13)
						{
							array[i] = instance.GetParameter("@n" + i.ToString(), DbType.String, "");
						}
						else
						{
							array[i] = instance.GetParameter("@n" + i.ToString(), DbType.Int32, 0);
						}
					}
					foreach (DataSyncTimetableItem dataSyncTimetableItem in list)
					{
						int dayOfWeek = (int)dataSyncTimetableItem.DayOfWeek;
						int num = dayOfWeek + 1;
						array[num] = instance.GetParameter("@n" + num.ToString(), DbType.Int32, dataSyncTimetableItem.StartMinutes);
						array[num + 7] = instance.GetParameter("@n" + num.ToString(), DbType.Int32, dataSyncTimetableItem.EndMinutes);
						array[num + 14] = instance.GetParameter("@n" + num.ToString(), DbType.String, dataSyncTimetableItem.Room);
					}
					instance.ExecuteNonQuery(query, array);
					actions.Add(new DataSyncCourseAction
					{
						ActionType = DataSyncActionType.Course_AddTimeTableItem,
						ClockWorkCourse = new DataSyncClockWorkCourse
						{
							LuCourseId = lucid
						},
						ActionResult = DataSyncActionResult.Success
					});
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0004E064 File Offset: 0x0004D064
		private static List<List<DataSyncTimetableItem>> FigureOutTimetableRows(List<DataSyncTimetableItem> timeTableItems)
		{
			List<List<DataSyncTimetableItem>> list = new List<List<DataSyncTimetableItem>>();
			List<DataSyncTimetableItem> item = new List<DataSyncTimetableItem>();
			list.Add(item);
			DataSyncTimetableItem tti;
			foreach (DataSyncTimetableItem tti2 in timeTableItems)
			{
				tti = tti2;
				List<DataSyncTimetableItem> list2 = null;
				foreach (List<DataSyncTimetableItem> list3 in list)
				{
					DataSyncTimetableItem dataSyncTimetableItem = list3.Find((DataSyncTimetableItem r) => r.DayOfWeek == tti.DayOfWeek);
					if (dataSyncTimetableItem == null)
					{
						list2 = list3;
						break;
					}
				}
				if (list2 == null)
				{
					list2 = new List<DataSyncTimetableItem>();
					list.Add(list2);
				}
				list2.Add(new DataSyncTimetableItem
				{
					DayOfWeek = tti.DayOfWeek,
					StartMinutes = tti.StartMinutes,
					EndMinutes = tti.EndMinutes,
					Room = tti.Room
				});
			}
			return list;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0004E1F0 File Offset: 0x0004D1F0
		private static int LoadInstructor(DataSyncInstructor instructor, List<DataSyncCourseAction> actions)
		{
			int result;
			if (string.IsNullOrEmpty(instructor.Name))
			{
				result = 0;
			}
			else
			{
				DatabaseLayer instance = DatabaseLayer.GetInstance();
				DbParameter[] array = new DbParameter[1];
				string query;
				DataTable dataTable;
				if (!string.IsNullOrEmpty(instructor.Username))
				{
					query = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND username=@username";
					array[0] = instance.GetParameter("@username", DbType.String, instructor.Username);
					dataTable = instance.ExecuteQuery(query, array);
					if (dataTable.Rows.Count > 0)
					{
						return (int)dataTable.Rows[0][0];
					}
				}
				else if (!string.IsNullOrEmpty(instructor.Email))
				{
					query = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email";
					array[0] = instance.GetParameter("@email", DbType.String, instructor.Email);
					dataTable = instance.ExecuteQuery(query, array);
					if (dataTable.Rows.Count > 0)
					{
						return (int)dataTable.Rows[0][0];
					}
				}
				else
				{
					if (string.IsNullOrEmpty(instructor.Name))
					{
						return 0;
					}
					query = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND (lookupstring=@name OR altlookupstring=@name)";
					array[0] = instance.GetParameter("@name", DbType.String, instructor.Name);
					dataTable = instance.ExecuteQuery(query, array);
					if (dataTable.Rows.Count > 0)
					{
						return (int)dataTable.Rows[0][0];
					}
				}
				query = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,username,phone) VALUES (1,@name,@name,@email,@username,@phone);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)";
				dataTable = instance.ExecuteQuery(query, new DbParameter[]
				{
					instance.GetParameter("@name", DbType.String, instructor.Name),
					instance.GetParameter("@email", DbType.String, instructor.Email),
					instance.GetParameter("@phone", DbType.String, instructor.Phone),
					instance.GetParameter("@username", DbType.String, instructor.Username)
				});
				int num = (int)dataTable.Rows[0][0];
				DataSyncCourseAction dataSyncCourseAction = new DataSyncCourseAction();
				dataSyncCourseAction.ActionType = DataSyncActionType.Course_CreateInstructor;
				dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
				instructor.Id = num;
				dataSyncCourseAction.ClockWorkInstructor = instructor;
				actions.Add(dataSyncCourseAction);
				result = num;
			}
			return result;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0004E444 File Offset: 0x0004D444
		private static int LoadSubjectId(string subject)
		{
			return ClockWorkDataSyncCourses.LoadSubjectId(subject, "");
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0004E464 File Offset: 0x0004D464
		private static int LoadSubjectId(string subject, string optionalSubjectCode)
		{
			int result;
			if (string.IsNullOrEmpty(subject))
			{
				result = 0;
			}
			else
			{
				DatabaseLayer instance = DatabaseLayer.GetInstance();
				string query = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=0 AND ((lookupstring=@subject OR altlookupstring=@subject) OR (NOT @subjectcode='' AND lookupstring=@subjectcode))";
				DataTable dataTable = instance.ExecuteQuery(query, new DbParameter[]
				{
					instance.GetParameter("@subject", DbType.String, subject),
					instance.GetParameter("@subjectcode", DbType.String, (optionalSubjectCode == null) ? "" : optionalSubjectCode.Trim())
				});
				if (dataTable.Rows.Count > 0)
				{
					result = (int)dataTable.Rows[0][0];
				}
				else if (optionalSubjectCode != null && optionalSubjectCode.Trim().Length > 0)
				{
					query = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring) VALUES (0,@subjectcode,@subject);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)";
					dataTable = instance.ExecuteQuery(query, new DbParameter[]
					{
						instance.GetParameter("@subject", DbType.String, subject),
						instance.GetParameter("@subjectcode", DbType.String, optionalSubjectCode)
					});
					result = (int)dataTable.Rows[0][0];
				}
				else
				{
					query = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring) VALUES (0,@subject,@subject);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)";
					dataTable = instance.ExecuteQuery(query, new DbParameter[]
					{
						instance.GetParameter("@subject", DbType.String, subject)
					});
					result = (int)dataTable.Rows[0][0];
				}
			}
			return result;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0004E5C4 File Offset: 0x0004D5C4
		public static List<DataSyncClockWorkCourse> LoadClockWorkCourses(int pid, DataSyncTermScope scope)
		{
			return ClockWorkDataSyncCourses.LoadClockWorkCourses(pid, scope, false);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0004E5E0 File Offset: 0x0004D5E0
		public static List<DataSyncClockWorkCourse> LoadClockWorkCourses(int pid, DataSyncTermScope scope, bool allCourses)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			string query = "SELECT c.personid,c.coursesid,c.lucourseid,c.registrationstatus,c.exemptfromdatasync\r\n        ,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=c.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=c.lucourseid\r\nWHERE   c.personid=@pid\r\n        AND (@allcourses=1 OR NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate))";
			DataTable t = instance.ExecuteQuery(query, new DbParameter[]
			{
				instance.GetParameter("@pid", DbType.Int32, pid),
				instance.GetParameter("@startdate", DbType.DateTime, scope.StartDate),
				instance.GetParameter("@enddate", DbType.DateTime, scope.EndDate),
				instance.GetParameter("@allcourses", DbType.Boolean, allCourses)
			});
			return DataSyncClockWorkCourse.ParseClockWorkCourses(t);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0004E728 File Offset: 0x0004D728
		public static DataSyncClockWorkCourse FindClockWorkCourseThatMatches(List<DataSyncClockWorkCourse> clockWorkCourses, DataSyncExternalCourse externalCourse)
		{
			return clockWorkCourses.Find((DataSyncClockWorkCourse c) => c.Term.Equals(externalCourse.Term, StringComparison.OrdinalIgnoreCase) && c.Duration.Equals(externalCourse.Duration, StringComparison.OrdinalIgnoreCase) && c.Subject.Equals(externalCourse.Subject, StringComparison.OrdinalIgnoreCase) && c.Course.Equals(externalCourse.Course, StringComparison.OrdinalIgnoreCase) && c.TimeOfDay.Equals(externalCourse.TimeOfDay, StringComparison.OrdinalIgnoreCase) && c.Section.Equals(externalCourse.Section, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0004E75C File Offset: 0x0004D75C
		public static List<DataSyncTermScope> ExtractTermScopes(List<DataSyncExternalCourse> externalCourses)
		{
			List<DataSyncTermScope> list = new List<DataSyncTermScope>();
			DataSyncTermScope item = new DataSyncTermScope();
			list.Add(item);
			return list;
		}
	}
}
