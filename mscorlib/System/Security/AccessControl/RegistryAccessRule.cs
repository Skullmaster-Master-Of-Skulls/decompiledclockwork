using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000933 RID: 2355
	public sealed class RegistryAccessRule : AccessRule
	{
		// Token: 0x060054F5 RID: 21749 RVA: 0x0013445D File Offset: 0x0013345D
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type) : this(identity, (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0013446B File Offset: 0x0013346B
		public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0013447E File Offset: 0x0013347E
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0013448E File Offset: 0x0013348E
		public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x001344A3 File Offset: 0x001334A3
		internal RegistryAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x001344B4 File Offset: 0x001334B4
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
