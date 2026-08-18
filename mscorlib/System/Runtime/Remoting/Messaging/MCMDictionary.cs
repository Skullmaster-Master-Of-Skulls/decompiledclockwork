using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000717 RID: 1815
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x060040D4 RID: 16596 RVA: 0x000DC94B File Offset: 0x000DB94B
		public MCMDictionary(IMethodCallMessage msg, IDictionary idict) : base(MCMDictionary.MCMkeys, idict)
		{
			this._mcmsg = msg;
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x000DC960 File Offset: 0x000DB960
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._mcmsg.Uri;
			case 1:
				return this._mcmsg.MethodName;
			case 2:
				return this._mcmsg.MethodSignature;
			case 3:
				return this._mcmsg.TypeName;
			case 4:
				return this._mcmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x000DC9E4 File Offset: 0x000DB9E4
		private LogicalCallContext FetchLogicalCallContext()
		{
			Message message = this._mcmsg as Message;
			if (message != null)
			{
				return message.GetLogicalCallContext();
			}
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (methodCall != null)
			{
				return methodCall.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x000DCA2C File Offset: 0x000DBA2C
		internal override void SetSpecialKey(int keyNum, object value)
		{
			Message message = this._mcmsg as Message;
			MethodCall methodCall = this._mcmsg as MethodCall;
			switch (keyNum)
			{
			case 0:
				if (message != null)
				{
					message.Uri = (string)value;
					return;
				}
				if (methodCall != null)
				{
					methodCall.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			case 1:
				if (message != null)
				{
					message.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x040020B0 RID: 8368
		public static string[] MCMkeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Args",
			"__CallContext"
		};

		// Token: 0x040020B1 RID: 8369
		internal IMethodCallMessage _mcmsg;
	}
}
