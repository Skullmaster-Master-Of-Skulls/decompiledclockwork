using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects
{
	// Token: 0x0200014E RID: 334
	internal sealed class ObjectViewListener
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x00053D84 File Offset: 0x00051F84
		internal ObjectViewListener(IObjectView view, IList list, object dataSource)
		{
			this._viewWeak = new WeakReference(view);
			this._dataSource = dataSource;
			this._list = list;
			this.RegisterCollectionEvents();
			this.RegisterEntityEvents();
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00053DB2 File Offset: 0x00051FB2
		private void CleanUpListener()
		{
			this.UnregisterCollectionEvents();
			this.UnregisterEntityEvents();
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00053DC0 File Offset: 0x00051FC0
		private void RegisterCollectionEvents()
		{
			ObjectStateManager objectStateManager = this._dataSource as ObjectStateManager;
			if (objectStateManager != null)
			{
				objectStateManager.EntityDeleted += this.CollectionChanged;
				return;
			}
			if (this._dataSource != null)
			{
				((RelatedEnd)this._dataSource).AssociationChangedForObjectView += this.CollectionChanged;
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00053E14 File Offset: 0x00052014
		private void UnregisterCollectionEvents()
		{
			ObjectStateManager objectStateManager = this._dataSource as ObjectStateManager;
			if (objectStateManager != null)
			{
				objectStateManager.EntityDeleted -= this.CollectionChanged;
				return;
			}
			if (this._dataSource != null)
			{
				((RelatedEnd)this._dataSource).AssociationChangedForObjectView -= this.CollectionChanged;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00053E68 File Offset: 0x00052068
		internal void RegisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged += this.EntityPropertyChanged;
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00053E94 File Offset: 0x00052094
		private void RegisterEntityEvents()
		{
			if (this._list != null)
			{
				foreach (object obj in this._list)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					if (notifyPropertyChanged != null)
					{
						notifyPropertyChanged.PropertyChanged += this.EntityPropertyChanged;
					}
				}
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00053F08 File Offset: 0x00052108
		internal void UnregisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.EntityPropertyChanged;
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00053F34 File Offset: 0x00052134
		private void UnregisterEntityEvents()
		{
			if (this._list != null)
			{
				foreach (object obj in this._list)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					if (notifyPropertyChanged != null)
					{
						notifyPropertyChanged.PropertyChanged -= this.EntityPropertyChanged;
					}
				}
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00053FA8 File Offset: 0x000521A8
		private void EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IObjectView objectView = (IObjectView)this._viewWeak.Target;
			if (objectView != null)
			{
				objectView.EntityPropertyChanged(sender, e);
				return;
			}
			this.CleanUpListener();
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00053FD8 File Offset: 0x000521D8
		private void CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			IObjectView objectView = (IObjectView)this._viewWeak.Target;
			if (objectView != null)
			{
				objectView.CollectionChanged(sender, e);
				return;
			}
			this.CleanUpListener();
		}

		// Token: 0x04000AC8 RID: 2760
		private WeakReference _viewWeak;

		// Token: 0x04000AC9 RID: 2761
		private object _dataSource;

		// Token: 0x04000ACA RID: 2762
		private IList _list;
	}
}
