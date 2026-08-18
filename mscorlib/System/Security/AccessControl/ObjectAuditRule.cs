using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000938 RID: 2360
	public abstract class ObjectAuditRule : AuditRule
	{
		// Token: 0x06005518 RID: 21784 RVA: 0x001347A0 File Offset: 0x001337A0
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				this._objectType = objectType;
				this._objectFlags |= ObjectAceFlags.ObjectAceTypePresent;
			}
			else
			{
				this._objectType = Guid.Empty;
			}
			if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				this._inheritedObjectType = inheritedObjectType;
				this._objectFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				return;
			}
			this._inheritedObjectType = Guid.Empty;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x0013482C File Offset: 0x0013382C
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x00134834 File Offset: 0x00133834
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x0013483C File Offset: 0x0013383C
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04002C3B RID: 11323
		private readonly Guid _objectType;

		// Token: 0x04002C3C RID: 11324
		private readonly Guid _inheritedObjectType;

		// Token: 0x04002C3D RID: 11325
		private readonly ObjectAceFlags _objectFlags;
	}
}
