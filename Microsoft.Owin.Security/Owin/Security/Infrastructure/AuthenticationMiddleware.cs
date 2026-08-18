using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000020 RID: 32
	public abstract class AuthenticationMiddleware<TOptions> : OwinMiddleware where TOptions : AuthenticationOptions
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003E47 File Offset: 0x00002047
		protected AuthenticationMiddleware(OwinMiddleware next, TOptions options) : base(next)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.Options = options;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003E6A File Offset: 0x0000206A
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003E72 File Offset: 0x00002072
		public TOptions Options { get; set; }

		// Token: 0x06000088 RID: 136 RVA: 0x000040E0 File Offset: 0x000022E0
		public override async Task Invoke(IOwinContext context)
		{
			AuthenticationHandler<TOptions> handler = this.CreateHandler();
			await handler.Initialize(this.Options, context);
			if (!(await handler.InvokeAsync()))
			{
				await base.Next.Invoke(context);
			}
			await handler.TeardownAsync();
		}

		// Token: 0x06000089 RID: 137
		protected abstract AuthenticationHandler<TOptions> CreateHandler();
	}
}
