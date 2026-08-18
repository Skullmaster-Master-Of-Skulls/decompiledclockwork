using System;
using System.IO;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000B0B RID: 2827
	internal class CharArrayNameBoundaryRegex : CharArrayLimitedRegex, IDisposable
	{
		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x060069D7 RID: 27095 RVA: 0x0018DB1A File Offset: 0x0018BD1A
		public char[] Name
		{
			get
			{
				return new UTF8Encoding().GetChars(this.NameStream.ToArray());
			}
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x060069D8 RID: 27096 RVA: 0x0018DB31 File Offset: 0x0018BD31
		public int NameStartIndex
		{
			get
			{
				return this._nameStartIndex;
			}
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x060069D9 RID: 27097 RVA: 0x0018DB39 File Offset: 0x0018BD39
		public int NameEndIndex
		{
			get
			{
				return this._nameEndIndex;
			}
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x060069DA RID: 27098 RVA: 0x0018DB41 File Offset: 0x0018BD41
		public int NameStartArrayIndex
		{
			get
			{
				return this._nameStartArrayIndex;
			}
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x060069DB RID: 27099 RVA: 0x0018DB49 File Offset: 0x0018BD49
		public int NameEndArrayIndex
		{
			get
			{
				return this._nameEndArrayIndex;
			}
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x0018DB51 File Offset: 0x0018BD51
		public CharArrayNameBoundaryRegex(string pattern) : base(pattern)
		{
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x060069DD RID: 27101 RVA: 0x0018DB76 File Offset: 0x0018BD76
		private MemoryStream NameStream
		{
			get
			{
				if (this._nameStream == null)
				{
					this._nameStream = new MemoryStream();
				}
				return this._nameStream;
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x0018DB94 File Offset: 0x0018BD94
		private StreamWriter NameStreamWriter
		{
			get
			{
				if (this._nameStreamWriter == null)
				{
					this._nameStreamWriter = new StreamWriter(this.NameStream)
					{
						AutoFlush = true
					};
				}
				return this._nameStreamWriter;
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x0018DBC9 File Offset: 0x0018BDC9
		private bool IsNameChar(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z') || (character >= '0' && character <= '9') || character == '.' || character == '_' || character == '-';
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x0018DBFC File Offset: 0x0018BDFC
		public override CharArrayRegexMatchResult Match(char[] input, int startIndex, int endIndex)
		{
			this._matchArraySpan++;
			CharArrayRegexMatchResult charArrayRegexMatchResult = base.Match(input, startIndex, endIndex);
			if (charArrayRegexMatchResult == CharArrayRegexMatchResult.Fail || charArrayRegexMatchResult == CharArrayRegexMatchResult.Pass)
			{
				this._boundarySearchStarted = false;
				this._matchArraySpan = 0;
				this._nameStartArrayIndex = -1;
				this._nameStartIndexFound = false;
			}
			else if (charArrayRegexMatchResult == CharArrayRegexMatchResult.Success)
			{
				this._matchArraySpan = 0;
			}
			return charArrayRegexMatchResult;
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x0018DC54 File Offset: 0x0018BE54
		protected override bool MatchStep(int inputIndex, char[] input)
		{
			if (base.Pattern[base.PatternIndex] != '^')
			{
				return base.MatchStep(inputIndex, input);
			}
			char c = base.Pattern[base.PatternIndex + 1];
			if (input[inputIndex] == c)
			{
				if (!this._boundarySearchStarted)
				{
					this._boundarySearchStarted = true;
				}
				else
				{
					this._boundarySearchStarted = false;
					this._nameEndArrayIndex = this._matchArraySpan - 1;
					base.PatternIndex += 2;
				}
				return true;
			}
			if (this.IsNameChar(input[inputIndex]))
			{
				if (!this._nameStartIndexFound)
				{
					this._nameStartIndex = inputIndex;
					this._nameStartArrayIndex = this._matchArraySpan - 1;
					this._nameStartIndexFound = true;
				}
				this.NameStreamWriter.Write(input[inputIndex]);
				this._nameEndIndex = inputIndex;
				return true;
			}
			return false;
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x0018DD1C File Offset: 0x0018BF1C
		protected override void ResetState()
		{
			base.ResetState();
			this._nameStartIndexFound = false;
			this._nameStartIndex = -1;
			this._nameStartArrayIndex = -1;
			if (this._nameStreamWriter != null)
			{
				this._nameStreamWriter.Dispose();
				this._nameStreamWriter = null;
			}
			if (this._nameStream != null)
			{
				this._nameStream.Dispose();
				this._nameStream = null;
			}
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0018DD78 File Offset: 0x0018BF78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0018DD87 File Offset: 0x0018BF87
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._nameStream != null)
			{
				this._nameStream.Dispose();
			}
		}

		// Token: 0x04001CA1 RID: 7329
		private bool _boundarySearchStarted;

		// Token: 0x04001CA2 RID: 7330
		private bool _nameStartIndexFound;

		// Token: 0x04001CA3 RID: 7331
		private MemoryStream _nameStream;

		// Token: 0x04001CA4 RID: 7332
		private StreamWriter _nameStreamWriter;

		// Token: 0x04001CA5 RID: 7333
		private int _nameStartIndex = -1;

		// Token: 0x04001CA6 RID: 7334
		private int _nameEndIndex = -1;

		// Token: 0x04001CA7 RID: 7335
		private int _nameStartArrayIndex = -1;

		// Token: 0x04001CA8 RID: 7336
		private int _nameEndArrayIndex = -1;

		// Token: 0x04001CA9 RID: 7337
		private int _matchArraySpan;
	}
}
