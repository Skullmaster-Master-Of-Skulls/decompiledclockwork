using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001C RID: 28
	public abstract class AuthenticationHandler<TOptions> : AuthenticationHandler where TOptions : AuthenticationOptions
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003A99 File Offset: 0x00001C99
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003AA1 File Offset: 0x00001CA1
		private protected TOptions Options { protected get; private set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00003AAA File Offset: 0x00001CAA
		internal Task Initialize(TOptions options, IOwinContext context)
		{
			this.Options = options;
			return base.BaseInitializeAsync(options, context);
		}
	}
}
