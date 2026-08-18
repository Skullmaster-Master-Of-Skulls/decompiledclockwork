using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001DA RID: 474
	internal class GroupByGrouping<TGroupKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x00036F44 File Offset: 0x00035144
		internal GroupByGrouping(KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> keyValues)
		{
			this.m_keyValues = keyValues;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00036F53 File Offset: 0x00035153
		TGroupKey IGrouping<!0, !1>.Key
		{
			get
			{
				return this.m_keyValues.Key.Value;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00036F65 File Offset: 0x00035165
		IEnumerator<TElement> IEnumerable<!1>.GetEnumerator()
		{
			return this.m_keyValues.Value.GetEnumerator();
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00036F77 File Offset: 0x00035177
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TElement>)this).GetEnumerator();
		}

		// Token: 0x040008D8 RID: 2264
		private KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> m_keyValues;
	}
}
