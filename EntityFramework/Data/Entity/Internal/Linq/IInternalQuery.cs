using System;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200078D RID: 1933
	internal interface IInternalQuery
	{
		// Token: 0x06005793 RID: 22419
		void ResetQuery();

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005794 RID: 22420
		InternalContext InternalContext { get; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005795 RID: 22421
		ObjectQuery ObjectQuery { get; }

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005796 RID: 22422
		Type ElementType { get; }

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005797 RID: 22423
		Expression Expression { get; }

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005798 RID: 22424
		ObjectQueryProvider ObjectQueryProvider { get; }

		// Token: 0x06005799 RID: 22425
		IDbAsyncEnumerator GetAsyncEnumerator();

		// Token: 0x0600579A RID: 22426
		IEnumerator GetEnumerator();
	}
}
