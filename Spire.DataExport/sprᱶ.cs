using System;

// Token: 0x0200008D RID: 141
internal class sprᱶ : spr\u2320
{
	// Token: 0x0600044C RID: 1100 RVA: 0x00029CA8 File Offset: 0x00028CA8
	public sprᱶ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00029CC0 File Offset: 0x00028CC0
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
				num = 4;
				continue;
			case 3:
				goto IL_88;
			case 4:
				if (array.Length == 0)
				{
					num = 0;
					continue;
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
			case 5:
				IL_6F:
				goto IL_88;
			case 6:
				return ᜀ;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 2;
				continue;
			}
			IL_64:
			byte* ptr = null;
			num = 5;
			continue;
			IL_88:
			ᜀ = ((spr\u25DE*)ptr)->ᜀ;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6F;
			default:
				if (false)
				{
				}
				num = 6;
				break;
			}
		}
		return ᜀ;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00029D88 File Offset: 0x00028D88
	public unsafe void ᜁ(ushort A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_87;
				default:
					goto IL_77;
				}
				break;
			case 2:
				goto IL_87;
			case 4:
				goto IL_54;
			case 5:
				if (true)
				{
				}
				num = 2;
				continue;
			}
			byte[] array;
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			goto IL_56;
			IL_87:
			if (array.Length == 0)
			{
				num = 0;
				continue;
			}
			fixed (byte* ptr = &array[0])
			{
				num = 4;
				continue;
				IL_56:;
			}
			num = 1;
		}
		IL_54:
		goto IL_A3;
		IL_77:
		if (false)
		{
		}
		IL_A3:
		byte* ptr;
		((spr\u25DE*)ptr)->ᜀ = A_0;
		ptr = null;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00029E44 File Offset: 0x00028E44
	public unsafe ushort ᜁ()
	{
		int num = 2;
		ushort ᜁ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				return ᜁ;
			case 1:
				num = 4;
				continue;
			case 3:
				goto IL_76;
			case 4:
				IL_67:
				if (array.Length == 0)
				{
					num = 5;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
					break;
				}
			case 5:
				goto IL_52;
			case 6:
				goto IL_76;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			goto IL_52;
			IL_76:
			byte* ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_67;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				ᜁ = ((spr\u25DE*)ptr)->ᜁ;
				num = 0;
				continue;
			}
			IL_52:
			ptr = null;
			num = 6;
		}
		return ᜁ;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00029F0C File Offset: 0x00028F0C
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 2:
				goto IL_4E;
			case 3:
				goto IL_4C;
			case 4:
				goto IL_7F;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7F;
				default:
					goto IL_6F;
				}
				break;
			}
			byte[] array;
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			goto IL_4E;
			IL_7F:
			if (true)
			{
			}
			if (array.Length == 0)
			{
				num = 2;
				continue;
			}
			fixed (byte* ptr = &array[0])
			{
				num = 3;
				continue;
				IL_4E:;
			}
			num = 5;
		}
		IL_4C:
		goto IL_A3;
		IL_6F:
		if (false)
		{
		}
		IL_A3:
		byte* ptr;
		((spr\u25DE*)ptr)->ᜁ = A_0;
		ptr = null;
	}
}
