using System;
using System.ComponentModel;
using System.Drawing;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000073 RID: 115
	public class DynamicControlWrapper_PanelStart : DynamicControlWrapper_Base
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00042EFA File Offset: 0x00041EFA
		public DynamicControlWrapper_PanelStart(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00042F08 File Offset: 0x00041F08
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00042F25 File Offset: 0x00041F25
		[Category("Display")]
		[Description("Indicates the border style, if any.")]
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

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00042F38 File Offset: 0x00041F38
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00042F5A File Offset: 0x00041F5A
		[Category("Display")]
		[Description("Indicates the background colour of the control.")]
		public Color BackgroundColour
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

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00042F70 File Offset: 0x00041F70
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00042F8D File Offset: 0x00041F8D
		[Description("Indicates the number of controls that will fit vertically in a single column.  Once this number is reached, layout flow wraps to the top of the next column.")]
		[Category("Display")]
		public int NumberOfControlsInOneColumn
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

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00042FA0 File Offset: 0x00041FA0
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00042FBD File Offset: 0x00041FBD
		[Category("Display")]
		[Description("Indicates the number of columns this panel should be broken into.  The width of the columns will depend on the total available width of this panel.")]
		public int NumberOfColumns
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

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00042FD0 File Offset: 0x00041FD0
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00042FF0 File Offset: 0x00041FF0
		[Description("Should this control act as a layout panel (you must specify at least number of columns.")]
		[Category("Display")]
		public bool IsLayoutPanel
		{
			get
			{
				return this.dynamicControl.Setting4 > 0;
			}
			set
			{
				if (value)
				{
					if (this.dynamicControl.Setting4 <= 0)
					{
						this.dynamicControl.Setting4 = 1;
					}
				}
				else
				{
					this.dynamicControl.Setting4 = 0;
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00043038 File Offset: 0x00042038
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00043068 File Offset: 0x00042068
		[Description("The number of rows (only works if IsLayoutPanel=true)")]
		[Category("Display")]
		public int NumberOfRows
		{
			get
			{
				return (this.dynamicControl.Setting4 <= 0) ? 0 : (this.dynamicControl.Setting4 - 1);
			}
			set
			{
				this.dynamicControl.Setting4 = value + 1;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0004307C File Offset: 0x0004207C
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00043094 File Offset: 0x00042094
		[Description("Should this group box be an expandable box (click to expand/contract).")]
		[Category("Display")]
		[Browsable(false)]
		public override bool DontWrapToNextLine
		{
			get
			{
				return base.DontWrapToNextLine;
			}
			set
			{
				base.DontWrapToNextLine = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000430A0 File Offset: 0x000420A0
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x000430B8 File Offset: 0x000420B8
		[Category("Display")]
		[Description("Should this group box be an expandable box (click to expand/contract).")]
		public bool Expandable
		{
			get
			{
				return base.DontWrapToNextLine;
			}
			set
			{
				base.DontWrapToNextLine = value;
			}
		}
	}
}
