using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C0 RID: 1216
	public sealed class SelectedDatesCollection : ICollection, IEnumerable
	{
		// Token: 0x06003C98 RID: 15512 RVA: 0x000C463F File Offset: 0x000C283F
		public SelectedDatesCollection(ArrayList dateList)
		{
			this.dateList = dateList;
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000C464E File Offset: 0x000C284E
		public int Count
		{
			get
			{
				return this.dateList.Count;
			}
		}

		// Token: 0x170011B5 RID: 4533
		public DateTime this[int index]
		{
			get
			{
				return (DateTime)this.dateList[index];
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000C4670 File Offset: 0x000C2870
		public void Add(DateTime date)
		{
			int index;
			if (!this.FindIndex(date.Date, out index))
			{
				this.dateList.Insert(index, date.Date);
			}
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000C46A6 File Offset: 0x000C28A6
		public void Clear()
		{
			this.dateList.Clear();
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000C46B4 File Offset: 0x000C28B4
		public bool Contains(DateTime date)
		{
			int num;
			return this.FindIndex(date.Date, out num);
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000C46D0 File Offset: 0x000C28D0
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

		// Token: 0x06003C9F RID: 15519 RVA: 0x000C4725 File Offset: 0x000C2925
		public IEnumerator GetEnumerator()
		{
			return this.dateList.GetEnumerator();
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000C4734 File Offset: 0x000C2934
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000C4764 File Offset: 0x000C2964
		public void Remove(DateTime date)
		{
			int index;
			if (this.FindIndex(date.Date, out index))
			{
				this.dateList.RemoveAt(index);
			}
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000C4790 File Offset: 0x000C2990
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

		// Token: 0x0400238F RID: 9103
		private ArrayList dateList;
	}
}
