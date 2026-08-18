using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Mail
{
	// Token: 0x02000267 RID: 615
	internal interface ISmtpAuthenticationModule
	{
		// Token: 0x06001726 RID: 5926
		Authorization Authenticate(string challenge, NetworkCredential credentials, object sessionCookie, string spn, ChannelBinding channelBindingToken);

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001727 RID: 5927
		string AuthenticationType { get; }

		// Token: 0x06001728 RID: 5928
		void CloseContext(object sessionCookie);
	}
}
