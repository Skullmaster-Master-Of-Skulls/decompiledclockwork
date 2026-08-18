using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000007 RID: 7
	public interface IIdentityMessageService
	{
		// Token: 0x06000013 RID: 19
		Task SendAsync(IdentityMessage message);
	}
}
