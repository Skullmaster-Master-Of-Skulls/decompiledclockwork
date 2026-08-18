using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Charts;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200038A RID: 906
internal class spr\u237B : XlsObject, IChartErrorBars
{
	// Token: 0x06003733 RID: 14131 RVA: 0x001F0AF0 File Offset: 0x001EFAF0
	public static void ᜀ(IList<IRecordStorage> A_0, int A_1)
	{
		int a_ = 6;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
			}
		}
		sprḠ sprḠ = (sprḠ)spr\u175E.ᜀ(TBIFFRecord.ChartSeries);
		sprḠ.ᜀ(sprḠ.DataType.Numeric);
		sprḠ.ᜂ(sprḠ.DataType.Numeric);
		sprḠ.ᜁ(sprḠ.DataType.Numeric);
		sprḠ.ᜀ((ushort)A_1);
		A_0.Add(sprḠ);
	}

	// Token: 0x06003734 RID: 14132 RVA: 0x001F0B84 File Offset: 0x001EFB84
	public static void ᜀ(IList<IRecordStorage> A_0, XlsChartBorder A_1, int A_2, int A_3, sprᣐ A_4)
	{
		int a_ = 3;
		int num = 2;
		BiffRecordRaw item;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_181;
			case 1:
			{
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				sprᲡ sprᲡ = (sprᲡ)spr\u175E.ᜀ(TBIFFRecord.ChartDataFormat);
				sprᲡ.ᜂ(ushort.MaxValue);
				sprᲡ.ᜁ((ushort)A_3);
				sprᲡ.ᜀ((ushort)A_2);
				A_0.Add(sprᲡ);
				item = spr\u175E.ᜀ(TBIFFRecord.Begin);
				A_0.Add(item);
				item = spr\u175E.ᜀ(TBIFFRecord.Chart3DDataFormat);
				A_0.Add(item);
				A_1.ᜀ(A_0);
				item = spr\u175E.ᜀ(TBIFFRecord.ChartAreaFormat);
				A_0.Add(item);
				item = spr\u175E.ᜀ(TBIFFRecord.ChartPieFormat);
				A_0.Add(item);
				num = 4;
				continue;
			}
			case 3:
				goto IL_47;
			case 4:
				if (A_4 != null)
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
			case 5:
				goto IL_E1;
			case 6:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_181;
				default:
					goto IL_6F;
				}
				break;
			case 7:
				goto IL_AF;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_181:
			num = 6;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺帼倾㍀❂㙄", a_));
		IL_6F:
		if (false)
		{
		}
		BiffRecordRaw biffRecordRaw = A_4;
		goto IL_186;
		IL_AF:
		biffRecordRaw = spr\u175E.ᜀ(TBIFFRecord.ChartMarkerFormat);
		goto IL_186;
		IL_E1:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬸吺似嬾⑀ㅂ", a_));
		IL_186:
		item = biffRecordRaw;
		A_0.Add(item);
		item = spr\u175E.ᜀ(TBIFFRecord.End);
		A_0.Add(item);
	}

	// Token: 0x06003735 RID: 14133 RVA: 0x001F0D34 File Offset: 0x001EFD34
	internal spr\u237B(spr\u1DF5 A_0, object A_1, bool A_2)
	{
		this.ᜊ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
		base..ctor(A_0, A_1);
		this.ᜂ = new ChartBorder((spr\u2158)A_0, this);
		this.ᜉ = A_2;
		this.ᜃ = (spr\u23FE)spr\u175E.ᜀ(TBIFFRecord.ChartSerAuxErrBar);
		if (!A_2)
		{
			this.ᜀ(1.0);
		}
		this.ᜀ();
	}

	// Token: 0x06003736 RID: 14134 RVA: 0x001F0DA8 File Offset: 0x001EFDA8
	internal spr\u237B(spr\u1DF5 A_0, object A_1, IList A_2)
	{
		int a_ = 18;
		this.ᜊ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
		base..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍", a_));
		}
		this.ᜀ();
		this.ᜀ(A_2);
	}

	// Token: 0x06003737 RID: 14135 RVA: 0x001F0E04 File Offset: 0x001EFE04
	private void ᜀ()
	{
		int a_ = 3;
		for (;;)
		{
			this.ᜆ = (XlsChartSerie)base.FindParent(typeof(XlsChartSerie));
			if (this.ᜆ != null)
			{
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_42;
			}
		}
		IL_42:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("稸娺匼儾⹀㝂敄ⅆ⁈╊⥌潎⅐㉒❔㉖㝘⽚絜ぞ͠ॢdѦᵨᡪ", a_));
	}

	// Token: 0x06003738 RID: 14136 RVA: 0x001F0E84 File Offset: 0x001EFE84
	public IChartBorder ᜁ()
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

	// Token: 0x06003739 RID: 14137 RVA: 0x001F0EC8 File Offset: 0x001EFEC8
	public ErrorBarIncludeType ᜂ()
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

	// Token: 0x0600373A RID: 14138 RVA: 0x001F0F0C File Offset: 0x001EFF0C
	public void ᜁ(ErrorBarIncludeType A_0)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 6;
				continue;
			case 2:
				goto IL_8A;
			case 3:
				if (!this.ᜆ.ParentBook.Loading)
				{
					num = 1;
					continue;
				}
				goto IL_B6;
			case 4:
				num = 3;
				continue;
			case 5:
				return;
			case 6:
				if (!this.ᜀ(A_0))
				{
					num = 2;
					continue;
				}
				goto IL_B6;
			}
			if (A_0 != this.ᜂ())
			{
				num = 4;
				continue;
			}
			return;
			IL_B6:
			this.ᜅ = A_0;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			default:
				if (false)
				{
				}
				num = 5;
				break;
			}
		}
		IL_4B:
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ࡀⵂ♄⭆㱈⽊⡌潎㉐㉒㭔睖㝘㑚⥜罞͠٢䕤ѦŨ੪ͬ࡮ᑰᝲ奴坶ᑸ๺๼୾ꆀꦈ力떔爵튠莢잤슦쾨쒪\udfac쪮龰", a_));
		IL_8A:
		goto IL_4B;
	}

	// Token: 0x0600373B RID: 14139 RVA: 0x001F1000 File Offset: 0x001F0000
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
		return this.ᜃ.ᜃ();
	}

	// Token: 0x0600373C RID: 14140 RVA: 0x001F1048 File Offset: 0x001F0048
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
		this.ᜃ.ᜀ(A_0);
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x001F1090 File Offset: 0x001F0090
	public ErrorBarType ᜅ()
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
		return this.ᜃ.ᜄ();
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x001F10D8 File Offset: 0x001F00D8
	public void ᜀ(ErrorBarType A_0)
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
		this.ᜃ.ᜀ(A_0);
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x001F1120 File Offset: 0x001F0120
	public double \u1712()
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
		return this.ᜃ.ᜂ();
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x001F1168 File Offset: 0x001F0168
	public void ᜀ(double A_0)
	{
		int a_ = 14;
		while (A_0 < 0.0)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("੃㍅╇⡉⥋㱍ُ㍑㡓⍕㵗", a_));
			}
		}
		this.ᜃ.ᜀ(A_0);
	}

	// Token: 0x06003741 RID: 14145 RVA: 0x001F11DC File Offset: 0x001F01DC
	internal bool ᜎ()
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

	// Token: 0x06003742 RID: 14146 RVA: 0x001F1220 File Offset: 0x001F0220
	internal void ᜄ(bool A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x001F1264 File Offset: 0x001F0264
	internal bool \u170D()
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

	// Token: 0x06003744 RID: 14148 RVA: 0x001F12A8 File Offset: 0x001F02A8
	internal void ᜂ(bool A_0)
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
		this.ᜑ = A_0;
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x001F12EC File Offset: 0x001F02EC
	public IXLSRange ᜉ()
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

	// Token: 0x06003746 RID: 14150 RVA: 0x001F1330 File Offset: 0x001F0330
	public void ᜂ(IXLSRange A_0)
	{
		int a_ = 5;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜅ = ((this.ᜈ == null) ? ErrorBarIncludeType.Plus : ErrorBarIncludeType.Both);
				this.ᜃ.ᜀ(ErrorBarType.Custom);
				this.ᜇ = A_0;
				num = 5;
				continue;
			case 2:
				this.ᜊ.ᜀ((A_0 as spr\u1A8B).ᜀ());
				this.ᜌ = true;
				num = 0;
				continue;
			case 4:
				goto IL_3C;
			case 5:
				if (!this.ᜆ.ParentBook.Loading)
				{
					num = 2;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
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
					num = 1;
					break;
				}
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("欺儼䨾㉀ᅂ⑄⥆⹈⹊", a_));
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x001F1444 File Offset: 0x001F0444
	public IXLSRange ᜐ()
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

	// Token: 0x06003748 RID: 14152 RVA: 0x001F1488 File Offset: 0x001F0488
	public void ᜁ(IXLSRange A_0)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜅ = ((this.ᜇ == null) ? ErrorBarIncludeType.Minus : ErrorBarIncludeType.Both);
				this.ᜃ.ᜀ(ErrorBarType.Custom);
				this.ᜈ = A_0;
				num = 3;
				continue;
			case 3:
				if (!this.ᜆ.ParentBook.Loading)
				{
					num = 5;
					continue;
				}
				return;
			case 4:
				goto IL_44;
			case 5:
				this.ᜊ.ᜀ((A_0 as spr\u1A8B).ᜀ());
				this.ᜌ = true;
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
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
					num = 2;
					break;
				}
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("椸场䠼䰾ፀ≂⭄⁆ⱈ", a_));
	}

	// Token: 0x06003749 RID: 14153 RVA: 0x001F159C File Offset: 0x001F059C
	public IShadow ᜏ()
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_76;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜄ = new ChartShadow(base.AppImplementation, this);
					num = 1;
					continue;
				case 1:
					goto IL_76;
				}
				if (this.ᜄ != null)
				{
					goto IL_78;
				}
				if (true)
				{
				}
				num = 0;
				break;
			}
		}
		IL_76:
		IL_78:
		return this.ᜄ;
	}

	// Token: 0x0600374A RID: 14154 RVA: 0x001F1628 File Offset: 0x001F0628
	public bool ᜊ()
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
		return this.ᜄ != null;
	}

	// Token: 0x0600374B RID: 14155 RVA: 0x001F1670 File Offset: 0x001F0670
	internal void ᜅ(bool A_0)
	{
		if (true)
		{
		}
		if (!A_0)
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
				this.ᜄ = null;
				return;
			}
		}
		this.ᜏ();
	}

	// Token: 0x0600374C RID: 14156 RVA: 0x001F16C0 File Offset: 0x001F06C0
	public IFormat3D ᜈ()
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_76;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.\u170D = new Format3D(base.AppImplementation, this);
					num = 1;
					continue;
				case 1:
					goto IL_76;
				}
				if (this.\u170D != null)
				{
					goto IL_78;
				}
				num = 0;
				break;
			}
		}
		IL_76:
		IL_78:
		return this.\u170D;
	}

	// Token: 0x0600374D RID: 14157 RVA: 0x001F174C File Offset: 0x001F074C
	public bool ᜑ()
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
		return this.\u170D != null;
	}

	// Token: 0x0600374E RID: 14158 RVA: 0x001F1794 File Offset: 0x001F0794
	internal void ᜃ(bool A_0)
	{
		if (!A_0)
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
				this.\u170D = null;
				return;
			}
		}
		this.ᜈ();
	}

	// Token: 0x0600374F RID: 14159 RVA: 0x001F17E4 File Offset: 0x001F07E4
	public void ᜃ()
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
		this.ᜀ(ErrorBarType.Fixed);
		this.ᜁ().UseDefaultFormat = true;
		this.ᜁ(true);
		this.ᜀ((double)(this.ᜉ ? 10 : 1));
		this.ᜅ = ErrorBarIncludeType.Both;
		this.ᜇ = null;
		this.ᜈ = null;
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x001F1868 File Offset: 0x001F0868
	public void ᜆ()
	{
		if (!this.ᜉ)
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
				this.ᜆ.HasErrorBarsX = false;
				return;
			}
		}
		this.ᜆ.HasErrorBarsY = false;
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x001F18C8 File Offset: 0x001F08C8
	private void ᜀ(IList A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 19;
			IXLSRange ixlsrange;
			for (;;)
			{
				bool flag;
				int num2;
				int count;
				TBIFFRecord typeCode;
				bool flag2;
				switch (num)
				{
				case 0:
					goto IL_296;
				case 1:
				{
					BiffRecordRaw biffRecordRaw;
					this.ᜃ = (spr\u23FE)biffRecordRaw;
					num = 22;
					continue;
				}
				case 2:
					if (ixlsrange != null)
					{
						num = 7;
						continue;
					}
					return;
				case 3:
					goto IL_296;
				case 4:
					num = 8;
					continue;
				case 5:
					flag = true;
					goto IL_305;
				case 6:
					return;
				case 7:
					num = 27;
					continue;
				case 8:
					goto IL_189;
				case 9:
					if (this.ᜊ.ᜁ() == sprᢀ.ReferenceType.Worksheet)
					{
						num = 25;
						continue;
					}
					goto IL_284;
				case 10:
					goto IL_284;
				case 11:
				{
					sprᢀ sprᢀ;
					if (sprᢀ.ᜄ() == sprᢀ.LinkIndex.LinkToValues)
					{
						num = 30;
						continue;
					}
					goto IL_284;
				}
				case 12:
					goto IL_B7;
				case 13:
					goto IL_389;
				case 14:
				{
					spr\u23FE.TErrorBarValue terrorBarValue = this.ᜃ.ᜀ();
					num = 21;
					continue;
				}
				case 15:
				{
					if (num2 >= count)
					{
						num = 14;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)A_0[num2];
					typeCode = biffRecordRaw.TypeCode;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_189;
					default:
						if (false)
						{
						}
						num = 26;
						continue;
					}
					break;
				}
				case 16:
				{
					if (typeCode != TBIFFRecord.ChartAI)
					{
						num = 4;
						continue;
					}
					BiffRecordRaw biffRecordRaw;
					sprᢀ sprᢀ = (sprᢀ)biffRecordRaw;
					num = 11;
					continue;
				}
				case 17:
					goto IL_284;
				case 18:
					num = 23;
					continue;
				case 20:
				{
					spr\u23FE.TErrorBarValue terrorBarValue;
					this.ᜉ = (terrorBarValue == spr\u23FE.TErrorBarValue.YDirectionMinus || terrorBarValue == spr\u23FE.TErrorBarValue.YDirectionPlus);
					num = 28;
					continue;
				}
				case 21:
				{
					spr\u23FE.TErrorBarValue terrorBarValue;
					if (terrorBarValue != spr\u23FE.TErrorBarValue.XDirectionPlus)
					{
						num = 18;
						continue;
					}
					num = 5;
					continue;
				}
				case 22:
					goto IL_284;
				case 23:
				{
					spr\u23FE.TErrorBarValue terrorBarValue;
					flag = (terrorBarValue == spr\u23FE.TErrorBarValue.YDirectionPlus);
					goto IL_305;
				}
				case 24:
					goto IL_284;
				case 25:
					ixlsrange = this.ᜆ.ᜀ(this.ᜊ);
					num = 10;
					continue;
				case 26:
					switch (typeCode)
					{
					case TBIFFRecord.ChartLineFormat:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜂ = new ChartBorder((spr\u2158)base.ReservedHandle, this, (spr\u22F3)biffRecordRaw);
						num = 17;
						continue;
					}
					case (TBIFFRecord)4104:
						goto IL_284;
					case TBIFFRecord.ChartMarkerFormat:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜋ = (sprᣐ)biffRecordRaw;
						num = 24;
						continue;
					}
					default:
						num = 29;
						continue;
					}
					break;
				case 27:
					if (flag2)
					{
						num = 13;
						continue;
					}
					this.ᜈ = ixlsrange;
					num = 6;
					continue;
				case 28:
					this.ᜅ = (flag2 ? ErrorBarIncludeType.Plus : ErrorBarIncludeType.Minus);
					if (true)
					{
					}
					num = 2;
					continue;
				case 29:
					num = 16;
					continue;
				case 30:
				{
					sprᢀ sprᢀ;
					this.ᜊ = sprᢀ;
					num = 9;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				ixlsrange = null;
				num2 = 0;
				count = A_0.Count;
				num = 0;
				continue;
				IL_189:
				if (typeCode == TBIFFRecord.ChartSerAuxErrBar)
				{
					num = 1;
					continue;
				}
				IL_284:
				num2++;
				num = 3;
				continue;
				IL_296:
				num = 15;
				continue;
				IL_305:
				flag2 = flag;
				num = 20;
			}
			IL_B7:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻弽㐿⍁", a_));
			IL_389:
			this.ᜇ = ixlsrange;
			return;
		}
		}
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x001F1C80 File Offset: 0x001F0C80
	public void ᜀ(IList<IRecordStorage> A_0)
	{
		int a_ = 2;
		int num = 5;
		int index;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜃ.ᜀ(1);
				num = 1;
				continue;
			case 1:
				goto IL_4D;
			case 2:
				if (this.ᜅ == ErrorBarIncludeType.Both)
				{
					num = 4;
					continue;
				}
				goto IL_126;
			case 3:
				if (this.ᜃ.ᜅ() == 0)
				{
					num = 0;
					continue;
				}
				goto IL_4D;
			case 4:
				goto IL_74;
			case 6:
				goto IL_4B;
			}
			if (true)
			{
			}
			if (A_0 != null)
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
					num = 3;
					continue;
				}
			}
			num = 6;
			continue;
			IL_4D:
			index = this.ᜆ.Index;
			num = 2;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		IL_74:
		this.ᜀ(A_0, this.ᜉ, true, index);
		this.ᜃ = (spr\u23FE)this.ᜃ.Clone();
		this.ᜀ(A_0, this.ᜉ, false, index);
		return;
		IL_126:
		bool a_2 = this.ᜅ == ErrorBarIncludeType.Plus;
		this.ᜀ(A_0, this.ᜉ, a_2, index);
	}

	// Token: 0x06003753 RID: 14163 RVA: 0x001F1DCC File Offset: 0x001F0DCC
	private void ᜀ(IList<IRecordStorage> A_0, bool A_1)
	{
		int a_ = 16;
		int num = 10;
		sprᢀ sprᢀ;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (!this.ᜉ)
				{
					num = 3;
					continue;
				}
				goto IL_138;
			case 1:
				goto IL_186;
			case 2:
				sprᢀ.ᜀ(sprᢀ.ReferenceType.Worksheet);
				sprᢀ.ᜀ(this.ᜀ(A_1));
				num = 11;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_114;
				default:
					if (false)
					{
					}
					sprᢀ.ᜀ(sprᢀ.ReferenceType.Worksheet);
					sprᢀ.ᜀ(this.ᜀ(A_1));
					num = 1;
					continue;
				}
				break;
			case 4:
				goto IL_60;
			case 5:
				goto IL_151;
			case 6:
				if (this.ᜅ() == ErrorBarType.Custom)
				{
					num = 7;
					continue;
				}
				goto IL_A1;
			case 7:
				num = 8;
				continue;
			case 8:
				if (this.ᜉ)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 9:
				if (this.ᜅ() == ErrorBarType.Custom)
				{
					num = 12;
					continue;
				}
				goto IL_138;
			case 11:
				goto IL_A1;
			case 12:
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			sprᢀ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToTitleOrText);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
			A_0.Add(sprᢀ);
			sprᢀ = (sprᢀ)sprᢀ.Clone();
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToValues);
			A_0.Add(sprᢀ);
			goto IL_114;
			IL_A1:
			sprᢀ = (sprᢀ)sprᢀ.Clone();
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToCategories);
			num = 9;
			continue;
			IL_114:
			num = 6;
			continue;
			IL_138:
			sprᢀ.ᜀ(null);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
			num = 5;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏⅑", a_));
		IL_151:
		IL_186:
		A_0.Add(sprᢀ);
		sprᢀ = (sprᢀ)sprᢀ.Clone();
		sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToBubbles);
		sprᢀ.ᜀ(null);
		sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
		A_0.Add(sprᢀ);
	}

	// Token: 0x06003754 RID: 14164 RVA: 0x001F1FE4 File Offset: 0x001F0FE4
	private int ᜀ(IXLSRange A_0)
	{
		int num = 9;
		int result;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return result;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				}
				goto Block_4;
			case 2:
				if (this.ᜊ.ᜆ() != null)
				{
					num = 14;
					continue;
				}
				goto IL_115;
			case 3:
				if (this.ᜊ.ᜆ().Length > 0)
				{
					num = 6;
					continue;
				}
				goto IL_115;
			case 4:
				goto IL_BB;
			case 5:
				num = 12;
				continue;
			case 6:
			{
				spr\u21F8 spr_u21F = this.ᜊ.ᜆ()[0] as spr\u21F8;
				num = 11;
				continue;
			}
			case 7:
				result = (A_0 as ICombinedRange).CellsCount;
				num = 1;
				continue;
			case 8:
				num2 = 1;
				goto IL_AF;
			case 10:
				goto IL_97;
			case 11:
			{
				spr\u21F8 spr_u21F;
				if (spr_u21F == null)
				{
					num = 5;
					continue;
				}
				num = 8;
				continue;
			}
			case 12:
				num2 = 0;
				goto IL_AF;
			case 13:
				num = 2;
				continue;
			case 14:
				num = 3;
				continue;
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			num = 10;
			continue;
			IL_97:
			if (this.ᜊ != null)
			{
				num = 13;
				continue;
			}
			goto IL_115;
			IL_AF:
			result = num2;
			num = 4;
			continue;
			IL_115:
			result = 0;
			num = 0;
		}
		IL_BB:
		return result;
		Block_4:
		if (false)
		{
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06003755 RID: 14165 RVA: 0x001F2168 File Offset: 0x001F1168
	private void ᜀ(IList<IRecordStorage> A_0, bool A_1, bool A_2, int A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 0;
			BiffRecordRaw item;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_115;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_121;
				case 3:
					num = 9;
					continue;
				case 4:
					this.ᜃ.ᜀ(A_2 ? spr\u23FE.TErrorBarValue.YDirectionPlus : spr\u23FE.TErrorBarValue.YDirectionMinus);
					goto IL_115;
				case 5:
				{
					spr\u237B.ᜀ(A_0, A_2 ? num2 : num3);
					item = spr\u175E.ᜀ(TBIFFRecord.Begin);
					A_0.Add(item);
					this.ᜀ(A_0, A_2);
					XlsChartSeries parentSeries = this.ᜆ.ParentSeries;
					spr\u237B.ᜀ(A_0, this.ᜂ, A_3, parentSeries.TrendErrorBarIndex, this.ᜋ);
					parentSeries.TrendErrorBarIndex++;
					sprᴀ sprᴀ = (sprᴀ)spr\u175E.ᜀ(TBIFFRecord.ChartSerParent);
					sprᴀ.ᜀ((ushort)(A_3 + 1));
					A_0.Add(sprᴀ);
					num = 6;
					continue;
				}
				case 6:
					if (A_1)
					{
						num = 1;
						continue;
					}
					num = 8;
					continue;
				case 7:
					goto IL_B0;
				case 8:
					this.ᜃ.ᜀ(A_2 ? spr\u23FE.TErrorBarValue.XDirectionPlus : spr\u23FE.TErrorBarValue.XDirectionMinus);
					num = 7;
					continue;
				case 9:
					goto IL_F9;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num3 = this.ᜀ(this.ᜈ);
				num2 = this.ᜀ(this.ᜇ);
				num = 5;
				continue;
				IL_115:
				num = 2;
			}
			IL_B0:
			goto IL_1DD;
			IL_F9:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌㱎", a_));
			IL_121:
			IL_1DD:
			A_0.Add(this.ᜃ);
			item = spr\u175E.ᜀ(TBIFFRecord.End);
			A_0.Add(item);
			return;
		}
		}
	}

	// Token: 0x06003756 RID: 14166 RVA: 0x001F2370 File Offset: 0x001F1370
	private Ptg[] ᜀ(bool A_0)
	{
		int num = 3;
		Ptg[] result;
		for (;;)
		{
			IXLSRange ixlsrange;
			IXLSRange ixlsrange2;
			Ptg[] array;
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				ixlsrange = this.ᜇ;
				goto IL_F5;
			case 2:
				return result;
			case 4:
				array = ((spr\u1A8B)ixlsrange2).ᜀ();
				goto IL_DD;
			case 5:
				num = 10;
				continue;
			case 6:
				num = 7;
				continue;
			case 7:
				if (!A_0)
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			case 8:
				return result;
			case 9:
				if (ixlsrange2 != null)
				{
					num = 0;
					continue;
				}
				num = 11;
				continue;
			case 10:
				ixlsrange = this.ᜈ;
				goto IL_F5;
			case 11:
				array = null;
				goto IL_DD;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			default:
				if (false)
				{
				}
				if (this.ᜌ)
				{
					num = 6;
					continue;
				}
				result = this.ᜊ.ᜆ();
				if (true)
				{
				}
				num = 2;
				continue;
			}
			IL_DD:
			result = array;
			num = 8;
			continue;
			IL_F5:
			ixlsrange2 = ixlsrange;
			num = 9;
		}
		return result;
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x001F24A8 File Offset: 0x001F14A8
	public bool ᜌ()
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
		return this.ᜉ;
	}

	// Token: 0x06003758 RID: 14168 RVA: 0x001F24EC File Offset: 0x001F14EC
	internal object[] ᜋ()
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

	// Token: 0x06003759 RID: 14169 RVA: 0x001F2530 File Offset: 0x001F1530
	internal void ᜁ(object[] A_0)
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
		this.ᜎ = A_0;
	}

	// Token: 0x0600375A RID: 14170 RVA: 0x001F2574 File Offset: 0x001F1574
	internal object[] ᜄ()
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
		return this.ᜏ;
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x001F25B8 File Offset: 0x001F15B8
	internal void ᜀ(object[] A_0)
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

	// Token: 0x0600375C RID: 14172 RVA: 0x001F25FC File Offset: 0x001F15FC
	public void ᜀ(bool[] A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					FormulaUtil.ᜀ(this.ᜊ.ᜆ(), A_0);
					num = 1;
					continue;
				case 1:
					goto IL_6D;
				}
				if (this.ᜊ == null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}
		IL_6D:
		if (true)
		{
		}
	}

	// Token: 0x0600375D RID: 14173 RVA: 0x001F2680 File Offset: 0x001F1680
	public void ᜀ(int[] A_0)
	{
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			Ptg[] a_;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				default:
					goto IL_68;
				}
				break;
			case 1:
				if (FormulaUtil.ᜀ(a_, A_0))
				{
					num = 4;
					continue;
				}
				return;
			case 2:
				goto IL_3C;
			case 4:
				this.ᜊ.ᜀ(a_);
				num = 0;
				continue;
			}
			if (this.ᜊ != null)
			{
				num = 2;
				continue;
			}
			return;
			IL_3C:
			a_ = this.ᜊ.ᜆ();
			num = 1;
		}
		IL_68:
		if (false)
		{
		}
	}

	// Token: 0x0600375E RID: 14174 RVA: 0x001F2734 File Offset: 0x001F1734
	private bool ᜀ(ErrorBarIncludeType A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E4;
			case 1:
				goto IL_6E;
			case 3:
				return true;
			case 4:
				if (this.ᜈ != null)
				{
					num = 0;
					continue;
				}
				return false;
			case 5:
				goto IL_B5;
			case 6:
				if (A_0 == ErrorBarIncludeType.Minus)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 7:
				if (A_0 == ErrorBarIncludeType.Plus)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B7;
					}
					if (false)
					{
					}
					num = 5;
					continue;
				}
				num = 6;
				continue;
			}
			if (this.ᜅ() != ErrorBarType.Custom)
			{
				num = 3;
			}
			else
			{
				num = 7;
			}
		}
		return true;
		IL_6E:
		this.ᜇ = null;
		return this.ᜈ != null;
		IL_B5:
		this.ᜈ = null;
		return this.ᜇ != null;
		IL_B7:
		return this.ᜇ != null;
		IL_E4:
		goto IL_B7;
	}

	// Token: 0x0600375F RID: 14175 RVA: 0x001F283C File Offset: 0x001F183C
	public spr\u237B ᜀ(object A_0, Dictionary<string, string> A_1)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			spr\u237B spr_u237B;
			XlsWorkbook book;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (this.ᜃ != null)
				{
					num = 5;
					continue;
				}
				goto IL_7B;
			case 1:
				goto IL_7B;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					if (false)
					{
					}
					spr_u237B.ᜇ = ((ICombinedRange)this.ᜇ).Clone(spr_u237B, A_1, book);
					num = 7;
					continue;
				}
				break;
			case 4:
				goto IL_50;
			case 5:
				spr_u237B.ᜃ = (spr\u23FE)spr\u1CD3.ᜀ(this.ᜃ);
				num = 1;
				continue;
			case 6:
				spr_u237B.ᜈ = ((ICombinedRange)this.ᜈ).Clone(spr_u237B, A_1, book);
				num = 8;
				continue;
			case 7:
				goto IL_153;
			case 8:
				return spr_u237B;
			case 9:
				if (this.ᜇ != null)
				{
					num = 2;
					continue;
				}
				goto IL_153;
			case 10:
				if (this.ᜈ != null)
				{
					num = 6;
					continue;
				}
				return spr_u237B;
			}
			goto IL_45;
			IL_48:
			num = 4;
			continue;
			IL_45:
			if (A_0 == null)
			{
				goto IL_48;
			}
			spr_u237B = (spr\u237B)base.MemberwiseClone();
			spr_u237B.SetParent(A_0);
			spr_u237B.ᜀ();
			book = spr_u237B.ᜆ.ParentBook;
			spr_u237B.ᜂ = this.ᜂ.Clone(spr_u237B);
			num = 0;
			continue;
			IL_7B:
			num = 9;
			continue;
			IL_153:
			num = 10;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒", a_));
	}

	// Token: 0x04001855 RID: 6229
	public const int ᜀ = 1;

	// Token: 0x04001856 RID: 6230
	public const int ᜁ = 10;

	// Token: 0x04001857 RID: 6231
	private XlsChartBorder ᜂ;

	// Token: 0x04001858 RID: 6232
	private spr\u23FE ᜃ;

	// Token: 0x04001859 RID: 6233
	private ChartShadow ᜄ;

	// Token: 0x0400185A RID: 6234
	private ErrorBarIncludeType ᜅ;

	// Token: 0x0400185B RID: 6235
	private XlsChartSerie ᜆ;

	// Token: 0x0400185C RID: 6236
	private IXLSRange ᜇ;

	// Token: 0x0400185D RID: 6237
	private IXLSRange ᜈ;

	// Token: 0x0400185E RID: 6238
	private bool ᜉ;

	// Token: 0x0400185F RID: 6239
	private sprᢀ ᜊ;

	// Token: 0x04001860 RID: 6240
	private sprᣐ ᜋ;

	// Token: 0x04001861 RID: 6241
	private bool ᜌ;

	// Token: 0x04001862 RID: 6242
	private Format3D \u170D;

	// Token: 0x04001863 RID: 6243
	private object[] ᜎ;

	// Token: 0x04001864 RID: 6244
	private object[] ᜏ;

	// Token: 0x04001865 RID: 6245
	private bool ᜐ;

	// Token: 0x04001866 RID: 6246
	private bool ᜑ;

	// Token: 0x04001867 RID: 6247
	private string \u1712;
}
