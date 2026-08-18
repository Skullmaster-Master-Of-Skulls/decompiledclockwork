using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000025 RID: 37
	public class DynamicControlWrapper_CalcButton : DynamicControlWrapper_Base
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00019D09 File Offset: 0x00018D09
		public DynamicControlWrapper_CalcButton(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00019D18 File Offset: 0x00018D18
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00019D35 File Offset: 0x00018D35
		[Category("Behaviour")]
		[Description("Enter the calculation (ex: 335=[332]+[335.amount]`396=[335]-[336]+[337])")]
		public string Calculation
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00019D48 File Offset: 0x00018D48
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00019D65 File Offset: 0x00018D65
		[Category("Behaviour")]
		[Description("The lookup table for the lookup function (ex: ab.c=alphabet,de.f=george)")]
		public string LookupTable
		{
			get
			{
				return this.dynamicControl.Mask;
			}
			set
			{
				this.dynamicControl.Mask = value;
			}
		}
	}
}
