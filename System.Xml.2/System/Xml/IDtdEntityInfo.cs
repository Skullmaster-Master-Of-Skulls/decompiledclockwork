using System;

namespace System.Xml
{
	// Token: 0x020000A9 RID: 169
	internal interface IDtdEntityInfo
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005D9 RID: 1497
		string Name { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005DA RID: 1498
		bool IsExternal { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005DB RID: 1499
		bool IsDeclaredInExternal { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005DC RID: 1500
		bool IsUnparsedEntity { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005DD RID: 1501
		bool IsParameterEntity { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005DE RID: 1502
		string BaseUriString { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005DF RID: 1503
		string DeclaredUriString { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005E0 RID: 1504
		string SystemId { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005E1 RID: 1505
		string PublicId { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005E2 RID: 1506
		string Text { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005E3 RID: 1507
		int LineNumber { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005E4 RID: 1508
		int LinePosition { get; }
	}
}
