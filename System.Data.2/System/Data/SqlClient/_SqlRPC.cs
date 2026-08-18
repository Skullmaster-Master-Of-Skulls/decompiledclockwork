using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000225 RID: 549
	internal sealed class _SqlRPC
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x000ED000 File Offset: 0x000EC400
		internal string GetCommandTextOrRpcName()
		{
			if (10 == this.ProcID)
			{
				return (string)this.parameters[0].Value;
			}
			return this.rpcName;
		}

		// Token: 0x04001496 RID: 5270
		internal string rpcName;

		// Token: 0x04001497 RID: 5271
		internal string databaseName;

		// Token: 0x04001498 RID: 5272
		internal ushort ProcID;

		// Token: 0x04001499 RID: 5273
		internal ushort options;

		// Token: 0x0400149A RID: 5274
		internal SqlParameter[] parameters;

		// Token: 0x0400149B RID: 5275
		internal byte[] paramoptions;

		// Token: 0x0400149C RID: 5276
		internal int? recordsAffected;

		// Token: 0x0400149D RID: 5277
		internal int cumulativeRecordsAffected;

		// Token: 0x0400149E RID: 5278
		internal int errorsIndexStart;

		// Token: 0x0400149F RID: 5279
		internal int errorsIndexEnd;

		// Token: 0x040014A0 RID: 5280
		internal SqlErrorCollection errors;

		// Token: 0x040014A1 RID: 5281
		internal int warningsIndexStart;

		// Token: 0x040014A2 RID: 5282
		internal int warningsIndexEnd;

		// Token: 0x040014A3 RID: 5283
		internal SqlErrorCollection warnings;

		// Token: 0x040014A4 RID: 5284
		internal bool needsFetchParameterEncryptionMetadata;
	}
}
