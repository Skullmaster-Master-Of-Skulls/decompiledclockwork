using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A0 RID: 1952
	[Serializable]
	internal class EnvoyTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x000EC8C8 File Offset: 0x000EB8C8
		internal static IMessageSink MessageSink
		{
			get
			{
				if (EnvoyTerminatorSink.messageSink == null)
				{
					EnvoyTerminatorSink envoyTerminatorSink = new EnvoyTerminatorSink();
					lock (EnvoyTerminatorSink.staticSyncObject)
					{
						if (EnvoyTerminatorSink.messageSink == null)
						{
							EnvoyTerminatorSink.messageSink = envoyTerminatorSink;
						}
					}
				}
				return EnvoyTerminatorSink.messageSink;
			}
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000EC91C File Offset: 0x000EB91C
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			return Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(reqMsg);
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000EC948 File Offset: 0x000EB948
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl result = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				result = Thread.CurrentContext.GetClientContextChain().AsyncProcessMessage(reqMsg, replySink);
			}
			return result;
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x000EC981 File Offset: 0x000EB981
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002296 RID: 8854
		private static EnvoyTerminatorSink messageSink;

		// Token: 0x04002297 RID: 8855
		private static object staticSyncObject = new object();
	}
}
