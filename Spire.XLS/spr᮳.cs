using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x020003B9 RID: 953
internal class spr᮳ : spr\u2389
{
	// Token: 0x06003A5D RID: 14941 RVA: 0x0020D910 File Offset: 0x0020C910
	public override Stream ᜀ()
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
				MemoryStream result;
				switch (num)
				{
				case 1:
					try
					{
						for (;;)
						{
							bool flag = this.ᜀ(spr_u1FDC, this.ᜄ, this.ᜂ.ᜅ().ᜀ(), this.ᜂ.ᜂ().ᜁ(), this.ᜂ.ᜂ().ᜀ());
							num = 8;
							for (;;)
							{
								byte[] array;
								int num6;
								byte[] array2;
								byte[] array3;
								uint num7;
								int num8;
								switch (num)
								{
								case 0:
								{
									int a_2;
									byte[] buffer = spr\u21CA.ᜀ(array, a_2, 0);
									result = new MemoryStream(buffer);
									num = 2;
									continue;
								}
								case 1:
								{
									int num2;
									if (num2 > 0)
									{
										num = 6;
										continue;
									}
									goto IL_285;
								}
								case 2:
									goto IL_2D0;
								case 3:
									goto IL_285;
								case 4:
									goto IL_195;
								case 5:
									goto IL_195;
								case 6:
								{
									int num3;
									num3++;
									int num2;
									int num5;
									int num4 = num5 + num6 - num2;
									array2 = new byte[num4];
									Buffer.BlockCopy(array3, 0, array2, 0, array3.Length);
									spr\u21CA.ᜀ(array2, array2.Length, 0);
									array3 = array2;
									num = 3;
									continue;
								}
								case 7:
									goto IL_EA;
								case 8:
								{
									if (!flag)
									{
										num = 7;
										continue;
									}
									spr_u1FDC.Position = 0L;
									byte[] array4 = new byte[8];
									spr_u1FDC.Read(array4, 0, 8);
									int a_2 = BitConverter.ToInt32(array4, 0);
									num6 = 4096;
									array3 = new byte[spr_u1FDC.Length - 8L];
									spr_u1FDC.Read(array3, 0, array3.Length);
									int num5 = array3.Length;
									int num3 = num5 / num6;
									int num2 = num5 % num6;
									num = 1;
									continue;
								}
								case 9:
								{
									int num3;
									if ((ulong)num7 >= (ulong)((long)num3))
									{
										num = 0;
										continue;
									}
									Buffer.BlockCopy(array3, num8, array2, 0, array2.Length);
									byte[] a_3 = spr\u21CA.ᜀ(this.ᜂ.ᜅ().ᜀ(), num7, this.ᜂ.ᜄ().HashAlgorithm);
									byte[] array5 = spr\u21CA.ᜀ(array2, this.ᜄ, a_3, 16, num6);
									Buffer.BlockCopy(array5, 0, array, num8, array5.Length);
									num8 += num6;
									num7 += 1U;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_195;
									default:
										if (false)
										{
										}
										num = 4;
										continue;
									}
									break;
								}
								}
								break;
								IL_195:
								num = 9;
								continue;
								IL_285:
								array = new byte[array3.Length];
								array2 = new byte[num6];
								num8 = 0;
								num7 = 0U;
								num = 5;
							}
						}
						IL_EA:
						throw new InvalidOperationException(RecordTableEnumerator.b("੃⥅㱇橉ⵋ湍♏㍑㡓㽕㱗穙㥛そ͟ၡᵣᙥᱧཀྵ࡫乭ᙯ᭱ᡳ፵噷", a_));
						IL_2D0:
						return result;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_310;
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
						IL_310:;
					}
					goto IL_313;
				case 2:
					goto IL_5B;
				}
				if (true)
				{
				}
				if (this.ᜄ == null)
				{
					num = 2;
					continue;
				}
				IL_313:
				result = new MemoryStream();
				this.ᜂ.ᜄ().BlockSize;
				spr_u1FDC = base.ᜁ().ᜁ(RecordTableEnumerator.b("Ń⡅⭇㡉㕋㹍⑏㝑こٕ㥗㥙㝛㽝ݟݡ", a_));
				num = 1;
			}
			IL_5B:
			throw new InvalidOperationException(RecordTableEnumerator.b("ൃ⡅⭇╉㹋㱍㕏ㅑ⁓癕⡗㭙⽛ⵝ᝟ൡᙣɥ䙧", a_));
		}
		}
	}

	// Token: 0x06003A5E RID: 14942 RVA: 0x0020DCA4 File Offset: 0x0020CCA4
	public override bool ᜀ(string A_0)
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
		EncryptedKeyInfo a_ = this.ᜂ.ᜄ();
		this.ᜄ = this.ᜀ(A_0, a_);
		return this.ᜄ != null;
	}

	// Token: 0x06003A5F RID: 14943 RVA: 0x0020DD08 File Offset: 0x0020CD08
	private byte[] ᜀ(string A_0, EncryptedKeyInfo A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				byte[] result;
				byte[] array;
				byte[] array2;
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
					byte[] bytes;
					byte[] a_2;
					switch (num)
					{
					case 0:
						result = this.ᜂ(A_1.KeyValue, bytes, a_2, A_1.SpinCount, A_1.KeyBits);
						num = 4;
						continue;
					case 1:
						goto IL_103;
					case 3:
						goto IL_65;
					case 4:
						return result;
					}
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					result = null;
					a_2 = A_1.SaltValue;
					bytes = Encoding.Unicode.GetBytes(A_0);
					byte[] a_3 = this.ᜀ(A_1.VerifierHashInput, bytes, a_2, A_1.SpinCount, A_1.KeyBits);
					array = this.ᜁ(A_1.VerifierHashValue, bytes, a_2, A_1.SpinCount, A_1.KeyBits);
					array2 = spr\u21CA.ᜀ(a_3, A_1.HashAlgorithm);
					num = 1;
					continue;
				}
				}
				IL_103:
				if (!BiffRecordRaw.CompareArrays(array2, 0, array, 0, array2.Length))
				{
					return result;
				}
				if (true)
				{
				}
				num = 0;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("ି❁㵃晅ṇ⽉㹋❍㙏㭑ㅓ⑕", a_));
		}
		}
	}

	// Token: 0x06003A60 RID: 14944 RVA: 0x0020DE58 File Offset: 0x0020CE58
	internal new byte[] ᜂ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
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
		byte[] a_ = new byte[]
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
		byte[] a_2 = spr\u21CA.ᜀ(A_1, A_2, a_, A_3, A_4, this.ᜂ.ᜄ().HashAlgorithm);
		return spr\u21CA.ᜀ(A_0, a_2, A_2, A_4, A_4);
	}

	// Token: 0x06003A61 RID: 14945 RVA: 0x0020DED0 File Offset: 0x0020CED0
	private byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
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
		byte[] a_ = new byte[]
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
		byte[] a_2 = spr\u21CA.ᜀ(A_1, A_2, a_, A_3, A_4, this.ᜂ.ᜄ().HashAlgorithm);
		return spr\u21CA.ᜀ(A_0, a_2, A_2, A_4, A_2.Length);
	}

	// Token: 0x06003A62 RID: 14946 RVA: 0x0020DF4C File Offset: 0x0020CF4C
	internal byte[] ᜁ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
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
		byte[] a_ = new byte[]
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
		byte[] a_2 = spr\u21CA.ᜀ(A_1, A_2, a_, A_3, A_4, this.ᜂ.ᜄ().HashAlgorithm);
		return spr\u21CA.ᜀ(A_0, a_2, A_2, A_4, this.ᜂ.ᜄ().HashSize);
	}

	// Token: 0x06003A63 RID: 14947 RVA: 0x0020DFD4 File Offset: 0x0020CFD4
	internal byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3)
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
		byte[] a_ = new byte[]
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
		byte[] a_2 = spr\u21CA.ᜀ(A_2, a_, this.ᜂ.ᜄ().HashAlgorithm);
		byte[] a_3 = spr\u21CA.ᜀ(A_0, A_1, a_2, A_1.Length, A_1.Length);
		return spr\u21CA.ᜀ(a_3, A_3, 0);
	}

	// Token: 0x06003A64 RID: 14948 RVA: 0x0020E054 File Offset: 0x0020D054
	internal byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2)
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
		byte[] a_ = new byte[]
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
		byte[] a_2 = spr\u21CA.ᜀ(A_2, a_, this.ᜂ.ᜄ().HashAlgorithm);
		return spr\u21CA.ᜀ(A_0, A_1, a_2, A_1.Length, A_1.Length);
	}

	// Token: 0x06003A65 RID: 14949 RVA: 0x0020E0CC File Offset: 0x0020D0CC
	internal bool ᜀ(Stream A_0, byte[] A_1, byte[] A_2, byte[] A_3, byte[] A_4)
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
		byte[] key = this.ᜀ(A_3, A_1, A_2, this.ᜂ.ᜄ().HashSize);
		HMAC hmac = spr\u21CA.ᜁ(this.ᜂ.ᜄ().HashAlgorithm);
		hmac.Key = key;
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		byte[] array2 = hmac.ComputeHash(array);
		byte[] array3 = this.ᜀ(A_4, A_1, A_2);
		array3 = spr\u21CA.ᜀ(array3, array2.Length, 0);
		return BiffRecordRaw.CompareArrays(array3, array2);
	}
}
