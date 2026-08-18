using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000358 RID: 856
[DefaultMember("Item")]
internal class spr\u2457 : CollectionExtended<IChartTrendLine>, IChartTrendLines
{
	// Token: 0x06003445 RID: 13381 RVA: 0x001E1F04 File Offset: 0x001E0F04
	internal spr\u2457(spr\u1DF5 A_0, object A_1)
	{
		int a_ = 17;
		base..ctor(A_0, A_1);
		this.ᜀ = (XlsChartSerie)base.FindParent(typeof(XlsChartSerie));
		if (this.ᜀ == null)
		{
			throw new ApplicationException(RecordTableEnumerator.b("ц⡈╊⍌⁎═獒㍔㹖㝘㽚絜⽞`ᅢd०ᵨ䭪ɬ൮᭰ᙲᙴͶ੸啺", a_));
		}
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x001E1F5C File Offset: 0x001E0F5C
	public new IChartTrendLine ᜀ(int A_0)
	{
		int a_ = 11;
		int num = 2;
		IChartTrendLine chartTrendLine;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D3;
			case 1:
				num = 3;
				continue;
			case 3:
				if (A_0 < 0)
				{
					num = 6;
					continue;
				}
				if (true)
				{
				}
				this.ᜁ();
				chartTrendLine = base.List[A_0];
				num = 4;
				continue;
			case 4:
				if (!this.ᜀ())
				{
					num = 5;
					continue;
				}
				return chartTrendLine;
			case 5:
				this.ᜀ(chartTrendLine.Type);
				num = 0;
				continue;
			case 6:
				goto IL_B0;
			}
			if (A_0 >= base.List.Count)
			{
				break;
			}
			num = 1;
		}
		IL_82:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ࡀⵂ⅄≆ㅈ歊⑌㱎煐㱒⁔⍖祘㑚㭜罞͠ౢၤ०൨ᡪ䵬nᝰ卲ᙴᡶᕸ᝺᡼᱾ꞈ", a_));
		IL_B0:
		goto IL_82;
		IL_D3:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_B0;
		default:
			if (false)
			{
			}
			break;
		}
		return chartTrendLine;
	}

	// Token: 0x06003447 RID: 13383 RVA: 0x001E205C File Offset: 0x001E105C
	public new IChartTrendLine ᜂ()
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
		return this.ᜁ(TrendLineType.Linear);
	}

	// Token: 0x06003448 RID: 13384 RVA: 0x001E20A0 File Offset: 0x001E10A0
	public new IChartTrendLine ᜁ(TrendLineType A_0)
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
		this.ᜁ();
		this.ᜀ(A_0);
		sprᴌ sprᴌ = new sprᴌ((spr\u2158)base.ReservedHandle, this);
		sprᴌ.ᜁ(A_0);
		base.Add(sprᴌ);
		return sprᴌ;
	}

	// Token: 0x06003449 RID: 13385 RVA: 0x001E210C File Offset: 0x001E110C
	public new void ᜁ(int A_0)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 >= base.Count)
				{
					goto IL_82;
				}
				goto IL_94;
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_92;
			}
			if (A_0 < 0)
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
			IL_82:
			if (true)
			{
			}
			num = 2;
		}
		IL_5D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶圸强堼䜾", a_));
		IL_92:
		goto IL_5D;
		IL_94:
		this.ᜁ();
		base.RemoveAt(A_0);
	}

	// Token: 0x0600344A RID: 13386 RVA: 0x001E21BC File Offset: 0x001E11BC
	[CLSCompliant(false)]
	public new void ᜀ(IList<IRecordStorage> A_0)
	{
		int a_ = 15;
		int num = 3;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 0:
			{
				if (true)
				{
				}
				if (num2 >= count)
				{
					num = 5;
					continue;
				}
				sprᴌ sprᴌ = (sprᴌ)base.List[num2];
				sprᴌ.ᜃ(A_0);
				num2++;
				num = 2;
				continue;
			}
			case 1:
				goto IL_3C;
			case 2:
				goto IL_96;
			case 4:
				goto IL_96;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				default:
					goto IL_D0;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_65:
			num2 = 0;
			count = base.Count;
			num = 4;
			continue;
			IL_96:
			num = 0;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⩈⑊㽌⭎≐", a_));
		IL_D0:
		if (false)
		{
		}
	}

	// Token: 0x0600344B RID: 13387 RVA: 0x001E22A0 File Offset: 0x001E12A0
	private new void ᜀ(TrendLineType A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num2;
				int num3;
				IXLSRange[] cells;
				switch (num)
				{
				case 0:
					goto IL_B1;
				case 1:
					num = 4;
					continue;
				case 2:
					num = 7;
					continue;
				case 3:
				{
					IXLSRange ixlsrange;
					if (ixlsrange.HasNumber)
					{
						num = 2;
						continue;
					}
					goto IL_F0;
				}
				case 4:
					if (A_0 != TrendLineType.Exponential)
					{
						num = 5;
						continue;
					}
					goto IL_153;
				case 5:
					return;
				case 7:
				{
					IXLSRange ixlsrange;
					if (ixlsrange.NumberValue <= 0.0)
					{
						num = 9;
						continue;
					}
					goto IL_F0;
				}
				case 8:
					goto IL_B1;
				case 9:
					goto IL_A6;
				case 10:
					if (num2 >= num3)
					{
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						IXLSRange ixlsrange = cells[num2];
						num = 3;
						continue;
					}
					}
					break;
				case 11:
					goto IL_CD;
				}
				if (A_0 != TrendLineType.Power)
				{
					num = 1;
					continue;
				}
				goto IL_153;
				IL_B1:
				num = 10;
				continue;
				IL_F0:
				num2++;
				num = 8;
				continue;
				IL_153:
				IXLSRange values = this.ᜀ.Values;
				cells = values.Cells;
				num2 = 0;
				num3 = cells.Length;
				num = 0;
			}
			IL_A6:
			if (true)
			{
			}
			throw new NotSupportedException(RecordTableEnumerator.b("݃❅♇⑉⍋㩍灏≑ㅓ⑕㹗㕙⹛㍝䁟šᅣᑥᩧཀྵɫᩭ偯ᵱѳ፵੷᭹ࡻ᝽ꒃﮍ뒓秊ﾙ벛욟톡솣풥솧쾩\udfab躭욯펱\ud8b3쎵\uddb7즹鲻ힽ뎿ꣃꏅ믇막ꇍꋏ뇓ꟕ귗믙냛ﻝ髟蟡難觥웧", a_));
			IL_CD:
			return;
		}
		}
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x001E242C File Offset: 0x001E142C
	public new void ᜀ(sprᴌ A_0)
	{
		int a_ = 5;
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
			if (A_0 != null)
			{
				base.Add(A_0);
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("伺似娾⽀❂", a_));
	}

	// Token: 0x0600344D RID: 13389 RVA: 0x001E2490 File Offset: 0x001E1490
	public new void ᜁ()
	{
		int a_ = 10;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 3;
				continue;
			case 2:
				goto IL_A8;
			case 3:
				if (Array.IndexOf<ExcelChartType>(XlsChart.DEF_SUPPORT_TREND_LINES, this.ᜀ.SerieType) == -1)
				{
					goto IL_9D;
				}
				return;
			}
			if (this.ᜀ())
			{
				return;
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
				num = 0;
				continue;
			}
			IL_9D:
			num = 2;
		}
		IL_A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("̿㝁㙃㑅ⵇ⑉㡋湍⍏㝑♓㽕㵗穙⡛❝ၟݡ䑣ɥݧཀྵὫmὯٱ味յ൷੹౻ᅽꒃ慎낏ﺑﶓﶗ늛", a_));
	}

	// Token: 0x0600344E RID: 13390 RVA: 0x001E2548 File Offset: 0x001E1548
	internal new void ᜀ(bool[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				List<IChartTrendLine> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4A;
					case 1:
						goto IL_4A;
					case 2:
						if (num >= count)
						{
							num2 = 3;
							continue;
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
							sprᴌ sprᴌ = (sprᴌ)innerList[num];
							sprᴌ.ᜀ(A_0);
							num++;
							break;
						}
						}
						num2 = 1;
						continue;
					case 3:
						return;
					}
					break;
					IL_4A:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x0600344F RID: 13391 RVA: 0x001E2604 File Offset: 0x001E1604
	internal new void ᜀ(int[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				List<IChartTrendLine> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_42;
					case 2:
						if (num >= count)
						{
							num2 = 0;
							continue;
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
							if (true)
							{
							}
							sprᴌ sprᴌ = (sprᴌ)innerList[num];
							sprᴌ.ᜀ(A_0);
							num++;
							break;
						}
						}
						num2 = 3;
						continue;
					case 3:
						goto IL_42;
					}
					break;
					IL_42:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06003450 RID: 13392 RVA: 0x001E26C0 File Offset: 0x001E16C0
	private new bool ᜀ()
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
		return this.ᜀ.ParentChart.IsParsed;
	}

	// Token: 0x06003451 RID: 13393 RVA: 0x001E270C File Offset: 0x001E170C
	public new spr\u2457 ᜀ(object A_0, Dictionary<int, int> A_1, Dictionary<string, string> A_2)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 0;
			spr\u2457 spr_u;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 1:
					goto IL_C4;
				case 2:
					goto IL_AE;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AE;
					default:
						goto IL_103;
					}
					break;
				case 4:
					goto IL_4D;
				case 5:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					sprᴌ sprᴌ = (sprᴌ)base.List[num2];
					spr_u.ᜀ(sprᴌ.ᜀ(spr_u, A_1, A_2));
					num2++;
					num = 1;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				spr_u = new spr\u2457((spr\u2158)base.ReservedHandle, A_0);
				num2 = 0;
				count = base.Count;
				num = 2;
				continue;
				IL_C4:
				num = 5;
				continue;
				IL_AE:
				goto IL_C4;
			}
			IL_4D:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
			IL_103:
			if (false)
			{
			}
			return spr_u;
		}
		}
	}

	// Token: 0x040016E4 RID: 5860
	private new XlsChartSerie ᜀ;
}
