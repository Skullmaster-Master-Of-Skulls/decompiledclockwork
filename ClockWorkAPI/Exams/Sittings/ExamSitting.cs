using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkAPI.EntityExtensions;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Appointments;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.Appointments;
using UnivOleDb;

namespace ClockWorkAPI.Exams.Sittings
{
	// Token: 0x02000013 RID: 19
	public class ExamSitting
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000354C File Offset: 0x0000254C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003563 File Offset: 0x00002563
		public string InvigilatorEmail { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000356C File Offset: 0x0000256C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003584 File Offset: 0x00002584
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003590 File Offset: 0x00002590
		public string Display_CalculatedBookingsRange
		{
			get
			{
				string result;
				if (this.minScheduledStartTime != DateTime.MinValue && this.maxScheduledEndTime != DateTime.MinValue)
				{
					result = string.Format("{0} to {1}", this.minScheduledStartTime.ToString("h:mm tt"), this.maxScheduledEndTime.ToString("h:mm tt"));
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003604 File Offset: 0x00002604
		public string Display_Room
		{
			get
			{
				string result;
				if (this.room == null)
				{
					result = this.location;
				}
				else if (string.IsNullOrEmpty(this.location))
				{
					result = this.room.GetName();
				}
				else
				{
					result = string.Format("{0} ({1})", this.room.FirstName, this.location);
				}
				return result;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000366C File Offset: 0x0000266C
		public string Display_InvigilatorName
		{
			get
			{
				return (this.invigilator == null) ? "" : this.invigilator.GetName();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003698 File Offset: 0x00002698
		public string Display_OverrideScheduledTime
		{
			get
			{
				string result;
				if (this.scheduledStartTime != DateTime.MinValue && this.scheduledEndTime != DateTime.MinValue)
				{
					result = string.Format("{0} to {1}", this.scheduledStartTime.ToString("h:mm tt"), this.scheduledEndTime.ToString("h:mm tt"));
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000370C File Offset: 0x0000270C
		public string Display_OverrideActualTime
		{
			get
			{
				string result;
				if (this.actualTimeIn != DateTime.MinValue && this.actualTimeOut != DateTime.MinValue)
				{
					result = string.Format("{0} to {1}", this.actualTimeIn.ToString("h:mm tt"), this.actualTimeOut.ToString("h:mm tt"));
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003780 File Offset: 0x00002780
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003798 File Offset: 0x00002798
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000037A4 File Offset: 0x000027A4
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000037C1 File Offset: 0x000027C1
		public DateTime ExamDate
		{
			get
			{
				return this.examDate.Date;
			}
			set
			{
				this.examDate = value.Date;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000037D4 File Offset: 0x000027D4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000037EC File Offset: 0x000027EC
		public DateTime DateCreated
		{
			get
			{
				return this.dateCreated;
			}
			set
			{
				this.dateCreated = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000037F8 File Offset: 0x000027F8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003810 File Offset: 0x00002810
		public PersonBaseDTO WhoCreated
		{
			get
			{
				return this.whoCreated;
			}
			set
			{
				this.whoCreated = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000381C File Offset: 0x0000281C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003834 File Offset: 0x00002834
		public PersonBaseDTO Invigilator
		{
			get
			{
				return this.invigilator;
			}
			set
			{
				this.invigilator = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003840 File Offset: 0x00002840
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003858 File Offset: 0x00002858
		public InvigilatorConfirmation InvigilatorConfirmation
		{
			get
			{
				return this.invigilatorConfirmation;
			}
			set
			{
				this.invigilatorConfirmation = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003864 File Offset: 0x00002864
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000387C File Offset: 0x0000287C
		public double RateOfPay
		{
			get
			{
				return this.rateOfPay;
			}
			set
			{
				this.rateOfPay = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003888 File Offset: 0x00002888
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000038A0 File Offset: 0x000028A0
		public PayMethod PaymentMethod
		{
			get
			{
				return this.paymentMethod;
			}
			set
			{
				this.paymentMethod = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000038AC File Offset: 0x000028AC
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000038C4 File Offset: 0x000028C4
		public PersonBaseDTO Room
		{
			get
			{
				return this.room;
			}
			set
			{
				this.room = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000038D0 File Offset: 0x000028D0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000038E8 File Offset: 0x000028E8
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000038F4 File Offset: 0x000028F4
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000390C File Offset: 0x0000290C
		public string PrivateNotes
		{
			get
			{
				return this.privateNotes;
			}
			set
			{
				this.privateNotes = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003918 File Offset: 0x00002918
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003930 File Offset: 0x00002930
		public string InvigilatorNotes
		{
			get
			{
				return this.invigilatorNotes;
			}
			set
			{
				this.invigilatorNotes = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000393C File Offset: 0x0000293C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003954 File Offset: 0x00002954
		public DateTime ScheduledStartTime
		{
			get
			{
				return this.scheduledStartTime;
			}
			set
			{
				this.scheduledStartTime = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003960 File Offset: 0x00002960
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003978 File Offset: 0x00002978
		public DateTime ScheduledEndTime
		{
			get
			{
				return this.scheduledEndTime;
			}
			set
			{
				this.scheduledEndTime = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003984 File Offset: 0x00002984
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000399C File Offset: 0x0000299C
		public DateTime ActualTimeIn
		{
			get
			{
				return this.actualTimeIn;
			}
			set
			{
				this.actualTimeIn = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000039A8 File Offset: 0x000029A8
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000039C0 File Offset: 0x000029C0
		public DateTime ActualTimeOut
		{
			get
			{
				return this.actualTimeOut;
			}
			set
			{
				this.actualTimeOut = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000039CC File Offset: 0x000029CC
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000039E4 File Offset: 0x000029E4
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000039F0 File Offset: 0x000029F0
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003A08 File Offset: 0x00002A08
		public DateTime MinScheduledStartTime
		{
			get
			{
				return this.minScheduledStartTime;
			}
			set
			{
				this.minScheduledStartTime = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003A14 File Offset: 0x00002A14
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003A2C File Offset: 0x00002A2C
		public DateTime MaxScheduledEndTime
		{
			get
			{
				return this.maxScheduledEndTime;
			}
			set
			{
				this.maxScheduledEndTime = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003A38 File Offset: 0x00002A38
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003A50 File Offset: 0x00002A50
		public List<AppointmentDTO> Bookings
		{
			get
			{
				return this.bookings;
			}
			set
			{
				this.bookings = value;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A5C File Offset: 0x00002A5C
		public string ToShortString()
		{
			DateTime d = (this.scheduledStartTime != DateTime.MinValue) ? this.scheduledStartTime : this.minScheduledStartTime;
			DateTime d2 = (this.scheduledEndTime != DateTime.MinValue) ? this.scheduledEndTime : this.maxScheduledEndTime;
			string result;
			if (d == DateTime.MinValue || d2 == DateTime.MinValue)
			{
				result = string.Format("{0}", string.IsNullOrEmpty(this.title) ? this.sittingId.ToString() : string.Format("{0} ({1})", this.title, this.sittingId.ToString()));
			}
			else
			{
				result = string.Format("{0} [{1} to {2}]", string.IsNullOrEmpty(this.title) ? this.sittingId.ToString() : string.Format("{0} ({1})", this.title, this.sittingId.ToString()), d.ToString("MMM d, yyyy . h:mm tt"), d2.ToString("MMM d, yyyy . h:mm tt"));
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003B70 File Offset: 0x00002B70
		private void Update()
		{
			string commandText = "UPDATE examsitting SET title=@title,examdate=@examdate,invigilatorpid=@invigilatorpid\r\n        ,rateofpay=@rateofpay,roompid=@roompid,location=@location,privatenotes=@privatenotes\r\n        ,invigilatornotes=@invigilatornotes,scheduledstarttime=@sst\r\n        ,scheduledendtime=@set,actualtimein=@ati,actualtimeout=@ato \r\nWHERE sittingid=@id";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@title", this.title);
			da.SelectCommand.Parameters.Add("@examdate", this.examDate);
			da.SelectCommand.Parameters.Add("@whoami", 0);
			if (this.invigilator == null)
			{
				da.SelectCommand.Parameters.Add("@invigilatorpid", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@invigilatorpid", this.invigilator.PersonId);
			}
			da.SelectCommand.Parameters.Add("@rateofpay", Convert.ToInt32(this.rateOfPay * 100.0));
			if (this.room == null)
			{
				da.SelectCommand.Parameters.Add("@roompid", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@roompid", this.room.PersonId);
			}
			da.SelectCommand.Parameters.Add("@location", this.location);
			da.SelectCommand.Parameters.Add("@privatenotes", this.privateNotes);
			da.SelectCommand.Parameters.Add("@invigilatornotes", this.invigilatorNotes);
			if (this.scheduledStartTime == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@sst", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@sst", this.scheduledStartTime);
			}
			if (this.scheduledEndTime == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@set", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@set", this.scheduledEndTime);
			}
			if (this.actualTimeIn == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@ati", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@ati", this.actualTimeIn);
			}
			if (this.actualTimeOut == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@ato", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@ato", this.actualTimeOut);
			}
			da.SelectCommand.Parameters.Add("@id", this.sittingId);
			da.Fill(new DataTable());
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003EBC File Offset: 0x00002EBC
		private void Insert()
		{
			string commandText = "INSERT INTO examsitting (title,examdate,datecreated,whocreated,invigilatorpid,invigilatorconfirmed,rateofpay,paymethod,roompid,location,privatenotes,invigilatornotes,scheduledstarttime,scheduledendtime,actualtimein,actualtimeout,cancelled)\r\nVALUES (@title,@examdate,getdate(),@whoami,@invigilatorpid,0,@rateofpay,0,@roompid,@location,@privatenotes,@invigilatornotes,@sst,@set,@ati,@ato,0)\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sittingid";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@title", this.title);
			da.SelectCommand.Parameters.Add("@examdate", this.examDate);
			da.SelectCommand.Parameters.Add("@whoami", 0);
			if (this.invigilator == null)
			{
				da.SelectCommand.Parameters.Add("@invigilatorpid", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@invigilatorpid", this.invigilator.PersonId);
			}
			da.SelectCommand.Parameters.Add("@rateofpay", Convert.ToInt32(this.rateOfPay * 100.0));
			if (this.room == null)
			{
				da.SelectCommand.Parameters.Add("@roompid", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@roompid", this.room.PersonId);
			}
			da.SelectCommand.Parameters.Add("@location", this.location);
			da.SelectCommand.Parameters.Add("@privatenotes", this.privateNotes);
			da.SelectCommand.Parameters.Add("@invigilatornotes", this.invigilatorNotes);
			if (this.scheduledStartTime == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@sst", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@sst", this.scheduledStartTime);
			}
			if (this.scheduledEndTime == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@set", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@set", this.scheduledEndTime);
			}
			if (this.actualTimeIn == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@ati", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@ati", this.actualTimeIn);
			}
			if (this.actualTimeOut == DateTime.MinValue)
			{
				da.SelectCommand.Parameters.Add("@ato", DBNull.Value);
			}
			else
			{
				da.SelectCommand.Parameters.Add("@ato", this.actualTimeOut);
			}
			da.SelectCommand.Parameters.Add("@id", this.sittingId);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				this.sittingId = (int)dataTable.Rows[0][0];
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000424C File Offset: 0x0000324C
		public void SaveToDatabase()
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			if (this.sittingId > 0)
			{
				this.Update();
			}
			else
			{
				this.Insert();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004294 File Offset: 0x00003294
		public ExamSitting()
		{
			this.bookings = new List<AppointmentDTO>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000042AC File Offset: 0x000032AC
		public static ExamSitting LoadSitting(DataTable t)
		{
			return ExamSitting.LoadSitting(t, 0, t.Rows.Count);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000042F8 File Offset: 0x000032F8
		public static ExamSitting LoadSitting(DataTable t, int startIndex, int length)
		{
			ExamSitting examSitting = new ExamSitting();
			bool flag = true;
			List<AppointmentDTO> list = new List<AppointmentDTO>();
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			for (int i = startIndex; i < startIndex + length; i++)
			{
				DataRow dataRow = t.Rows[i];
				if (flag)
				{
					flag = false;
					examSitting.SittingId = ((dataRow["sittingid"] == DBNull.Value) ? 0 : ((int)dataRow["sittingid"]));
					examSitting.Title = dataRow["title"].ToString();
					examSitting.ExamDate = (DateTime)dataRow["examdate"];
					examSitting.DateCreated = ((dataRow["datecreated"] == DBNull.Value) ? DateTime.MinValue : DateTime.MinValue);
					object obj = dataRow["whocreated"];
					if (obj == DBNull.Value)
					{
						examSitting.WhoCreated = null;
					}
					else
					{
						PersonBaseDTO personBaseDTO = new PersonBaseDTO
						{
							PersonId = (int)obj,
							FirstName = "",
							MiddleName = "",
							LastName = "",
							Student_no = "",
							CoreGroup = eCoreGroupDTO.Staff
						};
						examSitting.WhoCreated = personBaseDTO;
					}
					if (dataRow["roompid"] != DBNull.Value)
					{
						int personId = (int)dataRow["roompid"];
						string text = dataRow["room"].ToString();
						PersonBaseDTO personBaseDTO2 = new PersonBaseDTO
						{
							PersonId = personId,
							FirstName = text,
							LastName = text,
							MiddleName = "",
							Student_no = text,
							CoreGroup = eCoreGroupDTO.Rooms
						};
						examSitting.Room = personBaseDTO2;
					}
					if (dataRow["invigilatorpid"] != DBNull.Value)
					{
						int personId2 = (int)dataRow["invigilatorpid"];
						string firstName = dataRow["invigilatorfirstname"].ToString();
						string lastName = dataRow["invigilatorlastname"].ToString();
						PersonBaseDTO personBaseDTO3 = new PersonBaseDTO
						{
							PersonId = personId2,
							FirstName = firstName,
							LastName = lastName,
							MiddleName = "",
							CoreGroup = eCoreGroupDTO.Unknown
						};
						examSitting.Invigilator = personBaseDTO3;
					}
					else
					{
						PersonBaseDTO personBaseDTO4 = new PersonBaseDTO
						{
							PersonId = 0,
							FirstName = "",
							MiddleName = "",
							LastName = "",
							CoreGroup = eCoreGroupDTO.Unknown,
							Groups = new List<GroupDTO>(),
							Tag = new PersonExt()
						};
						examSitting.Invigilator = personBaseDTO4;
					}
					examSitting.InvigilatorConfirmation = (InvigilatorConfirmation)((dataRow["invigilatorconfirmed"] == DBNull.Value) ? 0 : ((int)dataRow["invigilatorconfirmed"]));
					examSitting.ScheduledStartTime = ((dataRow["scheduledstarttime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["scheduledstarttime"]));
					examSitting.ScheduledEndTime = ((dataRow["scheduledendtime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["scheduledendtime"]));
					examSitting.ActualTimeIn = ((dataRow["actualtimein"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["actualtimein"]));
					examSitting.ActualTimeOut = ((dataRow["actualtimeout"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["actualtimeout"]));
					examSitting.Cancelled = (dataRow["cancelled"] != DBNull.Value && Convert.ToBoolean(dataRow["cancelled"]));
					examSitting.Location = dataRow["location"].ToString();
					examSitting.PrivateNotes = dataRow["privatenotes"].ToString();
					examSitting.InvigilatorNotes = dataRow["invigilatornotes"].ToString();
					int value = (dataRow["rateofpay"] == DBNull.Value) ? 0 : ((int)dataRow["rateofpay"]);
					examSitting.RateOfPay = Convert.ToDouble(value) / 100.0;
				}
				if (dataRow["appointmentid"] != DBNull.Value)
				{
					int appId = (int)dataRow["appointmentid"];
					AppointmentDTO appointmentDTO = list.Find((AppointmentDTO f) => f.AppointmentId == appId);
					if (appointmentDTO == null)
					{
						IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
						AppointmentDTO item = appointmentClientManager.LoadAppointment(appId);
						list.Add(item);
						DateTime dateTime3 = (DateTime)dataRow["startdate"];
						DateTime dateTime4 = (DateTime)dataRow["enddate"];
						if (dateTime == null || dateTime3 < dateTime)
						{
							dateTime = new DateTime?(dateTime3);
						}
						if (dateTime2 == null || dateTime4 > dateTime2)
						{
							dateTime2 = new DateTime?(dateTime4);
						}
						int num = (dataRow["lucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["lucourseid"]);
						if (num > 0)
						{
						}
					}
				}
			}
			if (dateTime != null)
			{
				examSitting.MinScheduledStartTime = dateTime.Value;
			}
			else
			{
				examSitting.minScheduledStartTime = DateTime.MinValue;
			}
			if (dateTime2 != null)
			{
				examSitting.MaxScheduledEndTime = dateTime2.Value;
			}
			else
			{
				examSitting.maxScheduledEndTime = DateTime.MinValue;
			}
			examSitting.Bookings = list;
			return examSitting;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000498C File Offset: 0x0000398C
		public static ExamSitting LoadSitting(int sittingId)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "SELECT    es.sittingid,es.title,es.examdate,es.datecreated,es.whocreated,es.invigilatorpid\r\n            ,pi.firstname AS invigilatorfirstname,pi.lastname AS invigilatorlastname,es.invigilatorconfirmed\r\n            ,es.rateofpay,es.paymethod,es.roompid,es.location,es.privatenotes,es.invigilatornotes\r\n            ,es.scheduledstarttime,es.scheduledendtime\r\n            ,es.actualtimein,es.actualtimeout\r\n            ,es.cancelled\r\n            ,app.appointmentid,app.startdate,app.enddate\r\n            ,app.cancelled AS appcancelled,app.apptypeid,at.description,app.personid AS whoadded,app.dateadded,app.groupcode,app.islocked,app.ishidden\r\n            ,ac.lucourseid,lucd.altlookupstring + ' ' + luc.course + ' ' + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n            ,NULL AS personid,NULL AS memotext\r\n            ,proom.firstname AS room\r\nFROM        examsitting es LEFT JOIN appointments app ON app.sittingid=es.sittingid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN people pi ON pi.personid=es.invigilatorpid\r\n            LEFT JOIN people proom ON proom.personid=es.roompid\r\nWHERE       es.sittingid=@sittingid\r\nORDER BY app.startdate";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@sittingid", sittingId);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"room",
				"invigilatorfirstname",
				"invigilatorlastname"
			});
			return ExamSitting.LoadSitting(dataTable);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004A3C File Offset: 0x00003A3C
		public static List<ExamSitting> LoadSittings(DateTime startDate, DateTime endDate)
		{
			List<ExamSitting> list = new List<ExamSitting>();
			string commandText = "IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[examsitting]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)\r\nSELECT    es.sittingid,es.title,es.examdate,es.datecreated,es.whocreated,es.invigilatorpid\r\n            ,pi.firstname AS invigilatorfirstname,pi.lastname AS invigilatorlastname,es.invigilatorconfirmed\r\n            ,es.rateofpay,es.paymethod,es.roompid,es.location,es.privatenotes,es.invigilatornotes\r\n            ,es.scheduledstarttime,es.scheduledendtime\r\n            ,es.actualtimein,es.actualtimeout\r\n            ,es.cancelled\r\n            ,ac.lucourseid,lucd.altlookupstring + ' ' + luc.course + ' ' + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n            ,app.appointmentid,app.startdate,app.enddate\r\n            ,app.cancelled AS appcancelled,app.apptypeid,at.description,app.personid AS whoadded,app.dateadded,app.groupcode,app.islocked,app.ishidden\r\n            ,NULL AS personid,NULL AS memotext\r\n            ,proom.firstname AS room\r\nFROM        examsitting es LEFT JOIN appointments app ON app.sittingid=es.sittingid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN people pi ON pi.personid=es.invigilatorpid\r\n            LEFT JOIN people proom ON proom.personid=es.roompid\r\nWHERE       es.examdate>=@startdate AND es.examdate<=@enddate\r\nORDER BY es.examdate,es.sittingid,app.startdate\r\nELSE\r\n    SELECT 0 AS sittingid WHERE 1=0\r\n";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@startdate", startDate);
			da.SelectCommand.Parameters.Add("@enddate", endDate);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"room",
				"invigilatorfirstname",
				"invigilatorlastname"
			});
			int j;
			for (int i = 0; i < dataTable.Rows.Count; i = j)
			{
				DataRow dataRow = dataTable.Rows[i];
				int num = (int)dataRow["sittingid"];
				for (j = i + 1; j < dataTable.Rows.Count; j++)
				{
					DataRow dataRow2 = dataTable.Rows[j];
					int num2 = (int)dataRow2["sittingid"];
					if (num2 != num)
					{
						break;
					}
				}
				ExamSitting item = ExamSitting.LoadSitting(dataTable, i, j - i);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000032 RID: 50
		private int sittingId;

		// Token: 0x04000033 RID: 51
		private string title;

		// Token: 0x04000034 RID: 52
		private DateTime examDate;

		// Token: 0x04000035 RID: 53
		private DateTime dateCreated;

		// Token: 0x04000036 RID: 54
		private PersonBaseDTO whoCreated;

		// Token: 0x04000037 RID: 55
		private PersonBaseDTO invigilator;

		// Token: 0x04000038 RID: 56
		private InvigilatorConfirmation invigilatorConfirmation;

		// Token: 0x04000039 RID: 57
		private double rateOfPay;

		// Token: 0x0400003A RID: 58
		private PayMethod paymentMethod;

		// Token: 0x0400003B RID: 59
		private PersonBaseDTO room;

		// Token: 0x0400003C RID: 60
		private string location;

		// Token: 0x0400003D RID: 61
		private string privateNotes;

		// Token: 0x0400003E RID: 62
		private string invigilatorNotes;

		// Token: 0x0400003F RID: 63
		private DateTime scheduledStartTime;

		// Token: 0x04000040 RID: 64
		private DateTime scheduledEndTime;

		// Token: 0x04000041 RID: 65
		private DateTime actualTimeIn;

		// Token: 0x04000042 RID: 66
		private DateTime actualTimeOut;

		// Token: 0x04000043 RID: 67
		private bool cancelled;

		// Token: 0x04000044 RID: 68
		private DateTime minScheduledStartTime;

		// Token: 0x04000045 RID: 69
		private DateTime maxScheduledEndTime;

		// Token: 0x04000046 RID: 70
		private List<AppointmentDTO> bookings;
	}
}
