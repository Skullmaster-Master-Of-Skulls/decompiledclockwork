using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000296 RID: 662
	public interface IDbAsyncEnumerable<out T> : IDbAsyncEnumerable
	{
		// Token: 0x06001775 RID: 6005
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IDbAsyncEnumerator<T> GetAsyncEnumerator();
	}
}
