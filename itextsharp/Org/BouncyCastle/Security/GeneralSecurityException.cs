using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000107 RID: 263
	public class GeneralSecurityException : Exception
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00036BA4 File Offset: 0x00035BA4
		public GeneralSecurityException()
		{
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00036BAC File Offset: 0x00035BAC
		public GeneralSecurityException(string message) : base(message)
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00036BB5 File Offset: 0x00035BB5
		public GeneralSecurityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
