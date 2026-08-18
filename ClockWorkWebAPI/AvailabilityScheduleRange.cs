using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000F RID: 15
	public class AvailabilityScheduleRange
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000673C File Offset: 0x0000493C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00006754 File Offset: 0x00004954
		public bool IsBookedByLoggedInUser
		{
			get
			{
				return this.isBookedByLoggedInUser;
			}
			set
			{
				this.isBookedByLoggedInUser = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006760 File Offset: 0x00004960
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006778 File Offset: 0x00004978
		public bool Booked
		{
			get
			{
				return this.booked;
			}
			set
			{
				this.booked = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006784 File Offset: 0x00004984
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000067B8 File Offset: 0x000049B8
		public bool IsEmpty
		{
			get
			{
				bool flag = this.end <= this.start;
				if (flag)
				{
					this.isEmpty = true;
				}
				return this.isEmpty;
			}
			set
			{
				this.isEmpty = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000067C4 File Offset: 0x000049C4
		public int AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000067DC File Offset: 0x000049DC
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000067F4 File Offset: 0x000049F4
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000680C File Offset: 0x00004A0C
		public int AvailabilityGroupId
		{
			get
			{
				return this.availabilityGroupId;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006824 File Offset: 0x00004A24
		public AvailabilityScheduleRange(int pid, int availabilityGroupId, DateTime start, DateTime end, bool booked, int rid, int appId)
		{
			this.appId = appId;
			this.rid = rid;
			bool flag = end.Minute == 5 || end.Minute == 35;
			if (flag)
			{
				end = end.AddMinutes(-5.0);
			}
			this.pid = pid;
			this.availabilityGroupId = availabilityGroupId;
			this.start = start;
			this.end = end;
			this.booked = booked;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000068B8 File Offset: 0x00004AB8
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000068D0 File Offset: 0x00004AD0
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000068DC File Offset: 0x00004ADC
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000068F4 File Offset: 0x00004AF4
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006900 File Offset: 0x00004B00
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("pid/rid={0}/{1};start={2};end={3}", new object[]
			{
				this.pid.ToString(),
				this.rid.ToString(),
				this.start.ToString("yy-MM-dd h:mm tt"),
				this.end.ToString("yy-MM-dd h:mm tt")
			});
			return stringBuilder.ToString();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006974 File Offset: 0x00004B74
		public List<AvailabilityScheduleRange> ReduceRange(int appId, DateTime sdate, DateTime edate, bool showBookedApps)
		{
			List<AvailabilityScheduleRange> list = new List<AvailabilityScheduleRange>();
			bool flag = this.start.Year == sdate.Year && this.start.Month == sdate.Month && this.start.Day == sdate.Day;
			bool flag3;
			if (flag)
			{
				bool flag2 = sdate <= this.start && edate >= this.end;
				if (flag2)
				{
					flag3 = true;
				}
				else
				{
					bool flag4 = edate > this.start && edate <= this.end;
					if (flag4)
					{
						flag3 = true;
						bool flag5 = sdate > this.start;
						if (flag5)
						{
							list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, this.start, sdate, false, this.rid, 0));
							list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, edate, this.end, false, this.rid, 0));
						}
						else
						{
							bool flag6 = edate < this.end;
							if (flag6)
							{
								list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, edate, this.end, false, this.rid, 0));
							}
						}
					}
					else
					{
						bool flag7 = sdate >= this.start && sdate < this.end;
						if (flag7)
						{
							flag3 = true;
							bool flag8 = edate <= this.end;
							if (flag8)
							{
								list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, this.start, sdate, false, this.rid, 0));
							}
							else
							{
								list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, this.start, sdate, false, this.rid, 0));
								list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, edate, this.end, false, this.rid, 0));
							}
						}
						else
						{
							flag3 = false;
						}
					}
				}
			}
			else
			{
				flag3 = false;
			}
			bool flag9 = showBookedApps && flag3;
			if (flag9)
			{
				list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, sdate, edate, true, this.rid, appId));
			}
			else
			{
				bool flag10 = !flag3;
				if (flag10)
				{
					list.Add(new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, this.start, this.end, false, this.rid, appId));
				}
			}
			return list;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public int CompareTo(AvailabilityScheduleRange range)
		{
			return this.start.CompareTo(range.Start);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006C1C File Offset: 0x00004E1C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj is AvailabilityScheduleRange;
				if (flag2)
				{
					AvailabilityScheduleRange availabilityScheduleRange = (AvailabilityScheduleRange)obj;
					bool flag3 = availabilityScheduleRange.Pid == this.pid && availabilityScheduleRange.AvailabilityGroupId == this.availabilityGroupId && availabilityScheduleRange.Booked == this.booked && availabilityScheduleRange.IsBookedByLoggedInUser == this.isBookedByLoggedInUser && availabilityScheduleRange.Start == this.start && availabilityScheduleRange.End == this.end;
					result = flag3;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public bool Intersects(AvailabilityScheduleRange range)
		{
			return range.Pid == this.pid && range.AvailabilityGroupId == this.availabilityGroupId && !(this.end <= range.Start) && !(this.start >= range.End);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006D28 File Offset: 0x00004F28
		public void FindAvailableSpots(ref List<AvailabilityScheduleRange> found, int minutesRange)
		{
			DateTime dateTime = this.start;
			DateTime dateTime2 = dateTime.AddMinutes((double)minutesRange);
			while (dateTime2 <= this.end)
			{
				AvailabilityScheduleRange availabilityScheduleRange = new AvailabilityScheduleRange(this.pid, this.availabilityGroupId, dateTime, dateTime2, this.booked, this.rid, this.appId);
				availabilityScheduleRange.IsBookedByLoggedInUser = this.isBookedByLoggedInUser;
				found.Add(availabilityScheduleRange);
				dateTime = dateTime2;
				dateTime2 = dateTime.AddMinutes((double)minutesRange);
			}
		}

		// Token: 0x0400003C RID: 60
		private DateTime start;

		// Token: 0x0400003D RID: 61
		private DateTime end;

		// Token: 0x0400003E RID: 62
		private int pid;

		// Token: 0x0400003F RID: 63
		private int rid;

		// Token: 0x04000040 RID: 64
		private int availabilityGroupId;

		// Token: 0x04000041 RID: 65
		private bool isEmpty = false;

		// Token: 0x04000042 RID: 66
		private bool isBookedByLoggedInUser = false;

		// Token: 0x04000043 RID: 67
		private bool booked = false;

		// Token: 0x04000044 RID: 68
		private int appId = 0;
	}
}
