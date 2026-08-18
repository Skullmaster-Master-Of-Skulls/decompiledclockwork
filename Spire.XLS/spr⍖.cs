using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200034A RID: 842
internal class spr\u2356 : spr\u2127
{
	// Token: 0x06003331 RID: 13105 RVA: 0x001D52F0 File Offset: 0x001D42F0
	public void ᜃ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_81;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_58;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			goto IL_4D;
			IL_50:
			num = 2;
			continue;
			IL_4D:
			if (A_0 == null)
			{
				goto IL_50;
			}
			num = 3;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("弼倾⹀⡂", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("礼猾ቀ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("洼䴾⹀㝂⁄⑆㵈≊≌ⅎՐ⩒╔㉖", a_), RecordTableEnumerator.b("猼倾ᅀㅂ⩄㍆ⱈ⡊㥌♎㹐㵒", a_));
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		this.ᜀ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06003332 RID: 13106 RVA: 0x001D53F4 File Offset: 0x001D43F4
	private void ᜂ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_81;
			case 3:
				goto IL_58;
			}
			goto IL_4D;
			IL_50:
			num = 3;
			continue;
			IL_4D:
			if (A_0 == null)
			{
				goto IL_50;
			}
			num = 1;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("⍀ⱂ⩄ⱆ", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("㉀㝂㱄⭆ⱈ㡊", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("㉀㝂㱄⭆ⱈ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⡀❂", a_), RecordTableEnumerator.b("煀", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ཀ≂⡄≆", a_), RecordTableEnumerator.b("ཀⱂ㝄⩆⡈❊", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆", a_), RecordTableEnumerator.b("ᅀ≂㝄♆⹈㥊ⱌ㽎㥐R⅔⹖㕘㹚", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06003333 RID: 13107 RVA: 0x001D553C File Offset: 0x001D453C
	private void ᜁ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				return;
			case 3:
				goto IL_81;
			}
			goto IL_45;
			IL_48:
			num = 0;
			continue;
			IL_45:
			if (A_0 == null)
			{
				goto IL_48;
			}
			num = 2;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("崾⹀ⱂ⹄", a_));
	}

	// Token: 0x06003334 RID: 13108 RVA: 0x001D55EC File Offset: 0x001D45EC
	private void ᜀ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				this.ᜁ(A_0, A_1.Worksheets[num2]);
				num2++;
				goto IL_6A;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					goto IL_CC;
				}
				break;
			case 3:
				goto IL_51;
			case 4:
			{
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("ㅁ⅃╅㱇⍉⍋⁍⍏", a_));
				IWorksheets worksheets = A_1.Worksheets;
				int num2 = 0;
				int count = worksheets.Count;
				num = 6;
				continue;
			}
			case 5:
				goto IL_9C;
			case 6:
				goto IL_9C;
			case 7:
				goto IL_ED;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_6A:
			num = 5;
			continue;
			IL_9C:
			num = 0;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_CC:
		if (false)
		{
		}
		A_0.WriteEndElement();
		return;
		IL_ED:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⁁⭃⥅⍇", a_));
	}

	// Token: 0x06003335 RID: 13109 RVA: 0x001D5730 File Offset: 0x001D4730
	private void ᜁ(XmlWriter A_0, IWorksheet A_1)
	{
		int a_ = 16;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_3C;
			case 3:
				goto IL_CD;
			case 4:
				if (true)
				{
				}
				if (A_1 != null)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CD;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 5:
				if (((XlsWorksheet)A_1).IsEmpty)
				{
					num = 0;
					continue;
				}
				goto IL_CF;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_CD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_CF:
		A_0.WriteStartElement(RecordTableEnumerator.b("㕅ⵇ⥉㡋❍㽏㱑", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("х㩇⽉ⵋ╍ፏ㵑こ㍕", a_), RecordTableEnumerator.b("ࡅⵇ㵉᱋⽍㝏㝑", a_));
		this.ᜀ(A_0, (XlsPageSetup)A_1.PageSetup);
		double a_2 = this.ᜀ(A_0, A_1);
		this.ᜀ(A_0, A_1, a_2);
		A_0.WriteEndElement();
	}

	// Token: 0x06003336 RID: 13110 RVA: 0x001D586C File Offset: 0x001D486C
	private void ᜀ(XmlWriter A_0, XlsPageSetup A_1)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_81;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			goto IL_4D;
			IL_50:
			num = 0;
			continue;
			IL_4D:
			if (A_0 == null)
			{
				goto IL_50;
			}
			num = 1;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("䜶堸尺堼氾⑀㝂い㝆", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䜶堸尺堼ሾ㉀♂ㅄ㉆㥈", a_));
		this.ᜀ(A_0, RecordTableEnumerator.b("朶堸尺堼眾⑀⩂≄⽆㵈", a_), A_1.PageHeight);
		this.ᜀ(A_0, RecordTableEnumerator.b("朶堸尺堼栾⡀❂ㅄ⽆", a_), A_1.PageWidth);
		this.ᜀ(A_0, RecordTableEnumerator.b("然嘸吺䤼娾㍀݂ⱄ㑆㵈⩊⍌ⱎ㑐", a_), A_1.FooterMarginInch, MeasureUnits.Inch);
		this.ᜀ(A_0, RecordTableEnumerator.b("缶尸娺夼娾㍀݂ⱄ㑆㵈⩊⍌ⱎ㑐", a_), A_1.FooterMarginInch, MeasureUnits.Inch);
		this.ᜀ(A_0, RecordTableEnumerator.b("挶嘸䬺瀼帾㍀⑂ⱄ⥆", a_), A_1.FooterMarginInch, MeasureUnits.Inch);
		this.ᜀ(A_0, RecordTableEnumerator.b("甶嘸伺䤼倾ⱀโ⑄㕆⹈≊⍌", a_), A_1.BottomMargin, MeasureUnits.Inch);
		this.ᜀ(A_0, RecordTableEnumerator.b("笶尸崺䤼爾⁀ㅂ≄⹆❈", a_), A_1.LeftMargin, MeasureUnits.Inch);
		this.ᜀ(A_0, RecordTableEnumerator.b("收倸尺唼䬾ీ≂㝄⁆⁈╊", a_), A_1.RightMargin, MeasureUnits.Inch);
		A_0.WriteAttributeString(RecordTableEnumerator.b("砶䬸刺堼儾㕀≂ㅄ⹆♈╊", a_), A_1.Orientation.ToString());
		A_0.WriteEndElement();
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x001D5A38 File Offset: 0x001D4A38
	private double ᜀ(XmlWriter A_0, IWorksheet A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				SizeF a_3;
				SizeF a_4;
				OrderType order;
				IXLSRange ixlsrange;
				IXLSRange ixlsrange2;
				switch (num)
				{
				case 0:
					goto IL_78;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 10;
						continue;
					}
					XlsRangesCollection xlsRangesCollection;
					IXLSRange a_2 = xlsRangesCollection[num2];
					this.ᜀ(A_0, a_2, a_3, a_4, order);
					num2++;
					num = 5;
					continue;
				}
				case 2:
					ixlsrange = A_1.AllocatedRange;
					goto IL_152;
				case 3:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㉁╃㑅⥇ⵉ㹋⽍⁏㩑❓", a_));
					INameRanges names = ((XlsWorksheet)A_1).Names;
					num = 15;
					continue;
				}
				case 4:
					if (true)
					{
					}
					num = 2;
					continue;
				case 5:
					goto IL_125;
				case 6:
					goto IL_120;
				case 7:
					goto IL_125;
				case 8:
					if (ixlsrange2 is XlsRangesCollection)
					{
						num = 11;
						continue;
					}
					this.ᜀ(A_0, ixlsrange2, a_3, a_4, order);
					num = 6;
					continue;
				case 9:
					goto IL_22E;
				case 10:
					num = 9;
					continue;
				case 11:
				{
					XlsRangesCollection xlsRangesCollection = (XlsRangesCollection)ixlsrange2;
					int num2 = 0;
					int count = xlsRangesCollection.Count;
					num = 7;
					continue;
				}
				case 12:
				{
					INameRanges names;
					ixlsrange = names[XlsPageSetup.ᜀ].RefersToRange;
					goto IL_152;
				}
				case 13:
					goto IL_1F8;
				case 15:
				{
					INameRanges names;
					if (!names.Contains(XlsPageSetup.ᜀ))
					{
						num = 4;
						continue;
					}
					num = 12;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_125:
				num = 1;
				continue;
				IL_152:
				ixlsrange2 = ixlsrange;
				this.ᜀ(ixlsrange2, out a_3, out a_4);
				order = A_1.PageSetup.Order;
				num = 8;
			}
			IL_78:
			IL_7A:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_120:
			goto IL_230;
			IL_1F8:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
			IL_22E:
			IL_230:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_7A;
			default:
			{
				if (false)
				{
				}
				A_0.WriteEndElement();
				SizeF a_3;
				return (double)a_3.Width;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x001D5CA0 File Offset: 0x001D4CA0
	private void ᜀ(IXLSRange A_0, out SizeF A_1, out SizeF A_2)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				A_1 = new SizeF(0f, 0f);
				A_2 = new SizeF(0f, 0f);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = A_0.Worksheet;
						XlsPageSetup xlsPageSetup = (XlsPageSetup)worksheet.PageSetup;
						double num2 = xlsPageSetup.LeftMargin + xlsPageSetup.RightMargin;
						num2 = spr\u17FF.ᜀ(num2, MeasureUnits.Inch, MeasureUnits.Point);
						A_1.Width = (float)(xlsPageSetup.PageWidth - num2);
						num2 = xlsPageSetup.TopMargin + xlsPageSetup.BottomMargin;
						num2 = spr\u17FF.ᜀ(num2, MeasureUnits.Inch, MeasureUnits.Point);
						A_1.Height = (float)(xlsPageSetup.PageHeight - num2);
						OrderType order = xlsPageSetup.Order;
						if (true)
						{
						}
						num = 4;
						continue;
					}
					case 1:
						goto IL_131;
					case 2:
						goto IL_76;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F2;
						default:
						{
							if (false)
							{
							}
							IWorksheet worksheet;
							IWorkbook workbook = worksheet.Workbook;
							IStyle style = workbook.Styles[RecordTableEnumerator.b("੃⥅㩇❉ⵋ≍", a_)];
							FontWrapper fontWrapper = (FontWrapper)style.Font;
							XlsFont xlsFont = fontWrapper.Wrapped;
							string strValue = A_0.LastRow.ToString();
							A_2.Width = xlsFont.MeasureString(strValue).Width;
							A_2.Width = (float)spr\u17FF.ᜀ((double)A_2.Width, MeasureUnits.Pixel, MeasureUnits.Point);
							A_2.Height = (float)worksheet.DefaultRowHeight;
							A_1.Width -= A_2.Width;
							A_1.Height -= A_2.Height;
							num = 1;
							continue;
						}
						}
						break;
					case 4:
					{
						XlsPageSetup xlsPageSetup;
						if (xlsPageSetup.IsPrintHeadings)
						{
							num = 3;
							continue;
						}
						return;
					}
					}
					break;
				}
			}
			IL_76:
			goto IL_1F2;
			IL_131:
			return;
			IL_1F2:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃㑅ⅇ⑉㡋ཌྷ≏㝑㕓", a_));
		}
	}

	// Token: 0x06003339 RID: 13113 RVA: 0x001D5EB4 File Offset: 0x001D4EB4
	private void ᜀ(XmlWriter A_0, IXLSRange A_1, SizeF A_2, SizeF A_3, OrderType A_4)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = -1;
				int num2 = -1;
				int num3 = -1;
				int num4 = -1;
				int a_2 = -1;
				IWorksheet worksheet = A_1.Worksheet;
				IPageSetup pageSetup = worksheet.PageSetup;
				int num5 = 0;
				int num6 = 7;
				for (;;)
				{
					int num7;
					int num8;
					switch (num6)
					{
					case 0:
						goto IL_1DA;
					case 1:
						a_2 = this.ᜀ(A_0, worksheet, A_3, num2, num4, a_2);
						num6 = 6;
						continue;
					case 2:
						if (A_3.Width > 0f)
						{
							goto IL_14C;
						}
						goto IL_1DA;
					case 3:
						if (A_3.Height > 0f)
						{
							num6 = 1;
							continue;
						}
						goto IL_D4;
					case 4:
						goto IL_84;
					case 5:
						return;
					case 6:
						goto IL_D4;
					case 7:
						goto IL_184;
					case 8:
						goto IL_184;
					case 9:
						if (!this.ᜀ(A_1, ref num, ref num2, ref num3, ref num4, A_2, A_4))
						{
							num6 = 5;
							continue;
						}
						num7 = num4 - num2 + 1;
						num6 = 2;
						continue;
					case 10:
						goto IL_84;
					case 11:
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						num5++;
						num6 = 8;
						continue;
					case 12:
						if (true)
						{
						}
						if (num8 > num3)
						{
							num6 = 11;
							continue;
						}
						a_2 = this.ᜀ(A_0, worksheet, num8, num2, num4, a_2, A_3);
						num8++;
						num6 = 4;
						continue;
					case 13:
						num7++;
						num6 = 0;
						continue;
					}
					break;
					IL_84:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_14C:
						num6 = 13;
						continue;
					default:
						if (false)
						{
						}
						num6 = 12;
						continue;
					}
					IL_D4:
					num8 = num;
					num6 = 10;
					continue;
					IL_184:
					num6 = 9;
					continue;
					IL_1DA:
					A_0.WriteStartElement(RecordTableEnumerator.b("䜶堸䤺尼堾㍀≂㕄⽆", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("帶崸", a_), num5.ToString());
					A_0.WriteStartElement(RecordTableEnumerator.b("䜶堸䤺尼堾㍀≂㕄⽆摈ⵊ≌㵎㱐㉒⅔", a_));
					this.ᜀ(A_0, RecordTableEnumerator.b("朶堸尺堼紾㍀♂⑄ⱆࡈⵊ㥌⩎⍐", a_), true);
					A_0.WriteEndElement();
					A_0.WriteStartElement(RecordTableEnumerator.b("帶䴸帺值䰾", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("帶䴸帺值", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("帶崸", a_), RecordTableEnumerator.b("ܶ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䌶䀸䬺堼", a_), RecordTableEnumerator.b("挶堸夺儼娾", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("琶嘸场䠼刾⽀あل⡆㱈╊㥌", a_), num7.ToString());
					this.ᜁ(A_0);
					A_0.WriteStartElement(RecordTableEnumerator.b("䔶嘸䰺丼", a_));
					num6 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x0600333A RID: 13114 RVA: 0x001D61CC File Offset: 0x001D51CC
	private void ᜁ(XmlWriter A_0)
	{
		int a_ = 2;
		int num = 3;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_124;
			case 1:
				goto IL_E3;
			case 2:
				goto IL_3C;
			case 4:
				if (num2 < num3)
				{
					string localName = spr\u2356.\u1757[num2];
					A_0.WriteStartElement(localName);
					A_0.WriteAttributeString(RecordTableEnumerator.b("稷唹主娽┿ぁ၃㽅㡇⽉", a_), RecordTableEnumerator.b("瘷唹刻嬽", a_));
					A_0.WriteEndElement();
					num2++;
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 5:
				goto IL_E3;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("嬷弹倻刽洿⑁⭃㑅╇⭉㡋", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("娷唹主娽┿ぁ㝃", a_));
			num2 = 0;
			num3 = spr\u2356.\u1757.Length;
			num = 1;
			continue;
			IL_E3:
			num = 4;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_124:
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x001D630C File Offset: 0x001D530C
	private void ᜀ(XmlWriter A_0)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_110;
			case 1:
				goto IL_44;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				if (num2 < num3)
				{
					string localName = spr\u2356.\u1757[num2];
					A_0.WriteStartElement(localName);
					A_0.WriteAttributeString(RecordTableEnumerator.b("ك⥅㩇⹉⥋㱍я⭑⑓㍕", a_), RecordTableEnumerator.b("ᝃ⽅♇ⵉ⁋⭍", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ፃ⽅ⱇ㹉⑋", a_), RecordTableEnumerator.b("畃", a_));
					A_0.WriteEndElement();
					num2++;
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_149;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 4:
				goto IL_110;
			case 5:
				goto IL_149;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("❃⍅⑇♉態⡍㽏⁑㥓㝕ⱗ", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("♃⥅㩇⹉⥋㱍⍏", a_));
			num2 = 0;
			num3 = spr\u2356.\u1757.Length;
			num = 0;
			continue;
			IL_110:
			num = 3;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_149:
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x001D6470 File Offset: 0x001D5470
	private bool ᜀ(IXLSRange A_0, ref int A_1, ref int A_2, ref int A_3, ref int A_4, SizeF A_5, OrderType A_6)
	{
		int a_ = 17;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_6 == OrderType.DownThenOver)
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
			case 1:
				if (A_4 == A_0.LastColumn)
				{
					num = 15;
					continue;
				}
				goto IL_206;
			case 2:
				A_2 = A_0.Column;
				A_1 = A_3 + 1;
				num = 8;
				continue;
			case 3:
				if (true)
				{
				}
				num = 1;
				continue;
			case 4:
				num = 7;
				continue;
			case 5:
				goto IL_165;
			case 6:
				goto IL_17C;
			case 7:
				if (A_3 == A_0.LastRow)
				{
					num = 18;
					continue;
				}
				A_1 = A_3 + 1;
				num = 17;
				continue;
			case 8:
				goto IL_120;
			case 9:
				if (A_4 == A_0.LastColumn)
				{
					num = 2;
					continue;
				}
				A_2 = A_4 + 1;
				num = 6;
				continue;
			case 10:
				if (A_3 == A_0.LastRow)
				{
					num = 3;
					continue;
				}
				goto IL_206;
			case 11:
				goto IL_19C;
			case 12:
				if (A_1 == -1)
				{
					num = 13;
					continue;
				}
				num = 0;
				continue;
			case 13:
				A_1 = A_0.Row;
				A_2 = A_0.Column;
				goto IL_191;
			case 14:
				goto IL_73;
			case 15:
				return false;
			case 17:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_191;
				default:
					goto IL_9D;
				}
				break;
			case 18:
				A_1 = A_0.Row;
				A_2 = A_4 + 1;
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 10;
			continue;
			IL_191:
			num = 11;
			continue;
			IL_206:
			num = 12;
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆⡈╊⩌⩎", a_));
		IL_9D:
		if (false)
		{
		}
		IL_120:
		IL_165:
		IL_17C:
		IL_19C:
		A_3 = Math.Min(this.ᜀ(A_0.Worksheet, A_1, (double)A_5.Height), A_0.LastRow);
		A_4 = Math.Min(this.ᜁ(A_0.Worksheet, A_2, (double)A_5.Width), A_0.LastColumn);
		return true;
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x001D66F0 File Offset: 0x001D56F0
	private int ᜁ(IWorksheet A_0, int A_1, double A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_128;
				case 1:
				{
					double num2;
					if (num2 > A_2)
					{
						num = 8;
						continue;
					}
					int num3;
					int result = num3;
					num3++;
					num = 7;
					continue;
				}
				case 3:
				{
					if (true)
					{
					}
					int num3;
					if (num3 > A_1)
					{
						num = 5;
						continue;
					}
					goto IL_12D;
				}
				case 4:
					goto IL_12D;
				case 5:
					num = 10;
					continue;
				case 6:
					goto IL_75;
				case 7:
				{
					int num3;
					if (num3 <= A_0.Workbook.MaxColumnCount)
					{
						goto IL_DF;
					}
					int result;
					return result;
				}
				case 8:
				{
					int result;
					return result;
				}
				case 9:
					if (A_1 > 0)
					{
						double num2 = 0.0;
						int num3 = A_1 - 1;
						int result = A_1;
						IVPageBreaks vpageBreaks = A_0.VPageBreaks;
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DF;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 10:
				{
					int num3;
					IVPageBreaks vpageBreaks;
					if (((XlsVPageBreaksCollection)vpageBreaks).GetPageBreak(num3) == null)
					{
						num = 11;
						continue;
					}
					int result;
					return result;
				}
				case 11:
					goto IL_12D;
				case 12:
				{
					int num3;
					double num4 = this.ᜀ(A_0, num3);
					double num2;
					num2 += num4;
					num = 3;
					continue;
				}
				}
				if (A_2 <= 0.0)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
				IL_DF:
				num = 12;
				continue;
				IL_12D:
				num = 1;
			}
			IL_75:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬾ᅀ≂≄≆Ṉ≊⥌㭎㥐", a_));
			IL_128:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾݀⩂㝄㑆㵈ࡊ≌⍎⑐㹒㭔", a_));
		}
		}
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x001D68C4 File Offset: 0x001D58C4
	private int ᜀ(IWorksheet A_0, int A_1, double A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int maxRowCount;
					if (num2 <= maxRowCount)
					{
						goto IL_E6;
					}
					int result;
					return result;
				}
				case 1:
					if (true)
					{
					}
					num = 3;
					continue;
				case 2:
				{
					int num2;
					int rowHeightPixels = ((XlsWorksheet)A_0).GetRowHeightPixels(num2);
					double num3 = spr\u17FF.ᜀ((double)rowHeightPixels, MeasureUnits.Pixel, MeasureUnits.Point);
					double num4;
					num4 += num3;
					num = 9;
					continue;
				}
				case 3:
				{
					int num2;
					IHPageBreaks hpageBreaks;
					if (((XlsHPageBreaksCollection)hpageBreaks).GetPageBreak(num2) == null)
					{
						num = 12;
						continue;
					}
					int result;
					return result;
				}
				case 5:
				{
					int result;
					return result;
				}
				case 6:
					goto IL_12F;
				case 7:
					if (A_1 > 0)
					{
						double num4 = 0.0;
						int num2 = A_1 - 1;
						int result = A_1;
						IHPageBreaks hpageBreaks = A_0.HPageBreaks;
						int maxRowCount = A_0.Workbook.MaxRowCount;
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E6;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 8:
					goto IL_134;
				case 9:
				{
					int num2;
					if (num2 > A_1)
					{
						num = 1;
						continue;
					}
					goto IL_134;
				}
				case 10:
					goto IL_75;
				case 11:
				{
					double num4;
					if (num4 > A_2)
					{
						num = 5;
						continue;
					}
					int num2;
					int result = num2;
					num2++;
					num = 0;
					continue;
				}
				case 12:
					goto IL_134;
				}
				if (A_2 <= 0.0)
				{
					num = 10;
					continue;
				}
				num = 7;
				continue;
				IL_E6:
				num = 2;
				continue;
				IL_134:
				num = 11;
			}
			IL_75:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崸欺尼堾⑀ୂ⁄⹆⹈⍊㥌", a_));
			IL_12F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸紺吼䴾㉀㝂ᝄ⡆㹈", a_));
		}
		}
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x001D6AAC File Offset: 0x001D5AAC
	private int ᜀ(XmlWriter A_0, IWorksheet A_1, int A_2, int A_3, int A_4, int A_5, SizeF A_6)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
					IL_146:
					goto IL_D6;
				case 1:
					goto IL_111;
				case 2:
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					num = 7;
					continue;
				case 3:
				{
					float width;
					int num2;
					this.ᜀ(A_0, (double)width, A_2.ToString(), num2);
					num2++;
					num = 1;
					continue;
				}
				case 4:
					goto IL_D6;
				case 5:
				{
					double num3;
					if (num3 > 0.0)
					{
						num = 6;
						continue;
					}
					return A_5;
				}
				case 6:
				{
					A_5++;
					double num3 = spr\u17FF.ᜀ(num3, MeasureUnits.Pixel, MeasureUnits.Point);
					A_0.WriteStartElement(RecordTableEnumerator.b("似倾㙀", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("吼嬾", a_), A_5.ToString());
					this.ᜀ(A_0, RecordTableEnumerator.b("漼倾㙀ୂ⁄⹆⹈⍊㥌", a_), num3);
					A_0.WriteStartElement(RecordTableEnumerator.b("帼娾ⵀ⽂㙄", a_));
					int num2 = 0;
					float width = A_6.Width;
					num = 12;
					continue;
				}
				case 7:
					return A_5;
				case 8:
					goto IL_6D;
				case 9:
					goto IL_D1;
				case 11:
				{
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					double num3 = (double)((XlsWorksheet)A_1).GetRowHeightPixels(A_2);
					num = 5;
					continue;
				}
				case 12:
				{
					float width;
					if (width > 0f)
					{
						num = 3;
						continue;
					}
					goto IL_111;
				}
				case 13:
				{
					if (num4 > A_4)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					int num2;
					this.ᜀ(A_0, A_1, A_2, num4, num2);
					num4++;
					num2++;
					num = 4;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 11;
				continue;
				IL_D6:
				num = 13;
				continue;
				IL_111:
				num4 = A_3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_146;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_D1:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
		}
		}
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x001D6D08 File Offset: 0x001D5D08
	private int ᜀ(XmlWriter A_0, IWorksheet A_1, SizeF A_2, int A_3, int A_4, int A_5)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					return A_5;
				case 2:
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					num = 1;
					continue;
				case 3:
				{
					if (num2 > A_4)
					{
						num = 2;
						continue;
					}
					string a_2 = sprṔ.ᜀ(num2);
					double a_3 = this.ᜀ(A_1, num2);
					int num3;
					this.ᜀ(A_0, a_3, a_2, num3);
					num2++;
					num3++;
					num = 8;
					continue;
				}
				case 4:
				{
					float width;
					if (width > 0f)
					{
						num = 5;
						continue;
					}
					goto IL_125;
				}
				case 5:
				{
					int num3;
					float width;
					this.ᜀ(A_0, (double)width, string.Empty, num3);
					num3++;
					num = 12;
					continue;
				}
				case 6:
					goto IL_75;
				case 7:
				{
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					float num4 = A_2.Height;
					float width = A_2.Width;
					num = 11;
					continue;
				}
				case 8:
					goto IL_EF;
				case 9:
					goto IL_EA;
				case 10:
				{
					A_5++;
					float num4 = (float)spr\u17FF.ᜀ((double)num4, MeasureUnits.Pixel, MeasureUnits.Point);
					A_0.WriteStartElement(RecordTableEnumerator.b("䐵圷䴹", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("張尷", a_), A_5.ToString());
					this.ᜀ(A_0, RecordTableEnumerator.b("搵圷䴹琻嬽⤿╁ⱃ㉅", a_), (double)num4);
					A_0.WriteStartElement(RecordTableEnumerator.b("唵崷嘹倻䴽", a_));
					int num3 = 0;
					num = 4;
					continue;
				}
				case 11:
				{
					float num4;
					if (num4 > 0f)
					{
						num = 10;
						continue;
					}
					return A_5;
				}
				case 12:
					goto IL_125;
				case 13:
					IL_15A:
					goto IL_EF;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
				IL_EF:
				num = 3;
				continue;
				IL_125:
				num2 = A_3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15A;
				default:
					if (false)
					{
					}
					num = 13;
					break;
				}
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_EA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		}
		}
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x001D6F70 File Offset: 0x001D5F70
	private void ᜀ(XmlWriter A_0, IWorksheet A_1, int A_2, int A_3, int A_4)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 3;
			spr\u192F spr_u192F;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					long iCellIndex = sprṔ.ᜀ(A_3, A_2);
					XlsWorksheet xlsWorksheet;
					RichTextString rtfstring = xlsWorksheet.CellRecords.GetRTFString(iCellIndex, false);
					this.ᜀ(A_0, rtfstring, spr_u192F);
					num = 2;
					continue;
				}
				case 1:
					goto IL_199;
				case 2:
					goto IL_171;
				case 4:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					XlsWorksheet xlsWorksheet = (XlsWorksheet)A_1;
					int a_2 = xlsWorksheet.ᜅ(A_2, A_3);
					spr_u192F = xlsWorksheet.ParentWorkbook.InnerExtFormats.ᜁ(a_2);
					double a_3 = this.ᜀ(A_1, A_3);
					A_0.WriteStartElement(RecordTableEnumerator.b("❃⍅⑇♉", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⵃ≅", a_), A_4.ToString());
					this.ᜀ(A_0, RecordTableEnumerator.b("ፃ⽅ⱇ㹉⑋", a_), a_3);
					num = 5;
					continue;
				}
				case 5:
					if (A_1.CheckExistence(A_2, A_3))
					{
						num = 0;
						continue;
					}
					goto IL_19E;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_127;
					}
					break;
				}
				goto IL_45;
				IL_55:
				num = 6;
				continue;
				IL_45:
				if (A_0 == null)
				{
					goto IL_55;
				}
				if (true)
				{
				}
				num = 4;
			}
			IL_127:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
			IL_171:
			goto IL_19E;
			IL_199:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
			IL_19E:
			this.ᜁ(A_0, spr_u192F);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x001D712C File Offset: 0x001D612C
	private void ᜀ(XmlWriter A_0, double A_1, string A_2, int A_3)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_2 != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					}
					goto Block_2;
				}
				IL_5D:
				num = 3;
				continue;
			case 2:
				goto IL_34;
			case 3:
				goto IL_65;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_34:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋്㕏㹑㡓U㥗㙙⥛㭝", a_));
		Block_2:
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("⭇⽉⁋≍", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), A_3.ToString());
		this.ᜀ(A_0, RecordTableEnumerator.b("὇⍉⡋㩍㡏", a_), A_1);
		A_0.WriteStartElement(RecordTableEnumerator.b("㡇⭉㹋⽍㝏⁑㕓♕し⥙", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("㡇⭉㹋⽍㝏⁑㕓♕し", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("㡇⭉㹋⽍㝏⁑㕓♕し睙㩛ㅝ቟ཡգብ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("G㡉ോ≍㥏㕑㩓㭕㵗㑙⡛", a_), RecordTableEnumerator.b("େ⽉≋㩍㕏⁑", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("ⅇ㹉⥋⍍⍏", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("ⅇ㹉⥋⍍", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), RecordTableEnumerator.b("᱇⽉㑋㩍ɏ㍑㩓ㅕ㵗", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("㱇⽉㑋㩍", a_));
		A_0.WriteString(A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		this.ᜀ(A_0);
		A_0.WriteEndElement();
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x001D7314 File Offset: 0x001D6314
	private void ᜁ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 15;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("♄≆╈❊恌⥎㹐⅒㡔㙖ⵘ", a_));
				string a_2 = this.ᜀ(A_1.\u171D());
				this.ᜀ(A_0, RecordTableEnumerator.b("ፄن╈≊⩌ⅎ㱐㙒㭔⍖", a_), a_2);
				num = 3;
				continue;
			}
			case 1:
				goto IL_10D;
			case 2:
			{
				string value = this.ᜀ(A_1.ᜰ());
				A_0.WriteAttributeString(RecordTableEnumerator.b("ᙄ⽆⡈⽊⑌ⅎ㙐ၒ㩔㭖㙘⥚", a_), value);
				num = 1;
				continue;
			}
			case 3:
				if (!A_1.ᝑ())
				{
					num = 2;
					continue;
				}
				goto IL_13A;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_C6;
				}
				break;
			case 6:
				goto IL_135;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 0;
			}
		}
		IL_C6:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_10D:
		goto IL_13A;
		IL_135:
		throw new ArgumentNullException(RecordTableEnumerator.b("㵄ņ♈㥊⁌⹎═", a_));
		IL_13A:
		if (true)
		{
		}
		this.ᜀ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06003344 RID: 13124 RVA: 0x001D7474 File Offset: 0x001D6474
	private void ᜀ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				XlsBorder xlsBorder;
				int num2;
				switch (num)
				{
				case 1:
					goto IL_F7;
				case 2:
					goto IL_1CA;
				case 3:
				{
					BordersLineType bordersLineType;
					xlsBorder = new spr\u24D1((spr\u2158)A_1.ReservedHandle, A_1, A_1, bordersLineType);
					num = 11;
					continue;
				}
				case 4:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("唶嘸䤺夼娾㍀あ", a_));
					xlsBorder = null;
					num2 = 0;
					int num3 = spr\u2356.\u1756.Length;
					num = 13;
					continue;
				}
				case 5:
					goto IL_18F;
				case 6:
					if (xlsBorder.LineStyle != LineStyleType.None)
					{
						num = 8;
						continue;
					}
					goto IL_FC;
				case 7:
					goto IL_79;
				case 8:
					A_0.WriteAttributeString(RecordTableEnumerator.b("琶嘸场刼䴾", a_), this.ᜀ(xlsBorder.Color));
					A_0.WriteAttributeString(RecordTableEnumerator.b("笶倸唺堼栾⡀❂ㅄ⽆", a_), this.ᜁ(xlsBorder));
					num = 10;
					continue;
				case 9:
					goto IL_7E;
				case 10:
					goto IL_FC;
				case 11:
					goto IL_7E;
				case 12:
				{
					if (xlsBorder == null)
					{
						num = 3;
						continue;
					}
					BordersLineType bordersLineType;
					xlsBorder.BorderIndex = bordersLineType;
					num = 9;
					continue;
				}
				case 13:
					goto IL_18F;
				case 14:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					BordersLineType bordersLineType = spr\u2356.\u1756[num2];
					num = 12;
					continue;
				}
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				num = 4;
				continue;
				IL_7E:
				A_0.WriteStartElement(spr\u2356.\u1757[num2]);
				num = 6;
				continue;
				IL_FC:
				A_0.WriteAttributeString(RecordTableEnumerator.b("甶嘸䤺夼娾㍀ᝂ㱄㝆ⱈ", a_), this.ᜀ(xlsBorder));
				A_0.WriteEndElement();
				num2++;
				num = 5;
				continue;
				IL_18F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12F;
				default:
					if (false)
					{
					}
					num = 14;
					break;
				}
			}
			IL_79:
			goto IL_12F;
			IL_F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("伶缸吺似刾⁀㝂", a_));
			IL_12F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_1CA:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003345 RID: 13125 RVA: 0x001D76DC File Offset: 0x001D66DC
	private string ᜀ(VerticalAlignType A_0)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case VerticalAlignType.Top:
						goto IL_A3;
					case VerticalAlignType.Center:
					case VerticalAlignType.Distributed:
						goto IL_55;
					case VerticalAlignType.Bottom:
						goto IL_64;
					case VerticalAlignType.Justify:
						goto IL_A1;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_7B;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_55:
		return RecordTableEnumerator.b("୅ⅇ⹉⡋≍㕏", a_);
		IL_64:
		return RecordTableEnumerator.b("х❇㹉㡋⅍㵏", a_);
		IL_7B:
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A3:
			return RecordTableEnumerator.b("ቅ❇㩉", a_);
		default:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("❅⑇⍉⭋⁍", a_));
		}
		IL_A1:
		return null;
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x001D77B0 File Offset: 0x001D67B0
	private string ᜀ(HorizontalAlignType A_0)
	{
		int a_ = 13;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 1:
					switch (A_0)
					{
					case HorizontalAlignType.Left:
						goto IL_6A;
					case HorizontalAlignType.Center:
					case HorizontalAlignType.CenterAcrossSelection:
						goto IL_B6;
					case HorizontalAlignType.Right:
						goto IL_A7;
					case HorizontalAlignType.Fill:
						goto IL_C5;
					case HorizontalAlignType.Justify:
						goto IL_5B;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_5B:
		return RecordTableEnumerator.b("ूい㑆㵈≊⭌㙎", a_);
		IL_6A:
		return RecordTableEnumerator.b("ག⁄ⅆ㵈", a_);
		IL_81:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_B6:
			return RecordTableEnumerator.b("B⁄⥆㵈⹊㽌", a_);
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			goto IL_C5;
		}
		IL_A7:
		return RecordTableEnumerator.b("ᅂⱄ⁆ⅈ㽊", a_);
		IL_C5:
		return null;
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x001D7884 File Offset: 0x001D6884
	private string ᜁ(IBorder A_0)
	{
		int a_ = 10;
		for (;;)
		{
			LineStyleType lineStyle = A_0.LineStyle;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_9D;
				case 1:
					switch (lineStyle)
					{
					case LineStyleType.None:
						goto IL_C7;
					case LineStyleType.Thin:
					case LineStyleType.Dashed:
					case LineStyleType.Dotted:
					case LineStyleType.Double:
					case LineStyleType.DashDot:
					case LineStyleType.DashDotDot:
					case LineStyleType.SlantedDashDot:
						goto IL_8C;
					case LineStyleType.Medium:
					case LineStyleType.MediumDashed:
					case LineStyleType.MediumDashDot:
					case LineStyleType.MediumDashDotDot:
						goto IL_86;
					case LineStyleType.Thick:
						goto IL_BB;
					case LineStyleType.Hair:
						goto IL_C1;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_86:
		return spr\u2356.\u175B;
		IL_8C:
		return spr\u2356.\u175A;
		IL_9D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_C1:
			return spr\u2356.\u1759;
		default:
			if (false)
			{
			}
			goto IL_C7;
		}
		IL_BB:
		return spr\u2356.\u175C;
		IL_C7:
		return RecordTableEnumerator.b("瀿", a_);
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x001D7968 File Offset: 0x001D6968
	private string ᜀ(IBorder A_0)
	{
		int a_ = 11;
		for (;;)
		{
			LineStyleType lineStyle = A_0.LineStyle;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BB;
				case 1:
					switch (lineStyle)
					{
					case LineStyleType.None:
						goto IL_118;
					case LineStyleType.Thin:
					case LineStyleType.Medium:
					case LineStyleType.Hair:
						goto IL_92;
					case LineStyleType.Dashed:
					case LineStyleType.MediumDashed:
						goto IL_83;
					case LineStyleType.Dotted:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_EC;
						}
						break;
					case LineStyleType.Thick:
						goto IL_A1;
					case LineStyleType.Double:
						goto IL_109;
					case LineStyleType.DashDot:
					case LineStyleType.MediumDashDot:
					case LineStyleType.SlantedDashDot:
						goto IL_74;
					case LineStyleType.DashDotDot:
					case LineStyleType.MediumDashDotDot:
						goto IL_C7;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_74:
		return RecordTableEnumerator.b("Հⱂㅄ͆⡈㡊╌", a_);
		IL_83:
		return RecordTableEnumerator.b("Հ≂㙄⽆ᩈ♊ⱌ⍎㵐ᑒ㑔❖", a_);
		IL_92:
		return RecordTableEnumerator.b("ቀ⩂⭄⁆╈⹊", a_);
		IL_A1:
		return RecordTableEnumerator.b("ᕀ⭂ⱄ⑆≈", a_);
		IL_BB:
		goto IL_118;
		IL_C7:
		return RecordTableEnumerator.b("Հⱂㅄ͆♈㽊ौ⹎≐㭒", a_);
		IL_EC:
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("Հⱂㅄ", a_);
		IL_109:
		return RecordTableEnumerator.b("Հⱂい╆╈⹊", a_);
		IL_118:
		return RecordTableEnumerator.b("ཀⱂ⭄≆", a_);
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x001D7A9C File Offset: 0x001D6A9C
	private void ᜀ(XmlWriter A_0, RichTextString A_1, spr\u192F A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
				spr\u223A spr_u223A;
				int index;
				string text;
				string a_2;
				int num5;
				int length;
				int num6;
				XlsFontsCollection innerFonts;
				switch (num)
				{
				case 1:
					if (num2 != 0)
					{
						num = 7;
						continue;
					}
					goto IL_10A;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29C;
					default:
						goto IL_98;
					}
					break;
				case 3:
				{
					int num4;
					if (num3 >= num4)
					{
						num = 12;
						continue;
					}
					index = spr_u223A.ᜃ(num3);
					num2 = spr_u223A.ᜄ(num3);
					num = 15;
					continue;
				}
				case 4:
					goto IL_7D;
				case 5:
					goto IL_2D1;
				case 6:
					goto IL_29C;
				case 7:
					a_2 = text.Substring(0, num2);
					this.ᜀ(A_0, a_2, A_1.DefaultFont, num5);
					num5++;
					num = 11;
					continue;
				case 8:
					num6 = length;
					goto IL_2E3;
				case 9:
					if (true)
					{
					}
					goto IL_10A;
				case 10:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㙅⥇㡉ⵋ⥍≏㍑⑓㹕⭗", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("㙅⥇㡉ⵋ⥍≏㍑⑓㹕", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⽅ⱇ", a_), RecordTableEnumerator.b("癅", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("㙅⥇㡉ⵋ⥍≏㍑⑓㹕畗㱙㍛ⱝൟ͡ၣ", a_));
					string a_3 = this.ᜀ(A_2.ᜋ());
					this.ᜀ(A_0, RecordTableEnumerator.b("ๅ㩇୉⁋❍㝏㱑㥓㍕㙗⹙", a_), a_3);
					A_0.WriteEndElement();
					A_0.WriteStartElement(RecordTableEnumerator.b("⽅㱇⽉⅋㵍", a_));
					spr_u223A = A_1.TextObject;
					int num4 = spr_u223A.ᜆ();
					text = spr_u223A.ᜏ();
					XlsWorkbook xlsWorkbook = A_1.Workbook;
					innerFonts = xlsWorkbook.InnerFonts;
					num5 = 0;
					num = 17;
					continue;
				}
				case 11:
					goto IL_10A;
				case 12:
					num = 14;
					continue;
				case 13:
					num6 = spr_u223A.ᜄ(num3 + 1);
					goto IL_2E3;
				case 14:
					goto IL_105;
				case 15:
				{
					int num4;
					if (num3 == num4 - 1)
					{
						num = 16;
						continue;
					}
					num = 13;
					continue;
				}
				case 16:
					num = 8;
					continue;
				case 17:
				{
					int num4;
					if (num4 > 0)
					{
						num = 6;
						continue;
					}
					this.ᜀ(A_0, text, A_1.DefaultFont, num5);
					num = 5;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
				IL_10A:
				num = 3;
				continue;
				IL_29C:
				length = spr_u223A.ᜏ().Length;
				num3 = 0;
				num2 = spr_u223A.ᜄ(0);
				num = 1;
				continue;
				IL_2E3:
				int num7 = num6;
				a_2 = text.Substring(num2, num7 - num2);
				IFont a_4 = innerFonts[index];
				this.ᜀ(A_0, a_2, a_4, num5);
				num3++;
				num5++;
				num = 9;
			}
			IL_7D:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_98:
			if (false)
			{
			}
			return;
			IL_105:
			IL_2D1:
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600334A RID: 13130 RVA: 0x001D7DEC File Offset: 0x001D6DEC
	private void ᜀ(XmlWriter A_0, string A_1, IFont A_2, int A_3)
	{
		int a_ = 12;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, A_2);
				num = 8;
				continue;
			case 1:
				if (A_1.Length > 0)
				{
					num = 0;
					continue;
				}
				goto IL_160;
			case 2:
				goto IL_48;
			case 3:
				goto IL_15B;
			case 4:
				if (true)
				{
				}
				if (A_2 != null)
				{
					num = 3;
					continue;
				}
				goto IL_160;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15B;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("⭁ぃ⍅╇", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⭁⁃", a_), A_3.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("㙁㵃㙅ⵇ", a_), RecordTableEnumerator.b("ᙁ⅃㹅㱇ᡉⵋ⁍㝏㝑", a_));
					num = 4;
					continue;
				}
				break;
			case 7:
				goto IL_E7;
			case 8:
				goto IL_AD;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 5;
			continue;
			IL_15B:
			num = 1;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_AD:
		goto IL_160;
		IL_E7:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅᱇⽉㑋㩍", a_));
		IL_160:
		A_0.WriteStartElement(RecordTableEnumerator.b("㙁⅃㹅㱇", a_));
		A_0.WriteString(A_1);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x001D7F80 File Offset: 0x001D6F80
	private void ᜀ(XmlWriter A_0, IFont A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_62;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_8B;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (A_0 != null)
				{
					goto IL_78;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_54;
				}
			}
			IL_54:
			if (false)
			{
			}
			num = 1;
			continue;
			IL_78:
			num = 2;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑁⭃⡅㱇", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⅁ⱃ❅㩇⭉⽋㩍㕏⁑祓さ㝗⡙ㅛ㽝ᑟ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("с⭃⡅㱇щⵋ⍍㕏", a_), A_1.FontName.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("с⭃⡅㱇᥉╋㑍㕏", a_), A_1.Size.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("ᙁ⅃㹅㱇ॉ⍋≍㽏⁑", a_), this.ᜀ(A_1.Color));
		this.ᜀ(A_0, RecordTableEnumerator.b("A⭃⩅ⱇ", a_), A_1.IsBold);
		this.ᜀ(A_0, RecordTableEnumerator.b("ୁぃ❅⑇⍉⽋", a_), A_1.IsItalic);
		A_0.WriteAttributeString(RecordTableEnumerator.b("ᝁ⩃≅ⵇ㡉⁋❍㹏㝑", a_), this.ᜀ(A_1.Underline));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ᅁㅃ⑅ᭇ㽉㱋⭍≏ő㝓⑕ㅗ⩙⡛", a_), this.ᜀ(A_1));
		this.ᜀ(A_0, RecordTableEnumerator.b("ᅁぃ㑅ⅇⅉ⥋", a_), A_1.IsStrikethrough);
		A_0.WriteEndElement();
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x001D8138 File Offset: 0x001D7138
	private void ᜀ(XmlWriter A_0, string A_1, bool A_2)
	{
		int a_ = 5;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				num = 5;
				continue;
			case 2:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
			case 3:
				goto IL_B3;
			case 4:
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				A_0.WriteAttributeString(A_1, spr\u2356.\u1755);
				num = 3;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 8;
						continue;
					}
					num = 2;
					continue;
				}
				break;
			case 7:
				goto IL_124;
			case 8:
				goto IL_F2;
			}
			if (!A_2)
			{
				return;
			}
			num = 1;
		}
		IL_6F:
		throw new ArgumentException(RecordTableEnumerator.b("稺䤼䬾㍀⩂❄㉆㵈⹊͌⹎㱐㙒畔㑖㡘㕚絜ㅞ๠ᝢ䕤զ౨䭪࡬ɮŰݲ౴奶", a_));
		IL_B3:
		return;
		IL_F2:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_124:
		throw new ArgumentNullException(RecordTableEnumerator.b("稺䤼䬾㍀⩂❄㉆㵈⹊͌⹎㱐㙒", a_));
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x001D8270 File Offset: 0x001D7270
	private string ᜀ(Color A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		int num = A_0.ToArgb();
		return RecordTableEnumerator.b("故", a_) + num.ToString(RecordTableEnumerator.b("ṅ", a_));
	}

	// Token: 0x0600334E RID: 13134 RVA: 0x001D82E4 File Offset: 0x001D72E4
	private string ᜀ(FontUnderlineType A_0)
	{
		int a_ = 9;
		for (;;)
		{
			IL_2F:
			if (true)
			{
			}
			for (;;)
			{
				IL_39:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 1:
						switch (A_0)
						{
						case FontUnderlineType.None:
							goto IL_CA;
						case FontUnderlineType.Single:
							goto IL_BB;
						case FontUnderlineType.Double:
							goto IL_79;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_39;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						break;
					case 2:
						goto IL_90;
					case 3:
						num = 2;
						continue;
					case 4:
						switch (A_0)
						{
						case FontUnderlineType.SingleAccounting:
							goto IL_BB;
						case FontUnderlineType.DoubleAccounting:
							goto IL_79;
						default:
							num = 3;
							continue;
						}
						break;
					}
					goto IL_2F;
				}
			}
		}
		IL_79:
		return RecordTableEnumerator.b("笾⹀㙂❄⭆ⱈ", a_);
		IL_90:
		goto IL_CA;
		IL_BB:
		return RecordTableEnumerator.b("氾⡀ⵂ≄⭆ⱈ", a_);
		IL_CA:
		return RecordTableEnumerator.b("焾⹀ⵂ⁄", a_);
	}

	// Token: 0x0600334F RID: 13135 RVA: 0x001D83CC File Offset: 0x001D73CC
	private string ᜀ(IFont A_0)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.IsSuperscript)
				{
					num = 3;
					continue;
				}
				goto IL_D8;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_D6;
			case 3:
				goto IL_84;
			case 4:
				goto IL_6A;
			case 5:
				if (A_0.IsSubscript)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A9;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
			else
			{
				num = 5;
			}
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹医倽㐿", a_));
		IL_84:
		IL_A9:
		return RecordTableEnumerator.b("椹䤻丽┿ぁᝃ╅㩇⍉㱋㩍", a_);
		IL_D6:
		return RecordTableEnumerator.b("椹䤻尽ጿ⅁㙃⽅㡇㹉", a_);
		IL_D8:
		return RecordTableEnumerator.b("琹医倽┿", a_);
	}

	// Token: 0x06003350 RID: 13136 RVA: 0x001D84C0 File Offset: 0x001D74C0
	private void ᜀ(XmlWriter A_0, string A_1, double A_2, MeasureUnits A_3)
	{
		int a_ = 19;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			case 1:
				goto IL_7F;
			case 2:
				goto IL_65;
			case 3:
				if (A_1.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_E0;
			case 5:
				goto IL_DE;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B1;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			else
			{
				num = 0;
			}
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_7F:
		IL_B1:
		throw new ArgumentException(RecordTableEnumerator.b("ࡈ㽊㥌㵎㡐ㅒ⁔⍖㱘ᕚ㱜㉞Ѡ䍢٤٦ݨժɬ᭮兰ᅲၴ坶ᱸᙺർ୾궂", a_));
		IL_DE:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ࡈ㽊㥌㵎㡐ㅒ⁔⍖㱘ᕚ㱜㉞Ѡ", a_));
		IL_E0:
		A_2 = spr\u17FF.ᜀ(A_2, A_3, MeasureUnits.Point);
		string value = A_2.ToString(spr\u2356.\u1758);
		A_0.WriteAttributeString(A_1, value);
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x001D85D0 File Offset: 0x001D75D0
	private void ᜀ(XmlWriter A_0, string A_1, string A_2)
	{
		int a_ = 14;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_92;
			case 1:
				goto IL_4C;
			case 2:
				num = 8;
				continue;
			case 3:
				if (true)
				{
				}
				if (A_2 != null)
				{
					num = 2;
					continue;
				}
				return;
			case 4:
				if (A_1.Length == 0)
				{
					num = 9;
					continue;
				}
				num = 3;
				continue;
			case 6:
				goto IL_E3;
			case 7:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			case 8:
				if (A_2.Length != 0)
				{
					goto IL_129;
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
					num = 6;
					continue;
				}
				break;
			case 9:
				goto IL_106;
			}
			IL_41:
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 7;
			continue;
			goto IL_41;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_92:
		throw new ArgumentNullException(RecordTableEnumerator.b("Ճ㉅㱇㡉╋ⱍ╏♑ㅓᡕ㥗㝙㥛", a_));
		IL_E3:
		return;
		IL_106:
		throw new ArgumentException(RecordTableEnumerator.b("Ճ㉅㱇㡉╋ⱍ╏♑ㅓᡕ㥗㝙㥛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ᅳ᭵ࡷ๹ջ偽", a_));
		IL_129:
		A_0.WriteAttributeString(A_1, A_2);
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x001D8710 File Offset: 0x001D7710
	private void ᜀ(XmlWriter A_0, string A_1, double A_2)
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_87;
			case 1:
				if (true)
				{
				}
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				goto IL_E0;
			case 2:
				goto IL_65;
			case 3:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
			case 4:
				goto IL_DE;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B1;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			else
			{
				num = 3;
			}
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_87:
		IL_B1:
		throw new ArgumentException(RecordTableEnumerator.b("́ぃ㉅㩇⍉⹋㭍⑏㝑ᩓ㝕㕗㽙籛㵝şౡ੣॥ᱧ䩩๫୭偯᝱ᥳٵ౷͹剻", a_));
		IL_DE:
		throw new ArgumentNullException(RecordTableEnumerator.b("́ぃ㉅㩇⍉⹋㭍⑏㝑ᩓ㝕㕗㽙", a_));
		IL_E0:
		string value = A_2.ToString(spr\u2356.\u1758);
		A_0.WriteAttributeString(A_1, value);
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x001D8814 File Offset: 0x001D7814
	private void ᜀ(XmlWriter A_0, IWorksheet A_1, double A_2)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_5A;
			case 3:
				goto IL_8B;
			}
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
					num = 1;
					goto IL_13;
				}
			}
			num = 0;
		}
		IL_5A:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("嘽┿⍁⁃⍅㩇㥉態⡍㽏㵑⁓㍕⩗⥙", a_));
		IPageSetup pageSetup = A_1.PageSetup;
		string[] array = new string[]
		{
			pageSetup.LeftHeader,
			pageSetup.RightHeader,
			pageSetup.CenterHeader
		};
		A_0.WriteStartElement(RecordTableEnumerator.b("儽␿♁楃⹅ⵇ⭉⡋⭍≏", a_));
		this.ᜀ(A_0, array, A_2);
		A_0.WriteEndElement();
		array[0] = pageSetup.LeftFooter;
		array[1] = pageSetup.RightFooter;
		array[2] = pageSetup.CenterFooter;
		A_0.WriteStartElement(RecordTableEnumerator.b("儽␿♁楃⁅❇╉㡋⭍≏", a_));
		this.ᜀ(A_0, array, A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x001D8968 File Offset: 0x001D7968
	private void ᜀ(XmlWriter A_0, string[] A_1, double A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9C;
				case 1:
					goto IL_3F9;
				case 2:
					goto IL_23F;
				case 3:
				{
					int num2;
					if (num2 == 0)
					{
						num = 9;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("〿⍁㙃❅⽇㡉ⵋ㹍㡏⅑", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("〿⍁㙃❅⽇㡉ⵋ㹍㡏", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⤿㙁⅃⭅㭇", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⤿㙁⅃⭅", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁", a_), RecordTableEnumerator.b("瀿", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㐿㭁㑃⍅", a_), RecordTableEnumerator.b("ᐿ⍁♃⩅ⵇ", a_));
					int num3;
					A_0.WriteAttributeString(RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉㽋്㽏❑㩓≕", a_), num3.ToString());
					this.ᜁ(A_0);
					A_0.WriteStartElement(RecordTableEnumerator.b("㈿ⵁ㍃㕅", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("㈿ⵁ㍃", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁", a_), RecordTableEnumerator.b("瀿", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⌿❁⡃⩅㭇", a_));
					double a_2 = A_2 / (double)num3;
					int num4 = 0;
					num = 7;
					continue;
				}
				case 4:
				{
					int num3;
					int num4;
					if (num4 >= num3)
					{
						num = 11;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("⌿❁⡃⩅", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁", a_), num4.ToString());
					double a_2;
					this.ᜀ(A_0, RecordTableEnumerator.b("᜿⭁⁃㉅⁇", a_), a_2);
					A_0.WriteStartElement(RecordTableEnumerator.b("〿⍁㙃❅⽇㡉ⵋ㹍㡏⅑", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("〿⍁㙃❅⽇㡉ⵋ㹍㡏", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁", a_), RecordTableEnumerator.b("瀿", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⤿㙁⅃⭅㭇", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⤿㙁⅃⭅", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁", a_), RecordTableEnumerator.b("瀿", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㐿㭁㑃⍅", a_), RecordTableEnumerator.b("ᐿ❁㱃㉅ᩇ⭉≋⥍㕏", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("㐿❁㱃㉅", a_));
					A_0.WriteString(A_1[num4]);
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					num4++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_264;
				case 6:
					num = 3;
					continue;
				case 7:
					goto IL_264;
				case 8:
					return;
				case 9:
					goto IL_25F;
				case 10:
				{
					int num3;
					if (num3 == 0)
					{
						num = 8;
						continue;
					}
					int num2 = 0;
					int num5 = 0;
					num = 1;
					continue;
				}
				case 11:
					goto IL_284;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 13:
				{
					int num3;
					int num5;
					if (num5 >= num3)
					{
						num = 6;
						continue;
					}
					int num2;
					num2 += A_1[num5].Length;
					num5++;
					num = 14;
					continue;
				}
				case 14:
					goto IL_3F9;
				case 15:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					int num3 = A_1.Length;
					num = 10;
					continue;
				}
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				num = 15;
				continue;
				IL_264:
				num = 4;
				continue;
				IL_3F9:
				num = 13;
			}
			IL_9C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
			IL_23F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃၅⥇♉㥋⭍⍏", a_));
			IL_25F:
			return;
			IL_284:
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x001D8DD4 File Offset: 0x001D7DD4
	private double ᜀ(IWorksheet A_0, int A_1)
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
		int columnWidthPixels = ((XlsWorksheet)A_0).GetColumnWidthPixels(A_1);
		return spr\u17FF.ᜀ((double)columnWidthPixels, MeasureUnits.Pixel, MeasureUnits.Point) + 1.0;
	}

	// Token: 0x06003356 RID: 13142 RVA: 0x001D8E34 File Offset: 0x001D7E34
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2356()
	{
		int a_ = 2;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u2356.\u1755 = bool.TrueString.ToLower();
		spr\u2356.\u1756 = new BordersLineType[]
		{
			BordersLineType.EdgeBottom,
			BordersLineType.EdgeLeft,
			BordersLineType.EdgeRight,
			BordersLineType.EdgeTop
		};
		spr\u2356.\u1757 = new string[]
		{
			RecordTableEnumerator.b("稷唹䠻䨽⼿⽁", a_),
			RecordTableEnumerator.b("琷弹娻䨽", a_),
			RecordTableEnumerator.b("樷匹嬻嘽㐿", a_),
			RecordTableEnumerator.b("氷唹䰻", a_)
		};
		spr\u2356.\u1758 = CultureInfo.InvariantCulture;
		spr\u2356.\u1759 = 0.25.ToString(spr\u2356.\u1758);
		spr\u2356.\u175A = 0.5.ToString(spr\u2356.\u1758);
		spr\u2356.\u175B = 1.ToString(spr\u2356.\u1758);
		spr\u2356.\u175C = 2.25.ToString(spr\u2356.\u1758);
	}

	// Token: 0x04001651 RID: 5713
	private const string ᜀ = "DLS";

	// Token: 0x04001652 RID: 5714
	private const string ᜁ = "ProtectionType";

	// Token: 0x04001653 RID: 5715
	private const string ᜂ = "NoProtection";

	// Token: 0x04001654 RID: 5716
	private const string ᜃ = "styles";

	// Token: 0x04001655 RID: 5717
	private const string ᜄ = "style";

	// Token: 0x04001656 RID: 5718
	private const string ᜅ = "id";

	// Token: 0x04001657 RID: 5719
	private const string ᜆ = "Name";

	// Token: 0x04001658 RID: 5720
	private const string ᜇ = "type";

	// Token: 0x04001659 RID: 5721
	private const string ᜈ = "sections";

	// Token: 0x0400165A RID: 5722
	private const string ᜉ = "section";

	// Token: 0x0400165B RID: 5723
	private const string ᜊ = "BreakCode";

	// Token: 0x0400165C RID: 5724
	private const string ᜋ = "paragraphs";

	// Token: 0x0400165D RID: 5725
	private const string ᜌ = "paragraph";

	// Token: 0x0400165E RID: 5726
	private const string \u170D = "items";

	// Token: 0x0400165F RID: 5727
	private const string ᜎ = "item";

	// Token: 0x04001660 RID: 5728
	private const string ᜏ = "rows";

	// Token: 0x04001661 RID: 5729
	private const string ᜐ = "row";

	// Token: 0x04001662 RID: 5730
	private const string ᜑ = "cells";

	// Token: 0x04001663 RID: 5731
	private const string \u1712 = "cell";

	// Token: 0x04001664 RID: 5732
	private const string \u1713 = "Width";

	// Token: 0x04001665 RID: 5733
	private const string \u1714 = "TextRange";

	// Token: 0x04001666 RID: 5734
	private const string \u1715 = "text";

	// Token: 0x04001667 RID: 5735
	private const string \u1716 = "ColumnsCount";

	// Token: 0x04001668 RID: 5736
	private const string \u1717 = "format";

	// Token: 0x04001669 RID: 5737
	private const string \u1718 = "FontName";

	// Token: 0x0400166A RID: 5738
	private const string \u1719 = "FontSize";

	// Token: 0x0400166B RID: 5739
	private const string \u171A = "Bold";

	// Token: 0x0400166C RID: 5740
	private const string \u171B = "Italic";

	// Token: 0x0400166D RID: 5741
	private const string \u171C = "Underline";

	// Token: 0x0400166E RID: 5742
	private const string \u171D = "TextColor";

	// Token: 0x0400166F RID: 5743
	private const string \u171E = "#";

	// Token: 0x04001670 RID: 5744
	private const string \u171F = "None";

	// Token: 0x04001671 RID: 5745
	private const string ᜠ = "Single";

	// Token: 0x04001672 RID: 5746
	private const string ᜡ = "Double";

	// Token: 0x04001673 RID: 5747
	private const string ᜢ = "SubScript";

	// Token: 0x04001674 RID: 5748
	private const string ᜣ = "SuperScript";

	// Token: 0x04001675 RID: 5749
	private const string ᜤ = "None";

	// Token: 0x04001676 RID: 5750
	private const string ᜥ = "SubSuperScript";

	// Token: 0x04001677 RID: 5751
	private const string ᜦ = "Strike";

	// Token: 0x04001678 RID: 5752
	private const string ᜧ = "cell-format";

	// Token: 0x04001679 RID: 5753
	private const string ᜨ = "character-format";

	// Token: 0x0400167A RID: 5754
	private const string ᜩ = "borders";

	// Token: 0x0400167B RID: 5755
	private const string ᜪ = "border";

	// Token: 0x0400167C RID: 5756
	private const string ᜫ = "Color";

	// Token: 0x0400167D RID: 5757
	private const string ᜬ = "LineWidth";

	// Token: 0x0400167E RID: 5758
	private const string ᜭ = "BorderType";

	// Token: 0x0400167F RID: 5759
	private const string ᜮ = "0";

	// Token: 0x04001680 RID: 5760
	private const string ᜯ = "Single";

	// Token: 0x04001681 RID: 5761
	private const string ᜰ = "Double";

	// Token: 0x04001682 RID: 5762
	private const string ᜱ = "Dot";

	// Token: 0x04001683 RID: 5763
	private const string \u1732 = "DashSmallGap";

	// Token: 0x04001684 RID: 5764
	private const string \u1733 = "DotDash";

	// Token: 0x04001685 RID: 5765
	private const string \u1734 = "DotDotDash";

	// Token: 0x04001686 RID: 5766
	private const string \u1735 = "Thick";

	// Token: 0x04001687 RID: 5767
	private const string \u1736 = "None";

	// Token: 0x04001688 RID: 5768
	private const string \u1737 = "page-setup";

	// Token: 0x04001689 RID: 5769
	private const string \u1738 = "PageHeight";

	// Token: 0x0400168A RID: 5770
	private const string \u1739 = "PageWidth";

	// Token: 0x0400168B RID: 5771
	private const string \u173A = "FooterDistance";

	// Token: 0x0400168C RID: 5772
	private const string \u173B = "HeaderDistance";

	// Token: 0x0400168D RID: 5773
	private const string \u173C = "TopMargin";

	// Token: 0x0400168E RID: 5774
	private const string \u173D = "BottomMargin";

	// Token: 0x0400168F RID: 5775
	private const string \u173E = "LeftMargin";

	// Token: 0x04001690 RID: 5776
	private const string \u173F = "RightMargin";

	// Token: 0x04001691 RID: 5777
	private const string ᝀ = "PageBreakAfter";

	// Token: 0x04001692 RID: 5778
	private const string ᝁ = "Orientation";

	// Token: 0x04001693 RID: 5779
	private const string ᝂ = "paragraph-format";

	// Token: 0x04001694 RID: 5780
	private const string ᝃ = "headers-footers";

	// Token: 0x04001695 RID: 5781
	private const string ᝄ = "Table";

	// Token: 0x04001696 RID: 5782
	private const string ᝅ = "even-footer";

	// Token: 0x04001697 RID: 5783
	private const string ᝆ = "odd-footer";

	// Token: 0x04001698 RID: 5784
	private const string ᝇ = "even-header";

	// Token: 0x04001699 RID: 5785
	private const string ᝈ = "odd-header";

	// Token: 0x0400169A RID: 5786
	private const string ᝉ = "RowHeight";

	// Token: 0x0400169B RID: 5787
	private const string ᝊ = "ShadingColor";

	// Token: 0x0400169C RID: 5788
	private const int ᝋ = 1;

	// Token: 0x0400169D RID: 5789
	private const string ᝌ = "HrAlignment";

	// Token: 0x0400169E RID: 5790
	private const string ᝍ = "VAlignment";

	// Token: 0x0400169F RID: 5791
	private const string ᝎ = "Center";

	// Token: 0x040016A0 RID: 5792
	private const string ᝏ = "Top";

	// Token: 0x040016A1 RID: 5793
	private const string ᝐ = "Bottom";

	// Token: 0x040016A2 RID: 5794
	private const string ᝑ = "Middle";

	// Token: 0x040016A3 RID: 5795
	private const string \u1752 = "Left";

	// Token: 0x040016A4 RID: 5796
	private const string \u1753 = "Right";

	// Token: 0x040016A5 RID: 5797
	private const string \u1754 = "Justify";

	// Token: 0x040016A6 RID: 5798
	private static readonly string \u1755;

	// Token: 0x040016A7 RID: 5799
	private static readonly BordersLineType[] \u1756;

	// Token: 0x040016A8 RID: 5800
	private static readonly string[] \u1757;

	// Token: 0x040016A9 RID: 5801
	private static readonly CultureInfo \u1758;

	// Token: 0x040016AA RID: 5802
	private static readonly string \u1759;

	// Token: 0x040016AB RID: 5803
	private static readonly string \u175A;

	// Token: 0x040016AC RID: 5804
	private static readonly string \u175B;

	// Token: 0x040016AD RID: 5805
	private static readonly string \u175C;
}
