using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200090C RID: 2316
	public sealed class DiscretionaryAcl : CommonAcl
	{
		// Token: 0x060053D0 RID: 21456 RVA: 0x0012FFF4 File Offset: 0x0012EFF4
		public DiscretionaryAcl(bool isContainer, bool isDS, int capacity) : this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x0013000E File Offset: 0x0012F00E
		public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity) : base(isContainer, isDS, revision, capacity)
		{
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0013001B File Offset: 0x0012F01B
		public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl) : this(isContainer, isDS, rawAcl, false)
		{
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x00130027 File Offset: 0x0012F027
		internal DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted) : base(isContainer, isDS, (rawAcl == null) ? new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 0) : rawAcl, trusted, true)
		{
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0013004F File Offset: 0x0012F04F
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0013008C File Offset: 0x0012F08C
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x001300CC File Offset: 0x0012F0CC
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0013010B File Offset: 0x0012F10B
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00130140 File Offset: 0x0012F140
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0013019C File Offset: 0x0012F19C
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x001301F8 File Offset: 0x0012F1F8
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString(" InvalidOperation_OnlyValidForDS "));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0013024C File Offset: 0x0012F24C
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString(" InvalidOperation_OnlyValidForDS "));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060053DC RID: 21468 RVA: 0x0013029D File Offset: 0x0012F29D
		// (set) Token: 0x060053DD RID: 21469 RVA: 0x001302A5 File Offset: 0x0012F2A5
		internal bool EveryOneFullAccessForNullDacl
		{
			get
			{
				return this.everyOneFullAccessForNullDacl;
			}
			set
			{
				this.everyOneFullAccessForNullDacl = value;
			}
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x001302AE File Offset: 0x0012F2AE
		internal override void OnAclModificationTried()
		{
			this.everyOneFullAccessForNullDacl = false;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x001302B8 File Offset: 0x0012F2B8
		internal static DiscretionaryAcl CreateAllowEveryoneFullAccess(bool isDS, bool isContainer)
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 1);
			discretionaryAcl.AddAccess(AccessControlType.Allow, DiscretionaryAcl._sidEveryone, -1, isContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
			discretionaryAcl.everyOneFullAccessForNullDacl = true;
			return discretionaryAcl;
		}

		// Token: 0x04002B71 RID: 11121
		private static SecurityIdentifier _sidEveryone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

		// Token: 0x04002B72 RID: 11122
		private bool everyOneFullAccessForNullDacl;
	}
}
