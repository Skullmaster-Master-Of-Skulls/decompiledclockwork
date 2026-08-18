using System;
using System.Collections;
using System.Data;

namespace ClockWorkAPI
{
	// Token: 0x02000022 RID: 34
	public class EasyRecurringAppointmentTimes
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009038 File Offset: 0x00008038
		public bool SomethingChangedUncommitted
		{
			get
			{
				return this.timesOriginal != null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00009058 File Offset: 0x00008058
		public bool SomethingChangedCommitted
		{
			get
			{
				return this.somethingChangedCommitted;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00009070 File Offset: 0x00008070
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00009088 File Offset: 0x00008088
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000090A0 File Offset: 0x000080A0
		public bool[] Times
		{
			get
			{
				return this.times;
			}
			set
			{
				this.TimeChangedPre();
				this.times = value;
				this.TimeChangedPost();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000090B8 File Offset: 0x000080B8
		public bool CreatedFromDataRow
		{
			get
			{
				return this.createdFromDataRow;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000090D0 File Offset: 0x000080D0
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000090E8 File Offset: 0x000080E8
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000090F4 File Offset: 0x000080F4
		public EasyRecurringAppointmentTimes(int groupid, DateTime date)
		{
			this.GroupId = groupid;
			this.date = date;
			this.times = new bool[288];
			this.timesOriginal = null;
			for (int i = 0; i < 288; i++)
			{
				this.times[i] = false;
			}
			this.createdFromDataRow = false;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009164 File Offset: 0x00008164
		public bool IsEmpty()
		{
			for (int i = 0; i < this.times.Length; i++)
			{
				if (this.times[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000091A1 File Offset: 0x000081A1
		public EasyRecurringAppointmentTimes(DataRow dr)
		{
			this.SetFromDataRow(dr);
			this.createdFromDataRow = true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000091CC File Offset: 0x000081CC
		public void SetFromDataRow(DataRow dr)
		{
			this.groupId = (int)dr["availabilitygroupid"];
			this.date = (DateTime)dr["availabilitydate"];
			byte[] timeBytes = (byte[])dr["availability"];
			this.SetFromBytes(this.groupId, timeBytes);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009228 File Offset: 0x00008228
		public void SetFromBytes(int groupid, byte[] timeBytes)
		{
			this.GroupId = groupid;
			this.times = new bool[288];
			this.timesOriginal = null;
			for (int i = 0; i < 36; i++)
			{
				int num = (int)timeBytes[i];
				for (int j = 0; j < 8; j++)
				{
					this.times[i * 8 + j] = ((num & (int)Math.Pow(2.0, (double)j)) > 0);
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000092A0 File Offset: 0x000082A0
		public byte[] GetBytes()
		{
			byte[] array = new byte[36];
			for (int i = 0; i < 36; i++)
			{
				array[i] = 0;
				for (int j = 0; j < 8; j++)
				{
					if (this.times[i * 8 + j])
					{
						int num = (int)array[i];
						num |= (int)Math.Pow(2.0, (double)j);
						array[i] = (byte)num;
					}
				}
			}
			return array;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009320 File Offset: 0x00008320
		public void AddTime(DateTime stime, DateTime etime, bool val)
		{
			int num = stime.Hour * 60 + stime.Minute;
			int startInd = num / 5;
			int num2 = etime.Hour * 60 + etime.Minute;
			int endInd = num2 / 5;
			this.AddTimeByIndex(startInd, endInd, val);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009368 File Offset: 0x00008368
		private void AddTimeByIndex(int startInd, int endInd, bool val)
		{
			this.TimeChangedPre();
			for (int i = startInd; i <= endInd; i++)
			{
				this.times[i] = val;
			}
			this.TimeChangedPost();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000093A0 File Offset: 0x000083A0
		public void AddTime(int sminutes, int eminutes, bool val)
		{
			int startInd = sminutes / 5;
			int endInd = (eminutes - 5) / 5;
			this.AddTimeByIndex(startInd, endInd, val);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000093C4 File Offset: 0x000083C4
		private void TimeChangedPre()
		{
			if (this.timesOriginal == null)
			{
				this.timesOriginal = new bool[this.times.Length];
				this.times.CopyTo(this.timesOriginal, 0);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000940C File Offset: 0x0000840C
		private void TimeChangedPost()
		{
			if (this.timesOriginal != null)
			{
				bool flag = true;
				for (int i = 0; i < this.times.Length; i++)
				{
					if (this.times[i] != this.timesOriginal[i])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.timesOriginal = null;
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009470 File Offset: 0x00008470
		public void CommitChanges()
		{
			this.TimeChangedPost();
			if (this.timesOriginal != null)
			{
				for (int i = 0; i < this.times.Length; i++)
				{
					this.times[i] = this.timesOriginal[i];
				}
				this.timesOriginal = null;
			}
			this.somethingChangedCommitted = true;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000094CC File Offset: 0x000084CC
		public void ClearTime()
		{
			this.TimeChangedPre();
			for (int i = 0; i < this.times.Length; i++)
			{
				this.times[i] = false;
			}
			this.TimeChangedPost();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00009508 File Offset: 0x00008508
		public int[][] GetTimeRangesInMinutes()
		{
			ArrayList arrayList = new ArrayList();
			int num = -1;
			for (int i = 0; i < this.times.Length; i++)
			{
				bool flag = this.times[i];
				if (!flag && num >= 0)
				{
					arrayList.Add(new int[]
					{
						num * 5,
						i * 5
					});
					num = -1;
				}
				else if (flag && num < 0)
				{
					num = i;
				}
			}
			if (num >= 0)
			{
				arrayList.Add(new int[]
				{
					num * 5,
					1275
				});
			}
			int[][] result;
			if (arrayList.Count > 0)
			{
				DateTime[][] array = new DateTime[arrayList.Count][];
				int[][] array2 = new int[arrayList.Count][];
				for (int i = 0; i < arrayList.Count; i++)
				{
					array2[i] = (int[])arrayList[i];
				}
				arrayList.Clear();
				result = array2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009628 File Offset: 0x00008628
		public bool IsTimeSelected(DateTime time)
		{
			int totalMinutes = time.Hour * 60 + time.Minute;
			return this.IsTimeSelected(totalMinutes);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00009654 File Offset: 0x00008654
		public bool IsTimeSelected(int totalMinutes)
		{
			int num = totalMinutes / 5;
			return this.times[num];
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009674 File Offset: 0x00008674
		public DateTime[][] GetTimeRanges()
		{
			ArrayList arrayList = new ArrayList();
			int num = -1;
			for (int i = 0; i < this.times.Length; i++)
			{
				bool flag = this.times[i];
				if (!flag && num >= 0)
				{
					DateTime dateTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day);
					DateTime dateTime2 = dateTime.AddMinutes((double)(num * 5));
					DateTime dateTime3 = dateTime.AddMinutes((double)(i * 5)).AddMinutes(-5.0);
					arrayList.Add(new DateTime[]
					{
						dateTime2,
						dateTime3
					});
					num = -1;
				}
				else if (flag && num < 0)
				{
					num = i;
				}
			}
			if (num >= 0)
			{
				DateTime dateTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day);
				DateTime dateTime2 = dateTime.AddMinutes((double)(num * 5));
				DateTime dateTime3 = dateTime.AddMinutes((double)((this.times.Length - 1) * 5));
				arrayList.Add(new DateTime[]
				{
					dateTime2,
					dateTime3
				});
			}
			DateTime[][] result;
			if (arrayList.Count > 0)
			{
				DateTime[][] array = new DateTime[arrayList.Count][];
				for (int i = 0; i < arrayList.Count; i++)
				{
					array[i] = (DateTime[])arrayList[i];
				}
				arrayList.Clear();
				result = array;
			}
			else
			{
				result = new DateTime[0][];
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000986C File Offset: 0x0000886C
		public override string ToString()
		{
			DateTime[][] timeRanges = this.GetTimeRanges();
			string result;
			if (timeRanges == null)
			{
				result = "";
			}
			else
			{
				string text = "";
				foreach (DateTime[] array2 in timeRanges)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text = text + array2[0].ToString("hh:mm tt") + " - " + array2[1].ToString("hh:mm tt");
				}
				result = this.Date.ToString("yyyy-MM-dd") + ": " + text;
			}
			return result;
		}

		// Token: 0x040000C7 RID: 199
		private const int MINUTE_INTERVAL = 5;

		// Token: 0x040000C8 RID: 200
		private const int COUNT = 288;

		// Token: 0x040000C9 RID: 201
		private DateTime date;

		// Token: 0x040000CA RID: 202
		private bool[] times;

		// Token: 0x040000CB RID: 203
		private bool[] timesOriginal;

		// Token: 0x040000CC RID: 204
		private bool somethingChangedCommitted = false;

		// Token: 0x040000CD RID: 205
		private bool createdFromDataRow;

		// Token: 0x040000CE RID: 206
		private int groupId = -1;
	}
}
