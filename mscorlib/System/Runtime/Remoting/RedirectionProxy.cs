using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.Runtime.Remoting
{
	// Token: 0x0200073E RID: 1854
	internal class RedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x06004269 RID: 17001 RVA: 0x000E2048 File Offset: 0x000E1048
		internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
		{
			this._proxy = proxy;
			this._realProxy = RemotingServices.GetRealProxy(this._proxy);
			this._serverType = serverType;
			this._objectMode = WellKnownObjectMode.Singleton;
		}

		// Token: 0x17000BA7 RID: 2983
		// (set) Token: 0x0600426A RID: 17002 RVA: 0x000E2076 File Offset: 0x000E1076
		public WellKnownObjectMode ObjectMode
		{
			set
			{
				this._objectMode = value;
			}
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000E2080 File Offset: 0x000E1080
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage result = null;
			try
			{
				msg.Properties["__Uri"] = this._realProxy.IdentityObject.URI;
				if (this._objectMode == WellKnownObjectMode.Singleton)
				{
					result = this._realProxy.Invoke(msg);
				}
				else
				{
					MarshalByRefObject proxy = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					result = realProxy.Invoke(msg);
				}
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, msg as IMethodCallMessage);
			}
			return result;
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000E210C File Offset: 0x000E110C
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage msg2 = this.SyncProcessMessage(msg);
			if (replySink != null)
			{
				replySink.SyncProcessMessage(msg2);
			}
			return null;
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000E212F File Offset: 0x000E112F
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002140 RID: 8512
		private MarshalByRefObject _proxy;

		// Token: 0x04002141 RID: 8513
		private RealProxy _realProxy;

		// Token: 0x04002142 RID: 8514
		private Type _serverType;

		// Token: 0x04002143 RID: 8515
		private WellKnownObjectMode _objectMode;
	}
}
