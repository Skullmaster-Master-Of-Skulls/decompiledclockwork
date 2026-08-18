using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200002C RID: 44
	internal class ObxmlToken
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000CE8C File Offset: 0x0000B08C
		internal ObxmlToken(ulong tokenId, string tokenName, TokenTypes tokenType = TokenTypes.ElementToken)
		{
			this.TokenId = tokenId;
			this.TokenName = tokenName;
			this.TokenType = tokenType;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		internal ObxmlToken(ulong tokenId, ulong namespaceId, string tokenName, TokenTypes tokenType = TokenTypes.ElementToken)
		{
			this.TokenId = tokenId;
			this.NamespaceId = namespaceId;
			this.TokenName = tokenName;
			this.TokenType = tokenType;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		internal ulong TokenId { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		internal ulong NamespaceId { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000CF04 File Offset: 0x0000B104
		internal string TokenName { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000CF10 File Offset: 0x0000B110
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000CF18 File Offset: 0x0000B118
		internal string Uri { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000CF24 File Offset: 0x0000B124
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000CF2C File Offset: 0x0000B12C
		internal TokenTypes TokenType { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000CF38 File Offset: 0x0000B138
		internal bool IsAttribute
		{
			get
			{
				return this.TokenType == TokenTypes.AttributeToken;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal bool IsNamespaceToken
		{
			get
			{
				return this.TokenType == TokenTypes.NamespaceToken;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000CF50 File Offset: 0x0000B150
		internal bool IsPrefixToken
		{
			get
			{
				return this.TokenType == TokenTypes.PrefixToken;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000CF5C File Offset: 0x0000B15C
		internal bool IsElementToken
		{
			get
			{
				return this.TokenType == TokenTypes.ElementToken;
			}
		}
	}
}
