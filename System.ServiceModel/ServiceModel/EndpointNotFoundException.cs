using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x0200002D RID: 45
	[__DynamicallyInvokable]
	[Serializable]
	public class EndpointNotFoundException : CommunicationException
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00008A6A File Offset: 0x00006C6A
		public EndpointNotFoundException()
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008A72 File Offset: 0x00006C72
		[__DynamicallyInvokable]
		public EndpointNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008A7B File Offset: 0x00006C7B
		[__DynamicallyInvokable]
		public EndpointNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008A85 File Offset: 0x00006C85
		protected EndpointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
