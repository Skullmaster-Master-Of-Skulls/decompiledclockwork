using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000098 RID: 152
	public class GroupingOperator : Expression
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00029F07 File Offset: 0x00028107
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00029F4F File Offset: 0x0002814F
		public AstNode Operand
		{
			get
			{
				return this.m_operand;
			}
			set
			{
				this.m_operand.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_operand = value;
				this.m_operand.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00029F88 File Offset: 0x00028188
		public GroupingOperator(Context context) : base(context)
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00029F91 File Offset: 0x00028191
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00029F9D File Offset: 0x0002819D
		public override PrimitiveType FindPrimitiveType()
		{
			if (this.Operand == null)
			{
				return PrimitiveType.Other;
			}
			return this.Operand.FindPrimitiveType();
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00029FB4 File Offset: 0x000281B4
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.Primary;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00029FB8 File Offset: 0x000281B8
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Operand, null, null, null);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00029FC8 File Offset: 0x000281C8
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Operand == oldNode)
			{
				this.Operand = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00029FE0 File Offset: 0x000281E0
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			GroupingOperator groupingOperator = otherNode as GroupingOperator;
			return (groupingOperator != null && this.Operand.IsEquivalentTo(groupingOperator.Operand)) || this.Operand.IsEquivalentTo(otherNode);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0002A020 File Offset: 0x00028220
		public override bool IsConstant
		{
			get
			{
				return this.Operand.IfNotNull((AstNode o) => o.IsConstant);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002A04A File Offset: 0x0002824A
		public override string ToString()
		{
			return '(' + ((this.Operand == null) ? "<null>" : this.Operand.ToString()) + ')';
		}

		// Token: 0x04000347 RID: 839
		private AstNode m_operand;
	}
}
