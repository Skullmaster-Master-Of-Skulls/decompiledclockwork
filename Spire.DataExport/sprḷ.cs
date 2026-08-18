using System;

// Token: 0x02000112 RID: 274
internal class sprḷ : spr\u2320
{
	// Token: 0x0600064A RID: 1610 RVA: 0x0003C78C File Offset: 0x0003B78C
	public sprḷ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0003C7A4 File Offset: 0x0003B7A4
	public unsafe ushort ᜀ()
	{
		int num = 3;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				return ᜀ;
			case 1:
				goto IL_A4;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					if (false)
					{
					}
					goto IL_64;
				}
				break;
			case 4:
				goto IL_48;
			case 5:
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
			case 6:
				goto IL_A4;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			goto IL_64;
			IL_48:
			num = 5;
			continue;
			IL_64:
			byte* ptr = null;
			num = 1;
			continue;
			IL_A4:
			ᜀ = ((spr\u22EF*)ptr)->ᜀ;
			num = 0;
		}
		return ᜀ;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0003C86C File Offset: 0x0003B86C
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
				num = 4;
				continue;
			case 1:
				goto IL_70;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_9E;
			case 4:
				if (array.Length == 0)
				{
					num = 3;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			case 5:
				goto IL_7D;
			}
			if (true)
			{
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			IL_72:
			ptr = null;
			num = 5;
			continue;
			IL_9E:
			goto IL_72;
		}
		IL_70:
		IL_7D:
		((spr\u22EF*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
