using System;

namespace TechnoPro.Common.Public.Exceptions.InvalidParameters
{
	// Token: 0x020000D3 RID: 211
	public class InvalidParameterIdException : Exception
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x0000D70E File Offset: 0x0000B90E
		public InvalidParameterIdException()
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000D718 File Offset: 0x0000B918
		public InvalidParameterIdException(string message) : base(message)
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000D723 File Offset: 0x0000B923
		public InvalidParameterIdException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
