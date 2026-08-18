using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Convertors.Sgml;

// Token: 0x020002A1 RID: 673
[DefaultMember("Item")]
internal class sprὂ : XmlReader
{
	// Token: 0x060023CA RID: 9162 RVA: 0x002431A0 File Offset: 0x002421A0
	public sprὂ()
	{
		this.ᜌ();
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x002431CC File Offset: 0x002421CC
	public sprὂ(XmlNameTable A_0)
	{
		this.ᜌ();
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x002431F8 File Offset: 0x002421F8
	public spr\u2057 \u1738()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 1:
				this.ᜀ(this.ᜑ);
				num = 2;
				continue;
			case 2:
				goto IL_70;
			}
			if (this.ᜈ != null)
			{
				break;
			}
			num = 1;
		}
		IL_70:
		return this.ᜈ;
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x00243280 File Offset: 0x00242280
	public void ᜀ(spr\u2057 A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x002432C4 File Offset: 0x002422C4
	private void ᜀ(Uri A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					CaseFolding caseFolding = this.\u170D();
					num = 20;
					continue;
				}
				case 1:
					goto IL_414;
				case 2:
					A_0 = new Uri(A_0, this.ᜡ);
					num = 29;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42B;
					default:
						if (false)
						{
						}
						if (string.IsNullOrEmpty(this.ᜡ))
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					}
					break;
				case 4:
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					num = 25;
					continue;
				case 5:
					return;
				case 6:
					num = 17;
					continue;
				case 7:
				{
					Stream manifestResourceStream;
					if (manifestResourceStream != null)
					{
						num = 31;
						continue;
					}
					goto IL_414;
				}
				case 8:
					goto IL_465;
				case 10:
					if (this.ᜈ.ᜌ() != null)
					{
						num = 0;
						continue;
					}
					return;
				case 11:
					num = 18;
					continue;
				case 12:
					goto IL_2D0;
				case 13:
					num = 3;
					continue;
				case 14:
					if (true)
					{
					}
					A_0 = new Uri(this.ᜑ, this.ᜡ);
					num = 8;
					continue;
				case 15:
					goto IL_465;
				case 16:
				{
					Assembly assembly = typeof(sprὂ).Assembly;
					string name = assembly.FullName.Split(new char[]
					{
						','
					})[0] + ClipboardData.b("䅮㥰ݲᡴ᭶坸ὺॼ᭾", a_);
					Stream manifestResourceStream = assembly.GetManifestResourceStream(name);
					num = 7;
					continue;
				}
				case 17:
					if (this.ᜤ != null)
					{
						num = 23;
						continue;
					}
					goto IL_414;
				case 18:
					if (!this.\u1716)
					{
						num = 13;
						continue;
					}
					goto IL_414;
				case 19:
					num = 26;
					continue;
				case 20:
				{
					CaseFolding caseFolding;
					switch (caseFolding)
					{
					case CaseFolding.ToUpper:
						this.\u171B = this.ᜈ.ᜌ().ToUpperInvariant();
						num = 12;
						continue;
					case CaseFolding.ToLower:
						this.\u171B = this.ᜈ.ᜌ().ToLowerInvariant();
						num = 30;
						continue;
					default:
						num = 19;
						continue;
					}
					break;
				}
				case 21:
					goto IL_414;
				case 22:
					num = 10;
					continue;
				case 23:
					num = 28;
					continue;
				case 24:
					if (this.ᜈ != null)
					{
						goto IL_42B;
					}
					return;
				case 25:
					if (this.ᜑ != null)
					{
						num = 14;
						continue;
					}
					A_0 = new Uri(new Uri(Directory.GetCurrentDirectory() + ClipboardData.b("䁮", a_)), this.ᜡ);
					num = 15;
					continue;
				case 26:
					this.\u171B = this.ᜈ.ᜌ();
					num = 27;
					continue;
				case 27:
					goto IL_2D0;
				case 28:
					if (sprἿ.ᜀ(this.ᜤ, ClipboardData.b("ݮհṲᥴ", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_414;
				case 29:
					goto IL_465;
				case 30:
					goto IL_2D0;
				case 31:
				{
					Stream manifestResourceStream;
					StreamReader a_2 = new StreamReader(manifestResourceStream);
					this.ᜈ = spr\u2057.ᜀ(A_0, ClipboardData.b("❮╰㹲㥴", a_), a_2, null, this.\u171F, null);
					num = 21;
					continue;
				}
				}
				if (this.ᜈ == null)
				{
					num = 11;
					continue;
				}
				goto IL_414;
				IL_2D0:
				this.\u171A = sprἿ.ᜀ(this.ᜈ.ᜌ(), ClipboardData.b("ݮհṲᥴ", a_));
				num = 5;
				continue;
				IL_414:
				num = 24;
				continue;
				IL_42B:
				num = 22;
				continue;
				IL_465:
				this.ᜈ = spr\u2057.ᜀ(A_0, this.ᜤ, this.ᜢ, A_0.AbsoluteUri, this.ᜣ, this.\u171F, null);
				num = 1;
			}
			return;
		}
		}
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x00243774 File Offset: 0x00242774
	public string \u1735()
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
		return this.ᜤ;
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x002437B8 File Offset: 0x002427B8
	public void ᜉ(string A_0)
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
		this.ᜤ = A_0;
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x002437FC File Offset: 0x002427FC
	public string ᜪ()
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
		return this.\u171B;
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x00243840 File Offset: 0x00242840
	public string \u171B()
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
		return this.ᜢ;
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x00243884 File Offset: 0x00242884
	public void ᜆ(string A_0)
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
		this.ᜢ = A_0;
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x002438C8 File Offset: 0x002428C8
	public string ᜐ()
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
		return this.ᜡ;
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x0024390C File Offset: 0x0024290C
	public void ᜅ(string A_0)
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
		this.ᜡ = A_0;
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x00243950 File Offset: 0x00242950
	public string \u1713()
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
		return this.ᜣ;
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x00243994 File Offset: 0x00242994
	public void ᜋ(string A_0)
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
		this.ᜣ = A_0;
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x002439D8 File Offset: 0x002429D8
	public TextReader ᜯ()
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
		return this.ᜠ;
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x00243A1C File Offset: 0x00242A1C
	public void ᜁ(TextReader A_0)
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
		this.ᜠ = A_0;
		this.ᜌ();
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x00243A64 File Offset: 0x00242A64
	public string ᜬ()
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
		return this.\u171F;
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x00243AA8 File Offset: 0x00242AA8
	public void ᜏ(string A_0)
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
		this.\u171F = A_0;
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x00243AEC File Offset: 0x00242AEC
	public void ᜄ(string A_0)
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
		this.ᜑ = new Uri(A_0);
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x00243B34 File Offset: 0x00242B34
	public string ᜮ()
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
		return this.\u171C;
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x00243B78 File Offset: 0x00242B78
	public void ᜎ(string A_0)
	{
		int a_ = 11;
		for (;;)
		{
			this.\u171C = A_0;
			this.ᜌ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E3;
				case 1:
					num = 4;
					continue;
				case 2:
					if (this.ᜑ == null)
					{
						num = 1;
						continue;
					}
					goto IL_E3;
				case 3:
					goto IL_E1;
				case 4:
					if (this.\u171C.IndexOf(ClipboardData.b("䭰屲婴", a_)) > 0)
					{
						num = 3;
						continue;
					}
					goto IL_5F;
				}
				break;
				IL_5F:
				this.ᜑ = new Uri(ClipboardData.b("ᝰᩲᥴቶ䍸呺剼", a_) + Directory.GetCurrentDirectory() + ClipboardData.b("幰屲", a_));
				num = 0;
				continue;
				IL_E3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5F;
				default:
					goto IL_F9;
				}
			}
		}
		IL_E1:
		this.ᜑ = new Uri(this.\u171C);
		return;
		IL_F9:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x00243C8C File Offset: 0x00242C8C
	public bool \u1715()
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
		return this.ᜧ;
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x00243CD0 File Offset: 0x00242CD0
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
		this.ᜧ = A_0;
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x00243D14 File Offset: 0x00242D14
	public bool ᜰ()
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
		return this.\u1716;
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x00243D58 File Offset: 0x00242D58
	public void ᜀ(bool A_0)
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
		this.\u1716 = A_0;
	}

	// Token: 0x060023E3 RID: 9187 RVA: 0x00243D9C File Offset: 0x00242D9C
	public CaseFolding \u170D()
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
		return this.ᜦ;
	}

	// Token: 0x060023E4 RID: 9188 RVA: 0x00243DE0 File Offset: 0x00242DE0
	public void ᜀ(CaseFolding A_0)
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
		this.ᜦ = A_0;
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x00243E24 File Offset: 0x00242E24
	public TextWriter \u171A()
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
		return this.\u1714;
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x00243E68 File Offset: 0x00242E68
	public void ᜀ(TextWriter A_0)
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
		this.\u1714 = A_0;
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x00243EAC File Offset: 0x00242EAC
	public string ᜭ()
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
		return this.\u171D;
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x00243EF0 File Offset: 0x00242EF0
	public void \u170D(string A_0)
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
		this.\u171D = A_0;
		this.\u1714 = new StreamWriter(A_0);
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x00243F40 File Offset: 0x00242F40
	private void ᜀ(string A_0, params string[] A_1)
	{
		int a_ = 11;
		int num = 3;
		string text2;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				text = this.ᜉ.ᜁ().AbsolutePath;
				goto IL_8D;
			case 1:
				return;
			case 2:
				goto IL_FE;
			case 4:
				if (this.ᜉ.ᜁ() != null)
				{
					num = 0;
					continue;
				}
				goto IL_FE;
			case 5:
				text2 = string.Format(CultureInfo.CurrentUICulture, A_0, A_1);
				num = 6;
				continue;
			case 6:
				if (this.\u171E != this.ᜉ)
				{
					num = 7;
					continue;
				}
				text = "";
				num = 4;
				continue;
			case 7:
				goto IL_FC;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8D:
				num = 2;
				continue;
			default:
				if (false)
				{
				}
				if (this.\u171A() != null)
				{
					num = 5;
					continue;
				}
				return;
			}
			IL_FE:
			this.\u171A().WriteLine(ClipboardData.b("剰偲噴坶㱸ॺོၾꎂꦈ붌느꒔떘뮚쾠욢薤\udca6鮨횪膬辮솰\udcb2운\udeb6춸튺튼톾룂뫆뛌﯎곐", a_), new object[]
			{
				text,
				this.ᜉ.\u1712(),
				this.ᜉ.\u1715(),
				this.ᜉ.\u170D(),
				text2
			});
			num = 1;
		}
		IL_FC:
		text2 = text2 + ClipboardData.b("兰卲啴坶", a_) + this.ᜉ.ᜇ();
		this.\u171E = this.ᜉ;
		this.\u171A().WriteLine(ClipboardData.b("剰偲噴坶㱸ॺོၾ릂", a_) + text2);
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x00244104 File Offset: 0x00243104
	private void ᜀ(string A_0, char A_1)
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
		this.ᜀ(A_0, new string[]
		{
			A_1.ToString()
		});
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x00244158 File Offset: 0x00243158
	private void ᜌ()
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
		this.ᜊ = State.Initial;
		this.\u170D = new sprᢐ(10);
		this.ᜎ = this.ᜀ(null, XmlNodeType.Document, null);
		this.ᜎ.ᜄ = false;
		this.\u1712 = new StringBuilder();
		this.\u1713 = new StringBuilder();
		this.\u1718 = 0;
		this.ᜉ = null;
		this.ᜋ = '\0';
		this.ᜌ = null;
		this.ᜏ = null;
		this.ᜐ = 0;
		this.\u1717 = null;
		this.\u1719 = 0;
		this.\u1715 = false;
		this.ᜨ.Clear();
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x00244224 File Offset: 0x00243224
	private spr\u20C0 ᜀ(string A_0, XmlNodeType A_1, string A_2)
	{
		spr\u20C0 spr_u20C;
		for (;;)
		{
			for (;;)
			{
				spr_u20C = (spr\u20C0)this.\u170D.ᜃ();
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u20C != null)
						{
							goto IL_94;
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
						break;
					case 1:
						spr_u20C = new spr\u20C0();
						this.\u170D.ᜀ(this.\u170D.ᜀ() - 1, spr_u20C);
						num = 2;
						continue;
					case 2:
						goto IL_92;
					}
					break;
				}
			}
		}
		IL_92:
		IL_94:
		spr_u20C.ᜀ(A_0, A_1, A_2);
		this.ᜎ = spr_u20C;
		return spr_u20C;
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x002442D8 File Offset: 0x002432D8
	private void ᜋ()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.\u170D.ᜀ() - 1;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u20C0 a_ = (spr\u20C0)this.\u170D.ᜂ(num - 1);
						this.\u170D.ᜀ(num - 1, this.\u170D.ᜂ(num));
						this.\u170D.ᜀ(num, a_);
						num2 = 1;
						continue;
					}
					case 1:
						return;
					case 2:
						if (num <= 0)
						{
							return;
						}
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
							num2 = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
		}
	}

	// Token: 0x060023EE RID: 9198 RVA: 0x00244398 File Offset: 0x00243398
	private spr\u20C0 ᜂ(spr\u20C0 A_0)
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
		spr\u20C0 spr_u20C = this.ᜀ(A_0.ᜅ, A_0.ᜀ, A_0.ᜁ);
		spr_u20C.ᜆ = A_0.ᜆ;
		spr_u20C.ᜄ = A_0.ᜄ;
		spr_u20C.ᜂ = A_0.ᜂ;
		spr_u20C.ᜃ = A_0.ᜃ;
		spr_u20C.ᜇ = A_0.ᜇ;
		spr_u20C.ᜀ(A_0);
		this.ᜎ = spr_u20C;
		return spr_u20C;
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x00244438 File Offset: 0x00243438
	private void ᜊ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4E:
			num = 1;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜎ = (spr\u20C0)this.\u170D.ᜂ();
				num = 2;
				continue;
			case 2:
				return;
			}
			break;
		}
		if (this.\u170D.ᜀ() > 1)
		{
			goto IL_4E;
		}
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x002444C8 File Offset: 0x002434C8
	private spr\u20C0 ᜉ()
	{
		int num;
		for (;;)
		{
			num = this.\u170D.ᜀ() - 1;
			if (num <= 0)
			{
				goto IL_54;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_34;
			}
		}
		IL_34:
		if (true)
		{
		}
		if (false)
		{
		}
		return (spr\u20C0)this.\u170D.ᜂ(num);
		IL_54:
		return null;
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x0024452C File Offset: 0x0024352C
	public virtual XmlNodeType \u1712()
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return XmlNodeType.Attribute;
			case 2:
				if (this.ᜊ == State.AttrValue)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C5;
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				num = 6;
				continue;
			case 3:
				return XmlNodeType.Text;
			case 4:
				num = 7;
				continue;
			case 5:
				return XmlNodeType.EndElement;
			case 6:
				if (this.ᜊ != State.EndTag)
				{
					goto IL_C5;
				}
				return XmlNodeType.EndElement;
			case 7:
				if (this.ᜊ == State.AutoClose)
				{
					num = 5;
					continue;
				}
				goto IL_D5;
			}
			if (this.ᜊ == State.Attr)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			IL_C5:
			num = 4;
		}
		return XmlNodeType.Attribute;
		IL_D5:
		return this.ᜎ.ᜀ;
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x0024461C File Offset: 0x0024361C
	public virtual string ᜏ()
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
		string result;
		for (;;)
		{
			result = null;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					if (this.ᜊ != State.AttrValue)
					{
						num = 4;
						continue;
					}
					return result;
				case 2:
					return result;
				case 3:
					if (this.ᜊ == State.Attr)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				case 4:
					result = this.ᜎ.ᜅ;
					num = 0;
					continue;
				case 5:
					result = XmlConvert.EncodeName(this.ᜏ.ᜀ);
					num = 2;
					continue;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x002446E4 File Offset: 0x002436E4
	public virtual string ᜣ()
	{
		string text;
		for (;;)
		{
			IL_38:
			text = this.Name;
			int num = 2;
			for (;;)
			{
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						text = text.Substring(num2 + 1);
						num = 4;
						continue;
					case 2:
						if (text != null)
						{
							num = 0;
							continue;
						}
						return text;
					case 3:
						if (num2 != -1)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						return text;
					case 4:
						return text;
					}
					goto IL_38;
				}
				IL_52:
				num2 = text.IndexOf(':');
				num = 3;
			}
		}
		return text;
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x00244794 File Offset: 0x00243794
	public virtual string ᜤ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 33;
			string text2;
			for (;;)
			{
				string a_2;
				int num4;
				string prefix2;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_36B;
				case 1:
					goto IL_23D;
				case 2:
				{
					spr\u20C0 spr_u20C;
					if (spr_u20C != null)
					{
						num = 17;
						continue;
					}
					goto IL_5C8;
				}
				case 3:
					goto IL_2A0;
				case 4:
				{
					string prefix;
					if ((prefix = this.Prefix) != null)
					{
						num = 50;
						continue;
					}
					goto IL_481;
				}
				case 5:
					goto IL_324;
				case 6:
				{
					string text;
					return text;
				}
				case 7:
				{
					int num2;
					if (num2 >= 0)
					{
						num = 26;
						continue;
					}
					goto IL_5C8;
				}
				case 8:
				{
					spr\u20C0 spr_u20C2;
					int num3;
					text2 = spr_u20C2.ᜀ(num3).ᜁ();
					num = 38;
					continue;
				}
				case 9:
				{
					spr\u20C0 spr_u20C2;
					if (spr_u20C2 != null)
					{
						num = 25;
						continue;
					}
					goto IL_171;
				}
				case 10:
					goto IL_270;
				case 11:
					if (this.NodeType != XmlNodeType.Attribute)
					{
						num = 29;
						continue;
					}
					goto IL_564;
				case 12:
					num = 39;
					continue;
				case 13:
				{
					spr\u20C0 spr_u20C;
					int num2 = spr_u20C.ᜀ(ClipboardData.b("ὦѨݪͬᱮ", a_));
					num = 7;
					continue;
				}
				case 14:
				{
					spr\u20C0 spr_u20C2;
					if (spr_u20C2.ᜀ == XmlNodeType.Element)
					{
						num = 19;
						continue;
					}
					goto IL_171;
				}
				case 15:
					return text2;
				case 16:
				{
					string prefix;
					if (!(prefix == ""))
					{
						num = 44;
						continue;
					}
					goto IL_481;
				}
				case 17:
					num = 49;
					continue;
				case 18:
					if (string.Equals(this.ᜏ.ᜀ, ClipboardData.b("ὦѨݪͬᱮ", a_), StringComparison.OrdinalIgnoreCase))
					{
						num = 5;
						continue;
					}
					goto IL_5FE;
				case 19:
				{
					spr\u20C0 spr_u20C2;
					int num3 = spr_u20C2.ᜀ(a_2);
					num = 46;
					continue;
				}
				case 20:
					if (true)
					{
					}
					goto IL_36B;
				case 21:
					num = 31;
					continue;
				case 22:
					num = 16;
					continue;
				case 23:
				{
					string prefix;
					if (!(prefix == ClipboardData.b("ὦѨݪͬᱮ", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_162;
				}
				case 24:
					text2 = ClipboardData.b("䑦ᱨժ٬ŮṰѲ᭴", a_) + this.ᜨ.Count.ToString();
					num = 3;
					continue;
				case 25:
					num = 14;
					continue;
				case 26:
				{
					spr\u20C0 spr_u20C;
					int num2;
					string text = spr_u20C.ᜀ(num2).ᜁ();
					num = 40;
					continue;
				}
				case 27:
					goto IL_25C;
				case 28:
					goto IL_23D;
				case 29:
					num = 34;
					continue;
				case 30:
					goto IL_4A2;
				case 31:
				{
					string prefix;
					if (!(prefix == ClipboardData.b("ὦѨݪ", a_)))
					{
						num = 22;
						continue;
					}
					goto IL_261;
				}
				case 32:
					num4 = this.\u170D.ᜀ() - 1;
					num = 1;
					continue;
				case 33:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C9;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 34:
					if (this.NodeType == XmlNodeType.Element)
					{
						num = 35;
						continue;
					}
					goto IL_270;
				case 35:
					goto IL_564;
				case 36:
					if (!this.ᜨ.TryGetValue(prefix2, out text2))
					{
						num = 12;
						continue;
					}
					return text2;
				case 37:
					goto IL_2A0;
				case 38:
					if (text2 != null)
					{
						num = 15;
						continue;
					}
					goto IL_171;
				case 39:
					if (this.ᜨ.Count > 0)
					{
						num = 24;
						continue;
					}
					text2 = ClipboardData.b("䑦ᱨժ٬ŮṰѲ᭴", a_);
					num = 37;
					continue;
				case 40:
				{
					string text;
					if (text != null)
					{
						num = 6;
						continue;
					}
					goto IL_5C8;
				}
				case 41:
					num = 11;
					continue;
				case 42:
					goto IL_2BA;
				case 43:
					num = 18;
					continue;
				case 44:
					num = 41;
					continue;
				case 45:
				{
					if (num4 <= 0)
					{
						num = 27;
						continue;
					}
					spr\u20C0 spr_u20C = this.\u170D.ᜂ(num4) as spr\u20C0;
					num = 2;
					continue;
				}
				case 46:
				{
					int num3;
					if (num3 >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_171;
				}
				case 47:
				{
					if (num5 <= 0)
					{
						num = 10;
						continue;
					}
					spr\u20C0 spr_u20C2 = this.\u170D.ᜂ(num5) as spr\u20C0;
					num = 9;
					continue;
				}
				case 48:
					if (this.NodeType == XmlNodeType.Element)
					{
						num = 32;
						continue;
					}
					goto IL_15C;
				case 49:
				{
					spr\u20C0 spr_u20C;
					if (spr_u20C.ᜀ == XmlNodeType.Element)
					{
						num = 13;
						continue;
					}
					goto IL_5C8;
				}
				case 50:
					goto IL_4C9;
				case 51:
					if (this.NodeType == XmlNodeType.Attribute)
					{
						num = 30;
						continue;
					}
					num = 48;
					continue;
				}
				if (this.ᜊ == State.Attr)
				{
					num = 43;
					continue;
				}
				goto IL_5FE;
				IL_171:
				num5--;
				num = 20;
				continue;
				IL_23D:
				num = 45;
				continue;
				IL_270:
				num = 36;
				continue;
				IL_2A0:
				this.ᜨ[prefix2] = text2;
				num = 42;
				continue;
				IL_36B:
				num = 47;
				continue;
				IL_481:
				num = 51;
				continue;
				IL_4C9:
				num = 23;
				continue;
				IL_564:
				a_2 = ClipboardData.b("ὦѨݪͬᱮ䭰", a_) + prefix2;
				num5 = this.\u170D.ᜀ() - 1;
				num = 0;
				continue;
				IL_5C8:
				num4--;
				num = 28;
				continue;
				IL_5FE:
				prefix2 = this.Prefix;
				num = 4;
			}
			IL_15C:
			return string.Empty;
			IL_162:
			return ClipboardData.b("སᵨὪᵬ啮幰屲ɴv๸啺੼䱾꾀Ꚉ릊붌뾎ꆐ벒殺낞", a_);
			IL_25C:
			goto IL_15C;
			IL_261:
			return ClipboardData.b("སᵨὪᵬ啮幰屲ɴv๸啺੼䱾꾀Ꚉ펊삌쎎뺐ꊒ겔꺖ꆘ뒚ﺞ철욢횤힦좨좪좬", a_);
			IL_2BA:
			return text2;
			IL_324:
			return ClipboardData.b("སᵨὪᵬ啮幰屲ɴv๸啺੼䱾꾀Ꚉ릊붌뾎ꆐ벒殺낞", a_);
			IL_4A2:
			return string.Empty;
		}
		}
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x00244E04 File Offset: 0x00243E04
	public virtual string ᜨ()
	{
		string result;
		for (;;)
		{
			string text = this.Name;
			int num = 7;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_C8;
				case 1:
					goto IL_44;
				case 2:
					num2 = text.IndexOf(':');
					if (true)
					{
					}
					num = 4;
					continue;
				case 3:
					if ((result = text) == null)
					{
						num = 6;
						continue;
					}
					return result;
				case 4:
					if (num2 != -1)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C8;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					text = string.Empty;
					num = 5;
					continue;
				case 5:
					goto IL_44;
				case 6:
					goto IL_C1;
				case 7:
					if (text != null)
					{
						num = 2;
						continue;
					}
					goto IL_44;
				}
				break;
				IL_44:
				num = 3;
				continue;
				IL_C8:
				text = text.Substring(0, num2);
				num = 1;
			}
		}
		IL_C1:
		result = string.Empty;
		return result;
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x00244EF4 File Offset: 0x00243EF4
	public virtual bool \u1733()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 2:
				if (true)
				{
				}
				num = 3;
				continue;
			case 3:
				if (this.ᜊ == State.AttrValue)
				{
					num = 0;
					continue;
				}
				goto IL_7E;
			}
			IL_2A:
			if (this.ᜊ == State.Attr)
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
			goto IL_2A;
		}
		return true;
		IL_7C:
		return true;
		IL_7E:
		return this.ᜎ.ᜁ != null;
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x00244F90 File Offset: 0x00243F90
	public virtual string \u1732()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				if (this.ᜊ == State.AttrValue)
				{
					num = 3;
					continue;
				}
				goto IL_88;
			case 3:
				goto IL_86;
			}
			IL_2A:
			if (this.ᜊ == State.Attr)
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
			goto IL_2A;
		}
		IL_61:
		return this.ᜏ.ᜁ();
		IL_86:
		goto IL_61;
		IL_88:
		return this.ᜎ.ᜁ;
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x00245030 File Offset: 0x00244030
	public virtual int \u1719()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3B;
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
					if (this.ᜊ == State.AttrValue)
					{
						num = 2;
						continue;
					}
					goto IL_96;
				}
				break;
			case 2:
				goto IL_88;
			}
			IL_2A:
			if (this.ᜊ == State.Attr)
			{
				num = 0;
				continue;
			}
			if (true)
			{
			}
			num = 1;
			continue;
			goto IL_2A;
		}
		IL_3B:
		return this.\u170D.ᜀ();
		IL_88:
		return this.\u170D.ᜀ() + 1;
		IL_96:
		return this.\u170D.ᜀ() - 1;
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x002450E0 File Offset: 0x002440E0
	public virtual string \u1739()
	{
		while (!(this.ᜑ == null))
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
				return this.ᜑ.AbsoluteUri;
			}
		}
		return "";
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x0024513C File Offset: 0x0024413C
	public virtual bool \u1714()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8E;
			case 1:
				num = 3;
				continue;
			case 2:
				num = 4;
				continue;
			case 3:
				if (this.ᜊ == State.AttrValue)
				{
					num = 0;
					continue;
				}
				return false;
			case 4:
				if (this.ᜊ != State.Attr)
				{
					num = 1;
					continue;
				}
				goto IL_5F;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5F;
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
			if (this.ᜊ == State.Markup)
			{
				break;
			}
			num = 2;
		}
		IL_5F:
		return this.ᜎ.ᜄ;
		IL_8E:
		goto IL_5F;
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x002451F8 File Offset: 0x002441F8
	public virtual bool \u171F()
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				if (this.ᜊ == State.AttrValue)
				{
					num = 2;
					continue;
				}
				return false;
			case 2:
				goto IL_86;
			case 3:
				num = 1;
				continue;
			}
			IL_32:
			if (this.ᜊ == State.Attr)
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
				num = 3;
				continue;
			}
			goto IL_32;
		}
		IL_61:
		return this.ᜏ.ᜀ();
		IL_86:
		goto IL_61;
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x00245290 File Offset: 0x00244290
	public virtual char ᜩ()
	{
		while (this.ᜏ != null)
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
				return this.ᜏ.ᜂ;
			}
		}
		return '\0';
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x002452E4 File Offset: 0x002442E4
	public virtual XmlSpace \u1718()
	{
		XmlSpace xmlSpace;
		for (;;)
		{
			for (;;)
			{
				int num = this.\u170D.ᜀ() - 1;
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
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return XmlSpace.None;
						case 1:
							goto IL_A2;
						case 2:
						{
							if (num <= 1)
							{
								num2 = 0;
								continue;
							}
							spr\u20C0 spr_u20C = (spr\u20C0)this.\u170D.ᜂ(num);
							xmlSpace = spr_u20C.ᜂ;
							num2 = 3;
							continue;
						}
						case 3:
							if (xmlSpace != XmlSpace.None)
							{
								num2 = 4;
								continue;
							}
							num--;
							num2 = 5;
							continue;
						case 4:
							return xmlSpace;
						case 5:
							goto IL_A2;
						}
						break;
						IL_A2:
						if (true)
						{
						}
						num2 = 2;
					}
					break;
				}
				}
			}
		}
		return xmlSpace;
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x002453B8 File Offset: 0x002443B8
	public virtual string \u1734()
	{
		string text;
		for (;;)
		{
			for (;;)
			{
				int num = this.\u170D.ᜀ() - 1;
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
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num <= 1)
							{
								if (true)
								{
								}
								num2 = 5;
								continue;
							}
							spr\u20C0 spr_u20C = (spr\u20C0)this.\u170D.ᜂ(num);
							text = spr_u20C.ᜃ;
							num2 = 2;
							continue;
						}
						case 1:
							goto IL_A2;
						case 2:
							if (text != null)
							{
								num2 = 4;
								continue;
							}
							num--;
							num2 = 1;
							continue;
						case 3:
							goto IL_A2;
						case 4:
							return text;
						case 5:
							goto IL_C4;
						}
						break;
						IL_A2:
						num2 = 0;
					}
					break;
				}
				}
			}
		}
		return text;
		IL_C4:
		return string.Empty;
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x00245490 File Offset: 0x00244490
	public WhitespaceHandling ᜢ()
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
		return this.ᜥ;
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x002454D4 File Offset: 0x002444D4
	public void ᜀ(WhitespaceHandling A_0)
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
		this.ᜥ = A_0;
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x00245518 File Offset: 0x00244518
	public virtual int ᜥ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CF;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 3:
				goto IL_68;
			case 4:
				if (this.ᜎ.ᜀ == XmlNodeType.DocumentType)
				{
					num = 3;
					continue;
				}
				goto IL_E9;
			case 5:
				if (this.ᜎ.ᜀ != XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				goto IL_D1;
			case 6:
				if (this.ᜊ == State.AttrValue)
				{
					num = 7;
					continue;
				}
				num = 5;
				continue;
			case 7:
				goto IL_CF;
			}
			if (this.ᜊ == State.Attr)
			{
				goto IL_DD;
			}
			num = 0;
		}
		IL_68:
		goto IL_D1;
		IL_CF:
		goto IL_DD;
		IL_D1:
		return this.ᜎ.ᜀ();
		IL_DD:
		return this.ᜎ.ᜀ();
		IL_E9:
		if (true)
		{
		}
		return 0;
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x00245618 File Offset: 0x00244618
	public virtual string ᜌ(string A_0)
	{
		int num = 3;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				num2 = this.ᜎ.ᜀ(A_0);
				num = 2;
				continue;
			case 2:
				if (num2 >= 0)
				{
					goto IL_5F;
				}
				goto IL_B5;
			case 4:
				if (true)
				{
				}
				num = 5;
				continue;
			case 5:
				if (this.ᜊ == State.AttrValue)
				{
					goto IL_B5;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5F;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜊ != State.Attr)
			{
				num = 4;
				continue;
			}
			goto IL_B5;
			IL_5F:
			num = 0;
		}
		IL_67:
		return this.GetAttribute(num2);
		IL_B5:
		return null;
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x002456DC File Offset: 0x002446DC
	public virtual string ᜂ(string A_0, string A_1)
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
		return this.GetAttribute(A_0);
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x00245720 File Offset: 0x00244720
	public virtual string ᜂ(int A_0)
	{
		int a_ = 7;
		int num = 3;
		spr\u245C spr_u245C;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_76;
			case 2:
				if (this.ᜊ == State.AttrValue)
				{
					goto IL_BC;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 4:
				if (spr_u245C != null)
				{
					goto IL_6E;
				}
				goto IL_BC;
			case 5:
				spr_u245C = this.ᜎ.ᜀ(A_0);
				num = 4;
				continue;
			}
			if (this.ᜊ != State.Attr)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			goto IL_BC;
			IL_6E:
			num = 1;
		}
		IL_76:
		return spr_u245C.ᜁ();
		IL_BC:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ѭ", a_));
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x002457FC File Offset: 0x002447FC
	public virtual string ᜁ(int A_0)
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
		return this.GetAttribute(A_0);
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x00245840 File Offset: 0x00244840
	public virtual string ᜊ(string A_0)
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
		return this.GetAttribute(A_0);
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x00245884 File Offset: 0x00244884
	public virtual string ᜁ(string A_0, string A_1)
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
		return this.GetAttribute(A_0, A_1);
	}

	// Token: 0x06002408 RID: 9224 RVA: 0x002458C8 File Offset: 0x002448C8
	public virtual bool ᜈ(string A_0)
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			num = this.ᜎ.ᜀ(A_0);
			if (num >= 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_3A;
			}
		}
		this.MoveToAttribute(num);
		return true;
		IL_3A:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06002409 RID: 9225 RVA: 0x00245920 File Offset: 0x00244920
	public virtual bool ᜀ(string A_0, string A_1)
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
		return this.MoveToAttribute(A_0);
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x00245964 File Offset: 0x00244964
	public virtual void ᜀ(int A_0)
	{
		int a_ = 3;
		for (;;)
		{
			spr\u245C spr_u245C = this.ᜎ.ᜀ(A_0);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						this.ᜎ.ᜇ = this.ᜊ;
						num = 5;
						continue;
					}
					break;
				case 1:
					num = 4;
					continue;
				case 2:
					if (this.ᜊ != State.Attr)
					{
						goto IL_D8;
					}
					goto IL_E5;
				case 3:
					if (spr_u245C != null)
					{
						num = 6;
						continue;
					}
					goto IL_ED;
				case 4:
					if (this.ᜊ != State.AttrValue)
					{
						num = 0;
						continue;
					}
					goto IL_E5;
				case 5:
					goto IL_6B;
				case 6:
					this.ᜐ = A_0;
					this.ᜏ = spr_u245C;
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
				IL_D8:
				num = 1;
			}
		}
		IL_6B:
		IL_E5:
		this.ᜊ = State.Attr;
		return;
		IL_ED:
		throw new ArgumentOutOfRangeException(ClipboardData.b("h", a_));
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x00245A74 File Offset: 0x00244A74
	public virtual bool ᜠ()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜎ.ᜀ() > 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_37;
			}
		}
		this.MoveToAttribute(0);
		return true;
		IL_37:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x00245ACC File Offset: 0x00244ACC
	public virtual bool \u1717()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8F;
			case 1:
				if (this.ᜊ != State.AttrValue)
				{
					num = 0;
					continue;
				}
				goto IL_3B;
			case 2:
				if (this.ᜐ < this.ᜎ.ᜀ() - 1)
				{
					num = 4;
					continue;
				}
				return false;
			case 4:
				goto IL_60;
			case 5:
				num = 1;
				continue;
			}
			if (this.ᜊ != State.Attr)
			{
				num = 5;
				continue;
			}
			IL_3B:
			num = 2;
		}
		IL_60:
		this.MoveToAttribute(this.ᜐ + 1);
		return true;
		IL_8F:
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
			break;
		}
		return this.MoveToFirstAttribute();
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x00245BA0 File Offset: 0x00244BA0
	public virtual bool ᜱ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜊ == State.AttrValue)
				{
					num = 2;
					continue;
				}
				goto IL_99;
			case 2:
				goto IL_97;
			case 3:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_57;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (this.ᜊ == State.Attr)
			{
				break;
			}
			num = 3;
		}
		IL_57:
		this.ᜊ = this.ᜎ.ᜇ;
		this.ᜏ = null;
		return true;
		IL_97:
		goto IL_57;
		IL_99:
		return this.ᜎ.ᜀ == XmlNodeType.Element;
	}

	// Token: 0x0600240E RID: 9230 RVA: 0x00245C54 File Offset: 0x00244C54
	public bool ᜎ()
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
		return this.\u171A;
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x00245C98 File Offset: 0x00244C98
	public Encoding \u1737()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6A;
			case 1:
				if (true)
				{
				}
				this.ᜈ();
				goto IL_62;
			}
			if (this.ᜉ != null)
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
			IL_62:
			num = 0;
		}
		IL_6A:
		return this.ᜉ.ᜏ();
	}

	// Token: 0x06002410 RID: 9232 RVA: 0x00245D1C File Offset: 0x00244D1C
	private void ᜈ()
	{
		int a_ = 8;
		for (;;)
		{
			this.ᜀ(this.ᜑ);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜠ != null)
					{
						num = 11;
						continue;
					}
					goto IL_1DC;
				case 1:
					goto IL_9A;
				case 2:
					goto IL_1DA;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8D;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_75;
					}
					break;
				case 4:
					this.ᜑ = this.ᜉ.ᜁ();
					num = 3;
					continue;
				case 5:
					if (this.ᜮ() != null)
					{
						num = 10;
						continue;
					}
					num = 0;
					continue;
				case 6:
					if (this.ᜉ.ᜉ())
					{
						goto IL_8D;
					}
					return;
				case 7:
					goto IL_9A;
				case 8:
					this.ᜤ = ClipboardData.b("♭⑯㽱㡳", a_);
					this.ᜀ(this.ᜑ);
					num = 2;
					continue;
				case 9:
					num = 13;
					continue;
				case 10:
					this.ᜉ = new spr\u251B(ClipboardData.b("䵭ᑯᵱᝳ͵ᕷόቻ੽", a_), null, this.\u171C, this.\u171F);
					num = 1;
					continue;
				case 11:
					this.ᜉ = new spr\u251B(ClipboardData.b("䵭ᑯᵱᝳ͵ᕷόቻ੽", a_), null, this.ᜠ, this.\u171F);
					num = 7;
					continue;
				case 12:
					if (this.ᜉ.ᜁ() != null)
					{
						num = 4;
						continue;
					}
					goto IL_75;
				case 13:
					if (this.ᜈ == null)
					{
						num = 8;
						continue;
					}
					return;
				}
				break;
				IL_75:
				num = 6;
				continue;
				IL_8D:
				num = 9;
				continue;
				IL_9A:
				this.ᜉ.ᜀ(this.ᜎ());
				this.ᜉ.ᜀ(null, this.ᜑ);
				num = 12;
			}
		}
		IL_1DA:
		return;
		IL_1DC:
		throw new InvalidOperationException(ClipboardData.b("㝭Ὧݱ味᭵൷ॹࡻ幽꺍憐ﲑ몙鍊풟쪡솣풥袧\udca9얫쾭邯缾욳펵\udeb7骹펻첽证꫃뛅뷇뻉鿋뫍ꋏ럑뗓믕꫙껛뇝郟蟡難鋥臧迩鿫", a_));
	}

	// Token: 0x06002411 RID: 9233 RVA: 0x00245F5C File Offset: 0x00244F5C
	public virtual bool ᜫ()
	{
		int a_ = 7;
		int num = 30;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_3D4;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3D4;
				default:
					if (false)
					{
					}
					this.ᜉ.ᜃ();
					this.ᜉ = this.ᜉ.ᜑ();
					num = 37;
					continue;
				}
				break;
			case 2:
				num = 25;
				continue;
			case 3:
				this.ᜎ.ᜀ = XmlNodeType.Whitespace;
				num = 31;
				continue;
			case 4:
				goto IL_467;
			case 5:
				goto IL_586;
			case 6:
				if (this.\u1717 != null)
				{
					num = 47;
					continue;
				}
				num = 20;
				continue;
			case 7:
			{
				if (flag)
				{
					num = 34;
					continue;
				}
				State state = this.ᜊ;
				num = 27;
				continue;
			}
			case 8:
				goto IL_387;
			case 9:
				this.ᜊ = State.Eof;
				num = 40;
				continue;
			case 10:
				if (this.ᜎ.ᜄ)
				{
					num = 46;
					continue;
				}
				goto IL_488;
			case 11:
				if (flag)
				{
					num = 57;
					continue;
				}
				goto IL_77D;
			case 12:
				if (this.ᜎ())
				{
					num = 2;
					continue;
				}
				return true;
			case 13:
				goto IL_488;
			case 14:
				num = 60;
				continue;
			case 15:
				if (true)
				{
				}
				this.ᜈ();
				num = 67;
				continue;
			case 16:
				if (this.ᜎ.ᜈ)
				{
					num = 5;
					continue;
				}
				flag = false;
				num = 32;
				continue;
			case 17:
				if (this.ᜎ.ᜀ == XmlNodeType.Whitespace)
				{
					num = 41;
					continue;
				}
				goto IL_77D;
			case 18:
				if (string.Equals(this.ᜌ, this.ᜎ.ᜅ, StringComparison.OrdinalIgnoreCase))
				{
					num = 39;
					continue;
				}
				this.ᜊ();
				flag = true;
				num = 64;
				continue;
			case 19:
				goto IL_387;
			case 20:
				if (this.ᜎ.ᜀ == XmlNodeType.Document)
				{
					num = 9;
					continue;
				}
				goto IL_801;
			case 21:
				goto IL_801;
			case 22:
				goto IL_467;
			case 23:
				goto IL_387;
			case 24:
				this.ᜊ = State.Markup;
				num = 6;
				continue;
			case 25:
				if (this.NodeType == XmlNodeType.Element)
				{
					num = 58;
					continue;
				}
				goto IL_272;
			case 26:
				goto IL_272;
			case 27:
			{
				State state;
				switch (state)
				{
				case State.Initial:
					this.ᜊ = State.Markup;
					this.ᜉ.ᜂ();
					num = 38;
					continue;
				case State.Markup:
					goto IL_387;
				case State.EndTag:
					num = 18;
					continue;
				case State.Attr:
				case State.AttrValue:
					this.ᜊ = State.Markup;
					num = 19;
					continue;
				case State.Text:
					this.ᜊ();
					num = 8;
					continue;
				case State.PartialTag:
					this.ᜊ();
					this.ᜊ = State.Markup;
					flag = this.ᜁ(this.ᜋ);
					num = 22;
					continue;
				case State.AutoClose:
					this.ᜊ();
					num = 44;
					continue;
				case State.CData:
					flag = this.ᜀ();
					num = 54;
					continue;
				case State.PartialText:
					num = 48;
					continue;
				case State.PseudoStartTag:
					flag = this.ᜀ('<');
					num = 56;
					continue;
				case State.Eof:
					goto IL_35F;
				default:
					num = 35;
					continue;
				}
				break;
			}
			case 28:
				if (this.ᜥ == WhitespaceHandling.None)
				{
					num = 43;
					continue;
				}
				goto IL_77D;
			case 29:
				if (this.NodeType != XmlNodeType.Text)
				{
					num = 49;
					continue;
				}
				goto IL_1EE;
			case 31:
				goto IL_79B;
			case 32:
				goto IL_4D3;
			case 33:
				if (!this.\u1715)
				{
					num = 52;
					continue;
				}
				return true;
			case 34:
				num = 33;
				continue;
			case 35:
				num = 51;
				continue;
			case 36:
				num = 50;
				continue;
			case 37:
				goto IL_467;
			case 38:
				goto IL_387;
			case 39:
				this.ᜊ();
				this.ᜊ = State.Markup;
				num = 23;
				continue;
			case 40:
				goto IL_35F;
			case 41:
				num = 28;
				continue;
			case 42:
				goto IL_467;
			case 43:
				flag = false;
				num = 63;
				continue;
			case 44:
				if (this.\u170D.ᜀ() <= this.\u1718)
				{
					num = 24;
					continue;
				}
				goto IL_801;
			case 45:
				goto IL_467;
			case 46:
				this.ᜊ();
				num = 13;
				continue;
			case 47:
				this.ᜂ(this.\u1717);
				this.\u1717 = null;
				this.ᜊ = State.Markup;
				num = 21;
				continue;
			case 48:
				if (this.ᜀ(this.ᜉ.ᜎ(), false))
				{
					num = 3;
					continue;
				}
				goto IL_79B;
			case 49:
				num = 62;
				continue;
			case 50:
				if (this.\u170D.ᜀ() > 1)
				{
					num = 61;
					continue;
				}
				goto IL_4D3;
			case 51:
				goto IL_467;
			case 52:
				num = 59;
				continue;
			case 53:
				if (this.ᜉ.ᜑ() != null)
				{
					num = 1;
					continue;
				}
				return false;
			case 54:
				goto IL_467;
			case 55:
				num = 29;
				continue;
			case 56:
				goto IL_467;
			case 57:
				num = 17;
				continue;
			case 58:
				num = 65;
				continue;
			case 59:
				if (this.NodeType != XmlNodeType.Element)
				{
					num = 55;
					continue;
				}
				goto IL_1EE;
			case 60:
				if (this.ᜊ == State.Eof)
				{
					num = 36;
					continue;
				}
				goto IL_4D3;
			case 61:
				goto IL_400;
			case 62:
				if (this.NodeType == XmlNodeType.CDATA)
				{
					num = 0;
					continue;
				}
				return true;
			case 63:
				goto IL_77D;
			case 64:
				goto IL_467;
			case 65:
				if (!string.Equals(this.LocalName, ClipboardData.b("լ᭮ᱰὲ", a_), StringComparison.OrdinalIgnoreCase))
				{
					num = 26;
					continue;
				}
				return true;
			case 66:
				goto IL_2C7;
			case 67:
				goto IL_560;
			case 68:
				if (!flag)
				{
					num = 14;
					continue;
				}
				goto IL_4D3;
			}
			if (this.ᜉ == null)
			{
				num = 15;
				continue;
			}
			goto IL_560;
			IL_1EE:
			this.\u1715 = true;
			num = 12;
			continue;
			IL_3D4:
			goto IL_1EE;
			IL_272:
			this.ᜎ.ᜇ = this.ᜊ;
			spr\u20C0 spr_u20C = this.ᜀ(ClipboardData.b("լ᭮ᱰὲ", a_), XmlNodeType.Element, null);
			this.ᜋ();
			this.ᜎ = spr_u20C;
			spr_u20C.ᜈ = true;
			spr_u20C.ᜄ = false;
			this.ᜊ = State.Markup;
			num = 66;
			continue;
			IL_35F:
			num = 53;
			continue;
			IL_387:
			num = 10;
			continue;
			IL_467:
			num = 11;
			continue;
			IL_488:
			flag = this.ᜇ();
			num = 4;
			continue;
			IL_4D3:
			num = 7;
			continue;
			IL_560:
			num = 16;
			continue;
			IL_77D:
			num = 68;
			continue;
			IL_79B:
			flag = true;
			num = 45;
			continue;
			IL_801:
			flag = true;
			num = 42;
		}
		return false;
		IL_2C7:
		return true;
		IL_400:
		this.\u1718 = 1;
		this.ᜊ = State.AutoClose;
		this.ᜎ = this.ᜉ();
		return true;
		IL_586:
		this.ᜎ.ᜈ = false;
		this.ᜎ = this.ᜉ();
		this.ᜊ = this.ᜎ.ᜇ;
		return true;
	}

	// Token: 0x06002412 RID: 9234 RVA: 0x00246780 File Offset: 0x00245780
	private bool ᜇ()
	{
		char c;
		for (;;)
		{
			IL_34:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_148:
				if (this.ᜎ.ᜆ == null)
				{
					goto IL_73;
				}
				num = 8;
				break;
			default:
				if (false)
				{
				}
				c = this.ᜉ.ᜎ();
				num = 10;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					if (this.ᜀ(c, true))
					{
						num = 6;
						continue;
					}
					return true;
				case 2:
					goto IL_105;
				case 3:
					goto IL_138;
				case 4:
					if (this.ᜎ.ᜆ.ᜀ().ᜀ() == DeclaredContent.CDATA)
					{
						num = 3;
						continue;
					}
					goto IL_73;
				case 5:
					if (true)
					{
					}
					num = 7;
					continue;
				case 6:
					this.ᜎ.ᜀ = XmlNodeType.Whitespace;
					num = 2;
					continue;
				case 7:
					goto IL_148;
				case 8:
					num = 4;
					continue;
				case 9:
					if (c != '￿')
					{
						num = 5;
						continue;
					}
					goto IL_165;
				case 10:
					if (c == '<')
					{
						num = 0;
						continue;
					}
					num = 9;
					continue;
				}
				goto IL_34;
			}
			IL_73:
			num = 1;
			goto IL_02;
		}
		IL_71:
		c = this.ᜉ.ᜂ();
		return this.ᜁ(c);
		IL_105:
		return true;
		IL_138:
		this.ᜋ = '\0';
		this.ᜊ = State.CData;
		return false;
		IL_165:
		this.ᜊ = State.Eof;
		return false;
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x002468FC File Offset: 0x002458FC
	private bool ᜁ(char A_0)
	{
		int a_ = 2;
		int num = 22;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 != '_')
				{
					num = 21;
					continue;
				}
				goto IL_2EC;
			case 1:
				goto IL_16F;
			case 2:
				this.ᜂ();
				num = 8;
				continue;
			case 3:
				if (!char.IsLetter(A_0))
				{
					num = 13;
					continue;
				}
				goto IL_2EC;
			case 4:
				if (A_0 == '-')
				{
					num = 18;
					continue;
				}
				num = 15;
				continue;
			case 5:
				goto IL_2C6;
			case 6:
				A_0 = this.ᜉ.ᜂ();
				num = 4;
				continue;
			case 7:
				if (A_0 == '/')
				{
					num = 1;
					continue;
				}
				goto IL_3C3;
			case 8:
				if (this.GetAttribute(ClipboardData.b("㭧㍩㽫㩭㕯㽱", a_)) == null)
				{
					num = 16;
					continue;
				}
				goto IL_2C6;
			case 9:
				goto IL_110;
			case 10:
				if (this.ᜧ)
				{
					num = 17;
					continue;
				}
				goto IL_142;
			case 11:
				if (A_0 == '!')
				{
					num = 6;
					continue;
				}
				num = 14;
				continue;
			case 12:
				if (this.GetAttribute(ClipboardData.b("㡧㽩⹫≭㥯ㅱ", a_)) != null)
				{
					num = 19;
					continue;
				}
				goto IL_2C6;
			case 13:
				goto IL_1ED;
			case 14:
				if (A_0 == '?')
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			case 15:
				if (A_0 == '[')
				{
					num = 24;
					continue;
				}
				num = 0;
				continue;
			case 16:
				num = 12;
				continue;
			case 17:
				return false;
			case 18:
				goto IL_13D;
			case 19:
				this.ᜎ.ᜀ(ClipboardData.b("㭧㍩㽫㩭㕯㽱", a_), "", '"', this.ᜦ == CaseFolding.None);
				num = 5;
				continue;
			case 20:
				if (true)
				{
				}
				if (string.Equals(text, ClipboardData.b("Ⱨ╩⽫㩭⥯≱ㅳ", a_), StringComparison.OrdinalIgnoreCase))
				{
					num = 2;
					continue;
				}
				goto IL_27B;
			case 21:
				goto IL_1CC;
			case 23:
				goto IL_8D;
			case 24:
				goto IL_F0;
			}
			if (A_0 == '%')
			{
				num = 23;
				continue;
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
				num = 11;
				continue;
			}
			IL_1CC:
			num = 3;
			continue;
			IL_2C6:
			num = 10;
			continue;
			IL_2EC:
			text = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("䡧捩慫摭乯乱", a_), false);
			num = 20;
		}
		IL_8D:
		return this.ᜅ();
		IL_F0:
		return this.ᜃ();
		IL_110:
		this.ᜉ.ᜂ();
		return this.ᜁ();
		IL_13D:
		return this.ᜄ();
		IL_142:
		this.ᜎ.ᜀ = XmlNodeType.DocumentType;
		return true;
		IL_16F:
		return this.ᜆ();
		IL_1ED:
		string str = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("㩧ཀྵཫŭٯ᝱ٳήᙷᵹ", a_), ClipboardData.b("噧", a_));
		this.ᜀ(ClipboardData.b("Ⅷ൩ɫŭɯ᭱ᩳᅵ塷፹ቻࡽꢇﲍﮏ뚕뾗ꚙ붛", a_) + str + ClipboardData.b("噧", a_), new string[0]);
		return false;
		IL_27B:
		this.ᜀ(ClipboardData.b("Ⅷѩᩫ཭ᱯ᭱ၳ噵ᱷόύች꺍랏꺑떓ꢗ늛낝躟薡誣蚥袧풫\udead햯톱삳\udfb5횷\uddb9鲻馽ﲿ胃觅诇黉闋黍闏맕뛗뛙ꗛ", a_), new string[]
		{
			text
		});
		this.ᜉ.ᜀ(null, ClipboardData.b("㩧ཀྵཫŭٯ᝱ٳήᙷᵹ", a_), ClipboardData.b("噧", a_));
		return false;
		IL_3C3:
		return this.ᜀ(A_0);
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x00246CD4 File Offset: 0x00245CD4
	private string ᜃ(string A_0)
	{
		if (true)
		{
		}
		string text;
		for (;;)
		{
			text = this.ᜉ.ᜀ(this.\u1712, A_0, false);
			CaseFolding caseFolding = this.ᜦ;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return text;
				case 1:
					return text;
				case 2:
					num = 1;
					continue;
				case 3:
					switch (caseFolding)
					{
					case CaseFolding.ToUpper:
						text = text.ToUpperInvariant();
						num = 0;
						continue;
					case CaseFolding.ToLower:
						text = text.ToLowerInvariant();
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
					default:
						num = 2;
						continue;
					}
					break;
				case 4:
					return text;
				}
				break;
			}
		}
		return text;
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x00246DA0 File Offset: 0x00245DA0
	private static bool ᜂ(string A_0)
	{
		bool result;
		try
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
			XmlConvert.VerifyName(A_0);
			result = true;
		}
		catch (XmlException)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x00246DFC File Offset: 0x00245DFC
	private bool ᜀ(char A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			spr\u20C0 spr_u20C;
			for (;;)
			{
				string text = null;
				int num = 28;
				for (;;)
				{
					string text2;
					char a_3;
					switch (num)
					{
					case 0:
						this.ᜉ.ᜂ();
						A_0 = this.ᜉ.ᜌ();
						if (true)
						{
						}
						num = 36;
						continue;
					case 1:
						goto IL_4B9;
					case 2:
						goto IL_2ED;
					case 3:
						num = 52;
						continue;
					case 4:
					{
						string a_2 = ClipboardData.b("䭪摬扮筰䵲", a_);
						text2 = this.ᜉ.ᜀ(this.\u1712, a_2, false);
						num = 11;
						continue;
					}
					case 5:
						num = 38;
						continue;
					case 6:
					{
						string text3;
						if (sprὂ.ᜀ(text3))
						{
							num = 24;
							continue;
						}
						goto IL_2ED;
					}
					case 7:
					{
						if (A_0 == '<')
						{
							num = 26;
							continue;
						}
						string text3 = this.ᜃ(ClipboardData.b("䭪摬扮筰乲剴啶噸䕺", a_));
						A_0 = this.ᜉ.ᜌ();
						num = 23;
						continue;
					}
					case 8:
						if (ClipboardData.b("䭪摬扮筰乲婴䥶䕸", a_).IndexOf(A_0) >= 0)
						{
							num = 1;
							continue;
						}
						text = this.ᜃ(ClipboardData.b("䭪摬扮筰乲婴䥶䕸", a_));
						num = 42;
						continue;
					case 9:
						if (A_0 != '=')
						{
							num = 30;
							continue;
						}
						goto IL_76F;
					case 10:
						if (A_0 != '"')
						{
							num = 13;
							continue;
						}
						goto IL_76F;
					case 11:
						goto IL_1F7;
					case 12:
						goto IL_26A;
					case 13:
						num = 34;
						continue;
					case 14:
						goto IL_1F7;
					case 15:
						goto IL_26A;
					case 16:
						goto IL_45E;
					case 17:
						goto IL_2ED;
					case 18:
					{
						string text3;
						if (!string.Equals(text3, ClipboardData.b("偪", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 25;
							continue;
						}
						goto IL_5D1;
					}
					case 19:
						goto IL_7F3;
					case 20:
						num = 39;
						continue;
					case 21:
						goto IL_5D1;
					case 22:
						goto IL_3ED;
					case 23:
					{
						string text3;
						if (!string.Equals(text3, ClipboardData.b("䝪", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 20;
							continue;
						}
						goto IL_5D1;
					}
					case 24:
						num = 56;
						continue;
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_265;
						default:
							if (false)
							{
							}
							text2 = null;
							a_3 = '\0';
							num = 9;
							continue;
						}
						break;
					case 26:
						this.ᜀ(ClipboardData.b("㡪ᥬ๮Ͱݲ啴Ͷᡸᱺ嵼塾婢뎂ꂆꦈﺌ꾎ﲐ朗煮뾞蚠鶢芤", a_), new string[]
						{
							text
						});
						num = 15;
						continue;
					case 27:
						goto IL_6EB;
					case 28:
						if (this.ᜊ != State.PseudoStartTag)
						{
							num = 33;
							continue;
						}
						this.ᜊ = State.Markup;
						num = 27;
						continue;
					case 29:
						num = 18;
						continue;
					case 30:
						num = 10;
						continue;
					case 31:
						num = 48;
						continue;
					case 32:
						goto IL_5D1;
					case 33:
						num = 8;
						continue;
					case 34:
						if (A_0 == '\'')
						{
							num = 53;
							continue;
						}
						goto IL_1F7;
					case 35:
						goto IL_76A;
					case 36:
						goto IL_4FE;
					case 37:
						this.ᜉ.ᜀ(ClipboardData.b("㹪ͬ੮॰Ͳၴᑶ൸Ṻ᥼彾쒀첂쎄Ꞇ麗ﾌﲎﶒ랖ﲜ햠莢톤욦캨讪説풮膰캲銴", a_), text);
						num = 16;
						continue;
					case 38:
						if (A_0 == '"')
						{
							num = 22;
							continue;
						}
						num = 57;
						continue;
					case 39:
					{
						string text3;
						if (!string.Equals(text3, ClipboardData.b("噪", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 51;
							continue;
						}
						goto IL_5D1;
					}
					case 40:
						if (A_0 == '>')
						{
							num = 49;
							continue;
						}
						goto IL_45E;
					case 41:
						if (A_0 == '=')
						{
							num = 0;
							continue;
						}
						goto IL_4FE;
					case 42:
						goto IL_6EB;
					case 43:
						spr_u20C.ᜄ = true;
						A_0 = this.ᜉ.ᜂ();
						num = 58;
						continue;
					case 44:
						if (this.Depth == 1)
						{
							num = 31;
							continue;
						}
						goto IL_7F8;
					case 45:
						if (A_0 != '￿')
						{
							num = 3;
							continue;
						}
						goto IL_26A;
					case 46:
					{
						string text3;
						if (!string.Equals(text3, ClipboardData.b("兪", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 29;
							continue;
						}
						goto IL_5D1;
					}
					case 47:
						goto IL_688;
					case 48:
						if (this.\u1719 == 1)
						{
							num = 35;
							continue;
						}
						this.\u1719++;
						num = 47;
						continue;
					case 49:
						this.ᜉ.ᜂ();
						num = 61;
						continue;
					case 50:
					{
						string text3;
						this.ᜀ(ClipboardData.b("⽪ᡬὮᵰᩲᙴᙶ൸Ṻ嵼Ṿﺊ歷놐뒒Ꞗ벚붜욠춢쪤햦첨쾪", a_), new string[]
						{
							text3
						});
						num = 2;
						continue;
					}
					case 51:
						goto IL_265;
					case 52:
						if (A_0 == '>')
						{
							num = 12;
							continue;
						}
						num = 60;
						continue;
					case 53:
						goto IL_76F;
					case 54:
						if (A_0 != '\'')
						{
							num = 5;
							continue;
						}
						goto IL_3ED;
					case 55:
						if (A_0 == '￿')
						{
							num = 37;
							continue;
						}
						num = 40;
						continue;
					case 56:
					{
						spr\u20C0 spr_u20C2 = spr_u20C;
						string text3;
						string a_4 = text3;
						string a_5;
						if ((a_5 = text2) == null)
						{
							a_5 = text3;
						}
						spr\u245C spr_u245C = spr_u20C2.ᜀ(a_4, a_5, a_3, this.ᜦ == CaseFolding.None);
						num = 59;
						continue;
					}
					case 57:
						if (A_0 != '>')
						{
							num = 4;
							continue;
						}
						goto IL_1F7;
					case 58:
						if (A_0 != '>')
						{
							num = 19;
							continue;
						}
						goto IL_26A;
					case 59:
					{
						spr\u245C spr_u245C;
						if (spr_u245C == null)
						{
							num = 50;
							continue;
						}
						sprὂ.ᜀ(spr_u20C, spr_u245C);
						num = 17;
						continue;
					}
					case 60:
						if (A_0 == '/')
						{
							num = 43;
							continue;
						}
						num = 7;
						continue;
					case 61:
						goto IL_45E;
					}
					break;
					IL_1F7:
					num = 6;
					continue;
					IL_265:
					num = 46;
					continue;
					IL_26A:
					num = 55;
					continue;
					IL_2ED:
					A_0 = this.ᜉ.ᜌ();
					num = 21;
					continue;
					IL_3ED:
					a_3 = A_0;
					text2 = this.ᜁ(this.\u1712, A_0);
					num = 14;
					continue;
					IL_45E:
					num = 44;
					continue;
					IL_4FE:
					num = 54;
					continue;
					IL_5D1:
					num = 45;
					continue;
					IL_6EB:
					spr_u20C = this.ᜀ(text, XmlNodeType.Element, null);
					spr_u20C.ᜄ = false;
					this.ᜁ(spr_u20C);
					A_0 = this.ᜉ.ᜌ();
					num = 32;
					continue;
					IL_76F:
					num = 41;
				}
			}
			IL_4B9:
			this.\u1712.Length = 0;
			this.\u1712.Append('<');
			this.ᜊ = State.PartialText;
			return false;
			IL_688:
			goto IL_7F8;
			IL_76A:
			this.ᜊ = State.Eof;
			return false;
			IL_7F3:
			this.ᜀ(ClipboardData.b("⹪ᕬὮᑰၲŴቶᵸ孺᡼ቾﲄꞆ愈ﾊﶎ뎒ﺘ뮚몜낞龠蒢薤풦첨\udaaa\ud8ac쪮\udfb0킲킴鞶킸햺캼쮾꓀ꋂꇄꛈ귊꫐꣔", a_), A_0);
			this.ᜉ.ᜀ(null, ClipboardData.b("㥪࡬౮Ṱղၴնၸᕺ᩼", a_), ClipboardData.b("啪", a_));
			return false;
			IL_7F8:
			this.ᜀ(spr_u20C);
			return true;
		}
		}
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x0024760C File Offset: 0x0024660C
	private bool ᜆ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			string text;
			spr\u20C0 spr_u20C;
			for (;;)
			{
				this.ᜊ = State.EndTag;
				this.ᜉ.ᜂ();
				text = this.ᜃ(ClipboardData.b("卲籴究獸䙺剼䅾부", a_));
				char c = this.ᜉ.ᜌ();
				int num = 1;
				for (;;)
				{
					int num2;
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_1C3;
					case 1:
						if (c != '>')
						{
							num = 4;
							continue;
						}
						goto IL_C7;
					case 2:
						goto IL_C2;
					case 3:
						if (num2 <= 0)
						{
							num = 2;
							continue;
						}
						spr_u20C = (spr\u20C0)this.\u170D.ᜂ(num2);
						num = 8;
						continue;
					case 4:
						if (true)
						{
						}
						this.ᜀ(ClipboardData.b("㙲൴ݶᱸ᡺ॼ᩾ꎂ麗ﾊ꾎뮚ﺞ욠莢芤袦鞨貪趬\udcae풰슲살튶ힸ\ud8ba\ud8bc龾ꣀ귂뛄돆곈꫊꧌뻐뗒ꋘꃜ", a_), c);
						this.ᜉ.ᜀ(null, ClipboardData.b("ⅲၴᑶᙸൺ᡼ൾ", a_), ClipboardData.b("䵲", a_));
						num = 5;
						continue;
					case 5:
						goto IL_C7;
					case 6:
						goto IL_A6;
					case 7:
						goto IL_A6;
					case 8:
						if (string.Equals(spr_u20C.ᜅ, text, flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F9;
						default:
							if (false)
							{
							}
							num2--;
							num = 7;
							continue;
						}
						break;
					}
					break;
					IL_A6:
					num = 3;
					continue;
					IL_C7:
					this.ᜉ.ᜂ();
					this.ᜌ = text;
					flag = (this.ᜦ == CaseFolding.None);
					this.ᜎ = (spr\u20C0)this.\u170D.ᜂ(this.\u170D.ᜀ() - 1);
					num2 = this.\u170D.ᜀ() - 1;
					num = 6;
				}
			}
			IL_C2:
			goto IL_1F9;
			IL_1C3:
			this.ᜌ = spr_u20C.ᜅ;
			return true;
			IL_1F9:
			this.ᜀ(ClipboardData.b("㵲ᩴ坶ᑸ᩺ॼ᱾ꦈ歷떔ﲚ붜咽캠톢薤肦閨蒪횬龮첰趲銴", a_), new string[]
			{
				text
			});
			this.ᜊ = State.Markup;
			return false;
		}
		}
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x00247840 File Offset: 0x00246840
	private bool ᜅ()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string a_2 = ClipboardData.b("噩䥫", a_) + this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("⭩Ὣṭ㹯᝱s", a_), ClipboardData.b("佩剫", a_)) + ClipboardData.b("佩剫", a_);
		this.ᜀ(null, XmlNodeType.CDATA, a_2);
		return true;
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x002478E0 File Offset: 0x002468E0
	private bool ᜄ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			char c;
			string text;
			for (;;)
			{
				c = this.ᜉ.ᜂ();
				int num = 15;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_1E6;
					case 1:
						text += ClipboardData.b("卲", a_);
						num = 19;
						continue;
					case 2:
						goto IL_A2;
					case 3:
						goto IL_277;
					case 4:
						num = 20;
						continue;
					case 5:
						num = 16;
						continue;
					case 6:
						goto IL_A2;
					case 7:
						goto IL_9D;
					case 8:
						goto IL_277;
					case 9:
						goto IL_192;
					case 10:
						goto IL_C8;
					case 11:
						if (num2 > 0)
						{
							num = 10;
							continue;
						}
						text = ClipboardData.b("干", a_) + text.Substring(num3);
						num = 6;
						continue;
					case 12:
						if (text.Length > 0)
						{
							num = 5;
							continue;
						}
						goto IL_324;
					case 13:
						if (num2 < 0)
						{
							num = 14;
							continue;
						}
						num3 = num2 + 2;
						if (true)
						{
						}
						num = 3;
						continue;
					case 14:
						num = 12;
						continue;
					case 15:
						if (c != '-')
						{
							num = 7;
							continue;
						}
						text = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("ひᩴ᩶ᑸṺ፼୾", a_), ClipboardData.b("干塴䥶", a_));
						num2 = text.IndexOf(ClipboardData.b("干塴", a_));
						num = 9;
						continue;
					case 16:
						if (text[text.Length - 1] == '-')
						{
							num = 1;
							continue;
						}
						goto IL_324;
					case 17:
						if (num3 >= text.Length)
						{
							goto IL_1E6;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C8;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 18:
						goto IL_192;
					case 19:
						goto IL_1E1;
					case 20:
						if (text[num3] != '-')
						{
							num = 0;
							continue;
						}
						num3++;
						num = 8;
						continue;
					}
					break;
					IL_A2:
					num2 = text.IndexOf(ClipboardData.b("干塴", a_));
					num = 18;
					continue;
					IL_C8:
					text = text.Substring(0, num2 - 1) + ClipboardData.b("干", a_) + text.Substring(num3);
					num = 2;
					continue;
					IL_192:
					num = 13;
					continue;
					IL_1E6:
					num = 11;
					continue;
					IL_277:
					num = 17;
				}
			}
			IL_9D:
			this.ᜀ(ClipboardData.b("㙲൴ݶᱸ᡺ॼᙾꖄﾐ떔낖ꖘ몚난늞蚠莢잤튦\udda8讪쮬삮쒰\uddb2톴鞶슸论삼", a_), c);
			this.ᜉ.ᜀ(null, ClipboardData.b("ひᩴ᩶ᑸṺ፼୾", a_), ClipboardData.b("䵲", a_));
			return false;
			IL_1E1:
			IL_324:
			this.ᜀ(null, XmlNodeType.Comment, text);
			return true;
		}
		}
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x00247C1C File Offset: 0x00246C1C
	private bool ᜃ()
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			char c;
			string text;
			for (;;)
			{
				c = this.ᜉ.ᜂ();
				c = this.ᜉ.ᜌ();
				text = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("敫捭穯⥱⥳䩵䙷", a_), false);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (c != '[')
						{
							goto IL_CE;
						}
						goto IL_1F0;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CE;
						default:
							goto IL_148;
						}
						break;
					case 2:
						if (text.StartsWith(ClipboardData.b("ի࡭偯", a_)))
						{
							num = 1;
							continue;
						}
						num = 5;
						continue;
					case 3:
						goto IL_DA;
					case 4:
						goto IL_1EB;
					case 5:
						if (!string.Equals(text, ClipboardData.b("⽫⩭ㅯ♱㕳", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 4;
							continue;
						}
						c = this.ᜉ.ᜌ();
						num = 0;
						continue;
					}
					break;
					IL_CE:
					num = 3;
				}
			}
			IL_DA:
			if (true)
			{
			}
			this.ᜀ(ClipboardData.b("⥫᙭o᝱ᝳɵᅷᑹ᭻幽ꝿ\ud981ꎃꚅﾉ꺍﶑ﲗ몙뮛邟\udfa1莣", a_), c);
			this.ᜉ.ᜀ(null, ClipboardData.b("⽫⩭ㅯ♱㕳", a_), ClipboardData.b("剫", a_));
			return false;
			IL_148:
			if (false)
			{
			}
			this.ᜉ.ᜀ(null, ClipboardData.b("⽫⩭ㅯ♱㕳", a_), ClipboardData.b("剫", a_));
			return false;
			IL_1EB:
			this.ᜀ(ClipboardData.b("⥫᙭o᝱ᝳɵᅷᑹ᭻幽썿욁얃튅즇ꪉﮍ늑秊뺝螟\ud9a1钣\udba5辧", a_), new string[]
			{
				text
			});
			this.ᜉ.ᜀ(null, ClipboardData.b("⽫⩭ㅯ♱㕳", a_), ClipboardData.b("剫", a_));
			return false;
			IL_1F0:
			string a_2 = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("⽫⩭ㅯ♱㕳", a_), ClipboardData.b("ㅫ㍭乯", a_));
			this.ᜀ(null, XmlNodeType.CDATA, a_2);
			return true;
		}
		}
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x00247E54 File Offset: 0x00246E54
	private void ᜂ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				char c = this.ᜉ.ᜌ();
				string text = this.ᜃ(ClipboardData.b("味罵畷灹䉻", a_));
				this.ᜀ(text, XmlNodeType.DocumentType, null);
				c = this.ᜉ.ᜌ();
				int num = 8;
				for (;;)
				{
					string text2;
					string text3;
					string a_2;
					string a_3;
					switch (num)
					{
					case 0:
						if (!string.Equals(text2, ClipboardData.b("❳⽵⭷⹹㥻㍽", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 27;
							continue;
						}
						goto IL_46A;
					case 1:
						num = 10;
						continue;
					case 2:
						text2 = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("味罵畷灹䉻", a_), false);
						num = 25;
						continue;
					case 3:
						goto IL_46A;
					case 4:
						goto IL_346;
					case 5:
						if (this.ᜈ != null)
						{
							num = 23;
							continue;
						}
						goto IL_30C;
					case 6:
						c = this.ᜉ.ᜌ();
						num = 12;
						continue;
					case 7:
						goto IL_307;
					case 8:
						if (c != '>')
						{
							num = 26;
							continue;
						}
						goto IL_531;
					case 9:
						goto IL_4E7;
					case 10:
						if (c == '\'')
						{
							num = 14;
							continue;
						}
						goto IL_150;
					case 11:
						goto IL_296;
					case 12:
						if (c != '"')
						{
							num = 24;
							continue;
						}
						goto IL_296;
					case 13:
						goto IL_46A;
					case 14:
						goto IL_247;
					case 15:
						if (c != '[')
						{
							num = 2;
							continue;
						}
						goto IL_4E7;
					case 16:
						goto IL_192;
					case 17:
						goto IL_150;
					case 18:
						if (c == '\'')
						{
							num = 11;
							continue;
						}
						goto IL_46A;
					case 19:
						if (c != '>')
						{
							num = 21;
							continue;
						}
						goto IL_192;
					case 20:
						if (c == '[')
						{
							num = 29;
							continue;
						}
						goto IL_122;
					case 21:
						this.ᜀ(ClipboardData.b("ㅳ๵ࡷόύ੽ꚅ꺍ﾏ뒓튕힗\ud999좛잝蒣튥즧충肫躭튯잱삳隵\udeb7햹즻킽꒿뷅량", a_), c);
						this.ᜉ.ᜀ(null, ClipboardData.b("び㥵㭷⹹╻⹽앿", a_), ClipboardData.b("䩳", a_));
						num = 16;
						continue;
					case 22:
						goto IL_122;
					case 23:
						num = 30;
						continue;
					case 24:
						goto IL_16D;
					case 25:
						if (string.Equals(text2, ClipboardData.b("⑳⍵㩷㙹㕻㵽", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					case 26:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16D;
						default:
							if (false)
							{
							}
							text3 = "";
							a_2 = "";
							a_3 = "";
							num = 15;
							continue;
						}
						break;
					case 27:
						this.ᜀ(ClipboardData.b("ⅳᡵᵷɹ౻᭽ꢇﺉﲑ뒓ﾕ몙\ud89b톝ﶣ誩讫햭肯쾱鎳", a_), new string[]
						{
							text2
						});
						this.ᜉ.ᜀ(null, ClipboardData.b("び㥵㭷⹹╻⹽앿", a_), ClipboardData.b("䩳", a_));
						num = 3;
						continue;
					case 28:
						if (c != '"')
						{
							num = 1;
							continue;
						}
						goto IL_247;
					case 29:
						text3 = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("㵳ᡵ౷ό๻ၽꒃ햅ﶇﾋ", a_), ClipboardData.b("⥳", a_));
						this.ᜎ.ᜁ = text3;
						num = 22;
						continue;
					case 30:
						if (!string.Equals(this.ᜈ.ᜌ(), text, StringComparison.OrdinalIgnoreCase))
						{
							num = 7;
							continue;
						}
						goto IL_30C;
					}
					break;
					IL_122:
					c = this.ᜉ.ᜌ();
					num = 19;
					continue;
					IL_150:
					c = this.ᜉ.ᜌ();
					num = 9;
					continue;
					IL_16D:
					num = 18;
					continue;
					IL_192:
					num = 5;
					continue;
					IL_247:
					text2 = ClipboardData.b("❳⽵⭷⹹㥻㍽", a_);
					a_3 = this.ᜉ.ᜀ(this.\u1712, c);
					this.ᜎ.ᜀ(text2, a_3, c, this.ᜦ == CaseFolding.None);
					num = 17;
					continue;
					IL_296:
					if (true)
					{
					}
					a_2 = this.ᜉ.ᜀ(this.\u1712, c);
					this.ᜎ.ᜀ(text2, a_2, c, this.ᜦ == CaseFolding.None);
					num = 13;
					continue;
					IL_30C:
					this.ᜤ = text;
					this.ᜢ = a_2;
					this.ᜡ = a_3;
					this.ᜣ = text3;
					this.ᜀ(this.ᜉ.ᜁ());
					num = 4;
					continue;
					IL_46A:
					c = this.ᜉ.ᜌ();
					num = 28;
					continue;
					IL_4E7:
					num = 20;
				}
			}
			IL_307:
			throw new InvalidOperationException(ClipboardData.b("び≵㱷婹᡻ᅽꒃﺉ겋ﺕ뢗ﺙﶝ햟쾡솣좥\udca7誩\ud8ab힭삯ힱ", a_));
			IL_346:
			IL_531:
			this.ᜉ.ᜂ();
			return;
		}
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x002483A0 File Offset: 0x002473A0
	private bool ᜁ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			string text;
			string text2;
			for (;;)
			{
				if (true)
				{
				}
				text = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("䝦恨晪杬偮", a_), false);
				text2 = null;
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_D6;
					case 1:
						if (this.ᜉ.ᜎ() != '?')
						{
							num = 6;
							continue;
						}
						text2 = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("㝦᭨Ѫ๬੮ɰrᱴ᥶Ṹ孺㑼ᅾﾊﾐ", a_), ClipboardData.b("奦", a_));
						num = 4;
						continue;
					case 2:
						goto IL_D4;
					case 3:
						if (num2 > 0)
						{
							num = 5;
							continue;
						}
						goto IL_A2;
					case 4:
						goto IL_D6;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							text = text.Substring(num2 + 1);
							num = 7;
							continue;
						}
						break;
					case 6:
						text2 = this.ᜉ.ᜀ(this.\u1712, ClipboardData.b("㝦᭨Ѫ๬੮ɰrᱴ᥶Ṹ孺㑼ᅾﾊﾐ", a_), ClipboardData.b("奦", a_));
						text2 = text2.TrimEnd(new char[]
						{
							'/'
						});
						num = 0;
						continue;
					case 7:
						goto IL_A2;
					case 8:
						if (!string.Equals(text, ClipboardData.b("ὦѨݪ", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 2;
							continue;
						}
						return false;
					}
					break;
					IL_A2:
					num = 8;
					continue;
					IL_D6:
					num2 = text.IndexOf(':');
					num = 3;
				}
			}
			IL_D4:
			this.ᜀ(text, XmlNodeType.ProcessingInstruction, text2);
			return true;
		}
		}
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x00248580 File Offset: 0x00247580
	private bool ᜀ(char A_0, bool A_1)
	{
		int num = 19;
		bool result;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				if (A_0 != '?')
				{
					num = 13;
					continue;
				}
				goto IL_30A;
			case 1:
				flag = true;
				goto IL_278;
			case 2:
				flag = this.ᜉ.ᜀ();
				goto IL_278;
			case 3:
				this.\u1712.Length = 0;
				num = 27;
				continue;
			case 4:
				goto IL_323;
			case 5:
				num = 8;
				continue;
			case 6:
				goto IL_30A;
			case 7:
				if (A_0 == '&')
				{
					num = 20;
					continue;
				}
				num = 17;
				continue;
			case 8:
				if (A_0 != '!')
				{
					num = 21;
					continue;
				}
				goto IL_30A;
			case 9:
				goto IL_20E;
			case 10:
				if (A_0 != '/')
				{
					num = 5;
					continue;
				}
				goto IL_30A;
			case 11:
				if (A_0 == '<')
				{
					num = 28;
					continue;
				}
				num = 7;
				continue;
			case 12:
				goto IL_203;
			case 13:
				num = 16;
				continue;
			case 14:
				num = 2;
				continue;
			case 15:
				if (A_1)
				{
					num = 3;
					continue;
				}
				goto IL_1C4;
			case 16:
				if (char.IsLetter(A_0))
				{
					num = 6;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20E;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.\u1712.Append('<');
					this.\u1712.Append(A_0);
					result = false;
					A_0 = this.ᜉ.ᜂ();
					num = 18;
					continue;
				}
				break;
			case 17:
				if (!this.ᜉ.ᜀ())
				{
					num = 23;
					continue;
				}
				goto IL_17A;
			case 18:
				goto IL_203;
			case 20:
				this.ᜀ(this.\u1712, '<');
				result = false;
				A_0 = this.ᜉ.ᜎ();
				num = 24;
				continue;
			case 21:
				num = 0;
				continue;
			case 22:
				goto IL_17A;
			case 23:
				result = false;
				num = 22;
				continue;
			case 24:
				goto IL_203;
			case 25:
				goto IL_224;
			case 26:
				goto IL_203;
			case 27:
				goto IL_1C4;
			case 28:
				A_0 = this.ᜉ.ᜂ();
				num = 10;
				continue;
			}
			if (A_1)
			{
				num = 14;
				continue;
			}
			num = 1;
			continue;
			IL_17A:
			this.\u1712.Append(A_0);
			A_0 = this.ᜉ.ᜂ();
			num = 12;
			continue;
			IL_1C4:
			this.ᜊ = State.Text;
			num = 26;
			continue;
			IL_203:
			num = 9;
			continue;
			IL_20E:
			if (A_0 == '￿')
			{
				num = 25;
				continue;
			}
			num = 11;
			continue;
			IL_278:
			result = flag;
			num = 15;
			continue;
			IL_30A:
			this.ᜊ = State.PartialTag;
			this.ᜋ = A_0;
			num = 4;
		}
		IL_224:
		IL_323:
		string a_ = this.\u1712.ToString();
		this.ᜀ(null, XmlNodeType.Text, a_);
		return result;
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x002488CC File Offset: 0x002478CC
	private string ᜁ(StringBuilder A_0, char A_1)
	{
		for (;;)
		{
			A_0.Length = 0;
			char c = this.ᜉ.ᜂ();
			int num = 10;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_102;
				case 1:
					this.ᜉ.ᜂ();
					num = 3;
					continue;
				case 2:
					if (c != A_1)
					{
						num = 6;
						continue;
					}
					goto IL_102;
				case 3:
					goto IL_E0;
				case 4:
					if (c != A_1)
					{
						goto IL_17E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E0;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 5:
					goto IL_13A;
				case 6:
					num = 13;
					continue;
				case 7:
					goto IL_13A;
				case 8:
					if (c == '&')
					{
						num = 9;
						continue;
					}
					A_0.Append(c);
					c = this.ᜉ.ᜂ();
					num = 5;
					continue;
				case 9:
					this.ᜀ(A_0, A_1);
					c = this.ᜉ.ᜎ();
					num = 7;
					continue;
				case 10:
					goto IL_13A;
				case 11:
					num = 2;
					continue;
				case 12:
					if (c != '￿')
					{
						num = 11;
						continue;
					}
					goto IL_102;
				case 13:
					if (c == '>')
					{
						num = 0;
						continue;
					}
					num = 8;
					continue;
				}
				break;
				IL_102:
				num = 4;
				continue;
				IL_13A:
				num = 12;
			}
		}
		IL_E0:
		IL_17E:
		return A_0.ToString();
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x00248A60 File Offset: 0x00247A60
	private bool ᜀ()
	{
		int a_ = 8;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = this.ᜉ.ᜀ();
				this.\u1712.Length = 0;
				char c = this.ᜉ.ᜎ();
				int num = 43;
				for (;;)
				{
					string value;
					switch (num)
					{
					case 0:
						if (c == '-')
						{
							num = 20;
							continue;
						}
						this.\u1712.Append('<');
						this.\u1712.Append('!');
						this.\u1712.Append(c);
						flag = false;
						num = 26;
						continue;
					case 1:
						num = 34;
						continue;
					case 2:
						goto IL_3BB;
					case 3:
						goto IL_3B6;
					case 4:
						if (c == '￿')
						{
							num = 19;
							continue;
						}
						num = 25;
						continue;
					case 5:
						goto IL_5BA;
					case 6:
						goto IL_52A;
					case 7:
						goto IL_59A;
					case 8:
						flag = false;
						num = 9;
						continue;
					case 9:
						goto IL_204;
					case 10:
						c = this.ᜉ.ᜂ();
						num = 39;
						continue;
					case 11:
					{
						char c2;
						if (c2 != '?')
						{
							num = 1;
							continue;
						}
						goto IL_389;
					}
					case 12:
						if (this.ᜋ == '\0')
						{
							num = 28;
							continue;
						}
						return true;
					case 13:
						this.ᜉ.ᜂ();
						num = 30;
						continue;
					case 14:
						value = this.\u1712.ToString();
						num = 42;
						continue;
					case 15:
						goto IL_2D5;
					case 16:
						goto IL_319;
					case 17:
						if (!this.ᜉ.ᜀ())
						{
							num = 41;
							continue;
						}
						goto IL_204;
					case 18:
						goto IL_59A;
					case 19:
						goto IL_59A;
					case 20:
						num = 21;
						continue;
					case 21:
						if (flag)
						{
							num = 3;
							continue;
						}
						this.ᜋ = '!';
						num = 7;
						continue;
					case 22:
						if (c == '?')
						{
							num = 13;
							continue;
						}
						num = 31;
						continue;
					case 23:
						goto IL_1B2;
					case 24:
						if (!flag)
						{
							num = 29;
							continue;
						}
						return true;
					case 25:
						if (c == '<')
						{
							num = 10;
							continue;
						}
						num = 17;
						continue;
					case 26:
						goto IL_3BB;
					case 27:
						if (string.IsNullOrEmpty(value))
						{
							num = 6;
							continue;
						}
						this.ᜋ = '/';
						this.\u1712.Length = 0;
						this.\u1712.Append(value);
						this.ᜊ = State.CData;
						num = 36;
						continue;
					case 28:
						this.ᜋ = ' ';
						num = 16;
						continue;
					case 29:
						num = 27;
						continue;
					case 30:
						if (flag)
						{
							num = 32;
							continue;
						}
						this.ᜋ = '?';
						num = 18;
						continue;
					case 31:
						if (c == '/')
						{
							num = 14;
							continue;
						}
						this.\u1712.Append('<');
						this.\u1712.Append(c);
						flag = false;
						num = 2;
						continue;
					case 32:
						goto IL_43E;
					case 33:
						num = 23;
						continue;
					case 34:
						goto IL_2D5;
					case 35:
						c = this.ᜉ.ᜂ();
						num = 0;
						continue;
					case 36:
						goto IL_59A;
					case 37:
					{
						char c2;
						if (c2 != '/')
						{
							num = 44;
							continue;
						}
						goto IL_380;
					}
					case 38:
						num = 24;
						continue;
					case 39:
						if (c == '!')
						{
							num = 35;
							continue;
						}
						num = 22;
						continue;
					case 40:
					{
						this.ᜊ();
						char c2 = this.ᜋ;
						num = 48;
						continue;
					}
					case 41:
						num = 47;
						continue;
					case 42:
						if (this.ᜆ())
						{
							num = 33;
							continue;
						}
						goto IL_13B;
					case 43:
						if (this.ᜋ != '\0')
						{
							num = 40;
							continue;
						}
						goto IL_2D5;
					case 44:
						num = 11;
						continue;
					case 45:
						goto IL_2D5;
					case 46:
						num = 37;
						continue;
					case 47:
						if (!flag)
						{
							goto IL_204;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B2;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 48:
					{
						char c2;
						switch (c2)
						{
						case ' ':
							goto IL_2D5;
						case '!':
							goto IL_222;
						default:
							num = 46;
							continue;
						}
						break;
					}
					case 49:
					{
						if (c == '￿')
						{
							num = 5;
							continue;
						}
						string text = this.\u1712.ToString();
						text = text.Replace(ClipboardData.b("剭兯⥱㝳㉵㥷⹹㵻╽", a_), string.Empty);
						text = text.Replace(ClipboardData.b("㍭ⵯ䱱", a_), string.Empty);
						text = text.Replace(ClipboardData.b("䅭婯塱孳", a_), string.Empty);
						this.ᜀ(null, XmlNodeType.CDATA, text);
						num = 12;
						continue;
					}
					case 50:
						goto IL_3BB;
					}
					break;
					IL_13B:
					this.\u1712.Length = 0;
					this.\u1712.Append(value);
					this.\u1712.Append(ClipboardData.b("剭彯", a_) + this.ᜌ + ClipboardData.b("偭", a_));
					flag = false;
					c = this.ᜉ.ᜎ();
					num = 45;
					continue;
					IL_1B2:
					if (string.Equals(this.ᜌ, this.ᜎ.ᜅ, StringComparison.OrdinalIgnoreCase))
					{
						num = 38;
						continue;
					}
					goto IL_13B;
					IL_204:
					this.\u1712.Append(c);
					num = 50;
					continue;
					IL_2D5:
					if (true)
					{
					}
					num = 4;
					continue;
					IL_3BB:
					c = this.ᜉ.ᜂ();
					num = 15;
					continue;
					IL_59A:
					num = 49;
				}
			}
			IL_222:
			this.ᜋ = ' ';
			return this.ᜄ();
			IL_319:
			return true;
			IL_380:
			this.ᜊ = State.EndTag;
			return true;
			IL_389:
			this.ᜋ = ' ';
			return this.ᜁ();
			IL_3B6:
			this.ᜋ = ' ';
			return this.ᜄ();
			IL_43E:
			this.ᜋ = ' ';
			return this.ᜁ();
			IL_52A:
			return true;
			IL_5BA:
			this.ᜊ = State.Eof;
			return false;
		}
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x00249160 File Offset: 0x00248160
	private void ᜀ(StringBuilder A_0, char A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			char c;
			string text;
			spr\u251B spr_u251B;
			for (;;)
			{
				c = this.ᜉ.ᜂ();
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 76;
						continue;
					case 1:
						if (c != '&')
						{
							num = 13;
							continue;
						}
						return;
					case 2:
						if (c != '￿')
						{
							num = 79;
							continue;
						}
						return;
					case 3:
						num = 28;
						continue;
					case 4:
						if (!string.IsNullOrEmpty(text))
						{
							num = 39;
							continue;
						}
						goto IL_75C;
					case 5:
						num = 23;
						continue;
					case 6:
						c = this.ᜉ.ᜂ();
						num = 82;
						continue;
					case 7:
						if (c != A_1)
						{
							num = 59;
							continue;
						}
						return;
					case 8:
						if (c == '#')
						{
							num = 54;
							continue;
						}
						if (true)
						{
						}
						this.\u1713.Length = 0;
						num = 61;
						continue;
					case 9:
						goto IL_75C;
					case 10:
						if (c != A_1)
						{
							num = 75;
							continue;
						}
						return;
					case 11:
						num = 65;
						continue;
					case 12:
						if (c != A_1)
						{
							num = 52;
							continue;
						}
						return;
					case 13:
						num = 58;
						continue;
					case 14:
						num = 42;
						continue;
					case 15:
						if (c != '&')
						{
							num = 44;
							continue;
						}
						return;
					case 16:
						num = 33;
						continue;
					case 17:
						return;
					case 18:
						goto IL_27E;
					case 19:
						if (c != A_1)
						{
							num = 20;
							continue;
						}
						return;
					case 20:
						num = 57;
						continue;
					case 21:
						if (spr_u251B != null)
						{
							num = 56;
							continue;
						}
						this.ᜀ(ClipboardData.b("㵧ѩ࡫୭ᙯ᭱ᩳ፵ᱷ婹᥻ၽﾅꢇ궉뺍떑", a_), new string[]
						{
							text
						});
						num = 9;
						continue;
					case 22:
						goto IL_9E2;
					case 23:
						if (!char.IsDigit(c))
						{
							num = 24;
							continue;
						}
						goto IL_732;
					case 24:
						goto IL_901;
					case 25:
						if (c != A_1)
						{
							num = 63;
							continue;
						}
						return;
					case 26:
						num = 51;
						continue;
					case 27:
						if (c != '￿')
						{
							num = 6;
							continue;
						}
						return;
					case 28:
						if (c != '￿')
						{
							num = 84;
							continue;
						}
						return;
					case 29:
					{
						string a;
						if (!(a == ClipboardData.b("ᥧὩͫᩭ", a_)))
						{
							num = 30;
							continue;
						}
						A_0.Append(ClipboardData.b("䩧", a_));
						num = 43;
						continue;
					}
					case 30:
						num = 72;
						continue;
					case 31:
						if (this.\u1713.Length > 0)
						{
							num = 5;
							continue;
						}
						goto IL_901;
					case 32:
						num = 4;
						continue;
					case 33:
						if (c != '-')
						{
							num = 18;
							continue;
						}
						goto IL_732;
					case 34:
						c = this.ᜉ.ᜂ();
						num = 17;
						continue;
					case 35:
						num = 29;
						continue;
					case 36:
					{
						string a;
						if (!(a == ClipboardData.b("ѧṩ", a_)))
						{
							num = 74;
							continue;
						}
						A_0.Append(ClipboardData.b("呧", a_));
						num = 12;
						continue;
					}
					case 37:
						A_0.Append(spr_u251B.\u1713());
						num = 25;
						continue;
					case 38:
						num = 47;
						continue;
					case 39:
						spr_u251B = this.ᜈ.ᜄ(text);
						num = 21;
						continue;
					case 40:
						num = 68;
						continue;
					case 41:
						if (c != '&')
						{
							num = 71;
							continue;
						}
						return;
					case 42:
						if (c != '￿')
						{
							num = 83;
							continue;
						}
						return;
					case 43:
						if (c != A_1)
						{
							num = 81;
							continue;
						}
						return;
					case 44:
						num = 2;
						continue;
					case 45:
						return;
					case 46:
						num = 36;
						continue;
					case 47:
					{
						string a;
						if (!(a == ClipboardData.b("१ݩᱫ", a_)))
						{
							num = 46;
							continue;
						}
						A_0.Append(ClipboardData.b("乧", a_));
						num = 7;
						continue;
					}
					case 48:
					{
						string a;
						if ((a = text) != null)
						{
							num = 38;
							continue;
						}
						goto IL_8B3;
					}
					case 49:
						num = 64;
						continue;
					case 50:
						goto IL_99A;
					case 51:
						if (c != '￿')
						{
							num = 62;
							continue;
						}
						return;
					case 52:
						num = 1;
						continue;
					case 53:
					{
						string a;
						if (!(a == ClipboardData.b("ཧṩ", a_)))
						{
							num = 35;
							continue;
						}
						A_0.Append(ClipboardData.b("噧", a_));
						num = 19;
						continue;
					}
					case 54:
						goto IL_1AB;
					case 55:
						if (this.ᜈ != null)
						{
							num = 32;
							continue;
						}
						goto IL_75C;
					case 56:
						num = 70;
						continue;
					case 57:
						if (c != '&')
						{
							num = 26;
							continue;
						}
						return;
					case 58:
						if (c != '￿')
						{
							num = 34;
							continue;
						}
						return;
					case 59:
						num = 41;
						continue;
					case 60:
						num = 67;
						continue;
					case 61:
						goto IL_99A;
					case 62:
						c = this.ᜉ.ᜂ();
						num = 86;
						continue;
					case 63:
						num = 69;
						continue;
					case 64:
						if (c != '￿')
						{
							num = 77;
							continue;
						}
						return;
					case 65:
						if (c != '&')
						{
							num = 49;
							continue;
						}
						return;
					case 66:
						goto IL_1C8;
					case 67:
						if (!char.IsLetter(c))
						{
							num = 0;
							continue;
						}
						goto IL_732;
					case 68:
						goto IL_8B3;
					case 69:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_933;
						default:
							if (false)
							{
							}
							if (c != '&')
							{
								num = 3;
								continue;
							}
							return;
						}
						break;
					case 70:
						if (spr_u251B.ᜄ())
						{
							num = 37;
							continue;
						}
						goto IL_6C5;
					case 71:
						num = 27;
						continue;
					case 72:
					{
						string a;
						if (!(a == ClipboardData.b("१ᩩͫᵭ", a_)))
						{
							num = 40;
							continue;
						}
						A_0.Append(ClipboardData.b("佧", a_));
						num = 10;
						continue;
					}
					case 73:
						if (c != '￿')
						{
							num = 60;
							continue;
						}
						goto IL_27E;
					case 74:
						num = 53;
						continue;
					case 75:
						num = 15;
						continue;
					case 76:
						if (c != '_')
						{
							num = 16;
							continue;
						}
						goto IL_732;
					case 77:
						A_0.Append(c);
						c = this.ᜉ.ᜂ();
						num = 22;
						continue;
					case 78:
						if (c != A_1)
						{
							num = 11;
							continue;
						}
						return;
					case 79:
						c = this.ᜉ.ᜂ();
						num = 66;
						continue;
					case 80:
						if (c != '&')
						{
							num = 14;
							continue;
						}
						return;
					case 81:
						num = 80;
						continue;
					case 82:
						return;
					case 83:
						goto IL_933;
					case 84:
						c = this.ᜉ.ᜂ();
						num = 45;
						continue;
					case 85:
						goto IL_94B;
					case 86:
						goto IL_633;
					}
					break;
					IL_27E:
					num = 31;
					continue;
					IL_732:
					this.\u1713.Append(c);
					c = this.ᜉ.ᜂ();
					num = 50;
					continue;
					IL_75C:
					A_0.Append(ClipboardData.b("乧", a_));
					A_0.Append(text);
					num = 78;
					continue;
					IL_8B3:
					num = 55;
					continue;
					IL_901:
					text = this.\u1713.ToString();
					num = 48;
					continue;
					IL_933:
					c = this.ᜉ.ᜂ();
					num = 85;
					continue;
					IL_99A:
					num = 73;
				}
			}
			IL_1AB:
			string value = this.ᜉ.ᜈ();
			A_0.Append(value);
			c = this.ᜉ.ᜎ();
			return;
			IL_1C8:
			return;
			IL_633:
			return;
			IL_6C5:
			spr\u251B spr_u251B2 = new spr\u251B(text, spr_u251B.ᜋ(), spr_u251B.ᜆ(), this.ᜉ.ᜊ());
			spr_u251B.ᜀ(this.ᜉ, new Uri(spr_u251B.ᜆ()));
			this.ᜉ = spr_u251B2;
			this.ᜉ.ᜂ();
			return;
			IL_94B:
			return;
			IL_9E2:
			return;
		}
		}
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x00249B8C File Offset: 0x00248B8C
	public virtual bool \u1736()
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
		return this.ᜊ == State.Eof;
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x00249BD4 File Offset: 0x00248BD4
	public virtual void ᜦ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_94:
			num = 4;
			break;
		default:
			if (false)
			{
			}
			num = 5;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_84;
			case 1:
				goto IL_82;
			case 2:
				this.ᜉ.ᜃ();
				this.ᜉ = null;
				num = 0;
				continue;
			case 3:
				goto IL_8C;
			case 4:
				this.\u1714.Close();
				this.\u1714 = null;
				num = 1;
				continue;
			}
			if (this.ᜉ != null)
			{
				num = 2;
				continue;
			}
			IL_84:
			num = 3;
		}
		IL_82:
		return;
		IL_8C:
		if (this.\u1714 != null)
		{
			goto IL_94;
		}
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x00249CA0 File Offset: 0x00248CA0
	public virtual ReadState \u171D()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				goto IL_3A;
			case 2:
				if (this.ᜊ != State.Eof)
				{
					return ReadState.Interactive;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return ReadState.Initial;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (this.ᜊ == State.Initial)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_3A:
		return ReadState.Initial;
		IL_74:
		if (true)
		{
		}
		return ReadState.EndOfFile;
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x00249D30 File Offset: 0x00248D30
	public virtual string ᜑ()
	{
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_120;
			case 2:
				goto IL_E3;
			case 3:
				goto IL_137;
			case 4:
			{
				XmlNodeType nodeType;
				switch (nodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					goto IL_6B;
				default:
					num = 5;
					continue;
				}
				break;
			}
			case 5:
				goto IL_13C;
			case 6:
			{
				XmlNodeType nodeType;
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					goto IL_6B;
				default:
					num = 0;
					continue;
				}
				break;
			}
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13C;
				default:
				{
					if (false)
					{
					}
					if (!this.Read())
					{
						num = 1;
						continue;
					}
					XmlNodeType nodeType = this.NodeType;
					num = 4;
					continue;
				}
				}
				break;
			case 8:
				goto IL_E3;
			case 10:
				this.\u1712.Length = 0;
				num = 2;
				continue;
			}
			if (true)
			{
			}
			if (this.ᜎ.ᜀ == XmlNodeType.Element)
			{
				num = 10;
				continue;
			}
			goto IL_165;
			IL_6B:
			this.\u1712.Append(this.ᜎ.ᜁ);
			num = 8;
			continue;
			IL_E3:
			num = 7;
			continue;
			IL_13C:
			num = 6;
		}
		IL_120:
		return this.\u1712.ToString();
		IL_137:
		return this.\u1712.ToString();
		IL_165:
		return this.ᜎ.ᜁ;
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x00249EB0 File Offset: 0x00248EB0
	public virtual string ᜧ()
	{
		StringWriter stringWriter;
		XmlTextWriter xmlTextWriter;
		for (;;)
		{
			stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			XmlNodeType nodeType = this.NodeType;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (nodeType)
					{
					case XmlNodeType.Element:
						this.Read();
						num = 6;
						continue;
					case XmlNodeType.Attribute:
						stringWriter.Write(this.Value);
						num = 7;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				case 1:
					goto IL_111;
				case 2:
					if (!this.EOF)
					{
						num = 3;
						continue;
					}
					goto IL_111;
				case 3:
					num = 4;
					continue;
				case 4:
					if (this.NodeType == XmlNodeType.EndElement)
					{
						num = 1;
						continue;
					}
					xmlTextWriter.WriteNode(this, true);
					if (true)
					{
					}
					num = 8;
					continue;
				case 5:
					num = 9;
					continue;
				case 6:
					goto IL_9B;
				case 7:
					goto IL_E6;
				case 8:
					goto IL_9B;
				case 9:
					goto IL_10F;
				case 10:
					goto IL_12D;
				}
				break;
				IL_9B:
				num = 2;
				continue;
				IL_111:
				this.Read();
				num = 10;
			}
		}
		IL_E6:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_12D:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		IL_10F:
		xmlTextWriter.Close();
		return stringWriter.ToString();
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x0024A018 File Offset: 0x00249018
	public virtual string \u1716()
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
		StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
		XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlTextWriter.WriteNode(this, true);
		xmlTextWriter.Close();
		return stringWriter.ToString();
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x0024A080 File Offset: 0x00249080
	public virtual XmlNameTable \u171E()
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
		return null;
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x0024A0BC File Offset: 0x002490BC
	public virtual string ᜇ(string A_0)
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
		return null;
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x0024A0F8 File Offset: 0x002490F8
	public virtual void ᜡ()
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new InvalidOperationException(ClipboardData.b("ⅮṰݲ啴ᡶ᝸孺ᱼᅾꆀﾊ꾎ﺚﲞ쒠趢", a_));
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x0024A150 File Offset: 0x00249150
	public virtual bool \u171C()
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_44;
			case 1:
				return false;
			case 3:
				if (this.ᜊ != State.AttrValue)
				{
					goto IL_90;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_87;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜊ == State.Attr)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_44:
		IL_87:
		this.ᜊ = State.AttrValue;
		return true;
		IL_90:
		throw new InvalidOperationException(ClipboardData.b("㵲ᩴͶ奸ᑺ፼彾ꖄﶈﾊﾌ래", a_));
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x0024A200 File Offset: 0x00249200
	public static XmlDocument ᜀ(TextReader A_0)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		sprὂ sprὂ = new sprὂ();
		sprὂ.ᜉ(ClipboardData.b("╬㭮㱰㽲", a_));
		sprὂ.ᜀ(CaseFolding.ToLower);
		sprὂ.ᜁ(A_0);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.PreserveWhitespace = true;
		xmlDocument.XmlResolver = null;
		xmlDocument.Load(sprὂ);
		sprὂ.ᜀ(xmlDocument);
		return xmlDocument;
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x0024A290 File Offset: 0x00249290
	public static XmlDocument ᜁ(string A_0)
	{
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDocument result;
			try
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				try
				{
					streamWriter.Write(A_0);
					streamWriter.Flush();
					memoryStream.Position = 0L;
					TextReader textReader = new StreamReader(memoryStream);
					try
					{
						result = sprὂ.ᜀ(textReader);
					}
					finally
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)textReader).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_84;
							}
							if (textReader == null)
							{
								break;
							}
							num = 0;
						}
						IL_84:;
					}
				}
				finally
				{
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							((IDisposable)streamWriter).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_CA;
						}
						if (streamWriter == null)
						{
							break;
						}
						num = 1;
					}
					IL_CA:;
				}
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_124;
					case 2:
						((IDisposable)memoryStream).Dispose();
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
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 2;
				}
				IL_124:;
			}
			return result;
		}
		}
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x0024A3F8 File Offset: 0x002493F8
	public static void ᜀ(ref string A_0)
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
		XmlDocument xmlDocument = sprὂ.ᜁ(A_0);
		A_0 = xmlDocument.InnerXml;
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x0024A444 File Offset: 0x00249444
	private static void ᜀ(XmlNode A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				List<XmlNode> list = new List<XmlNode>();
				IEnumerator enumerator = A_0.ChildNodes.GetEnumerator();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3DA;
					case 1:
						try
						{
							num = 13;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									XmlNode previousSibling;
									XmlNode xmlNode;
									previousSibling.LastChild.AppendChild(xmlNode.Clone());
									list.Add(xmlNode);
									num = 14;
									continue;
								}
								case 1:
								{
									XmlNode previousSibling;
									if (previousSibling.Name == ClipboardData.b("Ὢ६", a_))
									{
										num = 4;
										continue;
									}
									break;
								}
								case 2:
								{
									XmlNode previousSibling;
									if (!(previousSibling.Name != ClipboardData.b("Ὢ६", a_)))
									{
										num = 6;
										continue;
									}
									previousSibling = previousSibling.PreviousSibling;
									num = 11;
									continue;
								}
								case 3:
								{
									XmlNode previousSibling;
									if (previousSibling != null)
									{
										num = 8;
										continue;
									}
									goto IL_341;
								}
								case 4:
									num = 7;
									continue;
								case 6:
									goto IL_341;
								case 7:
								{
									XmlNode previousSibling;
									if (previousSibling.LastChild.Name == ClipboardData.b("Ὢ౬൮ᵰᙲ", a_))
									{
										num = 0;
										continue;
									}
									break;
								}
								case 8:
									num = 2;
									continue;
								case 9:
									goto IL_1DD;
								case 10:
								{
									XmlNode xmlNode;
									if (xmlNode.Name == ClipboardData.b("ὪὬ", a_))
									{
										num = 23;
										continue;
									}
									goto IL_189;
								}
								case 11:
									goto IL_1DD;
								case 12:
								{
									XmlNode xmlNode;
									XmlNode parentNode = xmlNode.ParentNode;
									XmlNode previousSibling = xmlNode.PreviousSibling;
									num = 9;
									continue;
								}
								case 15:
								{
									XmlNode xmlNode;
									sprὂ.ᜀ(xmlNode);
									num = 5;
									continue;
								}
								case 16:
									goto IL_370;
								case 17:
								{
									XmlNode xmlNode;
									if (xmlNode.ParentNode.Name == ClipboardData.b("ὪὬ", a_))
									{
										num = 12;
										continue;
									}
									goto IL_189;
								}
								case 18:
								{
									XmlNode xmlNode;
									if (xmlNode.ChildNodes.Count > 0)
									{
										num = 15;
										continue;
									}
									break;
								}
								case 19:
									num = 16;
									continue;
								case 20:
								{
									if (!enumerator.MoveNext())
									{
										num = 19;
										continue;
									}
									XmlNode xmlNode = (XmlNode)enumerator.Current;
									num = 10;
									continue;
								}
								case 21:
								{
									XmlNode previousSibling;
									if (previousSibling != null)
									{
										num = 22;
										continue;
									}
									break;
								}
								case 22:
									num = 1;
									continue;
								case 23:
									num = 17;
									continue;
								}
								goto IL_11C;
								IL_189:
								num = 18;
								continue;
								IL_1B4:
								num = 20;
								continue;
								IL_11C:
								goto IL_1B4;
								IL_1DD:
								if (true)
								{
								}
								num = 3;
								continue;
								IL_341:
								num = 21;
							}
							IL_370:
							goto IL_84;
						}
						finally
						{
							for (;;)
							{
								IL_38A:
								IDisposable disposable = enumerator as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 1;
											continue;
										}
										goto IL_3D9;
									case 1:
										disposable.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_3BB;
									}
									goto IL_38A;
								}
								IL_3BB:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_3D1;
								}
							}
							IL_3D1:
							if (false)
							{
							}
							IL_3D9:;
						}
						goto IL_3DA;
						IL_84:
						num = 6;
						continue;
					case 2:
						goto IL_3DA;
					case 3:
					{
						int num2 = 0;
						int count = list.Count;
						num = 0;
						continue;
					}
					case 4:
						return;
					case 5:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						A_0.RemoveChild(list[num2]);
						num2++;
						num = 2;
						continue;
					}
					case 6:
						if (list.Count > 0)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
					IL_3DA:
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x0024A884 File Offset: 0x00249884
	private void ᜁ(spr\u20C0 A_0)
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
				{
					if (false)
					{
					}
					spr\u1D66 spr_u1D;
					A_0.ᜆ = spr_u1D;
					num = 5;
					continue;
				}
				}
				break;
			case 3:
			{
				spr\u1D66 spr_u1D;
				if (spr_u1D != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			case 4:
				A_0.ᜄ = true;
				num = 1;
				continue;
			case 5:
			{
				spr\u1D66 spr_u1D;
				if (spr_u1D.ᜀ().ᜀ() == DeclaredContent.EMPTY)
				{
					num = 4;
					continue;
				}
				return;
			}
			case 6:
			{
				spr\u1D66 spr_u1D = this.ᜈ.ᜃ(A_0.ᜅ);
				num = 3;
				continue;
			}
			}
			if (this.ᜈ == null)
			{
				break;
			}
			num = 6;
		}
	}

	// Token: 0x06002430 RID: 9264 RVA: 0x0024A96C File Offset: 0x0024996C
	private static void ᜀ(spr\u20C0 A_0, spr\u245C A_1)
	{
		for (;;)
		{
			spr\u1D66 spr_u1D = A_0.ᜆ;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᜏ sprᜏ = spr_u1D.ᜀ(A_1.ᜀ);
					num = 3;
					continue;
				}
				case 1:
					if (spr_u1D != null)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
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
						sprᜏ sprᜏ;
						if (sprᜏ == null)
						{
							return;
						}
						break;
					}
					}
					num = 4;
					continue;
				case 4:
				{
					sprᜏ sprᜏ;
					A_1.ᜁ = sprᜏ;
					num = 2;
					continue;
				}
				}
				break;
			}
		}
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x0024AA18 File Offset: 0x00249A18
	private static bool ᜀ(string A_0)
	{
		bool result;
		try
		{
			for (;;)
			{
				XmlConvert.VerifyNMTOKEN(A_0);
				int num = A_0.IndexOf(':');
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_88;
					case 1:
						if (num >= 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_7B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3E;
						default:
							if (false)
							{
							}
							goto IL_7B;
						}
						break;
					case 3:
						goto IL_3E;
					}
					break;
					IL_3E:
					if (true)
					{
					}
					XmlConvert.VerifyNCName(A_0.Substring(num + 1));
					num2 = 2;
					continue;
					IL_7B:
					result = true;
					num2 = 0;
				}
			}
			IL_88:;
		}
		catch (XmlException)
		{
			result = false;
		}
		catch (ArgumentNullException)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x0024AAE0 File Offset: 0x00249AE0
	private void ᜀ(spr\u20C0 A_0)
	{
		int a_ = 4;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 16;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						spr\u1D66 spr_u1D;
						if (!string.Equals(spr_u1D.ᜃ(), ClipboardData.b("⡩⍫⩭⥯", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 26;
							continue;
						}
						goto IL_FF;
					}
					case 1:
						if (!sprὂ.ᜂ(A_0.ᜅ))
						{
							num = 27;
							continue;
						}
						goto IL_1B4;
					case 2:
					{
						int num3;
						num2 = num3;
						num = 24;
						continue;
					}
					case 3:
						if (A_0.ᜆ != null)
						{
							num = 2;
							continue;
						}
						goto IL_FF;
					case 4:
					{
						spr\u1D66 spr_u1D;
						if (!string.Equals(spr_u1D.ᜃ(), this.ᜈ.ᜌ(), StringComparison.OrdinalIgnoreCase))
						{
							num = 5;
							continue;
						}
						goto IL_FF;
					}
					case 5:
						num = 11;
						continue;
					case 6:
						goto IL_3CE;
					case 7:
						num = 12;
						continue;
					case 8:
					{
						string text = A_0.ᜅ.ToUpperInvariant();
						num2 = 0;
						int num3 = this.\u170D.ᜀ() - 2;
						num = 3;
						continue;
					}
					case 9:
					{
						int num3;
						if (num2 == num3 - 1)
						{
							num = 14;
							continue;
						}
						goto IL_2A3;
					}
					case 10:
						goto IL_FF;
					case 11:
					{
						spr\u1D66 spr_u1D;
						string text;
						if (!spr_u1D.ᜀ(text, this.ᜈ))
						{
							num = 19;
							continue;
						}
						goto IL_FF;
					}
					case 12:
						if (num2 == 2)
						{
							num = 17;
							continue;
						}
						goto IL_220;
					case 13:
					{
						spr\u20C0 spr_u20C;
						if (!spr_u20C.ᜄ)
						{
							num = 30;
							continue;
						}
						goto IL_3CE;
					}
					case 14:
					{
						string text;
						spr\u20C0 spr_u20C2;
						string.Equals(text, spr_u20C2.ᜅ, StringComparison.OrdinalIgnoreCase);
						num = 31;
						continue;
					}
					case 15:
					{
						spr\u1D66 spr_u1D;
						if (spr_u1D != null)
						{
							num = 7;
							continue;
						}
						goto IL_FF;
					}
					case 17:
						num = 0;
						continue;
					case 18:
						goto IL_1DC;
					case 19:
						num = 21;
						continue;
					case 20:
					{
						if (num2 <= 0)
						{
							num = 10;
							continue;
						}
						spr\u20C0 spr_u20C = (spr\u20C0)this.\u170D.ᜂ(num2);
						num = 13;
						continue;
					}
					case 21:
					{
						spr\u1D66 spr_u1D;
						if (spr_u1D.ᜁ())
						{
							num = 6;
							continue;
						}
						goto IL_FF;
					}
					case 22:
						num = 1;
						continue;
					case 23:
						if (this.ᜈ != null)
						{
							num = 8;
							continue;
						}
						return;
					case 24:
						goto IL_1DC;
					case 25:
					{
						int num3;
						spr\u20C0 spr_u20C2 = (spr\u20C0)this.\u170D.ᜂ(num3);
						num = 9;
						continue;
					}
					case 26:
						goto IL_220;
					case 27:
						goto IL_31D;
					case 28:
						goto IL_2CC;
					case 29:
					{
						int num3;
						if (num2 < num3)
						{
							num = 25;
							continue;
						}
						return;
					}
					case 30:
					{
						spr\u20C0 spr_u20C;
						spr\u1D66 spr_u1D = spr_u20C.ᜆ;
						num = 15;
						continue;
					}
					case 31:
						goto IL_2A3;
					case 32:
						if (num2 == 0)
						{
							num = 33;
							continue;
						}
						num = 29;
						continue;
					case 33:
						goto IL_11D;
					}
					if (true)
					{
					}
					if (A_0.ᜀ == XmlNodeType.Element)
					{
						num = 22;
						continue;
					}
					goto IL_1B4;
					IL_FF:
					num = 32;
					continue;
					IL_1B4:
					num = 23;
					continue;
					IL_1DC:
					num = 20;
					continue;
					IL_220:
					num = 4;
					continue;
					IL_2A3:
					this.ᜊ = State.AutoClose;
					this.\u1717 = A_0;
					this.ᜊ();
					this.\u1718 = num2 + 1;
					num = 28;
					continue;
					IL_3CE:
					num2--;
					num = 18;
				}
				IL_11D:
				break;
				IL_2CC:
				return;
				IL_31D:
				this.ᜊ();
				this.ᜀ(null, XmlNodeType.Text, ClipboardData.b("噩", a_) + A_0.ᜅ + ClipboardData.b("呩", a_));
				return;
			}
			}
			break;
		}
	}

	// Token: 0x04002175 RID: 8565
	public const string ᜀ = "#unknown";

	// Token: 0x04002176 RID: 8566
	private const string ᜁ = " \t\r\n><";

	// Token: 0x04002177 RID: 8567
	private const string ᜂ = " \t\r\n=/><";

	// Token: 0x04002178 RID: 8568
	private const string ᜃ = " \t\r\n='\"/>";

	// Token: 0x04002179 RID: 8569
	private const string ᜄ = " \t\r\n>";

	// Token: 0x0400217A RID: 8570
	private const string ᜅ = "\t\r\n[]<>";

	// Token: 0x0400217B RID: 8571
	private const string ᜆ = " \t\r\n>";

	// Token: 0x0400217C RID: 8572
	private const string ᜇ = " \t\r\n?";

	// Token: 0x0400217D RID: 8573
	private spr\u2057 ᜈ;

	// Token: 0x0400217E RID: 8574
	private spr\u251B ᜉ;

	// Token: 0x0400217F RID: 8575
	private State ᜊ;

	// Token: 0x04002180 RID: 8576
	private char ᜋ;

	// Token: 0x04002181 RID: 8577
	private string ᜌ;

	// Token: 0x04002182 RID: 8578
	private sprᢐ \u170D;

	// Token: 0x04002183 RID: 8579
	private spr\u20C0 ᜎ;

	// Token: 0x04002184 RID: 8580
	private spr\u245C ᜏ;

	// Token: 0x04002185 RID: 8581
	private int ᜐ;

	// Token: 0x04002186 RID: 8582
	private Uri ᜑ;

	// Token: 0x04002187 RID: 8583
	private StringBuilder \u1712;

	// Token: 0x04002188 RID: 8584
	private StringBuilder \u1713;

	// Token: 0x04002189 RID: 8585
	private TextWriter \u1714;

	// Token: 0x0400218A RID: 8586
	private bool \u1715;

	// Token: 0x0400218B RID: 8587
	private bool \u1716;

	// Token: 0x0400218C RID: 8588
	private spr\u20C0 \u1717;

	// Token: 0x0400218D RID: 8589
	private int \u1718;

	// Token: 0x0400218E RID: 8590
	private int \u1719;

	// Token: 0x0400218F RID: 8591
	private bool \u171A;

	// Token: 0x04002190 RID: 8592
	private string \u171B;

	// Token: 0x04002191 RID: 8593
	private string \u171C;

	// Token: 0x04002192 RID: 8594
	private string \u171D;

	// Token: 0x04002193 RID: 8595
	private spr\u251B \u171E;

	// Token: 0x04002194 RID: 8596
	private string \u171F;

	// Token: 0x04002195 RID: 8597
	private TextReader ᜠ;

	// Token: 0x04002196 RID: 8598
	private string ᜡ;

	// Token: 0x04002197 RID: 8599
	private string ᜢ;

	// Token: 0x04002198 RID: 8600
	private string ᜣ;

	// Token: 0x04002199 RID: 8601
	private string ᜤ;

	// Token: 0x0400219A RID: 8602
	private WhitespaceHandling ᜥ;

	// Token: 0x0400219B RID: 8603
	private CaseFolding ᜦ;

	// Token: 0x0400219C RID: 8604
	private bool ᜧ = true;

	// Token: 0x0400219D RID: 8605
	private Dictionary<string, string> ᜨ = new Dictionary<string, string>();
}
