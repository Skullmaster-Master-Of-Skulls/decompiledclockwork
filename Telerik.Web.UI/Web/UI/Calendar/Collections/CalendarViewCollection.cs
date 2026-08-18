using System;
using System.Collections;
using System.Web.UI;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar.Collections
{
	// Token: 0x02000FE6 RID: 4070
	public class CalendarViewCollection : CollectionBase, IStateManager
	{
		// Token: 0x06009E62 RID: 40546 RVA: 0x00234B76 File Offset: 0x00232D76
		public CalendarViewCollection()
		{
		}

		// Token: 0x06009E63 RID: 40547 RVA: 0x00234B80 File Offset: 0x00232D80
		public CalendarViewCollection(ArrayList dateList)
		{
			for (int i = 0; i < dateList.Count; i++)
			{
				base.List.Add(dateList[i]);
			}
		}

		// Token: 0x06009E64 RID: 40548 RVA: 0x00234BB7 File Offset: 0x00232DB7
		public virtual int Add(CalendarView inputItem)
		{
			return base.List.Add(inputItem);
		}

		// Token: 0x06009E65 RID: 40549 RVA: 0x00234BC8 File Offset: 0x00232DC8
		public virtual int IndexOf(object inputItem)
		{
			if (inputItem is CalendarView)
			{
				return base.List.IndexOf(inputItem);
			}
			if (inputItem is string)
			{
				return -1;
			}
			if (inputItem is int)
			{
				return (int)inputItem;
			}
			throw new ArgumentException("You may use only a CalendarView object, date string or an integer as index in this " + base.GetType().ToString() + " type collection.");
		}

		// Token: 0x06009E66 RID: 40550 RVA: 0x00234C22 File Offset: 0x00232E22
		public virtual void Insert(int insertIndex, CalendarView inputItem)
		{
			base.List.Insert(insertIndex, inputItem);
		}

		// Token: 0x06009E67 RID: 40551 RVA: 0x00234C31 File Offset: 0x00232E31
		public virtual void Remove(CalendarView inputItem)
		{
			base.List.Remove(inputItem);
		}

		// Token: 0x06009E68 RID: 40552 RVA: 0x00234C3F File Offset: 0x00232E3F
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
		}

		// Token: 0x06009E69 RID: 40553 RVA: 0x00234C4D File Offset: 0x00232E4D
		public new void Clear()
		{
			base.List.Clear();
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x00234C5A File Offset: 0x00232E5A
		public virtual bool Contains(CalendarView inputItem)
		{
			return base.List.Contains(inputItem);
		}

		// Token: 0x06009E6B RID: 40555 RVA: 0x00234C68 File Offset: 0x00232E68
		public virtual void CopyTo(CalendarView[] inputArray, int startCopyIndex)
		{
			base.List.CopyTo(inputArray, startCopyIndex);
		}

		// Token: 0x06009E6C RID: 40556 RVA: 0x00234C77 File Offset: 0x00232E77
		public virtual void CopyTo(CalendarView[] inputArray)
		{
			base.List.CopyTo(inputArray, 0);
		}

		// Token: 0x06009E6D RID: 40557 RVA: 0x00234C86 File Offset: 0x00232E86
		public virtual ArrayList CloneInner()
		{
			return (ArrayList)base.InnerList.Clone();
		}

		// Token: 0x06009E6E RID: 40558 RVA: 0x00234C98 File Offset: 0x00232E98
		public virtual CalendarViewCollection Clone()
		{
			CalendarViewCollection calendarViewCollection = new CalendarViewCollection();
			Array.Copy((Array)base.List, (Array)calendarViewCollection.List, base.Count);
			return calendarViewCollection;
		}

		// Token: 0x06009E6F RID: 40559 RVA: 0x00234CCD File Offset: 0x00232ECD
		public virtual void Reverse()
		{
			base.InnerList.Reverse();
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x00234CDC File Offset: 0x00232EDC
		public virtual CalendarView[] ToArray()
		{
			CalendarView[] array = new CalendarView[0];
			return (CalendarView[])base.InnerList.ToArray(array.GetType());
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x00234D08 File Offset: 0x00232F08
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

		// Token: 0x06009E72 RID: 40562 RVA: 0x00234D80 File Offset: 0x00232F80
		public virtual void AddRange(CalendarView[] inputItems)
		{
			if (inputItems == null)
			{
				throw new ArgumentNullException();
			}
			if (inputItems.Length == 0)
			{
				return;
			}
			for (int i = 0; i < inputItems.Length; i++)
			{
				base.List.Add(inputItems[i]);
			}
		}

		// Token: 0x06009E73 RID: 40563 RVA: 0x00234DB9 File Offset: 0x00232FB9
		public virtual void Sort()
		{
			base.InnerList.Sort();
		}

		// Token: 0x06009E74 RID: 40564 RVA: 0x00234DC6 File Offset: 0x00232FC6
		public virtual void Sort(IComparer itemComparer)
		{
			base.InnerList.Sort(itemComparer);
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x00234DD4 File Offset: 0x00232FD4
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

		// Token: 0x1700320C RID: 12812
		public virtual CalendarView this[object obj]
		{
			get
			{
				return (CalendarView)base.List[this.IndexOf(obj)];
			}
			set
			{
				base.List[this.IndexOf(obj)] = value;
			}
		}

		// Token: 0x1700320D RID: 12813
		// (get) Token: 0x06009E78 RID: 40568 RVA: 0x00234E6C File Offset: 0x0023306C
		// (set) Token: 0x06009E79 RID: 40569 RVA: 0x00234E74 File Offset: 0x00233074
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

		// Token: 0x06009E7A RID: 40570 RVA: 0x00234E80 File Offset: 0x00233080
		private object SaveViewState()
		{
			int count = base.List.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				object obj = base.List[i];
				array[i] = ((IStateManager)obj).SaveViewState();
			}
			return array;
		}

		// Token: 0x06009E7B RID: 40571 RVA: 0x00234EC8 File Offset: 0x002330C8
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.LoadViewState(savedState);
			}
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x00234ED4 File Offset: 0x002330D4
		private void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			this.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				Type type = Type.GetType("CalendarView");
				object obj = Activator.CreateInstance(type);
				IStateManager stateManager = obj as IStateManager;
				stateManager.TrackViewState();
				stateManager.LoadViewState(array[i]);
				((IList)this).Add(obj);
			}
		}

		// Token: 0x06009E7D RID: 40573 RVA: 0x00234F2F File Offset: 0x0023312F
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x00234F38 File Offset: 0x00233138
		void IStateManager.TrackViewState()
		{
			this._IsTrackingViewState = true;
			foreach (object obj in base.List)
			{
				IStateManager stateManager = (IStateManager)obj;
				stateManager.TrackViewState();
			}
		}

		// Token: 0x1700320E RID: 12814
		// (get) Token: 0x06009E7F RID: 40575 RVA: 0x00234F98 File Offset: 0x00233198
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._IsTrackingViewState;
			}
		}

		// Token: 0x04002C77 RID: 11383
		private RadCalendar _ParentCalendar;

		// Token: 0x04002C78 RID: 11384
		private bool _IsTrackingViewState;
	}
}
