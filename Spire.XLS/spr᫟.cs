using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Spire.CompoundFile.XLS.Native;

// Token: 0x02000289 RID: 649
[Guid("0000000b-0000-0000-C000-000000000046")]
[CLSCompliant(false)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface spr\u1ADF
{
	// Token: 0x060026A9 RID: 9897
	int ᜀ([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, uint A_2, uint A_3, ref sprᮯ A_4);

	// Token: 0x060026AA RID: 9898
	int ᜀ([MarshalAs(UnmanagedType.LPWStr)] string A_0, uint A_1, STGM A_2, uint A_3, out sprᮯ A_4);

	// Token: 0x060026AB RID: 9899
	int ᜀ([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, uint A_2, uint A_3, out spr\u1ADF A_4);

	// Token: 0x060026AC RID: 9900
	int ᜀ([MarshalAs(UnmanagedType.LPWStr)] string A_0, IntPtr A_1, STGM A_2, IntPtr A_3, uint A_4, out spr\u1ADF A_5);

	// Token: 0x060026AD RID: 9901
	int ᜀ(uint A_0, IntPtr A_1, IntPtr A_2, spr\u1ADF A_3);

	// Token: 0x060026AE RID: 9902
	int ᜀ(string A_0, spr\u1ADF A_1, string A_2, uint A_3);

	// Token: 0x060026AF RID: 9903
	int ᜀ(uint A_0);

	// Token: 0x060026B0 RID: 9904
	int ᜀ();

	// Token: 0x060026B1 RID: 9905
	int ᜀ(uint A_0, IntPtr A_1, uint A_2, ref spr᠒ A_3);

	// Token: 0x060026B2 RID: 9906
	int ᜀ(string A_0);

	// Token: 0x060026B3 RID: 9907
	int ᜀ(string A_0, string A_1);

	// Token: 0x060026B4 RID: 9908
	int ᜀ(string A_0, ref System.Runtime.InteropServices.ComTypes.FILETIME A_1, ref System.Runtime.InteropServices.ComTypes.FILETIME A_2, ref System.Runtime.InteropServices.ComTypes.FILETIME A_3);

	// Token: 0x060026B5 RID: 9909
	int ᜀ(ref Guid A_0);

	// Token: 0x060026B6 RID: 9910
	int ᜀ(uint A_0, uint A_1);

	// Token: 0x060026B7 RID: 9911
	int ᜀ(ref spr\u20AB A_0, uint A_1);
}
