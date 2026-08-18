using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020008FB RID: 2299
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x06005347 RID: 21319 RVA: 0x0012D47C File Offset: 0x0012C47C
		internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier) : base(type, flags)
		{
			if (securityIdentifier == null)
			{
				throw new ArgumentNullException("securityIdentifier");
			}
			this.AccessMask = accessMask;
			this.SecurityIdentifier = securityIdentifier;
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x0012D4AA File Offset: 0x0012C4AA
		// (set) Token: 0x06005349 RID: 21321 RVA: 0x0012D4B2 File Offset: 0x0012C4B2
		public int AccessMask
		{
			get
			{
				return this._accessMask;
			}
			set
			{
				this._accessMask = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600534A RID: 21322 RVA: 0x0012D4BB File Offset: 0x0012C4BB
		// (set) Token: 0x0600534B RID: 21323 RVA: 0x0012D4C3 File Offset: 0x0012C4C3
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this._sid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._sid = value;
			}
		}

		// Token: 0x04002B35 RID: 11061
		internal const int AccessMaskLength = 4;

		// Token: 0x04002B36 RID: 11062
		private int _accessMask;

		// Token: 0x04002B37 RID: 11063
		private SecurityIdentifier _sid;
	}
}
