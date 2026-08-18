using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000077 RID: 119
	public class DynamicControlWrapper_FormSettings : DynamicControlWrapper_Base
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00047FF4 File Offset: 0x00046FF4
		public DynamicControlWrapper_FormSettings(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00048000 File Offset: 0x00047000
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0004801D File Offset: 0x0004701D
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[Category("Behaviour")]
		[Description("Custom c# code will be called after the form is rendered just after the form is displayed.")]
		public string Code_FormLoaded
		{
			get
			{
				return this.dynamicControl.HelpText;
			}
			set
			{
				this.dynamicControl.HelpText = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00048030 File Offset: 0x00047030
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0004804D File Offset: 0x0004704D
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[Description("Custom c# code will be called just before the data is saved.  True or false should be returned - true indicates saving may proceed, false indicates saving will be aborted.")]
		[Category("Behaviour")]
		public string Code_PreSave
		{
			get
			{
				return this.dynamicControl.DefaultValueString;
			}
			set
			{
				this.dynamicControl.DefaultValueString = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00048060 File Offset: 0x00047060
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0004807D File Offset: 0x0004707D
		[Category("Behaviour")]
		[Description("Custom c# code - the code placed here should define one or more custom functions.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Code_Misc
		{
			get
			{
				return this.dynamicControl.ActionHandlers;
			}
			set
			{
				this.dynamicControl.ActionHandlers = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00048090 File Offset: 0x00047090
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000480DE File Offset: 0x000470DE
		[Description("The control id of the field that should contain focus by default when the form loads.")]
		[Category("Behaviour")]
		public int DefaultActiveControl
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "activecontrol");
				int result;
				int num;
				if (string.IsNullOrEmpty(specialInstructionStringValue))
				{
					result = 0;
				}
				else if (int.TryParse(specialInstructionStringValue, out num))
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "activecontrol", (value > 0) ? value.ToString() : "");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00048114 File Offset: 0x00047114
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00048162 File Offset: 0x00047162
		[Category("Per Appointment Behaviour")]
		[Description("This is the form number of the per student form that should be included at the top.")]
		public int PerStudentScreenNumber
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "psscreennum");
				int result;
				int num;
				if (string.IsNullOrEmpty(specialInstructionStringValue))
				{
					result = 0;
				}
				else if (int.TryParse(specialInstructionStringValue, out num))
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "psscreennum", (value > 0) ? value.ToString() : "");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00048198 File Offset: 0x00047198
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000481E6 File Offset: 0x000471E6
		[Description("If using the PerStudentScreenNumber feature, then this is the override height of the top panel (use zero to indicate the default height).")]
		[Category("Per Appointment Behaviour")]
		public int PerStudentScreenNumber_Height
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "psscreennum_height");
				int result;
				int num;
				if (string.IsNullOrEmpty(specialInstructionStringValue))
				{
					result = 0;
				}
				else if (int.TryParse(specialInstructionStringValue, out num))
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "psscreennum_height", (value > 0) ? value.ToString() : "");
			}
		}
	}
}
