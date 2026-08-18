using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200006F RID: 111
	public class DynamicControlWrapper_AppointmentHistory : DynamicControlWrapper_Base
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00042B07 File Offset: 0x00041B07
		public DynamicControlWrapper_AppointmentHistory(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00042B14 File Offset: 0x00041B14
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00042B31 File Offset: 0x00041B31
		[Category("Behaviour")]
		[Description("Enter the appointment type ids of the appointment types to include.")]
		public string AppointmentTypeIds
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

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00042B44 File Offset: 0x00041B44
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00042B61 File Offset: 0x00041B61
		[Category("Display")]
		[Description("Enter the height in pixels.")]
		public int Height
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
	}
}
