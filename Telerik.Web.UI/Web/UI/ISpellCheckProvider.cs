using System;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x020011E0 RID: 4576
	public interface ISpellCheckProvider
	{
		// Token: 0x0600BCF2 RID: 48370
		ITextWord GetWord(int index);

		// Token: 0x17003CF6 RID: 15606
		// (get) Token: 0x0600BCF3 RID: 48371
		int WordCount { get; }

		// Token: 0x0600BCF4 RID: 48372
		bool CheckWord(ITextWord current, ITextWord previous);

		// Token: 0x0600BCF5 RID: 48373
		string[] GetSuggestions(ITextWord word);

		// Token: 0x0600BCF6 RID: 48374
		void Close();

		// Token: 0x17003CF7 RID: 15607
		// (get) Token: 0x0600BCF7 RID: 48375
		// (set) Token: 0x0600BCF8 RID: 48376
		WordIgnoreOptions WordIgnoreOptions { get; set; }

		// Token: 0x17003CF8 RID: 15608
		// (get) Token: 0x0600BCF9 RID: 48377
		// (set) Token: 0x0600BCFA RID: 48378
		string Text { get; set; }

		// Token: 0x17003CF9 RID: 15609
		// (get) Token: 0x0600BCFB RID: 48379
		// (set) Token: 0x0600BCFC RID: 48380
		string Language { get; set; }
	}
}
