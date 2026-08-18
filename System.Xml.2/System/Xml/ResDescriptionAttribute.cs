using System;
using System.ComponentModel;

namespace System.Xml
{
	// Token: 0x0200012A RID: 298
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060015E9 RID: 5609 RVA: 0x000610B0 File Offset: 0x0005F2B0
		public ResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x000610B9 File Offset: 0x0005F2B9
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

		// Token: 0x04000656 RID: 1622
		private bool replaced;
	}
}
