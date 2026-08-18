using System;

namespace Antlr.Runtime
{
	// Token: 0x0200000D RID: 13
	public class AstParserRuleReturnScope<TTree, TToken> : ParserRuleReturnScope<TToken>, IAstRuleReturnScope<TTree>, IAstRuleReturnScope, IRuleReturnScope
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000025AD File Offset: 0x000007AD
		public TTree Tree
		{
			get
			{
				return this._tree;
			}
			set
			{
				this._tree = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000025B6 File Offset: 0x000007B6
		object IAstRuleReturnScope.Tree
		{
			get
			{
				return this.Tree;
			}
		}

		// Token: 0x0400000F RID: 15
		private TTree _tree;
	}
}
