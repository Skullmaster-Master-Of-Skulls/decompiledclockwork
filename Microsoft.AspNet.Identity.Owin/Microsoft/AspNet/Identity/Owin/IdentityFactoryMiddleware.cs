using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000003 RID: 3
	public class IdentityFactoryMiddleware<TResult, TOptions> : OwinMiddleware where TResult : IDisposable where TOptions : IdentityFactoryOptions<TResult>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002437 File Offset: 0x00000637
		public IdentityFactoryMiddleware(OwinMiddleware next, TOptions options) : base(next)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (options.Provider == null)
			{
				throw new ArgumentNullException("options.Provider");
			}
			this.Options = options;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002474 File Offset: 0x00000674
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000247C File Offset: 0x0000067C
		public TOptions Options { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x0000262C File Offset: 0x0000082C
		public override async Task Invoke(IOwinContext context)
		{
			TOptions options = this.Options;
			TResult instance = options.Provider.Create(this.Options, context);
			try
			{
				context.Set(instance);
				if (base.Next != null)
				{
					await base.Next.Invoke(context);
				}
			}
			finally
			{
				TOptions options2 = this.Options;
				options2.Provider.Dispose(this.Options, instance);
			}
		}
	}
}
