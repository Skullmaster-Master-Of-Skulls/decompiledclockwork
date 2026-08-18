using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040C RID: 1036
	[Flags]
	[Serializable]
	public enum eMigrationCreateStudentStatus
	{
		// Token: 0x04001840 RID: 6208
		Unknown = 0,
		// Token: 0x04001841 RID: 6209
		Successful = 1,
		// Token: 0x04001842 RID: 6210
		Failed = 2,
		// Token: 0x04001843 RID: 6211
		StudentAlreadyExistsInClockWork = 4,
		// Token: 0x04001844 RID: 6212
		MissingStudentNumber = 8,
		// Token: 0x04001845 RID: 6213
		FailedToCreatePersonInClockWorkDatabase = 16
	}
}
