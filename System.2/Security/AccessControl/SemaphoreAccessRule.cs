using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200048D RID: 1165
	[ComVisible(false)]
	public sealed class SemaphoreAccessRule : AccessRule
	{
		// Token: 0x06002B38 RID: 11064 RVA: 0x000C4D18 File Offset: 0x000C2F18
		public SemaphoreAccessRule(IdentityReference identity, SemaphoreRights eventRights, AccessControlType type) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000C4D26 File Offset: 0x000C2F26
		public SemaphoreAccessRule(string identity, SemaphoreRights eventRights, AccessControlType type) : this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000C4D39 File Offset: 0x000C2F39
		internal SemaphoreAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000C4D4A File Offset: 0x000C2F4A
		public SemaphoreRights SemaphoreRights
		{
			get
			{
				return (SemaphoreRights)base.AccessMask;
			}
		}
	}
}
