using System;
using System.IO;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020004ED RID: 1261
internal class sprᥑ : spr\u2175
{
	// Token: 0x06004D41 RID: 19777 RVA: 0x002F1F00 File Offset: 0x002F0F00
	public sprᥑ(Stream A_0)
	{
		int a_ = 11;
		base..ctor();
		if (A_0 != null)
		{
			if (A_0.Length != 0L)
			{
				this.ᜀ = A_0;
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉀⭂⑄㝆ⱈὊ㑌㽎㑐R⅔╖㱘㩚ぜ", a_));
	}

	// Token: 0x06004D42 RID: 19778 RVA: 0x002F1F48 File Offset: 0x002F0F48
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DC;
			case 2:
				goto IL_3C;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DC;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				}
				break;
			case 4:
				if (A_2 == null)
				{
					num = 5;
					continue;
				}
				goto IL_DE;
			case 5:
				goto IL_5C;
			}
			if (A_0 == null)
			{
				num = 2;
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
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⽆♈❊⥌⩎⍐", a_));
		IL_DC:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⩊㵌⩎", a_));
		IL_DE:
		Stream xmlDataStream = A_1.XmlDataStream;
		xmlDataStream.Position = 0L;
		XmlReader reader = UtilityMethods.ᜀ(xmlDataStream);
		A_0.WriteNode(reader, false);
		A_0.Flush();
	}

	// Token: 0x06004D43 RID: 19779 RVA: 0x002F2058 File Offset: 0x002F1058
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int a_ = 19;
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
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			}
			break;
		}
		this.ᜀ.Position = 0L;
		XmlReader reader = UtilityMethods.ᜀ(this.ᜀ);
		A_0.WriteNode(reader, false);
		A_0.Flush();
	}

	// Token: 0x04002321 RID: 8993
	private new Stream ᜀ;
}
