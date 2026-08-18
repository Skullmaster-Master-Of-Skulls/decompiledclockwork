using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	// Token: 0x020000B9 RID: 185
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PipeAccessRule : AccessRule
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x00010A58 File Offset: 0x0000EC58
		public PipeAccessRule(string identity, PipeAccessRights rights, AccessControlType type) : this(new NTAccount(identity), PipeAccessRule.AccessMaskFromRights(rights, type), false, type)
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00010A6F File Offset: 0x0000EC6F
		public PipeAccessRule(IdentityReference identity, PipeAccessRights rights, AccessControlType type) : this(identity, PipeAccessRule.AccessMaskFromRights(rights, type), false, type)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00010A81 File Offset: 0x0000EC81
		internal PipeAccessRule(IdentityReference identity, int accessMask, bool isInherited, AccessControlType type) : base(identity, accessMask, isInherited, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00010A90 File Offset: 0x0000EC90
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return PipeAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		internal static int AccessMaskFromRights(PipeAccessRights rights, AccessControlType controlType)
		{
			if (rights < (PipeAccessRights)0 || rights > (PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity))
			{
				throw new ArgumentOutOfRangeException("rights", SR.GetString("ArgumentOutOfRange_NeedValidPipeAccessRights"));
			}
			if (controlType == AccessControlType.Allow)
			{
				rights |= PipeAccessRights.Synchronize;
			}
			else if (controlType == AccessControlType.Deny && rights != PipeAccessRights.FullControl)
			{
				rights &= ~PipeAccessRights.Synchronize;
			}
			return (int)rights;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00010AF2 File Offset: 0x0000ECF2
		internal static PipeAccessRights RightsFromAccessMask(int accessMask)
		{
			return (PipeAccessRights)accessMask;
		}
	}
}
