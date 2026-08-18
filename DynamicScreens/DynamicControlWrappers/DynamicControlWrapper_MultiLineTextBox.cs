using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200006B RID: 107
	public class DynamicControlWrapper_MultiLineTextBox : DynamicControlWrapper_Base
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0004259E File Offset: 0x0004159E
		public DynamicControlWrapper_MultiLineTextBox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000425AC File Offset: 0x000415AC
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000425C9 File Offset: 0x000415C9
		[Description("Indicates the number of rows this textbox should contain.  Use -1 to indicate it should fill it's container vertically.")]
		[Category("Display")]
		public int MultilineCount
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000425DC File Offset: 0x000415DC
		[ReadOnly(true)]
		[Description("Indicates whether the data for this textbox is encrypted.")]
		[Category("Design")]
		public bool Encrypted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000425EF File Offset: 0x000415EF
		public override void SetDefaultValues(DynamicControl dc)
		{
			dc.Setting3 = 1;
			dc.Setting1 = 4;
		}
	}
}
