using System;

namespace TechnoPro.Common.Public.Entities.Snapshot
{
	// Token: 0x020001B4 RID: 436
	public class SnapshotAreaAttribute : Attribute
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public SnapshotAreaAttribute()
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00013FDA File Offset: 0x000121DA
		public SnapshotAreaAttribute(string generateQueriesMethodName)
		{
			this.GenerateQueriesMethodName = generateQueriesMethodName;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00013FEC File Offset: 0x000121EC
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00013FF4 File Offset: 0x000121F4
		public string GenerateQueriesMethodName { get; set; }
	}
}
