using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000081 RID: 129
	public class DynamicControlWrapper_DynamicControlsChooser : DynamicControlWrapper_Base
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00048B2D File Offset: 0x00047B2D
		public DynamicControlWrapper_DynamicControlsChooser(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00048B3C File Offset: 0x00047B3C
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00048B59 File Offset: 0x00047B59
		[Description("Enter the height in pixels for this control (0 height means to use the default).")]
		[Category("Display")]
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00048B6C File Offset: 0x00047B6C
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00048B89 File Offset: 0x00047B89
		[Description("Enter the form type to show (0 = per student, 1 = per appointment).")]
		[Category("Design")]
		public int FormType
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

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00048B9C File Offset: 0x00047B9C
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00048BBC File Offset: 0x00047BBC
		[Description("Show disabled forms")]
		[Category("Design")]
		public bool ShowDisabledForms
		{
			get
			{
				return this.dynamicControl.Setting3 == 1;
			}
			set
			{
				this.dynamicControl.Setting3 = (value ? 1 : 0);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00048BD4 File Offset: 0x00047BD4
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00048BF1 File Offset: 0x00047BF1
		[Category("Design")]
		[Description("Enter a comma-separated list of control ids that should be checked by default.")]
		public string DefaultSelectedControlIds
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
	}
}
