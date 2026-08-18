using System;

namespace TechnoPro.Common.Public.Exceptions.InvalidParameters
{
	// Token: 0x020000D5 RID: 213
	public class NullParameterException : Exception
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000D70E File Offset: 0x0000B90E
		public NullParameterException()
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D718 File Offset: 0x0000B918
		public NullParameterException(string message) : base(message)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D723 File Offset: 0x0000B923
		public NullParameterException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
