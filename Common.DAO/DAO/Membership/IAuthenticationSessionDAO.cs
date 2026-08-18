using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.DAO.Membership
{
	// Token: 0x0200004F RID: 79
	public interface IAuthenticationSessionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B5 RID: 437
		void SaveSession(AuthenticationSession authSession);

		// Token: 0x060001B6 RID: 438
		void DeleteSession(string guid);

		// Token: 0x060001B7 RID: 439
		void UpdateClientParameters(string guid, ClientParameters clientParameters);

		// Token: 0x060001B8 RID: 440
		IList<AuthenticationSession> GetAllSessions();
	}
}
