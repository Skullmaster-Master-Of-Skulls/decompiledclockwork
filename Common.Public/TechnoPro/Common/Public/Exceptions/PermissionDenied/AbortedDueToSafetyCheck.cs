using System;

namespace TechnoPro.Common.Public.Exceptions.PermissionDenied
{
	// Token: 0x020000CE RID: 206
	public class AbortedDueToSafetyCheck : Exception
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x0000D70E File Offset: 0x0000B90E
		public AbortedDueToSafetyCheck()
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000D718 File Offset: 0x0000B918
		public AbortedDueToSafetyCheck(string message) : base(message)
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000D723 File Offset: 0x0000B923
		public AbortedDueToSafetyCheck(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
