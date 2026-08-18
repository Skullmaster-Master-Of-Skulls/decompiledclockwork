using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200020C RID: 524
	internal static class ReadOnlyCollectionExtensions
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0003A9D0 File Offset: 0x00038BD0
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

		// Token: 0x02000421 RID: 1057
		private static class DefaultReadOnlyCollection<T>
		{
			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0006DEC5 File Offset: 0x0006C0C5
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

			// Token: 0x04001289 RID: 4745
			private static volatile ReadOnlyCollection<T> _defaultCollection;
		}
	}
}
