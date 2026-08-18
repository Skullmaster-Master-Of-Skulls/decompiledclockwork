using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x02000128 RID: 296
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x00088998 File Offset: 0x00087D98
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this.listeners = new List<TElem>();
			this.filter = notifyFilter;
			this.ObjectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000889CC File Offset: 0x00087DCC
		internal bool HasListeners
		{
			get
			{
				return 0 < this.listeners.Count;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000889E8 File Offset: 0x00087DE8
		internal void Add(TElem listener)
		{
			this.listeners.Add(listener);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00088A04 File Offset: 0x00087E04
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this.listeners, listener);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00088A20 File Offset: 0x00087E20
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

		// Token: 0x060011CC RID: 4556 RVA: 0x00088A6C File Offset: 0x00087E6C
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

		// Token: 0x060011CD RID: 4557 RVA: 0x00088B24 File Offset: 0x00087F24
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

		// Token: 0x040005FD RID: 1533
		private readonly List<TElem> listeners;

		// Token: 0x040005FE RID: 1534
		private readonly Listeners<TElem>.Func<TElem, bool> filter;

		// Token: 0x040005FF RID: 1535
		private readonly int ObjectID;

		// Token: 0x04000600 RID: 1536
		private int _listenerReaderCount;

		// Token: 0x0200035C RID: 860
		// (Invoke) Token: 0x0600342C RID: 13356
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x0200035D RID: 861
		// (Invoke) Token: 0x06003430 RID: 13360
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}
