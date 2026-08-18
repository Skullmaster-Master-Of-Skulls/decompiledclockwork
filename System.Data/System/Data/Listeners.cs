using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000DF RID: 223
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x00215DE8 File Offset: 0x002151E8
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this.listeners = new List<TElem>();
			this.filter = notifyFilter;
			this.ObjectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00215E28 File Offset: 0x00215228
		internal bool HasListeners
		{
			get
			{
				return 0 < this.listeners.Count;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00215E48 File Offset: 0x00215248
		internal void Add(TElem listener)
		{
			this.listeners.Add(listener);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00215E68 File Offset: 0x00215268
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this.listeners, listener);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00215E88 File Offset: 0x00215288
		internal void Remove(TElem listener)
		{
			int index = this.IndexOfReference(listener);
			this.listeners[index] = default(TElem);
			if (this._listenerReaderCount == 0)
			{
				this.listeners.RemoveAt(index);
				this.listeners.TrimExcess();
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00215ED8 File Offset: 0x002152D8
		internal void Notify<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Listeners<TElem>.Action<TElem, T1, T2, T3> action)
		{
			int count = this.listeners.Count;
			if (0 < count)
			{
				int nullIndex = -1;
				this._listenerReaderCount++;
				try
				{
					for (int i = 0; i < count; i++)
					{
						TElem arg4 = this.listeners[i];
						if (this.filter(arg4))
						{
							action(arg4, arg1, arg2, arg3);
						}
						else
						{
							this.listeners[i] = default(TElem);
							nullIndex = i;
						}
					}
				}
				finally
				{
					this._listenerReaderCount--;
				}
				if (this._listenerReaderCount == 0)
				{
					this.RemoveNullListeners(nullIndex);
				}
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00215F98 File Offset: 0x00215398
		private void RemoveNullListeners(int nullIndex)
		{
			int num = nullIndex;
			while (0 <= num)
			{
				if (this.listeners[num] == null)
				{
					this.listeners.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x04000925 RID: 2341
		private readonly List<TElem> listeners;

		// Token: 0x04000926 RID: 2342
		private readonly Listeners<TElem>.Func<TElem, bool> filter;

		// Token: 0x04000927 RID: 2343
		private readonly int ObjectID;

		// Token: 0x04000928 RID: 2344
		private int _listenerReaderCount;

		// Token: 0x020000E0 RID: 224
		// (Invoke) Token: 0x06000D68 RID: 3432
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x06000D6C RID: 3436
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}
