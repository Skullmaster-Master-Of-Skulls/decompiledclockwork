using System;

namespace System.ServiceModel
{
	// Token: 0x0200004C RID: 76
	internal class Pool<T> where T : class
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		public Pool(int maxCount)
		{
			this.items = new T[maxCount];
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AE04 File Offset: 0x00009004
		public T Take()
		{
			if (this.count > 0)
			{
				T[] array = this.items;
				int num = this.count - 1;
				this.count = num;
				T result = array[num];
				this.items[this.count] = default(T);
				return result;
			}
			return default(T);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000AE5C File Offset: 0x0000905C
		public bool Return(T item)
		{
			if (this.count < this.items.Length)
			{
				T[] array = this.items;
				int num = this.count;
				this.count = num + 1;
				array[num] = item;
				return true;
			}
			return false;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AE9C File Offset: 0x0000909C
		public void Clear()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.items[i] = default(T);
			}
			this.count = 0;
		}

		// Token: 0x0400029C RID: 668
		private T[] items;

		// Token: 0x0400029D RID: 669
		private int count;
	}
}
