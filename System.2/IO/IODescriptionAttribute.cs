using System;
using System.ComponentModel;

namespace System.IO
{
	// Token: 0x02000402 RID: 1026
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x000B1EDA File Offset: 0x000B00DA
		public IODescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000B1EE3 File Offset: 0x000B00E3
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

		// Token: 0x040020E5 RID: 8421
		private bool replaced;
	}
}
