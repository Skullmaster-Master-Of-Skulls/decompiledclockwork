using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040A RID: 1034
	[Flags]
	[Serializable]
	public enum eMigrationAppointmentItemStatus
	{
		// Token: 0x04001832 RID: 6194
		Unknown = 0,
		// Token: 0x04001833 RID: 6195
		Successful = 1,
		// Token: 0x04001834 RID: 6196
		Failed = 2,
		// Token: 0x04001835 RID: 6197
		Ignored = 4,
		// Token: 0x04001836 RID: 6198
		MissingClockWorkStudent = 8,
		// Token: 0x04001837 RID: 6199
		MissingClockWorkStaff = 16,
		// Token: 0x04001838 RID: 6200
		InvalidDateTimes = 32,
		// Token: 0x04001839 RID: 6201
		AppAlreadyExistsInClockWork = 64,
		// Token: 0x0400183A RID: 6202
		UnableToCreateAppInClockWorkDatabase = 128
	}
}
