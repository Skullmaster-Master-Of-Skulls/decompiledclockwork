using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004DC RID: 1244
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06002EFA RID: 12026 RVA: 0x000D2F4B File Offset: 0x000D114B
		public MonitoringDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000D2F54 File Offset: 0x000D1154
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

		// Token: 0x040027A7 RID: 10151
		private bool replaced;
	}
}
