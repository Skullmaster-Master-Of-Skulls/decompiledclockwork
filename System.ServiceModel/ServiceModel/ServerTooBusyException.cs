using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000032 RID: 50
	[__DynamicallyInvokable]
	[Serializable]
	public class ServerTooBusyException : CommunicationException
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00008CF0 File Offset: 0x00006EF0
		public ServerTooBusyException()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008CF8 File Offset: 0x00006EF8
		[__DynamicallyInvokable]
		public ServerTooBusyException(string message) : base(message)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008D01 File Offset: 0x00006F01
		[__DynamicallyInvokable]
		public ServerTooBusyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008D0B File Offset: 0x00006F0B
		protected ServerTooBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
