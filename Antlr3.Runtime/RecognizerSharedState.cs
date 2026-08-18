using System;
using System.Collections.Generic;

namespace Antlr.Runtime
{
	// Token: 0x02000033 RID: 51
	public class RecognizerSharedState
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00006D65 File Offset: 0x00004F65
		public RecognizerSharedState()
		{
			this.following = new BitSet[100];
			this._fsp = -1;
			this.lastErrorIndex = -1;
			this.tokenStartCharIndex = -1;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00006D90 File Offset: 0x00004F90
		public RecognizerSharedState(RecognizerSharedState state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			this.following = (BitSet[])state.following.Clone();
			this._fsp = state._fsp;
			this.errorRecovery = state.errorRecovery;
			this.lastErrorIndex = state.lastErrorIndex;
			this.failed = state.failed;
			this.syntaxErrors = state.syntaxErrors;
			this.backtracking = state.backtracking;
			if (state.ruleMemo != null)
			{
				this.ruleMemo = (IDictionary<int, int>[])state.ruleMemo.Clone();
			}
			this.token = state.token;
			this.tokenStartCharIndex = state.tokenStartCharIndex;
			this.tokenStartCharPositionInLine = state.tokenStartCharPositionInLine;
			this.channel = state.channel;
			this.type = state.type;
			this.text = state.text;
		}

		// Token: 0x0400006A RID: 106
		public BitSet[] following;

		// Token: 0x0400006B RID: 107
		[CLSCompliant(false)]
		public int _fsp;

		// Token: 0x0400006C RID: 108
		public bool errorRecovery;

		// Token: 0x0400006D RID: 109
		public int lastErrorIndex;

		// Token: 0x0400006E RID: 110
		public bool failed;

		// Token: 0x0400006F RID: 111
		public int syntaxErrors;

		// Token: 0x04000070 RID: 112
		public int backtracking;

		// Token: 0x04000071 RID: 113
		public IDictionary<int, int>[] ruleMemo;

		// Token: 0x04000072 RID: 114
		public IToken token;

		// Token: 0x04000073 RID: 115
		public int tokenStartCharIndex;

		// Token: 0x04000074 RID: 116
		public int tokenStartLine;

		// Token: 0x04000075 RID: 117
		public int tokenStartCharPositionInLine;

		// Token: 0x04000076 RID: 118
		public int channel;

		// Token: 0x04000077 RID: 119
		public int type;

		// Token: 0x04000078 RID: 120
		public string text;
	}
}
