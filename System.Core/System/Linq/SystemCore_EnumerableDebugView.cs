using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x0200015E RID: 350
	internal sealed class SystemCore_EnumerableDebugView<T>
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002D274 File Offset: 0x0002B474
		public SystemCore_EnumerableDebugView(IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			this.enumerable = enumerable;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002D294 File Offset: 0x0002B494
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				List<T> list = new List<T>();
				IEnumerator<T> enumerator = this.enumerable.GetEnumerator();
				if (enumerator != null)
				{
					this.count = 0;
					while (enumerator.MoveNext())
					{
						T item = enumerator.Current;
						list.Add(item);
						this.count++;
					}
				}
				if (this.count == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				this.cachedCollection = new T[this.count];
				list.CopyTo(this.cachedCollection, 0);
				return this.cachedCollection;
			}
		}

		// Token: 0x0400079A RID: 1946
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<T> enumerable;

		// Token: 0x0400079B RID: 1947
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T[] cachedCollection;

		// Token: 0x0400079C RID: 1948
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int count;
	}
}
