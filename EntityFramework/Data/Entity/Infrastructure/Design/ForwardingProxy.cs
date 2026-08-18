using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200018C RID: 396
	internal class ForwardingProxy<T> : RealProxy
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x0003CF65 File Offset: 0x0003B165
		public ForwardingProxy(object target) : base(typeof(T))
		{
			this._target = (MarshalByRefObject)target;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003CF83 File Offset: 0x0003B183
		public override IMessage Invoke(IMessage msg)
		{
			new MethodCallMessageWrapper((IMethodCallMessage)msg).Uri = RemotingServices.GetObjectUri(this._target);
			return RemotingServices.GetEnvoyChainForProxy(this._target).SyncProcessMessage(msg);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003CFB1 File Offset: 0x0003B1B1
		public new T GetTransparentProxy()
		{
			return (T)((object)base.GetTransparentProxy());
		}

		// Token: 0x040003AC RID: 940
		private readonly MarshalByRefObject _target;
	}
}
