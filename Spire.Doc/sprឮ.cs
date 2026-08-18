using System;
using System.Runtime.InteropServices;

// Token: 0x020003DA RID: 986
internal sealed class sprឮ
{
	// Token: 0x0600376F RID: 14191 RVA: 0x0033C948 File Offset: 0x0033B948
	private sprឮ()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003770 RID: 14192
	[DllImport("gdi32.dll")]
	internal static extern IntPtr SelectObject(IntPtr A_0, IntPtr A_1);

	// Token: 0x06003771 RID: 14193
	[DllImport("gdi32.dll")]
	internal static extern int DeleteObject(IntPtr A_0);

	// Token: 0x06003772 RID: 14194
	[DllImport("gdi32.dll")]
	internal static extern uint GetFontData(IntPtr A_0, uint A_1, uint A_2, [In] [Out] byte[] A_3, uint A_4);

	// Token: 0x06003773 RID: 14195
	[DllImport("gdi32.dll")]
	internal static extern IntPtr CreateDC(string A_0, string A_1, string A_2, IntPtr A_3);

	// Token: 0x06003774 RID: 14196
	[DllImport("gdi32.dll")]
	internal static extern bool DeleteDC(IntPtr A_0);
}
