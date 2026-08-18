using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.Biff_Records;

// Token: 0x02000364 RID: 868
[CLSCompliant(false)]
internal class spr\u1CC1 : spr\u23F8
{
	// Token: 0x06002EA2 RID: 11938 RVA: 0x002C2E30 File Offset: 0x002C1E30
	internal int ᜄ()
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
		return spr\u23F8.ᜁ((int)this.ᜈ, 511, 0);
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x002C2E7C File Offset: 0x002C1E7C
	internal void ᜀ(int A_0)
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
		this.ᜈ = (ushort)((byte)spr\u23F8.ᜀ((int)this.ᜈ, 511, A_0 << 31));
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x002C2ED4 File Offset: 0x002C1ED4
	internal bool ᜏ()
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
		return spr\u23F8.ᜀ((int)this.ᜈ, 9);
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x002C2F1C File Offset: 0x002C1F1C
	internal void ᜀ(bool A_0)
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
		this.ᜈ = (ushort)((byte)spr\u23F8.ᜀ((int)this.ᜈ, 9, A_0));
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x002C2F6C File Offset: 0x002C1F6C
	internal WordSprmType \u1715()
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
		return (WordSprmType)spr\u23F8.ᜁ((int)this.ᜈ, 7168, 10);
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x002C2FBC File Offset: 0x002C1FBC
	internal void ᜀ(WordSprmType A_0)
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
		this.ᜈ = (ushort)(spr\u23F8.ᜀ((int)this.ᜈ, 7168, (int)((int)A_0 << 10)) & 65535);
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x002C3018 File Offset: 0x002C2018
	internal WordSprmOperandSize \u1713()
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
		return (WordSprmOperandSize)spr\u23F8.ᜁ((int)this.ᜈ, 57344, 13);
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x002C3068 File Offset: 0x002C2068
	internal void ᜁ(WordSprmOperandSize A_0)
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
		this.ᜈ = (ushort)(spr\u23F8.ᜀ((int)this.ᜈ, 57344, (int)((int)A_0 << 13)) & 65535);
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x002C30C4 File Offset: 0x002C20C4
	internal int ᜆ()
	{
		if (this.ᜊ != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜊ.Length;
			}
		}
		if (true)
		{
		}
		return 0;
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x002C3114 File Offset: 0x002C2114
	internal byte[] ᜎ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_76;
				default:
					if (false)
					{
					}
					this.ᜊ = new byte[this.ᜉ];
					num = 4;
					continue;
				}
				break;
			case 3:
				goto IL_76;
			case 4:
				goto IL_6C;
			}
			if (this.ᜉ > 0)
			{
				num = 0;
				continue;
			}
			break;
			IL_76:
			if (true)
			{
			}
			if (this.ᜊ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6C:
		return this.ᜊ;
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x002C31C4 File Offset: 0x002C21C4
	internal void ᜀ(byte[] A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x002C3208 File Offset: 0x002C2208
	internal bool ᜉ()
	{
		if (this.ᜆ() <= 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return false;
			}
		}
		if (true)
		{
		}
		return this.ᜊ[0] != 0;
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x002C3260 File Offset: 0x002C2260
	internal void ᜁ(bool A_0)
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
		this.ᜊ = new byte[spr\u1CC1.ᜀ(this.\u1713())];
		this.ᜊ[0] = (A_0 ? 1 : 0);
	}

	// Token: 0x06002EAF RID: 11951 RVA: 0x002C32C8 File Offset: 0x002C22C8
	internal byte \u1714()
	{
		if (this.ᜆ() <= 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return 0;
			}
		}
		if (true)
		{
		}
		return this.ᜎ()[0];
	}

	// Token: 0x06002EB0 RID: 11952 RVA: 0x002C3318 File Offset: 0x002C2318
	internal void ᜀ(byte A_0)
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					this.ᜀ(new byte[]
					{
						A_0
					});
					num = 2;
					continue;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (this.\u1714() == A_0)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x06002EB1 RID: 11953 RVA: 0x002C33A0 File Offset: 0x002C23A0
	internal ushort \u1716()
	{
		if (true)
		{
		}
		if (this.ᜆ() != 2)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return 0;
			}
		}
		return BitConverter.ToUInt16(this.ᜎ(), 0);
	}

	// Token: 0x06002EB2 RID: 11954 RVA: 0x002C33F4 File Offset: 0x002C23F4
	internal void ᜁ(ushort A_0)
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.ᜀ(BitConverter.GetBytes(A_0));
					if (true)
					{
					}
					num = 1;
					continue;
				}
				if (this.\u1716() == A_0)
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x06002EB3 RID: 11955 RVA: 0x002C3474 File Offset: 0x002C2474
	internal short ᜐ()
	{
		if (this.ᜆ() != 2)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return 0;
			}
		}
		if (true)
		{
		}
		return BitConverter.ToInt16(this.ᜎ(), 0);
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x002C34C8 File Offset: 0x002C24C8
	internal void ᜀ(short A_0)
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6B;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_69;
				case 2:
					this.ᜀ(BitConverter.GetBytes(A_0));
					num = 1;
					continue;
				}
				if (this.ᜐ() == A_0)
				{
					goto IL_6B;
				}
				num = 2;
				break;
			}
		}
		IL_69:
		IL_6B:
		if (true)
		{
		}
	}

	// Token: 0x06002EB5 RID: 11957 RVA: 0x002C3548 File Offset: 0x002C2548
	internal int \u1712()
	{
		if (this.ᜆ() != 4)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return 0;
			}
		}
		if (true)
		{
		}
		return BitConverter.ToInt32(this.ᜎ(), 0);
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x002C359C File Offset: 0x002C259C
	internal void ᜁ(int A_0)
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜀ(BitConverter.GetBytes(A_0));
					num = 2;
					continue;
				case 2:
					return;
				}
				if (this.\u1712() == A_0)
				{
					return;
				}
				if (true)
				{
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x002C361C File Offset: 0x002C261C
	internal uint ᜋ()
	{
		if (this.ᜆ() != 4)
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
				return 0U;
			}
		}
		return BitConverter.ToUInt32(this.ᜎ(), 0);
	}

	// Token: 0x06002EB8 RID: 11960 RVA: 0x002C3670 File Offset: 0x002C2670
	internal void ᜀ(uint A_0)
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					this.ᜀ(BitConverter.GetBytes(A_0));
					num = 2;
					continue;
				case 2:
					return;
				}
				if (this.ᜋ() == A_0)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x06002EB9 RID: 11961 RVA: 0x002C36F0 File Offset: 0x002C26F0
	internal byte[] ᜅ()
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
		return this.ᜎ();
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x002C3734 File Offset: 0x002C2734
	internal void ᜁ(byte[] A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06002EBB RID: 11963 RVA: 0x002C3778 File Offset: 0x002C2778
	internal int ᜈ()
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
		return (int)this.ᜈ;
	}

	// Token: 0x06002EBC RID: 11964 RVA: 0x002C37BC File Offset: 0x002C27BC
	internal void ᜂ(int A_0)
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
		this.ᜈ = (ushort)A_0;
	}

	// Token: 0x06002EBD RID: 11965 RVA: 0x002C3800 File Offset: 0x002C2800
	internal ushort ᜂ()
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

	// Token: 0x06002EBE RID: 11966 RVA: 0x002C3844 File Offset: 0x002C2844
	internal void ᜀ(ushort A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x002C3888 File Offset: 0x002C2888
	internal override int ᜇ()
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜋ = this.ᜀ();
				num = 1;
				continue;
			case 1:
				goto IL_51;
			case 2:
				IL_08:
				break;
			}
			if (this.ᜋ == 32767)
			{
				num = 0;
				continue;
			}
			IL_51:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_67;
			}
		}
		IL_67:
		if (false)
		{
		}
		return (int)this.ᜋ;
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x002C3914 File Offset: 0x002C2914
	internal WordSprmOptionType ᜌ()
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
		return (WordSprmOptionType)this.ᜈ;
	}

	// Token: 0x06002EC1 RID: 11969 RVA: 0x002C3958 File Offset: 0x002C2958
	internal spr\u1CC1()
	{
	}

	// Token: 0x06002EC2 RID: 11970 RVA: 0x002C3978 File Offset: 0x002C2978
	internal spr\u1CC1(int A_0)
	{
		this.ᜈ = (ushort)A_0;
		this.ᜉ = spr\u1CC1.ᜀ(this.\u1713());
	}

	// Token: 0x06002EC3 RID: 11971 RVA: 0x002C39B0 File Offset: 0x002C29B0
	internal spr\u1CC1(Stream A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x002C39D8 File Offset: 0x002C29D8
	internal new int ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 10;
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 9;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.\u1713() == WordSprmOperandSize.Variable)
					{
						num2 = 5;
						continue;
					}
					num = spr\u1CC1.ᜀ(this.\u1713());
					num2 = 2;
					continue;
				case 1:
					goto IL_5B;
				case 2:
					goto IL_10C;
				case 3:
					goto IL_B2;
				case 4:
					goto IL_10C;
				case 5:
					num2 = 10;
					continue;
				case 6:
					goto IL_10C;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						num = (int)(BitConverter.ToUInt16(A_0, A_1) - 1);
						A_1 += 2;
						num2 = 4;
						continue;
					}
					break;
				case 8:
					goto IL_123;
				case 9:
					if (A_1 + 2 > A_0.Length)
					{
						num2 = 1;
						continue;
					}
					this.ᜈ = BitConverter.ToUInt16(A_0, A_1);
					A_1 += 2;
					num2 = 3;
					continue;
				case 10:
					if (true)
					{
					}
					if (this.ᜈ == 54792)
					{
						num2 = 7;
						continue;
					}
					num = (int)A_0[A_1];
					A_1++;
					num2 = 6;
					continue;
				case 11:
					goto IL_C8;
				}
				break;
				IL_B2:
				if (A_1 + 1 > A_0.Length)
				{
					num2 = 11;
					continue;
				}
				num2 = 0;
				continue;
				IL_10C:
				this.ᜊ = new byte[num];
				num2 = 8;
			}
		}
		IL_5B:
		goto IL_F8;
		IL_C8:
		return A_1 + 1;
		IL_F8:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᥯㵱ታၵ୷όࡻ幽ꒃ겋뚗", a_));
		IL_123:
		try
		{
			Array.Copy(A_0, A_1, this.ᜊ, 0, this.ᜊ.Length);
			goto IL_1B2;
		}
		catch
		{
			goto IL_1B2;
		}
		goto IL_F8;
		IL_1B2:
		A_1 += num;
		return A_1;
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x002C3BB0 File Offset: 0x002C2BB0
	internal void ᜀ(Stream A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			byte[] array = new byte[2];
			A_0.Read(array, 0, 2);
			this.ᜈ = BitConverter.ToUInt16(array, 0);
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜈ == 54792)
					{
						num2 = 3;
						continue;
					}
					goto IL_71;
				case 1:
					num2 = 0;
					continue;
				case 2:
					goto IL_E1;
				case 3:
					goto IL_82;
				case 4:
					goto IL_E1;
				case 5:
					if (this.\u1713() == WordSprmOperandSize.Variable)
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					num = spr\u1CC1.ᜀ(this.\u1713());
					num2 = 2;
					continue;
				case 6:
					goto IL_71;
				}
				break;
				IL_71:
				num = A_0.ReadByte();
				num2 = 4;
				continue;
				IL_82:
				A_0.Read(array, 0, 2);
				num = (int)(BitConverter.ToUInt16(array, 0) - 1);
				num2 = 6;
				continue;
				IL_E1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_82;
				default:
					goto IL_F7;
				}
			}
		}
		IL_F7:
		if (false)
		{
		}
		this.ᜊ = new byte[num];
		A_0.Read(this.ᜊ, 0, num);
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x002C3CD8 File Offset: 0x002C2CD8
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 8;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 1:
					goto IL_10D;
				case 2:
					goto IL_E2;
				case 3:
					if (A_1 + this.ᜇ() > A_0.Length)
					{
						num = 5;
						continue;
					}
					num2 = A_1;
					BitConverter.GetBytes(this.ᜈ).CopyTo(A_0, A_1);
					A_1 += 2;
					num = 6;
					continue;
				case 4:
					goto IL_E2;
				case 5:
					goto IL_1C9;
				case 6:
					if (this.\u1713() == WordSprmOperandSize.Variable)
					{
						num = 7;
						continue;
					}
					goto IL_E2;
				case 7:
					num = 10;
					continue;
				case 9:
				{
					ushort value = (ushort)(this.ᜊ.Length + 1);
					byte[] bytes = BitConverter.GetBytes(value);
					A_0[A_1++] = bytes[0];
					A_0[A_1++] = bytes[1];
					num = 2;
					continue;
				}
				case 10:
				{
					if (this.ᜈ == 54792)
					{
						num = 9;
						continue;
					}
					byte b = (byte)this.ᜊ.Length;
					A_0[A_1++] = b;
					num = 4;
					continue;
				}
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CA;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 12:
					if (this.ᜊ != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_20D;
				case 13:
					goto IL_77;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 0;
				continue;
				IL_CA:
				if (A_1 >= 0)
				{
					num = 11;
					continue;
				}
				goto IL_174;
				IL_E2:
				num = 12;
			}
			IL_77:
			throw new ArgumentNullException(ClipboardData.b("ᕳѵ੷㹹ᵻ੽", a_));
			IL_10D:
			this.ᜊ.CopyTo(A_0, A_1);
			return A_1 - num2 + this.ᜊ.Length;
			IL_174:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᵳ㥵ṷᱹཻ᭽", a_));
			IL_1C9:
			goto IL_174;
			IL_20D:
			return A_1 - num2;
		}
		}
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x002C3EF8 File Offset: 0x002C2EF8
	internal int ᜀ(BinaryWriter A_0, Stream A_1)
	{
		int a_ = 7;
		int num = 9;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ushort value = (ushort)(this.ᜊ.Length + 1);
				A_0.Write(value);
				num = 6;
				continue;
			}
			case 1:
				goto IL_111;
			case 2:
				num = 3;
				continue;
			case 3:
			{
				if (this.ᜈ == 54792)
				{
					num = 0;
					continue;
				}
				byte value2 = (byte)this.ᜊ.Length;
				A_0.Write(value2);
				num = 1;
				continue;
			}
			case 4:
				goto IL_FB;
			case 5:
				if (this.\u1713() == WordSprmOperandSize.Variable)
				{
					num = 2;
					continue;
				}
				goto IL_111;
			case 6:
				goto IL_111;
			case 7:
				goto IL_50;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					this.ᜊ = new byte[spr\u1CC1.ᜀ(this.\u1713())];
					num = 4;
					continue;
				}
				break;
			case 10:
				if (this.ᜊ == null)
				{
					num = 8;
					continue;
				}
				goto IL_178;
			}
			goto IL_45;
			IL_48:
			num = 7;
			continue;
			IL_45:
			if (A_0 == null)
			{
				goto IL_48;
			}
			num2 = (int)A_1.Position;
			A_0.Write(this.ᜈ);
			num = 5;
			continue;
			IL_111:
			num = 10;
		}
		IL_50:
		throw new ArgumentNullException(ClipboardData.b("Ṭ᭮Ͱᙲᑴ᩶", a_));
		IL_FB:
		IL_178:
		A_0.Write(this.ᜊ);
		return (int)(A_1.Position - (long)num2);
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x002C4094 File Offset: 0x002C3094
	internal spr\u1CC1 ᜊ()
	{
		int num = 2;
		for (;;)
		{
			spr\u1CC1 spr_u1CC;
			switch (num)
			{
			case 0:
				spr_u1CC.ᜀ(new byte[this.ᜆ()]);
				num = 3;
				continue;
			case 1:
				this.ᜎ().CopyTo(spr_u1CC.ᜎ(), 0);
				num = 6;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EC;
				default:
					if (false)
					{
					}
					goto IL_57;
				}
				break;
			case 4:
				goto IL_4B;
			case 5:
				goto IL_EC;
			case 6:
				return spr_u1CC;
			case 7:
				if (spr_u1CC.ᜎ() != null)
				{
					num = 1;
					continue;
				}
				return spr_u1CC;
			}
			if (this.ᜎ() == null)
			{
				if (true)
				{
				}
				num = 4;
				continue;
			}
			spr_u1CC = new spr\u1CC1();
			spr_u1CC.ᜂ(this.ᜈ());
			num = 5;
			continue;
			IL_57:
			num = 7;
			continue;
			IL_EC:
			if (this.ᜆ() <= 0)
			{
				goto IL_57;
			}
			num = 0;
		}
		IL_4B:
		return null;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x002C41AC File Offset: 0x002C31AC
	internal static int ᜀ(WordSprmOperandSize A_0)
	{
		int a_ = 10;
		for (;;)
		{
			IL_1D:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_89:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_94;
				case 1:
					goto IL_83;
				case 2:
					switch (A_0)
					{
					case WordSprmOperandSize.OneBit:
					case WordSprmOperandSize.OneByte:
						return 1;
					case WordSprmOperandSize.TwoBytes:
					case WordSprmOperandSize.TwoBytes2:
					case WordSprmOperandSize.TwoBytes3:
						return 2;
					case WordSprmOperandSize.FourBytes:
						return 4;
					case WordSprmOperandSize.Variable:
						return -1;
					case WordSprmOperandSize.ThreeBytes:
						return 3;
					default:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				goto IL_1D;
			}
			IL_83:
			goto IL_89;
		}
		return 1;
		IL_94:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ὧɱᅳѵ᥷ᑹ᡻⵽", a_));
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x002C4268 File Offset: 0x002C3268
	internal static void ᜀ(int A_0, out WordSprmType A_1, out WordSprmOperandSize A_2)
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
		A_1 = (WordSprmType)spr\u23F8.ᜁ(A_0, 7168, 10);
		int num = spr\u23F8.ᜁ(A_0, 57344, 13);
		A_2 = (WordSprmOperandSize)num;
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x002C42C4 File Offset: 0x002C32C4
	private void ᜁ()
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
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x002C4300 File Offset: 0x002C3300
	private short ᜀ()
	{
		if (true)
		{
		}
		int num;
		for (;;)
		{
			num = 2;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
						if (false)
						{
						}
						if (this.\u1713() == WordSprmOperandSize.Variable)
						{
							num2 = 3;
							continue;
						}
						goto IL_7D;
					}
					break;
				case 1:
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_B0;
				case 3:
					num++;
					num2 = 6;
					continue;
				case 4:
					if (this.ᜈ == 54792)
					{
						num2 = 1;
						continue;
					}
					goto IL_B0;
				case 5:
					goto IL_BB;
				case 6:
					goto IL_7D;
				}
				break;
				IL_7D:
				num2 = 4;
				continue;
				IL_B0:
				num2 = 5;
			}
		}
		IL_BB:
		return (short)(num + ((this.ᜎ() != null) ? this.ᜊ.Length : 0));
	}

	// Token: 0x040026D3 RID: 9939
	private new const int ᜀ = 511;

	// Token: 0x040026D4 RID: 9940
	private new const int ᜁ = 0;

	// Token: 0x040026D5 RID: 9941
	private new const int ᜂ = 9;

	// Token: 0x040026D6 RID: 9942
	private new const int ᜃ = 7168;

	// Token: 0x040026D7 RID: 9943
	private new const int ᜄ = 10;

	// Token: 0x040026D8 RID: 9944
	private new const int ᜅ = 57344;

	// Token: 0x040026D9 RID: 9945
	private const int ᜆ = 13;

	// Token: 0x040026DA RID: 9946
	private const int ᜇ = 65535;

	// Token: 0x040026DB RID: 9947
	private ushort ᜈ;

	// Token: 0x040026DC RID: 9948
	private int ᜉ;

	// Token: 0x040026DD RID: 9949
	private byte[] ᜊ;

	// Token: 0x040026DE RID: 9950
	private short ᜋ = short.MaxValue;
}
