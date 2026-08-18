using System;

// Token: 0x0200029C RID: 668
[CLSCompliant(false)]
internal class sprឃ
{
	// Token: 0x06002748 RID: 10056 RVA: 0x00166F3C File Offset: 0x00165F3C
	public uint[] ᜄ()
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
		return this.ᜅ;
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x00166F80 File Offset: 0x00165F80
	public void ᜂ(uint[] A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x00166FC4 File Offset: 0x00165FC4
	public uint[] ᜃ()
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
		return this.ᜆ;
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x00167008 File Offset: 0x00166008
	public void ᜁ(uint[] A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x0016704C File Offset: 0x0016604C
	public byte[] ᜀ()
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
		return this.ᜇ;
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x00167090 File Offset: 0x00166090
	public void ᜁ(byte[] A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x001670D4 File Offset: 0x001660D4
	public byte[] ᜂ()
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
		return this.ᜈ;
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x00167118 File Offset: 0x00166118
	public void ᜀ(byte[] A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x0016715C File Offset: 0x0016615C
	public sprឃ()
	{
		this.ᜅ[0] = (this.ᜅ[1] = 0U);
		this.ᜆ[0] = 1732584193U;
		this.ᜆ[1] = 4023233417U;
		this.ᜆ[2] = 2562383102U;
		this.ᜆ[3] = 271733878U;
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x001671EC File Offset: 0x001661EC
	public void ᜀ(byte[] A_0, uint A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				uint[] array = new uint[16];
				int num = (int)(this.ᜅ[0] >> 3 & 63U);
				int num2 = 11;
				for (;;)
				{
					uint num3;
					uint num4;
					uint num5;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_156;
					case 1:
						num3 = 0U;
						num4 = 0U;
						num2 = 6;
						continue;
					case 2:
						goto IL_10F;
					case 3:
						if (num5 >= A_1)
						{
							num2 = 7;
							continue;
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
							this.ᜇ[num++] = A_0[(int)((UIntPtr)num5)];
							num2 = 5;
							continue;
						}
						break;
					case 4:
						goto IL_1C1;
					case 5:
						if (num == 64)
						{
							num2 = 1;
							continue;
						}
						goto IL_156;
					case 6:
						goto IL_19C;
					case 7:
						return;
					case 8:
						this.ᜅ[1] += 1U;
						num2 = 2;
						continue;
					case 9:
						goto IL_19C;
					case 10:
						goto IL_1C1;
					case 11:
						if (this.ᜅ[0] + (A_1 << 3) < this.ᜅ[0])
						{
							num2 = 8;
							continue;
						}
						goto IL_10F;
					case 12:
						this.ᜀ(array);
						num = 0;
						num2 = 0;
						continue;
					case 13:
						if (num3 >= 16U)
						{
							num2 = 12;
							continue;
						}
						goto IL_B7;
					}
					break;
					IL_B7:
					array[(int)((UIntPtr)num3)] = (uint)((int)this.ᜇ[(int)((UIntPtr)(num4 + 3U))] << 24 | (int)this.ᜇ[(int)((UIntPtr)(num4 + 2U))] << 16 | (int)this.ᜇ[(int)((UIntPtr)(num4 + 1U))] << 8 | (int)this.ᜇ[(int)((UIntPtr)num4)]);
					num3 += 1U;
					num4 += 4U;
					num2 = 9;
					continue;
					IL_10F:
					this.ᜅ[0] += A_1 << 3;
					this.ᜅ[1] += A_1 >> 29;
					num5 = 0U;
					num2 = 4;
					continue;
					IL_156:
					num5 += 1U;
					num2 = 10;
					continue;
					IL_19C:
					num2 = 13;
					continue;
					IL_1C1:
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x0016743C File Offset: 0x0016643C
	public void ᜁ()
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
					this.ᜅ[0],
					this.ᜅ[1]
				};
				uint num = this.ᜅ[0] >> 3 & 63U;
				int num2 = 3;
				for (;;)
				{
					uint num3;
					uint num4;
					uint num5;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						num2 = 6;
						continue;
					case 1:
						goto IL_11F;
					case 2:
						goto IL_FF;
					case 3:
						if (num >= 56U)
						{
							num2 = 0;
							continue;
						}
						goto IL_121;
					case 4:
						if (num3 >= 14U)
						{
							num2 = 1;
							continue;
						}
						array[(int)((UIntPtr)num3)] = (uint)((int)this.ᜇ[(int)((UIntPtr)(num4 + 3U))] << 24 | (int)this.ᜇ[(int)((UIntPtr)(num4 + 2U))] << 16 | (int)this.ᜇ[(int)((UIntPtr)(num4 + 1U))] << 8 | (int)this.ᜇ[(int)((UIntPtr)num4)]);
						num3 += 1U;
						num4 += 4U;
						num2 = 7;
						continue;
					case 5:
						num5 = 56U - num;
						goto IL_14D;
					case 6:
						num5 = 120U - num;
						goto IL_14D;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_121;
						default:
							if (false)
							{
							}
							goto IL_FF;
						}
						break;
					}
					break;
					IL_FF:
					num2 = 4;
					continue;
					IL_121:
					num2 = 5;
					continue;
					IL_14D:
					uint a_ = num5;
					this.ᜀ(sprឃ.ᜄ, a_);
					num3 = 0U;
					num4 = 0U;
					num2 = 2;
				}
			}
			IL_11F:
			this.ᜀ(array);
			this.ᜅ();
			return;
		}
		}
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x001675C4 File Offset: 0x001665C4
	public void ᜅ()
	{
		for (;;)
		{
			uint num = 0U;
			uint num2 = 0U;
			int num3 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					goto IL_2E;
				case 1:
					if (num >= 4U)
					{
						num3 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						this.ᜈ[(int)((UIntPtr)num2)] = (byte)(this.ᜆ[(int)((UIntPtr)num)] & 255U);
						this.ᜈ[(int)((UIntPtr)(num2 + 1U))] = (byte)(this.ᜆ[(int)((UIntPtr)num)] >> 8 & 255U);
						this.ᜈ[(int)((UIntPtr)(num2 + 2U))] = (byte)(this.ᜆ[(int)((UIntPtr)num)] >> 16 & 255U);
						this.ᜈ[(int)((UIntPtr)(num2 + 3U))] = (byte)(this.ᜆ[(int)((UIntPtr)num)] >> 24 & 255U);
						num += 1U;
						num2 += 4U;
						num3 = 0;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_2E;
				}
				break;
				IL_2E:
				num3 = 1;
			}
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x001676C8 File Offset: 0x001666C8
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

	// Token: 0x06002755 RID: 10069 RVA: 0x0016770C File Offset: 0x0016670C
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

	// Token: 0x06002756 RID: 10070 RVA: 0x00167750 File Offset: 0x00166750
	private uint ᜁ(uint A_0, uint A_1, uint A_2)
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
		return A_0 ^ A_1 ^ A_2;
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x00167790 File Offset: 0x00166790
	private uint ᜀ(uint A_0, uint A_1, uint A_2)
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
		return A_1 ^ (A_0 | ~A_2);
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x001677D4 File Offset: 0x001667D4
	private uint ᜀ(uint A_0, byte A_1)
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
		return A_0 << (int)A_1 | A_0 >> (int)(32 - A_1);
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x00167820 File Offset: 0x00166820
	private void ᜃ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜃ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x00167884 File Offset: 0x00166884
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

	// Token: 0x0600275B RID: 10075 RVA: 0x001678E8 File Offset: 0x001668E8
	private void ᜁ(ref uint A_0, uint A_1, uint A_2, uint A_3, uint A_4, byte A_5, uint A_6)
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
		A_0 += this.ᜁ(A_1, A_2, A_3) + A_4 + A_6;
		A_0 = this.ᜀ(A_0, A_5);
		A_0 += A_1;
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x0016794C File Offset: 0x0016694C
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

	// Token: 0x0600275D RID: 10077 RVA: 0x001679B0 File Offset: 0x001669B0
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
		uint num = this.ᜆ[0];
		uint num2 = this.ᜆ[1];
		uint num3 = this.ᜆ[2];
		uint num4 = this.ᜆ[3];
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
		this.ᜆ[0] += num;
		this.ᜆ[1] += num2;
		this.ᜆ[2] += num3;
		this.ᜆ[3] += num4;
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x00167FC0 File Offset: 0x00166FC0
	// Note: this type is marked as 'beforefieldinit'.
	static sprឃ()
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
		byte[] array = new byte[64];
		array[0] = 128;
		sprឃ.ᜄ = array;
	}

	// Token: 0x04001364 RID: 4964
	private const uint ᜀ = 1732584193U;

	// Token: 0x04001365 RID: 4965
	private const uint ᜁ = 4023233417U;

	// Token: 0x04001366 RID: 4966
	private const uint ᜂ = 2562383102U;

	// Token: 0x04001367 RID: 4967
	private const uint ᜃ = 271733878U;

	// Token: 0x04001368 RID: 4968
	private static byte[] ᜄ;

	// Token: 0x04001369 RID: 4969
	private uint[] ᜅ = new uint[2];

	// Token: 0x0400136A RID: 4970
	private uint[] ᜆ = new uint[4];

	// Token: 0x0400136B RID: 4971
	private byte[] ᜇ = new byte[64];

	// Token: 0x0400136C RID: 4972
	private byte[] ᜈ = new byte[16];
}
