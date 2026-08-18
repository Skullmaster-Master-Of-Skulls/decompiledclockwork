using System;

// Token: 0x0200005D RID: 93
internal class sprẇ : spr\u2320
{
	// Token: 0x0600030B RID: 779 RVA: 0x0001D0C8 File Offset: 0x0001C0C8
	public sprẇ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0001D0E0 File Offset: 0x0001C0E0
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
				goto IL_A4;
			case 1:
				goto IL_A4;
			case 2:
				if (array.Length == 0)
				{
					num = 5;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 0;
					continue;
					break;
				}
			case 3:
				return ᜀ;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				break;
			case 5:
				goto IL_80;
			case 6:
				num = 2;
				continue;
			}
			if (true)
			{
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 6;
				continue;
			}
			IL_80:
			byte* ptr = null;
			num = 1;
			continue;
			IL_A4:
			ᜀ = ((spr\u1AD8*)ptr)->ᜀ;
			num = 3;
		}
		return ᜀ;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0001D1A8 File Offset: 0x0001C1A8
	public unsafe void ᜁ(ushort A_0)
	{
		byte* ptr;
		for (;;)
		{
			IL_00:
			int num = 5;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_7C;
				case 2:
					goto IL_87;
				case 3:
					goto IL_7A;
				case 4:
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
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (true)
				{
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 0;
					continue;
				}
				IL_7C:
				ptr = null;
				num = 2;
			}
		}
		IL_7A:
		IL_87:
		((spr\u1AD8*)ptr)->ᜀ = A_0;
		ptr = null;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0001D260 File Offset: 0x0001C260
	public unsafe ushort ᜀ()
	{
		int num = 6;
		ushort ᜁ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				if (array.Length == 0)
				{
					num = 2;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			case 1:
				goto IL_A4;
			case 2:
				if (true)
				{
				}
				goto IL_78;
			case 3:
				goto IL_A4;
			case 4:
				return ᜁ;
			case 5:
				num = 0;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				break;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_78:
			byte* ptr = null;
			num = 3;
			continue;
			IL_A4:
			ᜁ = ((spr\u1AD8*)ptr)->ᜁ;
			num = 4;
		}
		return ᜁ;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0001D328 File Offset: 0x0001C328
	public unsafe void ᜀ(ushort A_0)
	{
		byte* ptr;
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_74;
				case 1:
					num = 4;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_7F;
				case 4:
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
						num = 5;
						continue;
						break;
					}
				case 5:
					goto IL_72;
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 1;
					continue;
				}
				IL_74:
				ptr = null;
				num = 3;
			}
		}
		IL_72:
		IL_7F:
		((spr\u1AD8*)ptr)->ᜁ = A_0;
		ptr = null;
	}
}
