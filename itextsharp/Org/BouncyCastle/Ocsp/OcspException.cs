using System;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000503 RID: 1283
	public class OcspException : Exception
	{
		// Token: 0x06002BCF RID: 11215 RVA: 0x00108E29 File Offset: 0x00107E29
		public OcspException()
		{
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00108E31 File Offset: 0x00107E31
		public OcspException(string message) : base(message)
		{
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00108E3A File Offset: 0x00107E3A
		public OcspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
