using System;
using System.Runtime.Serialization;

namespace NCrontab
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class CrontabException : Exception
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		public CrontabException() : base("Crontab error.")
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002195 File Offset: 0x00000395
		public CrontabException(string message) : base(message)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000219E File Offset: 0x0000039E
		public CrontabException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		protected CrontabException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
