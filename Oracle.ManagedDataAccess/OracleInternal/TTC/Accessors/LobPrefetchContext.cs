using System;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000204 RID: 516
	internal class LobPrefetchContext
	{
		// Token: 0x04001478 RID: 5240
		internal int m_chunkSize;

		// Token: 0x04001479 RID: 5241
		internal long m_totalLobSize;

		// Token: 0x0400147A RID: 5242
		internal bool m_bDbVaryingWidth;

		// Token: 0x0400147B RID: 5243
		internal short m_clobCharSet;

		// Token: 0x0400147C RID: 5244
		internal byte m_clobFormOfUse;

		// Token: 0x0400147D RID: 5245
		internal byte[] m_lobPrefetchData;

		// Token: 0x0400147E RID: 5246
		internal int m_lobDataLength;
	}
}
