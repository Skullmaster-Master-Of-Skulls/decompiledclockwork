using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x020002E1 RID: 737
	public static class DbDataReaderExtensions
	{
		// Token: 0x06002E40 RID: 11840 RVA: 0x001263AC File Offset: 0x001257AC
		public static ReadOnlyCollection<DbColumn> GetColumnSchema(this DbDataReader reader)
		{
			if (reader.CanGetColumnSchema())
			{
				return ((IDbColumnSchemaGenerator)reader).GetColumnSchema();
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x001263D4 File Offset: 0x001257D4
		public static bool CanGetColumnSchema(this DbDataReader reader)
		{
			return reader is IDbColumnSchemaGenerator;
		}
	}
}
