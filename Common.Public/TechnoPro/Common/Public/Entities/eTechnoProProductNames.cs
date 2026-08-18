using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public enum eTechnoProProductNames
	{
		// Token: 0x04000210 RID: 528
		[ProductDescription("Unknown", "Unknown product type")]
		Unknown,
		// Token: 0x04000211 RID: 529
		[ProductDescription("ClockWork", "ClockWork Enterprise client application")]
		ClockWork,
		// Token: 0x04000212 RID: 530
		[ProductDescription("ClockWork Web", "ClockWork Web application")]
		ClockWorkWeb,
		// Token: 0x04000213 RID: 531
		[ProductDescription("ClockWork Server", "ClockWork Server Enterprise application")]
		ClockWorkServer,
		// Token: 0x04000214 RID: 532
		[ProductDescription("Outlook Sync", "ClockWork - Outlook Sync application")]
		OutlookSync,
		// Token: 0x04000215 RID: 533
		[ProductDescription("Google Sync", "ClockWork - Google Sync application")]
		GoogleSync,
		// Token: 0x04000216 RID: 534
		[ProductDescription("Updating System", "ClockWork Updating System application")]
		UpdatingSystem,
		// Token: 0x04000217 RID: 535
		[ProductDescription("Daily Jobs", "ClockWork Daily Jobs application")]
		DailyJobs,
		// Token: 0x04000218 RID: 536
		[ProductDescription("ClockWork Web Updating Service", "ClockWork Web Updating System application")]
		ClockWorkWebUpdatingService
	}
}
