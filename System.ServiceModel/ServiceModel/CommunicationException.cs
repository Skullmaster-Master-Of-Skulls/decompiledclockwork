using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000029 RID: 41
	[__DynamicallyInvokable]
	[Serializable]
	public class CommunicationException : SystemException
	{
		// Token: 0x06000176 RID: 374 RVA: 0x000089FB File Offset: 0x00006BFB
		[__DynamicallyInvokable]
		public CommunicationException()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008A03 File Offset: 0x00006C03
		[__DynamicallyInvokable]
		public CommunicationException(string message) : base(message)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008A0C File Offset: 0x00006C0C
		[__DynamicallyInvokable]
		public CommunicationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008A16 File Offset: 0x00006C16
		protected CommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
