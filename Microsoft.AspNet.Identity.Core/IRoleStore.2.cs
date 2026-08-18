using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000029 RID: 41
	public interface IRoleStore<TRole> : IRoleStore<TRole, string>, IDisposable where TRole : IRole<string>
	{
	}
}
