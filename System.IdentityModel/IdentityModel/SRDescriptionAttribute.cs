using System;
using System.ComponentModel;

namespace System.IdentityModel
{
	// Token: 0x020000E8 RID: 232
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001A15C File Offset: 0x0001835C
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001A165 File Offset: 0x00018365
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

		// Token: 0x0400079D RID: 1949
		private bool replaced;
	}
}
