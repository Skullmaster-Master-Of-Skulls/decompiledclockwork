using System;
using System.Globalization;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.html;

namespace iTextSharp.text.factories
{
	// Token: 0x020004F6 RID: 1270
	public class ElementFactory
	{
		// Token: 0x06002B76 RID: 11126 RVA: 0x00107330 File Offset: 0x00106330
		public static Chunk GetChunk(Properties attributes)
		{
			Chunk chunk = new Chunk();
			chunk.Font = FontFactory.GetFont(attributes);
			string text = attributes["itext"];
			if (text != null)
			{
				chunk.Append(text);
			}
			text = attributes[ElementTags.LOCALGOTO];
			if (text != null)
			{
				chunk.SetLocalGoto(text);
			}
			text = attributes[ElementTags.REMOTEGOTO];
			if (text != null)
			{
				string text2 = attributes["page"];
				if (text2 != null)
				{
					chunk.SetRemoteGoto(text, int.Parse(text2));
				}
				else
				{
					string text3 = attributes["destination"];
					if (text3 != null)
					{
						chunk.SetRemoteGoto(text, text3);
					}
				}
			}
			text = attributes[ElementTags.LOCALDESTINATION];
			if (text != null)
			{
				chunk.SetLocalDestination(text);
			}
			text = attributes[ElementTags.SUBSUPSCRIPT];
			if (text != null)
			{
				chunk.SetTextRise(float.Parse(text, NumberFormatInfo.InvariantInfo));
			}
			text = attributes["vertical-align"];
			if (text != null && text.EndsWith("%"))
			{
				float num = float.Parse(text.Substring(0, text.Length - 1), NumberFormatInfo.InvariantInfo) / 100f;
				chunk.SetTextRise(num * chunk.Font.Size);
			}
			text = attributes[ElementTags.GENERICTAG];
			if (text != null)
			{
				chunk.SetGenericTag(text);
			}
			text = attributes["backgroundcolor"];
			if (text != null)
			{
				chunk.SetBackground(Markup.DecodeColor(text));
			}
			return chunk;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x00107484 File Offset: 0x00106484
		public static Phrase GetPhrase(Properties attributes)
		{
			Phrase phrase = new Phrase();
			phrase.Font = FontFactory.GetFont(attributes);
			string text = attributes["leading"];
			if (text != null)
			{
				phrase.Leading = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["line-height"];
			if (text != null)
			{
				phrase.Leading = Markup.ParseLength(text, 12f);
			}
			text = attributes["itext"];
			if (text != null)
			{
				Chunk chunk = new Chunk(text);
				if ((text = attributes[ElementTags.GENERICTAG]) != null)
				{
					chunk.SetGenericTag(text);
				}
				phrase.Add(chunk);
			}
			return phrase;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0010751C File Offset: 0x0010651C
		public static Anchor GetAnchor(Properties attributes)
		{
			Anchor anchor = new Anchor(ElementFactory.GetPhrase(attributes));
			string text = attributes["name"];
			if (text != null)
			{
				anchor.Name = text;
			}
			text = attributes.Remove("reference");
			if (text != null)
			{
				anchor.Reference = text;
			}
			return anchor;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00107564 File Offset: 0x00106564
		public static Paragraph GetParagraph(Properties attributes)
		{
			Paragraph paragraph = new Paragraph(ElementFactory.GetPhrase(attributes));
			string text = attributes["align"];
			if (text != null)
			{
				paragraph.SetAlignment(text);
			}
			text = attributes["indentationleft"];
			if (text != null)
			{
				paragraph.IndentationLeft = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["indentationright"];
			if (text != null)
			{
				paragraph.IndentationRight = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			return paragraph;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x001075D4 File Offset: 0x001065D4
		public static ListItem GetListItem(Properties attributes)
		{
			return new ListItem(ElementFactory.GetParagraph(attributes));
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x001075F0 File Offset: 0x001065F0
		public static List GetList(Properties attributes)
		{
			List list = new List();
			list.Numbered = Utilities.CheckTrueOrFalse(attributes, "numbered");
			list.Lettered = Utilities.CheckTrueOrFalse(attributes, "lettered");
			list.Lowercase = Utilities.CheckTrueOrFalse(attributes, "lowercase");
			list.Autoindent = Utilities.CheckTrueOrFalse(attributes, "autoindent");
			list.Alignindent = Utilities.CheckTrueOrFalse(attributes, "alignindent");
			string text = attributes["first"];
			if (text != null)
			{
				char c = text[0];
				if (char.IsLetter(c))
				{
					list.First = (int)c;
				}
				else
				{
					list.First = int.Parse(text);
				}
			}
			text = attributes["listsymbol"];
			if (text != null)
			{
				list.ListSymbol = new Chunk(text, FontFactory.GetFont(attributes));
			}
			text = attributes["indentationleft"];
			if (text != null)
			{
				list.IndentationLeft = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["indentationright"];
			if (text != null)
			{
				list.IndentationRight = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["symbolindent"];
			if (text != null)
			{
				list.SymbolIndent = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			return list;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x00107710 File Offset: 0x00106710
		public static ChapterAutoNumber GetChapter(Properties attributes)
		{
			ChapterAutoNumber chapterAutoNumber = new ChapterAutoNumber("");
			ElementFactory.SetSectionParameters(chapterAutoNumber, attributes);
			return chapterAutoNumber;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00107730 File Offset: 0x00106730
		public static Section GetSection(Section parent, Properties attributes)
		{
			Section section = parent.AddSection("");
			ElementFactory.SetSectionParameters(section, attributes);
			return section;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x00107754 File Offset: 0x00106754
		private static void SetSectionParameters(Section section, Properties attributes)
		{
			string text = attributes["numberdepth"];
			if (text != null)
			{
				section.NumberDepth = int.Parse(text);
			}
			text = attributes["indent"];
			if (text != null)
			{
				section.Indentation = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["indentationleft"];
			if (text != null)
			{
				section.IndentationLeft = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["indentationright"];
			if (text != null)
			{
				section.IndentationRight = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x001077DC File Offset: 0x001067DC
		public static Image GetImage(Properties attributes)
		{
			string text = attributes["url"];
			if (text == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.url.of.the.image.is.missing"));
			}
			Image instance = Image.GetInstance(text);
			text = attributes["align"];
			int num = 0;
			if (text != null)
			{
				if (Util.EqualsIgnoreCase("Left", text))
				{
					num = num;
				}
				else if (Util.EqualsIgnoreCase("Right", text))
				{
					num |= 2;
				}
				else if (Util.EqualsIgnoreCase("Middle", text))
				{
					num |= 5;
				}
			}
			if (Util.EqualsIgnoreCase("true", attributes["underlying"]))
			{
				num |= 8;
			}
			if (Util.EqualsIgnoreCase("true", attributes["textwrap"]))
			{
				num |= 4;
			}
			instance.Alignment = num;
			text = attributes["alt"];
			if (text != null)
			{
				instance.Alt = text;
			}
			string text2 = attributes["absolutex"];
			string text3 = attributes["absolutey"];
			if (text2 != null && text3 != null)
			{
				instance.SetAbsolutePosition(float.Parse(text2, NumberFormatInfo.InvariantInfo), float.Parse(text3, NumberFormatInfo.InvariantInfo));
			}
			text = attributes["plainwidth"];
			if (text != null)
			{
				instance.ScaleAbsoluteWidth(float.Parse(text, NumberFormatInfo.InvariantInfo));
			}
			text = attributes["plainheight"];
			if (text != null)
			{
				instance.ScaleAbsoluteHeight(float.Parse(text, NumberFormatInfo.InvariantInfo));
			}
			text = attributes["rotation"];
			if (text != null)
			{
				instance.Rotation = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			return instance;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00107948 File Offset: 0x00106948
		public static Annotation GetAnnotation(Properties attributes)
		{
			float llx = 0f;
			float lly = 0f;
			float urx = 0f;
			float ury = 0f;
			string text = attributes["llx"];
			if (text != null)
			{
				llx = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["lly"];
			if (text != null)
			{
				lly = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["urx"];
			if (text != null)
			{
				urx = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			text = attributes["ury"];
			if (text != null)
			{
				ury = float.Parse(text, NumberFormatInfo.InvariantInfo);
			}
			string text2 = attributes["title"];
			string text3 = attributes["content"];
			if (text2 != null || text3 != null)
			{
				return new Annotation(text2, text3, llx, lly, urx, ury);
			}
			text = attributes["url"];
			if (text != null)
			{
				return new Annotation(llx, lly, urx, ury, text);
			}
			text = attributes["named"];
			if (text != null)
			{
				return new Annotation(llx, lly, urx, ury, int.Parse(text));
			}
			string text4 = attributes["file"];
			string text5 = attributes["destination"];
			string text6 = attributes.Remove("page");
			if (text4 != null)
			{
				if (text5 != null)
				{
					return new Annotation(llx, lly, urx, ury, text4, text5);
				}
				if (text6 != null)
				{
					return new Annotation(llx, lly, urx, ury, text4, int.Parse(text6));
				}
			}
			if (text2 == null)
			{
				text2 = "";
			}
			if (text3 == null)
			{
				text3 = "";
			}
			return new Annotation(text2, text3, llx, lly, urx, ury);
		}
	}
}
