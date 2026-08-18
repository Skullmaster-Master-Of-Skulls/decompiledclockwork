using System;

namespace TechnoPro.Common.Public.Exceptions.InvalidParameters
{
	// Token: 0x020000D2 RID: 210
	public class InvalidParameterException : Exception
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0000D70E File Offset: 0x0000B90E
		public InvalidParameterException()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000D718 File Offset: 0x0000B918
		public InvalidParameterException(string message) : base(message)
		{
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000D723 File Offset: 0x0000B923
		public InvalidParameterException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
