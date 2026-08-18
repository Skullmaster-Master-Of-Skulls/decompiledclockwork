using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI.HtmlParsing
{
	// Token: 0x020011DC RID: 4572
	internal class HtmlTokenizer
	{
		// Token: 0x17003CEC RID: 15596
		// (get) Token: 0x0600BCC5 RID: 48325 RVA: 0x0029E0AC File Offset: 0x0029C2AC
		// (set) Token: 0x0600BCC6 RID: 48326 RVA: 0x0029E0B4 File Offset: 0x0029C2B4
		internal string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x0600BCC7 RID: 48327 RVA: 0x0029E0BD File Offset: 0x0029C2BD
		internal char[] Characters()
		{
			return this._characters;
		}

		// Token: 0x17003CED RID: 15597
		// (get) Token: 0x0600BCC8 RID: 48328 RVA: 0x0029E0C5 File Offset: 0x0029C2C5
		// (set) Token: 0x0600BCC9 RID: 48329 RVA: 0x0029E0CD File Offset: 0x0029C2CD
		internal StringBuilder CurrentWord
		{
			get
			{
				return this._currentWord;
			}
			set
			{
				this._currentWord = value;
			}
		}

		// Token: 0x0600BCCA RID: 48330 RVA: 0x0029E0D6 File Offset: 0x0029C2D6
		internal HtmlTokenizer(string text) : this(text, true, FragmentIgnoreOptions.All)
		{
		}

		// Token: 0x0600BCCB RID: 48331 RVA: 0x0029E0E1 File Offset: 0x0029C2E1
		internal HtmlTokenizer(string text, bool checkWordsWNumbers) : this(text, checkWordsWNumbers, FragmentIgnoreOptions.All)
		{
		}

		// Token: 0x0600BCCC RID: 48332 RVA: 0x0029E0EC File Offset: 0x0029C2EC
		internal HtmlTokenizer(string text, bool checkWordsWNumbers, FragmentIgnoreOptions ignoreFragments)
		{
			this.Text = this.RemoveIgnoredFragments(text);
			this.CheckWordsWNumbers = checkWordsWNumbers;
			this.IgnoreFragments = ignoreFragments;
			this._characters = (this.Text + Convert.ToChar(0)).ToCharArray();
			this.handler = new TextHandler(this);
			this.lexer = new HtmlLexer();
		}

		// Token: 0x0600BCCD RID: 48333 RVA: 0x0029E178 File Offset: 0x0029C378
		private string RemoveIgnoredFragments(string inputText)
		{
			Regex regex = new Regex("<(!--)([\\s\\S]*?)(--)>", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex("<(object|style|script)([\\s\\S]*?)(\\/\\1)>", RegexOptions.IgnoreCase);
			MatchEvaluator evaluator = new MatchEvaluator(this.ReplaceWithEmpty);
			string input = regex.Replace(inputText, evaluator);
			return regex2.Replace(input, evaluator);
		}

		// Token: 0x0600BCCE RID: 48334 RVA: 0x0029E1C0 File Offset: 0x0029C3C0
		internal string ReplaceWithEmpty(Match m)
		{
			int length = m.Value.Length;
			string empty = string.Empty;
			return empty.PadLeft(length);
		}

		// Token: 0x17003CEE RID: 15598
		// (get) Token: 0x0600BCCF RID: 48335 RVA: 0x0029E1E8 File Offset: 0x0029C3E8
		public bool IgnoreUrls
		{
			get
			{
				return (this.IgnoreFragments & FragmentIgnoreOptions.Urls) > FragmentIgnoreOptions.None;
			}
		}

		// Token: 0x17003CEF RID: 15599
		// (get) Token: 0x0600BCD0 RID: 48336 RVA: 0x0029E1F5 File Offset: 0x0029C3F5
		public bool IgnoreFileNames
		{
			get
			{
				return (this.IgnoreFragments & FragmentIgnoreOptions.FileNames) > FragmentIgnoreOptions.None;
			}
		}

		// Token: 0x17003CF0 RID: 15600
		// (get) Token: 0x0600BCD1 RID: 48337 RVA: 0x0029E202 File Offset: 0x0029C402
		public bool IgnoreEmailAddresses
		{
			get
			{
				return (this.IgnoreFragments & FragmentIgnoreOptions.EmailAddresses) > FragmentIgnoreOptions.None;
			}
		}

		// Token: 0x17003CF1 RID: 15601
		// (get) Token: 0x0600BCD2 RID: 48338 RVA: 0x0029E20F File Offset: 0x0029C40F
		public bool IgnoreNone
		{
			get
			{
				return this.IgnoreFragments == FragmentIgnoreOptions.None;
			}
		}

		// Token: 0x17003CF2 RID: 15602
		// (get) Token: 0x0600BCD3 RID: 48339 RVA: 0x0029E21A File Offset: 0x0029C41A
		public bool IgnoreAll
		{
			get
			{
				return this.IgnoreFragments == FragmentIgnoreOptions.All;
			}
		}

		// Token: 0x0600BCD4 RID: 48340 RVA: 0x0029E228 File Offset: 0x0029C428
		internal void AddWord(int position)
		{
			TextWord target = this.WordForPosition(position);
			this.AddWord(target);
			this.ResetCurrentWord();
		}

		// Token: 0x0600BCD5 RID: 48341 RVA: 0x0029E24A File Offset: 0x0029C44A
		internal void ResetCurrentWord()
		{
			this.CurrentWord.Length = 0;
		}

		// Token: 0x0600BCD6 RID: 48342 RVA: 0x0029E258 File Offset: 0x0029C458
		internal void AddWord(TextWord target)
		{
			if (this.VerifyWord(target))
			{
				this.words.Add(target);
			}
		}

		// Token: 0x0600BCD7 RID: 48343 RVA: 0x0029E270 File Offset: 0x0029C470
		internal TextWord WordForPosition(int position)
		{
			int offset = position - this.CurrentWord.Length;
			return new TextWord(this.CurrentWord.ToString(), offset);
		}

		// Token: 0x0600BCD8 RID: 48344 RVA: 0x0029E29C File Offset: 0x0029C49C
		private bool VerifyWord(TextWord target)
		{
			string word = target.Word;
			int num = 0;
			while (num < word.Length && !char.IsLetter(word[num]))
			{
				num++;
			}
			if (!this.CheckWordsWNumbers && num < word.Length)
			{
				num = 0;
				while (num < word.Length && !char.IsDigit(word[num]))
				{
					num++;
				}
				if (num != word.Length)
				{
					return false;
				}
				num = 0;
			}
			return num < word.Length;
		}

		// Token: 0x0600BCD9 RID: 48345 RVA: 0x0029E317 File Offset: 0x0029C517
		internal void StartParse()
		{
			this.lexer = new HtmlLexer();
			this.lexer.Text = this.Text;
		}

		// Token: 0x0600BCDA RID: 48346 RVA: 0x0029E335 File Offset: 0x0029C535
		internal bool HasTokens()
		{
			return this.lexer.HasTokens();
		}

		// Token: 0x0600BCDB RID: 48347 RVA: 0x0029E342 File Offset: 0x0029C542
		internal Token NextToken()
		{
			return this.lexer.NextToken();
		}

		// Token: 0x0600BCDC RID: 48348 RVA: 0x0029E34F File Offset: 0x0029C54F
		internal static string TagText(Token target)
		{
			return target.Contents.ToString();
		}

		// Token: 0x0600BCDD RID: 48349 RVA: 0x0029E35C File Offset: 0x0029C55C
		internal static string TagName(Token target)
		{
			Regex regex = new Regex("<(\\/|!|\\?)?(\\w+)[^>]*>");
			Match match = regex.Match(HtmlTokenizer.TagText(target));
			return match.Groups[2].Value.ToLower();
		}

		// Token: 0x0600BCDE RID: 48350 RVA: 0x0029E398 File Offset: 0x0029C598
		internal ArrayList Tokenize()
		{
			this.StartParse();
			while (this.lexer.HasTokens())
			{
				Token token = this.lexer.NextToken();
				switch (token.Type)
				{
				case TokenType.TagStart:
					this.tagCharacter = true;
					continue;
				case TokenType.TagEnd:
					this.tagCharacter = false;
					continue;
				case TokenType.OpenTag:
					this.thisTag = token;
					this.handler.Handle(token);
					continue;
				case TokenType.CloseTag:
					this.thisTag = null;
					this.handler.Handle(token);
					continue;
				}
				this.HandleToken(token);
			}
			this.handler.EndOfText();
			return this.words;
		}

		// Token: 0x0600BCDF RID: 48351 RVA: 0x0029E442 File Offset: 0x0029C642
		private void HandleToken(Token current)
		{
			if (!this.tagCharacter)
			{
				this.handler.Handle(current);
			}
		}

		// Token: 0x0400319A RID: 12698
		private HtmlLexer lexer;

		// Token: 0x0400319B RID: 12699
		private bool CheckWordsWNumbers = true;

		// Token: 0x0400319C RID: 12700
		private FragmentIgnoreOptions IgnoreFragments = FragmentIgnoreOptions.All;

		// Token: 0x0400319D RID: 12701
		private ArrayList words = new ArrayList();

		// Token: 0x0400319E RID: 12702
		private TextHandler handler;

		// Token: 0x0400319F RID: 12703
		private Token thisTag;

		// Token: 0x040031A0 RID: 12704
		private string _text;

		// Token: 0x040031A1 RID: 12705
		private char[] _characters;

		// Token: 0x040031A2 RID: 12706
		private StringBuilder _currentWord = new StringBuilder();

		// Token: 0x040031A3 RID: 12707
		private bool tagCharacter;
	}
}
