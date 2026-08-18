using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Mail
{
	// Token: 0x02000696 RID: 1686
	internal interface ISmtpAuthenticationModule
	{
		// Token: 0x06003408 RID: 13320
		Authorization Authenticate(string challenge, NetworkCredential credentials, object sessionCookie, string spn, ChannelBinding channelBindingToken);

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003409 RID: 13321
		string AuthenticationType { get; }

		// Token: 0x0600340A RID: 13322
		void CloseContext(object sessionCookie);
	}
}
