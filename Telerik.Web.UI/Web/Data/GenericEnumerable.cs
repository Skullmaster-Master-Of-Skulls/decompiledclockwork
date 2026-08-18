using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Data
{
	// Token: 0x02001B89 RID: 7049
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class GenericEnumerable<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06011142 RID: 69954 RVA: 0x003C4636 File Offset: 0x003C2836
		public GenericEnumerable(IEnumerable source)
		{
			this.source = source;
		}

		// Token: 0x06011143 RID: 69955 RVA: 0x003C4645 File Offset: 0x003C2845
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.source.GetEnumerator();
		}

		// Token: 0x06011144 RID: 69956 RVA: 0x003C47A4 File Offset: 0x003C29A4
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			foreach (object obj in this.source)
			{
				T item = (T)((object)obj);
				yield return item;
			}
			yield break;
		}

		// Token: 0x04004C6E RID: 19566
		private readonly IEnumerable source;
	}
}
