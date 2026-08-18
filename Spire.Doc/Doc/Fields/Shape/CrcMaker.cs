using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000063 RID: 99
	public class CrcMaker
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00006C40 File Offset: 0x00005C40
		public CrcMaker()
		{
			this.ᜀ(CrcEncoding.CRC32);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00006C94 File Offset: 0x00005C94
		internal CrcMaker(CrcEncoding A_0)
		{
			this.ᜀ(A_0);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00006CE8 File Offset: 0x00005CE8
		internal CrcEncoding Encoding
		{
			get
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
				return this.ᜌ;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00006D2C File Offset: 0x00005D2C
		private void ᜀ(CrcEncoding A_0)
		{
			switch (0)
			{
			default:
			{
				long num3;
				for (;;)
				{
					this.ᜌ = A_0;
					int num = 4;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
						{
							long num2;
							if (num2 != 0L)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_207;
						}
						case 1:
							goto IL_30B;
						case 2:
							num3 |= this.ᜈ;
							num = 11;
							continue;
						case 3:
							goto IL_2AB;
						case 4:
							switch (A_0)
							{
							case CrcEncoding.CRC16:
								this.ᜀ = 16;
								this.ᜂ = 1;
								this.ᜁ = 32773L;
								this.ᜃ = 0L;
								this.ᜄ = 0L;
								this.ᜅ = 1;
								this.ᜆ = 1;
								num = 24;
								continue;
							case CrcEncoding.CRC32:
								goto IL_24A;
							case CrcEncoding.CRC_CCITT:
								this.ᜀ = 16;
								this.ᜂ = 1;
								this.ᜁ = 4129L;
								this.ᜃ = 65535L;
								this.ᜄ = 0L;
								this.ᜅ = 0;
								this.ᜆ = 0;
								num = 16;
								continue;
							case CrcEncoding.CRC_CCITT_Reverse:
								this.ᜀ = 16;
								this.ᜂ = 1;
								this.ᜁ = 4129L;
								this.ᜃ = 0L;
								this.ᜄ = 0L;
								this.ᜅ = 1;
								this.ᜆ = 1;
								goto IL_1D0;
							default:
								num = 17;
								continue;
							}
							break;
						case 5:
							goto IL_24A;
						case 6:
							goto IL_EF;
						case 7:
							num3 ^= this.ᜁ;
							num = 19;
							continue;
						case 8:
							goto IL_113;
						case 9:
							goto IL_2AB;
						case 10:
							num3 ^= this.ᜁ;
							num = 18;
							continue;
						case 11:
							goto IL_207;
						case 12:
							if (this.ᜂ == 0)
							{
								num = 25;
								continue;
							}
							this.ᜉ = this.ᜃ;
							num3 = this.ᜃ;
							num4 = 0;
							num = 9;
							continue;
						case 13:
						{
							if (num4 >= this.ᜀ)
							{
								num = 8;
								continue;
							}
							long num2 = num3 & this.ᜈ;
							num3 <<= 1;
							num = 23;
							continue;
						}
						case 14:
							goto IL_2CF;
						case 15:
						{
							long num2;
							if (num2 != 0L)
							{
								num = 7;
								continue;
							}
							goto IL_166;
						}
						case 16:
							goto IL_30B;
						case 17:
							num = 5;
							continue;
						case 18:
							goto IL_235;
						case 19:
							goto IL_166;
						case 20:
							goto IL_30B;
						case 21:
						{
							if (num4 >= this.ᜀ)
							{
								num = 14;
								continue;
							}
							long num2 = num3 & 1L;
							num = 15;
							continue;
						}
						case 22:
							goto IL_EF;
						case 23:
						{
							long num2;
							if (num2 != 0L)
							{
								num = 10;
								continue;
							}
							goto IL_235;
						}
						case 24:
							goto IL_30B;
						case 25:
							this.ᜊ = this.ᜃ;
							num3 = this.ᜃ;
							num4 = 0;
							num = 6;
							continue;
						}
						break;
						IL_EF:
						num = 13;
						continue;
						IL_166:
						num3 >>= 1;
						num = 0;
						continue;
						IL_1D0:
						num = 20;
						continue;
						IL_207:
						num4++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D0;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						IL_235:
						num4++;
						num = 22;
						continue;
						IL_24A:
						this.ᜀ = 32;
						this.ᜂ = 1;
						this.ᜁ = 79764919L;
						this.ᜃ = (long)((ulong)-1);
						this.ᜄ = (long)((ulong)-1);
						this.ᜅ = 1;
						this.ᜆ = 1;
						num = 1;
						continue;
						IL_2AB:
						num = 21;
						continue;
						IL_30B:
						this.ᜇ = ((1L << this.ᜀ - 1) - 1L << 1 | 1L);
						this.ᜈ = 1L << this.ᜀ - 1;
						this.ᜀ();
						num = 12;
					}
				}
				IL_113:
				num3 &= this.ᜇ;
				this.ᜉ = num3;
				return;
				IL_2CF:
				this.ᜊ = num3;
				return;
			}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00007168 File Offset: 0x00006168
		public int MakeCRC(byte[] p)
		{
			long num2;
			for (;;)
			{
				IL_4C:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_132:
					num = 8;
					break;
				default:
					if (false)
					{
					}
					num2 = this.ᜉ;
					num = 10;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if ((this.ᜆ ^ this.ᜅ) != 0)
						{
							num = 7;
							continue;
						}
						goto IL_202;
					case 1:
						goto IL_179;
					case 2:
						goto IL_FA;
					case 3:
					{
						int num3;
						if (num3 >= p.Length)
						{
							num = 6;
							continue;
						}
						num2 = (num2 << 8 ^ this.ᜋ[(int)((num2 >> this.ᜀ - 8 & 255L) ^ (long)((ulong)p[num3]))]);
						num3++;
						num = 14;
						continue;
					}
					case 4:
					{
						if (this.ᜅ == 0)
						{
							num = 13;
							continue;
						}
						int num4 = 0;
						num = 15;
						continue;
					}
					case 5:
						goto IL_8A;
					case 6:
						num = 2;
						continue;
					case 7:
						num2 = this.ᜀ(num2, this.ᜀ);
						num = 1;
						continue;
					case 8:
						goto IL_CA;
					case 9:
						goto IL_13F;
					case 10:
						if (this.ᜅ != 0)
						{
							num = 5;
							continue;
						}
						goto IL_CA;
					case 11:
					{
						int num4;
						if (num4 >= p.Length)
						{
							num = 12;
							continue;
						}
						num2 = (num2 >> 8 ^ this.ᜋ[(int)((num2 & 255L) ^ (long)((ulong)p[num4]))]);
						num4++;
						num = 9;
						continue;
					}
					case 12:
						goto IL_FA;
					case 13:
					{
						int num3 = 0;
						if (true)
						{
						}
						num = 16;
						continue;
					}
					case 14:
						goto IL_17E;
					case 15:
						goto IL_13F;
					case 16:
						goto IL_17E;
					}
					goto IL_4C;
					IL_CA:
					num = 4;
					continue;
					IL_FA:
					num = 0;
					continue;
					IL_13F:
					num = 11;
					continue;
					IL_17E:
					num = 3;
				}
				IL_8A:
				num2 = this.ᜀ(num2, this.ᜀ);
				goto IL_132;
			}
			IL_179:
			IL_202:
			num2 ^= this.ᜄ;
			num2 &= this.ᜇ;
			return (int)num2;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000738C File Offset: 0x0000638C
		private long ᜀ(long A_0, int A_1)
		{
			long num2;
			for (;;)
			{
				for (;;)
				{
					long num = 1L;
					num2 = 0L;
					long num3 = 1L << A_1 - 1;
					int num4 = 2;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							if ((A_0 & num3) != 0L)
							{
								num4 = 1;
								continue;
							}
							goto IL_3E;
						case 1:
							num2 |= num;
							num4 = 3;
							continue;
						case 2:
							goto IL_96;
						case 3:
							goto IL_3E;
						case 4:
							return num2;
						case 5:
							goto IL_96;
						case 6:
							if (true)
							{
							}
							if (num3 == 0L)
							{
								num4 = 4;
								continue;
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
								num4 = 0;
								continue;
							}
							break;
						}
						break;
						IL_3E:
						num <<= 1;
						num3 >>= 1;
						num4 = 5;
						continue;
						IL_96:
						num4 = 6;
					}
				}
			}
			return num2;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00007460 File Offset: 0x00006460
		private void ᜀ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 16;
					for (;;)
					{
						long num3;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_14A;
						case 1:
							goto IL_1AF;
						case 2:
							num3 = this.ᜀ(num3, this.ᜀ);
							num2 = 9;
							continue;
						case 3:
							if (num >= 256)
							{
								num2 = 8;
								continue;
							}
							num3 = (long)num;
							num2 = 12;
							continue;
						case 4:
							if (this.ᜅ != 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_B9;
						case 5:
						{
							long num4;
							if (num4 != 0L)
							{
								num2 = 10;
								continue;
							}
							goto IL_8A;
						}
						case 6:
							num3 = this.ᜀ(num3, 8);
							num2 = 14;
							continue;
						case 7:
							goto IL_8A;
						case 8:
							return;
						case 9:
							goto IL_B9;
						case 10:
							if (true)
							{
							}
							num3 ^= this.ᜁ;
							num2 = 7;
							continue;
						case 11:
							goto IL_1AF;
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								if (this.ᜅ != 0)
								{
									num2 = 6;
									continue;
								}
								goto IL_191;
							}
							break;
						case 13:
						{
							if (num5 >= 8)
							{
								num2 = 15;
								continue;
							}
							long num4 = num3 & this.ᜈ;
							num3 <<= 1;
							num2 = 5;
							continue;
						}
						case 14:
							goto IL_191;
						case 15:
							num2 = 4;
							continue;
						case 16:
							goto IL_14A;
						}
						break;
						IL_8A:
						num5++;
						num2 = 1;
						continue;
						IL_B9:
						num3 &= this.ᜇ;
						this.ᜋ[num] = num3;
						num++;
						num2 = 0;
						continue;
						IL_14A:
						num2 = 3;
						continue;
						IL_191:
						num3 <<= this.ᜀ - 8;
						num5 = 0;
						num2 = 11;
						continue;
						IL_1AF:
						num2 = 13;
					}
				}
				return;
			}
		}

		// Token: 0x0400061F RID: 1567
		private int ᜀ = 16;

		// Token: 0x04000620 RID: 1568
		private long ᜁ = 4129L;

		// Token: 0x04000621 RID: 1569
		private int ᜂ = 1;

		// Token: 0x04000622 RID: 1570
		private byte[] \u25D9\u0083\u008D\u0096;

		// Token: 0x04000623 RID: 1571
		private long ᜃ = 65535L;

		// Token: 0x04000624 RID: 1572
		private long ᜄ;

		// Token: 0x04000625 RID: 1573
		private int ᜅ;

		// Token: 0x04000626 RID: 1574
		private bool[] \u25D8\u0090\u0095\u0097;

		// Token: 0x04000627 RID: 1575
		private int ᜆ;

		// Token: 0x04000628 RID: 1576
		private long ᜇ;

		// Token: 0x04000629 RID: 1577
		private long ᜈ;

		// Token: 0x0400062A RID: 1578
		private byte[] \u2460\u00AB\u0091\u008E;

		// Token: 0x0400062B RID: 1579
		private byte \u25D9\u008E\u009A\u0090;

		// Token: 0x0400062C RID: 1580
		private long[] \u25D8\u0086\u0083\u0094;

		// Token: 0x0400062D RID: 1581
		private long ᜉ;

		// Token: 0x0400062E RID: 1582
		private long ᜊ;

		// Token: 0x0400062F RID: 1583
		private long[] ᜋ = new long[256];

		// Token: 0x04000630 RID: 1584
		private bool[] \u25D9\u00AE\u008C\u0085;

		// Token: 0x04000631 RID: 1585
		private CrcEncoding ᜌ;
	}
}
