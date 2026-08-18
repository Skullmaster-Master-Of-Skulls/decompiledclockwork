using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020007A1 RID: 1953
	public abstract class AppDomainProtocolHandler : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x06005CBB RID: 23739 RVA: 0x000474BC File Offset: 0x000456BC
		protected AppDomainProtocolHandler()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x0000298D File Offset: 0x00000B8D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005CBD RID: 23741
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback);

		// Token: 0x06005CBE RID: 23742
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		// Token: 0x06005CBF RID: 23743
		public abstract void StopProtocol(bool immediate);

		// Token: 0x06005CC0 RID: 23744 RVA: 0x00140AFB File Offset: 0x0013ECFB
		public virtual void Stop(bool immediate)
		{
			this.StopProtocol(true);
			HostingEnvironment.UnregisterObject(this);
		}
	}
}
