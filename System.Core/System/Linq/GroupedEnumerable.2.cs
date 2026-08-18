using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000158 RID: 344
	internal class GroupedEnumerable<TSource, TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0002CE4C File Offset: 0x0002B04C
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			this.source = source;
			this.keySelector = keySelector;
			this.elementSelector = elementSelector;
			this.comparer = comparer;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002CEA6 File Offset: 0x0002B0A6
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this.source, this.keySelector, this.elementSelector, this.comparer).GetEnumerator();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002CECA File Offset: 0x0002B0CA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400078A RID: 1930
		private IEnumerable<TSource> source;

		// Token: 0x0400078B RID: 1931
		private Func<TSource, TKey> keySelector;

		// Token: 0x0400078C RID: 1932
		private Func<TSource, TElement> elementSelector;

		// Token: 0x0400078D RID: 1933
		private IEqualityComparer<TKey> comparer;
	}
}
