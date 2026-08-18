using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000248 RID: 584
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROP
	{
		// Token: 0x0600205D RID: 8285 RVA: 0x00280008 File Offset: 0x0027F408
		internal tagDBPROP()
		{
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00280028 File Offset: 0x0027F428
		internal tagDBPROP(int propertyID, bool required, object value)
		{
			this.dwPropertyID = propertyID;
			this.dwOptions = (required ? 0 : 1);
			this.vValue = value;
		}

		// Token: 0x040014DD RID: 5341
		internal int dwPropertyID;

		// Token: 0x040014DE RID: 5342
		internal int dwOptions;

		// Token: 0x040014DF RID: 5343
		internal OleDbPropertyStatus dwStatus;

		// Token: 0x040014E0 RID: 5344
		internal tagDBIDX columnid;

		// Token: 0x040014E1 RID: 5345
		[MarshalAs(UnmanagedType.Struct)]
		internal object vValue;
	}
}
