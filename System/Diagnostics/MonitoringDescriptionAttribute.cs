using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x02000762 RID: 1890
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06003A1A RID: 14874 RVA: 0x000F5ABF File Offset: 0x000F4ABF
		public MonitoringDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003A1B RID: 14875 RVA: 0x000F5AC8 File Offset: 0x000F4AC8
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

		// Token: 0x040032FF RID: 13055
		private bool replaced;
	}
}
