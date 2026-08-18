using System;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x02000288 RID: 648
[Guid("0000000c-0000-0000-C000-000000000046")]
[CLSCompliant(false)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface sprᮯ
{
	// Token: 0x0600269E RID: 9886
	int ᜀ([MarshalAs(UnmanagedType.LPArray)] byte[] A_0, uint A_1, ref uint A_2);

	// Token: 0x0600269F RID: 9887
	int ᜁ([MarshalAs(UnmanagedType.LPArray)] byte[] A_0, uint A_1, ref uint A_2);

	// Token: 0x060026A0 RID: 9888
	int ᜀ(long A_0, SeekOrigin A_1, out long A_2);

	// Token: 0x060026A1 RID: 9889
	int ᜀ(ulong A_0);

	// Token: 0x060026A2 RID: 9890
	int ᜀ(sprᮯ A_0, ulong A_1, ref ulong A_2, ref ulong A_3);

	// Token: 0x060026A3 RID: 9891
	int ᜀ(uint A_0);

	// Token: 0x060026A4 RID: 9892
	int ᜀ();

	// Token: 0x060026A5 RID: 9893
	int ᜀ(ulong A_0, ulong A_1, uint A_2);

	// Token: 0x060026A6 RID: 9894
	int ᜁ(ulong A_0, ulong A_1, uint A_2);

	// Token: 0x060026A7 RID: 9895
	int ᜀ(ref spr\u20AB A_0, uint A_1);

	// Token: 0x060026A8 RID: 9896
	int ᜀ(ref sprᮯ A_0);
}
