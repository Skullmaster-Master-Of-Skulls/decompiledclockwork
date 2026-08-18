using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040E RID: 1038
	[Flags]
	[Serializable]
	public enum eMigrationDataItemStatus
	{
		// Token: 0x0400184C RID: 6220
		Unknown = 0,
		// Token: 0x0400184D RID: 6221
		Successful = 1,
		// Token: 0x0400184E RID: 6222
		Failed = 2,
		// Token: 0x0400184F RID: 6223
		Ignored = 4,
		// Token: 0x04001850 RID: 6224
		MissingStudent = 8,
		// Token: 0x04001851 RID: 6225
		NoData = 16,
		// Token: 0x04001852 RID: 6226
		SuccessfulAndNoData = 17,
		// Token: 0x04001853 RID: 6227
		MissingMapper = 32,
		// Token: 0x04001854 RID: 6228
		MissingClockWorkField = 64,
		// Token: 0x04001855 RID: 6229
		UnSupportedControlCode = 128,
		// Token: 0x04001856 RID: 6230
		InvalidData = 256,
		// Token: 0x04001857 RID: 6231
		FailedToFindLookupListItem = 512,
		// Token: 0x04001858 RID: 6232
		CantFindOrCreatePerDateEntryId = 1024,
		// Token: 0x04001859 RID: 6233
		SuccessfulDeleteData = 2048,
		// Token: 0x0400185A RID: 6234
		FailedToParseBase64FileData = 4096,
		// Token: 0x0400185B RID: 6235
		SuccessfulSkippedBecauseDataAlreadyExistsInClockWork = 8192
	}
}
