using System;

namespace TechnoPro.Common.Public.Exceptions.DatabaseOperations
{
	// Token: 0x020000D6 RID: 214
	public class DatabaseDeleteFailedException : Exception
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000D70E File Offset: 0x0000B90E
		public DatabaseDeleteFailedException()
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D718 File Offset: 0x0000B918
		public DatabaseDeleteFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000D723 File Offset: 0x0000B923
		public DatabaseDeleteFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
