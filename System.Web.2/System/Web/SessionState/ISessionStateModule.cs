using System;
using System.Threading.Tasks;

namespace System.Web.SessionState
{
	// Token: 0x02000125 RID: 293
	public interface ISessionStateModule : IHttpModule
	{
		// Token: 0x06001197 RID: 4503
		void ReleaseSessionState(HttpContext context);

		// Token: 0x06001198 RID: 4504
		Task ReleaseSessionStateAsync(HttpContext context);
	}
}
