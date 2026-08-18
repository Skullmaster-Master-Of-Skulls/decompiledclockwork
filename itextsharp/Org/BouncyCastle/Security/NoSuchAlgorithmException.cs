using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200017C RID: 380
	[Obsolete("Never thrown")]
	public class NoSuchAlgorithmException : GeneralSecurityException
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005669E File Offset: 0x0005569E
		public NoSuchAlgorithmException()
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000566A6 File Offset: 0x000556A6
		public NoSuchAlgorithmException(string message) : base(message)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000566AF File Offset: 0x000556AF
		public NoSuchAlgorithmException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
