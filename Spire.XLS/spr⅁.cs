using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200056F RID: 1391
[spr\u2593(TBIFFRecord.ExternName)]
[CLSCompliant(false)]
internal class spr\u2141 : spr\u251F
{
	// Token: 0x06005394 RID: 21396 RVA: 0x003408EC File Offset: 0x0033F8EC
	public ushort ᜇ()
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
		return (ushort)this.ᜀ;
	}

	// Token: 0x06005395 RID: 21397 RVA: 0x00340930 File Offset: 0x0033F930
	public void ᜁ(ushort A_0)
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
		this.ᜀ = (spr\u2141.OptionFlags)A_0;
	}

	// Token: 0x06005396 RID: 21398 RVA: 0x00340974 File Offset: 0x0033F974
	public ushort ᜋ()
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
		return this.ᜁ;
	}

	// Token: 0x06005397 RID: 21399 RVA: 0x003409B8 File Offset: 0x0033F9B8
	public ushort ᜎ()
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

	// Token: 0x06005398 RID: 21400 RVA: 0x003409FC File Offset: 0x0033F9FC
	public byte[] ᜁ()
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

	// Token: 0x06005399 RID: 21401 RVA: 0x00340A40 File Offset: 0x0033FA40
	public string ᜌ()
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

	// Token: 0x0600539A RID: 21402 RVA: 0x00340A84 File Offset: 0x0033FA84
	public new void ᜀ(string A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x0600539B RID: 21403 RVA: 0x00340AC8 File Offset: 0x0033FAC8
	public virtual int ᜊ()
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
		return 0;
	}

	// Token: 0x0600539C RID: 21404 RVA: 0x00340B04 File Offset: 0x0033FB04
	public ushort ᜆ()
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
		return this.ᜄ;
	}

	// Token: 0x0600539D RID: 21405 RVA: 0x00340B48 File Offset: 0x0033FB48
	public new void ᜀ(ushort A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x0600539E RID: 21406 RVA: 0x00340B8C File Offset: 0x0033FB8C
	public virtual bool \u170D()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜀ == (spr\u2141.OptionFlags)0)
			{
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_28;
			}
		}
		IL_28:
		if (false)
		{
		}
		return this.ᜀ != spr\u2141.OptionFlags.BuiltIn;
	}

	// Token: 0x0600539F RID: 21407 RVA: 0x00340BE0 File Offset: 0x0033FBE0
	public bool ᜉ()
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
		return (this.ᜀ & spr\u2141.OptionFlags.BuiltIn) != (spr\u2141.OptionFlags)0;
	}

	// Token: 0x060053A0 RID: 21408 RVA: 0x00340C2C File Offset: 0x0033FC2C
	public void ᜂ(bool A_0)
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
		this.ᜀ(spr\u2141.OptionFlags.BuiltIn, A_0);
	}

	// Token: 0x060053A1 RID: 21409 RVA: 0x00340C70 File Offset: 0x0033FC70
	public bool ᜅ()
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
		return (this.ᜀ & spr\u2141.OptionFlags.WantAdvise) != (spr\u2141.OptionFlags)0;
	}

	// Token: 0x060053A2 RID: 21410 RVA: 0x00340CBC File Offset: 0x0033FCBC
	public new void ᜀ(bool A_0)
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
		this.ᜀ(spr\u2141.OptionFlags.WantAdvise, A_0);
	}

	// Token: 0x060053A3 RID: 21411 RVA: 0x00340D00 File Offset: 0x0033FD00
	public bool ᜄ()
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
		return (this.ᜀ & spr\u2141.OptionFlags.WantPicture) != (spr\u2141.OptionFlags)0;
	}

	// Token: 0x060053A4 RID: 21412 RVA: 0x00340D4C File Offset: 0x0033FD4C
	public void ᜁ(bool A_0)
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
		this.ᜀ(spr\u2141.OptionFlags.WantPicture, A_0);
	}

	// Token: 0x060053A5 RID: 21413 RVA: 0x00340D90 File Offset: 0x0033FD90
	public bool ᜈ()
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
		return (this.ᜀ & spr\u2141.OptionFlags.Ole) != (spr\u2141.OptionFlags)0;
	}

	// Token: 0x060053A6 RID: 21414 RVA: 0x00340DDC File Offset: 0x0033FDDC
	public new void ᜃ(bool A_0)
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
		this.ᜀ(spr\u2141.OptionFlags.Ole, A_0);
	}

	// Token: 0x060053A7 RID: 21415 RVA: 0x00340E20 File Offset: 0x0033FE20
	public new bool ᜃ()
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
		return (this.ᜀ & spr\u2141.OptionFlags.OleLink) != (spr\u2141.OptionFlags)0;
	}

	// Token: 0x060053A8 RID: 21416 RVA: 0x00340E6C File Offset: 0x0033FE6C
	public void ᜄ(bool A_0)
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
		this.ᜀ(spr\u2141.OptionFlags.OleLink, A_0);
	}

	// Token: 0x060053A9 RID: 21417 RVA: 0x00340EB0 File Offset: 0x0033FEB0
	private new void ᜀ(spr\u2141.OptionFlags A_0, bool A_1)
	{
		while (A_1)
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
				this.ᜀ |= A_0;
				return;
			}
		}
		this.ᜀ &= ~A_0;
	}

	// Token: 0x060053AA RID: 21418 RVA: 0x00340F10 File Offset: 0x0033FF10
	public spr\u2141()
	{
	}

	// Token: 0x060053AB RID: 21419 RVA: 0x00340F30 File Offset: 0x0033FF30
	public spr\u2141(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060053AC RID: 21420 RVA: 0x00340F50 File Offset: 0x0033FF50
	public spr\u2141(int A_0) : base(A_0)
	{
	}

	// Token: 0x060053AD RID: 21421 RVA: 0x00340F70 File Offset: 0x0033FF70
	public override void ᜂ()
	{
		int num;
		for (;;)
		{
			num = 0;
			this.ᜀ = (spr\u2141.OptionFlags)base.ᜌ(num);
			num += 2;
			this.ᜁ = base.ᜌ(num);
			num += 2;
			this.ᜂ = base.ᜌ(num);
			num += 2;
			int num2;
			this.ᜃ = base.ᜀ(num, out num2);
			num += num2 + 2;
			int num3 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					goto IL_F3;
				case 1:
					if (this.ᜉ())
					{
						num3 = 2;
						continue;
					}
					goto IL_AB;
				case 2:
					this.ᜄ = base.ᜌ(num);
					num += 2;
					num3 = 5;
					continue;
				case 3:
					this.ᜄ = (ushort)(this.m_iLength - num);
					num3 = 0;
					continue;
				case 4:
					if (!this.ᜃ())
					{
						num3 = 3;
						continue;
					}
					goto IL_F3;
				case 5:
					goto IL_F3;
				}
				break;
				IL_AB:
				num3 = 4;
				continue;
				IL_F3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AB;
				default:
					goto IL_109;
				}
			}
		}
		IL_109:
		if (false)
		{
		}
		this.ᜅ = new byte[(int)this.ᜄ];
		Buffer.BlockCopy(this.ᜀ, num, this.ᜅ, 0, (int)this.ᜄ);
		num += (int)this.ᜄ;
	}

	// Token: 0x060053AE RID: 21422 RVA: 0x003410C0 File Offset: 0x003400C0
	public override void ᜀ(ExcelVersion A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1E3;
			case 1:
				if (!this.ᜉ())
				{
					num = 14;
					continue;
				}
				goto IL_1E5;
			case 2:
				base.ᜀ(this.m_iLength, this.ᜅ, 0, (int)this.ᜄ);
				this.m_iLength += (int)this.ᜄ;
				num = 9;
				continue;
			case 4:
				if (this.ᜄ == 0)
				{
					num = 5;
					continue;
				}
				return;
			case 5:
				goto IL_B4;
			case 6:
				if (this.ᜀ != (spr\u2141.OptionFlags)0)
				{
					num = 12;
					continue;
				}
				goto IL_70;
			case 7:
				if (true)
				{
				}
				if (this.ᜀ == (spr\u2141.OptionFlags)0)
				{
					goto IL_B4;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E3;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				break;
			case 8:
				goto IL_70;
			case 9:
				goto IL_1AA;
			case 10:
				return;
			case 11:
				num = 1;
				continue;
			case 12:
				num = 15;
				continue;
			case 13:
				num = 17;
				continue;
			case 14:
				goto IL_184;
			case 15:
				if (this.ᜀ == spr\u2141.OptionFlags.BuiltIn)
				{
					num = 8;
					continue;
				}
				goto IL_1AA;
			case 16:
				if (this.ᜄ > 0)
				{
					num = 2;
					continue;
				}
				goto IL_1AA;
			case 17:
				if (this.ᜀ == spr\u2141.OptionFlags.BuiltIn)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (!this.ᜃ())
			{
				num = 11;
				continue;
			}
			goto IL_1E5;
			IL_70:
			base.ᜀ(this.m_iLength, this.ᜄ);
			this.m_iLength += 2;
			num = 16;
			continue;
			IL_B4:
			bool autoGrowData = this.AutoGrowData;
			this.AutoGrowData = true;
			base.ᜀ(0, (ushort)this.ᜀ);
			base.ᜀ(2, this.ᜁ);
			base.ᜀ(4, this.ᜂ);
			this.m_iLength = 6;
			base.ᜀ(ref this.m_iLength, this.ᜃ);
			num = 6;
			continue;
			IL_1AA:
			this.AutoGrowData = autoGrowData;
			num = 10;
			continue;
			IL_1E3:
			num = 4;
			continue;
			IL_1E5:
			num = 7;
		}
		IL_184:
		this.ᜀ();
	}

	// Token: 0x060053AF RID: 21423 RVA: 0x0034132C File Offset: 0x0034032C
	private new void ᜀ()
	{
		bool autoGrowData;
		for (;;)
		{
			autoGrowData = this.AutoGrowData;
			this.AutoGrowData = true;
			this.m_iLength = 0;
			base.ᜀ(this.m_iLength, (ushort)this.ᜀ);
			this.m_iLength += 2;
			base.ᜆ(this.m_iLength, 0);
			this.m_iLength += 4;
			this.m_iLength += base.ᜀ(this.m_iLength, this.ᜃ);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					IL_AA:
					num = 1;
					continue;
				case 1:
					if (this.ᜅ.Length > 0)
					{
						num = 2;
						continue;
					}
					goto IL_103;
				case 2:
					base.ᜁ(this.m_iLength, this.ᜅ);
					this.m_iLength += this.ᜅ.Length;
					num = 4;
					continue;
				case 3:
					if (this.ᜅ != null)
					{
						num = 0;
						continue;
					}
					goto IL_103;
				case 4:
					goto IL_103;
				}
				break;
				IL_103:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AA;
				default:
					goto IL_119;
				}
			}
		}
		IL_119:
		if (false)
		{
		}
		if (true)
		{
		}
		this.AutoGrowData = autoGrowData;
	}

	// Token: 0x04002715 RID: 10005
	[spr\u2429(0, 2)]
	private new spr\u2141.OptionFlags ᜀ;

	// Token: 0x04002716 RID: 10006
	[spr\u2429(2, 2)]
	private new ushort ᜁ;

	// Token: 0x04002717 RID: 10007
	[spr\u2429(4, 2)]
	private new ushort ᜂ;

	// Token: 0x04002718 RID: 10008
	[spr\u2429(6, TFieldType.String)]
	private new string ᜃ = string.Empty;

	// Token: 0x04002719 RID: 10009
	private new ushort ᜄ;

	// Token: 0x0400271A RID: 10010
	private new byte[] ᜅ;

	// Token: 0x02000570 RID: 1392
	[Flags]
	private enum OptionFlags
	{
		// Token: 0x0400271C RID: 10012
		BuiltIn = 1,
		// Token: 0x0400271D RID: 10013
		WantAdvise = 2,
		// Token: 0x0400271E RID: 10014
		WantPicture = 4,
		// Token: 0x0400271F RID: 10015
		Ole = 8,
		// Token: 0x04002720 RID: 10016
		OleLink = 16
	}
}
