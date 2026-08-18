using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.ICore.Authentication
{
	// Token: 0x020000DA RID: 218
	public interface ICASAuthManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006D3 RID: 1747
		CASAuthenticationResult AuthenticateCAS(CASAuthenticationOptions AuthenticationOptions, string ticket);

		// Token: 0x060006D4 RID: 1748
		CASAuthenticationResult AuthenticateCAS(string ticket);
	}
}
