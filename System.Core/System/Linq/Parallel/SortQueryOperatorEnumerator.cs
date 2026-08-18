using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001E5 RID: 485
	internal class SortQueryOperatorEnumerator<TInputOutput, TKey, TSortKey> : QueryOperatorEnumerator<TInputOutput, TSortKey>
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00037A54 File Offset: 0x00035C54
		internal SortQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, TSortKey> keySelector, IComparer<TSortKey> keyComparer)
		{
			this.m_source = source;
			this.m_keySelector = keySelector;
			this.m_keyComparer = keyComparer;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00037A71 File Offset: 0x00035C71
		public IComparer<TSortKey> KeyComparer
		{
			get
			{
				return this.m_keyComparer;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00037A7C File Offset: 0x00035C7C
		internal override bool MoveNext(ref TInputOutput currentElement, ref TSortKey currentKey)
		{
			TKey tkey = default(TKey);
			if (!this.m_source.MoveNext(ref currentElement, ref tkey))
			{
				return false;
			}
			currentKey = this.m_keySelector(currentElement);
			return true;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00037ABB File Offset: 0x00035CBB
		protected override void Dispose(bool disposing)
		{
			this.m_source.Dispose();
		}

		// Token: 0x040008F2 RID: 2290
		private readonly QueryOperatorEnumerator<TInputOutput, TKey> m_source;

		// Token: 0x040008F3 RID: 2291
		private readonly Func<TInputOutput, TSortKey> m_keySelector;

		// Token: 0x040008F4 RID: 2292
		private readonly IComparer<TSortKey> m_keyComparer;
	}
}
