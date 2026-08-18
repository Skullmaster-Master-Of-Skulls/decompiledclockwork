using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D4E RID: 3406
	internal class ReadOnlyList<V, T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable where V : T
	{
		// Token: 0x06007EFD RID: 32509 RVA: 0x001D0F00 File Offset: 0x001CF100
		public ReadOnlyList(IList<V> source)
		{
			this.source = source;
		}

		// Token: 0x1700287D RID: 10365
		// (get) Token: 0x06007EFE RID: 32510 RVA: 0x001D0F0F File Offset: 0x001CF10F
		public int Count
		{
			get
			{
				return this.source.Count;
			}
		}

		// Token: 0x1700287E RID: 10366
		public T this[int index]
		{
			get
			{
				return (T)((object)this.source[index]);
			}
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x001D0F34 File Offset: 0x001CF134
		public IEnumerator<T> GetEnumerator()
		{
			return this.source.GetEnumerator() as IEnumerator<T>;
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x001D0F53 File Offset: 0x001CF153
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.source.GetEnumerator();
		}

		// Token: 0x040022FB RID: 8955
		private IList<V> source;
	}
}
