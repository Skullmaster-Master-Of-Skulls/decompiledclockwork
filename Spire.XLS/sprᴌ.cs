using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Charts;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000425 RID: 1061
internal class sprᴌ : XlsObject, IChartTrendLine
{
	// Token: 0x06004032 RID: 16434 RVA: 0x00240C38 File Offset: 0x0023FC38
	static sprᴌ()
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᴌ.ᜁ = new Dictionary<TrendLineType, string>(6);
		sprᴌ.ᜁ.Add(TrendLineType.Exponential, RecordTableEnumerator.b("ͅぇ㩉⍋⁍繏牑", a_));
		sprᴌ.ᜁ.Add(TrendLineType.Linear, RecordTableEnumerator.b("੅ⅇ⑉⥋⽍≏牑", a_));
		sprᴌ.ᜁ.Add(TrendLineType.Logarithmic, RecordTableEnumerator.b("੅❇ⵉ手湍", a_));
		sprᴌ.ᜁ.Add(TrendLineType.Moving_Average, RecordTableEnumerator.b("晅㡇⽉㹋恍灏ὑ㭓⁕癗穙ᵛ⡝ݟ䱡䑣", a_));
		sprᴌ.ᜁ.Add(TrendLineType.Polynomial, RecordTableEnumerator.b("ᙅ❇♉㕋恍灏", a_));
		sprᴌ.ᜁ.Add(TrendLineType.Power, RecordTableEnumerator.b("ᙅ❇㵉⥋㱍灏", a_));
	}

	// Token: 0x06004033 RID: 16435 RVA: 0x00240D20 File Offset: 0x0023FD20
	internal sprᴌ(spr\u1DF5 A_0, object A_1)
	{
		this.ᜆ = TrendLineType.Linear;
		this.ᜇ = true;
		this.ᜈ = "";
		base..ctor(A_0, A_1);
		this.ᜃ();
		this.ᜄ = new ChartBorder((spr\u2158)base.ReservedHandle, this);
		this.ᜄ.HasLineProperties = true;
		this.ᜂ = (spr\u17F4)spr\u175E.ᜀ(TBIFFRecord.ChartSerAuxTrend);
	}

	// Token: 0x06004034 RID: 16436 RVA: 0x00240D8C File Offset: 0x0023FD8C
	private void ᜃ()
	{
		int a_ = 5;
		for (;;)
		{
			this.ᜅ = (XlsChartSerie)base.FindParent(typeof(XlsChartSerie));
			if (this.ᜅ != null)
			{
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_56;
			}
		}
		IL_56:
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("砺尼儾⽀ⱂㅄ杆⽈≊⍌⭎煐⍒㑔╖㱘㕚⥜罞๠Ţཤɦ੨ὪṬ", a_));
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x00240E0C File Offset: 0x0023FE0C
	internal sprᴌ(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, out XlsChartLegendEntry A_4)
	{
		int a_ = 8;
		this.ᜆ = TrendLineType.Linear;
		this.ᜇ = true;
		this.ᜈ = "";
		base..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃", a_));
		}
		this.ᜃ();
		A_4 = null;
		this.ᜀ(A_2, ref A_3, ref A_4);
	}

	// Token: 0x06004036 RID: 16438 RVA: 0x00240E74 File Offset: 0x0023FE74
	public IChartBorder ᜄ()
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

	// Token: 0x06004037 RID: 16439 RVA: 0x00240EB8 File Offset: 0x0023FEB8
	internal IShadow \u1713()
	{
		for (;;)
		{
			IL_00:
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
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜃ = new ChartShadow(base.AppImplementation, this);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_76;
				}
				if (this.ᜃ != null)
				{
					goto IL_78;
				}
				num = 1;
			}
		}
		IL_76:
		IL_78:
		return this.ᜃ;
	}

	// Token: 0x06004038 RID: 16440 RVA: 0x00240F44 File Offset: 0x0023FF44
	public bool \u170D()
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
		return this.ᜃ != null;
	}

	// Token: 0x06004039 RID: 16441 RVA: 0x00240F8C File Offset: 0x0023FF8C
	internal void ᜂ(bool A_0)
	{
		while (A_0)
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
				this.\u1713();
				return;
			}
		}
		this.ᜃ = null;
	}

	// Token: 0x0600403A RID: 16442 RVA: 0x00240FDC File Offset: 0x0023FFDC
	internal IFormat3D ᜋ()
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜋ = new Format3D(base.AppImplementation, this);
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_76;
				}
				if (true)
				{
				}
				if (this.ᜋ != null)
				{
					goto IL_78;
				}
				num = 0;
			}
		}
		IL_76:
		IL_78:
		return this.ᜋ;
	}

	// Token: 0x0600403B RID: 16443 RVA: 0x00241068 File Offset: 0x00240068
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
		return this.ᜋ != null;
	}

	// Token: 0x0600403C RID: 16444 RVA: 0x002410B0 File Offset: 0x002400B0
	internal void ᜄ(bool A_0)
	{
		while (A_0)
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
				this.ᜋ();
				return;
			}
		}
		this.ᜋ = null;
	}

	// Token: 0x0600403D RID: 16445 RVA: 0x00241100 File Offset: 0x00240100
	public double ᜆ()
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
		return this.ᜂ.ᜈ();
	}

	// Token: 0x0600403E RID: 16446 RVA: 0x00241148 File Offset: 0x00240148
	public void ᜁ(double A_0)
	{
		for (;;)
		{
			IL_00:
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜂ();
						this.ᜀ(A_0);
						this.ᜂ.ᜂ(A_0);
						num = 0;
						continue;
					}
					break;
				}
				if (A_0 == this.ᜆ())
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x0600403F RID: 16447 RVA: 0x002411D8 File Offset: 0x002401D8
	public double ᜌ()
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
		return this.ᜂ.ᜃ();
	}

	// Token: 0x06004040 RID: 16448 RVA: 0x00241220 File Offset: 0x00240220
	public void ᜂ(double A_0)
	{
		int a_ = 14;
		int num = 1;
		for (;;)
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
				switch (num)
				{
				case 0:
					this.ᜂ();
					num = 4;
					continue;
				case 2:
					goto IL_82;
				case 3:
					goto IL_C0;
				case 4:
					if (A_0 < 0.0)
					{
						num = 3;
						continue;
					}
					this.ᜂ.ᜁ(A_0);
					goto IL_7A;
				}
				if (this.ᜌ() != A_0)
				{
					num = 0;
					continue;
				}
				return;
			}
			IL_7A:
			num = 2;
		}
		IL_82:
		return;
		IL_C0:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ƀ⥅㩇㵉ⵋ㱍㑏", a_));
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x002412F0 File Offset: 0x002402F0
	public bool ᜐ()
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
		return this.ᜂ.ᜄ();
	}

	// Token: 0x06004042 RID: 16450 RVA: 0x00241338 File Offset: 0x00240338
	public void ᜅ(bool A_0)
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
					return;
				case 1:
					if (true)
					{
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
						this.ᜂ();
						this.ᜂ.ᜀ(A_0);
						this.ᜀ(A_0);
						num = 0;
						continue;
					}
					break;
				}
				if (A_0 == this.ᜐ())
				{
					return;
				}
				num = 1;
			}
		}
	}

	// Token: 0x06004043 RID: 16451 RVA: 0x002413C8 File Offset: 0x002403C8
	public bool \u1712()
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
		return this.ᜂ.ᜀ();
	}

	// Token: 0x06004044 RID: 16452 RVA: 0x00241410 File Offset: 0x00240410
	public void ᜁ(bool A_0)
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜂ();
						this.ᜂ.ᜁ(A_0);
						this.ᜀ(A_0);
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
				if (A_0 == this.\u1712())
				{
					return;
				}
				num = 0;
			}
		}
	}

	// Token: 0x06004045 RID: 16453 RVA: 0x002414A0 File Offset: 0x002404A0
	public double ᜎ()
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
		return this.ᜂ.ᜆ();
	}

	// Token: 0x06004046 RID: 16454 RVA: 0x002414E8 File Offset: 0x002404E8
	public void ᜃ(double A_0)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 <= 0.0)
				{
					num = 4;
					continue;
				}
				goto IL_D0;
			case 1:
				goto IL_A3;
			case 2:
				goto IL_E7;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A3;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				goto IL_A1;
			case 5:
				num = 0;
				continue;
			case 6:
				if (this.ᜇ() == TrendLineType.Exponential)
				{
					num = 5;
					continue;
				}
				goto IL_D0;
			}
			if (this.ᜎ() != A_0)
			{
				num = 1;
				continue;
			}
			goto IL_E9;
			IL_A3:
			this.ᜂ();
			this.ᜁ();
			num = 6;
			continue;
			IL_D0:
			this.ᜂ.ᜀ(A_0);
			num = 2;
		}
		IL_A1:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ཅ♇㹉⥋㱍㍏㝑⑓≕", a_));
		IL_E7:
		IL_E9:
		if (true)
		{
		}
	}

	// Token: 0x06004047 RID: 16455 RVA: 0x002415E8 File Offset: 0x002405E8
	public bool ᜉ()
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
		return double.IsNaN(this.ᜎ());
	}

	// Token: 0x06004048 RID: 16456 RVA: 0x00241630 File Offset: 0x00240630
	public void ᜆ(bool A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
				this.ᜃ((double)((this.ᜇ() == TrendLineType.Exponential) ? 1 : 0));
				num = 2;
				continue;
			case 2:
				return;
			case 3:
				goto IL_83;
			case 5:
				if (!A_0)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (this.ᜉ() != A_0)
			{
				num = 0;
				continue;
			}
			return;
			IL_48:
			this.ᜂ();
			this.ᜁ();
			num = 5;
		}
		IL_83:
		this.ᜃ(spr\u17F4.ᜂ);
	}

	// Token: 0x06004049 RID: 16457 RVA: 0x00241708 File Offset: 0x00240708
	public TrendLineType ᜇ()
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
		return this.ᜆ;
	}

	// Token: 0x0600404A RID: 16458 RVA: 0x0024174C File Offset: 0x0024074C
	public void ᜁ(TrendLineType A_0)
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
		this.ᜆ = A_0;
		this.ᜀ(A_0);
	}

	// Token: 0x0600404B RID: 16459 RVA: 0x00241798 File Offset: 0x00240798
	public int \u1714()
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
		return (int)this.ᜂ.ᜁ();
	}

	// Token: 0x0600404C RID: 16460 RVA: 0x002417E0 File Offset: 0x002407E0
	public void ᜂ(int A_0)
	{
		int a_ = 19;
		while (A_0 <= 0)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("و㥊⥌⩎⍐", a_));
			}
		}
		if (true)
		{
		}
		this.ᜂ.ᜀ((byte)A_0);
	}

	// Token: 0x0600404D RID: 16461 RVA: 0x0024184C File Offset: 0x0024084C
	public bool ᜈ()
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

	// Token: 0x0600404E RID: 16462 RVA: 0x00241890 File Offset: 0x00240890
	public void ᜃ(bool A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0)
				{
					num = 2;
					continue;
				}
				goto IL_4D;
			case 1:
				goto IL_4D;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4D;
				default:
					if (false)
					{
					}
					this.ᜈ = string.Empty;
					num = 1;
					continue;
				}
				break;
			case 3:
				return;
			case 5:
				num = 0;
				continue;
			}
			if (this.ᜈ() != A_0)
			{
				if (true)
				{
				}
				num = 5;
				continue;
			}
			break;
			IL_4D:
			this.ᜇ = A_0;
			num = 3;
		}
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x0024194C File Offset: 0x0024094C
	public string ᜏ()
	{
		int a_ = 19;
		int num = 1;
		string text;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				TrendLineType trendLineType;
				switch (num)
				{
				case 0:
					goto IL_63;
				case 2:
					text = this.\u1714().ToString() + text;
					goto IL_7A;
				case 3:
					goto IL_82;
				case 4:
					if (trendLineType == TrendLineType.Moving_Average)
					{
						num = 2;
						continue;
					}
					goto IL_C2;
				}
				if (!this.ᜈ())
				{
					num = 0;
					continue;
				}
				trendLineType = this.ᜇ();
				text = sprᴌ.ᜁ[trendLineType];
				if (true)
				{
				}
				num = 4;
				continue;
			}
			}
			IL_7A:
			num = 3;
		}
		IL_63:
		return this.ᜈ;
		IL_82:
		IL_C2:
		text = text + RecordTableEnumerator.b("慈", a_) + this.ᜅ.Name + RecordTableEnumerator.b("恈", a_);
		return text;
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x00241A4C File Offset: 0x00240A4C
	public void ᜀ(string A_0)
	{
		int a_ = 8;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("瀽ℿ⽁⅃", a_));
			}
		}
		this.ᜈ = A_0;
		this.ᜃ(false);
	}

	// Token: 0x06004051 RID: 16465 RVA: 0x00241AB8 File Offset: 0x00240AB8
	public IChartTextArea ᜅ()
	{
		int a_ = 8;
		while (this.ᜉ == null)
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
				throw new NotSupportedException(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉㹋⭍⑏❑♓㡕硗㹙㵛⩝ş䉡ࡣݥ੧ཀྵk䁭", a_));
			}
		}
		return this.ᜉ;
	}

	// Token: 0x06004052 RID: 16466 RVA: 0x00241B20 File Offset: 0x00240B20
	public void ᜑ()
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
		this.ᜄ.UseDefaultFormat = true;
		this.ᜆ = TrendLineType.Linear;
		this.ᜇ = true;
		this.ᜈ = "";
		this.ᜂ.ᜀ(1);
		this.ᜂ.ᜁ(false);
		this.ᜂ.ᜀ(false);
		this.ᜉ = null;
		this.ᜂ.ᜁ(0.0);
		this.ᜂ.ᜂ(0.0);
		this.ᜂ.ᜀ(spr\u17F4.ᜂ);
	}

	// Token: 0x06004053 RID: 16467 RVA: 0x00241BE4 File Offset: 0x00240BE4
	public void ᜀ(bool[] A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_48;
			}
			if (this.ᜉ == null)
			{
				break;
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
				num = 1;
				continue;
			}
			IL_48:
			if (true)
			{
			}
			this.ᜉ.ᜀ(A_0);
			num = 0;
		}
	}

	// Token: 0x06004054 RID: 16468 RVA: 0x00241C64 File Offset: 0x00240C64
	public void ᜀ(int[] A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5A;
			case 2:
				return;
			}
			if (true)
			{
			}
			if (this.ᜉ == null)
			{
				break;
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
				num = 1;
				continue;
			}
			IL_5A:
			this.ᜉ.ᜀ(A_0);
			num = 2;
		}
	}

	// Token: 0x06004055 RID: 16469 RVA: 0x00241CE4 File Offset: 0x00240CE4
	private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1, ref XlsChartLegendEntry A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 30;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartSeriesText)
					{
						num = 2;
						continue;
					}
					spr\u1D35 spr_u1D = (spr\u1D35)biffRecordRaw;
					this.ᜀ(spr_u1D.ᜁ());
					num = 32;
					continue;
				}
				case 1:
				{
					bool hasLegend;
					if (hasLegend)
					{
						num = 11;
						continue;
					}
					goto IL_226;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42D;
					default:
						if (false)
						{
						}
						num = 31;
						continue;
					}
					break;
				case 3:
					goto IL_3FF;
				case 4:
					goto IL_245;
				case 5:
					goto IL_226;
				case 6:
				{
					TBIFFRecord typeCode;
					if (typeCode <= TBIFFRecord.ChartSeriesText)
					{
						num = 21;
						continue;
					}
					num = 19;
					continue;
				}
				case 7:
					goto IL_189;
				case 8:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartSerAuxTrend)
					{
						num = 20;
						continue;
					}
					this.ᜂ = (spr\u17F4)biffRecordRaw;
					num = 13;
					continue;
				}
				case 9:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartDataFormat:
						this.ᜊ = (int)((sprᲡ)biffRecordRaw).ᜀ();
						num = 17;
						continue;
					case TBIFFRecord.ChartLineFormat:
						this.ᜄ = new ChartBorder((spr\u2158)base.ReservedHandle, this, (spr\u22F3)biffRecordRaw);
						num = 27;
						continue;
					default:
						num = 12;
						continue;
					}
					break;
				}
				case 10:
					goto IL_189;
				case 11:
				{
					XlsChart parentChart;
					ChartLegendEntriesColl a_2 = (ChartLegendEntriesColl)parentChart.Legend.LegendEntries;
					num = 14;
					continue;
				}
				case 12:
					num = 0;
					continue;
				case 13:
					goto IL_189;
				case 14:
					goto IL_226;
				case 15:
				{
					if (biffRecordRaw.TypeCode != TBIFFRecord.ChartSeries)
					{
						num = 3;
						continue;
					}
					A_1 += 2;
					int num2 = 1;
					XlsChart parentChart = this.ᜅ.ParentChart;
					bool hasLegend = parentChart.HasLegend;
					ChartLegendEntriesColl a_2 = null;
					num = 1;
					continue;
				}
				case 16:
				{
					bool hasLegend;
					if (hasLegend)
					{
						num = 23;
						continue;
					}
					goto IL_189;
				}
				case 17:
					goto IL_189;
				case 18:
					num = 22;
					continue;
				case 19:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
					{
						int num2;
						num2++;
						num = 24;
						continue;
					}
					case TBIFFRecord.End:
					{
						int num2;
						num2--;
						num = 28;
						continue;
					}
					default:
						num = 18;
						continue;
					}
					break;
				}
				case 20:
					num = 7;
					continue;
				case 21:
					num = 9;
					continue;
				case 22:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartLegendxn)
					{
						num = 25;
						continue;
					}
					num = 16;
					continue;
				}
				case 23:
				{
					int a_3 = this.ᜅ.ParentSeries.TrendIndex;
					ChartLegendEntriesColl a_2;
					A_2 = new ChartLegendEntry((spr\u2158)base.ReservedHandle, a_2, a_3, A_0, ref A_1);
					A_1--;
					num = 10;
					continue;
				}
				case 24:
					goto IL_189;
				case 25:
					num = 8;
					continue;
				case 26:
					goto IL_C2;
				case 27:
					goto IL_42D;
				case 28:
					goto IL_189;
				case 29:
				{
					int num2;
					if (num2 <= 0)
					{
						num = 4;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 6;
					continue;
				}
				case 31:
					goto IL_189;
				case 32:
					goto IL_189;
				}
				if (A_0 == null)
				{
					num = 26;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				num = 15;
				continue;
				IL_189:
				A_1++;
				num = 5;
				continue;
				IL_226:
				num = 29;
				continue;
				IL_42D:
				goto IL_189;
			}
			IL_C2:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅", a_));
			IL_245:
			A_1--;
			this.ᜀ();
			return;
			IL_3FF:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ቁ⭃㕅", a_));
		}
		}
	}

	// Token: 0x06004056 RID: 16470 RVA: 0x0024213C File Offset: 0x0024113C
	[CLSCompliant(false)]
	public void ᜃ(IList<IRecordStorage> A_0)
	{
		int a_ = 6;
		if (A_0 != null)
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
			{
				if (false)
				{
				}
				int index = this.ᜅ.Index;
				XlsChartSeries parentSeries = this.ᜅ.ParentSeries;
				spr\u237B.ᜀ(A_0, 0);
				BiffRecordRaw item = spr\u175E.ᜀ(TBIFFRecord.Begin);
				A_0.Add(item);
				this.ᜂ(A_0);
				spr\u237B.ᜀ(A_0, this.ᜄ, index, parentSeries.TrendErrorBarIndex, null);
				sprᴀ sprᴀ = (sprᴀ)spr\u175E.ᜀ(TBIFFRecord.ChartSerParent);
				this.ᜀ(parentSeries.TrendLabels);
				sprᴀ.ᜀ((ushort)(index + 1));
				A_0.Add(sprᴀ);
				this.ᜂ.ᜀ(this.ᜆ);
				A_0.Add(this.ᜂ);
				this.ᜁ(A_0);
				parentSeries.TrendIndex++;
				parentSeries.TrendErrorBarIndex++;
				item = spr\u175E.ᜀ(TBIFFRecord.End);
				A_0.Add(item);
				return;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
	}

	// Token: 0x06004057 RID: 16471 RVA: 0x00242264 File Offset: 0x00241264
	private void ᜂ(IList<IRecordStorage> A_0)
	{
		int a_ = 18;
		int num = 4;
		sprᢀ sprᢀ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜈ())
				{
					num = 3;
					continue;
				}
				goto IL_F7;
			case 1:
				goto IL_40;
			case 2:
				goto IL_99;
			case 3:
				for (;;)
				{
					spr\u1D35 spr_u1D = (spr\u1D35)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesText);
					spr_u1D.ᜀ(this.ᜈ);
					A_0.Add(spr_u1D);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_88;
					}
				}
				IL_88:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				sprᢀ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
				sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToTitleOrText);
				sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
				A_0.Add(sprᢀ);
				num = 0;
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
		IL_99:
		IL_F7:
		sprᢀ = (sprᢀ)sprᢀ.Clone();
		sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToValues);
		A_0.Add(sprᢀ);
		sprᢀ = (sprᢀ)sprᢀ.Clone();
		sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToCategories);
		A_0.Add(sprᢀ);
		sprᢀ = (sprᢀ)sprᢀ.Clone();
		sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToBubbles);
		A_0.Add(sprᢀ);
	}

	// Token: 0x06004058 RID: 16472 RVA: 0x002423B8 File Offset: 0x002413B8
	private void ᜁ(IList<IRecordStorage> A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 0;
			XlsChart parentChart;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					if (!parentChart.HasLegend)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_A2;
					}
					break;
				case 3:
					goto IL_45;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					if (true)
					{
					}
					parentChart = this.ᜅ.ParentChart;
					num = 2;
				}
			}
			IL_45:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏⅑", a_));
			IL_A2:
			if (false)
			{
			}
			int iIndex = this.ᜅ.ParentSeries.TrendIndex;
			IChartLegendEntries legendEntries = parentChart.Legend.LegendEntries;
			XlsChartLegendEntry xlsChartLegendEntry = (XlsChartLegendEntry)legendEntries[iIndex];
			xlsChartLegendEntry.ᜀ(A_0);
			return;
		}
		}
	}

	// Token: 0x06004059 RID: 16473 RVA: 0x002424A8 File Offset: 0x002414A8
	private void ᜀ(IList<IRecordStorage> A_0)
	{
		if (true)
		{
		}
		if (this.ᜉ != null)
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
				this.ᜉ.ObjectLink.ᜁ((ushort)this.ᜅ.ParentSeries.TrendErrorBarIndex);
				this.ᜉ.SerializeDataToList(A_0);
				return;
			}
		}
	}

	// Token: 0x0600405A RID: 16474 RVA: 0x0024251C File Offset: 0x0024151C
	private void ᜂ()
	{
		int a_ = 17;
		if (this.ᜇ() != TrendLineType.Moving_Average)
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
				return;
			}
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎⅐⅒㩔❖㱘⥚⥜♞䅠ݢ੤ɦᩨժɬ᭮兰rtݶॸᑺོ୾ꆀꞆﺊﾌﶎﶒ랖얠莢즤캦잨캪趬\udbae좰쎲킴馶", a_));
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x00242580 File Offset: 0x00241580
	private void ᜁ()
	{
		int a_ = 7;
		for (;;)
		{
			int num;
			TrendLineType trendLineType;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_58:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				trendLineType = this.ᜇ();
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (trendLineType != TrendLineType.Logarithmic)
					{
						num = 2;
						continue;
					}
					goto IL_64;
				case 1:
					if (trendLineType == TrendLineType.Power)
					{
						num = 3;
						continue;
					}
					return;
				case 2:
					goto IL_58;
				case 3:
					goto IL_8F;
				}
				break;
			}
		}
		IL_64:
		throw new NotSupportedException(RecordTableEnumerator.b("椼圾⡀あ敄㝆㭈⑊㵌⩎⍐❒ⱔ睖㵘㑚㡜ⱞའౢᅤ䝦ᩨṪᵬὮṰŲŴ坶ၸᕺ嵼᱾ﾊ권ﮎﮔ릘쒠莢톤\udea6\ud9a8캪莬", a_));
		IL_8F:
		if (true)
		{
		}
		goto IL_64;
	}

	// Token: 0x0600405C RID: 16476 RVA: 0x00242628 File Offset: 0x00241628
	private void ᜀ(double A_0)
	{
		int a_ = 13;
		int num = 3;
		for (;;)
		{
			string a;
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
				if (!(a == RecordTableEnumerator.b("ł⑄㕆", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_83;
			case 1:
				goto IL_186;
			case 2:
				num = 10;
				continue;
			case 4:
				goto IL_65;
			case 5:
				num = 8;
				continue;
			case 6:
				flag = true;
				goto IL_118;
			case 7:
				num = 9;
				continue;
			case 8:
				if (!(a == RecordTableEnumerator.b("B⩄⭆㱈♊⍌", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_83;
			case 9:
				IL_1AA:
				flag = (a == RecordTableEnumerator.b("གⱄ⥆ⱈ", a_));
				goto IL_118;
			case 10:
				if (A_0 > 0.5)
				{
					num = 12;
					continue;
				}
				goto IL_15A;
			case 11:
				if (a == RecordTableEnumerator.b("ɂ㝄≆⡈", a_))
				{
					num = 1;
					continue;
				}
				return;
			case 12:
				goto IL_1E5;
			case 13:
				if (flag2)
				{
					num = 2;
					continue;
				}
				goto IL_15A;
			}
			if (A_0 < 0.0)
			{
				num = 4;
				continue;
			}
			a = XlsChartFormat.ᜉ(this.ᜅ.SerieType);
			num = 0;
			continue;
			IL_83:
			num = 6;
			continue;
			IL_118:
			flag2 = flag;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1AA;
			default:
				if (false)
				{
				}
				num = 13;
				continue;
			}
			IL_15A:
			num = 11;
		}
		IL_65:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ł⑄⑆≈㱊ⱌ㵎㕐", a_));
		IL_186:
		throw new NotSupportedException(RecordTableEnumerator.b("ᝂⵄ⹆㩈歊㵌㵎㹐⍒ご╖ⵘ≚絜㭞๠٢ᙤ०٨Ὢ䵬ᱮѰͲմᡶ୸ེ᡼᭾ꆀꞆﺊﾌﶎﶒ랖얠쾢첤즦첨讪슬춮\udbb0횲횴쎶鞸", a_));
		IL_1E5:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᝂⵄ≆楈㵊ⱌ⍎⑐㙒畔㩖ⱘ⡚⥜罞͠٢䕤զ౨Ὢᩬ੮ᑰᵲ啴൶ᱸॺቼ彾Ꞇ릈꞊뢌", a_));
	}

	// Token: 0x0600405D RID: 16477 RVA: 0x0024281C File Offset: 0x0024181C
	private void ᜀ(TrendLineType A_0)
	{
		int a_ = 7;
		bool flag;
		for (;;)
		{
			flag = (A_0 == TrendLineType.Moving_Average);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜅ.PointNumber < 3)
					{
						num = 3;
						continue;
					}
					goto IL_D6;
				case 1:
					if (!this.ᜅ.ParentBook.Loading)
					{
						num = 4;
						continue;
					}
					goto IL_D6;
				case 2:
					goto IL_E1;
				case 3:
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
					num = 1;
					continue;
				case 4:
					goto IL_C0;
				case 5:
					if (flag)
					{
						num = 6;
						continue;
					}
					goto IL_D6;
				case 6:
					num = 0;
					continue;
				}
				break;
				IL_D6:
				num = 2;
			}
		}
		IL_C0:
		throw new NotSupportedException(RecordTableEnumerator.b("椼圾⡀あ敄㍆㭈⹊⍌⭎㵐㩒㭔㉖祘⽚⑜⽞Ѡ䍢ᙤቦᥨ᭪ɬᵮհᙲᅴ坶ၸᵺ嵼᱾ꦈ꾎릘쾠힢횤螦삨讪쪬\uddae풰튲솴鞶춸펺\ud8bc톾", a_));
		IL_E1:
		this.ᜂ.ᜀ((flag || A_0 == TrendLineType.Polynomial) ? 2 : 1);
		this.ᜂ.ᜀ(spr\u17F4.ᜂ);
	}

	// Token: 0x0600405E RID: 16478 RVA: 0x00242938 File Offset: 0x00241938
	private void ᜀ(int A_0)
	{
		int a_ = 2;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 10;
				continue;
			case 1:
				if (this.ᜆ != TrendLineType.Moving_Average)
				{
					num = 5;
					continue;
				}
				goto IL_80;
			case 2:
				IL_102:
				if (this.ᜆ == TrendLineType.Moving_Average)
				{
					num = 3;
					continue;
				}
				goto IL_E3;
			case 3:
				goto IL_9C;
			case 4:
				goto IL_7B;
			case 5:
			{
				int num2 = this.ᜅ.PointNumber - 1;
				num = 7;
				continue;
			}
			case 7:
				goto IL_80;
			case 8:
				if (A_0 >= 2)
				{
					num = 0;
					continue;
				}
				goto IL_118;
			case 9:
				num = 2;
				continue;
			case 10:
			{
				int num2;
				if (A_0 > num2)
				{
					num = 4;
					continue;
				}
				return;
			}
			}
			if (this.ᜆ != TrendLineType.Polynomial)
			{
				num = 9;
				continue;
			}
			goto IL_9C;
			IL_80:
			num = 8;
			continue;
			IL_9C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_102;
			default:
			{
				if (false)
				{
				}
				int num2 = 6;
				if (true)
				{
				}
				num = 1;
				break;
			}
			}
		}
		IL_7B:
		goto IL_118;
		IL_E3:
		throw new NotSupportedException(RecordTableEnumerator.b("氷刹唻䴽怿㉁㙃⥅㡇⽉㹋㩍⥏牑こ㥕㵗⥙㉛ㅝᑟ䉡ᝣ፥ᡧᩩͫᱭѯ剱ᵳᡵ塷᥹ॻ౽ꢇﺉﺋﺏﾕﾙ벛\ud99f튡솣袥", a_));
		IL_118:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("眷䠹堻嬽㈿", a_));
	}

	// Token: 0x0600405F RID: 16479 RVA: 0x00242A90 File Offset: 0x00241A90
	private void ᜀ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.\u1712())
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				this.ᜉ = null;
				num = 8;
				continue;
			case 3:
				goto IL_BD;
			case 4:
				num = 5;
				continue;
			case 5:
				if (this.ᜉ == null)
				{
					num = 3;
					continue;
				}
				goto IL_5E;
			case 6:
				IL_82:
				if (!this.ᜐ())
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				return;
			case 7:
				num = 0;
				continue;
			case 8:
				return;
			}
			if (A_0)
			{
				num = 4;
				continue;
			}
			IL_5E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_82;
			default:
				if (false)
				{
				}
				num = 6;
				break;
			}
		}
		IL_BD:
		this.ᜉ = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
		this.ᜉ.IsTrend = true;
	}

	// Token: 0x06004060 RID: 16480 RVA: 0x00242BA0 File Offset: 0x00241BA0
	private void ᜀ()
	{
		XlsChartSeries parentSeries;
		for (;;)
		{
			bool flag = this.ᜂ.ᜅ() == spr\u17F4.TRegression.Polynomial;
			parentSeries = this.ᜅ.ParentSeries;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_50;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_AB;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜆ = TrendLineType.Linear;
						break;
					}
					num = 2;
					continue;
				case 4:
					if (this.ᜂ.ᜁ() < 2)
					{
						num = 3;
						continue;
					}
					goto IL_50;
				case 5:
					goto IL_69;
				}
				break;
				IL_50:
				this.ᜆ = (TrendLineType)this.ᜂ.ᜅ();
				num = 5;
			}
		}
		IL_69:
		if (true)
		{
		}
		IL_AB:
		parentSeries.TrendIndex++;
	}

	// Token: 0x06004061 RID: 16481 RVA: 0x00242C90 File Offset: 0x00241C90
	public void ᜀ(XlsChartTextArea A_0)
	{
		int a_ = 3;
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
				this.ᜉ = A_0;
				this.ᜉ.IsTrend = true;
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺堼帾", a_));
	}

	// Token: 0x06004062 RID: 16482 RVA: 0x00242D00 File Offset: 0x00241D00
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
		return this.ᜊ;
	}

	// Token: 0x06004063 RID: 16483 RVA: 0x00242D44 File Offset: 0x00241D44
	public void ᜁ(int A_0)
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

	// Token: 0x06004064 RID: 16484 RVA: 0x00242D88 File Offset: 0x00241D88
	public sprᴌ ᜀ(object A_0, Dictionary<int, int> A_1, Dictionary<string, string> A_2)
	{
		int a_ = 13;
		int num = 6;
		for (;;)
		{
			sprᴌ sprᴌ;
			switch (num)
			{
			case 0:
				return sprᴌ;
			case 1:
				goto IL_49;
			case 2:
				goto IL_47;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					sprᴌ.ᜂ = (spr\u17F4)spr\u1CD3.ᜀ(this.ᜂ);
					break;
				}
				num = 1;
				continue;
			case 4:
				if (this.ᜂ != null)
				{
					num = 3;
					continue;
				}
				goto IL_49;
			case 5:
				if (this.ᜉ != null)
				{
					num = 7;
					continue;
				}
				return sprᴌ;
			case 7:
				if (true)
				{
				}
				sprᴌ.ᜉ = (XlsChartTextArea)this.ᜉ.Clone(sprᴌ, A_1, A_2);
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			sprᴌ = (sprᴌ)base.MemberwiseClone();
			sprᴌ.SetParent(A_0);
			sprᴌ.ᜃ();
			sprᴌ.ᜄ = this.ᜄ.Clone(sprᴌ);
			num = 4;
			continue;
			IL_49:
			num = 5;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍂⑄㕆ⱈ╊㥌", a_));
	}

	// Token: 0x04001CC8 RID: 7368
	private const int ᜀ = 6;

	// Token: 0x04001CC9 RID: 7369
	private static Dictionary<TrendLineType, string> ᜁ;

	// Token: 0x04001CCA RID: 7370
	private spr\u17F4 ᜂ;

	// Token: 0x04001CCB RID: 7371
	private ChartShadow ᜃ;

	// Token: 0x04001CCC RID: 7372
	private XlsChartBorder ᜄ;

	// Token: 0x04001CCD RID: 7373
	private XlsChartSerie ᜅ;

	// Token: 0x04001CCE RID: 7374
	private TrendLineType ᜆ;

	// Token: 0x04001CCF RID: 7375
	private bool ᜇ;

	// Token: 0x04001CD0 RID: 7376
	private string ᜈ;

	// Token: 0x04001CD1 RID: 7377
	private XlsChartTextArea ᜉ;

	// Token: 0x04001CD2 RID: 7378
	private int ᜊ;

	// Token: 0x04001CD3 RID: 7379
	private Format3D ᜋ;
}
