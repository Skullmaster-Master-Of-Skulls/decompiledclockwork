using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200020D RID: 525
[CLSCompliant(false)]
internal class spr\u19F9 : spr\u23F8
{
	// Token: 0x060018AE RID: 6318 RVA: 0x001789DC File Offset: 0x001779DC
	internal uint[] ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x00178A20 File Offset: 0x00177A20
	internal spr\u24D2[] ᜄ()
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
		return this.ᜂ;
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x00178A64 File Offset: 0x00177A64
	internal int ᜁ()
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
		return this.ᜂ.Length;
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x00178AA8 File Offset: 0x00177AA8
	internal void ᜀ(int A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.ᜁ = new uint[A_0 + 1];
				num = 9;
				continue;
			case 1:
				goto IL_6D;
			case 2:
				num = 7;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DA;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				goto IL_EE;
			case 5:
				goto IL_EE;
			case 6:
				if (this.ᜂ != null)
				{
					num = 2;
					continue;
				}
				goto IL_8A;
			case 7:
				if (A_0 != this.ᜂ.Length)
				{
					num = 10;
					continue;
				}
				return;
			case 8:
				if (num2 >= A_0)
				{
					num = 0;
					continue;
				}
				this.ᜂ[num2] = new spr\u24D2();
				num2++;
				num = 5;
				continue;
			case 9:
				goto IL_D8;
			case 10:
				goto IL_8A;
			}
			if (A_0 < 0)
			{
				num = 1;
				continue;
			}
			num = 6;
			continue;
			IL_8A:
			this.ᜂ = new spr\u24D2[A_0];
			num2 = 0;
			num = 4;
			continue;
			IL_EE:
			num = 8;
		}
		IL_6D:
		goto IL_DA;
		IL_D8:
		return;
		IL_DA:
		throw new ArgumentOutOfRangeException(ClipboardData.b("㽬ᩮὰr㙴ᡶ౸ᕺॼ", a_));
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x00178BFC File Offset: 0x00177BFC
	internal override int ᜇ()
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
		return 512;
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x00178C3C File Offset: 0x00177C3C
	internal spr\u19F9()
	{
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x00178C50 File Offset: 0x00177C50
	internal spr\u19F9(spr\u193A A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x00178C6C File Offset: 0x00177C6C
	private void ᜀ(spr\u193A A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] array = A_0.ᜂ();
				this.ᜁ = new uint[(int)(A_0.ᜁ() + 1)];
				byte[] array2 = new byte[(int)A_0.ᜁ()];
				int num = (int)((A_0.ᜁ() + 1) * 4);
				Buffer.BlockCopy(array, 0, this.ᜁ, 0, num);
				Array.Copy(array, num, array2, 0, (int)A_0.ᜁ());
				this.ᜂ = new spr\u24D2[(int)A_0.ᜁ()];
				int num2 = 0;
				if (true)
				{
				}
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB;
						default:
							goto IL_10B;
						}
						break;
					case 1:
						goto IL_9F;
					case 2:
						goto IL_AB;
					case 3:
						goto IL_9F;
					}
					break;
					IL_9F:
					num3 = 2;
					continue;
					IL_AB:
					if (num2 >= (int)A_0.ᜁ())
					{
						num3 = 0;
					}
					else
					{
						int a_ = (int)(array2[num2] * 2);
						this.ᜂ[num2] = new spr\u24D2(array, a_);
						num2++;
						num3 = 1;
					}
				}
			}
			IL_10B:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x00178D8C File Offset: 0x00177D8C
	private spr\u193A ᜀ()
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			spr\u193A spr_u193A;
			int num2;
			int num3;
			byte[] array;
			int num4;
			for (;;)
			{
				spr_u193A = new spr\u193A();
				int num = this.ᜁ();
				num2 = 0;
				spr_u193A.ᜀ((byte)num);
				num3 = (num + 1) * 4;
				Buffer.BlockCopy(this.ᜁ, 0, spr_u193A.ᜂ(), 0, num3);
				array = new byte[num];
				num4 = 511;
				int num5 = num - 1;
				int num6 = 11;
				for (;;)
				{
					int num7;
					int num8;
					switch (num6)
					{
					case 0:
						num6 = 3;
						continue;
					case 1:
						goto IL_CB;
					case 2:
						goto IL_298;
					case 3:
						goto IL_1B9;
					case 4:
						num7--;
						num6 = 12;
						continue;
					case 5:
						goto IL_1D0;
					case 6:
						if (num7 % 2 != 0)
						{
							num6 = 4;
							continue;
						}
						goto IL_248;
					case 7:
						if (true)
						{
						}
						num8 = this.ᜂ[num5].ᜇ();
						num7 = num4 - num8;
						num6 = 6;
						continue;
					case 8:
						if (this.ᜂ[num5] != null)
						{
							num6 = 7;
							continue;
						}
						goto IL_298;
					case 9:
						if (num5 < 0)
						{
							num6 = 0;
							continue;
						}
						num6 = 8;
						continue;
					case 10:
						if (num4 < num3 + array.Length)
						{
							num6 = 13;
							continue;
						}
						goto IL_2AF;
					case 11:
						goto IL_CB;
					case 12:
						goto IL_248;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B9;
						default:
							goto IL_17A;
						}
						break;
					}
					break;
					IL_CB:
					num6 = 9;
					continue;
					IL_1B9:
					if (num2 > 512)
					{
						num6 = 5;
						continue;
					}
					num6 = 10;
					continue;
					IL_248:
					num4 = num7;
					array[num5] = (byte)(num4 / 2);
					this.ᜂ[num5].ᜀ(spr_u193A.ᜂ(), num4);
					num2 += num8 + 4 + 1;
					num6 = 2;
					continue;
					IL_298:
					num5--;
					num6 = 1;
				}
			}
			IL_17A:
			if (false)
			{
			}
			throw new Exception(string.Concat(new object[]
			{
				ClipboardData.b("㝰㡲╴坶㩸፺ർݾꆀﾌ꾎ﺐﾘ趠莢趤螦쪨쎪\uddac힮醰삲솴횶쮸쾺鶼\udebe뗀蓼", a_),
				num4.ToString(),
				ClipboardData.b("㝰ひ啴ቶ᝸ὺ䝼彾", a_),
				num3,
				ClipboardData.b("嵰卲ၴ᥶ᵸ孺ቼ᥾ꆀ뎈ꮊ", a_),
				(num3 + array.Length).ToString()
			}));
			IL_1D0:
			throw new Exception(ClipboardData.b("㝰㡲╴坶㩸፺ർݾꆀﾌ꾎ﺐﾘ鮠莢", a_) + num2.ToString());
			IL_2AF:
			array.CopyTo(spr_u193A.ᜂ(), num3);
			return spr_u193A;
		}
		}
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x00179058 File Offset: 0x00178058
	internal override int ᜀ(byte[] A_0, int A_1)
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
		spr\u193A spr_u193A = this.ᜀ();
		return spr_u193A.ᜀ(A_0, A_1);
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x001790A4 File Offset: 0x001780A4
	internal int ᜀ(BinaryWriter A_0, Stream A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			long position;
			int num;
			int num2;
			for (;;)
			{
				position = A_1.Position;
				Dictionary<int, byte> dictionary = new Dictionary<int, byte>();
				num = this.ᜁ();
				num2 = 0;
				int num3 = (num + 1) * 4;
				byte[] array = new byte[num3];
				Buffer.BlockCopy(this.ᜁ, 0, array, 0, num3);
				A_1.Write(array, 0, array.Length);
				byte[] array2 = new byte[num];
				int num4 = 511;
				int num5 = num - 1;
				int num6 = 18;
				for (;;)
				{
					int num7;
					int num8;
					switch (num6)
					{
					case 0:
						goto IL_FD;
					case 1:
						goto IL_26A;
					case 2:
					{
						long position2;
						if (position2 > position + 511L)
						{
							num6 = 1;
							continue;
						}
						goto IL_35D;
					}
					case 3:
						if (A_1 == null)
						{
							num6 = 14;
							continue;
						}
						num6 = 2;
						continue;
					case 4:
						if (this.ᜂ[num5] != null)
						{
							num6 = 19;
							continue;
						}
						goto IL_32A;
					case 5:
					{
						if (num2 > 512)
						{
							num6 = 6;
							continue;
						}
						long position2 = A_1.Position;
						A_1.Position = position + (long)num3;
						A_1.Write(array2, 0, array2.Length);
						num6 = 3;
						continue;
					}
					case 6:
						goto IL_1AF;
					case 7:
						num7--;
						num6 = 11;
						continue;
					case 8:
						if (true)
						{
						}
						num6 = 16;
						continue;
					case 9:
						goto IL_32A;
					case 10:
					{
						IL_18A:
						int key;
						array2[num5] = dictionary[key];
						num6 = 15;
						continue;
					}
					case 11:
						goto IL_1C8;
					case 12:
					{
						if (num5 < 0)
						{
							num6 = 13;
							continue;
						}
						int key = -1;
						num6 = 20;
						continue;
					}
					case 13:
						num6 = 5;
						continue;
					case 14:
						goto IL_2AA;
					case 15:
						goto IL_32A;
					case 16:
					{
						int key;
						if (this.ᜀ(num5, out key))
						{
							num6 = 10;
							continue;
						}
						goto IL_21B;
					}
					case 17:
						if (num7 % 2 != 0)
						{
							num6 = 7;
							continue;
						}
						goto IL_1C8;
					case 18:
						goto IL_FD;
					case 19:
						num8 = this.ᜂ[num5].ᜇ();
						num7 = num4 - num8;
						num6 = 17;
						continue;
					case 20:
						if (num5 < num - 1)
						{
							num6 = 8;
							continue;
						}
						goto IL_21B;
					}
					break;
					IL_FD:
					num6 = 12;
					continue;
					IL_32A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18A;
					default:
						if (false)
						{
						}
						num5--;
						num6 = 0;
						continue;
					}
					IL_1C8:
					num4 = num7;
					array2[num5] = (byte)(num4 / 2);
					dictionary.Add(num5, array2[num5]);
					A_1.Position = position + (long)num4;
					this.ᜂ[num5].ᜀ(A_0, A_1, num8);
					num2 += num8 + 4 + 1;
					num6 = 9;
					continue;
					IL_21B:
					num6 = 4;
				}
			}
			IL_1AF:
			throw new Exception(ClipboardData.b("㕲㹴❶奸㡺ᕼཾ呂ꎂﶎ놐ﲒﶚ횠馢薤", a_) + num2.ToString());
			IL_26A:
			throw new Exception(ClipboardData.b("ၲᵴݶŸ孺ቼॾﲊ", a_));
			IL_2AA:
			throw new ArgumentNullException(ClipboardData.b("rŴնᱸ᩺ၼ", a_));
			IL_35D:
			A_1.Position = position + 511L;
			A_1.WriteByte((byte)num);
			return (int)A_1.Position;
		}
		}
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x0017942C File Offset: 0x0017842C
	internal bool ᜀ(int A_0, out int A_1)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				spr\u24D2 spr_u24D = this.ᜂ[A_0];
				result = false;
				A_1 = -1;
				int num = this.ᜂ.Length - 1;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num <= A_0)
						{
							num2 = 5;
							continue;
						}
						spr\u24D2 a_ = this.ᜂ[num];
						A_1 = num;
						num2 = 4;
						continue;
					}
					case 1:
						goto IL_53;
					case 2:
						goto IL_53;
					case 3:
						num--;
						num2 = 2;
						continue;
					case 4:
					{
						spr\u24D2 a_;
						if (result = spr_u24D.ᜁ(a_))
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					}
					case 5:
						return result;
					}
					break;
					IL_53:
					num2 = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x04001CC6 RID: 7366
	private new const int ᜀ = 4;

	// Token: 0x04001CC7 RID: 7367
	private new uint[] ᜁ;

	// Token: 0x04001CC8 RID: 7368
	private new spr\u24D2[] ᜂ;
}
