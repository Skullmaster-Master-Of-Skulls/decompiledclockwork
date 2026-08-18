using System;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000005 RID: 5
	public interface IIdentityFactoryProvider<T> where T : IDisposable
	{
		// Token: 0x06000012 RID: 18
		T Create(IdentityFactoryOptions<T> options, IOwinContext context);

		// Token: 0x06000013 RID: 19
		void Dispose(IdentityFactoryOptions<T> options, T instance);
	}
}
