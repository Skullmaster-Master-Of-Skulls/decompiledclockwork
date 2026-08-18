using System;
using System.IO;
using System.Security.Cryptography;
using Spire.CompoundFile.Doc;

// Token: 0x020003F5 RID: 1013
[CLSCompliant(false)]
internal class spr\u254C
{
	// Token: 0x060038BA RID: 14522 RVA: 0x0035029C File Offset: 0x0034F29C
	internal void ᜀ(Stream A_0, string A_1, spr\u2547 A_2)
	{
		int a_ = 16;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7A;
			case 1:
				goto IL_3C;
			case 3:
				num = 4;
				continue;
			case 4:
				if (A_1.Length != 0)
				{
					goto IL_B9;
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
					num = 0;
					continue;
				}
				break;
			case 5:
				if (A_1 != null)
				{
					goto IL_87;
				}
				goto IL_A5;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_87:
			num = 3;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ት᥷๹ᵻ", a_));
		IL_7A:
		IL_A5:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ٵ᥷ॹཻॽ", a_));
		IL_B9:
		this.ᜀ(A_0, A_2, A_1);
		this.ᜄ(A_2);
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x0035037C File Offset: 0x0034F37C
	private void ᜀ(Stream A_0, spr\u2547 A_1, string A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			SHA1 sha = new SHA1Managed();
			spr\u2578 spr_u = A_1.ᜀ(ClipboardData.b("㍵ᙷ᥹๻ݽ쎉ﾏ", a_));
			try
			{
				spr\u2084 spr_u2 = new spr\u2084();
				spr_u2.ᜁ(262148);
				spr_u2.ᜀ(64);
				sprṅ sprṅ = spr_u2.ᜁ().ᜁ();
				this.ᜀ(sprṅ);
				sprṅ.ᜀ(this.ᜀ(sprṅ.ᜅ()));
				sprḞ sprḞ = spr_u2.ᜁ().ᜂ().ᜀ();
				this.ᜀ(sprḞ);
				sprḞ.ᜂ(this.ᜀ(sprḞ.ᜉ()));
				byte[] array = this.ᜀ(sprḞ.ᜉ());
				byte[] a_2 = new byte[]
				{
					254,
					167,
					210,
					118,
					59,
					75,
					158,
					121
				};
				byte[] a_3 = this.ᜃ.ᜀ(A_2, sprḞ.ᜄ(), a_2, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
				sprḞ.ᜁ(this.ᜀ(array, sprḞ.ᜂ(), a_3, sprḞ.ᜄ()));
				byte[] a_4 = new byte[]
				{
					215,
					170,
					15,
					109,
					48,
					97,
					52,
					78
				};
				a_3 = this.ᜃ.ᜀ(A_2, sprḞ.ᜄ(), a_4, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
				sprḞ.ᜀ(this.ᜀ(sha.ComputeHash(array), sprḞ.ᜂ(), a_3, sprḞ.ᜄ()));
				byte[] array2 = this.ᜀ(sprṅ.ᜇ() / 8);
				byte[] a_5 = new byte[]
				{
					20,
					110,
					11,
					231,
					171,
					172,
					208,
					214
				};
				a_3 = this.ᜃ.ᜀ(A_2, sprḞ.ᜄ(), a_5, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
				sprḞ.ᜃ(this.ᜀ(array2, sprḞ.ᜂ(), a_3, sprḞ.ᜄ()));
				sprὅ sprὅ = spr_u2.ᜁ().ᜀ();
				byte[] a_6 = new byte[]
				{
					95,
					178,
					173,
					1,
					12,
					185,
					225,
					246
				};
				byte[] array3 = sha.ComputeHash(this.ᜃ.ᜁ(sprṅ.ᜂ(), a_6));
				array3 = this.ᜃ.ᜀ(array3, sprṅ.ᜁ(), 0);
				byte[] a_7 = this.ᜀ(sprṅ.ᜀ());
				sprὅ.ᜀ(this.ᜀ(a_7, sprṅ.ᜁ(), array2, array3));
				byte[] buffer = this.ᜀ(A_0, A_1, sprṅ, array2);
				HMACSHA1 hmacsha = new HMACSHA1();
				hmacsha.Key = this.ᜃ.ᜀ(a_7, sprṅ.ᜀ(), 0);
				byte[] a_8 = new byte[]
				{
					160,
					103,
					127,
					2,
					178,
					44,
					132,
					51
				};
				byte[] array4 = sha.ComputeHash(this.ᜃ.ᜁ(sprṅ.ᜂ(), a_8));
				array4 = this.ᜃ.ᜀ(array4, sprṅ.ᜁ(), 0);
				sprὅ.ᜁ(this.ᜀ(hmacsha.ComputeHash(buffer), sprṅ.ᜁ(), array2, array4));
				spr_u2.ᜀ(spr_u);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						for (;;)
						{
							((IDisposable)spr_u).Dispose();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_346;
							}
						}
						IL_346:
						if (false)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_355;
					}
					if (spr_u == null)
					{
						break;
					}
					num = 1;
				}
				IL_355:;
			}
			return;
		}
		}
	}

	// Token: 0x060038BC RID: 14524 RVA: 0x00350700 File Offset: 0x0034F700
	private byte[] ᜀ(Stream A_0, spr\u2547 A_1, sprṅ A_2, byte[] A_3)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			byte[] array = BitConverter.GetBytes(A_0.Length);
			SHA1 sha = new SHA1Managed();
			spr\u2578 spr_u = A_1.ᜀ(ClipboardData.b("⩮ὰၲݴ๶ॸེ᡼᭾톀", a_));
			try
			{
				for (;;)
				{
					int num = (int)(A_0.Length / 4096L);
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_DF:
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_1B0;
						case 1:
							spr_u.Write(array, 0, array.Length);
							num2 = 0;
							continue;
						case 2:
							goto IL_CB;
						case 3:
							if (A_0.Length % 4096L != 0L)
							{
								num2 = 4;
								continue;
							}
							goto IL_F0;
						case 4:
							num++;
							num2 = 5;
							continue;
						case 5:
							goto IL_F0;
						case 6:
						{
							if (num3 >= num)
							{
								goto IL_DF;
							}
							int num4 = Math.Min(4096, (int)(A_0.Length - (long)(num3 * 4096)));
							byte[] array2 = new byte[num4];
							byte[] a_2 = new byte[num4];
							A_0.Read(array2, 0, num4);
							byte[] a_3 = sha.ComputeHash(this.ᜃ.ᜁ(A_2.ᜂ(), BitConverter.GetBytes(num3)));
							a_2 = this.ᜀ(array2, A_2.ᜁ(), A_3, a_3);
							array = this.ᜃ.ᜁ(array, a_2);
							num3++;
							num2 = 7;
							continue;
						}
						case 7:
							goto IL_CB;
						}
						break;
						IL_CB:
						num2 = 6;
						continue;
						IL_F0:
						num3 = 0;
						num2 = 2;
					}
				}
				IL_1B0:;
			}
			finally
			{
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						((IDisposable)spr_u).Dispose();
						num2 = 1;
						continue;
					case 1:
						goto IL_1ED;
					}
					if (spr_u == null)
					{
						break;
					}
					num2 = 0;
				}
				IL_1ED:;
			}
			if (true)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x060038BD RID: 14525 RVA: 0x0035092C File Offset: 0x0034F92C
	private void ᜀ(sprṅ A_0)
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0.ᜂ(16);
		A_0.ᜃ(16);
		A_0.ᜀ(128);
		A_0.ᜁ(20);
		A_0.ᜁ(ClipboardData.b("㙶㱸⡺", a_));
		A_0.ᜀ(ClipboardData.b("㑶ᅸ᩺ᑼᅾ쪆첎펐킒", a_));
		A_0.ᜂ(ClipboardData.b("⑶ㅸ㩺䱼", a_));
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x003509D0 File Offset: 0x0034F9D0
	private void ᜀ(sprḞ A_0)
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
		A_0.ᜀ(100000);
		A_0.ᜃ(16);
		A_0.ᜄ(16);
		A_0.ᜁ(128);
		A_0.ᜂ(20);
		A_0.ᜁ(ClipboardData.b("⭩⥫㵭", a_));
		A_0.ᜀ(ClipboardData.b("⥩ѫ཭᥯ᱱᵳᡵί㝹፻᩽솁욃얅", a_));
		A_0.ᜂ(ClipboardData.b("㥩⑫⽭䅯", a_));
	}

	// Token: 0x060038BF RID: 14527 RVA: 0x00350A80 File Offset: 0x0034FA80
	private void ᜄ(spr\u2547 A_0)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			spr\u2547 spr_u;
			switch (num)
			{
			case 0:
				goto IL_3B;
			case 2:
				try
				{
					this.ᜁ(spr_u);
					this.ᜂ(spr_u);
					this.ᜃ(spr_u);
					this.ᜀ(spr_u);
					return;
				}
				finally
				{
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_B4;
						case 1:
							for (;;)
							{
								spr_u.Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_A6;
								}
							}
							IL_A6:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 1;
					}
					IL_B4:;
				}
				goto IL_B7;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_B7:
			spr_u = A_0.ᜄ(ClipboardData.b("繷㹹ᵻ੽톁ﾋ", a_));
			num = 2;
		}
		IL_3B:
		throw new ArgumentNullException(ClipboardData.b("੷ᕹ፻੽", a_));
	}

	// Token: 0x060038C0 RID: 14528 RVA: 0x00350B98 File Offset: 0x0034FB98
	private void ᜃ(spr\u2547 A_0)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			spr\u2578 spr_u;
			switch (num)
			{
			case 0:
				goto IL_38;
			case 2:
				try
				{
					sprᣳ sprᣳ = new sprᣳ();
					sprᣳ.ᜀ(spr_u);
					return;
				}
				finally
				{
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							for (;;)
							{
								((IDisposable)spr_u).Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_94;
								}
							}
							IL_94:
							if (false)
							{
							}
							num = 1;
							continue;
						case 1:
							goto IL_A2;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 0;
					}
					IL_A2:;
				}
				goto IL_A5;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_A5:
			spr_u = A_0.ᜀ(ClipboardData.b("⍴ቶ୸ࡺᑼၾ", a_));
			num = 2;
		}
		IL_38:
		throw new ArgumentNullException(ClipboardData.b("ᅴᙶ൸᩺⹼ཾ", a_));
	}

	// Token: 0x060038C1 RID: 14529 RVA: 0x00350CA0 File Offset: 0x0034FCA0
	private void ᜂ(spr\u2547 A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			spr\u2547 spr_u = A_0.ᜄ(ClipboardData.b("㍦᭨੪ͬᱮᝰᱲݴ᩶へᕺ᭼ၾ", a_));
			try
			{
				spr\u2547 spr_u2 = spr_u.ᜄ(ClipboardData.b("㑦ᵨᥪɬŮᙰ㙲᭴ᑶ୸ɺർ୾펆ﮈﲎﲒ殺", a_));
				try
				{
					spr\u2578 spr_u3 = spr_u2.ᜀ(ClipboardData.b("慦㥨ᥪѬɮၰŲ౴", a_));
					try
					{
						sprᯗ sprᯗ = new sprᯗ();
						sprᯗ.ᜀ(1);
						sprᯗ.ᜁ(ClipboardData.b("ᱦ⽨⵪呬⹮䉰㕲䕴䑶呸乺䭼㩾잀꺂놄놆뢈뢊ꂌ춎햐힒ꂔ몖겘\uda9aꦜ꺞銢鞦麨馪馬馮첰", a_));
						sprᯗ.ᜀ(ClipboardData.b("⩦hࡪὬnɰᱲ፴Ͷ坸㡺ቼᅾ力ꎌ쪎ﾐ쾠힤욦잨\ud8aa쮬삮쎰\udeb2", a_));
						sprᯗ.ᜂ(1);
						sprᯗ.ᜁ(1);
						sprᯗ.ᜃ(1);
						sprᯗ.ᜀ(spr_u3);
						spr\u1D52 spr_u1D = new spr\u1D52();
						spr_u1D.ᜀ(string.Empty);
						spr_u1D.ᜀ(spr_u3);
					}
					finally
					{
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_10F;
							case 1:
								((IDisposable)spr_u3).Dispose();
								num = 0;
								continue;
							}
							if (spr_u3 == null)
							{
								break;
							}
							num = 1;
						}
						IL_10F:;
					}
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							spr_u2.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_14F;
						}
						if (spr_u2 == null)
						{
							break;
						}
						num = 0;
					}
					IL_14F:;
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
						for (;;)
						{
							spr_u.Dispose();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_1A4;
							}
						}
						IL_1A4:
						if (false)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_1B3;
					}
					if (true)
					{
					}
					if (spr_u == null)
					{
						break;
					}
					num = 0;
				}
				IL_1B3:;
			}
			return;
		}
		}
	}

	// Token: 0x060038C2 RID: 14530 RVA: 0x00350EB0 File Offset: 0x0034FEB0
	private void ᜁ(spr\u2547 A_0)
	{
		int a_ = 16;
		spr\u2547 spr_u = A_0.ᜄ(ClipboardData.b("㉵᥷๹ᵻ⵽솇", a_));
		try
		{
			spr\u2578 spr_u2 = spr_u.ᜀ(ClipboardData.b("╵౷ࡹ፻ၽ잁慎ﲋ揄憐﶑望튕聯ﶛ춝킟쎡잣쎥", a_));
			try
			{
				spr\u1C3C spr_u1C3C = new spr\u1C3C();
				spr_u1C3C.ᜀ().Add(ClipboardData.b("╵౷ࡹ፻ၽ잁慎ﲋ揄憐﶑望슕ﮙ욟춡횣쮥", a_));
				spr_u1C3C.ᜀ(spr_u2);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B2;
					case 2:
						for (;;)
						{
							((IDisposable)spr_u2).Dispose();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_A4;
							}
						}
						IL_A4:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					if (spr_u2 == null)
					{
						break;
					}
					num = 2;
				}
				IL_B2:;
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
					spr_u.Dispose();
					num = 2;
					continue;
				case 2:
					goto IL_EE;
				}
				if (spr_u == null)
				{
					break;
				}
				num = 0;
			}
			IL_EE:;
		}
		if (true)
		{
		}
	}

	// Token: 0x060038C3 RID: 14531 RVA: 0x00350FDC File Offset: 0x0034FFDC
	private void ᜀ(spr\u2547 A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				sprᢳ sprᢳ;
				spr\u2578 spr_u;
				switch (num)
				{
				case 1:
					goto IL_53;
				case 2:
					try
					{
						sprᢳ.ᜀ(spr_u);
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								for (;;)
								{
									((IDisposable)spr_u).Dispose();
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										goto IL_AC;
									}
								}
								IL_AC:
								if (false)
								{
								}
								num = 2;
								continue;
							case 2:
								goto IL_BB;
							}
							if (spr_u == null)
							{
								break;
							}
							num = 1;
						}
						IL_BB:;
					}
					goto IL_BE;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_BE:
				sprᢳ = new sprᢳ();
				spr\u226A spr_u226A = new spr\u226A();
				spr\u2105 item = new spr\u2105(0, ClipboardData.b("㵷ᑹύ౽勵\uda89ﮏ", a_));
				sprᢳ.ᜀ().Add(spr_u226A);
				spr_u226A.ᜀ().Add(item);
				spr_u226A.ᜀ(ClipboardData.b("⭷๹๻ᅽ솃ﺍﮑﮓ\udc97ﮙﾝ튡얣얥춧", a_));
				spr_u = A_0.ᜀ(ClipboardData.b("㱷᭹ࡻώ퍿잉ﺍ", a_));
				num = 2;
			}
			IL_53:
			throw new ArgumentNullException(ClipboardData.b("ᱷ᭹ࡻώ퍿黎", a_));
		}
		}
	}

	// Token: 0x060038C4 RID: 14532 RVA: 0x00351140 File Offset: 0x00350140
	private byte[] ᜀ(int A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				byte[] array;
				Random random;
				int maxValue;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						goto IL_C4;
					}
					break;
				case 1:
					if (num2 >= A_0)
					{
						num = 2;
						continue;
					}
					array[num2] = (byte)random.Next(maxValue);
					num2++;
					num = 5;
					continue;
				case 2:
					return array;
				case 3:
					goto IL_DE;
				case 5:
					goto IL_7A;
				}
				if (A_0 <= 0)
				{
					num = 0;
					continue;
				}
				array = new byte[A_0];
				random = new Random((int)DateTime.Now.Ticks);
				maxValue = 256;
				num2 = 0;
				num = 3;
				continue;
				IL_DE:
				num = 1;
				continue;
				IL_7A:
				goto IL_DE;
			}
			IL_C4:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(ClipboardData.b("ṱᅳᡵί๹ᑻ", a_));
		}
		}
	}

	// Token: 0x060038C5 RID: 14533 RVA: 0x00351250 File Offset: 0x00350250
	private byte[] ᜀ(byte[] A_0, int A_1, byte[] A_2, byte[] A_3)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				int num = A_0.Length;
				int num2 = 6;
				for (;;)
				{
					int num3;
					byte[] array2;
					byte[] array3;
					switch (num2)
					{
					case 0:
						goto IL_182;
					case 1:
						goto IL_129;
					case 2:
						goto IL_182;
					case 3:
						return array;
					case 4:
					{
						if (num3 >= num)
						{
							goto IL_F3;
						}
						byte[] src;
						Buffer.BlockCopy(src, num3, array2, 0, A_1);
						num2 = 5;
						continue;
					}
					case 5:
						if (num3 == 0)
						{
							num2 = 7;
							continue;
						}
						array2 = this.ᜃ.ᜂ(array2, array3);
						num2 = 9;
						continue;
					case 6:
					{
						if (num % A_1 != 0)
						{
							num2 = 10;
							continue;
						}
						byte[] src = A_0;
						num2 = 2;
						continue;
					}
					case 7:
						array2 = this.ᜃ.ᜂ(array2, A_3);
						num2 = 1;
						continue;
					case 8:
						goto IL_E2;
					case 9:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F3;
						default:
							if (false)
							{
							}
							goto IL_129;
						}
						break;
					case 10:
					{
						num = (num / A_1 + 1) * A_1;
						byte[] src = this.ᜃ.ᜀ(A_0, num, 0);
						num2 = 0;
						continue;
					}
					case 11:
						goto IL_E2;
					}
					break;
					IL_E2:
					num2 = 4;
					continue;
					IL_F3:
					num2 = 3;
					continue;
					IL_129:
					spr\u21ED spr_u21ED;
					spr_u21ED.ᜁ(array2, array3);
					Buffer.BlockCopy(array3, 0, array, num3, A_1);
					num3 += A_1;
					num2 = 8;
					continue;
					IL_182:
					array = new byte[num];
					array2 = new byte[A_1];
					array3 = new byte[A_1];
					spr_u21ED = new spr\u21ED(spr\u21ED.KeySize.Bits128, A_2);
					num3 = 0;
					num2 = 11;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x04002A68 RID: 10856
	private const int ᜀ = 262148;

	// Token: 0x04002A69 RID: 10857
	private const int ᜁ = 64;

	// Token: 0x04002A6A RID: 10858
	private const int ᜂ = 4096;

	// Token: 0x04002A6B RID: 10859
	private spr\u1AED ᜃ = new spr\u1AED();
}
