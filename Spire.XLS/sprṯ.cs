using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200052E RID: 1326
[spr\u2593(TBIFFRecord.PivotViewFields)]
[CLSCompliant(false)]
internal class sprṮ : spr\u251F
{
	// Token: 0x06005107 RID: 20743 RVA: 0x0032CADC File Offset: 0x0032BADC
	public sprṮ()
	{
	}

	// Token: 0x06005108 RID: 20744 RVA: 0x0032CAF0 File Offset: 0x0032BAF0
	public sprṮ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005109 RID: 20745 RVA: 0x0032CB08 File Offset: 0x0032BB08
	public sprṮ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600510A RID: 20746 RVA: 0x0032CB1C File Offset: 0x0032BB1C
	public new AxisTypes ᜀ()
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
		return (AxisTypes)this.ᜂ;
	}

	// Token: 0x0600510B RID: 20747 RVA: 0x0032CB60 File Offset: 0x0032BB60
	public new void ᜀ(AxisTypes A_0)
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
		this.ᜂ = (ushort)A_0;
	}

	// Token: 0x0600510C RID: 20748 RVA: 0x0032CBA4 File Offset: 0x0032BBA4
	public new ushort ᜃ()
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

	// Token: 0x0600510D RID: 20749 RVA: 0x0032CBE8 File Offset: 0x0032BBE8
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
		this.ᜃ = A_0;
	}

	// Token: 0x0600510E RID: 20750 RVA: 0x0032CC2C File Offset: 0x0032BC2C
	public SubtotalTypes ᜄ()
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
		return (SubtotalTypes)this.ᜄ;
	}

	// Token: 0x0600510F RID: 20751 RVA: 0x0032CC70 File Offset: 0x0032BC70
	public new void ᜀ(SubtotalTypes A_0)
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
		this.ᜄ = (ushort)A_0;
	}

	// Token: 0x06005110 RID: 20752 RVA: 0x0032CCB4 File Offset: 0x0032BCB4
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
		return this.ᜅ;
	}

	// Token: 0x06005111 RID: 20753 RVA: 0x0032CCF8 File Offset: 0x0032BCF8
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
		this.ᜅ = A_0;
	}

	// Token: 0x06005112 RID: 20754 RVA: 0x0032CD3C File Offset: 0x0032BD3C
	public ushort ᜁ()
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
		return this.ᜆ;
	}

	// Token: 0x06005113 RID: 20755 RVA: 0x0032CD80 File Offset: 0x0032BD80
	public string ᜆ()
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
		return this.ᜇ;
	}

	// Token: 0x06005114 RID: 20756 RVA: 0x0032CDC4 File Offset: 0x0032BDC4
	public new void ᜀ(string A_0)
	{
		int a_ = 18;
		for (;;)
		{
			this.ᜇ = A_0;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				case 1:
					goto IL_43;
				case 2:
					goto IL_A3;
				case 3:
					if (A_0.Length > 65535)
					{
						num = 2;
						continue;
					}
					goto IL_B1;
				}
				break;
			}
		}
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A3:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㹇⭉⁋㭍㕏籑ᡓ㍕㙗㵙⡛㙝", a_), RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥ཧᡩ५཭ѯ᝱ٳ噵㱷㽹㩻ⅽ칿힁좃쪅힇욉즋삍힏욑\udc93", a_));
		default:
			if (false)
			{
			}
			this.ᜆ = ushort.MaxValue;
			return;
		}
		IL_B1:
		this.ᜆ = (ushort)A_0.Length;
	}

	// Token: 0x06005115 RID: 20757 RVA: 0x0032CE9C File Offset: 0x0032BE9C
	public override void ᜂ()
	{
		for (;;)
		{
			this.ᜂ = base.ᜌ(0);
			this.ᜃ = base.ᜌ(2);
			this.ᜄ = base.ᜌ(4);
			this.ᜅ = base.ᜌ(6);
			this.ᜆ = base.ᜌ(8);
			this.ᜇ = null;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					this.ᜇ = base.ᜀ(10, (int)this.ᜆ, out num2, false);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					if (this.ᜆ != 65535)
					{
						num = 0;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06005116 RID: 20758 RVA: 0x0032CF78 File Offset: 0x0032BF78
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.m_iLength = 10;
			this.ᜀ = new byte[this.m_iLength];
			base.ᜀ(0, this.ᜂ);
			base.ᜀ(2, this.ᜃ);
			base.ᜀ(4, this.ᜄ);
			base.ᜀ(6, this.ᜅ);
			base.ᜀ(8, this.ᜆ);
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_iLength += base.ᜂ(10, this.ᜇ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				case 2:
					if (this.ᜇ != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x04002437 RID: 9271
	private new const ushort ᜀ = 65535;

	// Token: 0x04002438 RID: 9272
	private new const int ᜁ = 10;

	// Token: 0x04002439 RID: 9273
	[spr\u2429(0, 2)]
	private new ushort ᜂ;

	// Token: 0x0400243A RID: 9274
	[spr\u2429(2, 2)]
	private new ushort ᜃ;

	// Token: 0x0400243B RID: 9275
	[spr\u2429(4, 2)]
	private new ushort ᜄ;

	// Token: 0x0400243C RID: 9276
	[spr\u2429(6, 2)]
	private new ushort ᜅ;

	// Token: 0x0400243D RID: 9277
	[spr\u2429(8, 2)]
	private new ushort ᜆ;

	// Token: 0x0400243E RID: 9278
	private string ᜇ;
}
