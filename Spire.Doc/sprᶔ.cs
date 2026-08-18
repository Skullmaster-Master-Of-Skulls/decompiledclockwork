using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Base;

// Token: 0x02000412 RID: 1042
internal class sprᶔ
{
	// Token: 0x060039E1 RID: 14817 RVA: 0x0035CDC0 File Offset: 0x0035BDC0
	private sprᶔ(sprά A_0)
	{
		this.ᜂ = A_0;
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x0035CDDC File Offset: 0x0035BDDC
	internal static void ᜀ(Stream A_0, spr\u2281 A_1, DigitalSignatures A_2, sprά A_3)
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
		sprᶔ sprᶔ = new sprᶔ(A_3);
		sprᶔ.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x060039E3 RID: 14819 RVA: 0x0035CE28 File Offset: 0x0035BE28
	private void ᜀ(Stream A_0, spr\u2281 A_1, DigitalSignatures A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_181;
				case 1:
				{
					int num2;
					bool a_2 = sprᶔ.ᜀ(A_0, num2++);
					A_2.ᜀ(this.ᜀ(A_1, a_2));
					num = 10;
					continue;
				}
				case 2:
					return;
				case 3:
				{
					if (this.ᜁ.\u171F() == ClipboardData.b("≰ᩲቴ᥶ᡸེࡼൾ", a_))
					{
						goto IL_102;
					}
					int num2 = 0;
					num = 0;
					continue;
				}
				case 5:
					goto IL_10E;
				case 6:
					goto IL_181;
				case 7:
				{
					if (true)
					{
					}
					string a;
					if ((a = this.ᜁ.\u171F()) != null)
					{
						num = 8;
						continue;
					}
					goto IL_113;
				}
				case 8:
					num = 11;
					continue;
				case 9:
					return;
				case 10:
					goto IL_181;
				case 11:
				{
					string a;
					if (a == ClipboardData.b("≰ᩲቴ᥶ᡸེࡼൾ", a_))
					{
						num = 1;
						continue;
					}
					goto IL_113;
				}
				case 12:
					if (!this.ᜁ.ᜃ(ClipboardData.b("ᕰᱲᙴɶᑸṺ፼୾검歷搜", a_)))
					{
						num = 9;
						continue;
					}
					num = 7;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				this.ᜁ = new spr\u20C4(A_0);
				num = 3;
				continue;
				IL_102:
				num = 5;
				continue;
				IL_113:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_102;
				default:
					if (false)
					{
					}
					this.ᜀ();
					num = 6;
					continue;
				}
				IL_181:
				num = 12;
			}
			return;
			IL_10E:
			bool a_3 = sprᶔ.ᜀ(A_0, 0);
			A_2.ᜀ(this.ᜀ(A_1, a_3));
			return;
		}
		}
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x0035D018 File Offset: 0x0035C018
	private static bool ᜀ(Stream A_0, int A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			long position;
			SignedXml signedXml;
			for (;;)
			{
				position = A_0.Position;
				A_0.Position = 0L;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(A_0);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(ClipboardData.b("❳ήίᑹᵻ੽", a_));
				int num = 3;
				for (;;)
				{
					int num2;
					Reference reference;
					switch (num)
					{
					case 0:
						return false;
					case 1:
						goto IL_DB;
					case 2:
						if (num2 < signedXml.SignedInfo.References.Count)
						{
							num = 5;
							continue;
						}
						goto IL_1B2;
					case 3:
						if (elementsByTagName.Count <= 0)
						{
							num = 0;
							continue;
						}
						signedXml = new SignedXml();
						signedXml.LoadXml((XmlElement)elementsByTagName[A_1]);
						num2 = 0;
						num = 4;
						continue;
					case 4:
						goto IL_DB;
					case 5:
						goto IL_155;
					case 6:
						goto IL_DB;
					case 7:
						if (!reference.Uri.StartsWith(ClipboardData.b("坳", a_)))
						{
							num = 8;
							continue;
						}
						num2++;
						num = 6;
						continue;
					case 8:
						signedXml.SignedInfo.References.RemoveAt(num2);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_155;
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
					IL_DB:
					num = 2;
					continue;
					IL_155:
					if (true)
					{
					}
					reference = (Reference)signedXml.SignedInfo.References[num2];
					num = 7;
				}
			}
			return false;
			IL_1B2:
			A_0.Position = position;
			return signedXml.CheckSignature();
		}
		}
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x0035D1E4 File Offset: 0x0035C1E4
	private DigitalSignature ᜀ(spr\u2281 A_0, bool A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			bool flag2;
			for (;;)
			{
				this.ᜀ = new DigitalSignature(DigitalSignatureType.XmlDsig);
				this.ᜀ.SignedXmlResult = A_1;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_87;
					case 1:
						num = 15;
						continue;
					case 2:
						goto IL_384;
					case 3:
					{
						string a;
						if (!(a == ClipboardData.b("⥥੧i५൭ѯ", a_)))
						{
							num = 16;
							continue;
						}
						this.ᜉ();
						num = 7;
						continue;
					}
					case 4:
						try
						{
							num = 1;
							for (;;)
							{
								sprᣔ sprᣔ;
								IEnumerator enumerator;
								Stream stream;
								switch (num)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_1B8;
									default:
										if (false)
										{
										}
										if (sprᣔ.ᜂ())
										{
											num = 3;
											continue;
										}
										sprᣔ.ᜀ(A_1);
										num = 4;
										continue;
									}
									break;
								case 2:
									num = 6;
									continue;
								case 3:
									goto IL_1B8;
								case 5:
								{
									try
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 2;
												continue;
											case 2:
												goto IL_268;
											case 4:
											{
												if (!enumerator.MoveNext())
												{
													num = 0;
													continue;
												}
												spr\u233A spr_u233A = (spr\u233A)enumerator.Current;
												stream = spr_u233A.ᜁ(stream);
												num = 3;
												continue;
											}
											}
											IL_220:
											num = 4;
											continue;
											goto IL_220;
										}
										IL_268:;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable = enumerator as IDisposable;
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													disposable.Dispose();
													num = 1;
													continue;
												case 1:
													goto IL_2B0;
												case 2:
													if (disposable != null)
													{
														num = 0;
														continue;
													}
													goto IL_2B2;
												}
												break;
											}
										}
										IL_2B0:
										IL_2B2:;
									}
									stream.Position = 0L;
									SHA1 sha;
									string a2 = Convert.ToBase64String(sha.ComputeHash(stream));
									bool flag = a2 == sprᣔ.ᜃ();
									sprᣔ.ᜀ(flag);
									flag2 = (flag2 && flag);
									num = 8;
									continue;
								}
								case 6:
									goto IL_302;
								case 7:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 2;
										continue;
									}
									sprᣔ = (sprᣔ)enumerator2.Current;
									num = 0;
									continue;
								}
								}
								goto IL_147;
								IL_1B8:
								stream = A_0.ᜀ(sprᣔ);
								enumerator = sprᣔ.ᜅ().GetEnumerator();
								num = 5;
								continue;
								IL_1BA:
								num = 7;
								continue;
								IL_147:
								goto IL_1BA;
							}
							IL_302:
							goto IL_46E;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable2 = enumerator2 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable2 != null)
										{
											num = 2;
											continue;
										}
										goto IL_34F;
									case 1:
										goto IL_34D;
									case 2:
										disposable2.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_34D:
							IL_34F:;
						}
						goto IL_350;
					case 5:
						goto IL_87;
					case 6:
					{
						string a;
						if ((a = this.ᜁ.\u171F()) != null)
						{
							num = 12;
							continue;
						}
						goto IL_384;
					}
					case 7:
						if (true)
						{
						}
						goto IL_87;
					case 8:
						num = 3;
						continue;
					case 9:
						goto IL_87;
					case 10:
					{
						flag2 = A_1;
						SHA1 sha = new SHA1CryptoServiceProvider();
						IEnumerator enumerator2 = this.ᜀ.References.GetEnumerator();
						num = 4;
						continue;
					}
					case 11:
						if (!this.ᜁ.ᜃ(ClipboardData.b("㕥ŧ൩ɫ཭ѯݱٳ፵", a_)))
						{
							num = 10;
							continue;
						}
						num = 6;
						continue;
					case 12:
						goto IL_350;
					case 13:
					{
						string a;
						if (!(a == ClipboardData.b("㕥ŧ൩ɫ୭ᑯ㭱ᩳၵ᝷", a_)))
						{
							num = 1;
							continue;
						}
						this.ᜆ();
						num = 14;
						continue;
					}
					case 14:
						goto IL_87;
					case 15:
					{
						string a;
						if (!(a == ClipboardData.b("ⵥ൧፩╫mᙯᵱ", a_)))
						{
							num = 8;
							continue;
						}
						this.ᜋ();
						num = 9;
						continue;
					}
					case 16:
						num = 2;
						continue;
					}
					break;
					IL_87:
					num = 11;
					continue;
					IL_350:
					num = 13;
					continue;
					IL_384:
					this.ᜀ();
					num = 0;
				}
			}
			IL_46E:
			this.ᜀ.ᜂ(flag2);
			return this.ᜀ;
		}
		}
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x0035D6A8 File Offset: 0x0035C6A8
	private void ᜋ()
	{
		int a_ = 7;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜊ();
				num = 8;
				continue;
			case 1:
				goto IL_E4;
			case 2:
				if (!this.ᜁ.ᜃ(ClipboardData.b("♬੮ࡰ㩲᭴ᅶᙸ", a_)))
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 3:
			{
				string a;
				if (a == ClipboardData.b("㕬婮䅰䩲ㅴᙶ൸᩺", a_))
				{
					num = 0;
					continue;
				}
				goto IL_3F;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E4;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
				return;
			case 6:
			{
				string a;
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 1;
					continue;
				}
				goto IL_3F;
			}
			}
			goto IL_3D;
			IL_3F:
			this.ᜀ();
			num = 4;
			continue;
			IL_9F:
			if (true)
			{
			}
			num = 2;
			continue;
			IL_3D:
			goto IL_9F;
			IL_E4:
			num = 3;
		}
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x0035D7D0 File Offset: 0x0035C7D0
	private void ᜊ()
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ.ᜀ(new X509Certificate2(Encoding.UTF8.GetBytes(this.ᜁ.\u1714())));
				num = 8;
				continue;
			case 1:
				return;
			case 2:
				goto IL_10C;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10C;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
			{
				string a;
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 2;
					continue;
				}
				goto IL_42;
			}
			case 6:
			{
				string a;
				if (a == ClipboardData.b("㹥嵧婩啫⵭ᕯqsήṷ፹ύώ", a_))
				{
					num = 0;
					continue;
				}
				goto IL_42;
			}
			case 7:
				if (!this.ᜁ.ᜃ(ClipboardData.b("㹥嵧婩啫⩭ᅯٱᕳ", a_)))
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			}
			goto IL_3D;
			IL_42:
			this.ᜀ();
			num = 4;
			continue;
			IL_CF:
			num = 7;
			continue;
			IL_3D:
			goto IL_CF;
			IL_10C:
			num = 6;
		}
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x0035D920 File Offset: 0x0035C920
	private void ᜉ()
	{
		int a_ = 5;
		for (;;)
		{
			string text = this.ᜁ.ᜀ(ClipboardData.b("≪६", a_), null);
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("ɪ६㥮ၰὲᱴ፶⩸ቺ᩼㍾쪂", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_D5;
				}
				case 2:
				{
					string a;
					if (!(a == ClipboardData.b("ɪ६⁮ᝰᕲᱴᑶᱸ㑺ὼᕾ", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_F8;
				}
				case 3:
					goto IL_73;
				case 4:
					num = 8;
					continue;
				case 5:
					num = 2;
					continue;
				case 6:
				{
					string a;
					if ((a = text) != null)
					{
						num = 10;
						continue;
					}
					return;
				}
				case 7:
					if (text == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 8:
				{
					string a;
					if (!(a == ClipboardData.b("ɪ६♮ὰղᑴ᭶ၸὺ⹼ᙾ쾂캆", a_)))
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F1;
					default:
						if (false)
						{
						}
						this.ᜀ.ImageBytesInvalid = Convert.FromBase64String(this.ᜁ.\u1714());
						num = 0;
						continue;
					}
					break;
				}
				case 9:
					return;
				case 10:
					num = 12;
					continue;
				case 11:
					num = 1;
					continue;
				case 12:
				{
					string a;
					if (!(a == ClipboardData.b("ɪ६㽮ၰၲṴᙶṸṺ㉼ᵾ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_75;
				}
				}
				break;
			}
		}
		IL_73:
		goto IL_F1;
		IL_75:
		this.ᜈ();
		return;
		IL_D5:
		this.ᜀ.ImageBytesValid = Convert.FromBase64String(this.ᜁ.\u1714());
		return;
		IL_F1:
		this.ᜈ();
		return;
		IL_F8:
		this.ᜇ();
	}

	// Token: 0x060039E9 RID: 14825 RVA: 0x0035DB18 File Offset: 0x0035CB18
	private void ᜈ()
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_12B;
			case 4:
				if (!this.ᜁ.ᜃ(ClipboardData.b("㹰ᅲὴቶེ᩸", a_)))
				{
					num = 10;
					continue;
				}
				if (true)
				{
				}
				num = 6;
				continue;
			case 6:
			{
				string a;
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 12;
					continue;
				}
				goto IL_12B;
			}
			case 7:
			{
				string a;
				if (!(a == ClipboardData.b("≰ᩲቴ᥶ᡸེࡼൾ펂麗ﾌﮎ", a_)))
				{
					num = 0;
					continue;
				}
				this.ᜅ();
				num = 1;
				continue;
			}
			case 8:
			{
				string a;
				if (!(a == ClipboardData.b("㱰ቲ᭴ṶὸṺ๼୾", a_)))
				{
					num = 11;
					continue;
				}
				this.ᜆ();
				num = 3;
				continue;
			}
			case 10:
				return;
			case 11:
				num = 7;
				continue;
			case 12:
				num = 8;
				continue;
			}
			IL_B9:
			num = 4;
			continue;
			goto IL_B9;
			IL_12B:
			this.ᜀ();
			num = 9;
		}
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x0035DCA4 File Offset: 0x0035CCA4
	private void ᜇ()
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				string a;
				if (a == ClipboardData.b("⭷፹᭻ၽ\uda89ﺋﾙ", a_))
				{
					goto IL_108;
				}
				goto IL_3F;
			}
			case 3:
				this.ᜅ();
				num = 4;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_108;
				default:
				{
					if (false)
					{
					}
					string a;
					if ((a = this.ᜁ.\u171F()) != null)
					{
						num = 6;
						continue;
					}
					goto IL_3F;
				}
				}
				break;
			case 6:
				num = 1;
				continue;
			case 7:
				if (true)
				{
				}
				break;
			case 8:
				if (!this.ᜁ.ᜃ(ClipboardData.b("㝷᡹ᙻ᭽", a_)))
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
			}
			goto IL_3D;
			IL_3F:
			this.ᜀ();
			num = 7;
			continue;
			IL_A7:
			num = 8;
			continue;
			IL_3D:
			goto IL_A7;
			IL_108:
			num = 3;
		}
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x0035DDCC File Offset: 0x0035CDCC
	private void ᜆ()
	{
		int a_ = 11;
		for (;;)
		{
			string a_2 = this.ᜁ.\u171F();
			int num = 5;
			for (;;)
			{
				string a;
				switch (num)
				{
				case 0:
					goto IL_B0;
				case 1:
					this.ᜀ.References.Add(this.ᜁ());
					num = 6;
					continue;
				case 2:
					goto IL_F0;
				case 3:
					if (a == ClipboardData.b("⍰ᙲ፴ቶ୸Ṻ፼᱾", a_))
					{
						num = 1;
						continue;
					}
					goto IL_71;
				case 4:
					num = 3;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
						if (false)
						{
						}
						goto IL_CC;
					}
					break;
				case 6:
					goto IL_CC;
				case 7:
					goto IL_CC;
				case 8:
					if (!this.ᜁ.ᜃ(a_2))
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				}
				break;
				IL_71:
				this.ᜀ();
				num = 7;
				continue;
				IL_B0:
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 4;
					continue;
				}
				goto IL_71;
				IL_CC:
				num = 8;
			}
		}
		IL_F0:
		if (true)
		{
		}
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x0035DF08 File Offset: 0x0035CF08
	private void ᜅ()
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
			{
				string a;
				if (a == ClipboardData.b("≰ᩲቴ᥶ᡸེࡼൾ펂麗ﾌﮎ", a_))
				{
					goto IL_108;
				}
				goto IL_47;
			}
			case 2:
				this.ᜄ();
				num = 4;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_108;
				default:
				{
					if (false)
					{
					}
					string a;
					if ((a = this.ᜁ.\u171F()) != null)
					{
						num = 0;
						continue;
					}
					goto IL_47;
				}
				}
				break;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				if (!this.ᜁ.ᜃ(ClipboardData.b("≰ᩲቴ᥶ᡸེࡼൾ펂麗ﾌﮎ", a_)))
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 7:
				return;
			}
			goto IL_45;
			IL_47:
			this.ᜀ();
			num = 8;
			continue;
			IL_A7:
			num = 6;
			continue;
			IL_45:
			goto IL_A7;
			IL_108:
			num = 2;
		}
	}

	// Token: 0x060039ED RID: 14829 RVA: 0x0035E030 File Offset: 0x0035D030
	private void ᜄ()
	{
		int a_ = 2;
		int num = 13;
		for (;;)
		{
			string a;
			switch (num)
			{
			case 0:
				return;
			case 2:
				num = 9;
				continue;
			case 3:
				if (!this.ᜁ.ᜃ(ClipboardData.b("㭧ͩ୫mᅯٱųѵᵷ⩹๻ᅽ", a_)))
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
			case 4:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1BA;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 5:
				goto IL_1BA;
			case 6:
				num = 12;
				continue;
			case 7:
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 2;
					continue;
				}
				goto IL_DF;
			case 9:
				if (!(a == ClipboardData.b("㭧ͩ୫mᅯٱųѵᵷ⹹ᕻ፽", a_)))
				{
					num = 4;
					continue;
				}
				this.ᜃ();
				num = 8;
				continue;
			case 11:
				num = 14;
				continue;
			case 12:
				goto IL_DF;
			case 14:
				if (!(a == ClipboardData.b("౧୩ᡫ୭", a_)))
				{
					num = 6;
					continue;
				}
				this.ᜀ.ᜀ(sprᶔ.ᜀ(this.ᜁ.\u1714()));
				num = 10;
				continue;
			}
			goto IL_63;
			IL_DF:
			this.ᜀ();
			num = 15;
			continue;
			IL_1BA:
			if (!(a == ClipboardData.b("㭧ͩ୫mᅯٱųѵᵷ㍹ቻ᡽풁떃", a_)))
			{
				num = 11;
				continue;
			}
			this.ᜂ();
			num = 1;
			continue;
			IL_12C:
			num = 3;
			continue;
			IL_63:
			goto IL_12C;
		}
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x0035E230 File Offset: 0x0035D230
	private void ᜃ()
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string a;
				if (a == ClipboardData.b("⁵᥷ᙹॻ᭽", a_))
				{
					goto IL_11D;
				}
				goto IL_3F;
			}
			case 1:
				if (!this.ᜁ.ᜃ(ClipboardData.b("╵ᅷᵹቻώ\udc87", a_)))
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11D;
				default:
				{
					if (false)
					{
					}
					string a;
					if ((a = this.ᜁ.\u171F()) != null)
					{
						num = 8;
						continue;
					}
					goto IL_3F;
				}
				}
				break;
			case 4:
				return;
			case 6:
				this.ᜀ.ᜀ(sprᶔ.ᜀ(this.ᜁ.\u1714()));
				num = 2;
				continue;
			case 7:
				if (true)
				{
				}
				break;
			case 8:
				num = 0;
				continue;
			}
			goto IL_3D;
			IL_3F:
			this.ᜀ();
			num = 7;
			continue;
			IL_BC:
			num = 1;
			continue;
			IL_3D:
			goto IL_BC;
			IL_11D:
			num = 6;
		}
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x0035E36C File Offset: 0x0035D36C
	private static DateTime ᜀ(string A_0)
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
		return DateTime.ParseExact(A_0, sprᶔ.ᜃ, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x0035E3BC File Offset: 0x0035D3BC
	private void ᜂ()
	{
		int a_ = 6;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string a;
				if (!(a == ClipboardData.b("㽫ݭᝯᱱᕳɵ൷ࡹ᥻⩽嬨", a_)))
				{
					num = 13;
					continue;
				}
				this.ᜀ.Text = this.ᜁ.\u1714();
				num = 14;
				continue;
			}
			case 1:
			{
				string a;
				if (!(a == ClipboardData.b("㽫ݭᝯᱱᕳɵ൷ࡹ᥻㝽", a_)))
				{
					num = 9;
					continue;
				}
				string text = this.ᜁ.\u1714();
				goto IL_8A;
			}
			case 2:
			{
				string a;
				if ((a = this.ᜁ.\u171F()) != null)
				{
					num = 10;
					continue;
				}
				goto IL_1E4;
			}
			case 5:
				num = 1;
				continue;
			case 8:
				num = 16;
				continue;
			case 9:
				num = 15;
				continue;
			case 10:
				num = 22;
				continue;
			case 11:
			{
				string text;
				this.ᜀ.ImageBytes = (spr\u1CC6.ᜋ(text) ? Convert.FromBase64String(text) : null);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8A;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			}
			case 13:
				num = 18;
				continue;
			case 15:
				goto IL_1E4;
			case 16:
			{
				string a;
				if (!(a == ClipboardData.b("㽫୭ѯݱѳ㽵㱷", a_)))
				{
					num = 21;
					continue;
				}
				string text2 = this.ᜁ.\u1714();
				num = 20;
				continue;
			}
			case 17:
				if (!this.ᜁ.ᜃ(ClipboardData.b("㽫ݭᝯᱱᕳɵ൷ࡹ᥻㝽킅릇", a_)))
				{
					num = 23;
					continue;
				}
				num = 2;
				continue;
			case 18:
			{
				string a;
				if (!(a == ClipboardData.b("㽫ݭᝯᱱᕳɵ൷ࡹ᥻⩽勵", a_)))
				{
					num = 5;
					continue;
				}
				this.ᜀ.Visible = (this.ᜁ.\u1714() == ClipboardData.b("幫", a_));
				num = 12;
				continue;
			}
			case 20:
			{
				string text2;
				this.ᜀ.SetupId = (spr\u1CC6.ᜋ(text2) ? new Guid(text2) : Guid.Empty);
				num = 6;
				continue;
			}
			case 21:
				if (true)
				{
				}
				num = 0;
				continue;
			case 22:
			{
				string a;
				if (!(a == ClipboardData.b("㽫ݭᝯᱱᕳɵ൷ࡹ᥻㵽ﺉﾋ", a_)))
				{
					num = 8;
					continue;
				}
				this.ᜀ.ᜀ(this.ᜁ.\u1714());
				num = 4;
				continue;
			}
			case 23:
				return;
			}
			goto IL_79;
			IL_8A:
			num = 11;
			continue;
			IL_1E4:
			this.ᜀ();
			num = 19;
			continue;
			IL_323:
			num = 17;
			continue;
			IL_79:
			goto IL_323;
		}
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x0035E724 File Offset: 0x0035D724
	private sprᣔ ᜁ()
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			string a_2;
			ArrayList arrayList;
			string a_3;
			for (;;)
			{
				a_2 = this.ᜁ.ᜀ(ClipboardData.b("❱♳㽵", a_), null);
				arrayList = new ArrayList();
				a_3 = "";
				int num = 6;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_154;
					case 1:
						goto IL_123;
					case 2:
						goto IL_F0;
					case 3:
					{
						string a;
						if (!(a == ClipboardData.b("♱ٳ᝵ᙷॹ᩻ᅽ", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_8E;
					}
					case 4:
					{
						string a;
						if (!(a == ClipboardData.b("㙱ᵳᅵᵷॹࡻ⡽", a_)))
						{
							num = 7;
							continue;
						}
						a_3 = this.ᜁ.\u1714();
						num = 9;
						continue;
					}
					case 5:
						if (!this.ᜁ.ᜃ(ClipboardData.b("ⁱᅳၵᵷࡹ᥻ၽ", a_)))
						{
							num = 1;
							continue;
						}
						num = 10;
						continue;
					case 6:
						goto IL_F0;
					case 7:
						num = 3;
						continue;
					case 8:
						num = 4;
						continue;
					case 9:
						goto IL_F0;
					case 10:
					{
						string a;
						if ((a = this.ᜁ.\u171F()) != null)
						{
							num = 8;
							continue;
						}
						goto IL_154;
					}
					case 11:
						goto IL_F0;
					case 12:
						num = 0;
						continue;
					}
					break;
					IL_8E:
					this.ᜀ(arrayList);
					num = 11;
					continue;
					IL_154:
					this.ᜀ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					IL_F0:
					num = 5;
				}
			}
			IL_123:
			return new sprᣔ(a_2, arrayList, a_3);
		}
		}
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x0035E90C File Offset: 0x0035D90C
	private void ᜀ(ArrayList A_0)
	{
		int a_ = 15;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜁ.ᜃ(ClipboardData.b("ⅴնᡸᕺ๼᥾", a_)))
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
			case 2:
				if (true)
				{
				}
				num = 3;
				continue;
			case 3:
			{
				string a;
				if (a == ClipboardData.b("ⅴնᡸᕺ๼᥾", a_))
				{
					goto IL_114;
				}
				goto IL_3F;
			}
			case 4:
				A_0.Add(new spr\u233A(this.ᜁ));
				num = 1;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_114;
				default:
				{
					if (false)
					{
					}
					string a;
					if ((a = this.ᜁ.\u171F()) != null)
					{
						num = 2;
						continue;
					}
					goto IL_3F;
				}
				}
				break;
			case 8:
				return;
			}
			goto IL_3D;
			IL_3F:
			this.ᜀ();
			num = 6;
			continue;
			IL_AB:
			num = 0;
			continue;
			IL_3D:
			goto IL_AB;
			IL_114:
			num = 4;
		}
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x0035EA40 File Offset: 0x0035DA40
	private void ᜀ(WarningTypeCore A_0, WarningSourceCore A_1, string A_2)
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

	// Token: 0x060039F4 RID: 14836 RVA: 0x0035EA7C File Offset: 0x0035DA7C
	private void ᜀ()
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
		this.ᜁ.ᜁ();
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x0035EAC4 File Offset: 0x0035DAC4
	// Note: this type is marked as 'beforefieldinit'.
	static sprᶔ()
	{
		int a_ = 0;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprᶔ.ᜃ = new string[]
		{
			ClipboardData.b("ὥᅧ፩ᕫ䍭㵯㽱女ትᱷ⹹㑻㙽멿벅ﮇ黎ꂋ좍횏", a_),
			ClipboardData.b("ὥᅧ፩ᕫ䍭㵯㽱女ትᱷ⹹㑻㙽멿벅ﮇ黎횋", a_),
			ClipboardData.b("ὥᅧ፩ᕫ䍭㵯㽱女ትᱷ⹹㑻㙽멿벅ﮇ黎", a_)
		};
	}

	// Token: 0x04002AFE RID: 11006
	private DigitalSignature ᜀ;

	// Token: 0x04002AFF RID: 11007
	private spr\u20C4 ᜁ;

	// Token: 0x04002B00 RID: 11008
	private readonly sprά ᜂ;

	// Token: 0x04002B01 RID: 11009
	private static readonly string[] ᜃ;
}
