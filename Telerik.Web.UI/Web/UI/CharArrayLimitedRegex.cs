using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B0A RID: 2826
	internal class CharArrayLimitedRegex
	{
		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x0018D8B3 File Offset: 0x0018BAB3
		public int MatchStartIndex
		{
			get
			{
				return this._matchStartIndex;
			}
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x060069CB RID: 27083 RVA: 0x0018D8BB File Offset: 0x0018BABB
		public int MatchEndIndex
		{
			get
			{
				return this._matchEndIndex;
			}
		}

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x0018D8C3 File Offset: 0x0018BAC3
		public int MatchLength
		{
			get
			{
				return this._matchLength;
			}
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x060069CD RID: 27085 RVA: 0x0018D8CB File Offset: 0x0018BACB
		protected string Pattern
		{
			get
			{
				return this._pattern;
			}
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x060069CE RID: 27086 RVA: 0x0018D8D3 File Offset: 0x0018BAD3
		// (set) Token: 0x060069CF RID: 27087 RVA: 0x0018D8DB File Offset: 0x0018BADB
		protected int PatternIndex
		{
			get
			{
				return this._patternIndex;
			}
			set
			{
				this._patternIndex = value;
			}
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0018D8E4 File Offset: 0x0018BAE4
		public CharArrayLimitedRegex(string pattern)
		{
			this._pattern = pattern;
			this._patternLength = this._pattern.Length;
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x0018D920 File Offset: 0x0018BB20
		private bool IsSpace(char character)
		{
			char[] array = new char[]
			{
				' ',
				'\n',
				'\t'
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (character == array[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x0018D956 File Offset: 0x0018BB56
		protected virtual void ResetState()
		{
			this._patternIndex = 0;
			this._matchEndIndex = -1;
			this._matchLength = 0;
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x0018D96D File Offset: 0x0018BB6D
		public CharArrayRegexMatchResult Match(char[] input)
		{
			return this.Match(input, 0);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0018D977 File Offset: 0x0018BB77
		public virtual CharArrayRegexMatchResult Match(char[] input, int startIndex)
		{
			return this.Match(input, startIndex, input.Length - 1);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x0018D988 File Offset: 0x0018BB88
		public virtual CharArrayRegexMatchResult Match(char[] input, int startIndex, int endIndex)
		{
			if (input.Length == 0)
			{
				return CharArrayRegexMatchResult.Pass;
			}
			if (this._resetBeforeNextMatch)
			{
				this.ResetState();
				this._resetBeforeNextMatch = false;
			}
			int i = startIndex;
			if (this._patternIndex == 0)
			{
				while (i <= endIndex)
				{
					if (input[i] == this._pattern[this._patternIndex])
					{
						this._patternIndex++;
						this._matchStartIndex = i++;
						this._matchLength++;
						break;
					}
					i++;
				}
			}
			if (i == endIndex + 1 && this._patternIndex == 0)
			{
				this._resetBeforeNextMatch = true;
				return CharArrayRegexMatchResult.Pass;
			}
			while (i <= endIndex)
			{
				if (this._patternIndex == this._patternLength)
				{
					this._matchEndIndex = i - 1;
					this._resetBeforeNextMatch = true;
					return CharArrayRegexMatchResult.Success;
				}
				if (!this.MatchStep(i, input))
				{
					this._matchEndIndex = i - 1;
					this._resetBeforeNextMatch = true;
					return CharArrayRegexMatchResult.Fail;
				}
				i++;
				this._matchLength++;
			}
			if (this._patternIndex == this._patternLength)
			{
				this._matchEndIndex = i - 1;
				this._resetBeforeNextMatch = true;
				return CharArrayRegexMatchResult.Success;
			}
			return CharArrayRegexMatchResult.InProgress;
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x0018DA90 File Offset: 0x0018BC90
		protected virtual bool MatchStep(int inputIndex, char[] input)
		{
			if (this._pattern[this._patternIndex] == ' ')
			{
				if (!this.IsSpace(input[inputIndex]))
				{
					this._patternIndex++;
					if (input[inputIndex] != this._pattern[this._patternIndex])
					{
						return false;
					}
					this._patternIndex++;
				}
			}
			else
			{
				if (input[inputIndex] != this._pattern[this._patternIndex])
				{
					return false;
				}
				this._patternIndex++;
			}
			return true;
		}

		// Token: 0x04001C9A RID: 7322
		private readonly string _pattern;

		// Token: 0x04001C9B RID: 7323
		private readonly int _patternLength;

		// Token: 0x04001C9C RID: 7324
		private int _patternIndex;

		// Token: 0x04001C9D RID: 7325
		private int _matchStartIndex = -1;

		// Token: 0x04001C9E RID: 7326
		private int _matchEndIndex = -1;

		// Token: 0x04001C9F RID: 7327
		private int _matchLength;

		// Token: 0x04001CA0 RID: 7328
		private bool _resetBeforeNextMatch;
	}
}
