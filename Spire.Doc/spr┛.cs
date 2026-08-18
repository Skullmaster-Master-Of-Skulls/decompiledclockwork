using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Convertors.Sgml;

// Token: 0x020001F4 RID: 500
internal class spr\u251B : IDisposable
{
	// Token: 0x060015CF RID: 5583 RVA: 0x00160688 File Offset: 0x0015F688
	public spr\u251B(string A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 10;
		base..ctor();
		this.ᜂ = A_0;
		this.ᜄ = A_1;
		this.ᜅ = A_2;
		this.ᜁ = A_3;
		this.ᜉ = (A_0 != null && sprἿ.ᜀ(A_0, ClipboardData.b("ᡯٱᥳ᩵", a_)));
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x001606E8 File Offset: 0x0015F6E8
	public spr\u251B(string A_0, string A_1)
	{
		this.ᜂ = A_0;
		this.ᜆ = A_1;
		this.ᜃ = true;
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x00160710 File Offset: 0x0015F710
	public spr\u251B(string A_0, Uri A_1, TextReader A_2, string A_3)
	{
		int a_ = 6;
		base..ctor();
		this.ᜂ = A_0;
		this.ᜃ = true;
		this.ᜏ = A_2;
		this.ᜎ = A_1;
		this.ᜁ = A_3;
		this.ᜉ = string.Equals(A_0, ClipboardData.b("ѫᩭᵯṱ", a_), StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x0016076C File Offset: 0x0015F76C
	public string \u1712()
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
		return this.ᜂ;
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x001607B0 File Offset: 0x0015F7B0
	public bool ᜉ()
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
		return this.ᜉ;
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x001607F4 File Offset: 0x0015F7F4
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
		this.ᜉ = A_0;
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x00160838 File Offset: 0x0015F838
	public string ᜋ()
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
		return this.ᜄ;
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x0016087C File Offset: 0x0015F87C
	public string ᜆ()
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
		return this.ᜅ;
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x001608C0 File Offset: 0x0015F8C0
	public Uri ᜁ()
	{
		int num;
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
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				goto IL_8A;
			case 2:
				goto IL_64;
			case 3:
				if (this.ᜈ != null)
				{
					num = 1;
					continue;
				}
				goto IL_93;
			}
			if (this.ᜎ != null)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_64:
		return this.ᜎ;
		IL_8A:
		return this.ᜈ.ᜁ();
		IL_93:
		return null;
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x00160964 File Offset: 0x0015F964
	public spr\u251B ᜑ()
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
		return this.ᜈ;
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x001609A8 File Offset: 0x0015F9A8
	public char ᜎ()
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
		return this.ᜋ;
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x001609EC File Offset: 0x0015F9EC
	public int \u1715()
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
		return this.ᜊ;
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x00160A30 File Offset: 0x0015FA30
	public int \u170D()
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
		return this.\u1712 - this.ᜑ + 1;
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00160A7C File Offset: 0x0015FA7C
	public bool ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00160AC0 File Offset: 0x0015FAC0
	public string \u1713()
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

	// Token: 0x060015DE RID: 5598 RVA: 0x00160B04 File Offset: 0x0015FB04
	public LiteralType ᜐ()
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

	// Token: 0x060015DF RID: 5599 RVA: 0x00160B48 File Offset: 0x0015FB48
	public bool ᜀ()
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
		return this.ᜌ;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x00160B8C File Offset: 0x0015FB8C
	public string ᜊ()
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
		return this.ᜁ;
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x00160BD0 File Offset: 0x0015FBD0
	public char ᜂ()
	{
		char c;
		for (;;)
		{
			if (true)
			{
			}
			c = (char)this.ᜏ.Read();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜊ++;
					this.ᜑ = this.\u1712;
					num = 18;
					continue;
				case 1:
					goto IL_21C;
				case 2:
					goto IL_159;
				case 3:
					if (this.ᜋ == '\r')
					{
						num = 0;
						continue;
					}
					goto IL_241;
				case 4:
					if (c == '\0')
					{
						num = 5;
						continue;
					}
					goto IL_D0;
				case 5:
					goto IL_7C;
				case 6:
					num = 15;
					continue;
				case 7:
					if (c == '\n')
					{
						num = 9;
						continue;
					}
					num = 17;
					continue;
				case 8:
					goto IL_A1;
				case 9:
					this.ᜌ = true;
					this.ᜑ = this.\u1712 + 1;
					this.ᜊ++;
					num = 12;
					continue;
				case 10:
					this.ᜌ = true;
					num = 2;
					continue;
				case 11:
					if (c == '\r')
					{
						num = 10;
						continue;
					}
					this.ᜌ = false;
					num = 3;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_1EF;
					}
					break;
				case 13:
					this.ᜑ = this.\u1712;
					this.ᜊ++;
					num = 1;
					continue;
				case 14:
					goto IL_D0;
				case 15:
					if (c == '\t')
					{
						num = 8;
						continue;
					}
					num = 11;
					continue;
				case 16:
					if (this.ᜋ == '\r')
					{
						num = 13;
						continue;
					}
					goto IL_241;
				case 17:
					if (c != ' ')
					{
						num = 6;
						continue;
					}
					goto IL_A1;
				case 18:
					goto IL_183;
				}
				break;
				IL_7C:
				c = ' ';
				num = 14;
				continue;
				IL_A1:
				this.ᜌ = true;
				num = 16;
				continue;
				IL_D0:
				this.\u1712++;
				num = 7;
			}
		}
		IL_159:
		IL_183:
		goto IL_241;
		IL_1EF:
		if (false)
		{
		}
		IL_21C:
		IL_241:
		this.ᜋ = c;
		return c;
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x00160E28 File Offset: 0x0015FE28
	public void ᜀ(spr\u251B A_0, Uri A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜈ = A_0;
				int num = 18;
				for (;;)
				{
					Encoding a_2;
					string text;
					HttpWebRequest httpWebRequest;
					int num4;
					Uri responseUri;
					string a_3;
					Stream a_4;
					switch (num)
					{
					case 0:
						if (this.ᜅ == null)
						{
							num = 14;
							continue;
						}
						num = 29;
						continue;
					case 1:
						try
						{
							string name;
							a_2 = Encoding.GetEncoding(name);
							goto IL_58C;
						}
						catch (ArgumentException)
						{
							goto IL_58C;
						}
						goto IL_22E;
					case 2:
						if (this.ᜆ != null)
						{
							num = 25;
							continue;
						}
						return;
					case 3:
					{
						int num2 = text.Length;
						num = 19;
						continue;
					}
					case 4:
						httpWebRequest.Proxy = new WebProxy(this.ᜁ);
						num = 36;
						continue;
					case 5:
						this.ᜉ = true;
						num = 9;
						continue;
					case 6:
					{
						int num3 = text.IndexOf(ClipboardData.b("坩", a_), num4);
						int num2 = text.IndexOf(ClipboardData.b("兩", a_), num3);
						num = 39;
						continue;
					}
					case 7:
						goto IL_25F;
					case 8:
						if (num4 >= 0)
						{
							num = 26;
							continue;
						}
						goto IL_5A5;
					case 9:
						IL_1DA:
						goto IL_ED;
					case 10:
						num = 2;
						continue;
					case 11:
						this.ᜎ = responseUri;
						num = 21;
						continue;
					case 12:
						goto IL_2F5;
					case 13:
						if (this.ᜃ)
						{
							num = 10;
							continue;
						}
						num = 0;
						continue;
					case 14:
						goto IL_2F0;
					case 15:
						if (!string.Equals(responseUri.AbsoluteUri, this.ᜎ.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
						{
							num = 11;
							continue;
						}
						goto IL_12F;
					case 16:
					{
						int num3;
						if (num3 > 0)
						{
							num = 20;
							continue;
						}
						goto IL_58C;
					}
					case 17:
						goto IL_5A5;
					case 18:
						if (A_0 != null)
						{
							num = 32;
							continue;
						}
						goto IL_541;
					case 19:
						goto IL_32D;
					case 20:
					{
						int num3;
						num3++;
						int num2;
						string name = text.Substring(num3, num2 - num3).Trim();
						num = 1;
						continue;
					}
					case 21:
						goto IL_12F;
					case 22:
						goto IL_541;
					case 23:
						this.ᜎ = new Uri(A_1, this.ᜅ);
						num = 12;
						continue;
					case 24:
						goto IL_22E;
					case 25:
						goto IL_49E;
					case 26:
						a_3 = text.Substring(0, num4);
						num = 17;
						continue;
					case 27:
						goto IL_2F5;
					case 28:
						if (sprἿ.ᜀ(a_3, ClipboardData.b("ṩ५᙭ѯ嵱ᱳɵᕷᙹ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_ED;
					case 29:
						if (A_1 != null)
						{
							num = 23;
							continue;
						}
						this.ᜎ = new Uri(this.ᜅ);
						num = 27;
						continue;
					case 30:
						if (this.ᜁ != null)
						{
							num = 4;
							continue;
						}
						goto IL_4DC;
					case 31:
					{
						string scheme;
						if ((scheme = this.ᜎ.Scheme) != null)
						{
							num = 37;
							continue;
						}
						goto IL_3A6;
					}
					case 32:
						this.ᜉ = A_0.ᜉ();
						num = 22;
						continue;
					case 33:
					{
						string scheme;
						if (scheme == ClipboardData.b("౩իɭᕯ", a_))
						{
							num = 35;
							continue;
						}
						goto IL_3A6;
					}
					case 34:
						if (num4 >= 0)
						{
							num = 6;
							continue;
						}
						goto IL_58C;
					case 35:
					{
						string localPath = this.ᜎ.LocalPath;
						a_4 = new FileStream(localPath, FileMode.Open, FileAccess.Read);
						num = 38;
						continue;
					}
					case 36:
						goto IL_4DC;
					case 37:
						num = 33;
						continue;
					case 38:
						goto IL_22E;
					case 39:
					{
						int num2;
						if (num2 < 0)
						{
							num = 3;
							continue;
						}
						goto IL_32D;
					}
					}
					break;
					IL_ED:
					num4 = text.IndexOf(ClipboardData.b("३ѫ཭ɯűᅳɵ", a_));
					a_2 = Encoding.Default;
					num = 34;
					continue;
					IL_541:
					this.ᜊ = 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1DA;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					IL_12F:
					WebResponse response;
					text = response.ContentType.ToLowerInvariant();
					a_3 = text;
					num4 = text.IndexOf(';');
					num = 8;
					continue;
					IL_22E:
					this.ᜐ = true;
					sprᧅ sprᧅ = new sprᧅ(a_4, a_2);
					this.\u170D = sprᧅ.ᜇ();
					this.ᜏ = sprᧅ;
					num = 7;
					continue;
					IL_2F5:
					a_4 = null;
					a_2 = Encoding.Default;
					num = 31;
					continue;
					IL_32D:
					num = 16;
					continue;
					IL_3A6:
					httpWebRequest = (HttpWebRequest)WebRequest.Create(this.ᜁ());
					httpWebRequest.UserAgent = ClipboardData.b("❩ͫᑭ᥯ṱᡳ᝵坷乹剻乽ꁿꪁ憎揄憐ꎗ뎙ꞛ", a_);
					httpWebRequest.Timeout = 10000;
					num = 30;
					continue;
					IL_4DC:
					if (true)
					{
					}
					httpWebRequest.PreAuthenticate = false;
					httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
					response = httpWebRequest.GetResponse();
					responseUri = response.ResponseUri;
					num = 15;
					continue;
					IL_58C:
					a_4 = response.GetResponseStream();
					num = 24;
					continue;
					IL_5A5:
					num = 28;
				}
			}
			IL_25F:
			return;
			IL_2F0:
			this.ᜀ(ClipboardData.b("㽩ɫᱭᕯű᭳᩵๷᭹ṻችꊁﲇ낏떑ꚕ붙", a_), this.ᜂ);
			return;
			IL_49E:
			this.ᜏ = new StringReader(this.ᜆ);
			return;
		}
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x00161424 File Offset: 0x00160424
	public Encoding ᜏ()
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
		return this.\u170D;
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x00161468 File Offset: 0x00160468
	public void ᜃ()
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_67;
				case 2:
					this.ᜏ.Close();
					num = 0;
					continue;
				}
				if (!this.ᜐ)
				{
					goto IL_69;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
		IL_67:
		IL_69:
		if (true)
		{
		}
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x001614E8 File Offset: 0x001604E8
	public char ᜌ()
	{
		char c;
		for (;;)
		{
			c = this.ᜋ;
			if (true)
			{
			}
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (c != '￿')
					{
						num = 4;
						continue;
					}
					goto IL_FD;
				case 1:
					num = 10;
					continue;
				case 2:
					if (c != '\t')
					{
						num = 3;
						continue;
					}
					goto IL_8C;
				case 3:
					goto IL_FD;
				case 4:
					num = 9;
					continue;
				case 5:
					IL_F8:
					num = 8;
					continue;
				case 6:
					goto IL_BD;
				case 7:
					goto IL_BD;
				case 8:
					if (c != '\n')
					{
						num = 11;
						continue;
					}
					goto IL_8C;
				case 9:
					if (c != ' ')
					{
						num = 1;
						continue;
					}
					goto IL_8C;
				case 10:
					if (c != '\r')
					{
						num = 5;
						continue;
					}
					goto IL_8C;
				case 11:
					num = 2;
					continue;
				}
				break;
				IL_8C:
				c = this.ᜂ();
				num = 6;
				continue;
				IL_BD:
				num = 0;
				continue;
				IL_FD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F8;
				default:
					goto IL_113;
				}
			}
		}
		IL_113:
		if (false)
		{
		}
		return c;
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x00161610 File Offset: 0x00160610
	public string ᜀ(StringBuilder A_0, string A_1, bool A_2)
	{
		int a_ = 1;
		int num = 0;
		char c;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_188;
			case 2:
				if (!char.IsLetter(c))
				{
					num = 24;
					continue;
				}
				goto IL_188;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A;
				default:
					if (false)
					{
					}
					if (A_2)
					{
						num = 26;
						continue;
					}
					goto IL_188;
				}
				break;
			case 4:
				num = 15;
				continue;
			case 5:
				if (c != '.')
				{
					num = 20;
					continue;
				}
				goto IL_2F2;
			case 6:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				A_0.Length = 0;
				c = this.ᜋ;
				num = 3;
				continue;
			case 7:
				if (c != '_')
				{
					num = 9;
					continue;
				}
				goto IL_2F2;
			case 8:
				c = this.ᜂ();
				num = 1;
				continue;
			case 9:
				num = 5;
				continue;
			case 10:
				num = 19;
				continue;
			case 11:
				goto IL_9A;
			case 12:
				if (c != '-')
				{
					num = 10;
					continue;
				}
				goto IL_2F2;
			case 13:
				goto IL_255;
			case 14:
				if (c != '_')
				{
					if (true)
					{
					}
					num = 25;
					continue;
				}
				goto IL_188;
			case 15:
				if (A_1.IndexOf(c) >= 0)
				{
					num = 13;
					continue;
				}
				num = 21;
				continue;
			case 16:
				goto IL_160;
			case 17:
				goto IL_2F2;
			case 18:
				num = 22;
				continue;
			case 19:
				if (c != ':')
				{
					num = 18;
					continue;
				}
				goto IL_2F2;
			case 20:
				num = 12;
				continue;
			case 21:
				if (A_2)
				{
					num = 23;
					continue;
				}
				goto IL_2F2;
			case 22:
				if (char.IsLetterOrDigit(c))
				{
					num = 17;
					continue;
				}
				goto IL_203;
			case 23:
				num = 7;
				continue;
			case 24:
				goto IL_2D2;
			case 25:
				num = 2;
				continue;
			case 26:
				num = 14;
				continue;
			case 27:
				if (c != '￿')
				{
					num = 4;
					continue;
				}
				goto IL_32A;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 6;
			continue;
			IL_188:
			num = 27;
			continue;
			IL_2F2:
			A_0.Append(c);
			num = 8;
		}
		IL_9A:
		throw new ArgumentNullException(ClipboardData.b("ᑦ୨", a_));
		IL_160:
		throw new ArgumentNullException(ClipboardData.b("፦౨ᥪl", a_));
		IL_203:
		throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("⹦ݨᵪ౬ͮᡰᝲ啴᥶ᡸᙺ᡼彾歷뎒는ꦘ몜", a_), new object[]
		{
			c
		}));
		IL_255:
		goto IL_32A;
		IL_2D2:
		throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("⹦ݨᵪ౬ͮᡰᝲ啴᥶ᡸᙺ᡼彾ﶈꮊﺚ뾞蚠\ud8a2閤\udaa6躨", a_), new object[]
		{
			c
		}));
		IL_32A:
		return A_0.ToString();
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x00161950 File Offset: 0x00160950
	public string ᜀ(StringBuilder A_0, char A_1)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			char c;
			switch (num)
			{
			case 0:
				goto IL_190;
			case 1:
				goto IL_DE;
			case 3:
				c = this.ᜂ();
				num = 13;
				continue;
			case 4:
				goto IL_DE;
			case 5:
				num = 10;
				continue;
			case 6:
				goto IL_DE;
			case 7:
				goto IL_DE;
			case 8:
				goto IL_5C;
			case 9:
				goto IL_154;
			case 10:
				if (c == A_1)
				{
					num = 9;
					continue;
				}
				num = 0;
				continue;
			case 11:
			{
				string value = this.ᜈ();
				A_0.Append(value);
				c = this.ᜋ;
				num = 6;
				continue;
			}
			case 12:
				if (c != '￿')
				{
					num = 5;
					continue;
				}
				goto IL_1A5;
			case 13:
				if (c == '#')
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				A_0.Append('&');
				A_0.Append(c);
				c = this.ᜂ();
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			A_0.Length = 0;
			c = this.ᜂ();
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
			IL_190:
			if (c == '&')
			{
				num = 3;
				continue;
			}
			A_0.Append(c);
			c = this.ᜂ();
			num = 1;
			continue;
			IL_DE:
			num = 12;
		}
		IL_5C:
		throw new ArgumentNullException(ClipboardData.b("ͯၱ", a_));
		IL_154:
		IL_1A5:
		this.ᜂ();
		return A_0.ToString();
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x00161B10 File Offset: 0x00160B10
	public string ᜀ(StringBuilder A_0, string A_1, string A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int a_2;
				int num2;
				int num3;
				char c;
				int num4;
				int num6;
				int num7;
				char c2;
				switch (num)
				{
				case 1:
					this.ᜀ(A_1 + ClipboardData.b("噵୷๹ᵻ౽ꢇ꺍ﲏﮑ望뢗겛肟햡얣향袧쒩즫\ud8ad햯삱钳햵풷햹쾻\udbbd꒿", a_), a_2);
					num = 35;
					continue;
				case 2:
					if (num2 < A_2.Length)
					{
						num = 30;
						continue;
					}
					goto IL_5A4;
				case 3:
					if (num3 != 0)
					{
						num = 51;
						continue;
					}
					num = 38;
					continue;
				case 4:
					goto IL_57B;
				case 5:
					if (A_0 != null)
					{
						num = 26;
						continue;
					}
					goto IL_5C7;
				case 6:
					num = 3;
					continue;
				case 7:
					goto IL_1FF;
				case 8:
					goto IL_2C3;
				case 9:
					if (num2 > 0)
					{
						num = 42;
						continue;
					}
					num = 27;
					continue;
				case 10:
					goto IL_3FC;
				case 11:
					if (A_0 != null)
					{
						num = 39;
						continue;
					}
					goto IL_57B;
				case 12:
					goto IL_51A;
				case 13:
					if (c == '\0')
					{
						num = 1;
						continue;
					}
					goto IL_4B7;
				case 14:
					goto IL_11A;
				case 15:
					A_0.Append(c);
					num = 50;
					continue;
				case 16:
					num2++;
					num = 2;
					continue;
				case 17:
				{
					if (true)
					{
					}
					int num5;
					if (A_2[num4 - num5] == A_2[num2 - num5])
					{
						num = 47;
						continue;
					}
					goto IL_1FF;
				}
				case 18:
					if (num4 > 0)
					{
						num = 20;
						continue;
					}
					goto IL_3FC;
				case 19:
					goto IL_2C3;
				case 20:
					A_0.Append(c);
					num = 10;
					continue;
				case 21:
					num3 = num4 + 1;
					num = 24;
					continue;
				case 22:
					if (num6 > num2 - num3 - num4)
					{
						num = 49;
						continue;
					}
					A_0.Append(A_2[num6]);
					num6++;
					num = 19;
					continue;
				case 23:
					goto IL_239;
				case 24:
					goto IL_239;
				case 25:
					if (c != '￿')
					{
						num = 32;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_28B;
					default:
						if (false)
						{
						}
						num = 43;
						continue;
					}
					break;
				case 26:
					goto IL_4DC;
				case 27:
					if (A_0 != null)
					{
						num = 15;
						continue;
					}
					goto IL_28B;
				case 28:
					if (A_0 != null)
					{
						num = 34;
						continue;
					}
					goto IL_3FC;
				case 29:
					num7 = 1;
					goto IL_451;
				case 30:
					c2 = A_2[num2];
					num = 33;
					continue;
				case 31:
					if (num4 >= 0)
					{
						num = 52;
						continue;
					}
					num = 29;
					continue;
				case 32:
					if (c == c2)
					{
						num = 16;
						continue;
					}
					num = 9;
					continue;
				case 33:
					goto IL_28B;
				case 34:
					num = 31;
					continue;
				case 35:
					goto IL_4B7;
				case 36:
					goto IL_313;
				case 37:
					goto IL_239;
				case 38:
					if (A_2[num4] == c)
					{
						num = 53;
						continue;
					}
					num4--;
					num = 37;
					continue;
				case 39:
					A_0.Length = 0;
					num = 4;
					continue;
				case 40:
					goto IL_313;
				case 41:
					num7 = 0;
					goto IL_451;
				case 42:
					num4 = num2 - 1;
					num3 = 0;
					num = 23;
					continue;
				case 43:
					goto IL_5A4;
				case 44:
				{
					int num5;
					if (num5 > num4)
					{
						num = 21;
						continue;
					}
					goto IL_239;
				}
				case 45:
					if (num4 >= 0)
					{
						num = 6;
						continue;
					}
					goto IL_467;
				case 46:
					goto IL_28B;
				case 47:
				{
					int num5;
					num5++;
					num = 48;
					continue;
				}
				case 48:
					goto IL_51A;
				case 49:
					num = 18;
					continue;
				case 50:
					goto IL_28B;
				case 51:
					goto IL_467;
				case 52:
					num = 41;
					continue;
				case 53:
				{
					int num5 = 1;
					num = 12;
					continue;
				}
				case 54:
				{
					int num5;
					if (num4 - num5 < 0)
					{
						num = 7;
						continue;
					}
					num = 17;
					continue;
				}
				}
				if (A_2 == null)
				{
					num = 14;
					continue;
				}
				num = 11;
				continue;
				IL_1FF:
				num = 44;
				continue;
				IL_239:
				num = 45;
				continue;
				IL_28B:
				c = this.ᜂ();
				num = 40;
				continue;
				IL_2C3:
				num = 22;
				continue;
				IL_313:
				num = 25;
				continue;
				IL_3FC:
				num2 = num3;
				c2 = A_2[num3];
				num = 46;
				continue;
				IL_451:
				num4 = num7;
				num6 = 0;
				num = 8;
				continue;
				IL_467:
				num = 28;
				continue;
				IL_4B7:
				this.ᜂ();
				num = 5;
				continue;
				IL_51A:
				num = 54;
				continue;
				IL_57B:
				a_2 = this.ᜊ;
				c = this.ᜂ();
				num2 = 0;
				c2 = A_2[num2];
				num = 36;
				continue;
				IL_5A4:
				num = 13;
			}
			IL_11A:
			throw new ArgumentNullException(ClipboardData.b("ɵᵷࡹᅻ᝽慎黎", a_));
			IL_4DC:
			return A_0.ToString();
			IL_5C7:
			return string.Empty;
		}
		}
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x001620EC File Offset: 0x001610EC
	public string ᜈ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			string text;
			int num;
			string str;
			for (;;)
			{
				num = this.ᜀ(out text);
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num <= 56319)
						{
							num2 = 1;
							continue;
						}
						goto IL_2F3;
					case 1:
						num2 = 5;
						continue;
					case 2:
						goto IL_148;
					case 3:
						goto IL_21D;
					case 4:
						goto IL_F6;
					case 5:
						if (this.ᜋ == '&')
						{
							num2 = 9;
							continue;
						}
						this.ᜀ(ClipboardData.b("㝦᭨๪l๮հٲݴቶ奸z䵼ɾꆀ愈놐煮ﺞ햠욢薤힦좨슪\udfac", a_), this.ᜋ);
						num2 = 6;
						continue;
					case 6:
						goto IL_1AF;
					case 7:
						if (num == -1)
						{
							num2 = 14;
							continue;
						}
						num2 = 17;
						continue;
					case 8:
					{
						char c;
						if (c == '#')
						{
							num2 = 10;
							continue;
						}
						this.ᜀ(ClipboardData.b("㝦᭨๪l๮հٲݴቶ奸z䵼ɾꆀ愈놐煮ﺞ햠욢薤힦좨슪\udfac", a_), c);
						num2 = 3;
						continue;
					}
					case 9:
					{
						char c = this.ᜂ();
						goto IL_2B1;
					}
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B1;
						default:
						{
							if (false)
							{
							}
							int num3 = this.ᜀ(out str);
							num2 = 15;
							continue;
						}
						}
						break;
					case 11:
					{
						int num3;
						if (num3 <= 57343)
						{
							num2 = 19;
							continue;
						}
						goto IL_2F3;
					}
					case 12:
					{
						int num3;
						if (56320 <= num3)
						{
							num2 = 16;
							continue;
						}
						goto IL_2F3;
					}
					case 13:
						if (num >= 128 & num <= 159)
						{
							num2 = 21;
							continue;
						}
						goto IL_24B;
					case 14:
						goto IL_A1;
					case 15:
					{
						int num3;
						if (num3 == -1)
						{
							num2 = 4;
							continue;
						}
						num2 = 12;
						continue;
					}
					case 16:
						num2 = 11;
						continue;
					case 17:
						if (this.ᜉ)
						{
							num2 = 22;
							continue;
						}
						goto IL_24B;
					case 18:
						if (55296 <= num)
						{
							num2 = 20;
							continue;
						}
						goto IL_2F3;
					case 19:
					{
						int num3;
						num = char.ConvertToUtf32((char)num, (char)num3);
						num2 = 2;
						continue;
					}
					case 20:
						num2 = 0;
						continue;
					case 21:
						goto IL_12C;
					case 22:
						num2 = 13;
						continue;
					}
					break;
					IL_24B:
					num2 = 18;
					continue;
					IL_2B1:
					num2 = 8;
				}
			}
			IL_A1:
			if (true)
			{
			}
			return text;
			IL_F6:
			return text + ClipboardData.b("屦", a_) + str;
			IL_12C:
			int num4 = num - 128;
			int value = spr\u251B.\u1713[num4];
			return Convert.ToChar(value).ToString();
			IL_148:
			IL_1AF:
			IL_21D:
			IL_2F3:
			return char.ConvertFromUtf32(num);
		}
		}
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x001623F4 File Offset: 0x001613F4
	private int ᜀ(out string A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				char c = this.ᜂ();
				A_0 = ClipboardData.b("䥮剰", a_);
				int num2 = 8;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
					{
						bool flag;
						if (!flag)
						{
							num2 = 19;
							continue;
						}
						goto IL_221;
					}
					case 1:
						if (c >= '0')
						{
							num2 = 27;
							continue;
						}
						goto IL_321;
					case 2:
						if (c <= 'f')
						{
							num2 = 4;
							continue;
						}
						goto IL_2DA;
					case 3:
						this.ᜀ(ClipboardData.b("㽮Ͱᙲᡴᙶ൸๺ོ᩾ꆀ떄惘ꦈﮊﶎ朗ﮔ릘ﺚ좠힢\udca4螦\udba8캪쮬쪮쎰횲\udbb4풶\udcb8", a_), c);
						num2 = 12;
						continue;
					case 4:
					{
						num3 = (int)(c - 'a' + '\n');
						bool flag2 = true;
						num2 = 35;
						continue;
					}
					case 5:
						goto IL_259;
					case 6:
						this.ᜂ();
						num2 = 38;
						continue;
					case 7:
						num2 = 2;
						continue;
					case 8:
						if (c != 'x')
						{
							bool flag = false;
							num2 = 13;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_345;
						default:
							if (false)
							{
							}
							num2 = 16;
							continue;
						}
						break;
					case 9:
						goto IL_36E;
					case 10:
						goto IL_1E9;
					case 11:
						num2 = 15;
						continue;
					case 12:
						goto IL_1E4;
					case 13:
						goto IL_19B;
					case 14:
						if (c != '￿')
						{
							num2 = 34;
							continue;
						}
						goto IL_1E9;
					case 15:
						if (c <= '9')
						{
							num2 = 37;
							continue;
						}
						goto IL_2B8;
					case 16:
					{
						bool flag2 = false;
						A_0 += ClipboardData.b("ᝮ", a_);
						c = this.ᜂ();
						num2 = 9;
						continue;
					}
					case 17:
						if (c >= '0')
						{
							num2 = 11;
							continue;
						}
						goto IL_2B8;
					case 18:
						goto IL_345;
					case 19:
						return -1;
					case 20:
					{
						num3 = (int)(c - 'A' + '\n');
						bool flag2 = true;
						num2 = 5;
						continue;
					}
					case 21:
						if (c >= 'A')
						{
							num2 = 30;
							continue;
						}
						goto IL_1E9;
					case 22:
						if (c <= 'F')
						{
							num2 = 20;
							continue;
						}
						goto IL_1E9;
					case 23:
						if (c != '￿')
						{
							num2 = 18;
							continue;
						}
						goto IL_321;
					case 24:
						goto IL_36E;
					case 25:
						if (c == ';')
						{
							num2 = 10;
							continue;
						}
						num3 = 0;
						num2 = 17;
						continue;
					case 26:
						goto IL_259;
					case 27:
						num2 = 41;
						continue;
					case 28:
						return -1;
					case 29:
						goto IL_19B;
					case 30:
						num2 = 22;
						continue;
					case 31:
						if (c == '\0')
						{
							num2 = 3;
							continue;
						}
						num2 = 40;
						continue;
					case 32:
						if (c >= 'a')
						{
							num2 = 7;
							continue;
						}
						goto IL_2DA;
					case 33:
						goto IL_321;
					case 34:
						num2 = 25;
						continue;
					case 35:
						goto IL_259;
					case 36:
					{
						bool flag2;
						if (!flag2)
						{
							num2 = 28;
							continue;
						}
						goto IL_221;
					}
					case 37:
					{
						num3 = (int)(c - '0');
						bool flag2 = true;
						num2 = 26;
						continue;
					}
					case 38:
						goto IL_254;
					case 39:
						if (c == ';')
						{
							num2 = 33;
							continue;
						}
						num2 = 1;
						continue;
					case 40:
						if (c == ';')
						{
							num2 = 6;
							continue;
						}
						return num;
					case 41:
						if (true)
						{
						}
						if (c <= '9')
						{
							num2 = 42;
							continue;
						}
						goto IL_321;
					case 42:
					{
						num = num * 10 + (int)(c - '0');
						bool flag = true;
						A_0 += c;
						c = this.ᜂ();
						num2 = 29;
						continue;
					}
					}
					break;
					IL_19B:
					num2 = 23;
					continue;
					IL_1E9:
					num2 = 36;
					continue;
					IL_221:
					num2 = 31;
					continue;
					IL_259:
					A_0 += c;
					num = num * 16 + num3;
					c = this.ᜂ();
					num2 = 24;
					continue;
					IL_2B8:
					num2 = 32;
					continue;
					IL_2DA:
					num2 = 21;
					continue;
					IL_321:
					num2 = 0;
					continue;
					IL_345:
					num2 = 39;
					continue;
					IL_36E:
					num2 = 14;
				}
			}
			IL_1E4:
			return num;
			IL_254:
			return num;
		}
		}
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x001628AC File Offset: 0x001618AC
	public void ᜁ(string A_0)
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
		throw new spr\u1FA8(A_0, this);
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x001628F0 File Offset: 0x001618F0
	public void ᜀ(string A_0, char A_1)
	{
		int a_ = 12;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				goto IL_89;
			case 2:
				goto IL_71;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5F;
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
			}
			goto IL_57;
			IL_5F:
			num = 0;
			continue;
			IL_57:
			if (A_1 != '￿')
			{
				goto IL_5F;
			}
			num = 2;
		}
		IL_71:
		string text = ClipboardData.b("㝱㭳ふ", a_);
		goto IL_91;
		IL_89:
		text = char.ToString(A_1);
		IL_91:
		string text2 = text;
		throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, A_0, new object[]
		{
			text2
		}), this);
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x001629AC File Offset: 0x001619AC
	public void ᜀ(string A_0, int A_1)
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
		throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, A_0, new object[]
		{
			A_1
		}), this);
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x00162A0C File Offset: 0x00161A0C
	public void ᜀ(string A_0, string A_1)
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
		throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, A_0, new object[]
		{
			A_1
		}), this);
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x00162A64 File Offset: 0x00161A64
	public string ᜇ()
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				if (true)
				{
				}
				spr\u251B spr_u251B = this;
				stringBuilder = new StringBuilder();
				int num = 1;
				for (;;)
				{
					string value;
					switch (num)
					{
					case 0:
						if (spr_u251B.ᜃ)
						{
							num = 2;
							continue;
						}
						value = string.Format(CultureInfo.InvariantCulture, ClipboardData.b("汥㩧ཀྵ੫୭ɯ᝱ᩳᕵᵷṹ屻ᅽꊁ겋ꂏ뢓뚕풟쮡쮣좥袧톩鶫펭邯\uddb1튳隵龷솹躻쎽ꇃꣅ볇ꏉ룋럍돑ꃓ菗ꇙꏝ뷟", a_), new object[]
						{
							spr_u251B.ᜊ,
							spr_u251B.\u170D(),
							spr_u251B.ᜂ,
							spr_u251B.ᜁ().AbsolutePath
						});
						num = 3;
						continue;
					case 1:
						goto IL_108;
					case 2:
						value = string.Format(CultureInfo.InvariantCulture, ClipboardData.b("汥㩧ཀྵ੫୭ɯ᝱ᩳᕵᵷṹ屻ᅽꊁ겋ꂏ뢓뚕풟쮡쮣좥袧톩鶫펭邯\uddb1튳隵톷풹좻\udbbd늿곁ꗃ꫅꿉ꋋ뫍맏ꛑ귓ￗꇙꏝ쟟", a_), new object[]
						{
							spr_u251B.ᜊ,
							spr_u251B.\u170D(),
							spr_u251B.ᜂ
						});
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E6;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 3:
						goto IL_5E;
					case 4:
						goto IL_126;
					case 5:
						goto IL_108;
					case 6:
						if (spr_u251B == null)
						{
							num = 4;
							continue;
						}
						goto IL_E6;
					case 7:
						goto IL_5E;
					}
					break;
					IL_5E:
					stringBuilder.Append(value);
					spr_u251B = spr_u251B.ᜑ();
					num = 5;
					continue;
					IL_E6:
					num = 0;
					continue;
					IL_108:
					num = 6;
				}
			}
			IL_126:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x00162C1C File Offset: 0x00161C1C
	public static bool ᜀ(string A_0)
	{
		int a_ = 4;
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return true;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_B8;
				case 2:
					num = 3;
					continue;
				case 3:
					if (!string.Equals(A_0, ClipboardData.b("㥩⡫⽭⑯㍱", a_), StringComparison.OrdinalIgnoreCase))
					{
						num = 0;
						continue;
					}
					return true;
				}
				if (string.Equals(A_0, ClipboardData.b("⥩⡫⽭⑯㍱", a_), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				num = 2;
				break;
			}
		}
		IL_B8:
		return string.Equals(A_0, ClipboardData.b("㩩╫", a_), StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x00162CE4 File Offset: 0x00161CE4
	public void ᜂ(string A_0)
	{
		int a_ = 16;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (!(A_0 == ClipboardData.b("㕵㱷㭹⡻㽽", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_4F;
				}
				break;
			case 1:
				goto IL_98;
			case 3:
				goto IL_9D;
			case 4:
				num = 7;
				continue;
			case 5:
				return;
			case 6:
				if (!(A_0 == ClipboardData.b("♵ㅷ", a_)))
				{
					num = 5;
					continue;
				}
				this.ᜇ = LiteralType.PI;
				num = 1;
				continue;
			case 7:
				if (!(A_0 == ClipboardData.b("╵㱷㭹⡻㽽", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_FF;
			case 8:
				num = 6;
				continue;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return;
			IL_9D:
			num = 0;
		}
		IL_4F:
		this.ᜇ = LiteralType.CDATA;
		return;
		IL_98:
		return;
		IL_FF:
		this.ᜇ = LiteralType.SDATA;
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x00162E2C File Offset: 0x00161E2C
	protected virtual void \u1714()
	{
		try
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
			this.ᜁ(false);
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x00162E88 File Offset: 0x00161E88
	public void ᜅ()
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
		this.ᜁ(true);
		GC.SuppressFinalize(this);
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x00162ED0 File Offset: 0x00161ED0
	protected virtual void ᜁ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
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
					this.ᜏ.Dispose();
					this.ᜏ = null;
					num = 4;
					continue;
				}
				break;
			case 3:
				if (this.ᜏ != null)
				{
					num = 1;
					continue;
				}
				return;
			case 4:
				return;
			}
			IL_24:
			if (true)
			{
			}
			if (A_0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_24;
		}
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x00162F78 File Offset: 0x00161F78
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u251B()
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
		spr\u251B.\u1713 = new int[]
		{
			8364,
			129,
			8218,
			402,
			8222,
			8230,
			8224,
			8225,
			710,
			8240,
			352,
			8249,
			338,
			141,
			381,
			143,
			144,
			8216,
			8217,
			8220,
			8221,
			8226,
			8211,
			8212,
			732,
			8482,
			353,
			8250,
			339,
			157,
			382,
			376
		};
	}

	// Token: 0x040019E6 RID: 6630
	public const char ᜀ = '￿';

	// Token: 0x040019E7 RID: 6631
	private string ᜁ;

	// Token: 0x040019E8 RID: 6632
	private string ᜂ;

	// Token: 0x040019E9 RID: 6633
	private bool ᜃ;

	// Token: 0x040019EA RID: 6634
	private string ᜄ;

	// Token: 0x040019EB RID: 6635
	private string ᜅ;

	// Token: 0x040019EC RID: 6636
	private string ᜆ;

	// Token: 0x040019ED RID: 6637
	private LiteralType ᜇ;

	// Token: 0x040019EE RID: 6638
	private spr\u251B ᜈ;

	// Token: 0x040019EF RID: 6639
	private bool ᜉ;

	// Token: 0x040019F0 RID: 6640
	private int ᜊ;

	// Token: 0x040019F1 RID: 6641
	private char ᜋ;

	// Token: 0x040019F2 RID: 6642
	private bool ᜌ;

	// Token: 0x040019F3 RID: 6643
	private Encoding \u170D;

	// Token: 0x040019F4 RID: 6644
	private Uri ᜎ;

	// Token: 0x040019F5 RID: 6645
	private TextReader ᜏ;

	// Token: 0x040019F6 RID: 6646
	private bool ᜐ;

	// Token: 0x040019F7 RID: 6647
	private int ᜑ;

	// Token: 0x040019F8 RID: 6648
	private int \u1712;

	// Token: 0x040019F9 RID: 6649
	private static int[] \u1713;
}
