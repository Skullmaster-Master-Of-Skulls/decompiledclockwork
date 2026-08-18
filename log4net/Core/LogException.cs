using System;
using System.Runtime.Serialization;

namespace log4net.Core
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class LogException : ApplicationException
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000055E8 File Offset: 0x000037E8
		public LogException()
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000055F0 File Offset: 0x000037F0
		public LogException(string message) : base(message)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000055F9 File Offset: 0x000037F9
		public LogException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005603 File Offset: 0x00003803
		protected LogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
