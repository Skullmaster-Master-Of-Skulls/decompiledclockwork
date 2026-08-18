using System;

namespace System.Collections.Generic
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	internal class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000BA0F File Offset: 0x00009C0F
		public HashSetEqualityComparer()
		{
			this.m_comparer = EqualityComparer<T>.Default;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BA22 File Offset: 0x00009C22
		public HashSetEqualityComparer(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				this.m_comparer = EqualityComparer<T>.Default;
				return;
			}
			this.m_comparer = comparer;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000BA40 File Offset: 0x00009C40
		public bool Equals(HashSet<T> x, HashSet<T> y)
		{
			return HashSet<T>.HashSetEquals(x, y, this.m_comparer);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000BA50 File Offset: 0x00009C50
		public int GetHashCode(HashSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T obj2 in obj)
				{
					num ^= (this.m_comparer.GetHashCode(obj2) & int.MaxValue);
				}
			}
			return num;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public override bool Equals(object obj)
		{
			HashSetEqualityComparer<T> hashSetEqualityComparer = obj as HashSetEqualityComparer<T>;
			return hashSetEqualityComparer != null && this.m_comparer == hashSetEqualityComparer.m_comparer;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000BADB File Offset: 0x00009CDB
		public override int GetHashCode()
		{
			return this.m_comparer.GetHashCode();
		}

		// Token: 0x040004DC RID: 1244
		private IEqualityComparer<T> m_comparer;
	}
}
