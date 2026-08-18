using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000911 RID: 2321
	public abstract class AuditRule : AuthorizationRule
	{
		// Token: 0x060053EF RID: 21487 RVA: 0x0013059C File Offset: 0x0012F59C
		protected AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (auditFlags == AuditFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "auditFlags");
			}
			if ((auditFlags & ~(AuditFlags.Success | AuditFlags.Failure)) != AuditFlags.None)
			{
				throw new ArgumentOutOfRangeException("auditFlags", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			this._flags = auditFlags;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x001305F3 File Offset: 0x0012F5F3
		public AuditFlags AuditFlags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x04002B8A RID: 11146
		private readonly AuditFlags _flags;
	}
}
