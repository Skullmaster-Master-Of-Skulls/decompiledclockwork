using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects
{
	// Token: 0x02000159 RID: 345
	internal sealed class ObjectViewEntityCollectionData<TViewElement, TItemElement> : IObjectViewData<TViewElement> where TViewElement : TItemElement where TItemElement : class
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x00059194 File Offset: 0x00057394
		internal ObjectViewEntityCollectionData(EntityCollection<TItemElement> entityCollection)
		{
			this._entityCollection = entityCollection;
			this._canEditItems = true;
			this._bindingList = new List<TViewElement>(entityCollection.Count);
			foreach (TItemElement titemElement in entityCollection)
			{
				TViewElement item = (TViewElement)((object)titemElement);
				this._bindingList.Add(item);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x00059210 File Offset: 0x00057410
		public IList<TViewElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00059218 File Offset: 0x00057418
		public bool AllowNew
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x00059228 File Offset: 0x00057428
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00059218 File Offset: 0x00057418
		public bool AllowRemove
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x00017938 File Offset: 0x00015B38
		public bool FiresEventOnAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x00017938 File Offset: 0x00015B38
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00017938 File Offset: 0x00015B38
		public bool FiresEventOnClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void EnsureCanAddNew()
		{
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00059230 File Offset: 0x00057430
		public int Add(TViewElement item, bool isAddNew)
		{
			if (isAddNew)
			{
				this._bindingList.Add(item);
			}
			else
			{
				this._entityCollection.Add((TItemElement)((object)item));
			}
			return this._bindingList.Count - 1;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00059268 File Offset: 0x00057468
		public void CommitItemAt(int index)
		{
			TViewElement tviewElement = this._bindingList[index];
			try
			{
				this._itemCommitPending = true;
				this._entityCollection.Add((TItemElement)((object)tviewElement));
			}
			finally
			{
				this._itemCommitPending = false;
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000592BC File Offset: 0x000574BC
		public void Clear()
		{
			if (0 < this._bindingList.Count)
			{
				List<object> list = new List<object>();
				foreach (TViewElement tviewElement in this._bindingList)
				{
					object item = tviewElement;
					list.Add(item);
				}
				this._entityCollection.BulkDeleteAll(list);
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00059334 File Offset: 0x00057534
		public bool Remove(TViewElement item, bool isCancelNew)
		{
			bool result;
			if (isCancelNew)
			{
				result = this._bindingList.Remove(item);
			}
			else
			{
				result = this._entityCollection.RemoveInternal((TItemElement)((object)item));
			}
			return result;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005936C File Offset: 0x0005756C
		public ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener)
		{
			ListChangedEventArgs result = null;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				if (e.Element is TViewElement && !this._itemCommitPending)
				{
					TViewElement tviewElement = (TViewElement)((object)e.Element);
					this._bindingList.Add(tviewElement);
					listener.RegisterEntityEvents(tviewElement);
					result = new ListChangedEventArgs(ListChangedType.ItemAdded, this._bindingList.Count - 1, -1);
				}
				break;
			case CollectionChangeAction.Remove:
				if (e.Element is TViewElement)
				{
					TViewElement tviewElement2 = (TViewElement)((object)e.Element);
					int num = this._bindingList.IndexOf(tviewElement2);
					if (num != -1)
					{
						this._bindingList.Remove(tviewElement2);
						listener.UnregisterEntityEvents(tviewElement2);
						result = new ListChangedEventArgs(ListChangedType.ItemDeleted, num, -1);
					}
				}
				break;
			case CollectionChangeAction.Refresh:
				foreach (TViewElement tviewElement3 in this._bindingList)
				{
					listener.UnregisterEntityEvents(tviewElement3);
				}
				this._bindingList.Clear();
				foreach (object obj in this._entityCollection.GetInternalEnumerable())
				{
					TViewElement tviewElement4 = (TViewElement)((object)obj);
					this._bindingList.Add(tviewElement4);
					listener.RegisterEntityEvents(tviewElement4);
				}
				result = new ListChangedEventArgs(ListChangedType.Reset, -1, -1);
				break;
			}
			return result;
		}

		// Token: 0x04000AE9 RID: 2793
		private List<TViewElement> _bindingList;

		// Token: 0x04000AEA RID: 2794
		private EntityCollection<TItemElement> _entityCollection;

		// Token: 0x04000AEB RID: 2795
		private readonly bool _canEditItems;

		// Token: 0x04000AEC RID: 2796
		private bool _itemCommitPending;
	}
}
