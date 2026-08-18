using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000725 RID: 1829
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class InternalMessageWrapper
	{
		// Token: 0x0600419C RID: 16796 RVA: 0x000DF848 File Offset: 0x000DE848
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000DF858 File Offset: 0x000DE858
		internal object GetIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.IdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetIdentityObject();
			}
			return null;
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000DF894 File Offset: 0x000DE894
		internal object GetServerIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.ServerIdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetServerIdentityObject();
			}
			return null;
		}

		// Token: 0x040020F6 RID: 8438
		protected IMessage WrappedMessage;
	}
}
