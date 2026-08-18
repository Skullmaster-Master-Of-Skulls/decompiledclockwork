using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x020000A6 RID: 166
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001F42E File Offset: 0x0001D62E
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001F437 File Offset: 0x0001D637
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x0400037E RID: 894
		private bool replaced;
	}
}
