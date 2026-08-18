using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200006D RID: 109
	public class ConditionalCompilationComment : AstNode
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000224AC File Offset: 0x000206AC
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000224F3 File Offset: 0x000206F3
		public Block Statements
		{
			get
			{
				return this.m_statements;
			}
			set
			{
				this.m_statements.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_statements = value;
				this.m_statements.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0002252C File Offset: 0x0002072C
		public ConditionalCompilationComment(Context context) : base(context)
		{
			this.Statements = new Block(context.FlattenToStart());
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00022546 File Offset: 0x00020746
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00022552 File Offset: 0x00020752
		public void Append(AstNode statement)
		{
			if (statement != null)
			{
				base.Context.UpdateWith(statement.Context);
				this.Statements.Append(statement);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00022575 File Offset: 0x00020775
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Statements, null, null, null);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00022585 File Offset: 0x00020785
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Statements == oldNode)
			{
				this.Statements = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x04000279 RID: 633
		private Block m_statements;
	}
}
