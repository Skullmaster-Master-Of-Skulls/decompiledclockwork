using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x0200002A RID: 42
	[__DynamicallyInvokable]
	[Serializable]
	public class CommunicationObjectAbortedException : CommunicationException
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00008A20 File Offset: 0x00006C20
		[__DynamicallyInvokable]
		public CommunicationObjectAbortedException()
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008A28 File Offset: 0x00006C28
		[__DynamicallyInvokable]
		public CommunicationObjectAbortedException(string message) : base(message)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008A31 File Offset: 0x00006C31
		[__DynamicallyInvokable]
		public CommunicationObjectAbortedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008A3B File Offset: 0x00006C3B
		protected CommunicationObjectAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
