using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.ICore.Authentication
{
	// Token: 0x020000DC RID: 220
	public interface IHashingAuthenticationManager : IBaseOperationContext<HashingOperationContext>
	{
		// Token: 0x060006DF RID: 1759
		bool ValidateClockWorkHash(ClockWorkHashAuthentication hashAuth);

		// Token: 0x060006E0 RID: 1760
		bool ValidateHash(eHashingType hashingType, HashAuthentication hashAuth);
	}
}
