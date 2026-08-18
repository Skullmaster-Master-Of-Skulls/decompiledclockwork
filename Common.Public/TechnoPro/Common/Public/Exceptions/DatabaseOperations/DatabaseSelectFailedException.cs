using System;

namespace TechnoPro.Common.Public.Exceptions.DatabaseOperations
{
	// Token: 0x020000D8 RID: 216
	public class DatabaseSelectFailedException : Exception
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x0000D70E File Offset: 0x0000B90E
		public DatabaseSelectFailedException()
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000D718 File Offset: 0x0000B918
		public DatabaseSelectFailedException(string message) : base(message)
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000D723 File Offset: 0x0000B923
		public DatabaseSelectFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
