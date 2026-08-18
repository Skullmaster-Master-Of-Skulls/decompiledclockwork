using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003B8 RID: 952
internal class spr\u2389
{
	// Token: 0x06003A52 RID: 14930 RVA: 0x0020CED4 File Offset: 0x0020BED4
	protected spr\u20C3 ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x0020CF18 File Offset: 0x0020BF18
	public virtual Stream ᜀ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				spr\u1FDC spr_u1FDC;
				MemoryStream memoryStream;
				switch (num)
				{
				case 1:
					goto IL_5B;
				case 2:
					try
					{
						for (;;)
						{
							int num2;
							int num3;
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
								byte[] array = new byte[8];
								spr_u1FDC.Read(array, 0, 8);
								num2 = BitConverter.ToInt32(array, 0);
								num3 = num2 % this.ᜀ;
								num = 2;
								break;
							}
							}
							for (;;)
							{
								int num4;
								switch (num)
								{
								case 0:
									goto IL_13E;
								case 1:
									num = 4;
									continue;
								case 2:
									if (num3 <= 0)
									{
										num = 1;
										continue;
									}
									num = 3;
									continue;
								case 3:
									num4 = num2 + this.ᜀ - num3;
									goto IL_FA;
								case 4:
									num4 = num2;
									goto IL_FA;
								}
								break;
								IL_FA:
								int num5 = num4;
								byte[] array2 = new byte[num5];
								spr_u1FDC.Read(array2, 0, num5);
								byte[] buffer = spr\u2389.ᜀ(array2, this.ᜄ);
								memoryStream.Write(buffer, 0, num2);
								memoryStream.Position = 0L;
								num = 0;
							}
						}
						IL_13E:
						return memoryStream;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_17E;
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
						IL_17E:;
					}
					goto IL_181;
				}
				if (true)
				{
				}
				if (this.ᜄ == null)
				{
					num = 1;
					continue;
				}
				IL_181:
				memoryStream = new MemoryStream();
				spr_u1FDC = this.ᜃ.ᜁ(RecordTableEnumerator.b("Ń⡅⭇㡉㕋㹍⑏㝑こٕ㥗㥙㝛㽝ݟݡ", a_));
				num = 2;
			}
			IL_5B:
			throw new InvalidOperationException(RecordTableEnumerator.b("ൃ⡅⭇╉㹋㱍㕏ㅑ⁓癕⡗㭙⽛ⵝ᝟ൡᙣɥ䙧", a_));
		}
		}
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x0020D0FC File Offset: 0x0020C0FC
	public void ᜄ(spr\u20C3 A_0)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			spr\u20C3 spr_u20C;
			switch (num)
			{
			case 1:
				goto IL_10D;
			case 2:
				goto IL_34;
			case 3:
				try
				{
					this.ᜁ(spr_u20C);
					this.ᜂ(spr_u20C);
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
							goto IL_10A;
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
								spr_u20C.Dispose();
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						IL_CB:
						if (spr_u20C != null)
						{
							num = 1;
							continue;
						}
						break;
						goto IL_CB;
					}
					IL_10A:;
				}
				goto Block_3;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			this.ᜃ = A_0;
			Stream stream = A_0.ᜁ(RecordTableEnumerator.b("縺匼尾㍀㩂㕄㍆⁈⑊⍌َ㽐㕒㩔", a_));
			num = 1;
			continue;
			Block_3:
			try
			{
				IL_10D:
				stream.Position = 0L;
				this.ᜂ = new spr\u2256(stream);
				goto IL_36;
			}
			finally
			{
				num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)stream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_15D;
					}
					if (stream == null)
					{
						break;
					}
					num = 0;
				}
				IL_15D:;
			}
			return;
			IL_36:
			spr_u20C = A_0.ᜅ(RecordTableEnumerator.b("㴺礼帾㕀≂ᙄ㝆⡈⡊⡌㱎", a_));
			num = 3;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼倾㍀≂≄≆", a_));
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x0020D288 File Offset: 0x0020C288
	public static bool ᜃ(spr\u20C3 A_0)
	{
		int a_ = 2;
		if (A_0.ᜃ(RecordTableEnumerator.b("紷吹弻䰽㤿㉁ぃ⽅❇⑉Ջ⁍㙏㵑", a_)))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_41;
				}
			}
			IL_41:
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0.ᜇ(RecordTableEnumerator.b("㸷縹崻䨽ℿᅁ㑃❅⭇⽉㽋", a_));
		}
		return false;
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x0020D2FC File Offset: 0x0020C2FC
	public virtual bool ᜀ(string A_0)
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
		spr\u241D a_ = this.ᜂ.ᜁ();
		this.ᜄ = this.ᜀ(A_0, a_);
		return this.ᜄ != null;
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x0020D360 File Offset: 0x0020C360
	private byte[] ᜀ(string A_0, spr\u241D A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				goto IL_47;
			}
			byte[] array;
			byte[] array2;
			byte[] array3;
			for (;;)
			{
				IL_34:
				switch (num)
				{
				case 0:
					return array;
				case 1:
					if (!BiffRecordRaw.CompareArrays(array2, 0, array3, 0, array2.Length))
					{
						num = 2;
						continue;
					}
					return array;
				case 2:
					goto IL_A5;
				}
				goto IL_47;
			}
			IL_A5:
			array = null;
			goto IL_A9;
			IL_47:
			byte[] a_ = A_1.ᜁ();
			array = sprṯ.ᜀ(A_0, a_, 16);
			byte[] buffer = spr\u2389.ᜀ(A_1.ᜀ(), array);
			array3 = spr\u2389.ᜀ(A_1.ᜂ(), array);
			SHA1 sha = new SHA1CryptoServiceProvider();
			array2 = sha.ComputeHash(buffer);
			num = 1;
			goto IL_34;
		}
		}
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x0020D42C File Offset: 0x0020C42C
	private static byte[] ᜀ(byte[] A_0, byte[] A_1)
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
		spr\u1C4C a_ = new spr\u1C4C(spr\u1C4C.KeySize.Bits128, A_1);
		return sprṯ.ᜀ(A_0, new sprṯ.ᜀ(a_.ᜀ), A_1.Length);
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x0020D484 File Offset: 0x0020C484
	private void ᜂ(spr\u20C3 A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
			for (;;)
			{
				List<spr\u2340> list = this.ᜁ.ᜀ();
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					spr\u20C3 spr_u20C3;
					switch (num)
					{
					case 0:
						goto IL_5F;
					case 1:
					{
						if (list.Count != 1)
						{
							num = 0;
							continue;
						}
						spr\u2340 spr_u = list[0];
						string a_2 = spr_u.ᜁ();
						string a_3 = null;
						spr\u20C3 spr_u20C = A_0.ᜅ(RecordTableEnumerator.b("Ʌ⥇㹉ⵋᵍ⁏㍑㝓㍕ᅗ㑙㩛ㅝ", a_));
						num = 2;
						continue;
					}
					case 2:
						goto IL_181;
					case 3:
						try
						{
							string a_3;
							spr\u20C3 spr_u20C2 = spr_u20C3.ᜅ(a_3);
							try
							{
								this.ᜀ(spr_u20C2);
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_139;
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
											spr_u20C2.Dispose();
											num = 0;
											continue;
										}
										break;
									}
									IL_FE:
									if (spr_u20C2 != null)
									{
										num = 1;
										continue;
									}
									break;
									goto IL_FE;
								}
								IL_139:;
							}
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
									goto IL_17E;
								case 1:
									spr_u20C3.Dispose();
									num = 0;
									continue;
								}
								if (spr_u20C3 == null)
								{
									break;
								}
								num = 1;
							}
							IL_17E:;
						}
						goto Block_3;
					}
					break;
					Block_3:
					try
					{
						IL_181:
						string a_2;
						spr\u20C3 spr_u20C;
						Stream stream = spr_u20C.ᜁ(a_2);
						try
						{
							for (;;)
							{
								spr\u234F spr_u234F = new spr\u234F(stream);
								List<string> list2 = spr_u234F.ᜀ();
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1EA;
									case 1:
										goto IL_1D0;
									case 2:
									{
										if (list2.Count != 1)
										{
											num = 1;
											continue;
										}
										string a_3 = list2[0];
										num = 0;
										continue;
									}
									}
									break;
								}
							}
							IL_1D0:
							throw new InvalidDataException();
							IL_1EA:;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_229;
								case 2:
									((IDisposable)stream).Dispose();
									num = 0;
									continue;
								}
								if (stream == null)
								{
									break;
								}
								num = 2;
							}
							IL_229:;
						}
						goto IL_61;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							spr\u20C3 spr_u20C;
							switch (num)
							{
							case 0:
								goto IL_26E;
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
						IL_26E:;
					}
					return;
					IL_61:
					spr_u20C3 = A_0.ᜅ(RecordTableEnumerator.b("ቅ㩇⭉≋㵍㙏㵑♓㭕ᅗ㑙㩛ㅝ", a_));
					num = 3;
				}
			}
			IL_5F:
			throw new InvalidDataException();
		}
	}

	// Token: 0x06003A5A RID: 14938 RVA: 0x0020D738 File Offset: 0x0020C738
	private void ᜁ(spr\u20C3 A_0)
	{
		int a_ = 10;
		int num = 1;
		for (;;)
		{
			spr\u1FDC spr_u1FDC;
			switch (num)
			{
			case 0:
				try
				{
					this.ᜁ = new spr\u1AB4(spr_u1FDC);
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
							((IDisposable)spr_u1FDC).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_7D;
						}
						if (spr_u1FDC == null)
						{
							break;
						}
						num = 0;
					}
					IL_7D:;
				}
				goto IL_80;
			case 1:
				IL_11:
				break;
			case 2:
				goto IL_30;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_80:
			spr_u1FDC = A_0.ᜁ(RecordTableEnumerator.b("п⍁ぃ❅ᭇ㩉ⵋⵍ㕏ὑ㕓♕", a_));
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 0;
				break;
			}
		}
		IL_30:
		throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅ᭇ㩉ⵋⵍ㕏⅑", a_));
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x0020D83C File Offset: 0x0020C83C
	private void ᜀ(spr\u20C3 A_0)
	{
		int a_ = 16;
		if (true)
		{
		}
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
			Stream stream = A_0.ᜁ(RecordTableEnumerator.b("䁅ᡇ㡉╋⍍ㅏ⁑ⵓ", a_));
			try
			{
				new spr\u1932(stream);
				new sprộ(stream);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_95;
					case 2:
						((IDisposable)stream).Dispose();
						num = 0;
						continue;
					}
					if (stream == null)
					{
						break;
					}
					num = 2;
				}
				IL_95:;
			}
			break;
		}
		}
	}

	// Token: 0x04001964 RID: 6500
	private int ᜀ = 16;

	// Token: 0x04001965 RID: 6501
	private spr\u1AB4 ᜁ;

	// Token: 0x04001966 RID: 6502
	protected spr\u2256 ᜂ;

	// Token: 0x04001967 RID: 6503
	private spr\u20C3 ᜃ;

	// Token: 0x04001968 RID: 6504
	protected byte[] ᜄ;
}
