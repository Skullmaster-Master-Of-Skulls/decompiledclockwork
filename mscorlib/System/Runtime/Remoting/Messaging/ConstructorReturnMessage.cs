using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000712 RID: 1810
	internal class ConstructorReturnMessage : ReturnMessage, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x0600408D RID: 16525 RVA: 0x000DBD33 File Offset: 0x000DAD33
		public ConstructorReturnMessage(MarshalByRefObject o, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IConstructionCallMessage ccm) : base(o, outArgs, outArgsCount, callCtx, ccm)
		{
			this._o = o;
			this._iFlags = 1;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000DBD50 File Offset: 0x000DAD50
		public ConstructorReturnMessage(Exception e, IConstructionCallMessage ccm) : base(e, ccm)
		{
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x000DBD5A File Offset: 0x000DAD5A
		public override object ReturnValue
		{
			get
			{
				if (this._iFlags == 1)
				{
					return RemotingServices.MarshalInternal(this._o, null, null);
				}
				return base.ReturnValue;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000DBD7C File Offset: 0x000DAD7C
		public override IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					object value = new CRMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, value, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000DBDB6 File Offset: 0x000DADB6
		internal object GetObject()
		{
			return this._o;
		}

		// Token: 0x04002099 RID: 8345
		private const int Intercept = 1;

		// Token: 0x0400209A RID: 8346
		private MarshalByRefObject _o;

		// Token: 0x0400209B RID: 8347
		private int _iFlags;
	}
}
