using System;

// Token: 0x02000099 RID: 153
internal class sprᣦ : spr\u2320
{
	// Token: 0x060004AE RID: 1198 RVA: 0x0002E210 File Offset: 0x0002D210
	public sprᣦ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0002E228 File Offset: 0x0002D228
	public unsafe ushort ᜁ()
	{
		int num = 3;
		ushort ᜁ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_80;
			case 1:
				num = 6;
				continue;
			case 2:
				return ᜁ;
			case 4:
				goto IL_AD;
			case 5:
				goto IL_AD;
			case 6:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					fixed (byte* ptr = &array[0])
					{
						num = 5;
						continue;
					}
				}
				break;
			}
			IL_36:
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			goto IL_80;
			goto IL_36;
			IL_80:
			byte* ptr = null;
			num = 4;
			continue;
			IL_AD:
			ᜁ = ((spr\u1DF3*)ptr)->ᜁ;
			num = 2;
		}
		return ᜁ;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0002E2F8 File Offset: 0x0002D2F8
	public unsafe void ᜁ(ushort A_0)
	{
		int num = 2;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_7D;
			case 1:
				if (array.Length == 0)
				{
					num = 4;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
					break;
				}
			case 3:
				goto IL_54;
			case 4:
				goto IL_72;
			case 5:
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_72:
			ptr = null;
			num = 0;
		}
		IL_54:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7D:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		((spr\u1DF3*)ptr)->ᜁ = A_0;
		ptr = null;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0002E3B8 File Offset: 0x0002D3B8
	public unsafe ushort ᜀ()
	{
		int num = 0;
		ushort ᜂ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				return ᜂ;
			case 2:
				if (array.Length == 0)
				{
					num = 6;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					fixed (byte* ptr = &array[0])
					{
						num = 3;
						continue;
					}
				}
				break;
			case 3:
				goto IL_AA;
			case 4:
				goto IL_AA;
			case 5:
				num = 2;
				continue;
			case 6:
				if (true)
				{
				}
				goto IL_78;
			}
			IL_36:
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			goto IL_78;
			goto IL_36;
			IL_78:
			byte* ptr = null;
			num = 4;
			continue;
			IL_AA:
			ᜂ = ((spr\u1DF3*)ptr)->ᜂ;
			num = 1;
		}
		return ᜂ;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0002E488 File Offset: 0x0002D488
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
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
					break;
				}
			case 1:
				goto IL_6A;
			case 2:
				goto IL_75;
			case 3:
				goto IL_4C;
			case 4:
				num = 0;
				continue;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_6A:
			ptr = null;
			num = 2;
		}
		IL_4C:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_75:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		((spr\u1DF3*)ptr)->ᜂ = A_0;
		ptr = null;
	}
}
