using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000718 RID: 1816
	internal class MRMDictionary : MessageDictionary
	{
		// Token: 0x060040D9 RID: 16601 RVA: 0x000DCB0E File Offset: 0x000DBB0E
		public MRMDictionary(IMethodReturnMessage msg, IDictionary idict) : base((msg.Exception != null) ? MRMDictionary.MCMkeysFault : MRMDictionary.MCMkeysNoFault, idict)
		{
			this.fault = (msg.Exception != null);
			this._mrmsg = msg;
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x000DCB44 File Offset: 0x000DBB44
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				if (this.fault)
				{
					return this.FetchLogicalCallContext();
				}
				return this._mrmsg.Uri;
			case 1:
				return this._mrmsg.MethodName;
			case 2:
				return this._mrmsg.MethodSignature;
			case 3:
				return this._mrmsg.TypeName;
			case 4:
				if (this.fault)
				{
					return this._mrmsg.Exception;
				}
				return this._mrmsg.ReturnValue;
			case 5:
				return this._mrmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x000DCBF8 File Offset: 0x000DBBF8
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			StackBasedReturnMessage stackBasedReturnMessage = this._mrmsg as StackBasedReturnMessage;
			if (stackBasedReturnMessage != null)
			{
				return stackBasedReturnMessage.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x000DCC58 File Offset: 0x000DBC58
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
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

		// Token: 0x040020B2 RID: 8370
		public static string[] MCMkeysFault = new string[]
		{
			"__CallContext"
		};

		// Token: 0x040020B3 RID: 8371
		public static string[] MCMkeysNoFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Return",
			"__OutArgs",
			"__CallContext"
		};

		// Token: 0x040020B4 RID: 8372
		internal IMethodReturnMessage _mrmsg;

		// Token: 0x040020B5 RID: 8373
		internal bool fault;
	}
}
