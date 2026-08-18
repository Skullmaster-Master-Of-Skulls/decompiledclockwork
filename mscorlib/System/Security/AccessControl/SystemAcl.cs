using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200090B RID: 2315
	public sealed class SystemAcl : CommonAcl
	{
		// Token: 0x060053C4 RID: 21444 RVA: 0x0012FDD5 File Offset: 0x0012EDD5
		public SystemAcl(bool isContainer, bool isDS, int capacity) : this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0012FDEF File Offset: 0x0012EDEF
		public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity) : base(isContainer, isDS, revision, capacity)
		{
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0012FDFC File Offset: 0x0012EDFC
		public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl) : this(isContainer, isDS, rawAcl, false)
		{
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0012FE08 File Offset: 0x0012EE08
		internal SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted) : base(isContainer, isDS, rawAcl, trusted, false)
		{
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0012FE16 File Offset: 0x0012EE16
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckFlags(inheritanceFlags, propagationFlags);
			base.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0012FE47 File Offset: 0x0012EE47
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckFlags(inheritanceFlags, propagationFlags);
			base.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0012FE78 File Offset: 0x0012EE78
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			return base.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0012FEAB File Offset: 0x0012EEAB
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x0012FED4 File Offset: 0x0012EED4
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckFlags(inheritanceFlags, propagationFlags);
			base.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0012FF24 File Offset: 0x0012EF24
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckFlags(inheritanceFlags, propagationFlags);
			base.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0012FF74 File Offset: 0x0012EF74
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			return base.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0012FFBA File Offset: 0x0012EFBA
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}
	}
}
