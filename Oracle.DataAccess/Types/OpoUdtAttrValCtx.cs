using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000021 RID: 33
	[StructLayout(LayoutKind.Explicit)]
	internal struct OpoUdtAttrValCtx
	{
		// Token: 0x040000D3 RID: 211
		[FieldOffset(0)]
		public byte m_byte;

		// Token: 0x040000D4 RID: 212
		[FieldOffset(0)]
		public short m_short;

		// Token: 0x040000D5 RID: 213
		[FieldOffset(0)]
		public int m_int;

		// Token: 0x040000D6 RID: 214
		[FieldOffset(0)]
		public long m_long;

		// Token: 0x040000D7 RID: 215
		[FieldOffset(0)]
		public float m_float;

		// Token: 0x040000D8 RID: 216
		[FieldOffset(0)]
		public double m_double;
	}
}
