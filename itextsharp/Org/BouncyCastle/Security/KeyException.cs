using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200017D RID: 381
	public class KeyException : GeneralSecurityException
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x000566B9 File Offset: 0x000556B9
		public KeyException()
		{
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000566C1 File Offset: 0x000556C1
		public KeyException(string message) : base(message)
		{
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000566CA File Offset: 0x000556CA
		public KeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
