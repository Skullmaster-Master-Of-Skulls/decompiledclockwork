using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x0200051F RID: 1311
	public class TestExamBookingGridViewTypeAttribute : Attribute
	{
		// Token: 0x06002894 RID: 10388 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public TestExamBookingGridViewTypeAttribute()
		{
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0002A264 File Offset: 0x00028464
		public TestExamBookingGridViewTypeAttribute(eSettingCode settingToStoreDataIn)
		{
			this.SettingToStoreDataIn = new eSettingCode?(settingToStoreDataIn);
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0002A27B File Offset: 0x0002847B
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0002A283 File Offset: 0x00028483
		public eSettingCode? SettingToStoreDataIn { get; set; }
	}
}
