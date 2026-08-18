using System;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x02000056 RID: 86
	public interface IOracleMetadata
	{
		// Token: 0x060003BF RID: 959
		IEnumerable<OracleLpTableColumns> GetColumnInformation(IEnumerable<OracleLpTable> tables);
	}
}
