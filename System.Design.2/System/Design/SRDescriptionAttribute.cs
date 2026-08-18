using System;
using System.ComponentModel;

namespace System.Design
{
	// Token: 0x02000284 RID: 644
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x0008B0EC File Offset: 0x000892EC
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0008B0F5 File Offset: 0x000892F5
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

		// Token: 0x04000E13 RID: 3603
		private bool replaced;
	}
}
