using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C7 RID: 199
	public class EmailFailedException : Exception
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D70E File Offset: 0x0000B90E
		public EmailFailedException()
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D718 File Offset: 0x0000B918
		public EmailFailedException(string message) : base(message)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D723 File Offset: 0x0000B923
		public EmailFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
