using System;

// Token: 0x0200006F RID: 111
internal class spr\u1B64 : spr\u2244
{
	// Token: 0x0600038D RID: 909 RVA: 0x000214CC File Offset: 0x000204CC
	public spr\u1B64(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000214E4 File Offset: 0x000204E4
	public unsafe ushort ᜀ()
	{
		int num = 0;
		ushort ᜂ;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 5;
				continue;
			case 2:
				goto IL_A0;
			case 3:
				goto IL_76;
			case 4:
				return ᜂ;
			case 5:
			{
				byte[] array;
				if (array.Length == 0)
				{
					num = 3;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 6;
					continue;
					break;
				}
			}
			case 6:
				goto IL_A0;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return ᜂ;
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
					num = 1;
					continue;
				}
				break;
			}
			}
			IL_76:
			byte* ptr = null;
			num = 2;
			continue;
			IL_A0:
			ᜂ = ((spr\u25D4*)ptr)->ᜂ;
			num = 4;
		}
		return ᜂ;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000215B4 File Offset: 0x000205B4
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
				goto IL_5E;
			case 1:
				if (array.Length == 0)
				{
					num = 2;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 0;
					continue;
					break;
				}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					goto IL_60;
				}
				break;
			case 3:
				goto IL_6B;
			case 4:
				if (true)
				{
				}
				num = 1;
				continue;
			}
			IL_32:
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			goto IL_60;
			goto IL_32;
			IL_60:
			ptr = null;
			num = 3;
		}
		IL_5E:
		IL_6B:
		((spr\u25D4*)ptr)->ᜂ = A_0;
		ptr = null;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0002166C File Offset: 0x0002066C
	public unsafe ushort ᜁ()
	{
		int num = 5;
		ushort ᜃ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return ᜃ;
			case 1:
				num = 6;
				continue;
			case 2:
				if (true)
				{
				}
				goto IL_6E;
			case 3:
				goto IL_9D;
			case 4:
				goto IL_9D;
			case 6:
			{
				byte[] array;
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
			}
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return ᜃ;
			default:
			{
				if (false)
				{
				}
				byte[] array;
				if ((array = base.ᜢ()) != null)
				{
					num = 1;
					continue;
				}
				break;
			}
			}
			IL_6E:
			byte* ptr = null;
			num = 4;
			continue;
			IL_9D:
			ᜃ = ((spr\u25D4*)ptr)->ᜃ;
			num = 0;
		}
		return ᜃ;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00021738 File Offset: 0x00020738
	public unsafe void ᜁ(ushort A_0)
	{
		int num = 3;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				if (array.Length == 0)
				{
					num = 4;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 1;
					continue;
					break;
				}
			case 1:
				goto IL_56;
			case 2:
				goto IL_63;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_58;
				}
				break;
			case 5:
				num = 0;
				continue;
			}
			IL_32:
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			goto IL_58;
			goto IL_32;
			IL_58:
			ptr = null;
			num = 2;
		}
		IL_56:
		IL_63:
		((spr\u25D4*)ptr)->ᜃ = A_0;
		ptr = null;
	}
}
