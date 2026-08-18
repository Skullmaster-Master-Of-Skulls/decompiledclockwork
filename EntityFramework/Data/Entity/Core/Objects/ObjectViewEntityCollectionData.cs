using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B3 RID: 1459
	internal sealed class ObjectViewEntityCollectionData<TViewElement, TItemElement> : IObjectViewData<TViewElement> where TViewElement : TItemElement where TItemElement : class
	{
		// Token: 0x06003A6B RID: 14955 RVA: 0x00115DBC File Offset: 0x00113FBC
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

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x00115E38 File Offset: 0x00114038
		public IList<TViewElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x00115E40 File Offset: 0x00114040
		public bool AllowNew
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x00115E50 File Offset: 0x00114050
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x00115E58 File Offset: 0x00114058
		public bool AllowRemove
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x00115E68 File Offset: 0x00114068
		public bool FiresEventOnAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x00115E6B File Offset: 0x0011406B
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x00115E6E File Offset: 0x0011406E
		public bool FiresEventOnClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x00115E71 File Offset: 0x00114071
		public void EnsureCanAddNew()
		{
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x00115E73 File Offset: 0x00114073
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

		// Token: 0x06003A75 RID: 14965 RVA: 0x00115EAC File Offset: 0x001140AC
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

		// Token: 0x06003A76 RID: 14966 RVA: 0x00115F00 File Offset: 0x00114100
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

		// Token: 0x06003A77 RID: 14967 RVA: 0x00115F78 File Offset: 0x00114178
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

		// Token: 0x06003A78 RID: 14968 RVA: 0x00115FB0 File Offset: 0x001141B0
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

		// Token: 0x04001623 RID: 5667
		private readonly List<TViewElement> _bindingList;

		// Token: 0x04001624 RID: 5668
		private readonly EntityCollection<TItemElement> _entityCollection;

		// Token: 0x04001625 RID: 5669
		private readonly bool _canEditItems;

		// Token: 0x04001626 RID: 5670
		private bool _itemCommitPending;
	}
}
