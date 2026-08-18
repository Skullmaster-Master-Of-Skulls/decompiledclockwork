using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200090E RID: 2318
	public abstract class AuthorizationRule
	{
		// Token: 0x060053E1 RID: 21473 RVA: 0x001302FC File Offset: 0x0012F2FC
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (!identity.IsValidTargetType(typeof(SecurityIdentifier)))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), "identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
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
			this._identity = identity;
			this._accessMask = accessMask;
			this._isInherited = isInherited;
			this._inheritanceFlags = inheritanceFlags;
			if (inheritanceFlags != InheritanceFlags.None)
			{
				this._propagationFlags = propagationFlags;
				return;
			}
			this._propagationFlags = PropagationFlags.None;
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x00130405 File Offset: 0x0012F405
		public IdentityReference IdentityReference
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x0013040D File Offset: 0x0012F40D
		protected internal int AccessMask
		{
			get
			{
				return this._accessMask;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x00130415 File Offset: 0x0012F415
		public bool IsInherited
		{
			get
			{
				return this._isInherited;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x0013041D File Offset: 0x0012F41D
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this._inheritanceFlags;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x00130425 File Offset: 0x0012F425
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this._propagationFlags;
			}
		}

		// Token: 0x04002B84 RID: 11140
		private readonly IdentityReference _identity;

		// Token: 0x04002B85 RID: 11141
		private readonly int _accessMask;

		// Token: 0x04002B86 RID: 11142
		private readonly bool _isInherited;

		// Token: 0x04002B87 RID: 11143
		private readonly InheritanceFlags _inheritanceFlags;

		// Token: 0x04002B88 RID: 11144
		private readonly PropagationFlags _propagationFlags;
	}
}
