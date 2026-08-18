using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200017E RID: 382
	public class InvalidParameterException : KeyException
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x000566D4 File Offset: 0x000556D4
		public InvalidParameterException()
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000566DC File Offset: 0x000556DC
		public InvalidParameterException(string message) : base(message)
		{
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000566E5 File Offset: 0x000556E5
		public InvalidParameterException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
