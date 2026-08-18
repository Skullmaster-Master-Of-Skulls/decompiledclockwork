using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020003BB RID: 955
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x000A9400 File Offset: 0x000A7600
		[__DynamicallyInvokable]
		public ReadOnlyObservableCollection(ObservableCollection<T> list) : base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600240E RID: 9230 RVA: 0x000A944C File Offset: 0x000A764C
		// (remove) Token: 0x0600240F RID: 9231 RVA: 0x000A9455 File Offset: 0x000A7655
		[__DynamicallyInvokable]
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			[__DynamicallyInvokable]
			add
			{
				this.CollectionChanged += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002410 RID: 9232 RVA: 0x000A9460 File Offset: 0x000A7660
		// (remove) Token: 0x06002411 RID: 9233 RVA: 0x000A9498 File Offset: 0x000A7698
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		[NonSerialized]
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x06002412 RID: 9234 RVA: 0x000A94CD File Offset: 0x000A76CD
		[__DynamicallyInvokable]
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002413 RID: 9235 RVA: 0x000A94E4 File Offset: 0x000A76E4
		// (remove) Token: 0x06002414 RID: 9236 RVA: 0x000A94ED File Offset: 0x000A76ED
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

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002415 RID: 9237 RVA: 0x000A94F8 File Offset: 0x000A76F8
		// (remove) Token: 0x06002416 RID: 9238 RVA: 0x000A9530 File Offset: 0x000A7730
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		[NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06002417 RID: 9239 RVA: 0x000A9565 File Offset: 0x000A7765
		[__DynamicallyInvokable]
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000A957C File Offset: 0x000A777C
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000A9585 File Offset: 0x000A7785
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}
	}
}
