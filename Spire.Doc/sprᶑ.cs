using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000297 RID: 663
internal class sprᶑ : spr\u1CDF
{
	// Token: 0x06002342 RID: 9026 RVA: 0x0023DD68 File Offset: 0x0023CD68
	internal sprᶑ(spr\u1B02 A_0) : this(0, A_0)
	{
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x0023DD80 File Offset: 0x0023CD80
	internal sprᶑ(int A_0, spr\u1B02 A_1)
	{
		this.ᜀ = A_1;
		base.ᜀ(A_0);
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x0023DDA4 File Offset: 0x0023CDA4
	internal override string ᜁ(string A_0)
	{
		int a_ = 8;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E5;
				default:
					goto IL_A5;
				}
				break;
			case 2:
				goto IL_109;
			case 3:
				goto IL_52;
			case 4:
				if (A_0.StartsWith(ClipboardData.b("㹭Ὧձᅳѵ⡷ᕹᕻၽ겁힃ﶉꊋ뚍", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_10B;
			case 5:
				goto IL_E5;
			}
			if (A_0.StartsWith(ClipboardData.b("⭭࡯ᅱᅳ᩵噷⥹ᑻ᭽ꪃ뺅", a_)))
			{
				num = 3;
				continue;
			}
			num = 5;
			continue;
			IL_E5:
			if (A_0.StartsWith(ClipboardData.b("㥭Ὧqၳ塵㱷ᕹύ୽ꚇ늉", a_)))
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
		}
		IL_52:
		if (true)
		{
		}
		return ClipboardData.b("཭oɱᡳή᭷᭹ࡻ᝽ꮃꊋ뾑ﮗﾙ", a_);
		IL_A5:
		if (false)
		{
		}
		return ClipboardData.b("཭oɱᡳή᭷᭹ࡻ᝽ꮃꊋ뾑秊ﾙ쾟쮡쪣튥", a_);
		IL_109:
		return ClipboardData.b("཭oɱᡳή᭷᭹ࡻ᝽ꮃﮇﶉﲍ", a_);
		IL_10B:
		return ClipboardData.b("཭oɱᡳή᭷᭹ࡻ᝽ꮃꊋ望瀞튟쾡얣튥\udba7螩쎫좭횯\udbb1ힳ펵\udcb7햹\udfbb쮽궿ꟁ꫃닅ꗉꃋꯍ鿏냑뻓돕믗껙", a_);
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x0023DECC File Offset: 0x0023CECC
	internal override string ᜀ(string A_0)
	{
		int a_ = 7;
		string text;
		for (;;)
		{
			text = spr\u2609.ᜀ(this.ᜁ(A_0));
			int num = 23;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.StartsWith(ClipboardData.b("㵬nٰᙲݴ❶ᙸቺ፼୾꾀킂ꎌ뺎ꎐ", a_)))
					{
						num = 12;
						continue;
					}
					num = 14;
					continue;
				case 1:
					if (A_0.StartsWith(ClipboardData.b("㩬nͰᝲ孴㡶ॸṺ፼㭾歷\udb8e", a_)))
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 2:
					goto IL_2F5;
				case 3:
					goto IL_326;
				case 4:
					if (A_0.StartsWith(ClipboardData.b("≬ᩮհὲᩴᡶቸ啺ぼ౾슂", a_)))
					{
						num = 22;
						continue;
					}
					num = 7;
					continue;
				case 5:
					if (A_0.StartsWith(ClipboardData.b("㵬nٰᙲݴ❶ᙸቺ፼୾꾀첂쾊ﺒ練쮚爵튠욢쮤펦좨\udfaa쒬삮\udfb0", a_)))
					{
						num = 11;
						continue;
					}
					num = 13;
					continue;
				case 6:
					goto IL_237;
				case 7:
					if (this.ᜃ())
					{
						num = 8;
						continue;
					}
					goto IL_393;
				case 8:
					goto IL_195;
				case 9:
					if (!A_0.StartsWith(ClipboardData.b("⁬๮ᡰὲ㡴ѶṸ㩺ॼ୾", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_1D7;
				case 10:
					return text;
				case 11:
					goto IL_35A;
				case 12:
					goto IL_277;
				case 13:
					if (A_0.StartsWith(ClipboardData.b("ⱬ౮Ͱᱲぴྲྀ᩸፺卼㭾歷", a_)))
					{
						num = 19;
						continue;
					}
					num = 9;
					continue;
				case 14:
					if (A_0.StartsWith(ClipboardData.b("⡬ᝮተᙲᥴ奶㙸୺᡼ᅾ얀ﮎ슐ﾚ쒠욢톤", a_)))
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
				case 15:
					if (A_0.StartsWith(ClipboardData.b("㩬nͰᝲ孴㍶ᙸ᡺ࡼቾꦆ뢈릊", a_)))
					{
						num = 6;
						continue;
					}
					num = 20;
					continue;
				case 16:
					if (A_0.StartsWith(ClipboardData.b("⡬ᝮተᙲᥴ奶⩸፺᡼᩾궂뒄떆", a_)))
					{
						num = 17;
						continue;
					}
					num = 15;
					continue;
				case 17:
					goto IL_38E;
				case 18:
					goto IL_1D5;
				case 19:
					goto IL_DE;
				case 20:
					if (A_0.StartsWith(ClipboardData.b("㵬nٰᙲݴ❶ᙸቺ፼୾꾀킂ﺈꖊ벌붎", a_)))
					{
						num = 18;
						continue;
					}
					num = 0;
					continue;
				case 21:
					num = 4;
					continue;
				case 22:
					goto IL_149;
				case 23:
					if (text != ClipboardData.b("䍬൮ᡰᵲ", a_))
					{
						num = 10;
						continue;
					}
					num = 16;
					continue;
				}
				break;
			}
		}
		return text;
		IL_DE:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_237:
			return ClipboardData.b("䍬୮Ṱၲ൴", a_);
		default:
			if (false)
			{
			}
			return ClipboardData.b("䍬Ὦᕰᕲ", a_);
		}
		IL_149:
		goto IL_1D7;
		IL_195:
		return Path.GetExtension(this.ᜄ()).ToLower();
		IL_1D5:
		return ClipboardData.b("䍬ὮŰݲ൴", a_);
		IL_1D7:
		return ClipboardData.b("䍬ɮɰᑲ", a_);
		IL_277:
		return ClipboardData.b("䍬ᱮᵰᝲ൴", a_);
		IL_2F5:
		return ClipboardData.b("䍬nᕰr", a_);
		IL_326:
		return ClipboardData.b("䍬nᕰݲ", a_);
		IL_35A:
		return ClipboardData.b("䍬nᕰͲ", a_);
		IL_38E:
		if (true)
		{
		}
		return ClipboardData.b("䍬ᝮᵰr൴", a_);
		IL_393:
		return ClipboardData.b("䍬൮ᡰᵲ", a_);
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x0023E27C File Offset: 0x0023D27C
	internal override void ᜀ(Stream A_0, string A_1)
	{
		if (this.ᜃ())
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
				if (true)
				{
				}
				this.ᜀ(A_0);
				return;
			}
		}
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x0023E2D4 File Offset: 0x0023D2D4
	internal string ᜄ()
	{
		sprḃ sprḃ;
		for (;;)
		{
			BinaryReader binaryReader = this.ᜀ();
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
						goto IL_70;
					}
					if (false)
					{
					}
					sprḃ = new sprḃ(binaryReader);
					num = 2;
					continue;
				case 1:
					goto IL_8C;
				case 2:
					goto IL_70;
				case 3:
					if (binaryReader != null)
					{
						num = 0;
						continue;
					}
					goto IL_8E;
				}
				break;
				IL_70:
				if (sprḃ.ᜁ != 2)
				{
					goto IL_8E;
				}
				if (true)
				{
				}
				num = 1;
			}
		}
		IL_8C:
		return sprḃ.ᜂ;
		IL_8E:
		return "";
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x0023E374 File Offset: 0x0023D374
	private void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			BinaryReader binaryReader;
			sprḃ sprḃ;
			for (;;)
			{
				binaryReader = this.ᜀ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (binaryReader == null)
						{
							num = 0;
							continue;
						}
						sprḃ = new sprḃ(binaryReader);
						num = 3;
						continue;
					case 2:
						goto IL_A8;
					case 3:
						if (sprḃ.ᜁ == 2)
						{
							num = 6;
							continue;
						}
						this.ᜅ().Position = 4L;
						spr\u1CC6.ᜀ(this.ᜅ(), A_0);
						if (true)
						{
						}
						num = 4;
						continue;
					case 4:
						goto IL_F4;
					case 5:
					{
						int ᜄ;
						switch (ᜄ)
						{
						case 1:
						case 2:
							return;
						case 3:
							goto IL_54;
						default:
							num = 2;
							continue;
						}
						break;
					}
					case 6:
					{
						int ᜄ = sprḃ.ᜄ;
						num = 5;
						continue;
					}
					}
					break;
				}
			}
			return;
			IL_54:
			byte[] array = new byte[sprḃ.ᜆ];
			binaryReader.Read(array, 0, array.Length);
			A_0.Write(array, 0, array.Length);
			return;
			IL_A8:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				return;
			}
			IL_F4:
			return;
		}
		}
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x0023E4B0 File Offset: 0x0023D4B0
	private BinaryReader ᜀ()
	{
		if (this.ᜅ().Length == 0L)
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
			return null;
		}
		this.ᜅ().Position = 0L;
		return new BinaryReader(this.ᜅ(), Encoding.ASCII);
	}

	// Token: 0x0600234A RID: 9034 RVA: 0x0023E51C File Offset: 0x0023D51C
	private void ᜁ(Stream A_0, string A_1)
	{
		int a_ = 4;
		MemoryStream memoryStream;
		spr\u1B02 spr_u1B;
		for (;;)
		{
			memoryStream = null;
			spr_u1B = null;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 14;
					continue;
				case 1:
					memoryStream = (this.ᜀ[ClipboardData.b("⥩⍫⁭⑯㝱㩳≵⭷", a_)] as MemoryStream);
					num = 19;
					continue;
				case 2:
					if (A_1.StartsWith(ClipboardData.b("⭩ཫᱭὯ㝱౳ᕵၷ呹㡻ᅽﺉ", a_)))
					{
						num = 1;
						continue;
					}
					num = 22;
					continue;
				case 3:
					goto IL_126;
				case 4:
					goto IL_10D;
				case 5:
					if (true)
					{
					}
					memoryStream = (this.ᜀ[ClipboardData.b("⭩ᡫᩭᅯᅱᱳ㕵᝷ᑹࡻ᭽", a_)] as MemoryStream);
					num = 13;
					continue;
				case 6:
					goto IL_294;
				case 7:
					goto IL_3FA;
				case 8:
					if (A_1.StartsWith(ClipboardData.b("㩩ͫᥭᕯq⑳᥵ᅷᑹࡻ偽퍿ꚇ뮉뺋", a_)))
					{
						num = 25;
						continue;
					}
					num = 15;
					continue;
				case 9:
					if (A_1.StartsWith(ClipboardData.b("⽩ᑫ൭ᕯṱ婳╵ၷό᥻੽깿몁", a_)))
					{
						num = 10;
						continue;
					}
					num = 16;
					continue;
				case 10:
				{
					Stream a_2 = this.ᜀ[ClipboardData.b("㵩ͫᱭ᭯ၱ᭳᥵፷", a_)] as MemoryStream;
					sprℒ.ᜀ(a_2);
					num = 21;
					continue;
				}
				case 11:
					num = 12;
					continue;
				case 12:
					if (A_1.StartsWith(ClipboardData.b("㩩ͫᥭᕯq⑳᥵ᅷᑹࡻ偽콿첇ﮍﶏ望좗鍊얟첡킣장\udca7쎩쎫삭", a_)))
					{
						num = 6;
						continue;
					}
					num = 31;
					continue;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B3;
					default:
						if (false)
						{
						}
						goto IL_10D;
					}
					break;
				case 14:
					if (A_1.StartsWith(ClipboardData.b("╩ᥫᩭᱯᵱ᭳ᵵ噷㝹ཻ᥽셿", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_10D;
				case 15:
					if (!A_1.StartsWith(ClipboardData.b("⽩ᑫ൭ᕯṱ婳㥵ࡷόቻ㩽\udd8dﲗﮝ얟횡", a_)))
					{
						num = 24;
						continue;
					}
					goto IL_294;
				case 16:
					if (A_1.StartsWith(ClipboardData.b("⽩ᑫ൭ᕯṱ婳╵ၷό᥻੽깿뎁뚃", a_)))
					{
						num = 27;
						continue;
					}
					num = 17;
					continue;
				case 17:
					if (!A_1.StartsWith(ClipboardData.b("㵩ͫᱭᑯ山び᥵᭷ཹᅻ᭽ꪃ랅몇", a_)))
					{
						num = 28;
						continue;
					}
					goto IL_1C3;
				case 18:
					if (!A_1.StartsWith(ClipboardData.b("㵩ͫᱭᑯ山㭳ٵᵷᑹ㡻ᅽﺉ\ud88b", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_294;
				case 19:
					goto IL_10D;
				case 20:
					goto IL_10D;
				case 21:
					goto IL_10D;
				case 22:
					if (!A_1.StartsWith(ClipboardData.b("❩൫ݭᱯ㽱ݳᅵ㥷๹ࡻ", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_3FA;
				case 23:
					goto IL_10D;
				case 24:
					num = 18;
					continue;
				case 25:
					goto IL_1C3;
				case 26:
					if (memoryStream != null)
					{
						num = 3;
						continue;
					}
					num = 30;
					continue;
				case 27:
					memoryStream = new MemoryStream();
					sprℒ.ᜀ(this.ᜀ[ClipboardData.b("㩩൫൭᭯፱፳፵", a_)] as MemoryStream, memoryStream);
					num = 4;
					continue;
				case 28:
					num = 8;
					continue;
				case 29:
					goto IL_10D;
				case 30:
					goto IL_133;
				case 31:
					if (A_1.StartsWith(ClipboardData.b("╩ᥫᩭᱯᵱ᭳ᵵ噷㱹ᕻች쎁", a_)))
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				}
				break;
				IL_10D:
				num = 26;
				continue;
				IL_1C3:
				memoryStream = (this.ᜀ[ClipboardData.b("㩩൫൭᭯፱፳፵", a_)] as MemoryStream);
				num = 20;
				continue;
				IL_2B3:
				num = 29;
				continue;
				IL_294:
				memoryStream = (this.ᜀ[ClipboardData.b("⽩ū౭ᕯᙱၳ፵ᱷ㕹᡻᡽", a_)] as MemoryStream);
				goto IL_2B3;
				IL_3FA:
				spr_u1B = (this.ᜀ[ClipboardData.b("❩⵫㹭㥯㽱ᅳյ୷᭹᭻᭽", a_)] as spr\u1B02);
				num = 23;
			}
		}
		IL_126:
		memoryStream.Position = 0L;
		spr\u1CC6.ᜀ(memoryStream, A_0);
		return;
		IL_133:
		sprᶑ.ᜀ(A_0, (spr_u1B != null) ? spr_u1B : this.ᜀ);
	}

	// Token: 0x0600234B RID: 9035 RVA: 0x0023E9C8 File Offset: 0x0023D9C8
	private static void ᜀ(Stream A_0, spr\u1B02 A_1)
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
		sprℛ sprℛ = new sprℛ(A_1);
		sprℛ.ᜂ(A_0);
	}

	// Token: 0x0600234C RID: 9036 RVA: 0x0023EA14 File Offset: 0x0023DA14
	internal MemoryStream ᜇ()
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (MemoryStream)this.ᜀ[ClipboardData.b("潫⍭㕯♱㕳", a_)];
	}

	// Token: 0x0600234D RID: 9037 RVA: 0x0023EA78 File Offset: 0x0023DA78
	internal MemoryStream ᜆ()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return (MemoryStream)this.ᜀ[ClipboardData.b("穸⭺㑼㱾", a_)];
	}

	// Token: 0x0600234E RID: 9038 RVA: 0x0023EADC File Offset: 0x0023DADC
	internal MemoryStream ᜅ()
	{
		int a_ = 9;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return (MemoryStream)this.ᜀ[ClipboardData.b("湮㹰ὲၴ䙶䥸㕺ᱼ୾", a_)];
	}

	// Token: 0x0600234F RID: 9039 RVA: 0x0023EB40 File Offset: 0x0023DB40
	internal MemoryStream ᜁ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (MemoryStream)this.ᜀ[ClipboardData.b("祷㥹፻፽춁", a_)];
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x0023EBA4 File Offset: 0x0023DBA4
	internal bool ᜃ()
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
		return this.ᜅ() != null;
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x0023EBEC File Offset: 0x0023DBEC
	internal spr\u1B02 ᜂ()
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

	// Token: 0x06002352 RID: 9042 RVA: 0x0023EC30 File Offset: 0x0023DC30
	internal void ᜀ(spr\u1B02 A_0)
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

	// Token: 0x04002146 RID: 8518
	private new spr\u1B02 ᜀ;
}
