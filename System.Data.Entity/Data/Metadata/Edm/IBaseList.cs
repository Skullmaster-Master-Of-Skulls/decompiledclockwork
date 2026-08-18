using System;
using System.Collections;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DA RID: 474
	internal interface IBaseList<T> : IList, ICollection, IEnumerable
	{
		// Token: 0x17000671 RID: 1649
		T this[string identity]
		{
			get;
		}

		// Token: 0x17000672 RID: 1650
		T this[int index]
		{
			get;
		}

		// Token: 0x0600200D RID: 8205
		int IndexOf(T item);
	}
}
