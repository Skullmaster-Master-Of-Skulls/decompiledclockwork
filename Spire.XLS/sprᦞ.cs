using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000298 RID: 664
internal class sprᦞ : ShapeParser
{
	// Token: 0x06002710 RID: 10000 RVA: 0x00162518 File Offset: 0x00161518
	static sprᦞ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᦞ.ᜁ = new Dictionary<string, ShapeParser>();
		sprᦞ.ᜁ.Add(RecordTableEnumerator.b("ɀ⭂⁄⑆≈⥊≌㝎", a_), new spr\u1DCB());
		sprᦞ.ᜁ.Add(RecordTableEnumerator.b("ፀ≂⅄⹆♈", a_), new spr\u229F());
		sprᦞ.ᜁ.Add(RecordTableEnumerator.b("Հㅂ⩄㝆", a_), new spr\u230B());
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x001625C0 File Offset: 0x001615C0
	public virtual XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 15;
		if (!A_0.MoveToAttribute(RecordTableEnumerator.b("ⱄ⍆", a_)))
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
				throw new XmlException();
			}
		}
		string value = A_0.Value;
		A_0.MoveToElement();
		Stream stream = ShapeParser.ReadNodeAsStream(A_0);
		this.ᜀ[value] = stream;
		return new XlsShape(A_1.AppImplementation, A_1)
		{
			XmlTypeStream = stream
		};
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x0016265C File Offset: 0x0016165C
	public virtual bool ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 17;
			for (;;)
			{
				string text;
				string text2;
				ShapeParser shapeParser;
				Stream stream2;
				bool result;
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					goto IL_10F;
				case 2:
					num = 15;
					continue;
				case 3:
					text = A_0.Value;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11E;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 4:
					goto IL_91;
				case 5:
					goto IL_11E;
				case 6:
					goto IL_8C;
				case 7:
					num = 14;
					continue;
				case 8:
					goto IL_91;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("猻尽⨿❁❃㉅᱇㍉㱋⭍", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_91;
				case 10:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("缻刽⤿❁⩃㉅ే⭉㡋⽍", a_))
					{
						num = 0;
						continue;
					}
					goto IL_292;
				}
				case 11:
					goto IL_91;
				case 12:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 7;
						continue;
					}
					A_0.Skip();
					num = 4;
					continue;
				case 13:
				{
					text2 = UtilityMethods.ᜀ(text2);
					Stream stream = this.ᜀ[text2];
					stream.Position = 0L;
					UtilityMethods.ᜀ(stream);
					A_1 = shapeParser.ParseShapeType(A_0, A_1.ParentShapes);
					stream2.Position = 0L;
					A_0 = UtilityMethods.ᜀ(stream2);
					result = shapeParser.ParseShape(A_0, A_1, A_2, A_3);
					num = 18;
					continue;
				}
				case 14:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 20;
						continue;
					}
					goto IL_292;
				}
				case 15:
					if (text != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 12;
					continue;
				case 16:
					goto IL_91;
				case 18:
					return result;
				case 19:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 2;
						continue;
					}
					goto IL_10F;
				case 20:
					num = 10;
					continue;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				stream2 = ShapeParser.ReadNodeAsStream(A_0);
				stream2.Position = 0L;
				A_0 = UtilityMethods.ᜀ(stream2);
				A_0.MoveToAttribute(RecordTableEnumerator.b("䠻䜽〿❁", a_));
				text2 = A_0.Value;
				A_0.MoveToElement();
				A_0.Read();
				text = null;
				num = 11;
				continue;
				IL_91:
				num = 19;
				continue;
				IL_10F:
				result = false;
				num = 5;
				continue;
				IL_11E:
				if (sprᦞ.ᜁ.TryGetValue(text, out shapeParser))
				{
					num = 13;
					continue;
				}
				return result;
				IL_292:
				A_0.Skip();
				num = 16;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		}
		}
	}

	// Token: 0x04001351 RID: 4945
	private Dictionary<string, Stream> ᜀ = new Dictionary<string, Stream>();

	// Token: 0x04001352 RID: 4946
	private static Dictionary<string, ShapeParser> ᜁ;
}
