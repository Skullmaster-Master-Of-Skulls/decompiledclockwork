using System;
using System.ComponentModel;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200001B RID: 27
	public class DynamicControlWrapper_Date : DynamicControlWrapper_Base
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00017804 File Offset: 0x00016804
		public DynamicControlWrapper_Date(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00017810 File Offset: 0x00016810
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0001784F File Offset: 0x0001684F
		[Description("Indicates the placement of the associated label.")]
		[Category("Display")]
		public eLabelOrientation LabelOrientation
		{
			get
			{
				int setting = this.dynamicControl.Setting4;
				eLabelOrientation result;
				if (Enum.IsDefined(typeof(eLabelOrientation), setting))
				{
					result = (eLabelOrientation)setting;
				}
				else
				{
					result = eLabelOrientation.LabelLeft;
				}
				return result;
			}
			set
			{
				this.dynamicControl.Setting4 = (int)value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00017860 File Offset: 0x00016860
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00017889 File Offset: 0x00016889
		[Category("Behaviour")]
		[Description("Default Value")]
		public DateDefaultValue DefaultSelection
		{
			get
			{
				int defaultValue = this.dynamicControl.DefaultValue;
				DateDefaultValue result;
				if (defaultValue != 1)
				{
					result = DateDefaultValue.Blank;
				}
				else
				{
					result = DateDefaultValue.Current_date;
				}
				return result;
			}
			set
			{
				this.dynamicControl.DefaultValue = (int)value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0001789C File Offset: 0x0001689C
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000178BC File Offset: 0x000168BC
		[Description("This date picker will show additional functionality so that it can be more easily used for the specific task of indicating when all of the student's accommodations will expire.")]
		[Category("Accommodation specific")]
		public bool ActAsAccommodationsExpiryDate
		{
			get
			{
				return this.dynamicControl.Setting2 == 1;
			}
			set
			{
				this.dynamicControl.Setting2 = (value ? 1 : 0);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000178D4 File Offset: 0x000168D4
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000178F1 File Offset: 0x000168F1
		[Category("Accommodation specific")]
		[Description("0 means no border, 1 means solid border, 2 means 3d border")]
		public BorderStyle BorderStyle
		{
			get
			{
				return (BorderStyle)this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = (int)value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00017904 File Offset: 0x00016904
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00017921 File Offset: 0x00016921
		[Category("Accommodation specific")]
		[Description("The value for this propery will override the 'Default selection' field on the regular date control.")]
		public DateDefaultValue OverrideDefaultSelection
		{
			get
			{
				return (DateDefaultValue)this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = (int)value;
			}
		}
	}
}
