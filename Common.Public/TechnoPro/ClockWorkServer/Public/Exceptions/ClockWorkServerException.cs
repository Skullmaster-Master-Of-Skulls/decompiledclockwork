using System;

namespace TechnoPro.ClockWorkServer.Public.Exceptions
{
	// Token: 0x020000B7 RID: 183
	public class ClockWorkServerException : Exception
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000D70E File Offset: 0x0000B90E
		public ClockWorkServerException()
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000D718 File Offset: 0x0000B918
		public ClockWorkServerException(string message) : base(message)
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000D723 File Offset: 0x0000B923
		public ClockWorkServerException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
