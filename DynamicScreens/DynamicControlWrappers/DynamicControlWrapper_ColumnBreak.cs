using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200004A RID: 74
	public class DynamicControlWrapper_ColumnBreak : DynamicControlWrapper_Base
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00037C3C File Offset: 0x00036C3C
		public DynamicControlWrapper_ColumnBreak(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00037C48 File Offset: 0x00036C48
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00037C65 File Offset: 0x00036C65
		[Description("Percentage to increase column width by (0 for no change).  Ex. 100")]
		[Category("Display")]
		public int PercentageToIncreaseColumnWidthBy
		{
			get
			{
				return this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = value;
			}
		}
	}
}
