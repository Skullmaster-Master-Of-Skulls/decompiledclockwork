using System;

// Token: 0x02000121 RID: 289
internal class sprញ : spr\u2320
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x0003FF74 File Offset: 0x0003EF74
	public sprញ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0003FF8C File Offset: 0x0003EF8C
	public unsafe ushort ᜁ()
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
				goto IL_80;
			case 2:
				num = 3;
				continue;
			case 3:
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return ᜀ;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
			case 5:
				goto IL_AD;
			case 6:
				return ᜀ;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 2;
				continue;
			}
			IL_80:
			byte* ptr = null;
			num = 0;
			continue;
			IL_AD:
			ᜀ = ((sprἢ*)ptr)->ᜀ;
			num = 6;
		}
		return ᜀ;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0004005C File Offset: 0x0003F05C
	public unsafe void ᜁ(ushort A_0)
	{
		int num = 5;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
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
			case 1:
				goto IL_6B;
			case 2:
				goto IL_60;
			case 3:
				goto IL_3A;
			case 4:
				goto IL_5E;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			goto IL_60;
			IL_3A:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_60:
			ptr = null;
			num = 1;
		}
		IL_5E:
		IL_6B:
		((sprἢ*)ptr)->ᜀ = A_0;
		ptr = null;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00040118 File Offset: 0x0003F118
	public unsafe ushort ᜀ()
	{
		int num = 5;
		ushort ᜁ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_5C;
			case 1:
				goto IL_AA;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return ᜁ;
				default:
					if (false)
					{
					}
					goto IL_AA;
				}
				break;
			case 3:
				num = 4;
				continue;
			case 4:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			case 6:
				return ᜁ;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			IL_5C:
			byte* ptr = null;
			num = 2;
			continue;
			IL_AA:
			ᜁ = ((sprἢ*)ptr)->ᜁ;
			num = 6;
		}
		return ᜁ;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x000401E8 File Offset: 0x0003F1E8
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
				goto IL_A1;
			case 1:
				goto IL_56;
			case 3:
				num = 5;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A1;
				default:
					goto IL_79;
				}
				break;
			case 5:
				if (true)
				{
				}
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			IL_58:
			ptr = null;
			num = 4;
			continue;
			IL_A1:
			goto IL_58;
		}
		IL_56:
		goto IL_A3;
		IL_79:
		if (false)
		{
		}
		IL_A3:
		((sprἢ*)ptr)->ᜁ = A_0;
		ptr = null;
	}
}
