using System;

namespace System.Security.Util
{
	// Token: 0x02000620 RID: 1568
	internal sealed class TokenizerStream
	{
		// Token: 0x0600388C RID: 14476 RVA: 0x000BEBC6 File Offset: 0x000BDBC6
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000BEBF4 File Offset: 0x000BDBF4
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			this.m_currentTokens.m_block[this.m_indexTokens++] = token;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000BEC6C File Offset: 0x000BDC6C
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			this.m_currentStrings.m_block[this.m_indexStrings++] = str;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000BECD4 File Offset: 0x000BDCD4
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000BED04 File Offset: 0x000BDD04
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			return this.m_currentTokens.m_block[this.m_indexTokens++];
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000BED68 File Offset: 0x000BDD68
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000BED84 File Offset: 0x000BDD84
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			return this.m_currentStrings.m_block[this.m_indexStrings++];
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000BEDDB File Offset: 0x000BDDDB
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000BEDE4 File Offset: 0x000BDDE4
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000BEE62 File Offset: 0x000BDE62
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000BEE6C File Offset: 0x000BDE6C
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04001D76 RID: 7542
		private int m_countTokens;

		// Token: 0x04001D77 RID: 7543
		private TokenizerShortBlock m_headTokens;

		// Token: 0x04001D78 RID: 7544
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04001D79 RID: 7545
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x04001D7A RID: 7546
		private int m_indexTokens;

		// Token: 0x04001D7B RID: 7547
		private TokenizerStringBlock m_headStrings;

		// Token: 0x04001D7C RID: 7548
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04001D7D RID: 7549
		private int m_indexStrings;
	}
}
