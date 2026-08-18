using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000920 RID: 2336
	public sealed class EventWaitHandleAccessRule : AccessRule
	{
		// Token: 0x06005461 RID: 21601 RVA: 0x00132240 File Offset: 0x00131240
		public EventWaitHandleAccessRule(IdentityReference identity, EventWaitHandleRights eventRights, AccessControlType type) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0013224E File Offset: 0x0013124E
		public EventWaitHandleAccessRule(string identity, EventWaitHandleRights eventRights, AccessControlType type) : this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x00132261 File Offset: 0x00131261
		internal EventWaitHandleAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x00132272 File Offset: 0x00131272
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
