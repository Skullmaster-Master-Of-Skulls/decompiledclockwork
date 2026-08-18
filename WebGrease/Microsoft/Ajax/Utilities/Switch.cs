using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C5 RID: 197
	public sealed class Switch : AstNode
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00040930 File Offset: 0x0003EB30
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00040977 File Offset: 0x0003EB77
		public AstNode Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_expression = value;
				this.m_expression.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000409B0 File Offset: 0x0003EBB0
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000409F7 File Offset: 0x0003EBF7
		public AstNodeList Cases
		{
			get
			{
				return this.m_cases;
			}
			set
			{
				this.m_cases.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_cases = value;
				this.m_cases.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00040A30 File Offset: 0x0003EC30
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00040A38 File Offset: 0x0003EC38
		public bool BraceOnNewLine { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00040A41 File Offset: 0x0003EC41
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x00040A49 File Offset: 0x0003EC49
		public Context BraceContext { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00040A52 File Offset: 0x0003EC52
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00040A5A File Offset: 0x0003EC5A
		public ActivationObject BlockScope { get; set; }

		// Token: 0x06000D70 RID: 3440 RVA: 0x00040A63 File Offset: 0x0003EC63
		public Switch(Context context) : base(context)
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00040A6C File Offset: 0x0003EC6C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00040A78 File Offset: 0x0003EC78
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Expression, this.Cases, null, null);
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00040A90 File Offset: 0x0003EC90
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Expression == oldNode)
			{
				this.Expression = newNode;
				return true;
			}
			if (this.Cases == oldNode)
			{
				AstNodeList astNodeList = newNode as AstNodeList;
				if (newNode == null || astNodeList != null)
				{
					this.Cases = astNodeList;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400053D RID: 1341
		private AstNode m_expression;

		// Token: 0x0400053E RID: 1342
		private AstNodeList m_cases;
	}
}
