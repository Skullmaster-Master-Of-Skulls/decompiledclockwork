using System;
using System.ComponentModel;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000333 RID: 819
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	internal sealed class SRDisplayNameAttribute : DisplayNameAttribute
	{
		// Token: 0x06002067 RID: 8295 RVA: 0x000C465E File Offset: 0x000C285E
		public SRDisplayNameAttribute(string displayName) : base(displayName)
		{
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000C4667 File Offset: 0x000C2867
		public override string DisplayName
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DisplayNameValue = SR.GetString(base.DisplayName);
				}
				return base.DisplayName;
			}
		}

		// Token: 0x040018DC RID: 6364
		private bool replaced;
	}
}
