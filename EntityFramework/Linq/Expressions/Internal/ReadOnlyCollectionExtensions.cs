using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x0200055C RID: 1372
	internal static class ReadOnlyCollectionExtensions
	{
		// Token: 0x06003523 RID: 13603 RVA: 0x000FAEFC File Offset: 0x000F90FC
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

		// Token: 0x0200055D RID: 1373
		private static class DefaultReadOnlyCollection<T>
		{
			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06003524 RID: 13604 RVA: 0x000FAF29 File Offset: 0x000F9129
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

			// Token: 0x040013E1 RID: 5089
			private static ReadOnlyCollection<T> _defaultCollection;
		}
	}
}
