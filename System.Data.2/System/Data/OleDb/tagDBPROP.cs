using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026D RID: 621
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROP
	{
		// Token: 0x0600266E RID: 9838 RVA: 0x001048DC File Offset: 0x00103CDC
		internal tagDBPROP()
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x001048F0 File Offset: 0x00103CF0
		internal tagDBPROP(int propertyID, bool required, object value)
		{
			this.dwPropertyID = propertyID;
			this.dwOptions = (required ? 0 : 1);
			this.vValue = value;
		}

		// Token: 0x040017EF RID: 6127
		internal int dwPropertyID;

		// Token: 0x040017F0 RID: 6128
		internal int dwOptions;

		// Token: 0x040017F1 RID: 6129
		internal OleDbPropertyStatus dwStatus;

		// Token: 0x040017F2 RID: 6130
		internal tagDBIDX columnid;

		// Token: 0x040017F3 RID: 6131
		[MarshalAs(UnmanagedType.Struct)]
		internal object vValue;
	}
}
