using System;

// Token: 0x0200005B RID: 91
internal class sprᡫ : spr\u2320
{
	// Token: 0x060002F5 RID: 757 RVA: 0x0001C148 File Offset: 0x0001B148
	public sprᡫ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0001C160 File Offset: 0x0001B160
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
				goto IL_AD;
			case 1:
				goto IL_5A;
			case 2:
				goto IL_AD;
			case 3:
				return ᜀ;
			case 5:
				num = 6;
				continue;
			case 6:
				if (array.Length == 0)
				{
					num = 1;
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
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_5A:
			byte* ptr = null;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return ᜀ;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_AD:
			ᜀ = ((sprḣ*)ptr)->ᜀ;
			num = 3;
		}
		return ᜀ;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0001C230 File Offset: 0x0001B230
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 5;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_89;
			case 1:
				goto IL_7C;
			case 2:
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
			case 3:
				goto IL_7A;
			case 4:
				goto IL_87;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			IL_7C:
			ptr = null;
			num = 4;
			continue;
			IL_89:
			num = 2;
		}
		IL_7A:
		IL_87:
		((sprḣ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
