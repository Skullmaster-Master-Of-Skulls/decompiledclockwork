using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020004B8 RID: 1208
	public class PgpKeyValidationException : PgpException
	{
		// Token: 0x060028E8 RID: 10472 RVA: 0x000F87C7 File Offset: 0x000F77C7
		public PgpKeyValidationException()
		{
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000F87CF File Offset: 0x000F77CF
		public PgpKeyValidationException(string message) : base(message)
		{
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000F87D8 File Offset: 0x000F77D8
		public PgpKeyValidationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
