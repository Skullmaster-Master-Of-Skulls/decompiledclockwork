using System;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000006 RID: 6
	public class IdentityFactoryProvider<T> : IIdentityFactoryProvider<T> where T : class, IDisposable
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000026C0 File Offset: 0x000008C0
		public IdentityFactoryProvider()
		{
			this.OnDispose = delegate(IdentityFactoryOptions<T> options, T instance)
			{
			};
			this.OnCreate = ((IdentityFactoryOptions<T> options, IOwinContext context) => default(T));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002719 File Offset: 0x00000919
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002721 File Offset: 0x00000921
		public Func<IdentityFactoryOptions<T>, IOwinContext, T> OnCreate { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000272A File Offset: 0x0000092A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002732 File Offset: 0x00000932
		public Action<IdentityFactoryOptions<T>, T> OnDispose { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x0000273B File Offset: 0x0000093B
		public virtual T Create(IdentityFactoryOptions<T> options, IOwinContext context)
		{
			return this.OnCreate(options, context);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000274A File Offset: 0x0000094A
		public virtual void Dispose(IdentityFactoryOptions<T> options, T instance)
		{
			this.OnDispose(options, instance);
		}
	}
}
