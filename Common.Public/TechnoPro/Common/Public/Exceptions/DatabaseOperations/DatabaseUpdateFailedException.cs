using System;

namespace TechnoPro.Common.Public.Exceptions.DatabaseOperations
{
	// Token: 0x020000D9 RID: 217
	public class DatabaseUpdateFailedException : Exception
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0000D70E File Offset: 0x0000B90E
		public DatabaseUpdateFailedException()
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000D718 File Offset: 0x0000B918
		public DatabaseUpdateFailedException(string message) : base(message)
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000D723 File Offset: 0x0000B923
		public DatabaseUpdateFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
