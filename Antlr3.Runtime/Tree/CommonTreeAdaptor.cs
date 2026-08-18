using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000048 RID: 72
	public class CommonTreeAdaptor : BaseTreeAdaptor
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000955F File Offset: 0x0000775F
		public override object Create(IToken payload)
		{
			return new CommonTree(payload);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009567 File Offset: 0x00007767
		public override IToken CreateToken(int tokenType, string text)
		{
			return new CommonToken(tokenType, text);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009570 File Offset: 0x00007770
		public override IToken CreateToken(IToken fromToken)
		{
			return new CommonToken(fromToken);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009578 File Offset: 0x00007778
		public override IToken GetToken(object t)
		{
			if (t is CommonTree)
			{
				return ((CommonTree)t).Token;
			}
			return null;
		}
	}
}
