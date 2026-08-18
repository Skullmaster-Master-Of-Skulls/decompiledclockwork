using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003E6 RID: 998
internal class sprᾹ : XlsObject
{
	// Token: 0x06003C1F RID: 15391 RVA: 0x00219FF0 File Offset: 0x00218FF0
	internal sprᾹ(spr\u1DF5 A_0, object A_1) : this(A_0, A_1, true)
	{
	}

	// Token: 0x06003C20 RID: 15392 RVA: 0x0021A008 File Offset: 0x00219008
	internal sprᾹ(spr\u1DF5 A_0, object A_1, bool A_2) : base(A_0, A_1)
	{
		this.ᜀ = (sprᶓ)spr\u175E.ᜀ(TBIFFRecord.ChartAxisParent);
		if (A_2)
		{
			this.ᜂ = new ChartCategoryAxis((spr\u2158)A_0, this, AxisType.Category, this.ᜁ());
			this.ᜃ = new ChartValueAxis((spr\u2158)A_0, this, AxisType.Value, this.ᜁ());
		}
		if (!this.ᜁ())
		{
			this.ᜀ.ᜀ(1);
		}
		this.ᜀ();
	}

	// Token: 0x06003C21 RID: 15393 RVA: 0x0021A08C File Offset: 0x0021908C
	private void ᜀ()
	{
		int a_ = 5;
		this.ᜅ = (XlsChart)base.FindParent(typeof(XlsChart));
		if (this.ᜅ == null)
		{
			for (;;)
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
					goto IL_4C;
				}
			}
			IL_4C:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ杆♈⥊❌⩎㉐❒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
		}
	}

	// Token: 0x06003C22 RID: 15394 RVA: 0x0021A10C File Offset: 0x0021910C
	internal void ᜃ(IList<BiffRecordRaw> A_0, ref int A_1)
	{
		int a_ = 19;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				int num2;
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					TBIFFRecord typeCode;
					if (typeCode != (TBIFFRecord)2131)
					{
						num = 18;
						continue;
					}
					goto IL_194;
				}
				case 2:
					goto IL_194;
				case 4:
					num = 9;
					continue;
				case 5:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartPos)
					{
						num = 25;
						continue;
					}
					this.ᜁ = (spr\u23BE)biffRecordRaw;
					num = 24;
					continue;
				}
				case 6:
					goto IL_194;
				case 7:
					goto IL_194;
				case 8:
					num = 14;
					continue;
				case 9:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						A_1 = BiffRecordRaw.SkipBeginEndBlock(A_0, A_1) - 1;
						num = 21;
						continue;
					case TBIFFRecord.End:
						num2--;
						num = 6;
						continue;
					case TBIFFRecord.ChartPlotArea:
						this.ᜅ.PlotArea = new ChartPlotArea((spr\u2158)base.ReservedHandle, this.ᜅ, A_0, ref A_1);
						num = 11;
						continue;
					default:
						num = 19;
						continue;
					}
					break;
				}
				case 10:
					num = 2;
					continue;
				case 11:
					goto IL_194;
				case 12:
				{
					if (num2 == 0)
					{
						num = 0;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 27;
					continue;
				}
				case 13:
					goto IL_194;
				case 14:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartAxis)
					{
						num = 10;
						continue;
					}
					this.ᜂ(A_0, ref A_1);
					num = 13;
					continue;
				}
				case 15:
					goto IL_20C;
				case 16:
					goto IL_20C;
				case 17:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartText)
					{
						num = 4;
						continue;
					}
					this.ᜁ(A_0, ref A_1);
					num = 7;
					continue;
				}
				case 18:
					num = 23;
					continue;
				case 19:
					num = 5;
					continue;
				case 20:
					goto IL_194;
				case 21:
					goto IL_194;
				case 22:
					num = 1;
					continue;
				case 23:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartChartFormat)
					{
						num = 8;
						continue;
					}
					this.ᜀ(A_0, ref A_1);
					num = 20;
					continue;
				}
				case 24:
					goto IL_194;
				case 25:
					num = 26;
					continue;
				case 26:
					goto IL_194;
				case 27:
				{
					TBIFFRecord typeCode;
					if (typeCode > TBIFFRecord.ChartAxis)
					{
						num = 17;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				}
				case 28:
					goto IL_9E;
				}
				if (A_0 == null)
				{
					num = 28;
					continue;
				}
				this.ᜀ = (sprᶓ)A_0[A_1];
				A_1++;
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
				A_1++;
				num2 = 1;
				num = 15;
				continue;
				IL_194:
				A_1++;
				num = 16;
				continue;
				IL_20C:
				num = 12;
			}
		}
		IL_9E:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ⩊㥌⹎", a_));
	}

	// Token: 0x06003C23 RID: 15395 RVA: 0x0021A474 File Offset: 0x00219474
	private void ᜂ(IList<BiffRecordRaw> A_0, ref int A_1)
	{
		int a_ = 5;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_78;
			case 1:
				goto IL_AF;
			case 3:
			{
				spr\u2426.ChartAxisType chartAxisType;
				switch (chartAxisType)
				{
				case spr\u2426.ChartAxisType.CategoryAxis:
					this.ᜂ = new ChartCategoryAxis((spr\u2158)base.ReservedHandle, this, A_0, ref A_1, this.ᜀ.ᜃ() == 0);
					num = 1;
					continue;
				case spr\u2426.ChartAxisType.ValueAxis:
					this.ᜃ = new ChartValueAxis((spr\u2158)base.ReservedHandle, this, A_0, ref A_1, this.ᜀ.ᜃ() == 0);
					num = 0;
					continue;
				case spr\u2426.ChartAxisType.SeriesAxis:
					if (true)
					{
					}
					this.ᜄ = new ChartSeriesAxis((spr\u2158)base.ReservedHandle, this, A_0, ref A_1, this.ᜀ.ᜃ() == 0);
					num = 4;
					continue;
				}
				goto IL_116;
			}
			case 4:
				goto IL_185;
			case 5:
				num = 6;
				continue;
			case 6:
				goto IL_19C;
			case 7:
				goto IL_44;
			}
			if (A_0 == null)
			{
				num = 7;
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
				BiffRecordRaw biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAxis);
				spr\u2426 spr_u = (spr\u2426)A_0[A_1];
				spr\u2426.ChartAxisType chartAxisType = spr_u.ᜃ();
				num = 3;
				continue;
			}
			}
			IL_116:
			num = 5;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("强尼䬾⁀", a_));
		IL_78:
		IL_AF:
		IL_185:
		goto IL_19E;
		IL_19C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("渺匼吾⽀ⱂ㉄⥆楈⡊╌⹎⍐❒畔㙖⅘㉚⹜罞ᕠᩢᕤɦ", a_));
		IL_19E:
		A_1--;
	}

	// Token: 0x06003C24 RID: 15396 RVA: 0x0021A628 File Offset: 0x00219628
	private void ᜁ(IList<BiffRecordRaw> A_0, ref int A_1)
	{
		int a_ = 10;
		int num = 8;
		XlsChartTextArea xlsChartTextArea;
		for (;;)
		{
			ObjectTextLinkType objectTextLinkType;
			switch (num)
			{
			case 0:
				if (this.ᜄ != null)
				{
					num = 9;
					continue;
				}
				return;
			case 1:
				goto IL_1AF;
			case 2:
				switch (objectTextLinkType)
				{
				case ObjectTextLinkType.YAxis:
					num = 11;
					continue;
				case ObjectTextLinkType.XAxis:
					goto IL_191;
				default:
					num = 5;
					continue;
				}
				break;
			case 3:
				if (objectTextLinkType != ObjectTextLinkType.ZAxis)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				num = 0;
				continue;
			case 4:
				return;
			case 5:
				num = 3;
				continue;
			case 6:
				goto IL_167;
			case 7:
				goto IL_14B;
			case 9:
				this.ᜄ.SetTitle(xlsChartTextArea);
				num = 6;
				continue;
			case 10:
				goto IL_58;
			case 11:
				if (this.ᜃ == null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_191;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 12:
				if (this.ᜂ != null)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			xlsChartTextArea = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
			A_1 = xlsChartTextArea.ᜀ(A_0, A_1) - 1;
			objectTextLinkType = xlsChartTextArea.ObjectLink.ᜃ();
			num = 2;
			continue;
			IL_191:
			num = 12;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅", a_));
		IL_14B:
		this.ᜃ.SetTitle(xlsChartTextArea);
		return;
		IL_167:
		return;
		IL_1AF:
		this.ᜂ.SetTitle(xlsChartTextArea);
	}

	// Token: 0x06003C25 RID: 15397 RVA: 0x0021A7EC File Offset: 0x002197EC
	private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_46;
			case 1:
				num = 0;
				continue;
			case 3:
				if (!this.ᜁ())
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 4:
				goto IL_BA;
			case 5:
				goto IL_3C;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 3;
			}
		}
		IL_3C:
		if (true)
		{
		}
		XlsChartFormatCollection xlsChartFormatCollection;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_BA:
			xlsChartFormatCollection = this.ᜆ.PrimaryFormats;
			goto IL_C7;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
		}
		IL_46:
		xlsChartFormatCollection = this.ᜆ.SecondaryFormats;
		IL_C7:
		XlsChartFormatCollection xlsChartFormatCollection2 = xlsChartFormatCollection;
		XlsChartFormat xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, xlsChartFormatCollection2);
		xlsChartFormat.ᜀ(A_0, ref A_1);
		xlsChartFormatCollection2.Add(xlsChartFormat, false);
		A_1--;
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x0021A8EC File Offset: 0x002198EC
	public void ᜀ(RecordArrayList A_0)
	{
		int a_ = 14;
		int num = 34;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_C9;
			case 1:
				if (this.ᜀ.ᜃ() == 0)
				{
					num = 18;
					continue;
				}
				goto IL_337;
			case 2:
				goto IL_1E6;
			case 3:
				this.ᜄ.SerializeDataToList(A_0);
				num = 5;
				continue;
			case 4:
				goto IL_314;
			case 5:
				goto IL_457;
			case 6:
				if (this.ᜁ != null)
				{
					num = 21;
					continue;
				}
				goto IL_1E6;
			case 7:
				num = 1;
				continue;
			case 8:
				A_0.ᜀ((BiffRecordRaw)this.ᜁ.Clone());
				num = 2;
				continue;
			case 9:
				this.ᜄ.ᜇ(A_0);
				num = 15;
				continue;
			case 10:
				if (this.ᜄ != null)
				{
					num = 28;
					continue;
				}
				goto IL_457;
			case 11:
				if (true)
				{
				}
				num = 36;
				continue;
			case 12:
				this.ᜂ.SerializeDataToList(A_0);
				num = 4;
				continue;
			case 13:
				if (this.ᜁ())
				{
					num = 37;
					continue;
				}
				this.ᜆ.SecondaryFormats.SerializeDataToList(A_0);
				num = 32;
				continue;
			case 14:
				this.ᜃ.ᜇ(A_0);
				num = 33;
				continue;
			case 15:
				goto IL_337;
			case 16:
				if (this.ᜄ != null)
				{
					num = 7;
					continue;
				}
				goto IL_337;
			case 17:
				if (this.ᜂ != null)
				{
					num = 12;
					continue;
				}
				goto IL_314;
			case 18:
				num = 25;
				continue;
			case 19:
				goto IL_1B8;
			case 20:
				if (this.ᜂ != null)
				{
					num = 31;
					continue;
				}
				goto IL_16A;
			case 21:
				num = 27;
				continue;
			case 22:
				if (this.ᜃ != null)
				{
					num = 24;
					continue;
				}
				goto IL_380;
			case 23:
				goto IL_380;
			case 24:
				this.ᜃ.SerializeDataToList(A_0);
				num = 23;
				continue;
			case 25:
				if (flag)
				{
					num = 9;
					continue;
				}
				goto IL_337;
			case 26:
				if (this.ᜀ.ᜃ() == 0)
				{
					num = 11;
					continue;
				}
				goto IL_457;
			case 27:
				if (this.ᜁ())
				{
					num = 8;
					continue;
				}
				goto IL_1E6;
			case 28:
				num = 26;
				continue;
			case 29:
				goto IL_16A;
			case 30:
				goto IL_411;
			case 31:
				this.ᜂ.ᜇ(A_0);
				num = 29;
				continue;
			case 32:
				goto IL_244;
			case 33:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_411;
				default:
					if (false)
					{
					}
					goto IL_35D;
				}
				break;
			case 35:
				return;
			case 36:
				if (flag)
				{
					num = 3;
					continue;
				}
				goto IL_457;
			case 37:
				this.ᜅ.ᜎ(A_0);
				this.ᜆ.PrimaryFormats.SerializeDataToList(A_0);
				num = 19;
				continue;
			case 38:
				if (this.ᜃ != null)
				{
					num = 14;
					continue;
				}
				goto IL_35D;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 30;
			continue;
			IL_16A:
			num = 38;
			continue;
			IL_1E6:
			num = 17;
			continue;
			IL_411:
			if (this.ᜀ == null)
			{
				num = 35;
				continue;
			}
			flag = (Array.IndexOf<ExcelChartType>(XlsChart.DEF_SUPPORT_SERIES_AXIS, this.ᜅ.ChartType) != -1);
			A_0.ᜀ((BiffRecordRaw)this.ᜀ.Clone());
			A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
			num = 6;
			continue;
			IL_314:
			num = 22;
			continue;
			IL_337:
			num = 13;
			continue;
			IL_35D:
			num = 16;
			continue;
			IL_380:
			num = 10;
			continue;
			IL_457:
			num = 20;
		}
		IL_C9:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍⍏", a_));
		IL_1B8:
		IL_244:
		A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x0021AD88 File Offset: 0x00219D88
	internal sprᶓ ᜈ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ = (sprᶓ)spr\u175E.ᜀ(TBIFFRecord.ChartAxisParent);
				num = 2;
				continue;
			case 2:
				goto IL_79;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (this.ᜀ != null)
				{
					goto IL_7B;
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
			num = 0;
		}
		IL_79:
		IL_7B:
		return this.ᜀ;
	}

	// Token: 0x06003C28 RID: 15400 RVA: 0x0021AE18 File Offset: 0x00219E18
	public XlsChartFormatCollection ᜊ()
	{
		if (!this.ᜁ())
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
				return this.ᜆ.SecondaryFormats;
			}
		}
		return this.ᜆ.PrimaryFormats;
	}

	// Token: 0x06003C29 RID: 15401 RVA: 0x0021AE74 File Offset: 0x00219E74
	public bool ᜁ()
	{
		if (this.ᜀ.ᜃ() == 0)
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
				return true;
			}
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06003C2A RID: 15402 RVA: 0x0021AEC4 File Offset: 0x00219EC4
	public XlsChartCategoryAxis ᜂ()
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

	// Token: 0x06003C2B RID: 15403 RVA: 0x0021AF08 File Offset: 0x00219F08
	public void ᜀ(XlsChartCategoryAxis A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003C2C RID: 15404 RVA: 0x0021AF4C File Offset: 0x00219F4C
	public XlsChartValueAxis ᜃ()
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

	// Token: 0x06003C2D RID: 15405 RVA: 0x0021AF90 File Offset: 0x00219F90
	public void ᜀ(XlsChartValueAxis A_0)
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

	// Token: 0x06003C2E RID: 15406 RVA: 0x0021AFD4 File Offset: 0x00219FD4
	public XlsChartSeriesAxis ᜅ()
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

	// Token: 0x06003C2F RID: 15407 RVA: 0x0021B018 File Offset: 0x0021A018
	public void ᜀ(XlsChartSeriesAxis A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003C30 RID: 15408 RVA: 0x0021B05C File Offset: 0x0021A05C
	internal XlsChart ᜇ()
	{
		int a_ = 14;
		if (true)
		{
		}
		object obj = base.FindParent(typeof(XlsChart));
		if (obj != null)
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
				return obj as XlsChart;
			}
		}
		throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㩇⽉≋㩍灏㵑㙓㱕㵗㥙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽", a_));
	}

	// Token: 0x06003C31 RID: 15409 RVA: 0x0021B0D0 File Offset: 0x0021A0D0
	public ChartDefaultFormatsCollection ᜆ()
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

	// Token: 0x06003C32 RID: 15410 RVA: 0x0021B114 File Offset: 0x0021A114
	public void ᜉ()
	{
		for (;;)
		{
			ChartDefaultFormatsCollection chartDefaultFormatsCollection = new ChartDefaultFormatsCollection(base.ReservedHandle, this.ᜇ().PrimaryParentAxis, this.ᜇ().SecondaryParentAxis);
			this.ᜇ().PrimaryParentAxis.ᜆ = chartDefaultFormatsCollection;
			this.ᜇ().SecondaryParentAxis.ᜆ = chartDefaultFormatsCollection;
			int num = 0;
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
						break;
					default:
						if (false)
						{
						}
						if (this.ᜅ.ParentWorkbook.Loading)
						{
							return;
						}
						break;
					}
					num = 2;
					continue;
				case 1:
					return;
				case 2:
				{
					XlsChartFormat format = new ChartFormat((spr\u2158)base.ReservedHandle, chartDefaultFormatsCollection.PrimaryFormats);
					chartDefaultFormatsCollection.PrimaryFormats.Add(format, false);
					num = 1;
					continue;
				}
				}
				break;
			}
		}
	}

	// Token: 0x06003C33 RID: 15411 RVA: 0x0021B200 File Offset: 0x0021A200
	public void ᜁ(bool A_0)
	{
		for (;;)
		{
			this.ᜀ = (sprᶓ)spr\u175E.ᜀ(TBIFFRecord.ChartAxisParent);
			this.ᜀ.ᜀ(1);
			this.ᜁ = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
			int num = 0;
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
					goto IL_11E;
				case 1:
					goto IL_11C;
				case 2:
					num = 6;
					continue;
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
						this.ᜂ = new ChartCategoryAxis((spr\u2158)base.ReservedHandle, this, AxisType.Category, false);
						break;
					}
					num = 7;
					continue;
				case 4:
					this.ᜃ = new ChartValueAxis((spr\u2158)base.ReservedHandle, this, AxisType.Value, false);
					num = 1;
					continue;
				case 5:
					if (this.ᜃ == null)
					{
						num = 4;
						continue;
					}
					goto IL_11E;
				case 6:
					if (this.ᜂ == null)
					{
						num = 3;
						continue;
					}
					goto IL_76;
				case 7:
					goto IL_76;
				}
				break;
				IL_76:
				num = 5;
			}
		}
		IL_11C:
		IL_11E:
		if (true)
		{
		}
	}

	// Token: 0x06003C34 RID: 15412 RVA: 0x0021B340 File Offset: 0x0021A340
	public sprᾹ ᜀ(object A_0, Dictionary<int, int> A_1, Dictionary<string, string> A_2)
	{
		sprᾹ sprᾹ;
		for (;;)
		{
			sprᾹ = (sprᾹ)base.MemberwiseClone();
			sprᾹ.SetParent(A_0);
			sprᾹ.ᜀ();
			sprᾹ.ᜀ = (sprᶓ)spr\u1CD3.ᜀ(this.ᜀ);
			sprᾹ.ᜁ = (spr\u23BE)spr\u1CD3.ᜀ(this.ᜁ);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return sprᾹ;
				case 1:
					if (this.ᜆ != null)
					{
						num = 7;
						continue;
					}
					goto IL_240;
				case 2:
					goto IL_240;
				case 3:
					if (this.ᜁ())
					{
						if (true)
						{
						}
						num = 14;
						continue;
					}
					num = 1;
					continue;
				case 4:
					if (this.ᜄ != null)
					{
						num = 9;
						continue;
					}
					goto IL_19F;
				case 5:
					goto IL_1C5;
				case 6:
					goto IL_130;
				case 7:
					sprᾹ.ᜆ = sprᾹ.ᜇ().PrimaryParentAxis.ᜆ();
					this.ᜆ.CloneForSecondary(sprᾹ.ᜆ, sprᾹ);
					num = 15;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C5;
					default:
						if (false)
						{
						}
						if (this.ᜂ != null)
						{
							num = 5;
							continue;
						}
						return sprᾹ;
					}
					break;
				case 9:
					sprᾹ.ᜄ = (XlsChartSeriesAxis)this.ᜄ.Clone(sprᾹ, A_1, A_2);
					num = 16;
					continue;
				case 10:
					if (this.ᜃ != null)
					{
						num = 13;
						continue;
					}
					goto IL_130;
				case 11:
					if (this.ᜆ != null)
					{
						num = 12;
						continue;
					}
					goto IL_240;
				case 12:
					sprᾹ.ᜆ = this.ᜆ.CloneForPrimary(sprᾹ);
					num = 2;
					continue;
				case 13:
					sprᾹ.ᜃ = (XlsChartValueAxis)this.ᜃ.Clone(sprᾹ, A_1, A_2);
					num = 6;
					continue;
				case 14:
					num = 11;
					continue;
				case 15:
					goto IL_240;
				case 16:
					goto IL_19F;
				}
				break;
				IL_130:
				num = 8;
				continue;
				IL_19F:
				num = 10;
				continue;
				IL_1C5:
				sprᾹ.ᜂ = (XlsChartCategoryAxis)this.ᜂ.Clone(sprᾹ, A_1, A_2);
				num = 0;
				continue;
				IL_240:
				num = 4;
			}
		}
		return sprᾹ;
	}

	// Token: 0x06003C35 RID: 15413 RVA: 0x0021B5B4 File Offset: 0x0021A5B4
	public void ᜄ()
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_103;
			case 1:
				return;
			case 2:
				if (true)
				{
				}
				this.ᜂ.HasMajorGridLines = false;
				this.ᜂ.HasMinorGridLines = false;
				num = 0;
				continue;
			case 3:
				if (this.ᜃ != null)
				{
					num = 8;
					continue;
				}
				goto IL_49;
			case 4:
				goto IL_49;
			case 5:
				this.ᜂ.HasMajorGridLines = false;
				this.ᜂ.HasMinorGridLines = false;
				num = 1;
				continue;
			case 7:
				if (this.ᜄ != null)
				{
					num = 5;
					continue;
				}
				return;
			case 8:
				this.ᜂ.HasMajorGridLines = false;
				this.ᜂ.HasMinorGridLines = false;
				num = 4;
				continue;
			}
			if (this.ᜂ != null)
			{
				num = 2;
				continue;
			}
			goto IL_103;
			IL_49:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_103:
			num = 3;
		}
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x0021B6EC File Offset: 0x0021A6EC
	internal void ᜀ(bool[] A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ != null)
				{
					num = 3;
					continue;
				}
				goto IL_49;
			case 2:
				if (this.ᜄ != null)
				{
					num = 5;
					continue;
				}
				return;
			case 3:
				this.ᜂ.MarkUsedReferences(A_0);
				num = 8;
				continue;
			case 4:
				this.ᜃ.MarkUsedReferences(A_0);
				num = 6;
				continue;
			case 5:
				this.ᜄ.MarkUsedReferences(A_0);
				num = 7;
				continue;
			case 6:
				goto IL_D7;
			case 7:
				return;
			case 8:
				goto IL_49;
			}
			if (this.ᜃ != null)
			{
				num = 4;
				continue;
			}
			goto IL_D7;
			IL_49:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_D7:
			if (true)
			{
			}
			num = 0;
		}
	}

	// Token: 0x06003C37 RID: 15415 RVA: 0x0021B7FC File Offset: 0x0021A7FC
	internal void ᜀ(int[] A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				if (this.ᜂ != null)
				{
					num = 4;
					continue;
				}
				goto IL_51;
			case 2:
				this.ᜃ.UpdateReferenceIndexes(A_0);
				num = 7;
				continue;
			case 3:
				this.ᜄ.UpdateReferenceIndexes(A_0);
				num = 8;
				continue;
			case 4:
				this.ᜂ.UpdateReferenceIndexes(A_0);
				num = 5;
				continue;
			case 5:
				goto IL_51;
			case 6:
				if (this.ᜄ != null)
				{
					num = 3;
					continue;
				}
				return;
			case 7:
				goto IL_DC;
			case 8:
				return;
			}
			if (this.ᜃ != null)
			{
				num = 2;
				continue;
			}
			goto IL_DC;
			IL_51:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 6;
				continue;
			}
			IL_DC:
			num = 1;
		}
	}

	// Token: 0x06003C38 RID: 15416 RVA: 0x0021B908 File Offset: 0x0021A908
	internal void ᜀ(bool A_0)
	{
		if (!A_0)
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
				this.ᜃ = null;
				return;
			}
		}
		this.ᜂ = null;
	}

	// Token: 0x04001A0E RID: 6670
	private sprᶓ ᜀ;

	// Token: 0x04001A0F RID: 6671
	private spr\u23BE ᜁ;

	// Token: 0x04001A10 RID: 6672
	private XlsChartCategoryAxis ᜂ;

	// Token: 0x04001A11 RID: 6673
	private XlsChartValueAxis ᜃ;

	// Token: 0x04001A12 RID: 6674
	private XlsChartSeriesAxis ᜄ;

	// Token: 0x04001A13 RID: 6675
	internal XlsChart ᜅ;

	// Token: 0x04001A14 RID: 6676
	private ChartDefaultFormatsCollection ᜆ;
}
