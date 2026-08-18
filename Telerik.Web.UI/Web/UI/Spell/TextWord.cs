using System;
using System.Web;

namespace Telerik.Web.UI.Spell
{
	// Token: 0x020011ED RID: 4589
	public class TextWord : ITextWord
	{
		// Token: 0x17003D29 RID: 15657
		// (get) Token: 0x0600BD90 RID: 48528 RVA: 0x0029FB34 File Offset: 0x0029DD34
		public string Word
		{
			get
			{
				return HttpUtility.HtmlDecode(this.word);
			}
		}

		// Token: 0x17003D2A RID: 15658
		// (get) Token: 0x0600BD91 RID: 48529 RVA: 0x0029FB41 File Offset: 0x0029DD41
		public string HtmlWord
		{
			get
			{
				return this.word;
			}
		}

		// Token: 0x17003D2B RID: 15659
		// (get) Token: 0x0600BD92 RID: 48530 RVA: 0x0029FB49 File Offset: 0x0029DD49
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x0600BD93 RID: 48531 RVA: 0x0029FB51 File Offset: 0x0029DD51
		public TextWord(string word, int offset)
		{
			this.word = word;
			this.offset = offset;
		}

		// Token: 0x0600BD94 RID: 48532 RVA: 0x0029FB67 File Offset: 0x0029DD67
		public bool StartsWithUpper()
		{
			return this.word.ToUpper()[0] == this.word[0] && char.IsLetter(this.word[0]);
		}

		// Token: 0x0600BD95 RID: 48533 RVA: 0x0029FB9B File Offset: 0x0029DD9B
		public bool AllUpper()
		{
			return this.word.ToUpper() == this.word;
		}

		// Token: 0x0600BD96 RID: 48534 RVA: 0x0029FBB3 File Offset: 0x0029DDB3
		public void MakeUpper()
		{
			this.word = this.word.ToUpper();
		}

		// Token: 0x040031D9 RID: 12761
		private string word;

		// Token: 0x040031DA RID: 12762
		private readonly int offset;
	}
}
