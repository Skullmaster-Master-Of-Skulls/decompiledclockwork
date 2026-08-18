using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000086 RID: 134 RVA: 0x001D9A78 File Offset: 0x001D8E78
		public ResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000087 RID: 135 RVA: 0x001D9A98 File Offset: 0x001D8E98
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = Res.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000083 RID: 131
		private bool replaced;
	}
}
