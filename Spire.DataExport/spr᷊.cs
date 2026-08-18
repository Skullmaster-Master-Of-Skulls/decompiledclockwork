using System;
using System.Runtime.InteropServices;

// Token: 0x020000AF RID: 175
internal struct spr\u1DCA
{
	// Token: 0x060004D0 RID: 1232
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004D1 RID: 1233 RVA: 0x0002F604 File Offset: 0x0002E604
	public unsafe static byte[] ᜀ(spr\u1DCA A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				array = new byte[spr\u1DCA.ᜀ()];
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_78;
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
							num = 3;
							continue;
							break;
						}
					}
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
							goto IL_50;
						}
						goto IL_78;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							goto IL_9A;
						}
						break;
					}
					break;
					IL_50:
					num = 0;
					continue;
					IL_78:
					ptr = null;
					num = 5;
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
			spr\u1DCA.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1DCA.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0002F6F4 File Offset: 0x0002E6F4
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
		return sizeof(spr\u1DCA);
	}

	// Token: 0x0400033D RID: 829
	public ushort ᜀ;

	// Token: 0x0400033E RID: 830
	public ushort ᜁ;

	// Token: 0x0400033F RID: 831
	public ushort ᜂ;

	// Token: 0x04000340 RID: 832
	public ushort ᜃ;

	// Token: 0x04000341 RID: 833
	public ushort ᜄ;

	// Token: 0x04000342 RID: 834
	public ushort ᜅ;

	// Token: 0x04000343 RID: 835
	public ushort ᜆ;

	// Token: 0x04000344 RID: 836
	public ushort ᜇ;

	// Token: 0x04000345 RID: 837
	public ushort ᜈ;
}
