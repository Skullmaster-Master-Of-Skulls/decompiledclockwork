using System;
using System.Drawing;
using System.Xml;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200023D RID: 573
internal class spr\u208B
{
	// Token: 0x060022A5 RID: 8869 RVA: 0x001369EC File Offset: 0x001359EC
	public void ᜁ(XmlWriter A_0, GradientStops A_1, IWorkbook A_2)
	{
		int a_ = 13;
		int num = 1;
		for (;;)
		{
			IL_1D:
			switch (num)
			{
			case 0:
				goto IL_46;
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
			while (A_0 != null)
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
					num = 2;
					goto IL_1D;
				}
			}
			if (true)
			{
			}
			num = 0;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑂㝄♆ⵈ≊⡌ⅎ═R⅔㡖⥘⡚", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⑂㝄♆ⵈൊ⑌⍎㵐", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
		this.ᜀ(A_0, A_1, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x00136ACC File Offset: 0x00135ACC
	private void ᜀ(XmlWriter A_0, GradientStops A_1, IWorkbook A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int top;
				int left;
				Rectangle fillToRect;
				switch (num)
				{
				case 0:
					goto IL_319;
				case 1:
					goto IL_319;
				case 2:
					goto IL_314;
				case 3:
					goto IL_9C;
				case 4:
					if (top != 0)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					goto IL_2BA;
				case 6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㝂", a_), top.ToString());
					num = 10;
					continue;
				case 7:
					goto IL_181;
				case 8:
					if (left != 0)
					{
						num = 18;
						continue;
					}
					goto IL_12F;
				case 9:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("⑂㙄୆㩈㽊", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
					int num2 = 0;
					int count = A_1.Count;
					num = 0;
					continue;
				}
				case 10:
					goto IL_2BA;
				case 11:
					num = 19;
					continue;
				case 12:
					goto IL_12F;
				case 13:
					goto IL_17C;
				case 14:
					goto IL_392;
				case 15:
					A_0.WriteEndElement();
					num = 17;
					continue;
				case 16:
					num = 20;
					continue;
				case 17:
				{
					if (A_1.GradientType == GradientType.Liniar)
					{
						num = 14;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㍂⑄㍆ⅈ", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
					string value = A_1.GradientType.ToString().ToLower();
					A_0.WriteAttributeString(RecordTableEnumerator.b("㍂⑄㍆ⅈ", a_), value);
					fillToRect = A_1.FillToRect;
					num = 24;
					continue;
				}
				case 18:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⽂", a_), left.ToString());
					num = 12;
					continue;
				case 19:
					if (fillToRect.Top == 0)
					{
						num = 21;
						continue;
					}
					goto IL_181;
				case 20:
					if (fillToRect.Bottom != 0)
					{
						num = 7;
						continue;
					}
					goto IL_459;
				case 21:
					num = 22;
					continue;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_138;
					default:
						if (false)
						{
						}
						if (fillToRect.Right == 0)
						{
							num = 16;
							continue;
						}
						goto IL_181;
					}
					break;
				case 23:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 15;
						continue;
					}
					this.ᜀ(A_0, A_1[num2], A_2);
					num2++;
					num = 1;
					continue;
				}
				case 24:
					if (fillToRect.Left == 0)
					{
						num = 11;
						continue;
					}
					goto IL_181;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 9;
				continue;
				IL_138:
				num = 4;
				continue;
				IL_12F:
				top = fillToRect.Top;
				goto IL_138;
				IL_181:
				A_0.WriteStartElement(RecordTableEnumerator.b("╂ⱄ⭆╈Ὂ≌ᵎ㑐げ⅔", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
				left = fillToRect.Left;
				num = 8;
				continue;
				IL_2BA:
				A_0.WriteAttributeString(RecordTableEnumerator.b("ㅂ", a_), fillToRect.Right.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("⅂", a_), fillToRect.Bottom.ToString());
				A_0.WriteEndElement();
				num = 2;
				continue;
				IL_319:
				num = 23;
			}
			IL_9C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
			IL_17C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑂㝄♆ⵈ≊⡌ⅎ═R⅔㡖⥘⡚", a_));
			IL_314:
			goto IL_459;
			IL_392:
			A_0.WriteStartElement(RecordTableEnumerator.b("⽂ⱄ⥆", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("≂⭄⁆", a_), A_1.Angle.ToString());
			A_0.WriteAttributeString(RecordTableEnumerator.b("あ♄♆╈⹊⥌", a_), RecordTableEnumerator.b("牂", a_));
			A_0.WriteEndElement();
			return;
			IL_459:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x00136F38 File Offset: 0x00135F38
	private void ᜀ(XmlWriter A_0, XlsGradientStop A_1, IWorkbook A_2)
	{
		int a_ = 17;
		int num = 3;
		for (;;)
		{
			IL_1D:
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_8B;
			case 2:
				goto IL_3E;
			}
			while (A_0 != null)
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
					num = 0;
					goto IL_1D;
				}
			}
			num = 2;
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁆㭈⩊⥌♎㑐㵒⅔іⵘ㑚ⵜ", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⁆㩈", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㝆♈㡊", a_), A_1.Position.ToString());
		spr\u1CFF.ᜀ(A_0, A_1.OColor.ᜁ(A_2), A_1.Transparency, A_1.Tint, A_1.Shade);
		A_0.WriteEndElement();
	}
}
