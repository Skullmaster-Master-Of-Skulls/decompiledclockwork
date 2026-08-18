using System;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000095 RID: 149
internal class sprᶀ
{
	// Token: 0x06000487 RID: 1159 RVA: 0x0002C524 File Offset: 0x0002B524
	public unsafe sprᶀ(bool A_0, ref spr\u2320 A_1, ref int A_2)
	{
		this.ᜃ = string.Empty;
		base..ctor();
		byte b = 0;
		byte b2 = 0;
		int num = 0;
		this.ᜀ = A_0;
		if (!this.ᜀ)
		{
			sprᮌ.ᜀ(ref A_1, ref A_2, (int)this.ᜄ(), (void*)(&b));
			this.ᜁ = (ushort)b;
		}
		else
		{
			fixed (ushort* ptr = &this.ᜁ)
			{
				sprᮌ.ᜀ(ref A_1, ref A_2, (int)this.ᜄ(), (void*)ptr);
			}
		}
		fixed (byte* ptr2 = &this.ᜂ)
		{
			sprᮌ.ᜀ(ref A_1, ref A_2, 1, (void*)ptr2);
		}
		b2 = this.ᜂ;
		if (this.ᜆ())
		{
			fixed (ushort* ptr3 = &this.ᜅ)
			{
				sprᮌ.ᜀ(ref A_1, ref A_2, 2, (void*)ptr3);
			}
		}
		else
		{
			this.ᜅ = 0;
		}
		if (this.ᜃ())
		{
			fixed (ushort* ptr4 = &this.ᜇ)
			{
				sprᮌ.ᜀ(ref A_1, ref A_2, 2, (void*)ptr4);
			}
		}
		else
		{
			this.ᜇ = 0;
		}
		this.ᜄ = new byte[(int)this.ᜁ];
		char[] value = new char[(int)this.ᜁ];
		sprᮌ.ᜀ(ref A_1, ref A_2, ref this.ᜄ, ref value, ref this.ᜂ, ref b2, ref num, (int)this.ᜁ);
		this.ᜃ = new string(value);
		if ((this.ᜇ() ? 1 : 1) == 1)
		{
			this.ᜃ = string.Empty;
		}
		else
		{
			this.ᜄ = new byte[0];
		}
		if (this.ᜅ > 0)
		{
			this.ᜆ = new byte[(int)(4 * this.ᜅ)];
			byte[] array;
			if ((array = this.ᜆ) != null)
			{
				if (array.Length != 0)
				{
					fixed (byte* ptr5 = &array[0])
					{
						goto IL_1BF;
					}
				}
			}
			byte* ptr5 = null;
			IL_1BF:
			sprᮌ.ᜀ(ref A_1, ref A_2, (int)(4 * this.ᜅ), (void*)ptr5);
			ptr5 = null;
		}
		if (this.ᜇ > 0)
		{
			this.ᜈ = new byte[(int)this.ᜇ];
			byte[] array2;
			if ((array2 = this.ᜈ) == null)
			{
				goto IL_22F;
			}
			if (array2.Length == 0)
			{
				goto IL_22F;
			}
			fixed (byte* ptr6 = &array2[0])
			{
				IL_F4:
				sprᮌ.ᜀ(ref A_1, ref A_2, (int)this.ᜇ, (void*)ptr6);
			}
			return;
			IL_22F:
			byte* ptr6 = null;
			goto IL_F4;
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0002C788 File Offset: 0x0002B788
	public sprᶀ(bool A_0, string A_1)
	{
		int a_ = 6;
		this.ᜃ = string.Empty;
		base..ctor();
		this.ᜀ = A_0;
		if (!this.ᜀ && A_1.Length > 255)
		{
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挡嘣䄥嬧甩缫娭䈯嬱娳儵簷嬹䠻弽", a_)));
		}
		this.ᜁ = (ushort)A_1.Length;
		if (A_0)
		{
			this.ᜂ = 1;
		}
		else
		{
			this.ᜂ = 0;
		}
		this.ᜅ = 0;
		this.ᜇ = 0;
		if (!this.ᜇ())
		{
			this.ᜄ = sprᮌ.ᜆ(A_1);
			return;
		}
		this.ᜃ = A_1;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0002C840 File Offset: 0x0002B840
	public int ᜀ(sprᶀ A_0)
	{
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return -1;
			case 1:
				return -1;
			case 2:
				goto IL_A0;
			case 3:
				if (this.ᜂ < A_0.ᜂ())
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
			case 4:
				if (this.ᜄ() > A_0.ᜄ())
				{
					goto IL_75;
				}
				num = 3;
				continue;
			case 5:
				if (this.ᜂ <= A_0.ᜂ())
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 6:
				return 1;
			case 7:
				return 1;
			case 8:
				if (!this.ᜇ())
				{
					num = 2;
					continue;
				}
				goto IL_14B;
			}
			if (this.ᜄ() < A_0.ᜄ())
			{
				num = 1;
				continue;
			}
			if (true)
			{
			}
			num = 4;
			continue;
			IL_75:
			num = 7;
		}
		return -1;
		IL_A0:
		string @string = Encoding.ASCII.GetString(this.ᜄ);
		string string2 = Encoding.ASCII.GetString(A_0.ᜈ());
		return string.Compare(@string, string2);
		IL_14B:
		return string.Compare(this.ᜃ, A_0.ᜀ());
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0002C9AC File Offset: 0x0002B9AC
	public void ᜀ(sprḗ A_0)
	{
		int a_ = 12;
		for (;;)
		{
			byte b = this.ᜄ();
			int num = 20;
			for (;;)
			{
				byte[] bytes;
				switch (num)
				{
				case 0:
					if (this.ᜃ())
					{
						num = 22;
						continue;
					}
					goto IL_2F8;
				case 1:
					if (this.ᜅ > 0)
					{
						num = 5;
						continue;
					}
					goto IL_18A;
				case 2:
					goto IL_20E;
				case 3:
					goto IL_18A;
				case 4:
					goto IL_B1;
				case 5:
					A_0.ᜁ(this.ᜆ, (int)(4 * this.ᜅ));
					num = 3;
					continue;
				case 6:
					num = 15;
					continue;
				case 7:
					bytes = BitConverter.GetBytes(this.ᜅ);
					A_0.ᜁ(bytes, 2);
					num = 23;
					continue;
				case 8:
					if (this.ᜅ() == 1)
					{
						num = 6;
						continue;
					}
					num = 4;
					continue;
				case 9:
					goto IL_1D6;
				case 10:
					if (this.ᜇ > 0)
					{
						num = 11;
						continue;
					}
					return;
				case 11:
					A_0.ᜁ(this.ᜈ, (int)this.ᜇ);
					num = 12;
					continue;
				case 12:
					goto IL_25D;
				case 13:
					goto IL_20E;
				case 14:
					num = 19;
					continue;
				case 15:
					if (this.ᜁ > 0)
					{
						num = 17;
						continue;
					}
					goto IL_20E;
				case 16:
					bytes = Encoding.Unicode.GetBytes(this.ᜃ);
					A_0.ᜁ(bytes, bytes.Length);
					num = 2;
					continue;
				case 17:
					A_0.ᜁ(this.ᜄ, this.ᜄ.Length);
					num = 13;
					continue;
				case 18:
					goto IL_2F8;
				case 19:
					goto IL_28F;
				case 20:
					switch (b)
					{
					case 1:
						bytes = BitConverter.GetBytes(this.ᜁ);
						A_0.ᜁ(bytes, 1);
						if (true)
						{
						}
						num = 9;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B1;
						default:
							if (false)
							{
							}
							bytes = BitConverter.GetBytes(this.ᜁ);
							A_0.ᜁ(bytes, 2);
							num = 21;
							continue;
						}
						break;
					default:
						num = 14;
						continue;
					}
					break;
				case 21:
					goto IL_1D6;
				case 22:
					bytes = BitConverter.GetBytes(this.ᜇ);
					A_0.ᜁ(bytes, 2);
					num = 18;
					continue;
				case 23:
					goto IL_2D5;
				case 24:
					if (this.ᜆ())
					{
						num = 7;
						continue;
					}
					goto IL_2D5;
				}
				break;
				IL_B1:
				if (this.ᜁ > 0)
				{
					num = 16;
					continue;
				}
				goto IL_20E;
				IL_18A:
				num = 10;
				continue;
				IL_1D6:
				bytes = BitConverter.GetBytes((short)this.ᜂ);
				A_0.Write(bytes, 0, 1);
				num = 24;
				continue;
				IL_20E:
				num = 1;
				continue;
				IL_2D5:
				num = 0;
				continue;
				IL_2F8:
				num = 8;
			}
		}
		IL_25D:
		return;
		IL_28F:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("椧堩䬫崭漯縱儳堵強丹吻焽☿แ⅃⡅", a_)));
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0002CD00 File Offset: 0x0002BD00
	public byte ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0002CD44 File Offset: 0x0002BD44
	public byte[] ᜈ()
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

