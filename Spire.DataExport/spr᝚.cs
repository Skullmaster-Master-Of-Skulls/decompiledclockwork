using System;

// Token: 0x02000120 RID: 288
internal class spr\u175A : spr\u2320
{
	// Token: 0x060006B0 RID: 1712 RVA: 0x0003FDCC File Offset: 0x0003EDCC
	public spr\u175A(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0003FDE4 File Offset: 0x0003EDE4
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
				goto IL_AA;
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
						break;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
			case 2:
				return ᜀ;
			case 4:
				goto IL_76;
			case 5:
				num = 1;
				continue;
			case 6:
				goto IL_AA;
			}
			IL_2C:
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			goto IL_76;
			goto IL_2C;
			IL_76:
			byte* ptr = null;
			num = 0;
			continue;
			IL_AA:
			ᜀ = ((sprᠲ*)ptr)->ᜀ;
			num = 2;
		}
		return ᜀ;
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0003FEB4 File Offset: 0x0003EEB4
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 4;
		byte* ptr;
		for (;;)
		{
			IL_0A:
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_7D;
			case 1:
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				while (array.Length != 0)
				{
					fixed (byte* ptr = &array[0])
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 3;
							goto IL_0A;
						}
					}
				}
				num = 5;
				continue;
			case 3:
				goto IL_70;
			case 5:
				goto IL_72;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_72:
			ptr = null;
			num = 0;
		}
		IL_70:
		IL_7D:
		((sprᠲ*)ptr)->ᜀ = A_0;
		ptr = null;
	}
}
