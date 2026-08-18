using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A5 RID: 1957
	[Serializable]
	internal class ServerObjectTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x060045A7 RID: 17831 RVA: 0x000ECEF3 File Offset: 0x000EBEF3
		internal ServerObjectTerminatorSink(MarshalByRefObject srvObj)
		{
			this._stackBuilderSink = new StackBuilderSink(srvObj);
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x000ECF08 File Offset: 0x000EBF08
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			ArrayWithSize serverSideDynamicSinks = serverIdentity.ServerSideDynamicSinks;
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, serverSideDynamicSinks, false, true, false);
			}
			IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
			IMessage message2;
			if (messageSink != null)
			{
				message2 = messageSink.SyncProcessMessage(reqMsg);
			}
			else
			{
				message2 = this._stackBuilderSink.SyncProcessMessage(reqMsg);
			}
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(message2, serverSideDynamicSinks, false, false, false);
			}
			return message2;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x000ECF78 File Offset: 0x000EBF78
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
				IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
				if (messageSink != null)
				{
					result = messageSink.AsyncProcessMessage(reqMsg, replySink);
				}
				else
				{
					result = this._stackBuilderSink.AsyncProcessMessage(reqMsg, replySink);
				}
			}
			return result;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x000ECFCC File Offset: 0x000EBFCC
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040022A0 RID: 8864
		internal StackBuilderSink _stackBuilderSink;
	}
}
