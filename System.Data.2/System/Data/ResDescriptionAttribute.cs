using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000148 RID: 328
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x00099F58 File Offset: 0x00099358
		public ResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00099F6C File Offset: 0x0009936C
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

		// Token: 0x0400078D RID: 1933
		private bool replaced;
	}
}
