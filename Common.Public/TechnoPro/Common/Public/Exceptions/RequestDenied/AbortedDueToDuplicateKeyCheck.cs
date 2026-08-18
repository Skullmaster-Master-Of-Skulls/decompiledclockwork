using System;

namespace TechnoPro.Common.Public.Exceptions.RequestDenied
{
	// Token: 0x020000D0 RID: 208
	public class AbortedDueToDuplicateKeyCheck : Exception
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000D70E File Offset: 0x0000B90E
		public AbortedDueToDuplicateKeyCheck()
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000D718 File Offset: 0x0000B918
		public AbortedDueToDuplicateKeyCheck(string message) : base(message)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000D723 File Offset: 0x0000B923
		public AbortedDueToDuplicateKeyCheck(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
