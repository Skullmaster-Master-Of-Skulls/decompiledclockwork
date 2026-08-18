using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x0200002B RID: 43
	[__DynamicallyInvokable]
	[Serializable]
	public class CommunicationObjectFaultedException : CommunicationException
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00008A45 File Offset: 0x00006C45
		[__DynamicallyInvokable]
		public CommunicationObjectFaultedException()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008A4D File Offset: 0x00006C4D
		[__DynamicallyInvokable]
		public CommunicationObjectFaultedException(string message) : base(message)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008A56 File Offset: 0x00006C56
		[__DynamicallyInvokable]
		public CommunicationObjectFaultedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008A60 File Offset: 0x00006C60
		protected CommunicationObjectFaultedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
