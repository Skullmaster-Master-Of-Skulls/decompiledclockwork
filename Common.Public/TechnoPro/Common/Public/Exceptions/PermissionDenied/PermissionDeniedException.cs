using System;

namespace TechnoPro.Common.Public.Exceptions.PermissionDenied
{
	// Token: 0x020000CF RID: 207
	public class PermissionDeniedException : Exception
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0000D70E File Offset: 0x0000B90E
		public PermissionDeniedException()
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000D718 File Offset: 0x0000B918
		public PermissionDeniedException(string message) : base(message)
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000D723 File Offset: 0x0000B923
		public PermissionDeniedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
