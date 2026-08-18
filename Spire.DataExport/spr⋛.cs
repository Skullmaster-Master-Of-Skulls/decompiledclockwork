using System;

// Token: 0x02000079 RID: 121
internal class spr\u22DB : spr\u2320
{
	// Token: 0x060003BA RID: 954 RVA: 0x00023058 File Offset: 0x00022058
	public spr\u22DB(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00023070 File Offset: 0x00022070
	public unsafe ushort ᜀ()
	{
		int num = 1;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_64;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					if (false)
					{
					}
					goto IL_AA;
				}
				break;
			case 3:
				goto IL_AA;
			case 4:
				return ᜀ;
			case 5:
				num = 6;
				continue;
			case 6:
				if (array.Length == 0)
				{
					goto IL_9D;
				}
				if (true)
				{
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
					break;
				}
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_64:
			byte* ptr = null;
			num = 2;
			continue;
			IL_9D:
			num = 0;
			continue;
			IL_AA:
			ᜀ = ((sprᮝ*)ptr)->ᜀ;
			num = 4;
		}
		return ᜀ;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00023140 File Offset: 0x00022140
	public unsafe void ᜀ(ushort A_0)
	{
		byte* ptr;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					if (true)
					{
					}
					num = 5;
					continue;
				case 2:
					goto IL_87;
				case 4:
					goto IL_7C;
				case 5:
					if (array.Length == 0)
					{
						num = 4;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 0;
						continue;
						break;
					}
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 1;
					continue;
				}
				IL_7C:
				ptr = null;
				num = 2;
			}
			IL_54:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_6A;
			}
		}
		IL_6A:
		if (false)
		{
		}
		IL_87:
		((sprᮝ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
