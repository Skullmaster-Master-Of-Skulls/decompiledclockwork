using System;
using System.ComponentModel;
using System.Drawing;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200000D RID: 13
	public class DynamicControlWrapper_HRule : DynamicControlWrapper_Base
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00007EB1 File Offset: 0x00006EB1
		public DynamicControlWrapper_HRule(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00007EC0 File Offset: 0x00006EC0
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00007EDD File Offset: 0x00006EDD
		[Category("Display")]
		[Description("Indicates the height of the horizontal rule in pixels.  The default height is 1 pixel.")]
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00007EF0 File Offset: 0x00006EF0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00007F12 File Offset: 0x00006F12
		[Description("Indicates the colour of the horizontal rule line.")]
		[Category("Display")]
		public Color ForeGroundColour
		{
			get
			{
				return Color.FromArgb(this.dynamicControl.Setting2);
			}
			set
			{
				this.dynamicControl.Setting2 = value.ToArgb();
			}
		}
	}
}
