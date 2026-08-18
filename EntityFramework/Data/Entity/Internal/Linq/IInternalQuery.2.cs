using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200078E RID: 1934
	internal interface IInternalQuery<out TElement> : IInternalQuery
	{
		// Token: 0x0600579B RID: 22427
		IInternalQuery<TElement> Include(string path);

		// Token: 0x0600579C RID: 22428
		IInternalQuery<TElement> AsNoTracking();

		// Token: 0x0600579D RID: 22429
		IInternalQuery<TElement> AsStreaming();

		// Token: 0x0600579E RID: 22430
		IInternalQuery<TElement> WithExecutionStrategy(IDbExecutionStrategy executionStrategy);

		// Token: 0x0600579F RID: 22431
		IDbAsyncEnumerator<TElement> GetAsyncEnumerator();

		// Token: 0x060057A0 RID: 22432
		IEnumerator<TElement> GetEnumerator();
	}
}
