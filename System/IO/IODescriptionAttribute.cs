using System;
using System.ComponentModel;

namespace System.IO
{
	// Token: 0x0200072F RID: 1839
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600383F RID: 14399 RVA: 0x000ED6DA File Offset: 0x000EC6DA
		public IODescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000ED6E3 File Offset: 0x000EC6E3
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

		// Token: 0x04003230 RID: 12848
		private bool replaced;
	}
}
