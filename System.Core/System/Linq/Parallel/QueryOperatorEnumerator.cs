using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001C5 RID: 453
	internal abstract class QueryOperatorEnumerator<TElement, TKey>
	{
		// Token: 0x06000F00 RID: 3840
		internal abstract bool MoveNext(ref TElement currentElement, ref TKey currentKey);

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035786 File Offset: 0x00033986
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003578F File Offset: 0x0003398F
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00035791 File Offset: 0x00033991
		internal virtual void Reset()
		{
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00035793 File Offset: 0x00033993
		internal IEnumerator<TElement> AsClassicEnumerator()
		{
			return new QueryOperatorEnumerator<TElement, TKey>.QueryOperatorClassicEnumerator(this);
		}

		// Token: 0x020003E8 RID: 1000
		private class QueryOperatorClassicEnumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001DF8 RID: 7672 RVA: 0x0006B537 File Offset: 0x00069737
			internal QueryOperatorClassicEnumerator(QueryOperatorEnumerator<TElement, TKey> operatorEnumerator)
			{
				this.m_operatorEnumerator = operatorEnumerator;
			}

			// Token: 0x06001DF9 RID: 7673 RVA: 0x0006B548 File Offset: 0x00069748
			public bool MoveNext()
			{
				TKey tkey = default(TKey);
				return this.m_operatorEnumerator.MoveNext(ref this.m_current, ref tkey);
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0006B570 File Offset: 0x00069770
			public TElement Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0006B578 File Offset: 0x00069778
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x06001DFC RID: 7676 RVA: 0x0006B585 File Offset: 0x00069785
			public void Dispose()
			{
				this.m_operatorEnumerator.Dispose();
				this.m_operatorEnumerator = null;
			}

			// Token: 0x06001DFD RID: 7677 RVA: 0x0006B599 File Offset: 0x00069799
			public void Reset()
			{
				this.m_operatorEnumerator.Reset();
			}

			// Token: 0x040011A9 RID: 4521
			private QueryOperatorEnumerator<TElement, TKey> m_operatorEnumerator;

			// Token: 0x040011AA RID: 4522
			private TElement m_current;
		}
	}
}
