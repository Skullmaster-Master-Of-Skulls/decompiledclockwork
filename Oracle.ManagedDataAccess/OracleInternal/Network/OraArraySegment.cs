using System;

namespace OracleInternal.Network
{
	// Token: 0x0200015B RID: 347
	internal class OraArraySegment
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00092AE0 File Offset: 0x00090CE0
		internal OraArraySegment(OraBuf ob, byte[] array, int offset, int count)
		{
			this.Array = array;
			this.Count = count;
			this.Offset = offset;
			this.OB = ob;
		}

		// Token: 0x04000F46 RID: 3910
		internal OraBuf OB;

		// Token: 0x04000F47 RID: 3911
		internal int Count;

		// Token: 0x04000F48 RID: 3912
		internal int Offset;

		// Token: 0x04000F49 RID: 3913
		internal byte[] Array;

		// Token: 0x04000F4A RID: 3914
		internal int m_maxRowNum = -1;

		// Token: 0x04000F4B RID: 3915
		internal bool m_bInUseByTTCLayer;
	}
}
