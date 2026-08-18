using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200023C RID: 572
internal class spr\u22AC : spr᠙
{
	// Token: 0x0600229F RID: 8863 RVA: 0x00136390 File Offset: 0x00135390
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 18;
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
			string value = '#' + string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓≕⍗橙⅛", a_), A_1.InnerSpRecord.\u1714());
			string value2 = string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓╕⍗橙⅛", a_), A_1.ShapeId);
			A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), value2);
			A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), value);
			List<string> list = new List<string>();
			this.ᜀ(list, A_1);
			spr\u1A65.ᜀ(A_0, list);
			A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㕏㙑", a_), RecordTableEnumerator.b("㱇", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㍏㵑㡓㥕⩗", a_), RecordTableEnumerator.b("㽇⍉≋⩍㽏║瑓ൕ湗潙ś", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑こ", a_), RecordTableEnumerator.b("㱇", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑㝓㥕㑗㕙⹛", a_), RecordTableEnumerator.b("㽇⍉≋⩍㽏║S㍕⁗⹙籛՝噟噡㥣", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("❇灉╋⁍⍏㝑⁓㭕㝗㹙㥛", a_), RecordTableEnumerator.b("⥇㽉㡋⅍", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("㹇", a_), RecordTableEnumerator.b("၇♉㽋ࡍ㥏㹑㡓", a_), null);
			A_0.WriteAttributeString(RecordTableEnumerator.b("⭇╉⁋⅍≏恑", a_), RecordTableEnumerator.b("㽇⍉≋⩍㽏║瑓ൕ湗潙ś", a_));
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_21B;
				case 1:
					if (!A_1.HasBorder)
					{
						goto IL_21D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					A_0.WriteEndElement();
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_21B:
		IL_21D:
		base.ᜀ(A_0, A_1, A_2, string.Empty, true, A_3);
		base.ᜀ(A_0, A_1, RecordTableEnumerator.b("ᡇ⍉⽋㩍", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x001365E8 File Offset: 0x001355E8
	protected override void ᜀ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 14;
		for (;;)
		{
			base.ᜀ(A_0, A_1);
			A_0.WriteElementString(RecordTableEnumerator.b("݃E", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_), RecordTableEnumerator.b("ᑃ⽅⭇㹉", a_));
			A_0.WriteElementString(RecordTableEnumerator.b("Ճ㍅㱇╉᱋❍㍏♑", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_), string.Empty);
			XlsBitmapShape xlsBitmapShape = A_1 as XlsBitmapShape;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (xlsBitmapShape.IsCamera)
					{
						num = 5;
						continue;
					}
					return;
				case 1:
					if (!xlsBitmapShape.IsDDE)
					{
						goto IL_103;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					A_0.WriteElementString(RecordTableEnumerator.b("CɅേ", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_), null);
					num = 3;
					continue;
				case 3:
					goto IL_103;
				case 4:
					return;
				case 5:
					A_0.WriteElementString(RecordTableEnumerator.b("݃❅╇⽉㹋⽍", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_), null);
					num = 4;
					continue;
				}
				break;
				IL_103:
				num = 0;
			}
		}
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x00136754 File Offset: 0x00135754
	private new void ᜀ(List<string> A_0, XlsShape A_1)
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0.Add(RecordTableEnumerator.b("伾⹀あⱄ㍆⁈⑊⍌畎ぐㅒ♔㡖㕘⹚⥜㩞", a_));
		this.ᜀ(A_0, RecordTableEnumerator.b("刾⁀ㅂ≄⹆❈晊⅌⩎㝐❒", a_), A_1.Left);
		this.ᜀ(A_0, RecordTableEnumerator.b("刾⁀ㅂ≄⹆❈晊㥌⁎⅐", a_), A_1.Top);
		this.ᜀ(A_0, RecordTableEnumerator.b("䠾⡀❂ㅄ⽆", a_), A_1.Width);
		this.ᜀ(A_0, RecordTableEnumerator.b("圾⑀⩂≄⽆㵈", a_), A_1.Height);
	}

	// Token: 0x060022A2 RID: 8866 RVA: 0x00136818 File Offset: 0x00135818
	private new void ᜀ(List<string> A_0, string A_1, int A_2)
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		double num = Math.Round(spr\u17FF.ᜀ((double)A_2, MeasureUnits.Point), 2);
		A_0.Add(string.Format(RecordTableEnumerator.b("䄹఻䌽稿㥁畃㭅㡇㹉", a_), A_1, num));
	}

	// Token: 0x060022A3 RID: 8867 RVA: 0x0013688C File Offset: 0x0013588C
	protected override string ᜀ(XlsShape A_0, sprᡟ A_1, bool A_2, RelationsCollection A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 3;
			Image picture;
			int blipId;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4D;
				case 1:
					goto IL_58;
				case 2:
					num = 1;
					continue;
				case 4:
					if (true)
					{
					}
					if (!A_2)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 5:
					goto IL_DF;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					XlsBitmapShape xlsBitmapShape = A_0 as XlsBitmapShape;
					picture = xlsBitmapShape.Picture;
					blipId = (int)xlsBitmapShape.BlipId;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BF;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
				}
			}
			IL_4D:
			goto IL_BF;
			IL_58:
			ImageFormat imageFormat = ImageFormat.Png;
			goto IL_E7;
			IL_BF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽", a_));
			IL_DF:
			imageFormat = picture.RawFormat;
			IL_E7:
			ImageFormat a_2 = imageFormat;
			sprវ sprវ = A_1.ᜋ();
			A_1.ᜋ().ᜁ(a_2);
			string arg = sprវ.ᜃ(blipId - 1);
			string text = A_3.GenerateRelationId();
			A_3[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹鎻ힽ궿ꏁꏃꏅ", a_));
			return text;
		}
		}
	}
}
