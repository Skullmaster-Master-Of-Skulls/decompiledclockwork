using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000798 RID: 1944
	internal class ObservableBackedBindingList<T> : SortableBindingList<T>
	{
		// Token: 0x06005816 RID: 22550 RVA: 0x0017AE38 File Offset: 0x00179038
		public ObservableBackedBindingList(ObservableCollection<T> obervableCollection) : base(obervableCollection.ToList<T>())
		{
			this._obervableCollection = obervableCollection;
			this._obervableCollection.CollectionChanged += this.ObservableCollectionChanged;
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x0017AE64 File Offset: 0x00179064
		protected override object AddNewCore()
		{
			this._addingNewInstance = true;
			this._addNewInstance = (T)((object)base.AddNewCore());
			return this._addNewInstance;
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x0017AE8C File Offset: 0x0017908C
		public override void CancelNew(int itemIndex)
		{
			if (itemIndex >= 0 && itemIndex < base.Count && object.Equals(base[itemIndex], this._addNewInstance))
			{
				this._cancelNewInstance = this._addNewInstance;
				this._addNewInstance = default(T);
				this._addingNewInstance = false;
			}
			base.CancelNew(itemIndex);
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x0017AEEC File Offset: 0x001790EC
		protected override void ClearItems()
		{
			foreach (T item in base.Items)
			{
				this.RemoveFromObservableCollection(item);
			}
			base.ClearItems();
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x0017AF40 File Offset: 0x00179140
		public override void EndNew(int itemIndex)
		{
			if (itemIndex >= 0 && itemIndex < base.Count && object.Equals(base[itemIndex], this._addNewInstance))
			{
				this.AddToObservableCollection(this._addNewInstance);
				this._addNewInstance = default(T);
				this._addingNewInstance = false;
			}
			base.EndNew(itemIndex);
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0017AF9E File Offset: 0x0017919E
		protected override void InsertItem(int index, T item)
		{
			base.InsertItem(index, item);
			if (!this._addingNewInstance && index >= 0 && index <= base.Count)
			{
				this.AddToObservableCollection(item);
			}
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0017AFC4 File Offset: 0x001791C4
		protected override void RemoveItem(int index)
		{
			if (index >= 0 && index < base.Count && object.Equals(base[index], this._cancelNewInstance))
			{
				this._cancelNewInstance = default(T);
			}
			else
			{
				this.RemoveFromObservableCollection(base[index]);
			}
			base.RemoveItem(index);
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x0017B020 File Offset: 0x00179220
		protected override void SetItem(int index, T item)
		{
			T t = base[index];
			base.SetItem(index, item);
			if (index >= 0 && index < base.Count)
			{
				if (object.Equals(t, this._addNewInstance))
				{
					this._addNewInstance = default(T);
					this._addingNewInstance = false;
				}
				else
				{
					this.RemoveFromObservableCollection(t);
				}
				this.AddToObservableCollection(item);
			}
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x0017B088 File Offset: 0x00179288
		private void ObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!this._changingObservableCollection)
			{
				try
				{
					this._inCollectionChanged = true;
					if (e.Action == NotifyCollectionChangedAction.Reset)
					{
						base.Clear();
					}
					if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
					{
						foreach (object obj in e.OldItems)
						{
							T item = (T)((object)obj);
							base.Remove(item);
						}
					}
					if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
					{
						foreach (object obj2 in e.NewItems)
						{
							T item2 = (T)((object)obj2);
							base.Add(item2);
						}
					}
				}
				finally
				{
					this._inCollectionChanged = false;
				}
			}
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x0017B18C File Offset: 0x0017938C
		private void AddToObservableCollection(T item)
		{
			if (!this._inCollectionChanged)
			{
				try
				{
					this._changingObservableCollection = true;
					this._obervableCollection.Add(item);
				}
				finally
				{
					this._changingObservableCollection = false;
				}
			}
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x0017B1D0 File Offset: 0x001793D0
		private void RemoveFromObservableCollection(T item)
		{
			if (!this._inCollectionChanged)
			{
				try
				{
					this._changingObservableCollection = true;
					this._obervableCollection.Remove(item);
				}
				finally
				{
					this._changingObservableCollection = false;
				}
			}
		}

		// Token: 0x04002358 RID: 9048
		private bool _addingNewInstance;

		// Token: 0x04002359 RID: 9049
		private T _addNewInstance;

		// Token: 0x0400235A RID: 9050
		private T _cancelNewInstance;

		// Token: 0x0400235B RID: 9051
		private readonly ObservableCollection<T> _obervableCollection;

		// Token: 0x0400235C RID: 9052
		private bool _inCollectionChanged;

		// Token: 0x0400235D RID: 9053
		private bool _changingObservableCollection;
	}
}
