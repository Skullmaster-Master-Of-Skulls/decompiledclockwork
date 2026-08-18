using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200003D RID: 61
	public class AstTreeRuleReturnScope<TOutputTree, TInputTree> : TreeRuleReturnScope<TInputTree>, IAstRuleReturnScope<TOutputTree>, IAstRuleReturnScope, IRuleReturnScope
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007AF3 File Offset: 0x00005CF3
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00007AFB File Offset: 0x00005CFB
		public TOutputTree Tree
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007B04 File Offset: 0x00005D04
		object IAstRuleReturnScope.Tree
		{
			get
			{
				return this.Tree;
			}
		}

		// Token: 0x0400008E RID: 142
		private TOutputTree _tree;
	}
}
