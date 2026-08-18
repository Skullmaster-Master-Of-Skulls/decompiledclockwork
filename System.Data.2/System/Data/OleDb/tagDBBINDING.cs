using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000267 RID: 615
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBBINDING
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x00104868 File Offset: 0x00103C68
		internal tagDBBINDING()
		{
		}

		// Token: 0x040017C8 RID: 6088
		internal IntPtr iOrdinal;

		// Token: 0x040017C9 RID: 6089
		internal IntPtr obValue;

		// Token: 0x040017CA RID: 6090
		internal IntPtr obLength;

		// Token: 0x040017CB RID: 6091
		internal IntPtr obStatus;

		// Token: 0x040017CC RID: 6092
		internal IntPtr pTypeInfo;

		// Token: 0x040017CD RID: 6093
		internal IntPtr pObject;

		// Token: 0x040017CE RID: 6094
		internal IntPtr pBindExt;

		// Token: 0x040017CF RID: 6095
		internal int dwPart;

		// Token: 0x040017D0 RID: 6096
		internal int dwMemOwner;

		// Token: 0x040017D1 RID: 6097
		internal int eParamIO;

		// Token: 0x040017D2 RID: 6098
		internal IntPtr cbMaxLen;

		// Token: 0x040017D3 RID: 6099
		internal int dwFlags;

		// Token: 0x040017D4 RID: 6100
		internal short wType;

		// Token: 0x040017D5 RID: 6101
		internal byte bPrecision;

		// Token: 0x040017D6 RID: 6102
		internal byte bScale;
	}
}
