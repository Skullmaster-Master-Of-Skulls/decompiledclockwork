using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000157 RID: 343
	internal class GroupedEnumerable<TSource, TKey, TElement, TResult> : IEnumerable<!3>, IEnumerable
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			this.source = source;
			this.keySelector = keySelector;
			this.elementSelector = elementSelector;
			this.comparer = comparer;
			this.resultSelector = resultSelector;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002CE08 File Offset: 0x0002B008
		public IEnumerator<TResult> GetEnumerator()
		{
			Lookup<TKey, TElement> lookup = Lookup<TKey, TElement>.Create<TSource>(this.source, this.keySelector, this.elementSelector, this.comparer);
			return lookup.ApplyResultSelector<TResult>(this.resultSelector).GetEnumerator();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002CE44 File Offset: 0x0002B044
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000785 RID: 1925
		private IEnumerable<TSource> source;

		// Token: 0x04000786 RID: 1926
		private Func<TSource, TKey> keySelector;

		// Token: 0x04000787 RID: 1927
		private Func<TSource, TElement> elementSelector;

		// Token: 0x04000788 RID: 1928
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04000789 RID: 1929
		private Func<TKey, IEnumerable<TElement>, TResult> resultSelector;
	}
}
