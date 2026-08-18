using System;

// Token: 0x0200003B RID: 59
internal class spr᠓ : spr\u2320
{
	// Token: 0x060001EE RID: 494 RVA: 0x000121FC File Offset: 0x000111FC
	public spr᠓(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00012214 File Offset: 0x00011214
	public unsafe ushort ᜀ()
	{
		int num = 4;
		ushort ᜀ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_AD;
			case 1:
				goto IL_AD;
			case 2:
				return ᜀ;
			case 3:
				goto IL_90;
			case 5:
				goto IL_80;
			case 6:
			{
				byte[] array;
				if (array.Length == 0)
				{
					num = 5;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			}
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_90:
				num = 6;
				continue;
			default:
			{
				if (false)
				{
				}
				if (true)
				{
				}
				byte[] array;
				if ((array = base.ᜢ()) != null)
				{
					num = 3;
					continue;
				}
				break;
			}
			}
			IL_80:
			byte* ptr = null;
			num = 0;
			continue;
			IL_AD:
			ᜀ = ((spr\u20A7*)ptr)->ᜀ;
			num = 2;
		}
		return ᜀ;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000122E4 File Offset: 0x000112E4
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
				goto IL_70;
			case 1:
				goto IL_72;
			case 3:
				goto IL_7D;
			case 4:
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 0;
					continue;
					break;
				}
			case 5:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_72:
			ptr = null;
			num = 3;
		}
		IL_70:
		IL_7D:
		((spr\u20A7*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
