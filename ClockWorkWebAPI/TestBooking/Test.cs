using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml.Serialization;
using Databases;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public class Test : IComparable<Test>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00011848 File Offset: 0x0000FA48
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0001186F File Offset: 0x0000FA6F
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00011894 File Offset: 0x0000FA94
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000118A0 File Offset: 0x0000FAA0
		// (set) Token: 0x060002CA RID: 714 RVA: 0x000118B8 File Offset: 0x0000FAB8
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000118C4 File Offset: 0x0000FAC4
		// (set) Token: 0x060002CC RID: 716 RVA: 0x000118DC File Offset: 0x0000FADC
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

		// Token: 0x060002CD RID: 717 RVA: 0x000118E6 File Offset: 0x0000FAE6
		public void ApplyBreakTime()
		{
			this.endDate = this.endDate.AddMinutes((double)this.breakTime);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011904 File Offset: 0x0000FB04
		public void MoveToAnotherDay(int numDaysOffset)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_OverrideRoomPidForAvailability);
			bool flag = settingValue < 1;
			string roomPids;
			if (flag)
			{
				string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Assets);
				List<Asset> availableAssets = Asset.LoadAssets(settingValue2);
				string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Rooms);
				List<Room> list = Room.LoadRooms(settingValue3, availableAssets);
				roomPids = string.Join(",", list.ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
			}
			else
			{
				roomPids = settingValue.ToString();
			}
			int testBookingAvailabilityGroupId = 2;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DateTime date = this.startDate.Date.AddDays((double)numDaysOffset);
			bool flag2 = numDaysOffset < -1;
			if (flag2)
			{
				numDaysOffset = -1;
			}
			else
			{
				bool flag3 = numDaysOffset > 1;
				if (flag3)
				{
					numDaysOffset = 1;
				}
				else
				{
					bool flag4 = numDaysOffset == 0;
					if (flag4)
					{
						numDaysOffset = 1;
					}
				}
			}
			for (int i = 0; i < 15; i++)
			{
				bool flag5 = date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday;
				if (flag5)
				{
					bool flag6 = !Test.IsHoliday(testBookingAvailabilityGroupId, roomPids, date);
					if (flag6)
					{
						this.startDate = new DateTime(date.Year, date.Month, date.Day, this.startDate.Hour, this.startDate.Minute, 0);
					}
					this.endDate = new DateTime(date.Year, date.Month, date.Day, this.endDate.Hour, this.endDate.Minute, 0);
					return;
				}
			}
			date = date.AddDays((double)numDaysOffset);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011ABC File Offset: 0x0000FCBC
		public static bool IsHoliday(int testBookingAvailabilityGroupId, string roomPids, DateTime date)
		{
			string query = "SELECT personid FROM availabilityschedule WHERE availabilitydate=@dt \r\n        AND availabilitygroupid=@gid \r\n        AND \r\n        (\r\n            personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n            --OR roomid2 IN (SELECT orderid AS roomid2 FROM splitorderids(@pids,','))\r\n        )";
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@gid", DbType.Int32, testBookingAvailabilityGroupId),
				clockWork.GetParameter("@pids", DbType.String, roomPids),
				clockWork.GetParameter("@dt", DbType.DateTime, date.Date)
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			return dataTable.Rows.Count < 1;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public Test(DateTime startDate, DateTime endDate, Room room)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.room = room;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011B62 File Offset: 0x0000FD62
		public Test()
		{
			this.startDate = DateTime.MinValue;
			this.endDate = DateTime.MinValue;
			this.room = null;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011B90 File Offset: 0x0000FD90
		public Test(Test test)
		{
			this.startDate = test.startDate;
			this.endDate = test.endDate;
			this.breakTime = test.BreakTime;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011BC8 File Offset: 0x0000FDC8
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

		// Token: 0x060002D4 RID: 724 RVA: 0x00011C58 File Offset: 0x0000FE58
		public bool SameTime(Test test)
		{
			return this.startDate.Equals(test.StartDate) && this.endDate.Equals(test.EndDate);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011C94 File Offset: 0x0000FE94
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

		// Token: 0x060002D6 RID: 726 RVA: 0x00011D54 File Offset: 0x0000FF54
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

		// Token: 0x060002D7 RID: 727 RVA: 0x00011E0C File Offset: 0x0001000C
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00011E84 File Offset: 0x00010084
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00011E9C File Offset: 0x0001009C
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00011EA8 File Offset: 0x000100A8
		public int Duration
		{
			get
			{
				return Convert.ToInt32((this.endDate - this.startDate).TotalMinutes);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00011ED8 File Offset: 0x000100D8
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00011EF0 File Offset: 0x000100F0
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00011EFC File Offset: 0x000100FC
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00011F14 File Offset: 0x00010114
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

		// Token: 0x0400017F RID: 383
		private DateTime startDate;

		// Token: 0x04000180 RID: 384
		private DateTime endDate;

		// Token: 0x04000181 RID: 385
		private Room room;

		// Token: 0x04000182 RID: 386
		private int breakTime = 0;

		// Token: 0x04000183 RID: 387
		private int lucid;

		// Token: 0x04000184 RID: 388
		private string courseDescription;

		// Token: 0x04000185 RID: 389
		private string location;
	}
}
