using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ClockWorkAPI.EntityExtensions;
using ClockWorkAPI.Exams.Sittings;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000023 RID: 35
	public class Test
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009934 File Offset: 0x00008934
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000994C File Offset: 0x0000894C
		public DateTime ScheduledStartDateTime
		{
			get
			{
				return this.scheduledStartDateTime;
			}
			set
			{
				this.scheduledStartDateTime = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00009958 File Offset: 0x00008958
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00009970 File Offset: 0x00008970
		public DateTime ScheduledEndDateTime
		{
			get
			{
				return this.scheduledEndDateTime;
			}
			set
			{
				this.scheduledEndDateTime = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000997C File Offset: 0x0000897C
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00009994 File Offset: 0x00008994
		public DateTime ClassStartDateTime
		{
			get
			{
				return this.classStartDateTime;
			}
			set
			{
				this.classStartDateTime = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000099A0 File Offset: 0x000089A0
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000099B8 File Offset: 0x000089B8
		public DateTime ClassEndDateTime
		{
			get
			{
				return this.classEndDateTime;
			}
			set
			{
				this.classEndDateTime = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000099C4 File Offset: 0x000089C4
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000099DC File Offset: 0x000089DC
		public DateTime ActualStartTime
		{
			get
			{
				return this.actualStartTime;
			}
			set
			{
				this.actualStartTime = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000099E8 File Offset: 0x000089E8
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009A00 File Offset: 0x00008A00
		public DateTime ActualEndTime
		{
			get
			{
				return this.actualEndTime;
			}
			set
			{
				this.actualEndTime = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009A0C File Offset: 0x00008A0C
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009A24 File Offset: 0x00008A24
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009A30 File Offset: 0x00008A30
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009A48 File Offset: 0x00008A48
		public string AppTypeDescription
		{
			get
			{
				return this.appTypeDescription;
			}
			set
			{
				this.appTypeDescription = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009A54 File Offset: 0x00008A54
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009A6C File Offset: 0x00008A6C
		public int AppCode
		{
			get
			{
				return this.appCode;
			}
			set
			{
				this.appCode = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009A78 File Offset: 0x00008A78
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009A90 File Offset: 0x00008A90
		public string AppCodeDescription
		{
			get
			{
				return this.appCodeDescription;
			}
			set
			{
				this.appCodeDescription = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009A9C File Offset: 0x00008A9C
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00009AB4 File Offset: 0x00008AB4
		public List<AttendeeDTO> Students
		{
			get
			{
				return this.students;
			}
			set
			{
				this.students = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00009AC0 File Offset: 0x00008AC0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00009AD8 File Offset: 0x00008AD8
		public List<AttendeeDTO> OtherAttendees
		{
			get
			{
				return this.otherAttendees;
			}
			set
			{
				this.otherAttendees = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009AE4 File Offset: 0x00008AE4
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009AFC File Offset: 0x00008AFC
		public int RoomPid
		{
			get
			{
				return this.roomPid;
			}
			set
			{
				this.roomPid = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009B08 File Offset: 0x00008B08
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00009B20 File Offset: 0x00008B20
		public string RoomDescription
		{
			get
			{
				return this.roomDescription;
			}
			set
			{
				this.roomDescription = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009B2C File Offset: 0x00008B2C
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00009B44 File Offset: 0x00008B44
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009B50 File Offset: 0x00008B50
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009B68 File Offset: 0x00008B68
		public int LuCourseId
		{
			get
			{
				return this.luCourseId;
			}
			set
			{
				this.luCourseId = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009B74 File Offset: 0x00008B74
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009B8C File Offset: 0x00008B8C
		public string CourseDescription
		{
			get
			{
				return this.courseDescription;
			}
			set
			{
				this.courseDescription = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009B98 File Offset: 0x00008B98
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009BB0 File Offset: 0x00008BB0
		public string TestNote
		{
			get
			{
				return this.testNote;
			}
			set
			{
				this.testNote = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009BBC File Offset: 0x00008BBC
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00009BD4 File Offset: 0x00008BD4
		public string StudentNote
		{
			get
			{
				return this.studentNote;
			}
			set
			{
				this.studentNote = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009BE0 File Offset: 0x00008BE0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00009BF8 File Offset: 0x00008BF8
		public int AppointmentId
		{
			get
			{
				return this.appointmentId;
			}
			set
			{
				this.appointmentId = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009C04 File Offset: 0x00008C04
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009C1C File Offset: 0x00008C1C
		public int ExamId
		{
			get
			{
				return this.examId;
			}
			set
			{
				this.examId = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009C28 File Offset: 0x00008C28
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009C40 File Offset: 0x00008C40
		public Course Course
		{
			get
			{
				return this.course;
			}
			set
			{
				this.course = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009C4C File Offset: 0x00008C4C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009C64 File Offset: 0x00008C64
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
			set
			{
				this.cancelled = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009C70 File Offset: 0x00008C70
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009C88 File Offset: 0x00008C88
		public string TestDelivered
		{
			get
			{
				return this.testDelivered;
			}
			set
			{
				this.testDelivered = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009C94 File Offset: 0x00008C94
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009CAC File Offset: 0x00008CAC
		public string PrivateNote2
		{
			get
			{
				return this.privateNote2;
			}
			set
			{
				this.privateNote2 = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009CB8 File Offset: 0x00008CB8
		public string Status
		{
			get
			{
				return string.IsNullOrEmpty(this.status) ? "" : this.status;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009CE4 File Offset: 0x00008CE4
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009CFC File Offset: 0x00008CFC
		public int SittingId
		{
			get
			{
				return this.sittingId;
			}
			set
			{
				this.sittingId = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009D08 File Offset: 0x00008D08
		public ExamSitting Sitting
		{
			get
			{
				ExamSitting result;
				if (this.sitting != null)
				{
					result = this.sitting;
				}
				else if (this.sittingId > 0)
				{
					this.sitting = ExamSitting.LoadSitting(this.sittingId);
					result = this.sitting;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009D5C File Offset: 0x00008D5C
		public string SittingInvigilator
		{
			get
			{
				return (this.Sitting == null || this.Sitting.Invigilator == null) ? "" : this.Sitting.Invigilator.GetName();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009D9C File Offset: 0x00008D9C
		public string SittingInvigilatorEmail
		{
			get
			{
				return (this.Sitting == null) ? "" : this.Sitting.InvigilatorEmail;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009DC8 File Offset: 0x00008DC8
		public string SittingInvigilatorNotes
		{
			get
			{
				return (this.Sitting == null) ? "" : ((this.Sitting.InvigilatorNotes == null) ? "" : this.Sitting.InvigilatorNotes);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009E08 File Offset: 0x00008E08
		public string SittingLocation
		{
			get
			{
				return (this.Sitting == null) ? "" : ((this.Sitting.Location == null) ? "" : this.Sitting.Location);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009E48 File Offset: 0x00008E48
		public string SittingRoom
		{
			get
			{
				return (this.Sitting == null) ? "" : ((this.Sitting.Room == null) ? "" : this.Sitting.Room.FirstName);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009E90 File Offset: 0x00008E90
		public string SittingRoomOrLocation
		{
			get
			{
				string result;
				if (this.Sitting == null)
				{
					result = "";
				}
				else
				{
					string sittingRoom = this.SittingRoom;
					string sittingLocation = this.SittingLocation;
					if (sittingLocation.Length > 0 && sittingRoom.Length > 0)
					{
						result = string.Format("{0}; {1}", sittingRoom, sittingLocation);
					}
					else if (sittingLocation.Length > 0)
					{
						result = sittingRoom;
					}
					else
					{
						result = sittingLocation;
					}
				}
				return result;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00009F08 File Offset: 0x00008F08
		public string SittingCalculatedBookingsRange
		{
			get
			{
				return (this.sitting == null) ? "" : this.Sitting.Display_CalculatedBookingsRange;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009F34 File Offset: 0x00008F34
		public string SittingOverrideScheduledTime
		{
			get
			{
				return (this.sitting == null) ? "" : this.Sitting.Display_OverrideScheduledTime;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00009F60 File Offset: 0x00008F60
		public string SittingTitle
		{
			get
			{
				return (this.sitting == null) ? "" : this.Sitting.Title;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009FC0 File Offset: 0x00008FC0
		public string Invigilators
		{
			get
			{
				string result;
				if (this.otherAttendees == null || this.otherAttendees.Count < 1)
				{
					result = "";
				}
				else
				{
					string text = string.Join(", ", this.otherAttendees.ConvertAll<string>((AttendeeDTO att) => string.Format("{0} {1}", att.Person.FirstName, att.Person.LastName)).ToArray());
					result = text;
				}
				return result;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A038 File Offset: 0x00009038
		public string RoomAndLocation
		{
			get
			{
				string result;
				if (string.IsNullOrEmpty(this.roomDescription))
				{
					result = this.location;
				}
				else if (string.IsNullOrEmpty(this.location))
				{
					result = this.roomDescription;
				}
				else
				{
					result = string.Format("{0} {1}", this.roomDescription, this.location);
				}
				return result;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A098 File Offset: 0x00009098
		private PersonBaseDTO GetStudentsCounsellor(int pid)
		{
			PersonBaseDTO result;
			if (this.assignedCounsellor == null)
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "DECLARE @counsellorcid int\r\nSET @counsellorcid = (SELECT TOP 1 settingvalue AS counsellorcid FROM settingsgroups WHERE groupid=-1 AND settingcode=99671)\r\n\r\nSELECT ps.valint AS personid,p.firstname,p.lastname,p.student_no\r\nFROM perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.valint\r\nWHERE ps.controlid=@counsellorcid AND ps.personid=@pid";
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.Fill(dataTable);
				TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
				if (dataTable.Rows.Count > 0)
				{
					dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname",
						"student_no"
					});
					DataRow dataRow = dataTable.Rows[0];
					string firstName = dataRow["firstname"].ToString().Trim();
					string lastName = dataRow["lastname"].ToString().Trim();
					PersonBaseDTO personBaseDTO = new PersonBaseDTO
					{
						PersonId = ((dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"])),
						FirstName = firstName,
						LastName = lastName,
						Student_no = "",
						MiddleName = "",
						CoreGroup = eCoreGroupDTO.Staff,
						Groups = new List<GroupDTO>(),
						Tag = new PersonExt()
					};
					result = personBaseDTO;
				}
				else
				{
					PersonBaseDTO personBaseDTO2 = new PersonBaseDTO
					{
						PersonId = -1,
						FirstName = "",
						MiddleName = "",
						LastName = "",
						Student_no = "",
						CoreGroup = eCoreGroupDTO.Staff,
						Groups = new List<GroupDTO>()
					};
					result = personBaseDTO2;
				}
			}
			else
			{
				result = this.assignedCounsellor;
			}
			return result;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A2AC File Offset: 0x000092AC
		public string CounsellorName
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				string result;
				if (firstStudent != null)
				{
					PersonBaseDTO studentsCounsellor = this.GetStudentsCounsellor(firstStudent.Person.PersonId);
					if (studentsCounsellor != null && studentsCounsellor.PersonId > 0)
					{
						result = studentsCounsellor.GetName();
					}
					else
					{
						result = "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A310 File Offset: 0x00009310
		public string CounsellorFirstName
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				if (firstStudent != null)
				{
					PersonBaseDTO studentsCounsellor = this.GetStudentsCounsellor(firstStudent.Person.PersonId);
					if (studentsCounsellor != null && studentsCounsellor.PersonId > 0)
					{
						return studentsCounsellor.FirstName;
					}
				}
				return "";
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A36C File Offset: 0x0000936C
		public string CounsellorLastName
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				if (firstStudent != null)
				{
					PersonBaseDTO studentsCounsellor = this.GetStudentsCounsellor(firstStudent.Person.PersonId);
					if (studentsCounsellor != null && studentsCounsellor.PersonId > 0)
					{
						return studentsCounsellor.LastName;
					}
				}
				return "";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000A3C8 File Offset: 0x000093C8
		public string AlternateContactName
		{
			get
			{
				string result;
				if (this.course != null)
				{
					result = ((this.course.AlternateContact == null) ? "" : this.course.AlternateContact.Name);
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A414 File Offset: 0x00009414
		public string AlternateContactEmail
		{
			get
			{
				string result;
				if (this.course != null)
				{
					result = ((this.course.AlternateContact == null) ? "" : this.course.AlternateContact.Email);
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A460 File Offset: 0x00009460
		public string AlternateContactPhone
		{
			get
			{
				string result;
				if (this.course != null)
				{
					result = ((this.course.AlternateContact == null) ? "" : this.course.AlternateContact.Phone);
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A4AC File Offset: 0x000094AC
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A4C4 File Offset: 0x000094C4
		public string InvigilatorNotes
		{
			get
			{
				return this.memo;
			}
			set
			{
				this.memo = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A4D0 File Offset: 0x000094D0
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A540 File Offset: 0x00009540
		public string InvigilatorNotesPlain
		{
			get
			{
				string text;
				using (RichTextBox richTextBox = new RichTextBox())
				{
					try
					{
						richTextBox.Rtf = this.memo;
					}
					catch
					{
						richTextBox.Text = this.memo;
					}
					text = richTextBox.Text;
				}
				return text;
			}
			set
			{
				using (RichTextBox richTextBox = new RichTextBox())
				{
					richTextBox.Text = value;
					this.memo = richTextBox.Rtf;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A590 File Offset: 0x00009590
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000A5A8 File Offset: 0x000095A8
		public string Memo
		{
			get
			{
				return this.memo;
			}
			set
			{
				this.memo = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A5B4 File Offset: 0x000095B4
		public string ScheduledEndTimeWithoutBreaks
		{
			get
			{
				return this.scheduledEndDateTime.AddMinutes((double)(-(double)this.totalBreakMinutes)).ToString("h:mm tt");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A5E8 File Offset: 0x000095E8
		public string FirstName
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				string result;
				if (firstStudent != null)
				{
					result = firstStudent.Person.FirstName;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A620 File Offset: 0x00009620
		public string LastName
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				string result;
				if (firstStudent != null)
				{
					result = firstStudent.Person.LastName;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A658 File Offset: 0x00009658
		public string Student_No
		{
			get
			{
				return this.StudentNo;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A670 File Offset: 0x00009670
		public string StudentNo
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				string result;
				if (firstStudent != null)
				{
					result = firstStudent.Person.Student_no;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A6A8 File Offset: 0x000096A8
		public int StudentPid
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				int result;
				if (firstStudent != null)
				{
					result = firstStudent.Person.PersonId;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A6DC File Offset: 0x000096DC
		private AttendeeDTO GetFirstStudent()
		{
			return (this.students == null || this.students.Count < 1) ? null : this.students[0];
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A714 File Offset: 0x00009714
		public string ScheduledDate
		{
			get
			{
				return this.scheduledStartDateTime.ToString("dddd MMMM d, yyyy");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A738 File Offset: 0x00009738
		public string ScheduledDate2
		{
			get
			{
				return this.scheduledStartDateTime.ToString("MM/dd/yy");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A75C File Offset: 0x0000975C
		public string ScheduledDate3
		{
			get
			{
				return this.scheduledStartDateTime.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A780 File Offset: 0x00009780
		public string ScheduledStartTime
		{
			get
			{
				return this.scheduledStartDateTime.ToString("h:mm tt");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A7A4 File Offset: 0x000097A4
		public string ScheduledEndTime
		{
			get
			{
				return this.scheduledEndDateTime.ToString("h:mm tt");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A7C8 File Offset: 0x000097C8
		public string ClassDate
		{
			get
			{
				return this.classStartDateTime.ToString("dddd MMMM d, yyyy");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A7EC File Offset: 0x000097EC
		public string ClassDate2
		{
			get
			{
				return this.classStartDateTime.ToString("MM/dd/yy");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A810 File Offset: 0x00009810
		public string ClassDate3
		{
			get
			{
				return this.classStartDateTime.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A834 File Offset: 0x00009834
		public string ClassStartTime
		{
			get
			{
				return this.classStartDateTime.ToString("h:mm tt");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A858 File Offset: 0x00009858
		public string ClassEndTime
		{
			get
			{
				return this.classEndDateTime.ToString("h:mm tt");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A87C File Offset: 0x0000987C
		public string ClassDuration
		{
			get
			{
				int durationInMinutes = Convert.ToInt32((this.classEndDateTime - this.classStartDateTime).TotalMinutes);
				return durationInMinutes.GetDurationDescription();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A8B4 File Offset: 0x000098B4
		public int ClassDurationMinutes
		{
			get
			{
				return Convert.ToInt32((this.classEndDateTime - this.classStartDateTime).TotalMinutes);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000A8E4 File Offset: 0x000098E4
		public int ScheduledDurationMinutes
		{
			get
			{
				return Convert.ToInt32((this.scheduledEndDateTime - this.scheduledStartDateTime).TotalMinutes);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A914 File Offset: 0x00009914
		public string ScheduledDuration
		{
			get
			{
				int scheduledDurationMinutes = this.ScheduledDurationMinutes;
				return Test.GetDurationDescription(scheduledDurationMinutes);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A934 File Offset: 0x00009934
		public string AdditionalTimeDuration
		{
			get
			{
				int additionalTimeDurationMinutes = this.AdditionalTimeDurationMinutes;
				return Test.GetDurationDescription(additionalTimeDurationMinutes);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A954 File Offset: 0x00009954
		public int AdditionalTimeDurationMinutes
		{
			get
			{
				int classDurationMinutes = this.ClassDurationMinutes;
				int scheduledDurationMinutes = this.ScheduledDurationMinutes;
				return scheduledDurationMinutes - classDurationMinutes;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A978 File Offset: 0x00009978
		private static string GetDurationDescription(int DurationMinutes)
		{
			int num = (int)(Convert.ToDouble(DurationMinutes) / 60.0);
			int num2 = DurationMinutes - num * 60;
			string text = "";
			if (num == 1)
			{
				text = "1 hour";
				if (num2 > 0)
				{
					text += "; ";
				}
			}
			else if (num > 1)
			{
				text = num.ToString() + " hours";
				if (num2 > 0)
				{
					text += "; ";
				}
			}
			if (num2 == 1)
			{
				text += "1 minute";
			}
			else if (num2 > 1)
			{
				text = text + num2.ToString() + " minutes";
			}
			return text;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000AA54 File Offset: 0x00009A54
		public string Subject
		{
			get
			{
				return (this.course == null) ? "" : this.course.Subject;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000AA80 File Offset: 0x00009A80
		public string CourseCode
		{
			get
			{
				return (this.course == null) ? "" : this.course.CourseCode;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000AAAC File Offset: 0x00009AAC
		public string Section
		{
			get
			{
				return (this.course == null) ? "" : this.course.Section;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000AAD8 File Offset: 0x00009AD8
		public string TimeOfDay
		{
			get
			{
				return (this.course == null) ? "" : this.course.TimeOfDay;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000AB04 File Offset: 0x00009B04
		public string Duration
		{
			get
			{
				return (this.course == null) ? "" : this.course.Duration;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000AB30 File Offset: 0x00009B30
		public string Term
		{
			get
			{
				return (this.course == null) ? "" : this.course.Term;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000AB5C File Offset: 0x00009B5C
		public string CourseStartDate
		{
			get
			{
				return (this.course == null) ? "" : this.course.StartDate.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000AB98 File Offset: 0x00009B98
		public string CourseEndDate
		{
			get
			{
				return (this.course == null) ? "" : this.course.EndDate.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000ABD4 File Offset: 0x00009BD4
		public string Campus
		{
			get
			{
				return (this.course.Campus == null) ? "" : this.course.Campus;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000AC08 File Offset: 0x00009C08
		public string Department
		{
			get
			{
				return (this.course.Department == null) ? "" : this.course.Department;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000AC3C File Offset: 0x00009C3C
		public string Instructor
		{
			get
			{
				return (this.course == null) ? "" : this.course.InstructorName;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000AC68 File Offset: 0x00009C68
		public string InstructorEmail
		{
			get
			{
				return (this.course == null) ? "" : this.course.InstructorEmail;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000AC94 File Offset: 0x00009C94
		public string InstructorPhone
		{
			get
			{
				return (this.course == null) ? "" : this.course.InstructorPhone;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000ACC0 File Offset: 0x00009CC0
		public string ExamAccommodations2
		{
			get
			{
				return this.testNote;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000ACF4 File Offset: 0x00009CF4
		public string ExamAccommodations
		{
			get
			{
				AttendeeDTO firstStudent = this.GetFirstStudent();
				PersonBaseDTO personBaseDTO = (firstStudent == null) ? null : firstStudent.Person;
				List<Accommodation> accommodationsChecked = Test.GetAccommodationsChecked(this.appointmentId, this.examId, (personBaseDTO == null) ? 0 : personBaseDTO.PersonId);
				string result;
				if (accommodationsChecked.Count > 0)
				{
					string text = "• " + string.Join("\r\n• ", accommodationsChecked.ConvertAll<string>((Accommodation acc) => acc.ToStringHtml(false, false)).ToArray());
					result = text;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AD98 File Offset: 0x00009D98
		public static List<Accommodation> GetAccommodationsChecked(int appointmentId, int examId, int personId)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			string commandText = "IF EXISTS(SELECT appointmentid FROM AccommodationsTest WHERE AppointmentId=@appid)\r\nBEGIN\r\nSELECT \tat.appointmentid,at.controlid,dc.controlcaption,ad.courseid,ac.lucourseid\r\n\t,ad.valtext,ad.valbytes,ad.valimage,ad.setting1,ad.setting2,ad.setting3,ad.setting4\r\n\t,ad.controlcode,ad.valint,ad.valdate,ad.dataid,ad.valbytesisencrypted,a.longdescription,ad.altlongdescription\r\nFROM\taccommodationstest at LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=at.controlid\r\n    LEFT JOIN dynamiccontrols dc ON dc.controlid=at.controlid\r\n\tLEFT JOIN appointmentcourses ac ON ac.appointmentid=at.appointmentid\r\n\tLEFT JOIN accommodationdata ad ON ad.personid=at.personid AND ad.controlid=at.controlid AND ad.courseid=dbo.accommodationscourseortemplate(at.personid,ac.lucourseid)\r\n\tLEFT JOIN Accommodations a ON a.ControlID=at.controlid \r\nWHERE at.AppointmentId=@appid\r\nORDER BY dsc.ordernum\r\nEND\r\nELSE\r\nBEGIN\r\nSELECT \tat.appointmentid,at.controlid,dc.controlcaption,ad.courseid,ac.lucourseid\r\n\t,ad.valtext,ad.valbytes,ad.valimage,ad.setting1,ad.setting2,ad.setting3,ad.setting4\r\n\t,ad.controlcode,ad.valint,ad.valdate,ad.dataid,ad.valbytesisencrypted,a.longdescription,ad.altlongdescription\r\nFROM\taccommodationstest at LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=at.controlid\r\n    LEFT JOIN dynamiccontrols dc ON dc.controlid=at.controlid\r\n\tLEFT JOIN appointmentcourses ac ON ac.appointmentid=at.appointmentid\r\n\tLEFT JOIN accommodationdata ad ON ad.personid=at.personid AND ad.controlid=at.controlid AND ad.courseid=dbo.accommodationscourseortemplate(at.personid,ac.lucourseid)\r\n\tLEFT JOIN Accommodations a ON a.ControlID=at.controlid \r\nWHERE at.ExamId=@examid AND at.personid=@pid\r\nORDER BY dsc.ordernum\r\nEND";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@appid", appointmentId);
			da.SelectCommand.Parameters.Add("@examid", examId);
			da.SelectCommand.Parameters.Add("@pid", personId);
			da.Fill(dataTable);
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"altlongdescription"
			});
			List<Accommodation> list = new List<Accommodation>();
			List<int> list2 = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Accommodation accommodation = new Accommodation(dr);
				if (!list2.Contains(accommodation.ControlId))
				{
					list2.Add(accommodation.ControlId);
					list.Add(accommodation);
				}
			}
			return list;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000AF0C File Offset: 0x00009F0C
		public string RoomDescriptionLastWord
		{
			get
			{
				string result;
				if (!string.IsNullOrEmpty(this.roomDescription))
				{
					int num = this.roomDescription.Trim().LastIndexOf(' ');
					result = ((num > 0) ? this.roomDescription.Substring(num + 1) : this.roomDescription);
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000AF64 File Offset: 0x00009F64
		public string RoomDescriptionFirstWord
		{
			get
			{
				string result;
				if (!string.IsNullOrEmpty(this.roomDescription))
				{
					int num = this.roomDescription.Trim().IndexOf(' ');
					result = ((num > 0) ? this.roomDescription.Substring(0, num) : this.roomDescription);
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000AFBC File Offset: 0x00009FBC
		public Test()
		{
			DateTime minValue = DateTime.MinValue;
			this.scheduledStartDateTime = minValue;
			this.scheduledEndDateTime = minValue;
			this.classStartDateTime = minValue;
			this.classEndDateTime = minValue;
			this.actualStartTime = minValue;
			this.actualEndTime = minValue;
			this.appTypeId = -1;
			this.appTypeDescription = "";
			this.appCode = 0;
			this.appCodeDescription = "";
			this.appointmentId = 0;
			this.examId = 0;
			this.roomPid = 0;
			this.roomDescription = "";
			this.location = "";
			this.luCourseId = 0;
			this.courseDescription = "";
			this.students = new List<AttendeeDTO>();
			this.otherAttendees = new List<AttendeeDTO>();
			this.studentNote = "";
			this.testNote = "";
			this.course = null;
			this.cancelled = false;
			this.status = "";
			this.memo = "";
			this.testDelivered = "";
			this.privateNote2 = "";
			this.sittingId = 0;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B0E0 File Offset: 0x0000A0E0
		public Test(DataRow dr)
		{
			DataTable table = dr.Table;
			bool flag = table.Columns.Contains("middlename");
			this.appointmentId = ((dr["appointmentid"] == DBNull.Value) ? 0 : ((int)dr["appointmentid"]));
			this.examId = ((dr["examid"] == DBNull.Value) ? 0 : ((int)dr["examid"]));
			this.scheduledStartDateTime = ((dr["scheduledstarttime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["scheduledstarttime"]));
			this.scheduledEndDateTime = ((dr["scheduledendtime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["scheduledendtime"]));
			this.totalBreakMinutes = ((dr["totalbreakminutes"] == DBNull.Value) ? 0 : ((int)dr["totalbreakminutes"]));
			this.appointmentId = ((dr["appointmentid"] == DBNull.Value) ? 0 : ((int)dr["appointmentid"]));
			this.appTypeId = ((dr["apptypeid"] == DBNull.Value) ? -1 : ((int)dr["apptypeid"]));
			this.appTypeDescription = dr["description"].ToString();
			if (this.appointmentId <= 0 && this.appTypeId <= 0)
			{
			}
			this.appCode = ((dr["appcode"] == DBNull.Value) ? 0 : ((int)dr["appcode"]));
			this.appCodeDescription = "";
			DateTime minValue = DateTime.MinValue;
			this.classStartDateTime = minValue;
			this.classEndDateTime = minValue;
			this.actualStartTime = minValue;
			this.actualEndTime = minValue;
			this.students = new List<AttendeeDTO>();
			this.otherAttendees = new List<AttendeeDTO>();
			this.classStartDateTime = ((dr["classstartdatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["classstartdatetime"]));
			this.classEndDateTime = ((dr["classenddatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["classenddatetime"]));
			if (this.scheduledStartDateTime == DateTime.MinValue)
			{
				this.scheduledStartDateTime = this.classStartDateTime;
				this.scheduledEndDateTime = this.classEndDateTime;
			}
			this.actualStartTime = ((dr["actualstarttime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["actualstarttime"]));
			this.actualEndTime = ((dr["actualendtime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["actualendtime"]));
			if (dr["personid"] != DBNull.Value)
			{
				bool isNoShow = dr["noshow"] != DBNull.Value && Convert.ToBoolean(dr["noshow"]);
				PersonBaseDTO person = new PersonBaseDTO
				{
					PersonId = (int)dr["personid"],
					FirstName = dr["firstname"].ToString(),
					MiddleName = (flag ? dr["middlename"].ToString() : ""),
					LastName = dr["lastname"].ToString(),
					Student_no = dr["student_no"].ToString(),
					CoreGroup = eCoreGroupDTO.Students,
					Tag = new PersonExt()
				};
				AttendeeDTO item = new AttendeeDTO
				{
					Person = person,
					IsNoShow = isNoShow,
					MiscCode = 0,
					Tag = new AttendeeExt()
				};
				this.students.Add(item);
			}
			if (table.Columns.Contains("invigilatorpid") && dr["invigilatorpid"] != DBNull.Value)
			{
				string firstName = table.Columns.Contains("invigilatorfirstname") ? dr["invigilatorfirstname"].ToString() : "";
				string lastName = table.Columns.Contains("invigilatorlastname") ? dr["invigilatorlastname"].ToString() : "";
				PersonBaseDTO person = new PersonBaseDTO
				{
					PersonId = (int)dr["invigilatorpid"],
					FirstName = firstName,
					LastName = lastName,
					MiddleName = "",
					CoreGroup = eCoreGroupDTO.Unknown,
					Tag = new PersonExt()
				};
				AttendeeDTO item = new AttendeeDTO
				{
					Person = person,
					IsNoShow = false,
					MiscCode = 0,
					Tag = new AttendeeExt()
				};
				this.otherAttendees.Add(item);
			}
			if (table.Columns.Contains("memotext") && dr["memotext"] != DBNull.Value)
			{
				this.memo = dr["memotext"].ToString();
			}
			this.roomPid = ((dr["roompid"] == DBNull.Value) ? 0 : ((int)dr["roompid"]));
			this.roomDescription = ((dr["room"] == DBNull.Value) ? "" : dr["room"].ToString());
			if (table.Columns.Contains("location"))
			{
				this.location = ((dr["location"] == DBNull.Value) ? "" : dr["location"].ToString());
			}
			else
			{
				this.location = "";
			}
			if (table.Columns.Contains("lucourseid"))
			{
				this.luCourseId = ((dr["lucourseid"] == DBNull.Value) ? 0 : ((int)dr["lucourseid"]));
			}
			else
			{
				this.luCourseId = 0;
			}
			if (this.luCourseId > 0)
			{
				this.courseDescription = string.Format("{0} {1} {2} {3}", new object[]
				{
					dr["subject"].ToString(),
					dr["course"].ToString(),
					dr["section"].ToString(),
					table.Columns.Contains("timeofday") ? dr["timeofday"].ToString() : ""
				});
				this.course = new Course(dr);
			}
			else
			{
				this.courseDescription = "";
				this.course = null;
			}
			this.testNote = dr["examaccommodations"].ToString();
			this.studentNote = dr["accommodationgroups"].ToString();
			this.cancelled = (dr["cancelled"] != DBNull.Value && Convert.ToBoolean(dr["cancelled"]));
			if (table.Columns.Contains("status"))
			{
				this.status = dr["status"].ToString();
			}
			else
			{
				this.status = "";
			}
			if (table.Columns.Contains("testdelivered"))
			{
				this.testDelivered = dr["testdelivered"].ToString();
			}
			else if (table.Columns.Contains("usercomment"))
			{
				this.testDelivered = dr["usercomment"].ToString();
			}
			else
			{
				this.testDelivered = "";
			}
			if (table.Columns.Contains("privatenote2"))
			{
				this.privateNote2 = dr["privatenote2"].ToString();
			}
			else
			{
				this.privateNote2 = "";
			}
			if (table.Columns.Contains("sittingid"))
			{
				this.sittingId = ((dr["sittingid"] == DBNull.Value) ? 0 : ((int)dr["sittingid"]));
			}
			else
			{
				this.sittingId = 0;
			}
			if (dr["ExamStatusLookupId"] != DBNull.Value)
			{
				this.ExamStatusLookupId = (int)dr["ExamStatusLookupId"];
				this.ExamStatusTitle = dr["ExamStatus"].ToString();
				if (dr["colourargb"] != DBNull.Value)
				{
					this.ExamStatusColourId = (int)dr["colourargb"];
				}
			}
			else
			{
				this.ExamStatusTitle = "";
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000BA44 File Offset: 0x0000AA44
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000BA5B File Offset: 0x0000AA5B
		public int ExamStatusLookupId { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000BA64 File Offset: 0x0000AA64
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000BA7B File Offset: 0x0000AA7B
		public string ExamStatusTitle { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000BA84 File Offset: 0x0000AA84
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000BA9B File Offset: 0x0000AA9B
		public int ExamStatusColourId { get; set; }

		// Token: 0x060001FF RID: 511 RVA: 0x0000BAA4 File Offset: 0x0000AAA4
		public static List<Test> TestsFromTable(DataTable t)
		{
			List<Test> list = new List<Test>();
			foreach (object obj in t.Rows)
			{
				DataRow dr = (DataRow)obj;
				Test item = new Test(dr);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x040000CF RID: 207
		private DateTime scheduledStartDateTime;

		// Token: 0x040000D0 RID: 208
		private DateTime scheduledEndDateTime;

		// Token: 0x040000D1 RID: 209
		private int totalBreakMinutes;

		// Token: 0x040000D2 RID: 210
		private DateTime classStartDateTime;

		// Token: 0x040000D3 RID: 211
		private DateTime classEndDateTime;

		// Token: 0x040000D4 RID: 212
		private DateTime actualStartTime;

		// Token: 0x040000D5 RID: 213
		private DateTime actualEndTime;

		// Token: 0x040000D6 RID: 214
		private int appTypeId;

		// Token: 0x040000D7 RID: 215
		private int appCode;

		// Token: 0x040000D8 RID: 216
		private string appTypeDescription;

		// Token: 0x040000D9 RID: 217
		private string appCodeDescription;

		// Token: 0x040000DA RID: 218
		private string memo;

		// Token: 0x040000DB RID: 219
		private string testDelivered;

		// Token: 0x040000DC RID: 220
		private int appointmentId;

		// Token: 0x040000DD RID: 221
		private int examId;

		// Token: 0x040000DE RID: 222
		private List<AttendeeDTO> students;

		// Token: 0x040000DF RID: 223
		private List<AttendeeDTO> otherAttendees;

		// Token: 0x040000E0 RID: 224
		private int roomPid;

		// Token: 0x040000E1 RID: 225
		private string roomDescription;

		// Token: 0x040000E2 RID: 226
		private string location;

		// Token: 0x040000E3 RID: 227
		private int luCourseId;

		// Token: 0x040000E4 RID: 228
		private string courseDescription;

		// Token: 0x040000E5 RID: 229
		private Course course;

		// Token: 0x040000E6 RID: 230
		private string testNote;

		// Token: 0x040000E7 RID: 231
		private string studentNote;

		// Token: 0x040000E8 RID: 232
		private string privateNote2;

		// Token: 0x040000E9 RID: 233
		private bool cancelled;

		// Token: 0x040000EA RID: 234
		private string status;

		// Token: 0x040000EB RID: 235
		private int sittingId;

		// Token: 0x040000EC RID: 236
		private ExamSitting sitting = null;

		// Token: 0x040000ED RID: 237
		private PersonBaseDTO assignedCounsellor = null;
	}
}
