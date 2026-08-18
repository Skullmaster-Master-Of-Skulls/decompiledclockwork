using System;
using System.Collections;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000716 RID: 1814
	internal class CRMDictionary : MessageDictionary
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x000DC6FD File Offset: 0x000DB6FD
		public CRMDictionary(IConstructionReturnMessage msg, IDictionary idict) : base((msg.Exception != null) ? CRMDictionary.CRMkeysFault : CRMDictionary.CRMkeysNoFault, idict)
		{
			this.fault = (msg.Exception != null);
			this._crmsg = msg;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000DC734 File Offset: 0x000DB734
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._crmsg.Uri;
			case 1:
				return this._crmsg.MethodName;
			case 2:
				return this._crmsg.MethodSignature;
			case 3:
				return this._crmsg.TypeName;
			case 4:
				if (!this.fault)
				{
					return this._crmsg.ReturnValue;
				}
				return this.FetchLogicalCallContext();
			case 5:
				return this._crmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000DC7D4 File Offset: 0x000DB7D4
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._crmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000DC81C File Offset: 0x000DB81C
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			MethodResponse methodResponse = this._crmsg as MethodResponse;
			switch (keyNum)
			{
			case 0:
				if (returnMessage != null)
				{
					returnMessage.Uri = (string)value;
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			case 1:
				if (returnMessage != null)
				{
					returnMessage.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x040020AC RID: 8364
		public static string[] CRMkeysFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__CallContext"
		};

		// Token: 0x040020AD RID: 8365
		public static string[] CRMkeysNoFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Return",
			"__OutArgs",
			"__CallContext"
		};

		// Token: 0x040020AE RID: 8366
		internal IConstructionReturnMessage _crmsg;

		// Token: 0x040020AF RID: 8367
		internal bool fault;
	}
}
