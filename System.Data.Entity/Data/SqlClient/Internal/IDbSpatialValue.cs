using System;
using System.Data.Metadata.Edm;

namespace System.Data.SqlClient.Internal
{
	// Token: 0x0200003E RID: 62
	internal interface IDbSpatialValue
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000550 RID: 1360
		bool IsGeography { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000551 RID: 1361
		PrimitiveTypeKind PrimitiveType { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000552 RID: 1362
		object ProviderValue { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000553 RID: 1363
		int? CoordinateSystemId { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000554 RID: 1364
		string WellKnownText { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000555 RID: 1365
		byte[] WellKnownBinary { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000556 RID: 1366
		string GmlString { get; }

		// Token: 0x06000557 RID: 1367
		Exception NotSqlCompatible();
	}
}
