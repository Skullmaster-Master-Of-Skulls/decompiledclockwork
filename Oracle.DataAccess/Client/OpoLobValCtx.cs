using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200006B RID: 107
	internal struct OpoLobValCtx
	{
		// Token: 0x04000353 RID: 851
		internal long inAmount;

		// Token: 0x04000354 RID: 852
		internal long outAmount;

		// Token: 0x04000355 RID: 853
		internal long src_offset;

		// Token: 0x04000356 RID: 854
		internal long dst_offset;

		// Token: 0x04000357 RID: 855
		internal long lobDataLength;

		// Token: 0x04000358 RID: 856
		internal long remainder;

		// Token: 0x04000359 RID: 857
		internal long totalAmount;

		// Token: 0x0400035A RID: 858
		internal long position;

		// Token: 0x0400035B RID: 859
		internal long count;

		// Token: 0x0400035C RID: 860
		internal long offset;

		// Token: 0x0400035D RID: 861
		internal int isFromEF;

		// Token: 0x0400035E RID: 862
		internal unsafe LobProperties* pLobProperties;
	}
}
