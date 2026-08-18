using System;
using System.Collections;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E6 RID: 1254
	internal interface IBaseList<T> : IList, ICollection, IEnumerable
	{
		// Token: 0x170006E8 RID: 1768
		T this[string identity]
		{
			get;
		}

		// Token: 0x170006E9 RID: 1769
		T this[int index]
		{
			get;
		}

		// Token: 0x06002EA7 RID: 11943
		int IndexOf(T item);
	}
}
