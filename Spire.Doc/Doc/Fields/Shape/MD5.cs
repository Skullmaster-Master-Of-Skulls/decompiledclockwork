using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200006B RID: 107
	public class MD5
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00007F00 File Offset: 0x00006F00
		public static byte[] ComputeHash(byte[] b)
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
			return MD5.ComputeHash(b, b.Length);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00007F44 File Offset: 0x00006F44
		public static byte[] ComputeHash(byte[] b, int length)
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
			MD5 md = new MD5();
			md.Update(b, length);
			md.FinalUpdate();
			return md.Digest;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00007F9C File Offset: 0x00006F9C
		public MD5()
		{
			this.ᝂ[0] = 1732584193U;
			this.ᝂ[1] = 4023233417U;
			this.ᝂ[2] = 2562383102U;
			this.ᝂ[3] = 271733878U;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00008018 File Offset: 0x00007018
		public void Update(byte[] buffer, int length)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = (int)(this.ᝁ[0] >> 3 & 63U);
					int num2 = 3;
					for (;;)
					{
						uint[] array;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_119;
						case 1:
							goto IL_1D3;
						case 2:
							goto IL_168;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_15A;
							default:
								if (false)
								{
								}
								if (this.ᝁ[0] + (uint)((uint)length << 3) < this.ᝁ[0])
								{
									num2 = 4;
									continue;
								}
								goto IL_119;
							}
							break;
						case 4:
							this.ᝁ[1] += 1U;
							num2 = 0;
							continue;
						case 5:
							goto IL_1AE;
						case 6:
						{
							int num3 = 0;
							int num4 = 0;
							if (true)
							{
							}
							num2 = 10;
							continue;
						}
						case 7:
							this.ᜀ(array);
							num = 0;
							num2 = 2;
							continue;
						case 8:
							if (num5 >= length)
							{
								num2 = 12;
								continue;
							}
							this.ᝃ[num++] = buffer[num5];
							num2 = 11;
							continue;
						case 9:
						{
							int num3;
							if (num3 >= 16)
							{
								num2 = 7;
								continue;
							}
							int num4;
							array[num3] = (uint)((int)this.ᝃ[num4 + 3] << 24 | (int)this.ᝃ[num4 + 2] << 16 | (int)this.ᝃ[num4 + 1] << 8 | (int)this.ᝃ[num4]);
							num3++;
							num4 += 4;
							num2 = 5;
							continue;
						}
						case 10:
							goto IL_1AE;
						case 11:
							if (num == 64)
							{
								num2 = 6;
								continue;
							}
							goto IL_168;
						case 12:
							return;
						case 13:
							goto IL_1D3;
						}
						break;
						IL_15A:
						num2 = 1;
						continue;
						IL_119:
						this.ᝁ[0] += (uint)((uint)length << 3);
						this.ᝁ[1] += (uint)length >> 29;
						array = new uint[16];
						num5 = 0;
						goto IL_15A;
						IL_168:
						num5++;
						num2 = 13;
						continue;
						IL_1AE:
						num2 = 9;
						continue;
						IL_1D3:
						num2 = 8;
					}
				}
				return;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008264 File Offset: 0x00007264
		public void FinalUpdate()
		{
			switch (0)
			{
			default:
			{
				uint[] array;
				for (;;)
				{
					array = new uint[]
					{
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						0U,
						this.ᝁ[0],
						this.ᝁ[1]
					};
					int num = (int)(this.ᝁ[0] >> 3 & 63U);
					int num2 = 0;
					for (;;)
					{
						int num3;
						int num4;
						int num5;
						switch (num2)
						{
						case 0:
							if (num >= 56)
							{
								num2 = 2;
								continue;
							}
							num2 = 6;
							continue;
						case 1:
							if (true)
							{
							}
							goto IL_D1;
						case 2:
							goto IL_10C;
						case 3:
							goto IL_D1;
						case 4:
							num3 = 120 - num;
							goto IL_11E;
						case 5:
							if (num4 >= 14)
							{
								num2 = 7;
								continue;
							}
							array[num4] = (uint)((int)this.ᝃ[num5 + 3] << 24 | (int)this.ᝃ[num5 + 2] << 16 | (int)this.ᝃ[num5 + 1] << 8 | (int)this.ᝃ[num5]);
							num4++;
							num5 += 4;
							num2 = 3;
							continue;
						case 6:
							num3 = 56 - num;
							goto IL_11E;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_10C;
							default:
								goto IL_15C;
							}
							break;
						}
						break;
						IL_D1:
						num2 = 5;
						continue;
						IL_10C:
						num2 = 4;
						continue;
						IL_11E:
						int length = num3;
						this.Update(MD5.ᝀ, length);
						num4 = 0;
						num5 = 0;
						num2 = 1;
					}
				}
				IL_15C:
				if (false)
				{
				}
				this.ᜀ(array);
				this.StoreDigest();
				return;
			}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000083E0 File Offset: 0x000073E0
		public void StoreDigest()
		{
			for (;;)
			{
				for (;;)
				{
					int num = 0;
					int num2 = 0;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return;
						case 1:
							if (num >= 4)
							{
								num3 = 0;
								continue;
							}
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
								this.Digest[num2] = (byte)(this.ᝂ[num] & 255U);
								this.Digest[num2 + 1] = (byte)(this.ᝂ[num] >> 8 & 255U);
								this.Digest[num2 + 2] = (byte)(this.ᝂ[num] >> 16 & 255U);
								this.Digest[num2 + 3] = (byte)(this.ᝂ[num] >> 24 & 255U);
								num++;
								num2 += 4;
								num3 = 3;
								continue;
							}
							break;
						case 2:
							goto IL_26;
						case 3:
							goto IL_26;
						}
						break;
						IL_26:
						num3 = 1;
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000084DC File Offset: 0x000074DC
		private void ᜀ(uint[] A_0)
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
			uint num = this.ᝂ[0];
			uint num2 = this.ᝂ[1];
			uint num3 = this.ᝂ[2];
			uint num4 = this.ᝂ[3];
			num += (((num3 ^ num4) & num2) ^ num4) + 3614090360U + A_0[0];
			num = (num << 7 | num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + 3905402710U + A_0[1];
			num4 = (num4 << 12 | num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + 606105819U + A_0[2];
			num3 = (num3 << 17 | num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + 3250441966U + A_0[3];
			num2 = (num2 << 22 | num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + 4118548399U + A_0[4];
			num = (num << 7 | num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + 1200080426U + A_0[5];
			num4 = (num4 << 12 | num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + 2821735955U + A_0[6];
			num3 = (num3 << 17 | num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + 4249261313U + A_0[7];
			num2 = (num2 << 22 | num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + 1770035416U + A_0[8];
			num = (num << 7 | num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + 2336552879U + A_0[9];
			num4 = (num4 << 12 | num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + 4294925233U + A_0[10];
			num3 = (num3 << 17 | num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + 2304563134U + A_0[11];
			num2 = (num2 << 22 | num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + 1804603682U + A_0[12];
			num = (num << 7 | num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + 4254626195U + A_0[13];
			num4 = (num4 << 12 | num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + 2792965006U + A_0[14];
			num3 = (num3 << 17 | num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + 1236535329U + A_0[15];
			num2 = (num2 << 22 | num2 >> 10);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + 4129170786U + A_0[1];
			num = (num << 5 | num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + 3225465664U + A_0[6];
			num4 = (num4 << 9 | num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + 643717713U + A_0[11];
			num3 = (num3 << 14 | num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + 3921069994U + A_0[0];
			num2 = (num2 << 20 | num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + 3593408605U + A_0[5];
			num = (num << 5 | num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + 38016083U + A_0[10];
			num4 = (num4 << 9 | num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + 3634488961U + A_0[15];
			num3 = (num3 << 14 | num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + 3889429448U + A_0[4];
			num2 = (num2 << 20 | num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + 568446438U + A_0[9];
			num = (num << 5 | num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + 3275163606U + A_0[14];
			num4 = (num4 << 9 | num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + 4107603335U + A_0[3];
			num3 = (num3 << 14 | num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + 1163531501U + A_0[8];
			num2 = (num2 << 20 | num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + 2850285829U + A_0[13];
			num = (num << 5 | num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + 4243563512U + A_0[2];
			num4 = (num4 << 9 | num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + 1735328473U + A_0[7];
			num3 = (num3 << 14 | num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + 2368359562U + A_0[12];
			num2 = (num2 << 20 | num2 >> 12);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + 4294588738U + A_0[5];
			num = (num << 4 | num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + 2272392833U + A_0[8];
			num4 = (num4 << 11 | num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + 1839030562U + A_0[11];
			num3 = (num3 << 16 | num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + 4259657740U + A_0[14];
			num2 = (num2 << 23 | num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + 2763975236U + A_0[1];
			num = (num << 4 | num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + 1272893353U + A_0[4];
			num4 = (num4 << 11 | num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + 4139469664U + A_0[7];
			num3 = (num3 << 16 | num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + 3200236656U + A_0[10];
			num2 = (num2 << 23 | num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + 681279174U + A_0[13];
			num = (num << 4 | num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + 3936430074U + A_0[0];
			num4 = (num4 << 11 | num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + 3572445317U + A_0[3];
			num3 = (num3 << 16 | num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + 76029189U + A_0[6];
			num2 = (num2 << 23 | num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + 3654602809U + A_0[9];
			num = (num << 4 | num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + 3873151461U + A_0[12];
			num4 = (num4 << 11 | num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + 530742520U + A_0[15];
			num3 = (num3 << 16 | num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + 3299628645U + A_0[2];
			num2 = (num2 << 23 | num2 >> 9);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + 4096336452U + A_0[0];
			num = (num << 6 | num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + 1126891415U + A_0[7];
			num4 = (num4 << 10 | num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + 2878612391U + A_0[14];
			num3 = (num3 << 15 | num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + 4237533241U + A_0[5];
			num2 = (num2 << 21 | num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + 1700485571U + A_0[12];
			num = (num << 6 | num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + 2399980690U + A_0[3];
			num4 = (num4 << 10 | num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + 4293915773U + A_0[10];
			num3 = (num3 << 15 | num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + 2240044497U + A_0[1];
			num2 = (num2 << 21 | num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + 1873313359U + A_0[8];
			num = (num << 6 | num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + 4264355552U + A_0[15];
			num4 = (num4 << 10 | num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + 2734768916U + A_0[6];
			num3 = (num3 << 15 | num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + 1309151649U + A_0[13];
			num2 = (num2 << 21 | num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + 4149444226U + A_0[4];
			num = (num << 6 | num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + 3174756917U + A_0[11];
			num4 = (num4 << 10 | num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + 718787259U + A_0[2];
			num3 = (num3 << 15 | num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + 3951481745U + A_0[9];
			num2 = (num2 << 21 | num2 >> 11);
			num2 += num3;
			this.ᝂ[0] += num;
			this.ᝂ[1] += num2;
			this.ᝂ[2] += num3;
			this.ᝂ[3] += num4;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00008DFC File Offset: 0x00007DFC
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00008E40 File Offset: 0x00007E40
		public byte[] Digest
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
				return this.ᝄ;
			}
			set
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
				this.ᝄ = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00008E84 File Offset: 0x00007E84
		// Note: this type is marked as 'beforefieldinit'.
		static MD5()
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
			byte[] array = new byte[64];
			array[0] = 128;
			MD5.ᝀ = array;
		}

		// Token: 0x040006A9 RID: 1705
		private float \u2609\u0082\u009F\u0080;

		// Token: 0x040006AA RID: 1706
		private const uint ᜀ = 3614090360U;

		// Token: 0x040006AB RID: 1707
		private const uint ᜁ = 3905402710U;

		// Token: 0x040006AC RID: 1708
		private const uint ᜂ = 606105819U;

		// Token: 0x040006AD RID: 1709
		private const uint ᜃ = 3250441966U;

		// Token: 0x040006AE RID: 1710
		private const uint ᜄ = 4118548399U;

		// Token: 0x040006AF RID: 1711
		private const uint ᜅ = 1200080426U;

		// Token: 0x040006B0 RID: 1712
		private const uint ᜆ = 2821735955U;

		// Token: 0x040006B1 RID: 1713
		private const uint ᜇ = 4249261313U;

		// Token: 0x040006B2 RID: 1714
		private const uint ᜈ = 1770035416U;

		// Token: 0x040006B3 RID: 1715
		private const uint ᜉ = 2336552879U;

		// Token: 0x040006B4 RID: 1716
		private const uint ᜊ = 4294925233U;

		// Token: 0x040006B5 RID: 1717
		private const uint ᜋ = 2304563134U;

		// Token: 0x040006B6 RID: 1718
		private const uint ᜌ = 1804603682U;

		// Token: 0x040006B7 RID: 1719
		private const uint \u170D = 4254626195U;

		// Token: 0x040006B8 RID: 1720
		private int \u2593\u00A5\u0092\u00A4;

		// Token: 0x040006B9 RID: 1721
		private const uint ᜎ = 2792965006U;

		// Token: 0x040006BA RID: 1722
		private bool[] \u25D8\u00A1\u0093\u007F;

		// Token: 0x040006BB RID: 1723
		private const uint ᜏ = 1236535329U;

		// Token: 0x040006BC RID: 1724
		private const uint ᜐ = 4129170786U;

		// Token: 0x040006BD RID: 1725
		private const uint ᜑ = 3225465664U;

		// Token: 0x040006BE RID: 1726
		private const uint \u1712 = 643717713U;

		// Token: 0x040006BF RID: 1727
		private const uint \u1713 = 3921069994U;

		// Token: 0x040006C0 RID: 1728
		private const uint \u1714 = 3593408605U;

		// Token: 0x040006C1 RID: 1729
		private const uint \u1715 = 38016083U;

		// Token: 0x040006C2 RID: 1730
		private const uint \u1716 = 3634488961U;

		// Token: 0x040006C3 RID: 1731
		private const uint \u1717 = 3889429448U;

		// Token: 0x040006C4 RID: 1732
		private const uint \u1718 = 568446438U;

		// Token: 0x040006C5 RID: 1733
		private const uint \u1719 = 3275163606U;

		// Token: 0x040006C6 RID: 1734
		private string \u2460\u007F\u0085\u0092;

		// Token: 0x040006C7 RID: 1735
		private const uint \u171A = 4107603335U;

		// Token: 0x040006C8 RID: 1736
		private int \u25D9\u009B\u0093\u00A5;

		// Token: 0x040006C9 RID: 1737
		private const uint \u171B = 1163531501U;

		// Token: 0x040006CA RID: 1738
		private const uint \u171C = 2850285829U;

		// Token: 0x040006CB RID: 1739
		private const uint \u171D = 4243563512U;

		// Token: 0x040006CC RID: 1740
		private const uint \u171E = 1735328473U;

		// Token: 0x040006CD RID: 1741
		private const uint \u171F = 2368359562U;

		// Token: 0x040006CE RID: 1742
		private const uint ᜠ = 4294588738U;

		// Token: 0x040006CF RID: 1743
		private const uint ᜡ = 2272392833U;

		// Token: 0x040006D0 RID: 1744
		private const uint ᜢ = 1839030562U;

		// Token: 0x040006D1 RID: 1745
		private const uint ᜣ = 4259657740U;

		// Token: 0x040006D2 RID: 1746
		private const uint ᜤ = 2763975236U;

		// Token: 0x040006D3 RID: 1747
		private const uint ᜥ = 1272893353U;

		// Token: 0x040006D4 RID: 1748
		private const uint ᜦ = 4139469664U;

		// Token: 0x040006D5 RID: 1749
		private const uint ᜧ = 3200236656U;

		// Token: 0x040006D6 RID: 1750
		private const uint ᜨ = 681279174U;

		// Token: 0x040006D7 RID: 1751
		private const uint ᜩ = 3936430074U;

		// Token: 0x040006D8 RID: 1752
		private const uint ᜪ = 3572445317U;

		// Token: 0x040006D9 RID: 1753
		private const uint ᜫ = 76029189U;

		// Token: 0x040006DA RID: 1754
		private const uint ᜬ = 3654602809U;

		// Token: 0x040006DB RID: 1755
		private const uint ᜭ = 3873151461U;

		// Token: 0x040006DC RID: 1756
		private const uint ᜮ = 530742520U;

		// Token: 0x040006DD RID: 1757
		private const uint ᜯ = 3299628645U;

		// Token: 0x040006DE RID: 1758
		private const uint ᜰ = 4096336452U;

		// Token: 0x040006DF RID: 1759
		private const uint ᜱ = 1126891415U;

		// Token: 0x040006E0 RID: 1760
		private const uint \u1732 = 2878612391U;

		// Token: 0x040006E1 RID: 1761
		private const uint \u1733 = 4237533241U;

		// Token: 0x040006E2 RID: 1762
		private const uint \u1734 = 1700485571U;

		// Token: 0x040006E3 RID: 1763
		private const uint \u1735 = 2399980690U;

		// Token: 0x040006E4 RID: 1764
		private const uint \u1736 = 4293915773U;

		// Token: 0x040006E5 RID: 1765
		private const uint \u1737 = 2240044497U;

		// Token: 0x040006E6 RID: 1766
		private const uint \u1738 = 1873313359U;

		// Token: 0x040006E7 RID: 1767
		private const uint \u1739 = 4264355552U;

		// Token: 0x040006E8 RID: 1768
		private const uint \u173A = 2734768916U;

		// Token: 0x040006E9 RID: 1769
		private const uint \u173B = 1309151649U;

		// Token: 0x040006EA RID: 1770
		private const uint \u173C = 4149444226U;

		// Token: 0x040006EB RID: 1771
		private const uint \u173D = 3174756917U;

		// Token: 0x040006EC RID: 1772
		private const uint \u173E = 718787259U;

		// Token: 0x040006ED RID: 1773
		private const uint \u173F = 3951481745U;

		// Token: 0x040006EE RID: 1774
		private static byte[] ᝀ;

		// Token: 0x040006EF RID: 1775
		private uint[] ᝁ = new uint[2];

		// Token: 0x040006F0 RID: 1776
		private uint[] ᝂ = new uint[4];

		// Token: 0x040006F1 RID: 1777
		private byte[] ᝃ = new byte[64];

		// Token: 0x040006F2 RID: 1778
		private byte[] ᝄ = new byte[16];
	}
}
