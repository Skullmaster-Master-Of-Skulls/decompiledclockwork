using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200013B RID: 315
	[DefaultEvent("CollectionChanged")]
	public class BindingsCollection : BaseCollection
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00021263 File Offset: 0x0001F463
		internal BindingsCollection()
		{
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002126B File Offset: 0x0001F46B
		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return base.Count;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002127D File Offset: 0x0001F47D
		protected override ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x170002E0 RID: 736
		public Binding this[int index]
		{
			get
			{
				return (Binding)this.List[index];
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000212AC File Offset: 0x0001F4AC
		protected internal void Add(Binding binding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, binding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.AddCore(binding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000212D6 File Offset: 0x0001F4D6
		protected virtual void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			this.List.Add(dataBinding);
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000B9C RID: 2972 RVA: 0x000212F3 File Offset: 0x0001F4F3
		// (remove) Token: 0x06000B9D RID: 2973 RVA: 0x0002130C File Offset: 0x0001F50C
		[SRDescription("collectionChangingEventDescr")]
		public event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				this.onCollectionChanging = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanging, value);
			}
			remove
			{
				this.onCollectionChanging = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanging, value);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000B9E RID: 2974 RVA: 0x00021325 File Offset: 0x0001F525
		// (remove) Token: 0x06000B9F RID: 2975 RVA: 0x0002133E File Offset: 0x0001F53E
		[SRDescription("collectionChangedEventDescr")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00021358 File Offset: 0x0001F558
		protected internal void Clear()
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.ClearCore();
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00021381 File Offset: 0x0001F581
		protected virtual void ClearCore()
		{
			this.List.Clear();
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002138E File Offset: 0x0001F58E
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanging != null)
			{
				this.onCollectionChanging(this, e);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000213A5 File Offset: 0x0001F5A5
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, ccevent);
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000213BC File Offset: 0x0001F5BC
		protected internal void Remove(Binding binding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Remove, binding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.RemoveCore(binding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000213E6 File Offset: 0x0001F5E6
		protected internal void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000213F5 File Offset: 0x0001F5F5
		protected virtual void RemoveCore(Binding dataBinding)
		{
			this.List.Remove(dataBinding);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00021403 File Offset: 0x0001F603
		protected internal bool ShouldSerializeMyAll()
		{
			return this.Count > 0;
		}

		// Token: 0x040006DC RID: 1756
		private ArrayList list;

		// Token: 0x040006DD RID: 1757
		private CollectionChangeEventHandler onCollectionChanging;

		// Token: 0x040006DE RID: 1758
		private CollectionChangeEventHandler onCollectionChanged;
	}
}
