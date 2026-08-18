using System;

namespace System.Web.Mvc
{
	// Token: 0x0200011D RID: 285
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class NoAsyncTimeoutAttribute : AsyncTimeoutAttribute
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x000144B6 File Offset: 0x000126B6
		public NoAsyncTimeoutAttribute() : base(-1)
		{
		}
	}
}
