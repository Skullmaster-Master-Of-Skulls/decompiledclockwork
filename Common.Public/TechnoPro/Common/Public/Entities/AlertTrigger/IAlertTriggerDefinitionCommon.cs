using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A8 RID: 1448
	public interface IAlertTriggerDefinitionCommon
	{
		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06002EEF RID: 12015
		// (set) Token: 0x06002EF0 RID: 12016
		string Name { get; set; }

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06002EF1 RID: 12017
		// (set) Token: 0x06002EF2 RID: 12018
		string Note { get; set; }

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06002EF3 RID: 12019
		// (set) Token: 0x06002EF4 RID: 12020
		int OrderNum { get; set; }

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06002EF5 RID: 12021
		// (set) Token: 0x06002EF6 RID: 12022
		bool IsDisabled { get; set; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06002EF7 RID: 12023
		// (set) Token: 0x06002EF8 RID: 12024
		bool DontAllowAppointmentBooking { get; set; }
	}
}
