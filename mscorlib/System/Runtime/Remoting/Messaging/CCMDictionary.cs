using System;
using System.Collections;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000715 RID: 1813
	internal class CCMDictionary : MessageDictionary
	{
		// Token: 0x060040CA RID: 16586 RVA: 0x000DC4FB File Offset: 0x000DB4FB
		public CCMDictionary(IConstructionCallMessage msg, IDictionary idict) : base(CCMDictionary.CCMkeys, idict)
		{
			this._ccmsg = msg;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000DC510 File Offset: 0x000DB510
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._ccmsg.Uri;
			case 1:
				return this._ccmsg.MethodName;
			case 2:
				return this._ccmsg.MethodSignature;
			case 3:
				return this._ccmsg.TypeName;
			case 4:
				return this._ccmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			case 6:
				return this._ccmsg.CallSiteActivationAttributes;
			case 7:
				return null;
			case 8:
				return this._ccmsg.ContextProperties;
			case 9:
				return this._ccmsg.Activator;
			case 10:
				return this._ccmsg.ActivationTypeName;
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000DC5D8 File Offset: 0x000DB5D8
		private LogicalCallContext FetchLogicalCallContext()
		{
			ConstructorCallMessage constructorCallMessage = this._ccmsg as ConstructorCallMessage;
			if (constructorCallMessage != null)
			{
				return constructorCallMessage.GetLogicalCallContext();
			}
			if (this._ccmsg is ConstructionCall)
			{
				return ((MethodCall)this._ccmsg).GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000DC628 File Offset: 0x000DB628
		internal override void SetSpecialKey(int keyNum, object value)
		{
			switch (keyNum)
			{
			case 0:
				((ConstructorCallMessage)this._ccmsg).Uri = (string)value;
				return;
			case 1:
				((ConstructorCallMessage)this._ccmsg).SetLogicalCallContext((LogicalCallContext)value);
				return;
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x040020AA RID: 8362
		public static string[] CCMkeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Args",
			"__CallContext",
			"__CallSiteActivationAttributes",
			"__ActivationType",
			"__ContextProperties",
			"__Activator",
			"__ActivationTypeName"
		};

		// Token: 0x040020AB RID: 8363
		internal IConstructionCallMessage _ccmsg;
	}
}
