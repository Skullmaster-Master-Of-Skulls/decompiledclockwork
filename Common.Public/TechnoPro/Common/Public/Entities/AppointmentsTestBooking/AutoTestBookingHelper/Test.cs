using System;
using System.Text;
using System.Xml.Serialization;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200054B RID: 1355
	[Serializable]
	public class Test
	{
		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x0003024C File Offset: 0x0002E44C
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x00030273 File Offset: 0x0002E473
		public string Location
		{
			get
			{
				return (this.location == null) ? "" : this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x00030280 File Offset: 0x0002E480
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x00030298 File Offset: 0x0002E498
		[XmlElement("lucid")]
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
			set
			{
				this.lucid = value;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000302A4 File Offset: 0x0002E4A4
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x000302BC File Offset: 0x0002E4BC
		[XmlElement("coursedescription")]
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

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000302C8 File Offset: 0x0002E4C8
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000302E0 File Offset: 0x0002E4E0
		[XmlElement("breaktime")]
		public int BreakTime
		{
			get
			{
				return this.breakTime;
			}
			set
			{
				this.breakTime = value;
			}
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000302EA File Offset: 0x0002E4EA
		public void ApplyBreakTime()
		{
			this.endDate = this.endDate.AddMinutes((double)this.breakTime);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00030305 File Offset: 0x0002E505
		public Test(DateTime startDate, DateTime endDate, Room room)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.room = room;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0003032B File Offset: 0x0002E52B
		public Test()
		{
			this.startDate = DateTime.MinValue;
			this.endDate = DateTime.MinValue;
			this.room = null;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x00030359 File Offset: 0x0002E559
		public Test(Test test)
		{
			this.startDate = test.startDate;
			this.endDate = test.endDate;
			this.breakTime = test.BreakTime;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x00030390 File Offset: 0x0002E590
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} to {1} [lucid={2}]", this.startDate.ToString("yyyy-MM-dd H:mm"), this.endDate.ToString("H:mm"), this.lucid.ToString());
			bool flag = this.room != null;
			if (flag)
			{
				stringBuilder.AppendFormat("; room={0}.{1}", this.room.RoomId.ToString(), this.room.Title);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00030420 File Offset: 0x0002E620
		public bool SameTime(Test test)
		{
			return this.startDate.Equals(test.StartDate) && this.endDate.Equals(test.EndDate);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0003045C File Offset: 0x0002E65C
		public void ShiftToStartAt(int numDaysDirection, int hour, int minute)
		{
			TimeSpan timeSpan = this.endDate - this.startDate;
			DateTime dateTime = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, hour, minute, 0);
			dateTime = dateTime.AddDays((double)numDaysDirection);
			this.startDate = dateTime;
			this.endDate = this.startDate.AddMinutes(Math.Abs(timeSpan.TotalMinutes));
			bool flag = this.endDate < this.startDate;
			if (flag)
			{
				this.endDate = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, 23, 59, 0);
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0003051C File Offset: 0x0002E71C
		public void ShiftToStartAt(int hour, int minute)
		{
			TimeSpan timeSpan = this.endDate - this.startDate;
			DateTime dateTime = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, hour, minute, 0);
			this.startDate = dateTime;
			this.endDate = this.startDate.AddMinutes(Math.Abs(timeSpan.TotalMinutes));
			bool flag = this.endDate < this.startDate;
			if (flag)
			{
				this.endDate = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, 23, 59, 0);
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000305D4 File Offset: 0x0002E7D4
		public int CompareTo(Test obj)
		{
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = this.startDate != obj.StartDate || this.endDate != obj.EndDate || !Room.RoomsEqual(this.room, obj.Room);
				if (flag2)
				{
					result = this.startDate.CompareTo(obj.StartDate);
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x0003064C File Offset: 0x0002E84C
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x00030664 File Offset: 0x0002E864
		[XmlElement("room")]
		public Room Room
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

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x00030670 File Offset: 0x0002E870
		public int Duration
		{
			get
			{
				return Convert.ToInt32((this.endDate - this.startDate).TotalMinutes);
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000306A0 File Offset: 0x0002E8A0
		// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x000306B8 File Offset: 0x0002E8B8
		[XmlElement("startdate")]
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000306C4 File Offset: 0x0002E8C4
		// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000306DC File Offset: 0x0002E8DC
		[XmlElement("enddate")]
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x04001ED8 RID: 7896
		private DateTime startDate;

		// Token: 0x04001ED9 RID: 7897
		private DateTime endDate;

		// Token: 0x04001EDA RID: 7898
		private Room room;

		// Token: 0x04001EDB RID: 7899
		private int breakTime = 0;

		// Token: 0x04001EDC RID: 7900
		private int lucid;

		// Token: 0x04001EDD RID: 7901
		private string courseDescription;

		// Token: 0x04001EDE RID: 7902
		private string location;
	}
}
