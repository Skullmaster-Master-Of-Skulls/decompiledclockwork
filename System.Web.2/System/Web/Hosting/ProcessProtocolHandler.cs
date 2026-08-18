using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020007E6 RID: 2022
	public abstract class ProcessProtocolHandler : MarshalByRefObject
	{
		// Token: 0x06006086 RID: 24710 RVA: 0x0000298D File Offset: 0x00000B8D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06006087 RID: 24711
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback, IAdphManager AdphManager);

		// Token: 0x06006088 RID: 24712
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		// Token: 0x06006089 RID: 24713
		public abstract void StopProtocol(bool immediate);
	}
}
