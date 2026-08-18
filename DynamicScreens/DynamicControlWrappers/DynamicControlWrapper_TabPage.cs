using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200000C RID: 12
	public class DynamicControlWrapper_TabPage : DynamicControlWrapper_Base
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00007E78 File Offset: 0x00006E78
		public DynamicControlWrapper_TabPage(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00007E84 File Offset: 0x00006E84
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00007EA1 File Offset: 0x00006EA1
		[Category("Display")]
		[Description("Width of column (percentage, eg. 33)")]
		public int ColumnWidth
		{
			get
			{
				return this.dynamicControl.Setting4;
			}
			set
			{
				this.dynamicControl.Setting4 = value;
			}
		}
	}
}
