using System;
using System.Collections;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200041E RID: 1054
internal class sprỆ : XlsObject, ICalculationOptions, ICloneParent
{
	// Token: 0x06003EF5 RID: 16117 RVA: 0x002391DC File Offset: 0x002381DC
	public sprỆ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003EF6 RID: 16118 RVA: 0x00239260 File Offset: 0x00238260
	internal sprỆ(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, int A_3) : this(A_0, A_1)
	{
		this.ᜀ(A_2, A_3);
	}

	// Token: 0x06003EF7 RID: 16119 RVA: 0x00239280 File Offset: 0x00238280
	public int ᜀ(IList A_0, int A_1)
	{
		int a_ = 18;
		for (;;)
		{
			IL_09:
			int num = 15;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
					goto IL_13F;
				case 1:
					if (A_1 >= count)
					{
						num = 13;
						continue;
					}
					goto IL_13F;
				case 2:
					goto IL_19C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					}
					if (false)
					{
					}
					goto IL_19C;
				case 4:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.SaveRecalc)
					{
						num = 16;
						continue;
					}
					BiffRecordRaw biffRecordRaw;
					this.ᜆ = (spr\u2169)biffRecordRaw;
					num = 12;
					continue;
				}
				case 5:
				{
					if (A_1 >= count)
					{
						num = 9;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 11;
					continue;
				}
				case 6:
					goto IL_19C;
				case 7:
					goto IL_19C;
				case 8:
					if (A_1 >= 0)
					{
						num = 10;
						continue;
					}
					goto IL_D7;
				case 9:
					return A_1;
				case 10:
					num = 1;
					continue;
				case 11:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.CalCount:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜂ = (sprℌ)biffRecordRaw;
						num = 17;
						continue;
					}
					case TBIFFRecord.CalcMode:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜁ = (spr\u18FD)biffRecordRaw;
						num = 7;
						continue;
					}
					case TBIFFRecord.Precision:
						goto IL_F9;
					case TBIFFRecord.RefMode:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜃ = (spr\u2482)biffRecordRaw;
						num = 6;
						continue;
					}
					case TBIFFRecord.Delta:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜅ = (spr\u1D56)biffRecordRaw;
						num = 3;
						continue;
					}
					case TBIFFRecord.Iteration:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜄ = (spr\u219D)biffRecordRaw;
						num = 2;
						continue;
					}
					default:
						num = 19;
						continue;
					}
					break;
				}
				case 12:
					goto IL_19C;
				case 13:
					goto IL_197;
				case 14:
					goto IL_250;
				case 16:
					num = 14;
					continue;
				case 17:
					goto IL_19C;
				case 18:
					goto IL_77;
				case 19:
					num = 4;
					continue;
				}
				if (A_0 == null)
				{
					num = 18;
					continue;
				}
				count = A_0.Count;
				num = 8;
				continue;
				IL_13F:
				num = 5;
				continue;
				IL_19C:
				A_1++;
				num = 0;
			}
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍", a_));
		IL_D7:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᡇ╉㽋", a_), RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥ѧཀྵὫᵭ偯ٱᱳ᝵ᙷ婹䱻幽ꚅ뚕ﶛ肟욡얣튥즧誩삫쮭\udeaf햱삳\udeb5隷", a_));
		IL_F9:
		if (true)
		{
		}
		return A_1;
		IL_197:
		goto IL_D7;
		IL_250:
		goto IL_F9;
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x00239500 File Offset: 0x00238500
	public void ᜀ(RecordArrayList A_0)
	{
		int a_ = 1;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			}
		}
		A_0.Add(this.ᜁ.Clone());
		A_0.Add(this.ᜂ.Clone());
		A_0.Add(this.ᜃ.Clone());
		A_0.Add(this.ᜄ.Clone());
		A_0.Add(this.ᜅ.Clone());
		A_0.Add(this.ᜆ.Clone());
	}

	// Token: 0x06003EF9 RID: 16121 RVA: 0x002395CC File Offset: 0x002385CC
	public int ᜂ()
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
		return (int)this.ᜂ.ᜀ();
	}

	// Token: 0x06003EFA RID: 16122 RVA: 0x00239614 File Offset: 0x00238614
	public void ᜀ(int A_0)
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
		this.ᜂ.ᜀ((ushort)A_0);
	}

	// Token: 0x06003EFB RID: 16123 RVA: 0x0023965C File Offset: 0x0023865C
	public ExcelCalculationMode ᜄ()
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
		return this.ᜁ.ᜀ();
	}

	// Token: 0x06003EFC RID: 16124 RVA: 0x002396A4 File Offset: 0x002386A4
	public void ᜀ(ExcelCalculationMode A_0)
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
		this.ᜁ.ᜀ(A_0);
	}

	// Token: 0x06003EFD RID: 16125 RVA: 0x002396EC File Offset: 0x002386EC
	public bool ᜁ()
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
		return this.ᜆ.ᜀ() == 1;
	}

	// Token: 0x06003EFE RID: 16126 RVA: 0x00239738 File Offset: 0x00238738
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
		this.ᜆ.ᜀ(A_0 ? 1 : 0);
	}

	// Token: 0x06003EFF RID: 16127 RVA: 0x0023978C File Offset: 0x0023878C
	public double ᜃ()
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
		return this.ᜅ.ᜁ();
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x002397D4 File Offset: 0x002387D4
	public void ᜀ(double A_0)
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
		this.ᜅ.ᜀ(A_0);
	}

	// Token: 0x06003F01 RID: 16129 RVA: 0x0023981C File Offset: 0x0023881C
	public bool ᜀ()
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
		return this.ᜄ.ᜁ() == 1;
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x00239868 File Offset: 0x00238868
	public void ᜁ(bool A_0)
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
		this.ᜄ.ᜀ(A_0 ? 1 : 0);
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x002398BC File Offset: 0x002388BC
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
		return this.ᜃ.ᜁ() == 0;
	}

	// Token: 0x06003F04 RID: 16132 RVA: 0x00239908 File Offset: 0x00238908
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
		this.ᜃ.ᜀ(A_0 ? 0 : 1);
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x0023995C File Offset: 0x0023895C
	public object ᜀ(object A_0)
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
		sprỆ sprỆ = (sprỆ)base.MemberwiseClone();
		sprỆ.SetParent(A_0);
		sprỆ.ᜁ = (spr\u18FD)spr\u1CD3.ᜀ(this.ᜁ);
		sprỆ.ᜂ = (sprℌ)spr\u1CD3.ᜀ(this.ᜂ);
		sprỆ.ᜃ = (spr\u2482)spr\u1CD3.ᜀ(this.ᜃ);
		sprỆ.ᜄ = (spr\u219D)spr\u1CD3.ᜀ(this.ᜄ);
		sprỆ.ᜅ = (spr\u1D56)spr\u1CD3.ᜀ(this.ᜅ);
		sprỆ.ᜆ = (spr\u2169)spr\u1CD3.ᜀ(this.ᜆ);
		return sprỆ;
	}

	// Token: 0x06003F06 RID: 16134 RVA: 0x00239A30 File Offset: 0x00238A30
	// Note: this type is marked as 'beforefieldinit'.
	static sprỆ()
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
		sprỆ.ᜀ = new TBIFFRecord[]
		{
			TBIFFRecord.CalcMode,
			TBIFFRecord.CalCount,
			TBIFFRecord.RefMode,
			TBIFFRecord.Iteration,
			TBIFFRecord.Delta,
			TBIFFRecord.SaveRecalc
		};
	}

	// Token: 0x04001CAB RID: 7339
	internal static readonly TBIFFRecord[] ᜀ;

	// Token: 0x04001CAC RID: 7340
	private spr\u18FD ᜁ = (spr\u18FD)spr\u175E.ᜀ(TBIFFRecord.CalcMode);

	// Token: 0x04001CAD RID: 7341
	private sprℌ ᜂ = (sprℌ)spr\u175E.ᜀ(TBIFFRecord.CalCount);

	// Token: 0x04001CAE RID: 7342
	private spr\u2482 ᜃ = (spr\u2482)spr\u175E.ᜀ(TBIFFRecord.RefMode);

	// Token: 0x04001CAF RID: 7343
	private spr\u219D ᜄ = (spr\u219D)spr\u175E.ᜀ(TBIFFRecord.Iteration);

	// Token: 0x04001CB0 RID: 7344
	private spr\u1D56 ᜅ = (spr\u1D56)spr\u175E.ᜀ(TBIFFRecord.Delta);

	// Token: 0x04001CB1 RID: 7345
	private spr\u2169 ᜆ = (spr\u2169)spr\u175E.ᜀ(TBIFFRecord.SaveRecalc);
}
