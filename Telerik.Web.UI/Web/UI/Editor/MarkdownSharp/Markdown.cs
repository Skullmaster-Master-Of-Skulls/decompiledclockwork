using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Editor.MarkdownSharp
{
	// Token: 0x02000B51 RID: 2897
	public class Markdown
	{
		// Token: 0x06006D09 RID: 27913 RVA: 0x00194A3B File Offset: 0x00192C3B
		public Markdown() : this(false)
		{
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x00194A44 File Offset: 0x00192C44
		public Markdown(bool loadOptionsFromConfigFile)
		{
			this._emptyElementSuffix = " />";
			this._linkEmails = true;
			this._urls = new Dictionary<string, string>();
			this._titles = new Dictionary<string, string>();
			this._htmlBlocks = new Dictionary<string, string>();
			base..ctor();
			if (!loadOptionsFromConfigFile)
			{
				return;
			}
			NameValueCollection appSettings = ConfigurationManager.AppSettings;
			foreach (object obj in appSettings.Keys)
			{
				string text = (string)obj;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Markdown.AutoHyperlink"))
					{
						if (!(a == "Markdown.AutoNewlines"))
						{
							if (!(a == "Markdown.EmptyElementSuffix"))
							{
								if (!(a == "Markdown.EncodeProblemUrlCharacters"))
								{
									if (!(a == "Markdown.LinkEmails"))
									{
										if (a == "Markdown.StrictBoldItalic")
										{
											this._strictBoldItalic = Convert.ToBoolean(appSettings[text]);
										}
									}
									else
									{
										this._linkEmails = Convert.ToBoolean(appSettings[text]);
									}
								}
								else
								{
									this._encodeProblemUrlCharacters = Convert.ToBoolean(appSettings[text]);
								}
							}
							else
							{
								this._emptyElementSuffix = appSettings[text];
							}
						}
						else
						{
							this._autoNewlines = Convert.ToBoolean(appSettings[text]);
						}
					}
					else
					{
						this._autoHyperlink = Convert.ToBoolean(appSettings[text]);
					}
				}
			}
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x00194BAC File Offset: 0x00192DAC
		public Markdown(MarkdownOptions options)
		{
			this._emptyElementSuffix = " />";
			this._linkEmails = true;
			this._urls = new Dictionary<string, string>();
			this._titles = new Dictionary<string, string>();
			this._htmlBlocks = new Dictionary<string, string>();
			base..ctor();
			this._autoHyperlink = options.AutoHyperlink;
			this._autoNewlines = options.AutoNewlines;
			this._emptyElementSuffix = options.EmptyElementSuffix;
			this._encodeProblemUrlCharacters = options.EncodeProblemUrlCharacters;
			this._linkEmails = options.LinkEmails;
			this._strictBoldItalic = options.StrictBoldItalic;
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x06006D0C RID: 27916 RVA: 0x00194C3A File Offset: 0x00192E3A
		// (set) Token: 0x06006D0D RID: 27917 RVA: 0x00194C42 File Offset: 0x00192E42
		public string EmptyElementSuffix
		{
			get
			{
				return this._emptyElementSuffix;
			}
			set
			{
				this._emptyElementSuffix = value;
			}
		}

		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06006D0E RID: 27918 RVA: 0x00194C4B File Offset: 0x00192E4B
		// (set) Token: 0x06006D0F RID: 27919 RVA: 0x00194C53 File Offset: 0x00192E53
		public bool LinkEmails
		{
			get
			{
				return this._linkEmails;
			}
			set
			{
				this._linkEmails = value;
			}
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06006D10 RID: 27920 RVA: 0x00194C5C File Offset: 0x00192E5C
		// (set) Token: 0x06006D11 RID: 27921 RVA: 0x00194C64 File Offset: 0x00192E64
		public bool StrictBoldItalic
		{
			get
			{
				return this._strictBoldItalic;
			}
			set
			{
				this._strictBoldItalic = value;
			}
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06006D12 RID: 27922 RVA: 0x00194C6D File Offset: 0x00192E6D
		// (set) Token: 0x06006D13 RID: 27923 RVA: 0x00194C75 File Offset: 0x00192E75
		public bool AutoNewLines
		{
			get
			{
				return this._autoNewlines;
			}
			set
			{
				this._autoNewlines = value;
			}
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06006D14 RID: 27924 RVA: 0x00194C7E File Offset: 0x00192E7E
		// (set) Token: 0x06006D15 RID: 27925 RVA: 0x00194C86 File Offset: 0x00192E86
		public bool AutoHyperlink
		{
			get
			{
				return this._autoHyperlink;
			}
			set
			{
				this._autoHyperlink = value;
			}
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x06006D16 RID: 27926 RVA: 0x00194C8F File Offset: 0x00192E8F
		// (set) Token: 0x06006D17 RID: 27927 RVA: 0x00194C97 File Offset: 0x00192E97
		public bool EncodeProblemUrlCharacters
		{
			get
			{
				return this._encodeProblemUrlCharacters;
			}
			set
			{
				this._encodeProblemUrlCharacters = value;
			}
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x00194CA0 File Offset: 0x00192EA0
		static Markdown()
		{
			Markdown._escapeTable = new Dictionary<string, string>();
			Markdown._invertedEscapeTable = new Dictionary<string, string>();
			Markdown._backslashEscapeTable = new Dictionary<string, string>();
			string text = "";
			string text2 = "\\`*_{}[]()>#+-.!";
			for (int i = 0; i < text2.Length; i++)
			{
				string text3 = text2[i].ToString();
				string hashKey = Markdown.GetHashKey(text3);
				Markdown._escapeTable.Add(text3, hashKey);
				Markdown._invertedEscapeTable.Add(hashKey, text3);
				Markdown._backslashEscapeTable.Add("\\" + text3, hashKey);
				text = text + Regex.Escape("\\" + text3) + "|";
			}
			Markdown._backslashEscapes = new Regex(text.Substring(0, text.Length - 1), RegexOptions.Compiled);
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x06006D19 RID: 27929 RVA: 0x00195008 File Offset: 0x00193208
		public string Version
		{
			get
			{
				return "1.13";
			}
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x00195010 File Offset: 0x00193210
		public string Transform(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			this.Setup();
			text = this.Normalize(text);
			text = this.HashHTMLBlocks(text);
			text = this.StripLinkDefinitions(text);
			text = this.RunBlockGamut(text);
			text = this.Unescape(text);
			this.Cleanup();
			return text + "\n";
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x00195070 File Offset: 0x00193270
		private string RunBlockGamut(string text)
		{
			text = this.DoHeaders(text);
			text = this.DoHorizontalRules(text);
			text = this.DoLists(text);
			text = this.DoCodeBlocks(text);
			text = this.DoBlockQuotes(text);
			text = this.HashHTMLBlocks(text);
			text = this.FormParagraphs(text);
			return text;
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x001950C0 File Offset: 0x001932C0
		private string RunSpanGamut(string text)
		{
			text = this.DoCodeSpans(text);
			text = this.EscapeSpecialCharsWithinTagAttributes(text);
			text = this.EscapeBackslashes(text);
			text = this.DoImages(text);
			text = this.DoAnchors(text);
			text = this.DoAutoLinks(text);
			text = this.EncodeAmpsAndAngles(text);
			text = this.DoItalicsAndBold(text);
			text = this.DoHardBreaks(text);
			return text;
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x00195120 File Offset: 0x00193320
		private string FormParagraphs(string text)
		{
			string[] array = Markdown._newlinesMultiple.Split(Markdown._newlinesLeadingTrailing.Replace(text, ""));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].StartsWith("\u001a"))
				{
					array[i] = this._htmlBlocks[array[i]];
				}
				else
				{
					array[i] = Markdown._leadingWhitespace.Replace(this.RunSpanGamut(array[i]), "<p>") + "</p>";
				}
			}
			return string.Join("\n\n", array);
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x001951A8 File Offset: 0x001933A8
		private void Setup()
		{
			this._urls.Clear();
			this._titles.Clear();
			this._htmlBlocks.Clear();
			this._listLevel = 0;
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x001951D2 File Offset: 0x001933D2
		private void Cleanup()
		{
			this.Setup();
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x001951DA File Offset: 0x001933DA
		private static string GetNestedBracketsPattern()
		{
			if (Markdown._nestedBracketsPattern == null)
			{
				Markdown._nestedBracketsPattern = Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^\\[\\]]+      # Anything other than brackets\r\n                     |\r\n                       \\[\r\n                           ", 6) + Markdown.RepeatString(" \\]\r\n                    )*", 6);
			}
			return Markdown._nestedBracketsPattern;
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x00195208 File Offset: 0x00193408
		private static string GetNestedParensPattern()
		{
			if (Markdown._nestedParensPattern == null)
			{
				Markdown._nestedParensPattern = Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^()\\s]+      # Anything other than parens or whitespace\r\n                     |\r\n                       \\(\r\n                           ", 6) + Markdown.RepeatString(" \\)\r\n                    )*", 6);
			}
			return Markdown._nestedParensPattern;
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x00195236 File Offset: 0x00193436
		private string StripLinkDefinitions(string text)
		{
			return Markdown._linkDef.Replace(text, new MatchEvaluator(this.LinkEvaluator));
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x00195250 File Offset: 0x00193450
		private string LinkEvaluator(Match match)
		{
			string key = match.Groups[1].Value.ToLowerInvariant();
			this._urls[key] = this.EncodeAmpsAndAngles(match.Groups[2].Value);
			if (match.Groups[3] != null && match.Groups[3].Length > 0)
			{
				this._titles[key] = match.Groups[3].Value.Replace("\"", "&quot;");
			}
			return "";
		}

		// Token: 0x06006D24 RID: 27940 RVA: 0x001952EC File Offset: 0x001934EC
		private static string GetBlockPattern()
		{
			string newValue = "ins|del";
			string newValue2 = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|address|script|noscript|form|fieldset|iframe|math";
			string text = "\r\n            (?>\t\t\t\t            # optional tag attributes\r\n              \\s\t\t\t            # starts with whitespace\r\n              (?>\r\n                [^>\"/]+\t            # text outside quotes\r\n              |\r\n                /+(?!>)\t\t            # slash not followed by >\r\n              |\r\n                \"[^\"]*\"\t\t        # text inside double quotes (tolerate >)\r\n              |\r\n                '[^']*'\t                # text inside single quotes (tolerate >)\r\n              )*\r\n            )?\t\r\n            ";
			string text2 = Markdown.RepeatString("\r\n                (?>\r\n                  [^<]+\t\t\t        # content without tag\r\n                |\r\n                  <\\2\t\t\t        # nested opening tag\r\n                    " + text + "       # attributes\r\n                  (?>\r\n                      />\r\n                  |\r\n                      >", 6) + ".*?" + Markdown.RepeatString("\r\n                      </\\2\\s*>\t        # closing nested tag\r\n                  )\r\n                  |\t\t\t\t\r\n                  <(?!/\\2\\s*>           # other tags with a different name\r\n                  )\r\n                )*", 6);
			string newValue3 = text2.Replace("\\2", "\\3");
			string text3 = "\r\n            (?>\r\n                  (?>\r\n                    (?<=\\n)     # Starting after a blank line\r\n                    |           # or\r\n                    \\A\\n?       # the beginning of the doc\r\n                  )\r\n                  (             # save in $1\r\n\r\n                    # Match from `\\n<tag>` to `</tag>\\n`, handling nested tags \r\n                    # in between.\r\n                      \r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_b_re)   # start tag = $2\r\n                        $attr>                # attributes followed by > and \\n\r\n                        $content              # content, support nesting\r\n                        </\\2>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n\r\n                  | # Special version for tags of group a.\r\n\r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_a_re)   # start tag = $3\r\n                        $attr>[ ]*\\n          # attributes followed by >\r\n                        $content2             # content, support nesting\r\n                        </\\3>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n                      \r\n                  | # Special case just for <hr />. It was easier to make a special \r\n                    # case than to make the other regex more complicated.\r\n                  \r\n                        [ ]{0,$less_than_tab}\r\n                        <(hr)                 # start tag = $2\r\n                        $attr                 # attributes\r\n                        /?>                   # the matching end tag\r\n                        [ ]*\r\n                        (?=\\n{2,}|\\Z)         # followed by a blank line or end of document\r\n                  \r\n                  | # Special case for standalone HTML comments:\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <!-- .*? -->\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                  \r\n                  | # PHP and ASP-style processor instructions (<? and <%)\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <([?%])                # $2\r\n                        .*?\r\n                        \\2>\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                      \r\n                  )\r\n            )";
			text3 = text3.Replace("$less_than_tab", 3.ToString());
			text3 = text3.Replace("$block_tags_b_re", newValue2);
			text3 = text3.Replace("$block_tags_a_re", newValue);
			text3 = text3.Replace("$attr", text);
			text3 = text3.Replace("$content2", newValue3);
			return text3.Replace("$content", text2);
		}

		// Token: 0x06006D25 RID: 27941 RVA: 0x001953B6 File Offset: 0x001935B6
		private string HashHTMLBlocks(string text)
		{
			return Markdown._blocksHtml.Replace(text, new MatchEvaluator(this.HtmlEvaluator));
		}

		// Token: 0x06006D26 RID: 27942 RVA: 0x001953D0 File Offset: 0x001935D0
		private string HtmlEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string hashKey = Markdown.GetHashKey(value);
			this._htmlBlocks[hashKey] = value;
			return "\n\n" + hashKey + "\n\n";
		}

		// Token: 0x06006D27 RID: 27943 RVA: 0x00195414 File Offset: 0x00193614
		private static string GetHashKey(string s)
		{
			return "\u001a" + Math.Abs(s.GetHashCode()).ToString() + "\u001a";
		}

		// Token: 0x06006D28 RID: 27944 RVA: 0x00195444 File Offset: 0x00193644
		private List<Markdown.Token> TokenizeHTML(string text)
		{
			int num = 0;
			List<Markdown.Token> list = new List<Markdown.Token>();
			foreach (object obj in Markdown._htmlTokens.Matches(text))
			{
				Match match = (Match)obj;
				int index = match.Index;
				if (num < index)
				{
					list.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(num, index - num)));
				}
				list.Add(new Markdown.Token(Markdown.TokenType.Tag, match.Value));
				num = index + match.Length;
			}
			if (num < text.Length)
			{
				list.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(num, text.Length - num)));
			}
			return list;
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x00195510 File Offset: 0x00193710
		private string DoAnchors(string text)
		{
			text = Markdown._anchorRef.Replace(text, new MatchEvaluator(this.AnchorRefEvaluator));
			text = Markdown._anchorInline.Replace(text, new MatchEvaluator(this.AnchorInlineEvaluator));
			text = Markdown._anchorRefShortcut.Replace(text, new MatchEvaluator(this.AnchorRefShortcutEvaluator));
			return text;
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x0019556C File Offset: 0x0019376C
		private string AnchorRefEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string text = match.Groups[3].Value.ToLowerInvariant();
			if (text == "")
			{
				text = value2.ToLowerInvariant();
			}
			string text3;
			if (this._urls.ContainsKey(text))
			{
				string text2 = this._urls[text];
				text2 = this.EncodeProblemUrlChars(text2);
				text2 = this.EscapeBoldItalic(text2);
				text3 = "<a href=\"" + text2 + "\"";
				if (this._titles.ContainsKey(text))
				{
					string text4 = this._titles[text];
					text4 = this.EscapeBoldItalic(text4);
					text3 = text3 + " title=\"" + text4 + "\"";
				}
				text3 = text3 + ">" + value2 + "</a>";
			}
			else
			{
				text3 = value;
			}
			return text3;
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x00195660 File Offset: 0x00193860
		private string AnchorRefShortcutEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string key = Regex.Replace(value2.ToLowerInvariant(), "[ ]*\\n[ ]*", " ");
			string text2;
			if (this._urls.ContainsKey(key))
			{
				string text = this._urls[key];
				text = this.EncodeProblemUrlChars(text);
				text = this.EscapeBoldItalic(text);
				text2 = "<a href=\"" + text + "\"";
				if (this._titles.ContainsKey(key))
				{
					string text3 = this._titles[key];
					text3 = this.EscapeBoldItalic(text3);
					text2 = text2 + " title=\"" + text3 + "\"";
				}
				text2 = text2 + ">" + value2 + "</a>";
			}
			else
			{
				text2 = value;
			}
			return text2;
		}

		// Token: 0x06006D2C RID: 27948 RVA: 0x0019573C File Offset: 0x0019393C
		private string AnchorInlineEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			string text = match.Groups[3].Value;
			string text2 = match.Groups[6].Value;
			text = this.EncodeProblemUrlChars(text);
			text = this.EscapeBoldItalic(text);
			if (text.StartsWith("<") && text.EndsWith(">"))
			{
				text = text.Substring(1, text.Length - 2);
			}
			string str = string.Format("<a href=\"{0}\"", text);
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = text2.Replace("\"", "&quot;");
				text2 = this.EscapeBoldItalic(text2);
				str += string.Format(" title=\"{0}\"", text2);
			}
			return str + string.Format(">{0}</a>", value);
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x0019580B File Offset: 0x00193A0B
		private string DoImages(string text)
		{
			text = Markdown._imagesRef.Replace(text, new MatchEvaluator(this.ImageReferenceEvaluator));
			text = Markdown._imagesInline.Replace(text, new MatchEvaluator(this.ImageInlineEvaluator));
			return text;
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x00195840 File Offset: 0x00193A40
		private string ImageReferenceEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string text = match.Groups[2].Value;
			string text2 = match.Groups[3].Value.ToLowerInvariant();
			if (text2 == "")
			{
				text2 = text.ToLowerInvariant();
			}
			text = text.Replace("\"", "&quot;");
			string text4;
			if (this._urls.ContainsKey(text2))
			{
				string text3 = this._urls[text2];
				text3 = this.EncodeProblemUrlChars(text3);
				text3 = this.EscapeBoldItalic(text3);
				text4 = string.Format("<img src=\"{0}\" alt=\"{1}\"", text3, text);
				if (this._titles.ContainsKey(text2))
				{
					string text5 = this._titles[text2];
					text5 = this.EscapeBoldItalic(text5);
					text4 += string.Format(" title=\"{0}\"", text5);
				}
				text4 += this._emptyElementSuffix;
			}
			else
			{
				text4 = value;
			}
			return text4;
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x00195938 File Offset: 0x00193B38
		private string ImageInlineEvaluator(Match match)
		{
			string text = match.Groups[2].Value;
			string text2 = match.Groups[3].Value;
			string text3 = match.Groups[6].Value;
			text = text.Replace("\"", "&quot;");
			text3 = text3.Replace("\"", "&quot;");
			if (text2.StartsWith("<") && text2.EndsWith(">"))
			{
				text2 = text2.Substring(1, text2.Length - 2);
			}
			text2 = this.EncodeProblemUrlChars(text2);
			text2 = this.EscapeBoldItalic(text2);
			string str = string.Format("<img src=\"{0}\" alt=\"{1}\"", text2, text);
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = this.EscapeBoldItalic(text3);
				str += string.Format(" title=\"{0}\"", text3);
			}
			return str + this._emptyElementSuffix;
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x00195A14 File Offset: 0x00193C14
		private string DoHeaders(string text)
		{
			text = Markdown._headerSetext.Replace(text, new MatchEvaluator(this.SetextHeaderEvaluator));
			text = Markdown._headerAtx.Replace(text, new MatchEvaluator(this.AtxHeaderEvaluator));
			return text;
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x00195A4C File Offset: 0x00193C4C
		private string SetextHeaderEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			int num = match.Groups[2].Value.StartsWith("=") ? 1 : 2;
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), num);
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x00195AA4 File Offset: 0x00193CA4
		private string AtxHeaderEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			int length = match.Groups[1].Value.Length;
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), length);
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x00195AF1 File Offset: 0x00193CF1
		private string DoHorizontalRules(string text)
		{
			return Markdown._horizontalRules.Replace(text, "<hr" + this._emptyElementSuffix + "\n");
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x00195B13 File Offset: 0x00193D13
		private string DoLists(string text)
		{
			if (this._listLevel > 0)
			{
				text = Markdown._listNested.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			else
			{
				text = Markdown._listTopLevel.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			return text;
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x00195B54 File Offset: 0x00193D54
		private string ListEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			string text2 = Regex.IsMatch(match.Groups[3].Value, "[*+-]") ? "ul" : "ol";
			text = Regex.Replace(text, "\\n{2,}", "\n\n\n");
			string arg = this.ProcessListItems(text, (text2 == "ul") ? "[*+-]" : "\\d+[.]");
			return string.Format("<{0}>\n{1}</{0}>\n", text2, arg);
		}

		// Token: 0x06006D36 RID: 27958 RVA: 0x00195BE0 File Offset: 0x00193DE0
		private string ProcessListItems(string list, string marker)
		{
			this._listLevel++;
			list = Regex.Replace(list, "\\n{2,}\\z", "\n");
			string pattern = string.Format("(\\n)?                      # leading line = $1\r\n                (^[ ]*)                    # leading whitespace = $2\r\n                ({0}) [ ]+                 # list marker = $3\r\n                ((?s:.+?)                  # list item text = $4\r\n                (\\n{{1,2}}))      \r\n                (?= \\n* (\\z | \\2 ({0}) [ ]+))", marker);
			list = Regex.Replace(list, pattern, new MatchEvaluator(this.ListItemEvaluator), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			this._listLevel--;
			return list;
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x00195C40 File Offset: 0x00193E40
		private string ListItemEvaluator(Match match)
		{
			string text = match.Groups[4].Value;
			string value = match.Groups[1].Value;
			if (!string.IsNullOrEmpty(value) || Regex.IsMatch(text, "\\n{2,}"))
			{
				text = this.RunBlockGamut(this.Outdent(text) + "\n");
			}
			else
			{
				text = this.DoLists(this.Outdent(text));
				text = text.TrimEnd(new char[]
				{
					'\n'
				});
				text = this.RunSpanGamut(text);
			}
			return string.Format("<li>{0}</li>\n", text);
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x00195CD5 File Offset: 0x00193ED5
		private string DoCodeBlocks(string text)
		{
			text = Markdown._codeBlock.Replace(text, new MatchEvaluator(this.CodeBlockEvaluator));
			return text;
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x00195CF4 File Offset: 0x00193EF4
		private string CodeBlockEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			text = this.EncodeCode(this.Outdent(text));
			text = Markdown._newlinesLeadingTrailing.Replace(text, "");
			return "\n\n<pre><code>" + text + "\n</code></pre>\n\n";
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x00195D42 File Offset: 0x00193F42
		private string DoCodeSpans(string text)
		{
			return Markdown._codeSpan.Replace(text, new MatchEvaluator(this.CodeSpanEvaluator));
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x00195D5C File Offset: 0x00193F5C
		private string CodeSpanEvaluator(Match match)
		{
			string text = match.Groups[2].Value;
			text = Regex.Replace(text, "^[ ]*", "");
			text = Regex.Replace(text, "[ ]*$", "");
			text = this.EncodeCode(text);
			return "<code>" + text + "</code>";
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x00195DB8 File Offset: 0x00193FB8
		private string DoItalicsAndBold(string text)
		{
			if (this._strictBoldItalic)
			{
				text = Markdown._strictBold.Replace(text, "$1<strong>$3</strong>$4");
				text = Markdown._strictItalic.Replace(text, "$1<em>$3</em>$4");
			}
			else
			{
				text = Markdown._bold.Replace(text, "<strong>$2</strong>");
				text = Markdown._italic.Replace(text, "<em>$2</em>");
			}
			return text;
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x00195E18 File Offset: 0x00194018
		private string DoHardBreaks(string text)
		{
			if (this._autoNewlines)
			{
				text = Regex.Replace(text, "\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			else
			{
				text = Regex.Replace(text, " {2,}\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			return text;
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x00195E6A File Offset: 0x0019406A
		private string DoBlockQuotes(string text)
		{
			return Markdown._blockquote.Replace(text, new MatchEvaluator(this.BlockQuoteEvaluator));
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x00195E84 File Offset: 0x00194084
		private string BlockQuoteEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			text = Regex.Replace(text, "^[ ]*>[ ]?", "", RegexOptions.Multiline);
			text = Regex.Replace(text, "^[ ]+$", "", RegexOptions.Multiline);
			text = this.RunBlockGamut(text);
			text = Regex.Replace(text, "^", "  ", RegexOptions.Multiline);
			text = Regex.Replace(text, "(\\s*<pre>.+?</pre>)", new MatchEvaluator(this.BlockQuoteEvaluator2), RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			return string.Format("<blockquote>\n{0}\n</blockquote>\n\n", text);
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x00195F06 File Offset: 0x00194106
		private string BlockQuoteEvaluator2(Match match)
		{
			return Regex.Replace(match.Groups[1].Value, "^  ", "", RegexOptions.Multiline);
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x00195F2C File Offset: 0x0019412C
		private string DoAutoLinks(string text)
		{
			if (this._autoHyperlink)
			{
				text = Markdown._autolinkBare.Replace(text, "$1<$2$3>$4");
			}
			text = Regex.Replace(text, "<((https?|ftp):[^'\">\\s]+)>", new MatchEvaluator(this.HyperlinkEvaluator));
			if (this._linkEmails)
			{
				string pattern = "<\r\n                      (?:mailto:)?\r\n                      (\r\n                        [-.\\w]+\r\n                        \\@\r\n                        [-a-z0-9]+(\\.[-a-z0-9]+)*\\.[a-z]+\r\n                      )\r\n                      >";
				text = Regex.Replace(text, pattern, new MatchEvaluator(this.EmailEvaluator), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			}
			return text;
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x00195F94 File Offset: 0x00194194
		private string HyperlinkEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			return string.Format("<a href=\"{0}\">{0}</a>", value);
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x00195FC0 File Offset: 0x001941C0
		private string EmailEvaluator(Match match)
		{
			string text = this.Unescape(match.Groups[1].Value);
			text = "mailto:" + text;
			text = this.EncodeEmailAddress(text);
			text = string.Format("<a href=\"{0}\">{0}</a>", text);
			return Regex.Replace(text, "\">.+?:", "\">");
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x00196017 File Offset: 0x00194217
		private string Outdent(string block)
		{
			return Markdown._outDent.Replace(block, "");
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x0019602C File Offset: 0x0019422C
		private string EncodeEmailAddress(string addr)
		{
			StringBuilder stringBuilder = new StringBuilder(addr.Length * 5);
			Random random = new Random();
			foreach (char c in addr)
			{
				int num = random.Next(1, 100);
				if ((num > 90 || c == ':') && c != '@')
				{
					stringBuilder.Append(c);
				}
				else if (num < 45)
				{
					stringBuilder.AppendFormat("&#x{0:x};", (int)c);
				}
				else
				{
					stringBuilder.AppendFormat("&#{0};", (int)c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x001960C4 File Offset: 0x001942C4
		private string EncodeCode(string code)
		{
			return Markdown._codeEncoder.Replace(code, new MatchEvaluator(this.EncodeCodeEvaluator));
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x001960E0 File Offset: 0x001942E0
		private string EncodeCodeEvaluator(Match match)
		{
			string value;
			if ((value = match.Value) != null)
			{
				if (value == "&")
				{
					return "&amp;";
				}
				if (value == "<")
				{
					return "&lt;";
				}
				if (value == ">")
				{
					return "&gt;";
				}
			}
			return Markdown._escapeTable[match.Value];
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x00196142 File Offset: 0x00194342
		private string EncodeAmpsAndAngles(string s)
		{
			s = Markdown._amps.Replace(s, "&amp;");
			s = Markdown._angles.Replace(s, "&lt;");
			return s;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x00196169 File Offset: 0x00194369
		private string EscapeBackslashes(string s)
		{
			return Markdown._backslashEscapes.Replace(s, new MatchEvaluator(this.EscapeBackslashesEvaluator));
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x00196182 File Offset: 0x00194382
		private string EscapeBackslashesEvaluator(Match match)
		{
			return Markdown._backslashEscapeTable[match.Value];
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x00196194 File Offset: 0x00194394
		private string Unescape(string s)
		{
			return Markdown._unescapes.Replace(s, new MatchEvaluator(this.UnescapeEvaluator));
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x001961AD File Offset: 0x001943AD
		private string UnescapeEvaluator(Match match)
		{
			return Markdown._invertedEscapeTable[match.Value];
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x001961BF File Offset: 0x001943BF
		private string EscapeBoldItalic(string s)
		{
			s = s.Replace("*", Markdown._escapeTable["*"]);
			s = s.Replace("_", Markdown._escapeTable["_"]);
			return s;
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x001961FC File Offset: 0x001943FC
		private string EncodeProblemUrlChars(string url)
		{
			if (!this._encodeProblemUrlCharacters)
			{
				return url;
			}
			StringBuilder stringBuilder = new StringBuilder(url.Length);
			for (int i = 0; i < url.Length; i++)
			{
				char c = url[i];
				bool flag = Array.IndexOf<char>(Markdown._problemUrlChars, c) != -1;
				if (flag && c == ':' && i < url.Length - 1)
				{
					flag = (url[i + 1] != '/' && (url[i + 1] < '0' || url[i + 1] > '9'));
				}
				if (flag)
				{
					stringBuilder.Append("%" + string.Format("{0:x}", (byte)c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x001962C8 File Offset: 0x001944C8
		private string EscapeSpecialCharsWithinTagAttributes(string text)
		{
			List<Markdown.Token> list = this.TokenizeHTML(text);
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (Markdown.Token token in list)
			{
				string text2 = token.Value;
				if (token.Type == Markdown.TokenType.Tag)
				{
					text2 = text2.Replace("\\", Markdown._escapeTable["\\"]);
					text2 = Regex.Replace(text2, "(?<=.)</?code>(?=.)", Markdown._escapeTable["`"]);
					text2 = this.EscapeBoldItalic(text2);
				}
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x00196384 File Offset: 0x00194584
		private string Normalize(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				switch (c)
				{
				case '\t':
				{
					int num = 4 - stringBuilder2.Length % 4;
					for (int j = 0; j < num; j++)
					{
						stringBuilder2.Append(' ');
					}
					break;
				}
				case '\n':
					if (flag)
					{
						stringBuilder.Append(stringBuilder2);
					}
					stringBuilder.Append('\n');
					stringBuilder2.Length = 0;
					flag = false;
					break;
				case '\v':
				case '\f':
					goto IL_CB;
				case '\r':
					if (i < text.Length - 1 && text[i + 1] != '\n')
					{
						if (flag)
						{
							stringBuilder.Append(stringBuilder2);
						}
						stringBuilder.Append('\n');
						stringBuilder2.Length = 0;
						flag = false;
					}
					break;
				default:
					if (c != '\u001a')
					{
						goto IL_CB;
					}
					break;
				}
				IL_E9:
				i++;
				continue;
				IL_CB:
				if (!flag && text[i] != ' ')
				{
					flag = true;
				}
				stringBuilder2.Append(text[i]);
				goto IL_E9;
			}
			if (flag)
			{
				stringBuilder.Append(stringBuilder2);
			}
			stringBuilder.Append('\n');
			return stringBuilder.Append("\n\n").ToString();
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x001964B0 File Offset: 0x001946B0
		private static string RepeatString(string text, int count)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length * count);
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001D5B RID: 7515
		private const string _version = "1.13";

		// Token: 0x04001D5C RID: 7516
		private const int _nestDepth = 6;

		// Token: 0x04001D5D RID: 7517
		private const int _tabWidth = 4;

		// Token: 0x04001D5E RID: 7518
		private const string _markerUL = "[*+-]";

		// Token: 0x04001D5F RID: 7519
		private const string _markerOL = "\\d+[.]";

		// Token: 0x04001D60 RID: 7520
		private string _emptyElementSuffix;

		// Token: 0x04001D61 RID: 7521
		private bool _linkEmails;

		// Token: 0x04001D62 RID: 7522
		private bool _strictBoldItalic;

		// Token: 0x04001D63 RID: 7523
		private bool _autoNewlines;

		// Token: 0x04001D64 RID: 7524
		private bool _autoHyperlink;

		// Token: 0x04001D65 RID: 7525
		private bool _encodeProblemUrlCharacters;

		// Token: 0x04001D66 RID: 7526
		private static readonly Dictionary<string, string> _escapeTable;

		// Token: 0x04001D67 RID: 7527
		private static readonly Dictionary<string, string> _invertedEscapeTable;

		// Token: 0x04001D68 RID: 7528
		private static readonly Dictionary<string, string> _backslashEscapeTable;

		// Token: 0x04001D69 RID: 7529
		private readonly Dictionary<string, string> _urls;

		// Token: 0x04001D6A RID: 7530
		private readonly Dictionary<string, string> _titles;

		// Token: 0x04001D6B RID: 7531
		private readonly Dictionary<string, string> _htmlBlocks;

		// Token: 0x04001D6C RID: 7532
		private int _listLevel;

		// Token: 0x04001D6D RID: 7533
		private static Regex _newlinesLeadingTrailing = new Regex("^\\n+|\\n+\\z", RegexOptions.Compiled);

		// Token: 0x04001D6E RID: 7534
		private static Regex _newlinesMultiple = new Regex("\\n{2,}", RegexOptions.Compiled);

		// Token: 0x04001D6F RID: 7535
		private static Regex _leadingWhitespace = new Regex("^[ ]*", RegexOptions.Compiled);

		// Token: 0x04001D70 RID: 7536
		private static string _nestedBracketsPattern;

		// Token: 0x04001D71 RID: 7537
		private static string _nestedParensPattern;

		// Token: 0x04001D72 RID: 7538
		private static Regex _linkDef = new Regex(string.Format("\r\n                        ^[ ]{{0,{0}}}\\[(.+)\\]:  # id = $1\r\n                          [ ]*\r\n                          \\n?                   # maybe *one* newline\r\n                          [ ]*\r\n                        <?(\\S+?)>?              # url = $2\r\n                          [ ]*\r\n                          \\n?                   # maybe one newline\r\n                          [ ]*\r\n                        (?:\r\n                            (?<=\\s)             # lookbehind for whitespace\r\n                            [\"(]\r\n                            (.+?)               # title = $3\r\n                            [\")]\r\n                            [ ]*\r\n                        )?                      # title is optional\r\n                        (?:\\n+|\\Z)", 3), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D73 RID: 7539
		private static Regex _blocksHtml = new Regex(Markdown.GetBlockPattern(), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D74 RID: 7540
		private static Regex _htmlTokens = new Regex("\r\n            (<!(?:--.*?--\\s*)+>)|        # match <!-- foo -->\r\n            (<\\?.*?\\?>)|                 # match <?foo?> " + Markdown.RepeatString(" \r\n            (<[A-Za-z\\/!$](?:[^<>]|", 6) + Markdown.RepeatString(")*>)", 6) + " # match <tag> and </tag>", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D75 RID: 7541
		private static Regex _anchorRef = new Regex(string.Format("\r\n            (                               # wrap whole match in $1\r\n                \\[\r\n                    ({0})                   # link text = $2\r\n                \\]\r\n\r\n                [ ]?                        # one optional space\r\n                (?:\\n[ ]*)?                 # one optional newline followed by spaces\r\n\r\n                \\[\r\n                    (.*?)                   # id = $3\r\n                \\]\r\n            )", Markdown.GetNestedBracketsPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D76 RID: 7542
		private static Regex _anchorInline = new Regex(string.Format("\r\n                (                           # wrap whole match in $1\r\n                    \\[\r\n                        ({0})               # link text = $2\r\n                    \\]\r\n                    \\(                      # literal paren\r\n                        [ ]*\r\n                        ({1})               # href = $3\r\n                        [ ]*\r\n                        (                   # $4\r\n                        (['\"])           # quote char = $5\r\n                        (.*?)               # title = $6\r\n                        \\5                  # matching quote\r\n                        [ ]*                # ignore any spaces between closing quote and )\r\n                        )?                  # title is optional\r\n                    \\)\r\n                )", Markdown.GetNestedBracketsPattern(), Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D77 RID: 7543
		private static Regex _anchorRefShortcut = new Regex("\r\n            (                               # wrap whole match in $1\r\n              \\[\r\n                 ([^\\[\\]]+)                 # link text = $2; can't contain [ or ]\r\n              \\]\r\n            )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D78 RID: 7544
		private static Regex _imagesRef = new Regex("\r\n                    (               # wrap whole match in $1\r\n                    !\\[\r\n                        (.*?)       # alt text = $2\r\n                    \\]\r\n\r\n                    [ ]?            # one optional space\r\n                    (?:\\n[ ]*)?     # one optional newline followed by spaces\r\n\r\n                    \\[\r\n                        (.*?)       # id = $3\r\n                    \\]\r\n\r\n                    )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D79 RID: 7545
		private static Regex _imagesInline = new Regex(string.Format("\r\n              (                     # wrap whole match in $1\r\n                !\\[\r\n                    (.*?)           # alt text = $2\r\n                \\]\r\n                \\s?                 # one optional whitespace character\r\n                \\(                  # literal paren\r\n                    [ ]*\r\n                    ({0})           # href = $3\r\n                    [ ]*\r\n                    (               # $4\r\n                    (['\"])       # quote char = $5\r\n                    (.*?)           # title = $6\r\n                    \\5              # matching quote\r\n                    [ ]*\r\n                    )?              # title is optional\r\n                \\)\r\n              )", Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D7A RID: 7546
		private static Regex _headerSetext = new Regex("\r\n                ^(.+?)\r\n                [ ]*\r\n                \\n\r\n                (=+|-+)     # $1 = string of ='s or -'s\r\n                [ ]*\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D7B RID: 7547
		private static Regex _headerAtx = new Regex("\r\n                ^(\\#{1,6})  # $1 = string of #'s\r\n                [ ]*\r\n                (.+?)       # $2 = Header text\r\n                [ ]*\r\n                \\#*         # optional closing #'s (not counted)\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D7C RID: 7548
		private static Regex _horizontalRules = new Regex("\r\n            ^[ ]{0,3}         # Leading space\r\n                ([-*_])       # $1: First marker\r\n                (?>           # Repeated marker group\r\n                    [ ]{0,2}  # Zero, one, or two spaces.\r\n                    \\1        # Marker character\r\n                ){2,}         # Group repeated at least twice\r\n                [ ]*          # Trailing spaces\r\n                $             # End of line.\r\n            ", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D7D RID: 7549
		private static string _wholeList = string.Format("\r\n            (                               # $1 = whole list\r\n              (                             # $2\r\n                [ ]{{0,{1}}}\r\n                ({0})                       # $3 = first list item marker\r\n                [ ]+\r\n              )\r\n              (?s:.+?)\r\n              (                             # $4\r\n                  \\z\r\n                |\r\n                  \\n{{2,}}\r\n                  (?=\\S)\r\n                  (?!                       # Negative lookahead for another list item marker\r\n                    [ ]*\r\n                    {0}[ ]+\r\n                  )\r\n              )\r\n            )", string.Format("(?:{0}|{1})", "[*+-]", "\\d+[.]"), 3);

		// Token: 0x04001D7E RID: 7550
		private static Regex _listNested = new Regex("^" + Markdown._wholeList, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D7F RID: 7551
		private static Regex _listTopLevel = new Regex("(?:(?<=\\n\\n)|\\A\\n?)" + Markdown._wholeList, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D80 RID: 7552
		private static Regex _codeBlock = new Regex(string.Format("\r\n                    (?:\\n\\n|\\A\\n?)\r\n                    (                        # $1 = the code block -- one or more lines, starting with a space\r\n                    (?:\r\n                        (?:[ ]{{{0}}})       # Lines must start with a tab-width of spaces\r\n                        .*\\n+\r\n                    )+\r\n                    )\r\n                    ((?=^[ ]{{0,{0}}}\\S)|\\Z) # Lookahead for non-space at line-start, or end of doc", 4), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D81 RID: 7553
		private static Regex _codeSpan = new Regex("\r\n                    (?<!\\\\)   # Character before opening ` can't be a backslash\r\n                    (`+)      # $1 = Opening run of `\r\n                    (.+?)     # $2 = The code block\r\n                    (?<!`)\r\n                    \\1\r\n                    (?!`)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D82 RID: 7554
		private static Regex _bold = new Regex("(\\*\\*|__) (?=\\S) (.+?[*_]*) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D83 RID: 7555
		private static Regex _strictBold = new Regex("([\\W_]|^) (\\*\\*|__) (?=\\S) ([^\\r]*?\\S[\\*_]*) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D84 RID: 7556
		private static Regex _italic = new Regex("(\\*|_) (?=\\S) (.+?) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D85 RID: 7557
		private static Regex _strictItalic = new Regex("([\\W_]|^) (\\*|_) (?=\\S) ([^\\r\\*_]*?\\S) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D86 RID: 7558
		private static Regex _blockquote = new Regex("\r\n            (                           # Wrap whole match in $1\r\n                (\r\n                ^[ ]*>[ ]?              # '>' at the start of a line\r\n                    .+\\n                # rest of the first line\r\n                (.+\\n)*                 # subsequent consecutive lines\r\n                \\n*                     # blanks\r\n                )+\r\n            )", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04001D87 RID: 7559
		private static Regex _autolinkBare = new Regex("(^|\\s)(https?|ftp)(://[-A-Z0-9+&@#/%?=~_|\\[\\]\\(\\)!:,\\.;]*[-A-Z0-9+&@#/%=~_|\\[\\]])($|\\W)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04001D88 RID: 7560
		private static Regex _outDent = new Regex("^[ ]{1," + 4 + "}", RegexOptions.Multiline | RegexOptions.Compiled);

		// Token: 0x04001D89 RID: 7561
		private static Regex _codeEncoder = new Regex("&|<|>|\\\\|\\*|_|\\{|\\}|\\[|\\]", RegexOptions.Compiled);

		// Token: 0x04001D8A RID: 7562
		private static Regex _amps = new Regex("&(?!(#[0-9]+)|(#[xX][a-fA-F0-9])|([a-zA-Z][a-zA-Z0-9]*);)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001D8B RID: 7563
		private static Regex _angles = new Regex("<(?![A-Za-z/?\\$!])", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001D8C RID: 7564
		private static Regex _backslashEscapes;

		// Token: 0x04001D8D RID: 7565
		private static Regex _unescapes = new Regex("\u001a\\d+\u001a", RegexOptions.Compiled);

		// Token: 0x04001D8E RID: 7566
		private static char[] _problemUrlChars = "\"'*()[]$:".ToCharArray();

		// Token: 0x02000B52 RID: 2898
		private enum TokenType
		{
			// Token: 0x04001D90 RID: 7568
			Text,
			// Token: 0x04001D91 RID: 7569
			Tag
		}

		// Token: 0x02000B53 RID: 2899
		private struct Token
		{
			// Token: 0x06006D52 RID: 27986 RVA: 0x001964E5 File Offset: 0x001946E5
			public Token(Markdown.TokenType type, string value)
			{
				this.Type = type;
				this.Value = value;
			}

			// Token: 0x04001D92 RID: 7570
			public Markdown.TokenType Type;

			// Token: 0x04001D93 RID: 7571
			public string Value;
		}
	}
}
