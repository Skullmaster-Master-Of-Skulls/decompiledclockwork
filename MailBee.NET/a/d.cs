using System;
using MailBee;

namespace a
{
	// Token: 0x0200048D RID: 1165
	internal class d : a6
	{
		// Token: 0x0600281D RID: 10269 RVA: 0x000BAE68 File Offset: 0x000B9E68
		public override string GetSaslID()
		{
			return "MSN";
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000BAE6F File Offset: 0x000B9E6F
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslMsn;
		}
	}
}
