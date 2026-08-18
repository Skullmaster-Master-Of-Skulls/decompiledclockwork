using System;

// Token: 0x02000111 RID: 273
internal class spr\u22E3 : spr\u2320
{
	// Token: 0x06000647 RID: 1607 RVA: 0x0003C5E8 File Offset: 0x0003B5E8
	public spr\u22E3(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0003C600 File Offset: 0x0003B600
	public unsafe ushort ᜀ()
	{
		int num = 5;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_9D;
			case 1:
				goto IL_9D;
			case 2:
				if (array.Length != 0)
				{
					if (true)
					{
					}
					fixed (byte* ptr = &array[0])
					{
						num = 1;
						continue;
					}
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 3:
				return ᜀ;
			case 4:
				num = 2;
				continue;
			case 6:
				goto IL_9B;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_5A:
			byte* ptr = null;
			num = 0;
			continue;
			IL_9B:
			goto IL_5A;
			IL_9D:
			ᜀ = ((spr\u23C5*)ptr)->ᜀ;
			num = 3;
		}
		return ᜀ;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0003C6CC File Offset: 0x0003B6CC
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 0;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				goto IL_60;
			case 2:
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 4;
					continue;
					break;
				}
			case 3:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_5E;
			case 5:
				goto IL_6B;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			IL_60:
			ptr = null;
			num = 5;
		}
		IL_5E:
		IL_6B:
		((spr\u23C5*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
