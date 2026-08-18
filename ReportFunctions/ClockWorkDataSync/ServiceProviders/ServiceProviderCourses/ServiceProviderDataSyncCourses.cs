using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkAPI;
using ClockWorkLogger;
using EncryptionClassLibrary;
using ReportFunctions.ClockWorkDataSync.Courses;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderCourses
{
	// Token: 0x02000027 RID: 39
	public class ServiceProviderDataSyncCourses
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00039F60 File Offset: 0x00038F60
		public List<DataSyncCourseAction> DataSyncCoursesServiceProvider(DataTable t)
		{
			if (t.Rows.Count > 0)
			{
				string text = t.Rows[0]["student_no"].ToString().Trim().ToUpper();
				if (text.Length > 0)
				{
					UnivDataAdapter da = ClientCache.CurrentInstance.da;
					TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
					string commandText = "SELECT serviceproviderid FROM serviceproviders WHERE student_no=@sne";
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(text));
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						int spid = (int)dataTable.Rows[0][0];
						return this.DataSyncCoursesServiceProvider(spid, t);
					}
				}
			}
			return new List<DataSyncCourseAction>();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0003A10C File Offset: 0x0003910C
		public List<DataSyncCourseAction> DataSyncCoursesServiceProvider(int spid, DataTable t)
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
				List<DataSyncClockWorkCourse> list5 = ServiceProviderDataSyncCourses.LoadClockWorkServiceProviderCourses(spid, termScope);
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
							Pid = spid
						});
					}
				}
				List<DataSyncClockWorkCourse> list7 = list5.FindAll((DataSyncClockWorkCourse cc) => cc.MatchingExternalCourse == null && !cc.IsDropped);
				foreach (DataSyncClockWorkCourse clockWorkCourse in list7)
				{
					list.Add(new DataSyncCourseAction
					{
						ActionType = DataSyncActionType.Course_DropWithStudent,
						Pid = spid,
						ClockWorkCourse = clockWorkCourse
					});
				}
				List<DataSyncClockWorkCourse> list8 = list5.FindAll((DataSyncClockWorkCourse cc) => cc.MatchingExternalCourse != null && cc.IsDropped);
				foreach (DataSyncClockWorkCourse clockWorkCourse2 in list8)
				{
					list.Add(new DataSyncCourseAction
					{
						ActionType = DataSyncActionType.Course_UnDropWithStudent,
						Pid = spid,
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
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			foreach (DataSyncCourseAction dataSyncCourseAction in list)
			{
				if (dataSyncCourseAction.ActionResult == DataSyncActionResult.Unknown)
				{
					try
					{
						DataSyncActionType actionType = dataSyncCourseAction.ActionType;
						switch (actionType)
						{
						case DataSyncActionType.Course_RegisterWithStudent:
						{
							int num3 = ServiceProviderDataSyncCourses.CreateServiceProviderApplication(spid, 128, dataSyncCourseAction.TermScope.StartDate, dataSyncCourseAction.TermScope.EndDate);
							if (num3 > 0)
							{
								string commandText = "INSERT INTO serviceproviderapplicationcourses (serviceproviderapplicationid,serviceprovidertype,lucourseid,datecancelled,note,registrationstatus)\r\n    SELECT @spaid,128,@lucid,NULL,NULL,NULL WHERE NOT EXISTS(SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spaid AND serviceprovidertype=128 AND lucourseid=@lucid)";
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@spaid", num3);
								da.SelectCommand.Parameters.Add("@lucid", dataSyncCourseAction.ClockWorkCourse.LuCourseId);
								da.Fill(new DataTable());
								dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
							}
							else
							{
								dataSyncCourseAction.ActionResult = DataSyncActionResult.Fail;
							}
							break;
						}
						case DataSyncActionType.Course_DropWithStudent:
						{
							string commandText = "UPDATE serviceproviderapplicationcourses SET registrationstatus=2 WHERE serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid) AND lucourseid=@lucid AND NOT registrationstatus=2";
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@spid", dataSyncCourseAction.Pid);
							da.SelectCommand.Parameters.Add("@lucid", dataSyncCourseAction.ClockWorkCourse.LuCourseId);
							da.Fill(new DataTable());
							dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
							break;
						}
						default:
							switch (actionType)
							{
							case DataSyncActionType.Course_UnDropWithStudent:
							{
								string commandText = "UPDATE serviceproviderapplicationcourses SET registrationstatus=1 WHERE serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid) AND lucourseid=@lucid AND registrationstatus=2";
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@spid", dataSyncCourseAction.Pid);
								da.SelectCommand.Parameters.Add("@lucid", dataSyncCourseAction.ClockWorkCourse.LuCourseId);
								da.Fill(new DataTable());
								dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
								break;
							}
							case DataSyncActionType.Course_UpdateCourse:
							{
								string commandText = "UPDATE lucourses SET startdate=@startdate,enddate=@enddate,campus=@campus,department=@department,location=@location WHERE lucourseid=@lucid";
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@campus", dataSyncCourseAction.ExternalCourse.Campus);
								da.SelectCommand.Parameters.Add("@location", dataSyncCourseAction.ExternalCourse.Location);
								da.SelectCommand.Parameters.Add("@department", dataSyncCourseAction.ExternalCourse.Department);
								da.SelectCommand.Parameters.Add("@lucid", dataSyncCourseAction.ClockWorkCourse.LuCourseId);
								da.SelectCommand.Parameters.Add("@startdate", dataSyncCourseAction.ExternalCourse.StartDate);
								da.SelectCommand.Parameters.Add("@enddate", dataSyncCourseAction.ExternalCourse.EndDate);
								da.Fill(new DataTable());
								dataSyncCourseAction.ActionResult = DataSyncActionResult.Success;
								break;
							}
							}
							break;
						}
					}
					catch (Exception argument)
					{
						dataSyncCourseAction.ActionResult = DataSyncActionResult.Fail;
						CWLogger.Logger.Error("DataSyncCoursesServiceProvider:ExecuteActions", argument);
					}
				}
			}
			return list;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0003AE8C File Offset: 0x00039E8C
		public static int CreateServiceProviderApplication(int serviceProviderId, int serviceProviderType, DateTime termStartDate, DateTime termEndDate)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = QueryStorage.QS_INSERT_NewServiceProviderApplicationInTerm;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@spid", serviceProviderId);
			da.SelectCommand.Parameters.Add("@sptype", serviceProviderType);
			da.SelectCommand.Parameters.Add("@termstartdate", termStartDate);
			da.SelectCommand.Parameters.Add("@termenddate", termEndDate);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			int result;
			if (dataTable.Rows.Count > 0)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0003AF7C File Offset: 0x00039F7C
		public static List<DataSyncClockWorkCourse> LoadClockWorkServiceProviderCourses(int spid, DataSyncTermScope scope)
		{
			return ServiceProviderDataSyncCourses.LoadClockWorkServiceProviderCourses(spid, scope, false);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0003AF98 File Offset: 0x00039F98
		public static List<DataSyncClockWorkCourse> LoadClockWorkServiceProviderCourses(int spid, DataSyncTermScope scope, bool allCourses)
		{
			string commandText = "SELECT sp.serviceproviderid,spac.serviceproviderapplicationcourseid,spac.registrationstatus,CAST(0 AS bit) AS exemptfromdatasync\r\n        ,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom\r\nFROM    serviceproviders sp LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderid=sp.serviceproviderid\r\n        LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=c.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=c.lucourseid\r\nWHERE   sp.serviceproviderid=@spid\r\n        AND (@allcourses=1 OR NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate))\r\n        AND NOT luc.lucourseid IS NULL";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@spid", spid);
			da.SelectCommand.Parameters.Add("@startdate", scope.StartDate);
			da.SelectCommand.Parameters.Add("@enddate", scope.EndDate);
			da.SelectCommand.Parameters.Add("@allcourses", allCourses);
			DataTable t = new DataTable();
			da.Fill(t);
			return DataSyncClockWorkCourse.ParseClockWorkCourses(t);
		}
	}
}
