using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Collections
{
	// Token: 0x02000FE7 RID: 4071
	public class DateTimeCollection : CollectionBase, IStateManager, IClientData
	{
		// Token: 0x06009E80 RID: 40576 RVA: 0x00234FA0 File Offset: 0x002331A0
		public DateTimeCollection(ArrayList dateList)
		{
			base.InnerList.AddRange(dateList);
		}

		// Token: 0x06009E81 RID: 40577 RVA: 0x00234FB4 File Offset: 0x002331B4
		public DateTimeCollection()
		{
		}

		// Token: 0x06009E82 RID: 40578 RVA: 0x00234FBC File Offset: 0x002331BC
		internal virtual int AddNew()
		{
			RadDate value = new RadDate();
			return base.List.Add(value);
		}

		// Token: 0x06009E83 RID: 40579 RVA: 0x00234FDB File Offset: 0x002331DB
		private DateTime TruncateTimeComponent(DateTime value)
		{
			return value.Subtract(value.TimeOfDay);
		}

		// Token: 0x06009E84 RID: 40580 RVA: 0x00234FEC File Offset: 0x002331EC
		public virtual int Add(RadDate inputItem)
		{
			inputItem.Date = this.TruncateTimeComponent(inputItem.Date);
			return base.List.Add(inputItem);
		}

		// Token: 0x06009E85 RID: 40581 RVA: 0x0023501C File Offset: 0x0023321C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual int IndexOf(object inputItem)
		{
			DateTime dateTime = DateTime.MinValue;
			if (inputItem is int)
			{
				return (int)inputItem;
			}
			if (inputItem is RadDate)
			{
				dateTime = ((RadDate)inputItem).Date;
			}
			else if (inputItem is string)
			{
				dateTime = DateTime.Parse((string)inputItem);
			}
			else if (inputItem is DateTime)
			{
				dateTime = (DateTime)inputItem;
			}
			if (dateTime != DateTime.MinValue)
			{
				dateTime = this.TruncateTimeComponent(dateTime);
				for (int i = 0; i < base.List.Count; i++)
				{
					if (((RadDate)base.List[i]).Date == dateTime)
					{
						return i;
					}
				}
				return -1;
			}
			throw new ArgumentException("You may use only a DateTime object, date string or an integer as index in this " + base.GetType().ToString() + " type collection.");
		}

		// Token: 0x06009E86 RID: 40582 RVA: 0x002350E5 File Offset: 0x002332E5
		public virtual void Insert(int insertIndex, RadDate inputItem)
		{
			inputItem.Date = this.TruncateTimeComponent(inputItem.Date);
			base.List.Insert(insertIndex, inputItem);
		}

		// Token: 0x06009E87 RID: 40583 RVA: 0x00235106 File Offset: 0x00233306
		public virtual void Remove(RadDate inputItem)
		{
			inputItem.Date = this.TruncateTimeComponent(inputItem.Date);
			base.List.Remove(inputItem);
		}

		// Token: 0x06009E88 RID: 40584 RVA: 0x00235126 File Offset: 0x00233326
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
		}

		// Token: 0x06009E89 RID: 40585 RVA: 0x00235134 File Offset: 0x00233334
		public new void Clear()
		{
			base.List.Clear();
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x00235144 File Offset: 0x00233344
		public virtual bool Contains(RadDate inputItem)
		{
			DateTime d = this.TruncateTimeComponent(inputItem.Date);
			for (int i = 0; i < base.List.Count; i++)
			{
				if (((RadDate)base.List[i]).Date == d)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009E8B RID: 40587 RVA: 0x00235198 File Offset: 0x00233398
		public virtual void CopyTo(DateTime[] inputArray, int startCopyIndex)
		{
			RadDate[] array = new RadDate[inputArray.Length];
			base.List.CopyTo(array, startCopyIndex);
			for (int i = startCopyIndex; i < array.Length; i++)
			{
				inputArray[i] = array[i].Date;
			}
		}

		// Token: 0x06009E8C RID: 40588 RVA: 0x002351E0 File Offset: 0x002333E0
		public virtual void CopyTo(DateTime[] inputArray)
		{
			RadDate[] array = new RadDate[inputArray.Length];
			base.List.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					inputArray[i] = array[i].Date;
				}
			}
		}

		// Token: 0x06009E8D RID: 40589 RVA: 0x0023522C File Offset: 0x0023342C
		public virtual void CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(((RadDate)obj).Date, index++);
			}
		}

		// Token: 0x06009E8E RID: 40590 RVA: 0x0023526B File Offset: 0x0023346B
		public virtual ArrayList CloneInner()
		{
			return (ArrayList)base.InnerList.Clone();
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x00235280 File Offset: 0x00233480
		public virtual DateTimeCollection Clone()
		{
			DateTimeCollection dateTimeCollection = new DateTimeCollection();
			Array.Copy((Array)base.List, (Array)dateTimeCollection.List, base.Count);
			return dateTimeCollection;
		}

		// Token: 0x06009E90 RID: 40592 RVA: 0x002352B5 File Offset: 0x002334B5
		public virtual void Reverse()
		{
			base.InnerList.Reverse();
		}

		// Token: 0x06009E91 RID: 40593 RVA: 0x002352C4 File Offset: 0x002334C4
		public virtual DateTime[] ToArray()
		{
			RadDate[] array = (RadDate[])base.InnerList.ToArray(typeof(RadDate));
			DateTime[] array2 = new DateTime[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].Date;
			}
			return array2;
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x00235318 File Offset: 0x00233518
		public void SelectRange(DateTime fromDate, DateTime toDate)
		{
			fromDate = this.TruncateTimeComponent(fromDate);
			toDate = this.TruncateTimeComponent(toDate);
			base.List.Clear();
			if (fromDate <= toDate)
			{
				DateTime dateTime = fromDate.Date;
				while (dateTime <= toDate.Date)
				{
					base.List.Add(new RadDate(dateTime));
					dateTime = dateTime.AddDays(1.0);
				}
			}
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x00235388 File Offset: 0x00233588
		public virtual void RemoveRange(int startIndex, int itemCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("index", startIndex, "Argument cannot be negative.");
			}
			if (itemCount < 0)
			{
				throw new ArgumentOutOfRangeException("count", itemCount, "Argument cannot be negative.");
			}
			if (startIndex + itemCount > base.List.Count)
			{
				throw new ArgumentException("Arguments denote invalid range of elements.");
			}
			if (itemCount == 0)
			{
				return;
			}
			for (int i = startIndex; i < itemCount; i++)
			{
				base.List.RemoveAt(startIndex);
			}
		}

		// Token: 0x06009E94 RID: 40596 RVA: 0x00235400 File Offset: 0x00233600
		public virtual void AddRange(RadDate[] inputItems)
		{
			if (inputItems == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < inputItems.Length; i++)
			{
				inputItems[i].Date = this.TruncateTimeComponent(inputItems[i].Date);
				base.List.Add(inputItems[i]);
			}
		}

		// Token: 0x06009E95 RID: 40597 RVA: 0x00235449 File Offset: 0x00233649
		public virtual void Sort()
		{
			base.InnerList.Sort();
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x00235456 File Offset: 0x00233656
		public virtual void Sort(IComparer itemComparer)
		{
			base.InnerList.Sort(itemComparer);
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x00235464 File Offset: 0x00233664
		public virtual void Sort(int startIndex, int itemCount, IComparer itemComparer)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("index", startIndex, "Argument cannot be negative.");
			}
			if (itemCount < 0)
			{
				throw new ArgumentOutOfRangeException("count", itemCount, "Argument cannot be negative.");
			}
			if (startIndex + itemCount > base.Count)
			{
				throw new ArgumentException("Arguments denote invalid range of elements.");
			}
			if (itemCount <= 1)
			{
				return;
			}
			base.InnerList.Sort(startIndex, itemCount, itemComparer);
		}

		// Token: 0x1700320F RID: 12815
		public virtual RadDate this[object obj]
		{
			get
			{
				string text = obj as string;
				if (obj == null || (text != null && string.IsNullOrEmpty(text)))
				{
					return null;
				}
				int num = this.IndexOf(obj);
				if (num < 0)
				{
					return null;
				}
				return (RadDate)base.List[num];
			}
			set
			{
				int index = this.IndexOf(obj);
				base.List[index] = value;
			}
		}

		// Token: 0x17003210 RID: 12816
		// (get) Token: 0x06009E9A RID: 40602 RVA: 0x00235536 File Offset: 0x00233736
		// (set) Token: 0x06009E9B RID: 40603 RVA: 0x0023553E File Offset: 0x0023373E
		public RadCalendar ParentCalendar
		{
			get
			{
				return this._ParentCalendar;
			}
			set
			{
				this._ParentCalendar = value;
			}
		}

		// Token: 0x06009E9C RID: 40604 RVA: 0x00235548 File Offset: 0x00233748
		private object SaveViewState()
		{
			int count = base.List.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				object obj = base.List[i];
				array[i] = ((RadDate)obj).Date;
			}
			return array;
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x00235595 File Offset: 0x00233795
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.LoadViewState(savedState);
			}
		}

		// Token: 0x06009E9E RID: 40606 RVA: 0x002355A4 File Offset: 0x002337A4
		private void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			this.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				RadDate value = new RadDate((DateTime)array[i]);
				((IList)this).Add(value);
			}
		}

		// Token: 0x06009E9F RID: 40607 RVA: 0x002355E2 File Offset: 0x002337E2
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x002355EC File Offset: 0x002337EC
		void IStateManager.TrackViewState()
		{
			this._IsTrackingViewState = true;
			foreach (object obj in base.List)
			{
				IStateManager stateManager = (IStateManager)obj;
				stateManager.TrackViewState();
			}
		}

		// Token: 0x17003211 RID: 12817
		// (get) Token: 0x06009EA1 RID: 40609 RVA: 0x0023564C File Offset: 0x0023384C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._IsTrackingViewState;
			}
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x00235654 File Offset: 0x00233854
		ArrayList IClientData.GetClientData()
		{
			return this.GetClientData();
		}

		// Token: 0x06009EA3 RID: 40611 RVA: 0x0023565C File Offset: 0x0023385C
		private ArrayList GetClientData()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.List.Count; i++)
			{
				arrayList.Add(((RadDate)base.List[i]).Date);
			}
			return arrayList;
		}

		// Token: 0x04002C79 RID: 11385
		private RadCalendar _ParentCalendar;

		// Token: 0x04002C7A RID: 11386
		private bool _IsTrackingViewState;
	}
}
