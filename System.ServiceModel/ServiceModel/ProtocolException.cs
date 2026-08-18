using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000031 RID: 49
	[__DynamicallyInvokable]
	[Serializable]
	public class ProtocolException : CommunicationException
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00008B53 File Offset: 0x00006D53
		public ProtocolException()
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008B5B File Offset: 0x00006D5B
		[__DynamicallyInvokable]
		public ProtocolException(string message) : base(message)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008B64 File Offset: 0x00006D64
		[__DynamicallyInvokable]
		public ProtocolException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008B6E File Offset: 0x00006D6E
		protected ProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008B78 File Offset: 0x00006D78
		internal static ProtocolException ReceiveShutdownReturnedNonNull(Message message)
		{
			if (message.IsFault)
			{
				try
				{
					MessageFault messageFault = MessageFault.CreateFault(message, 65536);
					FaultReasonText matchingTranslation = messageFault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture);
					string @string = SR.GetString("ReceiveShutdownReturnedFault", new object[]
					{
						matchingTranslation.Text
					});
					return new ProtocolException(@string);
				}
				catch (QuotaExceededException)
				{
					string string2 = SR.GetString("ReceiveShutdownReturnedLargeFault", new object[]
					{
						message.Headers.Action
					});
					return new ProtocolException(string2);
				}
			}
			string string3 = SR.GetString("ReceiveShutdownReturnedMessage", new object[]
			{
				message.Headers.Action
			});
			return new ProtocolException(string3);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008C34 File Offset: 0x00006E34
		internal static ProtocolException OneWayOperationReturnedNonNull(Message message)
		{
			if (message.IsFault)
			{
				try
				{
					MessageFault messageFault = MessageFault.CreateFault(message, 65536);
					FaultReasonText matchingTranslation = messageFault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture);
					string @string = SR.GetString("OneWayOperationReturnedFault", new object[]
					{
						matchingTranslation.Text
					});
					return new ProtocolException(@string);
				}
				catch (QuotaExceededException)
				{
					string string2 = SR.GetString("OneWayOperationReturnedLargeFault", new object[]
					{
						message.Headers.Action
					});
					return new ProtocolException(string2);
				}
			}
			string string3 = SR.GetString("OneWayOperationReturnedMessage", new object[]
			{
				message.Headers.Action
			});
			return new ProtocolException(string3);
		}
	}
}
