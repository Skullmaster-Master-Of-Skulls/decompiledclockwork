using System;
using MailBee;

namespace a
{
	// Token: 0x0200048A RID: 1162
	internal class ba : bi
	{
		// Token: 0x06002808 RID: 10248 RVA: 0x000BA3CC File Offset: 0x000B93CC
		public override string GetSaslID()
		{
			return "MSN";
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000BA3D3 File Offset: 0x000B93D3
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslMsn;
		}
	}
}
