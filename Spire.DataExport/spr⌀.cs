using System;

// Token: 0x02000078 RID: 120
internal class spr\u2300 : spr\u2320
{
	// Token: 0x060003B7 RID: 951 RVA: 0x00022EBC File Offset: 0x00021EBC
	public spr\u2300(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00022ED4 File Offset: 0x00021ED4
	public unsafe ushort ᜀ()
	{
		ushort ᜀ;
		for (;;)
		{
			IL_00:
			int num = 4;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_A7;
				case 1:
					return ᜀ;
				case 2:
					if (array.Length != 0)
					{
						if (true)
						{
						}
						fixed (byte* ptr = &array[0])
						{
							num = 0;
							continue;
						}
					}
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
					break;
				case 3:
					goto IL_A7;
				case 5:
					goto IL_64;
				case 6:
					num = 2;
					continue;
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 6;
					continue;
				}
				IL_64:
				byte* ptr = null;
				num = 3;
				continue;
				IL_A7:
				ᜀ = ((sprḲ*)ptr)->ᜀ;
				num = 1;
			}
		}
		return ᜀ;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00022FA0 File Offset: 0x00021FA0
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
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				if (array.Length == 0)
				{
					num = 2;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
					break;
				}
			case 2:
				goto IL_56;
			case 3:
				goto IL_84;
			case 4:
				IL_08:
				break;
			case 5:
				goto IL_84;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			goto IL_56;
			IL_84:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_9A;
			}
			IL_56:
			ptr = null;
			num = 5;
		}
		IL_9A:
		if (false)
		{
		}
		((sprḲ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
