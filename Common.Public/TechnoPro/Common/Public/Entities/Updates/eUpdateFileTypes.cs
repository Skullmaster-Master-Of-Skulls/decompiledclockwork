using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public enum eUpdateFileTypes
	{
		// Token: 0x04000633 RID: 1587
		[UpdateFileType("Database patch", "txt", false)]
		Database_patch,
		// Token: 0x04000634 RID: 1588
		[UpdateFileType("Database files patch", "txt", false)]
		Database_files_patch,
		// Token: 0x04000635 RID: 1589
		[UpdateFileType("Database tracking patch", "txt", false)]
		Database_tracking_patch,
		// Token: 0x04000636 RID: 1590
		[UpdateFileType("ClockWorkServer update", "zip", true)]
		ClockWorkServer_update,
		// Token: 0x04000637 RID: 1591
		[UpdateFileType("ClockWorkWeb update", "zip", true)]
		ClockWorkWeb_update,
		// Token: 0x04000638 RID: 1592
		[UpdateFileType("ClockWork update", "zip", true)]
		ClockWork_update
	}
}
