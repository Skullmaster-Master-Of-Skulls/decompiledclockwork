using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000294 RID: 660
	public interface IDbAsyncEnumerable
	{
		// Token: 0x06001716 RID: 5910
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IDbAsyncEnumerator GetAsyncEnumerator();
	}
}
