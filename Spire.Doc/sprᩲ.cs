using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200044F RID: 1103
internal class spr\u1A72 : Stream
{
	// Token: 0x06003D1A RID: 15642 RVA: 0x0038DC20 File Offset: 0x0038CC20
	public virtual bool ᜂ()
	{
		int a_ = 5;
		while (this.ᜁ != null)
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
				return this.ᜁ.CanRead;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᡪᥬᵮᑰቲᡴ", a_));
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x0038DC90 File Offset: 0x0038CC90
	public virtual bool ᜅ()
	{
		int a_ = 19;
		while (this.ᜁ != null)
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
				return this.ᜁ.CanWrite;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("੸ེོ᩾", a_));
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x0038DD00 File Offset: 0x0038CD00
	public virtual bool ᜄ()
	{
		int a_ = 12;
		while (this.ᜁ != null)
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
				return this.ᜁ.CanSeek;
			}
		}
		throw new ArgumentNullException(ClipboardData.b("űsѵᵷ᭹ᅻ", a_));
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x0038DD70 File Offset: 0x0038CD70
	public virtual long ᜉ()
	{
		int a_ = 5;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜅ > 0)
				{
					num = 4;
					continue;
				}
				goto IL_88;
			case 1:
				goto IL_3D;
			case 3:
				goto IL_88;
			case 4:
				this.ᜀ();
				goto IL_4F;
			}
			if (this.ᜁ == null)
			{
				num = 1;
				continue;
			}
			num = 0;
			continue;
			IL_4F:
			num = 3;
			continue;
			IL_88:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4F;
			default:
				goto IL_9E;
			}
		}
		IL_3D:
		throw new ArgumentNullException(ClipboardData.b("ᡪᥬᵮᑰቲᡴ", a_));
		IL_9E:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜁ.Length;
	}

	// Token: 0x06003D1E RID: 15646 RVA: 0x0038DE34 File Offset: 0x0038CE34
	public virtual long ᜇ()
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
		return this.ᜇ + (long)this.ᜃ + (long)this.ᜅ;
	}

	// Token: 0x06003D1F RID: 15647 RVA: 0x0038DE88 File Offset: 0x0038CE88
	public virtual void ᜀ(long A_0)
	{
		int a_ = 17;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜁ == null)
				{
					num = 6;
					continue;
				}
				num = 8;
				continue;
			case 1:
				num = 11;
				continue;
			case 2:
				goto IL_8D;
			case 3:
				goto IL_5A;
			case 4:
				goto IL_17B;
			case 5:
				if (this.ᜇ + (long)this.ᜄ > A_0)
				{
					num = 1;
					continue;
				}
				goto IL_1A5;
			case 6:
				goto IL_CC;
			case 7:
				goto IL_FD;
			case 8:
				if (!this.ᜁ.CanSeek)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			case 9:
				goto IL_5F;
			case 11:
				if (A_0 >= this.ᜇ)
				{
					num = 7;
					continue;
				}
				goto IL_1A5;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					this.ᜀ();
					num = 9;
					continue;
				}
				break;
			}
			if (A_0 < 0L)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_5F:
			num = 5;
			continue;
			IL_8D:
			if (true)
			{
			}
			if (this.ᜅ <= 0)
			{
				goto IL_5F;
			}
			num = 12;
		}
		IL_5A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ŷᡸ᝺ࡼ᩾", a_));
		IL_CC:
		throw new ArgumentNullException(ClipboardData.b("Ѷ൸ॺ᡼Ṿ", a_));
		IL_FD:
		this.ᜃ = (int)(A_0 - this.ᜇ);
		return;
		IL_17B:
		throw new ArgumentException(ClipboardData.b("⑶൸ॺ᡼Ṿꎂ권ﺐ떔펠힢薤풦첨캪욬辮\udeb0쎲킴얶\ud8b8쾺풼킾꿀", a_));
		IL_1A5:
		this.ᜃ = 0;
		this.ᜄ = 0;
		this.ᜇ = this.ᜁ.Seek(A_0, SeekOrigin.Begin);
	}

	// Token: 0x06003D20 RID: 15648 RVA: 0x0038E05C File Offset: 0x0038D05C
	public Stream ᜊ()
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

	// Token: 0x06003D21 RID: 15649 RVA: 0x0038E0A0 File Offset: 0x0038D0A0
	private spr\u1A72()
	{
	}

	// Token: 0x06003D22 RID: 15650 RVA: 0x0038E0B4 File Offset: 0x0038D0B4
	public spr\u1A72(Stream A_0) : this(A_0, 4096)
	{
	}

	// Token: 0x06003D23 RID: 15651 RVA: 0x0038E0D0 File Offset: 0x0038D0D0
	public spr\u1A72(Stream A_0, int A_1)
	{
		int a_ = 19;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("੸ེོ᩾", a_));
		}
		if (A_1 <= 0)
		{
			throw new ArgumentOutOfRangeException(ClipboardData.b("᭸๺᭼᥾횄", a_));
		}
		this.ᜁ = A_0;
		this.ᜆ = A_1;
		if (!this.ᜁ.CanRead && !this.ᜁ.CanWrite)
		{
			throw new ArgumentException(ClipboardData.b("⩸ེོ᩾ꖄ꾎뺐뎒뾞캠펢삤햦좨\udfaa쒬삮\udfb0삲閴\udeb6쪸鮺\udebc펾껀냂ꃄꏆ", a_), ClipboardData.b("੸ེོ᩾", a_));
		}
	}

	// Token: 0x06003D24 RID: 15652 RVA: 0x0038E170 File Offset: 0x0038D170
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 15;
		int num;
		long num4;
		for (;;)
		{
			num = this.ᜄ - this.ᜃ;
			int num2 = 16;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num == 0)
					{
						num2 = 11;
						continue;
					}
					this.ᜃ = 0;
					this.ᜄ = num;
					num2 = 8;
					continue;
				case 1:
				{
					int num3 = this.ᜁ.Read(A_0, A_1 + num, A_2 - num);
					num += num3;
					this.ᜇ = this.ᜁ.Position;
					this.ᜃ = 0;
					this.ᜄ = 0;
					num2 = 10;
					continue;
				}
				case 2:
					num = A_2;
					num2 = 9;
					continue;
				case 3:
					this.ᜂ = new byte[this.ᜆ];
					num2 = 17;
					continue;
				case 4:
					if (A_2 >= this.ᜆ)
					{
						num2 = 14;
						continue;
					}
					num2 = 19;
					continue;
				case 5:
					goto IL_238;
				case 6:
					goto IL_103;
				case 7:
					goto IL_282;
				case 8:
					goto IL_219;
				case 9:
					goto IL_1CD;
				case 10:
					goto IL_18F;
				case 11:
					goto IL_F6;
				case 12:
					this.ᜀ();
					num2 = 5;
					continue;
				case 13:
					num2 = 15;
					continue;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_103;
					}
					goto Block_9;
				case 15:
					if (!this.ᜁ.CanRead)
					{
						num2 = 7;
						continue;
					}
					num2 = 6;
					continue;
				case 16:
					if (num == 0)
					{
						num2 = 13;
						continue;
					}
					goto IL_219;
				case 17:
					goto IL_AC;
				case 18:
					if (true)
					{
					}
					if (num < A_2)
					{
						num2 = 1;
						continue;
					}
					return num;
				case 19:
					if (this.ᜂ == null)
					{
						num2 = 3;
						continue;
					}
					goto IL_AC;
				case 20:
					if (num > A_2)
					{
						num2 = 2;
						continue;
					}
					goto IL_1CD;
				}
				break;
				IL_AC:
				num4 = this.ᜇ;
				this.ᜇ = this.ᜁ.Position;
				num = this.ᜁ.Read(this.ᜂ, 0, this.ᜆ);
				num2 = 0;
				continue;
				IL_103:
				if (this.ᜅ > 0)
				{
					num2 = 12;
					continue;
				}
				goto IL_238;
				IL_1CD:
				Buffer.BlockCopy(this.ᜂ, this.ᜃ, A_0, A_1, num);
				this.ᜃ += num;
				num2 = 18;
				continue;
				IL_219:
				num2 = 20;
				continue;
				IL_238:
				num2 = 4;
			}
		}
		IL_F6:
		this.ᜇ = num4;
		return 0;
		IL_18F:
		return num;
		IL_282:
		throw new ArgumentException(ClipboardData.b("♴Ͷ୸Ṻᱼቾꆀ愈ꮊ뎒햠莢힤슦좨쾪趬삮솰횲잴횶춸튺튼톾", a_));
		Block_9:
		if (false)
		{
		}
		num = this.ᜁ.Read(A_0, A_1, A_2);
		this.ᜇ = this.ᜁ.Position;
		this.ᜃ = 0;
		this.ᜄ = 0;
		return num;
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x0038E47C File Offset: 0x0038D47C
	public virtual int ᜆ()
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜃ == this.ᜄ)
				{
					num = 16;
					continue;
				}
				goto IL_22B;
			case 2:
				this.ᜂ = new byte[this.ᜆ];
				num = 13;
				continue;
			case 3:
				if (this.ᜃ == this.ᜄ)
				{
					num = 9;
					continue;
				}
				goto IL_D9;
			case 4:
				goto IL_70;
			case 5:
				if (this.ᜅ > 0)
				{
					num = 10;
					continue;
				}
				goto IL_122;
			case 6:
				if (this.ᜄ == 0)
				{
					num = 11;
					continue;
				}
				goto IL_147;
			case 7:
				if (this.ᜂ == null)
				{
					num = 2;
					continue;
				}
				goto IL_18E;
			case 8:
				if (true)
				{
				}
				goto IL_122;
			case 9:
				num = 5;
				continue;
			case 10:
				IL_212:
				this.ᜀ();
				num = 8;
				continue;
			case 11:
				num = 14;
				continue;
			case 12:
				goto IL_D9;
			case 13:
				goto IL_18E;
			case 14:
				if (!this.ᜁ.CanRead)
				{
					num = 15;
					continue;
				}
				goto IL_147;
			case 15:
				goto IL_D4;
			case 16:
				return -1;
			}
			if (this.ᜁ == null)
			{
				num = 4;
				continue;
			}
			num = 6;
			continue;
			IL_D9:
			num = 0;
			continue;
			IL_122:
			num = 7;
			continue;
			IL_147:
			num = 3;
			continue;
			IL_18E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_212;
			default:
				if (false)
				{
				}
				this.ᜇ = this.ᜁ.Position;
				this.ᜄ = this.ᜁ.Read(this.ᜂ, 0, this.ᜆ);
				this.ᜃ = 0;
				num = 12;
				break;
			}
		}
		IL_70:
		throw new ArgumentNullException(ClipboardData.b("୷๹๻᭽", a_));
		IL_D4:
		throw new ArgumentException(ClipboardData.b("⭷๹๻᭽ꒃﾋ꺍ﺏ﶑뚕쾟킡킣蚥盛쾩춫쪭邯\uddb1쒳펵쪷\udbb9좻ힽ꾿곁", a_));
		IL_22B:
		return (int)this.ᜂ[this.ᜃ++];
	}

	// Token: 0x06003D26 RID: 15654 RVA: 0x0038E6CC File Offset: 0x0038D6CC
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 18;
		int num = 34;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_2 < 0)
				{
					num = 9;
					continue;
				}
				num = 15;
				continue;
			case 1:
				goto IL_20A;
			case 2:
				if (A_2 >= this.ᜆ)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (A_2 == 0)
				{
					num = 22;
					continue;
				}
				num = 8;
				continue;
			case 4:
				goto IL_2BB;
			case 5:
				goto IL_B9;
			case 6:
				this.ᜂ = new byte[this.ᜆ];
				num = 1;
				continue;
			case 7:
				goto IL_235;
			case 8:
				if (this.ᜂ == null)
				{
					num = 6;
					continue;
				}
				goto IL_444;
			case 9:
				goto IL_14F;
			case 10:
				if (A_1 < 0)
				{
					num = 11;
					continue;
				}
				num = 0;
				continue;
			case 11:
				goto IL_3C4;
			case 12:
				return;
			case 13:
				goto IL_283;
			case 14:
				if (this.ᜅ == 0)
				{
					num = 17;
					continue;
				}
				goto IL_199;
			case 15:
				if (A_0.Length - A_1 < A_2)
				{
					num = 23;
					continue;
				}
				num = 29;
				continue;
			case 16:
				if (this.ᜃ < this.ᜄ)
				{
					num = 26;
					continue;
				}
				goto IL_199;
			case 17:
				num = 28;
				continue;
			case 18:
				if (num2 > 0)
				{
					num = 27;
					continue;
				}
				goto IL_404;
			case 19:
				goto IL_199;
			case 20:
				if (num2 <= A_2)
				{
					goto IL_154;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_329;
				default:
					if (false)
					{
					}
					num = 31;
					continue;
				}
				break;
			case 21:
				num2 = this.ᜆ - this.ᜅ;
				num = 18;
				continue;
			case 22:
				return;
			case 23:
				goto IL_110;
			case 24:
				if (this.ᜅ > 0)
				{
					num = 21;
					continue;
				}
				goto IL_299;
			case 25:
				goto IL_154;
			case 26:
				if (true)
				{
				}
				this.ᜁ();
				num = 19;
				continue;
			case 27:
				num = 20;
				continue;
			case 28:
				if (!this.ᜁ.CanWrite)
				{
					num = 7;
					continue;
				}
				num = 16;
				continue;
			case 29:
				if (this.ᜁ == null)
				{
					num = 13;
					continue;
				}
				goto IL_329;
			case 30:
				if (A_2 == num2)
				{
					num = 12;
					continue;
				}
				A_1 += num2;
				A_2 -= num2;
				num = 33;
				continue;
			case 31:
				num2 = A_2;
				num = 25;
				continue;
			case 32:
				goto IL_299;
			case 33:
				goto IL_404;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 10;
			continue;
			IL_154:
			Buffer.BlockCopy(A_0, A_1, this.ᜂ, this.ᜅ, num2);
			this.ᜅ += num2;
			num = 30;
			continue;
			IL_199:
			num = 24;
			continue;
			IL_299:
			num = 2;
			continue;
			IL_329:
			num = 14;
			continue;
			IL_404:
			this.ᜁ.Write(this.ᜂ, 0, this.ᜅ);
			this.ᜇ = this.ᜁ.Position;
			this.ᜅ = 0;
			num = 32;
		}
		IL_B9:
		throw new ArgumentNullException(ClipboardData.b("᥷ࡹ๻ώ勵", a_));
		IL_110:
		throw new ArgumentException();
		IL_14F:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭷ᕹॻၽ", a_));
		IL_20A:
		goto IL_444;
		IL_235:
		throw new ArgumentException(ClipboardData.b("⭷๹๻᭽ꒃﾋ꺍ﺏ﶑뚕쾟킡킣蚥\udfa7\ud8a9얫\udaad햯銱\udbb3욵\uddb7좹\uddbb쪽ꦿ귁꫃", a_));
		IL_283:
		throw new ArgumentNullException(ClipboardData.b("୷๹๻᭽", a_));
		IL_2BB:
		this.ᜁ.Write(A_0, A_1, A_2);
		this.ᜇ = this.ᜁ.Position;
		return;
		IL_3C4:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᝷ᱹ᩻ൽ", a_));
		IL_444:
		Buffer.BlockCopy(A_0, A_1, this.ᜂ, 0, A_2);
		this.ᜅ += A_2;
	}

	// Token: 0x06003D27 RID: 15655 RVA: 0x0038EB3C File Offset: 0x0038DB3C
	public virtual void ᜀ(byte A_0)
	{
		int a_ = 12;
		int num = 12;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (this.ᜅ == 0)
				{
					num = 5;
					continue;
				}
				goto IL_DF;
			case 1:
				this.ᜂ = new byte[this.ᜆ];
				num = 4;
				continue;
			case 2:
				this.ᜀ();
				num = 10;
				continue;
			case 3:
				this.ᜁ();
				num = 6;
				continue;
			case 4:
				goto IL_DF;
			case 5:
				num = 14;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BC;
				default:
					if (false)
					{
					}
					goto IL_11C;
				}
				break;
			case 7:
				if (this.ᜂ == null)
				{
					num = 1;
					continue;
				}
				goto IL_DF;
			case 8:
				if (this.ᜅ == this.ᜆ)
				{
					num = 2;
					continue;
				}
				goto IL_1C1;
			case 9:
				goto IL_6D;
			case 10:
				goto IL_150;
			case 11:
				goto IL_193;
			case 13:
				if (this.ᜃ < this.ᜄ)
				{
					num = 3;
					continue;
				}
				goto IL_11C;
			case 14:
				if (!this.ᜁ.CanWrite)
				{
					num = 11;
					continue;
				}
				num = 13;
				continue;
			}
			if (this.ᜁ == null)
			{
				num = 9;
				continue;
			}
			IL_BC:
			num = 0;
			continue;
			IL_DF:
			num = 8;
			continue;
			IL_11C:
			num = 7;
		}
		IL_6D:
		throw new ArgumentNullException(ClipboardData.b("űsѵᵷ᭹ᅻ", a_));
		IL_150:
		goto IL_1C1;
		IL_193:
		throw new ArgumentException(ClipboardData.b("ⅱsѵᵷ᭹ᅻ幽ꢇ揄낏肟햡횣쾥\udca7쾩貫솭삯ힱ욳ힵ첷펹펻킽", a_));
		IL_1C1:
		this.ᜂ[this.ᜅ++] = A_0;
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x0038ED24 File Offset: 0x0038DD24
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
	{
		int a_ = 1;
		int num = 8;
		long num2;
		for (;;)
		{
			long num3;
			switch (num)
			{
			case 0:
				goto IL_310;
			case 1:
				if (this.ᜄ > 0)
				{
					num = 17;
					continue;
				}
				goto IL_3D9;
			case 2:
				if (num2 < num3 + (long)this.ᜄ - (long)this.ᜃ)
				{
					num = 27;
					continue;
				}
				goto IL_28F;
			case 3:
				goto IL_C3;
			case 4:
				if (this.ᜄ > 0)
				{
					if (true)
					{
					}
					num = 12;
					continue;
				}
				goto IL_3D9;
			case 5:
				if (A_1 == SeekOrigin.Current)
				{
					goto IL_303;
				}
				goto IL_310;
			case 6:
				goto IL_1D3;
			case 7:
				goto IL_9F;
			case 9:
				goto IL_1CE;
			case 10:
				num = 26;
				continue;
			case 11:
				if (this.ᜅ <= 0)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_303;
				default:
					if (false)
					{
					}
					num = 18;
					continue;
				}
				break;
			case 12:
				this.ᜁ.Seek((long)this.ᜄ, SeekOrigin.Current);
				num = 13;
				continue;
			case 13:
				goto IL_219;
			case 14:
				if (num3 - (long)this.ᜃ < num2)
				{
					num = 23;
					continue;
				}
				goto IL_28F;
			case 15:
				Buffer.BlockCopy(this.ᜂ, this.ᜃ, this.ᜂ, 0, this.ᜄ - this.ᜃ);
				this.ᜄ -= this.ᜃ;
				this.ᜃ = 0;
				num = 6;
				continue;
			case 16:
				if (!this.ᜁ.CanSeek)
				{
					num = 9;
					continue;
				}
				num = 11;
				continue;
			case 17:
				this.ᜁ.Seek((long)this.ᜄ, SeekOrigin.Current);
				num = 3;
				continue;
			case 18:
				this.ᜀ();
				num = 0;
				continue;
			case 19:
				A_0 -= (long)(this.ᜄ - this.ᜃ);
				num = 24;
				continue;
			case 20:
				goto IL_2A8;
			case 21:
				if (this.ᜃ > 0)
				{
					num = 15;
					continue;
				}
				goto IL_1D3;
			case 22:
				num = 21;
				continue;
			case 23:
				num = 2;
				continue;
			case 24:
				goto IL_310;
			case 25:
				if (this.ᜄ > 0)
				{
					num = 10;
					continue;
				}
				goto IL_3D9;
			case 26:
				if (num3 == num2)
				{
					num = 22;
					continue;
				}
				num = 14;
				continue;
			case 27:
			{
				int num4 = (int)(num2 - num3);
				Buffer.BlockCopy(this.ᜂ, this.ᜃ + num4, this.ᜂ, 0, this.ᜄ - (this.ᜃ + num4));
				this.ᜄ -= this.ᜃ + num4;
				this.ᜃ = 0;
				num = 4;
				continue;
			}
			}
			if (this.ᜁ == null)
			{
				num = 7;
				continue;
			}
			num = 16;
			continue;
			IL_1D3:
			num = 1;
			continue;
			IL_28F:
			this.ᜃ = 0;
			this.ᜄ = 0;
			num = 20;
			continue;
			IL_303:
			num = 19;
			continue;
			IL_310:
			num3 = this.ᜁ.Position + (long)(this.ᜃ - this.ᜄ);
			num2 = this.ᜁ.Seek(A_0, A_1);
			num = 25;
		}
		IL_9F:
		throw new ArgumentNullException(ClipboardData.b("ᑦᵨᥪ࡬๮ᱰ", a_));
		IL_C3:
		goto IL_3D9;
		IL_1CE:
		throw new ArgumentException(ClipboardData.b("㑦ᵨᥪ࡬๮ᱰ卲ᅴᡶᱸࡺ嵼ᅾꖄﲈﮊﶌ떔쒖ﲘﺚ뾞캠펢삤햦좨\udfaa쒬삮\udfb0鶲", a_));
		IL_219:
		IL_2A8:
		IL_3D9:
		this.ᜇ = num2;
		return num2;
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x0038F114 File Offset: 0x0038E114
	public virtual void ᜃ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
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
				this.Flush();
				this.ᜁ.Close();
				num = 2;
				continue;
			case 2:
				goto IL_75;
			}
			if (true)
			{
			}
			if (this.ᜁ == null)
			{
				break;
			}
			num = 1;
		}
		IL_75:
		this.ᜁ = null;
		this.ᜂ = null;
	}

	// Token: 0x06003D2A RID: 15658 RVA: 0x0038F1A8 File Offset: 0x0038E1A8
	public virtual void ᜈ()
	{
		int a_ = 0;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜁ.CanSeek)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				goto IL_B6;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B6;
				default:
					goto IL_66;
				}
				break;
			case 4:
				if (this.ᜅ > 0)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 5:
				num = 0;
				continue;
			case 6:
				goto IL_C7;
			case 7:
				goto IL_F2;
			case 8:
				if (this.ᜃ < this.ᜄ)
				{
					num = 5;
					continue;
				}
				return;
			}
			if (this.ᜁ == null)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_B6:
			this.ᜁ();
			num = 6;
		}
		IL_66:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᕥᱧᡩ५཭ᵯ", a_));
		IL_C7:
		return;
		IL_F2:
		this.ᜀ();
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x0038F2D4 File Offset: 0x0038E2D4
	private void ᜁ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
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
					break;
				}
				this.ᜁ.Seek((long)(this.ᜃ - this.ᜄ), SeekOrigin.Current);
				num = 2;
				continue;
			case 2:
				goto IL_86;
			}
			if (this.ᜃ - this.ᜄ == 0)
			{
				break;
			}
			num = 1;
		}
		IL_86:
		this.ᜃ = 0;
		this.ᜄ = 0;
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x0038F378 File Offset: 0x0038E378
	private void ᜀ()
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
		this.ᜁ.Write(this.ᜂ, 0, this.ᜅ);
		this.ᜅ = 0;
		this.ᜇ = this.ᜁ.Position;
		this.ᜁ.Flush();
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x0038F3F0 File Offset: 0x0038E3F0
	public virtual void ᜁ(long A_0)
	{
		int a_ = 19;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_14C;
			case 1:
				goto IL_5E;
			case 3:
				this.ᜀ();
				num = 6;
				continue;
			case 4:
				if (this.ᜅ > 0)
				{
					num = 3;
					continue;
				}
				num = 8;
				continue;
			case 5:
				if (this.ᜁ == null)
				{
					num = 9;
					continue;
				}
				num = 13;
				continue;
			case 6:
				goto IL_136;
			case 7:
				goto IL_1C2;
			case 8:
				if (this.ᜃ >= this.ᜄ)
				{
					goto IL_1C4;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13B;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 9:
				goto IL_C7;
			case 10:
				goto IL_13B;
			case 11:
				goto IL_A1;
			case 12:
				if (!this.ᜁ.CanWrite)
				{
					num = 11;
					continue;
				}
				num = 4;
				continue;
			case 13:
				if (!this.ᜁ.CanSeek)
				{
					num = 7;
					continue;
				}
				num = 12;
				continue;
			}
			if (A_0 < 0L)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_13B:
			this.ᜁ();
			num = 0;
		}
		IL_5E:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ླྀ᩺ᅼ੾", a_));
		IL_A1:
		throw new ArgumentException(ClipboardData.b("⩸ེོ᩾ꖄﺌ꾎ﾐﲒ랖캠톢톤螦ﺨ\ud9aa쒬\udbae풰鎲\udab4잶\udcb8즺\udcbc쮾ꣀ곂ꯄ", a_));
		IL_C7:
		throw new ArgumentNullException(ClipboardData.b("੸ེོ᩾", a_));
		IL_136:
		IL_14C:
		goto IL_1C4;
		IL_1C2:
		if (true)
		{
		}
		throw new ArgumentException(ClipboardData.b("⩸ེོ᩾ꖄﺌ꾎ﾐﲒ랖캠톢톤螦直캪좬쒮醰\udcb2어튶쮸\udaba즼횾껀귂", a_));
		IL_1C4:
		this.ᜁ.SetLength(A_0);
	}

	// Token: 0x04002C21 RID: 11297
	private const int ᜀ = 4096;

	// Token: 0x04002C22 RID: 11298
	private Stream ᜁ;

	// Token: 0x04002C23 RID: 11299
	private byte[] ᜂ;

	// Token: 0x04002C24 RID: 11300
	private int ᜃ;

	// Token: 0x04002C25 RID: 11301
	private int ᜄ;

	// Token: 0x04002C26 RID: 11302
	private int ᜅ;

	// Token: 0x04002C27 RID: 11303
	private int ᜆ;

	// Token: 0x04002C28 RID: 11304
	private long ᜇ;
}
