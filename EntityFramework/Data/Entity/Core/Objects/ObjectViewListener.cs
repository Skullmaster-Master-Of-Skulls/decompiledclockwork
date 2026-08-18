using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B5 RID: 1461
	internal sealed class ObjectViewListener
	{
		// Token: 0x06003A80 RID: 14976 RVA: 0x001165A1 File Offset: 0x001147A1
		internal ObjectViewListener(IObjectView view, IList list, object dataSource)
		{
			this._viewWeak = new WeakReference(view);
			this._dataSource = dataSource;
			this._list = list;
			this.RegisterCollectionEvents();
			this.RegisterEntityEvents();
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x001165CF File Offset: 0x001147CF
		private void CleanUpListener()
		{
			this.UnregisterCollectionEvents();
			this.UnregisterEntityEvents();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x001165E0 File Offset: 0x001147E0
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

		// Token: 0x06003A83 RID: 14979 RVA: 0x00116634 File Offset: 0x00114834
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

		// Token: 0x06003A84 RID: 14980 RVA: 0x00116688 File Offset: 0x00114888
		internal void RegisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged += this.EntityPropertyChanged;
			}
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x001166B4 File Offset: 0x001148B4
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

		// Token: 0x06003A86 RID: 14982 RVA: 0x00116728 File Offset: 0x00114928
		internal void UnregisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.EntityPropertyChanged;
			}
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00116754 File Offset: 0x00114954
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

		// Token: 0x06003A88 RID: 14984 RVA: 0x001167C8 File Offset: 0x001149C8
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

		// Token: 0x06003A89 RID: 14985 RVA: 0x001167F8 File Offset: 0x001149F8
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

		// Token: 0x0400162C RID: 5676
		private readonly WeakReference _viewWeak;

		// Token: 0x0400162D RID: 5677
		private readonly object _dataSource;

		// Token: 0x0400162E RID: 5678
		private readonly IList _list;
	}
}
