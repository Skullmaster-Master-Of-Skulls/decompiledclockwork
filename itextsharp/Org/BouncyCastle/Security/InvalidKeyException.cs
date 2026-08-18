using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020005FF RID: 1535
	public class InvalidKeyException : KeyException
	{
		// Token: 0x06003459 RID: 13401 RVA: 0x001454B7 File Offset: 0x001444B7
		public InvalidKeyException()
		{
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x001454BF File Offset: 0x001444BF
		public InvalidKeyException(string message) : base(message)
		{
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x001454C8 File Offset: 0x001444C8
		public InvalidKeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
