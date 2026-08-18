using System;

// Token: 0x0200004D RID: 77
internal class spr\u236E : spr\u2320
{
	// Token: 0x06000279 RID: 633 RVA: 0x00016DDC File Offset: 0x00015DDC
	public spr\u236E(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00016DF4 File Offset: 0x00015DF4
	public unsafe ushort ᜀ()
	{
		int num = 1;
		ushort ᜀ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return ᜀ;
			case 2:
				num = 3;
				continue;
			case 3:
			{
				byte[] array;
				if (array.Length == 0)
				{
					num = 6;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 5;
					continue;
					break;
				}
			}
			case 4:
				goto IL_8B;
			case 5:
				goto IL_AA;
			case 6:
				goto IL_80;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				goto IL_AA;
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
					num = 2;
					continue;
				}
				break;
			}
			}
			IL_80:
			byte* ptr = null;
			num = 4;
			continue;
			IL_AA:
			ᜀ = ((spr\u2254*)ptr)->ᜀ;
			num = 0;
		}
		return ᜀ;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00016EC4 File Offset: 0x00015EC4
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 3;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 2:
				goto IL_87;
			case 4:
				goto IL_7A;
			case 5:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7A;
				default:
					if (false)
					{
					}
					fixed (byte* ptr = &array[0])
					{
						num = 4;
						continue;
					}
				}
				break;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_7C:
			ptr = null;
			num = 2;
		}
		IL_7A:
		IL_87:
		((spr\u2254*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
