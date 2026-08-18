using System;
using System.Runtime.InteropServices;

namespace OracleInternal.MTS
{
	// Token: 0x0200013D RID: 317
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoDTCTxnXIDRefCtx
	{
		// Token: 0x04000DA7 RID: 3495
		internal int m_formatID;

		// Token: 0x04000DA8 RID: 3496
		internal int m_gtrid_length;

		// Token: 0x04000DA9 RID: 3497
		internal int m_bqual_length;

		// Token: 0x04000DAA RID: 3498
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] m_data;
	}
}
