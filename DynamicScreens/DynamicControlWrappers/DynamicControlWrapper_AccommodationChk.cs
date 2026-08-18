using System;
using System.ComponentModel;
using System.Drawing;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000018 RID: 24
	public class DynamicControlWrapper_AccommodationChk : DynamicControlWrapper_Base
	{
		// Token: 0x06000193 RID: 403 RVA: 0x0001618D File Offset: 0x0001518D
		public DynamicControlWrapper_AccommodationChk(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0001619C File Offset: 0x0001519C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000161BC File Offset: 0x000151BC
		[Description("Indent (number of pixels to pad on the left of the control)")]
		[Category("Display")]
		public int Indent
		{
			get
			{
				return this.dynamicControl.DefaultValue >> 1;
			}
			set
			{
				int num = this.dynamicControl.DefaultValue & 1;
				this.dynamicControl.DefaultValue = (value << 1) + num;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000161EC File Offset: 0x000151EC
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00016209 File Offset: 0x00015209
		[Description("Enter the control id of the field that will trigger this box to check when that field is changed.")]
		[Category("Behaviour")]
		public int ControlIdThatTriggersThisCheckboxToCheck
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0001621C File Offset: 0x0001521C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00016239 File Offset: 0x00015239
		[Description("Control Id to EnableOrDisableWhenThisCheckboxIsCheckedUnchecked.")]
		[Category("Behaviour")]
		public int ControlIdToEnableOrDisable
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0001624C File Offset: 0x0001524C
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00016269 File Offset: 0x00015269
		[Description("Font size percentage")]
		[Category("Display")]
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0001627C File Offset: 0x0001527C
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000162B4 File Offset: 0x000152B4
		[Category("Display")]
		[Description("Background Colour")]
		public Color BackgroundColour
		{
			get
			{
				return (this.dynamicControl.Setting4 == 0) ? Color.Transparent : Color.FromArgb(this.dynamicControl.Setting4);
			}
			set
			{
				if (value == Color.Transparent)
				{
					this.dynamicControl.Setting4 = 0;
				}
				else
				{
					this.dynamicControl.Setting4 = value.ToArgb();
				}
			}
		}
	}
}
