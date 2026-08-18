using System;

namespace OracleInternal.Common
{
	// Token: 0x020000AC RID: 172
	internal class CachedSchemaWithUrl
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00040638 File Offset: 0x0003E838
		internal CachedSchemaWithUrl(string url, string info)
		{
			this.schemaUrl = url;
			this.schemaInfo = info;
		}

		// Token: 0x0400094D RID: 2381
		internal string schemaUrl;

		// Token: 0x0400094E RID: 2382
		internal string schemaInfo;
	}
}
