using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020002DA RID: 730
internal class spr\u250C
{
	// Token: 0x060027BC RID: 10172 RVA: 0x0027D26C File Offset: 0x0027C26C
	public spr\u250C()
	{
		this.ᜅ = new byte[8];
		this.ᜆ = default(Guid);
		this.ᜇ = 62;
		this.ᜈ = 3;
		this.ᜉ = 65534;
		this.ᜊ = 9;
		this.ᜋ = 6;
		this.ᜐ = -1;
		this.\u1712 = 4096U;
		this.\u1713 = -2;
		this.\u1715 = -2;
		this.\u1717 = new int[109];
		base..ctor();
		Buffer.BlockCopy(spr\u250C.ᜄ, 0, this.ᜅ, 0, 8);
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x0027D304 File Offset: 0x0027C304
	public spr\u250C(Stream A_0)
	{
		int a_ = 14;
		this.ᜅ = new byte[8];
		this.ᜆ = default(Guid);
		this.ᜇ = 62;
		this.ᜈ = 3;
		this.ᜉ = 65534;
		this.ᜊ = 9;
		this.ᜋ = 6;
		this.ᜐ = -1;
		this.\u1712 = 4096U;
		this.\u1713 = -2;
		this.\u1715 = -2;
		this.\u1717 = new int[109];
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ݳɵ੷όᵻ፽", a_));
		}
		if (A_0.Length < 512L)
		{
			throw new sprᦹ();
		}
		byte[] array = new byte[512];
		A_0.Read(array, 0, 512);
		Buffer.BlockCopy(array, 0, this.ᜅ, 0, 8);
		this.ᜀ();
		int num = 8;
		byte[] array2 = new byte[16];
		Buffer.BlockCopy(array, num, array2, 0, 16);
		num += 16;
		this.ᜆ = new Guid(array2);
		this.ᜇ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜈ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜉ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜊ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜋ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜌ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.\u170D = BitConverter.ToUInt32(array, num);
		num += 4;
		this.ᜎ = BitConverter.ToUInt32(array, num);
		num += 4;
		this.ᜏ = BitConverter.ToInt32(array, num);
		num += 4;
		this.ᜐ = BitConverter.ToInt32(array, num);
		num += 4;
		this.ᜑ = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1712 = BitConverter.ToUInt32(array, num);
		num += 4;
		this.\u1713 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1714 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1715 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1716 = BitConverter.ToInt32(array, num);
		num += 4;
		Buffer.BlockCopy(array, num, this.\u1717, 0, this.\u1717.Length * 4);
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x0027D53C File Offset: 0x0027C53C
	public void ᜁ(Stream A_0)
	{
		int a_ = 16;
		if (A_0 == null)
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
				break;
			}
			throw new ArgumentNullException(ClipboardData.b("յ౷ࡹ᥻ώ", a_));
		}
		byte[] array = new byte[512];
		Buffer.BlockCopy(this.ᜅ, 0, array, 0, 8);
		int num = 8;
		byte[] src = this.ᜆ.ToByteArray();
		Buffer.BlockCopy(src, 0, array, num, 16);
		num += 16;
		this.ᜀ(array, num, this.ᜇ);
		num += 2;
		this.ᜀ(array, num, this.ᜈ);
		num += 2;
		this.ᜀ(array, num, this.ᜉ);
		num += 2;
		this.ᜀ(array, num, this.ᜊ);
		num += 2;
		this.ᜀ(array, num, this.ᜋ);
		num += 2;
		this.ᜀ(array, num, this.ᜌ);
		num += 2;
		this.ᜀ(array, num, this.\u170D);
		num += 4;
		this.ᜀ(array, num, this.ᜎ);
		num += 4;
		this.ᜀ(array, num, this.ᜏ);
		num += 4;
		this.ᜀ(array, num, this.ᜐ);
		num += 4;
		this.ᜀ(array, num, this.ᜑ);
		num += 4;
		this.ᜀ(array, num, this.\u1712);
		num += 4;
		this.ᜀ(array, num, this.\u1713);
		num += 4;
		this.ᜀ(array, num, this.\u1714);
		num += 4;
		this.ᜀ(array, num, this.\u1715);
		num += 4;
		this.ᜀ(array, num, this.\u1716);
		num += 4;
		Buffer.BlockCopy(this.\u1717, 0, array, num, this.\u1717.Length * 4);
		A_0.Write(array, 0, 512);
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x0027D718 File Offset: 0x0027C718
	public static bool ᜀ(Stream A_0)
	{
		byte[] array;
		long position;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_87:
			array = new byte[8];
			position = A_0.Position;
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3C;
		}
		bool result;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				if (A_0.Read(array, 0, 8) == 8)
				{
					num = 4;
					continue;
				}
				goto IL_65;
			case 1:
				return result;
			case 2:
				goto IL_63;
			case 3:
				if (A_0 != null)
				{
					num = 2;
					continue;
				}
				return result;
			case 4:
				result = spr\u250C.ᜀ(array);
				num = 5;
				continue;
			case 5:
				goto IL_65;
			}
			goto IL_3C;
			IL_65:
			A_0.Position = position;
			num = 1;
		}
		IL_63:
		goto IL_87;
		IL_3C:
		result = false;
		if (true)
		{
		}
		num = 3;
		goto IL_1E;
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x0027D7DC File Offset: 0x0027C7DC
	private void ᜀ()
	{
		int a_ = 10;
		if (!spr\u250C.ᜀ(this.ᜅ))
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
				break;
			}
			if (true)
			{
			}
			throw new sprᦹ(ClipboardData.b("❯q᭳ᡵί婹ཻ᝽ﶇ", a_));
		}
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x0027D844 File Offset: 0x0027C844
	private static bool ᜀ(byte[] A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					result = true;
					int num2 = 0;
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 1:
				{
					int num2;
					if (A_0[num2] == spr\u250C.ᜄ[num2])
					{
						num2++;
						num = 10;
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
						num = 4;
						continue;
					}
					break;
				}
				case 2:
					if (A_0.Length == 8)
					{
						num = 0;
						continue;
					}
					return result;
				case 3:
					num = 2;
					continue;
				case 4:
					result = false;
					num = 8;
					continue;
				case 5:
					if (A_0 != null)
					{
						num = 3;
						continue;
					}
					return result;
				case 6:
				{
					int num2;
					if (num2 >= 8)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				}
				case 7:
					return result;
				case 8:
					return result;
				case 9:
					goto IL_C1;
				case 10:
					goto IL_C1;
				}
				break;
				IL_C1:
				num = 6;
			}
		}
		return result;
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x0027D958 File Offset: 0x0027C958
	private void ᜀ(byte[] A_0, int A_1, ushort A_2)
	{
		int a_ = 19;
		if (A_0 == null)
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
				if (true)
				{
				}
				break;
			}
			throw new ArgumentNullException(ClipboardData.b("᭸๺᭼᥾", a_));
		}
		A_0[A_1] = (byte)(A_2 & 255);
		A_0[A_1 + 1] = (byte)((A_2 & 65280) >> 8);
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x0027D9D0 File Offset: 0x0027C9D0
	private void ᜀ(byte[] A_0, int A_1, uint A_2)
	{
		int a_ = 17;
		if (A_0 == null)
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
				break;
			}
			throw new ArgumentNullException(ClipboardData.b("ᕶ౸ᵺ᭼᩾", a_));
		}
		A_0[A_1] = (byte)(A_2 & 255U);
		A_2 >>= 8;
		A_0[A_1 + 1] = (byte)(A_2 & 255U);
		A_2 >>= 8;
		A_0[A_1 + 2] = (byte)(A_2 & 255U);
		A_2 >>= 8;
		A_0[A_1 + 3] = (byte)(A_2 & 255U);
		A_2 >>= 8;
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x0027DA74 File Offset: 0x0027CA74
	private void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 9;
		if (A_0 == null)
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
				break;
			}
			throw new ArgumentNullException(ClipboardData.b("൮Ѱᕲ፴ቶ୸", a_));
		}
		A_0[A_1] = (byte)(A_2 & 255);
		A_2 >>= 8;
		A_0[A_1 + 1] = (byte)(A_2 & 255);
		A_2 >>= 8;
		A_0[A_1 + 2] = (byte)(A_2 & 255);
		A_2 >>= 8;
		A_0[A_1 + 3] = (byte)(A_2 & 255);
		A_2 >>= 8;
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x0027DB18 File Offset: 0x0027CB18
	public int ᜁ()
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
		return 1 << (int)this.ᜊ;
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x0027DB60 File Offset: 0x0027CB60
	public ushort ᜈ()
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

	// Token: 0x060027C7 RID: 10183 RVA: 0x0027DBA4 File Offset: 0x0027CBA4
	public ushort ᜃ()
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
		return this.ᜈ;
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x0027DBE8 File Offset: 0x0027CBE8
	public ushort ᜄ()
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
		return this.ᜉ;
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x0027DC2C File Offset: 0x0027CC2C
	public ushort \u170D()
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
		return this.ᜊ;
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x0027DC70 File Offset: 0x0027CC70
	public ushort ᜑ()
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
		return this.ᜋ;
	}

	// Token: 0x060027CB RID: 10187 RVA: 0x0027DCB4 File Offset: 0x0027CCB4
	public ushort ᜌ()
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

	// Token: 0x060027CC RID: 10188 RVA: 0x0027DCF8 File Offset: 0x0027CCF8
	public uint ᜅ()
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
		return this.\u170D;
	}

	// Token: 0x060027CD RID: 10189 RVA: 0x0027DD3C File Offset: 0x0027CD3C
	public uint ᜇ()
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
		return this.ᜎ;
	}

	// Token: 0x060027CE RID: 10190 RVA: 0x0027DD80 File Offset: 0x0027CD80
	public int ᜏ()
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

	// Token: 0x060027CF RID: 10191 RVA: 0x0027DDC4 File Offset: 0x0027CDC4
	public void ᜁ(int A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x0027DE08 File Offset: 0x0027CE08
	public int ᜐ()
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
		return this.ᜐ;
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x0027DE4C File Offset: 0x0027CE4C
	public void ᜂ(int A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x0027DE90 File Offset: 0x0027CE90
	public int ᜊ()
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
		return this.ᜑ;
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x0027DED4 File Offset: 0x0027CED4
	public uint ᜉ()
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
		return this.\u1712;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x0027DF18 File Offset: 0x0027CF18
	public int \u1712()
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
		return this.\u1713;
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x0027DF5C File Offset: 0x0027CF5C
	public void ᜄ(int A_0)
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
		this.\u1713 = A_0;
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x0027DFA0 File Offset: 0x0027CFA0
	public int ᜆ()
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
		return this.\u1714;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x0027DFE4 File Offset: 0x0027CFE4
	public void ᜆ(int A_0)
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
		this.\u1714 = A_0;
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x0027E028 File Offset: 0x0027D028
	public int ᜋ()
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
		return this.\u1715;
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x0027E06C File Offset: 0x0027D06C
	public void ᜀ(int A_0)
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
		this.\u1715 = A_0;
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x0027E0B0 File Offset: 0x0027D0B0
	public int ᜎ()
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
		return this.\u1716;
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x0027E0F4 File Offset: 0x0027D0F4
	public void ᜃ(int A_0)
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
		this.\u1716 = A_0;
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x0027E138 File Offset: 0x0027D138
	public int[] ᜂ()
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
		return this.\u1717;
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x0027E17C File Offset: 0x0027D17C
	internal void ᜂ(Stream A_0)
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
		byte[] array = new byte[512];
		Buffer.BlockCopy(this.ᜅ, 0, array, 0, 8);
		int num = 8;
		byte[] src = this.ᜆ.ToByteArray();
		Buffer.BlockCopy(src, 0, array, num, 16);
		num += 16;
		src = BitConverter.GetBytes(this.ᜇ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜈ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜉ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜊ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜋ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜌ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.\u170D);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜎ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜏ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜐ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜑ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1712);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1713);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1714);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1715);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1716);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		Buffer.BlockCopy(this.\u1717, 0, array, num, this.\u1717.Length * 4);
		A_0.Position = 0L;
		A_0.Write(array, 0, 512);
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x0027E3BC File Offset: 0x0027D3BC
	internal long ᜅ(int A_0)
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
		return (long)((A_0 << (int)this.ᜊ) + 512);
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x0027E40C File Offset: 0x0027D40C
	internal long ᜀ(int A_0, int A_1)
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
		return (long)((A_0 << (int)this.ᜊ) + A_1);
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x0027E458 File Offset: 0x0027D458
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u250C()
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
		spr\u250C.ᜄ = new byte[]
		{
			208,
			207,
			17,
			224,
			161,
			177,
			26,
			225
		};
	}

	// Token: 0x040022E4 RID: 8932
	public const int ᜀ = 512;

	// Token: 0x040022E5 RID: 8933
	private const int ᜁ = 8;

	// Token: 0x040022E6 RID: 8934
	internal const int ᜂ = 2;

	// Token: 0x040022E7 RID: 8935
	internal const int ᜃ = 4;

	// Token: 0x040022E8 RID: 8936
	private static readonly byte[] ᜄ;

	// Token: 0x040022E9 RID: 8937
	private byte[] ᜅ;

	// Token: 0x040022EA RID: 8938
	private Guid ᜆ;

	// Token: 0x040022EB RID: 8939
	private ushort ᜇ;

	// Token: 0x040022EC RID: 8940
	private ushort ᜈ;

	// Token: 0x040022ED RID: 8941
	private ushort ᜉ;

	// Token: 0x040022EE RID: 8942
	private ushort ᜊ;

	// Token: 0x040022EF RID: 8943
	private ushort ᜋ;

	// Token: 0x040022F0 RID: 8944
	private ushort ᜌ;

	// Token: 0x040022F1 RID: 8945
	private uint \u170D;

	// Token: 0x040022F2 RID: 8946
	private uint ᜎ;

	// Token: 0x040022F3 RID: 8947
	private int ᜏ;

	// Token: 0x040022F4 RID: 8948
	private int ᜐ;

	// Token: 0x040022F5 RID: 8949
	private int ᜑ;

	// Token: 0x040022F6 RID: 8950
	private uint \u1712;

	// Token: 0x040022F7 RID: 8951
	private int \u1713;

	// Token: 0x040022F8 RID: 8952
	private int \u1714;

	// Token: 0x040022F9 RID: 8953
	private int \u1715;

	// Token: 0x040022FA RID: 8954
	private int \u1716;

	// Token: 0x040022FB RID: 8955
	private int[] \u1717;
}
