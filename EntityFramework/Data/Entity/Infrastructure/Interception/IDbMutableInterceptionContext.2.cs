using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000169 RID: 361
	internal interface IDbMutableInterceptionContext<TResult> : IDbMutableInterceptionContext
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000B9D RID: 2973
		InterceptionContextMutableData<TResult> MutableData { get; }
	}
}
