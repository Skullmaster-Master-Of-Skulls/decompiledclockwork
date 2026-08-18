using System;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x020011E1 RID: 4577
	internal class TelerikSpellCheckProvider : ISpellCheckProvider
	{
		// Token: 0x0600BCFD RID: 48381 RVA: 0x0029E7E3 File Offset: 0x0029C9E3
		internal TelerikSpellCheckProvider(SpellChecker checker)
		{
			this.checker = checker;
		}

		// Token: 0x0600BCFE RID: 48382 RVA: 0x0029E7F2 File Offset: 0x0029C9F2
		public ITextWord GetWord(int index)
		{
			return this.checker.GetWord(index);
		}

		// Token: 0x17003CFA RID: 15610
		// (get) Token: 0x0600BCFF RID: 48383 RVA: 0x0029E800 File Offset: 0x0029CA00
		public int WordCount
		{
			get
			{
				return this.checker.WordCount;
			}
		}

		// Token: 0x0600BD00 RID: 48384 RVA: 0x0029E810 File Offset: 0x0029CA10
		public bool CheckWord(ITextWord current, ITextWord previous)
		{
			string word = string.Empty;
			string sLastWord = string.Empty;
			if (current != null)
			{
				word = current.Word;
			}
			if (previous != null)
			{
				sLastWord = previous.Word;
			}
			return this.checker.CheckWord(word, sLastWord);
		}

		// Token: 0x0600BD01 RID: 48385 RVA: 0x0029E84A File Offset: 0x0029CA4A
		public string[] GetSuggestions(ITextWord word)
		{
			return this.checker.GetSuggestions(word.Word);
		}

		// Token: 0x0600BD02 RID: 48386 RVA: 0x0029E85D File Offset: 0x0029CA5D
		public void Close()
		{
		}

		// Token: 0x17003CFB RID: 15611
		// (get) Token: 0x0600BD03 RID: 48387 RVA: 0x0029E85F File Offset: 0x0029CA5F
		// (set) Token: 0x0600BD04 RID: 48388 RVA: 0x0029E86C File Offset: 0x0029CA6C
		WordIgnoreOptions ISpellCheckProvider.WordIgnoreOptions
		{
			get
			{
				return this.checker.WordIgnoreOptions;
			}
			set
			{
				this.checker.WordIgnoreOptions = value;
			}
		}

		// Token: 0x17003CFC RID: 15612
		// (get) Token: 0x0600BD05 RID: 48389 RVA: 0x0029E87A File Offset: 0x0029CA7A
		// (set) Token: 0x0600BD06 RID: 48390 RVA: 0x0029E887 File Offset: 0x0029CA87
		string ISpellCheckProvider.Text
		{
			get
			{
				return this.checker.Text;
			}
			set
			{
				this.checker.Text = value;
			}
		}

		// Token: 0x17003CFD RID: 15613
		// (get) Token: 0x0600BD07 RID: 48391 RVA: 0x0029E895 File Offset: 0x0029CA95
		// (set) Token: 0x0600BD08 RID: 48392 RVA: 0x0029E8A2 File Offset: 0x0029CAA2
		string ISpellCheckProvider.Language
		{
			get
			{
				return this.checker.DictionaryLanguage;
			}
			set
			{
				this.checker.DictionaryLanguage = value;
			}
		}

		// Token: 0x040031B7 RID: 12727
		private readonly SpellChecker checker;
	}
}
