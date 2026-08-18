using System;
using System.ComponentModel;

namespace System.Timers
{
	// Token: 0x02000739 RID: 1849
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06003871 RID: 14449 RVA: 0x000EDF7C File Offset: 0x000ECF7C
		public TimersDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000EDF85 File Offset: 0x000ECF85
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

		// Token: 0x04003250 RID: 12880
		private bool replaced;
	}
}
