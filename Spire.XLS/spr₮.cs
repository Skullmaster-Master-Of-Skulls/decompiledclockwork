using System;
using System.Runtime.InteropServices;

// Token: 0x020002DD RID: 733
[StructLayout(LayoutKind.Sequential)]
internal abstract class spr\u20AE
{
	// Token: 0x06002CEE RID: 11502
	internal abstract void ᜁ(byte[] A_0, int A_1);

	// Token: 0x06002CEF RID: 11503
	internal abstract int ᜀ(byte[] A_0, int A_1);

	// Token: 0x06002CF0 RID: 11504
	internal abstract int ᜀ();

	// Token: 0x06002CF1 RID: 11505 RVA: 0x00194778 File Offset: 0x00193778
	internal static short ᜄ(byte[] A_0, ref int A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		short result = BitConverter.ToInt16(A_0, A_1);
		A_1 += 16;
		return result;
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x001947C4 File Offset: 0x001937C4
	internal static int ᜃ(byte[] A_0, ref int A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int result = BitConverter.ToInt32(A_0, A_1);
		A_1 += 4;
		return result;
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x00194810 File Offset: 0x00193810
	internal static long ᜂ(byte[] A_0, ref int A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		long result = BitConverter.ToInt64(A_0, A_1);
		A_1 += 8;
		return result;
	}

	// Token: 0x06002CF4 RID: 11508 RVA: 0x0019485C File Offset: 0x0019385C
	internal static ushort ᜁ(byte[] A_0, ref int A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		ushort result = BitConverter.ToUInt16(A_0, A_1);
		A_1 += 16;
		return result;
	}

	// Token: 0x06002CF5 RID: 11509 RVA: 0x001948A8 File Offset: 0x001938A8
	internal static uint ᜀ(byte[] A_0, ref int A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		uint result = BitConverter.ToUInt32(A_0, A_1);
		A_1 += 4;
		return result;
	}

	// Token: 0x06002CF6 RID: 11510 RVA: 0x001948F4 File Offset: 0x001938F4
	internal static byte[] ᜀ(byte[] A_0, int A_1, ref int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] array = new byte[A_1];
		Buffer.BlockCopy(A_0, A_2, array, 0, A_1);
		A_2 += A_1;
		return array;
	}

	// Token: 0x06002CF7 RID: 11511 RVA: 0x00194948 File Offset: 0x00193948
	internal static void ᜀ(byte[] A_0, ref int A_1, short A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_2);
		spr\u20AE.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x00194994 File Offset: 0x00193994
	internal static void ᜀ(byte[] A_0, ref int A_1, ushort A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_2);
		spr\u20AE.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x001949E0 File Offset: 0x001939E0
	internal static void ᜀ(byte[] A_0, ref int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_2);
		spr\u20AE.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x00194A2C File Offset: 0x00193A2C
	internal static void ᜀ(byte[] A_0, ref int A_1, long A_2)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_2);
		spr\u20AE.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x00194A78 File Offset: 0x00193A78
	internal static void ᜀ(byte[] A_0, ref int A_1, uint A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_2);
		spr\u20AE.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x00194AC4 File Offset: 0x00193AC4
	internal static void ᜀ(byte[] A_0, ref int A_1, byte[] A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = A_2.Length;
		Buffer.BlockCopy(A_2, 0, A_0, A_1, num);
		A_1 += num;
	}

	// Token: 0x040014B5 RID: 5301
	public const int ᜀ = 4;

	// Token: 0x040014B6 RID: 5302
	public const int ᜁ = 512;
}
