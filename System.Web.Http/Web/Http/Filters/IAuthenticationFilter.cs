using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x02000073 RID: 115
	public interface IAuthenticationFilter : IFilter
	{
		// Token: 0x06000319 RID: 793
		Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken);

		// Token: 0x0600031A RID: 794
		Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken);
	}
}
