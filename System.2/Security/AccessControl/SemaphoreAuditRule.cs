using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200048E RID: 1166
	[ComVisible(false)]
	public sealed class SemaphoreAuditRule : AuditRule
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x000C4D52 File Offset: 0x000C2F52
		public SemaphoreAuditRule(IdentityReference identity, SemaphoreRights eventRights, AuditFlags flags) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000C4D60 File Offset: 0x000C2F60
		internal SemaphoreAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000C4D71 File Offset: 0x000C2F71
		public SemaphoreRights SemaphoreRights
		{
			get
			{
				return (SemaphoreRights)base.AccessMask;
			}
		}
	}
}
