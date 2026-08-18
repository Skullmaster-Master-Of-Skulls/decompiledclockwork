using System;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000D RID: 13
	internal interface IDbSpatialValue
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000086 RID: 134
		bool IsGeography { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000087 RID: 135
		object ProviderValue { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000088 RID: 136
		int? CoordinateSystemId { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000089 RID: 137
		string WellKnownText { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008A RID: 138
		byte[] WellKnownBinary { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008B RID: 139
		string GmlString { get; }

		// Token: 0x0600008C RID: 140
		Exception NotSqlCompatible();
	}
}
