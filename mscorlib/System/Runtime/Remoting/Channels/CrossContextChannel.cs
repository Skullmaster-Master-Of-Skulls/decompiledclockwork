using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006CA RID: 1738
	internal class CrossContextChannel : InternalSink, IMessageSink
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000D7156 File Offset: 0x000D6156
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000D716C File Offset: 0x000D616C
		private static CrossContextChannel messageSink
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink = value;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000D7184 File Offset: 0x000D6184
		internal static IMessageSink MessageSink
		{
			get
			{
				if (CrossContextChannel.messageSink == null)
				{
					CrossContextChannel messageSink = new CrossContextChannel();
					lock (CrossContextChannel.staticSyncObject)
					{
						if (CrossContextChannel.messageSink == null)
						{
							CrossContextChannel.messageSink = messageSink;
						}
					}
				}
				return CrossContextChannel.messageSink;
			}
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000D71D8 File Offset: 0x000D61D8
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = args[0] as IMessage;
			Context context = args[1] as Context;
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid id = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = message.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						id = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(id, false);
			}
			context.NotifyDynamicSinks(message, false, true, false, true);
			IMessage message2 = context.GetServerContextChain().SyncProcessMessage(message);
			context.NotifyDynamicSinks(message2, false, false, false, true);
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid guid;
				RemotingServices.CORProfilerRemotingServerSendingReply(out guid, false);
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					message2.Properties["CORProfilerCookie"] = guid;
				}
			}
			return message2;
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x000D7288 File Offset: 0x000D6288
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			object[] array = new object[2];
			object[] array2 = array;
			IMessage message = null;
			try
			{
				IMessage message2 = InternalSink.ValidateMessage(reqMsg);
				if (message2 != null)
				{
					return message2;
				}
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				array2[0] = reqMsg;
				array2[1] = serverIdentity.ServerContext;
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(serverIdentity.ServerContext, CrossContextChannel.s_xctxDel, array2);
			}
			catch (Exception e)
			{
				message = new ReturnMessage(e, (IMethodCallMessage)reqMsg);
				if (reqMsg != null)
				{
					((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]);
				}
			}
			return message;
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x000D7330 File Offset: 0x000D6330
		internal static object AsyncProcessMessageCallback(object[] args)
		{
			AsyncWorkItem replySink = null;
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context oldCtx = (Context)args[2];
			Context context = (Context)args[3];
			if (messageSink != null)
			{
				replySink = new AsyncWorkItem(messageSink, oldCtx);
			}
			context.NotifyDynamicSinks(msg, false, true, true, true);
			return context.GetServerContextChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000D7394 File Offset: 0x000D6394
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			object[] array = new object[4];
			object[] array2 = array;
			IMessageCtrl result = null;
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				if (RemotingServices.CORProfilerTrackRemotingAsync())
				{
					Guid id = Guid.Empty;
					if (RemotingServices.CORProfilerTrackRemotingCookie())
					{
						object obj = reqMsg.Properties["CORProfilerCookie"];
						if (obj != null)
						{
							id = (Guid)obj;
						}
					}
					RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
					if (replySink != null)
					{
						IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
						replySink = messageSink;
					}
				}
				Context serverContext = serverIdentity.ServerContext;
				if (serverContext.IsThreadPoolAware)
				{
					array2[0] = reqMsg;
					array2[1] = replySink;
					array2[2] = Thread.CurrentContext;
					array2[3] = serverContext;
					InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.AsyncProcessMessageCallback);
					result = (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, array2);
				}
				else
				{
					AsyncWorkItem @object = new AsyncWorkItem(reqMsg, replySink, Thread.CurrentContext, serverIdentity);
					WaitCallback callBack = new WaitCallback(@object.FinishAsyncWork);
					ThreadPool.QueueUserWorkItem(callBack);
				}
			}
			return result;
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000D7494 File Offset: 0x000D6494
		internal static object DoAsyncDispatchCallback(object[] args)
		{
			AsyncWorkItem replySink = null;
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context oldCtx = (Context)args[2];
			Context context = (Context)args[3];
			if (messageSink != null)
			{
				replySink = new AsyncWorkItem(messageSink, oldCtx);
			}
			return context.GetServerContextChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000D74E8 File Offset: 0x000D64E8
		internal static IMessageCtrl DoAsyncDispatch(IMessage reqMsg, IMessageSink replySink)
		{
			object[] array = new object[4];
			object[] array2 = array;
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			if (RemotingServices.CORProfilerTrackRemotingAsync())
			{
				Guid id = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = reqMsg.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						id = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
				if (replySink != null)
				{
					IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
					replySink = messageSink;
				}
			}
			Context serverContext = serverIdentity.ServerContext;
			array2[0] = reqMsg;
			array2[1] = replySink;
			array2[2] = Thread.CurrentContext;
			array2[3] = serverContext;
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.DoAsyncDispatchCallback);
			return (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, array2);
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000D7592 File Offset: 0x000D6592
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001FE9 RID: 8169
		private const string _channelName = "XCTX";

		// Token: 0x04001FEA RID: 8170
		private const int _channelCapability = 0;

		// Token: 0x04001FEB RID: 8171
		private const string _channelURI = "XCTX_URI";

		// Token: 0x04001FEC RID: 8172
		private static object staticSyncObject = new object();

		// Token: 0x04001FED RID: 8173
		private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossContextChannel.SyncProcessMessageCallback);
	}
}
