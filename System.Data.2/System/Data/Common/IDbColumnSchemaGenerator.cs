using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x020002E0 RID: 736
	public interface IDbColumnSchemaGenerator
	{
		// Token: 0x06002E3F RID: 11839
		ReadOnlyCollection<DbColumn> GetColumnSchema();
	}
}
