using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200092A RID: 2346
	public sealed class MutexAccessRule : AccessRule
	{
		// Token: 0x060054A9 RID: 21673 RVA: 0x00132AE3 File Offset: 0x00131AE3
		public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x00132AF1 File Offset: 0x00131AF1
		public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type) : this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x00132B04 File Offset: 0x00131B04
		internal MutexAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060054AC RID: 21676 RVA: 0x00132B15 File Offset: 0x00131B15
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
