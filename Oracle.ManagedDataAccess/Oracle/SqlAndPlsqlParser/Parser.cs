using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000265 RID: 613
	internal abstract class Parser
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x001041EC File Offset: 0x001023EC
		public Parser()
		{
		}

		// Token: 0x060018A7 RID: 6311
		public abstract ParseNode Parse(string scriptText, List<LexerToken> src);

		// Token: 0x02000266 RID: 614
		protected class ParsingHelper
		{
			// Token: 0x060018A8 RID: 6312 RVA: 0x001041F4 File Offset: 0x001023F4
			public ParsingHelper(string scriptText, List<LexerToken> lexerTokens)
			{
				this.m_vScriptText = scriptText;
				this.m_vLexerTokens = lexerTokens;
			}

			// Token: 0x04001B01 RID: 6913
			public List<LexerToken> m_vLexerTokens;

			// Token: 0x04001B02 RID: 6914
			public string m_vScriptText;
		}
	}
}
