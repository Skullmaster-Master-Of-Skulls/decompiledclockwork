using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000139 RID: 313
[CLSCompliant(false)]
[StructLayout(LayoutKind.Sequential)]
internal abstract class spr\u23F8
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x00058D68 File Offset: 0x00057D68
	internal spr\u23F8()
	{
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00058D7C File Offset: 0x00057D7C
	internal spr\u23F8(byte[] A_0)
	{
		this.ᜂ(A_0);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00058D98 File Offset: 0x00057D98
	internal spr\u23F8(byte[] A_0, int A_1)
	{
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00058DB4 File Offset: 0x00057DB4
	internal spr\u23F8(byte[] A_0, int A_1, int A_2)
	{
		this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00058DD0 File Offset: 0x00057DD0
	internal spr\u23F8(Stream A_0, int A_1)
	{
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00058DEC File Offset: 0x00057DEC
	internal virtual int ᜇ()
	{
		spr\u2562 spr_u;
		for (;;)
		{
			spr_u = this.ᜑ();
			if (spr_u == null)
			{
				return 0;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_20;
			}
		}
		IL_20:
		if (true)
		{
		}
		if (false)
		{
		}
		return spr_u.ᜀ();
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00058E3C File Offset: 0x00057E3C
	protected virtual spr\u2562 ᜑ()
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new Exception(ClipboardData.b("㭭ṯᙱᅳѵᑷ͹ᕻၽ톁ﶇﮍ뒓秊ﺗ몙\ude9b욟쒡쎥쮧얩\udeab쪭", a_));
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00058E94 File Offset: 0x00057E94
	internal static int ᜀ(Color A_0)
	{
		int result;
		for (;;)
		{
			result = 0;
			int num = 0;
			int num2 = spr\u23F8.ᜃ.Length;
			int num3 = 6;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (Color.FromArgb(spr\u23F8.ᜃ[num]) == A_0)
					{
						num3 = 3;
						continue;
					}
					num++;
					num3 = 1;
					continue;
				case 1:
					goto IL_A3;
				case 2:
					if (num >= num2)
					{
						num3 = 5;
						continue;
					}
					num3 = 0;
					continue;
				case 3:
					result = num;
					num3 = 4;
					continue;
				case 4:
					return result;
				case 5:
					return result;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_A3;
					}
					break;
				}
				break;
				IL_A3:
				if (true)
				{
				}
				num3 = 2;
			}
		}
		return result;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00058F6C File Offset: 0x00057F6C
	internal static bool ᜀ(byte A_0, int A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A4;
			case 2:
				num = 3;
				continue;
			case 3:
				goto IL_8D;
			}
			if (A_1 < 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_8D:
			if (A_1 < 8)
			{
				goto IL_A6;
			}
			if (true)
			{
			}
			num = 0;
		}
		IL_5D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("୨ɪᥬ㽮Ṱr", a_), A_1, ClipboardData.b("⭨ɪᥬ佮ⅰᱲٴṶ൸ቺቼᅾꆀ歷꾎떔ﮖﲘ뾞햠쮢쒤즦覨鮪趬삮쎰鎲튴얶\udcb8\udaba즼\udabe돀뇄꿆꣈ꗊ￐", a_));
		IL_A4:
		goto IL_5D;
		IL_A6:
		return spr\u23F8.ᜀ((int)A_0, A_1);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00059028 File Offset: 0x00058028
	internal static bool ᜀ(short A_0, int A_1)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7A;
			case 2:
				if (A_1 != 15)
				{
					goto IL_CE;
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
					num = 0;
					continue;
				}
				break;
			case 3:
				if (A_1 >= 16)
				{
					num = 5;
					continue;
				}
				num = 2;
				continue;
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_C7;
			}
			if (A_1 < 0)
			{
				goto IL_7C;
			}
			num = 4;
		}
		IL_7A:
		return A_0 < 0;
		IL_7C:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("᝴Ṷ൸⭺ቼ౾", a_), A_1, ClipboardData.b("㝴Ṷ൸孺⵼ၾ권ﶒﮔ뮚ﾜ爵膠쾢삤풦\udaa8讪\ud9ac잮킰\uddb2閴螶馸풺쾼龾ꛀ뇂ꃄꛆ뷈껊뿌ﯔ", a_));
		IL_C7:
		goto IL_7C;
		IL_CE:
		return spr\u23F8.ᜀ((int)A_0, A_1);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0005910C File Offset: 0x0005810C
	internal static bool ᜀ(int A_0, int A_1)
	{
		int a_ = 9;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 != 31)
				{
					goto IL_CE;
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
					num = 1;
					continue;
				}
				break;
			case 1:
				goto IL_82;
			case 2:
				if (true)
				{
				}
				num = 5;
				continue;
			case 4:
				goto IL_C7;
			case 5:
				if (A_1 >= 32)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_1 < 0)
			{
				goto IL_84;
			}
			num = 2;
		}
		IL_82:
		return A_0 < 0;
		IL_84:
		throw new ArgumentOutOfRangeException(ClipboardData.b("൮ᡰݲ╴ᡶ੸", a_), A_1, ClipboardData.b("⵮ᡰݲ啴❶ᙸࡺᑼ୾Ꞇﺐ떔ﲘ뮚爵튠킢薤펦솨쪪쎬辮膰鎲\udab4얶馸\udcba쾼\udabeꃀ럂ꃄ뗆ﳌ", a_));
		IL_C7:
		goto IL_84;
		IL_CE:
		return (A_0 & 1 << A_1) != 0;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x000591F8 File Offset: 0x000581F8
	internal static int ᜁ(int A_0, int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (A_0 & A_1) >> A_2;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0005923C File Offset: 0x0005823C
	internal static uint ᜁ(uint A_0, int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (uint)(((ulong)A_0 & (ulong)((long)A_1)) >> A_2);
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00059284 File Offset: 0x00058284
	internal static int ᜀ(int A_0, int A_1, bool A_2)
	{
		int a_ = 7;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_1 < 32)
					{
						num = 2;
						continue;
					}
					break;
				}
				num = 3;
				continue;
			case 1:
				goto IL_87;
			case 2:
				if (A_1 == 31)
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
			case 3:
				goto IL_110;
			case 4:
				num = 0;
				continue;
			case 5:
				if (true)
				{
				}
				A_0 |= 1 << A_1;
				num = 6;
				continue;
			case 6:
				return A_0;
			case 7:
				A_0 = -A_0;
				num = 1;
				continue;
			case 8:
				if (A_2)
				{
					num = 5;
					continue;
				}
				A_0 &= ~(1 << A_1);
				num = 10;
				continue;
			case 9:
				if (!A_2)
				{
					num = 7;
					continue;
				}
				return A_0;
			case 10:
				goto IL_CA;
			case 11:
				A_0 = Math.Abs(A_0);
				num = 9;
				continue;
			}
			if (A_1 < 0)
			{
				goto IL_8C;
			}
			num = 4;
		}
		IL_87:
		return A_0;
		IL_8C:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཬٮհ⍲ᩴѶ", a_), A_1, ClipboardData.b("⽬ٮհ卲╴ᡶ੸ቺॼᙾꖄ뎒릘튠莢톤쾦좨얪趬龮醰\udcb2잴鞶\udeb8즺\ud8bc\udebe뗀ꛂ럄靖流", a_));
		IL_CA:
		return A_0;
		IL_110:
		goto IL_8C;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00059404 File Offset: 0x00058404
	internal static int ᜀ(int A_0, int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0 &= ~A_1;
		A_0 += (A_2 & A_1);
		return A_0;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00059450 File Offset: 0x00058450
	internal static int ᜀ(int A_0, int A_1, int A_2, int A_3)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0 &= ~A_1;
		A_0 += (A_3 << A_2 & A_1);
		return A_0;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x000594A0 File Offset: 0x000584A0
	internal static uint ᜀ(uint A_0, int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 &= (uint)(~(uint)A_1);
		A_0 += (uint)(A_2 & A_1);
		return A_0;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x000594EC File Offset: 0x000584EC
	internal static bool ᜀ(uint A_0, int A_1)
	{
		int a_ = 14;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 2;
				continue;
			case 2:
				goto IL_8F;
			case 3:
				goto IL_9F;
			}
			if (true)
			{
			}
			if (A_1 < 0)
			{
				break;
			}
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
				continue;
			}
			IL_8F:
			if (A_1 <= 32)
			{
				goto IL_A1;
			}
			num = 3;
		}
		IL_65:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᙳή౷⩹፻ൽ", a_), ClipboardData.b("㙳ή౷婹ⱻᅽ겋ﲑ望秊몙ﺛﮝ肟캡솣향\udba7誩\ud8ab욭톯\udcb1钳蚵颷햹캻麽ꞿ냁ꇃꟅ볇꿉뻋䀘", a_));
		IL_9F:
		goto IL_65;
		IL_A1:
		return ((ulong)A_0 & (ulong)(1L << (A_1 & 31))) != 0UL;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x000595AC File Offset: 0x000585AC
	internal static uint ᜀ(uint A_0, int A_1, bool A_2)
	{
		int a_ = 3;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 >= 32)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			case 2:
				goto IL_73;
			case 3:
				if (A_2)
				{
					num = 4;
					continue;
				}
				A_0 &= ~(1U << A_1);
				num = 6;
				continue;
			case 4:
				A_0 |= 1U << A_1;
				num = 7;
				continue;
			case 5:
				goto IL_BC;
			case 6:
				goto IL_D4;
			case 7:
				goto IL_87;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_73:
				num = 1;
				continue;
			}
			if (false)
			{
			}
			if (A_1 < 0)
			{
				goto IL_D6;
			}
			if (true)
			{
			}
			num = 2;
		}
		IL_87:
		return A_0;
		IL_BC:
		goto IL_D6;
		IL_D4:
		return A_0;
		IL_D6:
		throw new ArgumentOutOfRangeException(ClipboardData.b("୨ɪᥬ㽮Ṱr", a_), ClipboardData.b("⭨ɪᥬ佮ⅰᱲٴṶ൸ቺቼᅾꆀꦈ꾎ﺚ膠첢힤螦캨\ud9aa좬캮얰횲잴鞶誸覺鎼", a_));
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x000596B4 File Offset: 0x000586B4
	internal static ushort ᜅ(Stream A_0)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[2];
			int num = A_0.Read(array, 0, 2);
			if (num == 2)
			{
				goto IL_4B;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2B;
			}
		}
		IL_2B:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new spr\u246D();
		IL_4B:
		return BitConverter.ToUInt16(array, 0);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00059714 File Offset: 0x00058714
	internal static uint ᜃ(Stream A_0)
	{
		byte[] array;
		for (;;)
		{
			if (true)
			{
			}
			array = new byte[4];
			int num = A_0.Read(array, 0, 4);
			if (num == 4)
			{
				goto IL_4B;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_33;
			}
		}
		IL_33:
		if (false)
		{
		}
		throw new spr\u246D();
		IL_4B:
		return BitConverter.ToUInt32(array, 0);
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00059774 File Offset: 0x00058774
	internal static short ᜂ(Stream A_0)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[2];
			int num = A_0.Read(array, 0, 2);
			if (num == 2)
			{
				goto IL_4B;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2B;
			}
		}
		IL_2B:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new spr\u246D();
		IL_4B:
		return BitConverter.ToInt16(array, 0);
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x000597D4 File Offset: 0x000587D4
	internal static int ᜁ(Stream A_0)
	{
		byte[] array = new byte[4];
		int num = A_0.Read(array, 0, 4);
		if (num != 4)
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
				break;
			}
			throw new spr\u246D();
		}
		return BitConverter.ToInt32(array, 0);
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00059834 File Offset: 0x00058834
	internal static ushort ᜃ(byte[] A_0, int A_1)
	{
		int a_ = 16;
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 > A_0.Length - 2)
					{
						num = 5;
						continue;
					}
					goto IL_CA;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_58;
				case 4:
					if (A_1 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_A8;
				case 5:
					goto IL_72;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 4;
				}
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("᝵੷ࡹ㡻ώ", a_));
			IL_CA:
			if (true)
			{
			}
			return BitConverter.ToUInt16(A_0, A_1);
		}
		}
		IL_72:
		IL_A8:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ή㝷ᱹ᩻ൽ", a_), ClipboardData.b("⁵᥷ᙹॻ᭽ꁿꢇ揄낏뚕ﾙ肟銡蒣장욧캩貫즭슯ힱ햳습\uddb7좹鲻\udfbd늿냁胃Ʂ볇ꯉ苍뗏병돓ꋕ냗龎ﻝꏟ跡諣闥鳧诩苫髭華\udcf1뛳迵賷鿹迻럽滿唁欃琅氇", a_));
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0005991C File Offset: 0x0005891C
	internal static ushort ᜀ(byte[] A_0, ref int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		ushort result = spr\u23F8.ᜃ(A_0, A_1);
		A_1 += 2;
		return result;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00059968 File Offset: 0x00058968
	internal static string ᜀ(Stream A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 5;
			byte[] array;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if ((long)num2 + A_0.Position > A_0.Length)
					{
						num = 1;
						continue;
					}
					array = new byte[num2];
					int num3 = A_0.Read(array, 0, num2);
					num = 4;
					continue;
				}
				case 1:
					goto IL_111;
				case 2:
					goto IL_86;
				case 3:
					goto IL_5A;
				case 4:
				{
					int num2;
					int num3;
					if (num3 != num2)
					{
						num = 2;
						continue;
					}
					goto IL_116;
				}
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					ushort num4 = spr\u23F8.ᜅ(A_0);
					int num2 = (int)(num4 * 2);
					num = 0;
				}
			}
			IL_5A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_116:
				return Encoding.Unicode.GetString(array);
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("ᩨὪὬ੮ၰṲ", a_));
			}
			IL_86:
			throw new Exception(ClipboardData.b("㱨ժ౬൮ᵰᙲ啴Ͷᙸ孺ོ᩾ꖄ慠랖ﶘ漢ﺞ膠얢힤좦쒨讪\ud9ac잮풰鎲운쎶쮸\udeba\udcbc튾", a_));
			IL_111:
			return string.Empty;
		}
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00059A98 File Offset: 0x00058A98
	internal static void ᜀ(Stream A_0, string A_1)
	{
		int a_ = 1;
		if (A_0 == null)
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
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ᑦᵨᥪ࡬๮ᱰ", a_));
		}
		spr\u23F8.ᜀ(A_0, (ushort)(Encoding.Unicode.GetByteCount(A_1) / 2));
		byte[] bytes = Encoding.Unicode.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00059B20 File Offset: 0x00058B20
	internal static string ᜂ(byte[] A_0, int A_1)
	{
		int a_ = 0;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 > A_0.Length - 2)
					{
						num = 1;
						continue;
					}
					goto IL_D2;
				case 1:
					goto IL_7A;
				case 2:
					goto IL_60;
				case 4:
					if (A_1 >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_B0;
				case 5:
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 4;
				}
			}
			IL_60:
			throw new ArgumentNullException(ClipboardData.b("ݥᩧᡩ⡫཭ѯ፱", a_));
			IL_D2:
			ushort count = spr\u23F8.ᜃ(A_0, A_1);
			A_1 += 2;
			return Encoding.Unicode.GetString(A_0, A_1, (int)count);
		}
		}
		IL_7A:
		IL_B0:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཥ❧౩੫ᵭᕯٱ", a_), ClipboardData.b("づ१٩ᥫ୭偯ᅱᕳᡵ塷ᑹ፻੽ꁿꚅﾋﶍ낏ꊑ뒓ﺙ벛劣튟잡얣튥춧\ud8a9貫쾭슯삱ힵ첷\udbb9銻ꖿ곁ꏃ닅ꃇ鏏뷑뫓ꗕ곗믙닛ꫝ鏟쳡ꛣ鿥鳧迩鿫꟭黯ꗱ鯳蓵鳷", a_));
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00059C1C File Offset: 0x00058C1C
	internal static string ᜀ(byte[] A_0, int A_1, ushort A_2)
	{
		int a_ = 14;
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_72;
				case 2:
					goto IL_58;
				case 3:
					if (A_1 >= 0)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_B0;
				case 4:
					num = 5;
					continue;
				case 5:
					if (A_1 > A_0.Length - 2)
					{
						num = 0;
						continue;
					}
					goto IL_D2;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("ᕳѵ੷㹹ᵻ੽", a_));
			IL_D2:
			return Encoding.Unicode.GetString(A_0, A_1, (int)A_2);
		}
		}
		IL_72:
		IL_B0:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᵳ㥵ṷᱹཻ᭽", a_), ClipboardData.b("≳᝵ᑷཹ᥻幽ꚅ꺍뒓歹ﶗ뺝邟芡얣좥첧誩쮫\udcad햯펱삳펵쪷骹\uddbb첽늿蛁ꗃ닅꧇胋ꯍ뻏뗑ꃓ뻕ﳛ鷝迟賡韣鋥觧蓩飫鷭\udeef냱跳苵鷷觹뗻都埿洁瘃戅", a_));
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00059D08 File Offset: 0x00058D08
	internal static string ᜀ(byte[] A_0, int A_1, out int A_2)
	{
		int a_ = 0;
		string result;
		for (;;)
		{
			byte b = A_0[A_1];
			A_1 += 2;
			result = string.Empty;
			A_2 = A_1 + (int)(b * 2);
			int num = 4;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (A_0[num2 + 1] == 0)
					{
						num = 3;
						continue;
					}
					goto IL_F8;
				case 1:
					goto IL_BB;
				case 2:
					if (num2 >= A_0.Length - 1)
					{
						num = 10;
						continue;
					}
					num = 6;
					continue;
				case 3:
					goto IL_A9;
				case 4:
					if (b != 0)
					{
						num = 5;
						continue;
					}
					goto IL_AB;
				case 5:
					result = Encoding.Unicode.GetString(A_0, A_1, (int)(b * 2));
					num = 8;
					continue;
				case 6:
					if (A_0[num2] == 0)
					{
						num = 9;
						continue;
					}
					goto IL_F8;
				case 7:
					goto IL_BB;
				case 8:
					goto IL_AB;
				case 9:
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
						num = 0;
						continue;
					}
					break;
				case 10:
					goto IL_D9;
				}
				break;
				IL_AB:
				num2 = A_2;
				num = 7;
				continue;
				IL_BB:
				num = 2;
				continue;
				IL_F8:
				num2++;
				num = 1;
			}
		}
		IL_A9:
		A_2 += 2;
		return result;
		IL_D9:
		throw new Exception(ClipboardData.b("㕥ᱧթṫ୭ᑯ剱ݳɵ੷፹ቻ᥽ꁿﶇ꺍뒓ﶗ뎝얟첡삣쎥첧", a_));
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00059E64 File Offset: 0x00058E64
	internal static byte[] ᜀ(string A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		byte[] array = new byte[A_0.Length * 2 + 4];
		array[0] = (byte)A_0.Length;
		Encoding.Unicode.GetBytes(A_0.ToCharArray(), 0, A_0.Length, array, 2);
		return array;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00059ED4 File Offset: 0x00058ED4
	internal static void ᜀ(byte[] A_0, ushort A_1, ref int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_2 = spr\u23F8.ᜀ(A_0, BitConverter.GetBytes(A_1), A_2);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00059F20 File Offset: 0x00058F20
	internal static void ᜀ(byte[] A_0, uint A_1, ref int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_2 = spr\u23F8.ᜀ(A_0, BitConverter.GetBytes(A_1), A_2);
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00059F6C File Offset: 0x00058F6C
	internal static void ᜀ(Stream A_0, uint A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00059FBC File Offset: 0x00058FBC
	internal static void ᜁ(Stream A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0005A00C File Offset: 0x0005900C
	internal static void ᜀ(Stream A_0, short A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0005A05C File Offset: 0x0005905C
	internal static void ᜀ(Stream A_0, ushort A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0005A0AC File Offset: 0x000590AC
	internal static void ᜀ(byte[] A_0, string A_1, ref int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Encoding unicode = Encoding.Unicode;
		A_2 = spr\u23F8.ᜀ(A_0, unicode.GetBytes(A_1), A_2);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0005A100 File Offset: 0x00059100
	internal static int ᜀ(byte[] A_0, byte[] A_1, int A_2)
	{
		int a_ = 19;
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
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 > A_0.Length - 2)
					{
						num = 3;
						continue;
					}
					goto IL_D2;
				case 1:
					if (A_2 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_B0;
				case 2:
					if (true)
					{
					}
					num = 0;
					continue;
				case 3:
					goto IL_7A;
				case 5:
					goto IL_58;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					num = 1;
				}
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("ᡸॺོ㭾", a_));
			IL_D2:
			A_1.CopyTo(A_0, A_2);
			return A_2 + A_1.Length;
		}
		}
		IL_7A:
		IL_B0:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ၸ㑺᭼᥾", a_), ClipboardData.b("⽸᩺ᅼ੾ꎂꮊ뎒릘튠莢閤螦좨얪즬辮횰솲킴횶춸\udeba쾼龾ꃀ뇂럄菆꣈뿊곌鷐뛒믔냖귘돚﷜쇠ꃢ諤触髨鿪賬臮藰胲\udbf4뗶胸迺飼賾䠀洂刄栆笈漊", a_));
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0005A1EC File Offset: 0x000591EC
	internal byte[] ᜂ(Stream A_0, int A_1)
	{
		byte[] array = new byte[A_1];
		int num = A_0.Read(array, 0, A_1);
		if (num != A_1)
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
				break;
			}
			throw new spr\u246D();
		}
		return array;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0005A248 File Offset: 0x00059248
	internal virtual void ᜂ(byte[] A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ(A_0, 0);
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0005A28C File Offset: 0x0005928C
	internal virtual void ᜁ(byte[] A_0, int A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ(A_0, A_1, A_0.Length - A_1);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0005A2D4 File Offset: 0x000592D4
	internal virtual void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 16;
		if (true)
		{
		}
		spr\u2562 spr_u;
		for (;;)
		{
			spr_u = this.ᜑ();
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9B;
				case 1:
					if (A_1 < 0)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_C1;
				case 3:
					goto IL_124;
				case 4:
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 5:
					goto IL_5B;
				case 6:
					if (A_2 < 0)
					{
						num = 9;
						continue;
					}
					num = 8;
					continue;
				case 7:
					if (spr_u == null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 8:
					if (A_1 + A_2 > A_0.Length)
					{
						num = 2;
						continue;
					}
					goto IL_163;
				case 9:
					goto IL_F1;
				}
				break;
			}
		}
		IL_5B:
		throw new ArgumentNullException(ClipboardData.b("⍵ᙷṹ᥻౽ﮁ\ud989ﲍﾙ", a_));
		IL_5D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ή㭷ᕹॻၽ", a_));
		IL_9B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_5D;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("᝵੷ࡹ㡻ώ", a_));
		}
		IL_C1:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ή㝷ᱹ᩻ൽꒃ궅ꢇ쾋ﲑ", a_));
		IL_F1:
		goto IL_5D;
		IL_124:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ή㝷ᱹ᩻ൽ", a_));
		IL_163:
		sprṵ.ᜀ().ᜀ(A_0, A_1, A_2, spr_u);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0005A454 File Offset: 0x00059454
	internal virtual void ᜀ(Stream A_0, int A_1)
	{
		int a_ = 1;
		byte[] array;
		for (;;)
		{
			IL_09:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_78;
				case 1:
					goto IL_46;
				case 3:
				{
					int num2;
					if (num2 != A_1)
					{
						num = 0;
						continue;
					}
					goto IL_EE;
				}
				case 4:
					goto IL_EC;
				case 5:
					if (A_1 >= 0)
					{
						array = new byte[A_1];
						int num2 = A_0.Read(array, 0, A_1);
						if (true)
						{
						}
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 5;
				}
			}
		}
		IL_46:
		throw new ArgumentNullException(ClipboardData.b("ᑦᵨᥪ࡬๮ᱰ", a_));
		IL_78:
		throw new Exception(ClipboardData.b("⑦٨ṪŬ୮ὰ呲Ŵ坶୸Ṻᱼ᭾ꆀﲈﾌ뎒ﺚ뾞잠톢쪤쪦覨\udfaa얬쪮醰삲솴얶\udcb8\udaba킼", a_));
		IL_EC:
		throw new ArgumentOutOfRangeException(ClipboardData.b("๦⩨ѪᡬŮհ卲ᙴᙶ᝸ᕺቼ୾ꆀꞆﺌﲎ놐ﶔ뮚궜", a_));
		IL_EE:
		this.ᜀ(array, 0, A_1);
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0005A558 File Offset: 0x00059558
	internal virtual int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 0;
		int num = 3;
		int num2;
		spr\u2562 spr_u;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 > A_0.Length)
				{
					num = 6;
					continue;
				}
				num2 = A_0.Length - A_1;
				spr_u = this.ᜑ();
				num = 4;
				continue;
			case 1:
				goto IL_6A;
			case 2:
				if (A_1 >= 0)
				{
					num = 7;
					continue;
				}
				goto IL_F5;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EB;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				if (spr_u == null)
				{
					num = 5;
					continue;
				}
				goto IL_109;
			case 5:
				goto IL_8F;
			case 6:
				goto IL_A7;
			case 7:
				goto IL_EB;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			IL_EB:
			if (true)
			{
			}
			num = 0;
		}
		IL_6A:
		throw new ArgumentNullException(ClipboardData.b("ݥᩧᡩ⡫཭ѯ፱", a_));
		IL_8F:
		throw new ArgumentNullException(ClipboardData.b("㍥٧๩५ᱭᱯୱᵳᡵί⥹ࡻ౽慎", a_));
		IL_A7:
		IL_F5:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཥ❧౩੫ᵭᕯٱ", a_));
		IL_109:
		sprṵ.ᜀ().ᜀ(spr_u, A_0, A_1, num2);
		return num2;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0005A680 File Offset: 0x00059680
	internal virtual int ᜄ(Stream A_0)
	{
		int a_ = 17;
		int num2;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 1:
					goto IL_3C;
				case 2:
					if (num2 < 0)
					{
						num = 0;
						continue;
					}
					goto IL_83;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num2 = this.ᜇ();
					num = 2;
				}
			}
			IL_83:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_99;
			}
		}
		IL_3C:
		throw new ArgumentNullException(ClipboardData.b("Ѷ൸ॺ᡼Ṿ", a_));
		IL_6D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ṷ㕸Ṻ፼᡾", a_));
		IL_99:
		if (false)
		{
		}
		byte[] array = new byte[num2];
		this.ᜀ(array, 0);
		A_0.Write(array, 0, num2);
		return num2;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0005A750 File Offset: 0x00059750
	internal virtual void \u170D()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0005A78C File Offset: 0x0005978C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u23F8()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u23F8.ᜃ = new int[]
		{
			0,
			-16777216,
			-16776961,
			-16711681,
			-16744448,
			-65281,
			-65536,
			-256,
			-1,
			-16777077,
			-16741493,
			-16751616,
			-7667573,
			-7667712,
			-256,
			-5658199,
			-2894893
		};
	}

	// Token: 0x04001141 RID: 4417
	private const int ᜀ = 8;

	// Token: 0x04001142 RID: 4418
	private const int ᜁ = 16;

	// Token: 0x04001143 RID: 4419
	private const int ᜂ = 32;

	// Token: 0x04001144 RID: 4420
	internal static readonly int[] ᜃ;
}
