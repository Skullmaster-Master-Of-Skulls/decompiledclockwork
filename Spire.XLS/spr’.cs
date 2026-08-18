using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.XLS.Native;

// Token: 0x0200028F RID: 655
[CLSCompliant(false)]
internal sealed class spr\u2019
{
	// Token: 0x060026D0 RID: 9936 RVA: 0x00161348 File Offset: 0x00160348
	private spr\u2019()
	{
	}

	// Token: 0x060026D1 RID: 9937
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string A_0, IntPtr A_1, STGM A_2, IntPtr A_3, uint A_4, out spr\u1ADF A_5);

	// Token: 0x060026D2 RID: 9938
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorageEx([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, STGFMT A_2, uint A_3, IntPtr A_4, IntPtr A_5, ref Guid A_6, out spr\u1ADF A_7);

	// Token: 0x060026D3 RID: 9939
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, uint A_2, out spr\u1ADF A_3);

	// Token: 0x060026D4 RID: 9940
	[DllImport("ole32.dll", EntryPoint = "StgCreatePropSetStg", SetLastError = true)]
	private static extern int ᜃ(spr\u1ADF A_0, uint A_1, out sprᮓ A_2);

	// Token: 0x060026D5 RID: 9941
	[DllImport("iprop.dll", EntryPoint = "StgCreatePropSetStg", SetLastError = true)]
	private static extern int ᜂ(spr\u1ADF A_0, uint A_1, out sprᮓ A_2);

	// Token: 0x060026D6 RID: 9942 RVA: 0x0016135C File Offset: 0x0016035C
	public static int ᜁ(spr\u1ADF A_0, uint A_1, out sprᮓ A_2)
	{
		if (IntPtr.Size == 8)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return spr\u2019.ᜃ(A_0, A_1, out A_2);
			}
		}
		return spr\u2019.ᜂ(A_0, A_1, out A_2);
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x001613B4 File Offset: 0x001603B4
	public static int ᜀ(spr\u1ADF A_0, uint A_1, out sprᮓ A_2)
	{
		if (IntPtr.Size == 8)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return spr\u2019.ᜃ(A_0, A_1, out A_2);
			}
		}
		return spr\u2019.ᜂ(A_0, A_1, out A_2);
	}

	// Token: 0x060026D8 RID: 9944
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int CreateILockBytesOnHGlobal(IntPtr A_0, bool A_1, out sprᥖ A_2);

	// Token: 0x060026D9 RID: 9945
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgCreateDocfileOnILockBytes(sprᥖ A_0, STGM A_1, int A_2, out spr\u1ADF A_3);

	// Token: 0x060026DA RID: 9946
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorageOnILockBytes(sprᥖ A_0, spr\u1ADF A_1, STGM A_2, int A_3, int A_4, out spr\u1ADF A_5);

	// Token: 0x060026DB RID: 9947
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalAlloc(spr\u2019.GlobalAllocFlags A_0, int A_1);

	// Token: 0x060026DC RID: 9948
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalReAlloc(IntPtr A_0, int A_1, spr\u2019.GlobalAllocFlags A_2);

	// Token: 0x060026DD RID: 9949
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalFree(IntPtr A_0);

	// Token: 0x060026DE RID: 9950
	[DllImport("ole32.dll")]
	internal static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, STGFMT A_2, int A_3, IntPtr A_4, IntPtr A_5, [In] ref Guid A_6, out spr\u1ADF A_7);

	// Token: 0x02000290 RID: 656
	[Flags]
	internal enum GlobalAllocFlags
	{
		// Token: 0x04001325 RID: 4901
		GMEM_FIXED = 0,
		// Token: 0x04001326 RID: 4902
		GMEM_MOVEABLE = 2,
		// Token: 0x04001327 RID: 4903
		GMEM_ZEROINIT = 64,
		// Token: 0x04001328 RID: 4904
		GMEM_NODISCARD = 32
	}
}
