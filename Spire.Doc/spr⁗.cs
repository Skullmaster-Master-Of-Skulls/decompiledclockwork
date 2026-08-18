using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Convertors.Sgml;

// Token: 0x020001FD RID: 509
internal class spr\u2057
{
	// Token: 0x0600163E RID: 5694 RVA: 0x0016705C File Offset: 0x0016605C
	public spr\u2057(string A_0, XmlNameTable A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = new Dictionary<string, spr\u1D66>();
		this.ᜃ = new Dictionary<string, spr\u251B>();
		this.ᜄ = new Dictionary<string, spr\u251B>();
		this.ᜅ = new StringBuilder();
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x001670A4 File Offset: 0x001660A4
	public string ᜌ()
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

	// Token: 0x06001640 RID: 5696 RVA: 0x001670E8 File Offset: 0x001660E8
	public XmlNameTable ᜋ()
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

	// Token: 0x06001641 RID: 5697 RVA: 0x00167124 File Offset: 0x00166124
	public static spr\u2057 ᜀ(Uri A_0, string A_1, string A_2, string A_3, string A_4, string A_5, XmlNameTable A_6)
	{
		spr\u2057 spr_u;
		for (;;)
		{
			spr_u = new spr\u2057(A_1, A_6);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D3;
				case 1:
					if (!string.IsNullOrEmpty(A_3))
					{
						num = 5;
						continue;
					}
					goto IL_D3;
				case 2:
					goto IL_6E;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						if (false)
						{
						}
						goto IL_46;
					}
					break;
				case 4:
					if (!string.IsNullOrEmpty(A_4))
					{
						num = 2;
						continue;
					}
					goto IL_46;
				case 5:
					spr_u.ᜀ(A_0, new spr\u251B(spr_u.ᜌ(), A_2, A_3, A_5));
					num = 0;
					continue;
				}
				break;
				IL_6E:
				spr_u.ᜀ(A_0, new spr\u251B(A_1, A_4));
				num = 3;
				continue;
				try
				{
					IL_46:
					spr_u.ᜈ();
					return spr_u;
				}
				catch (ApplicationException ex)
				{
					throw new spr\u1FA8(ex.Message + spr_u.ᜆ.ᜇ());
				}
				goto IL_6E;
				IL_D3:
				if (true)
				{
				}
				num = 4;
			}
		}
		return spr_u;
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x00167244 File Offset: 0x00166244
	public static spr\u2057 ᜀ(Uri A_0, string A_1, TextReader A_2, string A_3, string A_4, XmlNameTable A_5)
	{
		spr\u2057 spr_u;
		for (;;)
		{
			spr_u = new spr\u2057(A_1, A_5);
			spr_u.ᜀ(A_0, new spr\u251B(spr_u.ᜌ(), A_0, A_2, A_4));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					if (string.IsNullOrEmpty(A_3))
					{
						goto IL_73;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B9;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_B9;
				}
				break;
				IL_98:
				spr_u.ᜀ(A_0, new spr\u251B(A_1, A_3));
				if (true)
				{
				}
				num = 2;
				continue;
				try
				{
					IL_73:
					spr_u.ᜈ();
					return spr_u;
				}
				catch (ApplicationException ex)
				{
					throw new spr\u1FA8(ex.Message + spr_u.ᜆ.ᜇ());
				}
				goto IL_98;
				IL_B9:
				goto IL_73;
			}
		}
		return spr_u;
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x00167320 File Offset: 0x00166320
	public spr\u251B ᜄ(string A_0)
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
		spr\u251B result;
		this.ᜄ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x0016736C File Offset: 0x0016636C
	public spr\u1D66 ᜃ(string A_0)
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
		spr\u1D66 result;
		this.ᜂ.TryGetValue(A_0.ToUpperInvariant(), out result);
		return result;
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x001673BC File Offset: 0x001663BC
	private void ᜀ(Uri A_0, spr\u251B A_1)
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
		A_1.ᜀ(this.ᜆ, A_0);
		this.ᜆ = A_1;
		this.ᜆ.ᜂ();
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x00167418 File Offset: 0x00166418
	private void ᜉ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜆ.ᜃ();
				num = 4;
				continue;
			case 2:
				goto IL_96;
			case 3:
				IL_7E:
				if (this.ᜆ.ᜑ() != null)
				{
					num = 2;
					continue;
				}
				goto IL_B0;
			case 4:
				goto IL_48;
			}
			if (this.ᜆ != null)
			{
				num = 1;
				continue;
			}
			IL_48:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_7E;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 3;
				break;
			}
		}
		IL_96:
		this.ᜆ = this.ᜆ.ᜑ();
		return;
		IL_B0:
		this.ᜆ = null;
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x001674DC File Offset: 0x001664DC
	private void ᜈ()
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				char c = this.ᜆ.ᜎ();
				int num = 7;
				for (;;)
				{
					char c2;
					switch (num)
					{
					case 0:
						num = 14;
						continue;
					case 1:
						this.ᜉ();
						num = 17;
						continue;
					case 2:
					{
						if (c2 != '%')
						{
							num = 12;
							continue;
						}
						spr\u251B a_2 = this.ᜁ(ClipboardData.b("坶瑸煺瑼", a_));
						num = 11;
						continue;
					}
					case 3:
						num = 13;
						continue;
					case 4:
						goto IL_263;
					case 5:
						num = 6;
						continue;
					case 6:
						switch (c2)
						{
						case '\t':
						case '\n':
						case '\r':
							goto IL_2D5;
						case '\v':
						case '\f':
							goto IL_216;
						default:
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 7:
						goto IL_263;
					case 8:
						return;
					case 9:
						if (c2 == '￿')
						{
							goto IL_2C3;
						}
						goto IL_216;
					case 10:
						goto IL_263;
					case 11:
						try
						{
							spr\u251B a_2;
							this.ᜀ(this.ᜆ.ᜁ(), a_2);
							goto IL_14B;
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message + this.ᜆ.ᜇ());
							goto IL_14B;
						}
						goto IL_216;
						IL_14B:
						c = this.ᜆ.ᜎ();
						num = 10;
						continue;
					case 12:
						num = 15;
						continue;
					case 13:
						if (c2 != ' ')
						{
							num = 0;
							continue;
						}
						goto IL_2D5;
					case 14:
						goto IL_216;
					case 15:
						if (c2 != '<')
						{
							num = 19;
							continue;
						}
						this.ᜇ();
						c = this.ᜆ.ᜂ();
						num = 16;
						continue;
					case 16:
						goto IL_263;
					case 17:
						if (this.ᜆ == null)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2C3;
							}
							if (false)
							{
							}
							num = 8;
							continue;
						}
						c = this.ᜆ.ᜎ();
						num = 18;
						continue;
					case 18:
						goto IL_263;
					case 19:
						num = 9;
						continue;
					case 20:
						goto IL_263;
					case 21:
						if (c2 <= ' ')
						{
							num = 5;
							continue;
						}
						num = 2;
						continue;
					}
					break;
					IL_216:
					this.ᜆ.ᜀ(ClipboardData.b("≶᝸Ṻռཾꮊﺚ뾞蚠\ud8a2閤\udaa6躨", a_), c);
					num = 20;
					continue;
					IL_263:
					c2 = c;
					num = 21;
					continue;
					IL_2C3:
					num = 1;
					continue;
					IL_2D5:
					c = this.ᜆ.ᜂ();
					num = 4;
				}
			}
			return;
		}
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x001677EC File Offset: 0x001667EC
	private void ᜇ()
	{
		int a_ = 15;
		string text;
		for (;;)
		{
			char c = this.ᜆ.ᜂ();
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string a;
					if ((a = text) != null)
					{
						num = 2;
						continue;
					}
					goto IL_298;
				}
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("ぴ㭶㱸㙺㡼ㅾ햀", a_)))
					{
						num = 15;
						continue;
					}
					goto IL_1C5;
				}
				case 2:
					num = 10;
					continue;
				case 3:
					goto IL_252;
				case 4:
					goto IL_1E1;
				case 5:
					if (c != '-')
					{
						num = 14;
						continue;
					}
					goto IL_1E6;
				case 6:
					c = this.ᜆ.ᜂ();
					num = 5;
					continue;
				case 7:
					if (c != '!')
					{
						num = 12;
						continue;
					}
					c = this.ᜆ.ᜂ();
					num = 9;
					continue;
				case 8:
					if (c == '[')
					{
						num = 3;
						continue;
					}
					text = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("啴究獸牺", a_), true);
					num = 0;
					continue;
				case 9:
					if (c == '-')
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 10:
				{
					string a;
					if (!(a == ClipboardData.b("ぴ㥶⵸㉺⥼♾", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_12C;
				}
				case 11:
					goto IL_1C3;
				case 12:
					goto IL_7D;
				case 13:
				{
					string a;
					if (!(a == ClipboardData.b("㑴⍶⵸㝺㑼Ȿ햀", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_190;
				}
				case 14:
					this.ᜆ.ᜀ(ClipboardData.b("ぴྲྀॸṺṼ୾Ꞇﶒ랖뺘Ꞛ벜늞負蒢薤얦\udca8\udfaa趬즮\udeb0욲\udbb4펶馸삺趼슾", a_), c);
					goto IL_1B8;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B8;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 16:
					num = 4;
					continue;
				case 17:
					num = 1;
					continue;
				}
				break;
				IL_1B8:
				num = 11;
			}
		}
		IL_7D:
		if (true)
		{
		}
		this.ᜆ.ᜁ(ClipboardData.b("㍴ᡶ౸ᕺ᥼彾Ꚁ떄惘꺈꞊권떔ﲞ좠춢스螦춨캪캬쎮킰솲풴쎶킸풺펼龾닀럂꓄뗆뷈ꋊꏌ꣎ꓒ볔ꏖ뇘ﯚ﫜샠쓢", a_));
		return;
		IL_12C:
		this.ᜂ();
		return;
		IL_190:
		this.ᜀ();
		return;
		IL_1C3:
		goto IL_1E6;
		IL_1C5:
		this.ᜁ();
		return;
		IL_1E1:
		goto IL_298;
		IL_1E6:
		this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("㙴ᡶᑸᙺ᡼ᅾ", a_), ClipboardData.b("塴婶䝸", a_));
		return;
		IL_252:
		this.ᜅ();
		return;
		IL_298:
		this.ᜆ.ᜀ(ClipboardData.b("㱴᥶ླྀ᩺ᅼᙾꎂﶎﲔ뮚몜ꎞ肠\ud8a2閤\udaa6躨薪趬辮쮲어튶\udab8쾺풼톾ꛀ苆蟈鿊蓌鯎裐倫ﻘ黚釜髞고ꛢꯤ돦컨쯪苬鷮퇰퓲듴ꏶ그럺듼곾唀␂⬄", a_), text);
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x00167AAC File Offset: 0x00166AAC
	private char ᜆ()
	{
		char c;
		for (;;)
		{
			c = this.ᜆ.ᜎ();
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					goto IL_36;
				case 2:
					if (c != '-')
					{
						num = 3;
						continue;
					}
					c = this.ᜀ(true);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_79;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					return c;
				}
				break;
				IL_36:
				num = 2;
				continue;
				IL_79:
				goto IL_36;
			}
		}
		return c;
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x00167B40 File Offset: 0x00166B40
	private char ᜀ(bool A_0)
	{
		int a_ = 1;
		for (;;)
		{
			char c = this.ᜆ.ᜂ();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (c != '-')
					{
						num = 1;
						continue;
					}
					goto IL_AE;
				case 1:
					this.ᜆ.ᜀ(ClipboardData.b("≦ᅨ᭪࡬౮հᩲ᭴ၶ奸᡺ቼቾꦈﺒﲔﲘ붜뢞負躢芤螦쮨\udeaa\ud9ac辮ힰ\udcb2살\ud9b6\uddb8鮺욼达변", a_), c);
					num = 2;
					continue;
				case 2:
					goto IL_8F;
				case 3:
					goto IL_91;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_0)
						{
							num = 3;
							continue;
						}
						goto IL_AE;
					}
					break;
				}
				break;
				IL_91:
				num = 0;
			}
		}
		IL_8F:
		IL_AE:
		this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("⩦ࡨᥪ٬ᩮŰ卲㙴ᡶᑸᙺ᡼ᅾ", a_), ClipboardData.b("䩦䑨", a_));
		return this.ᜆ.ᜌ();
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x00167C40 File Offset: 0x00166C40
	private void ᜅ()
	{
		int a_ = 10;
		string text;
		for (;;)
		{
			this.ᜆ.ᜂ();
			text = this.ᜂ(ClipboardData.b("⭯", a_));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					if (true)
					{
					}
					if (string.Equals(text, ClipboardData.b("㥯㱱㝳㩵⵷㹹㥻", a_), StringComparison.OrdinalIgnoreCase))
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					goto IL_CD;
				case 3:
					if (string.Equals(text, ClipboardData.b("㥯㕱㩳㥵⩷㽹", a_), StringComparison.OrdinalIgnoreCase))
					{
						num = 2;
						continue;
					}
					goto IL_D6;
				}
				break;
			}
		}
		IL_7B:
		goto IL_CF;
		IL_CD:
		this.ᜃ();
		return;
		IL_CF:
		this.ᜄ();
		return;
		IL_D6:
		this.ᜆ.ᜀ(ClipboardData.b("╯ᱱݳ͵ࡷ੹፻౽ꚅﺋ뒓ﶗ蓮쾟첡蒣튥톧\udaa9즫躭鞯즱蒳쮵龷", a_), text);
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x00167D40 File Offset: 0x00166D40
	private void ᜄ()
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(ClipboardData.b("⹦ݨࡪŬᩮᕰᙲ啴⑶ᱸ᡺ॼᙾ", a_));
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x00167D98 File Offset: 0x00166D98
	private void ᜃ()
	{
		int a_ = 7;
		for (;;)
		{
			char c = this.ᜆ.ᜌ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_31;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31;
					default:
						if (false)
						{
						}
						this.ᜆ.ᜀ(ClipboardData.b("⡬ᝮŰᙲᙴͶၸᕺ᩼彾Ꚁ\ud882ꊄꞆﺊ歷꾎ﲒ練ﶘ뮚꾞\udca0", a_), c);
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_93;
				}
				break;
				IL_31:
				if (c == '[')
				{
					goto IL_95;
				}
				num = 1;
			}
		}
		IL_93:
		IL_95:
		this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("⹬nὰᝲᱴͶၸᑺ፼Ṿꎂ횄ﾊﾐ", a_), ClipboardData.b("ぬ㉮佰", a_));
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x00167E68 File Offset: 0x00166E68
	private string ᜂ(string A_0)
	{
		int a_ = 5;
		spr\u251B spr_u251B;
		for (;;)
		{
			char c = this.ᜆ.ᜌ();
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					spr_u251B = this.ᜁ(A_0);
					c = this.ᜆ.ᜎ();
					goto IL_9C;
				case 1:
					goto IL_BA;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9C;
					default:
						if (false)
						{
						}
						if (c == '%')
						{
							num = 0;
							continue;
						}
						goto IL_BC;
					}
					break;
				case 3:
					if (!spr_u251B.ᜄ())
					{
						num = 1;
						continue;
					}
					goto IL_68;
				}
				break;
				IL_9C:
				num = 3;
			}
		}
		IL_68:
		return spr_u251B.\u1713().Trim();
		IL_BA:
		throw new NotSupportedException(ClipboardData.b("⹪ᕬ᭮ᑰŲ᭴ᙶᕸ孺ർṾﶈﾌ꾎ﶒﺖ붜쒠킢쪤쮦\udca8\udfaa쒬삮\udfb0", a_));
		IL_BC:
		return this.ᜆ.ᜀ(this.ᜅ, A_0, true);
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x00167F50 File Offset: 0x00166F50
	private spr\u251B ᜁ(string A_0)
	{
		int a_ = 3;
		string a_2;
		for (;;)
		{
			this.ᜆ.ᜂ();
			a_2 = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("剨", a_) + A_0, false);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B6;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						if (false)
						{
						}
						this.ᜆ.ᜂ();
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_58;
				}
				break;
				IL_58:
				if (this.ᜆ.ᜎ() != ';')
				{
					goto IL_B8;
				}
				num = 1;
			}
		}
		IL_B6:
		IL_B8:
		return this.ᜀ(a_2);
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x00168020 File Offset: 0x00167020
	private spr\u251B ᜀ(string A_0)
	{
		int a_ = 7;
		spr\u251B spr_u251B;
		for (;;)
		{
			spr_u251B = null;
			this.ᜃ.TryGetValue(A_0, out spr_u251B);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return spr_u251B;
				case 1:
					goto IL_36;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						this.ᜆ.ᜀ(ClipboardData.b("㽬੮ᝰᙲݴቶ᝸᡺᡼彾ꖄﶒ릘ﲜ삠캢삤펦첨\ud9aa趬쪮\udfb0잲\udcb4쎶삸鮺骼쒾뻂", a_), A_0);
						num = 0;
						continue;
					}
					break;
				}
				break;
				IL_36:
				if (true)
				{
				}
				if (spr_u251B != null)
				{
					return spr_u251B;
				}
				num = 2;
			}
		}
		return spr_u251B;
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x001680C8 File Offset: 0x001670C8
	public Dictionary<string, spr\u251B> ᜊ()
	{
		Dictionary<string, spr\u251B> dictionary = new Dictionary<string, spr\u251B>();
		using (Dictionary<string, spr\u251B>.ValueCollection.Enumerator enumerator = this.ᜄ.Values.GetEnumerator())
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					spr\u251B spr_u251B = enumerator.Current;
					dictionary[spr_u251B.\u1713()] = spr_u251B;
					num = 0;
					continue;
				}
				case 3:
					goto IL_9D;
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
						break;
					}
					num = 3;
					continue;
				}
				IL_5E:
				num = 1;
				continue;
				goto IL_5E;
			}
			IL_9D:;
		}
		if (true)
		{
		}
		return dictionary;
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x001681A8 File Offset: 0x001671A8
	private void ᜂ()
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			spr\u251B spr_u251B;
			for (;;)
			{
				char c = this.ᜆ.ᜌ();
				bool flag = c == '%';
				int num = 12;
				for (;;)
				{
					string a_3;
					string a_4;
					switch (num)
					{
					case 0:
						c = this.ᜆ();
						num = 34;
						continue;
					case 1:
						this.ᜆ.ᜀ(ClipboardData.b("㝱౳ٵᵷ᥹ࡻ᝽ꒃ黎ﶏ늑ﶓﶗ욟쮡솣풥袧용얫\udaad햯삱햳\udab5颷\ud8b9즻쪽꓁ꯃ독ꛇ껉ꯏ꧓", a_), c);
						num = 29;
						continue;
					case 2:
						this.ᜆ.ᜂ();
						c = this.ᜆ.ᜌ();
						num = 31;
						continue;
					case 3:
						c = this.ᜆ.ᜌ();
						num = 30;
						continue;
					case 4:
					{
						string text;
						this.ᜆ.ᜀ(ClipboardData.b("㭱ᩳu᥷ᙹᕻ᩽ꁿﲃﲏ늑ﶓﶗ욟쮡솣풥袧趩힫麭춯閱骳隵颷ﾹ쒻캽ꖿꇁ귃ꣅ꿇黍藏郑飓鿕鯗﷙ﳛ뇝鋟싡쏣뗥뇧맩룫꯭뷯헱\udaf3", a_), text);
						num = 37;
						continue;
					}
					case 5:
					{
						c = this.ᜆ.ᜌ();
						string a_2 = this.ᜆ.ᜀ(this.ᜅ, c);
						spr_u251B = new spr\u251B(a_3, a_2);
						string text2;
						spr_u251B.ᜂ(text2);
						num = 9;
						continue;
					}
					case 6:
						goto IL_11F;
					case 7:
					{
						string text;
						if (!string.Equals(text, ClipboardData.b("ⅱ⵳╵ⱷ㽹ㅻ", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 4;
							continue;
						}
						goto IL_11F;
					}
					case 8:
						if (c != '"')
						{
							num = 20;
							continue;
						}
						goto IL_36B;
					case 9:
						goto IL_536;
					case 10:
						if (flag)
						{
							num = 17;
							continue;
						}
						goto IL_58C;
					case 11:
					{
						if (c == '\'')
						{
							num = 33;
							continue;
						}
						a_4 = null;
						string text = null;
						string text2 = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("剱祳籵煷", a_), true);
						num = 23;
						continue;
					}
					case 12:
						if (flag)
						{
							goto IL_E3;
						}
						goto IL_4E2;
					case 13:
						goto IL_218;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E3;
						default:
							if (false)
							{
							}
							goto IL_11F;
						}
						break;
					case 15:
						goto IL_3E6;
					case 16:
						if (c == '-')
						{
							num = 0;
							continue;
						}
						goto IL_2A3;
					case 17:
						goto IL_236;
					case 18:
						if (c == '\'')
						{
							num = 35;
							continue;
						}
						num = 24;
						continue;
					case 19:
						if (c != '"')
						{
							if (true)
							{
							}
							num = 21;
							continue;
						}
						goto IL_48C;
					case 20:
						num = 11;
						continue;
					case 21:
						num = 18;
						continue;
					case 22:
						goto IL_536;
					case 23:
					{
						string text2;
						if (spr\u251B.ᜀ(text2))
						{
							num = 5;
							continue;
						}
						string text = text2;
						num = 25;
						continue;
					}
					case 24:
						if (c != '>')
						{
							num = 1;
							continue;
						}
						goto IL_3E6;
					case 25:
					{
						string text;
						if (string.Equals(text, ClipboardData.b("≱ⅳ㑵㑷㍹㽻", a_), StringComparison.OrdinalIgnoreCase))
						{
							num = 3;
							continue;
						}
						num = 7;
						continue;
					}
					case 26:
						num = 36;
						continue;
					case 27:
						goto IL_2DB;
					case 28:
						goto IL_536;
					case 29:
						goto IL_3E6;
					case 30:
						if (c != '"')
						{
							num = 26;
							continue;
						}
						goto IL_2DB;
					case 31:
						goto IL_4E2;
					case 32:
						if (c != '>')
						{
							num = 38;
							continue;
						}
						goto IL_218;
					case 33:
						goto IL_36B;
					case 34:
						goto IL_2A3;
					case 35:
						goto IL_48C;
					case 36:
						if (c == '\'')
						{
							num = 27;
							continue;
						}
						this.ᜆ.ᜀ(ClipboardData.b("㝱౳ٵᵷ᥹ࡻ᝽ꒃﶇ늑ﶓﶗ욟쮡솣풥袧용얫\udaad햯삱햳\udab5颷\ud8b9즻쪽꓁ꯃ독ꛇ껉ꯏ꧓", a_), c);
						num = 14;
						continue;
					case 37:
						goto IL_11F;
					case 38:
						this.ᜆ.ᜀ(ClipboardData.b("㝱౳ٵᵷ᥹ࡻ᝽ꒃ겋늑肟욡솣얥쒧쮩\udeab쾭쒯\udbb1\udbb3\ud8b5颷鶹芻馽ꃁ뇃닅곉ꏋ믍뻏뛑ꏗꇛ利", a_), c);
						num = 13;
						continue;
					}
					break;
					IL_E3:
					num = 2;
					continue;
					IL_11F:
					string a_5 = null;
					c = this.ᜆ.ᜌ();
					num = 19;
					continue;
					IL_218:
					num = 10;
					continue;
					IL_2A3:
					num = 32;
					continue;
					IL_2DB:
					a_4 = this.ᜆ.ᜀ(this.ᜅ, c);
					num = 6;
					continue;
					IL_36B:
					string a_6 = this.ᜆ.ᜀ(this.ᜅ, c);
					spr_u251B = new spr\u251B(a_3, a_6);
					num = 28;
					continue;
					IL_3E6:
					spr_u251B = new spr\u251B(a_3, a_4, a_5, this.ᜆ.ᜊ());
					num = 22;
					continue;
					IL_48C:
					a_5 = this.ᜆ.ᜀ(this.ᜅ, c);
					num = 15;
					continue;
					IL_4E2:
					a_3 = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("剱祳籵煷", a_), true);
					c = this.ᜆ.ᜌ();
					spr_u251B = null;
					num = 8;
					continue;
					IL_536:
					c = this.ᜆ.ᜌ();
					num = 16;
				}
			}
			IL_236:
			this.ᜃ.Add(spr_u251B.\u1712(), spr_u251B);
			return;
			IL_58C:
			this.ᜄ.Add(spr_u251B.\u1712(), spr_u251B);
			return;
		}
		}
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x00168754 File Offset: 0x00167754
	private void ᜁ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				char c = this.ᜆ.ᜌ();
				string[] array = this.ᜀ(c, true);
				c = char.ToUpperInvariant(this.ᜆ.ᜌ());
				bool a_2 = false;
				bool a_3 = false;
				int num = 10;
				for (;;)
				{
					string[] a_4;
					int num2;
					string[] array2;
					sprᠭ a_5;
					string[] a_6;
					switch (num)
					{
					case 0:
						goto IL_12B;
					case 1:
						num = 33;
						continue;
					case 2:
						if (c == '-')
						{
							num = 18;
							continue;
						}
						this.ᜆ.ᜀ(ClipboardData.b("㩲᭴Ŷᡸ᝺ᑼ᭾ꆀﲄﶈ꾎떔낖ꮚ뢞", a_), c);
						num = 22;
						continue;
					case 3:
						goto IL_2AD;
					case 4:
						if (c == '-')
						{
							num = 8;
							continue;
						}
						goto IL_12B;
					case 5:
						if (c != '(')
						{
							num = 35;
							continue;
						}
						goto IL_1E9;
					case 6:
						return;
					case 7:
						if (c == '-')
						{
							num = 11;
							continue;
						}
						goto IL_387;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3DC;
						default:
							if (false)
							{
							}
							c = this.ᜆ();
							num = 0;
							continue;
						}
						break;
					case 9:
						if (c != 'O')
						{
							num = 30;
							continue;
						}
						goto IL_313;
					case 10:
						if (c != 'O')
						{
							num = 1;
							continue;
						}
						goto IL_47B;
					case 11:
						c = this.ᜆ();
						num = 26;
						continue;
					case 12:
						goto IL_3DC;
					case 13:
						goto IL_47B;
					case 14:
						goto IL_299;
					case 15:
						goto IL_409;
					case 16:
						if (c != '>')
						{
							num = 23;
							continue;
						}
						goto IL_299;
					case 17:
						if (c == '(')
						{
							num = 19;
							continue;
						}
						num = 2;
						continue;
					case 18:
						c = this.ᜀ(false);
						num = 15;
						continue;
					case 19:
						a_4 = this.ᜀ(c, true);
						c = this.ᜆ.ᜌ();
						num = 24;
						continue;
					case 20:
						if (c == '+')
						{
							num = 27;
							continue;
						}
						goto IL_106;
					case 21:
						goto IL_2AD;
					case 22:
						goto IL_409;
					case 23:
						this.ᜆ.ᜀ(ClipboardData.b("㙲൴ݶᱸ᡺ॼᙾꖄ권뎒킔\udb96\udc98횚\ud89c톞莢솤슦쪨잪첬\uddae킰잲\udcb4\ud8b6ힸ鮺骼膾Ꞔ닆뷈ꯌꃎꓐ뷒뇔ﻘꃚꋞ웠", a_), c);
						num = 14;
						continue;
					case 24:
						if (true)
						{
						}
						goto IL_409;
					case 25:
						if (c == '-')
						{
							num = 31;
							continue;
						}
						goto IL_14D;
					case 26:
						goto IL_387;
					case 27:
						c = this.ᜆ.ᜂ();
						num = 5;
						continue;
					case 28:
						goto IL_14D;
					case 29:
					{
						if (num2 >= array2.Length)
						{
							num = 6;
							continue;
						}
						string text = array2[num2];
						string text2 = text.ToUpperInvariant();
						this.ᜂ.Add(text2, new spr\u1D66(text2, a_2, a_3, a_5, a_6, a_4));
						num2++;
						num = 3;
						continue;
					}
					case 30:
						num = 25;
						continue;
					case 31:
						goto IL_313;
					case 32:
						c = this.ᜆ.ᜂ();
						num = 17;
						continue;
					case 33:
						if (c == '-')
						{
							num = 13;
							continue;
						}
						goto IL_14D;
					case 34:
						if (c == '-')
						{
							num = 32;
							continue;
						}
						goto IL_409;
					case 35:
						this.ᜆ.ᜀ(ClipboardData.b("㙲൴ݶᱸ᡺ॼᙾꖄ搜朗杖練뮚ﺞ철욢薤삦\udba8쒪\ud8ac\udfae", a_), c);
						num = 12;
						continue;
					case 36:
						goto IL_106;
					}
					break;
					IL_106:
					num = 7;
					continue;
					IL_12B:
					num = 20;
					continue;
					IL_14D:
					c = this.ᜆ.ᜌ();
					a_5 = this.ᜁ(c);
					c = this.ᜆ.ᜌ();
					a_4 = null;
					a_6 = null;
					num = 34;
					continue;
					IL_1E9:
					a_6 = this.ᜀ(c, true);
					c = this.ᜆ.ᜌ();
					num = 36;
					continue;
					IL_3DC:
					goto IL_1E9;
					IL_299:
					array2 = array;
					num2 = 0;
					num = 21;
					continue;
					IL_2AD:
					num = 29;
					continue;
					IL_313:
					a_3 = (c == 'O');
					c = this.ᜆ.ᜂ();
					num = 28;
					continue;
					IL_387:
					num = 16;
					continue;
					IL_409:
					num = 4;
					continue;
					IL_47B:
					a_2 = (c == 'O');
					this.ᜆ.ᜂ();
					c = char.ToUpperInvariant(this.ᜆ.ᜌ());
					num = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x00168C3C File Offset: 0x00167C3C
	private string[] ᜀ(char A_0, bool A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			ArrayList arrayList;
			for (;;)
			{
				arrayList = new ArrayList();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u251B a_2 = this.ᜁ(spr\u2057.ᜇ);
						this.ᜀ(this.ᜆ.ᜁ(), a_2);
						this.ᜀ(arrayList, A_1);
						this.ᜉ();
						A_0 = this.ᜆ.ᜎ();
						num = 7;
						continue;
					}
					case 1:
					{
						if (A_0 == '(')
						{
							num = 3;
							continue;
						}
						string text = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("啴究獸牺", a_), A_1);
						text = text.ToUpperInvariant();
						arrayList.Add(text);
						num = 14;
						continue;
					}
					case 2:
						goto IL_11A;
					case 3:
						A_0 = this.ᜆ.ᜂ();
						A_0 = this.ᜆ.ᜌ();
						num = 9;
						continue;
					case 4:
					{
						if (A_0 == '%')
						{
							num = 0;
							continue;
						}
						string text2 = this.ᜆ.ᜀ(this.ᜅ, spr\u2057.ᜇ, A_1);
						text2 = text2.ToUpperInvariant();
						arrayList.Add(text2);
						num = 5;
						continue;
					}
					case 5:
						goto IL_225;
					case 6:
						if (A_0 != '|')
						{
							num = 10;
							continue;
						}
						goto IL_14A;
					case 7:
						goto IL_225;
					case 8:
						goto IL_85;
					case 9:
						goto IL_85;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23A;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 11:
						this.ᜆ.ᜂ();
						num = 2;
						continue;
					case 12:
						if (A_0 == ')')
						{
							num = 11;
							continue;
						}
						A_0 = this.ᜆ.ᜌ();
						num = 4;
						continue;
					case 13:
						goto IL_14A;
					case 14:
						goto IL_1A4;
					case 15:
						if (A_0 == ',')
						{
							num = 13;
							continue;
						}
						goto IL_85;
					}
					break;
					IL_85:
					num = 12;
					continue;
					IL_14A:
					A_0 = this.ᜆ.ᜂ();
					num = 8;
					continue;
					IL_23A:
					num = 6;
					continue;
					IL_225:
					if (true)
					{
					}
					A_0 = this.ᜆ.ᜌ();
					goto IL_23A;
				}
			}
			IL_11A:
			IL_1A4:
			return (string[])arrayList.ToArray(typeof(string));
		}
		}
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x00168EDC File Offset: 0x00167EDC
	private void ᜀ(ArrayList A_0, bool A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			char c = this.ᜆ.ᜎ();
			c = this.ᜆ.ᜌ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_162;
				case 1:
					if (c == '|')
					{
						num = 4;
						continue;
					}
					goto IL_FC;
				case 2:
					goto IL_FC;
				case 3:
					if (c == '￿')
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 4:
					c = this.ᜆ.ᜂ();
					c = this.ᜆ.ᜌ();
					num = 9;
					continue;
				case 5:
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_162;
					default:
						if (false)
						{
						}
						goto IL_164;
					}
					break;
				case 7:
				{
					spr\u251B a_ = this.ᜁ(spr\u2057.ᜇ);
					this.ᜀ(this.ᜆ.ᜁ(), a_);
					this.ᜀ(A_0, A_1);
					this.ᜉ();
					c = this.ᜆ.ᜎ();
					num = 0;
					continue;
				}
				case 8:
				{
					if (c == '%')
					{
						num = 7;
						continue;
					}
					string text = this.ᜆ.ᜀ(this.ᜅ, spr\u2057.ᜇ, true);
					text = text.ToUpperInvariant();
					A_0.Add(text);
					num = 6;
					continue;
				}
				case 9:
					goto IL_FC;
				}
				break;
				IL_FC:
				num = 3;
				continue;
				IL_164:
				c = this.ᜆ.ᜌ();
				num = 1;
				continue;
				IL_162:
				goto IL_164;
			}
		}
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x0016907C File Offset: 0x0016807C
	private sprᠭ ᜁ(char A_0)
	{
		sprᠭ sprᠭ;
		for (;;)
		{
			sprᠭ = new sprᠭ();
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != '+')
					{
						num = 7;
						continue;
					}
					goto IL_80;
				case 1:
					goto IL_80;
				case 2:
					num = 0;
					continue;
				case 3:
					return sprᠭ;
				case 4:
					if (A_0 == '*')
					{
						num = 1;
						continue;
					}
					return sprᠭ;
				case 5:
					if (A_0 != '?')
					{
						num = 2;
						continue;
					}
					goto IL_80;
				case 6:
				{
					if (A_0 == '%')
					{
						num = 12;
						continue;
					}
					string a_ = this.ᜂ(spr\u2057.ᜈ);
					sprᠭ.ᜁ(a_);
					num = 10;
					continue;
				}
				case 7:
					num = 4;
					continue;
				case 8:
					this.ᜆ.ᜂ();
					this.ᜀ(')', sprᠭ);
					A_0 = this.ᜆ.ᜂ();
					num = 5;
					continue;
				case 9:
					if (A_0 == '(')
					{
						num = 8;
						continue;
					}
					goto IL_5C;
				case 10:
					return sprᠭ;
				case 11:
					return sprᠭ;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5C;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						spr\u251B a_2 = this.ᜁ(spr\u2057.ᜈ);
						this.ᜀ(this.ᜆ.ᜁ(), a_2);
						sprᠭ = this.ᜁ(this.ᜆ.ᜎ());
						this.ᜉ();
						num = 11;
						continue;
					}
					}
					break;
				}
				break;
				IL_5C:
				num = 6;
				continue;
				IL_80:
				sprᠭ.ᜀ(A_0);
				this.ᜆ.ᜂ();
				num = 3;
			}
		}
		return sprᠭ;
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x0016923C File Offset: 0x0016823C
	private void ᜀ(char A_0, sprᠭ A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_1.ᜁ();
				char c = this.ᜆ.ᜎ();
				c = this.ᜆ.ᜌ();
				int num2 = 17;
				for (;;)
				{
					string text;
					switch (num2)
					{
					case 0:
						num2 = 11;
						continue;
					case 1:
						goto IL_518;
					case 2:
						if (c != '+')
						{
							num2 = 24;
							continue;
						}
						goto IL_2B3;
					case 3:
						if (true)
						{
						}
						if (c != '?')
						{
							num2 = 32;
							continue;
						}
						goto IL_2B3;
					case 4:
						this.ᜆ.ᜁ(ClipboardData.b("╥ݧѩᡫ୭ṯٱ味㭵᝷ṹ᥻ችꁿꢇ揄낏秊ﾙ", a_));
						num2 = 10;
						continue;
					case 5:
						A_1.ᜂ();
						this.ᜆ.ᜂ();
						c = this.ᜆ.ᜌ();
						num2 = 1;
						continue;
					case 6:
						goto IL_518;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39D;
						default:
						{
							if (false)
							{
							}
							spr\u251B a_2 = this.ᜁ(spr\u2057.ᜉ);
							this.ᜀ(this.ᜆ.ᜁ(), a_2);
							this.ᜀ(char.MaxValue, A_1);
							this.ᜉ();
							c = this.ᜆ.ᜌ();
							num2 = 29;
							continue;
						}
						}
						break;
					case 8:
						if (c == ')')
						{
							num2 = 42;
							continue;
						}
						num2 = 15;
						continue;
					case 9:
						goto IL_53C;
					case 10:
						goto IL_4D7;
					case 11:
						if (c != '|')
						{
							num2 = 31;
							continue;
						}
						goto IL_482;
					case 12:
						if (c != '+')
						{
							num2 = 27;
							continue;
						}
						goto IL_320;
					case 13:
						if (c == '*')
						{
							num2 = 19;
							continue;
						}
						A_1.ᜀ(text);
						c = this.ᜆ.ᜌ();
						num2 = 6;
						continue;
					case 14:
						if (c == '?')
						{
							num2 = 38;
							continue;
						}
						goto IL_432;
					case 15:
						if (c != ',')
						{
							num2 = 0;
							continue;
						}
						goto IL_482;
					case 16:
						if (c == A_0)
						{
							num2 = 33;
							continue;
						}
						goto IL_344;
					case 17:
						goto IL_518;
					case 18:
						if (c == '￿')
						{
							num2 = 4;
							continue;
						}
						goto IL_4D7;
					case 19:
						goto IL_2B3;
					case 20:
						if (A_1.ᜃ() < num)
						{
							num2 = 44;
							continue;
						}
						goto IL_53C;
					case 21:
						goto IL_482;
					case 22:
						c = this.ᜆ.ᜂ();
						text = ClipboardData.b("䕥", a_) + this.ᜆ.ᜀ(this.ᜅ, spr\u2057.ᜉ, true);
						num2 = 45;
						continue;
					case 23:
						goto IL_5A8;
					case 24:
						num2 = 13;
						continue;
					case 25:
						goto IL_518;
					case 26:
						if (c == '#')
						{
							num2 = 22;
							continue;
						}
						text = this.ᜆ.ᜀ(this.ᜅ, spr\u2057.ᜉ, true);
						num2 = 23;
						continue;
					case 27:
						num2 = 14;
						continue;
					case 28:
						goto IL_432;
					case 29:
						goto IL_518;
					case 30:
						return;
					case 31:
						num2 = 36;
						continue;
					case 32:
						num2 = 2;
						continue;
					case 33:
						num2 = 40;
						continue;
					case 34:
						if (c != '*')
						{
							num2 = 35;
							continue;
						}
						goto IL_320;
					case 35:
						num2 = 12;
						continue;
					case 36:
						if (c == '&')
						{
							num2 = 21;
							continue;
						}
						num2 = 26;
						continue;
					case 37:
						if (c == '(')
						{
							num2 = 5;
							continue;
						}
						num2 = 8;
						continue;
					case 38:
						goto IL_320;
					case 39:
						goto IL_518;
					case 40:
						if (A_1.ᜁ() <= num)
						{
							num2 = 30;
							continue;
						}
						goto IL_344;
					case 41:
						goto IL_518;
					case 42:
						c = this.ᜆ.ᜂ();
						goto IL_39D;
					case 43:
						if (c == '%')
						{
							num2 = 7;
							continue;
						}
						num2 = 37;
						continue;
					case 44:
						this.ᜆ.ᜁ(ClipboardData.b("㙥१ᡩ൫ͭᕯٱᅳѵ塷όቻ੽ﶃꚅﾏ뒓ﮝ肟쎡蒣횥즧\ud8a9즫삭邯\uddb1솳습쮷펹\ud8bb\udbbdꯁ냃믇ꏋ맍뻏ꟓ뗕럗꫙맛", a_));
						num2 = 9;
						continue;
					case 45:
						goto IL_5A8;
					}
					break;
					IL_2B3:
					A_1.ᜂ();
					A_1.ᜀ(text);
					A_1.ᜀ(c);
					A_1.ᜃ();
					this.ᜆ.ᜂ();
					c = this.ᜆ.ᜌ();
					num2 = 39;
					continue;
					IL_320:
					A_1.ᜀ(c);
					c = this.ᜆ.ᜂ();
					num2 = 28;
					continue;
					IL_344:
					num2 = 18;
					continue;
					IL_39D:
					num2 = 34;
					continue;
					IL_432:
					num2 = 20;
					continue;
					IL_482:
					A_1.ᜁ(c);
					this.ᜆ.ᜂ();
					c = this.ᜆ.ᜌ();
					num2 = 41;
					continue;
					IL_4D7:
					num2 = 43;
					continue;
					IL_518:
					num2 = 16;
					continue;
					IL_53C:
					c = this.ᜆ.ᜌ();
					num2 = 25;
					continue;
					IL_5A8:
					text = text.ToUpperInvariant();
					c = this.ᜆ.ᜎ();
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x00169834 File Offset: 0x00168834
	private void ᜀ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				char a_2 = this.ᜆ.ᜌ();
				string[] array = this.ᜀ(a_2, true);
				Dictionary<string, sprᜏ> a_3 = new Dictionary<string, sprᜏ>();
				this.ᜀ(a_3, '>');
				string[] array2 = array;
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					string text;
					spr\u1D66 spr_u1D;
					switch (num2)
					{
					case 0:
						goto IL_EA;
					case 1:
						goto IL_EC;
					case 2:
						if (!this.ᜂ.TryGetValue(text, out spr_u1D))
						{
							num2 = 0;
							continue;
						}
						goto IL_71;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EA;
						default:
							goto IL_122;
						}
						break;
					case 4:
						goto IL_EC;
					case 5:
						if (num >= array2.Length)
						{
							num2 = 3;
							continue;
						}
						text = array2[num];
						num2 = 2;
						continue;
					case 6:
						goto IL_71;
					}
					break;
					IL_71:
					spr_u1D.ᜀ(a_3);
					num++;
					num2 = 1;
					continue;
					IL_EA:
					this.ᜆ.ᜀ(ClipboardData.b("⩪㥬㭮㵰㩲♴⍶奸ॺ᡼᥾ﺌ꾎ﶒﾘ爵얠莢鎲캴螶쒸", a_), text);
					num2 = 6;
					continue;
					IL_EC:
					num2 = 5;
				}
			}
			IL_122:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x00169974 File Offset: 0x00168974
	private void ᜀ(Dictionary<string, sprᜏ> A_0, char A_1)
	{
		for (;;)
		{
			char c = this.ᜆ.ᜌ();
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_F6;
				case 1:
					if (c == A_1)
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 2:
					goto IL_132;
				case 3:
				{
					spr\u251B a_ = this.ᜁ(spr\u2057.ᜊ);
					this.ᜀ(this.ᜆ.ᜁ(), a_);
					this.ᜀ(A_0, char.MaxValue);
					this.ᜉ();
					c = this.ᜆ.ᜌ();
					num = 10;
					continue;
				}
				case 4:
					c = this.ᜆ();
					num = 2;
					continue;
				case 5:
					goto IL_132;
				case 6:
					return;
				case 7:
				{
					if (c == '-')
					{
						num = 4;
						continue;
					}
					sprᜏ sprᜏ = this.ᜀ(c);
					A_0.Add(sprᜏ.ᜂ(), sprᜏ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_148;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 8:
					if (c == '%')
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				case 9:
					goto IL_F6;
				case 10:
					goto IL_132;
				}
				break;
				IL_F6:
				num = 1;
				continue;
				IL_148:
				num = 0;
				continue;
				IL_132:
				c = this.ᜆ.ᜌ();
				goto IL_148;
			}
		}
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x00169AFC File Offset: 0x00168AFC
	private sprᜏ ᜀ(char A_0)
	{
		int a_ = 3;
		sprᜏ sprᜏ;
		for (;;)
		{
			A_0 = this.ᜆ.ᜌ();
			string text = this.ᜂ(ClipboardData.b("䥨晪杬普", a_));
			text = text.ToUpperInvariant();
			sprᜏ = new sprᜏ(text);
			A_0 = this.ᜆ.ᜌ();
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
						goto IL_156;
					default:
						if (false)
						{
						}
						A_0 = this.ᜆ();
						num = 8;
						continue;
					}
					break;
				case 1:
					if (true)
					{
					}
					A_0 = this.ᜆ();
					num = 6;
					continue;
				case 2:
					A_0 = this.ᜆ();
					num = 5;
					continue;
				case 3:
					if (A_0 == '-')
					{
						num = 2;
						continue;
					}
					goto IL_12E;
				case 4:
					if (A_0 == '-')
					{
						goto IL_156;
					}
					goto IL_8C;
				case 5:
					goto IL_12E;
				case 6:
					return sprᜏ;
				case 7:
					if (A_0 == '-')
					{
						num = 1;
						continue;
					}
					return sprᜏ;
				case 8:
					goto IL_8C;
				}
				break;
				IL_8C:
				this.ᜀ(A_0, sprᜏ);
				A_0 = this.ᜆ.ᜌ();
				num = 7;
				continue;
				IL_12E:
				this.ᜁ(A_0, sprᜏ);
				A_0 = this.ᜆ.ᜌ();
				num = 4;
				continue;
				IL_156:
				num = 0;
			}
		}
		return sprᜏ;
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x00169C70 File Offset: 0x00168C70
	private void ᜁ(char A_0, sprᜏ A_1)
	{
		int a_ = 9;
		int num = 8;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 != '(')
				{
					num = 7;
					continue;
				}
				goto IL_10C;
			case 1:
				A_0 = this.ᜆ.ᜌ();
				num = 0;
				continue;
			case 2:
				goto IL_4D;
			case 3:
				if (string.Equals(text, ClipboardData.b("Ⅾ㹰❲㑴⍶へ㑺㍼", a_), StringComparison.OrdinalIgnoreCase))
				{
					num = 1;
					continue;
				}
				goto IL_1AD;
			case 4:
				if (A_0 == '(')
				{
					num = 6;
					continue;
				}
				text = this.ᜂ(ClipboardData.b("佮籰祲籴", a_));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_197;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 5:
				goto IL_197;
			case 6:
				goto IL_107;
			case 7:
				this.ᜆ.ᜀ(ClipboardData.b("⩮॰Ͳၴᑶ൸ቺ፼᡾ꆀꮊﶎﺐ랖뺘뎚몜뎞膠솢키펦覨춪슬\udaae\udfb0ힲ閴邶슸论삼颾", a_), A_0);
				num = 5;
				continue;
			}
			if (A_0 == '%')
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
		}
		IL_4D:
		if (true)
		{
		}
		spr\u251B a_2 = this.ᜁ(ClipboardData.b("佮籰祲籴", a_));
		this.ᜀ(this.ᜆ.ᜁ(), a_2);
		this.ᜁ(this.ᜆ.ᜎ(), A_1);
		this.ᜉ();
		A_0 = this.ᜆ.ᜎ();
		return;
		IL_107:
		A_1.ᜀ(this.ᜀ(A_0, false), AttributeType.ENUMERATION);
		return;
		IL_10C:
		A_1.ᜀ(this.ᜀ(A_0, true), AttributeType.NOTATION);
		return;
		IL_197:
		goto IL_10C;
		IL_1AD:
		A_1.ᜀ(text);
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x00169E34 File Offset: 0x00168E34
	private void ᜀ(char A_0, sprᜏ A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				bool flag;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (A_0 == '#')
						{
							num = 4;
							continue;
						}
						goto IL_1C8;
					case 1:
						goto IL_1C8;
					case 3:
						if (A_0 != '\'')
						{
							num = 7;
							continue;
						}
						goto IL_1A0;
					case 4:
					{
						this.ᜆ.ᜂ();
						string a_2 = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("啴究獸牺", a_), true);
						flag = A_1.ᜁ(a_2);
						A_0 = this.ᜆ.ᜌ();
						num = 1;
						continue;
					}
					case 5:
					{
						if (A_0 == '"')
						{
							num = 8;
							continue;
						}
						string text = this.ᜆ.ᜀ(this.ᜅ, ClipboardData.b("啴究獸牺", a_), false);
						text = text.ToUpperInvariant();
						A_1.ᜂ(text);
						A_0 = this.ᜆ.ᜌ();
						num = 10;
						continue;
					}
					case 6:
						num = 3;
						continue;
					case 7:
						num = 5;
						continue;
					case 8:
						goto IL_AA;
					case 9:
						if (flag)
						{
							num = 6;
							continue;
						}
						return;
					case 10:
						goto IL_14E;
					case 11:
						goto IL_8B;
					}
					if (A_0 == '%')
					{
						num = 11;
						continue;
					}
					break;
					IL_1C8:
					num = 9;
					continue;
				}
				flag = true;
				num = 0;
			}
			IL_8B:
			spr\u251B a_3 = this.ᜁ(ClipboardData.b("啴究獸牺", a_));
			this.ᜀ(this.ᜆ.ᜁ(), a_3);
			this.ᜀ(this.ᜆ.ᜎ(), A_1);
			this.ᜉ();
			A_0 = this.ᜆ.ᜎ();
			return;
			IL_AA:
			goto IL_1A0;
			IL_14E:
			return;
			IL_1A0:
			string a_4 = this.ᜆ.ᜀ(this.ᜅ, A_0);
			A_1.ᜂ(a_4);
			A_0 = this.ᜆ.ᜌ();
			return;
		}
		}
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x0016A07C File Offset: 0x0016907C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2057()
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u2057.ᜇ = ClipboardData.b("啴究獸牺ż卾ꢀ", a_);
		spr\u2057.ᜈ = ClipboardData.b("啴究獸牺䍼", a_);
		spr\u2057.ᜉ = ClipboardData.b("啴究獸牺兼奾ﶀꮂ겄뢆ꊈꆊ", a_);
		spr\u2057.ᜊ = ClipboardData.b("啴繶瑸煺䍼", a_);
	}

	// Token: 0x04001A1B RID: 6683
	private const string ᜀ = " \r\n\t";

	// Token: 0x04001A1C RID: 6684
	private string ᜁ;

	// Token: 0x04001A1D RID: 6685
	private Dictionary<string, spr\u1D66> ᜂ;

	// Token: 0x04001A1E RID: 6686
	private Dictionary<string, spr\u251B> ᜃ;

	// Token: 0x04001A1F RID: 6687
	private Dictionary<string, spr\u251B> ᜄ;

	// Token: 0x04001A20 RID: 6688
	private StringBuilder ᜅ;

	// Token: 0x04001A21 RID: 6689
	private spr\u251B ᜆ;

	// Token: 0x04001A22 RID: 6690
	private static string ᜇ;

	// Token: 0x04001A23 RID: 6691
	private static string ᜈ;

	// Token: 0x04001A24 RID: 6692
	private static string ᜉ;

	// Token: 0x04001A25 RID: 6693
	private static string ᜊ;
}
