using System;

// Token: 0x0200035F RID: 863
[CLSCompliant(false)]
internal class spr\u180F
{
	// Token: 0x06002E69 RID: 11881 RVA: 0x002C1014 File Offset: 0x002C0014
	internal uint[] ᜄ()
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

	// Token: 0x06002E6A RID: 11882 RVA: 0x002C1058 File Offset: 0x002C0058
	internal uint[] ᜃ()
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

	// Token: 0x06002E6B RID: 11883 RVA: 0x002C109C File Offset: 0x002C009C
	internal byte[] ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06002E6C RID: 11884 RVA: 0x002C10E0 File Offset: 0x002C00E0
	internal byte[] ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x002C1124 File Offset: 0x002C0124
	internal void ᜀ(byte[] A_0, uint A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				uint[] array = new uint[16];
				int num = (int)(this.ᜁ[0] >> 3 & 63U);
				int num2 = 8;
				for (;;)
				{
					uint num5;
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_168;
						default:
							goto IL_23A;
						}
						break;
					case 1:
						goto IL_14E;
					case 2:
						if (num == 64)
						{
							num2 = 9;
							continue;
						}
						goto IL_14E;
					case 3:
						goto IL_194;
					case 4:
						this.ᜁ[1] += 1U;
						num2 = 10;
						continue;
					case 5:
					{
						uint num3;
						if (num3 >= 16U)
						{
							num2 = 6;
							continue;
						}
						uint num4;
						array[(int)((UIntPtr)num3)] = (uint)((int)this.ᜃ[(int)((UIntPtr)(num4 + 3U))] << 24 | (int)this.ᜃ[(int)((UIntPtr)(num4 + 2U))] << 16 | (int)this.ᜃ[(int)((UIntPtr)(num4 + 1U))] << 8 | (int)this.ᜃ[(int)((UIntPtr)num4)]);
						num3 += 1U;
						num4 += 4U;
						num2 = 12;
						continue;
					}
					case 6:
						this.ᜀ(array);
						num = 0;
						num2 = 1;
						continue;
					case 7:
						goto IL_168;
					case 8:
						if (this.ᜁ[0] + (A_1 << 3) < this.ᜁ[0])
						{
							num2 = 4;
							continue;
						}
						goto IL_107;
					case 9:
					{
						uint num3 = 0U;
						uint num4 = 0U;
						num2 = 3;
						continue;
					}
					case 10:
						goto IL_107;
					case 11:
						if (num5 >= A_1)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						this.ᜃ[num++] = A_0[(int)((UIntPtr)num5)];
						num2 = 2;
						continue;
					case 12:
						goto IL_194;
					case 13:
						goto IL_1B9;
					}
					break;
					IL_107:
					this.ᜁ[0] += A_1 << 3;
					this.ᜁ[1] += A_1 >> 29;
					num5 = 0U;
					num2 = 13;
					continue;
					IL_14E:
					num5 += 1U;
					num2 = 7;
					continue;
					IL_194:
					num2 = 5;
					continue;
					IL_1B9:
					num2 = 11;
					continue;
					IL_168:
					goto IL_1B9;
				}
			}
			IL_23A:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x002C1374 File Offset: 0x002C0374
	internal void ᜁ()
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
					this.ᜁ[0],
					this.ᜁ[1]
				};
				uint num = this.ᜁ[0] >> 3 & 63U;
				int num2 = 2;
				for (;;)
				{
					uint num3;
					uint num4;
					uint num5;
					switch (num2)
					{
					case 0:
						goto IL_E1;
					case 1:
						goto IL_E1;
					case 2:
						if (num >= 56U)
						{
							num2 = 7;
							continue;
						}
						num2 = 3;
						continue;
					case 3:
						num3 = 56U - num;
						goto IL_14A;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FC;
						default:
							goto IL_11E;
						}
						break;
					case 5:
						num3 = 120U - num;
						goto IL_14A;
					case 6:
						if (num4 >= 14U)
						{
							goto IL_FC;
						}
						array[(int)((UIntPtr)num4)] = (uint)((int)this.ᜃ[(int)((UIntPtr)(num5 + 3U))] << 24 | (int)this.ᜃ[(int)((UIntPtr)(num5 + 2U))] << 16 | (int)this.ᜃ[(int)((UIntPtr)(num5 + 1U))] << 8 | (int)this.ᜃ[(int)((UIntPtr)num5)]);
						num4 += 1U;
						num5 += 4U;
						num2 = 1;
						continue;
					case 7:
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					break;
					IL_E1:
					num2 = 6;
					continue;
					IL_FC:
					num2 = 4;
					continue;
					IL_14A:
					uint a_ = num3;
					this.ᜀ(spr\u180F.ᜀ, a_);
					num4 = 0U;
					num5 = 0U;
					num2 = 0;
				}
			}
			IL_11E:
			if (false)
			{
			}
			this.ᜀ(array);
			this.ᜅ();
			return;
		}
		}
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x002C14FC File Offset: 0x002C04FC
	internal void ᜅ()
	{
		for (;;)
		{
			uint num = 0U;
			uint num2 = 0U;
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_24;
				case 1:
					return;
				case 2:
					goto IL_2E;
				case 3:
					if (num >= 4U)
					{
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						if (false)
						{
						}
						this.ᜄ[(int)((UIntPtr)num2)] = (byte)(this.ᜂ[(int)((UIntPtr)num)] & 255U);
						this.ᜄ[(int)((UIntPtr)(num2 + 1U))] = (byte)(this.ᜂ[(int)((UIntPtr)num)] >> 8 & 255U);
						this.ᜄ[(int)((UIntPtr)(num2 + 2U))] = (byte)(this.ᜂ[(int)((UIntPtr)num)] >> 16 & 255U);
						this.ᜄ[(int)((UIntPtr)(num2 + 3U))] = (byte)(this.ᜂ[(int)((UIntPtr)num)] >> 24 & 255U);
						num += 1U;
						num2 += 4U;
						num3 = 2;
						continue;
					}
					break;
				}
				break;
				IL_2E:
				num3 = 3;
				continue;
				IL_24:
				if (true)
				{
				}
				goto IL_2E;
			}
		}
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x002C1600 File Offset: 0x002C0600
	private uint ᜃ(uint A_0, uint A_1, uint A_2)
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
		return (A_0 & A_1) | (~A_0 & A_2);
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x002C1644 File Offset: 0x002C0644
	private uint ᜂ(uint A_0, uint A_1, uint A_2)
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
		return (A_0 & A_2) | (A_1 & ~A_2);
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x002C1688 File Offset: 0x002C0688
	private uint ᜁ(uint A_0, uint A_1, uint A_2)
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
		return A_0 ^ A_1 ^ A_2;
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x002C16C8 File Offset: 0x002C06C8
	private uint ᜀ(uint A_0, uint A_1, uint A_2)
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
		return A_1 ^ (A_0 | ~A_2);
	}

	// Token: 0x06002E74 RID: 11892 RVA: 0x002C170C File Offset: 0x002C070C
	private uint ᜀ(uint A_0, byte A_1)
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
		return A_0 << (int)A_1 | A_0 >> (int)(32 - A_1);
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x002C1758 File Offset: 0x002C0758
	private void ᜃ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜃ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x002C17BC File Offset: 0x002C07BC
	private void ᜂ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜂ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x002C1820 File Offset: 0x002C0820
	private void ᜁ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜁ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x002C1884 File Offset: 0x002C0884
	private void ᜀ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜀ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x002C18E8 File Offset: 0x002C08E8
	private void ᜀ(uint[] A_0)
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
		uint num = this.ᜂ[0];
		uint num2 = this.ᜂ[1];
		uint num3 = this.ᜂ[2];
		uint num4 = this.ᜂ[3];
		this.ᜃ(ref num, num2, num3, num4, A_0[0], 7, 3614090360U);
		this.ᜃ(ref num4, num, num2, num3, A_0[1], 12, 3905402710U);
		this.ᜃ(ref num3, num4, num, num2, A_0[2], 17, 606105819U);
		this.ᜃ(ref num2, num3, num4, num, A_0[3], 22, 3250441966U);
		this.ᜃ(ref num, num2, num3, num4, A_0[4], 7, 4118548399U);
		this.ᜃ(ref num4, num, num2, num3, A_0[5], 12, 1200080426U);
		this.ᜃ(ref num3, num4, num, num2, A_0[6], 17, 2821735955U);
		this.ᜃ(ref num2, num3, num4, num, A_0[7], 22, 4249261313U);
		this.ᜃ(ref num, num2, num3, num4, A_0[8], 7, 1770035416U);
		this.ᜃ(ref num4, num, num2, num3, A_0[9], 12, 2336552879U);
		this.ᜃ(ref num3, num4, num, num2, A_0[10], 17, 4294925233U);
		this.ᜃ(ref num2, num3, num4, num, A_0[11], 22, 2304563134U);
		this.ᜃ(ref num, num2, num3, num4, A_0[12], 7, 1804603682U);
		this.ᜃ(ref num4, num, num2, num3, A_0[13], 12, 4254626195U);
		this.ᜃ(ref num3, num4, num, num2, A_0[14], 17, 2792965006U);
		this.ᜃ(ref num2, num3, num4, num, A_0[15], 22, 1236535329U);
		this.ᜂ(ref num, num2, num3, num4, A_0[1], 5, 4129170786U);
		this.ᜂ(ref num4, num, num2, num3, A_0[6], 9, 3225465664U);
		this.ᜂ(ref num3, num4, num, num2, A_0[11], 14, 643717713U);
		this.ᜂ(ref num2, num3, num4, num, A_0[0], 20, 3921069994U);
		this.ᜂ(ref num, num2, num3, num4, A_0[5], 5, 3593408605U);
		this.ᜂ(ref num4, num, num2, num3, A_0[10], 9, 38016083U);
		this.ᜂ(ref num3, num4, num, num2, A_0[15], 14, 3634488961U);
		this.ᜂ(ref num2, num3, num4, num, A_0[4], 20, 3889429448U);
		this.ᜂ(ref num, num2, num3, num4, A_0[9], 5, 568446438U);
		this.ᜂ(ref num4, num, num2, num3, A_0[14], 9, 3275163606U);
		this.ᜂ(ref num3, num4, num, num2, A_0[3], 14, 4107603335U);
		this.ᜂ(ref num2, num3, num4, num, A_0[8], 20, 1163531501U);
		this.ᜂ(ref num, num2, num3, num4, A_0[13], 5, 2850285829U);
		this.ᜂ(ref num4, num, num2, num3, A_0[2], 9, 4243563512U);
		this.ᜂ(ref num3, num4, num, num2, A_0[7], 14, 1735328473U);
		this.ᜂ(ref num2, num3, num4, num, A_0[12], 20, 2368359562U);
		this.ᜁ(ref num, num2, num3, num4, A_0[5], 4, 4294588738U);
		this.ᜁ(ref num4, num, num2, num3, A_0[8], 11, 2272392833U);
		this.ᜁ(ref num3, num4, num, num2, A_0[11], 16, 1839030562U);
		this.ᜁ(ref num2, num3, num4, num, A_0[14], 23, 4259657740U);
		this.ᜁ(ref num, num2, num3, num4, A_0[1], 4, 2763975236U);
		this.ᜁ(ref num4, num, num2, num3, A_0[4], 11, 1272893353U);
		this.ᜁ(ref num3, num4, num, num2, A_0[7], 16, 4139469664U);
		this.ᜁ(ref num2, num3, num4, num, A_0[10], 23, 3200236656U);
		this.ᜁ(ref num, num2, num3, num4, A_0[13], 4, 681279174U);
		this.ᜁ(ref num4, num, num2, num3, A_0[0], 11, 3936430074U);
		this.ᜁ(ref num3, num4, num, num2, A_0[3], 16, 3572445317U);
		this.ᜁ(ref num2, num3, num4, num, A_0[6], 23, 76029189U);
		this.ᜁ(ref num, num2, num3, num4, A_0[9], 4, 3654602809U);
		this.ᜁ(ref num4, num, num2, num3, A_0[12], 11, 3873151461U);
		this.ᜁ(ref num3, num4, num, num2, A_0[15], 16, 530742520U);
		this.ᜁ(ref num2, num3, num4, num, A_0[2], 23, 3299628645U);
		this.ᜀ(ref num, num2, num3, num4, A_0[0], 6, 4096336452U);
		this.ᜀ(ref num4, num, num2, num3, A_0[7], 10, 1126891415U);
		this.ᜀ(ref num3, num4, num, num2, A_0[14], 15, 2878612391U);
		this.ᜀ(ref num2, num3, num4, num, A_0[5], 21, 4237533241U);
		this.ᜀ(ref num, num2, num3, num4, A_0[12], 6, 1700485571U);
		this.ᜀ(ref num4, num, num2, num3, A_0[3], 10, 2399980690U);
		this.ᜀ(ref num3, num4, num, num2, A_0[10], 15, 4293915773U);
		this.ᜀ(ref num2, num3, num4, num, A_0[1], 21, 2240044497U);
		this.ᜀ(ref num, num2, num3, num4, A_0[8], 6, 1873313359U);
		this.ᜀ(ref num4, num, num2, num3, A_0[15], 10, 4264355552U);
		this.ᜀ(ref num3, num4, num, num2, A_0[6], 15, 2734768916U);
		this.ᜀ(ref num2, num3, num4, num, A_0[13], 21, 1309151649U);
		this.ᜀ(ref num, num2, num3, num4, A_0[4], 6, 4149444226U);
		this.ᜀ(ref num4, num, num2, num3, A_0[11], 10, 3174756917U);
		this.ᜀ(ref num3, num4, num, num2, A_0[2], 15, 718787259U);
		this.ᜀ(ref num2, num3, num4, num, A_0[9], 21, 3951481745U);
		this.ᜂ[0] += num;
		this.ᜂ[1] += num2;
		this.ᜂ[2] += num3;
		this.ᜂ[3] += num4;
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x002C1EF8 File Offset: 0x002C0EF8
	public spr\u180F()
	{
		uint[] array = new uint[2];
		this.ᜁ = array;
		this.ᜂ = new uint[]
		{
			1732584193U,
			4023233417U,
			2562383102U,
			271733878U
		};
		this.ᜃ = new byte[64];
		this.ᜄ = new byte[16];
		base..ctor();
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x002C1F4C File Offset: 0x002C0F4C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u180F()
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
		spr\u180F.ᜀ = array;
	}

	// Token: 0x040026C2 RID: 9922
	private static byte[] ᜀ;

	// Token: 0x040026C3 RID: 9923
	private uint[] ᜁ;

	// Token: 0x040026C4 RID: 9924
	private uint[] ᜂ;

	// Token: 0x040026C5 RID: 9925
	private byte[] ᜃ;

	// Token: 0x040026C6 RID: 9926
	private byte[] ᜄ;
}
