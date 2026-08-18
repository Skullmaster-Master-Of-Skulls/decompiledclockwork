using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkAudit
{
	// Token: 0x02000463 RID: 1123
	[Serializable]
	public enum eClockWorkAuditType
	{
		// Token: 0x040019B2 RID: 6578
		[ClockWorkAuditType("Unknown", "", IsDisabled = true)]
		Unknown,
		// Token: 0x040019B3 RID: 6579
		[ClockWorkAuditType("Check login settings are configured", "Checks if the login settings have a configuration.  Does not check if the configuration works.")]
		LoginSettings,
		// Token: 0x040019B4 RID: 6580
		[ClockWorkAuditType("Check DataSync reports", "")]
		DataSyncReports,
		// Token: 0x040019B5 RID: 6581
		[ClockWorkAuditType("Check referenced controlids in settings", "")]
		ControlIdsInSettings,
		// Token: 0x040019B6 RID: 6582
		[ClockWorkAuditType("Check accommodation letter", "")]
		AccommodationLetters,
		// Token: 0x040019B7 RID: 6583
		[ClockWorkAuditType("Check active form controls with lists", "")]
		ListControls,
		// Token: 0x040019B8 RID: 6584
		[ClockWorkAuditType("Check daily job active", "")]
		DailyJob,
		// Token: 0x040019B9 RID: 6585
		[ClockWorkAuditType("Check Point of Contact", "")]
		PointOfContact,
		// Token: 0x040019BA RID: 6586
		[ClockWorkAuditType("Check Email", "")]
		Smtp,
		// Token: 0x040019BB RID: 6587
		[ClockWorkAuditType("Check miscellaneous", "")]
		Misc,
		// Token: 0x040019BC RID: 6588
		[ClockWorkAuditType("Check database connections", "")]
		Database,
		// Token: 0x040019BD RID: 6589
		[ClockWorkAuditType("Check self registration button is enabled, web is turned on", "")]
		SelfRegButton,
		// Token: 0x040019BE RID: 6590
		[ClockWorkAuditType("Check Use-ClockWork-Server is enabled", "")]
		UsingClockWorkServer
	}
}
