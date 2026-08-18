using System;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200046D RID: 1133
internal class spr\u181F : ShapeParser
{
	// Token: 0x0600457B RID: 17787 RVA: 0x002A66F8 File Offset: 0x002A56F8
	public virtual XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 10;
		for (;;)
		{
			IL_09:
			int num = 5;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㌿㉁ぃ", a_), RecordTableEnumerator.b("㔿ぁ⩃籅㭇⥉⑋⭍㵏㍑❓筕㕗㍙㽛ⱝཟᅡୣeᱧ䝩ཫŭᵯ䡱᭳ၵṷ፹ύ᭽멿", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_EE;
					case 1:
						goto IL_EC;
					case 2:
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
					case 3:
						goto IL_99;
					case 4:
						goto IL_60;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 4;
					}
					else
					{
						num = 2;
					}
					break;
				}
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_99:
		throw new XmlException();
		IL_EC:
		throw new ArgumentNullException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_));
		IL_EE:
		int a_2 = int.Parse(A_0.Value);
		A_0.MoveToElement();
		this.ᜀ(a_2, A_0, A_1);
		return new XlsShape(A_1.AppImplementation, A_1);
	}

	// Token: 0x0600457C RID: 17788 RVA: 0x002A6820 File Offset: 0x002A5820
	public virtual bool ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			for (;;)
			{
				XlsShape xlsShape = (XlsShape)A_1.Clone(A_1.Parent);
				memoryStream = new MemoryStream();
				XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
				xmlWriter.WriteNode(A_0, false);
				xmlWriter.Flush();
				xlsShape.XmlDataStream = memoryStream;
				memoryStream.Position = 0L;
				A_0 = UtilityMethods.ᜀ(memoryStream);
				A_0.Read();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string value = A_0.Value;
						xlsShape.ImageRelation = (sprᦨ)A_2[value].ᜁ();
						xlsShape.ImageRelationId = value;
						num = 1;
						continue;
					}
					case 1:
						goto IL_1E8;
					case 2:
						num = 4;
						continue;
					case 3:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("似娾ⵀ⩂⅄", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼ၾ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_1EA;
					case 4:
						if (A_0.LocalName == RecordTableEnumerator.b("吼刾⁀⑂⁄⍆⡈㽊ⱌ", a_))
						{
							num = 10;
							continue;
						}
						goto IL_B7;
					case 5:
						goto IL_17A;
					case 6:
						goto IL_15A;
					case 7:
						goto IL_15A;
					case 8:
						if (A_0.NodeType != XmlNodeType.Element)
						{
							goto IL_B7;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CF;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 9:
						if (A_0.NodeType == XmlNodeType.None)
						{
							num = 5;
							continue;
						}
						num = 8;
						continue;
					case 10:
						goto IL_CF;
					}
					break;
					IL_B7:
					A_0.Read();
					num = 6;
					continue;
					IL_CF:
					if (true)
					{
					}
					num = 3;
					continue;
					IL_15A:
					num = 9;
				}
			}
			IL_17A:
			IL_1E8:
			IL_1EA:
			memoryStream.Position = 0L;
			return true;
		}
		}
	}

	// Token: 0x0600457D RID: 17789 RVA: 0x002A6A20 File Offset: 0x002A5A20
	private void ᜀ(int A_0, XmlReader A_1, ShapeCollectionBase A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					goto IL_4F;
				case 2:
					if (A_2 == null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_B4;
				}
				if (A_1 != null)
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
						continue;
					}
				}
				num = 1;
			}
			IL_4F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_9E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
			IL_B4:
			MemoryStream a_2 = new MemoryStream();
			XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2, Encoding.UTF8);
			xmlWriter.WriteNode(A_1, false);
			xmlWriter.Flush();
			XlsWorksheetBase worksheetBase = A_2.WorksheetBase;
			sprᥑ value = new sprᥑ(a_2);
			worksheetBase.DataHolder.ᜋ().\u170D().ᜄ()[A_0] = value;
			worksheetBase.UnknownVmlShapes = true;
			return;
		}
		}
	}
}
