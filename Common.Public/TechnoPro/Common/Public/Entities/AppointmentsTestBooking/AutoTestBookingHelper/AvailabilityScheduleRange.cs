using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000538 RID: 1336
	public class AvailabilityScheduleRange
	{
		// Token: 0x06002A77 RID: 10871 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AvailabilityScheduleRange()
		{
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
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

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x0002C84C File Offset: 0x0002AA4C
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x0002C864 File Offset: 0x0002AA64
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

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x0002C870 File Offset: 0x0002AA70
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x0002C888 File Offset: 0x0002AA88
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

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x0002C894 File Offset: 0x0002AA94
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
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

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x0002C8D4 File Offset: 0x0002AAD4
		public int AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0002C904 File Offset: 0x0002AB04
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0002C91C File Offset: 0x0002AB1C
		public int AvailabilityGroupId
		{
			get
			{
				return this.availabilityGroupId;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0002C934 File Offset: 0x0002AB34
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0002C94C File Offset: 0x0002AB4C
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

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x0002C958 File Offset: 0x0002AB58
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x0002C970 File Offset: 0x0002AB70
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

		// Token: 0x06002A87 RID: 10887 RVA: 0x0002C97C File Offset: 0x0002AB7C
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

		// Token: 0x06002A88 RID: 10888 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
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

		// Token: 0x06002A89 RID: 10889 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		public int CompareTo(AvailabilityScheduleRange range)
		{
			return this.start.CompareTo(range.Start);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0002CC90 File Offset: 0x0002AE90
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

		// Token: 0x06002A8B RID: 10891 RVA: 0x0002CD38 File Offset: 0x0002AF38
		public bool Intersects(AvailabilityScheduleRange range)
		{
			return range.Pid == this.pid && range.AvailabilityGroupId == this.availabilityGroupId && !(this.end <= range.Start) && !(this.start >= range.End);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
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

		// Token: 0x04001E53 RID: 7763
		private DateTime start;

		// Token: 0x04001E54 RID: 7764
		private DateTime end;

		// Token: 0x04001E55 RID: 7765
		private int pid;

		// Token: 0x04001E56 RID: 7766
		private int rid;

		// Token: 0x04001E57 RID: 7767
		private int availabilityGroupId;

		// Token: 0x04001E58 RID: 7768
		private bool isEmpty;

		// Token: 0x04001E59 RID: 7769
		private bool isBookedByLoggedInUser;

		// Token: 0x04001E5A RID: 7770
		private bool booked;

		// Token: 0x04001E5B RID: 7771
		private int appId;
	}
}
