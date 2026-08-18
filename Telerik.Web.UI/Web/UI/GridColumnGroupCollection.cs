using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B76 RID: 2934
	public class GridColumnGroupCollection : IList, ICollection, IList<GridColumnGroup>, ICollection<GridColumnGroup>, IEnumerable<GridColumnGroup>, IEnumerable, IStateManager
	{
		// Token: 0x06006EB4 RID: 28340 RVA: 0x0019B3BB File Offset: 0x001995BB
		public GridColumnGroupCollection()
		{
			this._columnGroups = new List<GridColumnGroup>();
		}

		// Token: 0x06006EB5 RID: 28341 RVA: 0x0019B3CE File Offset: 0x001995CE
		private void InsertInternal(int index, GridColumnGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (index < 0)
			{
				this._columnGroups.Add(group);
			}
			else
			{
				this._columnGroups.Insert(index, group);
			}
			if (this._isTrackingViewState)
			{
				((IStateManager)group).TrackViewState();
			}
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x0019B40C File Offset: 0x0019960C
		private bool RemoveInternal(int index, GridColumnGroup group)
		{
			bool result;
			if (index < 0)
			{
				if (group == null)
				{
					throw new ArgumentNullException("group", "Value cannot be null.");
				}
				result = this._columnGroups.Remove(group);
			}
			else
			{
				this._columnGroups.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x17002451 RID: 9297
		public GridColumnGroup this[int index]
		{
			get
			{
				return ((IList<GridColumnGroup>)this)[index];
			}
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x0019B459 File Offset: 0x00199659
		public int IndexOf(GridColumnGroup item)
		{
			return this._columnGroups.IndexOf(item);
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x0019B467 File Offset: 0x00199667
		public void Insert(int index, GridColumnGroup item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x0019B471 File Offset: 0x00199671
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x17002452 RID: 9298
		GridColumnGroup IList<GridColumnGroup>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this._columnGroups.Count == 0)
				{
					throw new NullReferenceException("Columns collection is empty.");
				}
				return this._columnGroups[index];
			}
			set
			{
				this._columnGroups[index] = value;
			}
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x0019B4BB File Offset: 0x001996BB
		public void Add(GridColumnGroup item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x0019B4C5 File Offset: 0x001996C5
		public void Clear()
		{
			this._columnGroups.Clear();
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x0019B4D2 File Offset: 0x001996D2
		public bool Contains(GridColumnGroup item)
		{
			return this._columnGroups.Contains(item);
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x0019B4E0 File Offset: 0x001996E0
		public void CopyTo(GridColumnGroup[] array, int arrayIndex)
		{
			this._columnGroups.CopyTo(array, arrayIndex);
		}

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x06006EC1 RID: 28353 RVA: 0x0019B4EF File Offset: 0x001996EF
		public int Count
		{
			get
			{
				return this._columnGroups.Count;
			}
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x06006EC2 RID: 28354 RVA: 0x0019B4FC File Offset: 0x001996FC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x0019B4FF File Offset: 0x001996FF
		public bool Remove(GridColumnGroup item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x0019B509 File Offset: 0x00199709
		public IEnumerator<GridColumnGroup> GetEnumerator()
		{
			return this._columnGroups.GetEnumerator();
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x0019B51B File Offset: 0x0019971B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x0019B523 File Offset: 0x00199723
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (GridColumnGroup)value);
			return this.Count - 1;
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x0019B53A File Offset: 0x0019973A
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x0019B542 File Offset: 0x00199742
		bool IList.Contains(object value)
		{
			return this.Contains((GridColumnGroup)value);
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x0019B550 File Offset: 0x00199750
		int IList.IndexOf(object value)
		{
			return this.IndexOf((GridColumnGroup)value);
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0019B55E File Offset: 0x0019975E
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (GridColumnGroup)value);
		}

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x06006ECB RID: 28363 RVA: 0x0019B56D File Offset: 0x0019976D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x06006ECC RID: 28364 RVA: 0x0019B570 File Offset: 0x00199770
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x0019B578 File Offset: 0x00199778
		void IList.Remove(object value)
		{
			this.Remove((GridColumnGroup)value);
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x0019B587 File Offset: 0x00199787
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17002457 RID: 9303
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<GridColumnGroup>)this)[index] = (GridColumnGroup)value;
			}
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x0019B5A8 File Offset: 0x001997A8
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x06006ED2 RID: 28370 RVA: 0x0019B5D8 File Offset: 0x001997D8
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x06006ED3 RID: 28371 RVA: 0x0019B5E0 File Offset: 0x001997E0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x06006ED4 RID: 28372 RVA: 0x0019B5E3 File Offset: 0x001997E3
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x0019B5E8 File Offset: 0x001997E8
		public GridColumnGroup FindGroupByName(string groupName)
		{
			foreach (GridColumnGroup gridColumnGroup in this)
			{
				if (gridColumnGroup.Name == groupName)
				{
					return gridColumnGroup;
				}
			}
			return null;
		}

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x0019B640 File Offset: 0x00199840
		public bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06006ED7 RID: 28375 RVA: 0x0019B648 File Offset: 0x00199848
		public void LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				int num2 = (int)((Pair)array[0]).Second;
				int num3 = 0;
				while (num3 < num && num3 < num2)
				{
					object obj = array[num3 + 1];
					if (obj != null)
					{
						GridColumnGroup gridColumnGroup = this[num3];
						((IStateManager)gridColumnGroup).LoadViewState(obj);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					object obj2 = array[num4 + 1];
					if (obj2 != null)
					{
						GridColumnGroup gridColumnGroup2 = new GridColumnGroup();
						this.Add(gridColumnGroup2);
						((IStateManager)gridColumnGroup2).LoadViewState(obj2);
					}
					num4++;
				}
			}
		}

		// Token: 0x06006ED8 RID: 28376 RVA: 0x0019B6FC File Offset: 0x001998FC
		public object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedGroupsCount));
			bool flag = false;
			int num = 0;
			foreach (GridColumnGroup gridColumnGroup in this)
			{
				arrayList.Add(((IStateManager)gridColumnGroup).SaveViewState());
				num++;
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x0019B7A0 File Offset: 0x001999A0
		public void TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._notTrackedGroupsCount = this.Count;
			this._isTrackingViewState = true;
			this._columnGroups.ForEach(delegate(GridColumnGroup group)
			{
				((IStateManager)group).TrackViewState();
			});
		}

		// Token: 0x04001DE5 RID: 7653
		private List<GridColumnGroup> _columnGroups;

		// Token: 0x04001DE6 RID: 7654
		private bool _isTrackingViewState;

		// Token: 0x04001DE7 RID: 7655
		private bool _isMarked;

		// Token: 0x04001DE8 RID: 7656
		private int _notTrackedGroupsCount;
	}
}
