using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000283 RID: 643
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class AppDomainProtocolHandler : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x06002117 RID: 8471 RVA: 0x000913E0 File Offset: 0x000903E0
		protected AppDomainProtocolHandler()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000913EE File Offset: 0x000903EE
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06002119 RID: 8473
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback);

		// Token: 0x0600211A RID: 8474
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		// Token: 0x0600211B RID: 8475
		public abstract void StopProtocol(bool immediate);

		// Token: 0x0600211C RID: 8476 RVA: 0x000913F1 File Offset: 0x000903F1
		public virtual void Stop(bool immediate)
		{
			this.StopProtocol(true);
			HostingEnvironment.UnregisterObject(this);
		}
	}
}
