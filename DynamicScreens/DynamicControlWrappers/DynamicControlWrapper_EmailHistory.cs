using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000056 RID: 86
	public class DynamicControlWrapper_EmailHistory : DynamicControlWrapper_Base
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0003F04D File Offset: 0x0003E04D
		public DynamicControlWrapper_EmailHistory(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0003F05C File Offset: 0x0003E05C
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0003F079 File Offset: 0x0003E079
		[Category("Behaviour")]
		[Description("Mode (0 = student, 1 = case)")]
		public int Mode
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0003F08C File Offset: 0x0003E08C
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0003F0A9 File Offset: 0x0003E0A9
		[Description("Enter the height in pixels.")]
		[Category("Display")]
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
