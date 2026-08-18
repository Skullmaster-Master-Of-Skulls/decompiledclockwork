using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D9 RID: 217
	internal sealed class DataViewListener
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x00077BC8 File Offset: 0x00076FC8
		internal DataViewListener(DataView dv)
		{
			this.ObjectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00077BF4 File Offset: 0x00076FF4
		private void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ChildRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00077C28 File Offset: 0x00077028
		private void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ParentRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00077C5C File Offset: 0x0007705C
		private void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ColumnCollectionChangedInternal(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00077C90 File Offset: 0x00077090
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.MaintainDataView(changedType, row, trackAddRemove);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00077CC4 File Offset: 0x000770C4
		internal void IndexListChanged(ListChangedEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.IndexListChangedInternal(e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00077CF4 File Offset: 0x000770F4
		internal void RegisterMetaDataEvents(DataTable table)
		{
			this._table = table;
			if (table != null)
			{
				this.RegisterListener(table);
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged += value;
				table.Columns.CollectionChanged += value;
				CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged += value2;
				table.ChildRelations.CollectionChanged += value2;
				CollectionChangeEventHandler value3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged += value3;
				table.ParentRelations.CollectionChanged += value3;
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00077D90 File Offset: 0x00077190
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00077DA4 File Offset: 0x000771A4
		private void UnregisterMetaDataEvents(bool updateListeners)
		{
			DataTable table = this._table;
			this._table = null;
			if (table != null)
			{
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged -= value;
				table.Columns.CollectionChanged -= value;
				CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged -= value2;
				table.ChildRelations.CollectionChanged -= value2;
				CollectionChangeEventHandler value3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged -= value3;
				table.ParentRelations.CollectionChanged -= value3;
				if (updateListeners)
				{
					List<DataViewListener> listeners = table.GetListeners();
					List<DataViewListener> obj = listeners;
					lock (obj)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00077E8C File Offset: 0x0007728C
		internal void RegisterListChangedEvent(Index index)
		{
			this._index = index;
			if (index != null)
			{
				lock (index)
				{
					index.AddRef();
					index.ListChangedAdd(this);
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00077EE4 File Offset: 0x000772E4
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				Index obj = index;
				lock (obj)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00077F50 File Offset: 0x00077350
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00077F6C File Offset: 0x0007736C
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			List<DataViewListener> obj = listeners;
			lock (obj)
			{
				int num = listeners.Count - 1;
				while (0 <= num)
				{
					DataViewListener dataViewListener = listeners[num];
					if (!dataViewListener._dvWeak.IsAlive)
					{
						listeners.RemoveAt(num);
						dataViewListener.CleanUp(false);
					}
					num--;
				}
				listeners.Add(this);
			}
		}

		// Token: 0x04000437 RID: 1079
		private readonly WeakReference _dvWeak;

		// Token: 0x04000438 RID: 1080
		private DataTable _table;

		// Token: 0x04000439 RID: 1081
		private Index _index;

		// Token: 0x0400043A RID: 1082
		internal readonly int ObjectID;
	}
}
