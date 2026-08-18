using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200053F RID: 1343
	public class SignatureException : GeneralSecurityException
	{
		// Token: 0x06002E30 RID: 11824 RVA: 0x0011D79C File Offset: 0x0011C79C
		public SignatureException()
		{
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x0011D7A4 File Offset: 0x0011C7A4
		public SignatureException(string message) : base(message)
		{
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0011D7AD File Offset: 0x0011C7AD
		public SignatureException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