	// Token: 0x0600048D RID: 1165 RVA: 0x0002CD88 File Offset: 0x0002BD88
	public string ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002CDCC File Offset: 0x0002BDCC
	public byte ᜄ()
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_68;
				case 2:
					goto IL_73;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (true)
				{
				}
				if (!this.ᜀ)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
		}
		IL_68:
		byte b = 1;
		goto IL_76;
		IL_73:
		b = 1;
		IL_76:
		return b;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0002CE50 File Offset: 0x0002BE50
	public bool ᜇ()
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
		return (this.ᜂ & 1) != 0;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0002CE9C File Offset: 0x0002BE9C
	public byte ᜅ()
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 2:
					goto IL_68;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (!this.ᜇ())
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
		}
		IL_68:
		byte b = 1;
		goto IL_76;
		IL_73:
		b = 1;
		IL_76:
		return b;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002CF20 File Offset: 0x0002BF20
	public bool ᜆ()
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
		return (this.ᜂ & 8) == 8;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0002CF68 File Offset: 0x0002BF68
	public bool ᜃ()
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
		return (this.ᜂ & 4) == 4;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002CFB0 File Offset: 0x0002BFB0
	public string ᜉ()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (!this.ᜇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			goto IL_44;
		}
		if (false)
		{
		}
		return sprᮌ.ᜀ(this.ᜄ);
		IL_44:
		return this.ᜀ();
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0002D008 File Offset: 0x0002C008
	public int ᜁ()
	{
		int num;
		for (;;)
		{
			num = (int)((ushort)(this.ᜄ() + 1) + this.ᜁ * (ushort)this.ᜅ());
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜆ())
					{
						num2 = 5;
						continue;
					}
					goto IL_8A;
				case 1:
					goto IL_8A;
				case 2:
					if (this.ᜃ())
					{
						num2 = 4;
						continue;
					}
					return num;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_82;
					}
					break;
				case 4:
					num += (int)(2 + this.ᜇ);
					num2 = 3;
					continue;
				case 5:
					if (true)
					{
					}
					goto IL_B4;
				}
				break;
				IL_8A:
				num2 = 2;
				continue;
				IL_B4:
				num += (int)(2 + 4 * this.ᜅ);
				num2 = 1;
			}
		}
		IL_82:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x040002C7 RID: 711
	private bool ᜀ;

	// Token: 0x040002C8 RID: 712
	private ushort ᜁ;

	// Token: 0x040002C9 RID: 713
	private byte ᜂ;

	// Token: 0x040002CA RID: 714
	private string ᜃ;

	// Token: 0x040002CB RID: 715
	private byte[] ᜄ;

	// Token: 0x040002CC RID: 716
	private ushort ᜅ;

	// Token: 0x040002CD RID: 717
	private byte[] ᜆ;

	// Token: 0x040002CE RID: 718
	private ushort ᜇ;

	// Token: 0x040002CF RID: 719
	private byte[] ᜈ;
}
