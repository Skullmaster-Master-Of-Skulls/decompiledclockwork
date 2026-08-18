using System;

namespace System.Web.Compilation
{
	// Token: 0x0200082D RID: 2093
	[Serializable]
	public sealed class LinePragmaCodeInfo
	{
		// Token: 0x060063EF RID: 25583 RVA: 0x000030B5 File Offset: 0x000012B5
		public LinePragmaCodeInfo()
		{
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x0015E114 File Offset: 0x0015C314
		public LinePragmaCodeInfo(int startLine, int startColumn, int startGeneratedColumn, int codeLength, bool isCodeNugget)
		{
			this._startLine = startLine;
			this._startColumn = startColumn;
			this._startGeneratedColumn = startGeneratedColumn;
			this._codeLength = codeLength;
			this._isCodeNugget = isCodeNugget;
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x060063F1 RID: 25585 RVA: 0x0015E141 File Offset: 0x0015C341
		public int StartLine
		{
			get
			{
				return this._startLine;
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x0015E149 File Offset: 0x0015C349
		public int StartColumn
		{
			get
			{
				return this._startColumn;
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x0015E151 File Offset: 0x0015C351
		public int StartGeneratedColumn
		{
			get
			{
				return this._startGeneratedColumn;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x0015E159 File Offset: 0x0015C359
		public int CodeLength
		{
			get
			{
				return this._codeLength;
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x0015E161 File Offset: 0x0015C361
		public bool IsCodeNugget
		{
			get
			{
				return this._isCodeNugget;
			}
		}

		// Token: 0x040033BD RID: 13245
		internal int _startLine;

		// Token: 0x040033BE RID: 13246
		internal int _startColumn;

		// Token: 0x040033BF RID: 13247
		internal int _startGeneratedColumn;

		// Token: 0x040033C0 RID: 13248
		internal int _codeLength;

		// Token: 0x040033C1 RID: 13249
		internal bool _isCodeNugget;
	}
}
