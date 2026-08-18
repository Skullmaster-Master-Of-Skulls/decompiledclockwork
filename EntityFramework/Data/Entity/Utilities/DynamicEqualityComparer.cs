using System;
using System.Collections.Generic;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000722 RID: 1826
	internal sealed class DynamicEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06004B1B RID: 19227 RVA: 0x0016103D File Offset: 0x0015F23D
		public DynamicEqualityComparer(Func<T, T, bool> func)
		{
			this._func = func;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0016104C File Offset: 0x0015F24C
		public bool Equals(T x, T y)
		{
			return this._func(x, y);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0016105B File Offset: 0x0015F25B
		public int GetHashCode(T obj)
		{
			return 0;
		}

		// Token: 0x04001B5F RID: 7007
		private readonly Func<T, T, bool> _func;
	}
}
