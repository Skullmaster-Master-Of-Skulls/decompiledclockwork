using System;

namespace TechnoPro.Common.Public.Exceptions.DatabaseOperations
{
	// Token: 0x020000D7 RID: 215
	public class DatabaseInsertFailedException : Exception
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0000D70E File Offset: 0x0000B90E
		public DatabaseInsertFailedException()
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000D718 File Offset: 0x0000B918
		public DatabaseInsertFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000D723 File Offset: 0x0000B923
		public DatabaseInsertFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
