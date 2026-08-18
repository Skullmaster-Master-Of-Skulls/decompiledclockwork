using System;
using System.Text.RegularExpressions;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000028 RID: 40
internal class sprᣴ : sprạ
{
	// Token: 0x06000132 RID: 306 RVA: 0x0000B238 File Offset: 0x0000A238
	public ushort ᜆ()
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
		return this.ᜆ;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000B27C File Offset: 0x0000A27C
	public byte ᜃ()
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
		return this.ᜇ;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000B2C0 File Offset: 0x0000A2C0
	public bool ᜇ()
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
		return spr\u22CE.ᜀ((ushort)this.ᜈ, 128);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000B30C File Offset: 0x0000A30C
	public void ᜀ(bool A_0)
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
		this.ᜈ = spr\u22CE.ᜀ(this.ᜈ, 128, A_0);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000B360 File Offset: 0x0000A360
	public bool ᜅ()
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
		return spr\u22CE.ᜀ((ushort)this.ᜈ, 64);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000B3A8 File Offset: 0x0000A3A8
	public void ᜁ(bool A_0)
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
		this.ᜈ = spr\u22CE.ᜀ(this.ᜈ, 64, A_0);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000B3F8 File Offset: 0x0000A3F8
	protected sprᣴ(FormulaTokenCode A_0, int A_1) : base(A_0, A_1, FormulaTokenType.Operand)
	{
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000B410 File Offset: 0x0000A410
	public sprᣴ(FormulaTokenCode A_0) : base(A_0, 5, FormulaTokenType.Operand)
	{
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000B428 File Offset: 0x0000A428
	public static bool ᜈ(string A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			Match match = sprᣴ.ᜄ.Match(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (match.Value == A_0)
					{
						num = 3;
						continue;
					}
					return result;
				case 1:
					return result;
				case 2:
					if (true)
					{
					}
					if (match.Success)
					{
						num = 4;
						continue;
					}
					return result;
				case 3:
					result = sprᣴ.ᜀ(match);
					num = 1;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000B4E0 File Offset: 0x0000A4E0
	private static bool ᜀ(Match A_0)
	{
		int a_ = 6;
		string text;
		for (;;)
		{
			text = A_0.Groups[HyperlinksCollectionEditor.b("瀡䬣儥", a_)].Value;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string text2;
					if (sprᣴ.ᜀ(spr\u22CE.ᜀ(text2)))
					{
						num = 7;
						continue;
					}
					return false;
				}
				case 1:
					if (true)
					{
					}
					goto IL_DF;
				case 2:
				{
					string text2;
					if (text2[0] == sprᣴ.ᜅ)
					{
						num = 4;
						continue;
					}
					goto IL_7A;
				}
				case 3:
					if (text[0] == sprᣴ.ᜅ)
					{
						num = 5;
						continue;
					}
					goto IL_DF;
				case 4:
				{
					string text2 = text2.Remove(0, 1);
					num = 6;
					continue;
				}
				case 5:
					text = text.Remove(0, 1);
					num = 1;
					continue;
				case 6:
					goto IL_7A;
				case 7:
					goto IL_9D;
				}
				break;
				IL_7A:
				num = 0;
				continue;
				IL_DF:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
				{
					if (false)
					{
					}
					string text2 = A_0.Groups[HyperlinksCollectionEditor.b("愡䬣䨥崧䜩䈫", a_)].Value;
					num = 2;
					break;
				}
				}
			}
		}
		IL_9D:
		return sprᣴ.ᜁ(spr\u22CE.ᜃ(text));
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000B634 File Offset: 0x0000A634
	public static bool ᜇ(string A_0)
	{
		int a_ = 6;
		bool flag;
		for (;;)
		{
			flag = false;
			Match match = sprὶ.ᜁ.Match(A_0);
			int num = 6;
			for (;;)
			{
				bool flag2;
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					return flag;
				case 2:
					if (flag)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 3:
					flag = sprᣴ.ᜅ(match.Groups[HyperlinksCollectionEditor.b("愡䬣䨥崧䜩䈫Ἥ", a_)].Value);
					num = 2;
					continue;
				case 4:
					flag2 = flag;
					goto IL_103;
				case 5:
					flag2 = sprᣴ.ᜅ(match.Groups[HyperlinksCollectionEditor.b("愡䬣䨥崧䜩䈫ᰭ", a_)].Value);
					goto IL_103;
				case 6:
					if (match.Success)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						}
						if (false)
						{
						}
						num = 3;
						continue;
					}
					return flag;
				}
				break;
				IL_104:
				num = 1;
				continue;
				IL_103:
				flag = flag2;
				goto IL_104;
			}
		}
		return flag;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000B754 File Offset: 0x0000A754
	public static bool ᜆ(string A_0)
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
		return sprᣴ.ᜁ(spr\u2177.ᜁ(A_0));
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000B79C File Offset: 0x0000A79C
	public static bool ᜁ(int A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 < 0)
			{
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2E;
			}
		}
		IL_2E:
		if (false)
		{
		}
		return A_0 <= 65536;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000B7EC File Offset: 0x0000A7EC
	public static bool ᜀ(int A_0)
	{
		while (A_0 >= 0)
		{
			if (true)
			{
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
				return A_0 <= 255;
			}
		}
		return false;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000B83C File Offset: 0x0000A83C
	public static bool ᜅ(string A_0)
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
		return sprᣴ.ᜃ.Match(A_0).Success;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000B888 File Offset: 0x0000A888
	public static byte ᜄ(string A_0)
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (byte)spr\u22CE.ᜀ(sprᣴ.ᜄ.Match(A_0).Groups[HyperlinksCollectionEditor.b("椩䌫䈭䔯弱娳", a_)].Value);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000B8FC File Offset: 0x0000A8FC
	public static ushort ᜃ(string A_0)
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (ushort)spr\u22CE.ᜃ(sprᣴ.ᜄ.Match(A_0).Groups[HyperlinksCollectionEditor.b("䰝伟唡", a_)].Value);
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000B970 File Offset: 0x0000A970
	public override void ᜀ(object[] A_0)
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
		this.ᜂ(A_0[0] as string);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000B9BC File Offset: 0x0000A9BC
	public override void ᜀ(byte[] A_0, int A_1)
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
		this.ᜆ = BitConverter.ToUInt16(A_0, A_1);
		this.ᜇ = A_0[A_1 + 2];
		this.ᜈ = A_0[A_1 + 3];
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000BA1C File Offset: 0x0000AA1C
	public override byte[] ᜁ()
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
		byte[] array = base.ᜁ();
		BitConverter.GetBytes(this.ᜆ).CopyTo(array, 1);
		array[3] = this.ᜇ;
		array[4] = this.ᜈ;
		return array;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000BA84 File Offset: 0x0000AA84
	public override string ᜀ()
	{
		if (true)
		{
		}
		int num = 2;
		string str;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
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
					continue;
				}
				break;
			case 3:
				goto IL_6E;
			case 4:
				text = spr\u22CE.ᜀ((int)this.ᜆ);
				goto IL_EB;
			case 5:
				goto IL_86;
			case 6:
				text = sprᣴ.ᜅ + spr\u22CE.ᜀ((int)this.ᜆ);
				goto IL_EB;
			case 7:
				if (this.ᜅ())
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
			}
			if (this.ᜇ())
			{
				num = 1;
				continue;
			}
			num = 6;
			continue;
			IL_EB:
			str = text;
			num = 7;
		}
		IL_6E:
		string str2 = spr\u22CE.ᜁ((int)this.ᜇ);
		goto IL_112;
		IL_86:
		str2 = sprᣴ.ᜅ + spr\u22CE.ᜁ((int)this.ᜇ);
		IL_112:
		return str2 + str;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000BBAC File Offset: 0x0000ABAC
	private void ᜂ(string A_0)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		Match match = sprᣴ.ᜄ.Match(A_0);
		string value = match.Groups[HyperlinksCollectionEditor.b("縫䄭䜯", a_)].Value;
		string value2 = match.Groups[HyperlinksCollectionEditor.b("漫䄭尯䜱夳堵", a_)].Value;
		this.ᜀ(value, value2);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000BC44 File Offset: 0x0000AC44
	private void ᜁ(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_92;
			case 2:
				goto IL_47;
			case 3:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_71;
				default:
					goto IL_71;
				}
				IL_87:
				num = 0;
				continue;
				IL_71:
				if (false)
				{
				}
				this.ᜀ(false);
				A_0 = A_0.Substring(1);
				goto IL_87;
			}
			if (A_0[0] == sprᣴ.ᜅ)
			{
				num = 3;
			}
			else
			{
				this.ᜀ(true);
				num = 2;
			}
		}
		IL_47:
		IL_92:
		this.ᜆ = (ushort)spr\u22CE.ᜃ(A_0);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000BCF4 File Offset: 0x0000ACF4
	private void ᜀ(string A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_47;
			case 1:
				goto IL_92;
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
					this.ᜁ(false);
					A_0 = A_0.Substring(1);
					break;
				}
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (A_0[0] == sprᣴ.ᜅ)
			{
				num = 2;
			}
			else
			{
				this.ᜁ(true);
				num = 0;
			}
		}
		IL_47:
		IL_92:
		this.ᜇ = (byte)spr\u22CE.ᜀ(A_0);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000BDA4 File Offset: 0x0000ADA4
	protected void ᜀ(string A_0, string A_1)
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
		this.ᜁ(A_0);
		this.ᜀ(A_1);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000BDF0 File Offset: 0x0000ADF0
	// Note: this type is marked as 'beforefieldinit'.
	static sprᣴ()
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		sprᣴ.ᜂ = RegexOptions.Compiled;
		sprᣴ.ᜃ = new Regex(HyperlinksCollectionEditor.b("Ĩᐪᄬ氮帰弲䀴娶圸Ժ昼挾敀Ṃ穄᱆ࡈ晊ᝌ቎੐ቒ硔ൖј摚瑜", a_), sprᣴ.ᜂ);
		sprᣴ.ᜄ = new Regex(HyperlinksCollectionEditor.b("Ĩᐪᄬ氮帰弲䀴娶圸Ժ昼挾敀Ṃ穄᱆ࡈ晊ᝌ቎੐ቒ硔ൖј摚瑜睞幠形㝤ࡦṨ啪㙬㍮啰⹲䩴⭶ᵸ偺呼", a_), sprᣴ.ᜂ);
		sprᣴ.ᜅ = '$';
	}

	// Token: 0x04000067 RID: 103
	public new const byte ᜀ = 64;

	// Token: 0x04000068 RID: 104
	public new const byte ᜁ = 128;

	// Token: 0x04000069 RID: 105
	private static RegexOptions ᜂ;

	// Token: 0x0400006A RID: 106
	public static readonly Regex ᜃ;

	// Token: 0x0400006B RID: 107
	public static readonly Regex ᜄ;

	// Token: 0x0400006C RID: 108
	public static readonly char ᜅ;

	// Token: 0x0400006D RID: 109
	protected ushort ᜆ;

	// Token: 0x0400006E RID: 110
	protected byte ᜇ;

	// Token: 0x0400006F RID: 111
	protected byte ᜈ;
}
