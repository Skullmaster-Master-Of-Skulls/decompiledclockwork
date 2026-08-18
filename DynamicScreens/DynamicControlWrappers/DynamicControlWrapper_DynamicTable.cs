using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000009 RID: 9
	public class DynamicControlWrapper_DynamicTable : DynamicControlWrapper_Base
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004727 File Offset: 0x00003727
		public DynamicControlWrapper_DynamicTable(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004734 File Offset: 0x00003734
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004751 File Offset: 0x00003751
		[Description("Indicates the number of rows high.")]
		[Category("Display")]
		public int RowCount
		{
			get
			{
				return this.dynamicControl.Setting2;
			}
			set
			{
				this.dynamicControl.Setting2 = value;
			}
		}
	}
}
