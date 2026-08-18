using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000550 RID: 1360
	public class XlsStyle : AddtionalFormatWrapper, IStyle, IComparable, INamedObject
	{
		// Token: 0x06005272 RID: 21106 RVA: 0x00335F34 File Offset: 0x00334F34
		static XlsStyle()
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XlsStyle.DEF_DEFAULT_STYLES = new string[]
			{
				RecordTableEnumerator.b("瀽⼿ぁ⥃❅⑇", a_),
				RecordTableEnumerator.b("氽⼿㕁ࡃ⍅㹇⽉⁋ᅍ", a_),
				RecordTableEnumerator.b("紽⼿⹁ࡃ⍅㹇⽉⁋ᅍ", a_),
				RecordTableEnumerator.b("紽⼿⽁⥃❅", a_),
				RecordTableEnumerator.b("紽㔿ぁ㙃⍅♇⥉㕋", a_),
				RecordTableEnumerator.b("渽┿ぁ❃⍅♇㹉", a_),
				RecordTableEnumerator.b("紽⼿⽁⥃❅桇ᅉ籋ፍ", a_),
				RecordTableEnumerator.b("紽㔿ぁ㙃⍅♇⥉㕋湍୏扑॓", a_),
				RecordTableEnumerator.b("瘽㤿㉁⅃㑅⑇⍉≋╍", a_),
				RecordTableEnumerator.b("砽⼿⹁⡃⥅㽇⽉⡋湍ᡏ⭑⑓㍕⩗㙙㕛そୟ", a_),
				RecordTableEnumerator.b("瀽⼿㙁⅃", a_),
				RecordTableEnumerator.b("椽ℿぁ⩃⽅♇ⵉ汋ᩍ㕏⩑⁓", a_),
				RecordTableEnumerator.b("笽ⴿ㉁ⱃ❅㭇⍉㽋湍慏", a_),
				RecordTableEnumerator.b("笽ⴿ㉁ⱃ❅㭇⍉㽋湍扏", a_),
				"",
				RecordTableEnumerator.b("樽⤿㙁⡃⍅", a_),
				RecordTableEnumerator.b("瘽┿⍁⁃⽅♇ⵉ汋罍", a_),
				RecordTableEnumerator.b("瘽┿⍁⁃⽅♇ⵉ汋籍", a_),
				RecordTableEnumerator.b("瘽┿⍁⁃⽅♇ⵉ汋絍", a_),
				RecordTableEnumerator.b("瘽┿⍁⁃⽅♇ⵉ汋積", a_),
				RecordTableEnumerator.b("眽⸿㉁ㅃ㉅", a_),
				RecordTableEnumerator.b("焽㔿㙁㑃㍅㱇", a_),
				RecordTableEnumerator.b("紽ℿ⹁❃㍅⑇⭉㡋❍㽏㱑", a_),
				RecordTableEnumerator.b("紽⠿❁❃ⵅ桇ॉ⥋≍㱏", a_),
				RecordTableEnumerator.b("爽⤿ⱁ⽃⍅ⱇ橉ཋ⭍㱏㹑", a_),
				RecordTableEnumerator.b("樽⼿㙁╃⩅", a_),
				RecordTableEnumerator.b("礽⼿ⵁ⁃", a_),
				RecordTableEnumerator.b("簽ℿ♁", a_),
				RecordTableEnumerator.b("瀽┿㝁ぃ㑅⥇♉", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇等", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓杕", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓杕", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓杕", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇硉", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓摕", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓摕", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓摕", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇祉", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓敕", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓敕", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓敕", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇繉", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓払", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓払", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓払", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇罉", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓捕", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓捕", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓捕", a_),
				RecordTableEnumerator.b("缽⌿⅁⅃⡅㱇籉", a_),
				RecordTableEnumerator.b("ఽ瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓恕", a_),
				RecordTableEnumerator.b("਽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓恕", a_),
				RecordTableEnumerator.b("࠽瀿杁摃歅桇୉⽋ⵍ㕏㱑⁓恕", a_),
				RecordTableEnumerator.b("笽㠿㉁⡃❅♇⭉㡋⅍≏⭑瑓ɕ㵗≙⡛", a_)
			};
			XlsStyle.ᜆ = new XlsStyle.ᜁ[XlsStyle.DEF_DEFAULT_STYLES.Length];
			int num = 0;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsFill a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 255, 255, 204), spr\u1D39.ᜂ);
			XlsStyle.ᜀ a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜂ a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 178, 178, 178), LineStyleType.Thin);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3, a_4);
			num++;
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 255, 0, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, null);
			num++;
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 3), 18, FontStyle.Bold, RecordTableEnumerator.b("紽ℿ⽁♃㑅ⅇ⭉", a_));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3);
			num++;
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 3), 15, FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(new OColor(ColorType.Theme, 4), LineStyleType.None, LineStyleType.None, LineStyleType.None, LineStyleType.Thick);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3, a_4);
			num++;
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 3), 13, FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(new OColor(ColorType.Theme, 4, 0.499984740745262), LineStyleType.None, LineStyleType.None, LineStyleType.None, LineStyleType.Thick);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3, a_4);
			num++;
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, new XlsStyle.ᜀ(new OColor(ColorType.Theme, 3), FontStyle.Bold), new XlsStyle.ᜂ(new OColor(ColorType.Theme, 4, 0.3999755851924192), LineStyleType.None, LineStyleType.None, LineStyleType.None, LineStyleType.Medium));
			num++;
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 3), FontStyle.Bold);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 255, 204, 153), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 63, 63, 118));
			a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 127, 127, 127), LineStyleType.Thin);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3, a_4);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 242, 242, 242), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 63, 63, 63), FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 63, 63, 63), LineStyleType.Thin);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3, a_4);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 242, 242, 242), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 250, 125, 0), FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 127, 127, 127), LineStyleType.Thin);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3, a_4);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 165, 165, 165), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0), FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 63, 63, 63), LineStyleType.Double);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3, a_4);
			num++;
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 250, 125, 0));
			a_4 = new XlsStyle.ᜂ(Color.FromArgb(255, 255, 128, 1), LineStyleType.None, LineStyleType.None, LineStyleType.None, LineStyleType.Double);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3, a_4);
			num++;
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1), FontStyle.Bold);
			a_4 = new XlsStyle.ᜂ(new OColor(ColorType.Theme, 4), LineStyleType.None, LineStyleType.None, LineStyleType.Thin, LineStyleType.Double);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3, a_4);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 198, 239, 206), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 0, 97, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 255, 199, 206), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 156, 0, 6));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, Color.FromArgb(255, 255, 235, 156), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 156, 101, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 4), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 4, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 4, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 4, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 5), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 5, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 5, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 5, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 6), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 6, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 6, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 6, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 7), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 7, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 7, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 7, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 8), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 8, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 8, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 8, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 9), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 9, 0.7999816888943144), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 9, 0.5999938962981048), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 1));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_2 = new XlsFill(ExcelPatternType.Solid, new OColor(ColorType.Theme, 9, 0.3999755851924192), spr\u1D39.ᜂ);
			a_3 = new XlsStyle.ᜀ(new OColor(ColorType.Theme, 0));
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(a_2, a_3);
			num++;
			a_3 = new XlsStyle.ᜀ(Color.FromArgb(255, 127, 127, 127), FontStyle.Italic);
			XlsStyle.ᜆ[num] = new XlsStyle.ᜁ(null, a_3);
			num++;
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00336F34 File Offset: 0x00335F34
		internal XlsStyle(XlsWorkbook A_0) : base(A_0)
		{
			this.ᜇ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			base.SetFormatIndex((int)this.ᜇ.ᜅ());
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00336F70 File Offset: 0x00335F70
		internal XlsStyle(XlsWorkbook A_0, sprᬐ A_1) : base(A_0)
		{
			this.ᜇ = A_1;
			base.SetFormatIndex((int)this.ᜇ.ᜅ());
			if (A_1.ᜄ() && A_1.ᜀ() == 0)
			{
				this.m_font.IsDirectly = true;
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00336FC0 File Offset: 0x00335FC0
		internal XlsStyle(XlsWorkbook A_0, string A_1) : this(A_0, A_1, null)
		{
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00336FD8 File Offset: 0x00335FD8
		internal XlsStyle(XlsWorkbook A_0, string A_1, XlsStyle A_2) : this(A_0, A_1, A_2, false)
		{
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00336FF0 File Offset: 0x00335FF0
		internal XlsStyle(XlsWorkbook A_0, string A_1, XlsStyle A_2, bool A_3)
		{
			int a_ = 4;
			this..ctor(A_0);
			if (A_2 != null)
			{
				sprᬐ sprᬐ = A_2.ᜇ;
				this.ᜇ = (sprᬐ.Clone() as sprᬐ);
			}
			else
			{
				this.ᜇ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			}
			int num = -1;
			if (A_3)
			{
				num = Array.IndexOf<string>(XlsStyle.DEF_DEFAULT_STYLES, A_1);
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吹崻匽┿", a_), RecordTableEnumerator.b("椹䠻䜽ⰿ❁摃⡅⥇❉⥋湍㍏㍑㩓癕㙗㕙⡛繝ɟݡ䑣eݧὩɫ੭幯", a_));
				}
				this.ᜇ.ᜁ((byte)num);
			}
			else
			{
				this.ᜇ.ᜀ(A_1);
			}
			this.ᜇ.ᜀ(A_3);
			spr\u192F spr_u192F;
			if (A_2 == null)
			{
				spr_u192F = (spr\u192F)this.m_book.CreateExtFormat(true);
			}
			else
			{
				spr_u192F = (spr\u192F)this.m_book.CreateExtFormat(A_2.Wrapped, true);
			}
			spr_u192F.ᜄ(this.m_book.MaxXFCount);
			spr_u192F.ᜀ(sprỶ.TXFType.XF_CELL);
			this.ᜇ.ᜀ((ushort)spr_u192F.ᜠ());
			this.ᜇ.ᜀ(A_3);
			base.SetFormatIndex(spr_u192F.ᜠ());
			if (A_3)
			{
				if (this.m_book.Version != ExcelVersion.Version2007)
				{
					if (this.m_book.Version != ExcelVersion.Version2010)
					{
						return;
					}
				}
				if (num >= 10)
				{
					this.ᜀ(num);
				}
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00337178 File Offset: 0x00336178
		private new void ᜀ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_3B:
					XlsStyle.ᜁ ᜁ = XlsStyle.ᜆ[A_0];
					XlsFill xlsFill = ᜁ.ᜀ;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_D4:
						spr\u2306.ᜀ(xlsFill, this.ᜀ);
						num = 5;
						break;
					default:
						if (false)
						{
						}
						num = 8;
						break;
					}
					for (;;)
					{
						XlsStyle.ᜂ ᜂ;
						XlsStyle.ᜀ ᜀ;
						switch (num)
						{
						case 0:
							goto IL_88;
						case 1:
							this.ᜀ(ᜂ, this.ᜀ);
							num = 4;
							continue;
						case 2:
							if (ᜀ != null)
							{
								num = 6;
								continue;
							}
							goto IL_88;
						case 3:
							goto IL_86;
						case 4:
							return;
						case 5:
							goto IL_109;
						case 6:
							this.ᜀ(ᜀ, this.m_font);
							num = 0;
							continue;
						case 7:
							if (ᜂ != null)
							{
								num = 1;
								continue;
							}
							return;
						case 8:
							if (xlsFill != null)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							goto IL_109;
						}
						goto IL_3B;
						IL_88:
						ᜂ = ᜁ.ᜂ;
						num = 7;
						continue;
						IL_109:
						ᜀ = ᜁ.ᜁ;
						num = 2;
					}
					IL_86:
					goto IL_D4;
				}
				return;
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x003372B8 File Offset: 0x003362B8
		private new void ᜀ(XlsStyle.ᜂ A_0, spr\u192F A_1)
		{
			for (;;)
			{
				OColor ocolor = A_0.ᜀ;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.ᜃ != LineStyleType.None)
						{
							num = 6;
							continue;
						}
						goto IL_13C;
					case 1:
						if (A_0.ᜁ != LineStyleType.None)
						{
							num = 13;
							continue;
						}
						goto IL_1A3;
					case 2:
						if (A_0.ᜂ != LineStyleType.None)
						{
							num = 7;
							continue;
						}
						goto IL_9C;
					case 3:
						if (ocolor != null)
						{
							num = 4;
							continue;
						}
						goto IL_9C;
					case 4:
						A_1.\u1756().ᜀ(ocolor, true);
						num = 12;
						continue;
					case 5:
						A_1.\u173F().ᜀ(ocolor, true);
						num = 15;
						continue;
					case 6:
						A_1.ᜄ(A_0.ᜃ);
						if (true)
						{
						}
						num = 11;
						continue;
					case 7:
						A_1.ᜂ(A_0.ᜂ);
						num = 3;
						continue;
					case 8:
						goto IL_1A3;
					case 9:
						A_1.ᜡ().ᜀ(ocolor, true);
						num = 17;
						continue;
					case 10:
						if (A_0.ᜄ != LineStyleType.None)
						{
							num = 14;
							continue;
						}
						return;
					case 11:
						if (ocolor != null)
						{
							num = 5;
							continue;
						}
						goto IL_13C;
					case 12:
						goto IL_9C;
					case 13:
						A_1.ᜀ(A_0.ᜁ);
						num = 16;
						continue;
					case 14:
						A_1.ᜅ(A_0.ᜄ);
						num = 18;
						continue;
					case 15:
						goto IL_13C;
					case 16:
						if (ocolor != null)
						{
							num = 19;
							continue;
						}
						goto IL_1A3;
					case 17:
						return;
					case 18:
						if (ocolor != null)
						{
							num = 9;
							continue;
						}
						return;
					case 19:
						A_1.ᝅ().ᜀ(ocolor, true);
						num = 8;
						continue;
					}
					break;
					IL_9C:
					num = 0;
					continue;
					IL_13C:
					num = 10;
					continue;
					IL_1A3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13C;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00337518 File Offset: 0x00336518
		private new void ᜀ(XlsStyle.ᜀ A_0, FontWrapper A_1)
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					OColor ocolor = A_0.ᜀ;
					A_1.BeginUpdate();
					int num = 0;
					for (;;)
					{
						string text;
						switch (num)
						{
						case 0:
							if (ocolor != null)
							{
								num = 4;
								continue;
							}
							goto IL_66;
						case 1:
							goto IL_61;
						case 2:
							goto IL_66;
						case 3:
							A_1.FontName = text;
							num = 1;
							continue;
						case 4:
							if (true)
							{
							}
							A_1.OColor.ᜀ(ocolor, true);
							num = 2;
							continue;
						case 5:
							if (text != null)
							{
								num = 3;
								continue;
							}
							goto IL_EE;
						}
						break;
						IL_66:
						A_1.Size = (double)A_0.ᜁ;
						A_1.IsItalic = A_0.ᜃ;
						A_1.IsBold = A_0.ᜂ;
						text = A_0.ᜄ;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 5;
							break;
						}
					}
				}
			}
			IL_61:
			IL_EE:
			A_1.EndUpdate();
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x0033761C File Offset: 0x0033661C
		public new bool BuiltIn
		{
			get
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
				return this.ᜇ.ᜄ();
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x00337664 File Offset: 0x00336664
		public new string Name
		{
			get
			{
				string text;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (0)
					{
					default:
						for (;;)
						{
							text = null;
							bool flag = !this.BuiltIn;
							int num = 12;
							for (;;)
							{
								bool flag2;
								switch (num)
								{
								case 0:
									goto IL_1AF;
								case 1:
									num = 11;
									continue;
								case 2:
								{
									string text2;
									if (text2 != null)
									{
										num = 14;
										continue;
									}
									return text;
								}
								case 3:
									flag2 = (text.Length == 0);
									goto IL_163;
								case 4:
									goto IL_18D;
								case 5:
									if (flag)
									{
										num = 15;
										continue;
									}
									return text;
								case 6:
									num = 3;
									continue;
								case 7:
									goto IL_18F;
								case 8:
									goto IL_A7;
								case 9:
								{
									int num2;
									if (num2 != 1)
									{
										num = 1;
										continue;
									}
									goto IL_1AF;
								}
								case 10:
									if (text != null)
									{
										num = 6;
										continue;
									}
									num = 16;
									continue;
								case 11:
								{
									int num2;
									if (num2 == 2)
									{
										num = 0;
										continue;
									}
									goto IL_18F;
								}
								case 12:
									if (!flag)
									{
										num = 13;
										continue;
									}
									goto IL_A7;
								case 13:
								{
									int num2 = (int)this.ᜇ.ᜁ();
									text = XlsStyle.DEF_DEFAULT_STYLES[num2];
									num = 9;
									continue;
								}
								case 14:
									text = this.ᜇ.ᜆ();
									num = 4;
									continue;
								case 15:
								{
									if (true)
									{
									}
									string text2 = this.ᜇ.ᜆ();
									num = 2;
									continue;
								}
								case 16:
									flag2 = true;
									goto IL_163;
								}
								break;
								IL_A7:
								num = 5;
								continue;
								IL_163:
								flag = flag2;
								num = 8;
								continue;
								IL_18F:
								num = 10;
								continue;
								IL_1AF:
								text += ((int)(this.ᜇ.ᜀ() + 1)).ToString();
								num = 7;
							}
						}
						IL_18D:
						break;
					}
					break;
				}
				return text;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600527D RID: 21117 RVA: 0x00337868 File Offset: 0x00336868
		public new bool IsInitialized
		{
			get
			{
				string text = XlsStyle.DEF_DEFAULT_STYLES[0];
				if (!(this.Name == text))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_46;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					IL_46:
					return !XlsStylesCollection.CompareStyles(this, this.m_book.Styles[text]);
				}
				return false;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x003378D8 File Offset: 0x003368D8
		public int Index
		{
			get
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
				return this.ᜀ.ᜠ();
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x00337920 File Offset: 0x00336920
		// (set) Token: 0x06005280 RID: 21120 RVA: 0x00337964 File Offset: 0x00336964
		public bool NotCompareNames
		{
			get
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
				return this.ᜈ;
			}
			set
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x003379A8 File Offset: 0x003369A8
		internal sprᬐ Record
		{
			get
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
				this.UpdateStyleRecord();
				return this.ᜇ;
			}
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x003379F0 File Offset: 0x003369F0
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 6;
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				IL_3C:
				throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
			}
			A_0.ᜀ(this.ᜇ);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00337A5C File Offset: 0x00336A5C
		public void UpdateStyleRecord()
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
			this.ᜇ.ᜀ((ushort)this.ᜀ.ᜠ());
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00337AB0 File Offset: 0x00336AB0
		public override void EndUpdate()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.EndUpdate();
					int num = 5;
					for (;;)
					{
						int num2;
						int count;
						List<int> list;
						sprᢖ sprᢖ;
						switch (num)
						{
						case 0:
							goto IL_13E;
						case 1:
							goto IL_F6;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13E;
							default:
								if (false)
								{
								}
								goto IL_F6;
							}
							break;
						case 3:
							num = 6;
							continue;
						case 4:
							this.ᜊ(this, EventArgs.Empty);
							num = 0;
							continue;
						case 5:
							if (base.BeginCallsCount == 0)
							{
								num = 3;
								continue;
							}
							goto IL_AA;
						case 6:
							if (this.ᜊ != null)
							{
								num = 4;
								continue;
							}
							goto IL_AA;
						case 7:
							return;
						case 8:
						{
							if (num2 >= count)
							{
								num = 7;
								continue;
							}
							if (true)
							{
							}
							int a_ = list[num2];
							spr\u192F spr_u192F = sprᢖ.ᜁ(a_);
							spr_u192F.ᝂ();
							num2++;
							num = 2;
							continue;
						}
						}
						break;
						IL_AA:
						list = this.ᜀ();
						sprᢖ = this.m_book.InnerExtFormats;
						num2 = 0;
						count = list.Count;
						num = 1;
						continue;
						IL_13E:
						goto IL_AA;
						IL_F6:
						num = 8;
					}
				}
				return;
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00337C00 File Offset: 0x00336C00
		public override void BeginUpdate()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜉ != null)
					{
						num = 2;
						continue;
					}
					goto IL_9C;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜉ(this, EventArgs.Empty);
					num = 4;
					continue;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_7D;
				}
				IL_24:
				if (base.BeginCallsCount != 0)
				{
					break;
				}
				num = 3;
			}
			IL_7D:
			IL_9C:
			base.BeginUpdate();
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00337CB0 File Offset: 0x00336CB0
		public override object Clone(object parent)
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
			XlsStyle xlsStyle = (XlsStyle)base.Clone(parent);
			xlsStyle.ᜇ = (sprᬐ)spr\u1CD3.ᜀ(this.ᜇ);
			return xlsStyle;
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00337D10 File Offset: 0x00336D10
		private new List<int> ᜀ()
		{
			switch (0)
			{
			default:
			{
				List<int> list;
				for (;;)
				{
					list = new List<int>();
					sprᢖ sprᢖ = this.m_book.InnerExtFormats;
					int index = this.Index;
					int num = 0;
					int count = sprᢖ.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6E;
							default:
								goto IL_F6;
							}
							break;
						case 1:
							goto IL_61;
						case 2:
							goto IL_B9;
						case 3:
							list.Add(num);
							num2 = 1;
							continue;
						case 4:
							goto IL_6E;
						case 5:
						{
							spr\u192F spr_u192F;
							if (spr_u192F.ᜯ() == index)
							{
								num2 = 3;
								continue;
							}
							goto IL_61;
						}
						case 6:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							spr\u192F spr_u192F = sprᢖ.ᜁ(num);
							num2 = 5;
							continue;
						}
						}
						break;
						IL_61:
						num++;
						num2 = 4;
						continue;
						IL_B9:
						num2 = 6;
						continue;
						IL_6E:
						goto IL_B9;
					}
				}
				IL_F6:
				if (false)
				{
				}
				return list;
			}
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06005288 RID: 21128 RVA: 0x00337E1C File Offset: 0x00336E1C
		// (remove) Token: 0x06005289 RID: 21129 RVA: 0x00337EB4 File Offset: 0x00336EB4
		public event EventHandler BeforeChange
		{
			add
			{
				for (;;)
				{
					for (;;)
					{
						EventHandler eventHandler = this.ᜉ;
						int num = 2;
						for (;;)
						{
							EventHandler eventHandler2;
							switch (num)
							{
							case 0:
								return;
							case 1:
								if (eventHandler == eventHandler2)
								{
									num = 0;
									continue;
								}
								goto IL_49;
							case 2:
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
									goto IL_49;
								}
								break;
							}
							break;
							IL_49:
							eventHandler2 = eventHandler;
							EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
							eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜉ, value2, eventHandler2);
							num = 1;
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					for (;;)
					{
						EventHandler eventHandler = this.ᜉ;
						int num = 2;
						for (;;)
						{
							EventHandler eventHandler2;
							switch (num)
							{
							case 0:
								goto IL_72;
							case 1:
								if (eventHandler == eventHandler2)
								{
									num = 0;
									continue;
								}
								goto IL_41;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_41;
								}
								break;
							}
							break;
							IL_41:
							eventHandler2 = eventHandler;
							EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
							eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜉ, value2, eventHandler2);
							num = 1;
						}
					}
				}
				IL_72:
				if (true)
				{
				}
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600528A RID: 21130 RVA: 0x00337F48 File Offset: 0x00336F48
		// (remove) Token: 0x0600528B RID: 21131 RVA: 0x00337FE0 File Offset: 0x00336FE0
		public event EventHandler AfterChange
		{
			add
			{
				for (;;)
				{
					for (;;)
					{
						if (true)
						{
						}
						EventHandler eventHandler = this.ᜊ;
						int num = 2;
						for (;;)
						{
							EventHandler eventHandler2;
							switch (num)
							{
							case 0:
								if (eventHandler == eventHandler2)
								{
									num = 1;
									continue;
								}
								goto IL_49;
							case 1:
								return;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_49;
								}
								break;
							}
							break;
							IL_49:
							eventHandler2 = eventHandler;
							EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
							eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜊ, value2, eventHandler2);
							num = 0;
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					for (;;)
					{
						EventHandler eventHandler = this.ᜊ;
						int num = 1;
						for (;;)
						{
							EventHandler eventHandler2;
							switch (num)
							{
							case 0:
								return;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_41;
								}
								break;
							case 2:
								if (eventHandler == eventHandler2)
								{
									num = 0;
									continue;
								}
								goto IL_41;
							}
							break;
							IL_41:
							if (true)
							{
							}
							eventHandler2 = eventHandler;
							EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
							eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜊ, value2, eventHandler2);
							num = 2;
						}
					}
				}
			}
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00338078 File Offset: 0x00337078
		public int CompareTo(object obj)
		{
			for (;;)
			{
				XlsStyle xlsStyle = obj as XlsStyle;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 != 0)
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
								num = 7;
								continue;
							}
						}
						if (true)
						{
						}
						num2 = this.ᜀ.ᜄ(xlsStyle.ᜀ);
						num = 6;
						continue;
					}
					case 1:
					{
						int num2;
						return num2;
					}
					case 2:
					{
						int num2;
						return num2;
					}
					case 3:
					{
						if (!xlsStyle.ᜈ)
						{
							num = 11;
							continue;
						}
						int num2;
						return num2;
					}
					case 4:
					{
						int num2;
						if (num2 != 0)
						{
							num = 2;
							continue;
						}
						return num2;
					}
					case 5:
						return 1;
					case 6:
					{
						int num2;
						if (num2 != 0)
						{
							num = 1;
							continue;
						}
						num = 8;
						continue;
					}
					case 7:
					{
						int num2;
						return num2;
					}
					case 8:
					{
						if (!this.ᜈ)
						{
							num = 10;
							continue;
						}
						int num2;
						return num2;
					}
					case 9:
					{
						if (xlsStyle == null)
						{
							num = 5;
							continue;
						}
						int num2 = this.m_font.Wrapped.CompareTo(xlsStyle.m_font.Wrapped);
						num = 0;
						continue;
					}
					case 10:
						num = 3;
						continue;
					case 11:
					{
						int num2 = this.Name.CompareTo(xlsStyle.Name);
						num = 4;
						continue;
					}
					}
					break;
				}
			}
			return 1;
		}

		// Token: 0x040024BC RID: 9404
		private new const int ᜀ = 10;

		// Token: 0x040024BD RID: 9405
		internal const int ᜁ = -1;

		// Token: 0x040024BE RID: 9406
		internal const int ᜂ = 0;

		// Token: 0x040024BF RID: 9407
		internal const int ᜃ = 1;

		// Token: 0x040024C0 RID: 9408
		private new const int ᜄ = 1;

		// Token: 0x040024C1 RID: 9409
		private const int ᜅ = 2;

		// Token: 0x040024C2 RID: 9410
		public static readonly string[] DEF_DEFAULT_STYLES;

		// Token: 0x040024C3 RID: 9411
		private static readonly XlsStyle.ᜁ[] ᜆ;

		// Token: 0x040024C4 RID: 9412
		private sprᬐ ᜇ;

		// Token: 0x040024C5 RID: 9413
		private bool ᜈ;

		// Token: 0x040024C6 RID: 9414
		private EventHandler ᜉ;

		// Token: 0x040024C7 RID: 9415
		private EventHandler ᜊ;

		// Token: 0x02000638 RID: 1592
		private class ᜁ
		{
			// Token: 0x06006160 RID: 24928 RVA: 0x003D9A3C File Offset: 0x003D8A3C
			public ᜁ(XlsFill A_0, XlsStyle.ᜀ A_1) : this(A_0, A_1, null)
			{
			}

			// Token: 0x06006161 RID: 24929 RVA: 0x003D9A54 File Offset: 0x003D8A54
			public ᜁ(XlsFill A_0, XlsStyle.ᜀ A_1, XlsStyle.ᜂ A_2)
			{
				A_0 = A_0;
				this.ᜁ = A_1;
				this.ᜂ = A_2;
			}

			// Token: 0x04002E89 RID: 11913
			public XlsFill ᜀ;

			// Token: 0x04002E8A RID: 11914
			public XlsStyle.ᜀ ᜁ;

			// Token: 0x04002E8B RID: 11915
			public XlsStyle.ᜂ ᜂ;
		}

		// Token: 0x02000639 RID: 1593
		private new class ᜀ
		{
			// Token: 0x06006162 RID: 24930 RVA: 0x003D9A78 File Offset: 0x003D8A78
			public ᜀ(OColor A_0) : this(A_0, 11)
			{
			}

			// Token: 0x06006163 RID: 24931 RVA: 0x003D9A90 File Offset: 0x003D8A90
			public ᜀ(OColor A_0, int A_1) : this(A_0, A_1, FontStyle.Regular)
			{
			}

			// Token: 0x06006164 RID: 24932 RVA: 0x003D9AA8 File Offset: 0x003D8AA8
			public ᜀ(OColor A_0, FontStyle A_1) : this(A_0, 11, A_1)
			{
			}

			// Token: 0x06006165 RID: 24933 RVA: 0x003D9AC0 File Offset: 0x003D8AC0
			public ᜀ(OColor A_0, int A_1, FontStyle A_2) : this(A_0, A_1, A_2, null)
			{
			}

			// Token: 0x06006166 RID: 24934 RVA: 0x003D9AD8 File Offset: 0x003D8AD8
			public ᜀ(OColor A_0, int A_1, FontStyle A_2, string A_3)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_1;
				this.ᜂ = ((A_2 & FontStyle.Bold) != FontStyle.Regular);
				this.ᜃ = ((A_2 & FontStyle.Italic) != FontStyle.Regular);
				this.ᜄ = A_3;
			}

			// Token: 0x04002E8C RID: 11916
			public OColor ᜀ;

			// Token: 0x04002E8D RID: 11917
			public int ᜁ;

			// Token: 0x04002E8E RID: 11918
			public bool ᜂ;

			// Token: 0x04002E8F RID: 11919
			public bool ᜃ;

			// Token: 0x04002E90 RID: 11920
			public string ᜄ;
		}

		// Token: 0x0200063A RID: 1594
		private class ᜂ
		{
			// Token: 0x06006167 RID: 24935 RVA: 0x003D9B20 File Offset: 0x003D8B20
			public ᜂ(OColor A_0, LineStyleType A_1)
			{
				this.ᜀ = A_0;
				this.ᜄ = A_1;
				this.ᜃ = A_1;
				this.ᜂ = A_1;
				this.ᜁ = A_1;
			}

			// Token: 0x06006168 RID: 24936 RVA: 0x003D9B5C File Offset: 0x003D8B5C
			public ᜂ(OColor A_0, LineStyleType A_1, LineStyleType A_2, LineStyleType A_3, LineStyleType A_4)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_1;
				this.ᜂ = A_2;
				this.ᜃ = A_3;
				this.ᜄ = A_4;
			}

			// Token: 0x04002E91 RID: 11921
			public OColor ᜀ;

			// Token: 0x04002E92 RID: 11922
			public LineStyleType ᜁ;

			// Token: 0x04002E93 RID: 11923
			public LineStyleType ᜂ;

			// Token: 0x04002E94 RID: 11924
			public LineStyleType ᜃ;

			// Token: 0x04002E95 RID: 11925
			public LineStyleType ᜄ;
		}

		// Token: 0x0200063B RID: 1595
		[Flags]
		private enum StyleOptions
		{
			// Token: 0x04002E97 RID: 11927
			None = 0,
			// Token: 0x04002E98 RID: 11928
			UpdateStyleXF = 1,
			// Token: 0x04002E99 RID: 11929
			Temporary = 2
		}
	}
}
