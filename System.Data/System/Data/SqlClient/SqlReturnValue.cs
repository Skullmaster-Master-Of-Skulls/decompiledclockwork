using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200032D RID: 813
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x06002A6D RID: 10861 RVA: 0x002BEA48 File Offset: 0x002BDE48
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x04001BF8 RID: 7160
		internal ushort parmIndex;

		// Token: 0x04001BF9 RID: 7161
		internal string parameter;

		// Token: 0x04001BFA RID: 7162
		internal readonly SqlBuffer value;
	}
}
