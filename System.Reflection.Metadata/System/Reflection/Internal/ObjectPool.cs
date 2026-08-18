using System;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000167 RID: 359
	internal sealed class ObjectPool<T> where T : class
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x0002066B File Offset: 0x0001E86B
		internal ObjectPool(Func<T> factory) : this(factory, Environment.ProcessorCount * 2)
		{
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002067B File Offset: 0x0001E87B
		internal ObjectPool(Func<T> factory, int size)
		{
			this._factory = factory;
			this._items = new ObjectPool<T>.Element[size];
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00020696 File Offset: 0x0001E896
		private T CreateInstance()
		{
			return this._factory();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000206A4 File Offset: 0x0001E8A4
		internal T Allocate()
		{
			ObjectPool<T>.Element[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				T value = items[i].Value;
				if (value != null && value == Interlocked.CompareExchange<T>(ref items[i].Value, default(T), value))
				{
					return value;
				}
			}
			return this.CreateInstance();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002070C File Offset: 0x0001E90C
		internal void Free(T obj)
		{
			ObjectPool<T>.Element[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Value == null)
				{
					items[i].Value = obj;
					return;
				}
			}
		}

		// Token: 0x0400092C RID: 2348
		private readonly ObjectPool<T>.Element[] _items;

		// Token: 0x0400092D RID: 2349
		private readonly Func<T> _factory;

		// Token: 0x020001E4 RID: 484
		private struct Element
		{
			// Token: 0x04000B5C RID: 2908
			internal T Value;
		}
	}
}
