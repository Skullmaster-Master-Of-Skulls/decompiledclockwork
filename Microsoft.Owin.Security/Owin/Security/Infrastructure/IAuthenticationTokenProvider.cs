using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000017 RID: 23
	public interface IAuthenticationTokenProvider
	{
		// Token: 0x06000040 RID: 64
		void Create(AuthenticationTokenCreateContext context);

		// Token: 0x06000041 RID: 65
		Task CreateAsync(AuthenticationTokenCreateContext context);

		// Token: 0x06000042 RID: 66
		void Receive(AuthenticationTokenReceiveContext context);

		// Token: 0x06000043 RID: 67
		Task ReceiveAsync(AuthenticationTokenReceiveContext context);
	}
}
