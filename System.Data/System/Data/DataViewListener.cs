using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000AA RID: 170
	internal sealed class DataViewListener
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0020E118 File Offset: 0x0020D518
		internal DataViewListener(DataView dv)
		{
			this.ObjectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0020E148 File Offset: 0x0020D548
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

		// Token: 0x06000B9A RID: 2970 RVA: 0x0020E188 File Offset: 0x0020D588
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

		// Token: 0x06000B9B RID: 2971 RVA: 0x0020E1C8 File Offset: 0x0020D5C8
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

		// Token: 0x06000B9C RID: 2972 RVA: 0x0020E208 File Offset: 0x0020D608
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

		// Token: 0x06000B9D RID: 2973 RVA: 0x0020E248 File Offset: 0x0020D648
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

		// Token: 0x06000B9E RID: 2974 RVA: 0x0020E278 File Offset: 0x0020D678
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

		// Token: 0x06000B9F RID: 2975 RVA: 0x0020E318 File Offset: 0x0020D718
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0020E338 File Offset: 0x0020D738
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
					lock (listeners)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0020E418 File Offset: 0x0020D818
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

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0020E478 File Offset: 0x0020D878
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				lock (index)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0020E4E8 File Offset: 0x0020D8E8
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0020E508 File Offset: 0x0020D908
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			lock (listeners)
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

		// Token: 0x0400085B RID: 2139
		private readonly WeakReference _dvWeak;

		// Token: 0x0400085C RID: 2140
		private DataTable _table;

		// Token: 0x0400085D RID: 2141
		private Index _index;

		// Token: 0x0400085E RID: 2142
		internal readonly int ObjectID;
	}
}
