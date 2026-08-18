using System;
using System.Collections.Generic;

namespace System.IdentityModel
{
	// Token: 0x0200004E RID: 78
	internal struct MostlySingletonList<T> where T : class
	{
		// Token: 0x170000CB RID: 203
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000BB32 File Offset: 0x00009D32
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000BB3C File Offset: 0x00009D3C
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

		// Token: 0x060002EC RID: 748 RVA: 0x0000BBAA File Offset: 0x00009DAA
		private static bool Compare(T x, T y)
		{
			if (x != null)
			{
				return x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000BBCF File Offset: 0x00009DCF
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		private void EnsureValidSingletonIndex(int index)
		{
			if (this.count != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("ValueMustBeOne")));
			}
			if (index != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("ValueMustBeZero")));
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BC37 File Offset: 0x00009E37
		private bool MatchesSingleton(T item)
		{
			return this.count == 1 && MostlySingletonList<T>.Compare(this.singleton, item);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BC50 File Offset: 0x00009E50
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

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BC74 File Offset: 0x00009E74
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

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BCC8 File Offset: 0x00009EC8
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

		// Token: 0x040002AF RID: 687
		private int count;

		// Token: 0x040002B0 RID: 688
		private T singleton;

		// Token: 0x040002B1 RID: 689
		private List<T> list;
	}
}
