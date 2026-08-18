using System;
using System.ComponentModel;
using System.Drawing;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000024 RID: 36
	public class DynamicControlWrapper_Checkbox : DynamicControlWrapper_Base
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00019905 File Offset: 0x00018905
		public DynamicControlWrapper_Checkbox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00019914 File Offset: 0x00018914
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00019931 File Offset: 0x00018931
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00019944 File Offset: 0x00018944
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00019968 File Offset: 0x00018968
		[Description("Is this checkbox checked by default?")]
		[Category("Behaviour")]
		public bool DefaultChecked
		{
			get
			{
				return (this.dynamicControl.DefaultValue & 1) == 1;
			}
			set
			{
				int num = this.dynamicControl.DefaultValue >> 1;
				this.dynamicControl.DefaultValue = (num << 1) + (value ? 1 : 0);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0001999C File Offset: 0x0001899C
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000199BC File Offset: 0x000189BC
		[Category("Display")]
		[Description("Indent (number of pixels to pad on the left of the control)")]
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

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000199EC File Offset: 0x000189EC
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00019A09 File Offset: 0x00018A09
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00019A1C File Offset: 0x00018A1C
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00019A39 File Offset: 0x00018A39
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00019A4C File Offset: 0x00018A4C
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00019A84 File Offset: 0x00018A84
		[Description("Background Colour")]
		[Category("Display")]
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00019AC8 File Offset: 0x00018AC8
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00019B06 File Offset: 0x00018B06
		[Category("Behaviour")]
		[Description("If this checkbox is being used in a primary/secondary role, then should the primary radiobutton be hidden?")]
		public bool PrimarySecondary_HidePrimary
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "hideprimary");
				return !string.IsNullOrEmpty(specialInstructionStringValue) && specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "hideprimary", value ? "1" : "");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00019B3C File Offset: 0x00018B3C
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00019B7A File Offset: 0x00018B7A
		[Category("Behaviour")]
		[Description("If this checkbox is being used in a primary/secondary role, then should this secondary checkbox be hidden?")]
		public bool PrimarySecondary_HideSecondary
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "hidesecondary");
				return !string.IsNullOrEmpty(specialInstructionStringValue) && specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "hidesecondary", value ? "1" : "");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00019BB0 File Offset: 0x00018BB0
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00019BEE File Offset: 0x00018BEE
		[Category("Behaviour")]
		[Description("If this checkbox is being used in a primary/secondary role, then should the primary radiobutton be disabled?")]
		public bool PrimarySecondary_DisablePrimary
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "disableprimary");
				return !string.IsNullOrEmpty(specialInstructionStringValue) && specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "disableprimary", value ? "1" : "");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00019C24 File Offset: 0x00018C24
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00019C62 File Offset: 0x00018C62
		[Category("Behaviour")]
		[Description("If this checkbox is being used in a primary/secondary role, then should this secondary checkbox be disabled?")]
		public bool PrimarySecondary_DisableSecondary
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "disablesecondary");
				return !string.IsNullOrEmpty(specialInstructionStringValue) && specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "disablesecondary", value ? "1" : "");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00019C98 File Offset: 0x00018C98
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00019CD6 File Offset: 0x00018CD6
		[Description("If this checkbox is being used in a primary/secondary role, can the user check both the primary and secondary for this field?")]
		[Category("Behaviour")]
		public bool PrimarySecondary_AllowBoth
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "allowboth");
				return !string.IsNullOrEmpty(specialInstructionStringValue) && specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "allowboth", value ? "1" : "");
			}
		}
	}
}
