using System;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		public PrintingPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00020AD9 File Offset: 0x0001ECD9
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00020AE1 File Offset: 0x0001ECE1
		public PrintingPermissionLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				if (value < PrintingPermissionLevel.NoPrinting || value > PrintingPermissionLevel.AllPrinting)
				{
					throw new ArgumentException(SR.GetString("PrintingPermissionAttributeInvalidPermissionLevel"), "value");
				}
				this.level = value;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00020B07 File Offset: 0x0001ED07
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PrintingPermission(PermissionState.Unrestricted);
			}
			return new PrintingPermission(this.level);
		}

		// Token: 0x040006ED RID: 1773
		private PrintingPermissionLevel level;
	}
}
