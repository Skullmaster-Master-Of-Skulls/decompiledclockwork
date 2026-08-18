using System;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000089 RID: 137
	internal sealed class ObjectPool<T> where T : class
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00008A3A File Offset: 0x00006C3A
		internal ObjectPool(Func<T> factory) : this(factory, Environment.ProcessorCount * 2)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008A4A File Offset: 0x00006C4A
		internal ObjectPool(Func<T> factory, int size)
		{
			this._factory = factory;
			this._items = new ObjectPool<T>.Element[size];
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00008A68 File Offset: 0x00006C68
		private T CreateInstance()
		{
			return this._factory();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00008A84 File Offset: 0x00006C84
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

		// Token: 0x06000374 RID: 884 RVA: 0x00008AEC File Offset: 0x00006CEC
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

		// Token: 0x04000497 RID: 1175
		private readonly ObjectPool<T>.Element[] _items;

		// Token: 0x04000498 RID: 1176
		private readonly Func<T> _factory;

		// Token: 0x020002FF RID: 767
		private struct Element
		{
			// Token: 0x04000DFE RID: 3582
			internal T Value;
		}
	}
}
