using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000033 RID: 51
	[__DynamicallyInvokable]
	[Serializable]
	public class ServiceActivationException : CommunicationException
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00008D15 File Offset: 0x00006F15
		public ServiceActivationException()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008D1D File Offset: 0x00006F1D
		[__DynamicallyInvokable]
		public ServiceActivationException(string message) : base(message)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008D26 File Offset: 0x00006F26
		[__DynamicallyInvokable]
		public ServiceActivationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008D30 File Offset: 0x00006F30
		protected ServiceActivationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
