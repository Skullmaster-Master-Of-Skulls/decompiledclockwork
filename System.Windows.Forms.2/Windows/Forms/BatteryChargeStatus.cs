using System;

namespace System.Windows.Forms
{
	// Token: 0x02000321 RID: 801
	[Flags]
	public enum BatteryChargeStatus
	{
		// Token: 0x04001EB3 RID: 7859
		High = 1,
		// Token: 0x04001EB4 RID: 7860
		Low = 2,
		// Token: 0x04001EB5 RID: 7861
		Critical = 4,
		// Token: 0x04001EB6 RID: 7862
		Charging = 8,
		// Token: 0x04001EB7 RID: 7863
		NoSystemBattery = 128,
		// Token: 0x04001EB8 RID: 7864
		Unknown = 255
	}
}
