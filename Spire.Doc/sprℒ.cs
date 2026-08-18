using System;
using System.Collections;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000256 RID: 598
internal class sprℒ
{
	// Token: 0x06001DF8 RID: 7672 RVA: 0x001D9B90 File Offset: 0x001D8B90
	internal static MemoryStream ᜀ(string A_0, byte[] A_1)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprℛ sprℛ = new sprℛ(sprℒ.ᜀ(A_0));
		BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(A_1.Length + 4));
		binaryWriter.Write(A_1.Length);
		binaryWriter.Write(A_1);
		sprℛ.ᜃ().Add(ClipboardData.b("牲㩴᭶ᱸ䩺䵼ㅾ", a_), binaryWriter.BaseStream);
		MemoryStream memoryStream = new MemoryStream();
		sprℛ.ᜂ(memoryStream);
		return memoryStream;
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x001D9C30 File Offset: 0x001D8C30
	internal static string ᜀ(BinaryReader A_0)
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
		return sprℒ.ᜀ(A_0, A_0.ReadInt32());
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x001D9C78 File Offset: 0x001D8C78
	internal static string ᜀ(BinaryReader A_0, int A_1)
	{
		string result;
		for (;;)
		{
			result = "";
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_26;
					default:
						goto IL_7A;
					}
					break;
				case 1:
					if (A_1 > 0)
					{
						goto IL_26;
					}
					return result;
				case 2:
				{
					byte[] bytes = A_0.ReadBytes(A_1 - 1);
					A_0.ReadByte();
					result = Encoding.GetEncoding(1251).GetString(bytes);
					num = 0;
					continue;
				}
				}
				break;
				IL_26:
				num = 2;
			}
		}
		IL_7A:
		if (true)
		{
		}
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x001D9D10 File Offset: 0x001D8D10
	internal static void ᜁ(BinaryWriter A_0, string A_1)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					goto IL_35;
				case 3:
					goto IL_44;
				}
				if (A_1 != null)
				{
					num = 0;
				}
				else
				{
					num = 2;
				}
			}
			IL_44:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5A;
			}
		}
		IL_35:
		string text = "";
		goto IL_75;
		IL_5A:
		if (true)
		{
		}
		if (false)
		{
		}
		text = A_1;
		IL_75:
		A_1 = text;
		A_0.Write(A_1.Length + 1);
		A_0.Write(Encoding.GetEncoding(1251).GetBytes(A_1));
		A_0.Write(0);
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x001D9DC0 File Offset: 0x001D8DC0
	internal static void ᜀ(BinaryWriter A_0, string A_1)
	{
		while (!spr\u1CC6.ᜋ(A_1))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			A_0.Write(0);
			return;
		}
		sprℒ.ᜁ(A_0, A_1);
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x001D9E14 File Offset: 0x001D8E14
	internal static void ᜀ(spr\u1B02 A_0, spr\u23AC A_1)
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
		MemoryStream memoryStream = new MemoryStream();
		A_1.ᜀ(new BinaryWriter(memoryStream));
		A_0[A_1.ᜀ()] = memoryStream;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x001D9E70 File Offset: 0x001D8E70
	internal static Guid ᜀ(string A_0)
	{
		object obj;
		for (;;)
		{
			obj = sprℒ.ᜈ[A_0];
			if (obj == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_3F;
			}
		}
		if (true)
		{
		}
		return Guid.Empty;
		IL_3F:
		if (false)
		{
		}
		return new Guid((string)obj);
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x001D9ED0 File Offset: 0x001D8ED0
	static sprℒ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprℒ.ᜈ = new Hashtable();
		sprℒ.ᜈ.Add(ClipboardData.b("㱰⁲ㅴնᡸ౺", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾뚀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰㅲݴɶ੸፺", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰ɲtᙶ൸ቺቼᅾ", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾쎀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰቲᙴᱶᡸᱺ᡼", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾슀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("≰ᱲt᥶ᵸ⥺᡼᱾", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾얀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㱰⍲ᥴᙶxṺོ", a_), ClipboardData.b("ੰ䍲䕴䝶䩸䭺䵼佾쒀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰⍲㉴նᡸ୺ᕼᙾ놂뒄", a_), ClipboardData.b("ੰ䍲䕴䝶䵸᡺䕼᭾릀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖滛ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆戴ꆎꂐꆒ", a_), ClipboardData.b("ੰひ㍴䍶㽸乺䡼㥾떀꺂분솆놈벊ꂌ뮎햐Ꞓꊔ몖ꆘꮚ\udf9c\udd9e負隢鶤鞦醨骪鮬鮮蚴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆戴ꆎꚐ", a_), ClipboardData.b("ੰ㙲㑴䁶㭸㩺㡼䡾놀꺂쎄얆몈즊ꂌ뺎ꂐ킒톔몖\ud898ꊚ궜겞負鎢閤鮪鶬骮肰莲誸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆戴ꆎꦐ", a_), ClipboardData.b("ੰ䕲䅴佶䡸䍺㥼乾놀꺂놄솆낈즊ꂌ뺎ꂐ킒펔몖ꆘ궚\ud89c\ude9e負鎢閤鮪鶬袰膲貴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆戴슎\udc98ﲜﶞ춠욢솤覦風馪", a_), ClipboardData.b("ੰ㝲㙴䝶䭸䭺乼乾뚀꺂삄놆첈릊ꂌ뮎킐ꖒꞔ몖\udb98ꊚ\udb9c\ude9e負隤鲬馮螰膲莴趸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆뾐ꊒꞔ", a_), ClipboardData.b("ੰ䍲䅴佶㱸㥺䥼䱾쒀꺂랄랆번늊ꂌ뮎ꎐꆒ펔몖ꂘ꺚\ud89c꾞負隢邤邦钬馮膰育趴ﾸ욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆뾐꒒", a_), ClipboardData.b("ੰ㙲㑴䁶㭸㩺㡼䡾낀꺂쎄얆몈즊ꂌ뺎ꂐ킒톔몖\ud898ꊚ궜겞負鎢閤鮪鶬骮肰莲誸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆뾐ꮒ", a_), ClipboardData.b("ੰ䕲䅴佶䡸䍺㥼乾낀꺂놄솆낈즊ꂌ뺎ꂐ킒펔몖ꆘ궚\ud89c\ude9e負鎢閤鮪鶬袰膲貴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆\udc90\ude9aﺞ쎠쾢삤쎦螨骪龬", a_), ClipboardData.b("ੰ䁲㙴䙶䅸㹺㱼㩾떀꺂임쒆뮈뺊ꂌ뮎ꂐꂒꆔ몖\udb98겚\ud99c\ud99e負銢骪麬鲮蚰視욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆슐ﮒ杖래ꪚ꾜", a_), ClipboardData.b("ੰひ㍴䍶㽸乺䡼㥾떀꺂분솆놈벊ꂌ뮎햐Ꞓꊔ몖ꆘꮚ\udf9c\udd9e負隢鶤鞦醨骪鮬鮮蚴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆슐ﮒ杖래ꎚ", a_), ClipboardData.b("ੰ䕲䅴佶䡸䍺㥼乾놀꺂놄솆낈즊ꂌ뺎ꂐ킒펔몖ꆘ궚\ud89c\ude9e負鎢閤鮪鶬袰膲貴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ풆슐ﮒ杖풘漢ﺜ캠쮤욦쮨잪좬쮮龰芲螴", a_), ClipboardData.b("ੰ㝲㙴䝶䭸䭺乼乾뚀꺂삄놆첈릊ꂌ뮎킐ꖒꞔ몖\udb98ꊚ\udb9c\ude9e負隤鲬馮螰膲莴趸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ펆ﶌ릖ꢘꦚ", a_), ClipboardData.b("ੰ䑲䁴㍶䥸䩺䵼䡾놀꺂뒄떆몈뾊ꂌ뮎ꖐ횒겔몖ꆘꦚ\udb9cꦞ負銦颪钬薰蒲蚶誸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ펆ﶌ릖ꆘ", a_), ClipboardData.b("ੰ䕲䅴佶䡸䍺㥼乾낀꺂놄솆낈즊ꂌ뺎ꂐ킒펔몖ꆘ궚\ud89c\ude9e負鎢閤鮪鶬袰膲貴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ펆ﶌ\uda96춢쒤얦얨캪즬膮肰膲", a_), ClipboardData.b("ੰ㉲㑴䙶䵸㵺䑼㱾뢀꺂뎄떆쮈뺊ꂌ뮎Ꞑꂒꊔ몖ꆘ\uda9a\ude9cꮞ負鮢閦鲨鶮袰肴躸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ킆ﶎ붒궔", a_), ClipboardData.b("ੰ䕲䅴佶䡸䍺㥼乾놀꺂놄솆낈즊ꂌ뺎ꂐ킒펔몖ꆘ궚\ud89c\ude9e負鎢閤鮪鶬袰膲貴膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸㽺ቼ᱾ﶈꖊ벌붎", a_), ClipboardData.b("ੰ㕲䅴䁶䱸佺㹼䙾쎀꺂뎄뎆쾈뺊ꂌ뮎펐Ꞓꖔ몖ꆘ\uda9a\udb9cꮞ負関銤麦麨颪龬莲莴螶躸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸㽺ቼ᱾ﶈꖊ뮌", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸㽺ቼ᱾ﶈꖊ떌", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾란꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸㽺ቼ᱾ﶈ욊ﲒ킔練連爵얠趢钤閦", a_), ClipboardData.b("ੰ䉲䵴㙶䥸䵺㽼䥾쎀꺂랄솆몈춊ꂌ뮎풐ꆒ힔몖\ud898궚겜꺞負隢鞤鶪麬麮膲薶许욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸㑺ർ᩾잂ﲈ잒떚겜궞", a_), ClipboardData.b("ੰ䉲㝴䕶佸䩺㽼䵾뎀꺂쒄쒆뾈쪊ꂌ뮎풐ꖒ궔몖\ud898ꎚꪜ꾞負銦馨鎪鶬覰薲趴肶﮸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⭺ᑼ᱾ꞈ붊", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾낀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⭺ᑼ᱾ꞈ뎊", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾뚀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⥺⥼㥾꾀뮂", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾란꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⽺᡼ቾꖊ벌붎", a_), ClipboardData.b("ੰ䩲䑴䕶㡸㥺㹼䩾뎀꺂뚄놆첈릊ꂌ뮎Ꚑꊒꆔ몖ꆘ\ude9aꮜ궞負鶤麨颪蒰蚴躶覸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⽺᡼ቾꖊ떌", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䑼佾란꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("♰ᱲݴ፶坸⽺᡼ቾ욊ﲒ킔練連爵얠趢钤閦", a_), ClipboardData.b("ੰ䭲㑴䅶䭸佺乼䝾릀꺂쒄욆뮈벊ꂌ뮎ꊐ횒ꖔ몖ꆘꊚ\udb9cꞞ負醢隦鮨蚰ﶸ욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺㱼ཾﾊﾐ붒꒔ꖖ", a_), ClipboardData.b("ੰ䍲䕴䝶䭸佺䡼佾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺㹼᝾ꦆ번", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼乾낀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺㹼᝾ꦆ놈", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䵾낀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺㹼Ȿ힀", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䱾뎀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺㉼ཾ솄ﺊﾐ요ﺚﲜﮞ튠쮢삤슦\udda8薪鲬鶮", a_), ClipboardData.b("ੰ㙲㑴㕶㩸㹺㹼㭾쎀꺂욄쒆뢈좊ꂌ뮎킐ꖒ펔몖\udb98꾚\ud89c겞負钢龦醨鎪骮膸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⹼᝾ꦆ뢈릊", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䱾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⹼᝾ꦆ번", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼乾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⹼᝾ꦆ놈", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䵾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⹼᝾얆ﶎ\ude92\ud89c삠솢즤슦춨薪鲬鶮", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䱾늀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⹼᝾쪆ﾌ풐ﶒﺚ列놞邠醢", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䱾뎀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⥼᩾ﶈꎌ랎", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䵾놀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("㑰୲ᙴቶᕸ啺⥼᩾ﶈ삌杖튖漢ﾜ쒠잢", a_), ClipboardData.b("ੰ䍲䕴䝶䭸䭺䕼䱾뎀꺂떄랆릈뮊ꂌ뾎ꆐꎒꖔ몖\uda98ꮚ궜꾞負鎢閤鞦馨鮪鶬龮膰莲薴莶辸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰቲᱴ᥶൸啺⵼ᙾ", a_), ClipboardData.b("ੰ㝲䙴㉶䩸佺㽼䵾낀꺂버쎆뺈뺊ꂌ뺎ꆐꊒ풔몖ꆘ\ud89a꺜\udb9e負鎢閤鮪鶬麮芲莴芶许욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ⅰᱲɴቶ୸⭺ቼᙾꮄ좆麗쮎ﺐ殺ﲘ쾞펠욢횤슦잨\udfaa첬\udbae\ud8b0\udcb2\udbb4馶袸覺", a_), ClipboardData.b("ੰၲ䝴佶䭸佺䱼䡾꺂랄놆뾈릊ꂌ뮎ꖐ궔몖ꆘ漢꒜ꮞ負邢잤솦쾨鶪鲬첮蒰莲貴螶覸욺", a_));
		sprℒ.ᜈ.Add(ClipboardData.b("ṰͲၴ᥶ᵸᑺṼ੾ꞈ쾊ﾌ힒杖햠趢钤", a_), ClipboardData.b("ੰ䝲䅴佶㭸㥺䩼䡾낀꺂욄솆첈릊ꂌ뮎Ꚑ킒ꆔ몖\udb98\ud89a\ud99c\ud99e負銢颪骬鞮膲薴薶視욺", a_));
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x001DA640 File Offset: 0x001D9640
	internal static void ᜀ(Stream A_0, Stream A_1)
	{
		int a_ = 9;
		while (A_0 != null)
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
				A_0.Position = 0L;
				sprῑ sprῑ = new sprῑ(A_0);
				spr\u21B5 spr_u21B = sprῑ.ᜀ(null, ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰\udcb2펴톶킸\ud8ba\ud8bc﮾껀ꃂ냄꫆곈ꗊ만꧚룜돞胠韢賤裦蟨飪藬蛮臰胲\udaf4飶鿸鷺铼鳾搀䜂樄搆簈昊栌愎攐", a_));
				StreamReader streamReader = new StreamReader(spr_u21B.ᜅ());
				string text = streamReader.ReadToEnd();
				text = text.Replace(ClipboardData.b("᥮ᡰrᱴᕶၸ᝺ᑼ୾뺂Ꞅﾐ높", a_), "");
				spr_u21B.ᜀ(new MemoryStream());
				StreamWriter streamWriter = new StreamWriter(spr_u21B.ᜅ(), streamReader.CurrentEncoding);
				streamWriter.Write(text);
				streamWriter.Flush();
				sprῑ.ᜀ(A_1);
				return;
			}
			}
		}
		if (true)
		{
		}
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x001DA710 File Offset: 0x001D9710
	internal static void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			BinaryReader binaryReader;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B5;
				case 1:
				{
					if (A_0.Position >= A_0.Length)
					{
						num = 7;
						continue;
					}
					int num2 = (int)binaryReader.ReadInt16();
					int num3 = (int)binaryReader.ReadInt16();
					num = 3;
					continue;
				}
				case 2:
					goto IL_B2;
				case 3:
				{
					int num2;
					if (num2 == 61)
					{
						num = 2;
						continue;
					}
					int num3;
					A_0.Position += (long)num3;
					num = 5;
					continue;
				}
				case 4:
					goto IL_4F;
				case 5:
					goto IL_B5;
				case 7:
					goto IL_DB;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				A_0.Position = 0L;
				binaryReader = new BinaryReader(A_0);
				num = 0;
				continue;
				IL_B5:
				num = 1;
			}
			IL_4F:
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
			IL_B2:
			binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			int num4 = (int)binaryReader.ReadByte();
			num4 = sprṝ.ᜀ(num4, 1, false);
			A_0.Position -= 1L;
			A_0.WriteByte((byte)num4);
			return;
			IL_DB:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x04001F8C RID: 8076
	internal const string ᜀ = "\u0001Ole10Native";

	// Token: 0x04001F8D RID: 8077
	internal const string ᜁ = "\u0003OCXNAME";

	// Token: 0x04001F8E RID: 8078
	internal const string ᜂ = "\u0003ObjInfo";

	// Token: 0x04001F8F RID: 8079
	internal const string ᜃ = "\u0003PRINT";

	// Token: 0x04001F90 RID: 8080
	internal const string ᜄ = "\u0003OCXDATA";

	// Token: 0x04001F91 RID: 8081
	internal const string ᜅ = "\u0001CompObj";

	// Token: 0x04001F92 RID: 8082
	internal const string ᜆ = "\u0003LinkInfo";

	// Token: 0x04001F93 RID: 8083
	internal const int ᜇ = 1281;

	// Token: 0x04001F94 RID: 8084
	private static readonly Hashtable ᜈ;
}
