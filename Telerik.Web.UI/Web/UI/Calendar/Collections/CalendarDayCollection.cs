using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Collections
{
	// Token: 0x02000FE3 RID: 4067
	public class CalendarDayCollection : CollectionBase, IStateManager, IClientData
	{
		// Token: 0x06009E31 RID: 40497 RVA: 0x002343EE File Offset: 0x002325EE
		public CalendarDayCollection()
		{
		}

		// Token: 0x06009E32 RID: 40498 RVA: 0x002343F6 File Offset: 0x002325F6
		public CalendarDayCollection(RadCalendar parent)
		{
			this._ParentCalendar = parent;
		}

		// Token: 0x06009E33 RID: 40499 RVA: 0x00234408 File Offset: 0x00232608
		public CalendarDayCollection(ArrayList dateList, RadCalendar parent) : this(parent)
		{
			for (int i = 0; i < dateList.Count; i++)
			{
				int index = base.List.Add(dateList[i]);
				((RadCalendarDay)base.List[index]).ParentCalendar = this._ParentCalendar;
			}
		}

		// Token: 0x06009E34 RID: 40500 RVA: 0x00234460 File Offset: 0x00232660
		internal virtual int AddNew()
		{
			RadCalendarDay radCalendarDay = new RadCalendarDay();
			radCalendarDay.ParentCalendar = this._ParentCalendar;
			return base.List.Add(radCalendarDay);
		}

		// Token: 0x06009E35 RID: 40501 RVA: 0x0023448C File Offset: 0x0023268C
		public virtual int Add(RadCalendarDay inputItem)
		{
			int result = base.List.Add(inputItem);
			inputItem.ParentCalendar = this._ParentCalendar;
			return result;
		}

		// Token: 0x06009E36 RID: 40502 RVA: 0x002344B8 File Offset: 0x002326B8
		public virtual int IndexOf(object inputItem)
		{
			if (inputItem is RadCalendarDay)
			{
				return base.List.IndexOf(inputItem);
			}
			string text = inputItem as string;
			if (text != null)
			{
				DateTime d = DateTime.Parse(text);
				for (int i = 0; i < base.List.Count; i++)
				{
					if (((RadCalendarDay)base.List[i]).Date == d)
					{
						return i;
					}
				}
				return -1;
			}
			if (inputItem is DateTime)
			{
				for (int j = 0; j < base.List.Count; j++)
				{
					if (((RadCalendarDay)base.List[j]).Date == (DateTime)inputItem)
					{
						return j;
					}
				}
				return -1;
			}
			if (inputItem is int)
			{
				return (int)inputItem;
			}
			throw new ArgumentException("You may use only a RadCalendarDay object, date string or an integer as index in this " + base.GetType().ToString() + " type collection.");
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x00234596 File Offset: 0x00232796
		public virtual void Insert(int insertIndex, RadCalendarDay inputItem)
		{
			base.List.Insert(insertIndex, inputItem);
			((RadCalendarDay)base.List[insertIndex]).ParentCalendar = this._ParentCalendar;
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x002345C1 File Offset: 0x002327C1
		public virtual void Remove(RadCalendarDay inputItem)
		{
			base.List.Remove(inputItem);
		}

		// Token: 0x06009E39 RID: 40505 RVA: 0x002345CF File Offset: 0x002327CF
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
		}

		// Token: 0x06009E3A RID: 40506 RVA: 0x002345DD File Offset: 0x002327DD
		public new void Clear()
		{
			base.List.Clear();
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x002345EA File Offset: 0x002327EA
		public virtual bool Contains(RadCalendarDay inputItem)
		{
			return base.List.Contains(inputItem);
		}

		// Token: 0x06009E3C RID: 40508 RVA: 0x002345F8 File Offset: 0x002327F8
		public virtual void CopyTo(RadCalendarDay[] inputArray, int startCopyIndex)
		{
			base.List.CopyTo(inputArray, startCopyIndex);
		}

		// Token: 0x06009E3D RID: 40509 RVA: 0x00234607 File Offset: 0x00232807
		public virtual void CopyTo(RadCalendarDay[] inputArray)
		{
			base.List.CopyTo(inputArray, 0);
		}

		// Token: 0x06009E3E RID: 40510 RVA: 0x00234616 File Offset: 0x00232816
		public virtual ArrayList CloneInner()
		{
			return (ArrayList)base.InnerList.Clone();
		}

		// Token: 0x06009E3F RID: 40511 RVA: 0x00234628 File Offset: 0x00232828
		public virtual CalendarDayCollection Clone()
		{
			CalendarDayCollection calendarDayCollection = new CalendarDayCollection();
			Array.Copy((Array)base.List, (Array)calendarDayCollection.List, base.Count);
			return calendarDayCollection;
		}

		// Token: 0x06009E40 RID: 40512 RVA: 0x0023465D File Offset: 0x0023285D
		public virtual void Reverse()
		{
			base.InnerList.Reverse();
		}

		// Token: 0x06009E41 RID: 40513 RVA: 0x0023466A File Offset: 0x0023286A
		public virtual RadCalendarDay[] ToArray()
		{
			return (RadCalendarDay[])base.InnerList.ToArray(typeof(RadCalendarDay));
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x00234688 File Offset: 0x00232888
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

		// Token: 0x06009E43 RID: 40515 RVA: 0x00234700 File Offset: 0x00232900
		public virtual void AddRange(RadCalendarDay[] inputItems)
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
				int index = base.List.Add(inputItems[i]);
				((RadCalendarDay)base.List[index]).ParentCalendar = this._ParentCalendar;
			}
		}

		// Token: 0x06009E44 RID: 40516 RVA: 0x00234757 File Offset: 0x00232957
		public virtual void Sort()
		{
			this.Sort(new DefaultDateComparer());
		}

		// Token: 0x06009E45 RID: 40517 RVA: 0x00234764 File Offset: 0x00232964
		public virtual void Sort(IComparer itemComparer)
		{
			base.InnerList.Sort(itemComparer);
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x00234774 File Offset: 0x00232974
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

		// Token: 0x17003207 RID: 12807
		public virtual RadCalendarDay this[object obj]
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
				return (RadCalendarDay)base.List[num];
			}
			set
			{
				int index = this.IndexOf(obj);
				value.ParentCalendar = this._ParentCalendar;
				base.List[index] = value;
			}
		}

		// Token: 0x17003208 RID: 12808
		// (get) Token: 0x06009E49 RID: 40521 RVA: 0x00234854 File Offset: 0x00232A54
		// (set) Token: 0x06009E4A RID: 40522 RVA: 0x0023485C File Offset: 0x00232A5C
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

		// Token: 0x06009E4B RID: 40523 RVA: 0x00234868 File Offset: 0x00232A68
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

		// Token: 0x06009E4C RID: 40524 RVA: 0x002348B0 File Offset: 0x00232AB0
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.LoadViewState(savedState);
			}
		}

		// Token: 0x06009E4D RID: 40525 RVA: 0x002348BC File Offset: 0x00232ABC
		private void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			this.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				Type type = Type.GetType("Telerik.Web.UI.RadCalendarDay");
				object obj = Activator.CreateInstance(type);
				IStateManager stateManager = obj as IStateManager;
				stateManager.TrackViewState();
				stateManager.LoadViewState(array[i]);
				((IList)this).Add(obj);
			}
		}

		// Token: 0x06009E4E RID: 40526 RVA: 0x00234917 File Offset: 0x00232B17
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06009E4F RID: 40527 RVA: 0x00234920 File Offset: 0x00232B20
		void IStateManager.TrackViewState()
		{
			this._IsTrackingViewState = true;
			foreach (object obj in base.List)
			{
				IStateManager stateManager = (IStateManager)obj;
				stateManager.TrackViewState();
			}
		}

		// Token: 0x17003209 RID: 12809
		// (get) Token: 0x06009E50 RID: 40528 RVA: 0x00234980 File Offset: 0x00232B80
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._IsTrackingViewState;
			}
		}

		// Token: 0x06009E51 RID: 40529 RVA: 0x00234988 File Offset: 0x00232B88
		ArrayList IClientData.GetClientData()
		{
			return this.GetClientData();
		}

		// Token: 0x06009E52 RID: 40530 RVA: 0x00234990 File Offset: 0x00232B90
		private ArrayList GetClientData()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.List.Count; i++)
			{
				arrayList.Add((IClientData)base.List[i]);
			}
			return arrayList;
		}

		// Token: 0x04002C75 RID: 11381
		private RadCalendar _ParentCalendar;

		// Token: 0x04002C76 RID: 11382
		private bool _IsTrackingViewState;
	}
}
