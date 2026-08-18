using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000610 RID: 1552
	internal abstract class Rule
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x0011AB91 File Offset: 0x00118D91
		protected Rule(OpType opType, Rule.ProcessNodeDelegate nodeProcessDelegate)
		{
			this.m_opType = opType;
			this.m_nodeDelegate = nodeProcessDelegate;
		}

		// Token: 0x06003CFF RID: 15615
		internal abstract bool Match(Node node);

		// Token: 0x06003D00 RID: 15616 RVA: 0x0011ABA7 File Offset: 0x00118DA7
		internal bool Apply(RuleProcessingContext ruleProcessingContext, Node node, out Node newNode)
		{
			return this.m_nodeDelegate(ruleProcessingContext, node, out newNode);
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x0011ABB7 File Offset: 0x00118DB7
		internal OpType RuleOpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x04001711 RID: 5905
		private readonly Rule.ProcessNodeDelegate m_nodeDelegate;

		// Token: 0x04001712 RID: 5906
		private readonly OpType m_opType;

		// Token: 0x02000611 RID: 1553
		// (Invoke) Token: 0x06003D03 RID: 15619
		internal delegate bool ProcessNodeDelegate(RuleProcessingContext context, Node subTree, out Node newSubTree);
	}
}
