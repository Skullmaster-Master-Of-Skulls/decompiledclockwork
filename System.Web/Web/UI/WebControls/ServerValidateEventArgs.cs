using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200063B RID: 1595
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ServerValidateEventArgs : EventArgs
	{
		// Token: 0x06004EBB RID: 20155 RVA: 0x0013E204 File Offset: 0x0013D204
		public ServerValidateEventArgs(string value, bool isValid)
		{
			this.isValid = isValid;
			this.value = value;
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x0013E21A File Offset: 0x0013D21A
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x0013E222 File Offset: 0x0013D222
		// (set) Token: 0x06004EBE RID: 20158 RVA: 0x0013E22A File Offset: 0x0013D22A
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x04002CB0 RID: 11440
		private bool isValid;

		// Token: 0x04002CB1 RID: 11441
		private string value;
	}
}
