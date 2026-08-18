using System;
using System.Runtime.InteropServices;

// Token: 0x020000E1 RID: 225
[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal struct spr\u1B3E
{
	// Token: 0x060004D6 RID: 1238
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004D7 RID: 1239 RVA: 0x0002F86C File Offset: 0x0002E86C
	public unsafe static byte[] ᜀ(spr\u1B3E A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_95;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					array = new byte[spr\u1B3E.ᜀ()];
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							goto IL_97;
						case 2:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 1;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 4;
								continue;
								break;
							}
						}
						case 3:
							goto IL_A6;
						case 4:
							goto IL_95;
						case 5:
						{
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 0;
								continue;
							}
							goto IL_97;
						}
						}
						break;
						IL_97:
						ptr = null;
						num = 3;
					}
					break;
				}
				}
			}
			IL_95:
			IL_A6:
			void* value = (void*)(&A_0);
			spr\u1B3E.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1B3E.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0002F960 File Offset: 0x0002E960
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
		return sizeof(spr\u1B3E);
	}

	// Token: 0x04000408 RID: 1032
	public ushort ᜀ;

	// Token: 0x04000409 RID: 1033
	public ushort ᜁ;

	// Token: 0x0400040A RID: 1034
	public ushort ᜂ;

	// Token: 0x0400040B RID: 1035
	public sprὴ ᜃ;
}
