using System;
using System.Collections.Generic;

namespace System.ServiceModel
{
	// Token: 0x020000CB RID: 203
	internal struct MostlySingletonList<T> where T : class
	{
		// Token: 0x170000D6 RID: 214
		public T this[int index]
		{
			get
			{
				if (this.list == null)
				{
					this.EnsureValidSingletonIndex(index);
					return this.singleton;
				}
				return this.list[index];
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00014EBE File Offset: 0x000130BE
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00014EC8 File Offset: 0x000130C8
		public void Add(T item)
		{
			if (this.list == null)
			{
				if (this.count == 0)
				{
					this.singleton = item;
					this.count = 1;
					return;
				}
				this.list = new List<T>();
				this.list.Add(this.singleton);
				this.singleton = default(T);
			}
			this.list.Add(item);
			this.count++;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00014F36 File Offset: 0x00013136
		private static bool Compare(T x, T y)
		{
			if (x != null)
			{
				return x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00014F5B File Offset: 0x0001315B
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00014F6A File Offset: 0x0001316A
		private void EnsureValidSingletonIndex(int index)
		{
			if (this.count != 1 || index != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00014F8D File Offset: 0x0001318D
		private bool MatchesSingleton(T item)
		{
			return this.count == 1 && MostlySingletonList<T>.Compare(this.singleton, item);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00014FA6 File Offset: 0x000131A6
		public int IndexOf(T item)
		{
			if (this.list != null)
			{
				return this.list.IndexOf(item);
			}
			if (!this.MatchesSingleton(item))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00014FCC File Offset: 0x000131CC
		public bool Remove(T item)
		{
			if (this.list != null)
			{
				bool flag = this.list.Remove(item);
				if (flag)
				{
					this.count--;
				}
				return flag;
			}
			if (this.MatchesSingleton(item))
			{
				this.singleton = default(T);
				this.count = 0;
				return true;
			}
			return false;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00015020 File Offset: 0x00013220
		public void RemoveAt(int index)
		{
			if (this.list == null)
			{
				this.EnsureValidSingletonIndex(index);
				this.singleton = default(T);
				this.count = 0;
				return;
			}
			this.list.RemoveAt(index);
			this.count--;
		}

		// Token: 0x04000988 RID: 2440
		private int count;

		// Token: 0x04000989 RID: 2441
		private T singleton;

		// Token: 0x0400098A RID: 2442
		private List<T> list;
	}
}
