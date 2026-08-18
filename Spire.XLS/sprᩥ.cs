using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020003B0 RID: 944
internal abstract class spr\u1A65 : spr\u2175
{
	// Token: 0x06003956 RID: 14678
	protected new abstract int ᜀ();

	// Token: 0x06003957 RID: 14679
	protected new abstract string ᜁ();

	// Token: 0x06003958 RID: 14680 RVA: 0x00200FEC File Offset: 0x001FFFEC
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int a_ = 18;
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
			if (A_0 != null)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏♑ⵓ♕㵗", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
				string value = string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓≕⍗橙⅛", a_), this.ᜀ());
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), value);
				A_0.WriteAttributeString(RecordTableEnumerator.b("⭇╉⍋㱍㑏⅑㵓ⱕ㵗", a_), RecordTableEnumerator.b("穇等穋繍恏繑晓杕湗橙汛", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㩉㡋", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), this.ᜀ().ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("㡇⭉㡋♍", a_), RecordTableEnumerator.b("╇晉⁋才扏捑打晕桗⡙湛潝噟剡呣䩥ѧ塩嵫塭䁯䉱塳๵ᵷ", a_));
				this.ᜀ(A_0);
				A_0.WriteEndElement();
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
	}

	// Token: 0x06003959 RID: 14681 RVA: 0x00201120 File Offset: 0x00200120
	protected new virtual void ᜀ(XmlWriter A_0)
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
	}

	// Token: 0x0600395A RID: 14682 RVA: 0x0020115C File Offset: 0x0020015C
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7C;
				case 1:
				{
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					TextBoxShapeBase textBoxShapeBase = A_1 as TextBoxShapeBase;
					A_0.WriteStartElement(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
					string value = '#' + string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓≕⍗橙⅛", a_), A_1.InnerSpRecord.\u1714());
					string value2 = string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓╕⍗橙⅛", a_), A_1.ShapeId);
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), value2);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), value);
					this.ᜅ(A_0, A_1);
					goto IL_35E;
				}
				case 2:
				{
					TextBoxShapeBase textBoxShapeBase;
					string value3 = base.ᜁ(textBoxShapeBase.Fill.BackColor);
					A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㍏㵑㡓㥕⩗", a_), value3);
					num = 10;
					continue;
				}
				case 3:
				{
					TextBoxShapeBase textBoxShapeBase;
					if (textBoxShapeBase.Line.Weight > 0.0)
					{
						num = 12;
						continue;
					}
					goto IL_390;
				}
				case 4:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㕏㙑", a_), RecordTableEnumerator.b("⹇", a_));
					num = 13;
					continue;
				case 5:
					goto IL_21A;
				case 6:
					goto IL_12D;
				case 7:
					goto IL_132;
				case 8:
				{
					TextBoxShapeBase textBoxShapeBase;
					if (textBoxShapeBase.Line.BackColor != spr\u1D39.ᜂ)
					{
						num = 16;
						continue;
					}
					goto IL_132;
				}
				case 10:
					goto IL_284;
				case 11:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑こ", a_), RecordTableEnumerator.b("⹇", a_));
					num = 8;
					continue;
				case 12:
				{
					TextBoxShapeBase textBoxShapeBase;
					string value4 = textBoxShapeBase.Line.Weight.ToString() + RecordTableEnumerator.b("㡇㹉", a_);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑⍓㍕ㅗ㵙㑛⩝", a_), value4);
					num = 5;
					continue;
				}
				case 13:
				{
					TextBoxShapeBase textBoxShapeBase;
					if (textBoxShapeBase.FillColor != spr\u1D39.ᜂ)
					{
						num = 2;
						continue;
					}
					goto IL_284;
				}
				case 14:
				{
					if (true)
					{
					}
					TextBoxShapeBase textBoxShapeBase;
					if (!textBoxShapeBase.HasLineFormat)
					{
						num = 11;
						continue;
					}
					goto IL_390;
				}
				case 15:
				{
					TextBoxShapeBase textBoxShapeBase;
					if (!textBoxShapeBase.HasFill)
					{
						num = 4;
						continue;
					}
					goto IL_284;
				}
				case 16:
				{
					TextBoxShapeBase textBoxShapeBase;
					string value5 = base.ᜁ(textBoxShapeBase.Line.BackColor);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑㝓㥕㑗㕙⹛", a_), value5);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_35E;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
				IL_132:
				num = 3;
				continue;
				IL_284:
				num = 14;
				continue;
				IL_35E:
				num = 15;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_12D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_));
			IL_21A:
			IL_390:
			this.ᜁ(A_0, A_1);
			A_0.WriteStartElement(RecordTableEnumerator.b("㱇⽉㑋㩍㉏㵑ⱓ", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
			this.ᜆ(A_0, A_1);
			A_0.WriteStartElement(RecordTableEnumerator.b("ⱇ⍉㩋", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㕋≍㕏", a_), RecordTableEnumerator.b("㱇⽉㑋㩍絏㍑㡓㽕㽗㑙晛㉝՟ѡၣ", a_));
			this.ᜂ(A_0, A_1);
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			base.ᜀ(A_0, A_1, this.ᜁ());
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600395B RID: 14683 RVA: 0x0020158C File Offset: 0x0020058C
	protected virtual void ᜄ(XmlWriter A_0, XlsShape A_1)
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
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x002015C8 File Offset: 0x002005C8
	protected new virtual void ᜁ(XmlWriter A_0, XlsShape A_1)
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
	}

	// Token: 0x0600395D RID: 14685 RVA: 0x00201604 File Offset: 0x00200604
	protected void ᜃ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 7;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			}
			if (A_0 != null)
			{
				goto IL_6E;
			}
			num = 0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⁀㍂⁄", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("丼圾⁀❂⩄う", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮ݰṲᥴ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("刼儾", a_), RecordTableEnumerator.b("䤼", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("帼倾ⵀⱂ㝄", a_), RecordTableEnumerator.b("弼匾⁀⁂⹄", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("刼崾㉀⁂い㕆ⱈ⽊", a_), RecordTableEnumerator.b("䤼", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x0600395E RID: 14686 RVA: 0x00201740 File Offset: 0x00200740
	protected virtual void ᜂ(XmlWriter A_0, XlsShape A_1)
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
	}

	// Token: 0x0600395F RID: 14687 RVA: 0x0020177C File Offset: 0x0020077C
	internal void ᜆ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				goto IL_50;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			if (A_0 != null)
			{
				goto IL_6E;
			}
			num = 1;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_8B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹崻丽┿", a_));
		IL_A1:
		List<string> list = new List<string>();
		this.ᜀ(list, A_1);
		string value = UtilityMethods.ᜀ(RecordTableEnumerator.b("̷", a_), list);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䬷丹䔻刽┿", a_), value);
	}

	// Token: 0x06003960 RID: 14688 RVA: 0x00201864 File Offset: 0x00200864
	internal void ᜅ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 17;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_8B;
			}
			if (A_0 != null)
			{
				goto IL_6E;
			}
			num = 0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⩊㵌⩎", a_));
		IL_A1:
		List<string> list = new List<string>();
		this.ᜀ(list, A_1);
		spr\u1A65.ᜀ(A_0, list);
	}

	// Token: 0x06003961 RID: 14689 RVA: 0x00201928 File Offset: 0x00200928
	public new static void ᜀ(XmlWriter A_0, List<string> A_1)
	{
		int a_ = 5;
		for (;;)
		{
			IL_09:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string value = UtilityMethods.ᜀ(RecordTableEnumerator.b(":", a_), A_1);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺䤼䘾ⵀ♂", a_), value);
					num = 3;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					return;
				case 4:
					if (true)
					{
					}
					if (A_1.Count > 0)
					{
						num = 0;
						continue;
					}
					return;
				}
				if (A_1 == null)
				{
					return;
				}
				num = 1;
			}
		}
	}

	// Token: 0x06003962 RID: 14690 RVA: 0x002019F8 File Offset: 0x002009F8
	protected new virtual void ᜀ(List<string> A_0, XlsShape A_1)
	{
		int a_ = 13;
		for (;;)
		{
			A_0.Add(RecordTableEnumerator.b("⹂㙄⡆摈⽊⑌㵎㑐げ⅔㹖㙘㕚灜㹞ൠᝢ彤٦ᱨὪɬ", a_));
			TextBoxShapeBase textBoxShapeBase = A_1 as TextBoxShapeBase;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					TextRotationType textRotation = textBoxShapeBase.TextRotation;
					TextRotationType textRotationType = textRotation;
					num = 4;
					continue;
				}
				case 2:
					if (textBoxShapeBase != null)
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14F;
					default:
						goto IL_170;
					}
					break;
				case 4:
				{
					TextRotationType textRotationType;
					switch (textRotationType)
					{
					case TextRotationType.TopToBottom:
						goto IL_C0;
					case TextRotationType.CounterClockwise:
						goto IL_63;
					case TextRotationType.Clockwise:
						A_0.Add(RecordTableEnumerator.b("⽂⑄㹆♈㹊㥌扎㝐㽒㩔⁖", a_) + ':' + RecordTableEnumerator.b("㕂⁄㕆㵈≊⹌⹎㵐", a_));
						num = 0;
						continue;
					}
					goto IL_14F;
				}
				}
				break;
				IL_14F:
				num = 3;
			}
		}
		IL_63:
		A_0.Add(RecordTableEnumerator.b("⽂⑄㹆♈㹊㥌扎㝐㽒㩔⁖", a_) + ':' + RecordTableEnumerator.b("㕂⁄㕆㵈≊⹌⹎㵐", a_));
		A_0.Add(RecordTableEnumerator.b("⹂㙄⡆摈❊ⱌ㙎㹐♒⅔穖㽘㝚㉜⡞䱠ɢ।፦", a_) + ':' + RecordTableEnumerator.b("⅂⩄㍆㵈⑊⁌扎═㱒硔⍖㙘⭚", a_));
		return;
		IL_C0:
		A_0.Add(RecordTableEnumerator.b("⽂⑄㹆♈㹊㥌扎㝐㽒㩔⁖", a_) + ':' + RecordTableEnumerator.b("㕂⁄㕆㵈≊⹌⹎㵐", a_));
		A_0.Add(RecordTableEnumerator.b("⹂㙄⡆摈❊ⱌ㙎㹐♒⅔穖㽘㝚㉜⡞䱠ɢ।፦", a_) + ':' + RecordTableEnumerator.b("㝂⩄㝆摈㽊≌扎㍐㱒⅔⍖㙘㙚", a_));
		return;
		IL_170:
		if (false)
		{
		}
	}

	// Token: 0x06003963 RID: 14691 RVA: 0x00201BBC File Offset: 0x00200BBC
	protected override void ᜀ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 6;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				TextBoxShapeBase textBoxShapeBase = A_1 as TextBoxShapeBase;
				num = 5;
				continue;
			}
			case 1:
				goto IL_1C3;
			case 2:
				goto IL_81;
			case 3:
				goto IL_12D;
			case 4:
			{
				TextBoxShapeBase textBoxShapeBase;
				A_0.WriteElementString(RecordTableEnumerator.b("栻嬽㠿㙁ቃ݅⑇⍉⭋⁍", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), textBoxShapeBase.VAlignment.ToString());
				num = 3;
				continue;
			}
			case 5:
			{
				TextBoxShapeBase textBoxShapeBase;
				if (textBoxShapeBase.HAlignment != CommentHAlignType.Left)
				{
					num = 12;
					continue;
				}
				goto IL_164;
			}
			case 6:
			{
				TextBoxShapeBase textBoxShapeBase;
				if (textBoxShapeBase.VAlignment != CommentVAlignType.Top)
				{
					num = 4;
					continue;
				}
				goto IL_12D;
			}
			case 7:
				goto IL_164;
			case 8:
				goto IL_128;
			case 9:
			{
				TextBoxShapeBase textBoxShapeBase;
				if (!textBoxShapeBase.IsTextLocked)
				{
					num = 11;
					continue;
				}
				goto IL_204;
			}
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_128;
				}
				if (false)
				{
				}
				break;
			case 11:
				A_0.WriteElementString(RecordTableEnumerator.b("瀻儽⌿⥁၃⍅ぇ㹉", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), RecordTableEnumerator.b("稻弽ⰿㅁ⅃", a_));
				num = 1;
				continue;
			case 12:
			{
				if (true)
				{
				}
				TextBoxShapeBase textBoxShapeBase;
				A_0.WriteElementString(RecordTableEnumerator.b("栻嬽㠿㙁ృ݅⑇⍉⭋⁍", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), textBoxShapeBase.HAlignment.ToString());
				num = 7;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 0;
			continue;
			IL_12D:
			num = 9;
			continue;
			IL_164:
			num = 6;
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_128:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽ℿ㉁⅃", a_));
		IL_1C3:
		IL_204:
		base.ᜀ(A_0, A_1);
	}
}
