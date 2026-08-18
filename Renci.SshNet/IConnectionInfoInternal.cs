using System;
using System.Collections.Generic;
using Renci.SshNet.Messages.Authentication;

namespace Renci.SshNet
{
	// Token: 0x02000010 RID: 16
	internal interface IConnectionInfoInternal : IConnectionInfo
	{
		// Token: 0x060000BB RID: 187
		void UserAuthenticationBannerReceived(object sender, MessageEventArgs<BannerMessage> e);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BC RID: 188
		IList<IAuthenticationMethod> AuthenticationMethods { get; }

		// Token: 0x060000BD RID: 189
		IAuthenticationMethod CreateNoneAuthenticationMethod();
	}
}
