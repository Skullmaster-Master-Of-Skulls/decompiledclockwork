using System;

// Token: 0x02000105 RID: 261
internal class sprᥫ : spr\u2320
{
	// Token: 0x060005A9 RID: 1449 RVA: 0x00036B7C File Offset: 0x00035B7C
	public sprᥫ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00036B94 File Offset: 0x00035B94
	public unsafe ushort ᜀ()
	{
		int num = 2;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_5A;
			case 1:
				goto IL_7E;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					goto IL_A9;
				}
				break;
			case 4:
				num = 5;
				continue;
			case 5:
				if (array.Length == 0)
				{
					goto IL_74;
				}
				if (true)
				{
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			case 6:
				goto IL_7E;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_5A:
			byte* ptr = null;
			num = 6;
			continue;
			IL_74:
			num = 0;
			continue;
			IL_7E:
			ᜀ = ((spr\u2376*)ptr)->ᜀ;
			num = 3;
		}
		IL_A9:
		if (false)
		{
		}
		return ᜀ;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00036C5C File Offset: 0x00035C5C
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 4;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 2;
					continue;
					break;
				}
			case 2:
				goto IL_54;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					goto IL_81;
				}
				break;
			case 5:
				goto IL_3A;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			goto IL_56;
			IL_3A:
			if (true)
			{
			}
			num = 1;
			continue;
			IL_56:
			ptr = null;
			num = 3;
		}
		IL_54:
		goto IL_A6;
		IL_81:
		if (false)
		{
		}
		IL_A6:
		((spr\u2376*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
