using System;
using System.Collections;
using System.Collections.Generic;

namespace Facet.Combinatorics
{
	// Token: 0x02000003 RID: 3
	internal interface IMetaCollection<T> : IEnumerable<IList<T>>, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		long Count { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		GenerateOption Type { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		int UpperIndex { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		int LowerIndex { get; }
	}
}
