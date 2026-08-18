using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000CA RID: 202
	public class InvalidUserCalendarException : Exception
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0000D70E File Offset: 0x0000B90E
		public InvalidUserCalendarException()
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000D718 File Offset: 0x0000B918
		public InvalidUserCalendarException(string msg) : base(msg)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D723 File Offset: 0x0000B923
		public InvalidUserCalendarException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
