using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E2 RID: 226
	internal abstract class Rule
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x0003C460 File Offset: 0x0003A660
		protected Rule(OpType opType, Rule.ProcessNodeDelegate nodeProcessDelegate)
		{
			this.m_opType = opType;
			this.m_nodeDelegate = nodeProcessDelegate;
		}

		// Token: 0x06000CAF RID: 3247
		internal abstract bool Match(Node node);

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003C476 File Offset: 0x0003A676
		internal bool Apply(RuleProcessingContext ruleProcessingContext, Node node, out Node newNode)
		{
			return this.m_nodeDelegate(ruleProcessingContext, node, out newNode);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003C486 File Offset: 0x0003A686
		internal OpType RuleOpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x0400098D RID: 2445
		private Rule.ProcessNodeDelegate m_nodeDelegate;

		// Token: 0x0400098E RID: 2446
		private OpType m_opType;

		// Token: 0x02000496 RID: 1174
		// (Invoke) Token: 0x06003C05 RID: 15365
		internal delegate bool ProcessNodeDelegate(RuleProcessingContext context, Node subTree, out Node newSubTree);
	}
}
