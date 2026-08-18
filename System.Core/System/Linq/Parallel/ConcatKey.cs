using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001CC RID: 460
	internal struct ConcatKey<TLeftKey, TRightKey>
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003601B File Offset: 0x0003421B
		private ConcatKey(TLeftKey leftKey, TRightKey rightKey, bool isLeft)
		{
			this.m_leftKey = leftKey;
			this.m_rightKey = rightKey;
			this.m_isLeft = isLeft;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00036034 File Offset: 0x00034234
		internal static ConcatKey<TLeftKey, TRightKey> MakeLeft(TLeftKey leftKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(leftKey, default(TRightKey), true);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00036054 File Offset: 0x00034254
		internal static ConcatKey<TLeftKey, TRightKey> MakeRight(TRightKey rightKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(default(TLeftKey), rightKey, false);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00036071 File Offset: 0x00034271
		internal static IComparer<ConcatKey<TLeftKey, TRightKey>> MakeComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
		{
			return new ConcatKey<TLeftKey, TRightKey>.ConcatKeyComparer(leftComparer, rightComparer);
		}

		// Token: 0x040008BC RID: 2236
		private readonly TLeftKey m_leftKey;

		// Token: 0x040008BD RID: 2237
		private readonly TRightKey m_rightKey;

		// Token: 0x040008BE RID: 2238
		private readonly bool m_isLeft;

		// Token: 0x020003ED RID: 1005
		private class ConcatKeyComparer : IComparer<ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06001E0F RID: 7695 RVA: 0x0006B86D File Offset: 0x00069A6D
			internal ConcatKeyComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
			{
				this.m_leftComparer = leftComparer;
				this.m_rightComparer = rightComparer;
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x0006B884 File Offset: 0x00069A84
			public int Compare(ConcatKey<TLeftKey, TRightKey> x, ConcatKey<TLeftKey, TRightKey> y)
			{
				if (x.m_isLeft != y.m_isLeft)
				{
					if (!x.m_isLeft)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x.m_isLeft)
					{
						return this.m_leftComparer.Compare(x.m_leftKey, y.m_leftKey);
					}
					return this.m_rightComparer.Compare(x.m_rightKey, y.m_rightKey);
				}
			}

			// Token: 0x040011BB RID: 4539
			private IComparer<TLeftKey> m_leftComparer;

			// Token: 0x040011BC RID: 4540
			private IComparer<TRightKey> m_rightComparer;
		}
	}
}
