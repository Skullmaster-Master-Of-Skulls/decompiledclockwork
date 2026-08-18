using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000080 RID: 128
	public class DynamicControlWrapper_InfoBox : DynamicControlWrapper_Base
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00048A94 File Offset: 0x00047A94
		public DynamicControlWrapper_InfoBox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00048AA0 File Offset: 0x00047AA0
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00048ABD File Offset: 0x00047ABD
		[Description("Control id that specifies the lookup user (on this form)")]
		[Category("Behaviour")]
		public int ControlWithUser
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00048AD0 File Offset: 0x00047AD0
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00048AED File Offset: 0x00047AED
		[Description("Control id that holds the lookup data for the lookup user (not on this form)")]
		[Category("Behaviour")]
		public int ControlWithData
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00048B00 File Offset: 0x00047B00
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00048B1D File Offset: 0x00047B1D
		[Category("Display")]
		[Description("Height")]
		public int Height
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
	}
}
