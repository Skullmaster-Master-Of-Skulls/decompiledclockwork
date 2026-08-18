using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C4 RID: 196
	public class ClockWorkServerNotConnectedException : Exception
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000D70E File Offset: 0x0000B90E
		public ClockWorkServerNotConnectedException()
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D718 File Offset: 0x0000B918
		public ClockWorkServerNotConnectedException(string msg) : base(msg)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000D723 File Offset: 0x0000B923
		public ClockWorkServerNotConnectedException(string msg, Exception innerException) : base(msg, innerException)
		{
		}
	}
}
