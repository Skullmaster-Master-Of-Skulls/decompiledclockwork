using System;
using System.ComponentModel;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200005C RID: 92
	internal class CssToken
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001A57E File Offset: 0x0001877E
		public TokenType TokenType
		{
			get
			{
				return this.m_tokenType;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001A586 File Offset: 0x00018786
		public string Text
		{
			get
			{
				return this.m_text;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001A58E File Offset: 0x0001878E
		public CssContext Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001A596 File Offset: 0x00018796
		public CssToken(TokenType tokenType, [Localizable(false)] string text, CssContext context)
		{
			this.m_tokenType = tokenType;
			this.m_text = text;
			this.m_context = context.Clone();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001A5B8 File Offset: 0x000187B8
		public CssToken(TokenType tokenType, char ch, CssContext context) : this(tokenType, new string(ch, 1), context)
		{
		}

		// Token: 0x040001EC RID: 492
		private TokenType m_tokenType;

		// Token: 0x040001ED RID: 493
		private string m_text;

		// Token: 0x040001EE RID: 494
		private CssContext m_context;
	}
}
