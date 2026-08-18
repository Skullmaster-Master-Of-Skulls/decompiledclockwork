using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

// Token: 0x02000231 RID: 561
internal class sprḴ
{
	// Token: 0x06001ACB RID: 6859 RVA: 0x001BFC20 File Offset: 0x001BEC20
	internal byte[] ᜃ()
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
		return this.\u1715;
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x001BFC64 File Offset: 0x001BEC64
	internal OleObjectType ᜂ()
	{
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
					break;
				default:
					goto IL_89;
				}
				break;
			case 2:
				goto IL_49;
			case 3:
				this.\u1719 = spr\u20F5.ᜀ(this.\u1718.ᜂ());
				num = 1;
				continue;
			}
			IL_20:
			if (this.\u1718 != null)
			{
				if (true)
				{
				}
				num = 3;
				continue;
			}
			this.\u1719 = OleObjectType.Undefined;
			num = 2;
			continue;
			goto IL_20;
		}
		IL_49:
		goto IL_91;
		IL_89:
		if (false)
		{
		}
		IL_91:
		return this.\u1719;
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x001BFD08 File Offset: 0x001BED08
	internal string ᜀ()
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
		return this.\u171A;
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x001BFD4C File Offset: 0x001BED4C
	internal string ᜁ()
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
		return this.\u171B;
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x001BFD90 File Offset: 0x001BED90
	internal sprḴ(byte[] A_0, string A_1)
	{
		int a_ = 3;
		this.\u171A = string.Empty;
		this.\u171B = string.Empty;
		base..ctor();
		if (A_0 != null)
		{
			if (A_0.Length != 0)
			{
				MemoryStream a_2 = new MemoryStream(A_0);
				spr\u20BF spr_u20BF = new spr\u20BF(a_2);
				spr\u2547 spr_u = spr_u20BF.ᜇ().ᜅ(ClipboardData.b("♨४ݬ੮ተݲ╴ᡶᙸ᝺", a_));
				spr_u = spr_u.ᜅ(A_1);
				this.ᜂ(spr_u);
				return;
			}
		}
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x001BFE08 File Offset: 0x001BEE08
	internal sprḴ()
	{
		this.\u171A = string.Empty;
		this.\u171B = string.Empty;
		base..ctor();
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x001BFE34 File Offset: 0x001BEE34
	internal void ᜂ(spr\u2547 A_0)
	{
		int a_ = 14;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_8D;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 5;
				break;
			}
			break;
		}
		spr\u2578 spr_u5;
		for (;;)
		{
			IL_3E:
			switch (num)
			{
			case 0:
			{
				byte[] array;
				this.\u1715 = this.ᜀ(array);
				num = 16;
				continue;
			}
			case 1:
			{
				if (this.ᜂ() == OleObjectType.Package)
				{
					num = 0;
					continue;
				}
				int num2 = 0;
				byte[] array;
				int a_2 = spr\u2562.ᜃ(array, ref num2);
				this.\u1715 = spr\u2562.ᜀ(array, a_2, ref num2);
				num = 7;
				continue;
			}
			case 2:
			{
				spr\u2578 spr_u = A_0.ᜁ(ClipboardData.b("睳㥵᩷ၹ㕻ၽ", a_));
				this.\u1716 = new spr\u257F(spr_u);
				spr_u.Flush();
				spr_u.Close();
				num = 17;
				continue;
			}
			case 3:
			{
				spr\u2578 spr_u2 = A_0.ᜁ(ClipboardData.b("畳㥵ᑷό", a_));
				this.\u1714 = new sprᣕ(spr_u2);
				spr_u2.Flush();
				spr_u2.Close();
				num = 10;
				continue;
			}
			case 4:
				if (A_0.ᜃ(ClipboardData.b("畳㥵ᑷό䵻乽칿ﺇ", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_349;
			case 6:
			{
				spr\u2578 spr_u3 = A_0.ᜁ(ClipboardData.b("畳㕵᝷᝹౻ㅽ", a_));
				this.\u1718 = new sprᬟ(spr_u3);
				spr_u3.Flush();
				spr_u3.Close();
				num = 12;
				continue;
			}
			case 7:
				goto IL_1C3;
			case 8:
				if (A_0.ᜃ(ClipboardData.b("睳㩵ᅷᑹ᝻㝽", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_1FB;
			case 9:
				if (A_0.ᜃ(ClipboardData.b("睳㥵᩷ၹ㕻ၽ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_316;
			case 10:
				goto IL_2BC;
			case 11:
				goto IL_1FB;
			case 12:
				if (true)
				{
				}
				goto IL_2E0;
			case 13:
			{
				spr\u2578 spr_u4 = A_0.ᜁ(ClipboardData.b("睳㩵ᅷᑹ᝻㝽", a_));
				this.\u1717 = new spr\u17FD(spr_u4);
				spr_u4.Flush();
				spr_u4.Close();
				num = 11;
				continue;
			}
			case 14:
				if (A_0.ᜃ(ClipboardData.b("畳㕵᝷᝹౻ㅽ", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_2E0;
			case 15:
			{
				spr_u5 = A_0.ᜁ(ClipboardData.b("畳㥵ᑷό䵻乽칿ﺇ", a_));
				byte[] array = new byte[spr_u5.Length];
				spr_u5.Read(array, 0, array.Length);
				num = 1;
				continue;
			}
			case 16:
				goto IL_2DB;
			case 17:
				goto IL_316;
			}
			goto IL_8D;
			IL_1FB:
			num = 4;
			continue;
			IL_2E0:
			num = 8;
			continue;
			IL_316:
			num = 14;
		}
		IL_1C3:
		IL_231:
		spr_u5.Flush();
		spr_u5.Close();
		return;
		IL_2BC:
		goto IL_1C5;
		IL_2DB:
		goto IL_231;
		IL_349:
		this.\u1715 = this.ᜁ(A_0);
		return;
		IL_8D:
		if (A_0.ᜃ(ClipboardData.b("畳㥵ᑷό", a_)))
		{
			num = 3;
			goto IL_3E;
		}
		IL_1C5:
		num = 9;
		goto IL_3E;
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x001C0198 File Offset: 0x001BF198
	private byte[] ᜁ(spr\u2547 A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			sprᤘ sprᤘ;
			spr\u2578 spr_u;
			for (;;)
			{
				memoryStream = new MemoryStream();
				sprᤘ = sprᤘ.ᜆ();
				int num = this.ᜀ(A_0);
				int num2 = 0;
				int num3 = A_0.ᜁ().Length;
				int num4 = 9;
				for (;;)
				{
					switch (num4)
					{
					case 0:
					{
						string text;
						spr_u = A_0.ᜁ(text);
						num4 = 14;
						continue;
					}
					case 1:
						num4 = 3;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_136;
						default:
							if (false)
							{
							}
							num4 = 4;
							continue;
						}
						break;
					case 3:
					{
						string text;
						if (text != ClipboardData.b("獱㭳᩵ᵷ䭹䱻ぽ", a_))
						{
							num4 = 11;
							continue;
						}
						goto IL_288;
					}
					case 4:
					{
						string text;
						if (text != ClipboardData.b("灱㭳᩵ᵷ⩹๻᭽늁뒃뚅", a_))
						{
							num4 = 0;
							continue;
						}
						goto IL_288;
					}
					case 5:
						num4 = 20;
						continue;
					case 6:
						goto IL_37B;
					case 7:
						goto IL_1DC;
					case 8:
					{
						string text;
						if (text != ClipboardData.b("煱ㅳ♵⩷㍹㉻⩽", a_))
						{
							num4 = 2;
							continue;
						}
						goto IL_288;
					}
					case 9:
						goto IL_1DC;
					case 10:
						goto IL_288;
					case 11:
						num4 = 8;
						continue;
					case 12:
					{
						string text;
						if (text != ClipboardData.b("煱㡳ήᙷᅹ㕻ၽ", a_))
						{
							num4 = 1;
							continue;
						}
						goto IL_288;
					}
					case 13:
					{
						if (num2 >= num3)
						{
							num4 = 19;
							continue;
						}
						string text = A_0.ᜁ().GetValue(num2).ToString();
						num4 = 16;
						continue;
					}
					case 14:
					{
						if (num == 1)
						{
							num4 = 6;
							continue;
						}
						sprᤘ.ᜁ(spr_u.ᜋ());
						byte[] array = new byte[spr_u.Length];
						spr_u.Read(array, 0, array.Length);
						sprᤘ.Write(array, 0, array.Length);
						num4 = 18;
						continue;
					}
					case 15:
						num4 = 12;
						continue;
					case 16:
					{
						string text;
						if (text != ClipboardData.b("獱㭳᩵ᵷ", a_))
						{
							num4 = 5;
							continue;
						}
						goto IL_288;
					}
					case 17:
						if (true)
						{
						}
						num4 = 21;
						continue;
					case 18:
						try
						{
							sprᤘ.Flush();
							goto IL_136;
						}
						catch
						{
							goto IL_136;
						}
						goto IL_BE;
					case 19:
						goto IL_1FC;
					case 20:
					{
						string text;
						if (text != ClipboardData.b("煱㭳ᑵቷ㍹ቻ᡽", a_))
						{
							num4 = 17;
							continue;
						}
						goto IL_288;
					}
					case 21:
					{
						string text;
						if (text != ClipboardData.b("獱㝳᥵ᕷ੹㍻ᱽ", a_))
						{
							num4 = 15;
							continue;
						}
						goto IL_288;
					}
					}
					break;
					IL_136:
					sprᤘ.Close();
					spr_u.Close();
					num4 = 10;
					continue;
					IL_1DC:
					num4 = 13;
					continue;
					IL_288:
					num2++;
					num4 = 7;
				}
			}
			IL_BE:
			byte[] array2 = new byte[spr_u.Length];
			spr_u.Read(array2, 0, array2.Length);
			spr_u.Flush();
			spr_u.Close();
			A_0.Dispose();
			sprᤘ.Close();
			sprᤘ.Dispose();
			return array2;
			IL_1FC:
			sprᤘ.Flush();
			long length = sprᤘ.Length;
			sprᤘ.ᜀ(memoryStream);
			sprᤘ.Close();
			sprᤘ.Dispose();
			return memoryStream.GetBuffer();
			IL_37B:
			goto IL_BE;
		}
		}
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x001C055C File Offset: 0x001BF55C
	private int ᜀ(spr\u2547 A_0)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				int num;
				int num3;
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_215:
					num = 4;
					break;
				default:
					if (false)
					{
					}
					num2 = 0;
					num3 = 0;
					num4 = A_0.ᜁ().Length;
					num = 14;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 11;
						continue;
					case 1:
						num = 17;
						continue;
					case 2:
					{
						string a;
						if (a != ClipboardData.b("楩⍫౭ᩯ㭱ᩳၵ᝷", a_))
						{
							num = 0;
							continue;
						}
						goto IL_126;
					}
					case 3:
					{
						if (true)
						{
						}
						string a;
						if (a != ClipboardData.b("楩⥫㹭≯㭱㩳≵", a_))
						{
							num = 1;
							continue;
						}
						goto IL_126;
					}
					case 4:
					{
						string a;
						if (a != ClipboardData.b("歩⍫ɭᕯ䍱䑳㡵᥷๹ᕻࡽ", a_))
						{
							num = 13;
							continue;
						}
						goto IL_126;
					}
					case 5:
						goto IL_215;
					case 6:
						num2++;
						num = 15;
						continue;
					case 7:
					{
						string a;
						if (a != ClipboardData.b("楩⁫ݭṯᥱ㵳ᡵṷᕹ", a_))
						{
							num = 5;
							continue;
						}
						goto IL_126;
					}
					case 8:
						num = 7;
						continue;
					case 9:
						num = 2;
						continue;
					case 10:
						goto IL_1C0;
					case 11:
					{
						string a;
						if (a != ClipboardData.b("歩⽫ŭᵯɱ㭳ᑵቷ", a_))
						{
							num = 8;
							continue;
						}
						goto IL_126;
					}
					case 12:
					{
						if (num3 >= num4)
						{
							num = 16;
							continue;
						}
						string a = A_0.ᜁ().GetValue(num3).ToString();
						num = 18;
						continue;
					}
					case 13:
						num = 3;
						continue;
					case 14:
						goto IL_1C0;
					case 15:
						goto IL_126;
					case 16:
						return num2;
					case 17:
					{
						string a;
						if (a != ClipboardData.b("桩⍫ɭᕯ≱ٳ፵୷䩹䱻乽", a_))
						{
							num = 6;
							continue;
						}
						goto IL_126;
					}
					case 18:
					{
						string a;
						if (a != ClipboardData.b("歩⍫ɭᕯ", a_))
						{
							num = 9;
							continue;
						}
						goto IL_126;
					}
					}
					break;
					IL_126:
					num3++;
					num = 10;
					continue;
					IL_1C0:
					num = 12;
				}
			}
			return num2;
		}
		}
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x001C07EC File Offset: 0x001BF7EC
	private byte[] ᜀ(byte[] A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				string @string = asciiencoding.GetString(A_0);
				int num = 0;
				int num2 = 0;
				int num3 = 4;
				int num4 = 12;
				int num5 = 16;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						if (this.\u171B.Length == 0)
						{
							num5 = 26;
							continue;
						}
						goto IL_3E0;
					case 1:
					{
						int num6 = A_0.Length;
						num5 = 9;
						continue;
					}
					case 2:
						if (A_0[num3] == 0)
						{
							if (true)
							{
							}
							num5 = 31;
							continue;
						}
						goto IL_178;
					case 3:
					{
						int num6;
						num6 -= 2;
						num5 = 12;
						continue;
					}
					case 4:
						if (num == 1)
						{
							num5 = 28;
							continue;
						}
						goto IL_207;
					case 5:
						if (A_0[num3] != 0)
						{
							num5 = 6;
							continue;
						}
						num3++;
						num5 = 10;
						continue;
					case 6:
					{
						int num6;
						array = new byte[num6 - num3];
						int num7 = 0;
						int num8 = array.Length;
						num5 = 18;
						continue;
					}
					case 7:
						goto IL_3E0;
					case 8:
						goto IL_3E0;
					case 9:
						if (@string.Substring(@string.Length - 2) == ClipboardData.b("牱瑳", a_))
						{
							num5 = 3;
							continue;
						}
						goto IL_152;
					case 10:
						goto IL_152;
					case 11:
						num2 = num3 + 1;
						num5 = 13;
						continue;
					case 12:
						goto IL_152;
					case 13:
						goto IL_3E0;
					case 14:
						return array;
					case 15:
						num4 = 11;
						num5 = 7;
						continue;
					case 16:
						goto IL_369;
					case 17:
						if (num2 == 0)
						{
							num5 = 11;
							continue;
						}
						goto IL_207;
					case 18:
						goto IL_24E;
					case 19:
						goto IL_24E;
					case 20:
						this.\u171A = @string.Substring(num2, num3 - num2);
						num2 = num3 + 1;
						num5 = 27;
						continue;
					case 21:
						goto IL_369;
					case 22:
						if (num == 3)
						{
							num5 = 23;
							continue;
						}
						goto IL_3E0;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_226;
						default:
							if (false)
							{
							}
							num5 = 0;
							continue;
						}
						break;
					case 24:
						if (this.\u171A.Length == 0)
						{
							num5 = 20;
							continue;
						}
						goto IL_38E;
					case 25:
					{
						int num7;
						int num8;
						if (num7 >= num8)
						{
							num5 = 14;
							continue;
						}
						array[num7] = A_0[num3];
						num3++;
						num7++;
						num5 = 19;
						continue;
					}
					case 26:
						this.\u171B = @string.Substring(num2, num3 - num2);
						num5 = 8;
						continue;
					case 27:
						if (this.\u171A.EndsWith(ClipboardData.b("山ͳ᭵๷", a_)))
						{
							num5 = 15;
							continue;
						}
						goto IL_3E0;
					case 28:
						num5 = 17;
						continue;
					case 29:
						goto IL_226;
					case 30:
						goto IL_178;
					case 31:
						num++;
						num5 = 30;
						continue;
					case 32:
						if (num >= num4)
						{
							num5 = 1;
							continue;
						}
						num5 = 2;
						continue;
					case 33:
						if (num == 2)
						{
							num5 = 29;
							continue;
						}
						goto IL_38E;
					}
					break;
					IL_152:
					num5 = 5;
					continue;
					IL_178:
					num5 = 4;
					continue;
					IL_207:
					num5 = 33;
					continue;
					IL_226:
					num5 = 24;
					continue;
					IL_24E:
					num5 = 25;
					continue;
					IL_369:
					num5 = 32;
					continue;
					IL_38E:
					num5 = 22;
					continue;
					IL_3E0:
					num3++;
					num5 = 21;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x001C0BF4 File Offset: 0x001BFBF4
	internal byte[] ᜀ(byte[] A_0, string A_1, string A_2, OleLinkType A_3, OleObjectType A_4)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			sprᤘ sprᤘ;
			sprᤘ sprᤘ2;
			for (;;)
			{
				MemoryStream a_2 = new MemoryStream(A_0);
				sprᤘ = new sprᤘ(a_2, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
				sprᤘ2 = sprᤘ.ᜀ(ClipboardData.b("⑪ཬծᑰၲŴ❶ᙸᑺᅼ", a_), STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
				sprᤘ a_3 = sprᤘ2.ᜀ(A_2, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
				this.ᜀ(a_3, A_3, A_4, A_1);
				this.ᜀ(a_3, A_3, A_4);
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_BD;
					case 1:
						goto IL_106;
					case 2:
						if (A_3 == OleLinkType.Embed)
						{
							num = 3;
							continue;
						}
						this.ᜀ(a_3, A_4, A_1);
						num = 0;
						continue;
					case 3:
						this.\u1715 = File.ReadAllBytes(A_1);
						this.ᜀ(a_3, A_4);
						this.ᜀ(a_3, A_1, A_4);
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
					}
					break;
				}
			}
			IL_BD:
			IL_106:
			sprᤘ2.Flush();
			sprᤘ.Flush();
			MemoryStream memoryStream = new MemoryStream();
			sprᤘ.ᜀ(memoryStream);
			memoryStream.Flush();
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			sprᤘ.Close();
			sprᤘ.Dispose();
			sprᤘ2.Close();
			sprᤘ2.Dispose();
			return result;
		}
		}
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x001C0D58 File Offset: 0x001BFD58
	internal byte[] ᜀ(byte[] A_0, byte[] A_1, string A_2, DocOleObject A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			byte[] result;
			for (;;)
			{
				MemoryStream a_2 = new MemoryStream(A_0);
				string text = ClipboardData.b("Ⱳ", a_) + A_3.OleStorageName;
				int num = 1;
				for (;;)
				{
					sprᤘ a_3;
					switch (num)
					{
					case 0:
						goto IL_176;
					case 1:
						if (A_3.ᜑ == null)
						{
							goto IL_CA;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14B;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						if (true)
						{
						}
						result = this.ᜀ(a_2, A_3, text);
						num = 7;
						continue;
					case 3:
						num = 9;
						continue;
					case 4:
						if (A_3.LinkType == OleLinkType.Embed)
						{
							num = 5;
							continue;
						}
						this.ᜀ(a_3, A_3.OleObjectType, A_2);
						num = 0;
						continue;
					case 5:
						goto IL_14B;
					case 6:
						return result;
					case 7:
						return result;
					case 8:
						goto IL_176;
					case 9:
						if (A_3.OleObjectType == OleObjectType.Undefined)
						{
							num = 2;
							continue;
						}
						goto IL_CA;
					}
					break;
					IL_CA:
					sprᤘ sprᤘ = new sprᤘ(a_2, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
					sprᤘ sprᤘ2 = null;
					sprᤘ2 = sprᤘ.ᜀ(ClipboardData.b("㱲᝴ᵶᱸ᡺ॼ⽾", a_), STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
					a_3 = sprᤘ2.ᜀ(text, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
					this.ᜀ(a_3, A_3.LinkType, A_3.OleObjectType, A_2);
					this.ᜀ(a_3, A_3.LinkType, A_3.OleObjectType);
					num = 4;
					continue;
					IL_14B:
					this.\u1715 = A_1;
					this.ᜀ(a_3, A_3.OleObjectType);
					this.ᜀ(a_3, A_2, A_3.OleObjectType);
					num = 8;
					continue;
					IL_176:
					sprᤘ2.Flush();
					sprᤘ.Flush();
					MemoryStream memoryStream = new MemoryStream();
					sprᤘ.ᜀ(memoryStream);
					memoryStream.Flush();
					result = memoryStream.ToArray();
					memoryStream.Close();
					sprᤘ.Close();
					sprᤘ.Dispose();
					sprᤘ2.Close();
					sprᤘ2.Dispose();
					num = 6;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x001C0F9C File Offset: 0x001BFF9C
	private byte[] ᜀ(MemoryStream A_0, DocOleObject A_1, string A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			spr\u20BF spr_u20BF2;
			spr\u20BF spr_u20BF3;
			for (;;)
			{
				spr\u20BF spr_u20BF = new spr\u20BF(A_0);
				spr\u2547 spr_u = spr_u20BF.ᜇ().ᜅ(ClipboardData.b("♨४ݬ੮ተݲ╴ᡶᙸ᝺", a_));
				spr_u20BF2 = new spr\u20BF();
				spr_u20BF2.ᜇ().ᜀ(spr_u);
				spr\u2547 spr_u2 = spr_u20BF2.ᜇ().ᜅ(ClipboardData.b("♨४ݬ੮ተݲ╴ᡶᙸ᝺", a_));
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					List<spr\u2486>.Enumerator enumerator;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_131;
						default:
							if (false)
							{
							}
							if (spr_u.ᜇ(A_2))
							{
								num = 7;
								continue;
							}
							spr_u2 = spr_u2.ᜄ(A_2);
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_136;
					case 2:
						goto IL_131;
					case 3:
						goto IL_15F;
					case 4:
					{
						int num2;
						if (num2 >= spr_u20BF3.ᜇ().ᜁ().Length)
						{
							num = 3;
							continue;
						}
						spr\u2578 spr_u3 = spr_u20BF3.ᜇ().ᜁ(spr_u20BF3.ᜇ().ᜁ()[num2]);
						spr\u2578 spr_u4 = spr_u2.ᜀ(spr_u20BF3.ᜇ().ᜁ()[num2]);
						spr_u3.ᜀ(spr_u4);
						spr_u4.Flush();
						spr_u4.Close();
						spr_u3.Close();
						num2++;
						num = 1;
						continue;
					}
					case 5:
						goto IL_136;
					case 6:
					{
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 1;
									continue;
								case 1:
									goto IL_297;
								case 3:
								{
									spr\u2486 spr_u5;
									if (A_1.\u1712.ContainsKey(spr_u5.ᜀ()))
									{
										num = 5;
										continue;
									}
									break;
								}
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									spr\u2486 spr_u5 = enumerator.Current;
									num = 3;
									continue;
								}
								case 5:
								{
									spr\u2486 spr_u5;
									spr_u5.ᜀ(A_1.\u1712[spr_u5.ᜀ()]);
									num = 6;
									continue;
								}
								}
								IL_247:
								num = 4;
								continue;
								goto IL_247;
							}
							IL_297:
							goto IL_E6;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_2AA;
						IL_E6:
						A_1.\u1712.Clear();
						spr_u20BF3 = new spr\u20BF(A_1.ᜑ);
						A_1.ᜑ.Position = 0L;
						int num2 = 0;
						num = 5;
						continue;
					}
					case 7:
						spr_u2 = spr_u2.ᜅ(A_2);
						num = 8;
						continue;
					case 8:
						goto IL_2AA;
					}
					break;
					IL_136:
					num = 4;
					continue;
					IL_2AA:
					spr_u20BF.ᜊ();
					enumerator = spr_u20BF2.\u170D().ᜁ().GetEnumerator();
					num = 6;
					continue;
					IL_131:
					goto IL_2AA;
				}
			}
			IL_15F:
			spr_u20BF3.ᜊ();
			spr_u20BF2.ᜆ();
			byte[] result = (spr_u20BF2.ᜉ() as MemoryStream).ToArray();
			spr_u20BF2.ᜊ();
			return result;
		}
		}
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x001C12B4 File Offset: 0x001C02B4
	private void ᜀ(sprᤘ A_0, string A_1, OleObjectType A_2)
	{
		int a_ = 12;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_15E;
				case 1:
					switch (A_2)
					{
					case OleObjectType.AdobeAcrobatDocument:
						goto IL_160;
					case OleObjectType.BitmapImage:
						goto IL_12C;
					case OleObjectType.MediaClip:
					case OleObjectType.MIDISequence:
					case OleObjectType.OpenDocumentText:
					case OleObjectType.VideoClip:
					case OleObjectType.WaveSound:
						return;
					case OleObjectType.Equation:
						goto IL_F6;
					case OleObjectType.GraphChart:
						goto IL_135;
					case OleObjectType.Excel_97_2003_Worksheet:
					case OleObjectType.ExcelChart:
					case OleObjectType.PowerPoint_97_2003_Presentation:
					case OleObjectType.PowerPoint_97_2003_Slide:
					case OleObjectType.Word_97_2003_Document:
					case OleObjectType.VisioDrawing:
					case OleObjectType.OpenOfficeSpreadsheet1_1:
					case OleObjectType.OpenOfficeText_1_1:
					case OleObjectType.OpenOfficeSpreadsheet:
					case OleObjectType.OpenOfficeText:
						goto IL_18C;
					case OleObjectType.ExcelBinaryWorksheet:
					case OleObjectType.ExcelMacroWorksheet:
					case OleObjectType.ExcelWorksheet:
					case OleObjectType.PowerPointMacroPresentation:
					case OleObjectType.PowerPointMacroSlide:
					case OleObjectType.PowerPointPresentation:
					case OleObjectType.PowerPointSlide:
					case OleObjectType.WordDocument:
					case OleObjectType.WordMacroDocument:
						goto IL_116;
					case OleObjectType.OpenDocumentPresentation:
					case OleObjectType.OpenDocumentSpreadsheet:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_DA;
						}
						break;
					case OleObjectType.Package:
						this.ᜀ(A_0, A_1);
						num = 0;
						continue;
					case OleObjectType.WordPadDocument:
						goto IL_176;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
		return;
		IL_DA:
		if (false)
		{
		}
		this.ᜁ(A_0, ClipboardData.b("㝱ᥳᑵᵷṹ᡻᭽춁", a_));
		return;
		IL_F6:
		this.ᜁ(A_0, ClipboardData.b("㝱ճ͵᥷๹ᕻᅽꊁ쪃ﲇ懲", a_));
		return;
		IL_116:
		this.ᜁ(A_0, ClipboardData.b("≱ᕳᕵ፷᭹᭻᭽", a_));
		return;
		IL_12C:
		this.ᜀ(A_0);
		return;
		IL_135:
		this.ᜁ(A_0, ClipboardData.b("╱᭳ѵ፷᡹፻ᅽ", a_));
		return;
		IL_15E:
		return;
		IL_160:
		this.ᜁ(A_0, ClipboardData.b("ㅱ㭳㡵ⱷ㽹㉻⩽퍿", a_));
		return;
		IL_176:
		this.ᜁ(A_0, ClipboardData.b("ㅱ᭳ᡵ౷όቻ੽", a_));
		return;
		IL_18C:
		MemoryStream a_2 = new MemoryStream(this.\u1715);
		this.ᜀ(A_0, a_2);
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x001C1464 File Offset: 0x001C0464
	private void ᜁ(sprᤘ A_0, string A_1)
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
		A_0.ᜁ(A_1);
		A_0.Write(this.\u1715, 0, this.\u1715.Length);
		A_0.Close();
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x001C14C4 File Offset: 0x001C04C4
	private void ᜀ(sprᤘ A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = 0;
		byte[] array = new byte[this.\u1715.Length + 4];
		spr\u2562.ᜀ(array, ref num, this.\u1715.Length);
		spr\u2562.ᜀ(array, ref num, this.\u1715);
		A_0.ᜁ(ClipboardData.b("獱㭳᩵ᵷ䭹䱻ぽ", a_));
		A_0.Write(array, 0, array.Length);
		A_0.Close();
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x001C1560 File Offset: 0x001C0560
	private void ᜀ(sprᤘ A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u20BF spr_u20BF;
			for (;;)
			{
				IL_2B:
				spr_u20BF = new spr\u20BF(A_1);
				string[] array = spr_u20BF.ᜇ().ᜁ();
				int num = 0;
				int num2 = array.Length;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_84:
					num3 = 3;
					break;
				default:
					if (false)
					{
					}
					num3 = 2;
					break;
				}
				spr\u2578 spr_u;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_DF;
					case 1:
					{
						if (num >= num2)
						{
							num3 = 4;
							continue;
						}
						spr_u = spr_u20BF.ᜇ().ᜁ(array[num]);
						A_0.ᜁ(array[num]);
						byte[] array2 = new byte[spr_u.Length];
						spr_u.Read(array2, 0, array2.Length);
						A_0.Write(array2, 0, array2.Length);
						num3 = 0;
						continue;
					}
					case 2:
						goto IL_E1;
					case 3:
						goto IL_E1;
					case 4:
						goto IL_107;
					}
					goto IL_2B;
					IL_E1:
					num3 = 1;
				}
				IL_DF:
				try
				{
					A_0.Flush();
					goto IL_6B;
				}
				catch
				{
					goto IL_6B;
				}
				break;
				IL_6B:
				if (true)
				{
				}
				A_0.Close();
				spr_u.Close();
				num++;
				goto IL_84;
			}
			IL_107:
			spr_u20BF.ᜊ();
			return;
		}
		}
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x001C16A0 File Offset: 0x001C06A0
	private void ᜀ(sprᤘ A_0, OleObjectType A_1)
	{
		int a_ = 10;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch (A_1)
						{
						case OleObjectType.AdobeAcrobatDocument:
						case OleObjectType.BitmapImage:
						case OleObjectType.Equation:
						case OleObjectType.GraphChart:
						case OleObjectType.Excel_97_2003_Worksheet:
						case OleObjectType.ExcelBinaryWorksheet:
						case OleObjectType.ExcelChart:
						case OleObjectType.ExcelMacroWorksheet:
						case OleObjectType.ExcelWorksheet:
						case OleObjectType.PowerPoint_97_2003_Presentation:
						case OleObjectType.PowerPoint_97_2003_Slide:
						case OleObjectType.PowerPointMacroPresentation:
						case OleObjectType.PowerPointMacroSlide:
						case OleObjectType.PowerPointPresentation:
						case OleObjectType.PowerPointSlide:
						case OleObjectType.Word_97_2003_Document:
						case OleObjectType.WordDocument:
						case OleObjectType.WordMacroDocument:
						case OleObjectType.VisioDrawing:
						case OleObjectType.OpenDocumentPresentation:
						case OleObjectType.OpenDocumentSpreadsheet:
						case OleObjectType.OpenOfficeSpreadsheet1_1:
						case OleObjectType.OpenOfficeText_1_1:
						case OleObjectType.Package:
						case OleObjectType.OpenOfficeSpreadsheet:
						case OleObjectType.OpenOfficeText:
							num = 3;
							continue;
						case OleObjectType.MediaClip:
						case OleObjectType.MIDISequence:
						case OleObjectType.OpenDocumentText:
						case OleObjectType.VideoClip:
						case OleObjectType.WaveSound:
						case OleObjectType.WordPadDocument:
							return;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					case 3:
						if (!this.ᜀ(A_0.ᜎ(), ClipboardData.b("煯ㅱ᭳᭵ࡷ㕹ṻᑽ", a_)))
						{
							num = 4;
							continue;
						}
						return;
					case 4:
						A_0.ᜁ(ClipboardData.b("煯ㅱ᭳᭵ࡷ㕹ṻᑽ", a_));
						this.\u1718 = new sprᬟ(A_1);
						this.\u1718.ᜀ(A_0);
						A_0.Close();
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x001C1814 File Offset: 0x001C0814
	private bool ᜀ(string[] A_0, string A_1)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 0;
			int num2 = A_0.Length;
			int num3 = 6;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return result;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						goto IL_96;
					}
					break;
				case 2:
					if (A_0[num] == A_1)
					{
						num3 = 5;
						continue;
					}
					num++;
					num3 = 1;
					continue;
				case 3:
					return result;
				case 4:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					num3 = 2;
					continue;
				case 5:
					result = true;
					num3 = 0;
					continue;
				case 6:
					goto IL_96;
				}
				break;
				IL_96:
				if (true)
				{
				}
				num3 = 4;
			}
		}
		return result;
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x001C18DC File Offset: 0x001C08DC
	private void ᜀ(sprᤘ A_0, OleObjectType A_1, string A_2)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.ᜁ(ClipboardData.b("畵㑷፹ቻᕽ쥿", a_));
		this.\u1717 = new spr\u17FD(A_2);
		this.\u1717.ᜀ(A_0);
		A_0.Close();
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x001C1954 File Offset: 0x001C0954
	private void ᜀ(sprᤘ A_0, OleLinkType A_1, OleObjectType A_2, string A_3)
	{
		int a_ = 5;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_121;
				case 2:
					for (;;)
					{
						switch (A_2)
						{
						case OleObjectType.AdobeAcrobatDocument:
						case OleObjectType.BitmapImage:
						case OleObjectType.Equation:
						case OleObjectType.GraphChart:
						case OleObjectType.Excel_97_2003_Worksheet:
						case OleObjectType.ExcelBinaryWorksheet:
						case OleObjectType.ExcelChart:
						case OleObjectType.ExcelMacroWorksheet:
						case OleObjectType.ExcelWorksheet:
						case OleObjectType.PowerPoint_97_2003_Presentation:
						case OleObjectType.PowerPoint_97_2003_Slide:
						case OleObjectType.PowerPointMacroPresentation:
						case OleObjectType.PowerPointMacroSlide:
						case OleObjectType.PowerPointSlide:
						case OleObjectType.VisioDrawing:
						case OleObjectType.OpenDocumentPresentation:
						case OleObjectType.OpenDocumentSpreadsheet:
						case OleObjectType.OpenOfficeSpreadsheet1_1:
						case OleObjectType.OpenOfficeText_1_1:
						case OleObjectType.Package:
						case OleObjectType.WordPadDocument:
						case OleObjectType.OpenOfficeText:
							goto IL_E2;
						case OleObjectType.MediaClip:
						case OleObjectType.PowerPointPresentation:
						case OleObjectType.Word_97_2003_Document:
						case OleObjectType.WordDocument:
						case OleObjectType.WordMacroDocument:
						case OleObjectType.MIDISequence:
						case OleObjectType.OpenDocumentText:
						case OleObjectType.VideoClip:
						case OleObjectType.WaveSound:
						case OleObjectType.OpenOfficeSpreadsheet:
							goto IL_124;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_CF;
							}
							break;
						}
					}
					IL_CF:
					if (false)
					{
					}
					num = 0;
					continue;
					IL_E2:
					A_0.ᜁ(ClipboardData.b("橪≬ͮᑰ", a_));
					this.\u1714 = new sprᣕ(A_1, A_3);
					this.\u1714.ᜀ(A_0);
					A_0.Close();
					num = 1;
					continue;
				}
				break;
			}
		}
		return;
		IL_121:
		IL_124:
		if (true)
		{
		}
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x001C1A90 File Offset: 0x001C0A90
	private void ᜀ(sprᤘ A_0, OleLinkType A_1, OleObjectType A_2)
	{
		int a_ = 4;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.ᜁ(ClipboardData.b("楩⍫౭ᩯ㭱ᩳၵ᝷", a_));
		this.\u1716 = new spr\u257F();
		this.\u1716.ᜀ(A_0, A_1, A_2);
		A_0.Close();
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x001C1B08 File Offset: 0x001C0B08
	private void ᜀ(sprᤘ A_0, string A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_7D;
				case 2:
					A_1 = this.\u171B;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				goto IL_35;
				IL_3D:
				num = 2;
				continue;
				IL_35:
				if (string.IsNullOrEmpty(A_1))
				{
					goto IL_3D;
				}
				break;
			}
			IL_7D:
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			string fileName = Path.GetFileName(A_1);
			byte[] bytes = asciiencoding.GetBytes(fileName);
			byte[] bytes2 = asciiencoding.GetBytes(A_1);
			byte[] array = new byte[2];
			array[0] = 2;
			byte[] array2 = array;
			byte[] array3 = new byte[4];
			array3[2] = 3;
			byte[] array4 = array3;
			int num2 = 4;
			num2 += array2.Length;
			num2 += bytes.Length + 1;
			num2 += bytes2.Length + 1;
			num2 += array4.Length;
			num2 += 4;
			num2 += bytes2.Length + 1;
			num2 += 4;
			num2 += this.\u1715.Length;
			num2 += 2;
			int num3 = 0;
			byte[] array5 = new byte[num2];
			spr\u2562.ᜀ(array5, ref num3, num2 - 4);
			spr\u2562.ᜀ(array5, ref num3, array2);
			spr\u2562.ᜀ(array5, ref num3, bytes);
			num3++;
			spr\u2562.ᜀ(array5, ref num3, bytes2);
			num3++;
			spr\u2562.ᜀ(array5, ref num3, array4);
			spr\u2562.ᜀ(array5, ref num3, bytes2.Length + 1);
			spr\u2562.ᜀ(array5, ref num3, bytes2);
			num3++;
			spr\u2562.ᜀ(array5, ref num3, this.\u1715.Length);
			spr\u2562.ᜀ(array5, ref num3, this.\u1715);
			A_0.ᜁ(ClipboardData.b("畳㥵ᑷό䵻乽칿ﺇ", a_));
			A_0.Write(array5, 0, array5.Length);
			A_0.Close();
			return;
		}
		}
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x001C1CE4 File Offset: 0x001C0CE4
	internal void ᜀ(string A_0, string A_1)
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
		this.\u171A = A_0;
		this.\u171B = A_1;
	}

	// Token: 0x04001E6B RID: 7787
	private const string ᜀ = "\u0001Ole";

	// Token: 0x04001E6C RID: 7788
	private const string ᜁ = "CONTENTS";

	// Token: 0x04001E6D RID: 7789
	private const string ᜂ = "Contents";

	// Token: 0x04001E6E RID: 7790
	private const string ᜃ = "\u0003ObjInfo";

	// Token: 0x04001E6F RID: 7791
	private const string ᜄ = "\u0001CompObj";

	// Token: 0x04001E70 RID: 7792
	private const string ᜅ = "\u0003LinkInfo";

	// Token: 0x04001E71 RID: 7793
	private const string ᜆ = "\u0001Ole10Native";

	// Token: 0x04001E72 RID: 7794
	private const string ᜇ = "\u0003EPRINT";

	// Token: 0x04001E73 RID: 7795
	private const string ᜈ = "\u0002OlePres000";

	// Token: 0x04001E74 RID: 7796
	private const string ᜉ = "???";

	// Token: 0x04001E75 RID: 7797
	private const string ᜊ = "Equation Native";

	// Token: 0x04001E76 RID: 7798
	private const string ᜋ = "Workbook";

	// Token: 0x04001E77 RID: 7799
	private const string ᜌ = "Package";

	// Token: 0x04001E78 RID: 7800
	private const string \u170D = "PowerPoint Document";

	// Token: 0x04001E79 RID: 7801
	private const string ᜎ = "WordDocument";

	// Token: 0x04001E7A RID: 7802
	private const string ᜏ = "VisioDocument";

	// Token: 0x04001E7B RID: 7803
	private const string ᜐ = "EmbeddedOdf";

	// Token: 0x04001E7C RID: 7804
	private const string ᜑ = "package_stream";

	// Token: 0x04001E7D RID: 7805
	private const string \u1712 = "\u0005SummaryInformation";

	// Token: 0x04001E7E RID: 7806
	private const string \u1713 = "\u0005DocumentSummaryInformation";

	// Token: 0x04001E7F RID: 7807
	private sprᣕ \u1714;

	// Token: 0x04001E80 RID: 7808
	private byte[] \u1715;

	// Token: 0x04001E81 RID: 7809
	private spr\u257F \u1716;

	// Token: 0x04001E82 RID: 7810
	private spr\u17FD \u1717;

	// Token: 0x04001E83 RID: 7811
	private sprᬟ \u1718;

	// Token: 0x04001E84 RID: 7812
	private OleObjectType \u1719;

	// Token: 0x04001E85 RID: 7813
	private string \u171A;

	// Token: 0x04001E86 RID: 7814
	private string \u171B;
}
