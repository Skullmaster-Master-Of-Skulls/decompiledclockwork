using System;

// Token: 0x02000145 RID: 325
internal class spr\u24DF : spr\u2320
{
	// Token: 0x060007E4 RID: 2020 RVA: 0x0004F320 File Offset: 0x0004E320
	public spr\u24DF(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0004F338 File Offset: 0x0004E338
	public unsafe uint ᜁ()
	{
		uint ᜀ;
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_80;
				case 2:
					if (array.Length == 0)
					{
						num = 1;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 6;
						continue;
						break;
					}
				case 3:
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
				case 4:
					goto IL_A4;
				case 5:
					return ᜀ;
				case 6:
					goto IL_A4;
				}
				if (true)
				{
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 0;
					continue;
				}
				IL_80:
				byte* ptr = null;
				num = 4;
				continue;
				IL_A4:
				ᜀ = ((spr\u25DB*)ptr)->ᜀ;
				num = 5;
			}
		}
		return ᜀ;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0004F400 File Offset: 0x0004E400
	public unsafe void ᜀ(uint A_0)
	{
		byte* ptr;
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					if (array.Length == 0)
					{
						num = 2;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
				case 2:
					goto IL_7C;
				case 4:
					goto IL_87;
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
				IL_7C:
				ptr = null;
				num = 4;
			}
		}
		IL_7A:
		IL_87:
		((spr\u25DB*)ptr)->ᜀ = A_0;
		ptr = null;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0004F4BC File Offset: 0x0004E4BC
	public unsafe uint ᜀ()
	{
		uint ᜁ;
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
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
				case 1:
					goto IL_A4;
				case 2:
					return ᜁ;
				case 3:
					if (array.Length == 0)
					{
						num = 5;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 4;
						continue;
						break;
					}
				case 4:
					goto IL_A4;
				case 5:
					if (true)
					{
					}
					goto IL_78;
				case 6:
					num = 3;
					continue;
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 6;
					continue;
				}
				IL_78:
				byte* ptr = null;
				num = 1;
				continue;
				IL_A4:
				ᜁ = ((spr\u25DB*)ptr)->ᜁ;
				num = 2;
			}
		}
		return ᜁ;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0004F584 File Offset: 0x0004E584
	public unsafe void ᜁ(uint A_0)
	{
		byte* ptr;
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_72;
				case 1:
					if (true)
					{
					}
					if (array.Length == 0)
					{
						num = 4;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
				case 2:
					goto IL_7F;
				case 4:
					goto IL_74;
				case 5:
					num = 1;
					continue;
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 5;
					continue;
				}
				IL_74:
				ptr = null;
				num = 2;
			}
		}
		IL_72:
		IL_7F:
		((spr\u25DB*)ptr)->ᜁ = A_0;
		ptr = null;
	}
}
