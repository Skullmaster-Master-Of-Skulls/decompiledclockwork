using System;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x020002CC RID: 716
	internal abstract class DbReferenceCollection
	{
		// Token: 0x06002B2F RID: 11055 RVA: 0x0011BBFC File Offset: 0x0011AFFC
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[20];
			this._itemLock = new object();
			this._optimisticCount = 0;
			this._lastItemIndex = 0;
		}

		// Token: 0x06002B30 RID: 11056
		public abstract void Add(object value, int tag);

		// Token: 0x06002B31 RID: 11057 RVA: 0x0011BC38 File Offset: 0x0011B038
		protected void AddItem(object value, int tag)
		{
			bool flag = false;
			object itemLock = this._itemLock;
			lock (itemLock)
			{
				for (int i = 0; i <= this._lastItemIndex; i++)
				{
					if (this._items[i].Tag == 0)
					{
						this._items[i].NewTarget(tag, value);
						flag = true;
						break;
					}
				}
				if (!flag && this._lastItemIndex + 1 < this._items.Length)
				{
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j <= this._lastItemIndex; j++)
					{
						if (!this._items[j].HasTarget)
						{
							this._items[j].NewTarget(tag, value);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Array.Resize<DbReferenceCollection.CollectionEntry>(ref this._items, this._items.Length * 2);
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
				}
				this._optimisticCount++;
			}
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0011BD84 File Offset: 0x0011B184
		internal T FindItem<T>(int tag, Func<T, bool> filterMethod) where T : class
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (this._items[i].Tag == tag)
						{
							object target = this._items[i].Target;
							if (target != null)
							{
								T t = target as T;
								if (t != null && filterMethod(t))
								{
									return t;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
			return default(T);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x0011BE38 File Offset: 0x0011B238
		public void Notify(int message)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag)
				{
					try
					{
						this._isNotifying = true;
						if (this._optimisticCount > 0)
						{
							for (int i = 0; i <= this._lastItemIndex; i++)
							{
								object target = this._items[i].Target;
								if (target != null)
								{
									this.NotifyItem(message, this._items[i].Tag, target);
									this._items[i].RemoveTarget();
								}
							}
							this._optimisticCount = 0;
						}
						if (this._items.Length > 100)
						{
							this._lastItemIndex = 0;
							this._items = new DbReferenceCollection.CollectionEntry[20];
						}
					}
					finally
					{
						this._isNotifying = false;
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06002B34 RID: 11060
		protected abstract void NotifyItem(int message, int tag, object value);

		// Token: 0x06002B35 RID: 11061
		public abstract void Remove(object value);

		// Token: 0x06002B36 RID: 11062 RVA: 0x0011BF28 File Offset: 0x0011B328
		protected void RemoveItem(object value)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (value == this._items[i].Target)
						{
							this._items[i].RemoveTarget();
							this._optimisticCount--;
							break;
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0011BFB8 File Offset: 0x0011B3B8
		private void TryEnterItemLock(ref bool lockObtained)
		{
			lockObtained = false;
			while (!this._isNotifying && !lockObtained)
			{
				Monitor.TryEnter(this._itemLock, 100, ref lockObtained);
			}
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0011BFE8 File Offset: 0x0011B3E8
		private void ExitItemLockIfNeeded(bool lockObtained)
		{
			if (lockObtained)
			{
				Monitor.Exit(this._itemLock);
			}
		}

		// Token: 0x04001BAA RID: 7082
		private const int LockPollTime = 100;

		// Token: 0x04001BAB RID: 7083
		private const int DefaultCollectionSize = 20;

		// Token: 0x04001BAC RID: 7084
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x04001BAD RID: 7085
		private readonly object _itemLock;

		// Token: 0x04001BAE RID: 7086
		private int _optimisticCount;

		// Token: 0x04001BAF RID: 7087
		private int _lastItemIndex;

		// Token: 0x04001BB0 RID: 7088
		private volatile bool _isNotifying;

		// Token: 0x02000430 RID: 1072
		private struct CollectionEntry
		{
			// Token: 0x06003624 RID: 13860 RVA: 0x00148EB0 File Offset: 0x001482B0
			public void NewTarget(int tag, object target)
			{
				if (this._weak == null)
				{
					this._weak = new WeakReference(target, false);
				}
				else
				{
					this._weak.Target = target;
				}
				this._tag = tag;
			}

			// Token: 0x06003625 RID: 13861 RVA: 0x00148EE8 File Offset: 0x001482E8
			public void RemoveTarget()
			{
				this._tag = 0;
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x06003626 RID: 13862 RVA: 0x00148EFC File Offset: 0x001482FC
			public bool HasTarget
			{
				get
				{
					return this._tag != 0 && this._weak.IsAlive;
				}
			}

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x06003627 RID: 13863 RVA: 0x00148F20 File Offset: 0x00148320
			public int Tag
			{
				get
				{
					return this._tag;
				}
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x06003628 RID: 13864 RVA: 0x00148F34 File Offset: 0x00148334
			public object Target
			{
				get
				{
					if (this._tag != 0)
					{
						return this._weak.Target;
					}
					return null;
				}
			}

			// Token: 0x04002307 RID: 8967
			private int _tag;

			// Token: 0x04002308 RID: 8968
			private WeakReference _weak;
		}
	}
}
