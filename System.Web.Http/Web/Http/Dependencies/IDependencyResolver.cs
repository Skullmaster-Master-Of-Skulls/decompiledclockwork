using System;

namespace System.Web.Http.Dependencies
{
	// Token: 0x02000119 RID: 281
	public interface IDependencyResolver : IDependencyScope, IDisposable
	{
		// Token: 0x060006CD RID: 1741
		IDependencyScope BeginScope();
	}
}
