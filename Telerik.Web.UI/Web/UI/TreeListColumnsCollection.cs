using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001201 RID: 4609
	[PersistChildren(false)]
	public class TreeListColumnsCollection : IList, ICollection, IList<TreeListColumn>, ICollection<TreeListColumn>, IEnumerable<TreeListColumn>, IEnumerable, IStateManager
	{
		// Token: 0x17003D6A RID: 15722
		// (get) Token: 0x0600BE5D RID: 48733 RVA: 0x002A2FE0 File Offset: 0x002A11E0
		// (set) Token: 0x0600BE5E RID: 48734 RVA: 0x002A2FE8 File Offset: 0x002A11E8
		public RadTreeList Owner { get; internal set; }

		// Token: 0x0600BE5F RID: 48735 RVA: 0x002A2FF1 File Offset: 0x002A11F1
		public TreeListColumnsCollection(RadTreeList owner)
		{
			this._columns = new List<TreeListColumn>();
			this.Owner = owner;
		}

		// Token: 0x0600BE60 RID: 48736 RVA: 0x002A300B File Offset: 0x002A120B
		internal TreeListColumnsCollection() : this(null)
		{
		}

		// Token: 0x17003D6B RID: 15723
		public TreeListColumn this[int index]
		{
			get
			{
				return ((IList<TreeListColumn>)this)[index];
			}
		}

		// Token: 0x0600BE62 RID: 48738 RVA: 0x002A3020 File Offset: 0x002A1220
		private void InsertInternal(int index, TreeListColumn item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.SetOwner(this.Owner);
			if (this._isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0)
			{
				this._columns.Add(item);
			}
			else
			{
				this._columns.Insert(index, item);
			}
			this.Owner.ClearDefaultInsertValues();
		}

		// Token: 0x0600BE63 RID: 48739 RVA: 0x002A3080 File Offset: 0x002A1280
		private bool RemoveInternal(int index, TreeListColumn item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this._columns.Remove(item);
			}
			else
			{
				this._columns.RemoveAt(index);
				result = true;
			}
			this.Owner.ClearDefaultInsertValues();
			return result;
		}

		// Token: 0x0600BE64 RID: 48740 RVA: 0x002A30CF File Offset: 0x002A12CF
		public int IndexOf(TreeListColumn item)
		{
			return this._columns.IndexOf(item);
		}

		// Token: 0x0600BE65 RID: 48741 RVA: 0x002A30DD File Offset: 0x002A12DD
		public void Insert(int index, TreeListColumn item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x0600BE66 RID: 48742 RVA: 0x002A30E7 File Offset: 0x002A12E7
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x17003D6C RID: 15724
		TreeListColumn IList<TreeListColumn>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this._columns.Count == 0)
				{
					throw new NullReferenceException("Columns collection is empty.");
				}
				return this._columns[index];
			}
			set
			{
				this._columns[index] = value;
			}
		}

		// Token: 0x0600BE69 RID: 48745 RVA: 0x002A3131 File Offset: 0x002A1331
		public void Add(TreeListColumn item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x0600BE6A RID: 48746 RVA: 0x002A313B File Offset: 0x002A133B
		public void Clear()
		{
			this._columns.Clear();
			this.Owner.ClearDefaultInsertValues();
			this.Owner.ClearCustomEditorInitializers();
		}

		// Token: 0x0600BE6B RID: 48747 RVA: 0x002A315E File Offset: 0x002A135E
		public bool Contains(TreeListColumn item)
		{
			return this._columns.Contains(item);
		}

		// Token: 0x0600BE6C RID: 48748 RVA: 0x002A316C File Offset: 0x002A136C
		public void CopyTo(TreeListColumn[] array, int arrayIndex)
		{
			this._columns.CopyTo(array, arrayIndex);
		}

		// Token: 0x17003D6D RID: 15725
		// (get) Token: 0x0600BE6D RID: 48749 RVA: 0x002A317B File Offset: 0x002A137B
		public int Count
		{
			get
			{
				return this._columns.Count;
			}
		}

		// Token: 0x17003D6E RID: 15726
		// (get) Token: 0x0600BE6E RID: 48750 RVA: 0x002A3188 File Offset: 0x002A1388
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600BE6F RID: 48751 RVA: 0x002A318B File Offset: 0x002A138B
		public bool Remove(TreeListColumn item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x0600BE70 RID: 48752 RVA: 0x002A3195 File Offset: 0x002A1395
		public IEnumerator<TreeListColumn> GetEnumerator()
		{
			return this._columns.GetEnumerator();
		}

		// Token: 0x0600BE71 RID: 48753 RVA: 0x002A31A7 File Offset: 0x002A13A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600BE72 RID: 48754 RVA: 0x002A31AF File Offset: 0x002A13AF
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (TreeListColumn)value);
			return this.Count - 1;
		}

		// Token: 0x0600BE73 RID: 48755 RVA: 0x002A31C6 File Offset: 0x002A13C6
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600BE74 RID: 48756 RVA: 0x002A31CE File Offset: 0x002A13CE
		bool IList.Contains(object value)
		{
			return this.Contains((TreeListColumn)value);
		}

		// Token: 0x0600BE75 RID: 48757 RVA: 0x002A31DC File Offset: 0x002A13DC
		int IList.IndexOf(object value)
		{
			return this.IndexOf((TreeListColumn)value);
		}

		// Token: 0x0600BE76 RID: 48758 RVA: 0x002A31EA File Offset: 0x002A13EA
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (TreeListColumn)value);
		}

		// Token: 0x17003D6F RID: 15727
		// (get) Token: 0x0600BE77 RID: 48759 RVA: 0x002A31F9 File Offset: 0x002A13F9
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003D70 RID: 15728
		// (get) Token: 0x0600BE78 RID: 48760 RVA: 0x002A31FC File Offset: 0x002A13FC
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x0600BE79 RID: 48761 RVA: 0x002A3204 File Offset: 0x002A1404
		void IList.Remove(object value)
		{
			this.Remove((TreeListColumn)value);
		}

		// Token: 0x0600BE7A RID: 48762 RVA: 0x002A3213 File Offset: 0x002A1413
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17003D71 RID: 15729
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<TreeListColumn>)this)[index] = (TreeListColumn)value;
			}
		}

		// Token: 0x0600BE7D RID: 48765 RVA: 0x002A3234 File Offset: 0x002A1434
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17003D72 RID: 15730
		// (get) Token: 0x0600BE7E RID: 48766 RVA: 0x002A3264 File Offset: 0x002A1464
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17003D73 RID: 15731
		// (get) Token: 0x0600BE7F RID: 48767 RVA: 0x002A326C File Offset: 0x002A146C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003D74 RID: 15732
		// (get) Token: 0x0600BE80 RID: 48768 RVA: 0x002A326F File Offset: 0x002A146F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17003D75 RID: 15733
		// (get) Token: 0x0600BE81 RID: 48769 RVA: 0x002A3272 File Offset: 0x002A1472
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600BE82 RID: 48770 RVA: 0x002A327C File Offset: 0x002A147C
		void IStateManager.LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				int num2 = (int)((Pair)array[0]).Second;
				int num3 = 0;
				while (num3 < num2 && num3 < array.Length)
				{
					Pair pair = array[num3 + 1] as Pair;
					if (pair != null)
					{
						TreeListColumn treeListColumn = this[num3];
						((IStateManager)treeListColumn).LoadViewState(pair.Second);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					Pair pair2 = array[num4 + 1] as Pair;
					if (pair2 != null)
					{
						object first = pair2.First;
						if (first != null)
						{
							TreeListColumn treeListColumn2 = this.Owner.CreateColumnByType((string)first);
							if (treeListColumn2 != null)
							{
								this.Add(treeListColumn2);
								((IStateManager)treeListColumn2).LoadViewState(pair2.Second);
							}
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x0600BE83 RID: 48771 RVA: 0x002A3364 File Offset: 0x002A1564
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedColumnsCount));
			bool flag = false;
			foreach (TreeListColumn treeListColumn in this)
			{
				arrayList.Add(new Pair
				{
					First = treeListColumn.ColumnType,
					Second = ((IStateManager)treeListColumn).SaveViewState()
				});
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600BE84 RID: 48772 RVA: 0x002A341C File Offset: 0x002A161C
		void IStateManager.TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._notTrackedColumnsCount = this.Count;
			this._isTrackingViewState = true;
			this._columns.ForEach(delegate(TreeListColumn item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x0400320A RID: 12810
		private List<TreeListColumn> _columns;

		// Token: 0x0400320B RID: 12811
		private bool _isTrackingViewState;

		// Token: 0x0400320C RID: 12812
		private bool _isMarked;

		// Token: 0x0400320D RID: 12813
		private int _notTrackedColumnsCount;
	}
}
