using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000451 RID: 1105
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06004D60 RID: 19808 RVA: 0x0013FD03 File Offset: 0x0013DF03
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x0013FD0C File Offset: 0x0013DF0C
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

		// Token: 0x040028D9 RID: 10457
		private bool replaced;
	}
}
