using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000934 RID: 2356
	public sealed class RegistryAuditRule : AuditRule
	{
		// Token: 0x060054FB RID: 21755 RVA: 0x001344BC File Offset: 0x001334BC
		public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x001344CC File Offset: 0x001334CC
		public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x001344E1 File Offset: 0x001334E1
		internal RegistryAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x001344F2 File Offset: 0x001334F2
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
