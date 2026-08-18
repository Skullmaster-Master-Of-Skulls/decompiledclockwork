using System;
using System.ComponentModel;

namespace System.ServiceModel
{
	// Token: 0x0200017E RID: 382
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x000291C1 File Offset: 0x000273C1
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x000291CA File Offset: 0x000273CA
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

		// Token: 0x04000C0B RID: 3083
		private bool replaced;
	}
}
