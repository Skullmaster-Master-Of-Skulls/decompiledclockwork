using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200023E RID: 574
internal class spr\u1AA0
{
	// Token: 0x060022A9 RID: 8873 RVA: 0x00137068 File Offset: 0x00136068
	static spr\u1AA0()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1AA0.ᜅ = new Dictionary<KeyValuePair<string, string>, ChartLinePatternType>();
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("㙄⡆╈≊⥌", a_), string.Empty), ChartLinePatternType.Solid);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("⥄⁆ൈ⩊㹌❎", a_), string.Empty), ChartLinePatternType.Dash);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("㙄㹆㩈ཊⱌ㱎㥐", a_), string.Empty), ChartLinePatternType.Dot);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("⅄♆㩈⍊", a_), string.Empty), ChartLinePatternType.DashDot);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("⥄⁆ൈ⩊㹌❎ᕐ㱒⅔ፖ㙘⽚", a_), string.Empty), ChartLinePatternType.DashDotDot);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("㙄⡆╈≊⥌", a_), RecordTableEnumerator.b("㕄⑆㵈籊硌", a_)), ChartLinePatternType.DarkGray);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("㙄⡆╈≊⥌", a_), RecordTableEnumerator.b("㕄⑆㵈繊経", a_)), ChartLinePatternType.MediumGray);
		spr\u1AA0.ᜅ.Add(new KeyValuePair<string, string>(RecordTableEnumerator.b("㙄⡆╈≊⥌", a_), RecordTableEnumerator.b("㕄⑆㵈祊硌", a_)), ChartLinePatternType.LightGray);
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x001371EC File Offset: 0x001361EC
	public static void ᜀ(XlsWorkbook A_0)
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
		spr\u1AA0.ᜆ = A_0;
	}

	// Token: 0x060022AB RID: 8875 RVA: 0x00137230 File Offset: 0x00136230
	public static void ᜀ(XmlReader A_0, sprᮟ A_1, sprវ A_2, RelationsCollection A_3)
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
		spr\u1AA0.ᜀ(A_0, A_1, A_2, A_3, null);
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x00137280 File Offset: 0x00136280
	public static void ᜀ(XmlReader A_0, sprᮟ A_1, sprវ A_2, RelationsCollection A_3, float? A_4)
	{
		int a_ = 3;
		for (;;)
		{
			IL_09:
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					A_0.Read();
					num = 7;
					continue;
				case 2:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					spr\u1AA0.ᜀ(A_0, A_1, A_3, A_2, A_4);
					num = 5;
					continue;
				case 3:
					goto IL_B7;
				case 4:
					if (!A_0.IsEmptyElement)
					{
						num = 1;
						continue;
					}
					goto IL_12D;
				case 5:
					goto IL_B9;
				case 6:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						goto IL_B9;
					}
					break;
				case 8:
					goto IL_D9;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
				IL_B9:
				num = 2;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_B7:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴸帺䔼䬾@ㅂ⁄♆", a_));
		IL_D9:
		IL_12D:
		A_0.Read();
	}

	// Token: 0x060022AD RID: 8877 RVA: 0x001373C4 File Offset: 0x001363C4
	public static void ᜀ(XmlReader A_0, sprᮟ A_1, RelationsCollection A_2, sprវ A_3, float? A_4)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 4;
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
					switch (num)
					{
					case 0:
						goto IL_267;
					case 1:
						num = 2;
						continue;
					case 2:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							if (true)
							{
							}
							num = 10;
							continue;
						}
						goto IL_1FF;
					}
					case 3:
						num = 0;
						continue;
					case 5:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("嬶堸䈺刼䨾㕀", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_236;
					}
					case 6:
						num = 7;
						continue;
					case 7:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䐶䤸欺似", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_1BB;
					}
					case 8:
						num = 16;
						continue;
					case 9:
						num = 5;
						continue;
					case 10:
						num = 13;
						continue;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䌶䄸欺似", a_)))
						{
							num = 8;
							continue;
						}
						((XlsChartTextArea)A_1).ParagraphType = ChartParagraphType.Default;
						num = 15;
						continue;
					}
					case 12:
						return;
					case 13:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䌶䄸", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_24B;
					}
					case 14:
						goto IL_16F;
					case 15:
						goto IL_F2;
					case 16:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("堶伸帺似匾⁀㩂", a_)))
						{
							num = 3;
							continue;
						}
						Stream a_2 = ShapeParser.ReadNodeAsStream(A_0);
						((XlsChartTextArea)A_1).OverlayStream = a_2;
						num = 12;
						continue;
					}
					}
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					goto IL_2D6;
				}
				IL_16F:
				num = 11;
			}
			IL_F2:
			spr\u2306 a_3 = A_3.\u1718();
			float? num2 = A_4;
			spr\u1AA0.ᜀ(A_0, A_1, a_3, (num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null);
			return;
			IL_1BB:
			IChartFrameFormat frameFormat = A_1.FrameFormat;
			spr\u1772 a_4 = new spr\u1A7B(frameFormat.Border as XlsChartBorder, frameFormat.Interior as XlsChartInterior, frameFormat.Fill as spr\u1C26, frameFormat.Shadow, frameFormat.Format3D);
			spr\u1AA0.ᜀ(A_0, a_4, A_3, A_2);
			return;
			IL_1FF:
			A_0.Skip();
			return;
			IL_236:
			Stream a_5 = ShapeParser.ReadNodeAsStream(A_0);
			A_1.ᜀ(a_5);
			return;
			IL_24B:
			spr\u1AA0.ᜂ(A_0, A_1, A_3.\u1718(), A_4);
			return;
			IL_267:
			goto IL_1FF;
			IL_2D6:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x001376B0 File Offset: 0x001366B0
	private static void ᜀ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, double? A_3)
	{
		int a_ = 6;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 20;
				continue;
			case 1:
				goto IL_1C8;
			case 2:
				goto IL_220;
			case 3:
				if (A_3 != null)
				{
					num = 7;
					continue;
				}
				goto IL_220;
			case 4:
				goto IL_254;
			case 5:
				goto IL_1CD;
			case 6:
				if (true)
				{
				}
				goto IL_17B;
			case 7:
				A_1.Size = A_3.Value;
				num = 2;
				continue;
			case 8:
				if (!(A_0.LocalName != RecordTableEnumerator.b("堻嬽☿၁ᑃ㑅", a_)))
				{
					num = 21;
					continue;
				}
				A_0.Read();
				num = 5;
				continue;
			case 9:
				goto IL_1CD;
			case 10:
				num = 8;
				continue;
			case 11:
				if (A_0.LocalName != RecordTableEnumerator.b("䠻䘽ဿぁ", a_))
				{
					A_0.Read();
					num = 18;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_254;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 12:
				goto IL_14F;
			case 13:
				if (A_0.LocalName != RecordTableEnumerator.b("䠻䘽ဿぁ", a_))
				{
					num = 10;
					continue;
				}
				goto IL_154;
			case 14:
				goto IL_113;
			case 15:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 22;
					continue;
				}
				goto IL_154;
			case 17:
				goto IL_83;
			case 18:
				goto IL_17B;
			case 19:
				if (A_0.LocalName == RecordTableEnumerator.b("堻嬽☿၁ᑃ㑅", a_))
				{
					num = 4;
					continue;
				}
				goto IL_2EF;
			case 20:
				if (A_0.LocalName != RecordTableEnumerator.b("䠻䘽ဿぁ", a_))
				{
					num = 14;
					continue;
				}
				A_0.Read();
				num = 9;
				continue;
			case 21:
				goto IL_154;
			case 22:
				num = 13;
				continue;
			}
			if (A_0 == null)
			{
				num = 17;
				continue;
			}
			num = 0;
			continue;
			IL_154:
			num = 3;
			continue;
			IL_17B:
			num = 11;
			continue;
			IL_1CD:
			num = 15;
			continue;
			IL_220:
			num = 19;
			continue;
			IL_254:
			spr\u1AA0.ᜀ(A_0, A_1, A_2, null);
			num = 6;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_113:
		throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝", a_));
		IL_14F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁Ƀ⥅㩇❉ⵋ㩍⑏㭑㩓ㅕ", a_));
		IL_1C8:
		IL_2EF:
		A_0.Read();
	}

	// Token: 0x060022AF RID: 8879 RVA: 0x001379B4 File Offset: 0x001369B4
	public static string ᜄ(XmlReader A_0)
	{
		int a_ = 15;
		int num = 1;
		string value;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BE;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍄♆╈", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_B8;
				}
				break;
			case 2:
				goto IL_40;
			case 3:
				goto IL_5B;
			case 4:
				value = A_0.Value;
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_5B:
		goto IL_BE;
		IL_B8:
		throw new XmlException();
		IL_BE:
		A_0.Read();
		return value;
	}

	// Token: 0x060022B0 RID: 8880 RVA: 0x00137A88 File Offset: 0x00136A88
	public static bool ᜃ(XmlReader A_0)
	{
		int a_ = 16;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		}
		IL_50:
		string s = spr\u1AA0.ᜄ(A_0);
		return XmlConvert.ToBoolean(s);
	}

	// Token: 0x060022B1 RID: 8881 RVA: 0x00137AF4 File Offset: 0x00136AF4
	public static int ᜂ(XmlReader A_0)
	{
		int a_ = 14;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
			}
		}
		string s = spr\u1AA0.ᜄ(A_0);
		return XmlConvert.ToInt32(s);
	}

	// Token: 0x060022B2 RID: 8882 RVA: 0x00137B60 File Offset: 0x00136B60
	public static double ᜁ(XmlReader A_0)
	{
		int a_ = 13;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		}
		IL_50:
		string s = spr\u1AA0.ᜄ(A_0);
		return XmlConvert.ToDouble(s);
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x00137BCC File Offset: 0x00136BCC
	public static void ᜀ(XmlReader A_0, XlsChartBorder A_1, spr\u2306 A_2)
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
		spr\u1AA0.ᜀ(A_0, A_1, false, A_2);
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x00137C10 File Offset: 0x00136C10
	public static void ᜀ(XmlReader A_0, IShapeFill A_1, spr\u2306 A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				string text;
				XLSXGradientPattern xlsxgradientPattern;
				string text2;
				switch (num)
				{
				case 1:
					goto IL_1FD;
				case 2:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁁⍃Յ⑇㡉", a_)))
					{
						num = 26;
						continue;
					}
					A_0.Read();
					int num2;
					int num3;
					int num4;
					Color color = spr\u1AA0.ᜀ(A_0, out num2, out num3, out num4, A_2);
					A_1.BackColor = color;
					A_0.Read();
					num = 20;
					continue;
				}
				case 3:
					goto IL_1FD;
				case 4:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 27;
						continue;
					}
					A_0.Skip();
					num = 3;
					continue;
				case 5:
					A_0.Read();
					num = 19;
					continue;
				case 6:
					num = 22;
					continue;
				case 7:
					goto IL_227;
				case 8:
					text = A_0.Value;
					goto IL_284;
				case 9:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㉁㙃㕅㱇", a_)))
					{
						num = 11;
						continue;
					}
					num = 8;
					continue;
				case 10:
					xlsxgradientPattern = XLSXGradientPattern.dashDnDiag;
					goto IL_330;
				case 11:
					num = 29;
					continue;
				case 12:
					xlsxgradientPattern = (XLSXGradientPattern)Enum.Parse(typeof(XLSXGradientPattern), text2, false);
					goto IL_330;
				case 13:
					goto IL_222;
				case 14:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 6;
						continue;
					}
					goto IL_1E9;
				}
				case 15:
					if (A_0.LocalName != RecordTableEnumerator.b("㉁╃㉅㱇౉╋≍㱏", a_))
					{
						num = 25;
						continue;
					}
					num = 9;
					continue;
				case 16:
					goto IL_1FD;
				case 17:
					goto IL_1E9;
				case 18:
					num = 2;
					continue;
				case 19:
					goto IL_1FD;
				case 20:
					goto IL_1FD;
				case 21:
					if (text2 == null)
					{
						num = 7;
						continue;
					}
					num = 12;
					continue;
				case 22:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⑁⍃Յ⑇㡉", a_)))
					{
						num = 18;
						continue;
					}
					A_0.Read();
					int num2;
					int num3;
					int num4;
					Color color = spr\u1AA0.ᜀ(A_0, out num2, out num3, out num4, A_2);
					A_1.ForeColor = color;
					A_0.Read();
					num = 16;
					continue;
				}
				case 23:
					if (!A_0.IsEmptyElement)
					{
						num = 5;
						continue;
					}
					goto IL_3D9;
				case 24:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 13;
						continue;
					}
					num = 4;
					continue;
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_227;
					default:
						goto IL_176;
					}
					break;
				case 26:
					if (true)
					{
					}
					num = 17;
					continue;
				case 27:
					num = 14;
					continue;
				case 28:
					goto IL_B6;
				case 29:
					text = null;
					goto IL_284;
				}
				if (A_0 == null)
				{
					num = 28;
					continue;
				}
				num = 15;
				continue;
				IL_1E9:
				A_0.Skip();
				num = 1;
				continue;
				IL_1FD:
				num = 24;
				continue;
				IL_227:
				num = 10;
				continue;
				IL_284:
				text2 = text;
				num = 21;
				continue;
				IL_330:
				XLSXGradientPattern xlsxgradientPattern2 = xlsxgradientPattern;
				GradientPatternType pattern = (GradientPatternType)xlsxgradientPattern2;
				A_1.Pattern = pattern;
				A_0.MoveToElement();
				num = 23;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_176:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⁗㝙せ繝ᑟͣ͡䡥", a_));
			IL_222:
			IL_3D9:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x00138000 File Offset: 0x00137000
	public static void ᜀ(XmlReader A_0, XlsChartInterior A_1, spr\u2306 A_2, out int A_3)
	{
		int a_ = 14;
		if (A_1 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("ⵃ⡅㱇⽉㹋❍㽏⁑", a_));
			}
		}
		spr\u1AA0.ᜀ(A_0, A_2, A_1.ForegroundColorObject, out A_3);
		A_1.UseDefaultFormat = false;
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x00138074 File Offset: 0x00137074
	public static void ᜀ(XmlReader A_0, XlsChartInterior A_1, spr\u2306 A_2)
	{
		int a_ = 8;
		if (A_1 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("圽⸿㙁⅃㑅ⅇ╉㹋", a_));
			}
		}
		int num;
		spr\u1AA0.ᜀ(A_0, A_2, A_1.ForegroundColorObject, out num);
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x001380E0 File Offset: 0x001370E0
	internal static void ᜀ(XmlReader A_0, spr\u2306 A_1, OColor A_2)
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
		int num = 100000;
		spr\u1AA0.ᜀ(A_0, A_1, A_2, out num);
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x0013812C File Offset: 0x0013712C
	public static void ᜀ(XmlReader A_0, spr\u2306 A_1, OColor A_2, out int A_3)
	{
		int a_ = 5;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_150;
			case 1:
				goto IL_173;
			case 2:
				num = 14;
				continue;
			case 3:
				num = 4;
				continue;
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䠺似堾⍀B⥄㕆", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_8C;
			}
			case 5:
				goto IL_150;
			case 6:
				goto IL_141;
			case 7:
				num = 9;
				continue;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("䠺刼匾⡀❂̈́⹆╈❊", a_))
				{
					num = 6;
					continue;
				}
				A_3 = 100000;
				num = 21;
				continue;
			case 9:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 3;
					continue;
				}
				goto IL_F3;
			}
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8C;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 5;
					continue;
				}
				break;
			case 11:
				goto IL_150;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 20;
				continue;
			case 14:
				goto IL_F3;
			case 15:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䠺帼圾⑀⹂⁄ц╈㥊", a_)))
				{
					num = 2;
					continue;
				}
				A_2.ᜀ(spr\u1AA0.ᜁ(A_0, out A_3, A_1));
				num = 17;
				continue;
			}
			case 16:
				goto IL_150;
			case 17:
				goto IL_150;
			case 18:
				num = 15;
				continue;
			case 19:
				goto IL_87;
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 0;
				continue;
			case 21:
				if (!A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				goto IL_2A1;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 19;
				continue;
			}
			num = 8;
			continue;
			IL_8C:
			int num2;
			int num3;
			A_2.ᜀ(spr\u1AA0.ᜁ(A_0, out A_3, out num2, out num3, A_1));
			num = 16;
			continue;
			IL_F3:
			A_0.Skip();
			num = 11;
			continue;
			IL_150:
			num = 12;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_141:
		throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
		IL_173:
		IL_2A1:
		A_0.Read();
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x001383E4 File Offset: 0x001373E4
	public static Color ᜅ(XmlReader A_0, spr\u2306 A_1)
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
		int num;
		int num2;
		int num3;
		return spr\u1AA0.ᜁ(A_0, out num, out num2, out num3, A_1);
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x0013842C File Offset: 0x0013742C
	public static Color ᜁ(XmlReader A_0, out int A_1, out int A_2, out int A_3, spr\u2306 A_4)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 20;
			Color color;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("㌿ぁ⍃⑅େ♉㹋", a_))
					{
						num = 16;
						continue;
					}
					bool isEmptyElement = A_0.IsEmptyElement;
					color = spr\u1D39.ᜂ;
					A_1 = 100000;
					A_2 = -1;
					A_3 = -1;
					num = 19;
					continue;
				}
				case 2:
					num = 28;
					continue;
				case 3:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 34;
						continue;
					}
					goto IL_23F;
				}
				case 4:
					goto IL_1A5;
				case 5:
					goto IL_1A5;
				case 6:
					A_0.Read();
					num = 17;
					continue;
				case 7:
					goto IL_CA;
				case 8:
					goto IL_1A5;
				case 9:
					goto IL_23F;
				case 10:
					goto IL_1A5;
				case 11:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("ℿ⹁㑃⹅⥇", a_)))
					{
						num = 25;
						continue;
					}
					A_1 = spr\u1AA0.ᜂ(A_0);
					num = 14;
					continue;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22E;
					default:
					{
						if (false)
						{
						}
						string value = A_0.Value;
						int a_2 = int.Parse(value, NumberStyles.HexNumber, null);
						color = spr\u1D39.ᜀ(a_2);
						num = 13;
						continue;
					}
					}
					break;
				case 13:
					A_0.MoveToElement();
					A_0.Read();
					num = 23;
					continue;
				case 14:
					goto IL_1A5;
				case 15:
					num = 21;
					continue;
				case 16:
					goto IL_426;
				case 17:
					goto IL_26E;
				case 18:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⤿ⱁ㉃Ņ⥇❉⅋⽍", a_)))
					{
						num = 15;
						continue;
					}
					A_0.Skip();
					num = 31;
					continue;
				}
				case 19:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㘿⍁⡃", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_2B9;
				case 21:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㐿⭁⩃㉅", a_)))
					{
						num = 29;
						continue;
					}
					A_2 = spr\u1AA0.ᜂ(A_0);
					num = 22;
					continue;
				}
				case 22:
					goto IL_1A5;
				case 23:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						goto IL_22E;
					}
					return color;
				}
				case 24:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㌿⩁╃≅ⵇ", a_)))
					{
						num = 32;
						continue;
					}
					A_3 = spr\u1AA0.ᜂ(A_0);
					num = 4;
					continue;
				}
				case 25:
					num = 27;
					continue;
				case 26:
					if (true)
					{
					}
					num = 18;
					continue;
				case 27:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("✿⍁⥃⭅⥇", a_)))
					{
						num = 26;
						continue;
					}
					A_0.Skip();
					num = 10;
					continue;
				}
				case 28:
					goto IL_1A5;
				case 29:
					num = 24;
					continue;
				case 30:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 0;
						continue;
					}
					A_0.Skip();
					num = 8;
					continue;
				case 31:
					goto IL_1A5;
				case 32:
					num = 9;
					continue;
				case 33:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					num = 30;
					continue;
				case 34:
					num = 11;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
				IL_1A5:
				num = 33;
				continue;
				IL_22E:
				num = 2;
				continue;
				IL_23F:
				color = spr\u1AA0.ᜀ(A_0, color, A_4, out A_1);
				num = 5;
			}
			IL_CA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_26E:
			return color;
			IL_2B9:
			throw new XmlException();
			IL_426:
			throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		}
		}
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x001388B8 File Offset: 0x001378B8
	public static Color ᜄ(XmlReader A_0, spr\u2306 A_1)
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
		int num;
		return spr\u1AA0.ᜁ(A_0, out num, A_1);
	}

	// Token: 0x060022BC RID: 8892 RVA: 0x001388FC File Offset: 0x001378FC
	public static Color ᜁ(XmlReader A_0, out int A_1, spr\u2306 A_2)
	{
		int a_ = 5;
		int num = 7;
		Color color;
		for (;;)
		{
			string a_2;
			switch (num)
			{
			case 0:
				goto IL_141;
			case 1:
				goto IL_141;
			case 2:
				goto IL_141;
			case 3:
				goto IL_88;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴺尼匾", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_AB;
			case 5:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("䠺帼圾⑀⹂⁄ц╈㥊", a_))
				{
					num = 15;
					continue;
				}
				bool isEmptyElement = A_0.IsEmptyElement;
				A_1 = 100000;
				a_2 = null;
				num = 4;
				continue;
			}
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				A_0.Skip();
				num = 1;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12A;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				num = 6;
				continue;
			case 9:
				a_2 = A_0.Value;
				num = 13;
				continue;
			case 10:
				goto IL_12A;
			case 11:
				goto IL_164;
			case 12:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 14;
					continue;
				}
				goto IL_1F3;
			}
			case 13:
				goto IL_AB;
			case 14:
				A_0.Read();
				num = 0;
				continue;
			case 15:
				goto IL_10F;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 5;
			continue;
			IL_AB:
			color = A_2.ᜎ(a_2);
			A_0.MoveToElement();
			num = 12;
			continue;
			IL_12A:
			color = spr\u1AA0.ᜀ(A_0, color, A_2, out A_1);
			num = 2;
			continue;
			IL_141:
			num = 8;
		}
		IL_88:
		throw new ArgumentNullException();
		IL_10F:
		throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜", a_));
		IL_164:
		IL_1F3:
		A_0.Read();
		return color;
	}

	// Token: 0x060022BD RID: 8893 RVA: 0x00138B04 File Offset: 0x00137B04
	internal static Color ᜀ(XmlReader A_0, out int A_1, spr\u2306 A_2)
	{
		int a_ = 6;
		int num = 14;
		Color color;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 4;
				continue;
			case 1:
				goto IL_8B;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E7;
				default:
					if (false)
					{
					}
					goto IL_14C;
				}
				break;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("倻弽㌿㙁݃⩅㩇", a_)))
				{
					num = 10;
					continue;
				}
				color = spr\u1D39.ᜂ;
				num = 13;
				continue;
			case 4:
				goto IL_14C;
			case 5:
				if (!A_0.IsEmptyElement)
				{
					num = 15;
					continue;
				}
				goto IL_212;
			case 6:
				goto IL_177;
			case 7:
				goto IL_110;
			case 8:
				goto IL_14C;
			case 9:
				goto IL_E7;
			case 10:
			{
				string value = A_0.Value;
				int a_2 = int.Parse(value, NumberStyles.HexNumber, null);
				color = spr\u1D39.ᜀ(a_2);
				num = 1;
				continue;
			}
			case 11:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 12:
				color = spr\u1AA0.ᜀ(A_0, color, A_2, out A_1);
				num = 8;
				continue;
			case 13:
				goto IL_8B;
			case 15:
				A_0.Read();
				num = 2;
				continue;
			case 16:
				goto IL_68;
			}
			if (A_0 == null)
			{
				num = 16;
				continue;
			}
			num = 9;
			continue;
			IL_8B:
			A_0.MoveToElement();
			num = 5;
			continue;
			IL_E7:
			if (A_0.LocalName != RecordTableEnumerator.b("伻䜽㌿Ł⡃㑅", a_))
			{
				num = 7;
				continue;
			}
			A_1 = 100000;
			num = 3;
			continue;
			IL_14C:
			num = 11;
		}
		IL_68:
		throw new ArgumentNullException();
		IL_110:
		throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝", a_));
		IL_177:
		IL_212:
		A_0.Read();
		return color;
	}

	// Token: 0x060022BE RID: 8894 RVA: 0x00138D2C File Offset: 0x00137D2C
	private static Color ᜀ(XmlReader A_0, Color A_1, spr\u2306 A_2, out int A_3)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				A_3 = 100000;
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						goto IL_223;
					case 2:
						goto IL_F3;
					case 3:
						goto IL_2C3;
					case 4:
						goto IL_30A;
					case 5:
						goto IL_AE;
					case 6:
						goto IL_148;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30A;
						default:
							if (false)
							{
							}
							if (spr\u22D2.ᜎ == null)
							{
								num = 9;
								continue;
							}
							goto IL_F8;
						}
						break;
					case 8:
						goto IL_F8;
					case 9:
						spr\u22D2.ᜎ = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("⹁ㅃ⭅Շ╉⡋", a_),
								0
							},
							{
								RecordTableEnumerator.b("⹁ㅃ⭅݇ⱉ⩋", a_),
								1
							},
							{
								RecordTableEnumerator.b("ㅁ╃㉅Շ╉⡋", a_),
								2
							},
							{
								RecordTableEnumerator.b("㙁ⵃ⡅㱇", a_),
								3
							},
							{
								RecordTableEnumerator.b("ㅁⱃ❅ⱇ⽉", a_),
								4
							},
							{
								RecordTableEnumerator.b("⍁⡃㙅⁇⭉", a_),
								5
							}
						};
						num = 8;
						continue;
					case 10:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 0;
							continue;
						}
						goto IL_228;
					}
					case 11:
						goto IL_23A;
					case 12:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							int num3 = spr\u1AA0.ᜂ(A_0);
							double a_2;
							double num4;
							double num5;
							spr\u2306.ᜀ(A_1, out a_2, out num4, out num5);
							num4 *= (double)num3 / 100000.0;
							A_1 = spr\u2306.ᜁ(a_2, num4, num5);
							num = 14;
							continue;
						}
						case 1:
						{
							int num6 = spr\u1AA0.ᜂ(A_0);
							double a_2;
							double num4;
							double num5;
							spr\u2306.ᜀ(A_1, out a_2, out num4, out num5);
							num4 += (double)(255 * num6) / 100000.0;
							A_1 = spr\u2306.ᜁ(a_2, num4, num5);
							num = 2;
							continue;
						}
						case 2:
						{
							int num7 = spr\u1AA0.ᜂ(A_0);
							double a_2;
							double num4;
							double num5;
							spr\u2306.ᜀ(A_1, out a_2, out num4, out num5);
							num5 *= (double)num7 / 100000.0;
							A_1 = spr\u2306.ᜁ(a_2, num4, num5);
							num = 1;
							continue;
						}
						case 3:
						{
							double a_3 = spr\u1AA0.ᜁ(A_0);
							A_1 = spr\u2306.ᜀ(A_1, a_3);
							num = 5;
							continue;
						}
						case 4:
						{
							int num8 = spr\u1AA0.ᜂ(A_0);
							double a_4 = (double)num8 / 100000.0;
							A_1 = A_2.ᜁ(A_1, a_4);
							num = 3;
							continue;
						}
						case 5:
							A_3 = spr\u1AA0.ᜂ(A_0);
							num = 6;
							continue;
						default:
							num = 13;
							continue;
						}
						break;
					}
					case 13:
						num = 16;
						continue;
					case 14:
						return A_1;
					case 15:
					{
						string localName;
						int num2;
						if (spr\u22D2.ᜎ.TryGetValue(localName, out num2))
						{
							num = 4;
							continue;
						}
						goto IL_228;
					}
					case 16:
						goto IL_228;
					}
					break;
					IL_F8:
					num = 15;
					continue;
					IL_228:
					A_0.Skip();
					num = 11;
					continue;
					IL_30A:
					num = 12;
				}
			}
			IL_AE:
			IL_F3:
			IL_148:
			IL_223:
			IL_23A:
			return A_1;
			IL_2C3:
			if (true)
			{
			}
			return A_1;
		}
	}

	// Token: 0x060022BF RID: 8895 RVA: 0x00139080 File Offset: 0x00138080
	private static void ᜀ(XmlReader A_0, XlsChartBorder A_1, bool A_2, spr\u2306 A_3)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				bool flag;
				int num3;
				string text;
				switch (num)
				{
				case 0:
					goto IL_156;
				case 1:
					spr\u22D2.ᜏ = new Dictionary<string, int>(10)
					{
						{
							RecordTableEnumerator.b("圸吺笼嘾ⵀ⽂", a_),
							0
						},
						{
							RecordTableEnumerator.b("䬸吺䠼儾╀", a_),
							1
						},
						{
							RecordTableEnumerator.b("吸刺䤼娾㍀", a_),
							2
						},
						{
							RecordTableEnumerator.b("嬸帺䬼娾ⵀ", a_),
							3
						},
						{
							RecordTableEnumerator.b("䨸吺儼嘾╀Ղⱄ⭆╈", a_),
							4
						},
						{
							RecordTableEnumerator.b("䤸䤺丼䬾Հ≂㙄⽆", a_),
							5
						},
						{
							RecordTableEnumerator.b("䤸娺䤼䬾݀⩂⥄⭆", a_),
							6
						},
						{
							RecordTableEnumerator.b("常䤺尼嬾݀⩂⥄⭆", a_),
							7
						},
						{
							RecordTableEnumerator.b("儸帺尼嬾рⵂ⅄", a_),
							8
						},
						{
							RecordTableEnumerator.b("䴸娺吼匾рⵂ⅄", a_),
							9
						}
					};
					num = 22;
					continue;
				case 2:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("唸唺", a_))
					{
						num = 5;
						continue;
					}
					bool isEmptyElement = A_0.IsEmptyElement;
					num = 38;
					continue;
				}
				case 3:
				{
					KeyValuePair<string, string> key;
					ChartLinePatternType pattern;
					if (spr\u1AA0.ᜅ.TryGetValue(key, out pattern))
					{
						num = 25;
						continue;
					}
					goto IL_73C;
				}
				case 4:
					if (flag)
					{
						num = 18;
						continue;
					}
					goto IL_73C;
				case 5:
					goto IL_5B5;
				case 6:
					goto IL_65A;
				case 7:
					goto IL_156;
				case 9:
					goto IL_FA;
				case 10:
					goto IL_5C6;
				case 11:
					goto IL_156;
				case 12:
					num = 20;
					continue;
				case 13:
					goto IL_156;
				case 14:
					goto IL_277;
				case 15:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 23;
						continue;
					}
					A_0.Skip();
					num = 32;
					continue;
				case 16:
					goto IL_695;
				case 17:
					A_0.Read();
					num = 21;
					continue;
				case 18:
					A_1.Pattern = ChartLinePatternType.Solid;
					num = 30;
					continue;
				case 19:
					goto IL_21C;
				case 20:
					if (spr\u22D2.ᜏ == null)
					{
						num = 1;
						continue;
					}
					goto IL_27C;
				case 21:
					goto IL_156;
				case 22:
					goto IL_27C;
				case 23:
					num = 26;
					continue;
				case 24:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 17;
						continue;
					}
					flag = true;
					A_1.UseDefaultLineColor = true;
					num = 39;
					continue;
				}
				case 25:
				{
					ChartLinePatternType pattern;
					A_1.Pattern = pattern;
					num = 14;
					continue;
				}
				case 26:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 12;
						continue;
					}
					goto IL_399;
				}
				case 27:
					goto IL_156;
				case 28:
					num = 16;
					continue;
				case 29:
				{
					int num2;
					switch (num2)
					{
					case 0:
						A_1.Pattern = ChartLinePatternType.None;
						A_0.Read();
						num = 44;
						continue;
					case 1:
						A_1.JoinType = XLSXBorderJoinType.Round;
						A_0.Read();
						num = 40;
						continue;
					case 2:
						A_1.JoinType = XLSXBorderJoinType.Mitter;
						A_0.Read();
						num = 43;
						continue;
					case 3:
						A_1.JoinType = XLSXBorderJoinType.Bevel;
						A_0.Read();
						num = 41;
						continue;
					case 4:
						A_1.OColor.AfterChange += A_1.ᜃ;
						spr\u1AA0.ᜀ(A_0, A_3, A_1.Color, out num3);
						A_1.Transparency = (double)(1f - (float)num3 / 100000f);
						A_1.OColor.AfterChange -= A_1.ᜃ;
						A_1.UseDefaultFormat = false;
						flag = true;
						num = 11;
						continue;
					case 5:
						text = spr\u1AA0.ᜄ(A_0);
						num = 7;
						continue;
					case 6:
						A_0.Skip();
						num = 0;
						continue;
					case 7:
					{
						A_1.UseDefaultFormat = false;
						GradientStops a_2 = spr\u1AA0.ᜂ(A_0, A_3);
						spr\u1AA0.ᜀ(a_2, A_1.Fill);
						A_1.Fill.ᜀ(a_2);
						A_1.HasLineProperties = true;
						num = 27;
						continue;
					}
					case 8:
						if (true)
						{
						}
						A_0.Skip();
						num = 13;
						continue;
					case 9:
						A_0.Skip();
						num = 46;
						continue;
					default:
						num = 35;
						continue;
					}
					break;
				}
				case 30:
					if (text != null)
					{
						num = 36;
						continue;
					}
					goto IL_73C;
				case 31:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 28;
						continue;
					}
					num = 15;
					continue;
				case 32:
					goto IL_156;
				case 33:
				{
					string localName;
					int num2;
					if (spr\u22D2.ᜏ.TryGetValue(localName, out num2))
					{
						num = 45;
						continue;
					}
					goto IL_399;
				}
				case 34:
					Math.Round((double)int.Parse(A_0.Value) / 12700.0);
					A_1.LineWeightString = A_0.Value;
					A_1.Weight = ChartLineWeightType.Hairline;
					num = 37;
					continue;
				case 35:
					num = 10;
					continue;
				case 36:
				{
					KeyValuePair<string, string> key = new KeyValuePair<string, string>(text, string.Empty);
					num = 3;
					continue;
				}
				case 37:
					goto IL_21C;
				case 38:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("丸", a_)))
						{
							num = 34;
							continue;
						}
						A_1.Weight = ChartLineWeightType.Hairline;
						num = 19;
						continue;
					}
					break;
				case 39:
					goto IL_695;
				case 40:
					goto IL_156;
				case 41:
					goto IL_156;
				case 42:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 2;
					continue;
				case 43:
					goto IL_156;
				case 44:
					goto IL_156;
				case 45:
					num = 29;
					continue;
				case 46:
					goto IL_156;
				}
				IL_E8:
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 42;
				continue;
				goto IL_E8;
				IL_156:
				num = 31;
				continue;
				IL_21C:
				num3 = 100000;
				flag = false;
				text = null;
				num = 24;
				continue;
				IL_27C:
				num = 33;
				continue;
				IL_695:
				num = 4;
			}
			IL_FA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_277:
			goto IL_73C;
			IL_399:
			throw new NotImplementedException();
			IL_5B5:
			throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚", a_));
			IL_5C6:
			goto IL_399;
			IL_65A:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬸吺似嬾⑀ㅂ", a_));
			IL_73C:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x001397D0 File Offset: 0x001387D0
	public static void ᜀ(XmlReader A_0, IShapeFill A_1, RelationsCollection A_2, sprវ A_3)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 22;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_309;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							if (true)
							{
							}
							num = 12;
							continue;
						}
						num = 2;
						continue;
					}
					break;
				case 1:
				{
					string value = A_0.Value;
					sprᦨ sprᦨ = A_2[value];
					string text = A_2.ItemPath;
					int length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					text = sprវ.ᜀ(text, sprᦨ.ᜂ());
					Image im = A_3.ᜋ(text);
					A_1.CustomPicture(im, RecordTableEnumerator.b("唻匽ℿ╁⅃", a_));
					num = 13;
					continue;
				}
				case 2:
					if (A_0.LocalName != RecordTableEnumerator.b("帻刽⤿㉁Ƀ⽅⑇♉", a_))
					{
						num = 24;
						continue;
					}
					A_0.Read();
					num = 8;
					continue;
				case 3:
					num = 23;
					continue;
				case 4:
					num = 25;
					continue;
				case 5:
					num = 20;
					continue;
				case 6:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 26;
						continue;
					}
					num = 17;
					continue;
				case 7:
					goto IL_283;
				case 8:
					goto IL_283;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("夻匽∿❁⁃", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽좋煉뎛겝邟銡銣覥\udaa7쾩삫쾭쒯\udbb1\udbb3\ud8b5쮷특햻캽뎿", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_378;
				case 10:
					goto IL_283;
				case 11:
					num = 19;
					continue;
				case 12:
					goto IL_34B;
				case 13:
					goto IL_378;
				case 14:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("伻䨽㈿❁ぃ╅⁇", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_26F;
				}
				case 15:
					goto IL_A7;
				case 16:
					goto IL_283;
				case 17:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 11;
						continue;
					}
					A_0.Skip();
					num = 16;
					continue;
				case 18:
					num = 14;
					continue;
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 4;
						continue;
					}
					goto IL_26F;
				}
				case 20:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䠻圽ⰿ❁", a_)))
					{
						num = 18;
						continue;
					}
					(A_1 as spr\u1C26).ᜀ(true);
					A_0.Skip();
					num = 21;
					continue;
				}
				case 21:
					goto IL_283;
				case 23:
					goto IL_26F;
				case 24:
					goto IL_190;
				case 25:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("帻刽⤿㉁", a_)))
					{
						num = 5;
						continue;
					}
					num = 9;
					continue;
				}
				case 26:
					goto IL_2A8;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				goto IL_309;
				IL_26F:
				A_0.Skip();
				num = 10;
				continue;
				IL_283:
				num = 6;
				continue;
				IL_309:
				num = 0;
				continue;
				IL_378:
				A_0.MoveToElement();
				A_0.Skip();
				num = 7;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_190:
			throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
			IL_2A8:
			A_0.Read();
			return;
			IL_34B:
			throw new ArgumentNullException(RecordTableEnumerator.b("搻刽㌿сⵃ⩅⑇", a_));
		}
		}
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x00139BC4 File Offset: 0x00138BC4
	private static void ᜂ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, float? A_3)
	{
		int a_ = 5;
		int num = 17;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_168;
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 8;
				continue;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 3:
				goto IL_159;
			case 4:
				goto IL_7F;
			case 5:
				num = 7;
				continue;
			case 6:
				if (true)
				{
				}
				num = 20;
				continue;
			case 7:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_12D;
			}
			case 8:
				goto IL_168;
			case 9:
				num = 21;
				continue;
			case 10:
				goto IL_18B;
			case 11:
				goto IL_168;
			case 12:
				goto IL_168;
			case 13:
				goto IL_128;
			case 14:
				num = 16;
				continue;
			case 15:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_21E;
				default:
					if (false)
					{
					}
					num = 19;
					continue;
				}
				break;
			case 16:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䠺䤼䴾ፀ♂⍄", a_)))
				{
					num = 6;
					continue;
				}
				spr\u1AA0.ᜀ(A_0, A_1);
				num = 0;
				continue;
			}
			case 18:
				goto IL_168;
			case 19:
				if (A_0.LocalName != RecordTableEnumerator.b("伺䔼", a_))
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 11;
				continue;
			case 20:
				goto IL_12D;
			case 21:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䤺吼尾⥀", a_)))
				{
					num = 14;
					continue;
				}
				spr\u1AA0.ᜁ(A_0, A_1, A_2, A_3);
				num = 18;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 15;
			continue;
			IL_12D:
			A_0.Skip();
			num = 12;
			continue;
			IL_168:
			num = 2;
		}
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_128:
		goto IL_21E;
		IL_159:
		throw new ArgumentNullException(RecordTableEnumerator.b("伺堼䜾㕀ɂ㝄≆⡈", a_));
		IL_18B:
		A_0.Read();
		return;
		IL_21E:
		throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x00139E6C File Offset: 0x00138E6C
	private static void ᜀ(XmlReader A_0, sprᮟ A_1)
	{
		int a_ = 10;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (true)
				{
				}
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 19;
					continue;
				}
				goto IL_D0;
			}
			case 1:
				goto IL_168;
			case 2:
				goto IL_18B;
			case 3:
				goto IL_168;
			case 5:
				num = 0;
				continue;
			case 6:
				(A_1 as XlsChartTextArea).IsFormula = true;
				num = 21;
				continue;
			case 7:
				goto IL_13D;
			case 8:
				goto IL_168;
			case 9:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("☿", a_))
				{
					num = 13;
					continue;
				}
				goto IL_D0;
			}
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				num = 17;
				continue;
			case 11:
				if (A_1 is XlsChartTextArea)
				{
					num = 6;
					continue;
				}
				num = 14;
				continue;
			case 12:
				goto IL_1EE;
			case 13:
				num = 11;
				continue;
			case 14:
				if (A_1 is XlsChartDataLabels)
				{
					num = 15;
					continue;
				}
				goto IL_1EE;
			case 15:
				(A_1 as XlsChartDataLabels).HasFormula = true;
				num = 12;
				continue;
			case 16:
				if (A_0.LocalName != RecordTableEnumerator.b("㌿㙁㙃ᑅⵇⱉ", a_))
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 18;
				continue;
			case 17:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 1;
				continue;
			case 18:
				goto IL_168;
			case 19:
				num = 9;
				continue;
			case 20:
				goto IL_7F;
			case 21:
				goto IL_1EE;
			}
			if (A_0 == null)
			{
				num = 20;
				continue;
			}
			num = 16;
			continue;
			IL_D0:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_22B;
			default:
				if (false)
				{
				}
				A_0.Skip();
				num = 3;
				continue;
			}
			IL_168:
			num = 10;
			continue;
			IL_1EE:
			A_1.Text = A_0.ReadElementContentAsString();
			num = 8;
		}
		IL_7F:
		goto IL_22B;
		IL_13D:
		throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
		IL_18B:
		A_0.Read();
		return;
		IL_22B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x0013A118 File Offset: 0x00139118
	private static void ᜁ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, float? A_3)
	{
		int a_ = 17;
		int num = 21;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1BC;
			case 1:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 23;
				continue;
			case 2:
				goto IL_1A9;
			case 3:
				num = 7;
				continue;
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("╆♈⽊㑌὎⍐", a_)))
				{
					num = 5;
					continue;
				}
				spr\u1AA0.ᜁ(A_0, A_1);
				num = 12;
				continue;
			}
			case 5:
				num = 20;
				continue;
			case 6:
				goto IL_1DF;
			case 7:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 8;
					continue;
				}
				goto IL_1A9;
			}
			case 8:
				num = 4;
				continue;
			case 9:
				goto IL_24E;
			case 10:
				goto IL_9E;
			case 11:
				num = 2;
				continue;
			case 12:
				goto IL_1BC;
			case 13:
				goto IL_1BC;
			case 14:
				goto IL_1BC;
			case 15:
				A_1.Text += '\n';
				num = 27;
				continue;
			case 16:
				goto IL_E5;
			case 17:
				goto IL_33D;
			case 18:
				goto IL_1BC;
			case 19:
				num = 24;
				continue;
			case 20:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⭆㩈㽊Ṍ㭎⡐㽒ご", a_)))
				{
					num = 19;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1AF;
				default:
					if (false)
					{
					}
					spr\u1AA0.ᜀ(A_0, A_1);
					num = 14;
					continue;
				}
				break;
			}
			case 22:
				goto IL_1BC;
			case 23:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				A_0.Skip();
				num = 18;
				continue;
			case 24:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㝆", a_)))
				{
					num = 11;
					continue;
				}
				num = 26;
				continue;
			}
			case 25:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("㕆⁈⡊╌", a_))
				{
					num = 9;
					continue;
				}
				A_0.Read();
				bool flag = true;
				num = 13;
				continue;
			}
			case 26:
			{
				bool flag;
				if (!flag)
				{
					num = 15;
					continue;
				}
				flag = false;
				num = 16;
				continue;
			}
			case 27:
				goto IL_E5;
			case 28:
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				num = 25;
				continue;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 28;
			continue;
			IL_E5:
			spr\u1AA0.ᜀ(A_0, A_1, A_2, A_3);
			num = 0;
			continue;
			IL_1AF:
			num = 22;
			continue;
			IL_1A9:
			A_0.Skip();
			goto IL_1AF;
			IL_1BC:
			num = 1;
		}
		IL_9E:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_1DF:
		A_0.Read();
		return;
		IL_24E:
		throw new XmlException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚╜㉞ൠ䍢ᅤ٦๨䕪", a_));
		IL_33D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍆ⱈ㍊㥌๎⍐㙒㑔", a_));
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x0013A488 File Offset: 0x00139488
	private static void ᜁ(XmlReader A_0, IChartTextArea A_1)
	{
		int a_ = 18;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
			case 1:
				goto IL_C3;
			case 2:
				if (A_0.LocalName != RecordTableEnumerator.b("⩇╉⡋㝍O⁑", a_))
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
			case 3:
				goto IL_FD;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_107;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㩇╉㡋", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_160;
			case 6:
			{
				int num2 = XmlConvert.ToInt32(A_0.Value);
				A_1.TextRotationAngle = num2 / 60000;
				A_0.MoveToElement();
				num = 3;
				continue;
			}
			case 7:
				goto IL_64;
			case 8:
				goto IL_A8;
			}
			if (A_0 == null)
			{
				num = 7;
			}
			else
			{
				num = 0;
			}
		}
		IL_64:
		goto IL_107;
		IL_A8:
		throw new XmlException(RecordTableEnumerator.b("ᵇ⑉⥋㙍⁏㝑㝓≕㵗㹙籛♝ൟ๡䑣ብ१൩䉫", a_));
		IL_C3:
		throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍ᅏ⁑ㅓ㝕", a_));
		IL_FD:
		if (true)
		{
		}
		goto IL_160;
		IL_107:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_160:
		A_0.Skip();
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x0013A5FC File Offset: 0x001395FC
	private static void ᜀ(XmlReader A_0, IChartTextArea A_1)
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3F;
			case 1:
				goto IL_F7;
			case 2:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 3:
				goto IL_79;
			case 4:
				if (A_0.LocalName != RecordTableEnumerator.b("⩅㭇㹉Ὃ㩍⥏㹑ㅓ", a_))
				{
					num = 3;
					continue;
				}
				goto IL_F9;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 2;
			}
		}
		IL_3F:
		goto IL_8F;
		IL_79:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		default:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙⑛㍝౟䉡ၣݥཧ䑩", a_));
		}
		IL_F7:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉅ⵇ㉉㡋ཌྷ≏㝑㕓", a_));
		IL_F9:
		A_0.Skip();
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x0013A708 File Offset: 0x00139708
	private static void ᜀ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, float? A_3)
	{
		int a_ = 10;
		int num = 19;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("〿", a_))
				{
					num = 23;
					continue;
				}
				A_0.Read();
				TextSettings a_2 = null;
				List<sprᜰ.ᜀ> a_3 = null;
				num = 17;
				continue;
			}
			case 1:
				num = 12;
				continue;
			case 2:
				goto IL_1CF;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("〿ቁ㙃", a_)))
				{
					num = 11;
					continue;
				}
				TextSettings a_2 = spr\u1AA0.ᜁ(A_0, A_2, A_3);
				num = 20;
				continue;
			}
			case 4:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 24;
					continue;
				}
				num = 7;
				continue;
			case 5:
				if (A_1 == null)
				{
					num = 21;
					continue;
				}
				num = 0;
				continue;
			case 6:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				goto IL_107;
			}
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Skip();
				num = 10;
				continue;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㈿", a_)))
				{
					num = 18;
					continue;
				}
				TextSettings a_2;
				List<sprᜰ.ᜀ> a_3 = spr\u1AA0.ᜀ(A_0, A_1, A_2, a_2, a_3);
				num = 2;
				continue;
			}
			case 9:
				num = 3;
				continue;
			case 10:
				goto IL_1CF;
			case 11:
				num = 8;
				continue;
			case 12:
				if ((A_1 as XlsChartTextArea).ChartAlRuns != null)
				{
					num = 15;
					continue;
				}
				goto IL_1CF;
			case 13:
				num = 6;
				continue;
			case 14:
				goto IL_8F;
			case 15:
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
					List<sprᜰ.ᜀ> a_3 = new List<sprᜰ.ᜀ>((A_1 as XlsChartTextArea).ChartAlRuns.ᜀ());
					num = 22;
					continue;
				}
				}
				break;
			case 16:
				goto IL_107;
			case 17:
				if (A_1 is XlsChartTextArea)
				{
					num = 1;
					continue;
				}
				goto IL_1CF;
			case 18:
				num = 16;
				continue;
			case 20:
				goto IL_1CF;
			case 21:
				goto IL_136;
			case 22:
				goto IL_1CF;
			case 23:
				goto IL_102;
			case 24:
				goto IL_1EF;
			case 25:
				goto IL_1CF;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 5;
			continue;
			IL_107:
			A_0.Skip();
			num = 25;
			continue;
			IL_1CF:
			num = 4;
		}
		IL_8F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_102:
		throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
		IL_136:
		throw new ArgumentNullException(RecordTableEnumerator.b("㐿❁㱃㉅े㡉⥋⽍", a_));
		IL_1EF:
		A_0.Read();
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x0013AA24 File Offset: 0x00139A24
	private static TextSettings ᜁ(XmlReader A_0, spr\u2306 A_1, float? A_2)
	{
		int a_ = 13;
		int num = 10;
		TextSettings result;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				result = spr\u1AA0.ᜀ(A_0, A_1, A_2);
				num = 3;
				continue;
			case 1:
				goto IL_157;
			case 2:
				num = 5;
				continue;
			case 3:
				goto IL_111;
			case 4:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				A_0.Skip();
				num = 11;
				continue;
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 13;
					continue;
				}
				goto IL_6E;
			}
			case 6:
				goto IL_111;
			case 7:
				if (!A_0.IsEmptyElement)
				{
					num = 15;
					continue;
				}
				goto IL_1B9;
			case 8:
				goto IL_111;
			case 9:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("❂⁄ⅆᭈᭊ㽌", a_))
				{
					num = 0;
					continue;
				}
				goto IL_6E;
			}
			case 11:
				goto IL_111;
			case 12:
				goto IL_6C;
			case 13:
				num = 9;
				continue;
			case 14:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					goto IL_14C;
				}
				num = 4;
				continue;
			case 15:
				A_0.Read();
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			result = null;
			num = 7;
			continue;
			IL_6E:
			A_0.Skip();
			num = 8;
			continue;
			IL_111:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_14C:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 14;
				break;
			}
		}
		IL_6C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_157:
		IL_1B9:
		A_0.Read();
		return result;
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x0013ABF4 File Offset: 0x00139BF4
	internal static TextSettings ᜃ(XmlReader A_0, spr\u2306 A_1)
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
		return spr\u1AA0.ᜀ(A_0, A_1, null);
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x0013AC40 File Offset: 0x00139C40
	internal static TextSettings ᜀ(XmlReader A_0, spr\u2306 A_1, float? A_2)
	{
		int a_ = 5;
		int num = 25;
		TextSettings textSettings;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 31;
					continue;
				}
				A_0.Skip();
				if (true)
				{
				}
				num = 32;
				continue;
			case 1:
				if (!A_0.IsEmptyElement)
				{
					num = 34;
					continue;
				}
				goto IL_4E1;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠺䜼", a_)))
				{
					num = 29;
					continue;
				}
				goto IL_410;
			case 3:
				goto IL_3AF;
			case 4:
				num = 33;
				continue;
			case 5:
				goto IL_340;
			case 6:
				goto IL_250;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("伺䐼伾⑀╂⑄⑆ⱈ", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_127;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("刺", a_)))
				{
					num = 22;
					continue;
				}
				goto IL_29D;
			case 9:
				goto IL_410;
			case 10:
				goto IL_22D;
			case 11:
				goto IL_127;
			case 12:
			{
				textSettings.ActualFontName = A_0.Value;
				string fontName = spr\u1AA0.ᜁ(A_0.Value.ToString());
				textSettings.FontName = fontName;
				num = 11;
				continue;
			}
			case 13:
				textSettings.Bold = new bool?(XmlConvert.ToBoolean(A_0.Value));
				num = 23;
				continue;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("夺", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_2DB;
			case 15:
				goto IL_22D;
			case 16:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("场尼儾♀", a_)))
				{
					num = 37;
					continue;
				}
				goto IL_3AF;
			case 18:
				num = 5;
				continue;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠺䤼䴾⡀⡂⁄", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_255;
			case 20:
				goto IL_255;
			case 21:
				textSettings.Striked = new bool?(A_0.Value != RecordTableEnumerator.b("唺刼氾㕀ㅂⱄⱆⱈ", a_));
				num = 20;
				continue;
			case 22:
				textSettings.Italic = new bool?(XmlConvert.ToBoolean(A_0.Value));
				num = 28;
				continue;
			case 23:
				goto IL_2DB;
			case 24:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 4;
					continue;
				}
				goto IL_340;
			}
			case 26:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䠺刼匾⡀❂̈́⹆╈❊", a_)))
				{
					num = 18;
					continue;
				}
				spr\u1AA0.ᜀ(A_0, textSettings, A_1);
				num = 27;
				continue;
			}
			case 27:
				goto IL_22D;
			case 28:
				goto IL_29D;
			case 29:
				textSettings.FontSize = new float?(XmlConvert.ToSingle(A_0.Value) / 100f);
				num = 9;
				continue;
			case 30:
				goto IL_40B;
			case 31:
				num = 24;
				continue;
			case 32:
				goto IL_22D;
			case 33:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("场尼䬾⡀ⵂ", a_)))
				{
					num = 30;
					continue;
				}
				textSettings.HasLatin = new bool?(true);
				num = 7;
				continue;
			}
			case 34:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40B;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 10;
					continue;
				}
				break;
			case 35:
				goto IL_C5;
			case 36:
				goto IL_22D;
			case 37:
				textSettings.Language = A_0.Value;
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 35;
				continue;
			}
			textSettings = new TextSettings();
			textSettings.FontSize = A_2;
			num = 14;
			continue;
			IL_127:
			A_0.MoveToElement();
			A_0.Skip();
			num = 15;
			continue;
			IL_22D:
			num = 16;
			continue;
			IL_255:
			num = 17;
			continue;
			IL_29D:
			num = 2;
			continue;
			IL_2DB:
			num = 8;
			continue;
			IL_340:
			A_0.Skip();
			num = 36;
			continue;
			IL_3AF:
			A_0.MoveToElement();
			num = 1;
			continue;
			IL_40B:
			num = 26;
			continue;
			IL_410:
			num = 19;
		}
		IL_C5:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_250:
		IL_4E1:
		A_0.Read();
		return textSettings;
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x0013B138 File Offset: 0x0013A138
	internal static string ᜁ(string A_0)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			XlsFont xlsFont;
			for (;;)
			{
				xlsFont = null;
				int num = 4;
				for (;;)
				{
					string[] array;
					switch (num)
					{
					case 0:
					{
						string a;
						if ((a = array[1]) != null)
						{
							num = 8;
							continue;
						}
						goto IL_1F5;
					}
					case 1:
						if (true)
						{
						}
						num = 10;
						continue;
					case 2:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("刽㐿", a_)))
						{
							num = 17;
							continue;
						}
						spr\u1AA0.ᜆ.MajorFonts.TryGetValue(RecordTableEnumerator.b("刽㐿", a_), out xlsFont);
						num = 3;
						continue;
					}
					case 3:
						goto IL_D1;
					case 4:
						if (!(A_0 == RecordTableEnumerator.b("ᔽⴿ⡁楃⩅㱇", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_12F;
					case 5:
						num = 0;
						continue;
					case 6:
						num = 9;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A0;
						default:
						{
							if (false)
							{
							}
							string a;
							if (!(a == RecordTableEnumerator.b("崽㌿", a_)))
							{
								num = 6;
								continue;
							}
							spr\u1AA0.ᜆ.MajorFonts.TryGetValue(RecordTableEnumerator.b("崽㌿", a_), out xlsFont);
							num = 15;
							continue;
						}
						}
						break;
					case 8:
						num = 2;
						continue;
					case 9:
						goto IL_1F3;
					case 10:
						if (A_0 == RecordTableEnumerator.b("ᔽⴿ⡁楃⍅⥇", a_))
						{
							num = 14;
							continue;
						}
						return A_0;
					case 11:
						goto IL_291;
					case 12:
						num = 7;
						continue;
					case 13:
						if (array[0] == RecordTableEnumerator.b("ᔽⴿ⡁", a_))
						{
							num = 5;
							continue;
						}
						goto IL_1F5;
					case 14:
						goto IL_12F;
					case 15:
						goto IL_1A7;
					case 16:
						goto IL_A0;
					case 17:
						num = 19;
						continue;
					case 18:
						if (!(A_0 == RecordTableEnumerator.b("ᔽⴿ⡁楃╅㭇", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_12F;
					case 19:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("嬽ℿ", a_)))
						{
							num = 12;
							continue;
						}
						spr\u1AA0.ᜆ.MajorFonts.TryGetValue(RecordTableEnumerator.b("嬽ℿ", a_), out xlsFont);
						num = 11;
						continue;
					}
					}
					break;
					IL_A0:
					num = 18;
					continue;
					IL_12F:
					array = A_0.Split(new char[]
					{
						'-'
					});
					num = 13;
				}
			}
			IL_D1:
			IL_1A7:
			IL_1F3:
			IL_1F5:
			return xlsFont.FontName;
			IL_291:
			goto IL_1F5;
		}
		}
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x0013B438 File Offset: 0x0013A438
	private static void ᜀ(XmlReader A_0, TextSettings A_1, spr\u2306 A_2)
	{
		int a_ = 7;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B6;
			case 1:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 10;
					continue;
				}
				goto IL_66;
			}
			case 2:
				goto IL_B6;
			case 3:
				A_1.FontColor = new Color?(spr\u1AA0.ᜅ(A_0, A_2));
				num = 13;
				continue;
			case 4:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					goto IL_CB;
				}
				num = 5;
				continue;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 6:
				if (true)
				{
				}
				A_0.Read();
				num = 0;
				continue;
			case 7:
				num = 1;
				continue;
			case 8:
				goto IL_D6;
			case 9:
				goto IL_B6;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CB;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			case 12:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("丼䴾♀⅂ل⭆㭈", a_))
				{
					num = 3;
					continue;
				}
				goto IL_66;
			}
			case 13:
				goto IL_B6;
			}
			if (!A_0.IsEmptyElement)
			{
				num = 6;
				continue;
			}
			break;
			IL_66:
			A_0.Skip();
			num = 9;
			continue;
			IL_B6:
			num = 4;
			continue;
			IL_CB:
			num = 8;
		}
		IL_D6:
		A_0.Read();
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x0013B5D8 File Offset: 0x0013A5D8
	private static List<sprᜰ.ᜀ> ᜀ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, TextSettings A_3, List<sprᜰ.ᜀ> A_4)
	{
		int a_ = 0;
		int num = 28;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 30;
				continue;
			case 1:
				if (A_4 != null)
				{
					num = 21;
					continue;
				}
				goto IL_1E4;
			case 2:
				goto IL_279;
			case 3:
				num = 24;
				continue;
			case 4:
				(A_1 as XlsChartTextArea).ChartAlRuns.ᜀ(A_4.ToArray());
				num = 26;
				continue;
			case 5:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 8;
				continue;
			case 6:
			{
				ushort a_2 = (ushort)A_1.Text.Length;
				num = 27;
				continue;
			}
			case 7:
				if (A_1 is XlsChartTextArea)
				{
					num = 25;
					continue;
				}
				goto IL_1E4;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("䐵", a_))
				{
					goto IL_26E;
				}
				A_0.Read();
				spr\u1AA0.ᜀ(A_1, A_3);
				num = 14;
				continue;
			case 9:
				if (A_1.Text != null)
				{
					num = 6;
					continue;
				}
				goto IL_AE;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䐵样䠹", a_)))
				{
					num = 3;
					continue;
				}
				spr\u1AA0.ᜀ(A_0, A_1, A_2, A_3);
				num = 22;
				continue;
			}
			case 11:
				goto IL_1D1;
			case 12:
				goto IL_391;
			case 13:
				goto IL_207;
			case 14:
				goto IL_1E4;
			case 15:
				num = 10;
				continue;
			case 16:
				num = 11;
				continue;
			case 17:
				if ((A_1 as XlsChartTextArea).ChartAlRuns != null)
				{
					num = 4;
					continue;
				}
				goto IL_1E4;
			case 18:
				goto IL_1E4;
			case 19:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				num = 20;
				continue;
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				A_0.Skip();
				num = 18;
				continue;
			case 21:
			{
				ushort a_2;
				A_4.Add(new sprᜰ.ᜀ(a_2, (ushort)A_1.Font.Index));
				num = 7;
				continue;
			}
			case 22:
				goto IL_1E4;
			case 23:
				goto IL_1E4;
			case 24:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䈵", a_)))
				{
					num = 16;
					continue;
				}
				ushort a_2 = 0;
				num = 9;
				continue;
			}
			case 25:
				num = 17;
				continue;
			case 26:
				goto IL_1E4;
			case 27:
				goto IL_AE;
			case 29:
				goto IL_A9;
			case 30:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 15;
					continue;
				}
				goto IL_1D1;
			}
			}
			if (A_0 == null)
			{
				num = 29;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_26E;
			default:
				if (false)
				{
				}
				num = 5;
				continue;
			}
			IL_AE:
			A_1.Text += A_0.ReadElementContentAsString();
			num = 1;
			continue;
			IL_1D1:
			A_0.Skip();
			num = 23;
			continue;
			IL_1E4:
			num = 19;
			continue;
			IL_26E:
			num = 2;
		}
		IL_A9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_207:
		A_0.Read();
		return A_4;
		IL_279:
		if (true)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
		IL_391:
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻缽㈿❁╃", a_));
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x0013B9AC File Offset: 0x0013A9AC
	public static void ᜀ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2, TextSettings A_3)
	{
		int a_ = 6;
		int num = 38;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5B6;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帻", a_)))
				{
					num = 31;
					continue;
				}
				goto IL_254;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("唻", a_)))
				{
					num = 26;
					continue;
				}
				goto IL_376;
			case 3:
				if (!A_0.IsEmptyElement)
				{
					num = 23;
					continue;
				}
				goto IL_6A3;
			case 4:
				goto IL_FD;
			case 5:
				A_1.Underline = FontUnderlineType.Double;
				num = 6;
				continue;
			case 6:
				goto IL_227;
			case 7:
				goto IL_211;
			case 8:
				if (A_1 is XlsChartTextArea)
				{
					num = 41;
					continue;
				}
				goto IL_2D1;
			case 9:
				goto IL_227;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("倻弽㐿⭁⩃", a_)))
				{
					num = 35;
					continue;
				}
				A_1.Font.HasLatin = true;
				num = 13;
				continue;
			}
			case 11:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 24;
					continue;
				}
				num = 48;
				continue;
			case 12:
				goto IL_3DB;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻䜽〿❁≃❅⭇⽉", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_181;
			case 14:
				if (A_0.Value == RecordTableEnumerator.b("伻倽✿", a_))
				{
					num = 34;
					continue;
				}
				num = 28;
				continue;
			case 15:
				A_1.Font.ActualFontName = A_0.Value;
				A_1.FontName = spr\u1AA0.ᜁ(A_0.Value);
				A_0.MoveToElement();
				num = 37;
				continue;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("倻弽⸿╁", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_14D;
			case 17:
				num = 46;
				continue;
			case 18:
				num = 7;
				continue;
			case 19:
				num = 14;
				continue;
			case 20:
				num = 10;
				continue;
			case 21:
			{
				XlsFont xlsFont;
				A_1.FontName = xlsFont.FontName;
				num = 25;
				continue;
			}
			case 22:
			{
				XlsFont xlsFont;
				if (spr\u1AA0.ᜆ.MinorFonts.TryGetValue(RecordTableEnumerator.b("倻弽㐿⭁⩃", a_), out xlsFont))
				{
					num = 21;
					continue;
				}
				goto IL_14D;
			}
			case 23:
				A_0.Read();
				num = 44;
				continue;
			case 24:
				goto IL_2F4;
			case 25:
				goto IL_14D;
			case 26:
				A_1.IsItalic = XmlConvert.ToBoolean(A_0.Value);
				num = 49;
				continue;
			case 27:
				goto IL_5C7;
			case 28:
				if (A_0.Value == RecordTableEnumerator.b("堻尽ⰿ", a_))
				{
					num = 5;
					continue;
				}
				goto IL_227;
			case 29:
				goto IL_2D1;
			case 30:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("伻䐽", a_)))
				{
					num = 42;
					continue;
				}
				goto IL_3DB;
			case 31:
				A_1.IsBold = XmlConvert.ToBoolean(A_0.Value);
				num = 47;
				continue;
			case 32:
			{
				A_1.Font.Language = A_0.Value;
				XlsFont xlsFont = null;
				num = 22;
				continue;
			}
			case 33:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("伻䨽㈿⭁⽃⍅", a_)))
				{
					num = 50;
					continue;
				}
				goto IL_40F;
			case 34:
				A_1.Underline = FontUnderlineType.Single;
				num = 9;
				continue;
			case 35:
				num = 45;
				continue;
			case 36:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤻", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_227;
			case 37:
				goto IL_181;
			case 39:
				goto IL_40F;
			case 40:
				goto IL_2D1;
			case 41:
				if (true)
				{
				}
				(A_1 as XlsChartTextArea).IsAutoColor = false;
				num = 51;
				continue;
			case 42:
				A_1.Size = (double)int.Parse(A_0.Value) / 100.0;
				num = 12;
				continue;
			case 43:
				goto IL_2D1;
			case 44:
				goto IL_2D1;
			case 45:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("伻儽ⰿ⭁⁃Eⅇ♉⁋", a_)))
				{
					num = 18;
					continue;
				}
				OColor ocolor = A_1.ᜀ();
				spr\u1AA0.ᜀ(A_0, A_2, ocolor);
				A_1.Color = ocolor.ᜁ(A_2.ᜁ().Workbook);
				num = 8;
				continue;
			}
			case 46:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 20;
					continue;
				}
				goto IL_211;
			}
			case 47:
				goto IL_254;
			case 48:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5B6;
				default:
					if (false)
					{
					}
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 17;
						continue;
					}
					A_0.Skip();
					num = 29;
					continue;
				}
				break;
			case 49:
				goto IL_376;
			case 50:
				A_1.IsStrikethrough = (A_0.Value != RecordTableEnumerator.b("刻儽ጿ㙁㙃⽅⍇⽉", a_));
				num = 39;
				continue;
			case 51:
				goto IL_2D1;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_14D:
			num = 1;
			continue;
			IL_181:
			A_0.Skip();
			num = 40;
			continue;
			IL_211:
			A_0.Skip();
			num = 43;
			continue;
			IL_227:
			A_0.MoveToElement();
			num = 3;
			continue;
			IL_254:
			num = 2;
			continue;
			IL_2D1:
			num = 11;
			continue;
			IL_376:
			num = 33;
			continue;
			IL_3DB:
			num = 36;
			continue;
			IL_40F:
			num = 30;
			continue;
			IL_5B6:
			if (A_1 == null)
			{
				num = 27;
			}
			else
			{
				spr\u1AA0.ᜀ(A_1, A_3);
				num = 16;
			}
		}
		IL_FD:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_2F4:
		goto IL_6A3;
		IL_5C7:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁Ճ㑅ⵇ⭉", a_));
		IL_6A3:
		A_0.Skip();
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x0013C06C File Offset: 0x0013B06C
	internal static void ᜀ(IInternalFont A_0, TextSettings A_1)
	{
		int a_ = 7;
		int num = 30;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.IsBold = A_1.Bold.Value;
				num = 26;
				continue;
			case 1:
				A_0.IsStrikethrough = A_1.Striked.Value;
				num = 32;
				continue;
			case 2:
				A_0.Size = (double)A_1.FontSize.Value;
				num = 8;
				continue;
			case 3:
				if (A_1.Bold != null)
				{
					num = 0;
					continue;
				}
				goto IL_163;
			case 4:
				if (A_1.FontSize != null)
				{
					num = 2;
					continue;
				}
				goto IL_346;
			case 5:
				return;
			case 6:
				A_0.Font.HasLatin = A_1.HasLatin.Value;
				num = 20;
				continue;
			case 7:
				if (A_1.FontName != null)
				{
					num = 35;
					continue;
				}
				goto IL_2C3;
			case 8:
				goto IL_346;
			case 9:
				if (A_1.HasEastAsianFont != null)
				{
					num = 23;
					continue;
				}
				goto IL_1B6;
			case 10:
				if (A_1.HasLatin != null)
				{
					num = 6;
					continue;
				}
				goto IL_45E;
			case 11:
				A_0.Font.Color = A_1.FontColor.Value;
				num = 16;
				continue;
			case 12:
				if (A_1.ActualFontName != null)
				{
					num = 27;
					continue;
				}
				return;
			case 13:
				goto IL_2C3;
			case 14:
				goto IL_13B;
			case 15:
				goto IL_1B6;
			case 16:
				goto IL_18B;
			case 17:
			{
				A_0.Font.Language = A_1.Language;
				XlsFont xlsFont = null;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_341;
				default:
					if (false)
					{
					}
					num = 25;
					continue;
				}
				break;
			}
			case 18:
				A_0.Font.\u1716 = A_1;
				num = 3;
				continue;
			case 19:
				if (A_1.Italic != null)
				{
					num = 34;
					continue;
				}
				goto IL_110;
			case 20:
				goto IL_45E;
			case 21:
				goto IL_341;
			case 22:
			{
				XlsFont xlsFont;
				A_0.FontName = xlsFont.FontName;
				num = 24;
				continue;
			}
			case 23:
				A_0.Font.HasEastAsianFont = A_1.HasEastAsianFont.Value;
				num = 15;
				continue;
			case 24:
				goto IL_412;
			case 25:
			{
				XlsFont xlsFont;
				if (spr\u1AA0.ᜆ.MinorFonts.TryGetValue(RecordTableEnumerator.b("儼帾㕀⩂⭄", a_), out xlsFont))
				{
					num = 22;
					continue;
				}
				goto IL_412;
			}
			case 26:
				goto IL_163;
			case 27:
				A_0.Font.ActualFontName = A_1.ActualFontName.ToString();
				num = 5;
				continue;
			case 28:
				A_0.Font.HasComplexScripts = A_1.HasComplexScripts.Value;
				num = 14;
				continue;
			case 29:
				if (A_1.FontColor != null)
				{
					num = 11;
					continue;
				}
				goto IL_18B;
			case 30:
				if (true)
				{
				}
				break;
			case 31:
				if (A_1.Language != null)
				{
					num = 17;
					continue;
				}
				goto IL_412;
			case 32:
				goto IL_3EF;
			case 33:
				if (A_1.Striked != null)
				{
					num = 1;
					continue;
				}
				goto IL_3EF;
			case 34:
				A_0.IsItalic = A_1.Italic.Value;
				num = 21;
				continue;
			case 35:
				A_0.FontName = A_1.FontName;
				num = 13;
				continue;
			case 36:
				if (A_1.HasComplexScripts != null)
				{
					num = 28;
					continue;
				}
				goto IL_13B;
			}
			if (A_1 != null)
			{
				num = 18;
				continue;
			}
			break;
			IL_110:
			num = 4;
			continue;
			IL_341:
			goto IL_110;
			IL_13B:
			num = 9;
			continue;
			IL_163:
			num = 19;
			continue;
			IL_18B:
			num = 10;
			continue;
			IL_1B6:
			num = 12;
			continue;
			IL_2C3:
			num = 33;
			continue;
			IL_346:
			num = 7;
			continue;
			IL_3EF:
			num = 31;
			continue;
			IL_412:
			num = 29;
			continue;
			IL_45E:
			num = 36;
		}
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x0013C504 File Offset: 0x0013B504
	public static GradientStops ᜂ(XmlReader A_0, spr\u2306 A_1)
	{
		int a_ = 10;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 21;
				continue;
			case 1:
			{
				if (true)
				{
				}
				if (A_0.LocalName != RecordTableEnumerator.b("✿ぁ╃≅็⍉⁋≍", a_))
				{
					num = 18;
					continue;
				}
				GradientStops gradientStops = null;
				A_0.Read();
				num = 6;
				continue;
			}
			case 2:
				goto IL_17F;
			case 3:
				num = 7;
				continue;
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 11;
					continue;
				}
				goto IL_119;
			}
			case 6:
				goto IL_17F;
			case 7:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⰿ⭁⩃", a_)))
				{
					num = 16;
					continue;
				}
				GradientStops gradientStops;
				gradientStops.GradientType = GradientType.Liniar;
				A_0.MoveToAttribute(RecordTableEnumerator.b("ℿⱁ⍃", a_));
				gradientStops.Angle = int.Parse(A_0.Value);
				A_0.Read();
				num = 2;
				continue;
			}
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("✿ㅁࡃ㕅㱇", a_)))
				{
					num = 3;
					continue;
				}
				GradientStops gradientStops = spr\u1AA0.ᜁ(A_0, A_1);
				num = 17;
				continue;
			}
			case 9:
				goto IL_17F;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("〿⍁ぃ⹅", a_)))
				{
					num = 12;
					continue;
				}
				GradientStops gradientStops;
				spr\u1AA0.ᜀ(A_0, gradientStops);
				num = 9;
				continue;
			}
			case 11:
				num = 8;
				continue;
			case 12:
				num = 19;
				continue;
			case 13:
				num = 5;
				continue;
			case 14:
				goto IL_17F;
			case 15:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 20;
					continue;
				}
				num = 0;
				continue;
			case 16:
				num = 10;
				continue;
			case 17:
				goto IL_17F;
			case 18:
				goto IL_165;
			case 19:
				goto IL_119;
			case 20:
				goto IL_1A2;
			case 21:
				goto IL_17F;
			case 22:
				goto IL_83;
			}
			if (A_0 == null)
			{
				num = 22;
				continue;
			}
			num = 1;
			continue;
			IL_119:
			A_0.Skip();
			num = 14;
			continue;
			IL_17F:
			num = 15;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_165:
		throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
		IL_1A2:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_83;
		default:
		{
			if (false)
			{
			}
			A_0.Read();
			GradientStops gradientStops;
			return gradientStops;
		}
		}
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x0013C7FC File Offset: 0x0013B7FC
	private static void ᜀ(XmlReader A_0, GradientStops A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				bool isEmptyElement = A_0.IsEmptyElement;
				A_0.MoveToAttribute(RecordTableEnumerator.b("㕄♆㵈⍊", a_));
				A_1.GradientType = (GradientType)Enum.Parse(typeof(GradientType), A_0.Value, true);
				int num = 5;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						num2 = 0;
						goto IL_2B4;
					case 1:
						num2 = int.Parse(A_0.Value);
						goto IL_2B4;
					case 2:
						num = 14;
						continue;
					case 3:
						num3 = int.Parse(A_0.Value);
						goto IL_24F;
					case 4:
						num3 = 0;
						goto IL_24F;
					case 5:
						if (!isEmptyElement)
						{
							num = 20;
							continue;
						}
						return;
					case 6:
						num = 4;
						continue;
					case 7:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㝄", a_)))
						{
							num = 2;
							continue;
						}
						num = 16;
						continue;
					case 8:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("❄", a_)))
						{
							num = 6;
							continue;
						}
						num = 3;
						continue;
					case 9:
						goto IL_1A5;
					case 10:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("⥄", a_)))
						{
							num = 9;
							continue;
						}
						num = 1;
						continue;
					case 11:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("ㅄ", a_)))
						{
							num = 19;
							continue;
						}
						num = 12;
						continue;
					case 12:
						num4 = int.Parse(A_0.Value);
						goto IL_27D;
					case 13:
						return;
					case 14:
						num5 = 0;
						goto IL_111;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A5;
						default:
							if (false)
							{
							}
							num4 = 0;
							goto IL_27D;
						}
						break;
					case 16:
						num5 = int.Parse(A_0.Value);
						goto IL_111;
					case 17:
						num = 10;
						continue;
					case 18:
						if (A_0.LocalName == RecordTableEnumerator.b("⍄⹆╈❊᥌⁎͐㙒㙔⍖", a_))
						{
							num = 17;
							continue;
						}
						return;
					case 19:
						num = 15;
						continue;
					case 20:
						A_0.Read();
						num = 18;
						continue;
					}
					break;
					IL_111:
					int right = num5;
					num = 8;
					continue;
					IL_1A5:
					num = 0;
					continue;
					IL_24F:
					int bottom = num3;
					int left;
					int top;
					A_1.FillToRect = Rectangle.FromLTRB(left, top, right, bottom);
					A_0.Read();
					A_0.Read();
					num = 13;
					continue;
					IL_27D:
					top = num4;
					num = 7;
					continue;
					IL_2B4:
					left = num2;
					if (true)
					{
					}
					num = 11;
				}
			}
			return;
		}
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x0013CAFC File Offset: 0x0013BAFC
	private static GradientStops ᜁ(XmlReader A_0, spr\u2306 A_1)
	{
		int a_ = 2;
		int num = 2;
		GradientStops gradientStops;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_146;
			case 1:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("強䤹", a_))
				{
					goto IL_1D4;
				}
				goto IL_132;
			}
			case 3:
				goto IL_146;
			case 4:
				num = 1;
				continue;
			case 5:
				goto IL_E9;
			case 6:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 4;
					continue;
				}
				goto IL_132;
			}
			case 7:
				goto IL_146;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("強䤹瀻䴽㐿", a_))
				{
					num = 5;
					continue;
				}
				A_0.Read();
				gradientStops = new GradientStops();
				num = 7;
				continue;
			case 9:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D4;
				default:
					goto IL_118;
				}
				break;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 12;
					continue;
				}
				num = 11;
				continue;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 0;
				continue;
			case 12:
				goto IL_170;
			case 13:
				num = 6;
				continue;
			case 14:
				goto IL_146;
			case 15:
			{
				XlsGradientStop item = spr\u1AA0.ᜀ(A_0, A_1);
				gradientStops.Add(item);
				num = 3;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 8;
			continue;
			IL_132:
			A_0.Read();
			num = 14;
			continue;
			IL_146:
			num = 10;
			continue;
			IL_1D4:
			num = 15;
		}
		IL_E9:
		throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
		IL_118:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_170:
		A_0.Read();
		return gradientStops;
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x0013CD14 File Offset: 0x0013BD14
	private static XlsGradientStop ᜀ(XmlReader A_0, spr\u2306 A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 1;
			int position;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 2:
					for (;;)
					{
						position = XmlConvert.ToInt32(A_0.Value);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_E1;
						}
					}
					IL_E1:
					if (false)
					{
					}
					num = 3;
					continue;
				case 3:
					goto IL_F3;
				case 4:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕄⡆㩈", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_130;
				case 5:
					goto IL_12B;
				case 6:
					if (A_0.LocalName != RecordTableEnumerator.b("≄㑆", a_))
					{
						num = 5;
						continue;
					}
					position = -1;
					num = 4;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 6;
				}
			}
			IL_54:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			IL_F3:
			goto IL_130;
			IL_12B:
			throw new XmlException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⍚ぜ㍞䅠ᝢѤf䝨", a_));
			IL_130:
			A_0.Read();
			int transparency;
			int tint;
			int shade;
			Color color = spr\u1AA0.ᜀ(A_0, out transparency, out tint, out shade, A_1);
			A_0.Read();
			return new XlsGradientStop(color, position, transparency, tint, shade);
		}
		}
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x0013CE84 File Offset: 0x0013BE84
	private static Color ᜀ(XmlReader A_0, out int A_1, out int A_2, out int A_3, spr\u2306 A_4)
	{
		int a_ = 9;
		int num = 6;
		for (;;)
		{
			Color result;
			string localName;
			switch (num)
			{
			case 0:
				return result;
			case 1:
				goto IL_60;
			case 2:
				num = 4;
				continue;
			case 3:
				goto IL_137;
			case 4:
				if (!(localName == RecordTableEnumerator.b("䰾㡀あل⭆㭈", a_)))
				{
					num = 7;
					continue;
				}
				result = spr\u1AA0.ᜀ(A_0, out A_1, A_4);
				num = 11;
				continue;
			case 5:
				num = 3;
				continue;
			case 7:
				num = 12;
				continue;
			case 8:
				if (!(localName == RecordTableEnumerator.b("䰾㍀⑂❄ц╈㥊", a_)))
				{
					num = 5;
					continue;
				}
				result = spr\u1AA0.ᜁ(A_0, out A_1, out A_2, out A_3, A_4);
				num = 13;
				continue;
			case 9:
				if ((localName = A_0.LocalName) != null)
				{
					num = 10;
					continue;
				}
				goto IL_10E;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_137;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 11:
				return result;
			case 12:
				goto IL_10E;
			case 13:
				return result;
			case 14:
				return result;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			result = spr\u1D39.ᜂ;
			A_1 = -1;
			A_2 = -1;
			A_3 = -1;
			num = 9;
			continue;
			IL_137:
			if (!(localName == RecordTableEnumerator.b("䰾≀⭂⁄⩆ⱈࡊ⅌㵎", a_)))
			{
				num = 2;
				continue;
			}
			result = spr\u1AA0.ᜁ(A_0, out A_1, A_4);
			num = 14;
			continue;
			IL_10E:
			A_0.Skip();
			if (true)
			{
			}
			num = 0;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x0013D070 File Offset: 0x0013C070
	private static void ᜀ(GradientStops A_0, spr\u1C26 A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 10;
			GradientColorType gradientColorType;
			bool a_2;
			for (;;)
			{
				GradientPresetType gradientPresetType;
				switch (num)
				{
				case 0:
					if (gradientPresetType >= (GradientPresetType)0)
					{
						num = 11;
						continue;
					}
					goto IL_112;
				case 1:
					goto IL_BC;
				case 2:
					goto IL_71;
				case 3:
					gradientColorType = GradientColorType.TwoColor;
					A_1.ᜁ(false);
					num = 9;
					continue;
				case 4:
					goto IL_1D4;
				case 5:
					if (gradientColorType < GradientColorType.OneColor)
					{
						num = 4;
						continue;
					}
					goto IL_112;
				case 6:
					if (gradientColorType < GradientColorType.OneColor)
					{
						num = 3;
						continue;
					}
					goto IL_C1;
				case 7:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					gradientPresetType = GradientPresetType.GradBrass;
					a_2 = false;
					gradientColorType = spr\u1AA0.ᜃ(A_0);
					A_1.ᜁ(true);
					num = 5;
					continue;
				case 8:
					goto IL_112;
				case 9:
					goto IL_C1;
				case 11:
					gradientColorType = GradientColorType.Preset;
					A_1.PresetGradient(gradientPresetType);
					num = 8;
					continue;
				case 12:
					goto IL_18A;
				case 13:
					if (gradientColorType != GradientColorType.Preset)
					{
						num = 14;
						continue;
					}
					goto IL_201;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D4;
					default:
						if (false)
						{
						}
						spr\u1AA0.ᜀ(A_1.ᜁ(), A_0[0]);
						spr\u1AA0.ᜀ(A_1.ᜀ(), A_0[A_0.Count - 1]);
						A_1.FillType = ShapeFillType.Gradient;
						A_1.GradientColorType = gradientColorType;
						num = 12;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
				IL_C1:
				num = 13;
				continue;
				IL_112:
				if (true)
				{
				}
				num = 6;
				continue;
				IL_1D4:
				gradientPresetType = spr\u1AA0.ᜀ(A_0, out a_2);
				num = 0;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("尺似帾╀⩂⁄⥆㵈ᡊ㥌⁎⅐⁒", a_));
			IL_BC:
			throw new ArgumentNullException(RecordTableEnumerator.b("挺儼䰾݀⩂⥄⭆", a_));
			IL_18A:
			IL_201:
			A_1.FillType = ShapeFillType.Gradient;
			A_1.GradientVariant = spr\u1AA0.ᜀ(A_0, A_1.GradientStyle = spr\u1AA0.ᜂ(A_0), gradientColorType, a_2);
			spr\u1AA0.ᜀ(A_0, gradientColorType, A_1);
			return;
		}
		}
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x0013D2AC File Offset: 0x0013C2AC
	internal static void ᜀ(XlsChartTextArea A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = A_0.Font.Language != null;
				bool flag2 = A_0.FontName != RecordTableEnumerator.b("笷嬹倻圽∿ぁⵃ", a_);
				bool flag3 = A_0.Size != 10.0;
				bool isBold = A_0.IsBold;
				bool isItalic = A_0.IsItalic;
				bool flag4 = A_0.Underline != FontUnderlineType.None;
				bool isSuperscript = A_0.IsSuperscript;
				bool isSubscript = A_0.IsSubscript;
				bool isStrikethrough = A_0.IsStrikethrough;
				bool flag5 = A_0.Font.HasLatin;
				bool flag6 = !A_0.IsAutoColor;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						if (!flag)
						{
							num = 4;
							continue;
						}
						goto IL_21E;
					case 2:
						if (!flag3)
						{
							num = 10;
							continue;
						}
						goto IL_21E;
					case 3:
						if (!isSubscript)
						{
							num = 12;
							continue;
						}
						goto IL_21E;
					case 4:
						num = 14;
						continue;
					case 5:
						if (!isItalic)
						{
							num = 18;
							continue;
						}
						goto IL_21E;
					case 6:
						if (!isStrikethrough)
						{
							num = 19;
							continue;
						}
						goto IL_21E;
					case 7:
						num = 8;
						continue;
					case 8:
						if (!isSuperscript)
						{
							num = 0;
							continue;
						}
						goto IL_21E;
					case 9:
						if (true)
						{
						}
						goto IL_21E;
					case 10:
						num = 20;
						continue;
					case 11:
						return;
					case 12:
						num = 6;
						continue;
					case 13:
						num = 2;
						continue;
					case 14:
						if (!flag2)
						{
							num = 13;
							continue;
						}
						goto IL_21E;
					case 15:
						if (!flag5)
						{
							num = 21;
							continue;
						}
						goto IL_21E;
					case 16:
						num = 5;
						continue;
					case 17:
						if (!flag4)
						{
							num = 7;
							continue;
						}
						goto IL_21E;
					case 18:
						num = 17;
						continue;
					case 19:
						goto IL_1FD;
					case 20:
						if (!isBold)
						{
							num = 16;
							continue;
						}
						goto IL_21E;
					case 21:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1FD;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 22:
						if (flag6)
						{
							num = 9;
							continue;
						}
						return;
					}
					break;
					IL_1FD:
					num = 15;
					continue;
					IL_21E:
					((sprᮟ)A_0).ᜀ(ChartParagraphType.Default);
					num = 11;
				}
			}
			return;
		}
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x0013D57C File Offset: 0x0013C57C
	private static void ᜀ(OColor A_0, XlsGradientStop A_1)
	{
		OColor ocolor;
		for (;;)
		{
			IL_14:
			ocolor = A_1.OColor;
			int tint = A_1.Tint;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (tint >= 0)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14;
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					goto IL_93;
				case 1:
					goto IL_91;
				case 2:
				{
					double a_ = (double)tint / 100000.0;
					ocolor = spr\u2306.ᜀ(ocolor.ᜁ(null), a_);
					num = 1;
					continue;
				}
				}
				break;
			}
		}
		IL_91:
		IL_93:
		A_0.ᜀ(ocolor, true);
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x0013D624 File Offset: 0x0013C624
	internal static void ᜀ(XmlReader A_0, spr\u1772 A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 64;
			for (;;)
			{
				string a_2;
				spr\u2306 spr_u;
				string a_5;
				string a_6;
				switch (num)
				{
				case 0:
					goto IL_A87;
				case 1:
					goto IL_278;
				case 2:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("吵崷䰹夻刽ȿ", a_)))
					{
						num = 40;
						continue;
					}
					num = 39;
					continue;
				}
				case 3:
					goto IL_A87;
				case 4:
					goto IL_A87;
				case 5:
					if (!A_0.IsEmptyElement)
					{
						num = 24;
						continue;
					}
					goto IL_82A;
				case 6:
					goto IL_A24;
				case 7:
					goto IL_2DD;
				case 8:
					a_2 = A_0.Value;
					num = 72;
					continue;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䘵䨷䤹䠻猽ℿ㙁⅃㑅ⅇ⭉⁋", a_)))
					{
						num = 47;
						continue;
					}
					A_1.ᜅ().MaterialType = XLSXChartMaterialType.None;
					num = 63;
					continue;
				case 10:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 61;
						continue;
					}
					A_0.Skip();
					num = 78;
					continue;
				case 11:
					goto IL_2DD;
				case 12:
				{
					string localName2;
					int num2;
					if (spr\u22D2.ᜐ.TryGetValue(localName2, out num2))
					{
						num = 65;
						continue;
					}
					goto IL_994;
				}
				case 13:
					spr\u22D2.ᜐ = new Dictionary<string, int>(9)
					{
						{
							RecordTableEnumerator.b("娵嘷", a_),
							0
						},
						{
							RecordTableEnumerator.b("䔵圷嘹唻娽ؿ⭁⡃⩅", a_),
							1
						},
						{
							RecordTableEnumerator.b("䘵夷丹䠻砽⤿⹁⡃", a_),
							2
						},
						{
							RecordTableEnumerator.b("儵䨷嬹堻砽⤿⹁⡃", a_),
							3
						},
						{
							RecordTableEnumerator.b("吵吷匹䰻砽⤿⹁⡃", a_),
							4
						},
						{
							RecordTableEnumerator.b("堵圷簹唻刽ⰿ", a_),
							5
						},
						{
							RecordTableEnumerator.b("匵帷尹夻崽㐿แ㝃㉅", a_),
							6
						},
						{
							RecordTableEnumerator.b("䔵嬷弹刻嬽猿♁", a_),
							7
						},
						{
							RecordTableEnumerator.b("䔵䠷ह堻", a_),
							8
						}
					};
					num = 70;
					continue;
				case 14:
					num = 20;
					continue;
				case 15:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䄵", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_2A7;
				case 16:
					goto IL_A87;
				case 17:
					if (!A_0.IsEmptyElement)
					{
						num = 28;
						continue;
					}
					goto IL_B2A;
				case 18:
					goto IL_A87;
				case 19:
					goto IL_994;
				case 20:
					if (spr\u22D2.ᜐ == null)
					{
						num = 13;
						continue;
					}
					goto IL_6B2;
				case 21:
				{
					XlsChartInterior xlsChartInterior;
					if (xlsChartInterior != null)
					{
						num = 41;
						continue;
					}
					goto IL_278;
				}
				case 22:
					num = 2;
					continue;
				case 23:
					goto IL_2DD;
				case 24:
					A_0.Read();
					num = 23;
					continue;
				case 25:
				{
					int num2;
					switch (num2)
					{
					case 0:
						spr\u1AA0.ᜀ(A_0, A_1.ᜁ(), spr_u);
						num = 0;
						continue;
					case 1:
					{
						int num3;
						spr\u1AA0.ᜀ(A_0, A_1.ᜂ(), spr_u, out num3);
						A_1.ᜃ().Transparency = (double)(1f - (float)num3 / 100000f);
						num = 44;
						continue;
					}
					case 2:
						spr\u1AA0.ᜀ(A_0, A_1.ᜃ(), spr_u);
						num = 73;
						continue;
					case 3:
					{
						GradientStops a_3 = spr\u1AA0.ᜂ(A_0, spr_u);
						spr\u1AA0.ᜀ(a_3, A_1.ᜃ());
						A_1.ᜃ().ᜀ(a_3);
						A_1.ᜂ().UseDefaultFormat = false;
						num = 46;
						continue;
					}
					case 4:
						spr\u1AA0.ᜀ(A_0, A_1.ᜃ(), A_3, A_2);
						num = 18;
						continue;
					case 5:
						num = 53;
						continue;
					case 6:
						spr\u1AA0.ᜀ(A_0, A_1.ᜄ(), A_3, A_2, spr_u);
						num = 68;
						continue;
					case 7:
						spr\u1AA0.ᜀ(A_0, A_1.ᜅ(), A_3, A_2);
						num = 16;
						continue;
					case 8:
					{
						string a_4 = RecordTableEnumerator.b("堵䴷嘹倻", a_);
						num = 9;
						continue;
					}
					default:
						num = 37;
						continue;
					}
					break;
				}
				case 26:
					goto IL_182;
				case 27:
					if (A_1.ᜀ())
					{
						num = 79;
						continue;
					}
					goto IL_A87;
				case 28:
					A_0.Read();
					num = 27;
					continue;
				case 29:
					a_2 = A_0.Value;
					num = 62;
					continue;
				case 30:
					goto IL_AAC;
				case 31:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 42;
						continue;
					}
					A_0.Skip();
					num = 56;
					continue;
				case 32:
					a_5 = A_0.Value;
					num = 75;
					continue;
				case 33:
					if (A_0.LocalName != RecordTableEnumerator.b("䔵䠷樹主", a_))
					{
						num = 76;
						continue;
					}
					num = 34;
					continue;
				case 34:
				{
					if (A_1 == null)
					{
						num = 71;
						continue;
					}
					XlsChartInterior xlsChartInterior = A_1.ᜂ();
					int num3 = 100000;
					num = 21;
					continue;
				}
				case 35:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("帵", a_)))
					{
						num = 69;
						continue;
					}
					goto IL_87F;
				case 36:
					goto IL_A87;
				case 37:
					num = 19;
					continue;
				case 38:
					a_5 = A_0.Value;
					num = 74;
					continue;
				case 39:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䄵", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_64D;
				case 40:
					goto IL_A53;
				case 41:
				{
					XlsChartInterior xlsChartInterior;
					xlsChartInterior.Pattern = ExcelPatternType.None;
					xlsChartInterior.UseDefaultFormat = true;
					num = 1;
					continue;
				}
				case 42:
					a_2 = RecordTableEnumerator.b("堵䴷嘹倻", a_);
					a_6 = RecordTableEnumerator.b("堵䴷嘹倻", a_);
					a_5 = RecordTableEnumerator.b("堵䴷嘹倻", a_);
					num = 49;
					continue;
				case 43:
					goto IL_2DD;
				case 44:
					goto IL_A87;
				case 45:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A53;
					default:
						if (false)
						{
						}
						num = 50;
						continue;
					}
					break;
				case 46:
					goto IL_A87;
				case 47:
				{
					string a_4 = A_0.Value;
					A_1.ᜅ().MaterialType = spr\u1AA0.ᜀ(a_4, A_0);
					num = 6;
					continue;
				}
				case 48:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 14;
						continue;
					}
					goto IL_994;
				}
				case 49:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 45;
						continue;
					}
					goto IL_51F;
				}
				case 50:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("吵崷䰹夻刽ᐿ", a_)))
					{
						num = 22;
						continue;
					}
					num = 15;
					continue;
				}
				case 51:
					a_6 = A_0.Value;
					num = 59;
					continue;
				case 52:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 30;
						continue;
					}
					num = 10;
					continue;
				case 53:
					if (A_1.ᜃ() != null)
					{
						num = 58;
						continue;
					}
					goto IL_A87;
				case 54:
					goto IL_82A;
				case 55:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䘵䨷䤹䠻", a_)))
					{
						num = 38;
						continue;
					}
					goto IL_9AB;
				case 56:
					goto IL_2DD;
				case 57:
					goto IL_87F;
				case 58:
					A_1.ᜃ().FillType = ShapeFillType.Pattern;
					A_1.ᜃ().Pattern = (GradientPatternType)0;
					A_1.ᜂ().Pattern = ExcelPatternType.None;
					A_0.Skip();
					if (true)
					{
					}
					num = 36;
					continue;
				case 59:
					goto IL_187;
				case 60:
					goto IL_A87;
				case 61:
					num = 48;
					continue;
				case 62:
					goto IL_2A7;
				case 63:
					goto IL_A24;
				case 65:
					num = 25;
					continue;
				case 66:
					goto IL_51F;
				case 67:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 54;
						continue;
					}
					num = 31;
					continue;
				case 68:
					goto IL_A87;
				case 69:
					a_6 = A_0.Value;
					num = 57;
					continue;
				case 70:
					goto IL_6B2;
				case 71:
					goto IL_A82;
				case 72:
					goto IL_64D;
				case 73:
					goto IL_A87;
				case 74:
					goto IL_9AB;
				case 75:
					goto IL_40C;
				case 76:
					goto IL_732;
				case 77:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("帵", a_)))
					{
						num = 51;
						continue;
					}
					goto IL_187;
				case 78:
					goto IL_A87;
				case 79:
					A_1.ᜁ().UseDefaultFormat = true;
					num = 4;
					continue;
				case 80:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䘵䨷䤹䠻", a_)))
					{
						num = 32;
						continue;
					}
					goto IL_40C;
				}
				if (A_0 == null)
				{
					num = 26;
					continue;
				}
				num = 33;
				continue;
				IL_187:
				num = 55;
				continue;
				IL_278:
				spr_u = A_2.\u1718();
				num = 17;
				continue;
				IL_2A7:
				num = 77;
				continue;
				IL_2DD:
				num = 67;
				continue;
				IL_40C:
				A_1.ᜅ().BevelBottomType = spr\u1AA0.ᜀ(a_2, a_6, a_5, A_0);
				num = 11;
				continue;
				IL_51F:
				A_0.Skip();
				num = 43;
				continue;
				IL_64D:
				num = 35;
				continue;
				IL_6B2:
				num = 12;
				continue;
				IL_82A:
				A_0.Read();
				num = 60;
				continue;
				IL_87F:
				num = 80;
				continue;
				IL_994:
				A_0.Skip();
				num = 3;
				continue;
				IL_9AB:
				A_1.ᜅ().BevelTopType = spr\u1AA0.ᜀ(a_2, a_6, a_5, A_0);
				num = 7;
				continue;
				IL_A24:
				A_0.MoveToElement();
				num = 5;
				continue;
				IL_A53:
				num = 66;
				continue;
				IL_A87:
				num = 52;
			}
			IL_182:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
			IL_732:
			throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
			IL_A82:
			throw new ArgumentNullException(RecordTableEnumerator.b("夵娷倹夻崽㐿Ձ⅃㉅㱇⽉㹋", a_));
			IL_AAC:
			IL_B2A:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x0013E164 File Offset: 0x0013D164
	private static void ᜀ(XmlReader A_0, Format3D A_1, RelationsCollection A_2, sprវ A_3)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_18D;
			case 1:
				goto IL_1AD;
			case 2:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("娵儷崹吻䨽ሿ⭁⍃", a_))
				{
					num = 8;
					continue;
				}
				goto IL_1DA;
			}
			case 3:
				num = 13;
				continue;
			case 4:
				goto IL_15A;
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 14;
				continue;
			case 7:
				A_0.Read();
				num = 9;
				continue;
			case 8:
			{
				A_0.MoveToAttribute(RecordTableEnumerator.b("䐵儷崹", a_));
				string value = A_0.Value;
				A_1.LightingType = spr\u1AA0.ᜀ(value);
				num = 15;
				continue;
			}
			case 9:
				goto IL_18D;
			case 10:
				if (!A_0.IsEmptyElement)
				{
					num = 7;
					continue;
				}
				goto IL_237;
			case 11:
				goto IL_18D;
			case 12:
				if (A_0.LocalName != RecordTableEnumerator.b("䔵嬷弹刻嬽猿♁", a_))
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
			case 13:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FD;
					}
					if (false)
					{
					}
					num = 16;
					continue;
				}
				goto IL_1DA;
			}
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				A_0.Skip();
				num = 11;
				continue;
			case 15:
				if (true)
				{
				}
				goto IL_18D;
			case 16:
				goto IL_FD;
			case 17:
				goto IL_6F;
			}
			if (A_0 == null)
			{
				num = 17;
				continue;
			}
			num = 12;
			continue;
			IL_FD:
			num = 2;
			continue;
			IL_18D:
			num = 6;
			continue;
			IL_1DA:
			A_0.Skip();
			num = 0;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_15A:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
		IL_1AD:
		IL_237:
		A_0.Read();
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x0013E3B0 File Offset: 0x0013D3B0
	internal static XLSXChartLightingType ᜀ(string A_0)
	{
		XLSXChartLightingType result;
		for (;;)
		{
			IL_24:
			result = XLSXChartLightingType.ThreePoint;
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_56:
				goto IL_A0;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					if (num >= spr\u1CFF.ᜆ.GetLength(0))
					{
						num2 = 2;
						continue;
					}
					num2 = 3;
					continue;
				case 1:
					goto IL_56;
				case 2:
					return result;
				case 3:
					if (A_0.Equals(spr\u1CFF.ᜆ[num][0]))
					{
						num2 = 6;
						continue;
					}
					if (true)
					{
					}
					num++;
					num2 = 5;
					continue;
				case 4:
					return result;
				case 5:
					goto IL_6C;
				case 6:
					result = (XLSXChartLightingType)num;
					num2 = 4;
					continue;
				}
				goto IL_24;
			}
			IL_6C:
			IL_A0:
			num2 = 0;
			goto IL_02;
		}
		return result;
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x0013E484 File Offset: 0x0013D484
	internal static XLSXChartBevelType ᜀ(string A_0, string A_1, string A_2, XmlReader A_3)
	{
		int a_ = 14;
		XLSXChartBevelType result;
		for (;;)
		{
			result = XLSXChartBevelType.None;
			int num = 3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_188;
				case 1:
					result = XLSXChartBevelType.Circle;
					A_3.Skip();
					num = 7;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					if (A_0.Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_1AE;
				case 4:
					if (A_2.Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_1AE;
				case 5:
					goto IL_188;
				case 6:
					return result;
				case 7:
					return result;
				case 8:
					if (A_1.Equals(spr\u1CFF.ᜄ[num2][1]))
					{
						num = 15;
						continue;
					}
					goto IL_FB;
				case 9:
					if (!A_1.Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
					{
						goto IL_1AE;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_167;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 10:
					if (A_0.Equals(spr\u1CFF.ᜄ[num2][0]))
					{
						num = 13;
						continue;
					}
					goto IL_FB;
				case 11:
					num = 9;
					continue;
				case 12:
					goto IL_167;
				case 13:
					num = 8;
					continue;
				case 14:
					return result;
				case 15:
					num = 12;
					continue;
				case 16:
					if (num2 >= spr\u1CFF.ᜄ.GetLength(0))
					{
						num = 6;
						continue;
					}
					num = 10;
					continue;
				case 17:
					result = (XLSXChartBevelType)num2;
					num = 14;
					continue;
				}
				break;
				IL_FB:
				num2++;
				num = 5;
				continue;
				IL_167:
				if (A_2.Equals(spr\u1CFF.ᜄ[num2][2]))
				{
					num = 17;
					continue;
				}
				goto IL_FB;
				IL_188:
				num = 16;
				continue;
				IL_1AE:
				if (true)
				{
				}
				num2 = 0;
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x0013E6A8 File Offset: 0x0013D6A8
	internal static XLSXChartMaterialType ᜀ(string A_0, XmlReader A_1)
	{
		XLSXChartMaterialType result;
		for (;;)
		{
			IL_24:
			result = XLSXChartMaterialType.None;
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_56:
				goto IL_9C;
			default:
				if (false)
				{
				}
				num2 = 4;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return result;
				case 1:
					return result;
				case 2:
					if (A_0.Equals(spr\u1CFF.ᜅ[num][0]))
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 6;
					continue;
				case 3:
				{
					int num3 = num + 1;
					result = (XLSXChartMaterialType)num3;
					num2 = 0;
					continue;
				}
				case 4:
					goto IL_56;
				case 5:
					if (num >= spr\u1CFF.ᜅ.GetLength(0))
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					num2 = 2;
					continue;
				case 6:
					goto IL_64;
				}
				goto IL_24;
			}
			IL_64:
			IL_9C:
			num2 = 5;
			goto IL_02;
		}
		return result;
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x0013E780 File Offset: 0x0013D780
	private static void ᜀ(XmlReader A_0, ChartShadow A_1, RelationsCollection A_2, sprវ A_3, spr\u2306 A_4)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				string text6;
				string text7;
				string a_2;
				switch (num)
				{
				case 0:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬸场䠼䴾ፀ≂⅄", a_)))
					{
						num = 57;
						continue;
					}
					goto IL_53E;
				case 1:
					if (text.Equals(RecordTableEnumerator.b("圸为儼匾", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_3B6;
				case 2:
					goto IL_72D;
				case 3:
					text2 = A_0.Value;
					num = 5;
					continue;
				case 4:
					goto IL_8E6;
				case 5:
					goto IL_63E;
				case 6:
					if (!text3.Equals(RecordTableEnumerator.b("圸为儼匾", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_87B;
				case 7:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 40;
						continue;
					}
					A_0.Skip();
					num = 14;
					continue;
				case 8:
					num = 28;
					continue;
				case 9:
					goto IL_226;
				case 10:
					goto IL_8E6;
				case 11:
					text4 = A_0.Value;
					num = 39;
					continue;
				case 12:
					text5 = A_0.Value;
					num = 69;
					continue;
				case 13:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("崸刺似", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_6B2;
				case 14:
					goto IL_8E6;
				case 16:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("崸刺似", a_)))
					{
						num = 44;
						continue;
					}
					goto IL_250;
				case 17:
					num = 6;
					continue;
				case 18:
					goto IL_162;
				case 19:
					goto IL_639;
				case 20:
					text6 = A_0.Value;
					num = 62;
					continue;
				case 21:
					if (text == RecordTableEnumerator.b("圸为儼匾", a_))
					{
						num = 34;
						continue;
					}
					goto IL_8B2;
				case 22:
					text7 = A_0.Value;
					num = 31;
					continue;
				case 23:
					num = 35;
					continue;
				case 24:
					goto IL_250;
				case 25:
					goto IL_8E6;
				case 26:
					if (A_0.LocalName != RecordTableEnumerator.b("尸崺嬼娾≀㝂ॄ㑆㵈", a_))
					{
						num = 19;
						continue;
					}
					num = 59;
					continue;
				case 27:
					if (true)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨸䈺", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_571;
				case 28:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("倸唺匼娾㍀၂ⵄ⍆㹈", a_)))
					{
						num = 72;
						continue;
					}
					num = 32;
					continue;
				}
				case 29:
					num = 61;
					continue;
				case 30:
					a_2 = A_0.Value;
					num = 67;
					continue;
				case 31:
					goto IL_19D;
				case 32:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬸场䠼䴾ፀ≂⅄", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_804;
				case 33:
					goto IL_68A;
				case 34:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F2;
					default:
						if (false)
						{
						}
						num = 51;
						continue;
					}
					break;
				case 35:
					if (text6.Equals(RecordTableEnumerator.b("༸࠺࠼༾煀", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_87B;
				case 36:
					if (text == RecordTableEnumerator.b("圸为儼匾", a_))
					{
						num = 23;
						continue;
					}
					goto IL_87B;
				case 37:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 29;
						continue;
					}
					goto IL_72D;
				}
				case 38:
					goto IL_8E6;
				case 39:
					goto IL_2A5;
				case 40:
					text6 = RecordTableEnumerator.b("圸为儼匾", a_);
					text3 = RecordTableEnumerator.b("圸为儼匾", a_);
					a_2 = RecordTableEnumerator.b("圸为儼匾", a_);
					text4 = RecordTableEnumerator.b("圸为儼匾", a_);
					text5 = RecordTableEnumerator.b("圸为儼匾", a_);
					text2 = RecordTableEnumerator.b("圸为儼匾", a_);
					text7 = RecordTableEnumerator.b("圸为儼匾", a_);
					text = RecordTableEnumerator.b("圸为儼匾", a_);
					num = 37;
					continue;
				case 41:
					text = A_0.Value;
					num = 52;
					continue;
				case 42:
					goto IL_53E;
				case 43:
					goto IL_8E6;
				case 44:
					text5 = A_0.Value;
					num = 24;
					continue;
				case 45:
					if (text3.Equals(RecordTableEnumerator.b("8଺഼༾煀", a_)))
					{
						num = 71;
						continue;
					}
					goto IL_3B6;
				case 46:
					num = 45;
					continue;
				case 47:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("堸场娼儾", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_63E;
				case 48:
					goto IL_413;
				case 49:
					goto IL_8B2;
				case 50:
					goto IL_503;
				case 51:
					if (!text6.Equals(RecordTableEnumerator.b("స଺Լ༾煀", a_)))
					{
						num = 49;
						continue;
					}
					goto IL_226;
				case 52:
					goto IL_3E0;
				case 53:
					goto IL_8F2;
				case 54:
					text3 = A_0.Value;
					num = 50;
					continue;
				case 55:
					text4 = A_0.Value;
					num = 48;
					continue;
				case 56:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬸吺䤼栾⡀㝂ⵄᑆⅈ⩊㵌⩎", a_)))
					{
						num = 22;
						continue;
					}
					goto IL_19D;
				case 57:
					text6 = A_0.Value;
					num = 42;
					continue;
				case 58:
					goto IL_8E6;
				case 59:
					if (!A_0.IsEmptyElement)
					{
						num = 68;
						continue;
					}
					goto IL_97A;
				case 60:
					if (text.Equals(RecordTableEnumerator.b("圸为儼匾", a_)))
					{
						num = 46;
						continue;
					}
					goto IL_68A;
				case 61:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("嘸为䤼娾㍀၂ⵄ⍆㹈", a_)))
					{
						num = 8;
						continue;
					}
					num = 0;
					continue;
				}
				case 62:
					goto IL_804;
				case 63:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨸䌺", a_)))
					{
						num = 54;
						continue;
					}
					goto IL_503;
				case 64:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("券䌺", a_)))
					{
						num = 41;
						continue;
					}
					goto IL_3E0;
				case 65:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("崸刺丼䬾", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_2A5;
				case 66:
					goto IL_90B;
				case 67:
					goto IL_571;
				case 68:
					A_0.Read();
					num = 43;
					continue;
				case 69:
					goto IL_6B2;
				case 70:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("崸刺丼䬾", a_)))
					{
						num = 55;
						continue;
					}
					goto IL_413;
				case 71:
					num = 1;
					continue;
				case 72:
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 18;
					continue;
				}
				num = 26;
				continue;
				IL_19D:
				num = 21;
				continue;
				IL_8F2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 66;
					continue;
				}
				num = 7;
				continue;
				IL_226:
				A_1.ShadowOuterType = spr\u1AA0.ᜀ(text6, text3, a_2, text4, text5, text2, text7, A_1, A_0, A_4);
				num = 38;
				continue;
				IL_250:
				A_1.ShadowInnerType = spr\u1AA0.ᜀ(text6, text4, text5, A_0, A_1, A_4);
				num = 10;
				continue;
				IL_2A5:
				num = 16;
				continue;
				IL_3B6:
				A_1.ShadowOuterType = spr\u1AA0.ᜀ(text6, text3, a_2, text4, text5, text2, text7, A_1, A_0, A_4);
				num = 58;
				continue;
				IL_3E0:
				num = 70;
				continue;
				IL_413:
				num = 13;
				continue;
				IL_503:
				num = 27;
				continue;
				IL_53E:
				num = 63;
				continue;
				IL_571:
				num = 64;
				continue;
				IL_63E:
				num = 56;
				continue;
				IL_68A:
				A_1.ShadowPrespectiveType = spr\u1AA0.ᜀ(text6, text3, a_2, text, text4, text5, text2, text7);
				num = 4;
				continue;
				IL_6B2:
				num = 47;
				continue;
				IL_72D:
				A_0.Skip();
				num = 25;
				continue;
				IL_804:
				num = 65;
				continue;
				IL_87B:
				num = 60;
				continue;
				IL_8B2:
				num = 36;
				continue;
				IL_8E6:
				num = 53;
			}
			IL_162:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_639:
			throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
			IL_90B:
			IL_97A:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x0013F110 File Offset: 0x0013E110
	internal static XLSXChartShadowOuterType ᜀ(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5, string A_6, ChartShadow A_7, XmlReader A_8, spr\u2306 A_9)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			XLSXChartShadowOuterType result;
			for (;;)
			{
				int num = 0;
				result = XLSXChartShadowOuterType.None;
				int num2 = 18;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						if (A_5.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 2;
							continue;
						}
						goto IL_2F0;
					case 1:
						if (num == spr\u1CFF.ᜁ.GetLength(0))
						{
							num2 = 10;
							continue;
						}
						return result;
					case 2:
						result = XLSXChartShadowOuterType.None;
						num2 = 21;
						continue;
					case 3:
						num2 = 11;
						continue;
					case 4:
						if (A_3.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 8;
							continue;
						}
						goto IL_2F0;
					case 5:
						if (A_2.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 23;
							continue;
						}
						goto IL_2F0;
					case 6:
						if (A_4.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 13;
							continue;
						}
						goto IL_2F0;
					case 7:
					{
						int num3 = num4 + 1;
						result = (XLSXChartShadowOuterType)num3;
						num2 = 12;
						continue;
					}
					case 8:
						num2 = 6;
						continue;
					case 9:
						goto IL_198;
					case 10:
						result = spr\u1AA0.ᜀ(A_0, A_1, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
						num2 = 29;
						continue;
					case 11:
						if (A_1.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 22;
							continue;
						}
						goto IL_2F0;
					case 12:
						goto IL_1FF;
					case 13:
						num2 = 0;
						continue;
					case 14:
						if (A_6.Equals(spr\u1CFF.ᜁ[num4][6]))
						{
							num2 = 7;
							continue;
						}
						goto IL_188;
					case 15:
						goto IL_29C;
					case 16:
						num2 = 14;
						continue;
					case 17:
						if (A_1.Equals(spr\u1CFF.ᜁ[num4][1]))
						{
							num2 = 28;
							continue;
						}
						goto IL_188;
					case 18:
						if (A_0.Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num2 = 3;
							continue;
						}
						goto IL_2F0;
					case 19:
						if (A_5.Equals(spr\u1CFF.ᜁ[num4][5]))
						{
							num2 = 16;
							continue;
						}
						goto IL_188;
					case 20:
						if (A_2.Equals(spr\u1CFF.ᜁ[num4][2]))
						{
							num2 = 32;
							continue;
						}
						goto IL_188;
					case 21:
						return result;
					case 22:
						num2 = 5;
						continue;
					case 23:
						num2 = 4;
						continue;
					case 24:
						if (A_0.Equals(spr\u1CFF.ᜁ[num4][0]))
						{
							num2 = 31;
							continue;
						}
						goto IL_188;
					case 25:
						num2 = 19;
						continue;
					case 26:
						goto IL_1FF;
					case 27:
						if (A_3.Equals(spr\u1CFF.ᜁ[num4][3]))
						{
							if (true)
							{
							}
							num2 = 34;
							continue;
						}
						goto IL_188;
					case 28:
						num2 = 20;
						continue;
					case 29:
						return result;
					case 30:
						if (num4 >= spr\u1CFF.ᜁ.GetLength(0))
						{
							num2 = 26;
							continue;
						}
						num++;
						num2 = 24;
						continue;
					case 31:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_198;
						default:
							if (false)
							{
							}
							num2 = 17;
							continue;
						}
						break;
					case 32:
						num2 = 27;
						continue;
					case 33:
						if (A_4.Equals(spr\u1CFF.ᜁ[num4][4]))
						{
							num2 = 25;
							continue;
						}
						goto IL_188;
					case 34:
						num2 = 33;
						continue;
					}
					break;
					IL_188:
					num4++;
					num2 = 9;
					continue;
					IL_1FF:
					num2 = 1;
					continue;
					IL_29C:
					num2 = 30;
					continue;
					IL_198:
					goto IL_29C;
					IL_2F0:
					num4 = 0;
					num2 = 15;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x0013F560 File Offset: 0x0013E560
	internal static XLSXChartShadowOuterType ᜀ(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5, ChartShadow A_6, XmlReader A_7, spr\u2306 A_8)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			XLSXChartShadowOuterType result;
			for (;;)
			{
				result = XLSXChartShadowOuterType.None;
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 52;
						continue;
					case 1:
						goto IL_413;
					case 2:
					{
						string localName;
						if (localName == RecordTableEnumerator.b("⍁⡃㙅⁇⭉", a_))
						{
							num2 = 0;
							continue;
						}
						goto IL_65E;
					}
					case 3:
						A_6.Angle = ((A_3 != RecordTableEnumerator.b("ⱁㅃ⩅⑇", a_)) ? (Convert.ToInt32(A_3) / 60000) : 0);
						A_7.Read();
						num2 = 40;
						continue;
					case 4:
						goto IL_183;
					case 5:
						if (A_7.MoveToAttribute(RecordTableEnumerator.b("㑁╃⩅", a_)))
						{
							num2 = 35;
							continue;
						}
						goto IL_413;
					case 6:
						goto IL_688;
					case 7:
						goto IL_618;
					case 8:
						if (!A_7.IsEmptyElement)
						{
							num2 = 10;
							continue;
						}
						goto IL_6B0;
					case 9:
					{
						string localName2;
						if (!(localName2 == RecordTableEnumerator.b("㉁㙃㕅㱇ॉ⁋㱍", a_)))
						{
							num2 = 53;
							continue;
						}
						num2 = 5;
						continue;
					}
					case 10:
						A_7.Read();
						num2 = 54;
						continue;
					case 11:
						goto IL_183;
					case 12:
					{
						string localName2;
						if ((localName2 = A_7.LocalName) != null)
						{
							num2 = 31;
							continue;
						}
						goto IL_6C6;
					}
					case 13:
						if (num >= spr\u1CFF.ᜁ.GetLength(0))
						{
							num2 = 51;
							continue;
						}
						num2 = 22;
						continue;
					case 14:
						A_6.Size = ((A_1 != RecordTableEnumerator.b("ⱁㅃ⩅⑇", a_)) ? (Convert.ToInt32(A_1) / 1000) : 100);
						num2 = 28;
						continue;
					case 15:
						num2 = 48;
						continue;
					case 16:
						num2 = 2;
						continue;
					case 17:
					{
						string value = A_7.Value;
						A_6.Color = A_8.ᜎ(value);
						num2 = 21;
						continue;
					}
					case 18:
						num2 = 30;
						continue;
					case 19:
						goto IL_716;
					case 20:
						if (A_7.NodeType == XmlNodeType.EndElement)
						{
							num2 = 34;
							continue;
						}
						num2 = 32;
						continue;
					case 21:
						goto IL_48D;
					case 22:
						if (A_4.Equals(spr\u1CFF.ᜁ[num][5]))
						{
							num2 = 39;
							continue;
						}
						num++;
						num2 = 11;
						continue;
					case 23:
					{
						string localName2;
						if (!(localName2 == RecordTableEnumerator.b("ㅁ❃⹅ⵇ❉⥋്㱏⁑", a_)))
						{
							num2 = 15;
							continue;
						}
						num2 = 49;
						continue;
					}
					case 24:
						if (A_7.MoveToAttribute(RecordTableEnumerator.b("㑁╃⩅", a_)))
						{
							num2 = 29;
							continue;
						}
						goto IL_51E;
					case 25:
						A_6.Blur = ((A_0 != RecordTableEnumerator.b("ⱁㅃ⩅⑇", a_)) ? (Convert.ToInt32(A_0) / 12700) : 0);
						num2 = 14;
						continue;
					case 26:
						goto IL_6F0;
					case 27:
						goto IL_6F0;
					case 28:
						A_6.Distance = ((A_2 != RecordTableEnumerator.b("ⱁㅃ⩅⑇", a_)) ? (Convert.ToInt32(A_2) / 12700) : 0);
						num2 = 3;
						continue;
					case 29:
					{
						string value2 = A_7.Value;
						int a_2 = int.Parse(value2, NumberStyles.HexNumber, null);
						A_6.Color = spr\u1D39.ᜀ(a_2);
						num2 = 43;
						continue;
					}
					case 30:
						goto IL_6C6;
					case 31:
						num2 = 9;
						continue;
					case 32:
						if (A_7.NodeType == XmlNodeType.Element)
						{
							num2 = 42;
							continue;
						}
						A_7.Skip();
						num2 = 47;
						continue;
					case 33:
						goto IL_6F0;
					case 34:
						goto IL_6B0;
					case 35:
					{
						string value3 = A_7.Value;
						A_6.Color = A_8.ᜎ(value3);
						num2 = 1;
						continue;
					}
					case 36:
						if (A_7.NodeType == XmlNodeType.EndElement)
						{
							num2 = 19;
							continue;
						}
						num2 = 55;
						continue;
					case 37:
					{
						string localName;
						if ((localName = A_7.LocalName) != null)
						{
							num2 = 16;
							continue;
						}
						goto IL_65E;
					}
					case 38:
						goto IL_688;
					case 39:
					{
						int num3 = num + 1;
						result = (XLSXChartShadowOuterType)num3;
						num2 = 45;
						continue;
					}
					case 40:
						goto IL_6F0;
					case 41:
						num2 = 12;
						continue;
					case 42:
						num2 = 37;
						continue;
					case 43:
						goto IL_51E;
					case 44:
						goto IL_6F0;
					case 45:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_618;
						default:
							if (false)
							{
							}
							goto IL_1AE;
						}
						break;
					case 46:
						goto IL_6F0;
					case 47:
						goto IL_688;
					case 48:
					{
						string localName2;
						if (!(localName2 == RecordTableEnumerator.b("ㅁ㙃ⅅ⩇ॉ⁋㱍", a_)))
						{
							num2 = 18;
							continue;
						}
						num2 = 24;
						continue;
					}
					case 49:
						if (A_7.MoveToAttribute(RecordTableEnumerator.b("㑁╃⩅", a_)))
						{
							num2 = 17;
							continue;
						}
						goto IL_48D;
					case 50:
						goto IL_688;
					case 51:
						goto IL_1AE;
					case 52:
						if (A_7.MoveToAttribute(RecordTableEnumerator.b("㑁╃⩅", a_)))
						{
							num2 = 7;
							continue;
						}
						A_6.Transparency = 100000;
						num2 = 38;
						continue;
					case 53:
						num2 = 23;
						continue;
					case 54:
						goto IL_688;
					case 55:
						if (A_7.NodeType == XmlNodeType.Element)
						{
							num2 = 41;
							continue;
						}
						A_7.Skip();
						num2 = 33;
						continue;
					}
					break;
					IL_183:
					num2 = 13;
					continue;
					IL_1AE:
					A_6.HasCustomStyle = true;
					num2 = 25;
					continue;
					IL_413:
					A_7.MoveToElement();
					spr\u1AA0.ᜀ(A_7, A_6);
					num2 = 46;
					continue;
					IL_48D:
					A_7.MoveToElement();
					spr\u1AA0.ᜀ(A_7, A_6);
					num2 = 27;
					continue;
					IL_51E:
					A_7.MoveToElement();
					num2 = 8;
					continue;
					IL_618:
					A_6.Transparency = 100 - Convert.ToInt32(A_7.Value) / 1000;
					num2 = 6;
					continue;
					IL_65E:
					A_7.Skip();
					num2 = 50;
					continue;
					IL_688:
					num2 = 20;
					continue;
					IL_6B0:
					A_7.Read();
					num2 = 26;
					continue;
					IL_6C6:
					A_7.Skip();
					num2 = 44;
					continue;
					IL_6F0:
					num2 = 36;
				}
			}
			IL_716:
			if (true)
			{
			}
			A_7.Read();
			return result;
		}
		}
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x0013FC98 File Offset: 0x0013EC98
	internal static void ᜀ(XmlReader A_0, ChartShadow A_1)
	{
		int a_ = 6;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1A0;
			case 1:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 15;
					continue;
				}
				num = 18;
				continue;
			case 2:
				goto IL_1A0;
			case 3:
				num = 13;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15F;
				default:
					if (false)
					{
					}
					A_1.Transparency = 100 - XmlConvert.ToInt32(A_0.Value) / 1000;
					num = 0;
					continue;
				}
				break;
			case 6:
				num = 17;
				continue;
			case 7:
				num = 11;
				continue;
			case 8:
				goto IL_11E;
			case 9:
				goto IL_1A0;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("䰻䰽㌿㙁݃⩅㩇", a_))
				{
					goto IL_15F;
				}
				goto IL_26D;
			case 11:
				if (A_0.LocalName != RecordTableEnumerator.b("伻崽⠿❁⥃⍅େ♉㹋", a_))
				{
					num = 8;
					continue;
				}
				goto IL_26D;
			case 12:
				goto IL_1A0;
			case 13:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 19;
					continue;
				}
				goto IL_208;
			}
			case 14:
				goto IL_77;
			case 15:
				goto IL_1C8;
			case 16:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("崻刽〿⩁╃", a_))
				{
					num = 6;
					continue;
				}
				goto IL_208;
			}
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨻弽ⰿ", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_1A0;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 19:
				num = 16;
				continue;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 10;
			continue;
			IL_15F:
			num = 7;
			continue;
			IL_1A0:
			num = 1;
			continue;
			IL_208:
			A_0.Skip();
			num = 9;
			continue;
			IL_26D:
			A_0.Read();
			num = 12;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_11E:
		throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
		IL_1C8:
		A_0.Read();
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x0013FF30 File Offset: 0x0013EF30
	internal static string ᜀ(XmlReader A_0)
	{
		int a_ = 2;
		int num = 2;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				result = A_0.Value;
				goto IL_41;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帷唹主匽ℿ㙁݃⥅ⱇ⽉", a_)))
				{
					num = 0;
					continue;
				}
				return result;
			case 3:
				goto IL_38;
			case 4:
				goto IL_49;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			result = null;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_41;
			}
			if (false)
			{
			}
			num = 1;
			continue;
			IL_41:
			num = 4;
		}
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_49:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x0013FFF8 File Offset: 0x0013EFF8
	internal static XLSXChartPrespectiveType ᜀ(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5, string A_6, string A_7)
	{
		XLSXChartPrespectiveType result;
		for (;;)
		{
			result = XLSXChartPrespectiveType.None;
			int num = 0;
			int num2 = 16;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_1D6;
				case 1:
					num2 = 10;
					continue;
				case 2:
					if (true)
					{
					}
					num2 = 9;
					continue;
				case 3:
					num2 = 0;
					continue;
				case 4:
					num2 = 11;
					continue;
				case 5:
					result = (XLSXChartPrespectiveType)num;
					num2 = 17;
					continue;
				case 6:
					if (A_4.Equals(spr\u1CFF.ᜃ[num][2]))
					{
						num2 = 1;
						continue;
					}
					goto IL_120;
				case 7:
					num2 = 6;
					continue;
				case 8:
					num2 = 13;
					continue;
				case 9:
					if (A_6.Equals(spr\u1CFF.ᜃ[num][6]))
					{
						num2 = 15;
						continue;
					}
					goto IL_120;
				case 10:
					if (A_5.Equals(spr\u1CFF.ᜃ[num][1]))
					{
						num2 = 8;
						continue;
					}
					goto IL_120;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D6;
					default:
						if (false)
						{
						}
						if (A_1.Equals(spr\u1CFF.ᜃ[num][4]))
						{
							num2 = 3;
							continue;
						}
						goto IL_120;
					}
					break;
				case 12:
					goto IL_16F;
				case 13:
					if (A_3.Equals(spr\u1CFF.ᜃ[num][5]))
					{
						num2 = 2;
						continue;
					}
					goto IL_120;
				case 14:
					if (A_0.Equals(spr\u1CFF.ᜃ[num][0]))
					{
						num2 = 4;
						continue;
					}
					goto IL_120;
				case 15:
					num2 = 18;
					continue;
				case 16:
					goto IL_16F;
				case 17:
					return result;
				case 18:
					if (A_7.Equals(spr\u1CFF.ᜃ[num][7]))
					{
						num2 = 5;
						continue;
					}
					goto IL_120;
				case 19:
					if (num >= spr\u1CFF.ᜃ.GetLength(0))
					{
						num2 = 20;
						continue;
					}
					num2 = 14;
					continue;
				case 20:
					return result;
				}
				break;
				IL_120:
				num++;
				num2 = 12;
				continue;
				IL_1D6:
				if (A_2.Equals(spr\u1CFF.ᜃ[num][3]))
				{
					num2 = 7;
					continue;
				}
				goto IL_120;
				IL_16F:
				num2 = 19;
			}
		}
		return result;
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x00140260 File Offset: 0x0013F260
	internal static XLSXChartShadowInnerType ᜀ(string A_0, string A_1, string A_2, XmlReader A_3, ChartShadow A_4, spr\u2306 A_5)
	{
		int a_ = 0;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_149:
			num = 2;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_A2;
			}
			break;
		}
		XLSXChartShadowInnerType result;
		int num4;
		for (;;)
		{
			IL_47:
			int num2;
			switch (num)
			{
			case 0:
				if (A_1.Equals(spr\u1CFF.ᜂ[num2][1]))
				{
					num = 5;
					continue;
				}
				goto IL_D9;
			case 1:
				num = 10;
				continue;
			case 2:
				result = spr\u1AA0.ᜀ(A_0, A_1, A_2, A_4, A_3, true, A_5);
				num = 9;
				continue;
			case 3:
				goto IL_225;
			case 4:
			{
				int num3 = num2;
				num3++;
				result = (XLSXChartShadowInnerType)num3;
				num = 19;
				continue;
			}
			case 5:
				num = 12;
				continue;
			case 6:
				result = XLSXChartShadowInnerType.None;
				num = 20;
				continue;
			case 7:
				goto IL_225;
			case 8:
				if (num2 >= spr\u1CFF.ᜂ.GetLength(0))
				{
					num = 17;
					continue;
				}
				num4++;
				num = 11;
				continue;
			case 9:
				return result;
			case 10:
				if (A_2.Equals(RecordTableEnumerator.b("堵䴷嘹倻", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_281;
			case 11:
				if (A_0.Equals(spr\u1CFF.ᜂ[num2][0]))
				{
					num = 16;
					continue;
				}
				goto IL_D9;
			case 12:
				if (A_2.Equals(spr\u1CFF.ᜂ[num2][2]))
				{
					num = 4;
					continue;
				}
				goto IL_D9;
			case 13:
				goto IL_138;
			case 14:
				num = 18;
				continue;
			case 15:
				if (A_0.Equals(RecordTableEnumerator.b("堵䴷嘹倻", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_281;
			case 16:
				num = 0;
				continue;
			case 17:
				goto IL_12C;
			case 18:
				if (A_1.Equals(RecordTableEnumerator.b("堵䴷嘹倻", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_281;
			case 19:
				goto IL_12C;
			case 20:
				goto IL_127;
			}
			goto IL_A2;
			IL_D9:
			num2++;
			num = 7;
			continue;
			IL_12C:
			num = 13;
			continue;
			IL_225:
			num = 8;
			continue;
			IL_281:
			num2 = 0;
			num = 3;
		}
		IL_127:
		return result;
		IL_138:
		if (num4 == spr\u1CFF.ᜂ.GetLength(0))
		{
			goto IL_149;
		}
		return result;
		IL_A2:
		num4 = 0;
		result = XLSXChartShadowInnerType.None;
		num = 15;
		goto IL_47;
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x00140500 File Offset: 0x0013F500
	internal static XLSXChartShadowInnerType ᜀ(string A_0, string A_1, string A_2, ChartShadow A_3, XmlReader A_4, bool A_5, spr\u2306 A_6)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			XLSXChartShadowInnerType result;
			for (;;)
			{
				result = XLSXChartShadowInnerType.InsideBottom;
				A_3.HasCustomStyle = A_5;
				A_3.Blur = Convert.ToInt32(A_0) / 12700;
				A_3.Distance = Convert.ToInt32(A_1) / 12700;
				A_3.Angle = Convert.ToInt32(A_2) / 60000;
				A_4.Read();
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㭇⥉⑋⭍㵏㝑ᝓ㩕⩗", a_)))
						{
							num = 3;
							continue;
						}
						num = 20;
						continue;
					}
					case 1:
						A_3.Transparency = 100 - Convert.ToInt32(A_4.Value) / 1000;
						num = 29;
						continue;
					case 2:
						A_4.Read();
						num = 33;
						continue;
					case 3:
						num = 14;
						continue;
					case 4:
						goto IL_481;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_183;
						default:
							if (false)
							{
							}
							goto IL_295;
						}
						break;
					case 6:
						goto IL_295;
					case 7:
					{
						string localName2;
						if ((localName2 = A_4.LocalName) != null)
						{
							num = 30;
							continue;
						}
						goto IL_4D9;
					}
					case 8:
						goto IL_183;
					case 9:
					{
						string value = A_4.Value;
						A_3.Color = A_6.ᜎ(value);
						num = 27;
						continue;
					}
					case 10:
						goto IL_295;
					case 11:
						if (A_4.NodeType == XmlNodeType.Element)
						{
							num = 31;
							continue;
						}
						A_4.Skip();
						num = 28;
						continue;
					case 12:
						if (A_4.NodeType == XmlNodeType.EndElement)
						{
							num = 13;
							continue;
						}
						num = 40;
						continue;
					case 13:
						goto IL_2BB;
					case 14:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㭇㡉⭋ⱍፏ㹑♓", a_)))
						{
							num = 8;
							continue;
						}
						num = 16;
						continue;
					}
					case 15:
						if (A_4.NodeType == XmlNodeType.EndElement)
						{
							num = 17;
							continue;
						}
						num = 11;
						continue;
					case 16:
						if (A_4.MoveToAttribute(RecordTableEnumerator.b("㹇⭉⁋", a_)))
						{
							num = 23;
							continue;
						}
						goto IL_522;
					case 17:
						goto IL_27F;
					case 18:
						goto IL_295;
					case 19:
						goto IL_522;
					case 20:
						if (A_4.MoveToAttribute(RecordTableEnumerator.b("㹇⭉⁋", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_2C0;
					case 21:
					{
						string localName;
						if ((localName = A_4.LocalName) != null)
						{
							num = 44;
							continue;
						}
						goto IL_22A;
					}
					case 22:
						goto IL_194;
					case 23:
					{
						string value2 = A_4.Value;
						int a_2 = int.Parse(value2, NumberStyles.HexNumber, null);
						A_3.Color = spr\u1D39.ᜀ(a_2);
						num = 19;
						continue;
					}
					case 24:
					{
						string localName2;
						if (localName2 == RecordTableEnumerator.b("⥇♉㱋♍ㅏ", a_))
						{
							num = 41;
							continue;
						}
						goto IL_4D9;
					}
					case 25:
						num = 0;
						continue;
					case 26:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㡇㡉㽋㩍ፏ㹑♓", a_)))
						{
							num = 25;
							continue;
						}
						num = 42;
						continue;
					}
					case 27:
						goto IL_2C0;
					case 28:
						goto IL_194;
					case 29:
						goto IL_194;
					case 30:
						num = 24;
						continue;
					case 31:
						num = 7;
						continue;
					case 32:
						goto IL_295;
					case 33:
						goto IL_194;
					case 34:
					{
						string value3 = A_4.Value;
						A_3.Color = A_6.ᜎ(value3);
						num = 4;
						continue;
					}
					case 35:
						num = 21;
						continue;
					case 36:
						goto IL_295;
					case 37:
						goto IL_194;
					case 38:
						if (A_4.MoveToAttribute(RecordTableEnumerator.b("㹇⭉⁋", a_)))
						{
							num = 1;
							continue;
						}
						A_3.Transparency = 100000;
						num = 37;
						continue;
					case 39:
						if (!A_4.IsEmptyElement)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_27F;
					case 40:
						if (A_4.NodeType == XmlNodeType.Element)
						{
							num = 35;
							continue;
						}
						A_4.Skip();
						num = 32;
						continue;
					case 41:
						num = 38;
						continue;
					case 42:
						if (A_4.MoveToAttribute(RecordTableEnumerator.b("㹇⭉⁋", a_)))
						{
							num = 34;
							continue;
						}
						goto IL_481;
					case 43:
						goto IL_22A;
					case 44:
						num = 26;
						continue;
					}
					break;
					IL_183:
					num = 43;
					continue;
					IL_194:
					num = 15;
					continue;
					IL_22A:
					A_4.Skip();
					num = 36;
					continue;
					IL_27F:
					A_4.Read();
					num = 10;
					continue;
					IL_295:
					num = 12;
					continue;
					IL_2C0:
					A_4.MoveToElement();
					spr\u1AA0.ᜀ(A_4, A_3);
					num = 5;
					continue;
					IL_481:
					A_4.MoveToElement();
					spr\u1AA0.ᜀ(A_4, A_3);
					num = 18;
					continue;
					IL_4D9:
					A_4.Skip();
					num = 22;
					continue;
					IL_522:
					A_4.MoveToElement();
					num = 39;
				}
			}
			IL_2BB:
			A_4.Read();
			return result;
		}
		}
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x00140AF8 File Offset: 0x0013FAF8
	private static GradientColorType ᜃ(GradientStops A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				GradientColorType gradientColorType;
				int count;
				GradientColorType gradientColorType2;
				GradientColorType result;
				switch (num)
				{
				case 0:
					goto IL_80;
				case 1:
					gradientColorType = GradientColorType.OneColor;
					goto IL_216;
				case 2:
					if (count == 2)
					{
						num = 9;
						continue;
					}
					num = 15;
					continue;
				case 3:
					num = 14;
					continue;
				case 4:
					gradientColorType = GradientColorType.TwoColor;
					goto IL_216;
				case 5:
				{
					XlsGradientStop xlsGradientStop;
					XlsGradientStop xlsGradientStop2;
					if (!(xlsGradientStop.OColor == xlsGradientStop2.OColor))
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				}
				case 7:
					gradientColorType2 = GradientColorType.OneColor;
					goto IL_1CD;
				case 8:
				{
					XlsGradientStop xlsGradientStop3;
					XlsGradientStop xlsGradientStop4;
					if (!(xlsGradientStop3.OColor == xlsGradientStop4.OColor))
					{
						num = 11;
						continue;
					}
					num = 1;
					continue;
				}
				case 9:
				{
					XlsGradientStop xlsGradientStop3 = A_0[0];
					XlsGradientStop xlsGradientStop4 = A_0[1];
					num = 8;
					continue;
				}
				case 10:
					return result;
				case 11:
					num = 4;
					continue;
				case 12:
				{
					XlsGradientStop xlsGradientStop = A_0[0];
					XlsGradientStop xlsGradientStop2 = A_0[1];
					XlsGradientStop xlsGradientStop5 = A_0[2];
					num = 17;
					continue;
				}
				case 13:
					return result;
				case 14:
					gradientColorType2 = GradientColorType.TwoColor;
					goto IL_1CD;
				case 15:
					if (count == 3)
					{
						goto IL_CA;
					}
					return result;
				case 16:
					num = 5;
					continue;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CA;
					default:
					{
						if (false)
						{
						}
						XlsGradientStop xlsGradientStop;
						XlsGradientStop xlsGradientStop5;
						if (xlsGradientStop.OColor == xlsGradientStop5.OColor)
						{
							num = 16;
							continue;
						}
						return result;
					}
					}
					break;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				result = (GradientColorType)(-1);
				count = A_0.Count;
				num = 2;
				continue;
				IL_CA:
				num = 12;
				continue;
				IL_1CD:
				result = gradientColorType2;
				num = 13;
				continue;
				IL_216:
				result = gradientColorType;
				num = 10;
			}
			IL_80:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("✿ぁ╃≅ⅇ⽉≋㩍͏♑㭓♕⭗", a_));
		}
		}
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x00140D3C File Offset: 0x0013FD3C
	private static GradientVariantsType ᜀ(GradientStops A_0, GradientStyleType A_1, GradientColorType A_2, bool A_3)
	{
		int a_ = 9;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_134:
			num = 9;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 1;
				break;
			}
			break;
		}
		for (;;)
		{
			GradientVariantsType result;
			GradientVariantsType gradientVariantsType;
			bool flag;
			bool isDoubled;
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 2:
				num = 10;
				continue;
			case 3:
				return result;
			case 4:
				goto IL_97;
			case 5:
				gradientVariantsType = GradientVariantsType.ShadingVariants1;
				goto IL_154;
			case 6:
				return result;
			case 7:
				return result;
			case 8:
				return result;
			case 9:
				if (!flag)
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
			case 10:
				gradientVariantsType = GradientVariantsType.ShadingVariants2;
				goto IL_154;
			case 11:
				switch (A_1)
				{
				case GradientStyleType.Horizontal:
				case GradientStyleType.Vertical:
				case GradientStyleType.Diagonl_Up:
					result = spr\u1AA0.ᜀ(flag, isDoubled);
					num = 12;
					continue;
				case GradientStyleType.Diagonl_Down:
					result = spr\u1AA0.ᜁ(flag, isDoubled);
					num = 6;
					continue;
				case GradientStyleType.From_Corner:
					result = spr\u1AA0.ᜀ(A_0.FillToRect);
					num = 7;
					continue;
				case GradientStyleType.From_Center:
					goto IL_134;
				default:
					num = 0;
					continue;
				}
				break;
			case 12:
				return result;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			result = GradientVariantsType.ShadingVariants1;
			flag = spr\u1AA0.ᜀ(A_0, A_2, A_3);
			isDoubled = A_0.IsDoubled;
			num = 11;
			continue;
			IL_154:
			result = gradientVariantsType;
			num = 3;
		}
		IL_97:
		throw new ArgumentNullException(RecordTableEnumerator.b("堾㍀≂⅄⹆ⱈ╊㥌ᱎ═㱒╔⑖", a_));
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x00140EE8 File Offset: 0x0013FEE8
	private static GradientVariantsType ᜁ(bool A_0, bool A_1)
	{
		int num = 8;
		GradientVariantsType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				result = GradientVariantsType.ShadingVariants3;
				num = 7;
				continue;
			case 1:
				goto IL_9B;
			case 2:
				if (A_0)
				{
					num = 10;
					continue;
				}
				result = GradientVariantsType.ShadingVariants2;
				num = 9;
				continue;
			case 3:
				if (A_1)
				{
					num = 6;
					continue;
				}
				goto IL_C5;
			case 4:
				goto IL_85;
			case 5:
				return result;
			case 6:
				result = GradientVariantsType.ShadingVariants4;
				num = 4;
				continue;
			case 7:
				goto IL_76;
			case 9:
				goto IL_91;
			case 10:
				result = GradientVariantsType.ShadingVariants1;
				num = 5;
				continue;
			case 11:
				if (!A_1)
				{
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0)
			{
				num = 1;
				continue;
			}
			goto IL_C5;
			IL_9B:
			num = 3;
			continue;
			IL_C5:
			num = 11;
		}
		IL_76:
		IL_85:
		return result;
		IL_91:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x00140FF8 File Offset: 0x0013FFF8
	private static GradientVariantsType ᜀ(bool A_0, bool A_1)
	{
		int num = 4;
		GradientVariantsType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				result = GradientVariantsType.ShadingVariants3;
				num = 1;
				continue;
			case 1:
				return result;
			case 2:
				if (!A_1)
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_93;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				return result;
			case 5:
				return result;
			case 6:
				result = GradientVariantsType.ShadingVariants4;
				num = 5;
				continue;
			case 7:
				goto IL_93;
			case 8:
				if (A_0)
				{
					num = 10;
					continue;
				}
				result = GradientVariantsType.ShadingVariants1;
				num = 3;
				continue;
			case 9:
				if (A_1)
				{
					num = 6;
					continue;
				}
				goto IL_BD;
			case 10:
				result = GradientVariantsType.ShadingVariants2;
				num = 11;
				continue;
			case 11:
				return result;
			}
			if (A_0)
			{
				num = 7;
				continue;
			}
			goto IL_BD;
			IL_93:
			num = 9;
			continue;
			IL_BD:
			if (true)
			{
			}
			num = 2;
		}
		return result;
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x00141108 File Offset: 0x00140108
	private static GradientVariantsType ᜀ(Rectangle A_0)
	{
		switch (0)
		{
		default:
		{
			GradientVariantsType result;
			for (;;)
			{
				Rectangle[] u171A = XlsShapeFill.\u171A;
				result = GradientVariantsType.ShadingVariants1;
				int num = 0;
				int num2 = u171A.Length;
				int num3 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
						result = (GradientVariantsType)num;
						num3 = 6;
						continue;
					case 1:
						return result;
					case 2:
						goto IL_C4;
					case 3:
						goto IL_C4;
					case 4:
						if (u171A[num] == A_0)
						{
							num3 = 0;
							continue;
						}
						goto IL_54;
					case 5:
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						num3 = 4;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							goto IL_84;
						}
						break;
					}
					break;
					IL_54:
					num++;
					num3 = 2;
					continue;
					IL_C4:
					num3 = 5;
				}
			}
			IL_84:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x001411F8 File Offset: 0x001401F8
	private static bool ᜀ(GradientStops A_0, GradientColorType A_1, bool A_2)
	{
		int a_ = 19;
		int num = 10;
		for (;;)
		{
			bool result;
			switch (num)
			{
			case 0:
				if (A_0[0].Shade <= 0)
				{
					num = 1;
					continue;
				}
				goto IL_72;
			case 1:
				num = 5;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				default:
					goto IL_6A;
				}
				break;
			case 3:
				return result;
			case 4:
				switch (A_1)
				{
				case GradientColorType.OneColor:
					num = 0;
					continue;
				case GradientColorType.TwoColor:
					result = false;
					if (true)
					{
					}
					num = 6;
					continue;
				case GradientColorType.Preset:
					result = A_2;
					num = 3;
					continue;
				default:
					num = 7;
					continue;
				}
				break;
			case 5:
				if (A_0[0].Tint > 0)
				{
					num = 8;
					continue;
				}
				return result;
			case 6:
				return result;
			case 7:
				num = 11;
				continue;
			case 8:
				goto IL_72;
			case 9:
				return result;
			case 11:
				return result;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			goto IL_A8;
			IL_72:
			result = true;
			num = 9;
			continue;
			IL_A8:
			result = false;
			num = 4;
		}
		IL_6A:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⹈㥊ⱌ⭎㡐㙒㭔⍖੘⽚㉜⽞በ", a_));
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x0014135C File Offset: 0x0014035C
	private static GradientStyleType ᜂ(GradientStops A_0)
	{
		int a_ = 1;
		int num = 0;
		GradientStyleType result;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return result;
			case 2:
			{
				GradientType gradientType;
				switch (gradientType)
				{
				case GradientType.Liniar:
					result = spr\u1AA0.ᜀ(A_0);
					num = 3;
					continue;
				case GradientType.Circle:
					return result;
				case GradientType.Rect:
					result = spr\u1AA0.ᜁ(A_0);
					num = 5;
					continue;
				default:
					num = 4;
					continue;
				}
				break;
			}
			case 3:
				goto IL_51;
			case 4:
				num = 1;
				continue;
			case 5:
				return result;
			case 6:
				goto IL_40;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				result = GradientStyleType.Horizontal;
				GradientType gradientType = A_0.GradientType;
				num = 2;
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("倶䬸娺夼嘾⑀ⵂㅄᑆ㵈⑊㵌㱎", a_));
		IL_51:
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
			break;
		}
		return result;
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x00141450 File Offset: 0x00140450
	private static GradientStyleType ᜁ(GradientStops A_0)
	{
		int a_ = 7;
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_88;
			case 1:
				if (!(A_0.FillToRect == XlsShapeFill.\u1719))
				{
					num = 0;
					continue;
				}
				return GradientStyleType.From_Center;
			case 3:
				goto IL_3C;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("娼䴾⁀❂ⱄ≆❈㽊Ṍ㭎㹐⍒♔", a_));
		IL_88:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3C;
		}
		if (false)
		{
		}
		return GradientStyleType.From_Corner;
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x001414FC File Offset: 0x001404FC
	private static GradientStyleType ᜀ(GradientStops A_0)
	{
		int a_ = 5;
		int num = 7;
		GradientStyleType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				result = GradientStyleType.Diagonl_Down;
				num = 1;
				continue;
			case 1:
				goto IL_FD;
			case 2:
			{
				int angle;
				if (angle <= 5400000)
				{
					num = 9;
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
					num = 5;
					continue;
				}
				break;
			}
			case 3:
				goto IL_DA;
			case 4:
				return result;
			case 5:
			{
				int angle;
				if (angle <= 18900000)
				{
					num = 0;
					continue;
				}
				result = GradientStyleType.Diagonl_Up;
				num = 3;
				continue;
			}
			case 6:
				goto IL_9C;
			case 8:
				goto IL_54;
			case 9:
				result = GradientStyleType.Horizontal;
				num = 6;
				continue;
			case 10:
			{
				int angle;
				if (angle == 0)
				{
					num = 11;
					continue;
				}
				num = 2;
				continue;
			}
			case 11:
				result = GradientStyleType.Vertical;
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
			}
			else
			{
				int angle = A_0.Angle;
				num = 10;
			}
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("尺似帾╀⩂⁄⥆㵈ᡊ㥌⁎⅐⁒", a_));
		IL_9C:
		IL_DA:
		return result;
		IL_FD:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x00141648 File Offset: 0x00140648
	private static GradientPresetType ᜀ(GradientStops A_0, out bool A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 1;
			GradientPresetType result;
			for (;;)
			{
				GradientStops gradientStops2;
				int num2;
				int num3;
				GradientPresetType[] array;
				switch (num)
				{
				case 0:
					goto IL_141;
				case 2:
					A_0 = A_0.ShrinkGradientStops();
					num = 6;
					continue;
				case 3:
					return result;
				case 4:
					goto IL_71;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_179;
					default:
						goto IL_9B;
					}
					break;
				case 6:
					goto IL_160;
				case 7:
				{
					GradientStops gradientStops;
					if (gradientStops.ᜀ(gradientStops2))
					{
						num = 14;
						continue;
					}
					num2++;
					num = 0;
					continue;
				}
				case 8:
					return result;
				case 9:
					if (A_0.IsDoubled)
					{
						num = 2;
						continue;
					}
					goto IL_160;
				case 10:
				{
					GradientStops gradientStops;
					if (gradientStops.ᜀ(A_0))
					{
						num = 11;
						continue;
					}
					num = 7;
					continue;
				}
				case 11:
				{
					GradientPresetType gradientPresetType;
					result = gradientPresetType;
					num = 3;
					continue;
				}
				case 12:
				{
					if (num2 >= num3)
					{
						num = 8;
						continue;
					}
					GradientPresetType gradientPresetType = array[num2];
					GradientStops gradientStops = XlsShapeFill.ᜀ(gradientPresetType);
					num = 10;
					continue;
				}
				case 13:
					goto IL_141;
				case 14:
				{
					A_1 = true;
					GradientPresetType gradientPresetType;
					result = gradientPresetType;
					num = 5;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				array = (GradientPresetType[])Enum.GetValues(typeof(GradientPresetType));
				num = 9;
				continue;
				IL_141:
				num = 12;
				continue;
				IL_179:
				if (true)
				{
				}
				num = 13;
				continue;
				IL_160:
				A_1 = false;
				result = (GradientPresetType)(-1);
				gradientStops2 = A_0.Clone();
				gradientStops2.InvertGradientStops();
				num2 = 0;
				num3 = array.Length;
				goto IL_179;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("倶䬸娺夼嘾⑀ⵂㅄᑆ㵈⑊㵌㱎", a_));
			IL_9B:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x00141840 File Offset: 0x00140840
	private static void ᜀ(GradientStops A_0, GradientColorType A_1, IShapeFill A_2)
	{
		int a_ = 2;
		int num = 8;
		for (;;)
		{
			double gradientDegree;
			switch (num)
			{
			case 0:
				goto IL_13B;
			case 1:
			{
				int num2 = Math.Max(A_0[0].Tint, A_0[1].Tint);
				int num3 = Math.Max(A_0[0].Shade, A_0[1].Shade);
				num = 11;
				continue;
			}
			case 2:
			{
				int num2;
				gradientDegree = 1.0 - (double)num2 / 100000.0;
				num = 0;
				continue;
			}
			case 3:
				goto IL_13B;
			case 4:
				goto IL_5C;
			case 5:
				if (A_1 == GradientColorType.OneColor)
				{
					num = 1;
					continue;
				}
				return;
			case 6:
				return;
			case 7:
				goto IL_13B;
			case 9:
				goto IL_D0;
			case 10:
				if (A_2 == null)
				{
					num = 9;
					continue;
				}
				num = 5;
				continue;
			case 11:
			{
				int num3;
				if (num3 > 0)
				{
					num = 12;
					continue;
				}
				num = 13;
				continue;
			}
			case 12:
			{
				int num3;
				gradientDegree = (double)num3 / 100000.0;
				if (true)
				{
				}
				num = 7;
				continue;
			}
			case 13:
			{
				int num2;
				if (num2 <= 0)
				{
					gradientDegree = 0.5;
					num = 3;
					continue;
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
					num = 2;
					continue;
				}
				break;
			}
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 10;
			continue;
			IL_13B:
			A_2.GradientDegree = gradientDegree;
			num = 6;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("強䠹崻娽⤿❁⩃㉅ᭇ㹉⍋㹍⍏", a_));
		IL_D0:
		throw new ArgumentNullException(RecordTableEnumerator.b("怷嘹伻砽⤿⹁⡃", a_));
	}

	// Token: 0x04001207 RID: 4615
	private const string ᜀ = "null";

	// Token: 0x04001208 RID: 4616
	private const int ᜁ = 100;

	// Token: 0x04001209 RID: 4617
	private const int ᜂ = 0;

	// Token: 0x0400120A RID: 4618
	private const int ᜃ = 0;

	// Token: 0x0400120B RID: 4619
	private const int ᜄ = 0;

	// Token: 0x0400120C RID: 4620
	private static Dictionary<KeyValuePair<string, string>, ChartLinePatternType> ᜅ;

	// Token: 0x0400120D RID: 4621
	private static XlsWorkbook ᜆ;
}
