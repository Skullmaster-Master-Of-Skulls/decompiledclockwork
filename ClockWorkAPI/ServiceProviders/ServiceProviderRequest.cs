using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000054 RID: 84
	public class ServiceProviderRequest
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00015F58 File Offset: 0x00014F58
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00015F6F File Offset: 0x00014F6F
		public int ServiceProviderRequestId { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00015F78 File Offset: 0x00014F78
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00015F8F File Offset: 0x00014F8F
		public PersonBaseDTO StudentWhoRequested { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00015F98 File Offset: 0x00014F98
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00015FAF File Offset: 0x00014FAF
		public ServiceProvider ProviderAssigned { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00015FB8 File Offset: 0x00014FB8
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00015FCF File Offset: 0x00014FCF
		public Course CourseRequested { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00015FD8 File Offset: 0x00014FD8
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00015FEF File Offset: 0x00014FEF
		public Course CourseRequestedProvider { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00015FF8 File Offset: 0x00014FF8
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001600F File Offset: 0x0001500F
		public eServiceProviderType RequestType { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00016018 File Offset: 0x00015018
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001602F File Offset: 0x0001502F
		public string GeneralNote { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00016038 File Offset: 0x00015038
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001604F File Offset: 0x0001504F
		public string SpecialInstructions { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00016058 File Offset: 0x00015058
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001606F File Offset: 0x0001506F
		public string Status { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00016078 File Offset: 0x00015078
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001608F File Offset: 0x0001508F
		public DataRow DataRowForReference { get; set; }

		// Token: 0x060004B0 RID: 1200 RVA: 0x00016098 File Offset: 0x00015098
		public ServiceProviderRequest()
		{
			this.RequestType = eServiceProviderType.Unknown;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000160B4 File Offset: 0x000150B4
		public string Student
		{
			get
			{
				return (this.StudentWhoRequested == null) ? "" : string.Format("{0} {1}", this.StudentWhoRequested.FirstName, this.StudentWhoRequested.LastName);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000160F8 File Offset: 0x000150F8
		public string Student_no
		{
			get
			{
				return (this.StudentWhoRequested == null) ? "" : this.StudentWhoRequested.Student_no;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00016124 File Offset: 0x00015124
		public string FirstName
		{
			get
			{
				return (this.StudentWhoRequested == null) ? "" : this.StudentWhoRequested.FirstName;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00016150 File Offset: 0x00015150
		public string MiddleName
		{
			get
			{
				return (this.StudentWhoRequested == null) ? "" : this.StudentWhoRequested.MiddleName;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001617C File Offset: 0x0001517C
		public string LastName
		{
			get
			{
				return (this.StudentWhoRequested == null) ? "" : this.StudentWhoRequested.LastName;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000161A8 File Offset: 0x000151A8
		public string Course
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.ToStringSimple();
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000161D4 File Offset: 0x000151D4
		public string Term
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.Term;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00016200 File Offset: 0x00015200
		public string Subject
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.Subject;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001622C File Offset: 0x0001522C
		public string CourseCode
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.CourseCode;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00016258 File Offset: 0x00015258
		public string Section
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.Section;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00016284 File Offset: 0x00015284
		public string Duration
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.Duration;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000162B0 File Offset: 0x000152B0
		public string TimeOfDay
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.TimeOfDay;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000162DC File Offset: 0x000152DC
		public string Instructor
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.InstructorName;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00016308 File Offset: 0x00015308
		public string InstructorEmail
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.InstructorEmail;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00016334 File Offset: 0x00015334
		public string InstructorPhone
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.InstructorPhone;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00016360 File Offset: 0x00015360
		public string InstructorUsername
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.InstructorUsername;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001638C File Offset: 0x0001538C
		public string CourseYear
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.StartDate.Year.ToString();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000163C8 File Offset: 0x000153C8
		public string Timetable
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.GetCourseTimeTable(this.CourseRequested);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000163F8 File Offset: 0x000153F8
		public string CourseStartDate
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.StartDate.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00016434 File Offset: 0x00015434
		public string CourseEndDate
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.EndDate.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016470 File Offset: 0x00015470
		public string CourseDescription
		{
			get
			{
				return (this.CourseRequested == null) ? "" : this.CourseRequested.ToStringSimple();
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001649C File Offset: 0x0001549C
		public string AssignedProvider
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : (string.IsNullOrEmpty(this.ProviderAssigned.DisplayName) ? string.Format("{0} {1}", this.ProviderAssigned.FirstName, this.ProviderAssigned.LastName) : this.ProviderAssigned.DisplayName);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000164FC File Offset: 0x000154FC
		public string Provider
		{
			get
			{
				return this.AssignedProvider;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00016514 File Offset: 0x00015514
		public string ProviderEmail
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.Email;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00016540 File Offset: 0x00015540
		public string ProviderId
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.Student_no;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001656C File Offset: 0x0001556C
		public string ProviderAlternateEmail
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.AlternateEmail;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016598 File Offset: 0x00015598
		public string ProviderPhone
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.Phone;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000165C4 File Offset: 0x000155C4
		public string ProviderCell
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.Cell;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000165F0 File Offset: 0x000155F0
		public string ProviderSpecialization
		{
			get
			{
				return (this.ProviderAssigned == null) ? "" : this.ProviderAssigned.Specialization;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001661C File Offset: 0x0001561C
		public string ProviderCourse
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.ToStringSimple();
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016648 File Offset: 0x00015648
		public string ProviderCourseDescription
		{
			get
			{
				return this.ProviderCourse;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00016660 File Offset: 0x00015660
		public string ProviderTerm
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.Term;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001668C File Offset: 0x0001568C
		public string ProviderSubject
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.Subject;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000166B8 File Offset: 0x000156B8
		public string ProviderCourseCode
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.CourseCode;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000166E4 File Offset: 0x000156E4
		public string ProviderSection
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.Section;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016710 File Offset: 0x00015710
		public string ProviderDuration
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.Duration;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001673C File Offset: 0x0001573C
		public string ProviderTimeOfDay
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.TimeOfDay;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016768 File Offset: 0x00015768
		public string ProviderInstructor
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.InstructorName;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00016794 File Offset: 0x00015794
		public string ProviderInstructorEmail
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.InstructorEmail;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000167C0 File Offset: 0x000157C0
		public string ProviderInstructorPhone
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.InstructorPhone;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000167EC File Offset: 0x000157EC
		public string ProviderInstructorUsername
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.InstructorUsername;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016818 File Offset: 0x00015818
		public string ProviderCourseYear
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.CourseRequestedProvider.StartDate.Year.ToString();
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016854 File Offset: 0x00015854
		public string ProviderTimetable
		{
			get
			{
				return (this.CourseRequestedProvider == null) ? "" : this.GetCourseTimeTable(this.CourseRequestedProvider);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016884 File Offset: 0x00015884
		public string TypeOfRequest
		{
			get
			{
				string text = Enum.GetName(typeof(eServiceProviderType), this.RequestType);
				if (text == null)
				{
					text = "";
				}
				return text.Replace("_", " ");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000168D4 File Offset: 0x000158D4
		public string ProviderAssignedStudents
		{
			get
			{
				string result;
				if (this.ProviderAssigned == null)
				{
					result = "";
				}
				else
				{
					if (this.providerAssignedStudents == null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						string value = "\r\n";
						int serviceProviderId = this.ProviderAssigned.ServiceProviderId;
						int requestType = (int)this.RequestType;
						string commandText = "SELECT    DISTINCT spr.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        serviceproviderrequests spr LEFT JOIN people p ON p.personid=spr.personid\r\nWHERE       spr.serviceproviderid=@spid AND spr.serviceprovidertype=@sptype";
						UnivDataAdapter da = ClientCache.CurrentInstance.da;
						DataTable dataTable = new DataTable();
						da.SelectCommand.CommandText = commandText;
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@spid", serviceProviderId);
						da.SelectCommand.Parameters.Add("@sptype", requestType);
						da.Fill(dataTable);
						TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
						dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
						{
							"firstname",
							"middlename",
							"lastname",
							"student_no"
						});
						dataTable.TableName = "students";
						foreach (object obj in new DataView
						{
							Table = dataTable,
							Sort = "lastname,firstname"
						})
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow row = dataRowView.Row;
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(value);
							}
							stringBuilder.AppendFormat("{0} {1} ({2})", row["firstname"].ToString(), row["lastname"].ToString(), row["student_no"].ToString());
						}
						this.providerAssignedStudents = stringBuilder.ToString();
					}
					result = this.providerAssignedStudents;
				}
				return result;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016AFC File Offset: 0x00015AFC
		private string GetCourseTimeTable(Course course)
		{
			string result;
			if (course == null)
			{
				result = "";
			}
			else
			{
				List<TimeTableItem> items = TimeTableItem.LoadCourseTimetable(course.LuCourseId);
				result = TimeTableItem.TimeTableItemsToString(items);
			}
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016B4C File Offset: 0x00015B4C
		public static IList<ServiceProviderRequest> LoadRequests(IList<int> serviceProviderRequestIds)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "SELECT orderid AS serviceproviderrequestid INTO #t1 FROM splitorderids(@ids,',');\r\n\r\nSELECT    spr.serviceproviderrequestid,spr.personid,spr.lucourseid,spr.serviceprovidertype,spr.dateentered\r\n            ,spr.whoentered,spr.datetimerequesttitle,spr.startdatetimerequest,spr.enddatetimerequest\r\n            ,spr.startdate,spr.enddate,spr.serviceproviderrequestdetailid,spr.notes,spr.dateassigned\r\n            ,spr.specialinstructions,spr.studentrequested,spr.studentrequestedcancelnote\r\n            ,spr.partsgroupid,spr.partsdescription,spr.serviceproviderlucourseid\r\n            ,spr.serviceproviderid\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\n            ,luc.startdate AS coursestartdate,luc.enddate AS courseenddate,luc.term,luc.duration\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,lucd2.altlookupstring AS instructor\r\n            ,lucd2.email AS instructoremail,lucd2.phone AS instructorphone\r\n            ,lucd2.username AS instructorusername,lucd2.id AS instructorid\r\n            ,lucb.term AS providerterm,lucb.duration AS providerduration,lucdb.altlookupstring AS providersubject\r\n            ,lucb.course AS providercourse,lucb.[section] AS providersection,lucb.timeofday AS providertimeofday\r\n            ,lucdb2.altlookupstring AS providerinstructor,lucdb2.email AS providerinstructoremail,lucdb2.phone AS providerinstructorphone\r\n            ,lucdb2.username AS providerinstructorusername,lucdb2.id AS providerinstructorid\r\n            ,sp.firstname AS providerfirstname,sp.lastname AS providerlastname,sp.middlename AS providermiddlename\r\n            ,sp.student_no AS providerstudent_no,sp.altid AS provideraltid,sp.specialization AS providerspecialization\r\n            ,sp.notes1 AS providernotes1,sp.notes2 AS providernotes2,sp.email AS provideremail\r\n            ,sp.phone1 AS providerphone,sp.phone2 AS providercell,sp.phonenote AS providerphonenote\r\n            ,sp.address AS provideraddress\r\n            ,sp.email2 AS provideralternateemail\r\nFROM        serviceproviderrequests spr LEFT JOIN people p ON p.personid=spr.personid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spr.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN lucourses lucb ON lucb.lucourseid=spr.serviceproviderlucourseid\r\n            LEFT JOIN lucoursedata lucdb ON lucdb.lucoursedataid=lucb.subjectid\r\n            LEFT JOIN lucoursedata lucdb2 ON lucdb2.lucoursedataid=lucb.instructorid\r\n            LEFT JOIN people pw ON pw.personid=spr.whoentered\r\n            LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spr.serviceproviderid\r\nWHERE       spr.serviceproviderrequestid IN (SELECT serviceproviderrequestid FROM #t1)\r\nORDER BY spr.serviceproviderrequestid\r\n\r\nDROP TABLE #t1";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@ids", string.Join(",", serviceProviderRequestIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"middlename",
				"notes",
				"specialinstructions",
				"providerfirstname",
				"providermiddlename",
				"providerlastname",
				"providerstudent_no",
				"provideremail",
				"provideralternateemail",
				"providerphone",
				"providercell",
				"providerspecialization",
				"providernotes1",
				"providernotes2"
			});
			List<ServiceProviderRequest> list = new List<ServiceProviderRequest>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ServiceProviderRequest serviceProviderRequest = new ServiceProviderRequest();
				serviceProviderRequest.ServiceProviderRequestId = ((dataRow["serviceproviderrequestid"] is DBNull) ? 0 : ((int)dataRow["serviceproviderrequestid"]));
				int num = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
				if (num > 0)
				{
					PersonBaseDTO personFromDataRow = ClockWorkCore.GetPersonFromDataRow(tripleDES, dataRow);
					serviceProviderRequest.StudentWhoRequested = personFromDataRow;
				}
				int num2 = (dataRow["serviceproviderid"] == DBNull.Value) ? 0 : ((int)dataRow["serviceproviderid"]);
				if (num2 > 0)
				{
					serviceProviderRequest.ProviderAssigned = new ServiceProvider(dataRow);
				}
				int num3 = (dataRow["lucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["lucourseid"]);
				if (num3 > 0)
				{
					serviceProviderRequest.CourseRequested = new Course(dataRow);
				}
				int num4 = (dataRow["serviceproviderlucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["serviceproviderlucourseid"]);
				if (num4 > 0)
				{
					dataRow["lucourseid"] = dataRow["serviceproviderlucourseid"];
					string[] array = new string[]
					{
						"term",
						"duration",
						"subject",
						"course",
						"section",
						"timeofday",
						"instructor",
						"instructorphone",
						"instructoremail",
						"instructorusername"
					};
					foreach (string text in array)
					{
						dataRow[text] = dataRow["provider" + text];
					}
					serviceProviderRequest.CourseRequestedProvider = new Course(dataRow);
				}
				int num5 = (dataRow["serviceprovidertype"] == DBNull.Value) ? 0 : ((int)dataRow["serviceprovidertype"]);
				if (num5 > 0)
				{
					serviceProviderRequest.RequestType = (eServiceProviderType)num5;
				}
				list.Add(serviceProviderRequest);
			}
			return list;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016F90 File Offset: 0x00015F90
		public static ServiceProviderRequest LoadRequest(int serviceProviderRequestId)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "SELECT    spr.serviceproviderrequestid,spr.personid,spr.lucourseid,spr.serviceprovidertype,spr.dateentered\r\n            ,spr.whoentered,spr.datetimerequesttitle,spr.startdatetimerequest,spr.enddatetimerequest\r\n            ,spr.startdate,spr.enddate,spr.serviceproviderrequestdetailid,spr.notes,spr.dateassigned\r\n            ,spr.specialinstructions,spr.studentrequested,spr.studentrequestedcancelnote\r\n            ,spr.partsgroupid,spr.partsdescription,spr.serviceproviderlucourseid\r\n            ,spr.serviceproviderid\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\n            ,luc.startdate AS coursestartdate,luc.enddate AS courseenddate,luc.term,luc.duration\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,lucd2.altlookupstring AS instructor\r\n            ,lucd2.email AS instructoremail,lucd2.phone AS instructorphone\r\n            ,lucd2.username AS instructorusername,lucd2.id AS instructorid\r\n            ,lucb.term AS providerterm,lucb.duration AS providerduration,lucdb.altlookupstring AS providersubject\r\n            ,lucb.course AS providercourse,lucb.[section] AS providersection,lucb.timeofday AS providertimeofday\r\n            ,lucdb2.altlookupstring AS providerinstructor,lucdb2.email AS providerinstructoremail,lucdb2.phone AS providerinstructorphone\r\n            ,lucdb2.username AS providerinstructorusername,lucdb2.id AS providerinstructorid\r\n            ,sp.firstname AS providerfirstname,sp.lastname AS providerlastname,sp.middlename AS providermiddlename\r\n            ,sp.student_no AS providerstudent_no,sp.altid AS provideraltid,sp.specialization AS providerspecialization\r\n            ,sp.notes1 AS providernotes1,sp.notes2 AS providernotes2,sp.email AS provideremail\r\n            ,sp.phone1 AS providerphone,sp.phone2 AS providercell,sp.phonenote AS providerphonenote\r\n            ,sp.address AS provideraddress\r\n            ,sp.email2 AS provideralternateemail\r\nFROM        serviceproviderrequests spr LEFT JOIN people p ON p.personid=spr.personid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spr.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN lucourses lucb ON lucb.lucourseid=spr.serviceproviderlucourseid\r\n            LEFT JOIN lucoursedata lucdb ON lucdb.lucoursedataid=lucb.subjectid\r\n            LEFT JOIN lucoursedata lucdb2 ON lucdb2.lucoursedataid=lucb.instructorid\r\n            LEFT JOIN people pw ON pw.personid=spr.whoentered\r\n            LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spr.serviceproviderid\r\nWHERE       spr.serviceproviderrequestid=@id";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@id", serviceProviderRequestId);
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"middlename",
				"notes",
				"specialinstructions",
				"providerfirstname",
				"providermiddlename",
				"providerlastname",
				"providerstudent_no",
				"provideremail",
				"provideralternateemail",
				"providerphone",
				"providercell",
				"providerspecialization",
				"providernotes1",
				"providernotes2"
			});
			ServiceProviderRequest serviceProviderRequest = new ServiceProviderRequest();
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
				if (num > 0)
				{
					PersonBaseDTO personFromDataRow = ClockWorkCore.GetPersonFromDataRow(tripleDES, dataRow);
					serviceProviderRequest.StudentWhoRequested = personFromDataRow;
				}
				int num2 = (dataRow["serviceproviderid"] == DBNull.Value) ? 0 : ((int)dataRow["serviceproviderid"]);
				if (num2 > 0)
				{
					serviceProviderRequest.ProviderAssigned = new ServiceProvider(dataRow);
				}
				int num3 = (dataRow["lucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["lucourseid"]);
				if (num3 > 0)
				{
					serviceProviderRequest.CourseRequested = new Course(dataRow);
				}
				int num4 = (dataRow["serviceproviderlucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["serviceproviderlucourseid"]);
				if (num4 > 0)
				{
					dataRow["lucourseid"] = dataRow["serviceproviderlucourseid"];
					string[] array = new string[]
					{
						"term",
						"duration",
						"subject",
						"course",
						"section",
						"timeofday",
						"instructor",
						"instructorphone",
						"instructoremail",
						"instructorusername"
					};
					foreach (string text in array)
					{
						dataRow[text] = dataRow["provider" + text];
					}
					serviceProviderRequest.CourseRequestedProvider = new Course(dataRow);
				}
				int num5 = (dataRow["serviceprovidertype"] == DBNull.Value) ? 0 : ((int)dataRow["serviceprovidertype"]);
				if (num5 > 0)
				{
					serviceProviderRequest.RequestType = (eServiceProviderType)num5;
				}
			}
			return serviceProviderRequest;
		}

		// Token: 0x040001BB RID: 443
		private string providerAssignedStudents = null;
	}
}
