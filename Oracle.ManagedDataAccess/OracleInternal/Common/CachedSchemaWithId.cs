using System;

namespace OracleInternal.Common
{
	// Token: 0x020000AD RID: 173
	internal class CachedSchemaWithId
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00040650 File Offset: 0x0003E850
		internal CachedSchemaWithId(byte[] id, string info)
		{
			this.schemaId = id;
			this.schemaInfo = info;
		}

		// Token: 0x0400094F RID: 2383
		internal byte[] schemaId;

		// Token: 0x04000950 RID: 2384
		internal string schemaInfo;
	}
}
