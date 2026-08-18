using System;
using System.Runtime.InteropServices;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x0200009B RID: 155
internal class spr\u1F46 : spr\u2320
{
	// Token: 0x060004B8 RID: 1208 RVA: 0x0002E66C File Offset: 0x0002D66C
	public spr\u1F46(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3)
	{
		int a_ = 9;
		base..ctor(A_0, A_1, A_2, A_3);
		if (A_0.\u1712().ᜁ().ᜇ())
		{
			ushort num = sprᮌ.ᜁ(A_3, 0);
			if (num != 1536)
			{
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氤䤦弨䨪䄬䘮唰簲䔴制䬸娺䤼嘾⹀ⵂᩄɆㅈ⡊⡌⍎ݐ㙒❔⑖じ㑚㍜", a_)));
			}
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0002E6D4 File Offset: 0x0002D6D4
	public unsafe ushort ᜀ()
	{
		int num = 6;
		ushort ᜀ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				goto IL_AD;
			case 2:
				return ᜀ;
			case 3:
				goto IL_64;
			case 4:
				if (array.Length == 0)
				{
					num = 3;
					continue;
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
			case 5:
				goto IL_AD;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 0;
				continue;
			}
			IL_64:
			byte* ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				ptr = null;
				num = 5;
				continue;
			}
			IL_AD:
			ᜀ = ((spr\u1F46.ᜀ*)ptr)->ᜀ;
			num = 2;
		}
		return ᜀ;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0002E7A4 File Offset: 0x0002D7A4
	public unsafe void ᜃ(ushort A_0)
	{
		int num = 3;
		byte* ptr;
		for (;;)
		{
			IL_0A:
			byte[] array;
			switch (num)
			{
			case 0:
				while (array.Length == 0)
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
						num = 1;
						goto IL_0A;
					}
				}
				fixed (byte* ptr = &array[0])
				{
					num = 5;
					continue;
				}
			case 1:
				goto IL_56;
			case 2:
				goto IL_6B;
			case 4:
				if (true)
				{
				}
				num = 0;
				continue;
			case 5:
				goto IL_54;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_56:
			ptr = null;
			num = 2;
		}
		IL_54:
		IL_6B:
		((spr\u1F46.ᜀ*)ptr)->ᜀ = A_0;
		ptr = null;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0002E860 File Offset: 0x0002D860
	public unsafe ushort ᜅ()
	{
		int num = 1;
		ushort ᜁ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_52;
			case 2:
				num = 5;
				continue;
			case 3:
				goto IL_AA;
			case 4:
				return ᜁ;
			case 5:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 6;
					continue;
					break;
				}
			case 6:
				goto IL_AA;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 2;
				continue;
			}
			IL_52:
			byte* ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				ptr = null;
				num = 3;
				continue;
			}
			IL_AA:
			ᜁ = ((spr\u1F46.ᜀ*)ptr)->ᜁ;
			num = 4;
		}
		return ᜁ;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0002E930 File Offset: 0x0002D930
	public unsafe void ᜀ(ushort A_0)
	{
		int num = 5;
		byte* ptr;
		for (;;)
		{
			IL_0A:
			byte[] array;
			switch (num)
			{
			case 0:
				while (array.Length == 0)
				{
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
						num = 2;
						goto IL_0A;
					}
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
				}
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_4E;
			case 3:
				goto IL_4C;
			case 4:
				goto IL_63;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_4E:
			ptr = null;
			num = 4;
		}
		IL_4C:
		IL_63:
		((spr\u1F46.ᜀ*)ptr)->ᜁ = A_0;
		ptr = null;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0002E9EC File Offset: 0x0002D9EC
	public unsafe ushort ᜃ()
	{
		int num = 0;
		ushort ᜂ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				goto IL_52;
			case 2:
				goto IL_AA;
			case 3:
				goto IL_AA;
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
				if (true)
				{
				}
				num = 4;
				continue;
			case 6:
				return ᜂ;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 5;
				continue;
			}
			IL_52:
			byte* ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				ptr = null;
				num = 2;
				continue;
			}
			IL_AA:
			ᜂ = ((spr\u1F46.ᜀ*)ptr)->ᜂ;
			num = 6;
		}
		return ᜂ;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0002EABC File Offset: 0x0002DABC
	public unsafe void ᜂ(ushort A_0)
	{
		int num = 0;
		byte* ptr;
		for (;;)
		{
			IL_0A:
			byte[] array;
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 2:
				goto IL_6B;
			case 3:
				goto IL_54;
			case 4:
				goto IL_56;
			case 5:
				while (array.Length == 0)
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
						num = 4;
						goto IL_0A;
					}
				}
				fixed (byte* ptr = &array[0])
				{
					num = 3;
					continue;
				}
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_56:
			ptr = null;
			num = 2;
		}
		IL_54:
		IL_6B:
		((spr\u1F46.ᜀ*)ptr)->ᜂ = A_0;
		ptr = null;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002EB78 File Offset: 0x0002DB78
	public unsafe ushort ᜂ()
	{
		int num = 5;
		ushort ᜃ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_64;
			case 1:
				goto IL_AD;
			case 2:
				goto IL_AD;
			case 3:
				if (array.Length == 0)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				fixed (byte* ptr = &array[0])
				{
					num = 2;
					continue;
					break;
				}
			case 4:
				return ᜃ;
			case 6:
				num = 3;
				continue;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 6;
				continue;
			}
			IL_64:
			byte* ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				ptr = null;
				num = 1;
				continue;
			}
			IL_AD:
			ᜃ = ((spr\u1F46.ᜀ*)ptr)->ᜃ;
			num = 4;
		}
		return ᜃ;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0002EC48 File Offset: 0x0002DC48
	public unsafe void ᜁ(ushort A_0)
	{
		int num = 0;
		byte* ptr;
		for (;;)
		{
			IL_0A:
			byte[] array;
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				num = 4;
				continue;
			case 2:
				goto IL_4C;
			case 3:
				goto IL_4E;
			case 4:
				while (array.Length == 0)
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
				fixed (byte* ptr = &array[0])
				{
					num = 2;
					continue;
				}
			case 5:
				goto IL_63;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 1;
				continue;
			}
			IL_4E:
			ptr = null;
			num = 5;
		}
		IL_4C:
		IL_63:
		((spr\u1F46.ᜀ*)ptr)->ᜃ = A_0;
		ptr = null;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0002ED04 File Offset: 0x0002DD04
	public unsafe int ᜄ()
	{
		int num = 0;
		int ᜄ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				goto IL_80;
			case 2:
				if (true)
				{
				}
				num = 4;
				continue;
			case 3:
				goto IL_AD;
			case 4:
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 5;
					continue;
					break;
				}
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B5;
				}
				if (false)
				{
				}
				goto IL_AD;
			case 6:
				return ᜄ;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 2;
				continue;
			}
			IL_80:
			byte* ptr = null;
			num = 3;
			continue;
			IL_B5:
			num = 6;
			continue;
			IL_AD:
			ᜄ = ((spr\u1F46.ᜀ*)ptr)->ᜄ;
			goto IL_B5;
		}
		return ᜄ;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0002EDD4 File Offset: 0x0002DDD4
	public unsafe void ᜀ(int A_0)
	{
		int num = 4;
		byte* ptr;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					goto IL_81;
				case 2:
					goto IL_74;
				case 3:
					if (true)
					{
					}
					if (array.Length == 0)
					{
						num = 2;
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
				num = 0;
				continue;
			}
			}
			IL_81:
			num = 3;
		}
		IL_72:
		IL_7F:
		((spr\u1F46.ᜀ*)ptr)->ᜄ = A_0;
		ptr = null;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0002EE90 File Offset: 0x0002DE90
	public unsafe int ᜁ()
	{
		int num = 3;
		int ᜅ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_AA;
			case 1:
				goto IL_80;
			case 2:
				return ᜅ;
			case 3:
				if (true)
				{
				}
				break;
			case 4:
				num = 6;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B2;
				}
				if (false)
				{
				}
				goto IL_AA;
			case 6:
				if (array.Length == 0)
				{
					num = 1;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 5;
					continue;
					break;
				}
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 4;
				continue;
			}
			IL_80:
			byte* ptr = null;
			num = 0;
			continue;
			IL_B2:
			num = 2;
			continue;
			IL_AA:
			ᜅ = ((spr\u1F46.ᜀ*)ptr)->ᜅ;
			goto IL_B2;
		}
		return ᜅ;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002EF60 File Offset: 0x0002DF60
	public unsafe void ᜁ(int A_0)
	{
		int num = 4;
		byte* ptr;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] array;
				switch (num)
				{
				case 0:
					if (array.Length == 0)
					{
						num = 1;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 2;
						continue;
						break;
					}
				case 1:
					goto IL_7C;
				case 2:
					goto IL_7A;
				case 3:
					goto IL_87;
				case 5:
					goto IL_89;
				}
				if (true)
				{
				}
				if ((array = base.ᜢ()) != null)
				{
					num = 5;
					continue;
				}
				IL_7C:
				ptr = null;
				num = 3;
				continue;
			}
			}
			IL_89:
			num = 0;
		}
		IL_7A:
		IL_87:
		((spr\u1F46.ᜀ*)ptr)->ᜅ = A_0;
		ptr = null;
	}

	// Token: 0x0200009C RID: 156
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	private new struct ᜀ
	{
		// Token: 0x040002D0 RID: 720
		public ushort ᜀ;

		// Token: 0x040002D1 RID: 721
		public ushort ᜁ;

		// Token: 0x040002D2 RID: 722
		public ushort ᜂ;

		// Token: 0x040002D3 RID: 723
		public ushort ᜃ;

		// Token: 0x040002D4 RID: 724
		public int ᜄ;

		// Token: 0x040002D5 RID: 725
		public int ᜅ;
	}
}
