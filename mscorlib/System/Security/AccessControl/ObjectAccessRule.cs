using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000937 RID: 2359
	public abstract class ObjectAccessRule : AccessRule
	{
		// Token: 0x06005514 RID: 21780 RVA: 0x001346FC File Offset: 0x001336FC
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
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

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x00134788 File Offset: 0x00133788
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x00134790 File Offset: 0x00133790
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x00134798 File Offset: 0x00133798
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04002C38 RID: 11320
		private readonly Guid _objectType;

		// Token: 0x04002C39 RID: 11321
		private readonly Guid _inheritedObjectType;

		// Token: 0x04002C3A RID: 11322
		private readonly ObjectAceFlags _objectFlags;
	}
}
