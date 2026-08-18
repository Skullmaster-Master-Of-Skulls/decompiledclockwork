using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000912 RID: 2322
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		// Token: 0x060053F1 RID: 21489 RVA: 0x001305FB File Offset: 0x0012F5FB
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : this(identity, CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0013060E File Offset: 0x0012F60E
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : this(new NTAccount(identity), CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00130626 File Offset: 0x0012F626
		private CryptoKeyAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x00130637 File Offset: 0x0012F637
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return CryptoKeyAuditRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00130644 File Offset: 0x0012F644
		private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights)
		{
			return (int)cryptoKeyRights;
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00130647 File Offset: 0x0012F647
		internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
		{
			return (CryptoKeyRights)accessMask;
		}
	}
}
