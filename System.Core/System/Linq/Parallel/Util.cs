using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000208 RID: 520
	internal static class Util
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x0003A0A4 File Offset: 0x000382A4
		internal static int Sign(int x)
		{
			if (x < 0)
			{
				return -1;
			}
			if (x != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003A0B4 File Offset: 0x000382B4
		internal static Comparer<TKey> GetDefaultComparer<TKey>()
		{
			if (typeof(TKey) == typeof(int))
			{
				return (Comparer<TKey>)Util.s_fastIntComparer;
			}
			if (typeof(TKey) == typeof(long))
			{
				return (Comparer<TKey>)Util.s_fastLongComparer;
			}
			if (typeof(TKey) == typeof(float))
			{
				return (Comparer<TKey>)Util.s_fastFloatComparer;
			}
			if (typeof(TKey) == typeof(double))
			{
				return (Comparer<TKey>)Util.s_fastDoubleComparer;
			}
			if (typeof(TKey) == typeof(DateTime))
			{
				return (Comparer<TKey>)Util.s_fastDateTimeComparer;
			}
			return Comparer<TKey>.Default;
		}

		// Token: 0x04000950 RID: 2384
		private static Util.FastIntComparer s_fastIntComparer = new Util.FastIntComparer();

		// Token: 0x04000951 RID: 2385
		private static Util.FastLongComparer s_fastLongComparer = new Util.FastLongComparer();

		// Token: 0x04000952 RID: 2386
		private static Util.FastFloatComparer s_fastFloatComparer = new Util.FastFloatComparer();

		// Token: 0x04000953 RID: 2387
		private static Util.FastDoubleComparer s_fastDoubleComparer = new Util.FastDoubleComparer();

		// Token: 0x04000954 RID: 2388
		private static Util.FastDateTimeComparer s_fastDateTimeComparer = new Util.FastDateTimeComparer();

		// Token: 0x0200041C RID: 1052
		private class FastIntComparer : Comparer<int>
		{
			// Token: 0x06001EA5 RID: 7845 RVA: 0x0006DE6B File Offset: 0x0006C06B
			public override int Compare(int x, int y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x0200041D RID: 1053
		private class FastLongComparer : Comparer<long>
		{
			// Token: 0x06001EA7 RID: 7847 RVA: 0x0006DE7D File Offset: 0x0006C07D
			public override int Compare(long x, long y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x0200041E RID: 1054
		private class FastFloatComparer : Comparer<float>
		{
			// Token: 0x06001EA9 RID: 7849 RVA: 0x0006DE8F File Offset: 0x0006C08F
			public override int Compare(float x, float y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x0200041F RID: 1055
		private class FastDoubleComparer : Comparer<double>
		{
			// Token: 0x06001EAB RID: 7851 RVA: 0x0006DEA1 File Offset: 0x0006C0A1
			public override int Compare(double x, double y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x02000420 RID: 1056
		private class FastDateTimeComparer : Comparer<DateTime>
		{
			// Token: 0x06001EAD RID: 7853 RVA: 0x0006DEB3 File Offset: 0x0006C0B3
			public override int Compare(DateTime x, DateTime y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
