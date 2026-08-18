using System;
using System.Collections;
using System.Text;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x020011E5 RID: 4581
	[Serializable]
	public class SpellCheckErrors : CollectionBase
	{
		// Token: 0x0600BD53 RID: 48467 RVA: 0x0029F34F File Offset: 0x0029D54F
		private void CreateCollections()
		{
			this.badWords = new Hashtable();
		}

		// Token: 0x0600BD54 RID: 48468 RVA: 0x0029F35C File Offset: 0x0029D55C
		internal void Add(int wordIndex, ITextWord badWord, string[] suggestions)
		{
			if (badWord.AllUpper() && !this.checkAllCaps)
			{
				return;
			}
			SpellCheckError value = new SpellCheckError(wordIndex, badWord, suggestions, this.checkAllCaps);
			this.Add(value);
			if (!this.Contains(badWord.Word))
			{
				this.badWords.Add(badWord.Word, value);
			}
		}

		// Token: 0x0600BD55 RID: 48469 RVA: 0x0029F3B1 File Offset: 0x0029D5B1
		internal bool Contains(string word)
		{
			return this.badWords.Contains(word);
		}

		// Token: 0x0600BD56 RID: 48470 RVA: 0x0029F3C0 File Offset: 0x0029D5C0
		internal string ToJavaScriptArray()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (object obj in base.List)
			{
				SpellCheckError spellCheckError = (SpellCheckError)obj;
				string suggestionString = spellCheckError.SuggestionString;
				string mistakenWord = spellCheckError.MistakenWord;
				stringBuilder.Append(string.Format("{{textOffset: {0}, isFixed: {1}, wordString: '{2}', suggestionsString: {3}}},", new object[]
				{
					spellCheckError.WordIndex.ToString(),
					"false",
					mistakenWord.Replace("'", "\\'"),
					suggestionString
				}));
			}
			SpellCheckError.RemoveLastChar(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600BD57 RID: 48471 RVA: 0x0029F49C File Offset: 0x0029D69C
		internal string[] GetSuggestions(string word)
		{
			if (!this.Contains(word))
			{
				throw new ArgumentOutOfRangeException("No such error");
			}
			SpellCheckError spellCheckError = (SpellCheckError)this.badWords[word];
			return spellCheckError.Suggestions;
		}

		// Token: 0x0600BD58 RID: 48472 RVA: 0x0029F4D5 File Offset: 0x0029D6D5
		public SpellCheckErrors()
		{
			this.CreateCollections();
		}

		// Token: 0x0600BD59 RID: 48473 RVA: 0x0029F4E3 File Offset: 0x0029D6E3
		internal SpellCheckErrors(bool checkAllCaps)
		{
			this.CreateCollections();
			this.checkAllCaps = checkAllCaps;
		}

		// Token: 0x17003D17 RID: 15639
		public SpellCheckError this[int index]
		{
			get
			{
				return (SpellCheckError)base.List[index];
			}
		}

		// Token: 0x0600BD5B RID: 48475 RVA: 0x0029F50B File Offset: 0x0029D70B
		public int Add(SpellCheckError value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600BD5C RID: 48476 RVA: 0x0029F51C File Offset: 0x0029D71C
		public void AddRange(SpellCheckError[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600BD5D RID: 48477 RVA: 0x0029F544 File Offset: 0x0029D744
		public void AddRange(SpellCheckErrors value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600BD5E RID: 48478 RVA: 0x0029F570 File Offset: 0x0029D770
		public bool Contains(SpellCheckError value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600BD5F RID: 48479 RVA: 0x0029F57E File Offset: 0x0029D77E
		public void CopyTo(SpellCheckError[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600BD60 RID: 48480 RVA: 0x0029F58D File Offset: 0x0029D78D
		public int IndexOf(SpellCheckError value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600BD61 RID: 48481 RVA: 0x0029F59B File Offset: 0x0029D79B
		public void Insert(int index, SpellCheckError value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600BD62 RID: 48482 RVA: 0x0029F5AA File Offset: 0x0029D7AA
		public new SpellCheckErrors.SpellCheckErrorEnumerator GetEnumerator()
		{
			return new SpellCheckErrors.SpellCheckErrorEnumerator(this);
		}

		// Token: 0x0600BD63 RID: 48483 RVA: 0x0029F5B2 File Offset: 0x0029D7B2
		public void Remove(SpellCheckError value)
		{
			base.List.Remove(value);
		}

		// Token: 0x040031CE RID: 12750
		private Hashtable badWords;

		// Token: 0x040031CF RID: 12751
		private readonly bool checkAllCaps;

		// Token: 0x020011E6 RID: 4582
		public class SpellCheckErrorEnumerator : IEnumerator
		{
			// Token: 0x0600BD64 RID: 48484 RVA: 0x0029F5C0 File Offset: 0x0029D7C0
			public SpellCheckErrorEnumerator(SpellCheckErrors mappings)
			{
				this.baseEnumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x17003D18 RID: 15640
			// (get) Token: 0x0600BD65 RID: 48485 RVA: 0x0029F5E1 File Offset: 0x0029D7E1
			public SpellCheckError Current
			{
				get
				{
					return (SpellCheckError)this.baseEnumerator.Current;
				}
			}

			// Token: 0x17003D19 RID: 15641
			// (get) Token: 0x0600BD66 RID: 48486 RVA: 0x0029F5F3 File Offset: 0x0029D7F3
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			// Token: 0x0600BD67 RID: 48487 RVA: 0x0029F600 File Offset: 0x0029D800
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600BD68 RID: 48488 RVA: 0x0029F60D File Offset: 0x0029D80D
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600BD69 RID: 48489 RVA: 0x0029F61A File Offset: 0x0029D81A
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0600BD6A RID: 48490 RVA: 0x0029F627 File Offset: 0x0029D827
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x040031D0 RID: 12752
			private readonly IEnumerator baseEnumerator;
		}
	}
}
