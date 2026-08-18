using System;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020004B9 RID: 1209
internal class spr\u1715 : spr\u2175
{
	// Token: 0x06004AAF RID: 19119 RVA: 0x002D4300 File Offset: 0x002D3300
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2C:
				if (true)
				{
				}
				XlsComboBoxShape xlsComboBoxShape = A_1 as XlsComboBoxShape;
				for (;;)
				{
					IL_3B:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (xlsComboBoxShape.ComboType != ExcelComboType.AutoFilter)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3B;
							}
							if (false)
							{
							}
							A_0.WriteStartElement(RecordTableEnumerator.b("㙄⽆⡈㭊⡌", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ླྀᙺᅼ", a_));
							string value = '#' + string.Format(RecordTableEnumerator.b("ᩄ㽆祈筊経罎๐❒⹔杖⑘", a_), A_1.InnerSpRecord.\u1714());
							string value2 = string.Format(RecordTableEnumerator.b("ᩄ㽆祈筊経罎๐⁒⹔杖⑘", a_), A_1.ShapeId);
							A_0.WriteAttributeString(RecordTableEnumerator.b("ⱄ⍆", a_), value2);
							A_0.WriteAttributeString(RecordTableEnumerator.b("ㅄ㹆㥈⹊", a_), value);
							string text = RecordTableEnumerator.b("⡄♆㭈ⱊ⑌ⅎ籐㽒ごㅖⵘ慚", a_) + A_1.Left.ToString();
							string text2 = RecordTableEnumerator.b("⡄♆㭈ⱊ⑌ⅎ籐❒㩔❖捘", a_) + A_1.Top.ToString();
							string text3 = RecordTableEnumerator.b("㉄⹆ⵈ㽊╌畎", a_) + A_1.Width.ToString();
							string text4 = RecordTableEnumerator.b("ⵄ≆⁈ⱊ╌㭎歐", a_) + A_1.Height.ToString();
							string value3 = string.Format(RecordTableEnumerator.b("㹄睆㑈灊㙌繎ⱐ桒⹔敖⑘恚♜汞ᱠ", a_), new object[]
							{
								text,
								text2,
								text3,
								text4
							});
							A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㍆え❊⡌", a_), value3);
							this.ᜀ(A_0, A_1 as XlsComboBoxShape);
							A_0.WriteEndElement();
							num = 1;
							continue;
						}
						}
						goto IL_2C;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004AB0 RID: 19120 RVA: 0x002D4528 File Offset: 0x002D3528
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("䔵倷嬹䰻嬽㐿㭁㑃⍅", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧ᱩūɭ", a_));
		string value = string.Format(RecordTableEnumerator.b("椵䀷ਹ఻฽瀿ᵁぃ㵅硇㝉", a_), 201);
		A_0.WriteAttributeString(RecordTableEnumerator.b("張尷", a_), value);
		A_0.WriteAttributeString(RecordTableEnumerator.b("唵圷唹主娽㌿⭁㹃⍅", a_), RecordTableEnumerator.b("еषహ఻฽氿灁畃灅硇穉", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䔵䠷丹", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧թ੫࡭᥯ᅱᅳ䱵᝷ᱹ᩻᝽", a_), 201.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("䘵夷丹吻", a_), RecordTableEnumerator.b("嬵ᐷ嘹ျఽ焿瑁瑃癅㩇硉絋硍恏扑硓㩕橗歙橛湝偟乡ᱣͥ", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x06004AB1 RID: 19121 RVA: 0x002D463C File Offset: 0x002D363C
	private new void ᜀ(XmlWriter A_0, XlsComboBoxShape A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_44:
				A_0.WriteStartElement(RecordTableEnumerator.b("ل⭆⁈⹊⍌㭎ᕐ㉒⅔㙖", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("੄╆⍈⹊⹌㭎Ր⩒╔㉖", a_), RecordTableEnumerator.b("ń㕆♈㭊", a_));
				A_0.WriteElementString(RecordTableEnumerator.b("ᙄ⹆㍈⹊ᩌ♎═㭒ᙔ㉖㕘㝚⹜", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), (!A_1.IsSizeWithCell).ToString());
				string value = spr\u2175.ᜀ(A_1);
				A_0.WriteElementString(RecordTableEnumerator.b("ф⥆⩈⍊≌㵎", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), value);
				A_0.WriteElementString(RecordTableEnumerator.b("ф㉆㵈⑊Ō♎㽐㙒", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("̈́♆╈㡊⡌", a_));
				IXLSRange linkedCell = A_1.LinkedCell;
				int num = 2;
				for (;;)
				{
					IXLSRange listFillRange;
					switch (num)
					{
					case 0:
						A_0.WriteElementString(RecordTableEnumerator.b("̈́⩆╈⩊Ō♎㽐㡒", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), linkedCell.RangeAddress);
						num = 1;
						continue;
					case 1:
						goto IL_23C;
					case 2:
						if (linkedCell != null)
						{
							num = 0;
							continue;
						}
						goto IL_23C;
					case 3:
						goto IL_13F;
					case 4:
						A_0.WriteElementString(RecordTableEnumerator.b("̈́⩆╈⩊Ὄ⹎㽐㑒ご", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), listFillRange.RangeAddressLocal);
						num = 3;
						continue;
					case 5:
						if (listFillRange != null)
						{
							num = 4;
							continue;
						}
						goto IL_13F;
					case 6:
						if (!A_1.Display3DShading)
						{
							num = 8;
							continue;
						}
						goto IL_3AA;
					case 7:
						goto IL_237;
					case 8:
						A_0.WriteElementString(RecordTableEnumerator.b("ୄ⡆ᵈ⍊㽌⩎㑐ᝒ杔", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), string.Empty);
						num = 7;
						continue;
					}
					break;
					IL_13F:
					A_0.WriteElementString(RecordTableEnumerator.b("ᙄ≆╈", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), A_1.SelectedIndex.ToString());
					num = 6;
					continue;
					IL_23C:
					A_0.WriteElementString(RecordTableEnumerator.b("ፄ♆╈", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("睄", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("ࡄ⹆❈", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("畄", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("ࡄ♆ㅈ", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("睄", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("ౄ⥆⩈", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("瑄", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("ᕄ♆⹈⹊", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("組", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("ń㽆", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("瑄牆", a_));
					listFillRange = A_1.ListFillRange;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
			}
			IL_237:
			IL_3AA:
			A_0.WriteElementString(RecordTableEnumerator.b("ᙄ≆╈Ὂ㑌㽎㑐", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), spr\u1B68.SelectionTypes.Single.ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("ॄцᵈ", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), RecordTableEnumerator.b("ୄ⡆㭈♊ⱌ⍎", a_));
			A_0.WriteElementString(RecordTableEnumerator.b("ń㕆♈㭊Ṍ㭎⡐㽒ご", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), spr\u1B68.DropStyles.Combo.ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("ń㕆♈㭊Ō♎㽐㙒♔", a_), RecordTableEnumerator.b("い㕆❈煊㹌ⱎ㥐㙒㡔㙖⩘癚ぜ㙞ɠᅢ੤ᑦ٨൪ᥬ䉮ተᱲᡴ䵶ᙸᵺ᭼ᙾ뾄", a_), A_1.DropDownLines.ToString());
			A_0.WriteEndElement();
			return;
		}
	}
}
