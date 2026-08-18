using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI.HtmlParsing
{
	// Token: 0x020011DD RID: 4573
	internal class TextHandler
	{
		// Token: 0x0600BCE0 RID: 48352 RVA: 0x0029E458 File Offset: 0x0029C658
		private void ResetFragment()
		{
			if (this.tokenizer.IgnoreNone || !this.ValidateFragment())
			{
				foreach (object obj in this.words)
				{
					TextWord target = (TextWord)obj;
					this.tokenizer.AddWord(target);
				}
			}
			this.words.Clear();
			this.currentFragment = new StringBuilder();
		}

		// Token: 0x0600BCE1 RID: 48353 RVA: 0x0029E4E4 File Offset: 0x0029C6E4
		private bool Matches(string text, Regex matcher)
		{
			Match match = matcher.Match(text);
			return match.Success;
		}

		// Token: 0x0600BCE2 RID: 48354 RVA: 0x0029E500 File Offset: 0x0029C700
		private bool ValidateFragment()
		{
			string text = this.currentFragment.ToString();
			return (this.tokenizer.IgnoreUrls && this.Matches(text, this.urlMatcher)) || (this.tokenizer.IgnoreEmailAddresses && this.Matches(text, this.emailMatcher)) || (this.tokenizer.IgnoreFileNames && this.Matches(text, this.fileMatcher));
		}

		// Token: 0x0600BCE3 RID: 48355 RVA: 0x0029E574 File Offset: 0x0029C774
		internal TextHandler(HtmlTokenizer tokenizer)
		{
			this.tokenizer = tokenizer;
			this.urlMatcher = new Regex("(mailto\\:|(news|(ht|f)tp(s?))\\://)((\\S+)|(\\S+)( #([^#]+)#)?)", RegexOptions.Compiled);
			this.emailMatcher = new Regex("^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\\-+)|([A-Za-z0-9]+\\.+)|([A-Za-z0-9]+\\++))*[A-Za-z0-9]+@((\\w+\\-+)|(\\w+\\.))*\\w{1,63}\\.[a-zA-Z]{2,6}$", RegexOptions.Compiled);
			this.fileMatcher = new Regex("^(([a-zA-Z]\\:)|\\\\)(\\\\[^\\\\/:*?<>\"|]*(?<![ ]))*(\\.[a-zA-Z]{2,6})", RegexOptions.Compiled);
		}

		// Token: 0x0600BCE4 RID: 48356 RVA: 0x0029E5D8 File Offset: 0x0029C7D8
		internal void Handle(Token currentToken)
		{
			if (char.IsLetterOrDigit(currentToken.Contents[0]) || (this.InWordPunctuation(currentToken.Contents[0]) && this.HasLettersAfterPunctuation(currentToken.Offset)))
			{
				this.tokenizer.CurrentWord.Append(currentToken.Contents);
			}
			else
			{
				this.AddWord(currentToken.Offset);
			}
			this.HandleEntity(currentToken);
			this.HandleTag(currentToken);
			if ((currentToken.Type == TokenType.Character || currentToken.Type == TokenType.HtmlEntity) && !char.IsWhiteSpace(currentToken.Contents[0]))
			{
				this.currentFragment.Append(currentToken.Contents);
				return;
			}
			this.ResetFragment();
		}

		// Token: 0x0600BCE5 RID: 48357 RVA: 0x0029E68A File Offset: 0x0029C88A
		internal bool InWordPunctuation(char character)
		{
			return character == '\'';
		}

		// Token: 0x0600BCE6 RID: 48358 RVA: 0x0029E691 File Offset: 0x0029C891
		internal bool HasLettersAfterPunctuation(int position)
		{
			return checked(position + 2 < this.tokenizer.Characters().Length && this.tokenizer.CurrentWord.Length > 0 && char.IsLetterOrDigit(this.tokenizer.Characters()[position + 1]));
		}

		// Token: 0x0600BCE7 RID: 48359 RVA: 0x0029E6D0 File Offset: 0x0029C8D0
		protected virtual void AddWord(int position)
		{
			if (this.tokenizer.CurrentWord.Length > 0)
			{
				TextWord value = this.tokenizer.WordForPosition(position);
				this.words.Add(value);
				this.tokenizer.ResetCurrentWord();
			}
		}

		// Token: 0x0600BCE8 RID: 48360 RVA: 0x0029E715 File Offset: 0x0029C915
		internal void HandleTag(Token currentToken)
		{
			if (currentToken.Type == TokenType.OpenTag || currentToken.Type == TokenType.CloseTag)
			{
				this.AddWord(currentToken.Offset);
			}
		}

		// Token: 0x0600BCE9 RID: 48361 RVA: 0x0029E738 File Offset: 0x0029C938
		internal void HandleEntity(Token current)
		{
			if (current.Type == TokenType.HtmlEntity)
			{
				char c = HttpUtility.HtmlDecode(current.Contents)[0];
				if (char.IsLetterOrDigit(c))
				{
					this.tokenizer.CurrentWord.Append(current.Contents);
				}
			}
		}

		// Token: 0x0600BCEA RID: 48362 RVA: 0x0029E77F File Offset: 0x0029C97F
		internal void EndOfText()
		{
			this.AddWord(this.tokenizer.Text.Length);
			this.ResetFragment();
		}

		// Token: 0x040031A4 RID: 12708
		private const string urlPattern = "(mailto\\:|(news|(ht|f)tp(s?))\\://)((\\S+)|(\\S+)( #([^#]+)#)?)";

		// Token: 0x040031A5 RID: 12709
		private const string emailPattern = "^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\\-+)|([A-Za-z0-9]+\\.+)|([A-Za-z0-9]+\\++))*[A-Za-z0-9]+@((\\w+\\-+)|(\\w+\\.))*\\w{1,63}\\.[a-zA-Z]{2,6}$";

		// Token: 0x040031A6 RID: 12710
		private const string filePattern = "^(([a-zA-Z]\\:)|\\\\)(\\\\[^\\\\/:*?<>\"|]*(?<![ ]))*(\\.[a-zA-Z]{2,6})";

		// Token: 0x040031A7 RID: 12711
		private Regex urlMatcher;

		// Token: 0x040031A8 RID: 12712
		private Regex emailMatcher;

		// Token: 0x040031A9 RID: 12713
		private Regex fileMatcher;

		// Token: 0x040031AA RID: 12714
		private HtmlTokenizer tokenizer;

		// Token: 0x040031AB RID: 12715
		private StringBuilder currentFragment = new StringBuilder();

		// Token: 0x040031AC RID: 12716
		private ArrayList words = new ArrayList();
	}
}
