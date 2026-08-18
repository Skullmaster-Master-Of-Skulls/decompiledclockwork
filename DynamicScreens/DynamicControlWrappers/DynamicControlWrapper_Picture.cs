using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000022 RID: 34
	public class DynamicControlWrapper_Picture : DynamicControlWrapper_Base
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0001974D File Offset: 0x0001874D
		public DynamicControlWrapper_Picture(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0001975C File Offset: 0x0001875C
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00019779 File Offset: 0x00018779
		[Category("Display")]
		[Description("Indicates the height of the picture box in pixels.")]
		public int HeightInPixels
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0001978C File Offset: 0x0001878C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000197A9 File Offset: 0x000187A9
		[Description("Indicates the border style of the picture box.")]
		[Category("Display")]
		public BorderStyle BorderStyle
		{
			get
			{
				return (BorderStyle)this.dynamicControl.Setting2;
			}
			set
			{
				this.dynamicControl.Setting2 = (int)value;
			}
		}
	}
}
