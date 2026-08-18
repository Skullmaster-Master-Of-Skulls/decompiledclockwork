using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001179 RID: 4473
	internal class GridStringTokenizer : IEnumerable
	{
		// Token: 0x0600B65B RID: 46683 RVA: 0x00281E3C File Offset: 0x0028003C
		public GridStringTokenizer(string source, string delimiter, bool includeDelimiters)
		{
			this.tokens = new ArrayList(10);
			this.StrSource = source;
			this.StrDelimiter = delimiter;
			if (delimiter.Length == 0)
			{
				this.StrDelimiter = " ";
			}
			this._delimitersIncluded = includeDelimiters;
			this.Tokenize();
		}

		// Token: 0x0600B65C RID: 46684 RVA: 0x00281E8A File Offset: 0x0028008A
		public GridStringTokenizer(string source, string delimiter) : this(source, delimiter, false)
		{
		}

		// Token: 0x0600B65D RID: 46685 RVA: 0x00281E95 File Offset: 0x00280095
		public GridStringTokenizer(string source, char[] delimiter) : this(source, new string(delimiter))
		{
		}

		// Token: 0x0600B65E RID: 46686 RVA: 0x00281EA4 File Offset: 0x002800A4
		public GridStringTokenizer(string source) : this(source, "")
		{
		}

		// Token: 0x0600B65F RID: 46687 RVA: 0x00281EB2 File Offset: 0x002800B2
		public GridStringTokenizer() : this("", "")
		{
		}

		// Token: 0x0600B660 RID: 46688 RVA: 0x00281EC4 File Offset: 0x002800C4
		private void TokenizeMultipleDelimiters()
		{
			char[] anyOf = this.StrDelimiter.ToCharArray();
			string text = this.StrSource;
			this._numTokens = 0;
			this.tokens.Clear();
			this.CurrIndex = 0;
			int i = text.IndexOfAny(anyOf);
			if (i < 0 && text.Length > 0)
			{
				this._numTokens = 1;
				this.CurrIndex = 0;
				this.tokens.Add(text);
				this.tokens.TrimToSize();
				return;
			}
			if (i < 0 && text.Length <= 0)
			{
				this._numTokens = 0;
				this.CurrIndex = 0;
				this.tokens.TrimToSize();
				return;
			}
			while (i >= 0)
			{
				if (i == 0)
				{
					if (text.Length >= 1)
					{
						this.tokens.Add(text[i].ToString());
						text = text.Substring(1);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					string text2 = text.Substring(0, i);
					this.tokens.Add(text2);
					this.tokens.Add(text[i].ToString());
					if (text.Length > 1 + text2.Length)
					{
						text = text.Substring(1 + text2.Length);
					}
					else
					{
						text = "";
					}
				}
				i = text.IndexOfAny(anyOf);
			}
			if (text.Length > 0)
			{
				this.tokens.Add(text);
			}
			this.tokens.TrimToSize();
			this._numTokens = this.tokens.Count;
		}

		// Token: 0x0600B661 RID: 46689 RVA: 0x00282048 File Offset: 0x00280248
		private void Tokenize()
		{
			if (this._delimitersIncluded)
			{
				this.TokenizeMultipleDelimiters();
				return;
			}
			string text = this.StrSource;
			this._numTokens = 0;
			this.tokens.Clear();
			this.CurrIndex = 0;
			if (text.IndexOf(this.StrDelimiter) < 0 && text.Length > 0)
			{
				this._numTokens = 1;
				this.CurrIndex = 0;
				this.tokens.Add(text);
				this.tokens.TrimToSize();
				text = "";
			}
			else if (text.IndexOf(this.StrDelimiter) < 0 && text.Length <= 0)
			{
				this._numTokens = 0;
				this.CurrIndex = 0;
				this.tokens.TrimToSize();
			}
			while (text.IndexOf(this.StrDelimiter) >= 0)
			{
				if (text.IndexOf(this.StrDelimiter) == 0)
				{
					if (text.Length > this.StrDelimiter.Length)
					{
						text = text.Substring(this.StrDelimiter.Length);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					string text2 = text.Substring(0, text.IndexOf(this.StrDelimiter));
					this.tokens.Add(text2);
					if (text.Length > this.StrDelimiter.Length + text2.Length)
					{
						text = text.Substring(this.StrDelimiter.Length + text2.Length);
					}
					else
					{
						text = "";
					}
				}
			}
			if (text.Length > 0)
			{
				this.tokens.Add(text);
			}
			this.tokens.TrimToSize();
			this._numTokens = this.tokens.Count;
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x002821EC File Offset: 0x002803EC
		public void NewSource(string newSrc)
		{
			this.StrSource = newSrc;
			this.Tokenize();
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x002821FB File Offset: 0x002803FB
		public void NewDelim(string newDel)
		{
			if (newDel.Length == 0)
			{
				this.StrDelimiter = " ";
			}
			else
			{
				this.StrDelimiter = newDel;
			}
			this.Tokenize();
		}

		// Token: 0x0600B664 RID: 46692 RVA: 0x00282220 File Offset: 0x00280420
		public void NewDelim(char[] newDel)
		{
			string newDel2 = new string(newDel);
			this.NewDelim(newDel2);
		}

		// Token: 0x0600B665 RID: 46693 RVA: 0x0028223B File Offset: 0x0028043B
		public int CountTokens()
		{
			return this.tokens.Count;
		}

		// Token: 0x0600B666 RID: 46694 RVA: 0x00282248 File Offset: 0x00280448
		public bool HasMoreTokens()
		{
			return this.CurrIndex <= this.tokens.Count - 1;
		}

		// Token: 0x0600B667 RID: 46695 RVA: 0x00282264 File Offset: 0x00280464
		public string NextToken()
		{
			if (this.CurrIndex <= this.tokens.Count - 1)
			{
				string result = (string)this.tokens[this.CurrIndex];
				this.CurrIndex++;
				return result;
			}
			return null;
		}

		// Token: 0x17003AF3 RID: 15091
		// (get) Token: 0x0600B668 RID: 46696 RVA: 0x002822B4 File Offset: 0x002804B4
		public string Source
		{
			get
			{
				return this.StrSource;
			}
		}

		// Token: 0x17003AF4 RID: 15092
		// (get) Token: 0x0600B669 RID: 46697 RVA: 0x002822BC File Offset: 0x002804BC
		public string Delim
		{
			get
			{
				return this.StrDelimiter;
			}
		}

		// Token: 0x17003AF5 RID: 15093
		// (get) Token: 0x0600B66A RID: 46698 RVA: 0x002822C4 File Offset: 0x002804C4
		// (set) Token: 0x0600B66B RID: 46699 RVA: 0x002822CC File Offset: 0x002804CC
		public int NumTokens
		{
			get
			{
				return this._numTokens;
			}
			set
			{
				this._numTokens = value;
			}
		}

		// Token: 0x0600B66C RID: 46700 RVA: 0x002822D5 File Offset: 0x002804D5
		public IEnumerator GetEnumerator()
		{
			return new GridStringTokenizer.TokanizerEnumerator(this);
		}

		// Token: 0x0400300A RID: 12298
		private int CurrIndex;

		// Token: 0x0400300B RID: 12299
		private int _numTokens;

		// Token: 0x0400300C RID: 12300
		private ArrayList tokens;

		// Token: 0x0400300D RID: 12301
		private string StrSource;

		// Token: 0x0400300E RID: 12302
		private string StrDelimiter;

		// Token: 0x0400300F RID: 12303
		private readonly bool _delimitersIncluded;

		// Token: 0x0200117A RID: 4474
		private class TokanizerEnumerator : IEnumerator
		{
			// Token: 0x0600B66D RID: 46701 RVA: 0x002822DD File Offset: 0x002804DD
			public TokanizerEnumerator(GridStringTokenizer tokenizer)
			{
				this.tokenizer = tokenizer;
			}

			// Token: 0x0600B66E RID: 46702 RVA: 0x002822F4 File Offset: 0x002804F4
			public bool MoveNext()
			{
				this.currentToken = this.tokenizer.NextToken();
				if (this.currentToken != null)
				{
					bool result = this.lastHasMore;
					this.lastHasMore = this.tokenizer.HasMoreTokens();
					return result;
				}
				return this.tokenizer.HasMoreTokens();
			}

			// Token: 0x0600B66F RID: 46703 RVA: 0x0028233F File Offset: 0x0028053F
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17003AF6 RID: 15094
			// (get) Token: 0x0600B670 RID: 46704 RVA: 0x00282346 File Offset: 0x00280546
			public object Current
			{
				get
				{
					return this.currentToken;
				}
			}

			// Token: 0x04003010 RID: 12304
			private bool lastHasMore = true;

			// Token: 0x04003011 RID: 12305
			private GridStringTokenizer tokenizer;

			// Token: 0x04003012 RID: 12306
			private string currentToken;
		}
	}
}
