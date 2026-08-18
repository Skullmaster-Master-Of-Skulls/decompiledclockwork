using System;
using System.Runtime.InteropServices;

// Token: 0x020000AE RID: 174
[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal struct spr\u1CC5
{
	// Token: 0x060004CD RID: 1229
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004CE RID: 1230 RVA: 0x0002F4D4 File Offset: 0x0002E4D4
	public unsafe static byte[] ᜀ(spr\u1CC5 A_0)
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
					array = new byte[spr\u1CC5.ᜀ()];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_84;
						case 1:
						{
							if (true)
							{
							}
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 3;
								continue;
							}
							goto IL_78;
						}
						case 2:
							goto IL_76;
						case 3:
							num = 4;
							continue;
						case 4:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 5;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 2;
								continue;
								break;
							}
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_78;
							}
							break;
						}
						break;
						IL_78:
						ptr = null;
						num = 0;
					}
				}
			}
			IL_76:
			IL_84:
			void* value = (void*)(&A_0);
			spr\u1CC5.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1CC5.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0002F5C0 File Offset: 0x0002E5C0
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
		return sizeof(spr\u1CC5);
	}

	// Token: 0x0400033A RID: 826
	public ushort ᜀ;

	// Token: 0x0400033B RID: 827
	public ushort ᜁ;

	// Token: 0x0400033C RID: 828
	public uint ᜂ;
}
