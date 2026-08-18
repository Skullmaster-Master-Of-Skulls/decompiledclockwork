using System;
using System.IO;
using System.Security.Cryptography;
using Spire.CompoundFile.Doc;

// Token: 0x020001D1 RID: 465
[CLSCompliant(false)]
internal class spr\u22E2
{
	// Token: 0x0600140F RID: 5135 RVA: 0x0014C2A0 File Offset: 0x0014B2A0
	internal void ᜀ(Stream A_0, string A_1, spr\u2547 A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 != null)
					{
						num = 3;
						continue;
					}
					goto IL_8B;
				case 1:
				{
					if (A_1.Length == 0)
					{
						num = 6;
						continue;
					}
					byte[] a_2 = this.ᜀ(A_2, A_1);
					this.ᜄ(A_2);
					spr\u2578 spr_u = A_2.ᜀ(ClipboardData.b("㵷ᑹύ౽勵\uda89ﮏ", a_));
					num = 4;
					continue;
				}
				case 3:
					goto IL_152;
				case 4:
					try
					{
						long length = A_0.Length;
						byte[] bytes = BitConverter.GetBytes(length);
						spr\u2578 spr_u;
						spr_u.Write(bytes, 0, 8);
						byte[] a_2;
						this.ᜀ(A_0, a_2, spr_u);
						return;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_14F;
							case 2:
								goto IL_13E;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
							{
								IL_13E:
								spr\u2578 spr_u;
								((IDisposable)spr_u).Dispose();
								num = 0;
								break;
							}
							default:
							{
								if (false)
								{
								}
								spr\u2578 spr_u;
								if (spr_u == null)
								{
									goto IL_151;
								}
								num = 2;
								break;
							}
							}
						}
						IL_14F:
						IL_151:;
					}
					goto IL_152;
				case 5:
					goto IL_51;
				case 6:
					goto IL_17F;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
				IL_152:
				num = 1;
			}
			IL_51:
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ᱷ᭹ࡻώ", a_));
			IL_8B:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ࡷ᭹ཻൽ", a_));
			IL_17F:
			goto IL_8B;
		}
		}
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0014C444 File Offset: 0x0014B444
	private void ᜄ(spr\u2547 A_0)
	{
		int a_ = 0;
		int num = 0;
		for (;;)
		{
			spr\u2547 spr_u;
			switch (num)
			{
			case 1:
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
							goto IL_A4;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_A4:
							spr_u.Dispose();
							num = 0;
							break;
						default:
							if (false)
							{
							}
							if (spr_u == null)
							{
								goto IL_B6;
							}
							num = 1;
							break;
						}
					}
					IL_B4:
					IL_B6:;
				}
				goto IL_B7;
			case 2:
				goto IL_3B;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_B7:
			spr_u = A_0.ᜄ(ClipboardData.b("恥Ⱨ୩ᡫ཭⍯ɱᕳᕵᵷॹ", a_));
			num = 1;
		}
		IL_3B:
		throw new ArgumentNullException(ClipboardData.b("ᑥݧթᡫ", a_));
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0014C55C File Offset: 0x0014B55C
	private void ᜃ(spr\u2547 A_0)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			spr\u2578 spr_u;
			switch (num)
			{
			case 0:
				goto IL_30;
			case 1:
				if (true)
				{
				}
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
							goto IL_8A;
						case 1:
							goto IL_9A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_8A:
							((IDisposable)spr_u).Dispose();
							num = 1;
							break;
						default:
							if (false)
							{
							}
							if (spr_u == null)
							{
								goto IL_9C;
							}
							num = 0;
							break;
						}
					}
					IL_9A:
					IL_9C:;
				}
				goto IL_9D;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_9D:
			spr_u = A_0.ᜀ(ClipboardData.b("ⅶᱸॺ๼ᙾ", a_));
			num = 1;
		}
		IL_30:
		throw new ArgumentNullException(ClipboardData.b("፶ᡸེᱼⱾ愈", a_));
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x0014C664 File Offset: 0x0014B664
	private void ᜂ(spr\u2547 A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2547 spr_u = A_0.ᜄ(ClipboardData.b("❲ݴᙶ᝸ࡺ᭼ၾ첄", a_));
				try
				{
					spr\u2547 spr_u2 = spr_u.ᜄ(ClipboardData.b("⁲Ŵնᙸᕺ᩼㩾ﺆ麗ﾊﾐ잒ﮜ펠캢", a_));
					try
					{
						spr\u2578 spr_u3 = spr_u2.ᜀ(ClipboardData.b("畲╴նၸᙺᱼൾ", a_));
						try
						{
							sprᯗ sprᯗ = new sprᯗ();
							sprᯗ.ᜀ(1);
							sprᯗ.ᜁ(ClipboardData.b("ࡲ㍴ㅶ䁸㩺乼㥾놀낂ꢄ늆뾈캊쮌ꊎꖐꖒ꒔꒖뒘\ud99a\ud99c\udb9e钠躢邤鶨骪麮莲芴薶趸趺삼", a_));
							sprᯗ.ᜀ(ClipboardData.b("㹲ᱴᑶ୸ᑺ๼ၾꮄ쒆歷ﶒ래\ude9aﲞ펠\udaa2햤펦삨쒪쎬ﮮ쎰튲\udbb4쒶\udfb8풺쾼튾", a_));
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
							int num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									((IDisposable)spr_u3).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_105;
								}
								if (spr_u3 == null)
								{
									break;
								}
								num = 0;
							}
							IL_105:;
						}
					}
					finally
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_145;
							case 2:
								spr_u2.Dispose();
								num = 1;
								continue;
							}
							if (spr_u2 == null)
							{
								break;
							}
							num = 2;
						}
						IL_145:;
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
							goto IL_185;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 0;
					}
					IL_185:;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x0014C874 File Offset: 0x0014B874
	private void ᜁ(spr\u2547 A_0)
	{
		int a_ = 9;
		spr\u2547 spr_u = A_0.ᜄ(ClipboardData.b("⭮ၰݲᑴ⑶ॸ᩺Ṽ᩾좀", a_));
		goto IL_20;
		try
		{
			for (;;)
			{
				IL_20:
				spr\u2578 spr_u2 = spr_u.ᜀ(ClipboardData.b("㱮հŲᩴ᥶Ṹ㹺፼᱾廒쮎쒖漢ﺜ爵", a_));
				try
				{
					spr\u1C3C spr_u1C3C = new spr\u1C3C();
					spr_u1C3C.ᜀ().Add(ClipboardData.b("㱮հŲᩴ᥶Ṹ㹺፼᱾廒\udb8eﮔﾘ", a_));
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
							((IDisposable)spr_u2).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_96;
						}
						if (spr_u2 == null)
						{
							break;
						}
						num = 0;
					}
					IL_96:;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_AF;
				}
			}
			IL_AF:
			if (false)
			{
			}
		}
		finally
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					spr_u.Dispose();
					num = 2;
					continue;
				case 2:
					goto IL_F6;
				}
				if (true)
				{
				}
				if (spr_u == null)
				{
					break;
				}
				num = 1;
			}
			IL_F6:;
		}
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0014C9A0 File Offset: 0x0014B9A0
	private void ᜀ(spr\u2547 A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				sprᢳ sprᢳ;
				spr\u2578 spr_u;
				switch (num)
				{
				case 0:
					goto IL_53;
				case 1:
					try
					{
						sprᢳ.ᜀ(spr_u);
						return;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_AA;
							case 2:
								goto IL_BB;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_AA:
								((IDisposable)spr_u).Dispose();
								num = 2;
								break;
							default:
								if (false)
								{
								}
								if (spr_u == null)
								{
									goto IL_BD;
								}
								num = 0;
								break;
							}
						}
						IL_BB:
						IL_BD:;
					}
					goto IL_BE;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				IL_BE:
				sprᢳ = new sprᢳ();
				spr\u226A spr_u226A = new spr\u226A();
				spr\u2105 item = new spr\u2105(0, ClipboardData.b("㍵ᙷ᥹๻ݽ\ud887", a_));
				sprᢳ.ᜀ().Add(spr_u226A);
				spr_u226A.ᜀ().Add(item);
				spr_u226A.ᜀ(ClipboardData.b("╵౷ࡹ፻ၽ잁慎ﲋ揄憐﶑望튕聯ﶛ춝킟쎡잣쎥", a_));
				spr_u = A_0.ᜀ(ClipboardData.b("㉵᥷๹ᵻ⵽얇ﲋ", a_));
				num = 1;
			}
			IL_53:
			throw new ArgumentNullException(ClipboardData.b("ት᥷๹ᵻ⵽ﮇ", a_));
		}
		}
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0014CB04 File Offset: 0x0014BB04
	private byte[] ᜀ(spr\u2547 A_0, string A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			byte[] array = this.ᜀ(16);
			byte[] array2 = this.ᜇ.ᜀ(A_1, array, 16);
			byte[] array3 = this.ᜀ(16);
			SHA1 sha = new SHA1Managed();
			spr\u2578 spr_u = A_0.ᜀ(ClipboardData.b("⥫m፯q൳ٵ౷፹፻ၽ쥿", a_));
			try
			{
				for (;;)
				{
					spr\u2505 spr_u2 = new spr\u2505();
					spr_u2.ᜁ(131075);
					spr_u2.ᜀ(36);
					spr\u2258 spr_u3 = spr_u2.ᜃ();
					spr_u3.ᜄ(36);
					spr_u3.ᜅ(26126);
					spr_u3.ᜁ(32772);
					spr_u3.ᜆ(128);
					spr_u3.ᜃ(24);
					spr_u3.ᜀ(0);
					spr_u3.ᜂ(0);
					spr_u3.ᜀ(ClipboardData.b("Ⅻݭ፯q᭳յ᝷ᱹࡻ幽앿낏삑잓힕뢗ﮙ瞧肟袧\udeab힭삯욱\udbb3통쪷\udbb9첻횽ꦿꇁ雅뫇ꗉ뫋ꟍ듏럑ꛓ諙껛뇝铟跡郣鿥飧迩엫", a_));
					sprẰ sprẰ = spr_u2.ᜁ();
					sprẰ.ᜁ(array);
					sprẰ.ᜂ(this.ᜀ(array3, array2));
					byte[] array4 = sha.ComputeHash(array3);
					int num = array4.Length % 16;
					sprẰ.ᜀ(array4.Length);
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							array4 = this.ᜇ.ᜁ(array4, new byte[16 - num]);
							num2 = 2;
							continue;
						case 1:
							goto IL_197;
						case 2:
							goto IL_172;
						case 3:
							if (num != 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_172;
						}
						break;
						IL_172:
						sprẰ.ᜀ(this.ᜀ(array4, array2));
						spr_u2.ᜀ(spr_u);
						num2 = 1;
					}
				}
				IL_197:;
			}
			finally
			{
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_1E8;
					case 1:
						goto IL_1FA;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_1E8:
						((IDisposable)spr_u).Dispose();
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						if (spr_u == null)
						{
							goto IL_1FC;
						}
						num2 = 0;
						break;
					}
				}
				IL_1FA:
				IL_1FC:;
			}
			return array2;
		}
		}
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0014CD38 File Offset: 0x0014BD38
	private byte[] ᜀ(int A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num2;
				byte[] array;
				Random random;
				int maxValue;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B8;
					default:
						if (false)
						{
						}
						goto IL_CC;
					}
					break;
				case 1:
					if (num2 >= A_0)
					{
						num = 4;
						continue;
					}
					array[num2] = (byte)random.Next(maxValue);
					num2++;
					num = 0;
					continue;
				case 2:
					goto IL_4E;
				case 3:
					goto IL_CC;
				case 4:
					return array;
				}
				if (A_0 <= 0)
				{
					num = 2;
					continue;
				}
				array = new byte[A_0];
				random = new Random((int)DateTime.Now.Ticks);
				maxValue = 256;
				num2 = 0;
				num = 3;
				continue;
				IL_CC:
				if (true)
				{
				}
				num = 1;
			}
			IL_4E:
			IL_B8:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ṱᅳᡵί๹ᑻ", a_));
		}
		}
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x0014CE48 File Offset: 0x0014BE48
	private byte[] ᜀ(byte[] A_0, byte[] A_1)
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
		spr\u21ED a_ = new spr\u21ED(spr\u21ED.KeySize.Bits128, A_1);
		return this.ᜇ.ᜀ(A_0, new spr\u1AED.ᜀ(a_.ᜁ), A_1.Length);
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x0014CEA8 File Offset: 0x0014BEA8
	private void ᜀ(Stream A_0, byte[] A_1, Stream A_2)
	{
		for (;;)
		{
			spr\u21ED spr_u21ED = new spr\u21ED(spr\u21ED.KeySize.Bits128, A_1);
			byte[] array = new byte[16];
			byte[] array2 = new byte[16];
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_42;
				case 1:
					goto IL_42;
				case 2:
					if (A_0.Read(array, 0, 16) <= 0)
					{
						num = 3;
						continue;
					}
					goto IL_87;
				case 3:
					return;
				}
				break;
				IL_42:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_87:
					spr_u21ED.ᜁ(array, array2);
					A_2.Write(array2, 0, 16);
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
	}

	// Token: 0x040018F5 RID: 6389
	private const int ᜀ = 16;

	// Token: 0x040018F6 RID: 6390
	private const int ᜁ = 131075;

	// Token: 0x040018F7 RID: 6391
	private const int ᜂ = 36;

	// Token: 0x040018F8 RID: 6392
	private const int ᜃ = 26126;

	// Token: 0x040018F9 RID: 6393
	private const int ᜄ = 32772;

	// Token: 0x040018FA RID: 6394
	private const int ᜅ = 24;

	// Token: 0x040018FB RID: 6395
	private const string ᜆ = "Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)";

	// Token: 0x040018FC RID: 6396
	private spr\u1AED ᜇ = new spr\u1AED();
}
