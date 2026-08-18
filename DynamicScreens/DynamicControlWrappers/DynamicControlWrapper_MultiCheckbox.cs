using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000065 RID: 101
	public class DynamicControlWrapper_MultiCheckbox : DynamicControlWrapper_Base
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00041CCC File Offset: 0x00040CCC
		public DynamicControlWrapper_MultiCheckbox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00041CD8 File Offset: 0x00040CD8
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00041CF5 File Offset: 0x00040CF5
		[Description("Number of checkboxes")]
		[Category("Display")]
		public int CheckboxCount
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00041D08 File Offset: 0x00040D08
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00041D25 File Offset: 0x00040D25
		[Category("Display")]
		[Description("Font size percentage")]
		public int FontSize
		{
			get
			{
				return this.dynamicControl.Setting3;
			}
			set
			{
				this.dynamicControl.Setting3 = value;
			}
		}
	}
}
