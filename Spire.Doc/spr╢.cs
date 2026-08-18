using System;
using System.Runtime.InteropServices;

// Token: 0x02000178 RID: 376
[StructLayout(LayoutKind.Sequential)]
internal abstract class spr\u2562
{
	// Token: 0x06000D2A RID: 3370
	internal abstract void ᜁ(byte[] A_0, int A_1);

	// Token: 0x06000D2B RID: 3371
	internal abstract int ᜀ(byte[] A_0, int A_1);

	// Token: 0x06000D2C RID: 3372
	internal abstract int ᜀ();

	// Token: 0x06000D2D RID: 3373 RVA: 0x000DB754 File Offset: 0x000DA754
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
		A_1 += 2;
		return result;
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x000DB7A0 File Offset: 0x000DA7A0
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

	// Token: 0x06000D2F RID: 3375 RVA: 0x000DB7EC File Offset: 0x000DA7EC
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

	// Token: 0x06000D30 RID: 3376 RVA: 0x000DB838 File Offset: 0x000DA838
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
		A_1 += 2;
		return result;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000DB884 File Offset: 0x000DA884
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

	// Token: 0x06000D32 RID: 3378 RVA: 0x000DB8D0 File Offset: 0x000DA8D0
	internal static byte[] ᜀ(byte[] A_0, int A_1, ref int A_2)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[A_1];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_3D;
				case 1:
					if (num >= A_1)
					{
						num2 = 2;
						continue;
					}
					array[num] = A_0[A_2 + num];
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_6D;
				case 3:
					if (true)
					{
					}
					goto IL_3D;
				}
				break;
				IL_3D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
			}
		}
		IL_6D:
		A_2 += A_1;
		return array;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x000DB970 File Offset: 0x000DA970
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
		spr\u2562.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x000DB9BC File Offset: 0x000DA9BC
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
		spr\u2562.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x000DBA08 File Offset: 0x000DAA08
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
		spr\u2562.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x000DBA54 File Offset: 0x000DAA54
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
		spr\u2562.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x000DBAA0 File Offset: 0x000DAAA0
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
		spr\u2562.ᜀ(A_0, ref A_1, bytes);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000DBAEC File Offset: 0x000DAAEC
	internal static void ᜀ(byte[] A_0, ref int A_1, byte[] A_2)
	{
		int num;
		for (;;)
		{
			num = A_2.Length;
			int num2 = 0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 >= num)
					{
						num3 = 2;
						continue;
					}
					A_0[A_1 + num2] = A_2[num2];
					num2++;
					num3 = 3;
					continue;
				case 1:
					if (true)
					{
					}
					goto IL_3A;
				case 2:
					goto IL_6A;
				case 3:
					goto IL_3A;
				}
				break;
				IL_3A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num3 = 0;
					break;
				}
			}
		}
		IL_6A:
		A_1 += num;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x000DBB84 File Offset: 0x000DAB84
	internal static void ᜀ(byte[] A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			int num = 0;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_52;
					case 1:
						return;
					case 2:
						if (num >= A_2)
						{
							num2 = 1;
							continue;
						}
						A_0[num] = A_1[num];
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_52;
					}
					break;
					IL_52:
					num2 = 2;
				}
				break;
			}
			}
		}
	}
}
