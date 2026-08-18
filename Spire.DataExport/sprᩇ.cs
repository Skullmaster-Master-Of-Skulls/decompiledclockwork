using System;

// Token: 0x0200004F RID: 79
internal class sprᩇ : spr\u2320
{
	// Token: 0x0600027F RID: 639 RVA: 0x00017128 File Offset: 0x00016128
	public sprᩇ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00017140 File Offset: 0x00016140
	public unsafe ushort ᜀ()
	{
		int num = 4;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_88;
			case 1:
				goto IL_88;
			case 2:
				if (array.Length == 0)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				fixed (byte* ptr = &array[0])
				{
					num = 0;
					continue;
					break;
				}
			case 3:
				IL_86:
				goto IL_64;
			case 5:
				return ᜀ;
			case 6:
				num = 2;
				continue;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 6;
				continue;
			}
			IL_64:
			byte* ptr = null;
			num = 1;
			continue;
			IL_88:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_86;
			default:
				if (false)
				{
				}
				ᜀ = ((spr\u204F*)ptr)->ᜀ;
				num = 5;
				break;
			}
		}
		return ᜀ;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00017208 File Offset: 0x00016208
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 0;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				goto IL_61;
			case 2:
				goto IL_56;
			case 3:
				if (true)
				{
				}
				num = 5;
				continue;
			case 4:
				goto IL_54;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					if (false)
					{
					}
					if (array.Length == 0)
					{
						num = 2;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 4;
						continue;
						break;
					}
				}
				break;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			IL_56:
			ptr = null;
			num = 1;
		}
		IL_54:
		IL_61:
		((spr\u204F*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
