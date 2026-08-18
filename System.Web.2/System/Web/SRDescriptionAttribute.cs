using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x02000113 RID: 275
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x0002E448 File Offset: 0x0002C648
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00030592 File Offset: 0x0002E792
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

		// Token: 0x040006BB RID: 1723
		private bool replaced;
	}
}
