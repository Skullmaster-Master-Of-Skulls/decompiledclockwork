using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000055 RID: 85
	public class DynamicControlWrapper_PMTable : DynamicControlWrapper_Base
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0003EFB1 File Offset: 0x0003DFB1
		public DynamicControlWrapper_PMTable(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0003EFC0 File Offset: 0x0003DFC0
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0003EFDD File Offset: 0x0003DFDD
		[Description("Enter the control ids of the fields that will show in the case summary list.")]
		[Category("Behaviour")]
		public string ControlIds
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0003EFF0 File Offset: 0x0003DFF0
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0003F00D File Offset: 0x0003E00D
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0003F020 File Offset: 0x0003E020
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0003F03D File Offset: 0x0003E03D
		[Description("The case form number.")]
		[Category("Behaviour")]
		public int FormNumber
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
	}
}
