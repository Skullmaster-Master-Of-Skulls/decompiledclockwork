using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020004B5 RID: 1205
	public class CrlException : GeneralSecurityException
	{
		// Token: 0x060028C4 RID: 10436 RVA: 0x000F7E76 File Offset: 0x000F6E76
		public CrlException()
		{
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000F7E7E File Offset: 0x000F6E7E
		public CrlException(string msg) : base(msg)
		{
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000F7E87 File Offset: 0x000F6E87
		public CrlException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
