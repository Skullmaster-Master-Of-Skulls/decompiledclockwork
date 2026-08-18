using System;
using System.ComponentModel;

namespace System.Transactions
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000291E4 File Offset: 0x000285E4
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00029204 File Offset: 0x00028604
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

		// Token: 0x04000001 RID: 1
		private bool replaced;
	}
}
