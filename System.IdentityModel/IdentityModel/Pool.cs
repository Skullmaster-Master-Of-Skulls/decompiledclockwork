using System;

namespace System.IdentityModel
{
	// Token: 0x02000064 RID: 100
	internal class Pool<T> where T : class
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000BDC2 File Offset: 0x00009FC2
		public Pool(int maxCount)
		{
			this.items = new T[maxCount];
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000BDD6 File Offset: 0x00009FD6
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BDE0 File Offset: 0x00009FE0
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

		// Token: 0x0600031D RID: 797 RVA: 0x0000BE38 File Offset: 0x0000A038
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

		// Token: 0x0600031E RID: 798 RVA: 0x0000BE78 File Offset: 0x0000A078
		public void Clear()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.items[i] = default(T);
			}
			this.count = 0;
		}

		// Token: 0x04000340 RID: 832
		private T[] items;

		// Token: 0x04000341 RID: 833
		private int count;
	}
}
