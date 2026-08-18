using System;

// Token: 0x0200003D RID: 61
internal class sprᨫ : spr\u2320
{
	// Token: 0x060001F8 RID: 504 RVA: 0x00012844 File Offset: 0x00011844
	public unsafe sprᨫ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
		if (A_3 != null)
		{
			if (A_3.Length != 0)
			{
				fixed (byte* ptr = &A_3[0])
				{
					goto IL_29;
				}
			}
		}
		byte* ptr = null;
		IL_29:
		((sprᲂ*)ptr)->ᜁ = 1025;
		ptr = null;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0001288C File Offset: 0x0001188C
	public unsafe ushort ᜀ()
	{
		ushort ᜀ;
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_5A;
				case 3:
					if (array.Length == 0)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					fixed (byte* ptr = &array[0])
					{
						num = 6;
						continue;
						break;
					}
				case 4:
					return ᜀ;
				case 5:
					goto IL_AD;
				case 6:
					goto IL_AD;
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 1;
					continue;
				}
				IL_5A:
				byte* ptr = null;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_AD:
				ᜀ = ((sprᲂ*)ptr)->ᜀ;
				num = 4;
			}
		}
		return ᜀ;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0001295C File Offset: 0x0001195C
	public unsafe void ᜀ(ushort A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8C:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 5;
			break;
		}
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_72;
			case 1:
				num = 3;
				continue;
			case 2:
				goto IL_7D;
			case 3:
				if (array.Length == 0)
				{
					goto IL_8C;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 4;
					continue;
					break;
				}
			case 4:
				goto IL_70;
			}
			if (true)
			{
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_72:
			ptr = null;
			num = 2;
		}
		IL_70:
		IL_7D:
		((sprᲂ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
