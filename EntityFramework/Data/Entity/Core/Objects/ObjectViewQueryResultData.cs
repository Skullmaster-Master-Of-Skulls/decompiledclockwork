using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B6 RID: 1462
	internal sealed class ObjectViewQueryResultData<TElement> : IObjectViewData<TElement>
	{
		// Token: 0x06003A8A RID: 14986 RVA: 0x00116828 File Offset: 0x00114A28
		internal ObjectViewQueryResultData(IEnumerable queryResults, ObjectContext objectContext, bool forceReadOnlyList, EntitySet entitySet)
		{
			bool flag = ObjectViewQueryResultData<TElement>.IsEditable(typeof(TElement));
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

		// Token: 0x06003A8B RID: 14987 RVA: 0x001168D4 File Offset: 0x00114AD4
		private static bool IsEditable(Type elementType)
		{
			return !(elementType == typeof(DbDataRecord)) && (!(elementType != typeof(DbDataRecord)) || !elementType.IsSubclassOf(typeof(DbDataRecord)));
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x00116911 File Offset: 0x00114B11
		private void EnsureEntitySet()
		{
			if (this._entitySet == null)
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotResolveTheEntitySet(typeof(TElement).FullName));
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x00116935 File Offset: 0x00114B35
		public IList<TElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x0011693D File Offset: 0x00114B3D
		public bool AllowNew
		{
			get
			{
				return this._canModifyList && this._entitySet != null;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x00116955 File Offset: 0x00114B55
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x0011695D File Offset: 0x00114B5D
		public bool AllowRemove
		{
			get
			{
				return this._canModifyList;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x00116965 File Offset: 0x00114B65
		public bool FiresEventOnAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x00116968 File Offset: 0x00114B68
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x0011696B File Offset: 0x00114B6B
		public bool FiresEventOnClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x0011696E File Offset: 0x00114B6E
		public void EnsureCanAddNew()
		{
			this.EnsureEntitySet();
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x00116978 File Offset: 0x00114B78
		public int Add(TElement item, bool isAddNew)
		{
			this.EnsureEntitySet();
			if (!isAddNew)
			{
				this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet.EntityContainer.Name, this._entitySet.Name), item);
			}
			this._bindingList.Add(item);
			return this._bindingList.Count - 1;
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x001169D8 File Offset: 0x00114BD8
		public void CommitItemAt(int index)
		{
			this.EnsureEntitySet();
			TElement telement = this._bindingList[index];
			this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet.EntityContainer.Name, this._entitySet.Name), telement);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x00116A2C File Offset: 0x00114C2C
		public void Clear()
		{
			while (0 < this._bindingList.Count)
			{
				TElement item = this._bindingList[this._bindingList.Count - 1];
				this.Remove(item, false);
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00116A6C File Offset: 0x00114C6C
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

		// Token: 0x06003A99 RID: 15001 RVA: 0x00116AB4 File Offset: 0x00114CB4
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

		// Token: 0x0400162F RID: 5679
		private readonly List<TElement> _bindingList;

		// Token: 0x04001630 RID: 5680
		private readonly ObjectContext _objectContext;

		// Token: 0x04001631 RID: 5681
		private readonly EntitySet _entitySet;

		// Token: 0x04001632 RID: 5682
		private readonly bool _canEditItems;

		// Token: 0x04001633 RID: 5683
		private readonly bool _canModifyList;
	}
}
