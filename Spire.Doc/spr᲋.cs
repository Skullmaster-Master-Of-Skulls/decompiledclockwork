using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc;

// Token: 0x020003C5 RID: 965
internal class spr\u1C8B
{
	// Token: 0x06003673 RID: 13939 RVA: 0x0032F548 File Offset: 0x0032E548
	internal static Dictionary<string, FieldType> ᜃ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_68;
			case 2:
				goto IL_5B;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				spr\u1C8B.ᜁ();
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (spr\u1C8B.ᜀ != null)
				{
					goto IL_6A;
				}
				num = 2;
				break;
			}
		}
		IL_68:
		IL_6A:
		return spr\u1C8B.ᜀ;
	}

	// Token: 0x06003674 RID: 13940 RVA: 0x0032F5C4 File Offset: 0x0032E5C4
	internal static Dictionary<FieldType, string> ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5B;
			case 1:
				goto IL_68;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				spr\u1C8B.ᜀ();
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (spr\u1C8B.ᜁ != null)
				{
					goto IL_6A;
				}
				num = 0;
				break;
			}
		}
		IL_68:
		IL_6A:
		return spr\u1C8B.ᜁ;
	}

	// Token: 0x06003675 RID: 13941 RVA: 0x0032F640 File Offset: 0x0032E640
	internal spr\u1C8B()
	{
	}

	// Token: 0x06003676 RID: 13942 RVA: 0x0032F654 File Offset: 0x0032E654
	internal static FieldType ᜀ(string A_0)
	{
		int a_ = 9;
		string text;
		for (;;)
		{
			char[] separator = new char[]
			{
				' ',
				'\u00a0',
				'"'
			};
			string[] array = A_0.TrimStart(new char[0]).Split(separator);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (text.StartsWith(ClipboardData.b("剮", a_)))
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				case 1:
					goto IL_63;
				case 2:
					goto IL_85;
				case 3:
					if (spr\u1C8B.ᜃ().ContainsKey(text))
					{
						num = 2;
						continue;
					}
					return FieldType.FieldUnknown;
				case 4:
					return FieldType.FieldExpression;
				case 5:
					if (array.Length == 0)
					{
						num = 1;
						continue;
					}
					text = array[0].ToUpper();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_63:
		throw new Exception(string.Format(ClipboardData.b("㱮ŰᙲᙴṶὸቺ᡼᭾ꆀ떔ﺖ뮚햠莢펤욦얨슪즬膮", a_), A_0));
		IL_85:
		IL_A4:
		return spr\u1C8B.ᜃ()[text];
	}

	// Token: 0x06003677 RID: 13943 RVA: 0x0032F784 File Offset: 0x0032E784
	internal static string ᜀ(FieldType A_0)
	{
		if (spr\u1C8B.ᜂ().ContainsKey(A_0))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_35;
			}
			if (false)
			{
			}
			IL_35:
			if (true)
			{
			}
			return spr\u1C8B.ᜂ()[A_0];
		}
		return null;
	}

	// Token: 0x06003678 RID: 13944 RVA: 0x0032F7DC File Offset: 0x0032E7DC
	internal static bool ᜀ(sprᝑ A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6B;
			case 1:
				return false;
			case 2:
				num = 5;
				continue;
			case 4:
				if (A_0.ᜈ() != FieldType.FieldFormCheckBox)
				{
					num = 2;
					continue;
				}
				return true;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
					if (false)
					{
					}
					if (A_0.ᜈ() != FieldType.FieldFormDropDown)
					{
						num = 0;
						continue;
					}
					return true;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				if (true)
				{
				}
				num = 4;
			}
		}
		return false;
		IL_6B:
		return A_0.ᜈ() == FieldType.FieldFormTextInput;
	}

	// Token: 0x06003679 RID: 13945 RVA: 0x0032F898 File Offset: 0x0032E898
	private static void ᜁ()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1C8B.ᜀ = new Dictionary<string, FieldType>();
		spr\u1C8B.ᜀ.Add(ClipboardData.b("䑸", a_), FieldType.FieldFormula);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸㽺⭼㹾쾀삂삄", a_), FieldType.FieldAdvance);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⡺㙼", a_), FieldType.FieldAsk);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼㝾캀톂", a_), FieldType.FieldAuthor);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼ま쾀횂좄", a_), FieldType.FieldAutoNum);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼ま쾀횂좄쮆캈잊", a_), FieldType.FieldAutoNumLegal);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼ま쾀횂좄좆\udc88\udf8a", a_), FieldType.FieldAutoNumOutline);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼ま햀욂\udd84펆", a_), FieldType.FieldAutoText);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸⹺⥼ま햀욂\udd84펆얈슊\ude8c\udb8e", a_), FieldType.FieldAutoTextList);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㭸㩺⽼㱾캀잂삄", a_), FieldType.FieldBarCode);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㩸㑺ぼ㉾쒀춂톄풆", a_), FieldType.FieldComments);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㩸㑺ぼ⽾삀톂삄", a_), FieldType.FieldCompare);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㩸⥺㡼㹾햀욂솄욆\udd88캊", a_), FieldType.FieldCreateDate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㵸㩺⥼㹾쎀슂횄슆", a_), FieldType.FieldDatabase);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㵸㩺⥼㩾", a_), FieldType.FieldDate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㵸㑺㹼⽾펀첂햄슆\udb88\udf8a풌", a_), FieldType.FieldDocProperty);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㵸㑺㹼⥾삀톂첄욆쮈잊좌", a_), FieldType.FieldDocVariable);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㱸㽺㑼⭾햀쪂좄슆", a_), FieldType.FieldEditTime);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㱸⩺", a_), FieldType.FieldExpression);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㉺ㅼ㩾쾀슂좄슆", a_), FieldType.FieldFileName);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㉺ㅼ㩾튀쪂\udf84슆", a_), FieldType.FieldFileSize);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㉺ㅼ㍾좀춂", a_), FieldType.FieldFillIn);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㹸㑺⥼ま쎀횂톄펆욈얊", a_), FieldType.FieldGoToButton);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("ㅸ≺⵼㩾펀쾂첄즆슈", a_), FieldType.FieldHyperlink);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("へ㵺", a_), FieldType.FieldIf);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("へ㕺㹼㍾풀잂삄펆첈펊\ud98c", a_), FieldType.FieldIncludeText);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("へ㕺㹼㍾풀잂삄힆삈좊\ud98c\uda8e쎐횒", a_), FieldType.FieldIncludePicture);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("へ㕺㥼㩾\ud980", a_), FieldType.FieldIndex);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("へ㕺㭼ま", a_), FieldType.FieldInfo);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㉸㹺⑼⡾캀톂솄풆", a_), FieldType.FieldKeyWord);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㕸㩺⹼⭾튀슂펄슆춈즊풌", a_), FieldType.FieldLastSavedBy);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㕸㉺㍼㑾", a_), FieldType.FieldLink);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㕸㉺⹼⭾쾀횂좄", a_), FieldType.FieldListNum);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㑸㩺㹼⵾캀솂킄펆\udd88쒊쎌", a_), FieldType.FieldMacroButton);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㑸㹺⽼㡾쒀얂첄슆얈쾊", a_), FieldType.FieldMergeField);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㑸㹺⽼㡾쒀톂삄쒆", a_), FieldType.FieldMergeRec);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㑸㹺⽼㡾쒀킂삄횆", a_), FieldType.FieldMergeSeq);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸㹺╼⭾", a_), FieldType.FieldNext);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸㹺╼⭾좀얂", a_), FieldType.FieldNextIf);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸㑺⥼㩾펀욂쎄", a_), FieldType.FieldNoteRef);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸⹺ぼ㱾즀슂힄풆", a_), FieldType.FieldNumChars);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸⹺ぼ⽾삀쒂삄풆", a_), FieldType.FieldNumPages);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㝸⹺ぼ⡾캀톂솄풆", a_), FieldType.FieldNumWords);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⥸㩺㩼㩾", a_), FieldType.FieldPage);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⥸㩺㩼㩾펀욂쎄", a_), FieldType.FieldPageRef);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⥸⥺㑼ㅾ햀", a_), FieldType.FieldPrint);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⥸⥺㑼ㅾ햀잂쒄펆첈", a_), FieldType.FieldPrintDate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⥸⥺㑼⥾삀힂삄", a_), FieldType.FieldPrivate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⡸⹺㉼⭾쒀", a_), FieldType.FieldQuote);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⭸㹺㭼", a_), FieldType.FieldRef);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⭸㽺", a_), FieldType.FieldRefDoc);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⭸㹺⭼ㅾ풀캂", a_), FieldType.FieldRevisionNum);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㩺⭼㩾얀슂톄슆", a_), FieldType.FieldSaveDate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㹺㹼⭾좀첂쮄", a_), FieldType.FieldSection);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㹺㹼⭾좀첂쮄힆좈첊좌\udc8e", a_), FieldType.FieldSectionPages);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㹺ⱼ", a_), FieldType.FieldSequence);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㹺⥼", a_), FieldType.FieldSet);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸ぺ㑼⽾좀얂", a_), FieldType.FieldSkipIf);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸⽺⑼㍾쒀톂삄솆", a_), FieldType.FieldStyleRef);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸⹺㽼㕾쒀삂톄", a_), FieldType.FieldSubject);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸≺ぼ㵾캀쾂", a_), FieldType.FieldSymbol);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㹺ぼ⽾춀슂톄슆", a_), FieldType.FieldTemplate);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㉺ぼ㩾", a_), FieldType.FieldTime);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㉺⥼㍾쒀", a_), FieldType.FieldTitle);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㑺㱼", a_), FieldType.FieldTOA);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㩺", a_), FieldType.FieldTOAEntry);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㑺㹼", a_), FieldType.FieldTOC);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⵸㡺", a_), FieldType.FieldTOCEntry);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("ⱸ⡺㡼⵾삀잂솄햆첈\ud88a\ude8c", a_), FieldType.FieldUserAddress);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("ⱸ⡺㡼⵾좀춂첄펆삈쪊소\udc8e", a_), FieldType.FieldUserInitials);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("ⱸ⡺㡼⵾쾀슂좄슆", a_), FieldType.FieldUserName);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("ⅸ㹺", a_), FieldType.FieldIndexEntry);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("⩸㍺㱼⽾쒀", a_), FieldType.FieldShape);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸㽺㥼㙾쾀", a_), FieldType.FieldAddin);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㑺⽼㉾슀쮂삄쒆슈즊슌힎", a_), FieldType.FieldFormCheckBox);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㑺⽼㉾얀톂쪄힆춈쒊\uda8c솎", a_), FieldType.FieldFormDropDown);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㽸㑺⽼㉾햀욂\udd84펆", a_), FieldType.FieldFormTextInput);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㩸㑺㍼⭾펀첂즄", a_), FieldType.FieldOCX);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㱸㙺㽼㩾얀", a_), FieldType.FieldEmbed);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㡸㽺㥼⵾쒀킂횄얆얈쒊캌쒎", a_), FieldType.FieldAddressBlock);
		spr\u1C8B.ᜀ.Add(ClipboardData.b("㭸㉺㥼㙾캀횂톄쮆삈얊좌", a_), FieldType.FieldBidiOutline);
	}

	// Token: 0x0600367A RID: 13946 RVA: 0x0033011C File Offset: 0x0032F11C
	private static void ᜀ()
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_64;
			case 1:
				goto IL_7C;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_64:
				spr\u1C8B.ᜀ.Clear();
				spr\u1C8B.ᜀ = null;
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (spr\u1C8B.ᜀ == null)
				{
					goto IL_7E;
				}
				num = 0;
				break;
			}
		}
		IL_7C:
		IL_7E:
		spr\u1C8B.ᜁ = new Dictionary<FieldType, string>();
		spr\u1C8B.ᜁ.Add(FieldType.FieldFormula, ClipboardData.b("剮", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAdvance, ClipboardData.b("⹮㕰╲㑴㥶㩸㹺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAsk, ClipboardData.b("⹮≰㡲", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAuthor, ClipboardData.b("⹮⑰❲㵴㡶⭸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAutoNum, ClipboardData.b("⹮⑰❲㩴㥶ⱸ㙺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAutoNumLegal, ClipboardData.b("⹮⑰❲㩴㥶ⱸ㙺ㅼ㡾춀", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAutoNumOutline, ClipboardData.b("⹮⑰❲㩴㥶ⱸ㙺㉼⩾햀", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAutoText, ClipboardData.b("⹮⑰❲㩴⍶㱸⍺⥼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAutoTextList, ClipboardData.b("⹮⑰❲㩴⍶㱸⍺⥼㍾좀킂톄", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldBarCode, ClipboardData.b("⵮ばⅲ㙴㡶㵸㹺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldComments, ClipboardData.b("Ɱ㹰㹲㡴㉶㝸⽺⹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldCompare, ClipboardData.b("Ɱ㹰㹲╴㙶⭸㹺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldCreateDate, ClipboardData.b("Ɱ⍰㙲㑴⍶㱸㽺㱼⭾쒀", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldDatabase, ClipboardData.b("⭮ば❲㑴㕶㡸⡺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldDate, ClipboardData.b("⭮ば❲ぴ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldDocProperty, ClipboardData.b("⭮㹰ひ╴╶㙸⭺㡼⵾햀\uda82", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldDocVariable, ClipboardData.b("⭮㹰ひ⍴㙶⭸㉺㱼㵾춀욂", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldEditTime, ClipboardData.b("⩮㕰㩲ⅴ⍶へ㙺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldExpression, ClipboardData.b("⩮⁰", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFileName, ClipboardData.b("⥮㡰㽲ぴ㥶㡸㙺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFileSize, ClipboardData.b("⥮㡰㽲ぴ⑶へⅺ㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFillIn, ClipboardData.b("⥮㡰㽲㥴㹶㝸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldGoToButton, ClipboardData.b("⡮㹰❲㩴㕶ⱸ⽺⥼ま쾀", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldHyperlink, ClipboardData.b("❮⡰⍲ぴ╶㕸㉺㍼㑾", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldIf, ClipboardData.b("♮㝰", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldIncludeText, ClipboardData.b("♮㽰ひ㥴≶㵸㹺⥼㩾\ud980힂", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldIncludePicture, ClipboardData.b("♮㽰ひ㥴≶㵸㹺⵼㙾슀힂킄햆첈", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldIndex, ClipboardData.b("♮㽰㝲ぴ⽶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldInfo, ClipboardData.b("♮㽰㕲㩴", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldKeyWord, ClipboardData.b("⑮㑰⩲≴㡶⭸㽺⹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldLastSavedBy, ClipboardData.b("⍮ば⁲ⅴ⑶㡸⵺㡼㭾쎀\uda82", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldLink, ClipboardData.b("⍮㡰㵲㹴", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldListNum, ClipboardData.b("⍮㡰⁲ⅴ㥶ⱸ㙺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldMacroButton, ClipboardData.b("≮ばひ❴㡶㭸⹺⥼⭾캀춂", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldMergeField, ClipboardData.b("≮㑰ⅲ㉴㉶㽸㉺㡼㍾얀", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldMergeRec, ClipboardData.b("≮㑰ⅲ㉴㉶⭸㹺㹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldMergeSeq, ClipboardData.b("≮㑰ⅲ㉴㉶⩸㹺ⱼ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNext, ClipboardData.b("Ⅾ㑰⭲ⅴ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNextIf, ClipboardData.b("Ⅾ㑰⭲ⅴ㹶㽸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNoteRef, ClipboardData.b("Ⅾ㹰❲ぴ╶㱸㵺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNumChars, ClipboardData.b("Ⅾ⑰㹲㙴㽶㡸⥺⹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNumPages, ClipboardData.b("Ⅾ⑰㹲╴㙶㹸㹺⹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldNumWords, ClipboardData.b("Ⅾ⑰㹲≴㡶⭸㽺⹼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldPage, ClipboardData.b("㽮ば㑲ぴ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldPageRef, ClipboardData.b("㽮ば㑲ぴ╶㱸㵺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldPrint, ClipboardData.b("㽮⍰㩲㭴⍶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldPrintDate, ClipboardData.b("㽮⍰㩲㭴⍶㵸㩺⥼㩾", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldPrivate, ClipboardData.b("㽮⍰㩲⍴㙶⵸㹺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldQuote, ClipboardData.b("㹮⑰㱲ⅴ㉶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldRef, ClipboardData.b("㵮㑰㕲", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldRefDoc, ClipboardData.b("㵮㕰", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldRevisionNum, ClipboardData.b("㵮㑰╲㭴≶㑸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSaveDate, ClipboardData.b("㱮ば╲ぴ㍶㡸⽺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSection, ClipboardData.b("㱮㑰ひⅴ㹶㙸㕺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSectionPages, ClipboardData.b("㱮㑰ひⅴ㹶㙸㕺⵼㹾욀욂횄", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSequence, ClipboardData.b("㱮㑰≲", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSet, ClipboardData.b("㱮㑰❲", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSkipIf, ClipboardData.b("㱮㩰㩲╴㹶㽸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldStyleRef, ClipboardData.b("㱮╰⩲㥴㉶⭸㹺㭼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSubject, ClipboardData.b("㱮⑰ㅲ㽴㉶㩸⽺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldSymbol, ClipboardData.b("㱮⡰㹲㝴㡶㕸", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTemplate, ClipboardData.b("㭮㑰㹲╴㭶㡸⽺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTime, ClipboardData.b("㭮㡰㹲ぴ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTitle, ClipboardData.b("㭮㡰❲㥴㉶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTOA, ClipboardData.b("㭮㹰㉲", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTOAEntry, ClipboardData.b("㭮ば", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTOC, ClipboardData.b("㭮㹰ひ", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldTOCEntry, ClipboardData.b("㭮㉰", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldUserAddress, ClipboardData.b("㩮≰㙲❴㙶㵸㽺⽼㩾튀킂", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldUserInitials, ClipboardData.b("㩮≰㙲❴㹶㝸㉺⥼㙾삀쾂횄", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldUserName, ClipboardData.b("㩮≰㙲❴㥶㡸㙺㡼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldIndexEntry, ClipboardData.b("㝮㑰", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldShape, ClipboardData.b("㱮㥰㉲╴㉶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldAddin, ClipboardData.b("⹮㕰㝲㱴㥶", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFormCheckBox, ClipboardData.b("⥮㹰ⅲ㡴㑶ㅸ㹺㹼㑾쎀첂\udd84", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFormDropDown, ClipboardData.b("⥮㹰ⅲ㡴㍶⭸㑺⵼㭾캀풂쮄", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldFormTextInput, ClipboardData.b("⥮㹰ⅲ㡴⍶㱸⍺⥼", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldOCX, ClipboardData.b("Ɱ㹰㵲ⅴ╶㙸㝺", a_));
		spr\u1C8B.ᜁ.Add(FieldType.FieldEmbed, ClipboardData.b("⩮㱰ㅲぴ㍶", a_));
	}

	// Token: 0x040029B0 RID: 10672
	[ThreadStatic]
	private static Dictionary<string, FieldType> ᜀ;

	// Token: 0x040029B1 RID: 10673
	[ThreadStatic]
	private static Dictionary<FieldType, string> ᜁ;
}
