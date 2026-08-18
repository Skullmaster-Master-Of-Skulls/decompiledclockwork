using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200000E RID: 14
	public class DynamicControlWrapper_BlankSpace : DynamicControlWrapper_Base
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00007F28 File Offset: 0x00006F28
		public DynamicControlWrapper_BlankSpace(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007F34 File Offset: 0x00006F34
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00007F51 File Offset: 0x00006F51
		[Category("Display")]
		[Description("Height (percentage of normal height) eg. 50, 200")]
		public int Height
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00007F64 File Offset: 0x00006F64
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00007F81 File Offset: 0x00006F81
		[Description("Width (percentage of column width) eg. 50, 200")]
		[Category("Display")]
		public int WidthPercentage
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007F94 File Offset: 0x00006F94
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00007FB1 File Offset: 0x00006FB1
		[Category("Display")]
		[Description("Width in pixels (overrides percentage width)")]
		public int WidthPixels
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
