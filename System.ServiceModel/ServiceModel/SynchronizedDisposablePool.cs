using System;
using System.Collections.Generic;

namespace System.ServiceModel
{
	// Token: 0x02000053 RID: 83
	internal class SynchronizedDisposablePool<T> where T : class, IDisposable
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000C777 File Offset: 0x0000A977
		public SynchronizedDisposablePool(int maxCount)
		{
			this.items = new List<T>();
			this.maxCount = maxCount;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C791 File Offset: 0x0000A991
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C794 File Offset: 0x0000A994
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			T[] array;
			lock (thisLock)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					if (this.items.Count > 0)
					{
						array = new T[this.items.Count];
						this.items.CopyTo(array, 0);
						this.items.Clear();
					}
					else
					{
						array = null;
					}
				}
				else
				{
					array = null;
				}
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C840 File Offset: 0x0000AA40
		public bool Return(T value)
		{
			if (!this.disposed && this.items.Count < this.maxCount)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.disposed && this.items.Count < this.maxCount)
					{
						this.items.Add(value);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public T Take()
		{
			if (!this.disposed && this.items.Count > 0)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.disposed && this.items.Count > 0)
					{
						int index = this.items.Count - 1;
						T result = this.items[index];
						this.items.RemoveAt(index);
						return result;
					}
				}
			}
			return default(T);
		}

		// Token: 0x040004AD RID: 1197
		private List<T> items;

		// Token: 0x040004AE RID: 1198
		private int maxCount;

		// Token: 0x040004AF RID: 1199
		private bool disposed;
	}
}
