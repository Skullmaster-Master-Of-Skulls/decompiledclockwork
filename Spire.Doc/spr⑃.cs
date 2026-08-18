using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc.Native;

// Token: 0x02000339 RID: 825
[CLSCompliant(false)]
internal sealed class spr\u2443
{
	// Token: 0x06002C1D RID: 11293 RVA: 0x002ABC90 File Offset: 0x002AAC90
	private spr\u2443()
	{
	}

	// Token: 0x06002C1E RID: 11294
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string A_0, IntPtr A_1, STGM A_2, IntPtr A_3, uint A_4, out spr\u1CE7 A_5);

	// Token: 0x06002C1F RID: 11295
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorageEx([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, STGFMT A_2, uint A_3, IntPtr A_4, IntPtr A_5, ref Guid A_6, out spr\u1CE7 A_7);

	// Token: 0x06002C20 RID: 11296
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, uint A_2, out spr\u1CE7 A_3);

	// Token: 0x06002C21 RID: 11297
	[DllImport("ole32.dll", EntryPoint = "StgCreatePropSetStg", SetLastError = true)]
	private static extern int ᜃ(spr\u1CE7 A_0, uint A_1, out sprᵷ A_2);

	// Token: 0x06002C22 RID: 11298
	[DllImport("iprop.dll", EntryPoint = "StgCreatePropSetStg", SetLastError = true)]
	private static extern int ᜂ(spr\u1CE7 A_0, uint A_1, out sprᵷ A_2);

	// Token: 0x06002C23 RID: 11299 RVA: 0x002ABCA4 File Offset: 0x002AACA4
	public static int ᜁ(spr\u1CE7 A_0, uint A_1, out sprᵷ A_2)
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
			if (IntPtr.Size == 8)
			{
				return spr\u2443.ᜃ(A_0, A_1, out A_2);
			}
			break;
		}
		return spr\u2443.ᜂ(A_0, A_1, out A_2);
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x002ABCFC File Offset: 0x002AACFC
	public static int ᜀ(spr\u1CE7 A_0, uint A_1, out sprᵷ A_2)
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
			if (IntPtr.Size == 8)
			{
				return spr\u2443.ᜃ(A_0, A_1, out A_2);
			}
			break;
		}
		return spr\u2443.ᜂ(A_0, A_1, out A_2);
	}

	// Token: 0x06002C25 RID: 11301
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int CreateILockBytesOnHGlobal(IntPtr A_0, bool A_1, out sprḂ A_2);

	// Token: 0x06002C26 RID: 11302
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgCreateDocfileOnILockBytes(sprḂ A_0, STGM A_1, int A_2, out spr\u1CE7 A_3);

	// Token: 0x06002C27 RID: 11303
	[DllImport("ole32.dll", SetLastError = true)]
	public static extern int StgOpenStorageOnILockBytes(sprḂ A_0, spr\u1CE7 A_1, STGM A_2, int A_3, int A_4, out spr\u1CE7 A_5);

	// Token: 0x06002C28 RID: 11304
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalAlloc(spr\u2443.GlobalAllocFlags A_0, int A_1);

	// Token: 0x06002C29 RID: 11305
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalReAlloc(IntPtr A_0, int A_1, spr\u2443.GlobalAllocFlags A_2);

	// Token: 0x06002C2A RID: 11306
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GlobalFree(IntPtr A_0);

	// Token: 0x06002C2B RID: 11307
	[DllImport("ole32.dll")]
	internal static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string A_0, STGM A_1, STGFMT A_2, int A_3, IntPtr A_4, IntPtr A_5, [In] ref Guid A_6, out spr\u1CE7 A_7);

	// Token: 0x0200033A RID: 826
	[Flags]
	internal enum GlobalAllocFlags
	{
		// Token: 0x0400261C RID: 9756
		GMEM_FIXED = 0,
		// Token: 0x0400261D RID: 9757
		GMEM_MOVEABLE = 2,
		// Token: 0x0400261E RID: 9758
		GMEM_ZEROINIT = 64,
		// Token: 0x0400261F RID: 9759
		GMEM_NODISCARD = 32
	}
}
