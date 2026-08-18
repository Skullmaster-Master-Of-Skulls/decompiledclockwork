using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000057 RID: 87
	public class DynamicControlWrapper_AccommodationDtp : DynamicControlWrapper_Base
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0003F0B9 File Offset: 0x0003E0B9
		public DynamicControlWrapper_AccommodationDtp(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0003F0C8 File Offset: 0x0003E0C8
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0003F0E8 File Offset: 0x0003E0E8
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0003F118 File Offset: 0x0003E118
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0003F141 File Offset: 0x0003E141
		[Description("Default Value")]
		[Category("Behaviour")]
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
	}
}
