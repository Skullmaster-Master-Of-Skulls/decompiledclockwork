using System;
using System.Collections.Generic;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000205 RID: 517
	internal class LobPrefetchInfo
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x000CCA6C File Offset: 0x000CAC6C
		internal LobPrefetchInfo(int numRowsRequested)
		{
			this.ReInit(numRowsRequested);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000CCA7C File Offset: 0x000CAC7C
		internal void ReInit(int numRowsRequested)
		{
			if (this.m_prefetchedData == null || this.m_prefetchedData.Length < numRowsRequested)
			{
				this.m_prefetchedData = new List<ArraySegment<byte>>[numRowsRequested];
				this.m_prefetchedDataLength = new long[numRowsRequested];
				this.m_totalLobSizeInDB = new long[numRowsRequested];
			}
		}

		// Token: 0x0400147F RID: 5247
		internal int m_chunkSize;

		// Token: 0x04001480 RID: 5248
		internal bool m_bDbVaryingWidth;

		// Token: 0x04001481 RID: 5249
		internal short m_clobCharSet;

		// Token: 0x04001482 RID: 5250
		internal byte m_clobFormOfUse;

		// Token: 0x04001483 RID: 5251
		internal List<ArraySegment<byte>>[] m_prefetchedData;

		// Token: 0x04001484 RID: 5252
		internal long[] m_totalLobSizeInDB;

		// Token: 0x04001485 RID: 5253
		internal long[] m_prefetchedDataLength;
	}
}
