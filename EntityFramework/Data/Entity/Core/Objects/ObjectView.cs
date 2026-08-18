using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200054B RID: 1355
	internal class ObjectView<TElement> : IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IObjectView
	{
		// Token: 0x06003479 RID: 13433 RVA: 0x000F887F File Offset: 0x000F6A7F
		internal ObjectView(IObjectViewData<TElement> viewData, object eventDataSource)
		{
			this._viewData = viewData;
			this._listener = new ObjectViewListener(this, (IList)this._viewData.List, eventDataSource);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000F88B2 File Offset: 0x000F6AB2
		private void EnsureWritableList()
		{
			if (((IList)this).IsReadOnly)
			{
				throw new InvalidOperationException(Strings.ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000F88C7 File Offset: 0x000F6AC7
		private static bool IsElementTypeAbstract
		{
			get
			{
				return typeof(TElement).IsAbstract();
			}
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000F88D8 File Offset: 0x000F6AD8
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

		// Token: 0x0600347D RID: 13437 RVA: 0x000F8968 File Offset: 0x000F6B68
		void ICancelAddNew.EndNew(int itemIndex)
		{
			if (this._addNewIndex >= 0 && itemIndex == this._addNewIndex)
			{
				this._viewData.CommitItemAt(this._addNewIndex);
				this._addNewIndex = -1;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000F8994 File Offset: 0x000F6B94
		bool IBindingList.AllowNew
		{
			get
			{
				return this._viewData.AllowNew && !ObjectView<TElement>.IsElementTypeAbstract;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000F89AD File Offset: 0x000F6BAD
		bool IBindingList.AllowEdit
		{
			get
			{
				return this._viewData.AllowEdit;
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000F89BC File Offset: 0x000F6BBC
		object IBindingList.AddNew()
		{
			this.EnsureWritableList();
			if (ObjectView<TElement>.IsElementTypeAbstract)
			{
				throw new InvalidOperationException(Strings.ObjectView_AddNewOperationNotAllowedOnAbstractBindingList);
			}
			this._viewData.EnsureCanAddNew();
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			TElement telement = (TElement)((object)Activator.CreateInstance(typeof(TElement)));
			this._addNewIndex = this._viewData.Add(telement, true);
			this._listener.RegisterEntityEvents(telement);
			this.OnListChanged(ListChangedType.ItemAdded, this._addNewIndex, -1);
			return telement;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000F8A45 File Offset: 0x000F6C45
		bool IBindingList.AllowRemove
		{
			get
			{
				return this._viewData.AllowRemove;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000F8A52 File Offset: 0x000F6C52
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000F8A55 File Offset: 0x000F6C55
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000F8A58 File Offset: 0x000F6C58
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000F8A5B File Offset: 0x000F6C5B
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000F8A5E File Offset: 0x000F6C5E
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000F8A65 File Offset: 0x000F6C65
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06003488 RID: 13448 RVA: 0x000F8A6C File Offset: 0x000F6C6C
		// (remove) Token: 0x06003489 RID: 13449 RVA: 0x000F8A85 File Offset: 0x000F6C85
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

		// Token: 0x0600348A RID: 13450 RVA: 0x000F8A9E File Offset: 0x000F6C9E
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000F8AA5 File Offset: 0x000F6CA5
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000F8AAC File Offset: 0x000F6CAC
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000F8AB3 File Offset: 0x000F6CB3
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000F8ABA File Offset: 0x000F6CBA
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170007D1 RID: 2001
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "index")]
		public TElement this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotReplacetheEntityorRow);
			}
		}

		// Token: 0x170007D2 RID: 2002
		object IList.this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotReplacetheEntityorRow);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000F8B04 File Offset: 0x000F6D04
		bool IList.IsReadOnly
		{
			get
			{
				return !this._viewData.AllowNew && !this._viewData.AllowRemove;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000F8B23 File Offset: 0x000F6D23
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000F8B28 File Offset: 0x000F6D28
		int IList.Add(object value)
		{
			Check.NotNull<object>(value, "value");
			this.EnsureWritableList();
			if (!(value is TElement))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
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

		// Token: 0x06003496 RID: 13462 RVA: 0x000F8BA8 File Offset: 0x000F6DA8
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

		// Token: 0x06003497 RID: 13463 RVA: 0x000F8C18 File Offset: 0x000F6E18
		bool IList.Contains(object value)
		{
			return value is TElement && this._viewData.List.Contains((TElement)((object)value));
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000F8C4C File Offset: 0x000F6E4C
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

		// Token: 0x06003499 RID: 13465 RVA: 0x000F8C7D File Offset: 0x000F6E7D
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(Strings.ObjectView_IndexBasedInsertIsNotSupported);
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000F8C8C File Offset: 0x000F6E8C
		void IList.Remove(object value)
		{
			Check.NotNull<object>(value, "value");
			this.EnsureWritableList();
			if (!(value is TElement))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
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

		// Token: 0x0600349B RID: 13467 RVA: 0x000F8D1B File Offset: 0x000F6F1B
		void IList.RemoveAt(int index)
		{
			((IList)this).Remove(((IList)this)[index]);
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000F8D2A File Offset: 0x000F6F2A
		public int Count
		{
			get
			{
				return this._viewData.List.Count;
			}
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000F8D3C File Offset: 0x000F6F3C
		public void CopyTo(Array array, int index)
		{
			((IList)this._viewData.List).CopyTo(array, index);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000F8D55 File Offset: 0x000F6F55
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000F8D58 File Offset: 0x000F6F58
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000F8D5B File Offset: 0x000F6F5B
		public IEnumerator GetEnumerator()
		{
			return this._viewData.List.GetEnumerator();
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000F8D70 File Offset: 0x000F6F70
		private void OnListChanged(ListChangedType listchangedType, int newIndex, int oldIndex)
		{
			ListChangedEventArgs changeArgs = new ListChangedEventArgs(listchangedType, newIndex, oldIndex);
			this.OnListChanged(changeArgs);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000F8D8D File Offset: 0x000F6F8D
		private void OnListChanged(ListChangedEventArgs changeArgs)
		{
			if (this.onListChanged != null && !this._suspendEvent)
			{
				this.onListChanged(this, changeArgs);
			}
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000F8DAC File Offset: 0x000F6FAC
		void IObjectView.EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int num = ((IList)this).IndexOf((TElement)((object)sender));
			this.OnListChanged(ListChangedType.ItemChanged, num, num);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000F8DD4 File Offset: 0x000F6FD4
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

		// Token: 0x040013AC RID: 5036
		private bool _suspendEvent;

		// Token: 0x040013AD RID: 5037
		private ListChangedEventHandler onListChanged;

		// Token: 0x040013AE RID: 5038
		private readonly ObjectViewListener _listener;

		// Token: 0x040013AF RID: 5039
		private int _addNewIndex = -1;

		// Token: 0x040013B0 RID: 5040
		private readonly IObjectViewData<TElement> _viewData;
	}
}
