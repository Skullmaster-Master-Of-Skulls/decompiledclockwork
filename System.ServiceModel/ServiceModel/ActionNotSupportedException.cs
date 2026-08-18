using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000024 RID: 36
	[__DynamicallyInvokable]
	[Serializable]
	public class ActionNotSupportedException : CommunicationException
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000088C3 File Offset: 0x00006AC3
		[__DynamicallyInvokable]
		public ActionNotSupportedException()
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000088CB File Offset: 0x00006ACB
		[__DynamicallyInvokable]
		public ActionNotSupportedException(string message) : base(message)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000088D4 File Offset: 0x00006AD4
		[__DynamicallyInvokable]
		public ActionNotSupportedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000088DE File Offset: 0x00006ADE
		protected ActionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000088E8 File Offset: 0x00006AE8
		internal Message ProvideFault(MessageVersion messageVersion)
		{
			FaultCode faultCode = FaultCode.CreateSenderFaultCode("ActionNotSupported", messageVersion.Addressing.Namespace);
			string message = this.Message;
			return System.ServiceModel.Channels.Message.CreateMessage(messageVersion, faultCode, message, messageVersion.Addressing.FaultAction);
		}
	}
}
