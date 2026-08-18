using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200073F RID: 1855
	internal class ComRedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x0600426E RID: 17006 RVA: 0x000E2132 File Offset: 0x000E1132
		internal ComRedirectionProxy(MarshalByRefObject comObject, Type serverType)
		{
			this._comObject = comObject;
			this._serverType = serverType;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000E2148 File Offset: 0x000E1148
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMethodCallMessage reqMsg = (IMethodCallMessage)msg;
			IMethodReturnMessage methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
			if (methodReturnMessage != null)
			{
				COMException ex = methodReturnMessage.Exception as COMException;
				if (ex != null && (ex._HResult == -2147023174 || ex._HResult == -2147023169))
				{
					this._comObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
				}
			}
			return methodReturnMessage;
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000E21BC File Offset: 0x000E11BC
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage msg2 = this.SyncProcessMessage(msg);
			if (replySink != null)
			{
				replySink.SyncProcessMessage(msg2);
			}
			return null;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000E21DF File Offset: 0x000E11DF
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002144 RID: 8516
		private MarshalByRefObject _comObject;

		// Token: 0x04002145 RID: 8517
		private Type _serverType;
	}
}
