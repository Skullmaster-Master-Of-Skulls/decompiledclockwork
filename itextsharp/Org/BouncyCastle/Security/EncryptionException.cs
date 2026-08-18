using System;
using System.IO;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200000B RID: 11
	public class EncryptionException : IOException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004240 File Offset: 0x00003240
		public EncryptionException(string message) : base(message)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004249 File Offset: 0x00003249
		public EncryptionException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
