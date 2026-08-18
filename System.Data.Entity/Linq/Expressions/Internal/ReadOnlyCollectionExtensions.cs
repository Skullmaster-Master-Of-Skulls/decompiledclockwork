using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x02000008 RID: 8
	internal static class ReadOnlyCollectionExtensions
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000285C File Offset: 0x00000A5C
		internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
			{
				return ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>.Empty;
			}
			ReadOnlyCollection<T> readOnlyCollection = sequence as ReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			return new ReadOnlyCollection<T>(sequence.ToArray<T>());
		}

		// Token: 0x0200043D RID: 1085
		private static class DefaultReadOnlyCollection<T>
		{
			// Token: 0x17000AB2 RID: 2738
			// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000DDDEC File Offset: 0x000DBFEC
			internal static ReadOnlyCollection<T> Empty
			{
				get
				{
					if (ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection == null)
					{
						ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection = new ReadOnlyCollection<T>(new T[0]);
					}
					return ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection;
				}
			}

			// Token: 0x04001899 RID: 6297
			private static ReadOnlyCollection<T> _defaultCollection;
		}
	}
}
