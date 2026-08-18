using System;

// Token: 0x0200007A RID: 122
internal class sprὸ : spr\u2320
{
	// Token: 0x060003BD RID: 957 RVA: 0x00023200 File Offset: 0x00022200
	public sprὸ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00023218 File Offset: 0x00022218
	public unsafe ushort ᜀ()
	{
		int num = 6;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				return ᜀ;
			case 1:
				if (array.Length == 0)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				fixed (byte* ptr = &array[0])
				{
					num = 5;
					continue;
					break;
				}
			case 2:
				goto IL_AA;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 4:
				goto IL_64;
			case 5:
				goto IL_AA;
			}
			IL_2C:
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			goto IL_64;
			goto IL_2C;
			IL_64:
			byte* ptr = null;
			num = 2;
			continue;
			IL_AA:
			ᜀ = ((sprᦦ*)ptr)->ᜀ;
			num = 0;
		}
		return ᜀ;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000232E8 File Offset: 0x000222E8
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 2;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					if (array.Length == 0)
					{
						num = 5;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 0;
						continue;
						break;
					}
				}
				break;
			case 3:
				goto IL_6B;
			case 4:
				if (true)
				{
				}
				num = 1;
				continue;
			case 5:
				goto IL_60;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_60:
			ptr = null;
			num = 3;
		}
		IL_5E:
		IL_6B:
		((sprᦦ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
