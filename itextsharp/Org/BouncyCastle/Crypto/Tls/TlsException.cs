using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042B RID: 1067
	public class TlsException : Exception
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x000DDE54 File Offset: 0x000DCE54
		public TlsException()
		{
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000DDE5C File Offset: 0x000DCE5C
		public TlsException(string message) : base(message)
		{
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000DDE65 File Offset: 0x000DCE65
		public TlsException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
