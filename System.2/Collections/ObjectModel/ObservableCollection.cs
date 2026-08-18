using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020003BA RID: 954
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		// Token: 0x060023F4 RID: 9204 RVA: 0x000A9020 File Offset: 0x000A7220
		[__DynamicallyInvokable]
		public ObservableCollection()
		{
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000A9033 File Offset: 0x000A7233
		public ObservableCollection(List<T> list) : base((list != null) ? new List<T>(list.Count) : list)
		{
			this.CopyFrom(list);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000A905E File Offset: 0x000A725E
		[__DynamicallyInvokable]
		public ObservableCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.CopyFrom(collection);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000A9088 File Offset: 0x000A7288
		private void CopyFrom(IEnumerable<T> collection)
		{
			IList<T> items = base.Items;
			if (collection != null && items != null)
			{
				foreach (T item in collection)
				{
					items.Add(item);
				}
			}
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000A90DC File Offset: 0x000A72DC
		[__DynamicallyInvokable]
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060023F9 RID: 9209 RVA: 0x000A90E6 File Offset: 0x000A72E6
		// (remove) Token: 0x060023FA RID: 9210 RVA: 0x000A90EF File Offset: 0x000A72EF
		[__DynamicallyInvokable]
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			[__DynamicallyInvokable]
			add
			{
				this.PropertyChanged += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060023FB RID: 9211 RVA: 0x000A90F8 File Offset: 0x000A72F8
		// (remove) Token: 0x060023FC RID: 9212 RVA: 0x000A9130 File Offset: 0x000A7330
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		[NonSerialized]
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x060023FD RID: 9213 RVA: 0x000A9165 File Offset: 0x000A7365
		[__DynamicallyInvokable]
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionReset();
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000A9190 File Offset: 0x000A7390
		[__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000A91D6 File Offset: 0x000A73D6
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000A920C File Offset: 0x000A740C
		[__DynamicallyInvokable]
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000A9250 File Offset: 0x000A7450
		[__DynamicallyInvokable]
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A9294 File Offset: 0x000A7494
		[__DynamicallyInvokable]
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002403 RID: 9219 RVA: 0x000A92AC File Offset: 0x000A74AC
		// (remove) Token: 0x06002404 RID: 9220 RVA: 0x000A92E4 File Offset: 0x000A74E4
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		[NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06002405 RID: 9221 RVA: 0x000A931C File Offset: 0x000A751C
		[__DynamicallyInvokable]
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (this.BlockReentrancy())
				{
					this.CollectionChanged(this, e);
				}
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000A9364 File Offset: 0x000A7564
		[__DynamicallyInvokable]
		protected IDisposable BlockReentrancy()
		{
			this._monitor.Enter();
			return this._monitor;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A9377 File Offset: 0x000A7577
		[__DynamicallyInvokable]
		protected void CheckReentrancy()
		{
			if (this._monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
			{
				throw new InvalidOperationException(SR.GetString("ObservableCollectionReentrancyNotAllowed"));
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000A93AE File Offset: 0x000A75AE
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000A93BC File Offset: 0x000A75BC
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A93CC File Offset: 0x000A75CC
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000A93DE File Offset: 0x000A75DE
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000A93F0 File Offset: 0x000A75F0
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		// Token: 0x04001FFC RID: 8188
		private const string CountString = "Count";

		// Token: 0x04001FFD RID: 8189
		private const string IndexerName = "Item[]";

		// Token: 0x04001FFE RID: 8190
		private ObservableCollection<T>.SimpleMonitor _monitor = new ObservableCollection<T>.SimpleMonitor();

		// Token: 0x020007F1 RID: 2033
		[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
		[Serializable]
		private class SimpleMonitor : IDisposable
		{
			// Token: 0x06004428 RID: 17448 RVA: 0x0011E6BC File Offset: 0x0011C8BC
			public void Enter()
			{
				this._busyCount++;
			}

			// Token: 0x06004429 RID: 17449 RVA: 0x0011E6CC File Offset: 0x0011C8CC
			public void Dispose()
			{
				this._busyCount--;
			}

			// Token: 0x17000F75 RID: 3957
			// (get) Token: 0x0600442A RID: 17450 RVA: 0x0011E6DC File Offset: 0x0011C8DC
			public bool Busy
			{
				get
				{
					return this._busyCount > 0;
				}
			}

			// Token: 0x04003515 RID: 13589
			private int _busyCount;
		}
	}
}
