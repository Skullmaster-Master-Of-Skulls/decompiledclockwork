using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200028C RID: 652
	internal class StringTokenizer
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x0010900C File Offset: 0x0010720C
		public StringTokenizer(string src, DoubleStageCharPropertiesTable charTable, bool includeDelims)
		{
			if (src == null || charTable == null)
			{
				throw new ParserException(ParserExceptionType.StringTokenizer, ParserExceptionError.NullTokenOrPattern, OracleScParserErrorStrings.GetErrorString(ParserExceptionError.NullTokenOrPattern));
			}
			this.Tokenize(src, charTable, includeDelims);
			this.m_vCurIdx = ((this.m_vTokensCount > 0) ? 0 : -1);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00109060 File Offset: 0x00107260
		private void Tokenize(string src, DoubleStageCharPropertiesTable charTable, bool includeDelims)
		{
			int i = 0;
			int num = 0;
			int length = src.Length;
			this.m_vTokens = new List<LexerToken>();
			while (i < length)
			{
				if (num == length)
				{
					this.m_vTokens.Add(new LexerToken(src, i, num));
					break;
				}
				char c = src[num];
				if (charTable.Contains(c))
				{
					if (i != num)
					{
						this.m_vTokens.Add(new LexerToken(src, i, num));
					}
					if (includeDelims)
					{
						this.m_vTokens.Add(new LexerToken(src, num, num + 1));
					}
					num = (i = num + 1);
				}
				else
				{
					num++;
				}
			}
			this.m_vTokensCount = this.m_vTokens.Count;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00109100 File Offset: 0x00107300
		public bool HasMoreTokens()
		{
			return this.m_vCurIdx < this.m_vTokensCount;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00109110 File Offset: 0x00107310
		public LexerToken NextToken()
		{
			return this.m_vTokens[this.m_vCurIdx++];
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0010913C File Offset: 0x0010733C
		public LexerToken PeekNextToken()
		{
			if (this.m_vCurIdx < this.m_vTokensCount)
			{
				return this.m_vTokens[this.m_vCurIdx];
			}
			return null;
		}

		// Token: 0x04001B7E RID: 7038
		private List<LexerToken> m_vTokens;

		// Token: 0x04001B7F RID: 7039
		private int m_vCurIdx = -1;

		// Token: 0x04001B80 RID: 7040
		private int m_vTokensCount = -1;
	}
}
