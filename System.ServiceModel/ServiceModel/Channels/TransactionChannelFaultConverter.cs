using System;
using System.Globalization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A62 RID: 2658
	internal class TransactionChannelFaultConverter<TChannel> : FaultConverter where TChannel : class, IChannel
	{
		// Token: 0x060068FF RID: 26879 RVA: 0x0018860D File Offset: 0x0018680D
		internal TransactionChannelFaultConverter(TransactionChannel<TChannel> channel)
		{
			this.channel = channel;
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x0018861C File Offset: 0x0018681C
		protected override bool OnTryCreateException(Message message, MessageFault fault, out Exception exception)
		{
			if (message.Headers.Action == "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault")
			{
				exception = new ProtocolException(fault.Reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text);
				return true;
			}
			if (fault.IsMustUnderstandFault)
			{
				MessageHeader emptyTransactionHeader = this.channel.Formatter.EmptyTransactionHeader;
				if (MessageFault.WasHeaderNotUnderstood(message.Headers, emptyTransactionHeader.Name, emptyTransactionHeader.Namespace))
				{
					exception = new ProtocolException(SR.GetString("SFxTransactionHeaderNotUnderstood", new object[]
					{
						emptyTransactionHeader.Name,
						emptyTransactionHeader.Namespace,
						this.channel.Protocol
					}));
					return true;
				}
			}
			FaultConverter innerProperty = this.channel.GetInnerProperty<FaultConverter>();
			if (innerProperty != null)
			{
				return innerProperty.TryCreateException(message, fault, out exception);
			}
			exception = null;
			return false;
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x001886E8 File Offset: 0x001868E8
		protected override bool OnTryCreateFaultMessage(Exception exception, out Message message)
		{
			FaultConverter innerProperty = this.channel.GetInnerProperty<FaultConverter>();
			if (innerProperty != null)
			{
				return innerProperty.TryCreateFaultMessage(exception, out message);
			}
			message = null;
			return false;
		}

		// Token: 0x04003C22 RID: 15394
		private TransactionChannel<TChannel> channel;
	}
}
