using System;
using System.ComponentModel;
using System.Drawing;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000021 RID: 33
	internal class DynamicControlWrapper_TabControl : DynamicControlWrapper_Base
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0001964A File Offset: 0x0001864A
		public DynamicControlWrapper_TabControl(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00019658 File Offset: 0x00018658
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00019675 File Offset: 0x00018675
		[Category("Display")]
		[Description("Number of columns")]
		public int NumColumns
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00019688 File Offset: 0x00018688
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000196A5 File Offset: 0x000186A5
		[Category("Display")]
		[Description("Column width in percent (ex: 45)")]
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000196B8 File Offset: 0x000186B8
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000196DA File Offset: 0x000186DA
		[Description("Indicates the background colour.")]
		[Category("Display")]
		public Color PanelBackColour
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000196F0 File Offset: 0x000186F0
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0001970D File Offset: 0x0001870D
		[Category("Display")]
		[Description("Use zero to indicate infinite (bottomless); a number > 0 will indicate the number of controls that will fit in a column before wrapping to the top of the next column.")]
		public int NumberOfControlsInAColumn
		{
			get
			{
				return this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00019720 File Offset: 0x00018720
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0001973D File Offset: 0x0001873D
		[Description("Indicates the border style, if any.")]
		[Category("Display")]
		public PanelBorderStyle BorderStyle
		{
			get
			{
				return (PanelBorderStyle)this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = (int)value;
			}
		}
	}
}
