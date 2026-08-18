using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005B8 RID: 1464
internal class sprᜣ : Stream
{
	// Token: 0x0600586C RID: 22636 RVA: 0x00382968 File Offset: 0x00381968
	public virtual bool ᜂ()
	{
		int a_ = 19;
		while (this.ᜁ != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return this.ᜁ.CanRead;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌⩎ぐ㹒", a_));
	}

	// Token: 0x0600586D RID: 22637 RVA: 0x003829D8 File Offset: 0x003819D8
	public virtual bool ᜅ()
	{
		int a_ = 12;
		while (this.ᜁ != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return this.ᜁ.CanWrite;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅ⵇ⭉⅋", a_));
	}

	// Token: 0x0600586E RID: 22638 RVA: 0x00382A48 File Offset: 0x00381A48
	public virtual bool ᜄ()
	{
		int a_ = 18;
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
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋⭍ㅏ㽑", a_));
	}

	// Token: 0x0600586F RID: 22639 RVA: 0x00382AB8 File Offset: 0x00381AB8
	public virtual long ᜉ()
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4D;
			case 1:
				this.ᜀ();
				num = 0;
				continue;
			case 3:
				goto IL_3D;
			case 4:
				if (this.ᜅ > 0)
				{
					num = 1;
					continue;
				}
				goto IL_AC;
			}
			if (this.ᜁ == null)
			{
				num = 3;
			}
			else
			{
				num = 4;
			}
		}
		IL_3D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4D:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂", a_));
		}
		IL_AC:
		return this.ᜁ.Length;
	}

	// Token: 0x06005870 RID: 22640 RVA: 0x00382B7C File Offset: 0x00381B7C
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

	// Token: 0x06005871 RID: 22641 RVA: 0x00382BD0 File Offset: 0x00381BD0
	public virtual void ᜀ(long A_0)
	{
		int a_ = 6;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜅ > 0)
				{
					num = 6;
					continue;
				}
				goto IL_7B;
			case 1:
				if (this.ᜇ + (long)this.ᜄ > A_0)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				goto IL_1A8;
			case 2:
				if (this.ᜁ == null)
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
			case 3:
				goto IL_17E;
			case 4:
				goto IL_7B;
			case 5:
				num = 9;
				continue;
			case 6:
				this.ᜀ();
				goto IL_138;
			case 7:
				goto IL_76;
			case 8:
				goto IL_EB;
			case 9:
				if (A_0 >= this.ᜇ)
				{
					num = 10;
					continue;
				}
				goto IL_1A8;
			case 10:
				goto IL_112;
			case 12:
				if (!this.ᜁ.CanSeek)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_0 >= 0L)
			{
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_138;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_7B:
			num = 1;
			continue;
			IL_138:
			num = 4;
		}
		IL_76:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨻弽ⰿ㝁⅃", a_));
		IL_EB:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿❁╃⭅", a_));
		IL_112:
		this.ᜃ = (int)(A_0 - this.ᜇ);
		return;
		IL_17E:
		throw new ArgumentException(RecordTableEnumerator.b("漻䨽㈿❁╃⭅桇⹉⍋⭍⍏牑㩓㥕ⱗ穙⽛⭝ၟቡୣᑥᱧ䩩Ὣ୭ᕯᥱ味᥵ࡷό๻ώꚇ", a_));
		IL_1A8:
		this.ᜃ = 0;
		this.ᜄ = 0;
		this.ᜇ = this.ᜁ.Seek(A_0, SeekOrigin.Begin);
	}

	// Token: 0x06005872 RID: 22642 RVA: 0x00382DA8 File Offset: 0x00381DA8
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

	// Token: 0x06005873 RID: 22643 RVA: 0x00382DEC File Offset: 0x00381DEC
	private sprᜣ()
	{
	}

	// Token: 0x06005874 RID: 22644 RVA: 0x00382E00 File Offset: 0x00381E00
	public sprᜣ(Stream A_0) : this(A_0, 4096)
	{
	}

	// Token: 0x06005875 RID: 22645 RVA: 0x00382E1C File Offset: 0x00381E1C
	public sprᜣ(Stream A_0, int A_1)
	{
		int a_ = 13;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ⱈ⩊⁌", a_));
		}
		if (A_1 <= 0)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅂いⅆ⽈⹊㽌ᱎ㡐⥒ご", a_));
		}
		this.ᜁ = A_0;
		this.ᜆ = A_1;
		if (!this.ᜁ.CanRead && !this.ᜁ.CanWrite)
		{
			throw new ArgumentException(RecordTableEnumerator.b("၂ㅄ㕆ⱈ⩊⁌潎⍐㙒㑔㍖祘瑚絜⡞፠੢ᅤɦ䥨Ѫᵬ੮ͰቲŴṶᙸᕺ๼彾ꖄﺌ붒", a_), RecordTableEnumerator.b("あㅄ㕆ⱈ⩊⁌", a_));
		}
	}

	// Token: 0x06005876 RID: 22646 RVA: 0x00382EBC File Offset: 0x00381EBC
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 4;
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
					goto IL_242;
				case 1:
					if (num == 0)
					{
						num2 = 9;
						continue;
					}
					goto IL_8E;
				case 2:
					num = A_2;
					num2 = 18;
					continue;
				case 3:
					goto IL_AC;
				case 4:
					if (A_2 >= this.ᜆ)
					{
						num2 = 20;
						continue;
					}
					num2 = 11;
					continue;
				case 5:
					goto IL_223;
				case 6:
					if (true)
					{
					}
					if (num < A_2)
					{
						num2 = 13;
						continue;
					}
					return num;
				case 7:
					num2 = 10;
					continue;
				case 8:
					goto IL_2A8;
				case 9:
					goto IL_F6;
				case 10:
					if (!this.ᜁ.CanRead)
					{
						num2 = 8;
						continue;
					}
					num2 = 15;
					continue;
				case 11:
					if (this.ᜂ == null)
					{
						num2 = 19;
						continue;
					}
					goto IL_AC;
				case 12:
					this.ᜀ();
					num2 = 0;
					continue;
				case 13:
				{
					int num3 = this.ᜁ.Read(A_0, A_1 + num, A_2 - num);
					num += num3;
					this.ᜇ = this.ᜁ.Position;
					this.ᜃ = 0;
					this.ᜄ = 0;
					num2 = 17;
					continue;
				}
				case 14:
					if (num > A_2)
					{
						num2 = 2;
						continue;
					}
					goto IL_1D7;
				case 15:
					if (this.ᜅ > 0)
					{
						num2 = 12;
						continue;
					}
					goto IL_242;
				case 16:
					if (num == 0)
					{
						num2 = 7;
						continue;
					}
					goto IL_223;
				case 17:
					goto IL_199;
				case 18:
					goto IL_1D7;
				case 19:
					this.ᜂ = new byte[this.ᜆ];
					num2 = 3;
					continue;
				case 20:
					goto IL_280;
				}
				break;
				IL_8E:
				this.ᜃ = 0;
				this.ᜄ = num;
				num2 = 5;
				continue;
				IL_242:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8E;
				default:
					if (false)
					{
					}
					num2 = 4;
					continue;
				}
				IL_AC:
				num4 = this.ᜇ;
				this.ᜇ = this.ᜁ.Position;
				num = this.ᜁ.Read(this.ᜂ, 0, this.ᜆ);
				num2 = 1;
				continue;
				IL_1D7:
				Buffer.BlockCopy(this.ᜂ, this.ᜃ, A_0, A_1, num);
				this.ᜃ += num;
				num2 = 6;
				continue;
				IL_223:
				num2 = 14;
			}
		}
		IL_F6:
		this.ᜇ = num4;
		return 0;
		IL_199:
		return num;
		IL_280:
		num = this.ᜁ.Read(A_0, A_1, A_2);
		this.ᜇ = this.ᜁ.Position;
		this.ᜃ = 0;
		this.ᜄ = 0;
		return num;
		IL_2A8:
		throw new ArgumentException(RecordTableEnumerator.b("椹䠻䰽┿⍁⥃晅ⱇ╉⥋㵍灏㱑㭓≕硗⥙⥛⹝ၟൡᙣብ䡧ᡩ५཭ᑯ剱᭳ٵᵷࡹᵻ੽ꢅ", a_));
	}

	// Token: 0x06005877 RID: 22647 RVA: 0x003831C8 File Offset: 0x003821C8
	public virtual int ᜆ()
	{
		int a_ = 11;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				if (true)
				{
				}
				goto IL_12F;
			case 2:
				goto IL_D7;
			case 3:
				if (!this.ᜁ.CanRead)
				{
					num = 2;
					continue;
				}
				goto IL_170;
			case 4:
				goto IL_DC;
			case 5:
				if (this.ᜅ > 0)
				{
					num = 8;
					continue;
				}
				goto IL_12F;
			case 6:
				goto IL_70;
			case 7:
				this.ᜂ = new byte[this.ᜆ];
				num = 11;
				continue;
			case 8:
				goto IL_93;
			case 9:
				num = 3;
				continue;
			case 10:
				if (this.ᜂ == null)
				{
					num = 7;
					continue;
				}
				goto IL_1AD;
			case 11:
				goto IL_1AD;
			case 12:
				if (this.ᜄ == 0)
				{
					num = 9;
					continue;
				}
				goto IL_170;
			case 13:
				if (this.ᜃ == this.ᜄ)
				{
					num = 14;
					continue;
				}
				goto IL_22E;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_93;
				default:
					goto IL_168;
				}
				break;
			case 15:
				if (this.ᜃ == this.ᜄ)
				{
					num = 0;
					continue;
				}
				goto IL_DC;
			}
			if (this.ᜁ == null)
			{
				num = 6;
				continue;
			}
			num = 12;
			continue;
			IL_93:
			this.ᜀ();
			num = 1;
			continue;
			IL_DC:
			num = 13;
			continue;
			IL_12F:
			num = 10;
			continue;
			IL_170:
			num = 15;
			continue;
			IL_1AD:
			this.ᜇ = this.ᜁ.Position;
			this.ᜄ = this.ᜁ.Read(this.ᜂ, 0, this.ᜆ);
			this.ᜃ = 0;
			num = 4;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂㝄≆⡈♊", a_));
		IL_D7:
		throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄≆⡈♊浌⭎㹐㙒♔睖㝘㑚⥜罞በᙢᕤᝦ٨ᥪᥬ佮⍰ᙲᑴ፶奸ᑺർ᩾ꎌ", a_));
		IL_168:
		if (false)
		{
		}
		return -1;
		IL_22E:
		return (int)this.ᜂ[this.ᜃ++];
	}

	// Token: 0x06005878 RID: 22648 RVA: 0x0038341C File Offset: 0x0038241C
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 15;
		int num = 22;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_404;
			case 1:
				this.ᜁ();
				num = 2;
				continue;
			case 2:
				goto IL_199;
			case 3:
				goto IL_110;
			case 4:
				num = 26;
				continue;
			case 5:
				if (A_2 == num2)
				{
					num = 30;
					continue;
				}
				A_1 += num2;
				A_2 -= num2;
				num = 0;
				continue;
			case 6:
				goto IL_3E0;
			case 7:
				if (this.ᜅ > 0)
				{
					num = 14;
					continue;
				}
				goto IL_2B5;
			case 8:
				if (A_0.Length - A_1 < A_2)
				{
					num = 3;
					continue;
				}
				num = 29;
				continue;
			case 9:
				if (A_2 == 0)
				{
					num = 16;
					continue;
				}
				num = 25;
				continue;
			case 10:
				this.ᜂ = new byte[this.ᜆ];
				num = 17;
				continue;
			case 11:
				goto IL_B9;
			case 12:
				num2 = A_2;
				num = 13;
				continue;
			case 13:
				goto IL_154;
			case 14:
				num2 = this.ᜆ - this.ᜅ;
				num = 20;
				continue;
			case 15:
				goto IL_2D7;
			case 16:
				return;
			case 17:
				goto IL_200;
			case 18:
				if (this.ᜃ < this.ᜄ)
				{
					num = 1;
					continue;
				}
				goto IL_199;
			case 19:
				goto IL_22B;
			case 20:
				if (num2 > 0)
				{
					num = 4;
					continue;
				}
				goto IL_404;
			case 21:
				if (!this.ᜁ.CanWrite)
				{
					num = 19;
					continue;
				}
				num = 18;
				continue;
			case 23:
				if (A_2 < 0)
				{
					num = 34;
					continue;
				}
				num = 8;
				continue;
			case 24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C0;
				default:
					goto IL_28F;
				}
				break;
			case 25:
				if (true)
				{
				}
				if (this.ᜂ == null)
				{
					num = 10;
					continue;
				}
				goto IL_444;
			case 26:
				if (num2 > A_2)
				{
					num = 12;
					continue;
				}
				goto IL_154;
			case 27:
				if (this.ᜅ == 0)
				{
					num = 31;
					continue;
				}
				goto IL_199;
			case 28:
				goto IL_2C0;
			case 29:
				if (this.ᜁ == null)
				{
					num = 24;
					continue;
				}
				num = 27;
				continue;
			case 30:
				return;
			case 31:
				num = 21;
				continue;
			case 32:
				if (A_1 < 0)
				{
					num = 6;
					continue;
				}
				num = 23;
				continue;
			case 33:
				goto IL_2B5;
			case 34:
				goto IL_14F;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 32;
			continue;
			IL_154:
			Buffer.BlockCopy(A_0, A_1, this.ᜂ, this.ᜅ, num2);
			this.ᜅ += num2;
			num = 5;
			continue;
			IL_199:
			num = 7;
			continue;
			IL_2C0:
			if (A_2 >= this.ᜆ)
			{
				num = 15;
				continue;
			}
			num = 9;
			continue;
			IL_2B5:
			num = 28;
			continue;
			IL_404:
			this.ᜁ.Write(this.ᜂ, 0, this.ᜅ);
			this.ᜇ = this.ᜁ.Position;
			this.ᜅ = 0;
			num = 33;
		}
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑄㕆㭈⩊㑌", a_));
		IL_110:
		throw new ArgumentException();
		IL_14F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("♄⡆㱈╊㥌", a_));
		IL_200:
		goto IL_444;
		IL_22B:
		throw new ArgumentException(RecordTableEnumerator.b("ᙄ㍆㭈⹊ⱌ≎煐㝒㩔㉖⩘筚㍜ぞᕠ䍢ᙤቦᥨ᭪ɬᵮհ卲ɴնၸེ᡼彾ﾊﾐ붒", a_));
		IL_28F:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙄㍆㭈⹊ⱌ≎", a_));
		IL_2D7:
		this.ᜁ.Write(A_0, A_1, A_2);
		this.ᜇ = this.ᜁ.Position;
		return;
		IL_3E0:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩄ⅆ⽈㡊⡌㭎", a_));
		IL_444:
		Buffer.BlockCopy(A_0, A_1, this.ᜂ, 0, A_2);
		this.ᜅ += A_2;
	}

	// Token: 0x06005879 RID: 22649 RVA: 0x0038388C File Offset: 0x0038288C
	public virtual void ᜀ(byte A_0)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (!this.ᜁ.CanWrite)
				{
					num = 3;
					continue;
				}
				num = 9;
				continue;
			case 1:
				goto IL_10A;
			case 2:
				goto IL_13E;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AB;
				default:
					goto IL_190;
				}
				break;
			case 4:
				this.ᜀ();
				num = 2;
				continue;
			case 6:
				goto IL_6D;
			case 7:
				num = 0;
				continue;
			case 8:
				if (this.ᜂ == null)
				{
					num = 10;
					continue;
				}
				goto IL_C3;
			case 9:
				if (this.ᜃ < this.ᜄ)
				{
					num = 14;
					continue;
				}
				goto IL_10A;
			case 10:
				this.ᜂ = new byte[this.ᜆ];
				num = 11;
				continue;
			case 11:
				goto IL_C3;
			case 12:
				if (this.ᜅ == this.ᜆ)
				{
					num = 4;
					continue;
				}
				goto IL_1C4;
			case 13:
				goto IL_AB;
			case 14:
				this.ᜁ();
				num = 1;
				continue;
			}
			if (this.ᜁ == null)
			{
				num = 6;
				continue;
			}
			num = 13;
			continue;
			IL_AB:
			if (this.ᜅ == 0)
			{
				num = 7;
				continue;
			}
			IL_C3:
			num = 12;
			continue;
			IL_10A:
			num = 8;
		}
		IL_6D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㥊⡌⹎㱐", a_));
		IL_13E:
		goto IL_1C4;
		IL_190:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("ᑆ㵈㥊⡌⹎㱐獒ㅔ㡖㱘⡚絜ㅞ๠ᝢ䕤ᑦᱨ᭪ᵬnͰݲ啴v୸ቺॼ᩾ꆀﮈ歷ﺐﶒ뮔", a_));
		IL_1C4:
		this.ᜂ[this.ᜅ++] = A_0;
	}

	// Token: 0x0600587A RID: 22650 RVA: 0x00383A78 File Offset: 0x00382A78
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
	{
		int a_ = 9;
		int num = 19;
		long num3;
		for (;;)
		{
			long num2;
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				goto IL_219;
			case 2:
				goto IL_DF;
			case 3:
				this.ᜀ();
				num = 17;
				continue;
			case 4:
				goto IL_306;
			case 5:
				this.ᜁ.Seek((long)this.ᜄ, SeekOrigin.Current);
				num = 2;
				continue;
			case 6:
				if (this.ᜄ > 0)
				{
					if (true)
					{
					}
					num = 23;
					continue;
				}
				goto IL_3D9;
			case 7:
				Buffer.BlockCopy(this.ᜂ, this.ᜃ, this.ᜂ, 0, this.ᜄ - this.ᜃ);
				this.ᜄ -= this.ᜃ;
				this.ᜃ = 0;
				num = 18;
				continue;
			case 8:
				if (this.ᜃ > 0)
				{
					num = 7;
					continue;
				}
				goto IL_1D3;
			case 9:
				if (this.ᜅ > 0)
				{
					num = 3;
					continue;
				}
				num = 14;
				continue;
			case 10:
				if (!this.ᜁ.CanSeek)
				{
					num = 27;
					continue;
				}
				num = 9;
				continue;
			case 11:
				A_0 -= (long)(this.ᜄ - this.ᜃ);
				num = 4;
				continue;
			case 12:
				num = 26;
				continue;
			case 13:
				if (num2 == num3)
				{
					num = 21;
					continue;
				}
				num = 25;
				continue;
			case 14:
				if (A_1 == SeekOrigin.Current)
				{
					num = 11;
					continue;
				}
				goto IL_306;
			case 15:
				goto IL_9F;
			case 16:
				goto IL_29E;
			case 17:
				goto IL_306;
			case 18:
				goto IL_1D3;
			case 20:
				if (this.ᜄ > 0)
				{
					num = 5;
					continue;
				}
				goto IL_3D9;
			case 21:
				num = 8;
				continue;
			case 22:
			{
				int num4 = (int)(num3 - num2);
				Buffer.BlockCopy(this.ᜂ, this.ᜃ + num4, this.ᜂ, 0, this.ᜄ - (this.ᜃ + num4));
				this.ᜄ -= this.ᜃ + num4;
				this.ᜃ = 0;
				num = 6;
				continue;
			}
			case 23:
				this.ᜁ.Seek((long)this.ᜄ, SeekOrigin.Current);
				num = 1;
				continue;
			case 24:
				if (this.ᜄ > 0)
				{
					num = 0;
					continue;
				}
				goto IL_3D9;
			case 25:
				if (num2 - (long)this.ᜃ < num3)
				{
					num = 12;
					continue;
				}
				goto IL_285;
			case 26:
				if (num3 < num2 + (long)this.ᜄ - (long)this.ᜃ)
				{
					num = 22;
					continue;
				}
				goto IL_285;
			case 27:
				goto IL_1CE;
			}
			if (this.ᜁ == null)
			{
				num = 15;
				continue;
			}
			num = 10;
			continue;
			IL_1D3:
			num = 20;
			continue;
			IL_285:
			this.ᜃ = 0;
			this.ᜄ = 0;
			num = 16;
			continue;
			IL_306:
			num2 = this.ᜁ.Position + (long)(this.ᜃ - this.ᜄ);
			num3 = this.ᜁ.Seek(A_0, A_1);
			num = 24;
		}
		IL_9F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_DF:
			goto IL_3D9;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈", a_));
		}
		IL_1CE:
		throw new ArgumentException(RecordTableEnumerator.b("氾㕀ㅂ⁄♆⑈歊⥌⁎㑐⁒畔㥖㙘⽚絜ⱞᑠ።ᕤࡦ᭨Ὢ䵬㱮ᑰᙲṴ坶ᙸ୺᡼ൾꖊ", a_));
		IL_219:
		IL_29E:
		IL_3D9:
		this.ᜇ = num3;
		return num3;
	}

	// Token: 0x0600587B RID: 22651 RVA: 0x00383E68 File Offset: 0x00382E68
	public virtual void ᜃ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.Flush();
				this.ᜁ.Close();
				num = 2;
				continue;
			case 2:
				goto IL_6B;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6D;
			default:
				if (false)
				{
				}
				if (this.ᜁ == null)
				{
					goto IL_6D;
				}
				num = 0;
				break;
			}
		}
		IL_6B:
		IL_6D:
		this.ᜁ = null;
		this.ᜂ = null;
	}

	// Token: 0x0600587C RID: 22652 RVA: 0x00383EFC File Offset: 0x00382EFC
	public virtual void ᜈ()
	{
		int a_ = 7;
		for (;;)
		{
			IL_09:
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4D;
				case 1:
					if (this.ᜃ < this.ᜄ)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					if (this.ᜁ.CanSeek)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 3:
					this.ᜁ();
					num = 6;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_EC;
				case 6:
					goto IL_A5;
				case 8:
					if (this.ᜅ > 0)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				}
				if (this.ᜁ == null)
				{
					num = 0;
				}
				else
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					}
					if (false)
					{
					}
					num = 8;
				}
			}
		}
		IL_4D:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀♂⑄⩆", a_));
		IL_A5:
		return;
		IL_EC:
		this.ᜀ();
	}

	// Token: 0x0600587D RID: 22653 RVA: 0x00384020 File Offset: 0x00383020
	private void ᜁ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 1:
				this.ᜁ.Seek((long)(this.ᜃ - this.ᜄ), SeekOrigin.Current);
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_7E;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜃ - this.ᜄ == 0)
				{
					goto IL_7E;
				}
				num = 1;
				break;
			}
		}
		IL_7C:
		IL_7E:
		this.ᜃ = 0;
		this.ᜄ = 0;
	}

	// Token: 0x0600587E RID: 22654 RVA: 0x003840C4 File Offset: 0x003830C4
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

	// Token: 0x0600587F RID: 22655 RVA: 0x0038413C File Offset: 0x0038313C
	public virtual void ᜁ(long A_0)
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜅ > 0)
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 1:
				goto IL_137;
			case 2:
				if (!this.ᜁ.CanWrite)
				{
					num = 11;
					continue;
				}
				num = 0;
				continue;
			case 3:
				if (this.ᜃ < this.ᜄ)
				{
					num = 10;
					continue;
				}
				goto IL_1C7;
			case 4:
				if (this.ᜁ == null)
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
			case 6:
				goto IL_121;
			case 7:
				this.ᜀ();
				num = 6;
				continue;
			case 8:
				goto IL_D1;
			case 9:
				goto IL_1C2;
			case 10:
				this.ᜁ();
				num = 1;
				continue;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					goto IL_17A;
				}
				break;
			case 12:
				if (!this.ᜁ.CanSeek)
				{
					num = 9;
					continue;
				}
				num = 2;
				continue;
			case 13:
				goto IL_68;
			}
			if (A_0 < 0L)
			{
				num = 13;
				continue;
			}
			IL_B0:
			num = 4;
		}
		IL_68:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
		IL_D1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂㝄≆⡈♊", a_));
		IL_121:
		IL_137:
		goto IL_1C7;
		IL_17A:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄≆⡈♊浌⭎㹐㙒♔睖㝘㑚⥜罞በᙢᕤᝦ٨ᥪᥬ佮♰ŲᱴͶᱸ孺ቼཾꆎ", a_));
		IL_1C2:
		throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄≆⡈♊浌⭎㹐㙒♔睖㝘㑚⥜罞በᙢᕤᝦ٨ᥪᥬ佮≰ᙲၴᱶ奸ᑺർ᩾ꎌ", a_));
		IL_1C7:
		this.ᜁ.SetLength(A_0);
	}

	// Token: 0x04002A0B RID: 10763
	private const int ᜀ = 4096;

	// Token: 0x04002A0C RID: 10764
	private Stream ᜁ;

	// Token: 0x04002A0D RID: 10765
	private byte[] ᜂ;

	// Token: 0x04002A0E RID: 10766
	private int ᜃ;

	// Token: 0x04002A0F RID: 10767
	private int ᜄ;

	// Token: 0x04002A10 RID: 10768
	private int ᜅ;

	// Token: 0x04002A11 RID: 10769
	private int ᜆ;

	// Token: 0x04002A12 RID: 10770
	private long ᜇ;
}
