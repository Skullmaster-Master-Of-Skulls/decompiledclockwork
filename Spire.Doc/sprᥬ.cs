using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020002DE RID: 734
[CLSCompliant(false)]
internal class sprᥬ
{
	// Token: 0x0600285B RID: 10331 RVA: 0x00283B48 File Offset: 0x00282B48
	internal MemoryStream ᜄ()
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
		return this.ᜎ;
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x00283B8C File Offset: 0x00282B8C
	internal MemoryStream ᜇ()
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
		return this.ᜐ;
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x00283BD0 File Offset: 0x00282BD0
	internal MemoryStream ᜅ()
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
		return this.ᜏ;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x00283C14 File Offset: 0x00282C14
	internal sprᥬ()
	{
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x00283C68 File Offset: 0x00282C68
	internal sprᥬ(MemoryStream A_0, MemoryStream A_1, MemoryStream A_2, sprᾱ A_3)
	{
		this.ᜎ = A_0;
		this.ᜐ = A_1;
		this.ᜏ = A_2;
		this.\u170D = A_3;
		this.ᜑ = A_3.ឰ();
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x00283CE4 File Offset: 0x00282CE4
	public void ᜁ(ref MemoryStream A_0, string A_1, ref byte[] A_2, ref byte[] A_3, ref byte[] A_4)
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
		this.ᜈ = A_2;
		this.ᜃ(A_1);
		this.ᜀ();
		Buffer.BlockCopy(this.ᜈ, 0, this.ᜉ, 0, 16);
		this.ᜉ[16] = 128;
		Array.Clear(this.ᜉ, 17, 47);
		this.ᜉ[56] = 128;
		spr\u180F spr_u180F = new spr\u180F();
		spr_u180F.ᜀ(this.ᜉ, 64U);
		spr_u180F.ᜅ();
		Buffer.BlockCopy(spr_u180F.ᜂ(), 0, this.ᜊ, 0, 16);
		this.ᜁ(0U);
		this.ᜀ(this.ᜉ, 16);
		this.ᜀ(this.ᜊ, 16);
		A_0 = this.ᜀ(A_0);
		A_2 = this.ᜈ;
		A_3 = this.ᜉ;
		A_4 = this.ᜊ;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x00283DEC File Offset: 0x00282DEC
	public void ᜀ(ref MemoryStream A_0, string A_1, ref byte[] A_2, ref byte[] A_3, ref byte[] A_4)
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
		Buffer.BlockCopy(A_2, 0, this.ᜈ, 0, 16);
		Buffer.BlockCopy(A_3, 0, this.ᜉ, 0, 16);
		Buffer.BlockCopy(A_4, 0, this.ᜊ, 0, 16);
		this.ᜃ(A_1);
		this.ᜁ();
		A_0 = this.ᜀ(A_0);
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x00283E74 File Offset: 0x00282E74
	internal bool ᜅ(string A_0)
	{
		int a_ = 19;
		if (this.ᜎ == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_55;
			}
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("⵸᩺ὼ፾ꎂ횄ﮈ놐朗랖膠톢삤솦첨\ud9aa좬솮튰횲톴馶", a_));
		}
		IL_55:
		this.ᜎ.Position = 4L;
		this.ᜎ.Read(this.ᜈ, 0, 16);
		this.ᜎ.Read(this.ᜉ, 0, 16);
		this.ᜎ.Read(this.ᜊ, 0, 16);
		this.ᜃ(A_0);
		return this.ᜁ();
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x00283F30 File Offset: 0x00282F30
	internal void ᜆ()
	{
		for (;;)
		{
			this.ᜎ = this.ᜀ(this.ᜎ);
			this.ᜐ = this.ᜀ(this.ᜐ);
			if (true)
			{
			}
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
						goto IL_50;
					}
					if (false)
					{
					}
					this.ᜏ = this.ᜀ(this.ᜏ);
					num = 2;
					continue;
				case 1:
					if (this.ᜏ != null)
					{
						goto IL_50;
					}
					goto IL_9F;
				case 2:
					goto IL_9D;
				}
				break;
				IL_50:
				num = 0;
			}
		}
		IL_9D:
		IL_9F:
		this.ᜃ();
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x00283FE4 File Offset: 0x00282FE4
	internal void ᜄ(string A_0)
	{
		for (;;)
		{
			this.ᜈ = Guid.NewGuid().ToByteArray();
			this.ᜃ(A_0);
			this.ᜀ();
			Buffer.BlockCopy(this.ᜈ, 0, this.ᜉ, 0, 16);
			this.ᜉ[16] = 128;
			Array.Clear(this.ᜉ, 17, 47);
			this.ᜉ[56] = 128;
			spr\u180F spr_u180F = new spr\u180F();
			spr_u180F.ᜀ(this.ᜉ, 64U);
			spr_u180F.ᜅ();
			Buffer.BlockCopy(spr_u180F.ᜂ(), 0, this.ᜊ, 0, 16);
			this.ᜁ(0U);
			this.ᜀ(this.ᜉ, 16);
			this.ᜀ(this.ᜊ, 16);
			this.ᜎ = this.ᜀ(this.ᜎ);
			this.ᜎ.Position = 0L;
			this.ᜎ.WriteByte(1);
			this.ᜎ.WriteByte(0);
			this.ᜎ.WriteByte(1);
			this.ᜎ.WriteByte(0);
			this.ᜎ.Write(this.ᜈ, 0, 16);
			this.ᜎ.Write(this.ᜉ, 0, 16);
			this.ᜎ.Write(this.ᜊ, 0, 16);
			this.ᜐ = this.ᜀ(this.ᜐ);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜏ != null)
					{
						goto IL_176;
					}
					goto IL_1D0;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_176;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜏ = this.ᜀ(this.ᜏ);
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_1CE;
				}
				break;
				IL_176:
				num = 1;
			}
		}
		IL_1CE:
		IL_1D0:
		this.ᜂ();
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x002841C8 File Offset: 0x002831C8
	private void ᜃ()
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
		this.ᜐ.Position = 0L;
		this.\u170D.ᜂ(this.ᜐ);
		this.\u170D.\u170D(false);
		this.\u170D.ᜋ(this.ᜑ);
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x00284240 File Offset: 0x00283240
	private void ᜂ()
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
		this.ᜐ.Position = 0L;
		this.\u170D.ᜀ(this.ᜐ);
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x0028429C File Offset: 0x0028329C
	private MemoryStream ᜀ(MemoryStream A_0)
	{
		byte[] array;
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_118:
			array[num] = 1;
			num++;
			num2 = 0;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_63;
			}
			break;
		}
		int num3;
		long length;
		MemoryStream memoryStream;
		uint num5;
		for (;;)
		{
			IL_34:
			switch (num2)
			{
			case 0:
				goto IL_B1;
			case 1:
				goto IL_EF;
			case 2:
			{
				if ((long)num3 >= length)
				{
					num2 = 9;
					continue;
				}
				int num4 = A_0.Read(array, 0, 16);
				num = num4;
				num2 = 5;
				continue;
			}
			case 3:
				if (num >= 16)
				{
					num2 = 7;
					continue;
				}
				goto IL_118;
			case 4:
				goto IL_EF;
			case 5:
				goto IL_B1;
			case 6:
				if (num3 % 512 == 0)
				{
					num2 = 8;
					continue;
				}
				goto IL_EF;
			case 7:
				this.ᜀ(array, 16);
				memoryStream.Write(array, 0, 16);
				num3 += 16;
				num2 = 6;
				continue;
			case 8:
				num5 += 1U;
				this.ᜁ(num5);
				num2 = 4;
				continue;
			case 9:
				goto IL_116;
			}
			goto IL_63;
			IL_B1:
			num2 = 3;
			continue;
			IL_EF:
			num2 = 2;
		}
		IL_116:
		memoryStream.Position = 0L;
		return memoryStream;
		IL_63:
		array = new byte[16];
		memoryStream = new MemoryStream();
		length = A_0.Length;
		num3 = 0;
		A_0.Position = (long)num3;
		num5 = 0U;
		this.ᜁ(num5);
		num2 = 1;
		goto IL_34;
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x00284424 File Offset: 0x00283424
	private void ᜀ(ref byte A_0, ref byte A_1)
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
		byte b = A_0;
		A_0 = A_1;
		A_1 = b;
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x0028446C File Offset: 0x0028346C
	private void ᜃ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num < A_0.Length)
					{
						this.ᜋ[2 * num] = (byte)(A_0[num] & 'ÿ');
						this.ᜋ[2 * num + 1] = (byte)(A_0[num] >> 8 & 'ÿ');
						num++;
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2A;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				case 1:
					goto IL_2A;
				case 2:
					goto IL_6B;
				case 3:
					goto IL_2C;
				}
				break;
				IL_2C:
				num2 = 0;
				continue;
				IL_2A:
				goto IL_2C;
			}
		}
		IL_6B:
		this.ᜋ[2 * num] = 128;
		this.ᜋ[56] = (byte)(num << 4);
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x0028454C File Offset: 0x0028354C
	private void ᜀ(byte[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.\u1712 = new spr\u222B();
				byte b = 0;
				byte b2 = 0;
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_F6;
					case 1:
					{
						int num3 = 0;
						num2 = 5;
						continue;
					}
					case 2:
						goto IL_11B;
					case 3:
						if (num >= 256)
						{
							num2 = 1;
							continue;
						}
						this.\u1712.ᜁ()[num] = (byte)num;
						num++;
						num2 = 6;
						continue;
					case 4:
					{
						int num3;
						if (num3 >= 256)
						{
							num2 = 7;
							continue;
						}
						b2 = (byte)((int)(A_0[(int)b] + this.\u1712.ᜁ()[num3] + b2) % 256);
						this.ᜀ(ref this.\u1712.ᜁ()[num3], ref this.\u1712.ᜁ()[(int)b2]);
						b = (b + 1) % 16;
						num3++;
						goto IL_AF;
					}
					case 5:
						goto IL_F6;
					case 6:
						goto IL_11B;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							goto IL_153;
						}
						break;
					}
					break;
					IL_AF:
					num2 = 0;
					continue;
					IL_F6:
					num2 = 4;
					continue;
					IL_11B:
					num2 = 3;
				}
			}
			IL_153:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x002846B4 File Offset: 0x002836B4
	private void ᜁ(uint A_0)
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
		spr\u180F spr_u180F = new spr\u180F();
		byte[] array = new byte[64];
		Buffer.BlockCopy(this.ᜌ.ᜂ(), 0, array, 0, 5);
		array[5] = (byte)(A_0 & 255U);
		array[6] = (byte)(A_0 >> 8 & 255U);
		array[7] = (byte)(A_0 >> 16 & 255U);
		array[8] = (byte)(A_0 >> 24 & 255U);
		array[9] = 128;
		array[56] = 72;
		spr_u180F.ᜀ(array, 64U);
		spr_u180F.ᜅ();
		this.ᜀ(spr_u180F.ᜂ());
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x00284770 File Offset: 0x00283770
	private bool ᜀ(byte[] A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
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
						if (A_0[num] != A_1[num])
						{
							num2 = 2;
							continue;
						}
						num++;
						break;
					}
					num2 = 4;
					continue;
				case 1:
					goto IL_84;
				case 2:
					return false;
				case 3:
					if (num >= A_2)
					{
						num2 = 5;
						continue;
					}
					num2 = 0;
					continue;
				case 4:
					goto IL_84;
				case 5:
					return true;
				}
				break;
				IL_84:
				num2 = 3;
			}
		}
		return false;
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x00284820 File Offset: 0x00283820
	private bool ᜁ()
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
		this.ᜀ();
		this.ᜁ(0U);
		this.ᜀ(this.ᜉ, 16);
		this.ᜀ(this.ᜊ, 16);
		this.ᜉ[16] = 128;
		Array.Clear(this.ᜉ, 17, 47);
		this.ᜉ[56] = 128;
		spr\u180F spr_u180F = new spr\u180F();
		spr_u180F.ᜀ(this.ᜉ, 64U);
		spr_u180F.ᜅ();
		return this.ᜀ(spr_u180F.ᜂ(), this.ᜊ, 16);
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x002848E0 File Offset: 0x002838E0
	private void ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_13B:
			num = 8;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_5B;
			}
			break;
		}
		int num2;
		int srcOffset;
		int num3;
		spr\u180F spr_u180F;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 == 64)
				{
					num = 1;
					continue;
				}
				srcOffset = 0;
				num3 = 5;
				Buffer.BlockCopy(this.ᜈ, 0, this.ᜋ, num2, 16);
				num2 += 16;
				if (true)
				{
				}
				num = 6;
				continue;
			case 1:
				this.ᜌ.ᜀ(this.ᜋ, 64U);
				srcOffset = num3;
				num3 = 5 - num3;
				num2 = 0;
				num = 4;
				continue;
			case 2:
				goto IL_120;
			case 3:
				goto IL_C8;
			case 4:
				goto IL_120;
			case 5:
				if (num2 == 16)
				{
					goto IL_13B;
				}
				num = 7;
				continue;
			case 6:
				goto IL_120;
			case 7:
				if (64 - num2 < 5)
				{
					num = 9;
					continue;
				}
				goto IL_C8;
			case 8:
				goto IL_147;
			case 9:
				num3 = 64 - num2;
				num = 3;
				continue;
			}
			goto IL_5B;
			IL_C8:
			Buffer.BlockCopy(spr_u180F.ᜂ(), srcOffset, this.ᜋ, num2, num3);
			num2 += num3;
			num = 0;
			continue;
			IL_120:
			num = 5;
		}
		IL_147:
		this.ᜋ[16] = 128;
		Array.Clear(this.ᜋ, 17, 47);
		this.ᜋ[56] = 128;
		this.ᜋ[57] = 10;
		this.ᜌ.ᜀ(this.ᜋ, 64U);
		this.ᜌ.ᜅ();
		return;
		IL_5B:
		spr_u180F = new spr\u180F();
		spr_u180F.ᜀ(this.ᜋ, 64U);
		spr_u180F.ᜅ();
		this.ᜌ = new spr\u180F();
		num2 = 0;
		srcOffset = 0;
		num3 = 5;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x00284ACC File Offset: 0x00283ACC
	private void ᜀ(byte[] A_0, int A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_3E:
			goto IL_43;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num;
		int num2;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
			{
				if (num2 >= A_1)
				{
					num = 2;
					continue;
				}
				this.\u1712.ᜁ((byte)((int)(this.\u1712.ᜀ() + 1) % 256));
				this.\u1712.ᜀ((byte)((int)(this.\u1712.ᜁ()[(int)this.\u1712.ᜀ()] + this.\u1712.ᜂ()) % 256));
				this.ᜀ(ref this.\u1712.ᜁ()[(int)this.\u1712.ᜀ()], ref this.\u1712.ᜁ()[(int)this.\u1712.ᜂ()]);
				byte b = (byte)((int)(this.\u1712.ᜁ()[(int)this.\u1712.ᜀ()] + this.\u1712.ᜁ()[(int)this.\u1712.ᜂ()]) % 256);
				int num3 = num2;
				A_0[num3] ^= this.\u1712.ᜁ()[(int)b];
				num2++;
				num = 3;
				continue;
			}
			case 1:
				goto IL_3E;
			case 2:
				goto IL_5A;
			case 3:
				goto IL_156;
			}
			goto IL_34;
		}
		IL_5A:
		if (true)
		{
		}
		return;
		IL_156:
		goto IL_43;
		IL_34:
		num2 = 0;
		num = 1;
		goto IL_1E;
		IL_43:
		num = 0;
		goto IL_1E;
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x00284C48 File Offset: 0x00283C48
	internal static uint ᜂ(string A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_5B;
				case 1:
					if (A_0.Length > 15)
					{
						num = 2;
						continue;
					}
					goto IL_97;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						A_0 = A_0.Substring(0, 15);
						num = 0;
						continue;
					}
					break;
				case 4:
					return 0U;
				}
				if (string.IsNullOrEmpty(A_0))
				{
					num = 4;
				}
				else
				{
					num = 1;
				}
			}
		}
		return 0U;
		IL_5B:
		IL_97:
		ushort num2 = sprᥬ.ᜀ(A_0);
		ushort num3 = sprᥬ.ᜁ(A_0);
		uint num4 = (uint)num3;
		num4 <<= 16;
		return num4 | (uint)num2;
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x00284D08 File Offset: 0x00283D08
	private static uint ᜀ(uint A_0)
	{
		uint num;
		for (;;)
		{
			num = 0U;
			int num2 = 0;
			int num3 = 6;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_32;
				case 1:
					if (num2 >= 4)
					{
						num3 = 4;
						continue;
					}
					num |= (A_0 & 255U);
					num3 = 2;
					continue;
				case 2:
					if (num2 < 3)
					{
						num3 = 3;
						continue;
					}
					goto IL_32;
				case 3:
					goto IL_71;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_71;
					}
					goto Block_3;
				case 5:
					goto IL_7B;
				case 6:
					goto IL_7B;
				}
				break;
				IL_32:
				num2++;
				num3 = 5;
				continue;
				IL_71:
				if (true)
				{
				}
				num <<= 8;
				A_0 >>= 8;
				num3 = 0;
				continue;
				IL_7B:
				num3 = 1;
			}
		}
		Block_3:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x00284DD4 File Offset: 0x00283DD4
	private static ushort ᜁ(string A_0)
	{
		switch (0)
		{
		default:
		{
			ushort num;
			for (;;)
			{
				num = sprᥬ.ᜆ[A_0.Length - 1];
				int num2 = 15 - A_0.Length;
				int num3 = 0;
				int length = A_0.Length;
				int num4 = 10;
				for (;;)
				{
					int num5;
					switch (num4)
					{
					case 0:
						goto IL_71;
					case 1:
						goto IL_71;
					case 2:
						goto IL_8A;
					case 3:
						goto IL_BF;
					case 4:
					{
						bool[] array;
						if (array[num5])
						{
							num4 = 6;
							continue;
						}
						goto IL_8A;
					}
					case 5:
						goto IL_7A;
					case 6:
						num ^= sprᥬ.ᜇ[num5, num2 + num3];
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7A;
						default:
							if (false)
							{
							}
							num4 = 2;
							continue;
						}
						break;
					case 7:
					{
						if (num3 >= length)
						{
							num4 = 8;
							continue;
						}
						char a_ = A_0[num3];
						bool[] array = sprᥬ.ᜁ(a_);
						num5 = 0;
						num4 = 0;
						continue;
					}
					case 8:
						return num;
					case 9:
						num3++;
						num4 = 3;
						continue;
					case 10:
						goto IL_BF;
					}
					break;
					IL_71:
					num4 = 5;
					continue;
					IL_7A:
					if (num5 >= 7)
					{
						num4 = 9;
						continue;
					}
					num4 = 4;
					continue;
					IL_8A:
					num5++;
					num4 = 1;
					continue;
					IL_BF:
					num4 = 7;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x00284F4C File Offset: 0x00283F4C
	private static ushort ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 3;
			ushort num4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C6;
				case 1:
					goto IL_E2;
				case 2:
				{
					int num2;
					int length;
					if (num2 >= length)
					{
						num = 1;
						continue;
					}
					bool[] a_ = sprᥬ.ᜀ(A_0[num2]);
					a_ = sprᥬ.ᜀ(a_, num2 + 1);
					ushort num3 = sprᥬ.ᜀ(a_);
					num4 ^= num3;
					num2++;
					num = 4;
					continue;
				}
				case 4:
					goto IL_C6;
				case 5:
					return 0;
				}
				if (A_0 != null)
				{
					num4 = 0;
					int num2 = 0;
					int length = A_0.Length;
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return 0;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_C6:
				num = 2;
			}
			return 0;
			IL_E2:
			return (ushort)((long)((int)num4 ^ A_0.Length) ^ 52811L);
		}
		}
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x00285050 File Offset: 0x00284050
	private static bool[] ᜁ(char A_0)
	{
		switch (0)
		{
		default:
		{
			bool[] array;
			for (;;)
			{
				ushort num = 1;
				array = new bool[7];
				ushort num2 = Convert.ToUInt16(A_0);
				int num3 = 10;
				for (;;)
				{
					bool flag;
					bool flag2;
					int num4;
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							goto IL_DD;
						}
						break;
					case 1:
						goto IL_A0;
					case 2:
						flag = false;
						goto IL_11F;
					case 3:
						goto IL_A0;
					case 4:
						if (flag2)
						{
							num3 = 6;
							continue;
						}
						goto IL_70;
					case 5:
						flag = true;
						goto IL_11F;
					case 6:
						num2 = (ushort)(num2 >> 8);
						num3 = 8;
						continue;
					case 7:
						if (num4 >= 7)
						{
							num3 = 0;
							continue;
						}
						array[num4] = ((num2 & num) == num);
						num = (ushort)(num << 1);
						num4++;
						if (true)
						{
						}
						num3 = 3;
						continue;
					case 8:
						goto IL_70;
					case 9:
						goto IL_6E;
					case 10:
						if ((num2 & 255) != 0)
						{
							num3 = 9;
							continue;
						}
						num3 = 5;
						continue;
					}
					break;
					IL_6E:
					num3 = 2;
					continue;
					IL_70:
					num4 = 0;
					num3 = 1;
					continue;
					IL_A0:
					num3 = 7;
					continue;
					IL_11F:
					flag2 = flag;
					num3 = 4;
				}
			}
			IL_DD:
			if (false)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x002851A4 File Offset: 0x002841A4
	private static bool[] ᜀ(char A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9F:
			goto IL_6B;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num;
		int num2;
		bool[] array;
		ushort num3;
		ushort num4;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 >= 15)
				{
					num = 2;
					continue;
				}
				array[num2] = ((num3 & num4) == num4);
				num4 = (ushort)(num4 << 1);
				num2++;
				num = 3;
				continue;
			case 1:
				goto IL_69;
			case 2:
				goto IL_82;
			case 3:
				goto IL_9F;
			}
			goto IL_43;
		}
		IL_69:
		goto IL_6B;
		IL_82:
		if (true)
		{
		}
		return array;
		IL_43:
		array = new bool[15];
		num3 = Convert.ToUInt16(A_0);
		num4 = 1;
		num2 = 0;
		num = 1;
		goto IL_2C;
		IL_6B:
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x0028525C File Offset: 0x0028425C
	private static ushort ᜀ(bool[] A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				ushort num2;
				int num3;
				ushort num5;
				switch (num)
				{
				case 0:
					return num2;
				case 1:
				{
					int num4;
					if (num3 >= num4)
					{
						num = 0;
						continue;
					}
					num = 5;
					continue;
				}
				case 3:
					goto IL_63;
				case 4:
					goto IL_AD;
				case 5:
					if (A_0[num3])
					{
						num = 3;
						continue;
					}
					goto IL_E2;
				case 6:
					goto IL_A8;
				case 7:
				{
					if (A_0.Length > 16)
					{
						num = 6;
						continue;
					}
					num2 = 0;
					num5 = 1;
					num3 = 0;
					int num4 = A_0.Length;
					num = 10;
					continue;
				}
				case 8:
					goto IL_61;
				case 9:
					goto IL_E2;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_AD;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
				IL_63:
				num2 += num5;
				num = 9;
				continue;
				IL_AD:
				num = 1;
				continue;
				IL_E2:
				num5 = (ushort)(num5 << 1);
				num3++;
				num = 4;
			}
			IL_61:
			throw new ArgumentNullException(ClipboardData.b("ᕶၸེ๼", a_));
			IL_A8:
			throw new ArgumentOutOfRangeException(ClipboardData.b("⍶ᅸṺོ᩾ꆀ꺈ﾊ권뎒ﺚ붜즠슢쮤螦風鶪趬춮\ud8b0잲운", a_));
		}
		}
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x002853C0 File Offset: 0x002843C0
	private static bool[] ᜀ(bool[] A_0, int A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 0;
			bool[] array;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_5D;
				case 2:
					goto IL_14F;
				case 3:
				{
					if (A_1 < 0)
					{
						num = 2;
						continue;
					}
					array = new bool[A_0.Length];
					int num2 = 0;
					int num3 = A_0.Length;
					if (true)
					{
					}
					num = 4;
					continue;
				}
				case 4:
					goto IL_B7;
				case 5:
					if (A_0.Length == 0)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 6:
					return A_0;
				case 7:
					goto IL_B7;
				case 8:
					goto IL_D3;
				case 9:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 8;
						continue;
					}
					int num4 = (num2 + A_1) % num3;
					array[num4] = A_0[num2];
					num2++;
					num = 7;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
				IL_B7:
				num = 9;
			}
			IL_5D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return array;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("፰ᩲŴѶ", a_));
			}
			return A_0;
			IL_D3:
			return array;
			IL_14F:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ተᱲt᥶൸孺ṼṾꒂꞆ권랖ﲜ膠\ud9a2삤햦욨", a_));
		}
		}
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x00285524 File Offset: 0x00284524
	public static int ᜀ(int A_0, int A_1)
	{
		int a_ = 5;
		if (A_1 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = A_0 % A_1;
				return A_0 - num + A_1;
			}
			}
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཪ࡬࡮Ͱᙲၴ坶᩸᩺፼塾ꎂꦈ뮊", a_));
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x0028558C File Offset: 0x0028458C
	// Note: this type is marked as 'beforefieldinit'.
	static sprᥬ()
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
		sprᥬ.ᜆ = new ushort[]
		{
			57840,
			7439,
			52380,
			33984,
			4364,
			3600,
			61902,
			12606,
			6258,
			57657,
			54287,
			34041,
			10252,
			43370,
			20163
		};
		sprᥬ.ᜇ = new ushort[,]
		{
			{
				44796,
				31585,
				17763,
				885,
				55369,
				28485,
				60195,
				18387,
				47201,
				17824,
				43601,
				30388,
				14128,
				13105,
				4129
			},
			{
				19929,
				63170,
				35526,
				1770,
				41139,
				56970,
				50791,
				36774,
				24803,
				35648,
				17539,
				60776,
				28256,
				26210,
				8258
			},
			{
				39858,
				64933,
				1453,
				3540,
				20807,
				44341,
				40175,
				3949,
				49606,
				1697,
				35078,
				51953,
				56512,
				52420,
				16516
			},
			{
				10053,
				60267,
				2906,
				7080,
				41614,
				19019,
				10751,
				7898,
				37805,
				3394,
				557,
				34243,
				43425,
				35241,
				33032
			},
			{
				20106,
				50935,
				5812,
				14160,
				21821,
				38038,
				21502,
				15796,
				14203,
				6788,
				1114,
				7079,
				17251,
				883,
				4657
			},
			{
				40212,
				40399,
				11624,
				28320,
				43642,
				14605,
				43004,
				31592,
				28406,
				13576,
				2228,
				14158,
				34502,
				1766,
				9314
			},
			{
				10761,
				11199,
				23248,
				56640,
				17621,
				29210,
				24537,
				63184,
				56812,
				27152,
				4456,
				28316,
				7597,
				3532,
				18628
			}
		};
	}

	// Token: 0x04002329 RID: 9001
	private const int ᜀ = 16;

	// Token: 0x0400232A RID: 9002
	private const int ᜁ = 64;

	// Token: 0x0400232B RID: 9003
	private const int ᜂ = 512;

	// Token: 0x0400232C RID: 9004
	private const int ᜃ = 0;

	// Token: 0x0400232D RID: 9005
	private const int ᜄ = 256;

	// Token: 0x0400232E RID: 9006
	private const uint ᜅ = 52811U;

	// Token: 0x0400232F RID: 9007
	private static readonly ushort[] ᜆ;

	// Token: 0x04002330 RID: 9008
	private static readonly ushort[,] ᜇ;

	// Token: 0x04002331 RID: 9009
	private byte[] ᜈ = new byte[16];

	// Token: 0x04002332 RID: 9010
	private byte[] ᜉ = new byte[64];

	// Token: 0x04002333 RID: 9011
	private byte[] ᜊ = new byte[16];

	// Token: 0x04002334 RID: 9012
	private byte[] ᜋ = new byte[64];

	// Token: 0x04002335 RID: 9013
	private spr\u180F ᜌ = new spr\u180F();

	// Token: 0x04002336 RID: 9014
	private sprᾱ \u170D;

	// Token: 0x04002337 RID: 9015
	private MemoryStream ᜎ;

	// Token: 0x04002338 RID: 9016
	private MemoryStream ᜏ;

	// Token: 0x04002339 RID: 9017
	private MemoryStream ᜐ;

	// Token: 0x0400233A RID: 9018
	private bool ᜑ;

	// Token: 0x0400233B RID: 9019
	private spr\u222B \u1712;
}
