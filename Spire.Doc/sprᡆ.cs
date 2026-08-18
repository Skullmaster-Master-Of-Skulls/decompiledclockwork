using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020001AD RID: 429
[CLSCompliant(false)]
internal class sprᡆ : spr\u23F8
{
	// Token: 0x060010DB RID: 4315 RVA: 0x000FD44C File Offset: 0x000FC44C
	internal sprᡆ()
	{
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000FD460 File Offset: 0x000FC460
	internal sprᡆ(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x000FD474 File Offset: 0x000FC474
	internal sprᡆ(byte[] A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000FD48C File Offset: 0x000FC48C
	internal sprᡆ(byte[] A_0, int A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x000FD4A4 File Offset: 0x000FC4A4
	internal sprᡆ(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000FD4BC File Offset: 0x000FC4BC
	internal int[] ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000FD500 File Offset: 0x000FC500
	internal void ᜀ(int[] A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x000FD544 File Offset: 0x000FC544
	internal override int ᜇ()
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
		return this.ᜀ.Length * 4;
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x000FD58C File Offset: 0x000FC58C
	internal string ᜀ(string A_0, int A_1)
	{
		int a_ = 10;
		int num = 5;
		int num2;
		int num3;
		int length;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_BF;
			case 1:
				if (A_0.Length == 0)
				{
					num = 8;
					continue;
				}
				num2 = this.ᜀ().Length - 1;
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				if (A_1 >= 0)
				{
					num = 4;
					continue;
				}
				goto IL_118;
			case 3:
				goto IL_54;
			case 4:
				num = 9;
				continue;
			case 6:
				length = this.ᜀ()[A_1 + 1] - num3;
				num = 7;
				continue;
			case 7:
				goto IL_FF;
			case 8:
				goto IL_E2;
			case 9:
				if (A_1 > num2)
				{
					num = 0;
					continue;
				}
				num3 = this.ᜀ()[A_1];
				length = 0;
				num = 11;
				continue;
			case 10:
				goto IL_164;
			case 11:
				if (A_1 + 1 >= this.ᜀ().Length)
				{
					length = A_0.Length - num3;
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_198;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_54:
		throw new ArgumentNullException(ClipboardData.b("ѯ᝱౳ɵ", a_));
		IL_BF:
		goto IL_118;
		IL_E2:
		goto IL_198;
		IL_FF:
		goto IL_1AC;
		IL_118:
		throw new ArgumentOutOfRangeException(ClipboardData.b("oᵱݳή౷፹፻ၽ", a_), ClipboardData.b("♯፱ᡳ͵ᵷ婹ύώꊁﲇꪉ낏ﺑ몙겛뺝솟첡삣蚥쾧\ud8a9즫쾭쒯ힱ욳隵", a_) + num2.ToString());
		IL_164:
		goto IL_1AC;
		IL_198:
		throw new ArgumentException(ClipboardData.b("ѯ᝱౳ɵ塷坹屻ൽꪉﺏ늑望秊몙ﺛﮝ肟잡즣횥\udca7펩", a_));
		IL_1AC:
		return A_0.Substring(num3, length);
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x000FD750 File Offset: 0x000FC750
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 13;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 + A_2 > A_0.Length)
				{
					num = 5;
					continue;
				}
				goto IL_13E;
			case 1:
				goto IL_139;
			case 2:
				if (A_1 >= 0)
				{
					num = 9;
					continue;
				}
				goto IL_67;
			case 3:
				goto IL_62;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				default:
					goto IL_D6;
				}
				break;
			case 5:
				goto IL_116;
			case 6:
				if (A_1 > A_0.Length - 1)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			case 7:
				if (A_2 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_A2;
			case 9:
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 2;
			continue;
			IL_62:
			num = 0;
		}
		IL_67:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩲ㩴ᅶὸࡺ᡼୾", a_), ClipboardData.b("╲ᑴ᭶౸Ṻ嵼᱾ꖄﾊ권뎒璉붜꾞膠슢쮤쎦覨첪\udfac쪮킰잲킴얶馸\udaba쾼춾藀ꋂ뇄ꛆ蟊꣌ꇎ뛐꟒뷔ﯚ", a_));
		IL_A2:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩲ㙴ᡶ౸ᕺॼ", a_));
		IL_D6:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ቲݴն㵸᩺ॼṾ", a_));
		IL_116:
		goto IL_A2;
		IL_139:
		goto IL_67;
		IL_13E:
		int num2 = A_2 / 4;
		this.ᜀ = new int[num2];
		Buffer.BlockCopy(A_0, 0, this.ᜀ, 0, num2 * 4);
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000FD8BC File Offset: 0x000FC8BC
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 10;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 < 0)
				{
					goto IL_B3;
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
					num = 5;
					continue;
				}
				break;
			case 1:
				goto IL_68;
			case 2:
				goto IL_3C;
			case 4:
				if (A_1 > A_0.Length - 1)
				{
					num = 1;
					continue;
				}
				goto IL_D5;
			case 5:
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᅯqٳ㉵᥷๹ᵻ", a_));
		IL_68:
		IL_B3:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᥯㵱ታၵ୷όࡻ", a_), ClipboardData.b("♯፱ᡳ͵ᵷ婹ύώꊁﲇꪉ낏ﺑ몙겛뺝솟첡삣蚥쾧\ud8a9즫쾭쒯ힱ욳隵\ud9b7좹캻諾ꆿ뛁ꗃ蓇꿉ꋋ꧍꓏뫑ﯕ", a_));
		IL_D5:
		Buffer.BlockCopy(this.ᜀ, 0, A_0, 0, this.ᜇ());
		return A_0.Length;
	}

	// Token: 0x040017D5 RID: 6101
	private new int[] ᜀ;
}
