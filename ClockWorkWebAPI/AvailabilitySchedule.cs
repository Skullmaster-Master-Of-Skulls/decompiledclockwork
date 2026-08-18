using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000E RID: 14
	public class AvailabilitySchedule
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000580C File Offset: 0x00003A0C
		public List<int> AvailabilityGroups
		{
			get
			{
				return this.agids;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005824 File Offset: 0x00003A24
		private string ListToString(List<int> nums)
		{
			string text = "";
			for (int i = 0; i < nums.Count; i++)
			{
				bool flag = i > 0;
				if (flag)
				{
					text += ",";
				}
				text += nums[i].ToString();
			}
			return text;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005884 File Offset: 0x00003A84
		public AvailabilitySchedule(List<int> pids, List<int> availabilityGroupIds, DateTime startDate, DateTime endDate)
		{
			DateTime sdate = Appointments.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Appointments.FixDate(endDate).AddMinutes(1439.0);
			this.snapToMinuteBlocks = true;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			DataTable t = this.LoadAvailability(pids, this.agids, sdate, edate);
			this.FillAvailability(t);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000058F8 File Offset: 0x00003AF8
		public AvailabilitySchedule(List<int> pids, List<int> availabilityGroupIds, db conn, DateTime startDate, DateTime endDate)
		{
			DateTime sdate = Appointments.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Appointments.FixDate(endDate).AddMinutes(1439.0);
			this.snapToMinuteBlocks = true;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			DataTable t = this.LoadAvailability(pids, this.agids, sdate, edate);
			this.FillAvailability(t);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000596C File Offset: 0x00003B6C
		public AvailabilitySchedule(int overridePid, List<int> pids, List<int> availabilityGroupIds, db conn, DateTime startDate, DateTime endDate)
		{
			DateTime sdate = Appointments.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Appointments.FixDate(endDate).AddMinutes(1439.0);
			this.snapToMinuteBlocks = true;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			List<int> list = new List<int>(pids.Count + 1);
			for (int i = 0; i <= pids.Count; i++)
			{
				list.Add(0);
			}
			for (int j = 0; j < pids.Count; j++)
			{
				list[j] = pids[j];
			}
			list[list.Count - 1] = overridePid;
			DataTable dataTable = this.LoadAvailability(list, this.agids, sdate, edate);
			DataTable dataTable2 = dataTable.Clone();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["personid"];
				bool flag = num == overridePid;
				if (flag)
				{
					foreach (int num2 in pids)
					{
						dataTable2.ImportRow(dataRow);
						dataTable2.Rows[dataTable2.Rows.Count - 1]["personid"] = num2;
					}
				}
				else
				{
					dataTable2.ImportRow(dataRow);
				}
			}
			this.FillAvailability(new DataView(dataTable2)
			{
				Sort = "personid,availabilitydate,availabilitygroupid"
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005B64 File Offset: 0x00003D64
		public AvailabilitySchedule(int overridePid, List<int> pids, List<int> availabilityGroupIds, DateTime startDate, DateTime endDate)
		{
			DateTime sdate = Appointments.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Appointments.FixDate(endDate).AddMinutes(1439.0);
			this.snapToMinuteBlocks = true;
			this.pids = pids;
			this.agids = availabilityGroupIds;
			List<int> list = new List<int>(pids.Count + 1);
			for (int i = 0; i <= pids.Count; i++)
			{
				list.Add(0);
			}
			for (int j = 0; j < pids.Count; j++)
			{
				list[j] = pids[j];
			}
			list[list.Count - 1] = overridePid;
			DataTable dataTable = this.LoadAvailability(list, this.agids, sdate, edate);
			DataTable dataTable2 = dataTable.Clone();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["personid"];
				bool flag = num == overridePid;
				if (flag)
				{
					foreach (int num2 in pids)
					{
						dataTable2.ImportRow(dataRow);
						dataTable2.Rows[dataTable2.Rows.Count - 1]["personid"] = num2;
					}
				}
				else
				{
					dataTable2.ImportRow(dataRow);
				}
			}
			this.FillAvailability(new DataView(dataTable2)
			{
				Sort = "personid,availabilitydate,availabilitygroupid"
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005D5C File Offset: 0x00003F5C
		private DataTable LoadAvailability(List<int> pids, List<int> agids, DateTime sdate, DateTime edate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = clockWork.Parameter;
			}
			array[0].ParameterName = "@pids";
			array[0].DbType = DbType.String;
			array[0].Value = this.ListToString(pids);
			array[1].ParameterName = "@agids";
			array[1].DbType = DbType.String;
			array[1].Value = this.ListToString(agids);
			array[2].ParameterName = "@sdate";
			array[2].DbType = DbType.DateTime;
			array[2].Value = sdate;
			array[3].ParameterName = "@edate";
			array[3].DbType = DbType.DateTime;
			array[3].Value = edate;
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_Availability, array);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005E48 File Offset: 0x00004048
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00005E60 File Offset: 0x00004060
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

		// Token: 0x060000B6 RID: 182 RVA: 0x00005E6A File Offset: 0x0000406A
		private void FillAvailability(DataTable t)
		{
			this.FillAvailability(t.DefaultView);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005E7C File Offset: 0x0000407C
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005FE0 File Offset: 0x000041E0
		public List<int> Pids
		{
			get
			{
				return this.pids;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005FF8 File Offset: 0x000041F8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00006010 File Offset: 0x00004210
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

		// Token: 0x060000BB RID: 187 RVA: 0x0000601C File Offset: 0x0000421C
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

		// Token: 0x060000BC RID: 188 RVA: 0x000063B8 File Offset: 0x000045B8
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

		// Token: 0x060000BD RID: 189 RVA: 0x0000648C File Offset: 0x0000468C
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

		// Token: 0x060000BE RID: 190 RVA: 0x000065C8 File Offset: 0x000047C8
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(pid, null, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000065E8 File Offset: 0x000047E8
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, int availabilityGroupId, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(pid, new int[]
			{
				availabilityGroupId
			}, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006610 File Offset: 0x00004810
		public List<AvailabilityScheduleRange> FindAvailableSpots(int pid, int[] availabilityGroupIds, DateTime startLookingDateTime, DateTime endLookingDateTime, int minuteBlocks)
		{
			return this.FindAvailableSpots(new List<int>
			{
				pid
			}, availabilityGroupIds, startLookingDateTime, endLookingDateTime, minuteBlocks);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006640 File Offset: 0x00004840
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

		// Token: 0x04000036 RID: 54
		private const int MINUTE_INTERVAL = 5;

		// Token: 0x04000037 RID: 55
		private const int COUNT = 288;

		// Token: 0x04000038 RID: 56
		private List<AvailabilityScheduleRange> ranges;

		// Token: 0x04000039 RID: 57
		private List<int> pids;

		// Token: 0x0400003A RID: 58
		private List<int> agids;

		// Token: 0x0400003B RID: 59
		private bool snapToMinuteBlocks;
	}
}
