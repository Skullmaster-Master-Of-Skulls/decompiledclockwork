using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004C6 RID: 1222
internal class sprᮆ : spr\u237F
{
	// Token: 0x06004B3F RID: 19263 RVA: 0x002DC908 File Offset: 0x002DB908
	public override void ᜀ(Stream A_0, string A_1, spr\u20C3 A_2)
	{
		int a_ = 17;
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
					goto IL_1FF;
				case 1:
				{
					if (A_1.Length == 0)
					{
						num = 4;
						continue;
					}
					spr\u2256 spr_u = this.ᜀ(A_2, A_1);
					this.ᜀ(A_2);
					spr\u1FDC spr_u1FDC = A_2.ᜀ(RecordTableEnumerator.b("Ɇ❈⡊㽌㙎⅐❒ご㍖क़㩚㹜㑞`Ѣd", a_));
					num = 5;
					continue;
				}
				case 3:
					num = 1;
					continue;
				case 4:
					goto IL_233;
				case 5:
				{
					try
					{
						long length = A_0.Length;
						byte[] bytes = BitConverter.GetBytes(length);
						spr\u1FDC spr_u1FDC;
						spr_u1FDC.Write(bytes, 0, 8);
						spr\u2256 spr_u;
						Stream stream = this.ᜀ(A_0, this.ᜃ, spr_u.ᜅ().ᜀ());
						byte[] array = new byte[stream.Length];
						stream.Read(array, 0, array.Length);
						spr_u1FDC.Write(array, 0, array.Length);
						stream.Close();
						spr_u1FDC.Position = 0L;
						this.ᜀ(spr_u1FDC, this.ᜃ, spr_u.ᜅ().ᜀ(), spr_u.ᜄ().HashSize, spr_u);
						goto IL_67;
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
								goto IL_162;
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
						IL_162:;
					}
					goto IL_165;
					IL_67:
					spr\u1FDC spr_u1FDC2 = A_2.ᜀ(RecordTableEnumerator.b("Ɇ❈⡊㽌㙎⅐❒㱔㡖㝘ቚ㍜㥞๠", a_));
					if (true)
					{
					}
					num = 7;
					continue;
				}
				case 6:
					goto IL_62;
				case 7:
					goto IL_8E;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				IL_165:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			IL_62:
			goto IL_1EB;
			IL_8E:
			try
			{
				spr\u2256 spr_u;
				spr\u1FDC spr_u1FDC2;
				spr_u.ᜀ(spr_u1FDC2);
				return;
			}
			finally
			{
				num = 0;
				for (;;)
				{
					spr\u1FDC spr_u1FDC2;
					switch (num)
					{
					case 1:
						goto IL_1E8;
					case 2:
						((IDisposable)spr_u1FDC2).Dispose();
						num = 1;
						continue;
					}
					if (spr_u1FDC2 == null)
					{
						break;
					}
					num = 2;
				}
				IL_1E8:;
			}
			IL_1EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
			IL_1FF:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝆⡈㡊㹌㡎㹐⅒ㅔ", a_));
			IL_233:
			goto IL_1FF;
		}
		}
	}

	// Token: 0x06004B40 RID: 19264 RVA: 0x002DCB9C File Offset: 0x002DBB9C
	internal new Stream ᜀ(Stream A_0, byte[] A_1, byte[] A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 3;
			byte[] array;
			for (;;)
			{
				int num2;
				byte[] array2;
				int num4;
				uint num5;
				switch (num)
				{
				case 0:
				{
					if (num2 == 0)
					{
						num = 6;
						continue;
					}
					array = new byte[array2.Length];
					byte[] array3 = new byte[4096];
					int num3 = 0;
					num = 4;
					continue;
				}
				case 1:
					if (num4 > 0)
					{
						num = 10;
						continue;
					}
					goto IL_21E;
				case 2:
					if (num4 > 0)
					{
						num = 14;
						continue;
					}
					goto IL_29C;
				case 4:
					goto IL_76;
				case 5:
					goto IL_219;
				case 6:
				{
					byte[] a_2 = spr\u21CA.ᜀ(A_2, num5, null);
					byte[] array4 = spr\u21CA.ᜀ(array2, A_1, a_2, 16);
					array = array4;
					num = 8;
					continue;
				}
				case 7:
					goto IL_21E;
				case 8:
					goto IL_297;
				case 9:
					goto IL_76;
				case 10:
					goto IL_114;
				case 11:
					goto IL_71;
				case 12:
					num4 = array2.Length % 4096;
					num = 2;
					continue;
				case 13:
				{
					if ((ulong)num5 >= (ulong)((long)num2))
					{
						num = 12;
						continue;
					}
					byte[] array3;
					int num3;
					Buffer.BlockCopy(array2, num3, array3, 0, array3.Length);
					byte[] a_3 = spr\u21CA.ᜀ(A_2, num5, null);
					byte[] array5 = spr\u21CA.ᜀ(array3, A_1, a_3, 16);
					Buffer.BlockCopy(array5, 0, array, num3, array5.Length);
					num3 += 4096;
					num5 += 1U;
					num = 9;
					continue;
				}
				case 14:
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
						byte[] array3 = new byte[num4];
						int num3;
						Buffer.BlockCopy(array2, num3, array3, 0, array3.Length);
						byte[] a_4 = spr\u21CA.ᜀ(A_2, num5, null);
						byte[] array6 = spr\u21CA.ᜀ(array3, A_1, a_4, 16);
						Buffer.BlockCopy(array6, 0, array, num3, array6.Length);
						num = 5;
						continue;
					}
					}
					break;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				array2 = new byte[A_0.Length];
				A_0.Read(array2, 0, array2.Length);
				int num6 = array2.Length;
				num4 = num6 % 16;
				num = 1;
				continue;
				IL_76:
				num = 13;
				continue;
				IL_114:
				int a_5 = num6 + (16 - num4);
				byte[] array7 = spr\u21CA.ᜀ(array2, a_5, 0);
				array2 = array7;
				num = 7;
				continue;
				IL_21E:
				num2 = array2.Length / 4096;
				num5 = 0U;
				num = 0;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("ጿ㙁㙃⍅⥇❉", a_));
			IL_219:
			IL_297:
			IL_29C:
			return new MemoryStream(array);
		}
		}
	}

	// Token: 0x06004B41 RID: 19265 RVA: 0x002DCE50 File Offset: 0x002DBE50
	protected new void ᜀ(spr\u20C3 A_0)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			spr\u20C3 spr_u20C;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				try
				{
					base.ᜄ(spr_u20C);
					base.ᜂ(spr_u20C);
					base.ᜃ(spr_u20C);
					base.ᜁ(spr_u20C);
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
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8A;
							default:
								goto IL_B0;
							}
							break;
						case 1:
							goto IL_8A;
						}
						if (spr_u20C != null)
						{
							num = 1;
							continue;
						}
						goto IL_B8;
						IL_8A:
						spr_u20C.Dispose();
						num = 0;
					}
					IL_B0:
					if (false)
					{
					}
					IL_B8:;
				}
				goto IL_B9;
			case 2:
				goto IL_3D;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_B9:
			spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("㰹砻弽㐿⍁ᝃ㙅⥇⥉⥋㵍", a_));
			num = 0;
		}
		IL_3D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹医儽㐿", a_));
	}

	// Token: 0x06004B42 RID: 19266 RVA: 0x002DCF68 File Offset: 0x002DBF68
	internal new byte[] ᜀ(byte[] A_0, byte[] A_1, int A_2)
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
		byte[] a_ = base.ᜀ(16);
		byte[] a_2 = new byte[]
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
		byte[] array = this.ᜀ(A_0, A_1, a_2, A_2, A_1.Length);
		this.ᜃ = a_;
		return spr\u21CA.ᜀ(a_, array, A_1, array.Length);
	}

	// Token: 0x06004B43 RID: 19267 RVA: 0x002DCFE0 File Offset: 0x002DBFE0
	internal new byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3)
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
		byte[] array = this.ᜀ(A_1, A_2, a_, A_3, 16);
		return spr\u21CA.ᜀ(A_0, array, A_2, array.Length);
	}

	// Token: 0x06004B44 RID: 19268 RVA: 0x002DD04C File Offset: 0x002DC04C
	internal byte[] ᜁ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3)
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
		byte[] a_ = spr\u21CA.ᜀ(A_0, null);
		a_ = spr\u21CA.ᜀ(a_, 32, 0);
		byte[] a_2 = new byte[]
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
		byte[] array = this.ᜀ(A_1, A_2, a_2, A_3, A_2.Length);
		return spr\u21CA.ᜀ(a_, array, A_2, array.Length);
	}

	// Token: 0x06004B45 RID: 19269 RVA: 0x002DD0CC File Offset: 0x002DC0CC
	internal new byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider;
			byte[] buffer;
			byte[] array2;
			for (;;)
			{
				sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
				buffer = sprṯ.ᜀ(A_1, A_0);
				byte[] array = sha1CryptoServiceProvider.ComputeHash(buffer);
				array2 = array;
				uint num = 0U;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5F;
					case 1:
					{
						if ((ulong)num >= (ulong)((long)A_3))
						{
							num2 = 3;
							continue;
						}
						byte[] bytes = BitConverter.GetBytes(num);
						buffer = sprṯ.ᜀ(bytes, array2);
						array2 = sha1CryptoServiceProvider.ComputeHash(buffer);
						num += 1U;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_5F;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_8F;
						}
						break;
					}
					break;
					IL_5F:
					num2 = 1;
				}
			}
			IL_8F:
			if (false)
			{
			}
			buffer = sprṯ.ᜀ(array2, A_2);
			byte[] a_ = sha1CryptoServiceProvider.ComputeHash(buffer);
			return spr\u21CA.ᜀ(a_, A_4, 54);
		}
		}
	}

	// Token: 0x06004B46 RID: 19270 RVA: 0x002DD1C0 File Offset: 0x002DC1C0
	protected new spr\u2256 ᜀ(spr\u20C3 A_0, string A_1)
	{
		int a_ = 2;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷唹医䨽", a_));
			}
		}
		spr\u2256 spr_u = new spr\u2256();
		spr_u.ᜁ(262148);
		spr_u.ᜀ(64);
		int num = spr_u.ᜄ().SpinCount;
		byte[] bytes = Encoding.Unicode.GetBytes(A_1);
		byte[] array = base.ᜀ(16);
		spr_u.ᜄ().KeyValue = this.ᜀ(bytes, array, num);
		byte[] a_2 = base.ᜀ(16);
		spr_u.ᜄ().VerifierHashInput = this.ᜀ(a_2, bytes, array, num);
		spr_u.ᜄ().VerifierHashValue = this.ᜁ(a_2, bytes, array, num);
		spr_u.ᜄ().SaltValue = array;
		byte[] array2;
		spr_u.ᜅ().ᜀ(array2 = base.ᜀ(16));
		return spr_u;
	}

	// Token: 0x06004B47 RID: 19271 RVA: 0x002DD2C8 File Offset: 0x002DC2C8
	internal new void ᜀ(Stream A_0, byte[] A_1, byte[] A_2, int A_3, spr\u2256 A_4)
	{
		int a_ = 13;
		while (A_0 == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("♂⭄⑆㭈㉊㵌㭎㑐㝒畔ݖ㡘㡚㙜㹞٠٢", a_));
			}
		}
		byte[] array = base.ᜀ(A_3);
		byte[] a_2 = new byte[]
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
		byte[] a_3 = spr\u21CA.ᜀ(A_2, a_2, null);
		array = spr\u21CA.ᜀ(array, 2 * A_1.Length, 0);
		byte[] a_4 = spr\u21CA.ᜀ(array, A_1, a_3, A_1.Length);
		A_4.ᜂ().ᜁ(a_4);
		HMACSHA1 hmacsha = new HMACSHA1();
		hmacsha.Key = array;
		byte[] array2 = new byte[A_0.Length];
		A_0.Read(array2, 0, array2.Length);
		byte[] a_5 = hmacsha.ComputeHash(array2);
		a_5 = spr\u21CA.ᜀ(a_5, 2 * A_1.Length, 0);
		a_2 = new byte[]
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
		a_3 = spr\u21CA.ᜀ(A_2, a_2, null);
		A_4.ᜂ().ᜀ(spr\u21CA.ᜀ(a_5, A_1, a_3, A_1.Length));
	}

	// Token: 0x04002204 RID: 8708
	internal new const int ᜀ = 4096;

	// Token: 0x04002205 RID: 8709
	internal new const int ᜁ = 262148;

	// Token: 0x04002206 RID: 8710
	internal new const int ᜂ = 64;

	// Token: 0x04002207 RID: 8711
	private new byte[] ᜃ;
}
