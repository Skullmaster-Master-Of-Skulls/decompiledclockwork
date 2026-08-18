using System;
using System.IO;
using System.Security.Cryptography;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200024C RID: 588
internal class spr\u237F
{
	// Token: 0x06002387 RID: 9095 RVA: 0x0014C2F0 File Offset: 0x0014B2F0
	public virtual void ᜀ(Stream A_0, string A_1, spr\u20C3 A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_15C;
				case 1:
					if (A_1 != null)
					{
						num = 0;
						continue;
					}
					goto IL_8B;
				case 2:
					goto IL_51;
				case 3:
				{
					if (A_1.Length == 0)
					{
						num = 4;
						continue;
					}
					byte[] a_2 = this.ᜁ(A_2, A_1);
					this.ᜀ(A_2);
					spr\u1FDC spr_u1FDC = A_2.ᜀ(RecordTableEnumerator.b("ͅ♇⥉㹋㝍⁏♑ㅓ㉕ࡗ㭙㽛㕝şաţ", a_));
					num = 6;
					continue;
				}
				case 4:
					goto IL_17F;
				case 6:
					try
					{
						long length = A_0.Length;
						byte[] bytes = BitConverter.GetBytes(length);
						spr\u1FDC spr_u1FDC;
						spr_u1FDC.Write(bytes, 0, 8);
						byte[] a_2;
						this.ᜀ(A_0, a_2, spr_u1FDC);
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							spr\u1FDC spr_u1FDC;
							switch (num)
							{
							case 0:
								goto IL_159;
							case 1:
								((IDisposable)spr_u1FDC).Dispose();
								num = 0;
								continue;
							}
							IL_120:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_120;
							default:
								if (false)
								{
								}
								if (spr_u1FDC == null)
								{
									goto IL_15B;
								}
								num = 1;
								break;
							}
						}
						IL_159:
						IL_15B:;
					}
					goto IL_15C;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
				IL_15C:
				num = 3;
			}
			IL_51:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ", a_));
			IL_8B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙅⥇㥉㽋㥍㽏⁑こ", a_));
			IL_17F:
			goto IL_8B;
		}
		}
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x0014C494 File Offset: 0x0014B494
	private void ᜀ(spr\u20C3 A_0)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			spr\u20C3 spr_u20C;
			switch (num)
			{
			case 1:
				goto IL_3B;
			case 2:
				try
				{
					this.ᜄ(spr_u20C);
					this.ᜂ(spr_u20C);
					this.ᜃ(spr_u20C);
					this.ᜁ(spr_u20C);
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
							spr_u20C.Dispose();
							num = 0;
							continue;
						}
						IL_7D:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7D;
						default:
							if (false)
							{
							}
							if (spr_u20C == null)
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
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_B7:
			spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("ㄶ紸娺䤼帾ቀ㍂⑄⑆ⱈ㡊", a_));
			num = 2;
		}
		IL_3B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶嘸吺䤼", a_));
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x0014C5AC File Offset: 0x0014B5AC
	protected void ᜃ(spr\u20C3 A_0)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			spr\u1FDC spr_u1FDC;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				try
				{
					spr\u252F spr_u252F = new spr\u252F();
					spr_u252F.ᜀ(spr_u1FDC);
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
							((IDisposable)spr_u1FDC).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_9A;
						}
						IL_63:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						default:
							if (false)
							{
							}
							if (spr_u1FDC == null)
							{
								goto IL_9C;
							}
							num = 1;
							break;
						}
					}
					IL_9A:
					IL_9C:;
				}
				goto IL_9D;
			case 1:
				goto IL_30;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_9D:
			spr_u1FDC = A_0.ᜀ(RecordTableEnumerator.b("ᘿ❁㙃㕅ⅇ╉≋", a_));
			num = 0;
		}
		IL_30:
		throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅ᭇ㩉ⵋⵍ㕏⅑", a_));
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x0014C6B4 File Offset: 0x0014B6B4
	protected void ᜂ(spr\u20C3 A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			spr\u20C3 spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("漺似帾⽀あ⍄⡆㭈♊ьⅎ㝐㱒", a_));
			goto IL_2E;
			try
			{
				for (;;)
				{
					IL_2E:
					spr\u20C3 spr_u20C2 = spr_u20C.ᜄ(RecordTableEnumerator.b("栺䤼䴾⹀ⵂ≄Ɇ❈⡊㽌㙎⅐❒㱔㡖㝘ཚ⽜㹞འၢͤࡦ᭨٪", a_));
					try
					{
						spr\u1FDC spr_u1FDC = spr_u20C2.ᜀ(RecordTableEnumerator.b("㴺洼䴾⡀⹂⑄㕆え", a_));
						try
						{
							spr\u1932 spr_u = new spr\u1932();
							spr_u.ᜀ(1);
							spr_u.ᜁ(RecordTableEnumerator.b("䀺笼社础ɂ癄ņ祈硊恌穎材ᙒፔ穖浘浚汜汞䱠ⅢⅤ⍦屨䙪塬⹮䕰䉲㙴䙶㵸䭺䩼䵾떀떂", a_));
							spr_u.ᜀ(RecordTableEnumerator.b("瘺吼尾㍀ⱂ㙄⡆⽈㽊捌౎㹐㵒⅔㙖じ㕚㡜ⵞ你♢୤Ѧ᭨ቪᵬ᭮ᡰᱲ᭴⍶୸᩺፼౾", a_));
							spr_u.ᜂ(1);
							spr_u.ᜁ(1);
							spr_u.ᜃ(1);
							spr_u.ᜀ(spr_u1FDC);
							sprộ sprộ = new sprộ();
							sprộ.ᜀ(RecordTableEnumerator.b("稺砼氾灀煂組", a_));
							sprộ.ᜀ(spr_u1FDC);
						}
						finally
						{
							int num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_10E;
								case 1:
									((IDisposable)spr_u1FDC).Dispose();
									num = 0;
									continue;
								}
								if (spr_u1FDC == null)
								{
									break;
								}
								num = 1;
							}
							IL_10E:;
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
								goto IL_14E;
							case 2:
								spr_u20C2.Dispose();
								num = 1;
								continue;
							}
							if (spr_u20C2 == null)
							{
								break;
							}
							num = 2;
						}
						IL_14E:;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_167;
					}
				}
				IL_167:
				if (false)
				{
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
						goto IL_1AA;
					case 2:
						spr_u20C.Dispose();
						num = 0;
						continue;
					}
					if (spr_u20C == null)
					{
						break;
					}
					num = 2;
				}
				IL_1AA:;
			}
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x0014C8CC File Offset: 0x0014B8CC
	protected void ᜄ(spr\u20C3 A_0)
	{
		int a_ = 7;
		spr\u20C3 spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("礼帾㕀≂ᙄ㝆⡈⡊⡌َ㽐㕒㩔", a_));
		try
		{
			spr\u1FDC spr_u1FDC = spr_u20C.ᜀ(RecordTableEnumerator.b("渼䬾㍀ⱂ⭄⁆ై╊⹌㵎⡐⍒⅔㹖㙘㕚ᥜ㹞ᕠɢ㙤ᝦࡨࡪ࡬", a_));
			try
			{
				spr\u234F spr_u234F = new spr\u234F();
				spr_u234F.ᜀ().Add(RecordTableEnumerator.b("渼䬾㍀ⱂ⭄⁆ై╊⹌㵎⡐⍒⅔㹖㙘㕚ड़ⵞ`ൢᙤŦ٨ᥪl", a_));
				spr_u234F.ᜀ(spr_u1FDC);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_96;
					case 2:
						((IDisposable)spr_u1FDC).Dispose();
						num = 0;
						continue;
					}
					if (spr_u1FDC == null)
					{
						break;
					}
					num = 2;
				}
				IL_96:;
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
					spr_u20C.Dispose();
					num = 2;
					continue;
				case 2:
					goto IL_F6;
				}
				IL_B7:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B7;
				default:
					if (false)
					{
					}
					if (spr_u20C == null)
					{
						goto IL_F8;
					}
					num = 1;
					break;
				}
			}
			IL_F6:
			IL_F8:;
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x0014C9F8 File Offset: 0x0014B9F8
	protected void ᜁ(spr\u20C3 A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				spr\u1AB4 spr_u1AB;
				spr\u1FDC spr_u1FDC;
				switch (num)
				{
				case 0:
					goto IL_49;
				case 2:
					try
					{
						spr_u1AB.ᜀ(spr_u1FDC);
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
								goto IL_B1;
							case 2:
								((IDisposable)spr_u1FDC).Dispose();
								num = 1;
								continue;
							}
							IL_78:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_78;
							default:
								if (false)
								{
								}
								if (spr_u1FDC == null)
								{
									goto IL_B3;
								}
								num = 2;
								break;
							}
						}
						IL_B1:
						IL_B3:;
					}
					goto IL_B4;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				IL_B4:
				spr_u1AB = new spr\u1AB4();
				spr\u2340 spr_u = new spr\u2340();
				spr\u22B9 item = new spr\u22B9(0, RecordTableEnumerator.b("݁⩃╅㩇㍉㱋㩍㕏㙑ѓ㝕㭗ㅙ㵛㥝՟", a_));
				spr_u1AB.ᜀ().Add(spr_u);
				spr_u.ᜀ().Add(item);
				spr_u.ᜀ(RecordTableEnumerator.b("ᅁぃ㑅❇⑉⭋୍㹏ㅑ♓⽕⡗⹙㕛ㅝ๟♡գብ१㥩ᱫ཭፯᝱", a_));
				spr_u1FDC = A_0.ᜀ(RecordTableEnumerator.b("ف╃㉅⥇᥉㱋⽍㍏㝑ᥓ㝕⡗", a_));
				num = 2;
			}
			IL_49:
			throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇᥉㱋⽍㍏㝑❓", a_));
		}
		}
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x0014CB5C File Offset: 0x0014BB5C
	protected virtual byte[] ᜁ(spr\u20C3 A_0, string A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			byte[] array = this.ᜀ(16);
			byte[] array2 = sprṯ.ᜀ(A_1, array, 16);
			byte[] array3 = this.ᜀ(16);
			SHA1 sha = new SHA1CryptoServiceProvider();
			spr\u1FDC spr_u1FDC = A_0.ᜀ(RecordTableEnumerator.b("ై╊⹌㵎⡐⍒⅔㹖㙘㕚ᑜㅞݠౢ", a_));
			try
			{
				for (;;)
				{
					spr\u2256 spr_u = new spr\u2256();
					spr_u.ᜁ(131075);
					spr_u.ᜀ(36);
					spr\u21E7 spr_u21E = spr_u.ᜀ();
					spr_u21E.ᜄ(36);
					spr_u21E.ᜅ(26126);
					spr_u21E.ᜁ(32772);
					spr_u21E.ᜆ(128);
					spr_u21E.ᜃ(24);
					spr_u21E.ᜀ(0);
					spr_u21E.ᜂ(0);
					spr_u21E.ᜀ(RecordTableEnumerator.b("ш≊⹌㵎㹐⁒㩔ㅖⵘ筚ᡜㅞॠɢ୤Ѧ౨ཪ䵬㵮≰㉲啴ᙶ᝸ὺ嵼㹾쒀킂ꖄ쒆ﮈﶌﮎﺐﲞ膠힤좦\udfa8슪즬쪮쎰鎲鶴쮸풺즼킾뗀뫂뗄ꋆ", a_));
					spr\u241D spr_u241D = spr_u.ᜁ();
					spr_u241D.ᜁ(array);
					spr_u241D.ᜂ(this.ᜀ(array3, array2));
					byte[] array4 = sha.ComputeHash(array3);
					int num = array4.Length % 16;
					spr_u241D.ᜀ(array4.Length);
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							IL_13D:
							if (num != 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_170;
						case 1:
							array4 = sprṯ.ᜀ(array4, new byte[16 - num]);
							num2 = 2;
							continue;
						case 2:
							goto IL_170;
						case 3:
							goto IL_1B1;
						}
						break;
						IL_170:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13D;
						default:
							if (false)
							{
							}
							spr_u241D.ᜀ(this.ᜀ(array4, array2));
							spr_u.ᜀ(spr_u1FDC);
							num2 = 3;
							break;
						}
					}
				}
				IL_1B1:;
			}
			finally
			{
				int num2 = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_1F8;
					case 1:
						((IDisposable)spr_u1FDC).Dispose();
						num2 = 0;
						continue;
					}
					if (spr_u1FDC == null)
					{
						break;
					}
					num2 = 1;
				}
				IL_1F8:;
			}
			return array2;
		}
		}
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x0014CD84 File Offset: 0x0014BD84
	protected byte[] ᜀ(int A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					byte[] array;
					return array;
				}
				case 1:
					goto IL_CC;
				case 2:
				{
					int num2;
					if (num2 >= A_0)
					{
						num = 0;
						continue;
					}
					byte[] array;
					Random random;
					int maxValue;
					array[num2] = (byte)random.Next(maxValue);
					num2++;
					num = 1;
					continue;
				}
				case 4:
					goto IL_CC;
				case 5:
					goto IL_6A;
				}
				if (A_0 > 0)
				{
					byte[] array = new byte[A_0];
					Random random = new Random((int)DateTime.Now.Ticks);
					int maxValue = 256;
					int num2 = 0;
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_CC:
				if (true)
				{
				}
				num = 2;
			}
			IL_6A:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倻嬽⸿╁ぃ⹅", a_));
		}
		}
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x0014CE94 File Offset: 0x0014BE94
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
		spr\u1C4C a_ = new spr\u1C4C(spr\u1C4C.KeySize.Bits128, A_1);
		return sprṯ.ᜀ(A_0, new sprṯ.ᜀ(a_.ᜁ), A_1.Length);
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x0014CEEC File Offset: 0x0014BEEC
	private void ᜀ(Stream A_0, byte[] A_1, Stream A_2)
	{
		for (;;)
		{
			spr\u1C4C spr_u1C4C = new spr\u1C4C(spr\u1C4C.KeySize.Bits128, A_1);
			byte[] array = new byte[16];
			byte[] array2 = new byte[16];
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Read(array, 0, 16) <= 0)
					{
						num = 2;
						continue;
					}
					spr_u1C4C.ᜁ(array, array2);
					A_2.Write(array2, 0, 16);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 1:
					goto IL_42;
				case 2:
					return;
				case 3:
					goto IL_4C;
				}
				break;
				IL_4C:
				num = 0;
				continue;
				IL_42:
				if (true)
				{
				}
				goto IL_4C;
			}
		}
	}

	// Token: 0x04001232 RID: 4658
	internal const int ᜀ = 16;

	// Token: 0x04001233 RID: 4659
	private const int ᜁ = 131075;

	// Token: 0x04001234 RID: 4660
	private const int ᜂ = 36;

	// Token: 0x04001235 RID: 4661
	private const int ᜃ = 26126;

	// Token: 0x04001236 RID: 4662
	private const int ᜄ = 32772;

	// Token: 0x04001237 RID: 4663
	private const int ᜅ = 24;

	// Token: 0x04001238 RID: 4664
	private const string ᜆ = "Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)";
}
