using System;

namespace TechnoPro.Common.DAO.FileSign.Impl
{
	// Token: 0x02000002 RID: 2
	public class DecryptAndVerifyFailedException : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DecryptAndVerifyFailedException()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public DecryptAndVerifyFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public DecryptAndVerifyFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
