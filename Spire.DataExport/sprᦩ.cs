using System;

// Token: 0x0200004E RID: 78
internal class sprᦩ : spr\u2320
{
	// Token: 0x0600027C RID: 636 RVA: 0x00016F84 File Offset: 0x00015F84
	public sprᦩ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00016F9C File Offset: 0x00015F9C
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
				num = 1;
				continue;
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
					break;
				}
			case 2:
				goto IL_AD;
			case 4:
				goto IL_80;
			case 5:
				return ᜀ;
			case 6:
				goto IL_AD;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			IL_80:
			byte* ptr = null;
			num = 6;
			continue;
			IL_AD:
			ᜀ = ((spr\u1D40*)ptr)->ᜀ;
			num = 5;
		}
		return ᜀ;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0001706C File Offset: 0x0001606C
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
				goto IL_60;
			case 1:
				goto IL_87;
			case 2:
				goto IL_5E;
			case 3:
				if (true)
				{
				}
				num = 5;
				continue;
			case 5:
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
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			IL_60:
			ptr = null;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5E;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
		}
		IL_5E:
		IL_87:
		((spr\u1D40*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
