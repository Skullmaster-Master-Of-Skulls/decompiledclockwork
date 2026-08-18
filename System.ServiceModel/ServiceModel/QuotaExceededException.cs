using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x0200004D RID: 77
	[__DynamicallyInvokable]
	[Serializable]
	public class QuotaExceededException : SystemException
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000AED6 File Offset: 0x000090D6
		public QuotaExceededException()
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AEDE File Offset: 0x000090DE
		[__DynamicallyInvokable]
		public QuotaExceededException(string message) : base(message)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AEE7 File Offset: 0x000090E7
		[__DynamicallyInvokable]
		public QuotaExceededException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AEF1 File Offset: 0x000090F1
		protected QuotaExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
