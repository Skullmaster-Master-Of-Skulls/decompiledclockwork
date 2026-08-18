using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x0200015B RID: 347
	internal sealed class ObjectViewQueryResultData<TElement> : IObjectViewData<TElement>
	{
		// Token: 0x06001994 RID: 6548 RVA: 0x00059948 File Offset: 0x00057B48
		internal ObjectViewQueryResultData(IEnumerable queryResults, ObjectContext objectContext, bool forceReadOnlyList, EntitySet entitySet)
		{
			bool flag = this.IsEditable(typeof(TElement));
			this._objectContext = objectContext;
			this._entitySet = entitySet;
			this._canEditItems = flag;
			this._canModifyList = (!forceReadOnlyList && flag && this._objectContext != null);
			this._bindingList = new List<TElement>();
			foreach (object obj in queryResults)
			{
				TElement item = (TElement)((object)obj);
				this._bindingList.Add(item);
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000599F4 File Offset: 0x00057BF4
		private bool IsEditable(Type elementType)
		{
			return !(elementType == typeof(DbDataRecord)) && (!(elementType != typeof(DbDataRecord)) || !elementType.IsSubclassOf(typeof(DbDataRecord)));
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00059A31 File Offset: 0x00057C31
		private void EnsureEntitySet()
		{
			if (this._entitySet == null)
			{
				throw EntityUtil.CannotResolveTheEntitySetforGivenEntity(typeof(TElement));
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00059A4B File Offset: 0x00057C4B
		public IList<TElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00059A53 File Offset: 0x00057C53
		public bool AllowNew
		{
			get
			{
				return this._canModifyList && this._entitySet != null;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00059A68 File Offset: 0x00057C68
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00059A70 File Offset: 0x00057C70
		public bool AllowRemove
		{
			get
			{
				return this._canModifyList;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool FiresEventOnAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00017938 File Offset: 0x00015B38
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool FiresEventOnClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00059A78 File Offset: 0x00057C78
		public void EnsureCanAddNew()
		{
			this.EnsureEntitySet();
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00059A80 File Offset: 0x00057C80
		public int Add(TElement item, bool isAddNew)
		{
			this.EnsureEntitySet();
			if (!isAddNew)
			{
				this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet), item);
			}
			this._bindingList.Add(item);
			return this._bindingList.Count - 1;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00059AC0 File Offset: 0x00057CC0
		public void CommitItemAt(int index)
		{
			this.EnsureEntitySet();
			TElement telement = this._bindingList[index];
			this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet), telement);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00059AFC File Offset: 0x00057CFC
		public void Clear()
		{
			while (0 < this._bindingList.Count)
			{
				TElement item = this._bindingList[this._bindingList.Count - 1];
				this.Remove(item, false);
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00059B3C File Offset: 0x00057D3C
		public bool Remove(TElement item, bool isCancelNew)
		{
			bool result;
			if (isCancelNew)
			{
				result = this._bindingList.Remove(item);
			}
			else
			{
				EntityEntry entityEntry = this._objectContext.ObjectStateManager.FindEntityEntry(item);
				if (entityEntry != null)
				{
					entityEntry.Delete();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00059B84 File Offset: 0x00057D84
		public ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener)
		{
			ListChangedEventArgs result = null;
			if (e.Element.GetType().IsAssignableFrom(typeof(TElement)) && this._bindingList.Contains((TElement)((object)e.Element)))
			{
				TElement telement = (TElement)((object)e.Element);
				int num = this._bindingList.IndexOf(telement);
				if (num >= 0 && e.Action == CollectionChangeAction.Remove)
				{
					this._bindingList.Remove(telement);
					listener.UnregisterEntityEvents(telement);
					result = new ListChangedEventArgs(ListChangedType.ItemDeleted, num, -1);
				}
			}
			return result;
		}

		// Token: 0x04000AF1 RID: 2801
		private List<TElement> _bindingList;

		// Token: 0x04000AF2 RID: 2802
		private ObjectContext _objectContext;

		// Token: 0x04000AF3 RID: 2803
		private EntitySet _entitySet;

		// Token: 0x04000AF4 RID: 2804
		private bool _canEditItems;

		// Token: 0x04000AF5 RID: 2805
		private bool _canModifyList;
	}
}
