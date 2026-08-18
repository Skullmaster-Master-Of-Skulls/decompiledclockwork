using System;
using System.Runtime.InteropServices;

// Token: 0x020000BF RID: 191
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct sprᰐ
{
	// Token: 0x060004D3 RID: 1235
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004D4 RID: 1236 RVA: 0x0002F738 File Offset: 0x0002E738
	public unsafe static byte[] ᜀ(sprᰐ A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				for (;;)
				{
					array = new byte[sprᰐ.ᜀ()];
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_78;
						case 1:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 0;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 3;
								continue;
								break;
							}
						}
						case 2:
							goto IL_84;
						case 3:
							goto IL_76;
						case 4:
						{
							if (true)
							{
							}
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 5;
								continue;
							}
							goto IL_78;
						}
						case 5:
							num = 1;
							continue;
						}
						break;
						IL_78:
						ptr = null;
						num = 2;
					}
				}
				IL_84:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9A;
				}
			}
			IL_76:
			goto IL_C1;
			IL_9A:
			if (false)
			{
			}
			IL_C1:
			void* value = (void*)(&A_0);
			sprᰐ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, sprᰐ.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0002F828 File Offset: 0x0002E828
	public static int ᜀ()
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
		return sizeof(sprᰐ);
	}

	// Token: 0x04000388 RID: 904
	public byte ᜀ;

	// Token: 0x04000389 RID: 905
	public sprι ᜁ;
}
