using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091C RID: 2332
	internal abstract class ReliableListenerOverDuplex<TChannel, TReliableChannel> : ReliableListenerOverDatagram<TChannel, TReliableChannel, IDuplexChannel, Message> where TChannel : class, IChannel where TReliableChannel : class, IChannel
	{
		// Token: 0x06005980 RID: 22912 RVA: 0x00147564 File Offset: 0x00145764
		protected ReliableListenerOverDuplex(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			base.FaultHelper = new SendFaultHelper(context.Binding.SendTimeout, context.Binding.CloseTimeout);
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0014758F File Offset: 0x0014578F
		protected override IAsyncResult BeginTryReceiveItem(IDuplexChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginTryReceive(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0014759E File Offset: 0x0014579E
		protected override void DisposeItem(Message item)
		{
			((IDisposable)item).Dispose();
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x001475A6 File Offset: 0x001457A6
		protected override void EndTryReceiveItem(IDuplexChannel channel, IAsyncResult result, out Message item)
		{
			channel.EndTryReceive(result, out item);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x001475B1 File Offset: 0x001457B1
		protected override Message GetMessage(Message item)
		{
			return item;
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x001475B4 File Offset: 0x001457B4
		protected override void SendReply(Message reply, IDuplexChannel channel, Message item)
		{
			if (FaultHelper.AddressReply(item, reply))
			{
				channel.Send(reply);
			}
		}
	}
}
