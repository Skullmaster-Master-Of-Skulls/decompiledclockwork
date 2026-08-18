using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200044F RID: 1103
[spr\u2593(TBIFFRecord.ExtendedFormat)]
[CLSCompliant(false)]
internal class sprỶ : BiffRecordRaw
{
	// Token: 0x0600424A RID: 16970 RVA: 0x002523B8 File Offset: 0x002513B8
	public int \u171C()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num |= (this.\u171D ? 1 : 0);
					num <<= 1;
					if (true)
					{
					}
					num2 = 2;
					continue;
				case 1:
					num |= (this.ᜦ ? 1 : 0);
					num <<= 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_49;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 2:
					goto IL_49;
				case 3:
					goto IL_B0;
				}
				break;
				IL_49:
				num |= (this.\u171E ? 1 : 0);
				num <<= 1;
				num2 = 1;
			}
		}
		IL_B0:
		num |= (this.ᜥ ? 1 : 0);
		return num << 1;
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x00252490 File Offset: 0x00251490
	public int ᜠ()
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
		int num = 0;
		num |= (this.ᜰ ? 1 : 0);
		num <<= 1;
		num |= (this.ᜱ ? 1 : 0);
		num <<= 15;
		return num | (int)this.ᜮ;
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x00252508 File Offset: 0x00251508
	public int ᜪ()
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
		return (int)this.ᜡ;
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x0025254C File Offset: 0x0025154C
	public ushort \u171D()
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
		return this.\u171A;
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x00252590 File Offset: 0x00251590
	public void ᜉ(ushort A_0)
	{
		int a_ = 17;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_51;
		}
		if (false)
		{
		}
		if (A_0 == 4)
		{
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("ņ♈╊㥌َ㽐㝒ご⽖祘㙚⡜ⱞᕠ䍢ݤɦ䥨ݪ࡬ᱮɰ卲Ŵὶᡸᕺ嵼ၾꎂﶎ놐ﶔ뮚ꦜ뾞잠첢힤螦펪\ud9ac쪮\udfb0ힲ킴펶ﾸ풺쾼튾ꃀ럂韄ꋆ꫈꓊뿌ꯎꋐ﷒", a_));
		}
		IL_51:
		this.\u1734 = false;
		this.\u171A = A_0;
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x002525FC File Offset: 0x002515FC
	public ushort ᜂ()
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
		return this.\u171B;
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x00252640 File Offset: 0x00251640
	public void ᜈ(ushort A_0)
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
		this.\u1734 = false;
		this.\u171B = A_0;
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x0025268C File Offset: 0x0025168C
	public bool \u171F()
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
		return this.\u171D;
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x002526D0 File Offset: 0x002516D0
	public void ᜎ(bool A_0)
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
		this.\u1734 = false;
		this.\u171D = A_0;
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x0025271C File Offset: 0x0025171C
	public bool ᜄ()
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
		return this.\u171E;
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x00252760 File Offset: 0x00251760
	public void ᜂ(bool A_0)
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
		this.\u1734 = false;
		this.\u171E = A_0;
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x002527AC File Offset: 0x002517AC
	public sprỶ.TXFType ᜎ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return sprỶ.TXFType.XF_CELL;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (!this.\u171F)
		{
			return sprỶ.TXFType.XF_STYLE;
		}
		return sprỶ.TXFType.XF_CELL;
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x002527F4 File Offset: 0x002517F4
	public void ᜀ(sprỶ.TXFType A_0)
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
		this.\u1734 = false;
		this.\u171F = (A_0 == sprỶ.TXFType.XF_CELL);
	}

	// Token: 0x06004257 RID: 16983 RVA: 0x00252848 File Offset: 0x00251848
	public bool ᜫ()
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
		return this.ᜠ;
	}

	// Token: 0x06004258 RID: 16984 RVA: 0x0025288C File Offset: 0x0025188C
	public void ᜇ(bool A_0)
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
		this.\u1734 = false;
		this.ᜠ = A_0;
	}

	// Token: 0x06004259 RID: 16985 RVA: 0x002528D8 File Offset: 0x002518D8
	private ushort ᜁ()
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
		return this.\u1736;
	}

	// Token: 0x0600425A RID: 16986 RVA: 0x0025291C File Offset: 0x0025191C
	private void ᜀ(ushort A_0)
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
		this.\u1736 = A_0;
		this.\u1737 = true;
	}

	// Token: 0x0600425B RID: 16987 RVA: 0x00252968 File Offset: 0x00251968
	public bool ᜢ()
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
		return this.\u1737;
	}

	// Token: 0x0600425C RID: 16988 RVA: 0x002529AC File Offset: 0x002519AC
	public ushort \u1713()
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
		return this.ᜁ();
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x002529F0 File Offset: 0x002519F0
	public void ᜇ(ushort A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x00252A34 File Offset: 0x00251A34
	public bool ᜦ()
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
		return this.ᜢ;
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x00252A78 File Offset: 0x00251A78
	public void ᜈ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.\u1734 = false;
				this.ᜢ = A_0;
				base.SetBitInVar(ref this.ᜡ, this.ᜢ, 3);
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
					num = 1;
					continue;
				}
				break;
			case 1:
				return;
			}
			IL_1C:
			if (this.ᜢ != A_0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x06004260 RID: 16992 RVA: 0x00252B10 File Offset: 0x00251B10
	public bool ᜡ()
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
		return this.ᜣ;
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x00252B54 File Offset: 0x00251B54
	public void ᜄ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				this.\u1734 = false;
				this.ᜣ = A_0;
				base.SetBitInVar(ref this.ᜡ, this.ᜣ, 7);
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
					num = 1;
					continue;
				}
				break;
			}
			IL_1C:
			if (this.ᜣ != A_0)
			{
				num = 2;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x06004262 RID: 16994 RVA: 0x00252BEC File Offset: 0x00251BEC
	public byte ᜏ()
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
		return this.ᜭ;
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x00252C30 File Offset: 0x00251C30
	public void ᜀ(byte A_0)
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
		this.\u1734 = false;
		this.ᜭ = A_0;
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x00252C7C File Offset: 0x00251C7C
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
		return this.ᜥ;
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x00252CC0 File Offset: 0x00251CC0
	public void ᜌ(bool A_0)
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
		this.\u1734 = false;
		this.ᜥ = A_0;
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x00252D0C File Offset: 0x00251D0C
	public bool \u170D()
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
		return this.ᜦ;
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x00252D50 File Offset: 0x00251D50
	public void ᜀ(bool A_0)
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
		this.\u1734 = false;
		this.ᜦ = A_0;
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x00252D9C File Offset: 0x00251D9C
	public ushort \u171E()
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
		return (ushort)(BiffRecordRaw.ᜀ(this.ᜤ, 192) >> 6);
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x00252DEC File Offset: 0x00251DEC
	public void ᜋ(ushort A_0)
	{
		int a_ = 14;
		if (A_0 <= 3)
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
				this.\u1734 = false;
				BiffRecordRaw.ᜀ(ref this.ᜤ, 192, (ushort)(A_0 << 6));
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᙃ⍅⥇⹉╋⁍㝏牑᭓⑕㱗㽙⹛", a_));
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x00252E68 File Offset: 0x00251E68
	public ushort ᜣ()
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
		return (ushort)(BiffRecordRaw.ᜀ(this.ᜡ, 65280) >> 8);
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x00252EB8 File Offset: 0x00251EB8
	public void ᜁ(ushort A_0)
	{
		int a_ = 5;
		if (A_0 <= 255)
		{
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
				this.\u1734 = false;
				BiffRecordRaw.ᜀ(ref this.ᜡ, 65280, (ushort)(A_0 << 8));
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("椺刼䬾⁀㝂ⱄ⡆❈", a_));
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x00252F38 File Offset: 0x00251F38
	public bool \u1715()
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
		return this.ᜧ;
	}

	// Token: 0x0600426D RID: 17005 RVA: 0x00252F7C File Offset: 0x00251F7C
	public void ᜁ(bool A_0)
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
		this.\u1734 = false;
		this.ᜧ = A_0;
	}

	// Token: 0x0600426E RID: 17006 RVA: 0x00252FC8 File Offset: 0x00251FC8
	public new bool ᜃ()
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
		return this.ᜨ;
	}

	// Token: 0x0600426F RID: 17007 RVA: 0x0025300C File Offset: 0x0025200C
	public new void ᜃ(bool A_0)
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
		this.\u1734 = false;
		this.ᜨ = A_0;
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x00253058 File Offset: 0x00252058
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
		return this.ᜩ;
	}

	// Token: 0x06004271 RID: 17009 RVA: 0x0025309C File Offset: 0x0025209C
	public void \u170D(bool A_0)
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
		this.\u1734 = false;
		this.ᜩ = A_0;
	}

	// Token: 0x06004272 RID: 17010 RVA: 0x002530E8 File Offset: 0x002520E8
	public bool ᜥ()
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
		return this.ᜪ;
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x0025312C File Offset: 0x0025212C
	public void ᜆ(bool A_0)
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
		this.\u1734 = false;
		this.ᜪ = A_0;
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x00253178 File Offset: 0x00252178
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
		return this.ᜫ;
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x002531BC File Offset: 0x002521BC
	public void ᜋ(bool A_0)
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
		this.\u1734 = false;
		this.ᜫ = A_0;
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x00253208 File Offset: 0x00252208
	public bool \u1716()
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
		return this.ᜬ;
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x0025324C File Offset: 0x0025224C
	public void ᜅ(bool A_0)
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
		this.\u1734 = false;
		this.ᜬ = A_0;
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x00253298 File Offset: 0x00252298
	public ushort ᜅ()
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
		return (ushort)(this.\u1732 & 127U);
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x002532E0 File Offset: 0x002522E0
	public void ᜅ(ushort A_0)
	{
		if (A_0 <= 127)
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
				this.\u1734 = false;
				this.\u1732 &= 4294967168U;
				this.\u1732 += (uint)A_0;
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x0025334C File Offset: 0x0025234C
	public ushort ᜨ()
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
		return (ushort)((this.\u1732 & 16256U) >> 7);
	}

	// Token: 0x0600427B RID: 17019 RVA: 0x00253398 File Offset: 0x00252398
	public new void ᜃ(ushort A_0)
	{
		if (A_0 <= 127)
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
				this.\u1734 = false;
				this.\u1732 &= 4294951039U;
				this.\u1732 += (uint)((uint)A_0 << 7);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x0600427C RID: 17020 RVA: 0x0025340C File Offset: 0x0025240C
	public ushort \u1717()
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
		return this.ᜯ & 127;
	}

	// Token: 0x0600427D RID: 17021 RVA: 0x00253454 File Offset: 0x00252454
	public void ᜄ(ushort A_0)
	{
		if (A_0 <= 127)
		{
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
				this.\u1734 = false;
				BiffRecordRaw.ᜀ(ref this.ᜯ, 127, A_0);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x0600427E RID: 17022 RVA: 0x002534B4 File Offset: 0x002524B4
	public ushort ᜩ()
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
		return (ushort)(BiffRecordRaw.ᜀ(this.ᜯ, 16256) >> 7);
	}

	// Token: 0x0600427F RID: 17023 RVA: 0x00253504 File Offset: 0x00252504
	public void \u170D(ushort A_0)
	{
		if (A_0 <= 127)
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
				this.\u1734 = false;
				BiffRecordRaw.ᜀ(ref this.ᜯ, 16256, (ushort)(A_0 << 7));
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06004280 RID: 17024 RVA: 0x00253568 File Offset: 0x00252568
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
		return (ushort)((this.\u1732 & 2080768U) >> 14);
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x002535B4 File Offset: 0x002525B4
	public void ᜂ(ushort A_0)
	{
		if (A_0 <= 127)
		{
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
				this.\u1734 = false;
				this.\u1732 &= 4292886527U;
				this.\u1732 |= (uint)((uint)A_0 << 14);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x00253628 File Offset: 0x00252628
	public ushort ᜤ()
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
		return (ushort)((this.\u1732 & 31457280U) >> 21);
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x00253674 File Offset: 0x00252674
	public void ᜆ(ushort A_0)
	{
		if (true)
		{
		}
		if (A_0 <= 15)
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
				this.\u1734 = false;
				this.\u1732 &= 4263510015U;
				this.\u1732 |= (uint)((uint)A_0 << 21);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x002536E8 File Offset: 0x002526E8
	public bool \u1719()
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
		return this.ᜰ;
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x0025372C File Offset: 0x0025272C
	public void ᜊ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.\u1734 = false;
				this.ᜰ = A_0;
				base.SetBitInVar(ref this.ᜯ, this.ᜰ, 14);
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
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			IL_1C:
			if (this.ᜰ != A_0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x002537C4 File Offset: 0x002527C4
	public bool \u1714()
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
		return this.ᜱ;
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x00253808 File Offset: 0x00252808
	public void ᜉ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.\u1734 = false;
				this.ᜱ = A_0;
				base.SetBitInVar(ref this.ᜯ, this.ᜱ, 15);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			IL_1C:
			if (this.ᜱ != A_0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x002538A0 File Offset: 0x002528A0
	public ushort ᜧ()
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
		return this.\u1738;
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x002538E4 File Offset: 0x002528E4
	public void ᜌ(ushort A_0)
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
		this.\u1738 = A_0;
	}

	// Token: 0x0600428A RID: 17034 RVA: 0x00253928 File Offset: 0x00252928
	public LineStyleType ᜈ()
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
		return (LineStyleType)BiffRecordRaw.ᜀ(this.ᜮ, 15);
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x00253970 File Offset: 0x00252970
	public new void ᜃ(LineStyleType A_0)
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
		this.\u1734 = false;
		BiffRecordRaw.ᜀ(ref this.ᜮ, 15, (ushort)A_0);
	}

	// Token: 0x0600428C RID: 17036 RVA: 0x002539C4 File Offset: 0x002529C4
	public LineStyleType ᜭ()
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
		return (LineStyleType)(BiffRecordRaw.ᜀ(this.ᜮ, 240) >> 4);
	}

	// Token: 0x0600428D RID: 17037 RVA: 0x00253A14 File Offset: 0x00252A14
	public void ᜂ(LineStyleType A_0)
	{
		for (;;)
		{
			this.\u1734 = false;
			BiffRecordRaw.ᜀ(ref this.ᜮ, 240, (ushort)((ushort)A_0 << 4));
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
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
						this.\u170D(64);
						num = 4;
						continue;
					}
					break;
				case 1:
					num = 3;
					continue;
				case 2:
					if (A_0 != LineStyleType.None)
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					if (this.ᜯ == 0)
					{
						num = 0;
						continue;
					}
					return;
				case 4:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x0600428E RID: 17038 RVA: 0x00253AC8 File Offset: 0x00252AC8
	public LineStyleType ᜐ()
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
		return (LineStyleType)(BiffRecordRaw.ᜀ(this.ᜮ, 3840) >> 8);
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x00253B18 File Offset: 0x00252B18
	public void ᜁ(LineStyleType A_0)
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
		this.\u1734 = false;
		BiffRecordRaw.ᜀ(ref this.ᜮ, 3840, (ushort)((ushort)A_0 << 8));
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x00253B70 File Offset: 0x00252B70
	public LineStyleType ᜋ()
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
		return (LineStyleType)(BiffRecordRaw.ᜀ(this.ᜮ, 61440) >> 12);
	}

	// Token: 0x06004291 RID: 17041 RVA: 0x00253BC0 File Offset: 0x00252BC0
	public void ᜀ(LineStyleType A_0)
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
		this.\u1734 = false;
		BiffRecordRaw.ᜀ(ref this.ᜮ, 61440, (ushort)((ushort)A_0 << 12));
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x00253C18 File Offset: 0x00252C18
	public HorizontalAlignType ᜊ()
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
		return (HorizontalAlignType)BiffRecordRaw.ᜀ(this.ᜡ, 7);
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x00253C60 File Offset: 0x00252C60
	public void ᜀ(HorizontalAlignType A_0)
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
		this.\u1734 = false;
		BiffRecordRaw.ᜀ(ref this.ᜡ, 7, (ushort)A_0);
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x00253CB0 File Offset: 0x00252CB0
	public VerticalAlignType \u171A()
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
		return (VerticalAlignType)(BiffRecordRaw.ᜀ(this.ᜡ, 112) >> 4);
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x00253CFC File Offset: 0x00252CFC
	public void ᜀ(VerticalAlignType A_0)
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
		this.\u1734 = false;
		BiffRecordRaw.ᜀ(ref this.ᜡ, 112, (ushort)((ushort)A_0 << 4));
	}

	// Token: 0x06004296 RID: 17046 RVA: 0x00253D50 File Offset: 0x00252D50
	public ushort \u1712()
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
		return BiffRecordRaw.ᜀ(this.\u1733, 127);
	}

	// Token: 0x06004297 RID: 17047 RVA: 0x00253D98 File Offset: 0x00252D98
	public void ᜎ(ushort A_0)
	{
		if (A_0 <= 127)
		{
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
				this.\u1734 = false;
				BiffRecordRaw.ᜀ(ref this.\u1733, 127, A_0);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06004298 RID: 17048 RVA: 0x00253DF8 File Offset: 0x00252DF8
	public ushort ᜬ()
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
		return (ushort)((this.\u1733 & 16256) >> 7);
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x00253E44 File Offset: 0x00252E44
	public void ᜊ(ushort A_0)
	{
		int a_ = 11;
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
			if (A_0 <= 127)
			{
				this.\u1734 = false;
				this.\u1733 &= 49279;
				this.\u1733 |= (ushort)(A_0 << 7);
				return;
			}
			break;
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("݀⩂⥄⭆཈⑊㽌⩎㙐⅒㩔≖㝘㽚", a_), RecordTableEnumerator.b("@ㅂ≄㉆⑈⹊⍌㭎煐㩒♔睖ⵘ㑚㉜罞ൠɢᝤf౨", a_));
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x00253EE0 File Offset: 0x00252EE0
	public virtual int \u1718()
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
		return 20;
	}

	// Token: 0x0600429B RID: 17051 RVA: 0x00253F20 File Offset: 0x00252F20
	public virtual int \u171B()
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
		return 20;
	}

	// Token: 0x0600429C RID: 17052 RVA: 0x00253F60 File Offset: 0x00252F60
	public sprỶ()
	{
	}

	// Token: 0x0600429D RID: 17053 RVA: 0x00253F90 File Offset: 0x00252F90
	public sprỶ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600429E RID: 17054 RVA: 0x00253FC0 File Offset: 0x00252FC0
	public sprỶ(int A_0) : base(A_0)
	{
		this.m_iCode = 224;
	}

	// Token: 0x0600429F RID: 17055 RVA: 0x00253FFC File Offset: 0x00252FFC
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.\u171A = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.\u171B = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.\u171C = A_0.ReadUInt16(A_1);
		this.\u171F = A_0.ReadBit(A_1, 2);
		this.ᜠ = A_0.ReadBit(A_1, 3);
		this.\u171D = A_0.ReadBit(A_1, 0);
		this.\u171E = A_0.ReadBit(A_1, 1);
		A_1 += 2;
		this.ᜡ = A_0.ReadUInt16(A_1);
		this.ᜣ = A_0.ReadBit(A_1, 7);
		this.ᜢ = A_0.ReadBit(A_1, 3);
		A_1 += 2;
		this.ᜤ = A_0.ReadUInt16(A_1);
		this.ᜦ = A_0.ReadBit(A_1, 5);
		this.ᜥ = A_0.ReadBit(A_1, 4);
		this.ᜭ = (byte)BiffRecordRaw.ᜀ(this.ᜤ, 15);
		this.ᜤ = (ushort)((int)this.ᜤ & -16);
		A_1++;
		this.ᜪ = A_0.ReadBit(A_1, 5);
		this.ᜫ = A_0.ReadBit(A_1, 6);
		this.ᜬ = A_0.ReadBit(A_1, 7);
		this.ᜧ = A_0.ReadBit(A_1, 2);
		this.ᜨ = A_0.ReadBit(A_1, 3);
		this.ᜩ = A_0.ReadBit(A_1, 4);
		A_1++;
		this.ᜮ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜯ = A_0.ReadUInt16(A_1);
		A_1++;
		this.ᜱ = A_0.ReadBit(A_1, 7);
		this.ᜰ = A_0.ReadBit(A_1, 6);
		A_1++;
		this.\u1732 = A_0.ReadUInt32(A_1);
		this.\u1738 = (ushort)((this.\u1732 & 4227858432U) >> 26);
		A_1 += 4;
		this.\u1733 = A_0.ReadUInt16(A_1);
		this.m_iLength = 20;
		this.ᜀ();
		this.ᜀ((ushort)(BiffRecordRaw.ᜀ(this.\u171C, 65520) >> 4));
	}

	// Token: 0x060042A0 RID: 17056 RVA: 0x00254220 File Offset: 0x00253220
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.\u1738 > 63)
				{
					num = 4;
					continue;
				}
				goto IL_25A;
			case 1:
				goto IL_82;
			case 2:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_258;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				this.\u1738 = ((this.\u1738 == 4000) ? 1 : this.\u1738);
				num = 0;
				continue;
			case 4:
				goto IL_258;
			case 5:
				this.ᜀ(4095);
				num = 1;
				continue;
			}
			if (this.ᜁ() > 4095)
			{
				num = 5;
				continue;
			}
			IL_82:
			this.\u1734 = false;
			BiffRecordRaw.ᜀ(ref this.\u171C, 65520, (ushort)(this.ᜁ() << 4));
			this.ᜀ();
			A_0.WriteUInt16(A_1, this.\u171A);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.\u171B);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.\u171C);
			A_0.WriteBit(A_1, this.\u171F, 2);
			A_0.WriteBit(A_1, this.ᜠ, 3);
			A_0.WriteBit(A_1, this.\u171D, 0);
			A_0.WriteBit(A_1, this.\u171E, 1);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.ᜡ);
			A_1 += 2;
			BiffRecordRaw.ᜀ(ref this.ᜤ, 15, (ushort)this.ᜭ);
			A_0.WriteUInt16(A_1, this.ᜤ);
			A_0.WriteBit(A_1, this.ᜦ, 5);
			A_0.WriteBit(A_1, this.ᜥ, 4);
			A_1++;
			A_0.WriteBit(A_1, this.ᜪ, 5);
			A_0.WriteBit(A_1, this.ᜫ, 6);
			A_0.WriteBit(A_1, this.ᜬ, 7);
			A_0.WriteBit(A_1, this.ᜧ, 2);
			A_0.WriteBit(A_1, this.ᜨ, 3);
			A_0.WriteBit(A_1, this.ᜩ, 4);
			A_1++;
			A_0.WriteUInt16(A_1, this.ᜮ);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.ᜯ);
			A_1++;
			A_0.WriteBit(A_1, this.ᜱ, 7);
			A_0.WriteBit(A_1, this.ᜰ, 6);
			A_1++;
			num = 3;
		}
		IL_258:
		throw new ArgumentOutOfRangeException();
		IL_25A:
		this.\u1734 = false;
		this.\u1732 &= 67108863U;
		this.\u1732 += (uint)((uint)this.\u1738 << 26);
		A_0.WriteInt32(A_1, (int)this.\u1732);
		A_1 += 4;
		A_0.WriteUInt16(A_1, this.\u1733);
		this.m_iLength = 20;
		this.ᜀ();
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x002544E4 File Offset: 0x002534E4
	public int ᜀ(sprỶ A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 52;
			int num2;
			for (;;)
			{
				byte b;
				byte b2;
				byte b3;
				byte b4;
				byte b5;
				byte b6;
				byte b7;
				byte b8;
				byte b9;
				byte b10;
				byte b11;
				byte b12;
				byte b13;
				byte b14;
				byte b15;
				byte b16;
				byte b17;
				byte b18;
				byte b19;
				byte b20;
				byte b21;
				byte b22;
				switch (num)
				{
				case 0:
					num = 72;
					continue;
				case 1:
					goto IL_25B;
				case 2:
					b = 1;
					goto IL_851;
				case 3:
					b2 = 1;
					goto IL_B0F;
				case 4:
					b3 = 1;
					goto IL_707;
				case 5:
					b4 = 1;
					goto IL_8F6;
				case 6:
					if (!A_0.\u171E)
					{
						num = 69;
						continue;
					}
					num = 117;
					continue;
				case 7:
					if (!A_0.ᜧ)
					{
						num = 60;
						continue;
					}
					num = 50;
					continue;
				case 8:
					return num2;
				case 9:
					return num2;
				case 10:
					num = 98;
					continue;
				case 11:
					if (num2 != 0)
					{
						num = 31;
						continue;
					}
					num = 66;
					continue;
				case 12:
					num = 111;
					continue;
				case 13:
					b2 = 0;
					goto IL_B0F;
				case 14:
					if (num2 != 0)
					{
						num = 109;
						continue;
					}
					num = 25;
					continue;
				case 15:
					num = 127;
					continue;
				case 16:
					if (num2 != 0)
					{
						num = 73;
						continue;
					}
					num2 = (int)(this.\u171B - A_0.\u171B);
					num = 92;
					continue;
				case 17:
					return num2;
				case 18:
					if (num2 != 0)
					{
						num = 17;
						continue;
					}
					num = 85;
					continue;
				case 19:
					b5 = 0;
					goto IL_95A;
				case 20:
					if (num2 != 0)
					{
						num = 8;
						continue;
					}
					num2 = (int)(this.ᜡ - A_0.ᜡ);
					num = 44;
					continue;
				case 21:
					if (!this.\u171D)
					{
						num = 15;
						continue;
					}
					num = 87;
					continue;
				case 22:
					return num2;
				case 23:
					num = 89;
					continue;
				case 24:
					num = 123;
					continue;
				case 25:
					if (!this.\u171E)
					{
						num = 100;
						continue;
					}
					num = 93;
					continue;
				case 26:
					b6 = 1;
					goto IL_76B;
				case 27:
					if (num2 != 0)
					{
						num = 130;
						continue;
					}
					goto IL_BCE;
				case 28:
					b = 0;
					goto IL_851;
				case 29:
					return -1;
				case 30:
					num = 64;
					continue;
				case 31:
					return num2;
				case 32:
					if (num2 != 0)
					{
						num = 41;
						continue;
					}
					num2 = (int)(this.\u171E() - A_0.\u171E());
					num = 11;
					continue;
				case 33:
					b7 = 1;
					goto IL_30E;
				case 34:
					b4 = 0;
					goto IL_8F6;
				case 35:
				{
					long num3;
					if (num3 != 0L)
					{
						num = 24;
						continue;
					}
					num2 = (int)(this.\u1733 - A_0.\u1733);
					num = 80;
					continue;
				}
				case 36:
					if (num2 != 0)
					{
						num = 132;
						continue;
					}
					num2 = this.\u171F.CompareTo(A_0.\u171F);
					num = 20;
					continue;
				case 37:
					if (num2 != 0)
					{
						num = 112;
						continue;
					}
					num = 133;
					continue;
				case 38:
					return num2;
				case 39:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num2;
					default:
						if (false)
						{
						}
						num = 107;
						continue;
					}
					break;
				case 40:
					if (num2 != 0)
					{
						num = 77;
						continue;
					}
					num = 108;
					continue;
				case 41:
					return num2;
				case 42:
					b8 = 1;
					goto IL_4CF;
				case 43:
					if (!this.ᜬ)
					{
						num = 126;
						continue;
					}
					num = 46;
					continue;
				case 44:
					if (num2 != 0)
					{
						num = 116;
						continue;
					}
					num2 = (int)(this.ᜏ() - A_0.ᜏ());
					num = 32;
					continue;
				case 45:
					if (!A_0.ᜬ)
					{
						num = 10;
						continue;
					}
					num = 121;
					continue;
				case 46:
					b5 = 1;
					goto IL_95A;
				case 47:
					num = 53;
					continue;
				case 48:
					b9 = 0;
					goto IL_8B5;
				case 49:
					if (!A_0.ᜩ)
					{
						num = 65;
						continue;
					}
					num = 61;
					continue;
				case 50:
					b10 = 1;
					goto IL_662;
				case 51:
					b3 = 0;
					goto IL_707;
				case 53:
					b11 = 0;
					goto IL_57C;
				case 54:
					return num2;
				case 55:
					return num2;
				case 56:
					num = 48;
					continue;
				case 57:
					num = 51;
					continue;
				case 58:
					return num2;
				case 59:
					num = 34;
					continue;
				case 60:
					num = 135;
					continue;
				case 61:
					b12 = 1;
					goto IL_7AC;
				case 62:
					b13 = 1;
					goto IL_518;
				case 63:
					if (num2 != 0)
					{
						num = 128;
						continue;
					}
					num2 = (int)(this.ᜮ - A_0.ᜮ);
					num = 95;
					continue;
				case 64:
					b14 = 0;
					goto IL_B50;
				case 65:
					num = 134;
					continue;
				case 66:
					if (!this.ᜥ)
					{
						num = 23;
						continue;
					}
					num = 42;
					continue;
				case 67:
					return num2;
				case 68:
					if (!A_0.ᜥ)
					{
						num = 12;
						continue;
					}
					if (true)
					{
					}
					num = 62;
					continue;
				case 69:
					num = 101;
					continue;
				case 70:
					if (!this.ᜩ)
					{
						num = 103;
						continue;
					}
					num = 26;
					continue;
				case 71:
					num = 13;
					continue;
				case 72:
					b15 = 0;
					goto IL_6C6;
				case 73:
					return num2;
				case 74:
				{
					if (num2 != 0)
					{
						num = 58;
						continue;
					}
					long num3 = (long)((ulong)this.\u1732 - (ulong)A_0.\u1732);
					num = 35;
					continue;
				}
				case 75:
					b15 = 1;
					goto IL_6C6;
				case 76:
					b16 = 1;
					goto IL_810;
				case 77:
					return num2;
				case 78:
					return num2;
				case 79:
					b17 = 1;
					goto IL_621;
				case 80:
					if (num2 != 0)
					{
						num = 55;
						continue;
					}
					num2 = (int)(this.\u1738 - A_0.\u1738);
					num = 91;
					continue;
				case 81:
					b16 = 0;
					goto IL_810;
				case 82:
					num = 28;
					continue;
				case 83:
					if (!this.ᜨ)
					{
						num = 0;
						continue;
					}
					num = 75;
					continue;
				case 84:
					b14 = 1;
					goto IL_B50;
				case 85:
					if (!this.ᜦ)
					{
						num = 47;
						continue;
					}
					num = 115;
					continue;
				case 86:
					if (!A_0.ᜦ)
					{
						num = 105;
						continue;
					}
					num = 113;
					continue;
				case 87:
					b18 = 1;
					goto IL_2B1;
				case 88:
					num = 120;
					continue;
				case 89:
					b8 = 0;
					goto IL_4CF;
				case 90:
					if (!A_0.\u171D)
					{
						num = 39;
						continue;
					}
					num = 33;
					continue;
				case 91:
					if (num2 != 0)
					{
						num = 9;
						continue;
					}
					num = 94;
					continue;
				case 92:
					if (num2 != 0)
					{
						num = 38;
						continue;
					}
					num2 = (int)(this.\u171A - A_0.\u171A);
					num = 27;
					continue;
				case 93:
					b19 = 1;
					goto IL_372;
				case 94:
					if (!this.ᜠ)
					{
						num = 71;
						continue;
					}
					num = 3;
					continue;
				case 95:
					if (num2 != 0)
					{
						num = 104;
						continue;
					}
					num2 = (int)(this.ᜯ - A_0.ᜯ);
					num = 74;
					continue;
				case 96:
					b20 = 0;
					goto IL_5BD;
				case 97:
					if (!A_0.ᜨ)
					{
						num = 57;
						continue;
					}
					num = 4;
					continue;
				case 98:
					b21 = 0;
					goto IL_99B;
				case 99:
					if (num2 != 0)
					{
						num = 67;
						continue;
					}
					num = 43;
					continue;
				case 100:
					num = 119;
					continue;
				case 101:
					b22 = 0;
					goto IL_3B3;
				case 102:
					if (!A_0.ᜠ)
					{
						num = 30;
						continue;
					}
					num = 84;
					continue;
				case 103:
					num = 131;
					continue;
				case 104:
					return num2;
				case 105:
					num = 96;
					continue;
				case 106:
					if (!A_0.ᜪ)
					{
						num = 82;
						continue;
					}
					num = 2;
					continue;
				case 107:
					b7 = 0;
					goto IL_30E;
				case 108:
					if (!this.ᜧ)
					{
						num = 88;
						continue;
					}
					num = 79;
					continue;
				case 109:
					return num2;
				case 110:
					if (num2 != 0)
					{
						num = 78;
						continue;
					}
					num = 125;
					continue;
				case 111:
					b13 = 0;
					goto IL_518;
				case 112:
					return num2;
				case 113:
					b20 = 1;
					goto IL_5BD;
				case 114:
					if (!A_0.ᜫ)
					{
						num = 59;
						continue;
					}
					num = 5;
					continue;
				case 115:
					b11 = 1;
					goto IL_57C;
				case 116:
					return num2;
				case 117:
					b22 = 1;
					goto IL_3B3;
				case 118:
					if (num2 != 0)
					{
						num = 22;
						continue;
					}
					num = 70;
					continue;
				case 119:
					b19 = 0;
					goto IL_372;
				case 120:
					b17 = 0;
					goto IL_621;
				case 121:
					b21 = 1;
					goto IL_99B;
				case 122:
					num = 81;
					continue;
				case 123:
				{
					long num3;
					if (num3 <= 0L)
					{
						num = 29;
						continue;
					}
					return 1;
				}
				case 124:
					if (num2 != 0)
					{
						num = 54;
						continue;
					}
					num = 83;
					continue;
				case 125:
					if (!this.ᜫ)
					{
						num = 56;
						continue;
					}
					num = 129;
					continue;
				case 126:
					num = 19;
					continue;
				case 127:
					b18 = 0;
					goto IL_2B1;
				case 128:
					return num2;
				case 129:
					b9 = 1;
					goto IL_8B5;
				case 130:
					return num2;
				case 131:
					b6 = 0;
					goto IL_76B;
				case 132:
					return num2;
				case 133:
					if (!this.ᜪ)
					{
						num = 122;
						continue;
					}
					num = 76;
					continue;
				case 134:
					b12 = 0;
					goto IL_7AC;
				case 135:
					b10 = 0;
					goto IL_662;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 21;
				continue;
				IL_2B1:
				byte b23 = b18;
				num = 90;
				continue;
				IL_30E:
				byte b24 = b7;
				num2 = (int)(b23 - b24);
				num = 14;
				continue;
				IL_372:
				b23 = b19;
				num = 6;
				continue;
				IL_3B3:
				b24 = b22;
				num2 = (int)(b23 - b24);
				num = 36;
				continue;
				IL_4CF:
				b23 = b8;
				num = 68;
				continue;
				IL_518:
				b24 = b13;
				num2 = (int)(b23 - b24);
				num = 18;
				continue;
				IL_57C:
				b23 = b11;
				num = 86;
				continue;
				IL_5BD:
				b24 = b20;
				num2 = (int)(b23 - b24);
				num = 40;
				continue;
				IL_621:
				b23 = b17;
				num = 7;
				continue;
				IL_662:
				b24 = b10;
				num2 = (int)(b23 - b24);
				num = 124;
				continue;
				IL_6C6:
				b23 = b15;
				num = 97;
				continue;
				IL_707:
				b24 = b3;
				num2 = (int)(b23 - b24);
				num = 118;
				continue;
				IL_76B:
				b23 = b6;
				num = 49;
				continue;
				IL_7AC:
				b24 = b12;
				num2 = (int)(b23 - b24);
				num = 37;
				continue;
				IL_810:
				b23 = b16;
				num = 106;
				continue;
				IL_851:
				b24 = b;
				num2 = (int)(b23 - b24);
				num = 110;
				continue;
				IL_8B5:
				b23 = b9;
				num = 114;
				continue;
				IL_8F6:
				b24 = b4;
				num2 = (int)(b23 - b24);
				num = 99;
				continue;
				IL_95A:
				b23 = b5;
				num = 45;
				continue;
				IL_99B:
				b24 = b21;
				num2 = (int)(b23 - b24);
				num = 63;
				continue;
				IL_B0F:
				b23 = b2;
				num = 102;
				continue;
				IL_B50:
				b24 = b14;
				num2 = (int)(b23 - b24);
				num = 16;
			}
			IL_25B:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぃㅅⅇ⑉", a_));
			IL_BCE:
			num2 = (int)(this.ᜁ() - A_0.ᜁ());
			return num2;
		}
		}
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x002550D0 File Offset: 0x002540D0
	public virtual int ᜑ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.\u1735 = (this.\u171A.GetHashCode() ^ this.\u171B.GetHashCode() ^ ((int)(this.\u171C & 65520)).GetHashCode() ^ this.\u171D.GetHashCode() ^ this.\u171E.GetHashCode() ^ this.\u171F.GetHashCode() ^ this.ᜠ.GetHashCode() ^ this.ᜡ.GetHashCode() ^ this.ᜢ.GetHashCode() ^ this.ᜣ.GetHashCode() ^ this.ᜤ.GetHashCode() ^ this.ᜥ.GetHashCode() ^ this.ᜦ.GetHashCode() ^ this.ᜧ.GetHashCode() ^ this.ᜨ.GetHashCode() ^ this.ᜩ.GetHashCode() ^ this.ᜪ.GetHashCode() ^ this.ᜫ.GetHashCode() ^ this.ᜬ.GetHashCode() ^ this.ᜮ.GetHashCode() ^ this.ᜯ.GetHashCode() ^ this.ᜰ.GetHashCode() ^ this.ᜱ.GetHashCode() ^ this.\u1732.GetHashCode() ^ this.\u1738.GetHashCode() ^ this.\u1733.GetHashCode());
				this.\u1734 = true;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_1AD;
			}
			IL_1C:
			if (!this.\u1734)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
		IL_1AD:
		return this.\u1735;
	}

	// Token: 0x060042A3 RID: 17059 RVA: 0x0025529C File Offset: 0x0025429C
	private void ᜀ()
	{
		for (;;)
		{
			ushort num = this.ᜧ();
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != 1)
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					return;
				case 1:
					goto IL_37;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					}
					goto Block_2;
				}
				break;
				IL_37:
				ushort a_ = this.\u1712();
				ushort a_2 = this.ᜬ();
				this.ᜎ(a_2);
				this.ᜊ(a_);
				num2 = 2;
			}
		}
		Block_2:
		if (false)
		{
		}
	}

	// Token: 0x060042A4 RID: 17060 RVA: 0x00255330 File Offset: 0x00254330
	public void ᜁ(sprỶ A_0)
	{
		int a_ = 11;
		if (A_0 == null)
		{
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀ⱂい㕆⩈⹊", a_));
			}
		}
		this.ᜮ = A_0.ᜮ;
		this.ᜃ(A_0.ᜨ());
		this.ᜄ(A_0.\u1717());
		this.\u170D(A_0.ᜩ());
		this.ᜅ(A_0.ᜅ());
		this.ᜉ(A_0.\u1714());
		this.ᜊ(A_0.\u1719());
		this.ᜂ(A_0.ᜌ());
		this.ᜆ(A_0.ᜤ());
	}

	// Token: 0x060042A5 RID: 17061 RVA: 0x002553FC File Offset: 0x002543FC
	public void ᜄ(sprỶ A_0)
	{
		int a_ = 14;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃⥅㵇㡉⽋⭍", a_));
			}
		}
		if (true)
		{
		}
		this.ᜡ = A_0.ᜡ;
		this.ᜀ(A_0.\u170D());
		this.ᜁ(A_0.ᜣ());
		this.ᜌ(A_0.ᜉ());
		this.ᜀ(A_0.ᜏ());
	}

	// Token: 0x060042A6 RID: 17062 RVA: 0x00255498 File Offset: 0x00254498
	public void ᜂ(sprỶ A_0)
	{
		int a_ = 3;
		if (true)
		{
		}
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸吺䠼䴾≀♂", a_));
			}
		}
		this.ᜌ(A_0.ᜧ());
		this.ᜎ(A_0.\u1712());
		this.ᜊ(A_0.ᜬ());
	}

	// Token: 0x060042A7 RID: 17063 RVA: 0x0025551C File Offset: 0x0025451C
	public void ᜅ(sprỶ A_0)
	{
		int a_ = 6;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("儻愽┿㩁ぃE❇㡉⅋⽍⑏", a_));
			}
		}
		if (true)
		{
		}
		this.ᜎ(A_0.\u171F());
		this.ᜂ(A_0.ᜄ());
	}

	// Token: 0x060042A8 RID: 17064 RVA: 0x00255594 File Offset: 0x00254594
	internal void ᜀ(XlsWorkbook A_0)
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
		this.\u1739 = A_0;
	}

	// Token: 0x060042A9 RID: 17065 RVA: 0x002555D8 File Offset: 0x002545D8
	public new void ᜃ(sprỶ A_0)
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
		A_0.ᜠ = this.ᜠ;
		A_0.ᜱ = this.ᜱ;
		A_0.ᜰ = this.ᜰ;
		A_0.\u1734 = this.\u1734;
		A_0.\u171E = this.\u171E;
		A_0.ᜩ = this.ᜩ;
		A_0.ᜪ = this.ᜪ;
		A_0.ᜬ = this.ᜬ;
		A_0.ᜨ = this.ᜨ;
		A_0.ᜧ = this.ᜧ;
		A_0.ᜫ = this.ᜫ;
		A_0.ᜣ = this.ᜣ;
		A_0.\u171D = this.\u171D;
		A_0.ᜦ = this.ᜦ;
		A_0.ᜥ = this.ᜥ;
		A_0.ᜢ = this.ᜢ;
		A_0.m_iCode = this.m_iCode;
		A_0.\u1735 = this.\u1735;
		A_0.m_iLength = this.m_iLength;
		A_0.\u1732 = this.\u1732;
		A_0.ᜡ = this.ᜡ;
		A_0.ᜮ = this.ᜮ;
		A_0.\u171C = this.\u171C;
		A_0.\u1733 = this.\u1733;
		A_0.\u171A = this.\u171A;
		A_0.\u171B = this.\u171B;
		A_0.ᜤ = this.ᜤ;
		A_0.ᜯ = this.ᜯ;
		A_0.\u171F = this.\u171F;
		A_0.ᜀ(this.ᜁ());
		A_0.\u1738 = this.\u1738;
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x00255788 File Offset: 0x00254788
	public virtual void ᜀ(BiffRecordRaw A_0)
	{
		int a_ = 0;
		int num = 2;
		sprỶ sprỶ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (sprỶ != null)
				{
					num = 1;
					continue;
				}
				goto IL_9C;
			case 1:
				goto IL_60;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_78;
				}
				break;
			}
			IL_29:
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			sprỶ = (A_0 as sprỶ);
			if (true)
			{
			}
			num = 0;
			continue;
			goto IL_29;
		}
		IL_60:
		this.ᜃ(sprỶ);
		return;
		IL_78:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷䴹", a_));
		IL_9C:
		throw new ArgumentException(RecordTableEnumerator.b("䐵夷䴹", a_));
	}

	// Token: 0x04001D52 RID: 7506
	private new const ushort ᜀ = 15;

	// Token: 0x04001D53 RID: 7507
	private const ushort ᜁ = 192;

	// Token: 0x04001D54 RID: 7508
	private const ushort ᜂ = 6;

	// Token: 0x04001D55 RID: 7509
	private new const ushort ᜃ = 65520;

	// Token: 0x04001D56 RID: 7510
	private const ushort ᜄ = 65280;

	// Token: 0x04001D57 RID: 7511
	private const uint ᜅ = 127U;

	// Token: 0x04001D58 RID: 7512
	private const uint ᜆ = 16256U;

	// Token: 0x04001D59 RID: 7513
	private const uint ᜇ = 2080768U;

	// Token: 0x04001D5A RID: 7514
	private const uint ᜈ = 31457280U;

	// Token: 0x04001D5B RID: 7515
	private const uint ᜉ = 4227858432U;

	// Token: 0x04001D5C RID: 7516
	private const ushort ᜊ = 15;

	// Token: 0x04001D5D RID: 7517
	private const ushort ᜋ = 240;

	// Token: 0x04001D5E RID: 7518
	private const ushort ᜌ = 3840;

	// Token: 0x04001D5F RID: 7519
	private const ushort \u170D = 61440;

	// Token: 0x04001D60 RID: 7520
	private const ushort ᜎ = 7;

	// Token: 0x04001D61 RID: 7521
	private const ushort ᜏ = 112;

	// Token: 0x04001D62 RID: 7522
	private const ushort ᜐ = 127;

	// Token: 0x04001D63 RID: 7523
	private const ushort ᜑ = 16256;

	// Token: 0x04001D64 RID: 7524
	private const ushort \u1712 = 127;

	// Token: 0x04001D65 RID: 7525
	private const ushort \u1713 = 16256;

	// Token: 0x04001D66 RID: 7526
	private const int \u1714 = 7;

	// Token: 0x04001D67 RID: 7527
	private const int \u1715 = 20;

	// Token: 0x04001D68 RID: 7528
	private const int \u1716 = 16256;

	// Token: 0x04001D69 RID: 7529
	public new const int \u1717 = 65;

	// Token: 0x04001D6A RID: 7530
	public const int \u1718 = 64;

	// Token: 0x04001D6B RID: 7531
	private const int \u1719 = 4095;

	// Token: 0x04001D6C RID: 7532
	[spr\u2429(0, 2)]
	private ushort \u171A;

	// Token: 0x04001D6D RID: 7533
	[spr\u2429(2, 2)]
	private ushort \u171B;

	// Token: 0x04001D6E RID: 7534
	[spr\u2429(4, 2)]
	private ushort \u171C;

	// Token: 0x04001D6F RID: 7535
	[spr\u2429(4, 0, TFieldType.Bit)]
	private bool \u171D = true;

	// Token: 0x04001D70 RID: 7536
	[spr\u2429(4, 1, TFieldType.Bit)]
	private bool \u171E;

	// Token: 0x04001D71 RID: 7537
	[spr\u2429(4, 2, TFieldType.Bit)]
	private bool \u171F;

	// Token: 0x04001D72 RID: 7538
	[spr\u2429(4, 3, TFieldType.Bit)]
	private bool ᜠ;

	// Token: 0x04001D73 RID: 7539
	[spr\u2429(6, 2)]
	private ushort ᜡ = 32;

	// Token: 0x04001D74 RID: 7540
	[spr\u2429(6, 3, TFieldType.Bit)]
	private bool ᜢ;

	// Token: 0x04001D75 RID: 7541
	[spr\u2429(6, 7, TFieldType.Bit)]
	private bool ᜣ;

	// Token: 0x04001D76 RID: 7542
	[spr\u2429(8, 2)]
	private ushort ᜤ;

	// Token: 0x04001D77 RID: 7543
	[spr\u2429(8, 4, TFieldType.Bit)]
	private bool ᜥ;

	// Token: 0x04001D78 RID: 7544
	[spr\u2429(8, 5, TFieldType.Bit)]
	private bool ᜦ;

	// Token: 0x04001D79 RID: 7545
	[spr\u2429(9, 2, TFieldType.Bit)]
	private bool ᜧ;

	// Token: 0x04001D7A RID: 7546
	[spr\u2429(9, 3, TFieldType.Bit)]
	private bool ᜨ;

	// Token: 0x04001D7B RID: 7547
	[spr\u2429(9, 4, TFieldType.Bit)]
	private bool ᜩ;

	// Token: 0x04001D7C RID: 7548
	[spr\u2429(9, 5, TFieldType.Bit)]
	private bool ᜪ;

	// Token: 0x04001D7D RID: 7549
	[spr\u2429(9, 6, TFieldType.Bit)]
	private bool ᜫ;

	// Token: 0x04001D7E RID: 7550
	[spr\u2429(9, 7, TFieldType.Bit)]
	private bool ᜬ;

	// Token: 0x04001D7F RID: 7551
	private byte ᜭ;

	// Token: 0x04001D80 RID: 7552
	[spr\u2429(10, 2)]
	private ushort ᜮ;

	// Token: 0x04001D81 RID: 7553
	[spr\u2429(12, 2)]
	private ushort ᜯ;

	// Token: 0x04001D82 RID: 7554
	[spr\u2429(13, 6, TFieldType.Bit)]
	private bool ᜰ;

	// Token: 0x04001D83 RID: 7555
	[spr\u2429(13, 7, TFieldType.Bit)]
	private bool ᜱ;

	// Token: 0x04001D84 RID: 7556
	[spr\u2429(14, 4)]
	private uint \u1732;

	// Token: 0x04001D85 RID: 7557
	[spr\u2429(18, 2)]
	private ushort \u1733 = 8257;

	// Token: 0x04001D86 RID: 7558
	private bool \u1734;

	// Token: 0x04001D87 RID: 7559
	private int \u1735;

	// Token: 0x04001D88 RID: 7560
	private ushort \u1736;

	// Token: 0x04001D89 RID: 7561
	private bool \u1737;

	// Token: 0x04001D8A RID: 7562
	private ushort \u1738;

	// Token: 0x04001D8B RID: 7563
	private XlsWorkbook \u1739;

	// Token: 0x02000450 RID: 1104
	public enum TXFType
	{
		// Token: 0x04001D8D RID: 7565
		XF_CELL,
		// Token: 0x04001D8E RID: 7566
		XF_STYLE
	}
}
