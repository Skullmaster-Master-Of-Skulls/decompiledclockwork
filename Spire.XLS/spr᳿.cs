using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000581 RID: 1409
internal class spr\u1CFF
{
	// Token: 0x060054CA RID: 21706 RVA: 0x00354678 File Offset: 0x00353678
	static spr\u1CFF()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1CFF.ᜀ = new Dictionary<ChartLinePatternType, KeyValuePair<string, string>>();
		spr\u1CFF.ᜁ = new string[9][];
		spr\u1CFF.ᜂ = new string[9][];
		spr\u1CFF.ᜃ = new string[6][];
		spr\u1CFF.ᜄ = new string[13][];
		spr\u1CFF.ᜅ = new string[11][];
		spr\u1CFF.ᜆ = new string[15][];
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.Solid, new KeyValuePair<string, string>(RecordTableEnumerator.b("丼倾ⵀ⩂⅄", a_), string.Empty));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.Dash, new KeyValuePair<string, string>(RecordTableEnumerator.b("儼堾Հ≂㙄⽆", a_), string.Empty));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.Dot, new KeyValuePair<string, string>(RecordTableEnumerator.b("丼䘾㉀݂⑄㑆ⅈ", a_), string.Empty));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.DashDot, new KeyValuePair<string, string>(RecordTableEnumerator.b("儼堾Հ≂㙄⽆ൈ⑊㥌", a_), string.Empty));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.DashDotDot, new KeyValuePair<string, string>(RecordTableEnumerator.b("儼堾Հ≂㙄⽆ൈ⑊㥌୎㹐❒", a_), string.Empty));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.DarkGray, new KeyValuePair<string, string>(RecordTableEnumerator.b("丼倾ⵀ⩂⅄", a_), RecordTableEnumerator.b("䴼尾㕀瑂灄", a_)));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.MediumGray, new KeyValuePair<string, string>(RecordTableEnumerator.b("丼倾ⵀ⩂⅄", a_), RecordTableEnumerator.b("䴼尾㕀療畄", a_)));
		spr\u1CFF.ᜀ.Add(ChartLinePatternType.LightGray, new KeyValuePair<string, string>(RecordTableEnumerator.b("丼倾ⵀ⩂⅄", a_), RecordTableEnumerator.b("䴼尾㕀煂灄", a_)));
		spr\u1CFF.ᜁ[0] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("儼", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[1] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("༼࠾煀獂畄睆祈", a_),
			RecordTableEnumerator.b("䤼匾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[2] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("࠼ା煀獂畄睆祈", a_),
			RecordTableEnumerator.b("䤼", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[3] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("఼ా瑀獂畄睆祈筊", a_),
			RecordTableEnumerator.b("弼䴾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[4] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("఼༾獀獂畄睆", a_),
			RecordTableEnumerator.b("఼༾獀獂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("帼䬾㍀", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[5] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("఼ा獀獂畄睆祈筊", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[6] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("఼༾祀獂畄睆祈筊", a_),
			RecordTableEnumerator.b("似", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[7] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("఼ܾ础獂畄睆祈筊", a_),
			RecordTableEnumerator.b("弼匾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜁ[8] = new string[]
		{
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("฼ܾ灀獂畄", a_),
			RecordTableEnumerator.b("Լ฾煀獂畄睆祈", a_),
			RecordTableEnumerator.b("䤼䴾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜂ[0] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("Լ฾煀獂畄睆祈", a_)
		};
		spr\u1CFF.ᜂ[1] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("఼ा獀獂畄睆祈筊", a_)
		};
		spr\u1CFF.ᜂ[2] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜂ[3] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("఼༾祀獂畄睆祈筊", a_)
		};
		spr\u1CFF.ᜂ[4] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("఼ܾ础獂畄睆祈筊", a_)
		};
		spr\u1CFF.ᜂ[5] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("༼࠾煀獂畄睆祈", a_)
		};
		spr\u1CFF.ᜂ[6] = new string[]
		{
			RecordTableEnumerator.b("఼฾畀灂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜂ[7] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("࠼ା煀獂畄睆祈", a_)
		};
		spr\u1CFF.ᜂ[8] = new string[]
		{
			RecordTableEnumerator.b("଼ా瑀獂畄", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("఼ా瑀獂畄睆祈筊", a_)
		};
		spr\u1CFF.ᜃ[0] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜃ[1] = new string[]
		{
			RecordTableEnumerator.b("਼ा獀獂畄", a_),
			RecordTableEnumerator.b("఼ܾ础獂畄睆祈筊", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("༼ా煀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("ြ฾獀獂畄睆祈筊", a_),
			RecordTableEnumerator.b("弼匾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜃ[2] = new string[]
		{
			RecordTableEnumerator.b("਼ा獀獂畄", a_),
			RecordTableEnumerator.b("༼࠾煀獂畄睆祈", a_),
			RecordTableEnumerator.b("఼ാ癀獂畄", a_),
			RecordTableEnumerator.b("ြാ牀獂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("ြܾ煀獂煄睆祈", a_),
			RecordTableEnumerator.b("弼匾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜃ[3] = new string[]
		{
			RecordTableEnumerator.b("਼ा獀獂畄", a_),
			RecordTableEnumerator.b("఼ా瑀獂畄睆祈筊", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("༼ా煀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("఼ാ煀獂畄睆祈", a_),
			RecordTableEnumerator.b("弼䴾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜃ[4] = new string[]
		{
			RecordTableEnumerator.b("਼ा獀獂畄", a_),
			RecordTableEnumerator.b("Լ฾煀獂畄睆祈", a_),
			RecordTableEnumerator.b("఼ാ癀獂畄", a_),
			RecordTableEnumerator.b("ြാ牀獂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("Լ༾煀睂畄睆", a_),
			RecordTableEnumerator.b("弼䴾", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜃ[5] = new string[]
		{
			RecordTableEnumerator.b("఼ਾ獀睂畄睆", a_),
			RecordTableEnumerator.b("࠼ା煀獂畄睆祈", a_),
			RecordTableEnumerator.b("฼฾癀療畄睆", a_),
			RecordTableEnumerator.b("ြ฾础獂畄睆", a_),
			RecordTableEnumerator.b("м༾煀獂畄", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("഼", a_)
		};
		spr\u1CFF.ᜄ[0] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜄ[1] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("尼儾♀⽂⁄", a_)
		};
		spr\u1CFF.ᜄ[2] = new string[]
		{
			RecordTableEnumerator.b("఼฾畀灂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("尼䴾㕀݂⁄⑆♈", a_)
		};
		spr\u1CFF.ᜄ[3] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜄ[4] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("帼倾⽀㕂⁄㽆", a_)
		};
		spr\u1CFF.ᜄ[5] = new string[]
		{
			RecordTableEnumerator.b("఼ा瑀牂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("帼倾⹀⽂ᙄ⭆⡈╊㥌", a_)
		};
		spr\u1CFF.ᜄ[6] = new string[]
		{
			RecordTableEnumerator.b("఼ా础瑂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("帼䴾⹀あ㙄", a_)
		};
		spr\u1CFF.ᜄ[7] = new string[]
		{
			RecordTableEnumerator.b("఼ా础瑂畄睆", a_),
			RecordTableEnumerator.b("఼ా础瑂畄睆", a_),
			RecordTableEnumerator.b("夼嘾㝀ⱂㅄ", a_)
		};
		spr\u1CFF.ᜄ[8] = new string[]
		{
			RecordTableEnumerator.b("఼฾畀灂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("唼帾㍀❂D⍆⹈⹊", a_)
		};
		spr\u1CFF.ᜄ[9] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("似娾ⵀ≂㵄≆ⵈɊ⍌㱎㑐❒", a_)
		};
		spr\u1CFF.ᜄ[10] = new string[]
		{
			RecordTableEnumerator.b("఼༾灀畂畄睆", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("似嘾⍀⽂⁄㍆", a_)
		};
		spr\u1CFF.ᜄ[11] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_),
			RecordTableEnumerator.b("丼匾⹀㍂⁄", a_)
		};
		spr\u1CFF.ᜄ[12] = new string[]
		{
			RecordTableEnumerator.b("఼ਾ獀睂畄睆", a_),
			RecordTableEnumerator.b("࠼༾祀獂畄", a_),
			RecordTableEnumerator.b("丼倾❀㝂ᝄ⡆㱈╊⥌", a_)
		};
		spr\u1CFF.ᜅ[0] = new string[]
		{
			RecordTableEnumerator.b("值帾㕀㝂⁄", a_)
		};
		spr\u1CFF.ᜅ[1] = new string[]
		{
			RecordTableEnumerator.b("匼䨾ⵀ⽂", a_)
		};
		spr\u1CFF.ᜅ[2] = new string[]
		{
			RecordTableEnumerator.b("䴼匾⁀あㅄ⹆⩈", a_)
		};
		spr\u1CFF.ᜅ[3] = new string[]
		{
			RecordTableEnumerator.b("值娾㕀≂⥄", a_)
		};
		spr\u1CFF.ᜅ[4] = new string[]
		{
			RecordTableEnumerator.b("夼吾р❂≄≆", a_)
		};
		spr\u1CFF.ᜅ[5] = new string[]
		{
			RecordTableEnumerator.b("丼倾❀㝂D⍆⹈⹊", a_)
		};
		spr\u1CFF.ᜅ[6] = new string[]
		{
			RecordTableEnumerator.b("嬼匾⁀㝂", a_)
		};
		spr\u1CFF.ᜅ[7] = new string[]
		{
			RecordTableEnumerator.b("儼娾♀≂♄㹆Ṉ≊㽌⩎㝐⅒㑔㩖㱘", a_)
		};
		spr\u1CFF.ᜅ[8] = new string[]
		{
			RecordTableEnumerator.b("䴼倾㙀❂⁄㕆", a_)
		};
		spr\u1CFF.ᜅ[9] = new string[]
		{
			RecordTableEnumerator.b("䤼䴾⁀ⵂ㙄⭆㱈⡊⡌ⅎ═͒㩔⁖㵘㹚⽜", a_)
		};
		spr\u1CFF.ᜅ[10] = new string[]
		{
			RecordTableEnumerator.b("值帾㕀㝂⁄", a_)
		};
		spr\u1CFF.ᜆ[0] = new string[]
		{
			RecordTableEnumerator.b("䤼圾㍀♂⁄ᝆ㵈", a_)
		};
		spr\u1CFF.ᜆ[1] = new string[]
		{
			RecordTableEnumerator.b("弼帾ⵀ≂⭄⑆ⱈ⽊", a_)
		};
		spr\u1CFF.ᜆ[2] = new string[]
		{
			RecordTableEnumerator.b("弼䴾⡀⑂ⵄ㍆ᭈ⑊≌≎", a_)
		};
		spr\u1CFF.ᜆ[3] = new string[]
		{
			RecordTableEnumerator.b("帼圾⡀⽂⥄㹆", a_)
		};
		spr\u1CFF.ᜆ[4] = new string[]
		{
			RecordTableEnumerator.b("帼倾⽀㝂㝄♆㩈㽊⑌ⅎ㙐", a_)
		};
		spr\u1CFF.ᜆ[5] = new string[]
		{
			RecordTableEnumerator.b("嬼匾⁀㝂", a_)
		};
		spr\u1CFF.ᜆ[6] = new string[]
		{
			RecordTableEnumerator.b("嬼匾⹀ⱂ⅄", a_)
		};
		spr\u1CFF.ᜆ[7] = new string[]
		{
			RecordTableEnumerator.b("嬼䴾⑀♂㽄⹆❈ⱊ", a_)
		};
		spr\u1CFF.ᜆ[8] = new string[]
		{
			RecordTableEnumerator.b("娼匾⹀㑂", a_)
		};
		spr\u1CFF.ᜆ[9] = new string[]
		{
			RecordTableEnumerator.b("唼帾㍀あⵄ", a_)
		};
		spr\u1CFF.ᜆ[10] = new string[]
		{
			RecordTableEnumerator.b("值倾㍀ⵂⱄ⥆⹈", a_)
		};
		spr\u1CFF.ᜆ[11] = new string[]
		{
			RecordTableEnumerator.b("丼倾❀㝂", a_)
		};
		spr\u1CFF.ᜆ[12] = new string[]
		{
			RecordTableEnumerator.b("丼䨾⽀ㅂⱄ㑆ⱈ", a_)
		};
		spr\u1CFF.ᜆ[13] = new string[]
		{
			RecordTableEnumerator.b("丼䨾⽀あ⁄㍆", a_)
		};
		spr\u1CFF.ᜆ[14] = new string[]
		{
			RecordTableEnumerator.b("䤼䠾⹀ፂㅄ", a_)
		};
	}

	// Token: 0x060054CB RID: 21707 RVA: 0x00355A98 File Offset: 0x00354A98
	public static void ᜀ(XmlWriter A_0, IChartFillBorder A_1, XlsChart A_2, bool A_3)
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
		spr\u1CFF.ᜀ(A_0, A_1, A_2, A_3, false);
	}

	// Token: 0x060054CC RID: 21708 RVA: 0x00355AE0 File Offset: 0x00354AE0
	public static void ᜀ(XmlWriter A_0, IChartFillBorder A_1, XlsChart A_2, bool A_3, bool A_4)
	{
		int a_ = 13;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				if (true)
				{
				}
				if (A_2 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 2:
				return;
			case 3:
				goto IL_C3;
			case 5:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_C5;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C3;
				}
				if (false)
				{
				}
				num = 1;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_C3:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_));
		IL_C5:
		sprᡟ sprᡟ = A_2.DataHolder;
		sprវ a_2 = sprᡟ.ᜋ();
		RelationsCollection a_3 = A_2.Relations;
		spr\u1CFF.ᜀ(A_0, A_1, a_2, a_3, A_3, A_4);
	}

	// Token: 0x060054CD RID: 21709 RVA: 0x00355BD4 File Offset: 0x00354BD4
	public static void ᜀ(XmlWriter A_0, IChartFillBorder A_1, sprវ A_2, RelationsCollection A_3, bool A_4, bool A_5)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				spr\u1C26 spr_u1C;
				switch (num)
				{
				case 0:
					spr\u1CFF.ᜀ(A_0, spr_u1C, A_2, A_3);
					num = 2;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					goto IL_41B;
				case 3:
					if (A_1 == null)
					{
						num = 17;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("ㅁ㑃ᙅ㩇", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ慎ﮋﺏ煉歹랗ꢙ겛꺝隟趡잣캥즧\ud8a9\ud8ab", a_));
					num = 14;
					continue;
				case 4:
					if (A_1.HasLineProperties)
					{
						num = 13;
						continue;
					}
					goto IL_17A;
				case 5:
					if (A_1.Interior.Pattern == ExcelPatternType.None)
					{
						num = 10;
						continue;
					}
					goto IL_328;
				case 6:
					goto IL_1A2;
				case 7:
					if (!A_1.Shadow.HasCustomStyle)
					{
						num = 21;
						continue;
					}
					goto IL_224;
				case 8:
				{
					IFormat3D format3D = A_1.Format3D;
					spr\u1CFF.ᜀ(A_0, format3D);
					num = 15;
					continue;
				}
				case 9:
					if (A_1.HasShadow)
					{
						num = 1;
						continue;
					}
					goto IL_224;
				case 10:
					num = 27;
					continue;
				case 11:
					goto IL_C6;
				case 12:
					if (!A_1.Interior.UseDefaultFormat)
					{
						num = 31;
						continue;
					}
					goto IL_1CA;
				case 13:
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_39A;
						}
					}
					IL_39A:
					if (false)
					{
					}
					XlsChartBorder lineProperties = A_1.LineProperties;
					num = 28;
					continue;
				}
				case 14:
					if (A_1.HasInterior)
					{
						num = 23;
						continue;
					}
					goto IL_41B;
				case 15:
					goto IL_26A;
				case 16:
					goto IL_17A;
				case 17:
					return;
				case 19:
					if (spr_u1C != null)
					{
						num = 0;
						continue;
					}
					goto IL_41B;
				case 20:
					if (A_1.Interior.Pattern == ExcelPatternType.None)
					{
						num = 30;
						continue;
					}
					goto IL_41B;
				case 21:
				{
					IShadow shadow = A_1.Shadow;
					spr\u1CFF.ᜀ(A_0, shadow, A_1.Shadow.HasCustomStyle);
					num = 6;
					continue;
				}
				case 22:
					if (A_1.HasFormat3D)
					{
						num = 8;
						continue;
					}
					goto IL_443;
				case 23:
					num = 12;
					continue;
				case 24:
					goto IL_1A2;
				case 25:
					goto IL_41B;
				case 26:
					num = 32;
					continue;
				case 27:
					if (A_1.Fill.FillType != ShapeFillType.Pattern)
					{
						num = 26;
						continue;
					}
					goto IL_1CA;
				case 28:
				{
					XlsChartBorder lineProperties;
					if (!lineProperties.UseDefaultFormat)
					{
						if (true)
						{
						}
						num = 33;
						continue;
					}
					goto IL_17A;
				}
				case 29:
					goto IL_328;
				case 30:
					A_0.WriteElementString(RecordTableEnumerator.b("ⱁ⭃Eⅇ♉⁋", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ慎ﮋﺏ煉歹랗ꢙ겛꺝隟趡즣장솧쒩", a_), string.Empty);
					num = 25;
					continue;
				case 31:
					num = 5;
					continue;
				case 32:
					if (A_1.Fill.FillType != ShapeFillType.SolidColor)
					{
						num = 29;
						continue;
					}
					goto IL_1CA;
				case 33:
				{
					XlsChartBorder lineProperties;
					spr\u1CFF.ᜀ(A_0, lineProperties, A_4, A_2.\u171C(), A_5);
					num = 16;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 3;
				continue;
				IL_17A:
				num = 9;
				continue;
				IL_1A2:
				num = 22;
				continue;
				IL_1CA:
				num = 20;
				continue;
				IL_224:
				IShadow shadow2 = A_1.Shadow;
				spr\u1CFF.ᜀ(A_0, shadow2, A_1.Shadow.HasCustomStyle);
				num = 24;
				continue;
				IL_328:
				spr_u1C = (A_1.Fill as spr\u1C26);
				num = 19;
				continue;
				IL_41B:
				num = 4;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_26A:
			IL_443:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054CE RID: 21710 RVA: 0x0035602C File Offset: 0x0035502C
	internal static void ᜀ(XmlWriter A_0, IShadow A_1, bool A_2)
	{
		int a_ = 15;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_41;
			case 1:
				if (A_1.ShadowPrespectiveType != XLSXChartPrespectiveType.None)
				{
					num = 5;
					continue;
				}
				goto IL_EB;
			case 2:
				goto IL_E6;
			case 4:
				if (A_1.ShadowOuterType != XLSXChartShadowOuterType.None)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			case 5:
				goto IL_5E;
			}
			if (A_1.ShadowInnerType != XLSXChartShadowInnerType.None)
			{
				num = 0;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E6;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				num = 4;
			}
		}
		IL_41:
		int a_2 = A_1.ShadowInnerType - XLSXChartShadowInnerType.InsideDiagonalBottomLeft;
		spr\u1CFF.ᜂ(A_0, a_2, A_2, A_1);
		return;
		IL_5E:
		int shadowPrespectiveType = (int)A_1.ShadowPrespectiveType;
		spr\u1CFF.ᜀ(A_0, shadowPrespectiveType, A_2, A_1);
		return;
		IL_E6:
		int a_3 = A_1.ShadowOuterType - XLSXChartShadowOuterType.OffsetRight;
		spr\u1CFF.ᜁ(A_0, a_3, A_2, A_1);
		return;
		IL_EB:
		A_0.WriteElementString(RecordTableEnumerator.b("⁄ⅆ⽈⹊⹌㭎ᵐ⁒⅔", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_), string.Empty);
	}

	// Token: 0x060054CF RID: 21711 RVA: 0x0035614C File Offset: 0x0035514C
	internal static void ᜂ(XmlWriter A_0, int A_1, bool A_2, IShadow A_3)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("崷尹娻嬽⌿㙁ࡃ㕅㱇", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("儷吹刻嬽㈿ᅁⱃ≅㽇", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_2)
						{
							num = 11;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("娷嘹䤻䰽ሿ⍁⁃", a_), spr\u1CFF.ᜂ[A_1][0].ToString());
						num = 6;
						continue;
					case 1:
						goto IL_E7;
					case 2:
						goto IL_2FB;
					case 3:
						if (A_1 != 6)
						{
							num = 20;
							continue;
						}
						goto IL_48A;
					case 4:
						goto IL_E7;
					case 5:
						if (!spr\u1CFF.ᜂ[A_1][1].Equals(RecordTableEnumerator.b("嘷伹倻刽", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_2FB;
					case 6:
						goto IL_1A9;
					case 7:
						if (!spr\u1CFF.ᜂ[A_1][2].Equals(RecordTableEnumerator.b("嘷伹倻刽", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_E7;
					case 8:
						A_0.WriteStartElement(RecordTableEnumerator.b("夷嘹䰻嘽ℿ", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("丷嬹倻", a_), A_3.Transparency.ToString());
						A_0.WriteEndElement();
						num = 13;
						continue;
					case 9:
						A_0.WriteAttributeString(RecordTableEnumerator.b("尷匹伻䨽", a_), spr\u1CFF.ᜂ[A_1][1].ToString());
						num = 23;
						continue;
					case 10:
						goto IL_2F6;
					case 11:
						A_0.WriteAttributeString(RecordTableEnumerator.b("娷嘹䤻䰽ሿ⍁⁃", a_), A_3.Blur.ToString());
						num = 12;
						continue;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C7;
						default:
							if (false)
							{
							}
							goto IL_1A9;
						}
						break;
					case 13:
						goto IL_22B;
					case 14:
						if (A_2)
						{
							num = 22;
							continue;
						}
						num = 5;
						continue;
					case 15:
						A_0.WriteAttributeString(RecordTableEnumerator.b("尷匹主", a_), A_3.Angle.ToString());
						num = 4;
						continue;
					case 16:
						if (A_3.Transparency != 0)
						{
							num = 8;
							continue;
						}
						goto IL_48A;
					case 17:
						A_0.WriteAttributeString(RecordTableEnumerator.b("尷匹主", a_), spr\u1CFF.ᜂ[A_1][2].ToString());
						num = 1;
						continue;
					case 18:
						if (A_2)
						{
							num = 19;
							continue;
						}
						num = 3;
						continue;
					case 19:
						num = 16;
						continue;
					case 20:
						A_0.WriteStartElement(RecordTableEnumerator.b("夷嘹䰻嘽ℿ", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("丷嬹倻", a_), RecordTableEnumerator.b("ഷਹ఻฽瀿", a_));
						A_0.WriteEndElement();
						num = 10;
						continue;
					case 21:
						if (A_2)
						{
							num = 15;
							continue;
						}
						num = 7;
						continue;
					case 22:
						goto IL_1C7;
					case 23:
						goto IL_2FB;
					}
					break;
					IL_E7:
					A_0.WriteStartElement(RecordTableEnumerator.b("䬷䠹嬻尽̿⹁㙃", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("丷嬹倻", a_), (A_3.Color.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("怷హ", a_)));
					num = 18;
					continue;
					IL_1A9:
					num = 14;
					continue;
					IL_1C7:
					if (true)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("尷匹伻䨽", a_), A_3.Distance.ToString());
					num = 2;
					continue;
					IL_2FB:
					num = 21;
				}
			}
			IL_22B:
			IL_2F6:
			IL_48A:
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x060054D0 RID: 21712 RVA: 0x003565F8 File Offset: 0x003555F8
	internal static void ᜁ(XmlWriter A_0, int A_1, bool A_2, IShadow A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("♂⍄ⅆⱈ⡊㥌͎≐❒", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("ⱂい㍆ⱈ㥊Ṍ❎㕐⑒", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_0.WriteStartElement(RecordTableEnumerator.b("≂⥄㝆ⅈ⩊", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("㕂⑄⭆", a_), A_3.Transparency.ToString());
						A_0.WriteEndElement();
						num = 29;
						continue;
					case 1:
						goto IL_552;
					case 2:
						goto IL_4B1;
					case 3:
						goto IL_401;
					case 4:
						goto IL_145;
					case 5:
						A_0.WriteAttributeString(RecordTableEnumerator.b("あ㵄", a_), spr\u1CFF.ᜁ[A_1][1].ToString());
						num = 2;
						continue;
					case 6:
						A_0.WriteAttributeString(RecordTableEnumerator.b("あ㵄", a_), A_3.Size.ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("あ㱄", a_), A_3.Size.ToString());
						num = 21;
						continue;
					case 7:
						goto IL_24D;
					case 8:
						goto IL_145;
					case 9:
						if (!spr\u1CFF.ᜁ[A_1][3].Equals(RecordTableEnumerator.b("ⵂい⭆╈", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_145;
					case 10:
						goto IL_406;
					case 11:
						A_0.WriteAttributeString(RecordTableEnumerator.b("≂⥄⁆❈", a_), spr\u1CFF.ᜁ[A_1][5].ToString());
						num = 27;
						continue;
					case 12:
						if (true)
						{
						}
						if (A_2)
						{
							num = 6;
							continue;
						}
						num = 31;
						continue;
					case 13:
						A_0.WriteAttributeString(RecordTableEnumerator.b("❂ⱄ㑆㵈", a_), A_3.Distance.ToString());
						num = 4;
						continue;
					case 14:
						goto IL_24D;
					case 15:
						if (A_2)
						{
							num = 30;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("≂⥄㝆ⅈ⩊", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("㕂⑄⭆", a_), RecordTableEnumerator.b("睂畄睆祈筊", a_));
						A_0.WriteEndElement();
						num = 3;
						continue;
					case 16:
						A_0.WriteAttributeString(RecordTableEnumerator.b("❂ⱄ㕆", a_), spr\u1CFF.ᜁ[A_1][4].ToString());
						num = 24;
						continue;
					case 17:
						if (A_2)
						{
							num = 26;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("⅂⥄㉆㭈᥊ⱌ⭎", a_), spr\u1CFF.ᜁ[A_1][0].ToString());
						num = 7;
						continue;
					case 18:
						if (A_2)
						{
							num = 13;
							continue;
						}
						num = 9;
						continue;
					case 19:
						A_0.WriteAttributeString(RecordTableEnumerator.b("❂ⱄ㑆㵈", a_), spr\u1CFF.ᜁ[A_1][3].ToString());
						num = 8;
						continue;
					case 20:
						A_0.WriteAttributeString(RecordTableEnumerator.b("あ㱄", a_), spr\u1CFF.ᜁ[A_1][2].ToString());
						num = 10;
						continue;
					case 21:
						goto IL_406;
					case 22:
						if (A_2)
						{
							num = 25;
							continue;
						}
						num = 32;
						continue;
					case 23:
						if (A_3.Transparency != 0)
						{
							num = 0;
							continue;
						}
						goto IL_683;
					case 24:
						goto IL_552;
					case 25:
						A_0.WriteAttributeString(RecordTableEnumerator.b("❂ⱄ㕆", a_), A_3.Angle.ToString());
						num = 1;
						continue;
					case 26:
						A_0.WriteAttributeString(RecordTableEnumerator.b("⅂⥄㉆㭈᥊ⱌ⭎", a_), A_3.Blur.ToString());
						num = 14;
						continue;
					case 27:
						goto IL_1A6;
					case 28:
						if (!spr\u1CFF.ᜁ[A_1][5].Equals(RecordTableEnumerator.b("ⵂい⭆╈", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_1A6;
					case 29:
						goto IL_30D;
					case 30:
						num = 23;
						continue;
					case 31:
						IL_174:
						if (!spr\u1CFF.ᜁ[A_1][1].Equals(RecordTableEnumerator.b("ⵂい⭆╈", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_4B1;
					case 32:
						if (!spr\u1CFF.ᜁ[A_1][4].Equals(RecordTableEnumerator.b("ⵂい⭆╈", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_552;
					case 33:
						if (!spr\u1CFF.ᜁ[A_1][2].Equals(RecordTableEnumerator.b("ⵂい⭆╈", a_)))
						{
							num = 20;
							continue;
						}
						goto IL_406;
					}
					break;
					IL_145:
					num = 22;
					continue;
					IL_552:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_174;
					default:
						if (false)
						{
						}
						num = 28;
						continue;
					}
					IL_1A6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("ㅂ⩄㍆Ṉ≊㥌❎ɐ㭒㑔❖㱘", a_), spr\u1CFF.ᜁ[A_1][6].ToString());
					A_0.WriteStartElement(RecordTableEnumerator.b("あ㝄⁆⭈ࡊ⅌㵎", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㕂⑄⭆", a_), (A_3.Color.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("ᭂ獄", a_)));
					num = 15;
					continue;
					IL_24D:
					num = 12;
					continue;
					IL_406:
					num = 18;
					continue;
					IL_4B1:
					num = 33;
				}
			}
			IL_30D:
			IL_401:
			IL_683:
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x060054D1 RID: 21713 RVA: 0x00356C9C File Offset: 0x00355C9C
	internal static void ᜀ(XmlWriter A_0, int A_1, bool A_2, IShadow A_3)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("⁄ⅆ⽈⹊⹌㭎ᵐ⁒⅔", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("⩄㉆㵈⹊㽌ᱎ㥐㝒≔", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_469;
					case 1:
						A_0.WriteAttributeString(RecordTableEnumerator.b("❄⭆㱈㥊Ὄ⹎㕐", a_), A_3.Blur.ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("⅄⹆㭈", a_), A_3.Angle.ToString());
						num = 30;
						continue;
					case 2:
						if (!spr\u1CFF.ᜃ[A_1][4].Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_1BD;
					case 3:
						if (A_3.Transparency != 0)
						{
							num = 8;
							continue;
						}
						goto IL_6CE;
					case 4:
						A_0.WriteAttributeString(RecordTableEnumerator.b("⹄㽆", a_), spr\u1CFF.ᜃ[A_1][5].ToString());
						num = 29;
						continue;
					case 5:
						A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㹆", a_), spr\u1CFF.ᜃ[A_1][3].ToString());
						num = 32;
						continue;
					case 6:
						goto IL_116;
					case 7:
						if (A_2)
						{
							num = 0;
							continue;
						}
						num = 23;
						continue;
					case 8:
						A_0.WriteStartElement(RecordTableEnumerator.b("⑄⭆㥈⍊ⱌ", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("㍄♆╈", a_), A_3.Transparency.ToString());
						A_0.WriteEndElement();
						num = 21;
						continue;
					case 9:
						A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㽆", a_), spr\u1CFF.ᜃ[A_1][4].ToString());
						num = 12;
						continue;
					case 10:
						A_0.WriteAttributeString(RecordTableEnumerator.b("⑄⭆⹈╊", a_), spr\u1CFF.ᜃ[A_1][6].ToString());
						num = 6;
						continue;
					case 11:
						if (A_2)
						{
							num = 15;
							continue;
						}
						num = 34;
						continue;
					case 12:
						goto IL_1BD;
					case 13:
						if (A_1 != 4)
						{
							num = 19;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("㍄♆╈", a_), RecordTableEnumerator.b("瑄牆祈筊経", a_));
						num = 22;
						continue;
					case 14:
						A_0.WriteAttributeString(RecordTableEnumerator.b("⅄⹆㩈㽊", a_), spr\u1CFF.ᜃ[A_1][2].ToString());
						num = 24;
						continue;
					case 15:
						A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㹆", a_), A_3.Size.ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㽆", a_), A_3.Size.ToString());
						num = 31;
						continue;
					case 16:
						goto IL_3AD;
					case 17:
						if (A_2)
						{
							num = 1;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("❄⭆㱈㥊Ὄ⹎㕐", a_), spr\u1CFF.ᜃ[A_1][0].ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("⅄⹆㭈", a_), spr\u1CFF.ᜃ[A_1][1].ToString());
						num = 20;
						continue;
					case 18:
						goto IL_499;
					case 19:
						A_0.WriteAttributeString(RecordTableEnumerator.b("㍄♆╈", a_), RecordTableEnumerator.b("睄睆祈筊経", a_));
						num = 25;
						continue;
					case 20:
						goto IL_27C;
					case 21:
						goto IL_566;
					case 22:
						goto IL_39B;
					case 23:
						if (!spr\u1CFF.ᜃ[A_1][2].Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_499;
					case 24:
						goto IL_499;
					case 25:
						goto IL_39B;
					case 26:
						num = 3;
						continue;
					case 27:
						if (A_2)
						{
							num = 26;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("⑄⭆㥈⍊ⱌ", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
						num = 13;
						continue;
					case 28:
						if (!spr\u1CFF.ᜃ[A_1][6].Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_116;
					case 29:
						goto IL_56B;
					case 30:
						if (true)
						{
						}
						goto IL_27C;
					case 31:
						goto IL_1BD;
					case 32:
						goto IL_5D9;
					case 33:
						if (!spr\u1CFF.ᜃ[A_1][5].Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_56B;
					case 34:
						if (spr\u1CFF.ᜃ[A_1][3].Equals(RecordTableEnumerator.b("⭄㉆╈❊", a_)))
						{
							goto IL_5D9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_469;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					break;
					IL_116:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㝄⡆㵈᱊⑌㭎㥐R㵔㙖⥘㹚", a_), spr\u1CFF.ᜃ[A_1][7].ToString());
					A_0.WriteStartElement(RecordTableEnumerator.b("㙄㕆⹈⥊์⍎⍐", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㍄♆╈", a_), (A_3.Color.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("ᵄ煆", a_)));
					num = 27;
					continue;
					IL_1BD:
					num = 33;
					continue;
					IL_27C:
					num = 7;
					continue;
					IL_39B:
					A_0.WriteEndElement();
					num = 16;
					continue;
					IL_469:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⅄⹆㩈㽊", a_), A_3.Distance.ToString());
					num = 18;
					continue;
					IL_499:
					num = 11;
					continue;
					IL_56B:
					num = 28;
					continue;
					IL_5D9:
					num = 2;
				}
			}
			IL_3AD:
			IL_566:
			IL_6CE:
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x060054D2 RID: 21714 RVA: 0x0035738C File Offset: 0x0035638C
	internal static void ᜀ(XmlWriter A_0, IFormat3D A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = A_1.BevelBottomType == XLSXChartBevelType.None;
				bool flag2 = A_1.BevelTopType == XLSXChartBevelType.None;
				bool flag3 = A_1.MaterialType == XLSXChartMaterialType.None;
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E0;
					case 1:
						if (A_1.BevelBottomType != XLSXChartBevelType.None)
						{
							num = 18;
							continue;
						}
						goto IL_382;
					case 2:
						num = 10;
						continue;
					case 3:
						num = 4;
						continue;
					case 4:
						if (flag2)
						{
							num = 2;
							continue;
						}
						goto IL_224;
					case 5:
						goto IL_BB;
					case 6:
						goto IL_13C;
					case 7:
						goto IL_E0;
					case 8:
						goto IL_1EC;
					case 9:
					{
						int lightingType = (int)A_1.LightingType;
						spr\u1CFF.ᜃ(A_0, lightingType);
						num = 0;
						continue;
					}
					case 10:
						if (flag3)
						{
							num = 15;
							continue;
						}
						goto IL_224;
					case 11:
					{
						int bevelTopType = (int)A_1.BevelTopType;
						spr\u1CFF.ᜁ(A_0, bevelTopType);
						num = 6;
						continue;
					}
					case 12:
						if (A_1.BevelTopType != XLSXChartBevelType.None)
						{
							num = 11;
							continue;
						}
						goto IL_13C;
					case 13:
						if (flag)
						{
							num = 3;
							continue;
						}
						goto IL_224;
					case 14:
						if (true)
						{
						}
						if (A_1.MaterialType != XLSXChartMaterialType.None)
						{
							num = 17;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("㝃㙅筇⹉", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
						num = 5;
						continue;
					case 15:
						return;
					case 16:
						if (A_1.LightingType != XLSXChartLightingType.ThreePoint)
						{
							num = 9;
							continue;
						}
						goto IL_1F1;
					case 17:
					{
						A_0.WriteStartElement(RecordTableEnumerator.b("㝃㙅筇⹉", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
						int a_2 = A_1.MaterialType - XLSXChartMaterialType.Matte;
						spr\u1CFF.ᜂ(A_0, a_2);
						num = 19;
						continue;
					}
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F1;
						default:
						{
							if (false)
							{
							}
							int bevelBottomType = (int)A_1.BevelBottomType;
							spr\u1CFF.ᜀ(A_0, bevelBottomType);
							num = 8;
							continue;
						}
						}
						break;
					case 19:
						goto IL_BB;
					}
					break;
					IL_BB:
					num = 12;
					continue;
					IL_E0:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⁃⽅㩇", a_), RecordTableEnumerator.b("ぃ", a_));
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					num = 14;
					continue;
					IL_13C:
					num = 1;
					continue;
					IL_1F1:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㙃⽅⽇", a_), spr\u1CFF.ᜆ[0][0].ToString());
					num = 7;
					continue;
					IL_224:
					A_0.WriteStartElement(RecordTableEnumerator.b("㝃╅ⵇ⑉⥋絍㑏", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("❃❅╇⽉㹋⽍", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㑃㑅㭇㹉", a_), RecordTableEnumerator.b("⭃㑅㱇≉⍋⥍≏㍑⑓㹕ㅗ㥙ᩛⱝཟౡၣ", a_));
					A_0.WriteEndElement();
					A_0.WriteStartElement(RecordTableEnumerator.b("⡃⽅⽇≉㡋ᱍ㥏㕑", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
					num = 16;
				}
			}
			return;
			IL_1EC:
			IL_382:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x060054D3 RID: 21715 RVA: 0x00357724 File Offset: 0x00356724
	internal static void ᜃ(XmlWriter A_0, int A_1)
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
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑅ⅇⵉ", a_), spr\u1CFF.ᜆ[A_1][0].ToString());
	}

	// Token: 0x060054D4 RID: 21716 RVA: 0x0035778C File Offset: 0x0035678C
	internal static void ᜂ(XmlWriter A_0, int A_1)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.WriteAttributeString(RecordTableEnumerator.b("㙅㩇㥉㡋͍ㅏ♑ㅓ⑕ㅗ㭙せ", a_), spr\u1CFF.ᜅ[A_1][0].ToString());
	}

	// Token: 0x060054D5 RID: 21717 RVA: 0x003577F4 File Offset: 0x003567F4
	internal static void ᜁ(XmlWriter A_0, int A_1)
	{
		int a_ = 14;
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("♃⍅㹇⽉⁋ᩍ", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_95;
				case 1:
					goto IL_8A;
				case 2:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㍃", a_), spr\u1CFF.ᜄ[A_1][0].ToString());
					num = 7;
					continue;
				case 3:
					if (!spr\u1CFF.ᜄ[A_1][0].Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_17E;
				case 4:
					if (!spr\u1CFF.ᜄ[A_1][1].Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_8A;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_95;
					default:
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("㑃㑅㭇㹉", a_), spr\u1CFF.ᜄ[A_1][2].ToString());
						num = 8;
						continue;
					}
					break;
				case 6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⱃ", a_), spr\u1CFF.ᜄ[A_1][1].ToString());
					num = 1;
					continue;
				case 7:
					goto IL_17E;
				case 8:
					goto IL_17C;
				}
				break;
				IL_8A:
				num = 0;
				continue;
				IL_95:
				if (true)
				{
				}
				if (!spr\u1CFF.ᜄ[A_1][2].Equals(RecordTableEnumerator.b("⩃㍅⑇♉", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_1BA;
				IL_17E:
				num = 4;
			}
		}
		IL_17C:
		IL_1BA:
		A_0.WriteEndElement();
	}

	// Token: 0x060054D6 RID: 21718 RVA: 0x003579C4 File Offset: 0x003569C4
	internal static void ᜀ(XmlWriter A_0, int A_1)
	{
		int a_ = 11;
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("⍀♂㍄≆╈ॊ", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9D;
				case 1:
					if (!spr\u1CFF.ᜄ[A_1][0].Equals(RecordTableEnumerator.b("⽀㙂⥄⭆", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_17E;
				case 2:
					goto IL_17E;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9D;
					default:
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("ㅀㅂ㙄㍆", a_), spr\u1CFF.ᜄ[A_1][2].ToString());
						num = 6;
						continue;
					}
					break;
				case 4:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㙀", a_), spr\u1CFF.ᜄ[A_1][0].ToString());
					num = 2;
					continue;
				case 5:
					goto IL_8A;
				case 6:
					goto IL_17C;
				case 7:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥀", a_), spr\u1CFF.ᜄ[A_1][1].ToString());
					num = 5;
					continue;
				case 8:
					if (!spr\u1CFF.ᜄ[A_1][1].Equals(RecordTableEnumerator.b("⽀㙂⥄⭆", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_8A;
				}
				break;
				IL_8A:
				if (true)
				{
				}
				num = 0;
				continue;
				IL_9D:
				if (!spr\u1CFF.ᜄ[A_1][2].Equals(RecordTableEnumerator.b("⽀㙂⥄⭆", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1BA;
				IL_17E:
				num = 8;
			}
		}
		IL_17C:
		IL_1BA:
		A_0.WriteEndElement();
	}

	// Token: 0x060054D7 RID: 21719 RVA: 0x00357B94 File Offset: 0x00356B94
	internal static void ᜀ(XmlWriter A_0, spr\u1C26 A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 9;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 4;
				continue;
			case 2:
				goto IL_40;
			case 3:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				ShapeFillType fillType = A_1.FillType;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7B;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			}
			case 4:
				goto IL_162;
			case 5:
			{
				ShapeFillType fillType;
				switch (fillType)
				{
				case ShapeFillType.SolidColor:
					goto IL_57;
				case ShapeFillType.Pattern:
					goto IL_BC;
				case ShapeFillType.Texture:
					goto IL_9E;
				case ShapeFillType.Picture:
					goto IL_42;
				case ShapeFillType.UnknownGradient:
				case (ShapeFillType)5:
				case (ShapeFillType)6:
					goto IL_164;
				case ShapeFillType.Gradient:
					goto IL_DD;
				default:
					num = 1;
					continue;
				}
				break;
			}
			case 6:
				return;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_7B:
			if (true)
			{
			}
			num = 3;
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_42:
		spr\u1CFF.ᜀ(A_0, A_1.Picture, A_2, A_3, A_1.ᜂ());
		return;
		IL_57:
		spr\u1CFF.ᜀ(A_0, A_1.ᜁ(), false, A_2.\u171C(), 1.0 - A_1.Transparency);
		return;
		IL_9E:
		spr\u1CFF.ᜀ(A_0, A_1, A_2, A_3);
		return;
		IL_BC:
		spr\u1CFF.ᜀ(A_0, A_1.ᜁ(), false, A_1.ᜀ(), false, A_1.Pattern, A_2.\u171C());
		return;
		IL_DD:
		spr\u1CFF.ᜀ(A_0, A_1, A_2.\u171C());
		return;
		IL_162:
		IL_164:
		throw new NotImplementedException();
	}

	// Token: 0x060054D8 RID: 21720 RVA: 0x00357D0C File Offset: 0x00356D0C
	public static void ᜀ(XmlWriter A_0, IChartTextArea A_1, XlsWorkbook A_2, RelationsCollection A_3, double A_4)
	{
		int a_ = 4;
		for (;;)
		{
			IL_09:
			int num = 4;
			for (;;)
			{
				sprវ a_2;
				switch (num)
				{
				case 0:
				{
					XlsChartTextArea xlsChartTextArea;
					if (xlsChartTextArea.HasText)
					{
						num = 7;
						continue;
					}
					goto IL_132;
				}
				case 1:
					goto IL_132;
				case 2:
				{
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					XlsChartTextArea xlsChartTextArea = A_1 as XlsChartTextArea;
					a_2 = A_2.DataHolder;
					A_0.WriteStartElement(RecordTableEnumerator.b("丹唻䨽ⰿ❁", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					num = 0;
					continue;
				}
				case 3:
					goto IL_DC;
				case 5:
					goto IL_4C;
				case 6:
				{
					XlsChartTextArea xlsChartTextArea;
					if (xlsChartTextArea.ParagraphType == ChartParagraphType.Default)
					{
						num = 9;
						continue;
					}
					goto IL_188;
				}
				case 7:
					spr\u1CFF.ᜂ(A_0, A_1, A_2, A_4);
					num = 1;
					continue;
				case 8:
					goto IL_112;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						spr\u1CFF.ᜃ(A_0, A_1, A_2, A_4);
						num = 8;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
				IL_132:
				spr\u1CFF.ᜁ(A_0, A_1);
				spr\u1CFF.ᜀ(A_0, A_1);
				spr\u1CFF.ᜀ(A_0, A_1.FrameFormat, a_2, A_3, false, false);
				num = 6;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_DC:
		throw new ArgumentNullException(RecordTableEnumerator.b("丹夻䘽㐿́㙃⍅⥇", a_));
		IL_112:
		IL_188:
		A_0.WriteEndElement();
	}

	// Token: 0x060054D9 RID: 21721 RVA: 0x00357EA8 File Offset: 0x00356EA8
	public static void ᜀ(XmlWriter A_0, string A_1, string A_2)
	{
		int a_ = 17;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u1CFF.ᜀ(A_0, A_1, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_), A_2);
	}

	// Token: 0x060054DA RID: 21722 RVA: 0x00357F04 File Offset: 0x00356F04
	public static void ᜀ(XmlWriter A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_77;
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
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				case 1:
					goto IL_D6;
				case 2:
					if (A_3 == null)
					{
						num = 4;
						continue;
					}
					goto IL_D8;
				case 3:
					goto IL_62;
				case 4:
					goto IL_77;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎", a_));
		IL_D6:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍆⡈ⱊ͌⹎㱐㙒", a_));
		IL_D8:
		A_0.WriteStartElement(A_1, A_2);
		A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ⡈❊", a_), A_3);
		A_0.WriteEndElement();
	}

	// Token: 0x060054DB RID: 21723 RVA: 0x0035800C File Offset: 0x0035700C
	public static void ᜀ(XmlWriter A_0, string A_1, bool A_2)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_EB;
			case 1:
				goto IL_44;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					A_0.WriteStartElement(A_1, RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_));
					num = 4;
					continue;
				}
				break;
			case 4:
				if (!A_2)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 5:
				goto IL_4E;
			case 6:
				goto IL_46;
			case 7:
				goto IL_9C;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 3;
			continue;
			IL_46:
			num = 5;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_4E:
		string text = RecordTableEnumerator.b("഼", a_);
		goto IL_126;
		IL_9C:
		text = RecordTableEnumerator.b("఼", a_);
		goto IL_126;
		IL_EB:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤼帾♀ൂ⑄⩆ⱈ", a_));
		IL_126:
		string value = text;
		A_0.WriteAttributeString(RecordTableEnumerator.b("䬼帾ⵀ", a_), value);
		A_0.WriteEndElement();
	}

	// Token: 0x060054DC RID: 21724 RVA: 0x0035815C File Offset: 0x0035715C
	public static void ᜀ(XmlWriter A_0, IChartBorder A_1, IWorkbook A_2)
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
		spr\u1CFF.ᜀ(A_0, A_1 as XlsChartBorder, false, A_2, false);
	}

	// Token: 0x060054DD RID: 21725 RVA: 0x003581A8 File Offset: 0x003571A8
	public static void ᜀ(XmlWriter A_0, Color A_1, bool A_2, string A_3, string A_4, IWorkbook A_5, double A_6)
	{
		int a_ = 7;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_4 != null)
				{
					num = 1;
					continue;
				}
				goto IL_51;
			case 1:
				num = 7;
				continue;
			case 2:
				goto IL_86;
			case 3:
				goto IL_4F;
			case 5:
				goto IL_51;
			case 6:
				goto IL_E8;
			case 7:
			{
				if (A_4.Length == 0)
				{
					num = 5;
					continue;
				}
				OColor a_2 = new OColor(spr\u1D39.ᜁ);
				spr\u1CFF.ᜀ(A_0, A_1, A_2, a_2, A_2, A_4, A_5, A_6);
				num = 6;
				continue;
			}
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_51:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_A9;
			default:
				if (false)
				{
				}
				spr\u1CFF.ᜀ(A_0, A_1, A_2, A_5, A_6);
				num = 2;
				break;
			}
		}
		IL_4F:
		goto IL_A9;
		IL_86:
		goto IL_116;
		IL_A9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_E8:
		IL_116:
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䴼䴾㉀㝂ń♆㩈⍊", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), A_3);
	}

	// Token: 0x060054DE RID: 21726 RVA: 0x003582F0 File Offset: 0x003572F0
	public static void ᜀ(XmlWriter A_0, OColor A_1, bool A_2, OColor A_3, bool A_4, string A_5, IWorkbook A_6, double A_7)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_57;
			case 2:
				if (!A_4)
				{
					num = 4;
					continue;
				}
				goto IL_1FA;
			case 3:
				goto IL_1A6;
			case 4:
				A_0.WriteStartElement(RecordTableEnumerator.b("❄⁆ੈ❊㽌", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
				spr\u1CFF.ᜀ(A_0, A_3.ᜁ(A_6), A_7);
				A_0.WriteEndElement();
				if (true)
				{
				}
				num = 11;
				continue;
			case 5:
				goto IL_12E;
			case 6:
				A_0.WriteStartElement(RecordTableEnumerator.b("⍄⁆ੈ❊㽌", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
				spr\u1CFF.ᜀ(A_0, A_1.ᜁ(A_6), A_7);
				A_0.WriteEndElement();
				num = 3;
				continue;
			case 7:
				if (A_5.Length == 0)
				{
					num = 9;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㕄♆㵈㽊ୌ♎㵐㽒", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㕄㕆㩈㽊", a_), A_5);
				num = 10;
				continue;
			case 8:
				num = 7;
				continue;
			case 9:
				goto IL_1F8;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12E;
				default:
					if (false)
					{
					}
					if (!A_2)
					{
						num = 6;
						continue;
					}
					goto IL_1A6;
				}
				break;
			case 11:
				goto IL_190;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_12E:
			if (A_5 != null)
			{
				num = 8;
				continue;
			}
			goto IL_1C2;
			IL_1A6:
			num = 2;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_190:
		goto IL_1FA;
		IL_1C2:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙄㍆㭈ᭊ㽌⩎≐㙒⅔", a_));
		IL_1F8:
		goto IL_1C2;
		IL_1FA:
		A_0.WriteEndElement();
	}

	// Token: 0x060054DF RID: 21727 RVA: 0x00358500 File Offset: 0x00357500
	public static void ᜀ(XmlWriter A_0, OColor A_1, bool A_2, OColor A_3, bool A_4, GradientPatternType A_5, IWorkbook A_6)
	{
		int a_ = 0;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					goto IL_11A;
				}
				break;
			case 1:
				goto IL_53;
			case 2:
				if (!A_2)
				{
					num = 6;
					continue;
				}
				goto IL_53;
			case 3:
				goto IL_47;
			case 4:
				goto IL_6A;
			case 5:
				if (!A_4)
				{
					num = 4;
					continue;
				}
				goto IL_18F;
			case 6:
				A_0.WriteStartElement(RecordTableEnumerator.b("倵強礹倻䰽", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮙ", a_));
				spr\u1CFF.ᜀ(A_0, A_1.ᜁ(A_6));
				A_0.WriteEndElement();
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("䘵夷丹䠻砽⤿⹁⡃", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮙ", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("䘵䨷䤹䠻", a_), ((XLSXGradientPattern)A_5).ToString());
			if (true)
			{
			}
			num = 2;
			continue;
			IL_53:
			num = 5;
			continue;
			IL_6A:
			A_0.WriteStartElement(RecordTableEnumerator.b("吵強礹倻䰽", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮙ", a_));
			spr\u1CFF.ᜀ(A_0, A_3.ᜁ(A_6));
			A_0.WriteEndElement();
			num = 0;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_11A:
		if (false)
		{
		}
		IL_18F:
		A_0.WriteEndElement();
	}

	// Token: 0x060054E0 RID: 21728 RVA: 0x003586A4 File Offset: 0x003576A4
	public static void ᜀ(XmlWriter A_0, OColor A_1, bool A_2, IWorkbook A_3, double A_4)
	{
		int a_ = 17;
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
				goto IL_75;
			case 2:
				goto IL_5E;
			case 3:
				if (!A_2)
				{
					num = 2;
					continue;
				}
				goto IL_D2;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5E;
				default:
					goto IL_56;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("㑆♈❊⑌⭎ᝐ㩒㥔㭖", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
			num = 3;
			continue;
			IL_5E:
			spr\u1CFF.ᜀ(A_0, A_1.ᜁ(A_3), A_4);
			num = 1;
		}
		IL_56:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_75:
		IL_D2:
		A_0.WriteEndElement();
	}

	// Token: 0x060054E1 RID: 21729 RVA: 0x0035878C File Offset: 0x0035778C
	public static void ᜀ(XmlWriter A_0, Color A_1)
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
		spr\u1CFF.ᜀ(A_0, A_1, -1.0);
	}

	// Token: 0x060054E2 RID: 21730 RVA: 0x003587D8 File Offset: 0x003577D8
	public static void ᜀ(XmlWriter A_0, ExcelColors A_1, IWorkbook A_2)
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
		spr\u1CFF.ᜀ(A_0, A_2.GetPaletteColor(A_1), -1.0);
	}

	// Token: 0x060054E3 RID: 21731 RVA: 0x0035882C File Offset: 0x0035782C
	public static void ᜀ(XmlWriter A_0, Color A_1, double A_2)
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
		int a_ = Convert.ToInt32(A_2 * 100000.0);
		spr\u1CFF.ᜀ(A_0, A_1, a_, -1, -1);
	}

	// Token: 0x060054E4 RID: 21732 RVA: 0x00358884 File Offset: 0x00357884
	public static void ᜀ(XmlWriter A_0, Color A_1, int A_2, int A_3, int A_4)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.WriteElementString(RecordTableEnumerator.b("⁆⡈♊⁌⹎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_), string.Empty);
				A_0.WriteStartElement(RecordTableEnumerator.b("㑆ⅈ⩊⥌⩎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ⡈❊", a_), A_4.ToString());
				A_0.WriteEndElement();
				A_0.WriteElementString(RecordTableEnumerator.b("⹆❈㵊ੌ⹎㱐㹒㑔", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_), string.Empty);
				goto IL_FB;
			case 2:
				if (A_2 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_10B;
			case 3:
				A_0.WriteStartElement(RecordTableEnumerator.b("♆╈㭊╌⹎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ⡈❊", a_), A_2.ToString());
				A_0.WriteEndElement();
				num = 12;
				continue;
			case 4:
				if (A_3 >= 0)
				{
					num = 7;
					continue;
				}
				goto IL_31A;
			case 5:
				goto IL_106;
			case 6:
				num = 2;
				continue;
			case 7:
				A_0.WriteElementString(RecordTableEnumerator.b("⁆⡈♊⁌⹎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_), string.Empty);
				A_0.WriteStartElement(RecordTableEnumerator.b("㍆⁈╊㥌", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ⡈❊", a_), A_3.ToString());
				A_0.WriteEndElement();
				A_0.WriteElementString(RecordTableEnumerator.b("⹆❈㵊ੌ⹎㱐㹒㑔", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_), string.Empty);
				num = 9;
				continue;
			case 8:
				goto IL_65;
			case 9:
				goto IL_251;
			case 10:
				if (A_4 >= 0)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FB;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 11:
				if (A_2 != 100000)
				{
					num = 6;
					continue;
				}
				goto IL_10B;
			case 12:
				goto IL_10B;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("㑆㭈ⱊ⽌౎㵐⅒", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ⡈❊", a_), (A_1.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("὆罈", a_)));
			num = 11;
			continue;
			IL_FB:
			num = 5;
			continue;
			IL_10B:
			if (true)
			{
			}
			num = 10;
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_106:
		IL_251:
		IL_31A:
		A_0.WriteEndElement();
	}

	// Token: 0x060054E5 RID: 21733 RVA: 0x00358BB4 File Offset: 0x00357BB4
	private static void ᜀ(XmlWriter A_0, XlsChartBorder A_1, bool A_2, IWorkbook A_3, bool A_4)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_B5;
			default:
				if (false)
				{
				}
				num = 11;
				break;
			}
			for (;;)
			{
				IL_3E:
				ChartLinePatternType pattern;
				int num2;
				switch (num)
				{
				case 0:
				{
					if (pattern == ChartLinePatternType.Solid)
					{
						num = 10;
						continue;
					}
					KeyValuePair<string, string> keyValuePair = spr\u1CFF.ᜀ[pattern];
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					spr\u1CFF.ᜀ(A_0, A_1.Color, A_1.UseDefaultLineColor, key, value, A_3, 1.0 - A_1.Transparency);
					num = 25;
					continue;
				}
				case 1:
					if (pattern == ChartLinePatternType.None)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 2:
					num2 = 12700;
					num = 27;
					continue;
				case 3:
					A_0.WriteElementString(RecordTableEnumerator.b("❈⑊ୌ♎㵐㽒", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_), string.Empty);
					num = 22;
					continue;
				case 4:
					if (A_1.LineWeightString != null)
					{
						num = 20;
						continue;
					}
					goto IL_3C9;
				case 5:
					if (num2 != -1)
					{
						if (true)
						{
						}
						num = 23;
						continue;
					}
					num = 4;
					continue;
				case 6:
					goto IL_329;
				case 7:
					return;
				case 8:
					spr\u1CFF.ᜀ(A_0, A_1.Fill, A_3);
					num = 12;
					continue;
				case 9:
					goto IL_3C9;
				case 10:
					spr\u1CFF.ᜀ(A_0, A_1.Color, A_1.UseDefaultLineColor, A_3, 1.0 - A_1.Transparency);
					A_0.WriteStartElement(RecordTableEnumerator.b("㥈㥊㹌㭎ᕐ㉒♔㽖", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㽈⩊⅌", a_), RecordTableEnumerator.b("㩈⑊⅌♎㕐", a_));
					A_0.WriteEndElement();
					num = 24;
					continue;
				case 12:
					goto IL_2C8;
				case 13:
					goto IL_2E0;
				case 14:
					goto IL_C7;
				case 15:
					if (A_4)
					{
						num = 6;
						continue;
					}
					goto IL_42B;
				case 16:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("╈╊", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_));
					num = 18;
					continue;
				case 17:
					if (num2 == 0)
					{
						num = 2;
						continue;
					}
					goto IL_3F8;
				case 18:
					if (A_1.UseDefaultFormat)
					{
						num = 21;
						continue;
					}
					goto IL_329;
				case 19:
					goto IL_3C9;
				case 20:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㹈", a_), A_1.LineWeightString);
					num = 9;
					continue;
				case 21:
					num = 15;
					continue;
				case 22:
					goto IL_2C8;
				case 23:
					num2 = (int)(((short)A_1.Weight + 1) * 12700);
					num = 17;
					continue;
				case 24:
					goto IL_2C8;
				case 25:
					goto IL_2C8;
				case 26:
					if (A_1.HasGradientFill)
					{
						num = 8;
						continue;
					}
					num = 1;
					continue;
				case 27:
					goto IL_3F8;
				}
				goto IL_B5;
				IL_2C8:
				spr\u1CFF.ᜀ(A_0, A_1.JoinType);
				num = 13;
				continue;
				IL_329:
				num2 = (int)((short)A_1.Weight);
				num = 5;
				continue;
				IL_3C9:
				pattern = A_1.Pattern;
				num = 26;
				continue;
				IL_3F8:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㹈", a_), num2.ToString());
				num = 19;
			}
			IL_C7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			IL_2E0:
			IL_42B:
			A_0.WriteEndElement();
			return;
			IL_B5:
			if (A_0 == null)
			{
				num = 14;
				goto IL_3E;
			}
			num = 16;
			goto IL_3E;
		}
		}
	}

	// Token: 0x060054E6 RID: 21734 RVA: 0x00358FF4 File Offset: 0x00357FF4
	private static void ᜀ(XmlWriter A_0, XLSXBorderJoinType A_1)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				goto IL_11C;
			case 1:
				goto IL_11C;
			case 2:
				num = 1;
				continue;
			case 3:
				switch (A_1)
				{
				case XLSXBorderJoinType.Round:
					text = RecordTableEnumerator.b("䨷唹䤻倽␿", a_);
					num = 0;
					continue;
				case XLSXBorderJoinType.Bevel:
					text = RecordTableEnumerator.b("娷弹䨻嬽ⰿ", a_);
					num = 7;
					continue;
				case XLSXBorderJoinType.Mitter:
					text = RecordTableEnumerator.b("唷匹䠻嬽㈿", a_);
					num = 8;
					continue;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				break;
			case 4:
				if (text != null)
				{
					num = 9;
					continue;
				}
				return;
			case 6:
				goto IL_5A;
			case 7:
				goto IL_11C;
			case 8:
				goto IL_11C;
			case 9:
				if (true)
				{
				}
				A_0.WriteElementString(text, RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_), string.Empty);
				num = 10;
				continue;
			case 10:
				return;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			text = null;
			IL_9B:
			num = 3;
			continue;
			IL_11C:
			num = 4;
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
	}

	// Token: 0x060054E7 RID: 21735 RVA: 0x0035916C File Offset: 0x0035816C
	private static void ᜀ(XmlWriter A_0, Image A_1, sprវ A_2, RelationsCollection A_3, bool A_4)
	{
		int a_ = 18;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_272;
			case 1:
				goto IL_333;
			case 2:
				if (A_4)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㭇㹉㹋⭍⑏ㅑ㱓", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
				A_0.WriteElementString(RecordTableEnumerator.b("⹇⍉⁋≍ɏ㝑㝓≕", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_), string.Empty);
				A_0.WriteEndElement();
				num = 6;
				continue;
			case 3:
				goto IL_16E;
			case 4:
				A_0.WriteStartElement(RecordTableEnumerator.b("㱇⍉⁋⭍", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㉉", a_), RecordTableEnumerator.b("硇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉", a_), RecordTableEnumerator.b("硇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㉉", a_), RecordTableEnumerator.b("祇穉籋繍恏扑", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㍉", a_), RecordTableEnumerator.b("祇穉籋繍恏扑", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⹇♉╋㹍", a_), RecordTableEnumerator.b("♇╉≋⭍", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⥇♉⭋⁍", a_), RecordTableEnumerator.b("㱇♉", a_));
				A_0.WriteEndElement();
				num = 0;
				continue;
			case 5:
				goto IL_14D;
			case 6:
				goto IL_315;
			case 7:
			{
				if (A_3 == null)
				{
					num = 5;
					continue;
				}
				string arg = A_2.ᜀ(A_1, null);
				string text = A_3.GenerateRelationId();
				A_3[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﮑ\udc97ﾛ춟잡쪣튥螧颩鲫麭蚯鶱욳펵풷\udbb9좻ힽ꾿곁럃껅ꇇ뫉뿋맏뿑뗓뇕뷗", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("⩇♉╋㹍ᙏ㭑㡓㩕", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("⩇♉╋㹍", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⵇ❉⹋⭍㑏", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﮑ\udc97ﾛ춟잡쪣튥螧颩鲫麭蚯鶱욳펵풷\udbb9좻ힽ꾿곁럃껅ꇇ뫉뿋", a_), text);
				A_0.WriteEndElement();
				goto IL_112;
			}
			case 8:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 10;
				continue;
			case 10:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			case 11:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_112;
				default:
					goto IL_28D;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 8;
			continue;
			IL_112:
			num = 2;
		}
		IL_14D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⁋⽍⑏㭑㭓㡕⭗", a_));
		IL_16E:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⅇ❉ⵋ⥍㕏", a_));
		IL_272:
		goto IL_35D;
		IL_28D:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_315:
		goto IL_35D;
		IL_333:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁇╉⁋⩍㕏⁑", a_));
		IL_35D:
		A_0.WriteEndElement();
	}

	// Token: 0x060054E8 RID: 21736 RVA: 0x003594DC File Offset: 0x003584DC
	private static void ᜀ(XmlWriter A_0, IShapeFill A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 8;
				Image a_2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_CF;
					case 1:
						goto IL_1A2;
					case 2:
						if (A_2 == null)
						{
							num = 11;
							continue;
						}
						num = 4;
						continue;
					case 3:
						if (A_1 == null)
						{
							num = 9;
							continue;
						}
						num = 2;
						continue;
					case 4:
					{
						if (A_3 == null)
						{
							num = 0;
							continue;
						}
						GradientTextureType texture = A_1.Texture;
						num = 6;
						continue;
					}
					case 5:
						goto IL_93;
					case 6:
					{
						GradientTextureType texture;
						if (texture != GradientTextureType.UserDefined)
						{
							num = 7;
							continue;
						}
						a_2 = A_1.Picture;
						num = 1;
						continue;
					}
					case 7:
					{
						string str = RecordTableEnumerator.b("款⑀㭂ㅄ", a_);
						GradientTextureType texture;
						int num2 = (int)texture;
						byte[] resData = XlsShapeFill.GetResData(str + num2.ToString());
						byte[] array = new byte[resData.Length - 25];
						Array.Copy(resData, 25, array, 0, array.Length);
						MemoryStream memoryStream = new MemoryStream();
						XlsShapeFill.ᜀ(memoryStream, resData);
						memoryStream.Write(array, 0, array.Length);
						a_2 = spr\u17FF.ᜀ(memoryStream);
						num = 10;
						continue;
					}
					case 9:
						goto IL_F2;
					case 10:
						goto IL_162;
					case 11:
						goto IL_1C2;
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
				IL_93:
				break;
				IL_CF:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⽂⑄㍆⁈⑊⍌㱎", a_));
				IL_F2:
				throw new ArgumentNullException(RecordTableEnumerator.b("朾ⵀあ̈́⹆╈❊", a_));
				IL_162:
				IL_1A2:
				goto IL_1EC;
				IL_1C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("圾⹀⽂⅄≆㭈", a_));
				IL_1EC:
				spr\u1CFF.ᜀ(A_0, a_2, A_2, A_3, (A_1 as spr\u1C26).ᜂ());
				return;
			}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		}
	}

	// Token: 0x060054E9 RID: 21737 RVA: 0x003596EC File Offset: 0x003586EC
	private static void ᜀ(XmlWriter A_0, IShapeFill A_1, IWorkbook A_2)
	{
		switch (0)
		{
		default:
		{
			GradientStops gradientStops;
			spr\u208B spr_u208B;
			for (;;)
			{
				XlsShapeFill xlsShapeFill = (XlsShapeFill)A_1;
				gradientStops = xlsShapeFill.GradientStops;
				GradientStops preservedGradient = xlsShapeFill.PreservedGradient;
				spr_u208B = new spr\u208B();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						goto IL_C0;
					case 2:
						goto IL_10C;
					case 3:
						goto IL_11A;
					case 4:
						if (gradientStops == null)
						{
							num = 0;
							continue;
						}
						goto IL_C0;
					case 5:
						num = 8;
						continue;
					case 6:
						if (preservedGradient != null)
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						goto IL_11C;
					case 7:
						if ((A_1 as spr\u1C26).ᜄ())
						{
							num = 1;
							continue;
						}
						goto IL_10C;
					case 8:
						if (preservedGradient[0].Position <= 10000)
						{
							goto IL_11C;
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
							num = 2;
							continue;
						}
						break;
					}
					break;
					IL_C0:
					num = 6;
					continue;
					IL_10C:
					gradientStops = preservedGradient;
					num = 3;
				}
			}
			IL_11A:
			IL_11C:
			spr_u208B.ᜁ(A_0, gradientStops, A_2);
			return;
		}
		}
	}

	// Token: 0x060054EA RID: 21738 RVA: 0x00359820 File Offset: 0x00358820
	private void ᜅ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
	}

	// Token: 0x060054EB RID: 21739 RVA: 0x00359878 File Offset: 0x00358878
	private static void ᜃ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, double A_3)
	{
		int a_ = 8;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_46;
				case 1:
					goto IL_C3;
				case 2:
					return;
				case 4:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					num = 5;
					continue;
				case 5:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_C5;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 4;
				}
			}
			IL_46:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8F;
			}
		}
		return;
		IL_8F:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_C3:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("尽⼿ⵁ⽃", a_));
		IL_C5:
		A_0.WriteStartElement(RecordTableEnumerator.b("䨽㠿ቁ㙃", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝쎟쪡얣풥\udca7", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("尽⼿♁㵃ᙅ㩇", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("刽㌿㙁ᝃ㉅ㅇ♉⥋", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("丽", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("丽ဿぁ", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
		spr\u1CFF.ᜀ(A_0, A_1, RecordTableEnumerator.b("娽┿⑁ᙃᙅ㩇", a_), A_2, A_3);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060054EC RID: 21740 RVA: 0x00359A2C File Offset: 0x00358A2C
	internal static void ᜂ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, double A_3)
	{
		int a_ = 15;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_13E;
			case 1:
				if (A_1 is XlsChartDataLabels)
				{
					num = 7;
					continue;
				}
				goto IL_1A5;
			case 2:
				spr\u1CFF.ᜂ(A_0, A_1);
				num = 14;
				continue;
			case 3:
				goto IL_E7;
			case 4:
				if ((A_1 as XlsChartDataLabels).HasFormula)
				{
					num = 17;
					continue;
				}
				goto IL_1A5;
			case 5:
				goto IL_1A5;
			case 6:
				goto IL_1A5;
			case 7:
				num = 4;
				continue;
			case 8:
			{
				bool flag;
				if (flag)
				{
					num = 2;
					continue;
				}
				spr\u1CFF.ᜀ(A_0, A_1, A_2, RecordTableEnumerator.b("㝄⹆⩈⍊", a_), A_3);
				if (true)
				{
				}
				num = 0;
				continue;
			}
			case 9:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("ㅄ㽆", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쒦솨쪪\udfac\udbae", a_));
				bool flag = false;
				num = 15;
				continue;
			}
			case 10:
				num = 12;
				continue;
			case 11:
				goto IL_6C;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_191;
				default:
					if (false)
					{
					}
					if ((A_1 as XlsChartTextArea).IsFormula)
					{
						num = 13;
						continue;
					}
					goto IL_80;
				}
				break;
			case 13:
			{
				bool flag = true;
				num = 5;
				continue;
			}
			case 14:
				goto IL_C6;
			case 15:
				if (A_1 is XlsChartTextArea)
				{
					num = 10;
					continue;
				}
				goto IL_80;
			case 17:
			{
				bool flag = true;
				num = 6;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 9;
			continue;
			IL_80:
			num = 1;
			continue;
			IL_1A5:
			num = 8;
		}
		IL_6C:
		goto IL_191;
		IL_C6:
		goto IL_21F;
		IL_E7:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅄ≆ㅈ㽊ౌ㵎㑐㉒", a_));
		IL_13E:
		goto IL_21F;
		IL_191:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_21F:
		A_0.WriteEndElement();
	}

	// Token: 0x060054ED RID: 21741 RVA: 0x00359C60 File Offset: 0x00358C60
	public static void ᜀ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, string A_3, double A_4)
	{
		int a_ = 0;
		int num = 3;
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
				goto IL_A1;
			case 1:
				goto IL_8B;
			case 2:
				goto IL_3C;
			}
			IL_29:
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 2;
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
				num = 0;
				continue;
			}
			goto IL_29;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻缽㈿❁╃", a_));
		IL_A1:
		A_0.WriteStartElement(A_3, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_));
		spr\u1CFF.ᜄ(A_0, A_1);
		spr\u1CFF.ᜃ(A_0, A_1);
		spr\u1CFF.ᜁ(A_0, A_1, A_2, A_4);
		A_0.WriteEndElement();
	}

	// Token: 0x060054EE RID: 21742 RVA: 0x00359D44 File Offset: 0x00358D44
	private static void ᜄ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 12;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_138;
			case 1:
			{
				sprᮟ sprᮟ;
				int num2 = sprᮟ.TextRotationAngle * 60000;
				A_0.WriteAttributeString(RecordTableEnumerator.b("ぁ⭃㉅", a_), num2.ToString());
				num = 4;
				continue;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_71;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					sprᮟ sprᮟ;
					if (sprᮟ.ᜂ())
					{
						num = 1;
						continue;
					}
					goto IL_13D;
				}
				}
				break;
			case 3:
			{
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⁁⭃≅ㅇᩉ㹋", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ慎ﮋﺏ煉歹랗ꢙ겛꺝隟趡즣장솧쒩", a_));
				sprᮟ sprᮟ = A_1 as XlsChartTextArea;
				goto IL_71;
			}
			case 4:
				goto IL_11A;
			case 5:
				goto IL_43;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 3;
			continue;
			IL_71:
			num = 2;
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_11A:
		goto IL_13D;
		IL_138:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙁⅃㹅㱇୉㹋⭍ㅏ", a_));
		IL_13D:
		A_0.WriteEndElement();
	}

	// Token: 0x060054EF RID: 21743 RVA: 0x00359E94 File Offset: 0x00358E94
	private static void ᜃ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 11;
		for (;;)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3C;
				case 1:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_7B;
				case 3:
					goto IL_65;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			IL_7B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_91;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕀♂㵄㍆ࡈ㥊⡌⹎", a_));
		IL_91:
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("ⵀあㅄᑆ㵈㉊⅌⩎", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x060054F0 RID: 21744 RVA: 0x00359F6C File Offset: 0x00358F6C
	private static void ᜁ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, double A_3)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				int num2;
				int num3;
				string[] array;
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					if ((A_1 as XlsChartTextArea).ChartAlRuns != null)
					{
						num = 5;
						continue;
					}
					goto IL_12C;
				case 2:
					if (A_1 is XlsChartTextArea)
					{
						num = 15;
						continue;
					}
					goto IL_12C;
				case 3:
					goto IL_D9;
				case 4:
					goto IL_1F5;
				case 5:
					num = 12;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F5;
					default:
						if (false)
						{
						}
						goto IL_159;
					}
					break;
				case 7:
					if (num2 >= num3)
					{
						num = 8;
						continue;
					}
					spr\u1CFF.ᜀ(A_0, A_1, array[num2], A_2, A_3);
					num2++;
					num = 6;
					continue;
				case 8:
					return;
				case 9:
					goto IL_75;
				case 10:
					if ((A_1 as XlsChartTextArea).ChartAlRuns.ᜀ().Length > 0)
					{
						num = 4;
						continue;
					}
					goto IL_12C;
				case 11:
					goto IL_159;
				case 12:
					if ((A_1 as XlsChartTextArea).ChartAlRuns.ᜀ() != null)
					{
						num = 0;
						continue;
					}
					goto IL_12C;
				case 13:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 15:
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 13;
				continue;
				IL_12C:
				array = A_1.Text.Split(new char[]
				{
					'\n'
				});
				num2 = 0;
				num3 = array.Length;
				num = 11;
				continue;
				IL_159:
				num = 7;
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			IL_D9:
			throw new ArgumentNullException(RecordTableEnumerator.b("㵈⹊㕌㭎ၐ⅒ご㙖", a_));
			IL_1F5:
			spr\u1CFF.ᜀ(A_0, A_1, A_2, A_3);
			return;
		}
		}
	}

	// Token: 0x060054F1 RID: 21745 RVA: 0x0035A19C File Offset: 0x0035919C
	private static void ᜀ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, double A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				int length;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_23C;
				case 1:
					if (num2 < (A_1 as XlsChartTextArea).ChartAlRuns.ᜀ().Length - 1)
					{
						num = 11;
						continue;
					}
					length = A_1.Text.Length - (int)(A_1 as XlsChartTextArea).ChartAlRuns.ᜀ()[num2].ᜂ();
					num = 10;
					continue;
				case 2:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					if (true)
					{
					}
					num = 9;
					continue;
				case 3:
					goto IL_23C;
				case 5:
					goto IL_33E;
				case 6:
					if (num2 >= (A_1 as XlsChartTextArea).ChartAlRuns.ᜀ().Length)
					{
						num = 8;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㩇", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
					num3 = 0;
					length = 0;
					num3 = (int)(A_1 as XlsChartTextArea).ChartAlRuns.ᜀ()[num2].ᜂ();
					num = 1;
					continue;
				case 7:
					goto IL_70;
				case 8:
					goto IL_26C;
				case 9:
					if (A_2 == null)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67;
					default:
						if (false)
						{
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("㡇", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
						A_0.WriteStartElement(RecordTableEnumerator.b("㡇ᩉ㹋", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
						A_0.WriteStartElement(RecordTableEnumerator.b("ⱇ⽉⩋ᱍO⁑", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						num2 = 0;
						num = 3;
						continue;
					}
					break;
				case 10:
					goto IL_163;
				case 11:
					length = (int)(A_1 as XlsChartTextArea).ChartAlRuns.ᜀ()[num2 + 1].ᜂ() - num3;
					num = 12;
					continue;
				case 12:
					goto IL_163;
				case 13:
					goto IL_15E;
				}
				goto IL_61;
				IL_67:
				num = 7;
				continue;
				IL_61:
				if (A_0 == null)
				{
					goto IL_67;
				}
				num = 2;
				continue;
				IL_163:
				string text = A_1.Text.Substring(num3, length);
				(A_1 as XlsChartTextArea).ᜀ((int)(A_1 as XlsChartTextArea).ChartAlRuns.ᜀ()[num2].ᜀ());
				spr\u1CFF.ᜀ(A_0, A_1, RecordTableEnumerator.b("㩇ᩉ㹋", a_), A_2, A_3);
				A_0.WriteStartElement(RecordTableEnumerator.b("㱇", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧잩춫잭\udeaf", a_));
				A_0.WriteString(text);
				A_0.WriteEndElement();
				A_0.WriteEndElement();
				num2++;
				num = 0;
				continue;
				IL_23C:
				num = 6;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_15E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍ᅏ⁑ㅓ㝕", a_));
			IL_26C:
			A_0.WriteEndElement();
			return;
			IL_33E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩇╉⍋╍", a_));
		}
		}
	}

	// Token: 0x060054F2 RID: 21746 RVA: 0x0035A4F4 File Offset: 0x003594F4
	private static void ᜀ(XmlWriter A_0, IChartTextArea A_1, string A_2, IWorkbook A_3, double A_4)
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
		A_0.WriteStartElement(RecordTableEnumerator.b("䠷", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("䠷樹主", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("尷弹娻氽ဿぁ", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("䨷", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
		spr\u1CFF.ᜀ(A_0, A_1, RecordTableEnumerator.b("䨷樹主", a_), A_3, A_4);
		A_0.WriteStartElement(RecordTableEnumerator.b("䰷", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗ﶛ캟", a_));
		A_0.WriteString(A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060054F3 RID: 21747 RVA: 0x0035A620 File Offset: 0x00359620
	public static void ᜀ(XmlWriter A_0, IFont A_1, string A_2, IWorkbook A_3, double A_4)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 55;
			for (;;)
			{
				string text;
				int num2;
				string text2;
				IInternalFont internalFont;
				string text3;
				switch (num)
				{
				case 0:
					goto IL_444;
				case 1:
					goto IL_658;
				case 2:
					if (A_1.Underline != FontUnderlineType.Single)
					{
						num = 52;
						continue;
					}
					num = 58;
					continue;
				case 3:
					num = 21;
					continue;
				case 4:
					text = RecordTableEnumerator.b("瑄", a_);
					goto IL_255;
				case 5:
					goto IL_1F6;
				case 6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㍆㭈≊♌⩎", a_), RecordTableEnumerator.b("㙄⥆⹈ᡊ㥌㵎㡐㡒ご", a_));
					num = 42;
					continue;
				case 7:
					num = 43;
					continue;
				case 8:
				{
					string language;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥄♆❈ⱊ", a_), language);
					num = 56;
					continue;
				}
				case 9:
				{
					string language;
					if (language != null)
					{
						num = 8;
						continue;
					}
					goto IL_493;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_178;
					default:
						if (false)
						{
						}
						goto IL_468;
					}
					break;
				case 11:
					if (num2 != 0)
					{
						num = 47;
						continue;
					}
					goto IL_1F6;
				case 12:
					if (A_1.Underline != FontUnderlineType.None)
					{
						num = 28;
						continue;
					}
					goto IL_468;
				case 13:
					num2 = 30000;
					num = 0;
					continue;
				case 14:
					num = 16;
					continue;
				case 15:
					goto IL_6E0;
				case 16:
					text2 = RecordTableEnumerator.b("畄", a_);
					goto IL_548;
				case 17:
					goto IL_1D2;
				case 18:
					if (!A_1.IsItalic)
					{
						num = 7;
						continue;
					}
					goto IL_152;
				case 19:
					if (A_1.FontName != RecordTableEnumerator.b("ل♆╈≊⽌㵎㡐", a_))
					{
						num = 37;
						continue;
					}
					goto IL_852;
				case 20:
				{
					string language = internalFont.Font.Language;
					num = 9;
					continue;
				}
				case 21:
					if (internalFont.Font.\u1716.Bold != null)
					{
						num = 15;
						continue;
					}
					goto IL_690;
				case 22:
					goto IL_6B8;
				case 23:
					if (internalFont != null)
					{
						num = 20;
						continue;
					}
					goto IL_493;
				case 24:
					goto IL_12E;
				case 25:
					if (internalFont.Font.\u1716 != null)
					{
						num = 3;
						continue;
					}
					goto IL_690;
				case 26:
					if (!A_1.IsAutoColor)
					{
						num = 31;
						continue;
					}
					goto IL_77C;
				case 27:
					goto IL_690;
				case 28:
					num = 2;
					continue;
				case 29:
					num = 59;
					continue;
				case 30:
					if (!A_1.IsItalic)
					{
						num = 14;
						continue;
					}
					num = 46;
					continue;
				case 31:
					A_0.WriteStartElement(RecordTableEnumerator.b("㙄⡆╈≊⥌ॎ㡐㽒㥔", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					spr\u1CFF.ᜀ(A_0, A_1.Color);
					A_0.WriteEndElement();
					num = 53;
					continue;
				case 32:
					if (A_1.IsSuperscript)
					{
						num = 13;
						continue;
					}
					goto IL_444;
				case 33:
					text3 = RecordTableEnumerator.b("⅄╆╈", a_);
					goto IL_80D;
				case 34:
					text = RecordTableEnumerator.b("畄", a_);
					goto IL_255;
				case 35:
					goto IL_37C;
				case 36:
					num = 25;
					continue;
				case 37:
					A_0.WriteStartElement(RecordTableEnumerator.b("⥄♆㵈≊⍌", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ㅄ㹆㥈⹊⭌⹎㉐㙒", a_), A_1.FontName);
					A_0.WriteEndElement();
					A_0.WriteStartElement(RecordTableEnumerator.b("⁄♆", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ㅄ㹆㥈⹊⭌⹎㉐㙒", a_), A_1.FontName);
					A_0.WriteEndElement();
					A_0.WriteStartElement(RecordTableEnumerator.b("♄㑆", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ㅄ㹆㥈⹊⭌⹎㉐㙒", a_), A_1.FontName);
					A_0.WriteEndElement();
					num = 35;
					continue;
				case 38:
					if (A_1.IsSubscript)
					{
						num = 44;
						continue;
					}
					goto IL_6B8;
				case 39:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 57;
					continue;
				case 40:
					goto IL_615;
				case 41:
					if (A_1.IsStrikethrough)
					{
						num = 6;
						continue;
					}
					goto IL_4F2;
				case 42:
					goto IL_4F2;
				case 43:
					if (internalFont.Font.\u1716 != null)
					{
						num = 29;
						continue;
					}
					goto IL_615;
				case 44:
					num2 = -25000;
					num = 22;
					continue;
				case 45:
					num = 34;
					continue;
				case 46:
					text2 = RecordTableEnumerator.b("瑄", a_);
					goto IL_548;
				case 47:
					A_0.WriteAttributeString(RecordTableEnumerator.b("❄♆㩈⹊⅌♎㽐㙒", a_), num2.ToString());
					num = 5;
					continue;
				case 48:
					if (A_2.Length == 0)
					{
						num = 17;
						continue;
					}
					A_0.WriteStartElement(A_2, RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쪦좨슪쎬", a_));
					num = 54;
					continue;
				case 49:
					if (!A_1.IsBold)
					{
						num = 36;
						continue;
					}
					goto IL_6E0;
				case 50:
					goto IL_152;
				case 51:
					num = 48;
					continue;
				case 52:
					num = 33;
					continue;
				case 53:
					goto IL_77C;
				case 54:
					if (!A_1.IsBold)
					{
						num = 45;
						continue;
					}
					goto IL_178;
				case 56:
					goto IL_493;
				case 57:
					if (A_2 != null)
					{
						num = 51;
						continue;
					}
					goto IL_578;
				case 58:
					text3 = RecordTableEnumerator.b("㙄⥆⹈", a_);
					goto IL_80D;
				case 59:
					if (internalFont.Font.\u1716.Italic != null)
					{
						num = 50;
						continue;
					}
					goto IL_615;
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				num = 39;
				continue;
				IL_152:
				string value;
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⱄ", a_), value);
				num = 40;
				continue;
				IL_178:
				num = 4;
				continue;
				IL_1F6:
				num = 26;
				continue;
				IL_255:
				string value2 = text;
				num = 30;
				continue;
				IL_444:
				num = 11;
				continue;
				IL_468:
				num2 = 0;
				num = 38;
				continue;
				IL_493:
				num = 49;
				continue;
				IL_4F2:
				int num3 = (int)(A_1.Size * 100.0);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㙄㵆", a_), num3.ToString());
				num = 12;
				continue;
				IL_548:
				value = text2;
				internalFont = (A_1 as IInternalFont);
				if (true)
				{
				}
				num = 23;
				continue;
				IL_615:
				num = 41;
				continue;
				IL_690:
				num = 18;
				continue;
				IL_6B8:
				num = 32;
				continue;
				IL_6E0:
				A_0.WriteAttributeString(RecordTableEnumerator.b("❄", a_), value2);
				num = 27;
				continue;
				IL_77C:
				num = 19;
				continue;
				IL_80D:
				string value3 = text3;
				A_0.WriteAttributeString(RecordTableEnumerator.b("い", a_), value3);
				num = 10;
			}
			IL_12E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
			IL_1D2:
			goto IL_578;
			IL_37C:
			goto IL_852;
			IL_578:
			throw new ArgumentException(RecordTableEnumerator.b("⡄♆⁈╊᥌⹎㙐ᵒ㑔㩖㱘", a_));
			IL_658:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅄ≆ㅈ㽊ౌ㵎㑐㉒", a_));
			IL_852:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054F4 RID: 21748 RVA: 0x0035AE88 File Offset: 0x00359E88
	private static void ᜂ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 5;
		string text;
		for (;;)
		{
			text = A_1.Text;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (text[0] == '=')
					{
						num = 2;
						continue;
					}
					goto IL_B1;
				case 1:
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					text = UtilityMethods.ᜀ(text);
					num = 4;
					continue;
				case 3:
					goto IL_3F;
				case 4:
					goto IL_5A;
				}
				break;
			}
		}
		IL_3F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5A:
			break;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		}
		IL_B1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䠺䤼䴾ፀ♂⍄", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺜ삠톢톤", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("崺", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺜ삠톢톤", a_), text);
		A_0.WriteElementString(RecordTableEnumerator.b("䠺䤼䴾ɀ≂♄⽆ⱈ", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺜ삠톢톤", a_), string.Empty);
		A_0.WriteEndElement();
	}

	// Token: 0x060054F5 RID: 21749 RVA: 0x0035AFB8 File Offset: 0x00359FB8
	internal static void ᜁ(XmlWriter A_0, IChartTextArea A_1)
	{
		Stream stream;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_65:
			stream.Position = 0L;
			ShapeParser.WriteNodeFromStream(A_0, stream);
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
				if (true)
				{
				}
				if (stream != null)
				{
					num = 0;
					continue;
				}
				return;
			case 2:
				return;
			}
			goto IL_3A;
		}
		IL_63:
		goto IL_65;
		IL_3A:
		sprᮟ sprᮟ = A_1 as sprᮟ;
		stream = sprᮟ.ᜁ();
		num = 1;
		goto IL_28;
	}

	// Token: 0x060054F6 RID: 21750 RVA: 0x0035B044 File Offset: 0x0035A044
	private static void ᜀ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 0;
		int num = 0;
		for (;;)
		{
			Stream stream;
			switch (num)
			{
			case 1:
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 2;
				continue;
			case 2:
				goto IL_5B;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				default:
					goto IL_9C;
				}
				break;
			case 4:
				if (stream != null)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (A_1 == null)
			{
				num = 3;
				continue;
			}
			IL_65:
			stream = ((XlsChartTextArea)A_1).OverlayStream;
			num = 4;
		}
		IL_5B:
		if (true)
		{
		}
		return;
		IL_9C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻缽㈿❁╃", a_));
	}

	// Token: 0x04002900 RID: 10496
	private static Dictionary<ChartLinePatternType, KeyValuePair<string, string>> ᜀ;

	// Token: 0x04002901 RID: 10497
	internal static string[][] ᜁ;

	// Token: 0x04002902 RID: 10498
	internal static string[][] ᜂ;

	// Token: 0x04002903 RID: 10499
	internal static string[][] ᜃ;

	// Token: 0x04002904 RID: 10500
	internal static string[][] ᜄ;

	// Token: 0x04002905 RID: 10501
	internal static string[][] ᜅ;

	// Token: 0x04002906 RID: 10502
	internal static string[][] ᜆ;
}
