using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Objects
{
	// Token: 0x0200014D RID: 333
	internal class ObjectView<TElement> : IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IObjectView
	{
		// Token: 0x0600185B RID: 6235 RVA: 0x000537F5 File Offset: 0x000519F5
		internal ObjectView(IObjectViewData<TElement> viewData, object eventDataSource)
		{
			this._viewData = viewData;
			this._listener = new ObjectViewListener(this, (IList)this._viewData.List, eventDataSource);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00053828 File Offset: 0x00051A28
		private void EnsureWritableList()
		{
			if (((IList)this).IsReadOnly)
			{
				throw EntityUtil.WriteOperationNotAllowedOnReadOnlyBindingList();
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00053838 File Offset: 0x00051A38
		private bool IsElementTypeAbstract
		{
			get
			{
				return typeof(TElement).IsAbstract;
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005384C File Offset: 0x00051A4C
		void ICancelAddNew.CancelNew(int itemIndex)
		{
			if (this._addNewIndex >= 0 && itemIndex == this._addNewIndex)
			{
				TElement telement = this._viewData.List[this._addNewIndex];
				this._listener.UnregisterEntityEvents(telement);
				int addNewIndex = this._addNewIndex;
				this._addNewIndex = -1;
				try
				{
					this._suspendEvent = true;
					this._viewData.Remove(telement, true);
				}
				finally
				{
					this._suspendEvent = false;
				}
				this.OnListChanged(ListChangedType.ItemDeleted, addNewIndex, -1);
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000538DC File Offset: 0x00051ADC
		void ICancelAddNew.EndNew(int itemIndex)
		{
			if (this._addNewIndex >= 0 && itemIndex == this._addNewIndex)
			{
				this._viewData.CommitItemAt(this._addNewIndex);
				this._addNewIndex = -1;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00053908 File Offset: 0x00051B08
		bool IBindingList.AllowNew
		{
			get
			{
				return this._viewData.AllowNew && !this.IsElementTypeAbstract;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00053922 File Offset: 0x00051B22
		bool IBindingList.AllowEdit
		{
			get
			{
				return this._viewData.AllowEdit;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00053930 File Offset: 0x00051B30
		object IBindingList.AddNew()
		{
			this.EnsureWritableList();
			if (this.IsElementTypeAbstract)
			{
				throw EntityUtil.AddNewOperationNotAllowedOnAbstractBindingList();
			}
			this._viewData.EnsureCanAddNew();
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			TElement telement = (TElement)((object)Activator.CreateInstance(typeof(TElement)));
			this._addNewIndex = this._viewData.Add(telement, true);
			this._listener.RegisterEntityEvents(telement);
			this.OnListChanged(ListChangedType.ItemAdded, this._addNewIndex, -1);
			return telement;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000539B5 File Offset: 0x00051BB5
		bool IBindingList.AllowRemove
		{
			get
			{
				return this._viewData.AllowRemove;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00017938 File Offset: 0x00015B38
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00013A81 File Offset: 0x00011C81
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw EntityUtil.NotSupported();
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00013A81 File Offset: 0x00011C81
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw EntityUtil.NotSupported();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600186A RID: 6250 RVA: 0x000539C2 File Offset: 0x00051BC2
		// (remove) Token: 0x0600186B RID: 6251 RVA: 0x000539DB File Offset: 0x00051BDB
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00013A81 File Offset: 0x00011C81
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00013A81 File Offset: 0x00011C81
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00013A81 File Offset: 0x00011C81
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00013A81 File Offset: 0x00011C81
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00013A81 File Offset: 0x00011C81
		void IBindingList.RemoveSort()
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x170004F0 RID: 1264
		public TElement this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw EntityUtil.CannotReplacetheEntityorRow();
			}
		}

		// Token: 0x170004F1 RID: 1265
		object IList.this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw EntityUtil.CannotReplacetheEntityorRow();
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00053A26 File Offset: 0x00051C26
		bool IList.IsReadOnly
		{
			get
			{
				return !this._viewData.AllowNew && !this._viewData.AllowRemove;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00053A48 File Offset: 0x00051C48
		int IList.Add(object value)
		{
			this.EnsureWritableList();
			EntityUtil.CheckArgumentNull<object>(value, "value");
			if (!(value is TElement))
			{
				throw EntityUtil.IncompatibleArgument();
			}
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			int num = ((IList)this).IndexOf(value);
			if (num == -1)
			{
				num = this._viewData.Add((TElement)((object)value), false);
				if (!this._viewData.FiresEventOnAdd)
				{
					this._listener.RegisterEntityEvents(value);
					this.OnListChanged(ListChangedType.ItemAdded, num, -1);
				}
			}
			return num;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00053AC4 File Offset: 0x00051CC4
		void IList.Clear()
		{
			this.EnsureWritableList();
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			if (this._viewData.FiresEventOnClear)
			{
				this._viewData.Clear();
				return;
			}
			try
			{
				this._suspendEvent = true;
				this._viewData.Clear();
			}
			finally
			{
				this._suspendEvent = false;
			}
			this.OnListChanged(ListChangedType.Reset, -1, -1);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00053B34 File Offset: 0x00051D34
		bool IList.Contains(object value)
		{
			return value is TElement && this._viewData.List.Contains((TElement)((object)value));
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00053B68 File Offset: 0x00051D68
		int IList.IndexOf(object value)
		{
			int result;
			if (value is TElement)
			{
				result = this._viewData.List.IndexOf((TElement)((object)value));
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00053B99 File Offset: 0x00051D99
		void IList.Insert(int index, object value)
		{
			throw EntityUtil.IndexBasedInsertIsNotSupported();
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00053BA0 File Offset: 0x00051DA0
		void IList.Remove(object value)
		{
			this.EnsureWritableList();
			EntityUtil.CheckArgumentNull<object>(value, "value");
			if (!(value is TElement))
			{
				throw EntityUtil.IncompatibleArgument();
			}
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			TElement telement = (TElement)((object)value);
			int newIndex = this._viewData.List.IndexOf(telement);
			bool flag = this._viewData.Remove(telement, false);
			if (flag && !this._viewData.FiresEventOnRemove)
			{
				this._listener.UnregisterEntityEvents(telement);
				this.OnListChanged(ListChangedType.ItemDeleted, newIndex, -1);
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00053C2A File Offset: 0x00051E2A
		void IList.RemoveAt(int index)
		{
			((IList)this).Remove(((IList)this)[index]);
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00053C39 File Offset: 0x00051E39
		public int Count
		{
			get
			{
				return this._viewData.List.Count;
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00053C4B File Offset: 0x00051E4B
		public void CopyTo(Array array, int index)
		{
			((IList)this._viewData.List).CopyTo(array, index);
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00048AC0 File Offset: 0x00046CC0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000173E2 File Offset: 0x000155E2
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00053C64 File Offset: 0x00051E64
		public IEnumerator GetEnumerator()
		{
			return this._viewData.List.GetEnumerator();
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00053C78 File Offset: 0x00051E78
		private void OnListChanged(ListChangedType listchangedType, int newIndex, int oldIndex)
		{
			ListChangedEventArgs changeArgs = new ListChangedEventArgs(listchangedType, newIndex, oldIndex);
			this.OnListChanged(changeArgs);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00053C95 File Offset: 0x00051E95
		private void OnListChanged(ListChangedEventArgs changeArgs)
		{
			if (this.onListChanged != null && !this._suspendEvent)
			{
				this.onListChanged(this, changeArgs);
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00053CB4 File Offset: 0x00051EB4
		void IObjectView.EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int num = ((IList)this).IndexOf((TElement)((object)sender));
			this.OnListChanged(ListChangedType.ItemChanged, num, num);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00053CDC File Offset: 0x00051EDC
		void IObjectView.CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			TElement telement = default(TElement);
			if (this._addNewIndex >= 0)
			{
				telement = this[this._addNewIndex];
			}
			ListChangedEventArgs listChangedEventArgs = this._viewData.OnCollectionChanged(sender, e, this._listener);
			if (this._addNewIndex >= 0)
			{
				if (this._addNewIndex >= this.Count)
				{
					this._addNewIndex = ((IList)this).IndexOf(telement);
				}
				else
				{
					TElement telement2 = this[this._addNewIndex];
					if (!telement2.Equals(telement))
					{
						this._addNewIndex = ((IList)this).IndexOf(telement);
					}
				}
			}
			if (listChangedEventArgs != null)
			{
				this.OnListChanged(listChangedEventArgs);
			}
		}

		// Token: 0x04000AC3 RID: 2755
		private bool _suspendEvent;

		// Token: 0x04000AC4 RID: 2756
		private ListChangedEventHandler onListChanged;

		// Token: 0x04000AC5 RID: 2757
		private ObjectViewListener _listener;

		// Token: 0x04000AC6 RID: 2758
		private int _addNewIndex = -1;

		// Token: 0x04000AC7 RID: 2759
		private IObjectViewData<TElement> _viewData;
	}
}
