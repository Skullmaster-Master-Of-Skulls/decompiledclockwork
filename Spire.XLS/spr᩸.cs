using System;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020002CF RID: 719
internal class spr\u1A78 : spr\u2175
{
	// Token: 0x06002C2D RID: 11309 RVA: 0x00189C58 File Offset: 0x00188C58
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				Stream xmlDataStream;
				switch (num)
				{
				case 0:
					goto IL_243;
				case 1:
					goto IL_1B9;
				case 2:
					if (xmlDataStream != null)
					{
						num = 8;
						continue;
					}
					return;
				case 3:
				{
					sprᮋ sprᮋ;
					if (sprᮋ.\u170D())
					{
						num = 11;
						continue;
					}
					this.ᜁ(A_0, RecordTableEnumerator.b("㝂⩄", a_), A_1.RightColumn, A_1.RightColumnOffset, A_1.BottomRow, A_1.BottomRowOffset, A_1.Worksheet, RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_));
					num = 5;
					continue;
				}
				case 4:
					goto IL_2E0;
				case 5:
					goto IL_243;
				case 6:
					if (xmlDataStream.Length > 0L)
					{
						num = 17;
						continue;
					}
					return;
				case 7:
					A_0.WriteStartElement(RecordTableEnumerator.b("ⱂ⭄≆ੈ⹊⅌⍎ၐ㵒㙔㽖㙘⥚", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_));
					num = 9;
					continue;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_1BE;
				case 10:
					goto IL_84;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_243;
					default:
					{
						if (false)
						{
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("♂㵄㍆", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_));
						int num2 = (int)spr\u17FF.ᜀ((double)A_1.Width, MeasureUnits.EMU);
						A_0.WriteAttributeString(RecordTableEnumerator.b("⁂㵄", a_), num2.ToString());
						num2 = (int)spr\u17FF.ᜀ((double)A_1.Height, MeasureUnits.EMU);
						A_0.WriteAttributeString(RecordTableEnumerator.b("⁂㱄", a_), num2.ToString());
						A_0.WriteEndElement();
						num = 0;
						continue;
					}
					}
					break;
				case 12:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 18;
					continue;
				case 13:
				{
					sprᮋ sprᮋ;
					if (sprᮋ.\u170D())
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㝂㉄⡆ੈ⹊⅌⍎ၐ㵒㙔㽖㙘⥚", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_));
					num = 14;
					continue;
				}
				case 14:
					goto IL_1BE;
				case 16:
					goto IL_293;
				case 17:
				{
					sprᮋ sprᮋ = A_1.ClientAnchor;
					num = 13;
					continue;
				}
				case 18:
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					xmlDataStream = A_1.XmlDataStream;
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 12;
				continue;
				IL_1BE:
				this.ᜁ(A_0, RecordTableEnumerator.b("╂㝄⡆⑈", a_), A_1.LeftColumn, A_1.LeftColumnOffset, A_1.TopRow, A_1.TopRowOffset, A_1.Worksheet, RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_));
				num = 3;
				continue;
				IL_243:
				xmlDataStream.Position = 0L;
				XmlReader reader = UtilityMethods.ᜀ(xmlDataStream);
				A_0.WriteNode(reader, false);
				A_0.WriteElementString(RecordTableEnumerator.b("⁂⥄⹆ⱈ╊㥌୎ぐ❒㑔", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢횤힦\udba8캪첬쮮슰\udbb2킴튶춸ﾺ쾼\udebe뛀ꫂꯄꃆ", a_), string.Empty);
				A_0.WriteEndElement();
				num = 16;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
			IL_1B9:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ♆㥈⹊", a_));
			IL_293:
			return;
			IL_2E0:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭂⩄⭆ⵈ⹊㽌", a_));
		}
		}
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x0018A024 File Offset: 0x00189024
	public override void ᜀ(XmlWriter A_0, Type A_1)
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
		throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x0018A07C File Offset: 0x0018907C
	public new static string ᜀ(XlsShape A_0)
	{
		int a_ = 15;
		int num = 7;
		for (;;)
		{
			string result;
			switch (num)
			{
			case 0:
				return result;
			case 1:
				goto IL_A0;
			case 2:
				return result;
			case 3:
				return result;
			case 4:
				goto IL_67;
			case 5:
				if (A_0.IsMoveWithCell)
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				result = RecordTableEnumerator.b("⑄╆㩈⑊⅌㩎═㙒", a_);
				num = 0;
				continue;
			case 6:
				num = 8;
				continue;
			case 8:
				if (A_0.IsSizeWithCell)
				{
					num = 1;
					continue;
				}
				result = RecordTableEnumerator.b("⩄⥆ⱈࡊ⡌⍎㵐", a_);
				num = 3;
				continue;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
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
				num = 4;
				continue;
			}
			IL_A0:
			result = RecordTableEnumerator.b("ㅄう♈ࡊ⡌⍎㵐", a_);
			num = 2;
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆⡈㭊⡌", a_));
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x0018A1AC File Offset: 0x001891AC
	internal new void ᜁ(XmlWriter A_0, string A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheetBase A_6, string A_7)
	{
		int a_ = 11;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_86;
			case 2:
				goto IL_6C;
			case 3:
				num = 7;
				continue;
			case 4:
				if (A_6 is XlsWorksheet)
				{
					num = 2;
					continue;
				}
				goto IL_E7;
			case 5:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				goto IL_CB;
			case 6:
				goto IL_44;
			case 7:
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 5;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_6C:
		goto IL_9C;
		IL_86:
		goto IL_CB;
		IL_9C:
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
		return;
		IL_CB:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕀≂≄ॆ⡈♊⡌", a_));
		IL_E7:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_9C;
		default:
			if (false)
			{
			}
			this.ᜀ(A_0, A_1, A_2, A_4, A_7);
			return;
		}
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x0018A2CC File Offset: 0x001892CC
	private new void ᜀ(XmlWriter A_0, string A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheetBase A_6, string A_7)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 8;
			XlsWorksheet xlsWorksheet;
			double num3;
			for (;;)
			{
				double num2;
				switch (num)
				{
				case 0:
					goto IL_18B;
				case 1:
					num2 = (double)xlsWorksheet.GetColumnWidthPixels(A_2);
					goto IL_1A1;
				case 2:
					num = 12;
					continue;
				case 3:
					if (xlsWorksheet == null)
					{
						num = 13;
						continue;
					}
					num = 11;
					continue;
				case 4:
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					num = 10;
					continue;
				case 5:
					num = 14;
					continue;
				case 6:
					goto IL_112;
				case 7:
					goto IL_136;
				case 9:
					if (xlsWorksheet == null)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				case 10:
					goto IL_208;
				case 11:
					goto IL_153;
				case 12:
					if (A_1.Length == 0)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_208;
					default:
						if (false)
						{
						}
						xlsWorksheet = (A_6 as XlsWorksheet);
						num3 = 0.0;
						num = 9;
						continue;
					}
					break;
				case 13:
					num = 6;
					continue;
				case 14:
					num2 = (double)1;
					goto IL_1A1;
				case 15:
					goto IL_83;
				}
				if (A_6 == null)
				{
					num = 15;
					continue;
				}
				num = 4;
				continue;
				IL_1A1:
				num3 = num2;
				num3 = num3 * (double)A_3 / 1024.0;
				num3 = Math.Round(spr\u17FF.ᜀ(num3, MeasureUnits.EMU));
				A_2--;
				num = 3;
				continue;
				IL_208:
				if (A_1 == null)
				{
					goto IL_1E8;
				}
				num = 2;
			}
			IL_83:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽┿❁ぃ", a_));
			IL_112:
			double num4 = (double)1;
			goto IL_21C;
			IL_136:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_153:
			num4 = (double)xlsWorksheet.GetRowHeightPixels(A_4);
			goto IL_21C;
			IL_18B:
			IL_1E8:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠻弽✿ు╃⭅ⵇ", a_));
			IL_21C:
			double num5 = num4;
			num5 = num5 * (double)A_5 / 256.0;
			num5 = Math.Round(spr\u17FF.ᜀ(num5, MeasureUnits.EMU));
			A_4--;
			A_0.WriteStartElement(A_1, A_7);
			A_0.WriteElementString(RecordTableEnumerator.b("弻儽ⰿ", a_), A_7, A_2.ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("弻儽ⰿു≃⁅", a_), A_7, ((int)num3).ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("主儽㜿", a_), A_7, A_4.ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("主儽㜿ു≃⁅", a_), A_7, ((int)num5).ToString());
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x0018A5A4 File Offset: 0x001895A4
	private new void ᜀ(XmlWriter A_0, string A_1, int A_2, int A_3, string A_4)
	{
		int a_ = 8;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46;
				default:
					goto IL_BD;
				}
				break;
			case 2:
				goto IL_46;
			case 3:
				goto IL_3C;
			case 4:
				if (A_1 != null)
				{
					num = 2;
					continue;
				}
				goto IL_93;
			case 5:
				goto IL_5E;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_46:
			num = 0;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_5E:
		IL_93:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨽ℿ╁੃❅╇⽉", a_));
		IL_BD:
		if (false)
		{
		}
		A_0.WriteStartElement(A_1, A_4);
		string value = this.ᜀ(A_2);
		A_0.WriteElementString(RecordTableEnumerator.b("䘽", a_), A_4, value);
		value = this.ᜀ(A_3);
		A_0.WriteElementString(RecordTableEnumerator.b("䜽", a_), A_4, value);
		A_0.WriteEndElement();
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x0018A6C4 File Offset: 0x001896C4
	private new string ᜀ(int A_0)
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
		return XmlConvert.ToString((double)A_0 / 1000.0);
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x0018A710 File Offset: 0x00189710
	public new static void ᜀ(XmlWriter A_0, string A_1, string A_2, int A_3, int A_4, int A_5, int A_6)
	{
		int a_ = 17;
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
			}
			IL_24:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("㽆⽈㥊⁌", a_), A_1);
		A_0.WriteStartElement(RecordTableEnumerator.b("⡆⽈ⵊ", a_), A_2);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㽆", a_), A_3.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("㹆", a_), A_4.ToString());
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("≆ㅈ㽊", a_), A_2);
		A_0.WriteAttributeString(RecordTableEnumerator.b("⑆ㅈ", a_), A_5.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⑆え", a_), A_6.ToString());
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x0018A82C File Offset: 0x0018982C
	internal new static void ᜀ(XmlWriter A_0, string A_1, string A_2, int A_3, int A_4, int A_5, int A_6, IShape A_7)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_76;
			case 1:
				if (A_7.Rotation != 0)
				{
					num = 2;
					continue;
				}
				goto IL_EB;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
				}
				if (false)
				{
				}
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭈⑊㥌", a_), (A_7.Rotation * 60000).ToString());
				num = 0;
				continue;
			case 4:
				goto IL_40;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 4;
			}
			else
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("ㅈⵊ㽌≎", a_), A_1);
				num = 1;
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_76:
		IL_EB:
		A_0.WriteStartElement(RecordTableEnumerator.b("♈ⵊ⭌", a_), A_2);
		A_0.WriteAttributeString(RecordTableEnumerator.b("ㅈ", a_), A_3.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("え", a_), A_4.ToString());
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("ⱈ㍊㥌", a_), A_2);
		A_0.WriteAttributeString(RecordTableEnumerator.b("⩈㍊", a_), A_5.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⩈㉊", a_), A_6.ToString());
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x0018A9CC File Offset: 0x001899CC
	protected new void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, string A_3)
	{
		int a_ = 4;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("夹爻䠽ဿぁ", a_), A_3);
				sprវ sprវ = A_2.ᜋ();
				int num;
				sprវ.ᜅ(num = sprវ.\u171E() + 1);
				int num2 = num;
				A_0.WriteAttributeString(RecordTableEnumerator.b("匹堻", a_), num2.ToString());
				string name = A_1.Name;
				int num3 = 15;
				for (;;)
				{
					string alternativeText;
					switch (num3)
					{
					case 0:
						if (A_1.IsHyperlink)
						{
							num3 = 10;
							continue;
						}
						goto IL_2A6;
					case 1:
						if (!A_1.Visible)
						{
							num3 = 4;
							continue;
						}
						goto IL_14D;
					case 2:
						goto IL_242;
					case 3:
						num3 = 12;
						continue;
					case 4:
						A_0.WriteAttributeString(RecordTableEnumerator.b("刹唻娽␿❁⩃", a_), RecordTableEnumerator.b("ହ", a_));
						num3 = 13;
						continue;
					case 5:
						goto IL_198;
					case 6:
						A_0.WriteAttributeString(RecordTableEnumerator.b("帹夻䴽⌿ぁ", a_), alternativeText);
						num3 = 5;
						continue;
					case 7:
						if (alternativeText != null)
						{
							num3 = 3;
							continue;
						}
						goto IL_198;
					case 8:
						A_0.WriteAttributeString(RecordTableEnumerator.b("吹崻匽┿", a_), name);
						num3 = 11;
						continue;
					case 9:
						num3 = 14;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25A;
						default:
						{
							if (false)
							{
							}
							A_0.WriteStartElement(RecordTableEnumerator.b("刹倻圽⸿⥁݃⩅ⅇ⥉❋", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾝ즟첡", a_));
							string value = A_2.ᜈ().ᜀ(A_1.ImageRelation);
							A_0.WriteAttributeString(RecordTableEnumerator.b("匹堻", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽", a_), value);
							A_0.WriteEndElement();
							num3 = 2;
							continue;
						}
						}
						break;
					case 11:
						goto IL_27F;
					case 12:
						goto IL_25A;
					case 13:
						goto IL_14D;
					case 14:
						if (name.Length > 0)
						{
							num3 = 8;
							continue;
						}
						goto IL_27F;
					case 15:
						if (name != null)
						{
							num3 = 9;
							continue;
						}
						goto IL_27F;
					}
					break;
					IL_14D:
					num3 = 0;
					continue;
					IL_198:
					num3 = 1;
					continue;
					IL_25A:
					if (true)
					{
					}
					if (alternativeText.Length > 0)
					{
						num3 = 6;
						continue;
					}
					goto IL_198;
					IL_27F:
					alternativeText = A_1.AlternativeText;
					num3 = 7;
				}
			}
			IL_242:
			IL_2A6:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06002C37 RID: 11319 RVA: 0x0018AC88 File Offset: 0x00189C88
	protected new void ᜀ(XmlWriter A_0)
	{
		int a_ = 9;
		if (true)
		{
		}
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2C;
				}
			}
			IL_2C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("伾㍀あㅄFⱈ⑊⁌", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("伾㍀あㅄ", a_), RecordTableEnumerator.b("䴾⑀⁂ㅄ", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("帾㝀ག㙄㍆", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06002C38 RID: 11320 RVA: 0x0018AD58 File Offset: 0x00189D58
	protected new void ᜀ(XmlWriter A_0, XlsShape A_1, sprវ A_2, RelationsCollection A_3)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3A;
			case 1:
			{
				spr\u1C26 a_ = (spr\u1C26)A_1.Fill;
				spr\u1CFF.ᜀ(A_0, a_, A_2, A_3);
				num = 5;
				continue;
			}
			case 2:
				goto IL_A8;
			case 4:
				if (A_1.HasLineFormat)
				{
					num = 0;
					continue;
				}
				goto IL_A8;
			case 5:
				goto IL_61;
			}
			if (A_1.HasFill)
			{
				num = 1;
				continue;
			}
			goto IL_61;
			IL_3A:
			if (true)
			{
			}
			IShapeLineFormat line = A_1.Line;
			this.ᜀ(A_0, line, A_2.\u171C());
			num = 2;
			continue;
			IL_A8:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3A;
			default:
				goto IL_BE;
			}
			IL_61:
			num = 4;
		}
		IL_BE:
		if (false)
		{
		}
	}

	// Token: 0x06002C39 RID: 11321 RVA: 0x0018AE2C File Offset: 0x00189E2C
	protected new void ᜀ(XmlWriter A_0, IShapeLineFormat A_1, IWorkbook A_2)
	{
		int a_ = 5;
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("场匼", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺞ좠춢", a_));
			int num = (int)(A_1.Weight * 12700.0);
			A_0.WriteAttributeString(RecordTableEnumerator.b("䰺", a_), num.ToString());
			XLSXShapeLineStyle style = (XLSXShapeLineStyle)A_1.Style;
			A_0.WriteAttributeString(RecordTableEnumerator.b("堺值伾╀", a_), style.ToString());
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					if (A_1.Weight > 0.0)
					{
						num2 = 1;
						continue;
					}
					goto IL_C7;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C7;
					default:
						if (false)
						{
						}
						spr\u1CFF.ᜀ(A_0, A_1.ForeColor, false, A_2, 1.0 - A_1.Transparency);
						num2 = 2;
						continue;
					}
					break;
				case 2:
					goto IL_14F;
				case 3:
					goto IL_103;
				}
				break;
				IL_C7:
				A_0.WriteElementString(RecordTableEnumerator.b("唺刼社⡀⽂⥄", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺞ좠춢", a_), string.Empty);
				num2 = 3;
			}
		}
		IL_103:
		IL_14F:
		A_0.WriteEndElement();
	}
}
