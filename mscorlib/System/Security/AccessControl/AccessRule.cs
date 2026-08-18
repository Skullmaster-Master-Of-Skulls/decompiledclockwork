using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200090F RID: 2319
	public abstract class AccessRule : AuthorizationRule
	{
		// Token: 0x060053E7 RID: 21479 RVA: 0x00130430 File Offset: 0x0012F430
		protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (type != AccessControlType.Allow && type != AccessControlType.Deny)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			if (inheritanceFlags < InheritanceFlags.None || inheritanceFlags > (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit))
			{
				throw new ArgumentOutOfRangeException("inheritanceFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					inheritanceFlags,
					"InheritanceFlags"
				}));
			}
			if (propagationFlags < PropagationFlags.None || propagationFlags > (PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly))
			{
				throw new ArgumentOutOfRangeException("propagationFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					inheritanceFlags,
					"PropagationFlags"
				}));
			}
			this._type = type;
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x001304E2 File Offset: 0x0012F4E2
		public AccessControlType AccessControlType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04002B89 RID: 11145
		private readonly AccessControlType _type;
	}
}
