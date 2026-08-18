using System;
using System.Text;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x020011E4 RID: 4580
	[Serializable]
	public class SpellCheckError
	{
		// Token: 0x0600BD44 RID: 48452 RVA: 0x0029F175 File Offset: 0x0029D375
		public SpellCheckError() : this(0, null, new string[0], false)
		{
		}

		// Token: 0x0600BD45 RID: 48453 RVA: 0x0029F186 File Offset: 0x0029D386
		public SpellCheckError(int wordIndex, ITextWord badWord, string[] suggestions, bool checkAllCaps)
		{
			this.wordIndex = wordIndex;
			this.badWord = badWord;
			this.mistakenWord = badWord.HtmlWord;
			this.offset = badWord.Offset;
			this.suggestions = suggestions;
			this.checkAllCaps = checkAllCaps;
		}

		// Token: 0x0600BD46 RID: 48454 RVA: 0x0029F1C3 File Offset: 0x0029D3C3
		internal static void RemoveLastChar(StringBuilder target)
		{
			if (target.Length > 1)
			{
				target.Remove(target.Length - 1, 1);
			}
		}

		// Token: 0x17003D12 RID: 15634
		// (get) Token: 0x0600BD47 RID: 48455 RVA: 0x0029F1DE File Offset: 0x0029D3DE
		// (set) Token: 0x0600BD48 RID: 48456 RVA: 0x0029F1E6 File Offset: 0x0029D3E6
		public int WordIndex
		{
			get
			{
				return this.wordIndex;
			}
			set
			{
				this.wordIndex = value;
			}
		}

		// Token: 0x17003D13 RID: 15635
		// (get) Token: 0x0600BD49 RID: 48457 RVA: 0x0029F1EF File Offset: 0x0029D3EF
		// (set) Token: 0x0600BD4A RID: 48458 RVA: 0x0029F1F7 File Offset: 0x0029D3F7
		public int OffsetInText
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17003D14 RID: 15636
		// (get) Token: 0x0600BD4B RID: 48459 RVA: 0x0029F200 File Offset: 0x0029D400
		// (set) Token: 0x0600BD4C RID: 48460 RVA: 0x0029F208 File Offset: 0x0029D408
		public string MistakenWord
		{
			get
			{
				return this.mistakenWord;
			}
			set
			{
				this.mistakenWord = value;
			}
		}

		// Token: 0x17003D15 RID: 15637
		// (get) Token: 0x0600BD4D RID: 48461 RVA: 0x0029F211 File Offset: 0x0029D411
		// (set) Token: 0x0600BD4E RID: 48462 RVA: 0x0029F219 File Offset: 0x0029D419
		public string[] Suggestions
		{
			get
			{
				return this.suggestions;
			}
			set
			{
				this.suggestions = value;
			}
		}

		// Token: 0x17003D16 RID: 15638
		// (get) Token: 0x0600BD4F RID: 48463 RVA: 0x0029F222 File Offset: 0x0029D422
		internal string SuggestionString
		{
			get
			{
				return this.BuildSuggestionString().ToString();
			}
		}

		// Token: 0x0600BD50 RID: 48464 RVA: 0x0029F230 File Offset: 0x0029D430
		private StringBuilder BuildSuggestionString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			string[] strings = this.suggestions;
			if (this.badWord.StartsWithUpper())
			{
				SpellCheckError.MakeFirstCapital(strings);
			}
			if (this.checkAllCaps && this.badWord.AllUpper())
			{
				SpellCheckError.MakeUpper(this.suggestions);
			}
			foreach (string text in this.suggestions)
			{
				stringBuilder.Append(string.Format("'{0}',", text.Replace("'", "\\'")));
			}
			SpellCheckError.RemoveLastChar(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder;
		}

		// Token: 0x0600BD51 RID: 48465 RVA: 0x0029F2D4 File Offset: 0x0029D4D4
		private static void MakeUpper(string[] strings)
		{
			for (int i = 0; i < strings.Length; i++)
			{
				strings[i] = strings[i].ToUpper();
			}
		}

		// Token: 0x0600BD52 RID: 48466 RVA: 0x0029F2FC File Offset: 0x0029D4FC
		private static void MakeFirstCapital(string[] strings)
		{
			for (int i = 0; i < strings.Length; i++)
			{
				string text = strings[i];
				if (!string.IsNullOrEmpty(text))
				{
					char c = text.ToUpper()[0];
					string arg = text.Substring(1, text.Length - 1);
					strings[i] = c + arg;
				}
			}
		}

		// Token: 0x040031C8 RID: 12744
		[NonSerialized]
		private readonly ITextWord badWord;

		// Token: 0x040031C9 RID: 12745
		private int wordIndex;

		// Token: 0x040031CA RID: 12746
		private int offset;

		// Token: 0x040031CB RID: 12747
		private string mistakenWord;

		// Token: 0x040031CC RID: 12748
		private string[] suggestions;

		// Token: 0x040031CD RID: 12749
		private readonly bool checkAllCaps;
	}
}
