using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	// Token: 0x020000BA RID: 186
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PipeAuditRule : AuditRule
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00010AF5 File Offset: 0x0000ECF5
		public PipeAuditRule(IdentityReference identity, PipeAccessRights rights, AuditFlags flags) : this(identity, PipeAuditRule.AccessMaskFromRights(rights), false, flags)
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010B06 File Offset: 0x0000ED06
		public PipeAuditRule(string identity, PipeAccessRights rights, AuditFlags flags) : this(new NTAccount(identity), PipeAuditRule.AccessMaskFromRights(rights), false, flags)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010B1C File Offset: 0x0000ED1C
		internal PipeAuditRule(IdentityReference identity, int accessMask, bool isInherited, AuditFlags flags) : base(identity, accessMask, isInherited, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00010B2B File Offset: 0x0000ED2B
		private static int AccessMaskFromRights(PipeAccessRights rights)
		{
			if (rights < (PipeAccessRights)0 || rights > (PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity))
			{
				throw new ArgumentOutOfRangeException("rights", SR.GetString("ArgumentOutOfRange_NeedValidPipeAccessRights"));
			}
			return (int)rights;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00010B4F File Offset: 0x0000ED4F
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return PipeAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}
	}
}
