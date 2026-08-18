using System;
using System.Collections;

namespace ClockWorkAPI
{
	// Token: 0x0200002C RID: 44
	public class EasyRecurringAppointmentCollection : CollectionBase
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D384 File Offset: 0x0000C384
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000D39C File Offset: 0x0000C39C
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D3B4 File Offset: 0x0000C3B4
		public bool Intersects(DateTime startDate, DateTime endDate)
		{
			DateTime t = new DateTime(startDate.Year, startDate.Month, startDate.Day);
			DateTime t2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);
			return (this.startDate >= t && this.startDate <= t2) || (this.endDate >= t && this.endDate <= t2) || (this.startDate <= t && this.endDate >= t2);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D458 File Offset: 0x0000C458
		public EasyRecurringAppointmentTimes MergeEasyRecurringAppointmentTimes(int groupid, ArrayList dates, out bool[] greyedTimes)
		{
			EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = new EasyRecurringAppointmentTimes(groupid, DateTime.Now);
			greyedTimes = new bool[easyRecurringAppointmentTimes.Times.Length];
			for (int i = 0; i < greyedTimes.Length; i++)
			{
				greyedTimes[i] = false;
			}
			EasyRecurringAppointmentTimes easyRecurringAppointmentTimes2 = null;
			for (int i = 0; i < dates.Count; i++)
			{
				DateTime date = (DateTime)dates[i];
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes3 = this[groupid, date];
				if (easyRecurringAppointmentTimes3 != null && easyRecurringAppointmentTimes3.GroupId == groupid)
				{
					if (easyRecurringAppointmentTimes2 != null)
					{
						for (int j = 0; j < greyedTimes.Length; j++)
						{
							if (!greyedTimes[j])
							{
								if (easyRecurringAppointmentTimes3.Times[j] != easyRecurringAppointmentTimes2.Times[j])
								{
									greyedTimes[j] = true;
								}
							}
						}
					}
					easyRecurringAppointmentTimes2 = easyRecurringAppointmentTimes3;
					for (int j = 0; j < greyedTimes.Length; j++)
					{
						if (easyRecurringAppointmentTimes.Times[j] || easyRecurringAppointmentTimes2.Times[j])
						{
							easyRecurringAppointmentTimes.Times[j] = true;
						}
					}
				}
			}
			return easyRecurringAppointmentTimes;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D598 File Offset: 0x0000C598
		public bool IsEmpty(int groupid, DateTime sdate, DateTime edate)
		{
			foreach (object obj in base.List)
			{
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
				if (easyRecurringAppointmentTimes.GroupId == groupid && easyRecurringAppointmentTimes.Date >= sdate && easyRecurringAppointmentTimes.Date <= edate && easyRecurringAppointmentTimes.IsEmpty())
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D63C File Offset: 0x0000C63C
		public bool IsEmpty(int groupid)
		{
			return this.IsEmpty(groupid, this.startDate, this.EndDate);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D664 File Offset: 0x0000C664
		public bool IsEmpty(int groupid, DateTime date)
		{
			foreach (object obj in base.List)
			{
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
				if (easyRecurringAppointmentTimes.GroupId == groupid && easyRecurringAppointmentTimes.Date.Year == date.Year && easyRecurringAppointmentTimes.Date.Month == date.Month && easyRecurringAppointmentTimes.Date.Day == date.Day)
				{
					return easyRecurringAppointmentTimes.IsEmpty();
				}
			}
			return true;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D734 File Offset: 0x0000C734
		public ArrayList GetAvailableDates(int groupid, DateTime sdate, DateTime edate)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.List)
			{
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
				if (easyRecurringAppointmentTimes.GroupId == groupid && easyRecurringAppointmentTimes.Date >= sdate && easyRecurringAppointmentTimes.Date <= edate && !easyRecurringAppointmentTimes.IsEmpty())
				{
					arrayList.Add(easyRecurringAppointmentTimes.Date);
				}
			}
			return arrayList;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D7EC File Offset: 0x0000C7EC
		public EasyRecurringAppointmentCollection(DateTime startDate, DateTime endDate)
		{
			DateTime dateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day);
			DateTime dateTime2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);
			this.startDate = dateTime;
			this.endDate = dateTime2;
		}

		// Token: 0x170000FC RID: 252
		public EasyRecurringAppointmentTimes this[int index]
		{
			get
			{
				return (EasyRecurringAppointmentTimes)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		public EasyRecurringAppointmentTimes this[int groupid, DateTime date]
		{
			get
			{
				foreach (object obj in base.List)
				{
					EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
					DateTime date2 = easyRecurringAppointmentTimes.Date;
					if (easyRecurringAppointmentTimes.GroupId == groupid && date2.Year == date.Year && date2.Month == date.Month && date2.Day == date.Day)
					{
						return easyRecurringAppointmentTimes;
					}
				}
				return null;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D93C File Offset: 0x0000C93C
		public int Add(EasyRecurringAppointmentTimes easyRecurringAppointmentTimes)
		{
			return base.List.Add(easyRecurringAppointmentTimes);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D95A File Offset: 0x0000C95A
		public void Insert(int index, EasyRecurringAppointmentTimes easyRecurringAppointmentTimes)
		{
			base.List.Insert(index, easyRecurringAppointmentTimes);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D96B File Offset: 0x0000C96B
		public void Remove(EasyRecurringAppointmentTimes easyRecurringAppointmentTimes)
		{
			base.List.Remove(easyRecurringAppointmentTimes);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D97C File Offset: 0x0000C97C
		public bool Contains(EasyRecurringAppointmentTimes easyRecurringAppointmentTimes)
		{
			return base.List.Contains(easyRecurringAppointmentTimes);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000D99C File Offset: 0x0000C99C
		public EasyRecurringAppointmentTimes Find(int groupid, DateTime date)
		{
			foreach (object obj in base.List)
			{
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
				DateTime date2 = easyRecurringAppointmentTimes.Date;
				if (easyRecurringAppointmentTimes.GroupId == groupid && date2.Year == date.Year && date2.Month == date.Month && date2.Day == date.Day)
				{
					return easyRecurringAppointmentTimes;
				}
			}
			return null;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DA58 File Offset: 0x0000CA58
		public override string ToString()
		{
			string text = "";
			foreach (object obj in base.List)
			{
				EasyRecurringAppointmentTimes easyRecurringAppointmentTimes = (EasyRecurringAppointmentTimes)obj;
				if (text.Length > 0)
				{
					text += Environment.NewLine;
				}
				text = text + easyRecurringAppointmentTimes.GroupId.ToString() + " " + easyRecurringAppointmentTimes.ToString();
			}
			return text;
		}

		// Token: 0x0400012F RID: 303
		private DateTime startDate;

		// Token: 0x04000130 RID: 304
		private DateTime endDate;
	}
}
