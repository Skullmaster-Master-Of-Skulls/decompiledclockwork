using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x0200051E RID: 1310
	[Serializable]
	public enum eTestExamBookingGridViewType
	{
		// Token: 0x04001D76 RID: 7542
		[TestExamBookingGridViewType]
		Unknown,
		// Token: 0x04001D77 RID: 7543
		[TestExamBookingGridViewType(eSettingCode.SETTING_TestsGridTemplateOverride)]
		Bookings,
		// Token: 0x04001D78 RID: 7544
		[TestExamBookingGridViewType]
		Calendar,
		// Token: 0x04001D79 RID: 7545
		[TestExamBookingGridViewType]
		ClassTestDefinitions,
		// Token: 0x04001D7A RID: 7546
		[TestExamBookingGridViewType]
		UnbookedStudents
	}
}
