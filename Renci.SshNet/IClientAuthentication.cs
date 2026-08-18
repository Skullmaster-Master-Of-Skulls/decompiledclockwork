using System;

namespace Renci.SshNet
{
	// Token: 0x0200000F RID: 15
	internal interface IClientAuthentication
	{
		// Token: 0x060000BA RID: 186
		void Authenticate(IConnectionInfoInternal connectionInfo, ISession session);
	}
}
