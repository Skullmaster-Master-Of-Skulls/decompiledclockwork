using System;

namespace TechnoPro.Common.Public.Exceptions.InvalidParameters
{
	// Token: 0x020000D4 RID: 212
	public class NullOrInvalidIdParameterException : Exception
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x0000D70E File Offset: 0x0000B90E
		public NullOrInvalidIdParameterException()
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000D718 File Offset: 0x0000B918
		public NullOrInvalidIdParameterException(string message) : base(message)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000D723 File Offset: 0x0000B923
		public NullOrInvalidIdParameterException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
