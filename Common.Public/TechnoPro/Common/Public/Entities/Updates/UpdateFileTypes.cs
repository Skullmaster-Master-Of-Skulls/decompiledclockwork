using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200013E RID: 318
	public static class UpdateFileTypes
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00010820 File Offset: 0x0000EA20
		public static IList<string> UpdateFileTypesList
		{
			get
			{
				return new string[]
				{
					"Database patch",
					"Database files patch",
					"Database tracking patch",
					"ClockWorkServer update",
					"ClockWorkWeb update",
					"ClockWork update"
				};
			}
		}

		// Token: 0x0400062C RID: 1580
		public const string CLOCKWORK_UPDATE_FILE_TYPE = "ClockWork update";

		// Token: 0x0400062D RID: 1581
		public const string CLOCKWORKSERVER_UPDATE_FILE_TYPE = "ClockWorkServer update";

		// Token: 0x0400062E RID: 1582
		public const string CLOCKWORKWEB_UPDATE_FILE_TYPE = "ClockWorkWeb update";

		// Token: 0x0400062F RID: 1583
		public const string DATABASE_PATCH_UPDATE_FILE_TYPE = "Database patch";

		// Token: 0x04000630 RID: 1584
		public const string DATABASE_FILES_PATCH_UPDATE_FILE_TYPE = "Database files patch";

		// Token: 0x04000631 RID: 1585
		public const string DATABASE_TRACKING_PATCH_UPDATE_FILE_TYPE = "Database tracking patch";
	}
}
