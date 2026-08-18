using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000226 RID: 550
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x000ED044 File Offset: 0x000EC444
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x040014A5 RID: 5285
		internal ushort parmIndex;

		// Token: 0x040014A6 RID: 5286
		internal string parameter;

		// Token: 0x040014A7 RID: 5287
		internal readonly SqlBuffer value;
	}
}
