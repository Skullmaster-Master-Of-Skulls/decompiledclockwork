using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000537 RID: 1335
	public class AvailabilitySchedule
	{
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x0002BE90 File Offset: 0x0002A090
		public List<int> AvailabilityGroups
		{
			get
			{
				return this.agids;
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AvailabilitySchedule()
		{
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		public AvailabilitySchedule(DataTable availabilityFromDatabase, bool snapToMinuteBlocks, List<int> pids, List<int> availabilityGroupIds)
		{
			this.snapToMinuteBlocks = snapToMinuteBlocks;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			this.FillAvailability(availabilityFromDatabase);
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public AvailabilitySchedule(DataView availabilityFromDatabase, bool snapToMinuteBlocks, List<int> pids, List<int> availabilityGroupIds)
		{
			this.snapToMinuteBlocks = snapToMinuteBlocks;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			this.FillAvailability(availabilityFromDatabase);
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x0002BEF8 File Offset: 0x0002A0F8
		// (set) Token: 0x06002A6A RID: 10858 RVA: 0x0002BF10 File Offset: 0x0002A110
		public List<AvailabilityScheduleRange> Ranges
		{
			get
			{
				return this.ranges;
			}
			set
			{
				this.ranges = value;
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0002BF1A File Offset: 0x0002A11A
		private void FillAvailability(DataTable t)
		{
			this.FillAvailability(t.DefaultView);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0002BF2C File Offset: 0x0002A12C
		private void FillAvailability(DataView dv)
		{
			this.ranges = new List<AvailabilityScheduleRange>();
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				byte[] array = (byte[])row["availability"];
				DateTime date = (DateTime)row["availabilitydate"];
				int pid = (int)row["personid"];
				int availabilityGroupId = (int)row["availabilitygroupid"];
				int rid = (row["roomid"] == DBNull.Value) ? 0 : ((int)row["roomid"]);
				bool[] array2 = new bool[288];
				for (int i = 0; i < 36; i++)
				{
					int num = (int)array[i];
					for (int j = 0; j < 8; j++)
					{
						array2[i * 8 + j] = ((num & (int)Math.Pow(2.0, (double)j)) > 0);
					}
				}
				this.ExtractRanges(ref this.ranges, pid, availabilityGroupId, date, array2, rid);
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x0002C090 File Offset: 0x0002A290
		public List<int> Pids
		{
			get
			{
				return this.pids;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		// (set) Token: 0x06002A6F RID: 10863 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		public bool SnapToMinuteBlocks
		{
			get
			{
				return this.snapToMinuteBlocks;
			}
			set
			{
				this.snapToMinuteBlocks = value;
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0002C0CC File Offset: 0x0002A2CC
		public void ReduceAvailabilityBasedOnBookedAppointments(DataTable appointmentsTable, bool showBookedApps)
		{
			bool flag = appointmentsTable.Columns.Contains("currentuserappid");
			List<AvailabilityScheduleRange> list2;
			foreach (object obj in appointmentsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DateTime sdate = (DateTime)dataRow["startdate"];
				DateTime edate = (DateTime)dataRow["enddate"];
				int pid = (int)dataRow["personid"];
				int appId = (int)dataRow["appointmentid"];
				bool flag2 = flag && dataRow["currentuserappid"] != DBNull.Value;
				List<AvailabilityScheduleRange> list = this.ReduceAvailabilityBasedOnBookedAppointments(pid, appId, sdate, edate, showBookedApps);
				list2 = new List<AvailabilityScheduleRange>();
				foreach (AvailabilityScheduleRange availabilityScheduleRange in this.ranges)
				{
					bool flag3 = !availabilityScheduleRange.Booked;
					if (flag3)
					{
						list2.Add(availabilityScheduleRange);
					}
				}
				foreach (AvailabilityScheduleRange item in list2)
				{
					this.ranges.Remove(item);
				}
				foreach (AvailabilityScheduleRange availabilityScheduleRange2 in list)
				{
					bool flag4 = availabilityScheduleRange2.Booked && flag2;
					if (flag4)
					{
						availabilityScheduleRange2.IsBookedByLoggedInUser = true;
					}
					this.ranges.Add(availabilityScheduleRange2);
				}
			}
			list2 = new List<AvailabilityScheduleRange>();
			foreach (AvailabilityScheduleRange availabilityScheduleRange3 in this.ranges)
			{
				bool flag5 = !list2.Contains(availabilityScheduleRange3);
				if (flag5)
				{
					bool booked = availabilityScheduleRange3.Booked;
					if (booked)
					{
						foreach (AvailabilityScheduleRange availabilityScheduleRange4 in this.ranges)
						{
							bool flag6 = availabilityScheduleRange4 != availabilityScheduleRange3;
							if (flag6)
							{
								bool flag7 = availabilityScheduleRange4.Equals(availabilityScheduleRange3);
								if (flag7)
								{
									list2.Add(availabilityScheduleRange4);
								}
								else
								{
									bool flag8 = !availabilityScheduleRange4.Booked && availabilityScheduleRange4.Intersects(availabilityScheduleRange3);
									if (flag8)
									{
										list2.Add(availabilityScheduleRange4);
									}
								}
							}
						}
					}
				}
			}
			foreach (AvailabilityScheduleRange item2 in list2)
			{
				this.ranges.Remove(item2);
			}
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0002C460 File Offset: 0x0002A660
		public List<AvailabilityScheduleRange> ReduceAvailabilityBasedOnBookedAppointments(int pid, int appId, DateTime sdate, DateTime edate, bool showBookedApps)
		{
			List<AvailabilityScheduleRange> list = this.ranges;
			List<AvailabilityScheduleRange> list2 = new List<AvailabilityScheduleRange>();
			foreach (AvailabilityScheduleRange availabilityScheduleRange in list)
			{
				bool flag = availabilityScheduleRange.Pid == pid;
				if (flag)
				{
					List<AvailabilityScheduleRange> list3 = availabilityScheduleRange.ReduceRange(appId, sdate, edate, showBookedApps);
					foreach (AvailabilityScheduleRange item in list3)
					{
						list2.Add(item);
					}
				}
				else
				{
					list2.Add(availabilityScheduleRange);
				}
			}
			return list2;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0002C530 File Offset: 0x0002A730
		public void ExtractRanges(ref List<AvailabilityScheduleRange> ranges, int pid, int availabilityGroupId, DateTime Date, bool[] times, int rid)
		{
			int num = -1;
			for (int i = 0; i < times.Length; i++)
			{
				bool flag = times[i];
				bool flag2 = !flag && num >= 0;
				if (flag2)
				{
					DateTime dateTime = new DateTime(Date.Year, Date.Month, Date.Day);
					int num2 = num * 5;
					bool flag3 = num2 % 30 == 5;
					if (flag3)
					{
						num2 -= 5;
					}
					DateTime start = dateTime.AddMinutes((double)num2);
					int num3 = i * 5;
					bool flag4 = num3 % 30 == 5;
					if (flag4)
					{
						num3 -= 5;
					}
					DateTime end = dateTime.AddMinutes((double)num3);
					ranges.Add(new AvailabilityScheduleRange(pid, availabilityGroupId, start, end, false, rid, -1));
					num = -1;
				}
				else
				{
					bool flag5 = flag && num < 0;
					if (flag5)
					{
						num = i;
					}
				}
			}
			bool flag6 = num >= 0;
			if (flag6)
			{
				DateTime dateTime2 = new DateTime(Date.Year, Date.Month, Date.Day);
				DateTime start2 = dateTime2.AddMinutes((double)(num * 5));
				DateTime end2 = dateTime2.AddMinutes((double)((times.Length - 1) * 5));
				ranges.Add(new AvailabilityScheduleRange(pid, availabilityGroupId, start2, end2, false, rid, -1));
			}
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0002C66C File Offset: 0x0002A86C
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(pid, null, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0002C68C File Offset: 0x0002A88C
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, int availabilityGroupId, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(pid, new int[]
			{
				availabilityGroupId
			}, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, int[] availabilityGroupIds, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(new List<int>
			{
				pid
			}, availabilityGroupIds, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		public List<AvailabilityScheduleRange> FindAvailableSpots(List<int> pids, int[] availabilityGroupIds, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			this.ranges.Sort((AvailabilityScheduleRange r1, AvailabilityScheduleRange r2) => r1.CompareTo(r2));
			List<AvailabilityScheduleRange> result = new List<AvailabilityScheduleRange>();
			foreach (AvailabilityScheduleRange availabilityScheduleRange in this.ranges)
			{
				bool flag = pids.Contains(availabilityScheduleRange.Pid) && (availabilityGroupIds == null || Array.IndexOf<int>(availabilityGroupIds, availabilityScheduleRange.AvailabilityGroupId) >= 0);
				if (flag)
				{
					bool flag2 = availabilityScheduleRange.End < startLookingDateTime;
					if (!flag2)
					{
						bool flag3 = availabilityScheduleRange.Start > endLookingDateTime;
						if (flag3)
						{
							break;
						}
						availabilityScheduleRange.FindAvailableSpots(ref result, minuteBlocks);
					}
				}
			}
			return result;
		}

		// Token: 0x04001E4D RID: 7757
		private const int MINUTE_INTERVAL = 5;

		// Token: 0x04001E4E RID: 7758
		private const int COUNT = 288;

		// Token: 0x04001E4F RID: 7759
		private List<AvailabilityScheduleRange> ranges;

		// Token: 0x04001E50 RID: 7760
		private List<int> pids;

		// Token: 0x04001E51 RID: 7761
		private List<int> agids;

		// Token: 0x04001E52 RID: 7762
		private bool snapToMinuteBlocks;
	}
}
