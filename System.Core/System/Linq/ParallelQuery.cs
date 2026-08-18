using System;
using System.Collections;
using System.Linq.Parallel;

namespace System.Linq
{
	// Token: 0x02000170 RID: 368
	[__DynamicallyInvokable]
	public class ParallelQuery : IEnumerable
	{
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00030A29 File Offset: 0x0002EC29
		internal ParallelQuery(QuerySettings specifiedSettings)
		{
			this.m_specifiedSettings = specifiedSettings;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00030A38 File Offset: 0x0002EC38
		internal QuerySettings SpecifiedQuerySettings
		{
			get
			{
				return this.m_specifiedSettings;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00030A40 File Offset: 0x0002EC40
		internal virtual ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00030A47 File Offset: 0x0002EC47
		internal virtual ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00030A4E File Offset: 0x0002EC4E
		internal virtual IEnumerator GetEnumeratorUntyped()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00030A55 File Offset: 0x0002EC55
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorUntyped();
		}

		// Token: 0x040007B1 RID: 1969
		private QuerySettings m_specifiedSettings;
	}
}
