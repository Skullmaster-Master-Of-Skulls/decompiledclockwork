using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200092B RID: 2347
	public sealed class MutexAuditRule : AuditRule
	{
		// Token: 0x060054AD RID: 21677 RVA: 0x00132B1D File Offset: 0x00131B1D
		public MutexAuditRule(IdentityReference identity, MutexRights eventRights, AuditFlags flags) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00132B2B File Offset: 0x00131B2B
		internal MutexAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x00132B3C File Offset: 0x00131B3C
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
