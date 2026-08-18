using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200026B RID: 619
	internal class IntSortedMap<TKey> : Dictionary<TKey, int>
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0010439C File Offset: 0x0010259C
		public IntSortedMap()
		{
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x001043A4 File Offset: 0x001025A4
		public IntSortedMap(IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}

		// Token: 0x170003E6 RID: 998
		public new int this[TKey key]
		{
			get
			{
				int result;
				if (!base.TryGetValue(key, out result))
				{
					result = -1;
				}
				return result;
			}
			set
			{
				base[key] = value;
			}
		}
	}
}
