using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000638 RID: 1592
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SelectedDatesCollection : ICollection, IEnumerable
	{
		// Token: 0x06004EA4 RID: 20132 RVA: 0x0013E015 File Offset: 0x0013D015
		public SelectedDatesCollection(ArrayList dateList)
		{
			this.dateList = dateList;
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06004EA5 RID: 20133 RVA: 0x0013E024 File Offset: 0x0013D024
		public int Count
		{
			get
			{
				return this.dateList.Count;
			}
		}

		// Token: 0x170013E4 RID: 5092
		public DateTime this[int index]
		{
			get
			{
				return (DateTime)this.dateList[index];
			}
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0013E044 File Offset: 0x0013D044
		public void Add(DateTime date)
		{
			int index;
			if (!this.FindIndex(date.Date, out index))
			{
				this.dateList.Insert(index, date.Date);
			}
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0013E07A File Offset: 0x0013D07A
		public void Clear()
		{
			this.dateList.Clear();
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0013E088 File Offset: 0x0013D088
		public bool Contains(DateTime date)
		{
			int num;
			return this.FindIndex(date.Date, out num);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0013E0A4 File Offset: 0x0013D0A4
		private bool FindIndex(DateTime date, out int index)
		{
			int count = this.Count;
			int i = 0;
			int num = count;
			while (i < num)
			{
				index = (i + num) / 2;
				if (date == this[index])
				{
					return true;
				}
				if (date < this[index])
				{
					num = index;
				}
				else
				{
					i = index + 1;
				}
			}
			index = i;
			return false;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0013E0F9 File Offset: 0x0013D0F9
		public IEnumerator GetEnumerator()
		{
			return this.dateList.GetEnumerator();
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0013E108 File Offset: 0x0013D108
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x0013E138 File Offset: 0x0013D138
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0013E13B File Offset: 0x0013D13B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x0013E13E File Offset: 0x0013D13E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0013E144 File Offset: 0x0013D144
		public void Remove(DateTime date)
		{
			int index;
			if (this.FindIndex(date.Date, out index))
			{
				this.dateList.RemoveAt(index);
			}
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0013E170 File Offset: 0x0013D170
		public void SelectRange(DateTime fromDate, DateTime toDate)
		{
			this.dateList.Clear();
			if (fromDate <= toDate)
			{
				this.dateList.Add(fromDate);
				DateTime dateTime = fromDate;
				while (dateTime < toDate)
				{
					dateTime = dateTime.AddDays(1.0);
					this.dateList.Add(dateTime);
				}
			}
		}

		// Token: 0x04002CAD RID: 11437
		private ArrayList dateList;
	}
}
