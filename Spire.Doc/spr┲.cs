using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Spire.CompoundFile.Doc;

// Token: 0x020002B3 RID: 691
[CLSCompliant(false)]
internal class spr\u2532
{
	// Token: 0x06002548 RID: 9544 RVA: 0x00256AEC File Offset: 0x00255AEC
	internal Stream ᜀ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				spr\u2578 spr_u;
				sprṅ sprṅ;
				MemoryStream memoryStream;
				SHA1 sha;
				switch (num)
				{
				case 1:
					try
					{
						for (;;)
						{
							byte[] array = new byte[8];
							spr_u.Read(array, 0, 8);
							int num2 = BitConverter.ToInt32(array, 0);
							int num3 = num2 % sprṅ.ᜁ();
							num = 2;
							for (;;)
							{
								int num5;
								int num4;
								byte[] array2;
								int num6;
								int num7;
								int num8;
								byte[] src;
								byte[] array4;
								int num9;
								int num10;
								byte[] a_3;
								switch (num)
								{
								case 0:
									num4 = num5 / 4096 + 1;
									goto IL_266;
								case 1:
									num = 7;
									continue;
								case 2:
									if (num3 <= 0)
									{
										num = 1;
										continue;
									}
									num = 9;
									continue;
								case 3:
									memoryStream.Write(array2, 0, num2);
									memoryStream.Position = 0L;
									num = 18;
									continue;
								case 4:
									if (num5 % 4096 != 0)
									{
										num = 10;
										continue;
									}
									num = 5;
									continue;
								case 5:
									num4 = num5 / 4096;
									goto IL_266;
								case 6:
								{
									if (num6 >= num7)
									{
										num = 3;
										continue;
									}
									num8 = Math.Min(4096, num5 - num6 * 4096);
									byte[] array3 = new byte[num8];
									src = new byte[num8];
									Buffer.BlockCopy(array4, num6 * 4096, array3, 0, num8);
									byte[] a_2 = sha.ComputeHash(this.ᜅ.ᜁ(sprṅ.ᜂ(), BitConverter.GetBytes(num6)));
									src = this.ᜀ(array3, sprṅ.ᜁ(), this.ᜄ, a_2, num8);
									num = 16;
									continue;
								}
								case 7:
									num9 = num2;
									goto IL_1FF;
								case 8:
									goto IL_249;
								case 9:
									num9 = num2 + sprṅ.ᜁ() - num3;
									goto IL_1FF;
								case 10:
									num = 0;
									continue;
								case 11:
									num10 = num8 - (num5 - num2);
									goto IL_12C;
								case 12:
									num10 = num8;
									goto IL_12C;
								case 13:
									if (!this.ᜀ(a_3))
									{
										num = 8;
										continue;
									}
									array2 = new byte[num2];
									num = 4;
									continue;
								case 14:
									num = 12;
									continue;
								case 15:
									goto IL_199;
								case 16:
									if (num6 != num7 - 1)
									{
										num = 14;
										continue;
									}
									num = 11;
									continue;
								case 17:
									goto IL_199;
								case 18:
									goto IL_33A;
								}
								break;
								IL_12C:
								num8 = num10;
								Buffer.BlockCopy(src, 0, array2, num6 * 4096, num8);
								num6++;
								num = 15;
								continue;
								IL_199:
								num = 6;
								continue;
								IL_1FF:
								num5 = num9;
								array4 = new byte[num5];
								spr_u.Read(array4, 0, num5);
								a_3 = this.ᜅ.ᜁ(array, array4);
								num = 13;
								continue;
								IL_266:
								num7 = num4;
								num6 = 0;
								num = 17;
							}
						}
						IL_249:
						throw new Exception(ClipboardData.b("㙲᭴ᑶ୸ɺർ୾ꖄ떔ﺖ뮚힠슢즤캦춨", a_));
						IL_33A:
						return memoryStream;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)spr_u).Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_360;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 1:
								goto IL_396;
							}
							goto IL_35D;
							IL_360:
							num = 0;
							continue;
							IL_35D:
							if (spr_u != null)
							{
								goto IL_360;
							}
							break;
						}
						IL_396:;
					}
					goto IL_399;
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
				IL_399:
				memoryStream = new MemoryStream();
				sprṅ = this.ᜂ.ᜁ().ᜁ();
				sha = new SHA1Managed();
				spr_u = this.ᜃ.ᜁ(ClipboardData.b("㙲᭴ᑶ୸ɺർ୾햄", a_));
				num = 1;
			}
			IL_5B:
			throw new InvalidOperationException(ClipboardData.b("㩲᭴ᑶᙸॺོ᩾ꖄﺌﺐ릖", a_));
		}
		}
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x00256F0C File Offset: 0x00255F0C
	internal void ᜃ(spr\u2547 A_0)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			spr\u2547 spr_u;
			switch (num)
			{
			case 0:
				try
				{
					this.ᜁ(spr_u);
					this.ᜂ(spr_u);
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
							goto IL_EB;
						case 1:
							spr_u.Dispose();
							num = 0;
							continue;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 1;
					}
					IL_EB:;
				}
				goto Block_3;
			case 1:
				goto IL_3E;
			case 3:
				goto IL_EE;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			this.ᜃ = A_0;
			Stream stream = A_0.ᜁ(ClipboardData.b("⡬ŮተŲ౴ݶ൸ቺቼᅾ좀", a_));
			num = 3;
			continue;
			Block_3:
			try
			{
				IL_EE:
				this.ᜂ = new spr\u2084(stream);
				goto IL_48;
			}
			finally
			{
				num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_152;
					case 1:
						((IDisposable)stream).Dispose();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11E;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					goto IL_11B;
					IL_11E:
					num = 1;
					continue;
					IL_11B:
					if (stream != null)
					{
						goto IL_11E;
					}
					break;
				}
				IL_152:;
			}
			return;
			IL_48:
			spr_u = A_0.ᜅ(ClipboardData.b("歬⭮ၰݲᑴ⑶ॸ᩺Ṽ᩾", a_));
			num = 0;
		}
		IL_3E:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("Ṭ᭮ṰŲᑴၶᱸ", a_));
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x0025708C File Offset: 0x0025608C
	internal bool ᜀ(string A_0)
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
		sprṅ sprṅ = this.ᜂ.ᜁ().ᜁ();
		sprḞ sprḞ = this.ᜂ.ᜁ().ᜂ().ᜀ();
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
		byte[] a_2 = this.ᜅ.ᜀ(A_0, sprḞ.ᜄ(), a_, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
		byte[] buffer = this.ᜀ(sprḞ.ᜅ(), sprḞ.ᜂ(), a_2, sprḞ.ᜄ(), sprḞ.ᜉ());
		byte[] a_3 = new byte[]
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
		byte[] a_4 = this.ᜅ.ᜀ(A_0, sprḞ.ᜄ(), a_3, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
		byte[] a_5 = this.ᜀ(sprḞ.ᜀ(), sprḞ.ᜂ(), a_4, sprḞ.ᜄ(), sprḞ.ᜁ());
		SHA1 sha = new SHA1Managed();
		byte[] a_6 = sha.ComputeHash(buffer);
		bool result = this.ᜅ.ᜀ(a_5, a_6);
		byte[] a_7 = new byte[]
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
		byte[] a_8 = this.ᜅ.ᜀ(A_0, sprḞ.ᜄ(), a_7, sprḞ.ᜆ() >> 3, sprḞ.ᜋ());
		this.ᜄ = this.ᜀ(sprḞ.ᜃ(), sprḞ.ᜂ(), a_8, sprḞ.ᜄ(), sprṅ.ᜇ() / 8);
		return result;
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x00257220 File Offset: 0x00256220
	private bool ᜀ(byte[] A_0)
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
		sprṅ sprṅ = this.ᜂ.ᜁ().ᜁ();
		sprὅ sprὅ = this.ᜂ.ᜁ().ᜀ();
		SHA1 sha = new SHA1Managed();
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
		byte[] array = sha.ComputeHash(this.ᜅ.ᜁ(sprṅ.ᜂ(), a_));
		array = this.ᜅ.ᜀ(array, sprṅ.ᜁ(), 0);
		byte[] a_2 = this.ᜀ(sprὅ.ᜁ(), sprṅ.ᜁ(), this.ᜄ, array, sprṅ.ᜀ());
		byte[] a_3 = new HMACSHA1
		{
			Key = this.ᜅ.ᜀ(a_2, sprṅ.ᜀ(), 0)
		}.ComputeHash(A_0);
		byte[] a_4 = new byte[]
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
		byte[] array2 = sha.ComputeHash(this.ᜅ.ᜁ(sprṅ.ᜂ(), a_4));
		array2 = this.ᜅ.ᜀ(array2, sprṅ.ᜁ(), 0);
		byte[] a_5 = this.ᜀ(sprὅ.ᜀ(), sprṅ.ᜁ(), this.ᜄ, array2, sprṅ.ᜀ());
		return this.ᜅ.ᜀ(a_3, a_5);
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x0025738C File Offset: 0x0025638C
	private byte[] ᜀ(byte[] A_0, int A_1, byte[] A_2, byte[] A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				int num = A_0.Length;
				array = new byte[num];
				byte[] array2 = new byte[A_1];
				byte[] array3 = new byte[A_1];
				byte[] array4 = new byte[A_1];
				spr\u21ED spr_u21ED = new spr\u21ED(spr\u21ED.KeySize.Bits128, A_2);
				int num2 = 0;
				int num3 = 11;
				for (;;)
				{
					byte[] src;
					switch (num3)
					{
					case 0:
						return array;
					case 1:
						if (array.Length > A_4)
						{
							goto IL_FA;
						}
						return array;
					case 2:
						num3 = 1;
						continue;
					case 3:
						if (num2 == 0)
						{
							num3 = 8;
							continue;
						}
						Buffer.BlockCopy(array2, 0, array4, 0, A_1);
						num3 = 13;
						continue;
					case 4:
						num = (num / A_1 + 1) * A_1;
						src = this.ᜅ.ᜀ(A_0, num, 0);
						num3 = 5;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FA;
						default:
							if (false)
							{
							}
							goto IL_99;
						}
						break;
					case 6:
						array2 = new byte[A_4];
						Buffer.BlockCopy(array, 0, array2, 0, A_4);
						array = array2;
						num3 = 0;
						continue;
					case 7:
						goto IL_99;
					case 8:
						Buffer.BlockCopy(A_3, 0, array4, 0, A_1);
						num3 = 9;
						continue;
					case 9:
						goto IL_14F;
					case 10:
						goto IL_99;
					case 11:
						if (num % A_1 != 0)
						{
							num3 = 4;
							continue;
						}
						src = A_0;
						num3 = 7;
						continue;
					case 12:
						if (num2 >= num)
						{
							num3 = 2;
							continue;
						}
						num3 = 3;
						continue;
					case 13:
						goto IL_14F;
					}
					break;
					IL_99:
					num3 = 12;
					continue;
					IL_FA:
					num3 = 6;
					continue;
					IL_14F:
					if (true)
					{
					}
					Buffer.BlockCopy(src, num2, array2, 0, A_1);
					spr_u21ED.ᜀ(array2, array3);
					array3 = this.ᜅ.ᜂ(array3, array4);
					Buffer.BlockCopy(array3, 0, array, num2, A_1);
					num2 += A_1;
					num3 = 10;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x002575A8 File Offset: 0x002565A8
	private void ᜂ(spr\u2547 A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				List<spr\u226A> list = this.ᜁ.ᜀ();
				int num = 2;
				for (;;)
				{
					spr\u2547 spr_u2;
					switch (num)
					{
					case 0:
						goto IL_173;
					case 1:
						try
						{
							string a_2;
							spr\u2547 spr_u = spr_u2.ᜅ(a_2);
							try
							{
								this.ᜀ(spr_u);
							}
							finally
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										spr_u.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_12B;
									}
									if (spr_u == null)
									{
										break;
									}
									num = 1;
								}
								IL_12B:;
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
									goto IL_170;
								case 1:
									spr_u2.Dispose();
									num = 0;
									continue;
								}
								if (spr_u2 == null)
								{
									break;
								}
								num = 1;
							}
							IL_170:;
						}
						goto Block_3;
					case 2:
					{
						if (true)
						{
						}
						if (list.Count != 1)
						{
							num = 3;
							continue;
						}
						spr\u226A spr_u226A = list[0];
						string a_3 = spr_u226A.ᜁ();
						string a_2 = null;
						spr\u2547 spr_u3 = A_0.ᜅ(ClipboardData.b("⍦ࡨὪ౬㱮Űቲᙴቶへᕺ᭼ၾ", a_));
						num = 0;
						continue;
					}
					case 3:
						goto IL_5F;
					}
					break;
					Block_3:
					try
					{
						IL_173:
						string a_3;
						spr\u2547 spr_u3;
						Stream stream = spr_u3.ᜁ(a_3);
						try
						{
							for (;;)
							{
								spr\u1C3C spr_u1C3C = new spr\u1C3C(stream);
								List<string> list2 = spr_u1C3C.ᜀ();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
								{
									IL_1F4:
									string a_2 = list2[0];
									num = 2;
									break;
								}
								default:
									if (false)
									{
									}
									num = 1;
									break;
								}
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1DE;
									case 1:
										if (list2.Count != 1)
										{
											num = 0;
											continue;
										}
										goto IL_1F4;
									case 2:
										goto IL_209;
									}
									break;
								}
							}
							IL_1DE:
							throw new Exception(ClipboardData.b("⹦ݨᵪ౬ͮᡰᝲ啴፶ᡸེᱼ", a_));
							IL_209:;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_248;
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
							IL_248:;
						}
						goto IL_6B;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							spr\u2547 spr_u3;
							switch (num)
							{
							case 0:
								goto IL_28D;
							case 2:
								spr_u3.Dispose();
								num = 0;
								continue;
							}
							if (spr_u3 == null)
							{
								break;
							}
							num = 2;
						}
						IL_28D:;
					}
					return;
					IL_6B:
					spr_u2 = A_0.ᜅ(ClipboardData.b("㍦᭨੪ͬᱮᝰᱲݴ᩶へᕺ᭼ၾ", a_));
					num = 1;
				}
			}
			IL_5F:
			throw new Exception(ClipboardData.b("⹦ݨᵪ౬ͮᡰᝲ啴፶ᡸེᱼ", a_));
		}
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x0025787C File Offset: 0x0025687C
	private void ᜁ(spr\u2547 A_0)
	{
		int a_ = 10;
		int num = 1;
		for (;;)
		{
			spr\u2578 spr_u;
			switch (num)
			{
			case 0:
				try
				{
					this.ᜁ = new sprᢳ(spr_u);
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
							goto IL_AB;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								((IDisposable)spr_u).Dispose();
								num = 0;
								continue;
							}
							break;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 2;
					}
					IL_AB:;
				}
				goto IL_AE;
			case 2:
				goto IL_42;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_AE:
			spr_u = A_0.ᜁ(ClipboardData.b("㑯፱s᝵⭷੹ᵻᵽ쾁", a_));
			num = 0;
		}
		IL_42:
		throw new ArgumentNullException(ClipboardData.b("ᑯ፱s᝵⭷੹ᵻᵽ", a_));
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x00257980 File Offset: 0x00256980
	private void ᜀ(spr\u2547 A_0)
	{
		int a_ = 0;
		Stream stream = A_0.ᜁ(ClipboardData.b("恥㡧ᡩիͭᅯq൳", a_));
		try
		{
			new sprᯗ(stream);
			new spr\u1D52(stream);
		}
		finally
		{
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
						break;
					default:
						if (false)
						{
						}
						((IDisposable)stream).Dispose();
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_8B;
				}
				if (true)
				{
				}
				if (stream == null)
				{
					break;
				}
				num = 0;
			}
			IL_8B:;
		}
	}

	// Token: 0x040021DF RID: 8671
	private const int ᜀ = 4096;

	// Token: 0x040021E0 RID: 8672
	private sprᢳ ᜁ;

	// Token: 0x040021E1 RID: 8673
	private spr\u2084 ᜂ;

	// Token: 0x040021E2 RID: 8674
	private spr\u2547 ᜃ;

	// Token: 0x040021E3 RID: 8675
	private byte[] ᜄ;

	// Token: 0x040021E4 RID: 8676
	private spr\u1AED ᜅ = new spr\u1AED();
}
