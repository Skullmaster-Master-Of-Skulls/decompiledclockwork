using System;
using System.ComponentModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000273 RID: 627
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600166D RID: 5741 RVA: 0x0004A152 File Offset: 0x00048352
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0004A15B File Offset: 0x0004835B
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

		// Token: 0x04000A6D RID: 2669
		private bool replaced;
	}
}
