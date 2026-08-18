using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	internal class ServerContextTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x000ECD08 File Offset: 0x000EBD08
		internal static IMessageSink MessageSink
		{
			get
			{
				if (ServerContextTerminatorSink.messageSink == null)
				{
					ServerContextTerminatorSink serverContextTerminatorSink = new ServerContextTerminatorSink();
					lock (ServerContextTerminatorSink.staticSyncObject)
					{
						if (ServerContextTerminatorSink.messageSink == null)
						{
							ServerContextTerminatorSink.messageSink = serverContextTerminatorSink;
						}
					}
				}
				return ServerContextTerminatorSink.messageSink;
			}
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x000ECD5C File Offset: 0x000EBD5C
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			Context currentContext = Thread.CurrentContext;
			IMessage message2;
			if (reqMsg is IConstructionCallMessage)
			{
				message = currentContext.NotifyActivatorProperties(reqMsg, true);
				if (message != null)
				{
					return message;
				}
				message2 = ((IConstructionCallMessage)reqMsg).Activator.Activate((IConstructionCallMessage)reqMsg);
				message = currentContext.NotifyActivatorProperties(message2, true);
				if (message != null)
				{
					return message;
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject = null;
				try
				{
					message2 = this.GetObjectChain(reqMsg, out marshalByRefObject).SyncProcessMessage(reqMsg);
				}
				finally
				{
					IDisposable disposable;
					if (marshalByRefObject != null && (disposable = (marshalByRefObject as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			return message2;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x000ECDF4 File Offset: 0x000EBDF4
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl result = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message == null)
			{
				message = InternalSink.DisallowAsyncActivation(reqMsg);
			}
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject;
				IMessageSink objectChain = this.GetObjectChain(reqMsg, out marshalByRefObject);
				IDisposable iDis;
				if (marshalByRefObject != null && (iDis = (marshalByRefObject as IDisposable)) != null)
				{
					DisposeSink disposeSink = new DisposeSink(iDis, replySink);
					replySink = disposeSink;
				}
				result = objectChain.AsyncProcessMessage(reqMsg, replySink);
			}
			return result;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x000ECE54 File Offset: 0x000EBE54
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000ECE58 File Offset: 0x000EBE58
		internal virtual IMessageSink GetObjectChain(IMessage reqMsg, out MarshalByRefObject obj)
		{
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			return serverIdentity.GetServerObjectChain(out obj);
		}

		// Token: 0x0400229C RID: 8860
		private static ServerContextTerminatorSink messageSink;

		// Token: 0x0400229D RID: 8861
		private static object staticSyncObject = new object();
	}
}
